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
        List<ErpModule> erpModules = new List<ErpModule>();

        List<ErpModule> editErpModules = new List<ErpModule>();
        public MenuIndex()
        {
            InitializeComponent();


            this.FirstActiveControl = gridControl1;

            this.Load += MenuIndex_Load;
            this.FormClosed += MenuIndex_FormClosed;
            this.addMenuSimpleButton.Click += AddMenuSimpleButton_Click;
            this.gridView1.CellValueChanged += GridView1_CellValueChanged;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.Shown += MenuIndex_Shown;
        }

        private void MenuIndex_Shown(object sender, EventArgs e)
        {
            if (KontoGlobals.UserName.ToUpper() != "KEYSOFT")
            {
                MessageBox.Show("Unathorised Access !!");
                this.Close();
                 this.Dispose();
                return;
            }
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            using(var db = new KontoContext())
            {
                foreach (var em in erpModules)
                {
                    var pkg = db.Menu_Packages.FirstOrDefault(x => x.PackageId == KontoGlobals.PackageId
                                                    && x.MenuId == em.Id);
                    if(pkg== null)
                    {
                        pkg = new Menu_PackageModel();
                        pkg.MenuId = em.Id;
                        pkg.PackageId = KontoGlobals.PackageId;
                        db.Menu_Packages.Add(pkg);
                    }
                }

                foreach (var em in editErpModules)
                {
                    var mu = db.ErpModules.Find(em.Id);
                    if (mu != null)
                    {
                        mu.ModuleDesc = em.ModuleDesc;
                        mu.MainAssembly = em.MainAssembly;
                        mu.AssemblyName = em.AssemblyName;
                        mu.Visible = em.Visible;
                        mu.OrderIndex = em.OrderIndex;
                        mu.ParentId = em.ParentId;
                        mu.ShortCutKey = em.ShortCutKey;
                        mu.Title = em.Title;
                        mu.IsSeprator = em.IsSeprator;
                    }
                }

                db.SaveChanges();

                MessageBox.Show("Menu & package Updated Successfully");
            }
            this.Close();
            this.Dispose();
        }

        private void GridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var rw = gridView1.GetRow(e.RowHandle) as ErpModule;
            if (rw != null)
                editErpModules.Add(rw);
        }

        private void AddMenuSimpleButton_Click(object sender, EventArgs e)
        {
            var frm = new AddMenuView();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                erpModules = new List<ErpModule>();
                var lst = this.bindingSource1.DataSource as List<ErpModule>;

                var rows = frm.gridView1.GetSelectedRows();
                foreach (var item in rows)
                {
                    var em = frm.gridView1.GetRow(item) as ErpModule;
                    this.erpModules.Add(em);
                    lst.Add(em);
                }

                gridControl1.RefreshDataSource();
            }
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
                //lst =  db.ErpModules.Where(x => !x.IsDeleted).OrderBy(x => x.Id).ToList();
                lst = (from p in db.ErpModules
                       join pk in db.Menu_Packages on p.Id equals pk.MenuId
                       where pk.PackageId == KontoGlobals.PackageId && !p.IsDeleted
                       orderby p.Id
                       select p
                       ).ToList();

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
