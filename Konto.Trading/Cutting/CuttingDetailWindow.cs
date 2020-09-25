using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters.Dtos;
using Konto.Data.Models.Transaction.Dtos;
using Konto.Shared.Masters.Color;
using Konto.Shared.Masters.Design;
using Konto.Shared.Masters.Item;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Trading.Cutting
{
    public partial class CuttingDetailWindow : KontoForm
    {
        public List<CuttingTransDto> TransList { get; set; }
        public List<CuttingTransDto> DelTakaCutList { get; set; }
        public decimal FinalMtr { get;  set; }

        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        private string GridLayoutFileName = KontoFileLayout.Cutting_Detail_Window;

        public CuttingDetailWindow()
        {
            InitializeComponent();

            DelTakaCutList = new List<CuttingTransDto>();
            this.Shown += CuttingDetailWindow_Shown;
            headerEdit.Hide();
            headerEdit.Parent = this.gridControl1;
            headerEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            headerEdit.Leave += new EventHandler(headerEdit_Leave);
            this.gridView1.DoubleClick += GridView1_DoubleClick;
            this.gridView1.MouseUp += GridView1_MouseUp;
             this.gridView1.CustomDrawRowIndicator += GridView1_CustomDrawRowIndicator;

            gridView1.InitNewRow += GridView1_InitNewRow;
            gridView1.CellValueChanged += GridView1_CellValueChanged;
            gridView1.KeyDown += GridView1_KeyDown;
            gridControl1.Enter += GridControl1_Enter; 
            gridView1.ShowingEditor += GridView1_ShowingEditor; 

            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.gridControl1.ProcessGridKey += GridControl1_ProcessGridKey;
        }

        private void CuttingDetailWindow_Shown(object sender, EventArgs e)
        {
            try
            {
                using (var db = new KontoContext())
                {
                      this.bindingSource1.DataSource = TransList;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Cutting Detail");
                MessageBox.Show(ex.ToString());
            }
        }
        private CuttingTransDto PreOpenLookup()
        {
                      gridView1.GetRow(gridView1.FocusedRowHandle);
            if (gridView1.GetRow(gridView1.FocusedRowHandle) == null)
            {
                gridView1.AddNewRow();
            }
            var dr = (CuttingTransDto)gridView1.GetRow(gridView1.FocusedRowHandle);
            dr.ProductId = TransList.FirstOrDefault().ProductId;
            dr.ProductName= TransList.FirstOrDefault().ProductName;

            dr.ColorId= TransList.FirstOrDefault().ColorId;
            dr.ColorName= TransList.FirstOrDefault().ColorName;

            dr.DesignId= TransList.FirstOrDefault().DesignId;
            dr.DesignNo= TransList.FirstOrDefault().DesignNo;

            dr.MiscId= TransList.FirstOrDefault().MiscId;
            dr.BatchId = TransList.FirstOrDefault().BatchId; 
            dr.LotNo= TransList.FirstOrDefault().LotNo;
            dr.Pcs = 1;
            return dr;
        }
        private void OpenItemLookup(int _selvalue, CuttingTransDto er)
        {
            var frm = new ProductLkpWindow();

            frm.SelectedValue = _selvalue;
            frm.PTypeId = ProductTypeEnum.GREY;
            frm.VoucherType = VoucherTypeEnum.GreyOrder;

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                er.ProductId = frm.SelectedValue;
                er.ProductName = frm.SelectedTex;
                var model = frm.SelectedItem as ProductLookupDto;
                er.UnitId = model.PurUomId;
                er.Rate = model.DealerPrice;

                gridView1.FocusedColumn = gridView1.VisibleColumns[gridView1.FocusedColumn.VisibleIndex + 1];
            }
        }
        private void OpenColorLookup(int _selvalue, CuttingTransDto er)
        {
            var frm = new ColorLkpWindow();
            frm.SelectedValue = _selvalue;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                gridView1.BeginDataUpdate();
                er.ColorId = frm.SelectedValue;
                er.ColorName = frm.SelectedTex;
                gridView1.EndDataUpdate();
                gridView1.FocusedColumn = gridView1.GetVisibleColumn(colColorName.VisibleIndex + 1);
            }

        }
        private void OpenDesignLookup(int _selvalue, CuttingTransDto er)
        {
            var frm = new DesignLkpWindow();
            frm.SelectedValue = _selvalue;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                gridView1.BeginDataUpdate();
                er.DesignId = frm.SelectedValue;
                er.DesignNo = frm.SelectedTex;
                gridView1.EndDataUpdate();
                gridView1.FocusedColumn = gridView1.GetVisibleColumn(colDesignNo.VisibleIndex + 1);
            }

        }
    
        private void GridControl1_Enter(object sender, EventArgs e)
        {
            gridView1.FocusedColumn = gridView1.VisibleColumns[0];
        }
        private void GridView1_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!"Pcs,Qty,IssueQty".Contains(gridView1.FocusedColumn.FieldName)) return;
            if (!(gridView1.GetFocusedRow() is MrvTransDto itm)) return;
        }

        private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var rw = gridView1.GetRow(e.RowHandle) as CuttingTransDto;
            rw.Id = -1 * gridView1.RowCount;
        }
        private void GridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == null) return;
            if (!(gridView1.GetRow(e.RowHandle) is CuttingTransDto er)) return;
          
        }
        private void GridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete) return;

            if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
            {
                if (MessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo) !=
                  DialogResult.Yes)
                    return;
                GridView view = sender as GridView;
                var row = view.GetRow(view.FocusedRowHandle) as CuttingTransDto;
                view.DeleteRow(view.FocusedRowHandle);
                DelTakaCutList.Add(row);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                GridView view = sender as GridView;
                if (gridView1.FocusedColumn.FieldName == "ColorName")
                {
                    view.SetRowCellValue(view.FocusedRowHandle, colColorName, string.Empty);
                    view.SetRowCellValue(view.FocusedRowHandle, colColorId, 0);
                }
                else if (gridView1.FocusedColumn.FieldName == "DesignNo")
                {
                    view.SetRowCellValue(view.FocusedRowHandle, colDesignNo, string.Empty);
                    view.SetRowCellValue(view.FocusedRowHandle, colDesignId, 0);
                }
            }
        }

        private void GridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode != Keys.Return) return;
            //okSimpleButton.PerformClick();
            try
            {
                 var dr = PreOpenLookup();
                if (dr == null) return;
                if (gridView1.FocusedColumn.FieldName == "ProductName")
                {

                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.ProductId == 0)
                        {
                            OpenItemLookup(dr.ProductId, dr);
                            // e.Handled = true;
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        OpenItemLookup(dr.ProductId, dr);
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Cutting GridControl KeyDown");
                MessageBoxAdv.Show(this, "Error Lookup Setup !!", "Exception ", ex.ToString());

            }

        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            TransList = bindingSource1.DataSource as List<CuttingTransDto>;
            if (TransList.Sum(k => k.Qty) > FinalMtr)
            {
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", "Total Qty can not greater than finish meter!!!");
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void GridView1_MouseUp(object sender, MouseEventArgs e)
        {
            DevExpress.Utils.DXMouseEventArgs args = (e as DevExpress.Utils.DXMouseEventArgs);
            GridView view = sender as GridView;
            GridHitInfo hi = view.CalcHitInfo(args.Location);
            if (hi.InColumn)
            {
                GridViewInfo ViewInfo = view.GetViewInfo() as GridViewInfo;
                GridState prevState = view.State;
                if ((e.Button & MouseButtons.Left) != 0)
                {
                    if (ViewInfo.ColumnsInfo[hi.Column].CaptionRect.Contains(new Point(e.X, e.Y)))
                    {
                        ViewInfo.SelectionInfo.ClearPressedInfo();
                        args.Handled = true;
                    }
                }
            }
        }

        private void GridView1_DoubleClick(object sender, EventArgs e)
        {
            DevExpress.Utils.DXMouseEventArgs args = (e as DevExpress.Utils.DXMouseEventArgs);
            GridView view = sender as GridView;
            GridHitInfo hi = view.CalcHitInfo(args.Location);
            if (hi.InColumn)
            {
                GridViewInfo vi = view.GetViewInfo() as GridViewInfo;
                Rectangle bounds = vi.ColumnsInfo[hi.Column].Bounds;
                bounds.Width -= 10;
                bounds.Height -= 3;
                bounds.Y += 3;
                headerEdit.SetBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);
                headerEdit.EditValue = hi.Column.Caption;
                headerEdit.Show();
                headerEdit.Focus();
                activeCol = hi.Column;
            }
        }

        void headerEdit_Leave(object sender, EventArgs e)
        {
            activeCol.Caption = headerEdit.Text;
            headerEdit.Hide();
        }

        private void GridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.Appearance.ForeColor = Color.FromArgb(227, 22, 91);
            if (e.RowHandle < 0)
                e.Info.DisplayText = "";
            else
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.F1 | Keys.Shift))
            {
                KontoUtils.SaveLayoutGrid(this.GridLayoutFileName, this.gridView1);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

       
        private void ProductRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
                OpenItemLookup(dr.ProductId, dr);

        }
        private void ColorRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            var dr = PreOpenLookup();
            if (dr != null)
                OpenColorLookup(dr.ColorId, dr);
        }

        private void DesignRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
                OpenDesignLookup(dr.DesignId, dr);
        }

    }
}
