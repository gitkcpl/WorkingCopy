using DevExpress.XtraGrid.Views.Grid;
using Konto.App.Shared;
using Konto.App.Shared.Para;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Account
{
    public partial class PendingBillViewWindow : KontoForm
    {
        #region Decleration
        public List<PendBillListDto> BillList = new List<PendBillListDto>();
        public List<BtoBModel> BillAdj = new List<BtoBModel>();
        public List<PendBillListDto> DelBillList = new List<PendBillListDto>();

        public string Adlp1;

        public string Adla1;
        public string Adlp2;
        public string Adla2;
        public string Adlp3;
        public string Adla3;
        public string Adlp4;
        public string Adla4;
        public string Adlp5;
        public string Adla5;
        public string Adlp6;
        public string Adla6;
        public string Adlp7;
        public string Adla7;
        public string Adlp8;
        public string Adla8;
        public string Adlp9;
        public string Adla9;
        public string Adlp10;
        public string Adla10;

        public bool Adlp1Req;

        public bool Adla1Req;

        public bool Adlp2Req;

        public bool Adla2Req;

        public bool Adlp3Req;

        public bool Adla3Req;

        public bool Adlp4Req;

        public bool Adla4Req;

        public bool Adlp5Req;

        public bool Adla5Req;

        public bool Adlp6Req;

        public bool Adla6Req;

        public bool Adlp7Req;

        public bool Adla7Req;

        public bool Adlp8Req;

        public bool Adla8Req;

        public bool Adlp9Req;

        public bool Adla9Req;

        public bool Adlp10Req;

        public bool Adla10Req;

        public string pm1;

        public string pm2;

        public string pm3;

        public string pm4;

        public string pm5;
        public string pm6;
        public string pm7;
        public string pm8;
        public string pm9;
        public string pm10;

        #endregion


        public decimal TotalAmount { get; set; }
        decimal Balance;
        private string GridLayoutFile = KontoFileLayout.Pending_Bill;
        private string Vtype;
        private int AccountId;
        private int VoucherId;
        private string _Type;
        private int Id;
        private int TransId;
        private int RefvoucherId;

        public List<PendBillListDto> AllBill = new List<PendBillListDto>();

        public PendingBillViewWindow(string _vtype, int accid, int _vid, string typ, int _id, int _transid, int _refvoucher)
        {
            InitializeComponent();
            Vtype = _vtype;
            AccountId = accid;
            VoucherId = _vid;
            _Type = typ;
            Id = _id;
            TransId = _transid;

            RefvoucherId = _refvoucher;

            this.Load += PendingBillViewWindow_Load;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.gridView1.CustomDrawRowIndicator += GridView1_CustomDrawRowIndicator;
            this.gridView1.KeyDown += GridView1_KeyDown;
            this.gridView1.CellValueChanged += GridView1_CellValueChanged;
            this.gridView1.ValidateRow += GridView1_ValidateRow;
            this.gridView1.InvalidRowException += GridView1_InvalidRowException;
        }

        private void GridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void GridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            GridView view = sender as GridView;

            decimal amt = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, colDueAmt));
            if (amt < 0)
            {
                e.Valid = false;
                view.SetColumnError(colAmount, "Due Amount Can not be Less than zero");
            }
        }

        private void GridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var er = gridView1.GetRow(e.RowHandle) as PendBillListDto;//e.Row;

            if (er == null) return;
            if(e.Column.FieldName == "Adlp1")
            {
                er.Adla1 = decimal.Round((decimal)er.NetTotal * er.Adlp1 / 100, 0, MidpointRounding.AwayFromZero);
            }
            else if (e.Column.FieldName == "Adlp2")
            {
                er.Adla2 = decimal.Round((decimal)er.NetTotal * er.Adlp2 / 100, 0, MidpointRounding.AwayFromZero);
            }
            else if (e.Column.FieldName == "Adlp3")
            {
                er.Adla3 = decimal.Round((decimal)er.NetTotal * er.Adlp3 / 100, 0, MidpointRounding.AwayFromZero);
            }
            else if (e.Column.FieldName == "Adlp4")
            {
                er.Adla4 = decimal.Round((decimal)er.NetTotal * er.Adlp4 / 100, 0, MidpointRounding.AwayFromZero);
            }
            else if (e.Column.FieldName == "Adlp5")
            {
                er.Adla5 = decimal.Round((decimal)er.NetTotal * er.Adlp5 / 100, 0, MidpointRounding.AwayFromZero);
            }
            else if (e.Column.FieldName == "Adlp6")
            {
                er.Adla6 = decimal.Round((decimal)er.NetTotal * er.Adlp6 / 100, 0, MidpointRounding.AwayFromZero);
            }
            else if (e.Column.FieldName == "Adlp7")
            {
                er.Adla7 = decimal.Round((decimal)er.NetTotal * er.Adlp7 / 100, 0, MidpointRounding.AwayFromZero);
            }
            else if (e.Column.FieldName == "Adlp8")
            {
                er.Adla8 = decimal.Round((decimal)er.NetTotal * er.Adlp8 / 100, 0, MidpointRounding.AwayFromZero);
            }
            else if (e.Column.FieldName == "Adlp9")
            {
                er.Adla9 = decimal.Round((decimal)er.NetTotal * er.Adlp9 / 100, 0, MidpointRounding.AwayFromZero);
            }
            else if (e.Column.FieldName == "Adlp10")
            {
                er.Adla10 = decimal.Round((decimal)er.NetTotal * er.Adlp10 / 100, 0, MidpointRounding.AwayFromZero);
            }

            if (er.Amount != null)
                {
                if (pm1 == "P")
                {
                    er.Adla1 = er.Adla1;
                }
                else
                {
                    er.Adla1 = -er.Adla1;
                }
                if (pm2 == "P")
                {
                    er.Adla2 = er.Adla2;
                }
                else
                {
                    er.Adla2 = -er.Adla2;
                }
                if (pm3 == "P")
                {
                    er.Adla3 = er.Adla3;
                }
                else
                {
                    er.Adla3 = -er.Adla3;
                }
                if (pm4 == "P")
                {
                    er.Adla4 = er.Adla4;
                }
                else
                {
                    er.Adla4 = -er.Adla4;
                }
                if (pm5 == "P")
                {
                    er.Adla5 = er.Adla5;
                }
                else
                {
                    er.Adla5 = -er.Adla5;
                }
                if (pm6 == "P")
                {
                    er.Adla6 = er.Adla6;
                }
                else
                {
                    er.Adla6 = -er.Adla6;
                }
                if (pm7 == "P")
                {
                    er.Adla7 = er.Adla7;
                }
                else
                {
                    er.Adla7 = -er.Adla7;
                }
                if (pm8 == "P")
                {
                    er.Adla8 = er.Adla8;
                }
                else
                {
                    er.Adla8 = -er.Adla8;
                }
                if (pm9 == "P")
                {
                    er.Adla9 = er.Adla9;
                }
                else
                {
                    er.Adla9 = -er.Adla9;
                }
                if (pm10 == "P")
                {
                    er.Adla10 = er.Adla10;
                }
                else
                {
                    er.Adla10 = -er.Adla10;
                }
            }


            decimal Bal = Convert.ToInt32(BillList.Sum(k => k.Amount));
            // decimal? Sum = viewModel.Trans.Sum(k => k.NetTotal);
            decimal Sum = TotalAmount;
            Balance = Sum - Bal;
            if (Balance < 0 && Sum > 0)
            {
                er.Amount = 0;
                Balance = Sum;
            }

            var due = er.NetTotal - er.TdsAmt - er.PaidAmt - er.RetAmt;
            er.DueAmt = due - er.Amount - er.Adla1 - er.Adla2 - er.Adla3 - er.Adla4 - er.Adla5 - er.Adla6 - er.Adla7 - er.Adla8 - er.Adla9 - er.Adla10;
            gridView1.UpdateCurrentRow();
            Bal = Convert.ToInt32(BillList.Sum(k => k.Amount));
            lblBalance.Text = "Pending : " + (this.TotalAmount - Bal).ToString("F");
        }

        private void GridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                var dr = gridView1.GetRow(gridView1.FocusedRowHandle) as PendBillListDto;
                dr.Amount = dr.DueAmt;
                gridView1.UpdateCurrentRow();
            }
        }

        private void GridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.Appearance.ForeColor = Color.FromArgb(227, 22, 91);
            if (e.RowHandle < 0)
                e.Info.DisplayText = "";
            else
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        public PendingBillViewWindow()
        {
            InitializeComponent();

            this.Load += PendingBillViewWindow_Load;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.gridView1.CustomDrawRowIndicator += GridView1_CustomDrawRowIndicator;
            this.gridView1.KeyDown += GridView1_KeyDown;
            this.gridView1.CellValueChanged += GridView1_CellValueChanged;
        }
        private void PendingBills(KontoContext db)
        {
            var spcol = db.SpCollections.FirstOrDefault(k => k.Id ==
        (int)SpCollectionEnum.PendingBill);

            if (spcol == null)
            {
                BillList = db.Database.SqlQuery<PendBillListDto>(
                  "dbo.PendingBill @CompanyId={0},@AccountId={1},@VoucherTypeId={2},@BillType={3}" +
                  ",@RefId={4},@RefTransId={5},@RefVoucherId={6}",
                  KontoGlobals.CompanyId, AccountId, VoucherId, _Type, Id, TransId, RefvoucherId).ToList();
            }
            else
            {
                BillList = db.Database.SqlQuery<PendBillListDto>(
                 spcol.Name + " @CompanyId={0},@AccountId={1},@VoucherTypeId={2},@BillType={3}" +
                  ",@RefId={4},@RefTransId={5},@RefVoucherId={6}",
                KontoGlobals.CompanyId, AccountId, VoucherId, _Type, Id, TransId, RefvoucherId).ToList();
            }


        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            var lst = this.BillList.Where(x => x.DueAmt < 0).ToList();
            if (lst.Count > 0)
            {
                MessageBox.Show("Due Amount Can not be less than zero");
                gridView1.Focus();
                return;
            }
            if (this.TotalAmount > 0 && this.TotalAmount < Convert.ToDecimal(colAmount.SummaryItem.SummaryValue))
            {
                MessageBox.Show("Adjust Amount Can No Be Greater Than Received Amount");
                gridView1.Focus();
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.DelBillList = new List<PendBillListDto>();

            var del = this.AllBill.Where(x => x.Amount == 0 && x.RefTransId > 0).ToList();
            this.DelBillList.AddRange(del);
            this.Close();
        }

        private void PendingBillViewWindow_Load(object sender, EventArgs e)
        {

            // decimal? Sum = viewModel.Trans.Sum(k => k.NetTotal);

            using (var db = new KontoContext())
            {
                if (Vtype == "R" || Vtype == "P")
                {
                    AgainstBillSet(Vtype, db);
                }
                this.BillList = this.AllBill.Where(x => x.RefTransId == this.TransId).ToList();
                if (BillList.Count == 0)
                {
                    this.PendingBills(db);
                    this.AllBill.AddRange(BillList);
                }
            }
            decimal Bal = Convert.ToDecimal(BillList.Sum(k => k.Amount));
            decimal _bal = this.TotalAmount;
            if (((Vtype == "R" && ReceiptPara.Auto_Bill_Adjust) || (Vtype=="P" && PaymentPara.Auto_Bill_Adjust))
                    && this.TotalAmount > 0 && Bal==0 )
            {
                foreach (var item in this.BillList)
                {
                    if (_bal == 0) break;
                    if (_bal >= item.DueAmt)
                    {
                        item.Amount = item.DueAmt;
                        _bal = _bal - (decimal) item.DueAmt;
                    }
                    else
                    {
                        item.Amount = _bal;
                        break;
                    }
                }
            }
             Bal = Convert.ToDecimal(BillList.Sum(k => k.Amount));
            lblBalance.Text = "Pending : " + (this.TotalAmount-Bal) .ToString("F");
            this.bindingSource1.DataSource = this.BillList;

            SetGridColumn();

            this.gridView1.FocusedColumn = colAmount;
            this.ActiveControl = this.gridControl1;
        }

       

        private void AgainstBillSet(string Vtype,KontoContext db)
        {
            var adl1 = db.RPSets.FirstOrDefault(k => k.Field == "Adl1" && k.RecPay == Vtype && k.YearId == KontoGlobals.YearId
             && k.CompId == KontoGlobals.CompanyId);
            if (adl1 != null)
            {
                Adla1Req = true;
                Adlp1Req = true;
                if (adl1.PerCap != "")
                {
                    Adlp1 = adl1.PerCap;
                }
                else
                {
                    Adlp1Req = false;
                }

                pm1 = adl1.PlusMinus;
                Adla1 = adl1.AmtCap;
            }
            else
            {
                Adlp1Req = false;
                Adla1Req = false;
            }

            var adl2 = db.RPSets.FirstOrDefault(k => k.Field == "Adl2" && k.RecPay == Vtype && k.YearId == KontoGlobals.YearId
             && k.CompId == KontoGlobals.CompanyId);
            if (adl2 != null)
            {
                Adla2Req = true;
                Adlp2Req = true;
                if (adl2.PerCap != "")
                {
                    Adlp2 = adl2.PerCap;
                }
                else
                {
                    Adlp2Req = false;
                }
                Adla2 = adl2.AmtCap;
                pm2 = adl2.PlusMinus;
            }
            else
            {
                Adlp2Req = false;
                Adla2Req = false;
            }

            var adl3 = db.RPSets.FirstOrDefault(k => k.Field == "Adl3" && k.RecPay == Vtype && k.YearId == KontoGlobals.YearId
             && k.CompId == KontoGlobals.CompanyId);
            if (adl3 != null)
            {
                Adla3Req = true;
                Adlp3Req = true;
                if (adl3.PerCap != "")
                {
                    Adlp3 = adl3.PerCap;
                }
                else
                {
                    Adlp3Req = false;
                }
                Adla3 = adl3.AmtCap;
                pm3 = adl3.PlusMinus;
            }
            else
            {
                Adlp3Req = false;
                Adla3Req = false;
            }

            var adl4 = db.RPSets.FirstOrDefault(k => k.Field == "Adl4" && k.RecPay == Vtype && k.YearId == KontoGlobals.YearId
             && k.CompId == KontoGlobals.CompanyId);
            if (adl4 != null)
            {
                Adla4Req = true;
                Adlp4Req = true;
                if (adl4.PerCap != "")
                {
                    Adlp4 = adl4.PerCap;
                }
                else
                {
                    Adlp4Req = false;
                }
                Adla4 = adl4.AmtCap;
                pm4 = adl4.PlusMinus;
            }
            else
            {
                Adlp4Req = false;
                Adla4Req = false;
            }

            var adl5 = db.RPSets.FirstOrDefault(k => k.Field == "Adl5" && k.RecPay == Vtype && k.YearId == KontoGlobals.YearId
             && k.CompId == KontoGlobals.CompanyId);
            if (adl5 != null)
            {
                Adla5Req = true;
                Adlp5Req = true;
                if (adl5.PerCap != "")
                {
                    Adlp5 = adl5.PerCap;
                }
                else
                {
                    Adlp5Req = false;
                }
                Adla5 = adl5.AmtCap;
                pm5 = adl5.PlusMinus;
            }
            else
            {
                Adlp5Req = false;
                Adla5Req = false;
            }

            var adl6 = db.RPSets.FirstOrDefault(k => k.Field == "Adl6" && k.RecPay == Vtype && k.YearId == KontoGlobals.YearId
             && k.CompId == KontoGlobals.CompanyId);
            if (adl6 != null)
            {
                Adla6Req = true;
                Adlp6Req = true;
                if (adl6.PerCap != "")
                {
                    Adlp6 = adl6.PerCap;
                }
                else
                {
                    Adlp6Req = false;
                }
                Adla6 = adl6.AmtCap;
                pm6 = adl6.PlusMinus;
            }
            else
            {
                Adlp6Req = false;
                Adla6Req = false;
            }

            var adl7 = db.RPSets.FirstOrDefault(k => k.Field == "Adl7" && k.RecPay == Vtype && k.YearId == KontoGlobals.YearId
             && k.CompId == KontoGlobals.CompanyId);
            if (adl7 != null)
            {
                Adla7Req = true;
                Adlp7Req = true;
                if (adl7.PerCap != "")
                {
                    Adlp7 = adl7.PerCap;
                }
                else
                {
                    Adlp7Req = false;
                }
                Adla7 = adl7.AmtCap;
                pm7 = adl7.PlusMinus;
            }
            else
            {
                Adlp7Req = false;
                Adla7Req = false;
            }
            var adl8 = db.RPSets.FirstOrDefault(k => k.Field == "Adl8" && k.RecPay == Vtype 
            && k.YearId == KontoGlobals.YearId && k.CompId == KontoGlobals.CompanyId);
            if (adl8 != null)
            {
                Adla8Req = true;
                Adlp8Req = true;
                if (adl8.PerCap != "")
                {
                    Adlp8 = adl8.PerCap;
                }
                else
                {
                    Adlp8Req = false;
                }
                Adla8 = adl8.AmtCap;
                pm8 = adl8.PlusMinus;
            }
            else
            {
                Adlp8Req = false;
                Adla8Req = false;
            }
            var adl9 = db.RPSets.FirstOrDefault(k => k.Field == "Adl9" && k.RecPay == Vtype 
            && k.YearId == KontoGlobals.YearId && k.CompId == KontoGlobals.CompanyId);
            if (adl9 != null)
            {
                Adla9Req = true;
                Adlp9Req = true;
                if (adl9.PerCap != "")
                {
                    Adlp9 = adl9.PerCap;
                }
                else
                {
                    Adlp9Req = false;
                }
                Adla9 = adl9.AmtCap;
                pm9 = adl9.PlusMinus;
            }
            else
            {
                Adlp9Req = false;
                Adla9Req = false;
            }
            var adl10 = db.RPSets.FirstOrDefault(k => k.Field == "Adl10" && k.RecPay == Vtype 
                    && k.YearId == KontoGlobals.YearId && k.CompId == KontoGlobals.CompanyId);
            if (adl10 != null)
            {
                Adla10Req = true;
                Adlp10Req = true;
                if (adl10.PerCap != "")
                {
                    Adlp10 = adl10.PerCap;
                }
                else
                {
                    Adlp10Req = false;
                }
                Adla10 = adl10.AmtCap;
                pm10 = adl10.PlusMinus;
            }
            else
            {
                Adlp10Req = false;
                Adla10Req = false;
            }
        }
        private void SetGridColumn()
        {
            colAdla1.Visible = Adla1Req;
            colAdla1.Caption = Adla1;
            colAdlp1.Visible = Adlp1Req;
            colAdlp1.Caption = Adlp1;
            colAdla2.Visible = Adla2Req;
            colAdla2.Caption = Adla2;
            colAdlp2.Visible = Adlp2Req;
            colAdlp2.Caption = Adlp2;
            colAdla3.Visible = Adla3Req;
            colAdla3.Caption = Adla3;
            colAdlp3.Visible = Adlp3Req;
            colAdlp3.Caption = Adlp3;

            colAdla4.Visible = Adla4Req;
            colAdla4.Caption = Adla4;
            colAdlp4.Visible = Adlp4Req;
            colAdlp4.Caption = Adlp4;

            colAdlp5.Visible = Adlp5Req;
            colAdlp5.Caption = Adlp5;
            colAdla5.Visible = Adla5Req;
            colAdla5.Caption = Adla5;

            colAdla6.Visible = Adla6Req;
            colAdla6.Caption = Adla6;
            colAdlp6.Visible = Adlp6Req;
            colAdlp6.Caption = Adlp6;

            colAdla7.Visible = Adla7Req;
            colAdla7.Caption = Adla7;
            colAdlp7.Visible = Adlp7Req;
            colAdlp7.Caption = Adlp7;

            colAdla8.Visible = Adla8Req;
            colAdla8.Caption = Adla8;
            colAdlp8.Visible = Adlp8Req;
            colAdlp8.Caption = Adlp8;

            colAdla9.Visible = Adla9Req;
            colAdla9.Caption = Adla9;
            colAdlp9.Visible = Adlp9Req;
            colAdlp9.Caption = Adlp9;

            colAdlp10.Visible = Adlp10Req;
            colAdlp10.Caption = Adlp10;
            colAdla10.Visible = Adla10Req;
            colAdla10.Caption = Adla10;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.F2 | Keys.Control))
            {
                var frm = new GridPropertView();
                frm.gridControl1.DataSource = this.gridControl1.DataSource;
                frm.gridView1.Assign(this.gridView1, false);
                if (frm.ShowDialog() != DialogResult.OK) return true; ;
                this.gridView1.Assign(frm.gridView1, false);
                KontoUtils.SaveLayoutGrid(this.GridLayoutFile, this.gridView1);
                return true;
            }
            else if(keyData == (Keys.F1 | Keys.Shift))
            {
                KontoUtils.SaveLayoutGrid(this.GridLayoutFile, this.gridView1);
                return true;
            }
                return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
