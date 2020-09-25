using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Konto.App.Shared;
using Konto.App.Shared.Para;
using Konto.Core.Shared;
using Konto.Core.Shared.Libs;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Konto.Trading.MillReturn
{
    public partial class MrTakaDetails : KontoForm
    {
        public string GridLayoutFileName { get; set; }
        TextEdit headerEdit = new TextEdit();
        public List<ProdOutModel> DelProd { get; set; }

        GridColumn activeCol = null;
        public int AccId { get; set; }
        public int ChallanId { get; set; }
        public int ChallanTransId { get; set; }
        public int ItemId { get; set; }
        public int TransId { get; set; }


        public MrTakaDetails()
        {
            InitializeComponent();
            this.gridView1.CellValueChanged += GridView1_CellValueChanged;
            this.gridView1.InitNewRow += GridView1_InitNewRow;
            this.gridView1.CustomDrawRowIndicator += GridView1_CustomDrawRowIndicator;
            this.Shown += MrTakaDetails_Shown;
            this.gridView1.MouseUp += GridView1_MouseUp;
            this.gridView1.DoubleClick += GridView1_DoubleClick;
            this.GridLayoutFileName = KontoFileLayout.Mr_Taka_Detail;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.gridView1.ValidateRow += GridView1_ValidateRow;
            this.gridView1.InvalidRowException += GridView1_InvalidRowException;
            pendSimpleButton.Click += PendSimpleButton_Click;
            this.gridView1.KeyDown += GridView1_KeyDown;
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
                var row = view.GetRow(view.FocusedRowHandle) as ProdOutModel;
                view.DeleteRow(view.FocusedRowHandle);
                DelProd.Add(row);
            }
        }
        private void PendSimpleButton_Click(object sender, EventArgs e)
        {
            var frm = new MrStockTakaDetails();
            frm.AccId = this.AccId;
            frm.ChallanId = this.ChallanId;
            frm.ChallanTransId = this.ChallanTransId;
            frm.ItemId = this.ItemId;
            frm.ShowDialog();
            var rows = frm.gridView1.GetSelectedRows();
            var _list = this.prodOutModelBindingSource.DataSource as List<ProdOutModel>;
            foreach (var item in rows)
            {
                var prod = frm.gridView1.GetRow(item) as PendingMRProd;
               
                ProdOutModel pm = new ProdOutModel();

                pm.Id = 0;
                pm.SrNo = (int)prod.SrNo;
                pm.ProdId = (int)prod.ProdId;
                pm.VoucherNo = prod.VoucherNo;
                pm.ColorId = prod.ColorId;
                pm.CompId = prod.CompId;
                pm.GradeId = prod.GradeId;
                pm.YearId = prod.YearId;
                pm.ProductId = prod.ProductId;
                pm.GrayMtr = prod.GrayMtr;
                pm.FinMrt = prod.GrayMtr;
                pm.TP1 = prod.GrayMtr;
                pm.TransId = this.TransId;
                _list.Add(pm);
            }
            gridControl1.RefreshDataSource();
        }

        private void GridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.DisplayError;
        }

        private void GridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (!MillRecPara.FinishMeter_more_than_GreyMeter)
            {
                var dr = e.Row as ProdOutModel;
                if (dr == null) return;
                if (dr.GrayMtr < dr.FinMrt)
                    e.Valid = false;
            }
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
            var er = gridView1.GetRow(e.RowHandle) as ProdOutModel;
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

        private void MrTakaDetails_Load(object sender, EventArgs e)
        {
            this.DelProd = new List<ProdOutModel>();
        }
    }
}
