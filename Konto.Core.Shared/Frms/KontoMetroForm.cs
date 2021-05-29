using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using Konto.App.Shared;
using Konto.Core.Shared.Libs;
using Konto.Data.Models.Admin;

using Syncfusion.Windows.Forms.Tools;
using System;
using System.Windows.Forms;

namespace Konto.Core.Shared.Frms
{
    public partial class KontoMetroForm : KontoForm
    {
       
        public  bool IsLoadingData { get; set; }
        public bool ViewOnlyMode { get; set; }
        public int PrimaryKey { get; set; }
        public bool OpenForLookup { get; set; }

        public long _SerialValue  { get; set;}
        public int TotalRecord { get; set; }
        
        public int RecordNo { get; set; }

        public LayoutControl KontoLayout { get; set; }

        public GridView KontoMainView { get; set; }

        public string MainLayoutFile { get; set; }

        public string GridLayoutFile { get; set; }
       
        public void UpdateMessage( string msg)
        {
            msgStatusBarAdvPanel.Text = msg;
        }
        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            if (!this.Create_Permission)
                okSimpleButton.Enabled = false;
        }
        public void ShowAlert(string msg,string cap)
        {
            alertControl1.Show(this, cap, msg);
        }
        public KontoMetroForm()
        {
            InitializeComponent();
            helpStatusBarAdvPanel.Text = KontoHelp.MainBarHelpMsg;
            navAction1.PrintButtonClick += NavAction1_PrintButtonClick;
            okSimpleButton.Click += OkSimpleButton_Click;
            this.Shown += KontoMetroForm_Shown;

            this.Create_Permission = true;
            this.View_Permission = true;
            this.Modify_Permission = true;
            this.Delete_Permission = true;
            this.Export_Permission = true;
            this.Print_Permission = true;
            this.Settings_Permission = true;

           

        }

        private void KontoMetroForm_Shown(object sender, EventArgs e)
        {
            if (ViewOnlyMode)
            {
                okSimpleButton.Enabled = false;
                navAction1.SetPermission(false, false, false);
            }

            if (this.EditKey > 0)
            {
                this.EditPage(EditKey);
            }
        }

        private void NavAction1_PrintButtonClick(object sender, EventArgs e)
        {
            this.Print();
        }

        public virtual void Print() { }
        public virtual void ResetPage() {
           
           
        }
        public virtual void SaveDataAsync(bool newmode) {
         
            this.RecordNo = 0;
            this.TotalRecord = 0;
            this.PrimaryKey = 0;
            navAction1.RecPos = this.RecordNo;
            navAction1.TotalRecord = this.TotalRecord;
            navAction1.NavigationEnabled(false);
        }
        
        public virtual void EditPage(int _key) {
            
            if(this.Modify_Permission)
                okSimpleButton.Enabled = true;

            navAction1.NavigationEnabled(false);

                if (ViewOnlyMode)
                {
                    okSimpleButton.Enabled = false;
                    navAction1.SetPermission(false, false, false);
                }

                IsLoadingData = true;
        }
        public virtual void FirstRec() {
            IsLoadingData = true;
            this.RecordNo = 0;
        }
        public virtual void NextRec() {
            IsLoadingData = true;
            this.RecordNo = this.RecordNo + 1;

            if (this.RecordNo >= this.TotalRecord)
                this.RecordNo = this.TotalRecord - 1;
        }
        public virtual void PrevRec() {
            IsLoadingData = true;
            this.RecordNo = this.RecordNo - 1;
            if (this.RecordNo == -1)
                this.RecordNo = 0;
        }
        public virtual void LastRec() {
            IsLoadingData = true;
            this.RecordNo = this.TotalRecord - 1;
        }
        public virtual void FindRec() {
            IsLoadingData = true;
            if (this.TotalRecord > 0 && this.Modify_Permission)
                okSimpleButton.Enabled = true;
        }

     

