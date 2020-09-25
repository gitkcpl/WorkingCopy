using AutoMapper;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Konto.App.Shared;
using Konto.App.Shared.Para;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Masters.Dtos;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using Konto.Shared.Masters.Acc;
using Konto.Shared.Masters.Color;
using Konto.Shared.Masters.Design;
using Konto.Shared.Masters.Item;
using Konto.Weaves.BeamLoading;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Weaves.ColorMatching
{
    public partial class ColorMIndex : KontoMetroForm
    {
        private List<WeftItemModel> FilterView = new List<WeftItemModel>();
        private List<WeftItemDto> _DelWeft = new List<WeftItemDto>();
        private List<WeftItemDto> _DelWarp = new List<WeftItemDto>();
        private decimal ModelMtr = 0;
        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        public ColorMIndex()
        {
            InitializeComponent();

            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            this.Load += ColorMIndex_Load;
            this.MainLayoutFile = KontoFileLayout.ColorMatching_Index;
            this.GridLayoutFile = KontoFileLayout.ColorMatching_Trans;

            WarpgridView.InitNewRow += ProdgridView_InitNewRow;
            WarpgridView.KeyDown += ProdgridView_KeyDown;
            WarpgridControl.Enter += ProdGridControl_Enter;
            WarpgridView.MouseUp += ProdgridView_MouseUp;
            WarpgridView.InvalidRowException += ProdgridView_InvalidRowException;
            WarpgridView.ShowingEditor += ProdgridView_ShowingEditor;
            WarpgridView.DoubleClick += ProdgridView_DoubleClick;
            WarpgridView.CustomDrawRowIndicator += ProdgridView_CustomDrawRowIndicator;
            WarpgridControl.ProcessGridKey += OrderDetailgridControl_ProcessGridKey;
            WarpgridView.CellValueChanged += WarpgridView_CellValueChanged;

            WeftgridView.InitNewRow += WeftgridView_InitNewRow;
            WeftgridView.KeyDown += WeftgridView_KeyDown;
            WeftgridControl.Enter += WeftgridControl_Enter;
            WeftgridView.MouseUp += WeftgridView_MouseUp;
            WeftgridView.InvalidRowException += WeftgridView_InvalidRowException;
            WeftgridView.ShowingEditor += WeftgridView_ShowingEditor;
            WeftgridView.DoubleClick += WeftgridView_DoubleClick;
            WeftgridView.CustomDrawRowIndicator += WeftgridView_CustomDrawRowIndicator;
            WeftgridControl.ProcessGridKey += WeftgridControl_ProcessGridKey;
            WeftgridView.CellValueChanged += WeftgridView_CellValueChanged;

            productRepositoryItemButtonEdit1.ButtonClick += ProductRepositoryItemButtonEdit1_ButtonClick;
            colorRepositoryItemButtonEdit1.ButtonClick += ColorRepositoryItemButtonEdit1_ButtonClick;
            AccrepositoryItemButtonEdit1.ButtonClick += AccrepositoryItemButtonEdit1_ButtonClick;

            ItemrepositoryItemButtonEdit.ButtonClick += ItemrepositoryItemButtonEdit_ButtonClick;
            ColorrepositoryItemButtonEdit.ButtonClick += ColorrepositoryItemButtonEdit_ButtonClick;
            AccrepositoryItemButtonEdit.ButtonClick += AccrepositoryItemButtonEdit_ButtonClick;

            WastageInPerspinEdit.ValueChanged += WastageInPerspinEdit_ValueChanged;
            NetWeightSpinEdit.ValueChanged += NetWeightSpinEdit_ValueChanged;
            JobChargeSpinEdit.ValueChanged += JobChargeSpinEdit_ValueChanged;
        }

        #region UDF

        private WeftItemDto PreOpenLookup()
        {
            WarpgridView.GetRow(WarpgridView.FocusedRowHandle);
            if (WarpgridView.GetRow(WarpgridView.FocusedRowHandle) == null)
            {
                WarpgridView.AddNewRow();
            }
            var dr = (WeftItemDto)WarpgridView.GetRow(WarpgridView.FocusedRowHandle);
            return dr;
        }
        private void OpenItemLookup(int _selvalue, WeftItemDto er)
        {
            var frm = new ProductLkpWindow();

            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Product_Master;
            frm.VoucherType = VoucherTypeEnum.JobCard;

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                er.ProductId = frm.SelectedValue;
                er.ProductName = frm.SelectedTex;
                var model = frm.SelectedItem as ProductLookupDto;

                WarpgridView.FocusedColumn = WarpgridView.GetNearestCanFocusedColumn(WarpgridView.FocusedColumn);
            }
        }
        private void OpenColorLookup(int _selvalue, WeftItemDto er)
        {
            var frm = new ColorLkpWindow();
            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Color;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                WarpgridView.BeginDataUpdate();
                er.ColorId = frm.SelectedValue;
                er.ColorName = frm.SelectedTex;

                WarpgridView.EndDataUpdate();
                WarpgridView.FocusedColumn = WarpgridView.GetVisibleColumn(colColorName1.VisibleIndex + 1);
            }
        }

        private void OpenAccLookup(int _selvalue, WeftItemDto er)
        {
            var frm = new AccLkpWindow();
            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Color;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                WarpgridView.BeginDataUpdate();
                er.AccId = frm.SelectedValue;
                er.AccName = frm.SelectedTex;

                WarpgridView.EndDataUpdate();
                WarpgridView.FocusedColumn = WarpgridView.GetVisibleColumn(colAccName1.VisibleIndex + 1);
            }
        }

        private WeftItemDto PreOpenWeftLookup()
        {
            WeftgridView.GetRow(WeftgridView.FocusedRowHandle);
            if (WeftgridView.GetRow(WeftgridView.FocusedRowHandle) == null)
            {
                WeftgridView.AddNewRow();
            }
            var dr = (WeftItemDto)WeftgridView.GetRow(WeftgridView.FocusedRowHandle);
            return dr;
        }
        private void OpenItemWeftLookup(int _selvalue, WeftItemDto er)
        {
            var frm = new ProductLkpWindow();

            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Product_Master;
            frm.VoucherType = VoucherTypeEnum.JobCard;

            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {
                er.ProductId = frm.SelectedValue;
                er.ProductName = frm.SelectedTex;
                var model = frm.SelectedItem as ProductLookupDto;

                //yarngridView.FocusedColumn = yarngridView.GetNearestCanFocusedColumn(yarngridView.FocusedColumn);
                WeftgridView.FocusedColumn = WeftgridView.GetVisibleColumn(colProductName.VisibleIndex + 1);
            }
        }
        private void OpenColorWeftLookup(int _selvalue, WeftItemDto er)
        {
            var frm = new ColorLkpWindow();
            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Color;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                WeftgridView.BeginDataUpdate();
                er.ColorId = frm.SelectedValue;
                er.ColorName = frm.SelectedTex;

                WeftgridView.EndDataUpdate();
                WeftgridView.FocusedColumn = WeftgridView.GetVisibleColumn(colColorName.VisibleIndex + 1);
            }
        }
        private void OpenAccWeftLookup(int _selvalue, WeftItemDto er)
        {
            var frm = new AccLkpWindow();
            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Color;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                WeftgridView.BeginDataUpdate();
                er.AccId = frm.SelectedValue;
                er.AccName = frm.SelectedTex;

                WeftgridView.EndDataUpdate();
                WeftgridView.FocusedColumn = WeftgridView.GetVisibleColumn(colAccName.VisibleIndex + 1);
            }
        }
        private void UpdateFooter()
        {
            if (QualityWeightspinEdit.Value == 0 || NetWeightSpinEdit == null) return;
            WastespinEdit.Value = QualityWeightspinEdit.Value - NetWeightSpinEdit.Value;
            WastePerspinEdit.Value = (NetWeightSpinEdit.Value * 100 / QualityWeightspinEdit.Value) - 100;

            if (JobChargeSpinEdit.Value == 0 || AveragePickspinEdit.Value == null
                || AveragePickspinEdit.Value == 0 || LengthinMeterspinEdit.Value == null || LengthinMeterspinEdit.Value == 0) return;
            JobChargeOneSareespinEdit.Value = JobChargeSpinEdit.Value * AveragePickspinEdit.Value * LengthinMeterspinEdit.Value;
            CostWithWastagespinEdit.Value = YarnCostspinEdit.Value + JobChargeOneSareespinEdit.Value;
            CostWithWastagespinEdit.Value = (CostWithoutWastagespinEdit1.Value * WastageInPerspinEdit.Value / 100) + CostWithoutWastagespinEdit1.Value;
            OneMeterCostspinEdit.Value = CostWithWastagespinEdit.Value / LengthinMeterspinEdit.Value;
        }

        #endregion
        #region WeftGrid
        private void WeftgridView_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            var rw = WeftgridView.GetRow(e.RowHandle) as WeftItemDto;
            rw.Id = -1 * WeftgridView.RowCount;

            rw.ItemId = Convert.ToInt32(ProductLookup.SelectedValue);
            rw.ProductId = Convert.ToInt32(ProductLookup.SelectedValue);
            rw.ProductName = ProductLookup.SelectedText;

            rw.ColorId = Convert.ToInt32(colorLookup1.SelectedValue);
            rw.ColorName = colorLookup1.SelectedText;
        }

        private void WeftgridControl_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                var dr = PreOpenWeftLookup();
                if (dr == null) return;
                if (WeftgridView.FocusedColumn.FieldName == "ProductName")
                {

                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.ProductId == null)
                        {
                            OpenItemWeftLookup(0, dr);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenItemWeftLookup((int)dr.ProductId, dr);
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        if (dr.ProductId == null)
                        {
                            OpenItemWeftLookup(0, dr);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenItemWeftLookup((int)dr.ProductId, dr);
                        }
                        e.Handled = true;
                    }
                }
                else if (WeftgridView.FocusedColumn.FieldName == "ColorName")
                {
                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.ColorId == null)
                        {
                            OpenColorWeftLookup(0, dr);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenColorWeftLookup((int)dr.ColorId, dr);
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        if (dr.ColorId == null)
                        {
                            OpenColorWeftLookup(0, dr);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenColorWeftLookup((int)dr.ColorId, dr);
                        }
                        e.Handled = true;
                    }
                }
                else if (WeftgridView.FocusedColumn.FieldName == "AccName")
                {
                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.AccId == null)
                        {
                            OpenAccWeftLookup(0, dr);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenAccWeftLookup((int)dr.AccId, dr);
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        if (dr.AccId == null)
                        {
                            OpenAccWeftLookup(0, dr);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenAccWeftLookup((int)dr.AccId, dr);
                        }
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Color Matching Weft GridControl KeyDown");
                MessageBoxAdv.Show(this, "Error Lookup Setup !!", "Exception ", ex.ToString());
            }
        }

        private void WeftgridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.Appearance.ForeColor = Color.FromArgb(227, 22, 91);
            if (e.RowHandle < 0)
                e.Info.DisplayText = "";
            else
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void WeftgridView_DoubleClick(object sender, EventArgs e)
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
        private void WeftgridControl_Enter(object sender, EventArgs e)
        {
            WeftgridView.FocusedColumn = WeftgridView.VisibleColumns[2];
        }
        private void WeftgridView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var itm = WeftgridView.GetFocusedRow() as WeftItemDto;
            if (itm == null) return;
            if (!"VoucherNo,Extra1".Contains(WeftgridView.FocusedColumn.FieldName)) return;
        }
        private void WeftgridView_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }
        private void WeftgridView_MouseUp(object sender, MouseEventArgs e)
        {
            DevExpress.Utils.DXMouseEventArgs args = (e as DevExpress.Utils.DXMouseEventArgs);
            GridView view = sender as GridView;
            GridHitInfo hi = view.CalcHitInfo(args.Location);
            if (hi.InColumn)
            {
                GridViewInfo ViewInfo = view.GetViewInfo() as GridViewInfo;

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
        private void WeftgridView_KeyDown(object sender, KeyEventArgs e)
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
                _DelWeft.Add(row);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                GridView view = sender as GridView;
                if (WeftgridView.FocusedColumn.FieldName == "ColorName")
                {
                    view.SetRowCellValue(view.FocusedRowHandle, colColorName, string.Empty);
                    view.SetRowCellValue(view.FocusedRowHandle, colColorId, 0);
                }
                else if (WeftgridView.FocusedColumn.FieldName == "AccName")
                {
                    view.SetRowCellValue(view.FocusedRowHandle, colAccName, string.Empty);
                    view.SetRowCellValue(view.FocusedRowHandle, colAccId, 0);
                }
            }
        }

        private void WeftgridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(ProductLookup.SelectedText)) return;
            if (string.IsNullOrEmpty(colorLookup1.SelectedText)) return;
            if (e.Column == null) return;
            var er = WeftgridView.GetRow(e.RowHandle) as WeftItemDto;
            if (er == null) return;

            er.ItemId = Convert.ToInt32(ProductLookup.SelectedValue);
            er.MColorId = Convert.ToInt32(colorLookup1.SelectedValue);
            decimal Qty;
            decimal denier = er.Denier != null ? (decimal)er.Denier : 0;
            decimal Epi = er.PI != null ? (decimal)er.PI : 0;
            decimal RS = er.RS != null ? (decimal)er.RS : 0;
            decimal Mtr = er.Mtr != null ? (decimal)er.Mtr : 0;
            decimal Ends = er.Ends != null ? (decimal)er.Ends : 0;
            decimal Change = er.Change != null ? (decimal)er.Change : 0;
            decimal q = 0;

            if (ModelMtr == 0)
            {
                if (er.Card != 0)
                {
                    q = (decimal)er.Card / (decimal)PickOnLoomsspinEdit.Value;
                    ModelMtr = q;
                    var r = (decimal)TotalPickspinEdit.Value / q;
                    LengthInInchSpintEdit.Value = q;
                    q = q / (decimal)39.37;
                    LengthinMeterspinEdit.Value = q;
                    AveragePickspinEdit.Value = r;
                }
            }

            if (e.Column.FieldName == "Denier")
            {
                denier = er.Denier != null ? (decimal)er.Denier : 0;
            }
            if (Epi > 0 && RS > 0 && TotalPickspinEdit.Value == 0 && er.Card == 0)
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
                    Qty = (denier * Epi * RS) / 90000;
                    //  Qty = Qty + Qty * Change / 100;
                }
                else
                {
                    Qty = ((((denier * Epi * RS) / 9000) + (((denier * Epi * RS) / 9000) * 10 / 100)) * 100) / 1000;
                    //  Qty = Qty + Qty * Change / 100;
                }

                (WeftgridView.Columns["Costing"] as GridColumn).Visible = false;
                (WeftgridView.Columns["Tar"] as GridColumn).Visible = false;
            }
            else if (er.Card != 0 && PickOnLoomsspinEdit.Value != 0 && TotalPickspinEdit.Value != 0 && er.Tar != 0 && er.Denier != null)
            {

                //  var i = er.Totcard / vm.Model.TotPick;
                er.Ends = er.Card / ModelMtr;

                //    var i = er.Totcard / vm.Model.TotPick;

                er.PI = er.Ends * er.Tar;
                er.Qty = er.PI * ModelMtr * PannaspinEdit.Value * er.Denier / 354330000;
                Qty = (decimal)er.Qty;
                Qty = Qty + Qty * Change / 100;
                er.Costing = Qty * er.Rate;
                er.RS = PannaspinEdit.Value;
            }
            else
            {
                Qty = 0;
            }

            er.Qty = Qty;

            //var total = vm.Trans.Where(k => k.IsDeleted == false).Sum(k => k.Qty);
            //var wefts = WeftbindingSource.DataSource as List<WeftItemDto>;
            //var total = wefts.Sum(k => k.Qty);
            var warps = WarpbindingSource.DataSource as List<WeftItemDto>;
            var wefts = WeftbindingSource.DataSource as List<WeftItemDto>;

            var qty = wefts.Sum(k => k.Qty);
            if (warps.Sum(k => k.Qty) > 0)
                qty = qty + warps.Sum(k => k.Qty);
            QualityWeightspinEdit.Value = qty != null ? (decimal)qty : 0;

            var costing = wefts.Sum(k => k.Costing);
            if (warps.Sum(k => k.Costing) > 0)
                costing = costing + warps.Sum(k => k.Costing);

            YarnCostspinEdit.Value = costing;
            CostWithoutWastagespinEdit1.Value = costing;
        }


        #endregion
        #region WarpGrid
        private void ProdgridView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var itm = WarpgridView.GetFocusedRow() as WeftItemDto;
            if (itm == null) return;
            if (!"VoucherNo,Extra1".Contains(WarpgridView.FocusedColumn.FieldName)) return;
        }
        private void ProdgridView_MouseUp(object sender, MouseEventArgs e)
        {
            DevExpress.Utils.DXMouseEventArgs args = (e as DevExpress.Utils.DXMouseEventArgs);
            GridView view = sender as GridView;
            GridHitInfo hi = view.CalcHitInfo(args.Location);
            if (hi.InColumn)
            {
                GridViewInfo ViewInfo = view.GetViewInfo() as GridViewInfo;

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
        private void ProdgridView_DoubleClick(object sender, EventArgs e)
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
        private void ProdgridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.Appearance.ForeColor = Color.FromArgb(227, 22, 91);
            if (e.RowHandle < 0)
                e.Info.DisplayText = "";
            else
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        private void ProdGridControl_Enter(object sender, EventArgs e)
        {
            WarpgridView.FocusedColumn = WarpgridView.VisibleColumns[2];
        }
        private void ProdgridView_KeyDown(object sender, KeyEventArgs e)
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
                _DelWarp.Add(row);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                GridView view = sender as GridView;
                if (WarpgridView.FocusedColumn.FieldName == "ColorName")
                {
                    view.SetRowCellValue(view.FocusedRowHandle, colColorName1, string.Empty);
                    view.SetRowCellValue(view.FocusedRowHandle, colColorId1, 0);
                }
                else if (WarpgridView.FocusedColumn.FieldName == "AccName")
                {
                    view.SetRowCellValue(view.FocusedRowHandle, colAccName1, string.Empty);
                    view.SetRowCellValue(view.FocusedRowHandle, colAccId1, 0);
                }
            }
        }
        private void ProdgridView_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var rw = WarpgridView.GetRow(e.RowHandle) as WeftItemDto;
            rw.Id = -1 * WarpgridView.RowCount;

            if (string.IsNullOrEmpty(ProductLookup.SelectedText))
            {
                MessageBoxAdv.Show("Select Product!!!");
                return;
            }
            if (string.IsNullOrEmpty(colorLookup1.SelectedText))
            {
                MessageBoxAdv.Show("Select Color!!!");
                return;
            }
            rw.ItemId = Convert.ToInt32(ProductLookup.SelectedValue);
            rw.ProductId = Convert.ToInt32(ProductLookup.SelectedValue);
            rw.ProductName = ProductLookup.SelectedText;

            rw.ColorId = Convert.ToInt32(colorLookup1.SelectedValue);
            rw.ColorName = colorLookup1.SelectedText;

        }
        private void ProdgridView_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }
        private void OrderDetailgridControl_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {

                var dr = PreOpenLookup();
                if (dr == null) return;
                if (WarpgridView.FocusedColumn.FieldName == "ProductName")
                {

                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.ProductId == null)
                        {
                            OpenItemLookup(0, dr);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenItemLookup((int)dr.ProductId, dr);
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        if (dr.ProductId == null)
                        {
                            OpenItemLookup(0, dr);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenItemLookup((int)dr.ProductId, dr);
                        }
                        e.Handled = true;
                    }
                }
                else if (WarpgridView.FocusedColumn.FieldName == "ColorName")
                {
                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.ColorId == null)
                        {
                            OpenColorLookup(0, dr);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenColorLookup((int)dr.ColorId, dr);
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        if (dr.ColorId == null)
                        {
                            OpenColorLookup(0, dr);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenColorLookup((int)dr.ColorId, dr);
                        }
                        e.Handled = true;
                    }
                }
                else if (WarpgridView.FocusedColumn.FieldName == "AccName")
                {
                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.AccId == null)
                        {
                            OpenAccLookup(0, dr);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenAccLookup((int)dr.AccId, dr);
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        if (dr.AccId == null)
                        {
                            OpenAccLookup(0, dr);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenAccLookup((int)dr.AccId, dr);
                        }
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Color Matching Warp Detail GridControl KeyDown");
                MessageBoxAdv.Show(this, "Error Lookup Setup !!", "Exception ", ex.ToString());
            }
        }

        private void WarpgridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(ProductLookup.SelectedText)) return;
            if (string.IsNullOrEmpty(colorLookup1.SelectedText)) return;
            if (e.Column == null) return;
            var err = WarpgridView.GetRow(e.RowHandle) as WeftItemDto;
            if (err == null) return;

            err.ItemId = Convert.ToInt32(ProductLookup.SelectedValue);
            err.ProductId = Convert.ToInt32(ProductLookup.SelectedValue);
            err.ProductName = ProductLookup.SelectedText;
            err.MColorId = Convert.ToInt32(colorLookup1.SelectedValue);
            err.ColorName = colorLookup1.SelectedText;

            decimal Qty;
            decimal denier = err.Denier != null ? (decimal)err.Denier : 0;
            decimal Epi = err.PI != null ? (decimal)err.PI : 0;
            decimal RS = err.RS != null ? (decimal)err.RS : 0;
            decimal Mtr = err.Mtr != null ? (decimal)err.Mtr : 0;
            decimal Ends = err.Ends != null ? (decimal)err.Ends : 0;
            decimal Tar = err.Tar;

            if (e.Column.FieldName == "Denier")
            {
                denier = err.Denier != null ? (decimal)err.Denier : 0;
            }
            if (Tar == 0 && (Epi > 0 || RS > 0))
            {
                if (e.Column.FieldName == "PI")
                {
                    Epi = err.PI != null ? (decimal)err.PI : 0;
                }
                if (e.Column.FieldName == "RS")
                {
                    RS = err.RS != null ? (decimal)err.RS : 0;
                }
                if (ProductPara.RS_In_Inch)
                {
                    Qty = (denier * Epi * RS) / 90000;
                }
                else
                {
                    Qty = ((((denier * Epi * RS) / 9000) + (((denier * Epi * RS) / 9000) * 10 / 100)) * 100) / 1000;
                }
            }
            else if (PickOnLoomsspinEdit.Value != 0 && TotalPickspinEdit.Value != 0 && err.Tar != 0 && err.Denier != null)
            {
                err.RS = PannaspinEdit.Value;
                err.PI = PickOnLoomsspinEdit.Value;
                err.Qty = ModelMtr * err.Tar * err.Denier / 354330000;
                Qty = (decimal)err.Qty;
                err.Costing = Qty * err.Rate;

                var warps = WarpbindingSource.DataSource as List<WeftItemDto>;
                var wefts = WeftbindingSource.DataSource as List<WeftItemDto>;

                var qty = wefts.Sum(k => k.Qty);
                qty = qty + warps.Sum(k => k.Qty);
                QualityWeightspinEdit.Value = qty != null ? (decimal)qty : 0;

                var costing = wefts.Sum(k => k.Costing);

                costing = costing + warps.Sum(k => k.Costing);
                YarnCostspinEdit.Value = costing;
                CostWithoutWastagespinEdit1.Value = costing;
                //  Qty = 0;
            }
            else
            {
                Qty = 0;
            }
            err.Qty = Qty;
        }

        #endregion

        #region Event

        private void JobChargeSpinEdit_ValueChanged(object sender, EventArgs e)
        {
            UpdateFooter();
        }
        private void NetWeightSpinEdit_ValueChanged(object sender, EventArgs e)
        {
            UpdateFooter();
        }
        private void WastageInPerspinEdit_ValueChanged(object sender, EventArgs e)
        {
            UpdateFooter();
        }
        private void ColorMIndex_Load(object sender, EventArgs e)
        {

        }
        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                voucherDateEdit.Focus();
                return;
            }
            else if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new ColorMListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Color Matching [View]";
            }
        }
        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveDataAsync(true);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Color Matching Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private void ColorRepositoryItemButtonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
            {
                if (dr.ColorId != null && dr.ColorId > 0)
                    OpenColorLookup((int)dr.ColorId, dr);
                else
                    OpenColorLookup(0, dr);
            }
        }

        private void ProductRepositoryItemButtonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
            {
                if (dr.ProductId != null && dr.ProductId > 0)
                    OpenItemLookup((int)dr.ProductId, dr);
                else
                    OpenItemLookup(0, dr);
            }
        }

        private void ColorrepositoryItemButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var dr = PreOpenWeftLookup();
            if (dr != null)
            {
                if (dr.ColorId != null && dr.ColorId > 0)
                    OpenColorWeftLookup((int)dr.ColorId, dr);
                else
                    OpenColorWeftLookup(0, dr);
            }
        }

        private void ItemrepositoryItemButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var dr = PreOpenWeftLookup();
            if (dr != null)
            {
                if (dr.ProductId != null && dr.ProductId > 0)
                    OpenItemWeftLookup((int)dr.ProductId, dr);
                else
                    OpenItemWeftLookup(0, dr);
            }
        }

        private void ProductRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenWeftLookup();
            if (dr.ProductId != null)
                OpenItemLookup((int)dr.ProductId, dr);
            else
                OpenItemLookup(0, dr);
        }

        private void ColorrepositoryItemButtonEdit2_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
            {
                if (dr.ColorId != null && dr.ColorId > 0)
                    OpenColorLookup((int)dr.ColorId, dr);
                else
                    OpenColorLookup(0, dr);
            }
        }
        private void ProductrepositoryItemButtonEdit2_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
            {
                if (dr.ProductId != null)
                    OpenItemWeftLookup((int)dr.ProductId, dr);
                else
                    OpenItemWeftLookup(0, dr);
            }
        }

        private void AccrepositoryItemButtonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
            {
                if (dr.AccId != null)
                    OpenAccLookup((int)dr.AccId, dr);
                else
                    OpenAccLookup(0, dr);
            }
        }

        private void AccrepositoryItemButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var dr = PreOpenWeftLookup();
            if (dr != null)
            {
                if (dr.AccId != null)
                    OpenAccWeftLookup((int)dr.AccId, dr);
                else
                    OpenAccWeftLookup(0, dr);
            }
        }

        #endregion
        #region Parent Function

        public override void Print()
        {
            base.Print();
        }
        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<WeftItemModel>();
            this.Text = "Color Matching [Add New]";

            voucherDateEdit.EditValue = DateTime.Now;

            colorLookup1.SetEmpty();
            ProductLookup.SetEmpty();

            TotalPickspinEdit.Value = 0;
            PickOnLoomsspinEdit.Value = 0;
            PannaspinEdit.Value = 0;
            LengthInInchSpintEdit.Value = 0;
            LengthinMeterspinEdit.Value = 0;
            QualityWeightspinEdit.Value = 0;
            YarnCostspinEdit.Value = 0;
            AveragePickspinEdit.Value = 0;
            WastespinEdit.Value = 0;
            NetWeightSpinEdit.Value = 0;
            JobChargeSpinEdit.Value = 0;
            CostWithWastagespinEdit.Value = 0;
            OneMeterCostspinEdit.Value = 0;
            WastespinEdit.Value = 0;
            WastePerspinEdit.Value = 0;
            WastageInPerspinEdit.Value = 0;
            JobChargeOneSareespinEdit.Value = 0;
            CostWithoutWastagespinEdit1.Value = 0;

            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;

            WarpbindingSource.DataSource = new List<WeftItemDto>();
            WeftbindingSource.DataSource = new List<WeftItemDto>();

            _DelWarp = new List<WeftItemDto>();
            _DelWeft = new List<WeftItemDto>();

            ModelMtr = 0;
            voucherDateEdit.Focus();
        }

        public override void ResetPage()
        {
            base.ResetPage();

            voucherDateEdit.EditValue = DateTime.Now;

            colorLookup1.SetEmpty();
            ProductLookup.SetEmpty();

            TotalPickspinEdit.Value = 0;
            PickOnLoomsspinEdit.Value = 0;
            PannaspinEdit.Value = 0;
            LengthInInchSpintEdit.Value = 0;
            LengthinMeterspinEdit.Value = 0;
            QualityWeightspinEdit.Value = 0;
            YarnCostspinEdit.Value = 0;
            AveragePickspinEdit.Value = 0;
            WastespinEdit.Value = 0;
            NetWeightSpinEdit.Value = 0;
            JobChargeSpinEdit.Value = 0;
            CostWithWastagespinEdit.Value = 0;
            OneMeterCostspinEdit.Value = 0;
            WastePerspinEdit.Value = 0;
            WastageInPerspinEdit.Value = 0;
            JobChargeOneSareespinEdit.Value = 0;
            CostWithoutWastagespinEdit1.Value = 0;
            WarpbindingSource.DataSource = new List<WeftItemDto>();
            WeftbindingSource.DataSource = new List<WeftItemDto>();

            _DelWeft = new List<WeftItemDto>();
            _DelWarp = new List<WeftItemDto>();

            ModelMtr = 0;
            voucherDateEdit.Focus();
        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var pdata = db.WeftItems.Find(_key);

                LoadData(pdata);
                createdLabelControl.Text = "Create By: " + pdata.CreateUser;
            }

            this.Text = "Color Matching [Edit New]";

            modifyLabelControl.Text = "Modified By: " + KontoGlobals.UserName;
        }

        public override void FirstRec()
        {
            base.FirstRec();
            var model = FilterView[RecordNo];
            LoadData(model);
        }
        public override void NextRec()
        {
            base.NextRec();
            LoadData(FilterView[this.RecordNo]);
        }
        public override void PrevRec()
        {
            base.PrevRec();
            LoadData(FilterView[this.RecordNo]);
        }
        public override void LastRec()
        {
            base.LastRec();
            LoadData(FilterView[this.RecordNo]);
        }

        public override void FindRec()
        {
            List<Filter> filter = new List<Filter>();

            if (Convert.ToInt32(ProductLookup.SelectedValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "ProductId", Operation = Op.Equals, Value = Convert.ToInt32(ProductLookup.SelectedValue) });
            }

            //filter.Add(new Filter { PropertyName = "CompanyId", Operation = Op.Equals, Value = KontoGlobals.CompanyId });
            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });
            filter.Add(new Filter { PropertyName = "TypeId", Operation = Op.GreaterThan, Value = 100});

            using (var db = new KontoContext())
            {
                FilterView = db.WeftItems.Where(ExpressionBuilder.GetExpression<WeftItemModel>(filter))
                    .OrderBy(x => x.Id).ToList();

                if (FilterView.Count == 0)
                {
                    MessageBoxAdv.Show(this, "No Record Found", "Find !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ResetPage();
                    return;
                }
                this.TotalRecord = FilterView.Count;
                this.RecordNo = 0;
                LoadData(this.FilterView[0]);
            }
        }

        public override void SaveDataAsync(bool newmode)
        {
            bool IsSaved = false;
            if (!ValidateData()) return;

            WeftItemModel _find = new WeftItemModel();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<WeftItemDto, WeftItemModel>().ForMember(x => x.Id, p => p.Ignore());
            });

            var warp = WarpbindingSource.DataSource as List<WeftItemDto>;
            var weft = WeftbindingSource.DataSource as List<WeftItemDto>;

            using (var db = new KontoContext())
            {
                using (var _tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        string createuser = KontoGlobals.UserName;
                        DateTime createdate = DateTime.Now;

                        if (this.PrimaryKey != 0)
                        {
                            _find = db.WeftItems.Find(this.PrimaryKey);
                            createuser = _find.CreateUser;
                            createdate = Convert.ToDateTime(_find.CreateDate);
                        }

                        _find.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));

                        _find.ProductId = Convert.ToInt32(ProductLookup.SelectedValue);
                        _find.ItemId = Convert.ToInt32(ProductLookup.SelectedValue);

                        if (!string.IsNullOrEmpty(colorLookup1.SelectedText))
                            _find.MColorId = Convert.ToInt32(colorLookup1.SelectedValue);

                        _find.Totcard = TotalPickspinEdit.Value;
                        _find.TotPick = PickOnLoomsspinEdit.Value;
                        if (PannaspinEdit.Value > 0)
                            _find.Panno = Convert.ToInt32(PannaspinEdit.Value);

                        _find.Wasteper = WastePerspinEdit.Value;
                        _find.NWeight = NetWeightSpinEdit.Value;
                        _find.JobCharge = JobChargeSpinEdit.Value;
                        //_find.CostWWaste = CostWithWastagespinEdit.Value;
                        //_find.OneMtrCost = OneMeterCostspinEdit.Value;
                        //_find.Wastage = WastespinEdit.Value;
                        //_find.Wastageper = WastageInPerspinEdit.Value;
                        //_find.JobCharges = JobChargeOneSareespinEdit.Value;
                        //_find.CostNWaste = CostWithoutWastagespinEdit1.Value;
                        //_find.LengthInCm = LengthInInchSpintEdit.Value;
                        //_find.LengthInMtr = LengthinMeterspinEdit.Value;
                        //_find.QWeight = QualityWeightspinEdit.Value;
                        //_find.YCost = YarnCostspinEdit.Value;
                        //_find.AvPick = AveragePickspinEdit.Value;

                        //if (this.PrimaryKey == 0)
                        //{

                        //    db.WeftItems.Add(_find);
                        //    db.SaveChanges();
                        //}
                        //else
                        //{
                        //    _find.CreateDate = createdate;
                        //    _find.CreateUser = createuser;
                        //}

                        //weft Detail 
                        var pid = 101;
                        var pd = db.WeftItems.Where(k => k.TypeId > 100).ToList();
                        if (pd.Count > 0)
                        {
                            pid = pd.Max(k => k.TypeId) + 1;
                        }

                        var map = new Mapper(config);
                        WeftItemModel tranModel;
                        foreach (var item in weft)
                        {
                            var transid = item.Id;
                            // item.RefId = _find.Id;

                            item.Totcard = _find.Totcard;
                            item.TotPick = _find.TotPick;
                            item.Panno = _find.Panno;

                            item.Wasteper = _find.Wasteper;
                            item.NWeight = _find.NWeight;
                            item.JobCharge = _find.JobCharge;
                            item.Mtr = ModelMtr;
                            //item.CostWWaste = _find.CostWWaste;
                            //item.OneMtrCost = _find.OneMtrCost;
                            //item.Wastage = _find.Wastage;
                            //item.Wastageper = _find.Wastageper;
                            //item.JobCharges = _find.JobCharges;
                            //item.CostNWaste = _find.CostNWaste;
                            //item.LengthInCm = _find.LengthInCm;
                            //item.LengthInMtr = _find.LengthInMtr;
                            //item.QWeight = _find.QWeight;
                            //item.YCost = _find.YCost;
                            //item.AvPick = _find.AvPick;
                            item.IType = "T";
                            item.ItemId = _find.ItemId;
                            item.ProductId = _find.ProductId;
                            item.MColorId = _find.MColorId;
                            item.VoucherDate = _find.VoucherDate;

                            tranModel = new WeftItemModel();
                            if (item.Id > 0)
                            {
                                tranModel = db.WeftItems.Find(item.Id);
                                pid = tranModel.TypeId;
                            }
                            map = new Mapper(config);
                            map.Map(item, tranModel);

                            if (item.Id <= 0)
                            {
                                tranModel.TypeId = pid;
                                tranModel.IType = "T";
                                db.WeftItems.Add(tranModel);
                                db.SaveChanges();
                            }
                        }

                        foreach (var item in warp)
                        {
                            var transid = item.Id;
                            // item.RefId = _find.Id;

                            item.Totcard = _find.Totcard;
                            item.TotPick = _find.TotPick;
                            item.Panno = _find.Panno;

                            item.Wasteper = _find.Wasteper;
                            item.NWeight = _find.NWeight;
                            item.JobCharge = _find.JobCharge;
                            item.Mtr = ModelMtr;
                            //item.CostWWaste = _find.CostWWaste;
                            //item.OneMtrCost = _find.OneMtrCost;
                            //item.Wastage = _find.Wastage;
                            //item.Wastageper = _find.Wastageper;
                            //item.JobCharges = _find.JobCharges;
                            //item.CostNWaste = _find.CostNWaste;
                            //item.LengthInCm = _find.LengthInCm;
                            //item.LengthInMtr = _find.LengthInMtr;
                            //item.QWeight = _find.QWeight;
                            //item.YCost = _find.YCost;
                            //item.AvPick = _find.AvPick;

                            item.ItemId = _find.ItemId;
                            item.MColorId = _find.MColorId;
                            item.VoucherDate = _find.VoucherDate;
                            item.IType = "P";
                            tranModel = new WeftItemModel();
                            if (item.Id > 0)
                            {
                                tranModel = db.WeftItems.Find(item.Id);
                                pid = tranModel.TypeId;
                            }
                            map = new Mapper(config);
                            map.Map(item, tranModel);

                            if (item.Id <= 0)
                            {
                                tranModel.TypeId = pid;
                                tranModel.IType = "P";

                                db.WeftItems.Add(tranModel);
                                db.SaveChanges();
                            }
                        }

                        //DELETED ENTRY FROM DATABASE WARP

                        foreach (var p in _DelWarp)
                        {
                            var pro = db.WeftItems.FirstOrDefault(k => k.Id == p.Id);
                            if (pro != null)
                                pro.IsDeleted = true;
                        }
                        //DELETED ENTRY FROM DATABASE weft
                        foreach (var p in _DelWeft)
                        {
                            var pro = db.WeftItems.FirstOrDefault(k => k.Id == p.Id);
                            if (pro != null)
                                pro.IsDeleted = true;
                        }

                        db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Color Matching Save");
                        MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
                    }
                }
            }

            if (IsSaved)
            {
                // NewRec();

                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (!this.OpenForLookup && newmode)
                {
                    //if (this.voucherLookup.GroupDto.PrintAfterSave && MessageBox.Show("Print JobCard ?", "Print", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    //{
                    //    this.PrimaryKey = _find.Id;
                    //    Print();
                    //    this.PrimaryKey = 0;
                    //}

                    base.SaveDataAsync(newmode);
                    this.ResetPage();
                    this.NewRec();
                    voucherDateEdit.Focus();
                }
                else
                {
                    this.Close();
                    this.Dispose();
                }
            }
        }

        private bool ValidateData()
        {
            var dt = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));

            var warp = WarpbindingSource.DataSource as List<WeftItemDto>;
            var weft = WeftbindingSource.DataSource as List<WeftItemDto>;

            if (dt > KontoGlobals.ToDate || dt < KontoGlobals.FromDate)
            {
                MessageBoxAdv.Show(this, "Date out of financial range", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherDateEdit.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(ProductLookup.SelectedText))
            {
                MessageBoxAdv.Show(this, "Invalid Product", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ProductLookup.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(colorLookup1.SelectedText))
            {
                MessageBoxAdv.Show(this, "Invalid Color", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                colorLookup1.Focus();
                return false;
            }
            else if (weft.Count <= 0)
            {
                MessageBoxAdv.Show(this, "Atleast one transaction must be entered!", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                WeftgridView.Focus();
                return false;
            }
            else if (warp.Count <= 0)
            {
                MessageBoxAdv.Show(this, "Atleast one Warp Product transaction must be entered!", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                WarpgridView.Focus();
                return false;
            }

            return true;
        }

        private void LoadData(WeftItemModel model)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<WeftItemModel, WeftItemDto>();
            });
            var pdata = new WeftItemDto();
            var mapper = new Mapper(config);
            mapper.Map(model, pdata);

            voucherDateEdit.DateTime = KontoUtils.IToD((int)pdata.VoucherDate);

            if (pdata.ProductId > 0)
            {
                ProductLookup.SelectedValue = pdata.ProductId;
                ProductLookup.SetGroup((int)pdata.ProductId);
            }

            colorLookup1.SelectedValue = pdata.MColorId;
            colorLookup1.SetGroup();
            KontoContext db = new KontoContext();

            var _weftTrans = (from wi in db.WeftItems
                              join pd in db.Products on wi.ProductId equals pd.Id into join_pd
                              from pd in join_pd.DefaultIfEmpty()
                              join ac in db.Accs on wi.AccId equals ac.Id into join_ac
                              from ac in join_ac.DefaultIfEmpty()
                              join co in db.ColorModels on wi.ColorId equals co.Id into join_co
                              from co in join_co.DefaultIfEmpty()
                                  //join mco in db.ColorModels on wi.MColorId equals mco.Id into join_mco
                                  //from mco in join_mco.DefaultIfEmpty()
                                  //join p in db.Products on wi.ItemId equals p.Id into join_p
                                  //from p in join_p.DefaultIfEmpty()
                              orderby wi.Id
                              where !wi.IsDeleted && wi.TypeId == pdata.TypeId && wi.TypeId >= 99 && wi.IType == "T"
                              select new WeftItemDto
                              {
                                  Totcard = wi.Totcard,
                                  TotPick = wi.TotPick,
                                  Card = wi.Card,
                                  Tar = wi.Tar,
                                  Panno = wi.Panno,
                                  Wasteper = wi.Wasteper,
                                  NWeight = wi.NWeight,
                                  JobCharge = wi.JobCharge,
                                  Costing = wi.Costing,
                                  Weight = wi.Weight,
                                  Rate = wi.Rate,
                                  Denier = wi.Denier,
                                  Extra1 = wi.Extra1,
                                  Extra2 = wi.Extra2,
                                  Id = wi.Id,
                                  PI = wi.PI,
                                  ProductId = wi.ProductId,
                                  ProductName = pd.ProductName,
                                  //   ItemName = p.ProductName,
                                  ItemId = wi.ItemId,
                                  Qty = wi.Qty,
                                  RefId = wi.RefId,
                                  Remark = wi.Remark,
                                  RS = wi.RS,
                                  TypeId = wi.TypeId,
                                  AccId = wi.AccId,
                                  Ends = wi.Ends,
                                  Mtr = wi.Mtr,
                                  ColorId = wi.ColorId,
                                  //    MainColor = mco.ColorName,
                                  MColorId = wi.MColorId,
                                  AccName = ac.AccName,
                                  ColorName = co.ColorName,
                                  Change = wi.Change
                              }
              ).ToList();

            //var itrans1 = db.jobCardTrans.Where(k => k.JobCardId == pdata.Id && k.RefId < 1
            //            && k.IsDeleted == false).ProjectToList<JobCardTransDto>(config);

            var _warptrans = (from wi in db.WeftItems
                              join pd in db.Products on wi.ProductId equals pd.Id into join_pd
                              from pd in join_pd.DefaultIfEmpty()
                              join ac in db.Accs on wi.AccId equals ac.Id into join_ac
                              from ac in join_ac.DefaultIfEmpty()
                              join co in db.ColorModels on wi.ColorId equals co.Id into join_co
                              from co in join_co.DefaultIfEmpty()
                                  //join mco in db.ColorModels on wi.MColorId equals mco.Id into join_mco
                                  //from mco in join_mco.DefaultIfEmpty()
                                  //join p in db.Products on wi.ItemId equals p.Id into join_p
                                  //from p in join_p.DefaultIfEmpty()
                              orderby wi.Id
                              where !wi.IsDeleted && wi.TypeId == pdata.TypeId && wi.TypeId >= 99
                              && wi.IType == "P"
                              select new WeftItemDto
                              {
                                  Totcard = wi.Totcard,
                                  TotPick = wi.TotPick,
                                  Card = wi.Card,
                                  Tar = wi.Tar,
                                  Panno = wi.Panno,
                                  Wasteper = wi.Wasteper,
                                  NWeight = wi.NWeight,
                                  JobCharge = wi.JobCharge,
                                  Costing = wi.Costing,
                                  Weight = wi.Weight,
                                  Rate = wi.Rate,
                                  Denier = wi.Denier,
                                  Extra1 = wi.Extra1,
                                  Extra2 = wi.Extra2,
                                  Id = wi.Id,
                                  PI = wi.PI,
                                  ProductId = wi.ProductId,
                                  ProductName = pd.ProductName,
                                  //     ItemName = p.ProductName,
                                  ItemId = wi.ItemId,
                                  Qty = wi.Qty,
                                  RefId = wi.RefId,
                                  Remark = wi.Remark,
                                  RS = wi.RS,
                                  TypeId = wi.TypeId,
                                  AccId = wi.AccId,
                                  Ends = wi.Ends,
                                  Mtr = wi.Mtr,
                                  ColorId = wi.ColorId,
                                  //  MainColor = mco.ColorName,
                                  MColorId = wi.MColorId,
                                  AccName = ac.AccName,
                                  ColorName = co.ColorName,
                                  Change = wi.Change
                              }
             ).ToList();

            WarpbindingSource.DataSource = _warptrans;
            WeftbindingSource.DataSource = _weftTrans;

            pdata = _weftTrans.FirstOrDefault();

            //Update footer
            if (pdata.Totcard > 0 && pdata.TotPick > 0 && pdata.Panno > 0)
            {
                pdata.LengthInCm = pdata.Card / pdata.TotPick;
                pdata.LengthInMtr = pdata.LengthInCm / (decimal)39.37;
                var totw = _weftTrans.Sum(k => k.Qty);
                var totw1 = _warptrans.Sum(k => k.Qty);

                pdata.QWeight = totw + totw1;

                var totc = _weftTrans.Sum(k => k.Costing);
                var totc1 = _warptrans.Sum(k => k.Costing);

                pdata.YCost = totc + totc1;
                pdata.CostNWaste = pdata.YCost;

                if (pdata.LengthInCm > 0 && pdata.Totcard > 0)
                    pdata.AvPick = pdata.Totcard / pdata.LengthInCm;

                pdata.JobCharges = pdata.JobCharge * pdata.AvPick * pdata.LengthInMtr;
                pdata.Wastage = pdata.QWeight - pdata.NWeight;

                if (pdata.QWeight > 0)
                    pdata.Wastageper = (pdata.NWeight * 100 / pdata.QWeight) - 100;
                pdata.CostNWaste = pdata.YCost + pdata.JobCharges;
                pdata.CostWWaste = (pdata.CostNWaste * pdata.Wasteper / 100) + pdata.CostNWaste;
                pdata.OneMtrCost = pdata.CostWWaste / pdata.LengthInMtr;

                TotalPickspinEdit.Value = pdata.Totcard;
                PickOnLoomsspinEdit.Value = pdata.TotPick;
                if (pdata.Panno > 0)
                    PannaspinEdit.Value = pdata.Panno;

                WastespinEdit.Value = pdata.Wasteper;
                NetWeightSpinEdit.Value = pdata.NWeight;
                JobChargeSpinEdit.Value = pdata.JobCharge;

                CostWithWastagespinEdit.Value = pdata.CostWWaste != null ? (decimal)pdata.CostWWaste : 0;
                OneMeterCostspinEdit.Value = pdata.OneMtrCost != null ? (decimal)pdata.OneMtrCost : 0;
                WastespinEdit.Value = pdata.Wastage != null ? (decimal)pdata.Wastage : 0;
                WastageInPerspinEdit.Value = pdata.Wastageper != null ? (decimal)pdata.Wastageper : 0;
                JobChargeOneSareespinEdit.Value = pdata.JobCharges != null ? (decimal)pdata.JobCharges : 0;
                CostWithoutWastagespinEdit1.Value = pdata.CostNWaste != null ? (decimal)pdata.CostNWaste : 0;
                LengthInInchSpintEdit.Value = pdata.LengthInCm != null ? (decimal)pdata.LengthInCm : 0;
                LengthinMeterspinEdit.Value = pdata.LengthInMtr != null ? (decimal)pdata.LengthInMtr : 0;
                QualityWeightspinEdit.Value = pdata.QWeight != null ? (decimal)pdata.QWeight : 0;
                YarnCostspinEdit.Value = pdata.YCost != null ? (decimal)pdata.YCost : 0;
                AveragePickspinEdit.Value = pdata.AvPick != null ? (decimal)pdata.AvPick : 0;
            }

            _DelWarp = new List<WeftItemDto>();
            _DelWeft = new List<WeftItemDto>();
        }

        #endregion
    }
}