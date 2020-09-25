using System;
using System.Windows.Forms;
using Konto.Core.Shared.Libs;
using DevExpress.XtraEditors;
using Serilog;
using Syncfusion.Windows.Forms;
using DevExpress.XtraVerticalGrid;
using Syncfusion.Windows.Forms.Tools;
using Konto.App.Shared;

namespace Konto.Core.Shared.Frms
{
    public partial class ListBaseView : DevExpress.XtraEditors.XtraUserControl
    {
        private bool GridSetting;
        public CustomGridView KontoView { get; set; }
      
        public string GridLayoutFileName {get;set;}
        public CustomGridControl KontoGrid { get; set; }

        public int ModuleId { get; set; }
        public virtual void RefreshGrid() { }
        public virtual void DeleteRec() { }
        public virtual void ImportExcel() { }

        public virtual void CancleRecord() { }
        public bool ReportPrint { get; set; }
        public virtual void Print() { }

        public bool ViewOnlyMode { get; set; }
        public ListBaseView()
        {
            InitializeComponent();
            listAction1.ModuleId = this.ModuleId;
        }

      
        private void ListBaseView_Load(object sender, EventArgs e)
        {
            
           if(this.KontoView !=null)
            {
                this.KontoView.FocusedColumnChanged += KontoView_FocusedColumnChanged;
                this.KontoView.DoubleClick += KontoView_DoubleClick;
            }

            var frm = this.Parent.Parent.Parent as KontoMetroForm;
            if(frm!=null)
            {
                listAction1.SetPermission(frm.Delete_Permission, frm.Create_Permission, frm.Export_Permission,
                    frm.Print_Permission, frm.Settings_Permission, frm.Modify_Permission, frm.View_Permission);
                if (frm.View_Permission || frm.Modify_Permission)
                    this.RefreshGrid();
            }

            if (ViewOnlyMode)
            {
                listAction1.SetPermission(false, false, frm.Export_Permission, frm.Print_Permission,
                    frm.Settings_Permission, frm.Modify_Permission, frm.View_Permission);
            }
        }

        private void KontoView_DoubleClick(object sender, EventArgs e)
        {
            var editBarButtonItem = listAction1.Controls["editSimpleButton"] as SimpleButton;
            editBarButtonItem.PerformClick();
        }

        private void KontoView_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            if (!this.panelControl3.Visible) return;
            if (this.GridSetting) return;
            ((PropertyGridControl)this.panelControl3.Controls[0]).SelectedObject = e.FocusedColumn;
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Insert)
            {
                var newBarButtonItem = listAction1.Controls["newSimpleButton"] as SimpleButton;
                newBarButtonItem.PerformClick();
                return true;
            }
            else if (keyData == (Keys.Return | Keys.Control))
            {
                var editBarButtonItem = listAction1.Controls["editSimpleButton"] as SimpleButton;
                editBarButtonItem.PerformClick();
                return true;
            }
            else if (keyData == (Keys.P | Keys.Control))
            {
                var printBarButtonItem = listAction1.Controls["printSimpleButton"] as SimpleButton;
                printBarButtonItem.PerformClick();
                return true;
            }
            else if (keyData == (Keys.R | Keys.Control))
            {
                var refreshBarButtonItem = listAction1.Controls["refreshSimpleButton"] as SimpleButton;
                refreshBarButtonItem.PerformClick();
                return true;
            }
            else if (keyData == (Keys.Delete | Keys.Control))
            {
                var deleteBarButtonItem = listAction1.Controls["deleteSimpleButton"] as SimpleButton;
                deleteBarButtonItem.PerformClick();
                return true;
            }
            //else if (keyData == Keys.Back)
            //{
            //    var _parent = this.Parent.Parent as TabControlAdv;
            //    _parent.SelectedIndex = 0;
            //}


            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void listAction1_NewButtonClick(object sender, EventArgs e)
        {
            var _parent = this.Parent.Parent.Parent as KontoMetroForm;
            if (_parent != null)
            {
                _parent.tabControlAdv1.SelectedIndex = 0;
                _parent.ResetPage();
                _parent.NewRec();
            }
        }

