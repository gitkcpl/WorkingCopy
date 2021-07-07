using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Konto.App.Shared;
using Konto.App.Shared.Para;
using Konto.Core.Shared;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Trading.MillReturn
{
    public partial class MrStockTakaDetails : KontoForm
    {
        public string GridLayoutFileName { get; set; }
        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        public int AccId { get; set; }
        public int ChallanId { get; set; }
        public int ChallanTransId { get; set; }
        public int ItemId { get; set; }
        public MrStockTakaDetails()
        {
            InitializeComponent();
        
            this.gridView1.CustomDrawRowIndicator += GridView1_CustomDrawRowIndicator;
            this.Shown += MrTakaDetails_Shown;
            this.gridView1.MouseUp += GridView1_MouseUp;
            this.gridView1.DoubleClick += GridView1_DoubleClick;
            this.GridLayoutFileName = KontoFileLayout.Mr_Stock_Taka_Detail;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.Load += MrStockTakaDetails_Load;
        }

        private void MrStockTakaDetails_Load(object sender, EventArgs e)
        {
            var Prlist = new List<PendingMRProd>();

            using (var _db = new KontoContext())
            {
                var spcol1 = _db.SpCollections.FirstOrDefault(k => k.Id ==
                            (int)SpCollectionEnum.PendingMRProd);

                if (spcol1 == null)
                {
                    Prlist = _db.Database.SqlQuery<PendingMRProd>("dbo.PendingMRProd @CompanyId={0}," +
                        "@AccountId={1},@RefId={2}, @TransId={3},@itemid={4}",
                                KontoGlobals.CompanyId, this.AccId, this.ChallanId, 
                                this.ChallanTransId,this.ItemId).ToList();
                }
                else
                {
                    Prlist = _db.Database.SqlQuery<PendingMRProd>(
                            spcol1.Name + " @CompanyId={0},@AccountId={1},@RefId={2}, @TransId={3},@itemid={4}",
                            KontoGlobals.CompanyId, this.AccId, this.ChallanId,this.ChallanTransId,this.ItemId).ToList();
                }

                this.gridControl1.DataSource = Prlist;
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
           // this.gridView1.FocusedColumn = colTP1;
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
