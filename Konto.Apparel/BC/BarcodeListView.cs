using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Konto.Core.Shared.Frms;
using Konto.App.Shared;
using Konto.Data;
using Konto.Core.Shared.Libs;
using Syncfusion.Windows.Forms;
using Serilog;
using Konto.Data.Models.Apparel.Dtos;
using Syncfusion.Windows.Forms.Tools;
using System.IO;
using GrapeCity.ActiveReports;

namespace Konto.Apparel.BC
{
    public partial class BarcodeListView : ListBaseView
    {
        private List<BarcodeTransDto> _bmodelList = new List<BarcodeTransDto>();
        public BarcodeListView()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.Barcode_List_Layout;
            this.Load += ProductListView_Load;
            this.listDateRange1.GetButtonClick += ListDateRange1_GetButtonClick;
            this.ReportPrint = true;
        }

        private void ListDateRange1_GetButtonClick(object sender, EventArgs e)
        {
            using (var _context = new KontoContext())
            {
                _context.Database.CommandTimeout = 0;

                _bmodelList = (from pd in _context.Barcodes
                               join p in _context.Products on pd.ProductId equals p.Id
                               join act in _context.Accs on pd.AccId equals act.Id
                               where pd.RefBarcodeId != 0

                               select new BarcodeTransDto
                               {
                                   ReportId = pd.ReportId,
                                   BarcodeNo = pd.BarcodeNo,
                                   ProductName = p.ProductName,
                                   AccName = act.AccName,
                                   Qty = pd.Qty
                               }
                ).ToList();
            }

            customGridControl1.DataSource = _bmodelList;
            if (string.IsNullOrEmpty(this.GridLayoutFileName) || this.KontoView == null) return;

            KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, this.KontoView);

            this.ActiveControl = customGridControl1;
            if (_bmodelList.Count == 0)
                listAction1.EditDeleteDisabled(false);
            else
               listAction1.EditDeleteDisabled(true);
        }

        private void ProductListView_Load(object sender, EventArgs e)
        {
            
        }

        public override void RefreshGrid()
        {
            base.RefreshGrid();
           

            
        }

        public override void DeleteRec()
        {
            base.DeleteRec();

            if (customGridView1.FocusedRowHandle < 0) return;
            try
            {
                var _id = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "Id"));

                if (MessageBoxAdv.Show(KontoGlobals.DeleteBeforeMsg, "Delete ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                using (var db = new KontoContext())
                {
                    var model = db.Products.Find(_id);
                    model.IsDeleted = true;
                    db.SaveChanges();
                    customGridView1.DeleteRow(customGridView1.FocusedRowHandle);
                    MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "product delete");
                MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
            }
        }
        public override void Print()
        {
            base.Print();

          
            if (this.customGridView1.FocusedRowHandle < 0) return;
            if (KontoView.Columns.ColumnByFieldName("ReportId") != null)
            {
                
                string accname = Convert.ToString(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "AccName"));
                int reportid = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "ReportId"));
                if (KontoView.Columns.ColumnByFieldName("IsDeleted") != null)
                {
                    if (Convert.ToBoolean(this.KontoView.GetRowCellValue(this.KontoView.FocusedRowHandle, "IsDeleted")))
                    {
                        return;
                    }
                }
                PageReport rpt = new PageReport();

                rpt.Load(new FileInfo("reg\\doc\\Outwardbarcode.rdlx"));

                rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);

                doc.Parameters["Party"].CurrentValue = accname;
                doc.Parameters["ReportId"].CurrentValue =reportid;
                doc.Parameters["BarcodeNo"].CurrentValue = "0";
                var frm = new KontoRepViewer(doc);
                frm.Text = "Barcode";
                var _tab = this.ParentForm.Parent.Parent as TabControlAdv;
                if (_tab == null) return;
                var pg1 = new TabPageAdv();
                pg1.Text = "Barcode Print";
                _tab.TabPages.Add(pg1);
                _tab.SelectedTab = pg1;
                frm.TopLevel = false;
                frm.Parent = pg1;
                frm.Location = new System.Drawing.Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                frm.Show();// = true;
            }

        }
        private void excelSimpleButton_Click(object sender, EventArgs e)
        {
            Print();
        }
    }
}
