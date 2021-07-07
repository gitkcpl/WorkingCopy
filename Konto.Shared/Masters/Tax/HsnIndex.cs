using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Gstn;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Masters.Tax
{
    public partial class HsnIndex : KontoMetroForm
    {
        private List<HsnMaster> FilterView = new List<HsnMaster>();
        private List<HsnTransDto> DelTrans = new List<HsnTransDto>();
        public HsnIndex()
        {
            InitializeComponent();
            tabControlAdv1.TabPages[2].TabVisible = false;
            tabControlAdv1.TabPages[3].TabVisible = false;

            this.Load += CategoryIndex_Load;
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

           

            this.FirstActiveControl = taxNameTextBox;
        }

        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                taxNameTextBox.Focus();
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as TaxListView;
                _list.ActiveControl = _list.KontoGrid;
                this.Text = "Hsn Master [View]";
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new TaxListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Hsn Master [View]";
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

                Log.Error(ex, "Hsn Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private void CategoryIndex_Load(object sender, EventArgs e)
        {
            try
            {
                NewRec();

                this.ActiveControl = taxNameTextBox;

                if (this.PrimaryKey == 0)
                {
                    toggleSwitch1.Enabled = false;
                }

                using(var db = new KontoContext())
                {
                    var slabs = db.TaxMasters.Where(x => !x.IsDeleted)
                                .OrderBy(x => x.TaxName)
                                .Select(x => new BaseLookupDto { DisplayText = x.TaxName, Id = x.Id }).ToList();
                    uomRepositoryItemLookUpEdit.DataSource = slabs;
                }

            }
            catch (Exception ex)
            {

                Log.Error(ex, "Tax Load");
                MessageBox.Show(ex.ToString());
            }
        }
        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(taxNameTextBox.Text) || taxNameTextBox.Text.Length <= 3)
            {
                MessageBoxAdv.Show(this, "Invalid Hsn Code", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                taxNameTextBox.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(descrTextBox.Text.Trim()))
            {
                MessageBoxAdv.Show(this, "Invalid Hsn Description", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                descrTextBox.Focus();
                return false;
            }


            var trans = hsnTransDtoBindingSource.DataSource as List<HsnTransDto>;
            if (trans.Count == 0)
            {
                MessageBoxAdv.Show(this, "Empty Tax Slab..", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridControl1.Focus();
                return false;
            }

            using (var db = new KontoContext())
            {
                var find = db.HsnMasters.FirstOrDefault(
                   x => x.HsnCode == taxNameTextBox.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "hsn Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    taxNameTextBox.Focus();
                    return false;
                }
            }

            return true;
        }

        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<HsnMaster>();
            this.Text = "Hsn Master [Add New]";
            this.ActiveControl = taxNameTextBox;

            hsnTransDtoBindingSource.DataSource = new List<HsnTransDto>();
            gridControl1.RefreshDataSource();
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
        }
        public override void ResetPage()
        {
            base.ResetPage();
            taxNameTextBox.Clear();
            descrTextBox.Clear();
            toggleSwitch1.EditValue = true;
            toggleSwitch1.Enabled = false;
            hsnTransDtoBindingSource.DataSource = new List<HsnTransDto>();
            gridControl1.RefreshDataSource();
        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var model = db.HsnMasters.Find(_key);
                LoadData(model);
            }

        }
        private void LoadData(HsnMaster model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            taxNameTextBox.Text = model.HsnCode;
            descrTextBox.Text = model.HsnDescr;
           
            toggleSwitch1.EditValue = model.IsActive;
            toggleSwitch1.Enabled = true;
            taxNameTextBox.Focus();
            this.Text = "Hsn Master [View/Modify]";

            using(var db = new KontoContext())
            {
                var lsts = db.HsnTrans.Where(x => x.HsnMasterId == model.Id)
                    .Select(x => new HsnTransDto { Id = x.Id, ApplyDate = x.ApplyDate, MasterId = x.HsnMasterId, TaxMasterId = x.TaxMasterId }).ToList();
                hsnTransDtoBindingSource.DataSource = lsts;
            }
            gridControl1.RefreshDataSource();

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
            HsnMaster _find = new HsnMaster();

            if (!string.IsNullOrWhiteSpace(taxNameTextBox.Text.Trim()))
                filter.Add(new Filter { PropertyName = "HsnCode", Operation = Op.Contains, Value = taxNameTextBox.Text.Trim() });

         

            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });


            using (var db = new KontoContext())
            {
                FilterView = db.HsnMasters.Where(ExpressionBuilder.GetExpression<HsnMaster>(filter))
                    .OrderBy(x => x.HsnCode).ToList();
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
            HsnMaster model = new HsnMaster();
            using (var db = new KontoContext())
            {
                using (var _trn = db.Database.BeginTransaction()) {
                    try
                    {
                        if (this.PrimaryKey != 0)
                            model = db.HsnMasters.Find(this.PrimaryKey);

                        model.HsnCode = taxNameTextBox.Text.Trim();

                        model.IsActive = Convert.ToBoolean(toggleSwitch1.EditValue);

                        if (this.PrimaryKey == 0)
                        {
                            // model.RowId = Guid.NewGuid();
                            db.HsnMasters.Add(model);
                        }
                        db.SaveChanges();

                        var trans = hsnTransDtoBindingSource.DataSource as List<HsnTransDto>;
                        foreach (var item in trans)
                        {
                            var tran = new HsnTrans();
                            if (item.Id > 0)
                                tran = db.HsnTrans.Find(item.Id);
                            tran.TaxMasterId = item.TaxMasterId;
                            tran.HsnMasterId = model.Id;
                            tran.ApplyDate = item.ApplyDate;

                            if (item.Id == 0)
                                db.HsnTrans.Add(tran);
                        }

                        foreach (var item in DelTrans)
                        {
                            if (item.Id == 0) continue;
                            var find = db.HsnTrans.Find(item.Id);
                            db.HsnTrans.Remove(find);
                        }

                        db.SaveChanges();
                        _trn.Commit();

                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _trn.Rollback();
                        Log.Error(ex, "hsn trans");
                       
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
                    taxNameTextBox.Focus();
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
