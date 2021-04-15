using AutoMapper;
using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AutoMapper.QueryableExtensions;
using System.Windows.Forms;
using ExpressionBuilder = Konto.Core.Shared.Libs.ExpressionBuilder;

namespace Konto.Shared.OpBill
{
    public partial class SPOpBillIndex : KontoMetroForm
    {
        private List<OpBillDto> FilterView = new List<OpBillDto>();
        public SPOpBillIndex()
        {
            InitializeComponent();
            
            tabControlAdv1.TabPages[2].TabVisible = false;
            tabControlAdv1.TabPages[3].TabVisible = false;

            this.Load += ColorIndex_Load;
            okSimpleButton.Click += OkSimpleButton_Click;
            againstLookUpEdit.EditValueChanged += AgainstLookUpEdit_EditValueChanged;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;
            billAmtSpinEdit.ValueChanged += BillAmtSpinEdit_ValueChanged;
            payAmtspinEdit.ValueChanged += BillAmtSpinEdit_ValueChanged;
            rgSpinEdit.ValueChanged += BillAmtSpinEdit_ValueChanged;
            tdsSpinEdit.ValueChanged += BillAmtSpinEdit_ValueChanged;
            this.MainLayoutFile = KontoFileLayout.Op_Bill_Main;

            this.FirstActiveControl = voucherLookup1;

            FillAgainst();
        }

        private void AgainstLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (againstLookUpEdit.Text == "Sales")
                bookLookup.VoucherType = VoucherTypeEnum.SaleInvoice;
            else if (againstLookUpEdit.Text == "Purchase")
                bookLookup.VoucherType = VoucherTypeEnum.PurchaseInvoice;
            else if (againstLookUpEdit.Text == "CRNote")
                bookLookup.VoucherType = VoucherTypeEnum.DebitCreditNote;
            else if (againstLookUpEdit.Text == "Payment")
                bookLookup.VoucherType = VoucherTypeEnum.PaymentVoucher;
            else if (againstLookUpEdit.Text == "Receipt")
                bookLookup.VoucherType = VoucherTypeEnum.ReceiptVoucher;

        }

        private void BillAmtSpinEdit_ValueChanged(object sender, EventArgs e)
        {
            SetBal();
        }