        private void listAction1_EditButtonClick(object sender, EventArgs e)
        {
            if (this.KontoView == null || this.KontoView.FocusedRowHandle < 0) return;
            var _parent = this.Parent.Parent.Parent as KontoMetroForm;
            if (_parent != null)
            {
               

                if (KontoView.Columns.ColumnByFieldName("Id") != null)
                {
                    if (KontoView.Columns.ColumnByFieldName("IsDeleted") != null)
                    {
                        if(Convert.ToBoolean(this.KontoView.GetRowCellValue(this.KontoView.FocusedRowHandle, "IsDeleted")))
                        {
                            return;
                        }
                    }
                    var id = Convert.ToInt32(this.KontoView.GetRowCellValue(this.KontoView.FocusedRowHandle, "Id"));
                    _parent.tabControlAdv1.SelectedIndex = 0;
                    _parent.EditPage(id);
                }
            }
        }

        private void listAction1_DeleteButtonClick(object sender, EventArgs e)
        {
            this.DeleteRec();

            if(this.KontoGrid!=null)
                this.ActiveControl = this.KontoGrid;
        }

        private void listAction1_RefreshButtonClick(object sender, EventArgs e)
        {
            this.RefreshGrid();
        }

        private void listAction1_GridSettingsButtonClick(object sender, EventArgs e)
        {
            if (this.KontoView == null ) return;

            GridSetting = true;
            KontoUtils.LoadGridProperty(panelControl3, KontoView, listAction1);
        }

        private void listAction1_ColumnSettingsButtonClick(object sender, EventArgs e)
        {
            if (this.KontoView == null ) return;

            GridSetting = false;
            KontoUtils.LoadGridProperty(panelControl3, KontoView.FocusedColumn, listAction1);
        }

        private void listAction1_ResetSettingsButtonClick(object sender, EventArgs e)
        {
            if (this.KontoView == null) return;
            GridSetting = false;
            KontoUtils.ResetGridLayout("", this.KontoView);
        }

        private void listAction1_CancelSettingsButtonClick(object sender, EventArgs e)
        {
            if (this.KontoView == null) return;
            GridSetting = false;
            KontoUtils.HideGridProperty(this.panelControl3, this.listAction1);
        }

        private void listAction1_SaveSettingsButtonClick(object sender, EventArgs e)
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

        private void listAction1_ExcelButtonClick(object sender, EventArgs e)
        {
            if (this.KontoView == null) return;
            KontoUtils.ExportGridToExcel(this.KontoView, splashScreenManager1);
        }

        private void listAction1_WordButtonClick(object sender, EventArgs e)
        {
            if (this.KontoView == null) return;
            KontoUtils.ExportGridToWord(this.KontoView, splashScreenManager1);
        }

        private void listAction1_PdfButtonClick(object sender, EventArgs e)
        {
            if (this.KontoView == null) return;
            KontoUtils.ExportGridToPDF(this.KontoView, splashScreenManager1);
        }

        private void listAction1_PrintButtonClick(object sender, EventArgs e)
        {
            if (ReportPrint) {
                this.Print();
                return;
            }

            if (this.KontoGrid == null) return;
            if (!this.KontoGrid.IsPrintingAvailable)
            {
                MessageBox.Show("The 'Printing' library is not found", "Error");
                return;
            }

            // Open the Preview window. 
            KontoGrid.ShowPrintPreview();
        }

        private void listAction1_ImportButtonClick(object sender, EventArgs e)
        {
            this.ImportExcel();
        }

        private void listAction1_CancleRecordClick(object sender, EventArgs e)
        {
            this.CancleRecord();
        }
    }
}
