using Konto.App.Shared;
using Konto.Core.Shared;
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

namespace Konto.Shared.Masters.Acc
{
    public partial class InterestView : KontoForm
    {
        public int AccId { get; set; }
        public InterestView()
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
                if(Convert.ToInt32(this.interestAccLookup.SelectedValue) == 0)
                {
                    MessageBox.Show("Please select account");
                    this.tdsAccLookup.buttonEdit1.Focus();
                    return;
                }
                using(var db = new KontoContext())
                {
                    var model = db.AccOthers.FirstOrDefault(x => x.AccId == this.AccId);
                    if (model == null)
                        model = new AccOtherModel();
                    model.TdsAccId = Convert.ToInt32(tdsAccLookup.SelectedValue);
                    model.IntTdsPer = tdsPerspinEdit.Value;
                    model.IntPer = intPerSpinEdit.Value;
                    model.IntAccId = Convert.ToInt32(interestAccLookup.SelectedValue);
                    model.TdsDrCr = drCrComboBoxEx.SelectedValue.ToString();
                    if (model.Id == 0)
                    {
                        model.AccId = this.AccId;
                        model.RowId = Guid.NewGuid();
                        db.AccOthers.Add(model);
                    }
                    db.SaveChanges();
                }
                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            drCrComboBoxEx.DisplayMember = "_Key";
            drCrComboBoxEx.ValueMember = "_Value";
            List<ComboBoxPairs> cbdr = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Debit", "Dr"),
                new ComboBoxPairs("Credit", "Cr")
            };
            drCrComboBoxEx.DataSource = cbdr;
            drCrComboBoxEx.SelectedIndex = 0;

            using (var db = new KontoContext())
            {
                var model = db.AccOthers.FirstOrDefault(x => x.AccId == this.AccId);
                if (model != null)
                {
                    intPerSpinEdit.Value = Convert.ToDecimal(model.IntPer);
                    interestAccLookup.SelectedValue = model.IntAccId;
                    interestAccLookup.SetAcc(Convert.ToInt32(model.IntAccId));

                    tdsAccLookup.SelectedValue = model.IntTdsAccId;
                    tdsAccLookup.SetAcc(Convert.ToInt32(model.IntTdsAccId));
                    drCrComboBoxEx.SelectedValue = model.TdsDrCr;

                    tdsPerspinEdit.Value = Convert.ToDecimal(model.IntTdsPer);
                }
            }
        }
    }
}
