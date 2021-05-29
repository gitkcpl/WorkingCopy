using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Masters;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Masters.Acc
{
    public partial class TcsView : KontoForm
    {
        public int AccId { get; set; }
        public  int TcsAcId { get; set; }
        public  decimal TcsPer { get; set; }
        public TcsView()
        {
            InitializeComponent();
            this.Load += DeprView_Load;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;

        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if(Convert.ToInt32(this.accLookup1.SelectedValue) == 0)
                {
                    MessageBox.Show("Please select account");
                    this.accLookup1.buttonEdit1.Focus();
                    return;
                }

                if (this.AccId != 0)
                {
                    using (var db = new KontoContext())
                    {
                        var model = db.AccOthers.FirstOrDefault(x => x.AccId == this.AccId);
                        if (model == null)
                            model = new AccOtherModel();
                        model.TcsAccId = Convert.ToInt32(accLookup1.SelectedValue);
                        model.TcsPer = perNumericUpDown.Value;

                        if (model.Id == 0)
                        {
                            model.AccId = this.AccId;
                            model.RowId = Guid.NewGuid();
                            db.AccOthers.Add(model);
                        }

                        db.SaveChanges();
                    }

                    MessageBoxAdv.Show(this, KontoGlobals.SaveMessage, "Saved !", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                this.TcsPer = perNumericUpDown.Value;
                this.TcsAcId = Convert.ToInt32(accLookup1.SelectedValue);
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Depr Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());

            }
        }

        private void DeprView_Load(object sender, EventArgs e)
        {
            using(var db = new KontoContext())
            {
                var model = db.AccOthers.FirstOrDefault(x => x.AccId == this.AccId);
                if (model != null)
                {
                    accLookup1.SelectedValue = model.TcsAccId;
                    accLookup1.SetAcc(Convert.ToInt32(model.TcsAccId));
                    perNumericUpDown.Value = Convert.ToDecimal(model.TcsPer);
                }
                else if(TcsAcId > 0)
                {
                    accLookup1.SelectedValue = this.TcsAcId;
                    accLookup1.SetAcc(this.TcsAcId);
                    perNumericUpDown.Value = this.TcsPer;
                }
            }
        }
    }
}
