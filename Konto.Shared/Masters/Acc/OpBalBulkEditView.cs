using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraVerticalGrid;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Admin;
using Konto.Data.Models.Op.Dto;
using Serilog;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Masters.Acc
{
    public partial class OpBalBulkEditView : KontoForm
    {
        public List<OpBalDto> AccountOpBalList { get; set; }
        public List<OpBalDto> UpdatedOpBal { get; set; }

        private string GridLayoutFileName = KontoFileLayout.Acc_Opbal_List_Layout;
        private bool GridSetting;
        public OpBalBulkEditView()
        {
            InitializeComponent();

            this.Create_Permission = true;
            this.View_Permission = true;
            this.Modify_Permission = true;
            this.Delete_Permission = true;
            this.Export_Permission = true;
            this.Print_Permission = true;
            this.Settings_Permission = true;

            this.customGridView1.FocusedColumnChanged += CustomGridView1_FocusedColumnChanged;
            
            listAction1.GridSettingsButtonClick += ListAction1_GridSettingsButtonClick;
            listAction1.ColumnSettingsButtonClick += ListAction1_ColumnSettingsButtonClick;
            listAction1.ResetSettingsButtonClick += ListAction1_ResetSettingsButtonClick;
            listAction1.CancelSettingsButtonClick += ListAction1_CancelSettingsButtonClick;
            listAction1.SaveSettingsButtonClick += ListAction1_SaveSettingsButtonClick;
            listAction1.PrintButtonClick += ListAction1_PrintButtonClick;
            listAction1.ExcelButtonClick += ListAction1_ExcelButtonClick;
            listAction1.WordButtonClick += ListAction1_WordButtonClick;
            listAction1.PdfButtonClick += ListAction1_PdfButtonClick;

            this.customGridView1.RowUpdated += CustomGridView1_RowUpdated;
         
            this.customGridView1.ValidatingEditor += CustomGridView1_ValidatingEditor;
            this.customGridView1.InvalidValueException += CustomGridView1_InvalidValueException;
            
            this.okSimpleButton.Click += okSimpleButton_Click;

            listAction1.NewButtonClick += ListAction1_NewButtonClick;
            listAction1.EditButtonClick += ListAction1_EditButtonClick;

            this.FormClosed += OpBalBulkEditView_FormClosed;
            this.Shown += OpBalBulkEditView_Shown;

        }

        private void OpBalBulkEditView_Shown(object sender, EventArgs e)
        {

            var deleteBarButtonItem = listAction1.Controls["dropDownButton1"] as DevExpress.XtraEditors.DropDownButton;
            if(deleteBarButtonItem!=null)
            deleteBarButtonItem.Enabled = false;
        }

        private void ListAction1_PrintButtonClick(object sender, EventArgs e)
        {
            customGridControl1.ShowPrintPreview();
        }

        private void CustomGridView1_InvalidValueException(object sender, InvalidValueExceptionEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            if (view == null) return;
            e.ExceptionMode = ExceptionMode.DisplayError;
            e.WindowCaption = "Input Error";
            e.ErrorText = "Debit & Credit Both Has Amount";
            view.HideEditor();
        }

        private void CustomGridView1_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            GridColumn column = (e as EditFormValidateEditorEventArgs)?.Column ?? view.FocusedColumn;
            if (column.Name != "colOpCredit" && column.Name != "colOpDebit") return;
            decimal opcredit, opdebit;
            if (column.Name == "colOpCredit")
            {
                opcredit = Convert.ToDecimal(e.Value);
                opdebit = Convert.ToDecimal(customGridView1.GetRowCellValue(view.FocusedRowHandle, "OpDebit"));
            }
            else
            {
                opdebit = Convert.ToDecimal(e.Value);
                opcredit = Convert.ToDecimal(customGridView1.GetRowCellValue(view.FocusedRowHandle, "OpCredit"));
            }
           
            if(opcredit != 0 && opdebit != 0)
            {
                e.Value = false;
            }
        }

        private void OpBalBulkEditView_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage != null)
            {
                var tabcontrol = tabpage.Parent as TabControlAdv;
                if (tabcontrol != null)
                    tabcontrol.TabPages.Remove(tabpage);
            }
        }

        private void ListAction1_EditButtonClick(object sender, EventArgs e)
        {
            var frm = new AccIndex();
            frm.EditKey=  Convert.ToInt32(this.customGridView1.GetRowCellValue(this.customGridView1.FocusedRowHandle, customGridView1.Columns["AccountId"]));
            frm.OpenForLookup = true;
            frm.ShowDialog(this);
            this.RefreshGrid();
        }

        private void ListAction1_NewButtonClick(object sender, EventArgs e)
        {
            var frm = new AccIndex();
            frm.OpenForLookup = true;
            frm.ShowDialog(this);
            this.RefreshGrid();
        }

      

        private void CustomGridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var rw = (OpBalDto)e.Row;
            if (rw.OpDebit > 0)
                rw.OpBal = rw.OpDebit;
            else
                rw.OpBal = -1 * rw.OpCredit;

            UpdatedOpBal.Add(rw);
        }

        private void ListAction1_PdfButtonClick(object sender, EventArgs e)
        {
            KontoUtils.ExportGridToPDF(this.customGridView1, splashScreenManager1);
        }

        private void ListAction1_WordButtonClick(object sender, EventArgs e)
        {
            KontoUtils.ExportGridToWord(this.customGridView1, splashScreenManager1);
        }

        private void ListAction1_ExcelButtonClick(object sender, EventArgs e)
        {
            KontoUtils.ExportGridToExcel(this.customGridView1, splashScreenManager1);
        }

        private void ListAction1_SaveSettingsButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.GridLayoutFileName)) return;
                KontoUtils.SaveLayoutGrid(this.GridLayoutFileName, this.customGridView1);
            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show("Error While Saveing", "Error", ex.ToString());
                Log.Information(ex, "save layout");
            }
        }

        private void ListAction1_CancelSettingsButtonClick(object sender, EventArgs e)
        {
            GridSetting = false;
            KontoUtils.HideGridProperty(this.panelControl2, this.listAction1);
        }

        private void ListAction1_ResetSettingsButtonClick(object sender, EventArgs e)
        {
            GridSetting = false;
            KontoUtils.ResetGridLayout("", this.customGridView1);
        }

        private void ListAction1_ColumnSettingsButtonClick(object sender, EventArgs e)
        {
            GridSetting = false;
            KontoUtils.LoadGridProperty(panelControl2, customGridView1.FocusedColumn, listAction1);
        }

        private void ListAction1_GridSettingsButtonClick(object sender, EventArgs e)
        {
            GridSetting = true;
            KontoUtils.LoadGridProperty(panelControl2, customGridView1, listAction1);
        }

        private void CustomGridView1_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            if (!this.panelControl2.Visible) return;
            if (this.GridSetting) return;
            ((PropertyGridControl)this.panelControl2.Controls[0]).SelectedObject = e.FocusedColumn;
        }

        private void OpBalBulkEditView_Load(object sender, EventArgs e)
        {

            this.UpdatedOpBal = new List<OpBalDto>();
            this.RefreshGrid();

            if (RBAC.UserRight == null || RBAC.UserRight.IsSysAdmin) return;

            this.Create_Permission = RBAC.UserRight.HasPermission(Convert.ToInt32(this.Tag), (int)Permission.Create);
            this.Modify_Permission = RBAC.UserRight.HasPermission(Convert.ToInt32(this.Tag), (int)Permission.Modify);
            this.Delete_Permission = RBAC.UserRight.HasPermission(Convert.ToInt32(this.Tag), (int)Permission.Delete);
            this.View_Permission = RBAC.UserRight.HasPermission(Convert.ToInt32(this.Tag), (int)Permission.View);
            this.Print_Permission = RBAC.UserRight.HasPermission(Convert.ToInt32(this.Tag), (int)Permission.Print);
            this.Export_Permission = RBAC.UserRight.HasPermission(Convert.ToInt32(this.Tag), (int)Permission.Export);
            this.Settings_Permission = RBAC.UserRight.HasPermission(Convert.ToInt32(this.Tag), (int)Permission.Settings);

            listAction1.SetPermission(this.Delete_Permission, this.Create_Permission, this.Export_Permission,
                   this.Print_Permission, this.Settings_Permission, this.Modify_Permission, this.View_Permission);

        }
        private void RefreshGrid()
        {
            try
            {
                using (var db = new KontoContext())
                {
                    this.AccountOpBalList = db.Database.SqlQuery<OpBalDto>("dbo.opbalsp  @companyid={0},@yearid={1}"
                        , KontoGlobals.CompanyId, KontoGlobals.YearId).ToList();
                }
                this.customGridControl1.DataSource = this.AccountOpBalList;

                if (string.IsNullOrEmpty(this.GridLayoutFileName)) return;

                KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, this.customGridView1);

                this.ActiveControl = customGridControl1;
                if (AccountOpBalList.Count == 0)
                    listAction1.EditDeleteDisabled(false);
                else
                    listAction1.EditDeleteDisabled(true);

                this.ActiveControl = this.customGridControl1;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Opening Balance refresh Grid");
                MessageBoxAdv.Show(this, "Error While List !!", "Exception ", ex.ToString());

            }
           
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.F3)
            {
                okSimpleButton.PerformClick();
                return true;
            }
            else if(keyData == Keys.Insert)
            {
                var newbBarButtonItem = listAction1.Controls["newSimpleButton"] as SimpleButton;
                newbBarButtonItem.PerformClick();
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
            else if (keyData == (Keys.Delete | Keys.Control))
            {
                var deleteBarButtonItem = listAction1.Controls["deleteSimpleButton"] as SimpleButton;
                deleteBarButtonItem.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void okSimpleButton_Click(object sender, EventArgs e)
        {
            using (KontoContext db = new KontoContext())
            {
                using(var _tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in UpdatedOpBal)
                        {
                            var mod = db.AccBals.Find(item.BalId);
                            
                            mod.Bal = mod.Bal - mod.OpBal + item.OpBal;
                            
                            mod.OpBal = item.OpBal;
                            mod.OpCredit = item.OpCredit;
                            mod.OpDebit = item.OpDebit;

                            
                           
                            var acmodel = db.Accs.Find(item.AccountId);
                            acmodel.GstIn = item.GstIn;
                            acmodel.PanNo = item.PanNo;
                            acmodel.AadharNo = item.AadharNo;


                        }
                        db.SaveChanges();
                        _tran.Commit();
                        MessageBoxAdv.Show(this, KontoGlobals.SaveMessage, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UpdatedOpBal = new List<OpBalDto>();
                        this.ActiveControl = this.customGridControl1;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Opening Balance Save");
                        MessageBoxAdv.Show(this, "Error While Save Opening Balance !!", "Exception ", ex.ToString());
                    }  
                }
            }
        }

        private void listAction1_RefreshButtonClick(object sender, EventArgs e)
        {
            this.RefreshGrid();
            
        }

        private void cancelSimpleButton_Click(object sender, EventArgs e)
        {
           
            this.Close();
            this.Dispose();
        }
    }
}
