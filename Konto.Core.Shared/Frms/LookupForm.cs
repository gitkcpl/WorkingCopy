using DevExpress.XtraEditors;
using DevExpress.XtraVerticalGrid;
using Konto.App.Shared;
using Konto.Core.Shared.Libs;
using Konto.Data.Models.Admin;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace Konto.Core.Shared.Frms
{
    public partial class LookupForm : KontoForm
    {
      
        public int SelectedValue { get; set; }
        public string SelectedTex { get; set; }
        public CustomGridView KontoView { get; set; }
        public int RefId { get; set; }
        public object SelectedItem { get; set; }
        public string AsemblyName { get; set; }
        public string FormClassName { get; set; }
        public string GridLayoutFileName { get; set; }
        public virtual void LoadData() { }
        public virtual void Ok() { }

        private bool GridSetting;
        public LookupForm()
        {
            InitializeComponent();
            this.Create_Permission = true;
            this.View_Permission = true;
            this.Modify_Permission = true;
            this.Delete_Permission = true;
            this.Export_Permission = true;
            this.Print_Permission = true;
            this.Settings_Permission = true;

            this.CancelButton = lkpAction1.Controls["cancelSimpleButton"] as SimpleButton;

            this.Shown += LookupForm_Shown;

        }

        private void LookupForm_Shown(object sender, EventArgs e)
        {
            if(this.KontoView!=null)
            this.KontoView.FocusedColumn = this.KontoView.VisibleColumns[0];
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            if (keyData == Keys.Insert)
            {
                var newbBarButtonItem = lkpAction1.Controls["newSimpleButton"] as SimpleButton;
                newbBarButtonItem.PerformClick();
                return true;
            }
            else if (keyData == (Keys.Return | Keys.Control))
            {
                var editBarButtonItem = lkpAction1.Controls["editSimpleButton"] as SimpleButton;
                editBarButtonItem.PerformClick();
                return true;
            }
            else if (keyData == Keys.Enter)
            {
                var okBarButtonItem = lkpAction1.Controls["okSimpleButton"] as SimpleButton;
                okBarButtonItem.PerformClick();
                return true;
            }
          
            return base.ProcessCmdKey(ref msg, keyData);

        }

        private void lkpAction1_EditButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (KontoView == null || KontoView.FocusedRowHandle < 0) return;
                int rowno = KontoView.FocusedRowHandle;
                var _frm = Activator.CreateInstance(this.AsemblyName, this.FormClassName).Unwrap() as KontoMetroForm;
               
                _frm.OpenForLookup = true;
                _frm.EditKey = Convert.ToInt32(KontoView.GetRowCellValue(
                    KontoView.FocusedRowHandle, "Id"));
                _frm.ShowDialog(this);
                this.LoadData();
                this.KontoView.MakeRowVisible(rowno);
                this.KontoView.FocusedRowHandle = rowno;
                this.KontoView.Focus();

            }
            catch (Exception ex)
            {

                Log.Information(ex, "edit lookup");
            }
        }

        private void lkpAction1_NewButtonClick(object sender, EventArgs e)
        {

            KontoMetroForm _frm;
            KontoForm _frm1=null;
            _frm = Activator.CreateInstance(this.AsemblyName, this.FormClassName).Unwrap() as KontoMetroForm;

            if (_frm == null)
            {
            
                _frm1 = Activator.CreateInstance(this.AsemblyName, this.FormClassName).Unwrap() as KontoForm;
            }
            if (_frm != null) // address View
            {
                if (this.RefId > 0 && _frm.GetType().GetProperty("GroupId") != null)
                {
                    PropertyInfo groupid = _frm.GetType().GetProperty("GroupId");
                    groupid.SetValue(_frm, this.RefId);
                }
                _frm.Tag = this.Tag;
                _frm.OpenForLookup = true;
                _frm.ShowDialog(this);
            }
            else if(_frm1!=null)
            {
                if (this.RefId > 0 && _frm1.GetType().GetProperty("AccId") != null)
                {
                    PropertyInfo groupid = _frm1.GetType().GetProperty("AccId");
                    groupid.SetValue(_frm1, this.RefId);
                }
                _frm1.Tag = this.Tag;
                //_frm1.OpenForLookup = true;
                _frm1.ShowDialog(this);

            }
            this.LoadData();
            this.KontoView.Focus();
        }

        private void lkpAction1_OkButtonClick(object sender, EventArgs e)
        {

            if (KontoView.FocusedRowHandle < 0)
                return;
            this.SelectedValue =
              Convert.ToInt32(KontoView.GetRowCellValue(KontoView.FocusedRowHandle, "Id"));
            this.SelectedTex = KontoView.GetRowCellValue(KontoView.FocusedRowHandle, KontoView.VisibleColumns[0]).ToString();
            this.SelectedItem = KontoView.GetRow(KontoView.FocusedRowHandle);
            this.DialogResult = DialogResult.OK;
            this.Close();
            this.Ok();
        }

        private void lkpAction1_CancelButtonClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
            this.Dispose();
        }

        private void LookupForm_Load(object sender, EventArgs e)
        {
            if (this.KontoView != null)
            {
               
                this.KontoView.FocusedColumnChanged += KontoView_FocusedColumnChanged;
            }
            this.LoadData();

            if (RBAC.UserRight == null || RBAC.UserRight.IsSysAdmin) return;

            this.Create_Permission = RBAC.UserRight.HasPermission(Convert.ToInt32(this.Tag), (int)Permission.Create);
            this.Modify_Permission = RBAC.UserRight.HasPermission(Convert.ToInt32(this.Tag), (int)Permission.Modify);
            // this.Delete_Permission = RBAC.UserRight.HasPermission(Convert.ToInt32(this.Tag), (int)Permission.Delete);
            // this.View_Permission = RBAC.UserRight.HasPermission(Convert.ToInt32(this.Tag), (int)Permission.View);
            //  this.Print_Permission = RBAC.UserRight.HasPermission(Convert.ToInt32(this.Tag), (int)Permission.Print);
            //  this.Export_Permission = RBAC.UserRight.HasPermission(Convert.ToInt32(this.Tag), (int)Permission.Export);
            this.Settings_Permission = RBAC.UserRight.HasPermission(Convert.ToInt32(this.Tag), (int)Permission.Settings);

           
        }
        
        private void KontoView_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            if (!this.panelControl2.Visible) return;
            if (this.GridSetting) return;
            ((PropertyGridControl)this.panelControl2.Controls[0]).SelectedObject = e.FocusedColumn;
        }

        private void lkpAction1_GridSettingsButtonClick(object sender, EventArgs e)
        {
            if (this.KontoView == null) return;

            GridSetting = true;
            KontoUtils.LoadGridProperty(panelControl2, KontoView);
        }

        private void lkpAction1_ColumnSettingsButtonClick(object sender, EventArgs e)
        {
            if (this.KontoView == null) return;

            GridSetting = false;
            KontoUtils.LoadGridProperty(panelControl2, KontoView.FocusedColumn);
        }

        private void lkpAction1_ResetSettingsButtonClick(object sender, EventArgs e)
        {
            if (this.KontoView == null) return;
            GridSetting = false;
            KontoUtils.ResetGridLayout("", this.KontoView);
        }

        private void lkpAction1_CancelSettingsButtonClick(object sender, EventArgs e)
        {
            if (this.KontoView == null) return;
            GridSetting = false;
            KontoUtils.HideGridProperty(this.panelControl2);
        }

        private void lkpAction1_SaveSettingsButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.GridLayoutFileName) || this.KontoView == null) return;
                KontoUtils.SaveLayoutGrid(this.GridLayoutFileName, this.KontoView);
            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show("Error While Saveing", "Error", ex.ToString());
                Log.Information(ex, "save layout");
            }
        }
    }
}
