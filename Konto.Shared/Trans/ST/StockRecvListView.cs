using System;
using System.Linq;
using Konto.Core.Shared.Frms;
using Konto.App.Shared;
using Konto.Data;
using Konto.Core.Shared.Libs;
using Syncfusion.Windows.Forms;
using Serilog;
using Konto.Data.Models.Transaction.Dtos;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using GrapeCity.ActiveReports;
using Syncfusion.Windows.Forms.Tools;

namespace Konto.Shared.Trans.ST
{
    public partial class StockRecvListView : ListBaseView
    {
        //private List<OpBillListDto> _modelList = new List<OpBillListDto>();

        public StockRecvListView()

        {
            InitializeComponent();
            this.listDateRange1.GetButtonClick += ListDateRange1_GetButtonClick;
            //  this.GridLayoutFileName = KontoFileLayout.Op_Bill_List;

            this.ReportPrint = true;
            listAction1.EditDeleteDisabled(false);
            
        }

    

        private void ListDateRange1_GetButtonClick(object sender, EventArgs e)
        {
            //this.GridLayoutFileName = listDateRange1.SelectedItem.LayoutFile;
            var DtCriterias = new List<TransferReceive>();
            try
            {
                using (var db = new KontoContext())
                {
                    DtCriterias = (from c in db.Challans
                            join ct in db.ChallanTranses on c.Id equals ct.ChallanId
                            join p in db.Products on ct.ProductId equals p.Id
                            join v in db.Vouchers on c.VoucherId equals v.Id
                            join cl in db.ColorModels on p.ColorId equals cl.Id into cl_join
                            from cl in cl_join.DefaultIfEmpty()
                            join e1 in db.Emps on ct.ReceivedById equals e1.Id into e_j
                            from e1 in e_j.DefaultIfEmpty()
                            where v.VTypeId == (int) VoucherTypeEnum.Stock_Transfer
                                  && ct.IsReceived && (ct.ReceiveDateTime >= listDateRange1.DfDate
                                  && ct.ReceiveDateTime <= listDateRange1.TfDate)
                                  && ct.BranchId == KontoGlobals.BranchId
                            select new TransferReceive
                            {
                                Qty = ct.Qty, BarCode = p.BarCode, VoucherNo = c.VoucherNo,
                                ProductName = p.ProductName, ColorName = cl.ColorName,
                                Pcs = ct.Pcs, ReceiveDateTime = ct.ReceiveDateTime,
                                ReceivedBy = e1.EmpName
                            }
                        ).ToList();
                }

                customGridControl1.DataSource = DtCriterias;
                    if (string.IsNullOrEmpty(this.GridLayoutFileName) || this.KontoView == null) return;

                KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, this.KontoView);

                this.ActiveControl = customGridControl1;
                listAction1.EditDeleteDisabled(false);
               
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Stock Transfer List Error");
                MessageBoxAdv.Show(this, "Error While Generating List !!", "Exception ", ex.ToString());
            }
        }



        public override void Print()
        {
            base.Print();
            if (this.customGridView1.FocusedRowHandle < 0) return;
            if (KontoView.Columns.ColumnByFieldName("Id") != null)
            {
                if (KontoView.Columns.ColumnByFieldName("IsDeleted") != null)
                {
                    if (Convert.ToBoolean(this.KontoView.GetRowCellValue(this.KontoView.FocusedRowHandle, "IsDeleted")))
                    {
                        return;
                    }
                }
                var id = this.KontoView.GetRowCellValue(this.KontoView.FocusedRowHandle, "Id");

                PageReport rpt = new PageReport();

                rpt.Load(new FileInfo("reg\\doc\\stock_receive_challan.rdlx"));

                rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);

                doc.Parameters["id"].CurrentValue = id;
                doc.Parameters["challan"].CurrentValue = "N";
                doc.Parameters["reportid"].CurrentValue = 0;
                var frm = new KontoRepViewer(doc);
                frm.Text = "Receive Challan";
                var _tab = this.Parent.Parent as TabControlAdv;
                if (_tab == null) return;
                var pg1 = new TabPageAdv();
                pg1.Text = "Stock Receive Print";
                _tab.TabPages.Add(pg1);
                _tab.SelectedTab = pg1;
                frm.TopLevel = false;
                frm.Parent = pg1;
                frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                frm.Show();// = true;

            }

        }


    }
}
