using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using System;
using System.Collections.Generic;
using Konto.Data.Models.Apparel.Dtos;
using System.Linq;


namespace Konto.Apparel.Qc
{
    public partial class QualityControlList : ListBaseView
    {
        private List<BarcodeTransDto> barcode_translist = new List<BarcodeTransDto>();
        public QualityControlList()
        {
            InitializeComponent();

            this.GridLayoutFileName = KontoFileLayout.QC_List_Layout;
            listAction1.EditDeleteDisabled(false);
            this.ReportPrint = true;
            this.listDateRange1.GetButtonClick += ListDateRange1_GetButtonClick;
        }

        private void ListDateRange1_GetButtonClick(object sender, EventArgs e)
        {
            try
            {

                
                using (var _db = new KontoContext())
                {
                    _db.Database.CommandTimeout = 0;

                    barcode_translist = (from bo in _db.BarcodeTrans
                                         join it in _db.Products on bo.ProductId equals it.Id
                                         join b in _db.Barcodes on bo.BarcodeId equals b.Id
                                         join e1 in _db.Emps on bo.EmpId equals e1.Id
                                         join dv in _db.Divisions on bo.DivId equals dv.Id
                                         where bo.TransType == 1 && bo.VoucherDate >= listDateRange1.FromDate && bo.VoucherDate <= listDateRange1.ToDate
                                         select new BarcodeTransDto
                                         {
                                             VoucherDate = bo.VoucherDate,
                                             BarcodeNo = bo.BarcodeNo,
                                             ProductName = it.ProductName,
                                             Remarks = bo.Remarks,
                                             Qty = bo.Qty,
                                             EmpName = e1.EmpName,
                                             TrnasType = bo.TransType,
                                             DivName = dv.DivisionName,
                                             QcPassed = bo.QcPassed,
                                             Id = bo.Id
                                         }
                                ).ToList();

                    customGridView1.ShowLoadingPanel();
                    customGridView1.Columns.Clear();
                    customGridControl1.DataSource = barcode_translist;
                    customGridView1.HideLoadingPanel();


                    if (string.IsNullOrEmpty(this.GridLayoutFileName) || this.KontoView == null) return;

                    if (barcode_translist.Count == 0)
                        listAction1.EditDeleteDisabled(false);
                    else
                    {
                        if (customGridView1.Columns.ColumnByFieldName("Id") != null && customGridView1.Columns.ColumnByFieldName("VoucherId") != null)
                            listAction1.EditDeleteDisabled(true);
                        else
                            listAction1.EditDeleteDisabled(false);
                    }

                    if (string.IsNullOrEmpty(this.GridLayoutFileName) || this.KontoView == null) return;

                    KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, this.KontoView);
                    this.ActiveControl = customGridControl1;
                }
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }
        }

        public override void RefreshGrid()
        {
            base.RefreshGrid();
        }
    }
}