        public virtual void NewRec() {
            this.RecordNo = 0;
            this.TotalRecord = 0;
            navAction1.NavigationEnabled(false);
            this.PrimaryKey = 0;
            IsLoadingData = false;
        }

      
        private void cancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            if (keyData == (Keys.P | Keys.Control))
            {
                var printBarButtonItem = navAction1.Controls["printSimpleButton"] as SimpleButton;
                printBarButtonItem.PerformClick();
                return true;
            }
            else if (keyData == Keys.F10)
            {
                okSimpleButton.PerformClick();
            }

            else if (keyData == Keys.F5)
            {
                var filterBarButtonItem = navAction1.Controls["filterSimpleButton"] as SimpleButton;
                filterBarButtonItem.PerformClick();
                return true;
            }
            else if (keyData == Keys.F12)
            {
                var listBarButtonItem = navAction1.Controls["listSimpleButton"] as SimpleButton;
                listBarButtonItem.PerformClick();
                return true;
            }
            else if (keyData == Keys.Insert)
            {
                var newbBarButtonItem = navAction1.Controls["newSimpleButton"] as SimpleButton;
                newbBarButtonItem.PerformClick();
                return true;
            }
            else if (keyData == (Keys.Home | Keys.Control))
            {
                var firstBarButtonItem = navAction1.Controls["firstSimpleButton"] as SimpleButton;
                firstBarButtonItem.PerformClick();
                return true;
            }
            else if (keyData == (Keys.End | Keys.Control))
            {
                var lastBarButtonItem = navAction1.Controls["lastSimpleButton"] as SimpleButton;
                lastBarButtonItem.PerformClick();
                return true;
            }
            else if (keyData == (Keys.Add | Keys.Control))
            {
                var nextBarButtonItem = navAction1.Controls["nextSimpleButton"] as SimpleButton;
                nextBarButtonItem.PerformClick();
                return true;
            }
            else if (keyData == (Keys.Subtract | Keys.Control))
            {
                var prevBarButtonItem = navAction1.Controls["prevSimpleButton"] as SimpleButton;
                prevBarButtonItem.PerformClick();
                return true;
            }
            else if (keyData == (Keys.F1 | Keys.Shift))
            {
                if(this.KontoLayout!=null && !string.IsNullOrEmpty(this.MainLayoutFile))
                    KontoUtils.SaveMainFormLayout(this.MainLayoutFile, KontoLayout);

                if(this.KontoMainView!=null && !string.IsNullOrEmpty(this.GridLayoutFile))
                    KontoUtils.SaveLayoutGrid(this.GridLayoutFile, KontoMainView);
                
                return true;
            }
            else if(keyData ==  (Keys.F2 | Keys.Shift))
            {
                if (KontoMainView == null) return false;
                if (!KontoMainView.IsFocusedView) return false;
                var frm = new GridPropertView();
                frm.gridControl1.DataSource = this.KontoMainView.GridControl.DataSource;
                frm.gridView1.Assign(this.KontoMainView, false);
                if (frm.ShowDialog() != DialogResult.OK) return true;
                this.KontoMainView.Assign(frm.gridView1, false);
                KontoUtils.SaveLayoutGrid(this.GridLayoutFile, KontoMainView);
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

       
       

        private void navAction1_NewButtonClick(object sender, EventArgs e)
        {

            if (KontoGlobals.PackageId != (int)PackageType.POS)
            {
                this.ResetPage();

                
            }
            this.NewRec();

            this.RecordNo = 0;
            this.TotalRecord = 0;
            this.PrimaryKey = 0;
            navAction1.RecPos = this.RecordNo;
            navAction1.TotalRecord = this.TotalRecord;
            navAction1.NavigationEnabled(false);

            if (this.Create_Permission)
            {
                okSimpleButton.Enabled = true;
            }
        }

        private void navAction1_LastButtonClick(object sender, EventArgs e)
        {
            this.LastRec();
            navAction1.RecPos = this.RecordNo;
            navAction1.TotalRecord = this.TotalRecord;
        }

        private void navAction1_NextButtonClick(object sender, EventArgs e)
        {
            this.NextRec();
            navAction1.RecPos = this.RecordNo;
            navAction1.TotalRecord = this.TotalRecord;
        }

        private void navAction1_FirstButtonClick(object sender, EventArgs e)
        {
            this.FirstRec();
            navAction1.RecPos = this.RecordNo;
            navAction1.TotalRecord = this.TotalRecord;
        }

        private void navAction1_PrevButtonClick(object sender, EventArgs e)
        {
            this.PrevRec();
            navAction1.RecPos = this.RecordNo;
            navAction1.TotalRecord = this.TotalRecord;
        }

        private void navAction1_FilterButtonClick(object sender, EventArgs e)
        {
            this.FindRec();
            navAction1.TotalRecord = this.TotalRecord;
          
            if (this.TotalRecord > 0)
            {
                navAction1.RecPos = this.RecordNo;
                navAction1.NavigationEnabled(true);
            }
            else
            {
                navAction1.NavigationEnabled(false);
            }
        }

        private void navAction1_ListButtonClick(object sender, EventArgs e)
        {
            tabControlAdv1.SelectedIndex = 1;
        }

        private void tabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            helpStatusBarAdvPanel.Text = tabControlAdv1.SelectedIndex == 0 ? KontoHelp.MainBarHelpMsg : KontoHelp.ListBarHelpMsg;
        }

