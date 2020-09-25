using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Transaction.Dtos;
using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Trading.MillReceipt
{
    public partial class PendingMillIssueView : KontoForm
    {
        public int AccId { get; set; }
        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        private string GridLayoutFileName = KontoFileLayout.Mrv_Pending_Detail;
        public PendingMillReceiptSp SelectedRow { get; set; }
        public PendingMillIssueView()
        {
            InitializeComponent();
            this.Shown += PendingMillIssueView_Shown;
            headerEdit.Hide();
            headerEdit.Parent = this.gridControl1;
            headerEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            headerEdit.Leave += new EventHandler(headerEdit_Leave);
            this.gridView1.DoubleClick += GridView1_DoubleClick;
            this.gridView1.MouseUp += GridView1_MouseUp;
            this.gridView1.CustomDrawRowIndicator += GridView1_CustomDrawRowIndicator;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.gridControl1.ProcessGridKey += GridControl1_ProcessGridKey;
           
            
        }

        private void GridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return) return;
            okSimpleButton.PerformClick();

        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            this.SelectedRow = gridView1.GetFocusedRow() as PendingMillReceiptSp;
            this.DialogResult = DialogResult.OK;
            this.Close();
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

        private void PendingMillIssueView_Shown(object sender, EventArgs e)
        {
            try
            {
                using(var db = new KontoContext())
                {
                    var list = new List<PendingMillReceiptSp>();
                    db.Database.CommandTimeout = 0;
                    var spcol = db.SpCollections.FirstOrDefault(k => k.Id ==
                                (int)SpCollectionEnum.PendingMillReceipt);
                    if (spcol == null)
                    {
                        list = db.Database.SqlQuery<PendingMillReceiptSp>(
                        "dbo.PendingMillReceipt @CompanyId={0},@AccountId={1},@VoucherTypeID={2}",
                         (int)KontoGlobals.CompanyId, this.AccId, (int)VoucherTypeEnum.MillIssue).ToList();
                    }
                    else
                    {
                        list = db.Database.SqlQuery<PendingMillReceiptSp>(
                         spcol.Name + " @CompanyId={0},@AccountId={1},@VoucherTypeID={2}",
                         (int)KontoGlobals.CompanyId, this.AccId, (int)VoucherTypeEnum.MillIssue).ToList();
                    }
                    this.pendingMillReceiptSpBindingSource.DataSource = list;   
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Pending Mill Issue");
                MessageBox.Show(ex.ToString());
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.F1 | Keys.Shift))
            {
                KontoUtils.SaveLayoutGrid(this.GridLayoutFileName, this.gridView1);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
