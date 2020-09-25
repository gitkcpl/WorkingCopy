using DevExpress.Data.WcfLinq.Helpers;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Admin;
using Konto.Data.Models.Reports;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Shared.Security
{
    public partial class MenuIndex : KontoForm
    {
        public MenuIndex()
        {
            InitializeComponent();
            this.Load += MenuIndex_Load;
            this.FormClosed += MenuIndex_FormClosed;
        }

        private void MenuIndex_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage != null)
            {
                var tabcontrol = tabpage.Parent as TabControlAdv;
                if (tabcontrol != null)
                    tabcontrol.TabPages.Remove(tabpage);
            }
        }

        private void MenuIndex_Load(object sender, EventArgs e)
        {
            var lst = new List<ErpModule>();
            using(var db = new KontoContext())
            {
                lst = db.ErpModules.Where(x => !x.IsDeleted).OrderBy(x => x.Id).ToList();

            }
            bindingSource1.DataSource = lst;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            using (KontoContext db = new KontoContext())
            {
                var Lst = db.Database.SqlQuery<BalDto>(
               "dbo.Bal_sheet @CompanyId={0},@FromDate={1},@ToDate={2},@YearId={3},@Summary={4},@zero={5}",
               Convert.ToInt32(KontoGlobals.CompanyId), KontoGlobals.FromDate, KontoGlobals.ToDate,
               KontoGlobals.YearId, "N",1).ToList().Where(X => X.TransType == 3);

               
                //var accbal = db.AccBals.Where(x => x.YearId == KontoGlobals.YearId && x.CompId == KontoGlobals.CompanyId);
                var lastyear = db.FinYears.Where(x => x.FromDate > KontoGlobals.FromDate).OrderBy(x => x.FromDate).FirstOrDefault();
                if (Lst != null)
                {
                    try
                    {
                        foreach (var item in Lst)
                        {
                            var _accbalnext = db.AccBals.Where(x => x.AccId == item.AcId && x.YearId == lastyear.Id && x.CompId == KontoGlobals.CompanyId).FirstOrDefault();
                            if (_accbalnext != null)
                            {
                                if (item.Bal > 0)
                                    _accbalnext.OpDebit = item.Bal;
                                else
                                    _accbalnext.OpCredit = -1 * item.Bal;

                                _accbalnext.OpBal = item.Bal;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                db.SaveChanges();
                MessageBox.Show("Balance Transfer Successfully");
            }
        }
    
    }
}
