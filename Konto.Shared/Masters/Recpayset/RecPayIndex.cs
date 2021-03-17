using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Masters.Recpayset
{
    public partial class RecPayIndex : KontoMetroForm
    {
        private List<RPSetModel> FilterView = new List<RPSetModel>();
        public RecPayIndex()
        {
            InitializeComponent();
            tabControlAdv1.TabPages[2].TabVisible = false;
            tabControlAdv1.TabPages[3].TabVisible = false;

            this.Load += GroupIndex_Load;
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            this.FirstActiveControl = typeLookUpEdit;

            List<ComboBoxPairs> rpc = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Receipt","R"),
                new ComboBoxPairs("Payment", "P"),

            };
            typeLookUpEdit.Properties.DataSource = rpc;
            List<ComboBoxPairs> fc = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("AddLess1", "Adl1"),
                new ComboBoxPairs("AddLess2", "Adl2"),
                new ComboBoxPairs("AddLess3", "Adl3"),
                new ComboBoxPairs("AddLess4", "Adl4"),
                new ComboBoxPairs("AddLess5", "Adl5"),
                new ComboBoxPairs("AddLess6", "Adl6"),
                new ComboBoxPairs("AddLess7", "Adl7"),
                new ComboBoxPairs("AddLess8", "Adl8"),
                new ComboBoxPairs("AddLess9", "Adl9"),
                new ComboBoxPairs("AddLess10", "Adl10"),

            };

            fieldLookUpEdit1.Properties.DataSource = fc;

            List<ComboBoxPairs> pmc = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("+","P"),
                new ComboBoxPairs("-", "M"),

            };

            addLessLookUpEdit.Properties.DataSource = pmc;
            List<ComboBoxPairs> pm = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("N","N"),
                new ComboBoxPairs("Y", "Y"),

            };

            drCrLookUpEdit.Properties.DataSource = pm;
            using (var db = new KontoContext())
            {
                var model = (from p in db.TaxMasters
                             where !p.IsDeleted && p.IsActive
                             orderby p.TaxName
                             select new BaseLookupDto
                             {
                                 DisplayText = p.TaxName,
                                 Id = p.Id
                             }).ToList();
                taxTypelookUpEdit.Properties.DataSource = model;
            }
        }

        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                typeLookUpEdit.Focus();
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as RecPayListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new RecPayListView();
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

                Log.Error(ex, "Rec/Pay Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private void GroupIndex_Load(object sender, EventArgs e)
        {
            try
            {
                NewRec();

                this.ActiveControl = typeLookUpEdit;

                

            }
            catch (Exception ex)
            {

                Log.Error(ex, "RecPay Load");
                MessageBox.Show(ex.ToString());
            }
        }
        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(typeLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Receipt/Payment Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                typeLookUpEdit.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(fieldLookUpEdit1.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Field Selection", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fieldLookUpEdit1.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(addLessLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Add/Less Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                addLessLookUpEdit.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(perHeadKontoTextBoxExt.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Percentage Heading", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                perHeadKontoTextBoxExt.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(amtHeadingKontoTextBoxExt.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Amount Heading", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                amtHeadingKontoTextBoxExt.Focus();
                return false;
            }
            else if (Convert.ToInt32(accLookup1.SelectedValue)==0)
            {
                MessageBoxAdv.Show(this, "Invalid Ledger Selection", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                accLookup1.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(drCrLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Dr/Cr Note Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                drCrLookUpEdit.Focus();
                return false;
            }
            else if (drCrLookUpEdit.Text == "Y")
            {
                if (string.IsNullOrEmpty(taxTypelookUpEdit.Text))
                {
                    MessageBoxAdv.Show(this, "Invalid Gst Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    taxTypelookUpEdit.Focus();
                    return false;
                }
                else if (string.IsNullOrEmpty(hsnCodeKontoTextBoxExt.Text))
                {
                    MessageBoxAdv.Show(this, "Invalid Hsn Code", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    hsnCodeKontoTextBoxExt.Focus();
                    return false;
                }
                else if (string.IsNullOrEmpty(descrKontoTextBoxExt.Text))
                {
                    MessageBoxAdv.Show(this, "Invalid Cr/dr Note Description", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    descrKontoTextBoxExt.Focus();
                    return false;
                }
                else if (Convert.ToInt32(voucherLookup1.SelectedValue)==0)
                {
                    MessageBoxAdv.Show(this, "Invalid voucher type for Cr/Dr Note", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    voucherLookup1.Focus();
                    return false;
                }
            }


            using (var db = new KontoContext())
            {
                var find = db.RPSets.FirstOrDefault(
                   x => x.Field == fieldLookUpEdit1.EditValue.ToString() && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "Group Name Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    typeLookUpEdit.Focus();
                    return false;
                }
            }

            return true;
        }

        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<RPSetModel>();
            this.Text = "Rec/Pay Setting [Add New]";
            drCrLookUpEdit.EditValue = "N";
            addLessLookUpEdit.EditValue = "M";
            typeLookUpEdit.EditValue = "R";
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
            typeLookUpEdit.Focus();
                
        }
        public override void ResetPage()
        {
            base.ResetPage();
            typeLookUpEdit.EditValue  = DBNull.Value;
            fieldLookUpEdit1.EditValue = DBNull.Value;
            addLessLookUpEdit.EditValue = DBNull.Value;
            perHeadKontoTextBoxExt.Clear();
            amtHeadingKontoTextBoxExt.Clear();
            drCrLookUpEdit.EditValue = DBNull.Value;
            accLookup1.SetEmpty();
            taxTypelookUpEdit.EditValue = DBNull.Value;
            hsnCodeKontoTextBoxExt.Clear();
            descrKontoTextBoxExt.Clear();
            voucherLookup1.SetEmpty();
           
        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var model = db.RPSets.Find(_key);
                LoadData(model);
            }

        }
        private void LoadData(RPSetModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            typeLookUpEdit.EditValue = model.RecPay;
            fieldLookUpEdit1.EditValue = model.Field;
            addLessLookUpEdit.EditValue = model.PlusMinus;
            perHeadKontoTextBoxExt.Text = model.PerCap;
            amtHeadingKontoTextBoxExt.Text = model.AmtCap;
            drCrLookUpEdit.EditValue = model.Drcr;
            accLookup1.SelectedValue = model.AccountId;
            accLookup1.SetAcc(model.AccountId);
            taxTypelookUpEdit.EditValue = model.TaxId;
            hsnCodeKontoTextBoxExt.Text = model.HsnCode;
            descrKontoTextBoxExt.Text = model.Remark;
            if (model.VoucherId != null)
            {
                voucherLookup1.SelectedValue = model.VoucherId;
                voucherLookup1.SetGroup(Convert.ToInt32( model.VoucherId));
            }
            this.Text = "Rec/Pay Setting [View/Modify]";

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
            PGroupModel _find = new PGroupModel();

            if (!string.IsNullOrWhiteSpace(typeLookUpEdit.Text.Trim()))
                filter.Add(new Filter { PropertyName = "GroupName", Operation = Op.Contains, Value = typeLookUpEdit.Text.Trim() });

            if (!string.IsNullOrWhiteSpace(typeLookUpEdit.Text.Trim()))
                filter.Add(new Filter { PropertyName = "GroupCode", Operation = Op.StartsWith, Value = typeLookUpEdit.Text.Trim() });

        

            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });


            //using (var db = new KontoContext())
            //{
            //    FilterView = db.RPSets.Where(ExpressionBuilder.GetExpression<PGroupModel>(filter))
            //        .OrderBy(x => x.GroupName).ToList();
            //    if (FilterView.Count == 0)
            //    {
            //        MessageBoxAdv.Show(this, "No Record Found", "Find !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        this.ResetPage();
            //        return;
            //    }
            //    this.TotalRecord = FilterView.Count;
            //    this.RecordNo = 0;
            //    LoadData(this.FilterView[0]);

            //}

        }

        public override void SaveDataAsync(bool newmode)
        {

            bool IsSaved = false;
            if (!ValidateData()) return;
            RPSetModel model = new RPSetModel();
            using (var db = new KontoContext())
            {
                if (this.PrimaryKey != 0)
                    model = db.RPSets.Find(this.PrimaryKey);

                model.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);
                model.RecPay = typeLookUpEdit.EditValue.ToString();
                model.Field = fieldLookUpEdit1.EditValue.ToString();
                model.PlusMinus = addLessLookUpEdit.EditValue.ToString();
                model.PerCap = perHeadKontoTextBoxExt.Text.Trim();
                model.AmtCap = amtHeadingKontoTextBoxExt.Text.Trim();
                model.Drcr = drCrLookUpEdit.EditValue.ToString();
                model.AccountId = Convert.ToInt32(accLookup1.SelectedValue);
                model.TaxId = Convert.ToInt32(taxTypelookUpEdit.EditValue);
                model.HsnCode = hsnCodeKontoTextBoxExt.Text.Trim();
                model.Remark = descrKontoTextBoxExt.Text.Trim();
                model.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);
                model.YearId = KontoGlobals.YearId;
                model.CompId = KontoGlobals.CompanyId;
                if (this.PrimaryKey == 0)
                {
                    // model.RowId = Guid.NewGuid();
                    db.RPSets.Add(model);
                }
                db.SaveChanges();

                IsSaved = true;

            }
            if (IsSaved)
            {
                NewRec();
                base.SaveDataAsync(newmode);
                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (!this.OpenForLookup && newmode)
                {
                    this.ResetPage();
                    typeLookUpEdit.Focus();
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
