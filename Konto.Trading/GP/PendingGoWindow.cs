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

namespace Konto.Trading.GP
{
    public partial class PendingGoWindow : KontoForm
    {
        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;

        public VoucherTypeEnum VoucherType { get; set; }
        public int AccId { get; set; }
        private string GridLayoutFileName = "trans\\go_pending_order.xml";
        public int[] SelectedRows { get; set; }
        public PendingGoWindow()
        {
            InitializeComponent();
            this.Load += PendingGoWindow_Load;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            cancelSimpleButton.Click += CancelSimpleButton_Click;
            headerEdit.Hide();
            headerEdit.Parent = this.gridControl1;
            headerEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            headerEdit.Leave += new EventHandler(headerEdit_Leave);
            this.gridView1.DoubleClick += GridView1_DoubleClick;
            this.gridView1.MouseUp += GridView1_MouseUp;
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
        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.F1 | Keys.Control))
            {
                KontoUtils.SaveLayoutGrid(this.GridLayoutFileName, this.gridView1);
                return true;
            }
            else if(keyData == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void PendingGoWindow_Load(object sender, EventArgs e)
        {
            try
            {
                using (var db = new KontoContext())
                {
                    var spcol = db.SpCollections.FirstOrDefault(k => k.Id ==
                      (int)SpCollectionEnum.PendingGreyOrderonChallan);
                    List<GreyPendingOrderDto> listDtos = new List<GreyPendingOrderDto>();
                    if (spcol == null)
                    {
                        listDtos = db.Database.SqlQuery<GreyPendingOrderDto>(
                            "dbo.PendingGreyOrderonChallan @CompanyId={0},@AccountId={1},@VoucherTypeID={2}",
                      KontoGlobals.CompanyId, this.AccId, this.VoucherType).ToList();
                    }
                    else
                    {
                        listDtos = db.Database.SqlQuery<GreyPendingOrderDto>(
                         spcol.Name + " @CompanyId={0},@AccountId={1},@VoucherTypeID={2}",
                         KontoGlobals.CompanyId, this.AccId, this.VoucherType).ToList();
                    }
                    if (listDtos.Count == 0)
                    {
                        this.Close();
                        this.Dispose();
                        return;
                    }
                    this.gridControl1.DataSource = listDtos;
                    KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, gridView1);
                }
                this.ActiveControl = gridControl1;
            }

            catch (Exception ex)
            {
                Log.Error(ex, "Grey Pending Order View");
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
