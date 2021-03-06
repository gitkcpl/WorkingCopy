﻿using AutoUpdaterDotNET;
using DevExpress.XtraBars;
using DevExpress.XtraSplashScreen;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Admin;
using Konto.Data.Models.Reports;
using Konto.Shared.Masters.Branch;
using Konto.Shared.Masters.Comp;
using Konto.Shared.Masters.FinYear;
using Konto.Shared.Masters.LogIn;
using KontoWin.Db;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Data.WcfLinq.Helpers;

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
            this.tabControlAdv1.ControlRemoved += TabControlAdv1_ControlRemoved;
            this.tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;
           
        }

        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedTab.Controls.Count == 0) return;

            var frm = tabControlAdv1.SelectedTab.Controls[0] as KontoMetroForm;

            if (frm != null && frm.FirstActiveControl != null)
            {
                frm.FirstActiveControl.Focus();
            }
        }

        private void TabControlAdv1_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (tabControlAdv1.SelectedTab.Controls.Count == 0) return;
            var frm = tabControlAdv1.SelectedTab.Controls[0] as KontoMetroForm;

            if (frm != null && frm.FirstActiveControl != null)
            {
                frm.FirstActiveControl.Focus();
            }
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

            DialogResult res = DialogResult.Cancel;

            if (!KontoGlobals.IsDevelopment) {
                var frm = new LogInWindowView();
                frm.IsStartup = true;
                 res = frm.ShowDialog();
            }
            else
            {
                if (AutoLogin())
                    res = DialogResult.OK;
            }
            
            if (res == DialogResult.OK)
            {
                var frmc = new SelectCompanyView();
                if (frmc.ShowDialog() == DialogResult.OK)
                {
                    compBarStaticItem.Caption = frmc.Company.CompName;
                    KontoGlobals.CompanyName = frmc.Company.CompName;
                    KontoGlobals.GstIn = frmc.Company.GstIn;
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
                var searchstring = "," + KontoGlobals.PackageId.ToString() + ",";
                if (role.IsSysAdmin)
                {

                    //erp = (from em in db.ErpModules
                    //       join pkg in db.Menu_Packages on em.Id equals pkg.MenuId into pkg_join
                    //       from pkg in pkg_join.DefaultIfEmpty()
                    //       where (em.IsActive && !em.IsDeleted && em.Visible == true && (em.PackageId == 0 || em.PackageId == KontoGlobals.Edition)
                    //                && pkg.PackageId == KontoGlobals.PackageId && em.ModuleDesc != "-")
                    //       select em).ToList();
                   
                    erp = (from em in db.ErpModules
                           where (em.IsActive && !em.IsDeleted && em.Visible == true && (em.PackageId == 0 || em.PackageId == KontoGlobals.Edition)
                                   &&  em.Extra2.Contains(searchstring) && em.ModuleDesc != "-"
                                    
                                    )
                           select em).ToList();
                }
                else
                {
                    var erpuser = (from em in db.ErpModules
                                   //join pkg in db.Menu_Packages on em.Id equals pkg.MenuId into pkg_join
                                   //from pkg in pkg_join.DefaultIfEmpty()

                                   join p in db.Permissions on em.Id equals p.ModuleId into pm_join
                                   from pm in pm_join.DefaultIfEmpty()

                                   join rp in db.RolePermissions on pm.Id equals rp.PermissionId into rp_join
                                   from rp in rp_join.DefaultIfEmpty()
                                   where (rp.RoleId == KontoGlobals.UserRoleId && em.IsActive && !em.IsDeleted
                                    && (em.PackageId == 0 || em.PackageId == KontoGlobals.Edition)
                                    && em.Extra2.Contains(searchstring) && em.ModuleDesc != "-")

                                  // && pkg.PackageId == KontoGlobals.PackageId && em.ModuleDesc != "-")
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
                                       erpGrp.Key.OrderIndex,
                                       erpGrp.Key.ImageIndex
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
                        if (!erp.Any(x => x.Id == nr.Id))
                            erp.Add(nr);
                        //if (!nr.IsSeprator)
                        //  TMainMenus.Add(nr);
                    }
                }


            }


            erp = erp.OrderBy(x => x.ParentId).ThenBy(x => x.OrderIndex).ToList();
            foreach (var item in erp)
            {
                if (item.ParentId == 0)
                    CreateMenuItem(item, true);
                else
                {

                    var _top = erp.FirstOrDefault(x => x.ParentId == item.Id);
                    if (_top != null)
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


            //read system level parameter
            DbUtils.SetSysParameter();
        }

        private bool AutoLogin()
        {
            using (var db = new KontoContext())
            {
                string enc = KontoUtils.Encrypt("key@1234", "sblw-3hn8-sqoy19");
                var Ulist = db.UserMasters.Include("Role").FirstOrDefault(k => k.UserName.ToUpper() == "KEYSOFT"
                         && k.UserPass == enc);

                if (Ulist != null)
                {
                    KontoGlobals.UserId = Ulist.Id;
                    KontoGlobals.UserName = Ulist.UserName;
                    var usr = db.UserMasters.FirstOrDefault(k => k.Id == KontoGlobals.UserId);
                    KontoGlobals.UserRoleId = usr.RoleId;

                    if (usr.Role != null)
                        KontoGlobals.isSysAdm = usr.Role.IsSysAdmin;

                    KontoGlobals.EmpId = usr.EmpId != null ? (int)usr.EmpId : 0;
                    RBAC.UserRight = new RBACUser(Ulist.UserName);
                }
            }
            return true;
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
                    {
                        var lnk = b2.AddItem(b1);
                        if (_erp.IsSeprator)
                            lnk.BeginGroup = true;
                    }
                        //b2.ItemLinks.Add(b1);

                    
                        
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


                if(e.Item.Id==1066) // balance carry forward
                {

                    BalanceCarryToNextYear();
                    return;
                }
                if(e.Item.Id==909 && KontoGlobals.UserName.ToUpper() == "KEYSOFT") // global setup
                {
                    GlobalSetup();
                    return;
                }

                if(e.Item.Id==906 && KontoGlobals.UserName.ToUpper()=="KEYSOFT") // Report Designer
                {
                    var frm = new RepDesignerIndex();
                    frm.ShowDialog();
                    return;
                }

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
               
                _frm.Parent = pg1;
                tabControlAdv1.SelectedTab = pg1;
                _frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - _frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - _frm.Height / 2);
                _frm.Show();// = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
           
            //form1.StartPosition = FormStartPosition.CenterParent ;
        }

        private void GlobalSetup()
        {
            var frm = new Konto.Shared.Setup.SettingWindow();
            frm.SettingCategroy = "sys";
            frm.ShowDialog();
            DbUtils.SetSysParameter();
        }
        private void BalanceCarryToNextYear()
        {
            using (KontoContext db = new KontoContext())
            {
                db.Database.CommandTimeout = 0;

                //var accbal = db.AccBals.Where(x => x.YearId == KontoGlobals.YearId && x.CompId == KontoGlobals.CompanyId);
                var lastyear = db.FinYears.FirstOrDefault(x => x.PrevYearId == KontoGlobals.YearId);
                if(lastyear == null)
                {
                    MessageBox.Show("Next Year Not Found");
                    return;
                }

                var Lst = db.Database.SqlQuery<BalDto>(
                            "dbo.Bal_sheet @CompanyId={0},@FromDate={1},@ToDate={2},@YearId={3},@Summary={4},@zero={5}",
                                    Convert.ToInt32(KontoGlobals.CompanyId), KontoGlobals.FromDate, KontoGlobals.ToDate,
                                    KontoGlobals.YearId, "N", 1).ToList().Where(X => X.TransType == 3);



                if (Lst != null)
                {
                    try
                    {
                        var accbals = db.AccBals.Where(x => x.YearId == lastyear.Id && x.CompId == KontoGlobals.CompanyId).ToList();

                        foreach (var item in accbals)
                        {
                            //var _accbalnext = db.AccBals.Where(x => x.AccId == item.AcId && x.YearId == lastyear.Id && x.CompId == KontoGlobals.CompanyId).FirstOrDefault();
                            var _accbalnext = Lst.FirstOrDefault(x => x.AcId == item.AccId);

                            if (_accbalnext != null)
                            {
                                if (_accbalnext.Bal > 0)
                                    item.OpDebit = _accbalnext.Bal;
                                else
                                    item.OpCredit = -1 * _accbalnext.Bal;

                                item.OpBal = _accbalnext.Bal;
                            }
                            else
                            {
                                item.OpBal = 0;
                                item.OpDebit = 0;
                                item.OpCredit = 0;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        return;
                    }
                }

                //update next year balance

                DbUtils.Update_Account_Balance(db,lastyear.FromDate ?? 0,lastyear.ToDate ?? 0,lastyear.Id);

                //transfer stock balance;
                var brs = db.Branches.Where(x=>x.CompId == KontoGlobals.CompanyId
                && !x.IsDeleted).ToList();

                foreach (var br in brs)
                {
                    var stlist = db.StockTranses
                        .Where(x => x.CompanyId == KontoGlobals.CompanyId
                                    && x.YearId == KontoGlobals.YearId && !x.IsDeleted
                                    && x.BranchId==br.Id)
                        .GroupBy(x => x.ItemId)
                        .Select(g => new
                        {
                            Id = g.Key,
                            StockPcs = g.Sum(x => x.Pcs),
                            StockQty = g.Sum(x => x.Qty)
                        }).ToList();

                    var pbs = db.StockBals.Where(x => x.CompanyId == KontoGlobals.CompanyId
                                                      && x.YearId == lastyear.Id && x.BranchId == br.Id).ToList();

                    foreach (var pb in pbs)
                    {
                        //if (pb.ProductId == 65057)
                        //    MessageBox.Show("ex");
                        var st = stlist.FirstOrDefault(x => x.Id == pb.ProductId);

                        var pdo = db.StockBals.FirstOrDefault(x => x.CompanyId == KontoGlobals.CompanyId
                                                                   && x.YearId == KontoGlobals.YearId &&
                                                                   x.BranchId == br.Id
                                                                   && x.ProductId == pb.ProductId);

                        if (st != null)
                        {
                            pb.OpQty = pdo == null ? 0 : pdo.OpQty + st.StockQty;
                            pb.OpNos = pdo == null ? 0 : pdo.OpNos + st.StockPcs;
                        }
                        else
                        {
                            if (pdo != null)
                            {
                                pb.OpNos = pdo.OpNos;
                                pb.OpQty = pdo.OpQty;
                            }
                            else
                            {
                                pb.OpNos = 0;
                                pb.OpQty = 0;
                            }
                        }
                    }
                }
               

                   


                db.SaveChanges();
                MessageBox.Show("Balance Transfer Successfully");
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            var frm = sender as MetroForm;
            tabControlAdv1.TabPages.Remove(frm.Parent);
        }

        private void checkForBarStaticItem_ItemClick(object sender, ItemClickEventArgs e)
        {
           
            AutoUpdater.ReportErrors = true;
            AutoUpdater.Start("https://keysoftoffice.github.io/autoupdate.xml");
        }

        private void SetToolBar()
        {
            accBarButtonItem.Tag = "Konto.Shared;" + "Konto.Shared.Masters.Acc.AccIndex";
            accBarButtonItem.Id = MenuId.Account;
            
            productBarButtonItem.Tag= "Konto.Shared;" + "Konto.Shared.Masters.Item.ProductIndex";
            productBarButtonItem.Id = MenuId.Product_Master;

            if (KontoGlobals.PackageId == (int) PackageType.POS)
            {
                salesBarButtonItem.Tag = "Konto.Pos;" + "Konto.Pos.Sales.SalesIndex";
                salesBarButtonItem.Id = MenuId.Sales_Invoice;
            }
            else
            {
                salesBarButtonItem.Tag = "Konto.Shared;" + "Konto.Shared.Trans.SInvoice.SInvoiceIndex";
                salesBarButtonItem.Id = MenuId.Sales_Invoice;
            }

            if (KontoGlobals.PackageId == (int)PackageType.POS)
            {
                purchaseBarButtonItem.Tag = "Konto.Pos;" + "Konto.Pos.Purchase.PurchaseIndex";
                purchaseBarButtonItem.Id = MenuId.Purchase_Invoice;
            }
            else
            {
                purchaseBarButtonItem.Tag = "Konto.Shared;" + "Konto.Shared.Trans.PInvoice.PInvoiceIndex";
                purchaseBarButtonItem.Id = MenuId.Purchase_Invoice;
            }

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
