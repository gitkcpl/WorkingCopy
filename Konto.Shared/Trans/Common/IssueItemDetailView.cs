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
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Trans.Common
{
    public partial class IssueItemDetailView : KontoForm
    {
        public ProductTypeEnum TypeEnum { get; set; }
        public BindingList<GrnProdDto> prodDtos { get; set; }
        public List<GrnProdDto> DelProd { get; set; }
        public bool IsEditableQty { get; set; }
        public int TransId { get; set; }
        public int ItemId { get; set; }

        public  bool CommonStock { get; set; }
        public string GridLayoutFileName { get; set; }
        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        //private int colorId;
        //private int gradeId;
        //private int plyProductId;
        //private string productName;

        public decimal MetersPerKgs { get; set; } 
        public IssueItemDetailView()
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
            this.gridView1.KeyDown += GridView1_KeyDown;
            this.gridControl1.ProcessGridKey += GridControl1_ProcessGridKey;
            this.gridControl1.Enter += GridControl1_Enter;

            this.pendSimpleButton.Click += PendSimpleButton_Click;
        }

        private void GridView1_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            var row = gridView1.GetRow(e.RowHandle) as GrnProdDto;
            row.SrNo = gridView1.RowCount;
            row.VoucherNo = row.SrNo.ToString();
        }

        private void PendSimpleButton_Click(object sender, EventArgs e)
        {
            var frm = new PendingStockView();
            frm.ItemId = this.ItemId;
            frm.StockType = "Stock";
            frm.ProductType = this.TypeEnum;
            frm.CommonStock = this.CommonStock;
            
            frm.ShowDialog();

            var selpd = frm.list.Where(x => x.IsSelected).ToList();

            foreach (var _taka in selpd)
            {
                var ptrans = new GrnProdDto();
                ptrans.RefId = 0;
                ptrans.TransId = TransId;
                ptrans.ProductId = _taka.ProductId;
                ptrans.ColorId = _taka.ColorId;
                ptrans.GradeId = _taka.GradeId;
                ptrans.Id = _taka.Id;
                ptrans.SrNo = _taka.SrNo;
                ptrans.ProdOutId = 0;
                ptrans.NetWt = Convert.ToDecimal(_taka.Qty);
                ptrans.VoucherNo = _taka.VoucherNo;
                ptrans.Weaver = _taka.Weaver;
                ptrans.ChallanNo = _taka.InwardNo;
                ptrans.VoucherDate = Convert.ToInt32(_taka.VoucherDate);
                ptrans.ColorName = _taka.ColorName;
                if (Convert.ToInt32(_taka.Tops) == 0)
                    ptrans.Tops = 1;
                else
                    ptrans.Tops = Convert.ToInt32(_taka.Tops); // pcs for taka;

                this.prodDtos.Add(ptrans);
            }
        }

        private void GridControl1_Enter(object sender, EventArgs e)
        {
            gridView1.FocusedColumn = gridView1.VisibleColumns[0];
        }

        private void GridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            
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
            KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, gridView1);
            this.ActiveControl = gridControl1;


            this.gridView1.OptionsBehavior.Editable = true;

            this.gridView1.OptionsBehavior.ReadOnly = false;

            foreach (GridColumn item in gridView1.Columns)
            {
                item.OptionsColumn.ReadOnly = true;
            }
            gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            if (this.IsEditableQty)
            {
                gridView1.Columns["NetWt"].OptionsColumn.ReadOnly = false;
                gridView1.Columns["Tops"].OptionsColumn.ReadOnly = false;
                gridView1.Columns["Cops"].OptionsColumn.ReadOnly = false;
                gridView1.Columns["GrossWt"].OptionsColumn.ReadOnly = false;
                gridView1.Columns["TareWt"].OptionsColumn.ReadOnly = false;
                
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
