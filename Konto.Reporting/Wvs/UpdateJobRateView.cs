using Konto.Core.Shared;
using Konto.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Reporting.Wvs
{
    public partial class UpdateJobRateView : KontoForm
    {
        public UpdateJobRateView()
        {
            InitializeComponent();
            fromDateEdit.EditValue = DateTime.Now;
            toDateEdit.EditValue = DateTime.Now;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (rateSpinEdit.Value == 0) return;
                int fdate = Convert.ToInt32(fromDateEdit.DateTime.ToString("yyyyMMdd"));
                int tdate = Convert.ToInt32(toDateEdit.DateTime.ToString("yyyyMMdd"));
                int macid = 0;int qualityid = 0;int empid = 0;

                if (!string.IsNullOrEmpty(MachineNolookUpEdit.Text))
                    macid = Convert.ToInt32(MachineNolookUpEdit.EditValue);

                qualityid = Convert.ToInt32(productLookup1.SelectedValue);

                empid = Convert.ToInt32(empLookup1.SelectedValue);

                using (var _db = new KontoContext())
                {
                    var list = (from o in _db.Prods
                                join e1 in _db.Prod_Emps on o.Id equals e1.ProdId
                                where (macid == 0 || o.MacId == macid) && !o.IsDeleted && o.IsActive && (qualityid == 0 || o.ProductId == qualityid)
                                && (empid == 0 || e1.EmpId == empid) && e1.IsActive && !e1.IsDeleted
                                && o.VoucherDate >= fdate && o.VoucherDate <= tdate
                                select new { e1.Id }).ToList();

                    foreach (var item in list)
                    {
                        var pemp = _db.Prod_Emps.Find(item.Id);
                        if (pemp != null)
                        {
                            pemp.Rate = rateSpinEdit.Value;
                        }
                    }

                    _db.SaveChanges();
                }
                MessageBox.Show("Rate Updated !!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