        private void FillAgainst()
        {
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Sales", "Sales"),
                new ComboBoxPairs("Purchase", "Purchase"),
                new ComboBoxPairs("CRNote", "CRNote"),
                new ComboBoxPairs("DrNote", "DrNote"),
                new ComboBoxPairs("Payment", "Payment"),
                new ComboBoxPairs("Receipt", "Receipt")
            };
            againstLookUpEdit.Properties.DataSource = cbp;
        }
        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                voucherLookup1.Focus();
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as SPOpBillListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new SPOpBillListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Opening Bills [View]";

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

                Log.Error(ex, "Opening Bill Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private void ColorIndex_Load(object sender, EventArgs e)
        {
            try
            {
                this.ResetPage();
                NewRec();

                this.ActiveControl = voucherLookup1.buttonEdit1;

            }
            catch (Exception ex)
            {

                Log.Error(ex, "Size Load");
                MessageBox.Show(ex.ToString());
            }
        }
        private void SetBal()
        {
            balanceSpinEdit.Value = billAmtSpinEdit.Value - payAmtspinEdit.Value - rgSpinEdit.Value - tdsSpinEdit.Value;
        }
        private bool ValidateData()
        {

            if (Convert.ToInt32(voucherLookup1.SelectedValue)==0)
            {
                MessageBoxAdv.Show(this, "Invalid Voucher", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherLookup1.Focus();
                return false;
            }
            if (Convert.ToInt32(accLookup1.SelectedValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Party", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                accLookup1.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(againstLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Against", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                againstLookUpEdit.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(billNotextEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Bill No", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                billNotextEdit.Focus();
                return false;
            }
            else if (billAmtSpinEdit.Value==0)
            {
                MessageBoxAdv.Show(this, "Invalid Bill Amount", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                billAmtSpinEdit.Focus();
                return false;
            }


            //using (var db = new KontoContext())
            //{
            //    var find = db.ColorModels.FirstOrDefault(
            //       x => x.ColorName == colorNameTextBox.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

            //    if (find != null)
            //    {
            //        MessageBoxAdv.Show(this, "Color Name Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        colorNameTextBox.Focus();
            //        return false;
            //    }
            //}

            return true;
        }

        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<OpBillDto>();
            this.Text = "Opening Bills [Add New]";
            voucherNoTextEdit.Text = "New";
            this.ActiveControl = voucherLookup1;
            voucherLookup1.SetDefault();

            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
        }
        public override void ResetPage()
        {
            base.ResetPage();
            againstLookUpEdit.EditValue = DBNull.Value;
            accLookup1.SetEmpty();
            bookLookup.SetEmpty();
            billNotextEdit.Text = string.Empty;
            billDateEdit.DateTime = DateTime.Now;
            challanNotextEdit.Text = string.Empty;
            challanDateEdit.DateTime = DateTime.Now;
            agentLookup.SetEmpty();
            qtyspinEdit.Value = 0;
            ratespinEdit.Value = 0;
            billAmtSpinEdit.Value = 0;
            payAmtspinEdit.Value = 0;
            rgSpinEdit.Value = 0;
            tdsSpinEdit.Value = 0;
            balanceSpinEdit.Value = 0;
            remarkTextEdit.Text = string.Empty;
        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BillModel, OpBillDto>();
            });

            using (var db = new KontoContext())
            {
                var bill = db.Bills.Find(_key);
                var model = new OpBillDto();
                var map = new Mapper(config);
                map.Map(bill, model);
                LoadData(model);
            }

        }
        private void LoadData(OpBillDto model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            voucherLookup1.SelectedValue = model.VoucherId;
            voucherLookup1.SetGroup(model.VoucherId);
            againstLookUpEdit.EditValue = model.BillType;
            voucherNoTextEdit.Text = model.VoucherNo;
            accLookup1.SelectedValue = model.AccId;
            accLookup1.SetAcc(model.AccId);
            billNotextEdit.Text = model.BillNo;
            billDateEdit.EditValue = KontoUtils.IToD(model.VoucherDate);
            challanNotextEdit.Text = model.DocNo;
            challanDateEdit.EditValue = model.DocDate;
            if( Convert.ToInt32(model.AgentId) == 0)
            {
                agentLookup.SetEmpty();
            }
            else
            {
                agentLookup.SelectedValue = model.AgentId;
                agentLookup.SetAcc((int)model.AgentId);
            }
            if(Convert.ToInt32(model.BookAcId)==0)
            {
                bookLookup.SetEmpty();
            }
            else
            {
                bookLookup.SelectedValue = model.BookAcId;
                bookLookup.SetAcc((int)model.BookAcId);
            }
            qtyspinEdit.Value = Convert.ToDecimal (model.TotalQty);
            ratespinEdit.Value = model.ExchRate;
            billAmtSpinEdit.Value = model.TotalAmount;
            payAmtspinEdit.Value = model.GrossAmount;
            rgSpinEdit.Value =Convert.ToDecimal( model.TotalPcs);
            tdsSpinEdit.Value = model.TdsAmt;
            balanceSpinEdit.Value = model.TotalAmount - model.GrossAmount - Convert.ToDecimal(model.TotalPcs) - model.TdsAmt;
            this.Text = "Opening Bills [View/Modify]";

           // createdLabelControl.Text = "Created By: " + model.CreateUser + " [ " + model.CreateDate + " ]";
           // modifyLabelControl.Text = "Modified By: " + model.ModifyUser + " [ " + model.ModifyDate ?? string.Empty + " ]";

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
            //ColorModel _find = new ColorModel();

            if (Convert.ToInt32(voucherLookup1.SelectedValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "VoucherId", Operation = Op.Equals, Value = Convert.ToInt32(voucherLookup1.SelectedValue) });
            }
            if (!string.IsNullOrEmpty(againstLookUpEdit.Text))
            {
                filter.Add(new Filter { PropertyName = "BillType", Operation = Op.Equals, Value = againstLookUpEdit.EditValue.ToString() });
            }
            
            if (!string.IsNullOrEmpty(billNotextEdit.Text))
            {
                filter.Add(new Filter { PropertyName = "BillNo", Operation = Op.Contains, Value = billNotextEdit.Text.Trim() });
            }
            if (Convert.ToInt32(accLookup1.SelectedValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "AccId", Operation = Op.Equals, Value = Convert.ToInt32(accLookup1.SelectedValue) });
            }

            filter.Add(new Filter { PropertyName = "CompId", Operation = Op.Equals, Value = KontoGlobals.CompanyId });
            filter.Add(new Filter { PropertyName = "YearId", Operation = Op.Equals, Value = KontoGlobals.YearId });
            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BillModel, OpBillDto>();
            });

            using (var db = new KontoContext())
            {
                FilterView = db.Bills.Where(ExpressionBuilder.GetExpression<BillModel>(filter))
                    .OrderBy(x => x.Id).ProjectToList<OpBillDto>(config);
                   

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
            OpBillDto model = new OpBillDto();
            model.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);
            model.BillType = againstLookUpEdit.EditValue.ToString();
            model.VoucherNo = voucherNoTextEdit.Text.Trim();
            model.AccId = Convert.ToInt32(accLookup1.SelectedValue);
            model.BillNo = billNotextEdit.Text.Trim();
            model.VoucherDate = Convert.ToInt32( billDateEdit.DateTime.ToString("yyyyMMdd"));
            model.DocNo = challanNotextEdit.Text.Trim();
            model.DocDate = Convert.ToDateTime(challanDateEdit.EditValue);
            if (Convert.ToInt32(agentLookup.SelectedValue) > 0)
                model.AgentId = Convert.ToInt32(agentLookup.SelectedValue);
            else
                model.AgentId = null;
            model.TotalQty = qtyspinEdit.Value;
            model.ExchRate = ratespinEdit.Value;
            model.TotalAmount = billAmtSpinEdit.Value;
            model.GrossAmount = payAmtspinEdit.Value;
            model.TotalPcs = rgSpinEdit.Value;
            model.TdsAmt = tdsSpinEdit.Value;
            model.CompId = KontoGlobals.CompanyId;
            model.YearId = KontoGlobals.YearId;
            model.BranchId = KontoGlobals.BranchId;
            
            var _find = new BillModel();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OpBillDto, BillModel>().ForMember(x => x.Id, p => p.Ignore()
                );
            });

            using (var db = new KontoContext())
            {
                using (var _tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (this.PrimaryKey != 0)
                            _find = db.Bills.Find(this.PrimaryKey);

                        var map = new Mapper(config);
                        map.Map(model, _find);
                        _find.TypeId = (int)VoucherTypeEnum.SalePurchaseOpBill;
                        _find.BookAcId = Convert.ToInt32(bookLookup.SelectedValue);
                        if (this.PrimaryKey == 0)
                        {
                            _find.VoucherNo = DbUtils.NextSerialNo(_find.VoucherId, db);
                            model.VoucherNo = _find.VoucherNo;
                        
                            db.Bills.Add(_find);
                            db.SaveChanges();
                        }

                        // add entry in bill ref table
                        string Type = "Debit";
                        if (model.BillType.ToUpper() == "SALES" || model.BillType.ToUpper() == "DRNOTE"
                            || model.BillType.ToUpper() == "PAYMENT")
                        {
                            LedgerEff.BillRefEntry(Type,  _find,0,  db);       //Insert or update in Billref table
                        }
                        else if (model.BillType.ToUpper() == "PURCHASE" || model.BillType.ToUpper() == "CRNOTE"
                            || model.BillType.ToUpper() == "RECEIPT")
                        {
                            Type = "Credit";
                            LedgerEff.BillRefEntry(Type,  _find, 0, db);
                        }
                        db.SaveChanges();
                       
                       // UpdateOpeningBalance(db,model);

                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "op bill Save");
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
                    voucherLookup1.buttonEdit1.Focus();
                }
                else
                {
                    this.Close();
                    this.Dispose();
                }
            }
        }

        private void UpdateOpeningBalance(KontoContext db,OpBillDto model)
        {
            var _opdr =(from br in db.BillRefs
                         join vc in db.Vouchers on br.BillVoucherId equals vc.Id into join_vc
                         from vc in join_vc.DefaultIfEmpty()
                         where br.RefType == "Debit" && br.AccountId == model.AccId
                         && br.VoucherDate < KontoGlobals.FromDate && vc.VTypeId == 26
                         select br.BillAmt - br.RetAmt - br.AdjustAmt - br.TdsAmt ).DefaultIfEmpty(0m).Sum();

            var _opcr =(from br in db.BillRefs
                         join vc in db.Vouchers on br.BillVoucherId equals vc.Id into join_vc
                         from vc in join_vc.DefaultIfEmpty()
                         where br.RefType == "Credit" && br.AccountId == model.AccId
                         && br.VoucherDate < KontoGlobals.FromDate && vc.VTypeId == 26
                         select  br.BillAmt - br.RetAmt - br.AdjustAmt - br.TdsAmt ).DefaultIfEmpty(0m).Sum();

            var _ab = db.AccBals.FirstOrDefault(x => x.AccId == model.AccId && x.CompId ==
                        KontoGlobals.CompanyId && x.YearId == KontoGlobals.YearId);

            var bal = _opdr - _opcr;
            if (bal < 0)
                _ab.OpCredit = -1 * bal;
            else
                _ab.OpDebit = bal;

            _ab.OpBal = bal;

            db.SaveChanges();
            LedgerEff.UpdateBalance(model.AccId, db);
            db.SaveChanges();
        }
    }
}
