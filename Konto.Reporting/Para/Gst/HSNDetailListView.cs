using DevExpress.XtraGrid.Views.Grid;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data.Models.Transaction.Dtos;
using Konto.Pos.Sales;
using Konto.Shared.Trans.SInvoice;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Konto.Reporting.Para.Gst
{
    public partial class HSNDetailListView : KontoForm
    {
        public string GridLayoutFileName { get; set; }
        string GridTypeName = string.Empty;
        public HSNDetailListView()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.HSN_Detail_List;
            customGridView1.KeyDown += customGridView1_KeyDown;
        }

        public void RefreshGrid(List<HsnSmryDetail> HSNDetailList, string HeaderText, string type)
        {
            customGridControl1.DataSource = HSNDetailList;
            KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, customGridView1);
            this.Text = HeaderText;
            GridTypeName = type;
        }
        public void RefreshDocDetailGrid(List<docDetailList> list, string HeaderText,string type)
        {
            customGridControl1.DataSource = list;
            //KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, customGridView1);
          
            this.Text = HeaderText;
            GridTypeName = type;
        }
        private void customGridView1_KeyDown(object sender, KeyEventArgs e)
        {
          
            if (e.KeyCode != Keys.Enter) return;
 
            GridView view = sender as GridView;
            int BillId = 0;
            if (GridTypeName == "HSN")
            {
                var row = view.GetRow(view.FocusedRowHandle) as HsnSmryDetail;
                BillId = row.BillId;
            }
            else
            {
                var row = view.GetRow(view.FocusedRowHandle) as docDetailList;
                BillId = row.BillId;
            }
            var vw = new KontoMetroForm();

            if (KontoGlobals.PackageId == (int)PackageType.POS)
                vw = new SalesIndex();
            else
             vw = new SInvoiceIndex();

            vw.tabControlAdv1.SelectedIndex = 0;
            vw.EditKey = BillId;
            vw.OpenForLookup = true;
            vw.ShowDialog();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.F1 | Keys.Shift))
            {
                KontoUtils.SaveLayoutGrid(this.GridLayoutFileName, this.customGridView1);
                return true;
            }
            else if (keyData == (Keys.F2 | Keys.Shift))
            {

                var frm = new GridPropertView();
                frm.gridControl1.DataSource = this.customGridControl1.DataSource;
                frm.gridView1.Assign(this.customGridView1, false);
                if (frm.ShowDialog() != DialogResult.OK) return true;
                this.customGridView1.Assign(frm.gridView1, false);
                KontoUtils.SaveLayoutGrid(this.GridLayoutFileName, this.customGridView1);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void okSimpleButton_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void cancelSimpleButton_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
