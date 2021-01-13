using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Konto.App.Shared;
using Konto.App.Shared.Para;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data.Models.Masters.Dtos;
using Konto.Data.Models.Transaction.Dtos;
using Konto.Shared.Masters.Acc;
using Konto.Shared.Masters.Color;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Konto.Shared.Masters.Item
{
    public partial class WeavingDetailView : KontoForm
    {
        public List<WeftItemDto> WeftData { get; set; }
        public List<WeftItemDto> DelWeft { get; set; }
        public string GridLayoutFileName { get; set; }
        public List<WeftItemDto> WarpData { get; set; }
        public List<WeftItemDto> DelWarpData { get; set; }

        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;

        public WeavingDetailView()
        {
            InitializeComponent();
            WeftData = new List<WeftItemDto>();
            WarpData = new List<WeftItemDto>();
            DelWarpData = new List<WeftItemDto>();
            DelWeft = new List<WeftItemDto>();
            headerEdit.Hide();
            headerEdit.Parent = this.gridControl1;
            headerEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            headerEdit.Leave += new EventHandler(headerEdit_Leave);

            this.Load += WeavingDetailView_Load;

            this.gridView1.CustomDrawRowIndicator += GridView1_CustomDrawRowIndicator;
            this.gridView1.DoubleClick += GridView1_DoubleClick;
            this.gridView1.MouseUp += GridView1_MouseUp;
            this.gridView1.InitNewRow += GridView1_InitNewRow;
            this.gridView1.CellValueChanged += GridView1_CellValueChanged;
            this.gridView1.KeyDown += GridView1_KeyDown;
            this.gridControl1.ProcessGridKey += GridControl1_ProcessGridKey;
            this.gridControl1.Enter += GridControl1_Enter;

            this.WarpgridView.CustomDrawRowIndicator += WarpgridView_CustomDrawRowIndicator;
            this.WarpgridView.DoubleClick += GridView1_DoubleClick;
            this.WarpgridView.MouseUp += GridView1_MouseUp;
            this.WarpgridView.InitNewRow += WarpgridView_InitNewRow;
            this.WarpgridView.CellValueChanged += WarpgridView_CellValueChanged;
            this.WarpgridView.KeyDown += WarpgridView_KeyDown;
            this.WarpgridControl.ProcessGridKey += WarpgridControl_ProcessGridKey;
            this.WarpgridControl.Enter += WarpgridControl_Enter;

            productRepositoryItemButtonEdit.ButtonClick += ProductRepositoryItemButtonEdit_ButtonClick;
            ProductrepositoryItemButtonEdit2.ButtonClick += ProductrepositoryItemButtonEdit2_ButtonClick;

            colorRepositoryItemButtonEdit.ButtonClick += ColorRepositoryItemButtonEdit_ButtonClick;
            ColorrepositoryItemButtonEdit2.ButtonClick += ColorrepositoryItemButtonEdit2_ButtonClick;

            AccrepositoryItemButtonEdit.ButtonClick += AccrepositoryItemButtonEdit_ButtonClick;
            AccrepositoryItemButtonEdit2.ButtonClick += AccrepositoryItemButtonEdit2_ButtonClick;
            this.gridView1.ShowingEditor += GridView1_ShowingEditor;
        }

        private void GridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            var rw = gridView1.GetRow(gridView1.FocusedRowHandle) as WeftItemDto;
            if (rw == null) return;

            if (rw.RS > 0 && rw.PI > 0 && (gridView1.FocusedColumn.FieldName == "Mtr" ||
                gridView1.FocusedColumn.FieldName == "Ends"))
                e.Cancel = true;
            
            if (rw.Mtr > 0 && rw.Ends > 0 && (gridView1.FocusedColumn.FieldName == "RS" ||
                gridView1.FocusedColumn.FieldName == "PI"))
                e.Cancel = true;
           
        }


        #region Event

        private void AccrepositoryItemButtonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup(WarpgridView);
            if (dr != null)
            {
                if (dr.AccId != null)
                    OpenAccLookup((int)dr.AccId, dr, WarpgridView);
                else
                    OpenAccLookup(0, dr, WarpgridView);
            }
        }

        private void AccrepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup(gridView1);
            if (dr != null)
            {
                if (dr.AccId != null)
                    OpenAccLookup((int)dr.AccId, dr, gridView1);
                else
                    OpenAccLookup(0, dr, gridView1);
            }
        }

        private void ColorrepositoryItemButtonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup(WarpgridView);
            if (dr != null)
            {
                if (dr.ColorId != null)
                    OpenColorLookup((int)dr.ColorId, dr, WarpgridView);
                else
                    OpenColorLookup(0, dr, WarpgridView);
            }
        }

        private void ColorRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup(gridView1);
            if (dr != null)
            {
                if (dr.ColorId != null)
                    OpenColorLookup((int)dr.ColorId, dr, gridView1);
                else
                    OpenColorLookup(0, dr, gridView1);
            }
        }

        private void ProductrepositoryItemButtonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        { 
            var dr = PreOpenLookup(WarpgridView);
            if (dr != null)
            {
                if (dr.ProductId != null)
                    OpenItemLookup((int)dr.ProductId, dr,WarpgridView);
                else
                    OpenItemLookup(0, dr,WarpgridView);
            }
        }

        private void ProductRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup(gridView1);
            if (dr != null)
            {
                if (dr.ProductId != null)
                    OpenItemLookup((int)dr.ProductId, dr, gridView1);
                else
                    OpenItemLookup(0, dr, gridView1);
            }
        }

        private void WeavingDetailView_Load(object sender, EventArgs e)
        {
            try
            {
                WeftbindingSource.DataSource = WeftData;
                WarpbindingSource.DataSource = WarpData;
            }
            catch (Exception ex)
            {
            }
        }

        #endregion
        #region WeftGrid

        private void GridControl1_Enter(object sender, EventArgs e)
        {
            gridView1.FocusedColumn = gridView1.VisibleColumns[0];
        }
         private void GridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                // if (Convert.ToInt32(accLookup1.SelectedValue) == 0) return;
                var dr = PreOpenLookup(gridView1);
                if (dr == null) return;
                if (gridView1.FocusedColumn.FieldName == "ProductName")
                {
                    if (Convert.ToInt32(dr.RefId) > 0) return;
                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.ProductId == 0 || dr.ProductId == null)
                        {
                            OpenItemLookup(0, dr, gridView1);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenItemLookup((int)dr.ProductId, dr, gridView1);
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        OpenItemLookup((int)dr.ProductId, dr, gridView1);
                        e.Handled = true;
                    }
                }
                else if (gridView1.FocusedColumn.FieldName == "ColorName")
                {
                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.ColorId == 0 || dr.ColorId == null)
                        {
                            OpenColorLookup(0, dr, gridView1);
                            // e.Handled = true;
                        }
                        else
                            OpenColorLookup((int)dr.ColorId, dr, gridView1);

                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        OpenColorLookup((int)dr.ColorId, dr, gridView1);
                        e.Handled = true;
                    }
                }
                else if (gridView1.FocusedColumn.FieldName == "AccName")
                {
                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.AccId == 0 || dr.AccId == null)
                        {
                            OpenAccLookup(0, dr, gridView1);
                            // e.Handled = true;
                        }
                        else
                            OpenAccLookup((int)dr.AccId, dr, gridView1);

                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        OpenAccLookup((int)dr.AccId, dr, gridView1);
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Weaving Detail GridControl KeyDown");
                MessageBoxAdv.Show(this, "Error Lookup Setup !!", "Exception ", ex.ToString());
            }
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
                var row = view.GetRow(view.FocusedRowHandle) as WeftItemDto;
                view.DeleteRow(view.FocusedRowHandle);
                DelWeft.Add(row);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                GridView view = sender as GridView;
                if (gridView1.FocusedColumn.FieldName == "ColorName")
                {
                    view.SetRowCellValue(view.FocusedRowHandle, colColorName, string.Empty);
                    view.SetRowCellValue(view.FocusedRowHandle, colColorId, 0);
                }
            }
        }
        private void GridView1_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            var rw = gridView1.GetRow(e.RowHandle) as WeftItemDto;
            rw.Id = -1 * gridView1.RowCount;
        }
        private void GridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var er = gridView1.GetFocusedRow() as WeftItemDto;
            if (er == null) return;
            decimal Qty;
            decimal denier = er.Denier != null ? (decimal)er.Denier : 0;
            decimal Epi = er.PI != null ? (decimal)er.PI : 0;
            decimal RS = er.RS != null ? (decimal)er.RS : 0;
            decimal Mtr = er.Mtr != null ? (decimal)er.Mtr : 0;
            decimal Ends = er.Ends != null ? (decimal)er.Ends : 0;

            if (e.Column.FieldName == "Denier")
            {
                denier = er.Denier != null ? (decimal)er.Denier : 0;
            }

            if (Epi > 0 || RS > 0)
            {
                if (e.Column.FieldName == "PI")
                {
                    Epi = er.PI != null ? (decimal)er.PI : 0;
                }


                if (e.Column.FieldName == "RS")
                {
                    RS = er.RS != null ? (decimal)er.RS : 0;
                }
                if (ProductPara.RS_In_Inch)
                {
                    Qty = decimal.Round((denier * Epi * RS) / 90000,2,MidpointRounding.AwayFromZero);
                }
                else
                {
                    Qty = decimal.Round( ((((denier * Epi * RS) / 9000) + (((denier * Epi * RS) / 9000) * 10 / 100)) * 100) / 1000,2,MidpointRounding.AwayFromZero);
                }
                 // (gridView1.Columns["Mtr"] as GridColumn).OptionsColumn.ReadOnly = true;
                 //(gridView1.Columns["Ends"] as GridColumn).OptionsColumn.ReadOnly = true;
            }
            else if (Mtr > 0 || Ends > 0)
            {
                if (e.Column.FieldName == "Mtr")
                {
                    Mtr = er.Mtr != null ? (decimal)er.Mtr : 0;
                }
                if (e.Column.FieldName == "Ends")
                {
                    Ends = er.Ends != null ? (decimal)er.Ends : 0;
                }
                Qty = decimal.Round( (denier * Mtr * Ends) / 9000000,2,MidpointRounding.AwayFromZero);

                //(gridView1.Columns["RS"] as GridColumn).OptionsColumn.ReadOnly = true;
                //(gridView1.Columns["PI"] as GridColumn).OptionsColumn.ReadOnly = true;
            }
            else
            {
                Qty = 0;
            }

            er.Qty = Qty;
        }
        private void GridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.Appearance.ForeColor = System.Drawing.Color.FromArgb(227, 22, 91);

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

        #endregion
        #region WarpGrid

        private void WarpgridControl_Enter(object sender, EventArgs e)
        {
            WarpgridView.FocusedColumn = WarpgridView.VisibleColumns[0];
        }
        private void WarpgridControl_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                // if (Convert.ToInt32(accLookup1.SelectedValue) == 0) return;
                var dr = PreOpenLookup(WarpgridView);
                if (dr == null) return;
                if (WarpgridView.FocusedColumn.FieldName == "ProductName")
                {
                    if (Convert.ToInt32(dr.RefId) > 0) return;
                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.ProductId == 0 || dr.ProductId == null)
                        {
                            OpenItemLookup(0, dr, WarpgridView);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenItemLookup((int)dr.ProductId, dr, WarpgridView);
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        OpenItemLookup((int)dr.ProductId, dr, WarpgridView);
                        e.Handled = true;
                    }
                }
                else if (WarpgridView.FocusedColumn.FieldName == "ColorName")
                {
                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.ColorId == 0 || dr.ColorId == null)
                        {
                            OpenColorLookup(0, dr, WarpgridView);
                            // e.Handled = true;
                        }
                        else
                            OpenColorLookup((int)dr.ColorId, dr, WarpgridView);

                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        OpenColorLookup((int)dr.ColorId, dr, WarpgridView);
                        e.Handled = true;
                    }
                }
                else if (WarpgridView.FocusedColumn.FieldName == "AccName")
                {
                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.AccId == 0 || dr.AccId == null)
                        {
                            OpenAccLookup(0, dr, WarpgridView);
                            // e.Handled = true;
                        }
                        else
                            OpenAccLookup((int)dr.AccId, dr, WarpgridView);

                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        OpenAccLookup((int)dr.AccId, dr, WarpgridView);
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Weaving Detail Warp GridControl KeyDown");
                MessageBoxAdv.Show(this, "Error Lookup Setup !!", "Exception ", ex.ToString());
            }
        }

        private void WarpgridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete) return;
            if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
            {
                if (MessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo) !=
                  DialogResult.Yes)
                    return;
                GridView view = sender as GridView;
                var row = view.GetRow(view.FocusedRowHandle) as WeftItemDto;
                view.DeleteRow(view.FocusedRowHandle);
                DelWarpData.Add(row);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                GridView view = sender as GridView;
                if (WarpgridView.FocusedColumn.FieldName == "ColorName")
                {
                    view.SetRowCellValue(view.FocusedRowHandle, colColorName, string.Empty);
                    view.SetRowCellValue(view.FocusedRowHandle, colColorId, 0);
                }
            }
        }
        private void WarpgridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var er = WarpgridView.GetFocusedRow() as WeftItemDto;
            if (er == null) return;
            decimal Qty;
            decimal denier = er.Denier != null ? (decimal)er.Denier : 0;
            decimal Epi = er.PI != null ? (decimal)er.PI : 0;
            decimal RS = er.RS != null ? (decimal)er.RS : 0;
            decimal Mtr = er.Mtr != null ? (decimal)er.Mtr : 0;
            decimal Ends = er.Ends != null ? (decimal)er.Ends : 0;
            if (e.Column.FieldName == "Denier")
            {
                denier = er.Denier != null ? (decimal)er.Denier : 0;
            }
            if (Epi > 0 || RS > 0)
            {
                if (e.Column.FieldName == "PI")
                {
                    Epi = er.PI != null ? (decimal)er.PI : 0;
                }

                if (e.Column.FieldName == "RS")
                {
                    RS = er.RS != null ? (decimal)er.RS : 0;
                }
                if (ProductPara.RS_In_Inch)
                {
                    Qty = decimal.Round( (denier * Epi * RS) / 90000,2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    Qty = decimal.Round( ((((denier * Epi * RS) / 9000) + (((denier * Epi * RS) / 9000) * 10 / 100)) * 100) / 1000,2, MidpointRounding.AwayFromZero);
                }
               

            }
            else if (Mtr > 0 || Ends > 0)
            {
                if (e.Column.FieldName == "Mtr")
                {
                    Mtr = er.Mtr != null ? (decimal)er.Mtr : 0;
                }

                if (e.Column.FieldName == "Ends")
                {
                    Ends = er.Ends != null ? (decimal)er.Ends : 0;
                }

                Qty = decimal.Round((denier * Mtr * Ends) / 9000000,2, MidpointRounding.AwayFromZero);
               
            }
            else
            {
                Qty = 0;
            }
            er.Qty = Qty;
        }
        private void WarpgridView_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            var rw = WarpgridView.GetRow(e.RowHandle) as WeftItemDto;
            rw.Id = -1 * WarpgridView.RowCount;
        }
        private void WarpgridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.Appearance.ForeColor = System.Drawing.Color.FromArgb(227, 22, 91);

            if (e.RowHandle < 0)
                e.Info.DisplayText = "";
            else
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        #endregion
        #region org
        //private WeftItemDto PreOpenLookup()
        //{
        //    gridView1.GetRow(gridView1.FocusedRowHandle);
        //    if (gridView1.GetRow(gridView1.FocusedRowHandle) == null)
        //    {
        //        gridView1.AddNewRow();
        //    }
        //    var dr = (WeftItemDto)gridView1.GetRow(gridView1.FocusedRowHandle);
        //    return dr;
        //}
        //private void OpenItemLookup(int _selvalue, WeftItemDto er)
        //{
        //    var frm = new ProductLkpWindow();

        //    frm.SelectedValue = _selvalue;
        //    frm.Tag = MenuId.Product_Master;

        //    frm.ShowDialog();
        //    if (frm.DialogResult == DialogResult.OK)
        //    {
        //        er.ProductId = frm.SelectedValue;
        //        er.ProductName = frm.SelectedTex;
        //        var model = frm.SelectedItem as ProductLookupDto;

        //        er.Rate = model.DealerPrice;

        //        gridView1.FocusedColumn = gridView1.GetNearestCanFocusedColumn(gridView1.FocusedColumn);
        //    }
        //}
        //private void OpenAccLookup(int _selvalue, WeftItemDto er)
        //{
        //    var frm = new AccLkpWindow();
        //    frm.SelectedValue = _selvalue;
        //    frm.Tag = MenuId.Product_Master;
        //    frm.ShowDialog();
        //    if (frm.DialogResult == DialogResult.OK)
        //    {
        //        er.AccId = frm.SelectedValue;
        //        er.AccName = frm.SelectedTex;

        //        gridView1.FocusedColumn = gridView1.GetVisibleColumn(colAccName.VisibleIndex + 1);
        //    }
        //}
        //private void OpenColorLookup(int _selvalue, WeftItemDto er)
        //{
        //    var frm = new ColorLkpWindow();
        //    frm.SelectedValue = _selvalue;
        //    frm.Tag = MenuId.Color;
        //    frm.ShowDialog();
        //    if (frm.DialogResult == DialogResult.OK)
        //    {
        //        gridView1.BeginDataUpdate();
        //        er.ColorId = frm.SelectedValue;
        //        er.ColorName = frm.SelectedTex;

        //        gridView1.EndDataUpdate();
        //        gridView1.FocusedColumn = gridView1.GetVisibleColumn(colColorName.VisibleIndex + 1);
        //    }

        //}

        #endregion
        #region UDF

        private WeftItemDto PreOpenLookup(GridView gridView)
        {
            gridView.GetRow(gridView.FocusedRowHandle);
            if (gridView.GetRow(gridView.FocusedRowHandle) == null)
            {
                gridView.AddNewRow();
            }
            var dr = (WeftItemDto)gridView.GetRow(gridView.FocusedRowHandle);
            return dr;
        }
        private void OpenItemLookup(int _selvalue, WeftItemDto er, GridView gridView)
        {
            var frm = new ProductLkpWindow();

            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Product_Master;

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                er.ProductId = frm.SelectedValue;
                er.ProductName = frm.SelectedTex;
                var model = frm.SelectedItem as ProductLookupDto;

                er.Rate = model.DealerPrice;

                gridView.FocusedColumn = gridView.GetNearestCanFocusedColumn(gridView.FocusedColumn);
            }
        }
        private void OpenAccLookup(int _selvalue, WeftItemDto er, GridView gridView)
        {
            var frm = new AccLkpWindow();
            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Product_Master;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                er.AccId = frm.SelectedValue;
                er.AccName = frm.SelectedTex;

                // gridView.FocusedColumn = gridView.GetNearestCanFocusedColumn(gridView.FocusedColumn);
                int index = gridView.Columns["AccName"].VisibleIndex;
                    gridView.FocusedColumn = gridView.GetVisibleColumn(index + 1);
                
            }
        }
        private void OpenColorLookup(int _selvalue, WeftItemDto er, GridView gridView)
        {
            var frm = new ColorLkpWindow();
            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Color;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                gridView.BeginDataUpdate();
                er.ColorId = frm.SelectedValue;
                er.ColorName = frm.SelectedTex;

                gridView.EndDataUpdate();
                //gridView.FocusedColumn = gridView.GetNearestCanFocusedColumn(gridView.FocusedColumn);
                int index = gridView.Columns["ColorName"].VisibleIndex;
                gridView.FocusedColumn = gridView.GetVisibleColumn(index + 1);
            }
        }
        #endregion
        void headerEdit_Leave(object sender, EventArgs e)
        {
            activeCol.Caption = headerEdit.Text;
            headerEdit.Hide();
        }

        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    //if (keyData == (Keys.F1 | Keys.Shift))
        //    //{
        //    //    KontoUtils.SaveLayoutGrid(this.GridLayoutFileName, this.gridView1);
        //    //    return true;
        //    //}
        //    //else if (keyData == (Keys.F2 | Keys.Shift))
        //    //{

        //    //    var frm = new GridPropertView();
        //    //    frm.gridControl1.DataSource = this.gridControl1.DataSource;
        //    //    frm.gridView1.Assign(this.gridView1, false);
        //    //    if (frm.ShowDialog() != DialogResult.OK) return true;
        //    //    this.gridView1.Assign(frm.gridView1, false);
        //    //    KontoUtils.SaveLayoutGrid(this.GridLayoutFileName, this.gridView1);
        //    //    return true;
        //    //}
        //    //return base.ProcessCmdKey(ref msg, keyData);
        //}

        private void okSimpleButton_Click(object sender, EventArgs e)
        {
            WeftData = WeftbindingSource.DataSource as List<WeftItemDto>;
            WarpData = WarpbindingSource.DataSource as List<WeftItemDto>;
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
