using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
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

namespace Konto.Shared.Masters.Item
{
    public partial class ProdOpBalBulkEditView : KontoForm
    {
        public List<OpStockDto> ItemOpBalList { get; set; }
        public List<OpStockDto> UpdatedOpBal { get; set; }

        private string GridLayoutFileName = KontoFileLayout.Item_Opbal_List_Layout;
        private bool GridSetting;
        public ProdOpBalBulkEditView()
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

            listAction1.ExcelButtonClick += ListAction1_ExcelButtonClick;
            listAction1.WordButtonClick += ListAction1_WordButtonClick;
            listAction1.PdfButtonClick += ListAction1_PdfButtonClick;

            this.customGridView1.RowUpdated += CustomGridView1_RowUpdated;
            this.customGridView1.InvalidRowException += CustomGridView1_InvalidRowException;
          
            //this.customGridView1.CellValueChanged += CustomGridView1_CellValueChanged;

            this.okSimpleButton.Click += okSimpleButton_Click;

            listAction1.NewButtonClick += ListAction1_NewButtonClick;
            listAction1.EditButtonClick += ListAction1_EditButtonClick;

            this.FormClosed += OpBalBulkEditView_FormClosed;
            this.Shown += ProdOpBalBulkEditView_Shown;
            listAction1.PrintButtonClick += ListAction1_PrintButtonClick;
        }

        private void ListAction1_PrintButtonClick(object sender, EventArgs e)
        {
            customGridControl1.ShowPrintPreview();
        }

        private void ProdOpBalBulkEditView_Shown(object sender, EventArgs e)
        {
            var deleteBarButtonItem = listAction1.Controls["dropDownButton1"] as DevExpress.XtraEditors.DropDownButton;
            if (deleteBarButtonItem != null)
                deleteBarButtonItem.Enabled = false;
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
            var frm = new ProductIndex();
            frm.EditKey= Convert.ToInt32(this.customGridView1.GetRowCellValue(this.customGridView1.FocusedRowHandle, customGridView1.Columns["Id"]));
            frm.OpenForLookup = true;
            frm.ShowDialog(this);
            this.RefreshGrid();
        }

        private void ListAction1_NewButtonClick(object sender, EventArgs e)
        {
            var frm = new ProductIndex();
            frm.OpenForLookup = true;
            frm.ShowDialog(this);
            this.RefreshGrid();
        }

       

        private void CustomGridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
           
        }

        private void CustomGridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var rw = (OpStockDto)e.Row;
            
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

            this.UpdatedOpBal = new List<OpStockDto>();
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
                    this.ItemOpBalList = db.Database.SqlQuery<OpStockDto>("dbo.ProductOpBalSp  @companyid={0},@BranchId={1}"
                        , KontoGlobals.CompanyId, KontoGlobals.BranchId).ToList();
                }
                this.customGridControl1.DataSource = this.ItemOpBalList;

                if (string.IsNullOrEmpty(this.GridLayoutFileName)) return;

                KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, this.customGridView1);

                this.ActiveControl = customGridControl1;
                if (ItemOpBalList.Count == 0)
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
                            var mod = db.StockBals.Find(item.BalId);
                            mod.OpQty = item.OpQty != null ? (decimal)item.OpQty : 0;
                            mod.OpNos = item.OpNos != null ? (int)item.OpNos : 0;
                            mod.StockValue = item.StockValue != null ? (decimal)item.StockValue : 0;

                            var pmodel = db.Products.Find(item.Id);
                            pmodel.UomId = item.UomId != null ? (int)item.UomId : 0;
                            pmodel.PTypeId = item.PTypeId != null ? (int)item.PTypeId : 0;
                            pmodel.GroupId = item.GroupId != null ? (int)item.GroupId : 0;
                        }
                        db.SaveChanges();
                        _tran.Commit();
                        MessageBoxAdv.Show(this, KontoGlobals.SaveMessage, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UpdatedOpBal = new List<OpStockDto>();
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
