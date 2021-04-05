using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;

using Konto.Data.Models.Admin;
using Konto.Data.Models.Masters;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Masters.Voucher
{
    public partial class VoucherIndex : KontoMetroForm
    {
        private List<VoucherModel> FilterView = new List<VoucherModel>();
        private LastSerialModel _Serial = new LastSerialModel();
        public int GroupId { get; set; }
        public VoucherIndex()
        {
            tabControlAdv1.TabPages[2].TabVisible = false;
            tabControlAdv1.TabPages[3].TabVisible = false;


            InitializeComponent();
            this.Load += ColorIndex_Load;
            okSimpleButton.Click += OkSimpleButton_Click;
            this.MainLayoutFile = KontoFileLayout.Voucher_Master;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;
            this.Shown += VoucherIndex_Shown;
            FillVoucher();

            this.FirstActiveControl = nameTextEdit;
        }

        private void VoucherIndex_Shown(object sender, EventArgs e)
        {
            if (this.EditKey > 0)
                this.EditPage(EditKey);

            
        }

        private void FillVoucher()
        {
            using(var db = new KontoContext())
            {
                var vtype = (from p in db.VoucherTypes
                             where !p.IsDeleted && p.IsActive
                             orderby p.TypeName
                             select new BaseLookupDto()
                             {
                                 DisplayText = p.TypeName,
                                 Id = p.Id
                             }).ToList();

                var vhc = (from p in db.Vouchers
                             where !p.IsDeleted && p.IsActive
                             orderby p.VoucherName
                             select new BaseLookupDto()
                             {
                                 DisplayText = p.VoucherName,
                                 Id = p.Id
                             }).ToList();

                typeLookUpEdit.Properties.DataSource = vtype;
                voucherLookUpEdit.Properties.DataSource = vhc;

            }

        }
        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                nameTextEdit.Focus();
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as VoucherListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new VoucherListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Voucher Setup [View]";

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

                Log.Error(ex, "Voucher Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private void ColorIndex_Load(object sender, EventArgs e)
        {
            try
            {
                NewRec();

                this.ActiveControl = nameTextEdit;

                if (this.PrimaryKey == 0)
                {
                    toggleSwitch1.Enabled = false;
                }

            }
            catch (Exception ex)
            {

                Log.Error(ex, "Voucher Load");
                MessageBox.Show(ex.ToString());
            }
        }
        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(nameTextEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Voucher Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nameTextEdit.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(sortNametextEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Sort Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                sortNametextEdit.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(typeLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Voucher Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                typeLookUpEdit.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(maskTextEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Serial Mask", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                maskTextEdit.Focus();
                return false;
            }


            using (var db = new KontoContext())
            {
                var find = db.Vouchers.FirstOrDefault(
                   x => x.VoucherName == nameTextEdit.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "Voucher Name Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nameTextEdit.Focus();
                    return false;
                }
            }

            return true;
        }

        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<VoucherModel>();
            this.Text = "Voucher Setup [Add New]";
            this.ActiveControl = nameTextEdit;
            maskTextEdit.Text = "{#}/{yy}";
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;

            if (this.GroupId != 0)
            {
                this.typeLookUpEdit.EditValue = this.GroupId;
            }

        }
        public override void ResetPage()
        {
            base.ResetPage();
            nameTextEdit.Text = string.Empty;
            sortNametextEdit.Text = string.Empty;
            typeLookUpEdit.EditValue = DBNull.Value;
            voucherLookUpEdit.EditValue = DBNull.Value;
            accLookup1.SetEmpty();
            remarkstextEdit.Text = string.Empty;
            headingTextEdit.Text = string.Empty;
            widthspinEdit.Value = 0;
            startFromspinEdit.Value = 0;
            ZeroCheckEdit.Checked = false;
            incBySpinEdit.Value = 1;
            maskTextEdit.Text = string.Empty;
            resetCheckEdit.Checked = false;
            printCheckEdit.Checked = false;
            emailCheckEdit.Checked = false;
            smsCheckEdit.Checked = false;
            toggleSwitch1.EditValue = true;
            toggleSwitch1.Enabled = false;

        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var model = db.Vouchers.Find(_key);
                LoadData(model);
            }

        }
        private void LoadData(VoucherModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            nameTextEdit.Text = model.VoucherName;
            sortNametextEdit.Text = model.SortName;
            typeLookUpEdit.EditValue = model.VTypeId;
            voucherLookUpEdit.EditValue = model.RefVoucherId;
            if (Convert.ToInt32(model.BookAcId) > 0)
            {
                accLookup1.SelectedValue = model.BookAcId;
                accLookup1.SetAcc(Convert.ToInt32(model.BookAcId));
            }
            else
            {
                accLookup1.SetEmpty();
            }
            remarkstextEdit.Text = model.Extra1;
            toggleSwitch1.EditValue = model.IsActive;
            toggleSwitch1.Enabled = true;

            using(var db = new KontoContext())
            {
                var md = db.VchSetups.FirstOrDefault(x => x.VoucherId == model.Id && x.CompId == KontoGlobals.CompanyId);
                if (md != null)
                {
                    headingTextEdit.Text = md.InvoiceHeading;
                    widthspinEdit.Value = (int) md.VchWidth;
                    startFromspinEdit.Value = (int)md.StartFrom;
                    ZeroCheckEdit.Checked = Convert.ToBoolean(md.PreFillZero);
                    incBySpinEdit.Value = (int) md.Increment;
                    maskTextEdit.Text = md.Serial_Mask;
                    resetCheckEdit.Checked = Convert.ToBoolean(md.FyReset);
                    printCheckEdit.Checked = Convert.ToBoolean(md.PrintAfterSave);
                    smsCheckEdit.Checked = Convert.ToBoolean(md.SmsAfterSave);
                    emailCheckEdit.Checked = Convert.ToBoolean(md.EmailAfterSave);
                    manualCheckEdit.Checked = md.ManualSeries;
                }
            }

            nameTextEdit.Focus();
            this.Text = "Voucher Setup [View/Modify]";

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
            VoucherModel _find = new VoucherModel();

            if (!string.IsNullOrWhiteSpace(nameTextEdit.Text.Trim()))
                filter.Add(new Filter { PropertyName = "VoucherName", Operation = Op.Contains, Value = nameTextEdit.Text.Trim() });

            if (!string.IsNullOrWhiteSpace(sortNametextEdit.Text.Trim()))
                filter.Add(new Filter { PropertyName = "SortName", Operation = Op.Contains, Value = sortNametextEdit.Text.Trim() });

            if (!string.IsNullOrEmpty(typeLookUpEdit.Text))
                filter.Add(new Filter { PropertyName = "VTypeId", Operation = Op.Equals, Value = Convert.ToInt32(typeLookUpEdit.EditValue) });

            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });


            using (var db = new KontoContext())
            {
                FilterView = db.Vouchers.Where(ExpressionBuilder.GetExpression<VoucherModel>(filter))
                    .OrderBy(x => x.VoucherName).ToList();
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
            VoucherModel model = new VoucherModel();
            VchSetupModel md = new VchSetupModel();
            using (var db = new KontoContext())
            {
                using(var _tran  = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (this.PrimaryKey != 0)
                        {
                            model = db.Vouchers.Find(this.PrimaryKey);
                            md = db.VchSetups.FirstOrDefault(x => x.VoucherId == model.Id && x.CompId == KontoGlobals.CompanyId);
                        }

                        model.VoucherName = nameTextEdit.Text.Trim();
                        model.SortName = sortNametextEdit.Text.Trim();
                        model.VTypeId = Convert.ToInt32(typeLookUpEdit.EditValue);

                        if (!string.IsNullOrEmpty(voucherLookUpEdit.Text))
                            model.RefVoucherId = Convert.ToInt32(voucherLookUpEdit.EditValue);
                        else
                            model.RefVoucherId = null;

                        if (Convert.ToInt32(accLookup1.SelectedValue) > 0)
                            model.BookAcId = Convert.ToInt32(accLookup1.SelectedValue);
                        else
                            model.BookAcId = null;

                        model.Extra1 = remarkstextEdit.Text.Trim();


                        model.IsActive = Convert.ToBoolean(toggleSwitch1.EditValue);

                        if (this.PrimaryKey == 0)
                        {
                            db.Vouchers.Add(model);
                            db.SaveChanges();
                        }
                        if (md == null)
                            md = new VchSetupModel();
                        md.InvoiceHeading = headingTextEdit.Text.Trim();
                        md.VoucherId = model.Id;
                        md.VchWidth = (int)widthspinEdit.Value;
                        md.StartFrom = (int)startFromspinEdit.Value;
                        md.PreFillZero = ZeroCheckEdit.Checked;
                        md.Increment = (int)incBySpinEdit.Value;
                        md.Serial_Mask = maskTextEdit.Text.Trim();
                        md.FyReset = resetCheckEdit.Checked;
                        md.PrintAfterSave = printCheckEdit.Checked;
                        md.SmsAfterSave = smsCheckEdit.Checked;
                        md.EmailAfterSave = emailCheckEdit.Checked;
                        md.ManualSeries = manualCheckEdit.Checked;
                        md.CompId = KontoGlobals.CompanyId;
                        
                        if (md.Id == 0)
                        {
                            db.VchSetups.Add(md);
                        }

                        
                        db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;

                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "voucher setup Save");
                        MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());

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
                    NewRec();
                    nameTextEdit.Focus();
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
