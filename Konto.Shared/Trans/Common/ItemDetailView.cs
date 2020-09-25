using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Konto.App.Shared;
using Konto.App.Shared.Para;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data.Models.Transaction.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Konto.Shared.Trans.Common
{
    public partial class ItemDetailView : KontoForm
    {
        public ProductTypeEnum TypeEnum { get; set; }
        public BindingList<GrnProdDto> prodDtos { get; set; }
        public List<GrnProdDto> DelProd { get; set; }
        public string GridLayoutFileName { get; set; }
        public bool EditableNetWeight { get; set; }
        public bool IsReadOnly = false;

        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        private int colorId;
        private int gradeId;
        private int productid;
        private int plyProductId;
        private string productName;

        public decimal MetersPerKgs { get; set; } 
        public ItemDetailView()
        {
            InitializeComponent();
            prodDtos = new BindingList<GrnProdDto>();
            headerEdit.Hide();
            headerEdit.Parent = this.gridControl1;
            headerEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            headerEdit.Leave += new EventHandler(headerEdit_Leave);
            this.Load += ItemDetailView_Load;
            this.gridView1.CustomDrawRowIndicator += GridView1_CustomDrawRowIndicator;
            this.gridView1.DoubleClick += GridView1_DoubleClick;
            this.gridView1.MouseUp += GridView1_MouseUp;
            this.gridView1.InitNewRow += GridView1_InitNewRow;
            this.gridView1.CellValueChanged += GridView1_CellValueChanged;
            this.gridView1.KeyDown += GridView1_KeyDown;
            this.gridControl1.ProcessGridKey += GridControl1_ProcessGridKey;
            this.gridControl1.Enter += GridControl1_Enter;
        }

        private void GridControl1_Enter(object sender, EventArgs e)
        {
            gridView1.FocusedColumn = gridView1.VisibleColumns[0];
        }

        private void GridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            //if()
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
                var row = view.GetRow(view.FocusedRowHandle) as GrnProdDto;
                view.DeleteRow(view.FocusedRowHandle);
                DelProd.Add(row);
            }
        }

        private void GridView1_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            
            gridView1.SetRowCellValue(e.RowHandle, "SrNo", gridView1.RowCount);
            gridView1.SetRowCellValue(e.RowHandle, "ProductName", productName);
            gridView1.SetRowCellValue(e.RowHandle, "GradeId", gradeId);
            gridView1.SetRowCellValue(e.RowHandle, "ColorId", colorId);
            gridView1.SetRowCellValue(e.RowHandle, "ProductId", productid);
            gridView1.SetRowCellValue(e.RowHandle, "PlyProductId", plyProductId);
           
            if(gridView1.Columns["GrossWt"].VisibleIndex!=0)
            gridView1.SetRowCellValue(e.RowHandle, "GrossWt", 0);

            gridView1.SetRowCellValue(e.RowHandle, "IsOk", true);

            gridView1.SetRowCellValue(e.RowHandle, "Cops", 0);
            gridView1.SetRowCellValue(e.RowHandle, "Ply", 0);
            gridView1.SetRowCellValue(e.RowHandle, "Tops", 0);
            gridView1.SetRowCellValue(e.RowHandle, "TareWt", 0);
            gridView1.SetRowCellValue(e.RowHandle, "CurrQty", 0);
            gridView1.SetRowCellValue(e.RowHandle, "FinQty", 0);
        }

        private void GridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var er = gridView1.GetFocusedRow() as GrnProdDto;
            if (e.Column.FieldName == "CopsRate" || e.Column.FieldName == "Tops")
            {
                er.NetWt = er.Tops * er.CopsRate;
                er.CurrQty = er.NetWt;
                er.IsOk = true;
            }
            if (er.VoucherNo == null)
            {
                er.VoucherNo = er.SrNo.ToString();
            }

            if (e.Column.FieldName == "NetWt")
            {
                er.CurrQty = er.NetWt;
                
            }
            if(e.Column.FieldName=="GrossWt" && MetersPerKgs > 0)
            {
                er.NetWt = er.GrossWt * MetersPerKgs;
                er.CurrQty = er.NetWt;
            }
            if (this.TypeEnum == ProductTypeEnum.YARN || this.TypeEnum == ProductTypeEnum.POY || this.TypeEnum == ProductTypeEnum.BEAM)
            {
                er.NetWt = er.GrossWt - er.TareWt;
            }
            if (e.Column.FieldName == "GradeId")
            {
                if (er.GradeId != null)
                    gradeId = (int)er.GradeId;
            }
            if (e.Column.FieldName == "ColorId")
            {
                if (er.ColorId != null)
                    colorId = (int)er.ColorId;
            }
            if (e.Column.FieldName == "PlyProductId")
            {
                if (er.PlyProductId != null)
                    plyProductId = (int)er.PlyProductId;
            }
        }

        private void GridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.Appearance.ForeColor = Color.FromArgb(227, 22, 91);

            if (e.RowHandle < 0)
                e.Info.DisplayText = "";
            else
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
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
        private void ItemDetailView_Load(object sender, System.EventArgs e)
        {
            this.gridControl1.DataSource = prodDtos;
            DelProd = new List<GrnProdDto>();
            KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName,gridView1);
            this.ActiveControl = gridControl1;

            gridView1.Columns["Extra1"].Visible = false;

            if (this.IsReadOnly)
            {
                foreach (GridColumn item in gridView1.Columns)
                {
                    item.OptionsColumn.AllowEdit = true;
                    item.OptionsColumn.ReadOnly = true;
                }
            }
            if (GRNPara.Generate_Barcode || JobRecPara.Generate_Barcode)
            {
                gridView1.Columns["Extra1"].Visible = true;
                gridView1.Columns["Extra1"].OptionsColumn.AllowFocus = false;
                gridView1.Columns["Extra1"].Caption = "Barcode";
            }

            if (this.EditableNetWeight)
            {
                gridView1.Columns["NetWt"].OptionsColumn.ReadOnly = false;
                gridView1.Columns["NetWt"].OptionsColumn.AllowEdit = true;
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.F1 | Keys.Shift))
            {
                KontoUtils.SaveLayoutGrid(this.GridLayoutFileName, this.gridView1);
                return true;
            }
            else if (keyData == (Keys.F2 | Keys.Shift))
            {
                
                var frm = new GridPropertView();
                frm.gridControl1.DataSource = this.gridControl1.DataSource;
                frm.gridView1.Assign(this.gridView1, false);
                if (frm.ShowDialog() != DialogResult.OK) return true;
                this.gridView1.Assign(frm.gridView1, false);
                KontoUtils.SaveLayoutGrid(this.GridLayoutFileName, this.gridView1);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void okSimpleButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
