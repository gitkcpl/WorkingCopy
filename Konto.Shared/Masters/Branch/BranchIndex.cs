using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Shared.Masters.Branch
{
    public partial class BranchIndex : KontoMetroForm
    {
        private List<BranchModel> FilterView = new List<BranchModel>();
        public BranchIndex()
        {
            InitializeComponent();

            this.FirstActiveControl = branchNameTextBox;

            tabControlAdv1.TabPages[2].TabVisible = false;
            tabControlAdv1.TabPages[3].TabVisible = false;
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;
        }

        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                branchNameTextBox.Focus();
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as BranchListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new BranchListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);

            }
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveDataAsync(true);
            }
            catch (Exception ex)
            {

                Log.Error(ex, "State Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(branchNameTextBox.Text) || branchNameTextBox.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid Branch Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                branchNameTextBox.Clear();
                branchNameTextBox.Focus();
                return false;
            }
           
            using (var db = new KontoContext())
            {
                var find = db.Branches.FirstOrDefault(
                   x => x.BranchName == branchNameTextBox.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "Branch Name Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    branchNameTextBox.Focus();
                    return false;
                }
            }

            return true;
        }

        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<BranchModel>();
            this.Text = "Branch Master [Add New]";
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
        }

        private void BranchIndex_Load(object sender, EventArgs e)
        {
            try
            {
                NewRec();

                this.ActiveControl = branchNameTextBox;

                if (this.PrimaryKey == 0)
                {
                    toggleSwitch1.Enabled = false;
                }

            }
            catch (Exception ex)
            {

                Log.Error(ex, "div Load");
                MessageBox.Show(ex.ToString());
            }
        }

        public override void ResetPage()
        {
            base.ResetPage();
            branchNameTextBox.Clear();
            codeTextBoxExt.Clear();
            address1TextBoxExt.Clear();
            address2TextBoxExt.Clear();
            cityLookup1.SetEmpty();
            areaLookup1.SetEmpty();
            pinCodeTextBox.Clear();
            gstinTextBox.Clear();
            toggleSwitch1.EditValue = true;
            toggleSwitch1.Enabled = false;
            
        }

        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var model = db.Branches.Find(_key);
                LoadData(model);
            }

        }

        private void LoadData(BranchModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            branchNameTextBox.Text = model.BranchName;
            cityLookup1.SelectedValue = model.CityId;
            cityLookup1.SetCity();
            toggleSwitch1.EditValue = model.IsActive;
            toggleSwitch1.Enabled = true;
            codeTextBoxExt.Text = model.BranchCode;
            address1TextBoxExt.Text = model.Address1;
            address2TextBoxExt.Text = model.Address2;
            areaLookup1.SelectedValue = model.AreaId;
            areaLookup1.SetArea();
            pinCodeTextBox.Text = model.PinCode;
            gstinTextBox.Text = model.GstIn;
            branchNameTextBox.Focus();
            this.Text = "Branch Master [View/Modify]";
            createdLabelControl.Text = "Created By: " + model.CreateUser + " [ " + model.CreateDate + " ]";
            modifyLabelControl.Text = "Modified By: " + model.ModifyUser + " [ " + model.ModifyDate ?? string.Empty + " ]";
        }
        public override void FirstRec()
        {
            base.FirstRec();
            var model = FilterView[RecordNo];
            LoadData(model);
        }
        public override void NextRec()
        {
            base.NextRec();

            LoadData(FilterView[this.RecordNo]);

        }
        public override void PrevRec()
        {
            base.PrevRec();

            LoadData(FilterView[this.RecordNo]);
        }
        public override void LastRec()
        {
            base.LastRec();
            LoadData(FilterView[this.RecordNo]);
        }
        public override void FindRec()
        {
            List<Filter> filter = new List<Filter>();
            BranchModel _find = new BranchModel();
            if (!string.IsNullOrWhiteSpace(branchNameTextBox.Text.Trim()))
                filter.Add(new Filter { PropertyName = "BranchName", Operation = Op.Contains, Value = branchNameTextBox.Text.Trim() });

          
            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });
           // filter.Add(new Filter { PropertyName = "CompId", Operation = Op.Equals, Value = KontoGlobals.CompanyId });

            using (var db = new KontoContext())
            {
                FilterView = db.Branches.Where(ExpressionBuilder.GetExpression<BranchModel>(filter)).ToList();
                if (FilterView.Count == 0)
                {
                    MessageBoxAdv.Show(this, "No Record Found", "Find !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ResetPage();
                    return;
                }
                this.TotalRecord = FilterView.Count;
                this.RecordNo = 0;
                LoadData(this.FilterView[0]);

            }

        }

        public override void SaveDataAsync(bool newmode)
        {

            bool IsSaved = false;
            if (!ValidateData()) return;
            BranchModel model = new BranchModel();
            
            using (var db = new KontoContext())
            {
                using(var _tran = db.Database.BeginTransaction())
                {
                    try
                    {

                        if (this.PrimaryKey != 0)
                            model = db.Branches.Find(this.PrimaryKey);
                        model.BranchName = branchNameTextBox.Text.Trim();
                        model.BranchCode = codeTextBoxExt.Text.Trim();
                        model.Address1 = address1TextBoxExt.Text.Trim();
                        model.Address2 = address2TextBoxExt.Text.Trim();
                        model.CompId = KontoGlobals.CompanyId;

                        if (Convert.ToInt32(cityLookup1.SelectedValue) != 0)
                            model.CityId = Convert.ToInt32(cityLookup1.SelectedValue);
                        else
                            model.CityId = null;


                        if (Convert.ToInt32(areaLookup1.SelectedValue) != 0)
                            model.AreaId = Convert.ToInt32(areaLookup1.SelectedValue);
                        else
                            model.AreaId = null;

                        model.PinCode = pinCodeTextBox.Text.Trim();
                        model.GstIn = gstinTextBox.Text.Trim();
                        model.IsActive = Convert.ToBoolean(toggleSwitch1.EditValue);

                        if (this.PrimaryKey == 0)
                        {
                            // model.RowId = Guid.NewGuid();
                            db.Branches.Add(model);
                        }
                        db.SaveChanges();


                        var complist = db.Companies.Where(p => p.IsActive && !p.IsDeleted).ToList();
                        var yearlist = db.FinYears.Where(x => x.IsActive == true && x.IsDeleted == false).ToList();
                        var pds = db.Products.Where(x => x.IsDeleted == false).ToList();
                        foreach (var item in pds)
                        {

                            foreach (var comp in complist)
                            {
                                foreach (var yr in yearlist)
                                {
                                    StockBalModel _model = new StockBalModel();
                                    _model = db.StockBals.FirstOrDefault(x => x.CompanyId == comp.Id && x.YearId == yr.Id && x.BranchId == model.Id
                                                  && x.ProductId == item.Id);

                                    if (_model == null)
                                    {
                                        _model = new StockBalModel();

                                        _model.ProductId = item.Id;
                                        _model.ItemCode = item.RowId;

                                        _model.CompanyId = comp.Id;
                                        _model.YearId = yr.Id;
                                        _model.BranchId = model.Id;
                                        _model.GodownId = KontoGlobals.GodownId;


                                        _model.BalQty = 0;
                                        _model.RowId = Guid.NewGuid();
                                        _model.CreateUser = KontoGlobals.UserName;
                                        _model.CreateDate = DateTime.Now;
                                        _model.OpNos = 0;
                                        _model.OpQty = 0;

                                        db.StockBals.Add(_model);
                                    }
                                }

                            }
                        }

                        db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        MessageBox.Show(ex.ToString());

                    }
                }
               

            }
            if (IsSaved)
            {
                NewRec();
                base.SaveDataAsync(newmode);
                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (!this.OpenForLookup && newmode)
                {
                    this.ResetPage();
                    branchNameTextBox.Focus();
                }
                else
                {
                    this.Close();
                    this.Dispose();
                }
            }
        }
    }
}