        private void KontoMetroForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage == null) return;
            var tabcontrol = tabpage.Parent as TabControlAdv;
            if (tabcontrol != null)
                tabcontrol.TabPages.Remove(tabpage);

        }

        private void KontoMetroForm_Load(object sender, EventArgs e)
        {
            if (KontoLayout != null)
            {
                KontoUtils.RestoreMainFormLayout(this.MainLayoutFile, KontoLayout);
            }
            if (KontoMainView != null)
            {
                KontoUtils.RestoreLayoutGrid(this.GridLayoutFile, KontoMainView);
            }
            this.ResetPage();
            this.NewRec();

            navAction1.ModuleId = Convert.ToInt32(this.Tag);
            if (RBAC.UserRight == null || RBAC.UserRight.IsSysAdmin) return;

            this.Create_Permission = RBAC.UserRight.HasPermission(Convert.ToInt32(this.Tag), (int)Permission.Create);
            this.Modify_Permission = RBAC.UserRight.HasPermission(Convert.ToInt32(this.Tag), (int)Permission.Modify);
            this.Delete_Permission = RBAC.UserRight.HasPermission(Convert.ToInt32(this.Tag), (int)Permission.Delete);
            this.View_Permission = RBAC.UserRight.HasPermission(Convert.ToInt32(this.Tag), (int)Permission.View);
            this.Print_Permission = RBAC.UserRight.HasPermission(Convert.ToInt32(this.Tag), (int)Permission.Print);
            this.Export_Permission = RBAC.UserRight.HasPermission(Convert.ToInt32(this.Tag), (int)Permission.Export);
            this.Settings_Permission = RBAC.UserRight.HasPermission(Convert.ToInt32(this.Tag), (int)Permission.Settings);

            if (this.Create_Permission)
                okSimpleButton.Enabled = true;
            else
                okSimpleButton.Enabled = false;

            navAction1.SetPermission(this.Create_Permission, this.View_Permission, this.Print_Permission);

            
        }

        private void navAction1_SettingButtonClick(object sender, EventArgs e)
        {
            if(!KontoGlobals.isSysAdm) return;
            var _frm = Activator.CreateInstance("Konto.Shared", "Konto.Shared.Setup.SettingWindow").Unwrap() as KontoForm;
            _frm.GetType().GetProperty("SettingCategroy").SetValue(_frm,this.SettingCategroy, null);
            _frm.ShowDialog();
           
        }
        public virtual void OpenSettings()
        {

        }
    }
}
