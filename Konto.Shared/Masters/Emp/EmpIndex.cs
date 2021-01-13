using DevExpress.XtraGrid.Views.Grid;
using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Masters.Dtos;
using Konto.Shared.Masters.Item;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Masters.Emp
{
    public partial class EmpIndex : KontoMetroForm
    {
        private List<EmpModel> FilterView = new List<EmpModel>();
        private List<EmpRateDto> DelTrans = new List<EmpRateDto>();
        public EmpIndex()
        {
            InitializeComponent();

            tabControlAdv1.TabPages[2].TabVisible = false;
            tabControlAdv1.TabPages[3].TabVisible = false;
            gridView1.KeyDown += GridView1_KeyDown;
            this.Load += EmpIndex_Load;
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;
            productRepositoryItemButtonEdit.ButtonClick += ProductRepositoryItemButtonEdit_ButtonClick;
            gridControl1.ProcessGridKey += GridControl1_ProcessGridKey;
        }
        private void GridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                
                var dr = PreOpenLookup();
                if (dr == null) return;
                if (gridView1.FocusedColumn.FieldName == "ProductName")
                {
                    //if (Convert.ToInt32(dr.RefId) > 0) return;
                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.ProductId == 0)
                        {
                            OpenItemLookup(dr.ProductId, dr);
                            // e.Handled = true;
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        OpenItemLookup(dr.ProductId, dr);
                        e.Handled = true;
                    }
                }
               
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Emp GridControl KeyDown");
                MessageBoxAdv.Show(this, "Error Lookup Setup !!", "Exception ", ex.ToString());

            }

        }
        private void ProductRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
                OpenItemLookup(dr.ProductId, dr);

        }
        private EmpRateDto PreOpenLookup()
        {
          
            gridView1.GetRow(gridView1.FocusedRowHandle);
            if (gridView1.GetRow(gridView1.FocusedRowHandle) == null)
            {
                gridView1.AddNewRow();
            }
            var dr = (EmpRateDto)gridView1.GetRow(gridView1.FocusedRowHandle);
            return dr;
        }
        private void OpenItemLookup(int _selvalue, EmpRateDto er)
        {
            var frm = new ProductLkpWindow();

            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Product_Master;
          

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                er.ProductId = frm.SelectedValue;
                er.ProductName = frm.SelectedTex;
                var model = frm.SelectedItem as ProductLookupDto;
               
                gridView1.FocusedColumn = gridView1.GetNearestCanFocusedColumn(gridView1.FocusedColumn);
            }

        }
        private void GridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete) return;

            if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
            {
                if (MessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo) !=
                  DialogResult.Yes)
                    return;
                GridView view = sender as GridView;
                var row = view.GetRow(view.FocusedRowHandle) as EmpRateDto;
                view.DeleteRow(view.FocusedRowHandle);
                DelTrans.Add(row);
            }
           
            
        }
        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                nameTextBox.Focus();
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as EmpListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new EmpListView();
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

                Log.Error(ex, "Emp Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private void EmpIndex_Load(object sender, EventArgs e)
        {
            try
            {
                NewRec();

                this.ActiveControl = nameTextBox;
               

                if (this.PrimaryKey == 0)
                {
                    toggleSwitch1.Enabled = false;
                }

            }
            catch (Exception ex)
            {

                Log.Error(ex, "Emp Load");
                MessageBox.Show(ex.ToString());
            }
        }
        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(nameTextBox.Text) || nameTextBox.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid Salesman Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nameTextBox.Focus();
                return false;
            }
           


            using (var db = new KontoContext())
            {
                var find = db.Emps.FirstOrDefault(
                   x => x.EmpName == nameTextBox.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "Salesman Name Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nameTextBox.Focus();
                    return false;
                }
            }

            return true;
        }

        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<EmpModel>();
            this.Text = "Salesman [Add New]";
            this.ActiveControl = nameTextBox; ;
            empRateDtoBindingSource.DataSource = new List<EmpRateDto>();
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
        }
        public override void ResetPage()
        {
            base.ResetPage();
            nameTextBox.Clear();
            contactNoTextBox.Clear();
            address1TextBoxExt.Clear();
            address2TextBoxExt.Clear();
            aadharTrextBoxExt.Clear();
            panNoTextBoxExt.Clear();
            emailTextBoxExt.Clear();
            remarkTextBox.Clear();
            toggleSwitch1.EditValue = true;
            toggleSwitch1.Enabled = false;
            empRateDtoBindingSource.DataSource = new List<EmpRateDto>();
        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var model = db.Emps.Find(_key);
                LoadData(model);
            }

        }
        private void LoadData(EmpModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            nameTextBox.Text = model.EmpName;
            contactNoTextBox.Text = model.MobileNo;
            remarkTextBox.Text = model.Remark;
            address1TextBoxExt.Text = model.Address1;
            address2TextBoxExt.Text = model.Address2;
            aadharTrextBoxExt.Text = model.AadharNo;
            panNoTextBoxExt.Text = model.PanNo;
            emailTextBoxExt.Text = model.Email;
            toggleSwitch1.EditValue = model.IsActive;
            toggleSwitch1.Enabled = true;
            nameTextBox.Focus();
            
            using(var db = new KontoContext())
            {
                var lst = (from r in db.EmpRates
                           join p in db.Products on r.ProductId equals p.Id
                           where r.EmpId == model.Id
                           select new EmpRateDto
                           {
                               EmpId = r.EmpId,
                               Id = r.Id,
                               ProductId = r.ProductId,
                               ProductName = p.ProductName,
                               Rate = r.Rate,
                               Remarks = r.Remarks
                           }
                           ).ToList();

                empRateDtoBindingSource.DataSource = lst;
            }
            
            this.Text = "Salesman [View/Modify]";




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
            EmpModel _find = new EmpModel();

            if (!string.IsNullOrWhiteSpace(nameTextBox.Text.Trim()))
                filter.Add(new Filter { PropertyName = "EmpName", Operation = Op.Contains, Value = nameTextBox.Text.Trim() });

            

            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });


            using (var db = new KontoContext())
            {
                FilterView = db.Emps.Where(ExpressionBuilder.GetExpression<EmpModel>(filter))
                    .OrderBy(x => x.EmpName).ToList();
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
            EmpModel model = new EmpModel();
            using (var db = new KontoContext())
            {
                using(var _tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (this.PrimaryKey != 0)
                            model = db.Emps.Find(this.PrimaryKey);

                        model.EmpName = nameTextBox.Text.Trim();
                        model.MobileNo = contactNoTextBox.Text.Trim();
                        model.Remark = remarkTextBox.Text.Trim();
                        model.Address1 = address1TextBoxExt.Text.Trim();
                        model.IsActive = Convert.ToBoolean(toggleSwitch1.EditValue);
                        model.Address2 = address2TextBoxExt.Text.Trim();
                        model.AadharNo = aadharTrextBoxExt.Text.Trim();
                        model.PanNo = panNoTextBoxExt.Text.Trim();
                        model.Email = emailTextBoxExt.Text.Trim();
                        model.CompId = KontoGlobals.CompanyId;
                        if (this.PrimaryKey == 0)
                        {
                            // model.RowId = Guid.NewGuid();
                            db.Emps.Add(model);
                        }
                        db.SaveChanges();


                        foreach (var item in DelTrans)
                        {
                            if (item.Id == 0) continue;
                            var er = db.EmpRates.Find(item.Id);
                            if (er != null)
                                er.IsDeleted = true;
                        }

                        var lsts = empRateDtoBindingSource.DataSource as List<EmpRateDto>;
                        foreach (var item in lsts)
                        {
                            var er = new EmpRate();
                            if (item.Id != 0)
                                er = db.EmpRates.Find(item.Id);

                            er.EmpId = model.Id;
                            er.ProductId = item.ProductId;
                            er.Rate = item.Rate;
                            er.Remarks = item.Remarks;

                            if (er.Id == 0)
                                db.EmpRates.Add(er);
                        }


                        db.SaveChanges();

                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Emp save");
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
                    nameTextBox.Focus();
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
