using AutoUpdaterDotNET;
using DevExpress.Data.Helpers;
using DevExpress.XtraBars;
using DevExpress.XtraSplashScreen;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Admin;
using Konto.Shared.Masters.Branch;
using Konto.Shared.Masters.Comp;
using Konto.Shared.Masters.FinYear;
using Konto.Shared.Masters.LogIn;
using KontoWin.Db;
using Microsoft.SqlServer.Dac;
using Serilog;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace KontoWin
{
    public partial class Form1 : KontoForm
    {
        public Form1()
        {
            InitializeComponent();
            compBarStaticItem.ItemClick += CompBarStaticItem_ItemClick;
            branchBarStaticItem.ItemClick += BranchBarStaticItem_ItemClick;
            yearBarStaticItem.ItemClick += YearBarStaticItem_ItemClick;
            userBarStaticItem.ItemClick += UserBarStaticItem_ItemClick;
            treeList1.DoubleClick += TreeList1_DoubleClick;
            treeList1.KeyDown += TreeList1_KeyDown;
        }

        private void TreeList1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            OpenFromTree();
        }

        private void TreeList1_DoubleClick(object sender, EventArgs e)
        {
            OpenFromTree();
        }

        private void UserBarStaticItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if(MessageBox.Show("Are you sure want to switch user ?", "User Changed !", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (tabControlAdv1.TabPages.Count > 1)
                    this.tabControlAdv1.TabPages.RemoveRange(1, tabControlAdv1.TabPages.Count - 1);

                var frm = new LogInWindowView();
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    var frmc = new SelectCompanyView();
                    if (frmc.ShowDialog() == DialogResult.OK)
                    {
                        compBarStaticItem.Caption = frmc.Company.CompName;
                        var frmBr = new SelectBranchWindow();
                        if (frmBr.ShowDialog() == DialogResult.OK)
                        {
                            branchBarStaticItem.Caption = frmBr.Branch.BranchName;
                            var frmy = new SelectYearView();
                            if (frmy.ShowDialog() == DialogResult.OK)
                            {
                                yearBarStaticItem.Caption = frmy.FinYear.YearCode;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                        userBarStaticItem.Caption = KontoGlobals.UserName;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }

            }
        }

        private void YearBarStaticItem_ItemClick(object sender, ItemClickEventArgs e)
        {

            //foreach (Form OpenFrm in this.tabControlAdv1.TabPages)
            //{
            //    if (OpenFrm.Name == "tabPageAdv1") continue;
            //    this.tabControlAdv1.TabPages.Remove(OpenFrm);
            //}

            if(tabControlAdv1.TabPages.Count > 1)
                this.tabControlAdv1.TabPages.RemoveRange(1, tabControlAdv1.TabPages.Count - 1);

            var frm = new SelectYearView();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                yearBarStaticItem.Caption = frm.FinYear.YearCode;
            }
            
        }

        private void BranchBarStaticItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (tabControlAdv1.TabPages.Count > 1)
                this.tabControlAdv1.TabPages.RemoveRange(1, tabControlAdv1.TabPages.Count - 1);
            var frm = new SelectBranchWindow();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                branchBarStaticItem.Caption = frm.Branch.BranchName;
            }
        }

        private void CompBarStaticItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (tabControlAdv1.TabPages.Count > 1)
                this.tabControlAdv1.TabPages.RemoveRange(1, tabControlAdv1.TabPages.Count - 1);
            var frm = new SelectCompanyView();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                compBarStaticItem.Caption = frm.Company.CompName;
                KontoGlobals.CompanyName = frm.Company.CompName;
                KontoGlobals.GstIn = frm.Company.GstIn;
            }
        }

        List<ErpModule> erp = new List<ErpModule>();
        private void Form1_Load(object sender, EventArgs e)
        {
           
            var frmg = new SelectGroupWindow();
            frmg.ShowDialog();
            if(frmg.DialogResult!= DialogResult.OK)
            {
                Application.Exit();
                return;
            }
            
           

            string constring = ConfigurationManager.ConnectionStrings["KontoContext"].ConnectionString;

               var sqlconbuilder = new SqlConnectionStringBuilder(constring);

                sqlconbuilder.InitialCatalog = "Master";

               if (!DacpacService.CheckDatabaseExists(sqlconbuilder.ConnectionString, KontoGlobals.DbName))
               {
                    var dbst = new DbSetting();
                    dbst.ShowDialog();
               }

            KontoGlobals.sqlConnectionString.InitialCatalog = KontoGlobals.DbName;

            var frm = new LogInWindowView();
            frm.IsStartup = true;
            if(frm.ShowDialog(this) == DialogResult.OK)
            {
                var frmc = new SelectCompanyView();
                if(frmc.ShowDialog() == DialogResult.OK)
                {
                    compBarStaticItem.Caption = frmc.Company.CompName;
                    KontoGlobals.CompanyName = frmc.Company.CompName;
                    KontoGlobals.GstIn = frmc.Company.GstIn;
                    var frmBr = new SelectBranchWindow();
                    if(frmBr.ShowDialog() == DialogResult.OK)
                    {
                        branchBarStaticItem.Caption = frmBr.Branch.BranchName;
                        var frmy = new SelectYearView();
                        if(frmy.ShowDialog() == DialogResult.OK)
                        {
                            yearBarStaticItem.Caption = frmy.FinYear.YearCode;
                        }
                        else
                        {
                            Application.Exit();
                            return;
                        }
                    }
                    else
                    {
                        Application.Exit();
                        return;
                    }
                    userBarStaticItem.Caption = KontoGlobals.UserName;
                }
                else
                {
                    Application.Exit();
                    return;
                }
            }
            else
            {
                Application.Exit();
                return;
            }


            var splash = new SplashScreenManager(this, typeof(WaitForm1), true, true);
            splash.ShowWaitForm();


            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Office 2019 White");

            using (var db = new KontoContext())
            {
                var role = db.Roles.Find(KontoGlobals.UserRoleId);
                if (role.IsSysAdmin)
                {

                    erp = (from em in db.ErpModules
                           join pkg in db.Menu_Packages on em.Id equals pkg.MenuId into pkg_join
                           from pkg in pkg_join.DefaultIfEmpty()
                           where (em.IsActive && !em.IsDeleted && em.Visible == true
                                   && pkg.PackageId == KontoGlobals.PackageId && em.ModuleDesc!="-")
                           select em).ToList();
                }
                else
                {
                    var erpuser = (from em in db.ErpModules
                                   join pkg in db.Menu_Packages on em.Id equals pkg.MenuId into pkg_join
                                   from pkg in pkg_join.DefaultIfEmpty()

                                   join p in db.Permissions on pkg.MenuId equals p.ModuleId into pm_join
                                   from pm in pm_join.DefaultIfEmpty()

                                   join rp in db.RolePermissions on pm.Id equals rp.PermissionId into rp_join
                                   from rp in rp_join.DefaultIfEmpty()
                                   where (rp.RoleId == KontoGlobals.UserRoleId && em.IsActive && !em.IsDeleted
                                   && pkg.PackageId == KontoGlobals.PackageId && em.ModuleDesc != "-")
                                   orderby em.ParentId, em.OrderIndex
                                   group new { em } by new
                                   {
                                       em.Id,
                                       em.ModuleDesc,
                                       em.ParentId,
                                       em.IconPath,
                                       em.MainAssembly,
                                       em.ListAssembly,
                                       em.AssemblyName,
                                       em.Title,
                                       em.IsSeprator,
                                       em.OrderIndex,
                                       em.ImageIndex
                                   } into erpGrp
                                   select new
                                   {
                                       erpGrp.Key.Id,
                                       erpGrp.Key.ModuleDesc,
                                       erpGrp.Key.ParentId,
                                       erpGrp.Key.IconPath,
                                       erpGrp.Key.IsSeprator,
                                       erpGrp.Key.MainAssembly,
                                       erpGrp.Key.ListAssembly,
                                       erpGrp.Key.AssemblyName,
                                       erpGrp.Key.Title,
                                       erpGrp.Key.OrderIndex,erpGrp.Key.ImageIndex
                                   }).ToList();

                    var lst = erpuser.GroupBy(k => k.ParentId).Select(g => new { id = g.Key }).ToList();

                    foreach (var id in lst)
                    {
                        int Id = (int)id.id;
                        if (Id == 0) continue;
                        var dr = db.ErpModules.FirstOrDefault(k => k.Id == Id && k.IsActive && !k.IsDeleted);
                        if (dr != null)
                        {
                            int parntid = (int)dr.ParentId;

                            do
                            {
                                var nr = new ErpModule
                                {
                                    Id = (int)dr.Id,
                                    ModuleDesc = dr.ModuleDesc,
                                    ParentId = dr.ParentId,
                                    IconPath = dr.IconPath,
                                    IsSeprator = dr.IsSeprator,
                                    MainAssembly = dr.MainAssembly,
                                    ListAssembly = dr.ListAssembly,
                                    AssemblyName = dr.AssemblyName,
                                    Title = dr.Title,
                                    OrderIndex = dr.OrderIndex,
                                    ImageIndex = dr.ImageIndex
                                };
                                if (!erp.Any(x => x.Id == nr.Id))
                                    erp.Add(nr);
                                
                                //TMainMenus.Add(nr);

                                dr = db.ErpModules.FirstOrDefault(k => k.Id == parntid && k.IsActive && !k.IsDeleted);
                                if (dr != null)
                                {
                                    parntid = (int)dr.ParentId;
                                    if (dr.ParentId == 0)
                                    {
                                        var _ex = erp.FirstOrDefault(x => x.Id == dr.Id);
                                        if (_ex == null)
                                        {
                                            nr = new ErpModule
                                            {
                                                Id = (int)dr.Id,
                                                ModuleDesc = dr.ModuleDesc,
                                                ParentId = dr.ParentId,
                                                IconPath = dr.IconPath,
                                                IsSeprator = dr.IsSeprator,
                                                MainAssembly = dr.MainAssembly,
                                                ListAssembly = dr.ListAssembly,
                                                AssemblyName = dr.AssemblyName,
                                                Title = dr.Title,
                                                OrderIndex = dr.OrderIndex,
                                                ImageIndex = dr.ImageIndex
                                            };
                                            if (!erp.Any(x => x.Id == nr.Id))
                                                erp.Add(nr);
                                            
                                            // TMainMenus.Add(nr);
                                        }
                                    }
                                }
                                else
                                {
                                    parntid = 0;

                                }


                            } while (parntid != 0);
                        }
                    }

                    foreach (var dr in erpuser)
                    {
                        var nr = new ErpModule
                        {
                            Id = dr.Id,
                            ModuleDesc = dr.ModuleDesc,
                            ParentId = dr.ParentId,
                            IconPath = dr.IconPath,
                            IsSeprator = dr.IsSeprator,
                            MainAssembly = dr.MainAssembly,
                            ListAssembly = dr.ListAssembly,
                            AssemblyName = dr.AssemblyName,
                            Title = dr.Title,
                            ImageIndex = dr.ImageIndex
                        };
                        //if (nr.ParentId != 0)
                            //nr.MenuCommand = new RelayCommand(ExecuteMenu, CanExecuteMenu);
                        if(!erp.Any(x=>x.Id == nr.Id))
                            erp.Add(nr);
                        //if (!nr.IsSeprator)
                          //  TMainMenus.Add(nr);
                    }
                }

                
            }


            erp = erp.OrderBy(x => x.ParentId).ThenBy(x => x.OrderIndex).ToList();
            foreach (var item in erp)
            {
                if(item.ParentId ==0 )
                    CreateMenuItem(item, true);
                else
                {
                    
                    var _top = erp.FirstOrDefault(x => x.ParentId == item.Id);
                    if (_top!=null)
                    {
                        CreateMenuItem(item, true);
                    }
                    else
                    CreateMenuItem(item, false);
                    
                }
       
            }
            var lst12 = erp.Where(x => x.Id == 100).ToList();

            treeList1.DataSource = erp;

            splash.CloseWaitForm();
            SetToolBar();
        }
       
        void CreateMenuItem(ErpModule _erp, bool _isgroup)
        {
            if (_isgroup)
            {
                BarItem b2 = barManager1.Items["menu" +_erp.Id];
                if (b2 != null) return;

                BarSubItem mnu = new BarSubItem(barManager1, _erp.ModuleDesc);
                
                mnu.Name = "menu" + _erp.Id;
                mnu.Id = Convert.ToInt32(_erp.Id);
                if (_erp.ImageIndex != null)
                    mnu.ImageIndex = (int)_erp.ImageIndex;

                //if (dr["IconID"] != null && dr["IconID"] != DBNull.Value)
                //    mnu.ImageIndex = Convert.ToInt32(dr["IconID"]);

                if (_erp.ParentId.ToString() != "0")
                {
                    //DataRow[] dttemp = dtMenu.Select("MenuID=" + dr["ParentID"]);
                    var temp = erp.FirstOrDefault(x => x.Id == _erp.ParentId);

                    BarSubItem b3 = (BarSubItem)barManager1.Items["menu" + temp.Id];
                    b3.ItemLinks.Add(mnu);
                    // b3.ItemLinks.Add(mnu).BeginGroup = Convert.ToBoolean(_erp.IsSeprator);
                }
                else
                    bar2.ItemLinks.Add(mnu);
                  //  bar2.ItemLinks.Add(mnu).BeginGroup = Convert.ToBoolean(dr["BeginGroup"]);
            }
            else
            {
                BarButtonItem b1 = new BarButtonItem(barManager1, _erp.ModuleDesc);
                b1.Name = "menu" + _erp.Id;
                b1.Tag = _erp.MainAssembly + ";" + _erp.AssemblyName;
                b1.Id = _erp.Id;
                
                if(_erp.ImageIndex!=null)
                    b1.ImageIndex = (int) _erp.ImageIndex;

                b1.ItemClick += B1_ItemClick;
                barManager1.Items.Add(b1);
                var temp = erp.FirstOrDefault(x => x.Id == _erp.ParentId);
                if (temp != null)
                {
                    BarSubItem b2 = (BarSubItem)barManager1.Items["menu" + temp.Id];
                    if (b2 != null)
                        b2.ItemLinks.Add(b1);
                }

            }
        }

        private void OpenFromTree()
        {
            var dr = treeList1.GetFocusedRow() as ErpModule;
            if (dr == null || dr.AssemblyName== null) return;
            try
            {
                var pg1 = new TabPageAdv();
                pg1.Text = dr.ModuleDesc;
                string objectToInstantiate = dr.AssemblyName + "," + dr.MainAssembly;
                var objectType = Type.GetType(objectToInstantiate);
                var _frm = Activator.CreateInstance(objectType) as MetroForm;
                // var _frm = Activator.CreateInstance(_assmbly[0], _assmbly[1]).Unwrap() as MetroForm;
                _frm.Tag = dr.Id;
                _frm.TopLevel = false;

                tabControlAdv1.TabPages.Add(pg1);
                tabControlAdv1.SelectedTab = pg1;
                _frm.Parent = pg1;
                _frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - _frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - _frm.Height / 2);
                _frm.Show();// = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void B1_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var _assmbly = e.Item.Tag.ToString().Split(';');

                var pg1 = new TabPageAdv();
                pg1.Text = e.Item.Caption;
                string objectToInstantiate = _assmbly[1] + "," + _assmbly[0];
                var objectType = Type.GetType(objectToInstantiate);
                var _frm = Activator.CreateInstance(objectType) as MetroForm;
                // var _frm = Activator.CreateInstance(_assmbly[0], _assmbly[1]).Unwrap() as MetroForm;
                _frm.Tag = e.Item.Id;
                _frm.TopLevel = false;

                tabControlAdv1.TabPages.Add(pg1);
                tabControlAdv1.SelectedTab = pg1;
                _frm.Parent = pg1;
                _frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - _frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - _frm.Height / 2);
                _frm.Show();// = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
           
            //form1.StartPosition = FormStartPosition.CenterParent ;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            var frm = sender as MetroForm;
            tabControlAdv1.TabPages.Remove(frm.Parent);
        }

        private void checkForBarStaticItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            AutoUpdater.InstalledVersion = new Version("1.2.0.0");
            AutoUpdater.ReportErrors = true;
            AutoUpdater.Start("https://keysoftoffice.github.io/autoupdate.xml");
        }

        private void SetToolBar()
        {
            accBarButtonItem.Tag = "Konto.Shared;" + "Konto.Shared.Masters.Acc.AccIndex";
            accBarButtonItem.Id = MenuId.Account;
            
            productBarButtonItem.Tag= "Konto.Shared;" + "Konto.Shared.Masters.Item.ProductIndex";
            productBarButtonItem.Id = MenuId.Product_Master;

            salesBarButtonItem.Tag = "Konto.Shared;" + "Konto.Shared.Trans.SInvoice.SInvoiceIndex";
            salesBarButtonItem.Id = MenuId.Sales_Invoice;
            
            purchaseBarButtonItem.Tag = "Konto.Shared;" + "Konto.Shared.Trans.PInvoice.PInvoiceIndex";
            purchaseBarButtonItem.Id = MenuId.Purchase_Invoice;

            ledgerBarButtonItem.Tag = "Konto.Reporting;" + "Konto.Reporting.Para.Ledger.LedgerMainView";
            ledgerBarButtonItem.Id = MenuId.Ledger;

            outsBarButtonItem.Tag = "Konto.Reporting;" + "Konto.Reporting.Para.Outs.OutsMainView";
            outsBarButtonItem.Id = MenuId.Outstanding;

            bsBarButtonItem.Tag = "Konto.Reporting;" + "Konto.Reporting.Para.BlSheet.BlMainView";
            bsBarButtonItem.Id = MenuId.Balancesheet;

            receiptBarButtonItem.Tag = "Konto.Shared;" + "Konto.Shared.Account.Receipt.ReceiptIndex";
            receiptBarButtonItem.Id = MenuId.Receipt_Vouecher;

            paymentBarButtonItem.Tag = "Konto.Shared;" + "Konto.Shared.Account.Payment.PaymentIndex";
            paymentBarButtonItem.Id = MenuId.Payment_Vouecher;

            if (RBAC.UserRight == null || RBAC.UserRight.IsSysAdmin) return;

            // Account Master
            if (!RBAC.UserRight.HasPermission(MenuId.Account, (int)Permission.View))
                accBarButtonItem.Visibility = BarItemVisibility.Never;
            else
                accBarButtonItem.Visibility = BarItemVisibility.Always;

            //product master
            if (!RBAC.UserRight.HasPermission(MenuId.Product_Master, (int)Permission.View))
                productBarButtonItem.Visibility = BarItemVisibility.Never;
            else
                productBarButtonItem.Visibility = BarItemVisibility.Always;

            // Sales Invoice
            if (!RBAC.UserRight.HasPermission(MenuId.Sales_Invoice, (int)Permission.View))
                salesBarButtonItem.Visibility = BarItemVisibility.Never;
            else
                salesBarButtonItem.Visibility = BarItemVisibility.Always;

            // Purchase Invoice
            if (!RBAC.UserRight.HasPermission(MenuId.Purchase_Invoice, (int)Permission.View))
                purchaseBarButtonItem.Visibility = BarItemVisibility.Never;
            else
                purchaseBarButtonItem.Visibility = BarItemVisibility.Always;

            //Ledgr
            if (!RBAC.UserRight.HasPermission(MenuId.Ledger, (int)Permission.View))
                ledgerBarButtonItem.Visibility = BarItemVisibility.Never;
            else
                ledgerBarButtonItem.Visibility = BarItemVisibility.Always;

            //Outstanding
            if (!RBAC.UserRight.HasPermission(MenuId.Outstanding, (int)Permission.View))
                outsBarButtonItem.Visibility = BarItemVisibility.Never;
            else
                outsBarButtonItem.Visibility = BarItemVisibility.Always;

            //Balancesheet
            if (!RBAC.UserRight.HasPermission(MenuId.Balancesheet, (int)Permission.View))
                bsBarButtonItem.Visibility = BarItemVisibility.Never;
            else
                bsBarButtonItem.Visibility = BarItemVisibility.Always;

            if (!RBAC.UserRight.HasPermission(MenuId.Receipt_Vouecher, (int)Permission.View))
                receiptBarButtonItem.Visibility = BarItemVisibility.Never;
            else
                receiptBarButtonItem.Visibility = BarItemVisibility.Always;

            if (!RBAC.UserRight.HasPermission(MenuId.Payment_Vouecher, (int)Permission.View))
                paymentBarButtonItem.Visibility = BarItemVisibility.Never;
            else
                receiptBarButtonItem.Visibility = BarItemVisibility.Always;

        }

        
    }
}
