using DevExpress.XtraEditors;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Pos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Pos.Sales
{
    public partial class SalesPayView : KontoForm
    {
        public KontoContext db { get; set; }
        public string BillNo { get; set; }
        public int BillId { get; set; }
        public decimal BillAmt { get; set; }

        public BillPay BP { get; set; }
        public SalesPayView()
        {
            InitializeComponent();
            this.Load += SalesPayView_Load;

            this.pay1kontoSpinEdit.EditValueChanged += Pay1kontoSpinEdit_EditValueChanged;
            this.pay2kontoSpinEdit.EditValueChanged += Pay1kontoSpinEdit_EditValueChanged;
            this.pay3kontoSpinEdit.EditValueChanged += Pay1kontoSpinEdit_EditValueChanged;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.disckontoSpinEdit3.EditValueChanged += Pay1kontoSpinEdit_EditValueChanged;
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            var bp = new BillPay();
            bp.IsActive = true;
            bp.BillId = this.BillId;
            bp.DiscAmt = disckontoSpinEdit3.Value;
            bp.Pay1Id = Convert.ToInt32(mode1lookUpEdit.EditValue);
            
            if(!string.IsNullOrEmpty(mode2lookUpEdit.Text))
               bp.Pay2Id= Convert.ToInt32(mode2lookUpEdit.EditValue);

            if (!string.IsNullOrEmpty(mode3lookUpEdit.Text))
                bp.Pay3Id = Convert.ToInt32(mode3lookUpEdit.EditValue);

            bp.Pay1Amt = pay1kontoSpinEdit.Value;
            bp.Pay2Amt = pay2kontoSpinEdit.Value;
            bp.Pay3Amt = pay3kontoSpinEdit.Value;

            bp.ChangeAmt = changekontoSpinEdit.Value;

            bp.RefNo1 = ref1kontoTextBoxExt.Text.Trim();
            bp.RefNo2 = ref2kontoTextBoxExt.Text.Trim();
            bp.PayDate = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));

            db.BillPays.Add(bp);

            this.BP = bp;
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void Pay1kontoSpinEdit_EditValueChanged(object sender, EventArgs e)
        {
            Calc();
            //var edit = sender as SpinEdit;
            //if(edit.Name == "pay1kontoSpinEdit")
            //{
            //    var pend = this.BillAmt - partkontoSpinEdit2.Value - disckontoSpinEdit3.Value;
            //    pend = pend - pay1kontoSpinEdit.Value - pay2kontoSpinEdit.Value - pay3kontoSpinEdit.Value;

            //    changekontoSpinEdit.Value = pend;
            //}
        }
        private void Calc()
        {
            var pend = this.BillAmt - partkontoSpinEdit2.Value - disckontoSpinEdit3.Value;
            
            var cash = pay1kontoSpinEdit.Value;
            if (cash > pend)
                changekontoSpinEdit.Value = cash - pend;
            else
                changekontoSpinEdit.Value = 0;

            pend = pend - cash + changekontoSpinEdit.Value - pay2kontoSpinEdit.Value - pay3kontoSpinEdit.Value;

            pendkontoTextEdit2.Text = pend.ToString("F");
        }
        private void SalesPayView_Load(object sender, EventArgs e)
        {
            this.Text = "Receipt =>  Bill No: " + this.BillNo;
            billAmtkontoTextEdit1.Text = this.BillAmt.ToString("F");

            Calc();
            if (db == null)
                db = new KontoContext();

                var modes = db.Hastes.Where(x => x.MType == "PM" && !x.IsDeleted).ToList();
            mode1lookUpEdit.Properties.DataSource = modes;
            mode2lookUpEdit.Properties.DataSource = modes;
            mode3lookUpEdit.Properties.DataSource = modes;

            var mod1 = modes.FirstOrDefault(x => x.PanNo == "CASH");

            if (mod1 != null)
            {
                mode1lookUpEdit.EditValue = mod1.Id;
                mode1lookUpEdit.ReadOnly = true;
                mode1lookUpEdit.TabStop = false;
                this.ActiveControl = pay1kontoSpinEdit;
            }
            
        }
    }
}
