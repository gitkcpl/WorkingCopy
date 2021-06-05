﻿using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Libs;
using Konto.Data.Models.Transaction.Dtos;
using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraGrid;

namespace Konto.Trading.OutJobChallan
{
    public partial class OJCTakaDetails : KontoForm
    {
        public string GridLayoutFileName { get; set; }
        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        public OJCTakaDetails()
        {
            InitializeComponent();
            this.gridView1.CellValueChanged += GridView1_CellValueChanged;
            this.gridView1.InitNewRow += GridView1_InitNewRow;
            this.gridView1.CustomDrawRowIndicator += GridView1_CustomDrawRowIndicator;
            this.Shown += MrTakaDetails_Shown;
            this.gridView1.MouseUp += GridView1_MouseUp;
            this.gridView1.DoubleClick += GridView1_DoubleClick;
            this.GridLayoutFileName = KontoFileLayout.Mrv_Taka_Detail;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.gridView1.ValidateRow += GridView1_ValidateRow;
            this.gridView1.InvalidRowException += GridView1_InvalidRowException;
            this.gridView1.CustomSummaryCalculate += GridView1_CustomSummaryCalculate;
            this.headerEdit.Leave += headerEdit_Leave;
           
            
            this.gridView1.KeyDown += GridView1_KeyDown;
        }

        private void GridView1_KeyDown(object sender, KeyEventArgs e)
        {
            var gv = sender as GridView;
            if (e.KeyCode != Keys.Enter ||
                gv.FocusedColumn.FieldName == "GrayMtr") return;


            gridView1.PostEditor();
            gridView1.UpdateCurrentRow();
            if (gv.FocusedColumn.FieldName == "TP1" && Convert.ToDecimal(gridView1.GetFocusedValue()) == 0)
            {
            
                e.Handled = true;
                okSimpleButton.Focus();
                return;
            }
            if (Convert.ToDecimal(gridView1.GetFocusedValue()) == 0)
            {
                if (gridView1.FocusedRowHandle == gridView1.RowCount - 2)
                {
                    e.Handled = true;
                    okSimpleButton.Focus();
                    return;
                }
                gv.FocusedColumn = colTP1;
                gv.FocusedRowHandle = gridView1.FocusedRowHandle + 1;
            }

            
        }

        
       

        private decimal _GreyMtrs;
        private int _GreyPcs, _finPcs;

        private void GridView1_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {
                _GreyMtrs = 0;
                _GreyPcs = 0;
                _finPcs = 0;
            }

            if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                var rw = e.Row as ProdOutDto;
                if (rw.FinMrt > 0)
                {
                    _GreyPcs = _GreyPcs + 1;
                    _GreyMtrs = _GreyMtrs + Convert.ToDecimal(rw.GrayMtr);
                    if (rw.TP1 > 0)
                        _finPcs = _finPcs + 1;
                    if (rw.TP2 > 0)
                        _finPcs = _finPcs + 1;
                    if (rw.TP3 > 0)
                        _finPcs = _finPcs + 1;
                    if (rw.TP4 > 0)
                        _finPcs = _finPcs + 1;
                    if (rw.TP5 > 0)
                        _finPcs = _finPcs + 1;
                }
            }

            if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                autoLabel1.Text = "Grey Used: " + _GreyMtrs.ToString("F") + ", Pcs Used: " + _GreyPcs.ToString() +
                                  ", Fin. Pcs: " + _finPcs.ToString();
            }
        }

        private void GridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.DisplayError;
        }

        private void GridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            //if (!MillRecPara.FinishMeter_more_than_GreyMeter)
            //{
            //    var dr = e.Row as ProdOutDto;
            //    if (dr == null) return;
            //    if (dr.GrayMtr < dr.FinMrt)
            //        e.Valid = false;
            //}
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
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
        private void MrTakaDetails_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = gridControl1;
            this.gridView1.FocusedColumn = colTP1;
            this.gridView1.OptionsNavigation.EnterMoveNextColumn = true;
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

        private void GridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.Appearance.ForeColor = Color.FromArgb(227, 22, 91);
            if (e.RowHandle < 0)
                e.Info.DisplayText = "";
            else
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gridView1.SetRowCellValue(e.RowHandle, "SrNo", gridView1.RowCount);
        }

        private void GridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var er = gridView1.GetRow(e.RowHandle) as ProdOutDto;
            if (er == null) return;
            er.FinMrt = er.TP1 + er.TP2 + er.TP3 + er.TP4 + er.TP5;
            er.ShMtr = er.GrayMtr - er.FinMrt;
            er.ShPer = decimal.Round(Convert.ToInt32(er.ShMtr) * 100 / Convert.ToDecimal( er.GrayMtr), 2, System.MidpointRounding.AwayFromZero);
        }
        void headerEdit_Leave(object sender, EventArgs e)
        {
            activeCol.Caption = headerEdit.Text;
            headerEdit.Hide();
        }

        private void cancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

       
    }
}
