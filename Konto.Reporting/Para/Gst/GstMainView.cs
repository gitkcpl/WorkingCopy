using Aspose.Cells;
using FarPoint.Win.Spread;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Admin.Dtos;
using Konto.Data.Models.Reports;
using Konto.Data.Models.Transaction.Dtos;
using Konto.Shared.Account.DRCRNote;
using Konto.Shared.Trans.SInvoice;
using Serilog;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Reporting.Para.Gst
{
    public partial class GstMainView : KontoForm
    {
        FarPoint.Win.Spread.FpSpread gridControl1;
        List<GstDto> Gstrs = new List<GstDto>();

        List<Gstrb2csDto> GstrB2CS = new List<Gstrb2csDto>();
        public GstMainView()
        {
            InitializeComponent();
            FillLookup();
            this.okSimpleButton.Click += OkSimpleButton_Click;
            cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.FormClosed += GstMainView_FormClosed;
            this.gst3BSimpleButton.Click += Gst3BSimpleButton_Click;
            this.gst1SimpleButton.Click += Gst1SimpleButton_Click;
            gridControl1 = new FarPoint.Win.Spread.FpSpread();
            gridControl1.Dock = DockStyle.Fill;
            this.panelControl1.Controls.Add(this.gridControl1);
            gridControl1.PreviewKeyDown += GridControl1_PreviewKeyDown;
            this.Load += GstMainView_Load;
            fromDateEdit.EditValue = KontoGlobals.DFromDate;
            toDateEdit.EditValue = KontoGlobals.DToDate;
        }

        private void GstMainView_Load(object sender, EventArgs e)
        {
            //fnYearlookUpEdit.EditValue = KontoGlobals.YearId;
            //if (DateTime.Now.Month != 1)
            //    monthLookUpEdit.ItemIndex = DateTime.Now.Month - 2;
            //else
            //    monthLookUpEdit.ItemIndex = 11;

            this.ActiveControl = fromDateEdit;
            
        }

        private void GridControl1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            
            int fdate = Convert.ToInt32(fromDateEdit.DateTime.ToString("yyyyMMdd"));
            int tdate= Convert.ToInt32(toDateEdit.DateTime.ToString("yyyyMMdd"));

            try
            {
                if (gridControl1.ActiveSheet.SheetName == "GSTR-3B" && e.KeyCode == Keys.Enter)
                {
                    var vw = new HSNDetailListView();
                    KontoContext db = new KontoContext();


                    string Row = gridControl1.ActiveSheet.Cells[gridControl1.ActiveSheet.ActiveRow.Index, 1].Value.ToString();

                    int rowNo = gridControl1.ActiveSheet.ActiveRow.Index + 1;
                    if (Row == "(a) Outward Taxable  supplies  (other than zero rated, nil rated and exempted)")
                    {
                        var list = db.Database.SqlQuery<HsnSmryDetail>("dbo.GSTR3BDetails @CompanyId={0},@FromDate={1}," +
                                    "@ToDate={2},@YearId={3}, @Type={4}", KontoGlobals.CompanyId, fdate, tdate, KontoGlobals.YearId, "A").ToList();
                        if (list.Count == 0) return;
                        // vw.HSNDetailList = new List<HsnSmryDetail>(list);
                        vw.RefreshGrid(list, Row, "HSN");
                        vw.ShowDialog();
                    }
                    else if (Row == "(b) Outward Taxable  supplies  (zero rated )")
                    {
                        var list = db.Database.SqlQuery<HsnSmryDetail>("dbo.GSTR3BDetails @CompanyId={0},@FromDate={1}," +
                                   "@ToDate={2},@YearId={3}, @Type={4}", KontoGlobals.CompanyId, fdate, tdate, KontoGlobals.YearId, "B").ToList();
                        if (list.Count == 0) return;
                        vw.RefreshGrid(list, Row, "HSN");
                        vw.ShowDialog();
                    }
                    else if (Row == "(c) Other Outward Taxable  supplies (Nil rated, exempted)")
                    {
                        var list = db.Database.SqlQuery<HsnSmryDetail>("dbo.GSTR3BDetails @CompanyId={0},@FromDate={1}," +
                          "@ToDate={2},@YearId={3}, @Type={4}", KontoGlobals.CompanyId, fdate, tdate, KontoGlobals.YearId, "C").ToList();
                        if (list.Count == 0) return;
                        vw.RefreshGrid(list, Row, "HSN");
                        vw.ShowDialog();
                    }
                    else if (Row == "(d) Inward supplies (liable to reverse charge) ")
                    {
                        var list = db.Database.SqlQuery<HsnSmryDetail>("dbo.GSTR3BDetails @CompanyId={0},@FromDate={1}," +
                                    "@ToDate={2},@YearId={3}, @Type={4}", KontoGlobals.CompanyId, fdate, tdate, KontoGlobals.YearId, "D").ToList();
                        if (list.Count == 0) return;
                        vw.RefreshGrid(list, Row, "HSN");
                        vw.ShowDialog();
                    }
                    else if (Row == "(1)   Import of goods ")
                    {
                        var list = db.Database.SqlQuery<HsnSmryDetail>("dbo.GSTR3BDetails @CompanyId={0},@FromDate={1}," +
                                    "@ToDate={2},@YearId={3}, @Type={4}", KontoGlobals.CompanyId, fdate, tdate, KontoGlobals.YearId, "E").ToList();
                        if (list.Count == 0) return;
                        vw.RefreshGrid(list, Row, "HSN");
                        vw.ShowDialog();
                    }
                    else if (Row == "(2)   Import of services")
                    {
                        var list = db.Database.SqlQuery<HsnSmryDetail>("dbo.GSTR3BDetails @CompanyId={0},@FromDate={1}," +
                                    "@ToDate={2},@YearId={3}, @Type={4}", KontoGlobals.CompanyId, fdate, tdate, KontoGlobals.YearId, "F").ToList();
                        if (list.Count == 0) return;
                        vw.RefreshGrid(list, Row, "HSN");
                        vw.ShowDialog();
                    }
                    else if (Row == "(5)   All other ITC")
                    {
                        var list = db.Database.SqlQuery<HsnSmryDetail>("dbo.GSTR3BDetails @CompanyId={0},@FromDate={1}," +
                                    "@ToDate={2},@YearId={3}, @Type={4}", KontoGlobals.CompanyId, fdate, tdate, KontoGlobals.YearId, "G").ToList();
                        if (list.Count == 0) return;
                        vw.RefreshGrid(list, Row, "HSN");
                        vw.ShowDialog();
                    }
                    else if (Row == "From a supplier under composition scheme, Exempt and Nil rated supply")
                    {
                        var list = db.Database.SqlQuery<HsnSmryDetail>("dbo.GSTR3BDetails @CompanyId={0},@FromDate={1}," +
                                    "@ToDate={2},@YearId={3}, @Type={4}", KontoGlobals.CompanyId, fdate, tdate, KontoGlobals.YearId, "H").ToList();
                        if (list.Count == 0) return;
                        vw.RefreshGrid(list, Row, "HSN");
                        vw.ShowDialog();
                    }
                    else if (Row == "Non GST supply")
                    {
                        var list = db.Database.SqlQuery<HsnSmryDetail>("dbo.GSTR3BDetails @CompanyId={0},@FromDate={1}," +
                                    "@ToDate={2},@YearId={3}, @Type={4}", KontoGlobals.CompanyId, fdate, tdate, KontoGlobals.YearId, "I").ToList();
                        if (list.Count == 0) return;
                        vw.RefreshGrid(list, Row, "HSN");
                        vw.ShowDialog();
                    }
                    else if (rowNo >= 79) //(Row == "Supplies made to Unregister Person" )//State Name
                    {
                        var list = db.Database.SqlQuery<HsnSmryDetail>("dbo.GSTR3BDetails @CompanyId={0},@FromDate={1}," +
                                    "@ToDate={2},@YearId={3}, @Type={4},@StateName={5}", KontoGlobals.CompanyId, fdate, tdate, KontoGlobals.YearId, "J", Row.Substring(3)).ToList();
                        if (list.Count == 0) return;
                        vw.RefreshGrid(list, Row, "HSN");
                        vw.ShowDialog();
                    }
                }
                else if (gridControl1.ActiveSheet.SheetName == "B2B" && e.KeyCode == Keys.Enter)
                {
                    var vw = new SInvoiceIndex();
                    var win = new KontoForm();
                    int id = (int)gridControl1.ActiveSheet.Cells[gridControl1.ActiveSheet.ActiveRow.Index, 14].Value;
                    vw.tabControlAdv1.SelectedIndex = 0;
                    vw.EditKey = id;
                    vw.OpenForLookup = true;
                    vw.ShowDialog();
                }
                else if (gridControl1.ActiveSheet.SheetName == "B2BA" && e.KeyCode == Keys.Enter)
                {
                    var vw = new SInvoiceIndex();
                    int id = (int)gridControl1.ActiveSheet.Cells[gridControl1.ActiveSheet.ActiveRow.Index, 16].Value;
                    vw.tabControlAdv1.SelectedIndex = 0;
                    vw.EditKey = id;
                    vw.OpenForLookup = true;
                    vw.ShowDialog();
                }
                else if (gridControl1.ActiveSheet.SheetName == "B2CL" && e.KeyCode == Keys.Enter)
                {
                    var vw = new SInvoiceIndex();
                    int id = (int)gridControl1.ActiveSheet.Cells[gridControl1.ActiveSheet.ActiveRow.Index, 11].Value;
                    vw.tabControlAdv1.SelectedIndex = 0;
                    vw.EditKey = id;
                    vw.OpenForLookup = true;
                    vw.ShowDialog();
                }
                else if (gridControl1.ActiveSheet.SheetName == "B2CLA" && e.KeyCode == Keys.Enter)
                {
                    var vw = new SInvoiceIndex();
                    int id = (int)gridControl1.ActiveSheet.Cells[gridControl1.ActiveSheet.ActiveRow.Index, 13].Value;
                    vw.tabControlAdv1.SelectedIndex = 0;
                    vw.EditKey = id;
                    vw.OpenForLookup = true;
                    vw.ShowDialog();
                }
                else if (gridControl1.ActiveSheet.SheetName == "B2CS" && e.KeyCode == Keys.Enter)
                {
                    var vw = new HSNDetailListView();
                    KontoContext db = new KontoContext();

                    string Row = gridControl1.ActiveSheet.Cells[gridControl1.ActiveSheet.ActiveRow.Index, 1].Value.ToString();
                    string gstin = gridControl1.ActiveSheet.Cells[gridControl1.ActiveSheet.ActiveRow.Index, 6].Value.ToString();
                    string taxrate = gridControl1.ActiveSheet.Cells[gridControl1.ActiveSheet.ActiveRow.Index, 3].Value.ToString();
                    var list = db.Database.SqlQuery<HsnSmryDetail>("dbo.GSTR3BDetails @CompanyId={0},@FromDate={1}," +
                                    "@ToDate={2},@YearId={3}, @Type={4},@StateName={5},@gstin={6},@taxrate={7}",
                                    KontoGlobals.CompanyId, fdate, tdate, KontoGlobals.YearId, "J",
                                    Row.Substring(3), gstin, taxrate).ToList();
                    if (list.Count == 0) return;
                    vw.RefreshGrid(list, Row, "HSN");
                    vw.ShowDialog();
                }
                else if (gridControl1.ActiveSheet.SheetName == "EXP" && e.KeyCode == Keys.Enter)
                {
                    var vw = new SInvoiceIndex();
                    int id = (int)gridControl1.ActiveSheet.Cells[gridControl1.ActiveSheet.ActiveRow.Index, 12].Value;
                    vw.tabControlAdv1.SelectedIndex = 0;
                    vw.EditKey = id;
                    vw.OpenForLookup = true;
                    vw.ShowDialog();
                }
                else if (gridControl1.ActiveSheet.SheetName == "EXPA" && e.KeyCode == Keys.Enter)
                {
                    var vw = new SInvoiceIndex();
                    int id = (int)gridControl1.ActiveSheet.Cells[gridControl1.ActiveSheet.ActiveRow.Index, 14].Value;
                    vw.tabControlAdv1.SelectedIndex = 0;
                    vw.EditKey = id;
                    vw.OpenForLookup = true;
                    vw.ShowDialog();
                }
                else if (gridControl1.ActiveSheet.SheetName == "CDNR" && e.KeyCode == Keys.Enter)
                {
                    var vw = new DRCRNoteIndex();
                    int id = (int)gridControl1.ActiveSheet.Cells[gridControl1.ActiveSheet.ActiveRow.Index, 15].Value;
                    vw.tabControlAdv1.SelectedIndex = 0;
                    vw.EditKey = id;
                    vw.OpenForLookup = true;
                    vw.ShowDialog();
                }
                else if (gridControl1.ActiveSheet.SheetName == "CDNRA" && e.KeyCode == Keys.Enter)
                {
                    var vw = new DRCRNoteIndex();
                    int id = (int)gridControl1.ActiveSheet.Cells[gridControl1.ActiveSheet.ActiveRow.Index, 17].Value;
                    vw.tabControlAdv1.SelectedIndex = 0;
                    vw.EditKey = id;
                    vw.OpenForLookup = true;
                    vw.ShowDialog();
                }
                else if (gridControl1.ActiveSheet.SheetName == "CDNUR" && e.KeyCode == Keys.Enter)
                {
                    var vw = new DRCRNoteIndex();
                    int id = (int)gridControl1.ActiveSheet.Cells[gridControl1.ActiveSheet.ActiveRow.Index, 14].Value;
                    vw.tabControlAdv1.SelectedIndex = 0;
                    vw.EditKey = id;
                    vw.OpenForLookup = true;
                    vw.ShowDialog();
                }
                else if (gridControl1.ActiveSheet.SheetName == "CDNURA" && e.KeyCode == Keys.Enter)
                {
                    var vw = new DRCRNoteIndex();
                    int id = (int)gridControl1.ActiveSheet.Cells[gridControl1.ActiveSheet.ActiveRow.Index, 16].Value;
                    vw.tabControlAdv1.SelectedIndex = 0;
                    vw.EditKey = id;
                    vw.OpenForLookup = true;
                    vw.ShowDialog();
                }
                else if (gridControl1.ActiveSheet.SheetName == "HSN" && e.KeyCode == Keys.Enter)
                {
                    int UnitId = 0;
                    if (gridControl1.ActiveSheet.Cells[gridControl1.ActiveSheet.ActiveRow.Index, 10].Value != null)
                        UnitId = (int)gridControl1.ActiveSheet.Cells[gridControl1.ActiveSheet.ActiveRow.Index, 10].Value;

                    var vw = new HSNDetailListView();
                    string HsnCode = gridControl1.ActiveSheet.Cells[gridControl1.ActiveSheet.ActiveRow.Index, 0].Value.ToString();
                    KontoContext db = new KontoContext();

                    string gid = gridControl1.ActiveSheet.Cells[gridControl1.ActiveSheet.ActiveRow.Index, 11].Value.ToString();

                    var list = db.Database.SqlQuery<HsnSmryDetail>("dbo.HsnSmryDetail @CompanyId={0},@FromDate={1}," +
                        "@ToDate={2},@YearId={3},@HsnCode={4},@UnitId={5},@groupid={6}", KontoGlobals.CompanyId,
                        fdate, tdate, KontoGlobals.YearId, HsnCode, UnitId,gid).ToList();

                    vw.RefreshGrid(list, "HSN Detail", "HSN");
                    vw.ShowDialog();
                }
                else if (gridControl1.ActiveSheet.SheetName == "DOC" && e.KeyCode == Keys.Enter)
                {
                    var vw = new HSNDetailListView();

                    KontoContext db = new KontoContext();

                    if ((gridControl1.ActiveSheet.Cells[gridControl1.ActiveSheet.ActiveRow.Index, 0].Value) != null)
                    {
                        string NOB = gridControl1.ActiveSheet.Cells[gridControl1.ActiveSheet.ActiveRow.Index, 0].Value.ToString();
                        int VoucherId = Convert.ToInt32(gridControl1.ActiveSheet.Cells[gridControl1.ActiveSheet.ActiveRow.Index, 5].Value.ToString());
                        var list = db.Database.SqlQuery<docDetailList>("dbo.docDetailList @fromdate={0},@todate={1}," +
                            "@companyid={2},@NOB={3},@voucherId={4}", fdate, tdate, KontoGlobals.CompanyId, NOB, VoucherId).ToList();

                        vw.RefreshDocDetailGrid(list, "Doc Detail", "Doc");
                        vw.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Log.Error(ex, "Gstr1 Zoom");
            }

            

        }

        private void Gst1SimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if ( File.Exists("gstr1.xlsx") && this.IsFileLocked(new FileInfo("gstr1.xlsx")))
                {
                    MessageBox.Show("File Already Open. Please Close and Download Again");
                    return;
                }

                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += bw_Gstr1DoWork;
                bw.RunWorkerCompleted += bw_Gstr1RunWorkerCompleted;
               
                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormDescription("Generating Gstr1 Excel");
                bw.RunWorkerAsync();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
          

        }
        private void bw_Gstr1RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            splashScreenManager1.CloseWaitForm();
        }
        private void bw_Gstr1DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                GstDownload();
            }
            catch (Exception ex)
            {
                if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                MessageBox.Show(ex.ToString());
            }

        }
        private void GstDownload()
        {
            
           
            var lst = new List<GstDto>();
            WorkbookDesigner wd = new WorkbookDesigner();
            wd.Workbook = new Aspose.Cells.Workbook("Excel\\GSTR1_Excel_Workbook_Template_V1.5.xlsx");

            Worksheet w = wd.Workbook.Worksheets[1];

            int fyear = Convert.ToInt32(KontoGlobals.DFromDate.ToString("yyyy"));

            int tyear = Convert.ToInt32(KontoGlobals.DToDate.ToString("yyyy"));
            //int monthno = int.Parse(monthLookUpEdit.EditValue.ToString());
            int fdate = Convert.ToInt32(fromDateEdit.DateTime.ToString("yyyyMMdd"));
            int tdate = Convert.ToInt32(toDateEdit.DateTime.ToString("yyyyMMdd"));

            //if (monthno < 4)
            //{
            //    fdate = int.Parse(tyear.ToString() + monthLookUpEdit.EditValue.ToString() + "01");
            //    tdate = int.Parse(tyear.ToString() + monthLookUpEdit.EditValue.ToString() + DateTime.DaysInMonth(tyear, monthno).ToString());
            //}
            //else
            //{
            //    fdate = int.Parse(fyear.ToString() + monthLookUpEdit.EditValue.ToString() + "01");
            //    tdate = int.Parse(fyear.ToString() + monthLookUpEdit.EditValue.ToString() + DateTime.DaysInMonth(tyear, monthno).ToString());
            //}

            var _db = new KontoContext();

            if (File.Exists("Excel\\GSTR1_Excel_Workbook_Template_V1.5.xlsx"))
            {
                _db.Database.CommandTimeout = 0;

                ///////////////////////////////////B2B///////////////////////////////////////////////////////////////////////////////////                    
                lst = _db.Database.SqlQuery<GstDto>(
                    "dbo.GstReport @CompanyId={0},@FromDate={1},@ToDate={2},@yearid={3}",
                    Convert.ToInt32(KontoGlobals.CompanyId), fdate, tdate, KontoGlobals.YearId).ToList();



                int row = 5;

                var b2b = lst.Where(x => x.IsRevice == 0 && x.VTypeId == 12 && (x.Type == "REG" || x.Type == "CMP")).ToList();

                foreach (var t in b2b)
                {
                    w.Cells["A" + row].PutValue(t.GstIn);
                    w.Cells["B" + row].PutValue(t.Account);
                    w.Cells["C" + row].PutValue(t.VoucherNo);
                    w.Cells["D" + row].PutValue(t.VoucherDate);

                    w.Cells["E" + row].PutValue(t.BillAmount);
                    w.Cells["F" + row].PutValue(t.StateName);
                    w.Cells["G" + row].PutValue("N");
                    w.Cells["I" + row].PutValue(t.BillType);
                    w.Cells["J" + row].PutValue("");
                    w.Cells["K" + row].PutValue(t.GSTRate);
                    w.Cells["L" + row].PutValue(t.TaxableValue);
                    w.Cells["M" + row].PutValue(t.Cess);

                    row += 1;

                }

                /////////////////////////////////////B2CL///////////////////////////////////////////////////////////////////////////////////
                //List = new ObservableCollection<GstDto>(_db.Database.SqlQuery<GstDto>(
                //        "dbo.GstReportBtoc @CompanyId={0},@TransTypeId={1},@FromDate={2},@ToDate={3}", Convert.ToInt32(KontoGlobals.CompanyId), VoucherTypeEnum.SaleInvoice,fdate, tdate).ToList());
                var b2cllist = lst.Where(x => x.IsRevice == 0 && x.BillAmount >= 250000 && x.IGSTAmt > 0
                       && (x.Type != "REG" && x.Type != "CMP") && x.VTypeId == 12).ToList();
                w = wd.Workbook.Worksheets[3];
                row = 5;
                foreach (var t in b2cllist)
                {

                    w.Cells["A" + row].PutValue(t.VoucherNo);
                    w.Cells["B" + row].PutValue(t.VoucherDate);
                    w.Cells["C" + row].PutValue(t.BillAmount);
                    w.Cells["D" + row].PutValue(t.StateName);
                    w.Cells["E" + row].PutValue("");
                    w.Cells["F" + row].PutValue(t.GSTRate);
                    w.Cells["G" + row].PutValue(t.TaxableValue);
                    w.Cells["H" + row].PutValue(t.Cess);
                    w.Cells["I" + row].PutValue("");
                    w.Cells["J" + row].PutValue(t.BondedWH);

                    row += 1;

                }

                ///////////////////////////////////B2CS///////////////////////////////////////////////////////////////////////////////////
                var b2cs = _db.Database.SqlQuery<Gstrb2csDto>(
                            "dbo.GstReportBtocs @CompanyId={0},@yearid={1},@FromDate={2},@ToDate={3}",
                                Convert.ToInt32(KontoGlobals.CompanyId), KontoGlobals.YearId, fdate, tdate).ToList();

                w = wd.Workbook.Worksheets[5];
                row = 5;
                foreach (var t in b2cs)
                {

                    w.Cells["A" + row].PutValue(t.Type);
                    w.Cells["B" + row].PutValue(t.StateName);
                    w.Cells["C" + row].PutValue("");
                    w.Cells["D" + row].PutValue(t.GSTRate);
                    w.Cells["E" + row].PutValue(t.TaxableValue);
                    w.Cells["F" + row].PutValue(t.Cess);
                    w.Cells["G" + row].PutValue(t.GstIn);

                    row += 1;

                }

                ///////////////////////////////////CDNR///////////////////////////////////////////////////////////////////////////////////
                // List = new ObservableCollection<GstDto>(_db.Database.SqlQuery<GstDto>(
                ///   "dbo.GstReport @CompanyId={0},@TransTypeId={1},@TransTypeId1={2},@FromDate={3},@ToDate={4},@Billag={5},@TransTypeId2 ={6}", Convert.ToInt32(KontoGlobals.CompanyId), VoucherTypeEnum.DebitCreditNote, VoucherTypeEnum.SaleReturn, fdate, tdate, "SALE", VoucherTypeEnum.DebitCreditNote).ToList());

                // var cdnr = List.Where(x => x.VTypeId != 12 && (x.Type == "REG" || x.Type == "CMP") && x.GstIn.Length == 15).ToList();
                var cdnr = lst.Where(x => x.IsRevice == 0 && x.VTypeId != 12 && (x.Type == "REG" || x.Type == "CMP")).ToList();
                w = wd.Workbook.Worksheets[7];
                row = 5;
                foreach (var t in cdnr)
                {
                    if (t.GstIn != "")
                    {
                        w.Cells["A" + row].PutValue(t.GstIn);
                        w.Cells["B" + row].PutValue(t.Account);
                        w.Cells["C" + row].PutValue(t.InvoiceNo);
                        w.Cells["D" + row].PutValue(t.InvoiceDate);
                        w.Cells["E" + row].PutValue(t.VoucherNo);
                        w.Cells["F" + row].PutValue(t.VoucherDate);
                        w.Cells["G" + row].PutValue(t.NoteType);
                        w.Cells["H" + row].PutValue(t.StateName);
                        w.Cells["I" + row].PutValue(t.BillAmount);
                        w.Cells["J" + row].PutValue("");
                        w.Cells["K" + row].PutValue(t.GSTRate);
                        w.Cells["L" + row].PutValue(t.TaxableValue);
                        w.Cells["M" + row].PutValue(t.Cess);
                        w.Cells["N" + row].PutValue("N");

                        row += 1;
                    }
                }

                ///////////////////////////////////CDNUR///////////////////////////////////////////////////////////////////////////////////
                //List = new ObservableCollection<GstDto>(_db.Database.SqlQuery<GstDto>(
                //    "dbo.GstReport @CompanyId={0},@TransTypeId={1},@TransTypeId1={2},@FromDate={3},@ToDate={4}", Convert.ToInt32(KontoGlobals.CompanyId), VoucherTypeEnum.DebitCreditNote, VoucherTypeEnum.SaleReturn, fdate, tdate).ToList());
                var cdnur = lst.Where(x => x.IsRevice == 0 && (x.Type != "REG" && x.Type != "CMP") && x.BillAmount >= 250000
            && x.VTypeId != 12).ToList(); ;
                w = wd.Workbook.Worksheets[9];
                row = 5;
                foreach (var t in cdnur)
                {
                    var type = "";
                    if (t.BillType == "SEZ Supplies with Payment")
                    {
                        type = "EXPWP";
                    }
                    else if (t.BillType == "SEZ Supplies without Payment")
                    {
                        type = "EXPWOP";
                    }
                    else
                    {
                        type = "B2CL";
                    }

                    w.Cells["A" + row].PutValue(type);
                    w.Cells["B" + row].PutValue(t.VoucherNo);
                    w.Cells["C" + row].PutValue(t.VoucherDate);
                    w.Cells["D" + row].PutValue(t.NoteType);
                    w.Cells["E" + row].PutValue(t.InvoiceNo);
                    w.Cells["F" + row].PutValue(t.InvoiceDate);
                    w.Cells["G" + row].PutValue(t.StateName);
                    w.Cells["H" + row].PutValue(t.BillAmount);
                    w.Cells["I" + row].PutValue("");
                    w.Cells["J" + row].PutValue(t.GSTRate);
                    w.Cells["K" + row].PutValue(t.TaxableValue);
                    w.Cells["L" + row].PutValue(t.Cess);
                    w.Cells["M" + row].PutValue("N");

                    row += 1;

                }

                ///////////////////////////////////EXP///////////////////////////////////////////////////////////////////////////////////
                //                List = new ObservableCollection<GstDto>(_db.Database.SqlQuery<GstDto>(
                //"dbo.GstReport @CompanyId={0},@TransTypeId={1},@FromDate={2},@ToDate={3}", Convert.ToInt32(KontoGlobals.CompanyId), VoucherTypeEnum.SaleInvoice, fdate, tdate).ToList());

                var exp = lst.Where(x => (x.BillType == "SEZ Supplies with Payment" || x.BillType == "SEZ Supplies without Payment"
                || x.BillType == "Deemed Exp")).ToList();

                w = wd.Workbook.Worksheets[11];
                row = 5;
                foreach (var t in exp)
                {
                    var type = "";
                    if (t.BillType == "SEZ Supplies with Payment")
                    {
                        type = "WPAY";
                    }
                    else if (t.BillType == "SEZ Supplies without Payment")
                    {
                        type = "WOPAY";
                    }
                    else
                    {
                        type = "WPAY";
                    }

                    w.Cells["A" + row].PutValue(type);
                    w.Cells["B" + row].PutValue(t.VoucherNo);
                    w.Cells["C" + row].PutValue(t.VoucherDate);
                    w.Cells["D" + row].PutValue(t.BillAmount);
                    w.Cells["E" + row].PutValue(t.PortCode);
                    w.Cells["F" + row].PutValue("");
                    w.Cells["G" + row].PutValue("");
                    w.Cells["H" + row].PutValue("");
                    w.Cells["I" + row].PutValue(t.GSTRate);
                    w.Cells["J" + row].PutValue(t.TaxableValue);
                    w.Cells["K" + row].PutValue(t.Cess);

                    row += 1;

                }



                ///////////////////////////////////HsnSummary///////////////////////////////////////////////////////////////////////////////////
                var hsn = _db.Database.SqlQuery<Gstr1HsnDto>(
                    "dbo.HsnSummary @CompanyId={0},@FromDate={1},@ToDate={2}",
                    Convert.ToInt32(KontoGlobals.CompanyId), fdate, tdate).ToList();

                string _uqc = "NA";

                if (System.Configuration.ConfigurationManager.AppSettings["Uqc"] != null)
                    _uqc = System.Configuration.ConfigurationManager.AppSettings["Uqc"];

                w = wd.Workbook.Worksheets[18];
                row = 5;
                foreach (var t in hsn)
                {
                    w.Cells["A" + row].PutValue(t.HsnCode);
                    w.Cells["B" + row].PutValue(t.Description);
                    
                    if(_uqc == "NA") // defaul uqc
                        w.Cells["C" + row].PutValue(t.Uqc);
                    else
                        w.Cells["C" + row].PutValue(_uqc);

                    w.Cells["D" + row].PutValue(t.TotalQty);
                    w.Cells["E" + row].PutValue(t.BillAmount);
                    w.Cells["F" + row].PutValue(t.TaxableValue);
                    w.Cells["G" + row].PutValue(t.IgstAmt);
                    w.Cells["H" + row].PutValue(t.CgstAmt);
                    w.Cells["I" + row].PutValue(t.SgstAmt);
                    w.Cells["J" + row].PutValue(t.Cess);

                    row += 1;

                }
                
                   
                   var doc = _db.Database.SqlQuery<gstr1_docDto>(
                          "dbo.gstr1_doc @fromdate={0},@todate={1},@companyid={2}",
                          fdate, tdate, KontoGlobals.CompanyId).ToList();

                w = wd.Workbook.Worksheets[19];
                row = 5;
                foreach (var t in doc)
                {
                    w.Cells["A" + row].PutValue(t.DocType);
                    w.Cells["B" + row].PutValue(t.StratBill);
                    w.Cells["C" + row].PutValue(t.EndBill);
                    w.Cells["D" + row].PutValue(t.Total);
                    w.Cells["E" + row].PutValue(t.TotalCancel);
                    row += 1;
                }
                    wd.Process(true);
                wd.Workbook.Save("gstr1.xlsx");

                DirectoryInfo info = new DirectoryInfo("gstr1.xlsx");
                string windir = Environment.GetEnvironmentVariable("WINDIR");
                Process prc = new Process();
                prc.StartInfo.FileName = windir + @"\explorer.exe";
                prc.StartInfo.Arguments = System.Windows.Forms.Application.StartupPath + "\\gstr1.xlsx";
                prc.Start();
            }
        }
        private void Gst3BSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists("gstr3.xlsx") && this.IsFileLocked(new FileInfo("gstr3.xlsx")))
                {
                    MessageBox.Show("File Already Open. Please Close and Download Again");
                    return;
                }
                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormDescription("Generateing Gsr3b...");
                List<Gst3bDto> lst = new List<Gst3bDto>();

                WorkbookDesigner wd = new WorkbookDesigner();
                wd.Workbook = new Aspose.Cells.Workbook("Excel\\GSTR3B_Excel_Utility_V4.1.xlsm");

                Worksheet w = wd.Workbook.Worksheets[1];

                int fyear = Convert.ToInt32(KontoGlobals.DFromDate.ToString("yyyy"));

                int tyear = Convert.ToInt32(KontoGlobals.DToDate.ToString("yyyy"));
                //int monthno = int.Parse(monthLookUpEdit.EditValue.ToString());
                int fdate = Convert.ToInt32(fromDateEdit.DateTime.ToString("yyyyMMdd"));
                int tdate = Convert.ToInt32(toDateEdit.DateTime.ToString("yyyyMMdd"));

                //if (monthno < 4)
                //{
                //    fdate = int.Parse(tyear.ToString() + monthLookUpEdit.EditValue.ToString() + "01");
                //    tdate = int.Parse(tyear.ToString() + monthLookUpEdit.EditValue.ToString() + DateTime.DaysInMonth(tyear, monthno).ToString());
                //}
                //else
                //{
                //    fdate = int.Parse(fyear.ToString() + monthLookUpEdit.EditValue.ToString() + "01");
                //    tdate = int.Parse(fyear.ToString() + monthLookUpEdit.EditValue.ToString() + DateTime.DaysInMonth(tyear, monthno).ToString());
                //}

                //  int typeid = 0;


                if (File.Exists("Excel\\GSTR3B_Excel_Utility_V4.1.xlsm"))
                {
                    KontoContext db = new KontoContext();
                    var comp = db.Companies.FirstOrDefault(k => k.Id == KontoGlobals.CompanyId);
                    if (comp != null)
                    {
                        var month = fromDateEdit.DateTime.ToString("MMMM");
                        w.Cells["C" + 5].PutValue(comp.GstIn);
                        w.Cells["C" + 6].PutValue(comp.CompName);
                        w.Cells["G" + 6].PutValue(month);
                    }
                    var year = db.FinYears.FirstOrDefault(k => k.Id == KontoGlobals.YearId);
                    if (year != null)
                    {
                        w.Cells["G" + 5].PutValue(year.YearCode);
                    }

                    using (var _db = new KontoContext())
                    {
                        _db.Database.CommandTimeout = 0;
                        lst = _db.Database.SqlQuery<Gst3bDto>(
                            "dbo.Gst3BReport @CompanyId={0},@FromDate={1},@ToDate={2},@YearId={3}",
                            Convert.ToInt32(KontoGlobals.CompanyId), fdate, tdate, KontoGlobals.YearId).ToList();
                    }
                    int row = 79;
                    foreach (var t in lst)
                    {
                        if (t.TransType == "Outward taxable supplies[Registeres]")
                        {
                            w.Cells["C" + 11].PutValue(t.TaxableValue);
                            w.Cells["D" + 11].PutValue(t.IGSTAmt);
                            w.Cells["E" + 11].PutValue(t.CGSTAmt);
                            w.Cells["F" + 11].PutValue(t.SGSTAmt);
                        }
                        if (t.TransType == "Outward taxable supplies (zero rated)")
                        {
                            w.Cells["C" + 12].PutValue(t.TaxableValue);
                            w.Cells["D" + 12].PutValue(t.IGSTAmt);
                            w.Cells["E" + 12].PutValue(t.CGSTAmt);
                            w.Cells["F" + 12].PutValue(t.SGSTAmt);
                        }
                        if (t.TransType == "Outward taxable supplies (Exempted)")
                        {
                            w.Cells["C" + 13].PutValue(t.TaxableValue);
                            w.Cells["D" + 13].PutValue(t.IGSTAmt);
                            w.Cells["E" + 13].PutValue(t.CGSTAmt);
                            w.Cells["F" + 13].PutValue(t.SGSTAmt);
                        }
                        if (t.TransType == "Inward supplies (liable to reverse charge)")
                        {
                            w.Cells["C" + 14].PutValue(t.TaxableValue);
                            w.Cells["D" + 14].PutValue(t.IGSTAmt);
                            w.Cells["E" + 14].PutValue(t.CGSTAmt);
                            w.Cells["F" + 14].PutValue(t.SGSTAmt);
                        }
                        if (t.TransType == "Import of goods")
                        {
                            w.Cells["C" + 22].PutValue(t.IGSTAmt);
                            w.Cells["F" + 22].PutValue(t.CmpIgst);
                        }
                        if (t.TransType == "Import of services")
                        {
                            w.Cells["C" + 23].PutValue(t.IGSTAmt);
                            w.Cells["F" + 23].PutValue(t.CmpIgst);
                        }
                        if (t.TransType == "Inward taxable supplies")
                        {
                            w.Cells["C" + 26].PutValue(t.IGSTAmt);
                            w.Cells["D" + 26].PutValue(t.CGSTAmt);
                            w.Cells["E" + 26].PutValue(t.SGSTAmt);
                        }
                        if (t.TransType == "From a supplier under composition scheme, Exempt and Nil rated supply")
                        {
                            w.Cells["D" + 39].PutValue(t.UrdTaxable);
                            w.Cells["E" + 39].PutValue(t.CmpTaxable);
                        }
                        if (t.TransType == "Non GST supply")
                        {
                            w.Cells["D" + 40].PutValue(t.UrdTaxable);
                            w.Cells["E" + 40].PutValue(t.CmpTaxable);
                        }
                        if (t.TransType == "Supplies made to Unregister Person")
                        {
                            w.Cells["B" + row].PutValue(t.StateName);
                            w.Cells["C" + row].PutValue(t.UrdTaxable);
                            w.Cells["D" + row].PutValue(t.UrdIgst);
                            w.Cells["E" + row].PutValue(t.CmpTaxable);
                            w.Cells["F" + row].PutValue(t.CmpIgst);

                            row += 1;
                        }
                    }

                   if(splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                    wd.Process(true);
                    wd.Workbook.Save("gstr3.xlsx");

                    DirectoryInfo info = new DirectoryInfo("gstr3.xlsx");
                    string windir = Environment.GetEnvironmentVariable("WINDIR");
                    Process prc = new Process();
                    prc.StartInfo.FileName = windir + @"\explorer.exe";
                    prc.StartInfo.Arguments = System.Windows.Forms.Application.StartupPath + "\\gstr3.xlsx";
                    prc.Start();
                }
            }
            catch (Exception ex)
            {

                if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                MessageBox.Show(ex.ToString());
            }
           
        }

        private void GstMainView_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage != null)
            {
                var tabcontrol = tabpage.Parent as TabControlAdv;
                if (tabcontrol != null)
                    tabcontrol.TabPages.Remove(tabpage);
            }
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
             this.Close();
             this.Dispose();
            //var frm = new PaymentAssist();
            //var _tab = this.Parent.Parent as TabControlAdv;
            //if (_tab == null) return;
            //var pg1 = new TabPageAdv();
            //pg1.Text = "Gst Payment Assist";
            //_tab.TabPages.Add(pg1);
            //_tab.SelectedTab = pg1;
            //frm.WindowState = FormWindowState.Maximized;
            //frm.TopLevel = false;
            //frm.Parent = pg1;
            //frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
            //frm.Show();// = true;
        }

        private void FillLookup()
        {
            //using (var db = new KontoContext())
            //{
            //    fnYearlookUpEdit.Properties.DataSource = db.FinYears.ToList().OrderByDescending(X => X.ToDate);
            //}

            //List<ComboBoxPairs> months = new List<ComboBoxPairs>
            //{
            //    new ComboBoxPairs("January","01"),
            //    new ComboBoxPairs("February","02"),
            //    new ComboBoxPairs("March","03"),
            //    new ComboBoxPairs("April","04"),
            //    new ComboBoxPairs("May","05"),
            //    new ComboBoxPairs("June","06"),
            //    new ComboBoxPairs("July","07"),
            //    new ComboBoxPairs("August","08"),
            //    new ComboBoxPairs("September","09"),
            //    new ComboBoxPairs("October","10"),
            //    new ComboBoxPairs("November","11"),
            //    new ComboBoxPairs("December","12")
            //};
            //monthLookUpEdit.Properties.DataSource = months;
        }
        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormDescription("Preparing Gst Register...");
               
                SetSpreadSheet();
                splashScreenManager1.CloseWaitForm();
            }
            catch (Exception ex)
            {
                if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                Log.Error(ex, "Gst Report");
                MessageBox.Show(ex.ToString());
            }
            
        }
        private void SetSpreadSheet()
        {
            
            FarPoint.Win.Spread.DefaultSpreadSkins.Office2016White.Apply(gridControl1);
            var sfir = new FarPoint.Win.Spread.SolidFocusIndicatorRenderer(Color.Red, 2);
            gridControl1.FocusRenderer = sfir;

            gridControl1.Sheets.Count = 15;

            GenerateData();

            var f = "excel\\GSTR3B_Excel_Utility_V4.1.xlsm";

            System.IO.FileStream s = new System.IO.FileStream(f, FileMode.Open, FileAccess.ReadWrite);

            gridControl1.Sheets[0].OpenExcel(f, 1);
            FarPoint.Win.Spread.SheetView newsheet = new FarPoint.Win.Spread.SheetView();
            s.Close();
            splashScreenManager1.SetWaitFormDescription("Generating 3b Register...");
            Gst3bShow();

            gridControl1.Sheets[1].SheetName = "B2B";
            gridControl1.Sheets[2].SheetName = "B2BA";
            gridControl1.Sheets[3].SheetName = "B2CL";
            gridControl1.Sheets[4].SheetName = "B2CLA";
            gridControl1.Sheets[5].SheetName = "B2CS";
            gridControl1.Sheets[6].SheetName = "B2CSA";
            gridControl1.Sheets[7].SheetName = "CDNR";
            gridControl1.Sheets[8].SheetName = "CDNRA";
            gridControl1.Sheets[9].SheetName = "CDNUR";
            gridControl1.Sheets[10].SheetName = "CDNURA";
            gridControl1.Sheets[11].SheetName = "EXP";
            gridControl1.Sheets[12].SheetName = "EXPA";
            gridControl1.Sheets[13].SheetName = "HSN";
            gridControl1.Sheets[14].SheetName = "DOC";

            splashScreenManager1.SetWaitFormDescription("Generating B2B Register...");
            Gen_B2B_Sheet();
            splashScreenManager1.SetWaitFormDescription("Generating CDNR Register...");
            CDNR_Sheet();
            splashScreenManager1.SetWaitFormDescription("Generating B2BA Register...");
            B2BA();
            splashScreenManager1.SetWaitFormDescription("Generating B2CL Register...");
            B2CL();
            splashScreenManager1.SetWaitFormDescription("Generating B2CLA Register...");
            B2CLA();
            splashScreenManager1.SetWaitFormDescription("Generating B2CS Register...");
            B2CS_Sheet();
            splashScreenManager1.SetWaitFormDescription("Generating B2CSA Register...");
            B2CSA();
            splashScreenManager1.SetWaitFormDescription("Generating CDNRA Register...");
            CDNRA_Sheet();
            splashScreenManager1.SetWaitFormDescription("Generating CDNUR Register...");
            CDNUR_Sheet();
            splashScreenManager1.SetWaitFormDescription("Generating CDNURA Register...");
            CDNURA_Sheet();
            splashScreenManager1.SetWaitFormDescription("Generating Export Register...");
            Export_Sheet();
            splashScreenManager1.SetWaitFormDescription("Generating ExportA Register...");
            ExpA_Sheet();
            splashScreenManager1.SetWaitFormDescription("Generating HSN Register...");
            HSN_Sheet();
            splashScreenManager1.SetWaitFormDescription("Generating DOC Register...");
            Doc_Sheet();
        }
        private void GenerateData()
        {
            // if (Gstrs.Count > 0) return;
            var List = new List<GstDto>();
            int fyear = Convert.ToInt32(KontoGlobals.DFromDate.ToString("yyyy"));

            int tyear = Convert.ToInt32(KontoGlobals.DToDate.ToString("yyyy"));
            // int monthno = int.Parse(monthLookUpEdit.EditValue.ToString());
            var fdate = Convert.ToInt32(fromDateEdit.DateTime.ToString("yyyyMMdd"));
            var tdate = Convert.ToInt32(toDateEdit.DateTime.ToString("yyyyMMdd"));

            //if (monthno < 4)
            //{
            //    fdate = int.Parse(tyear.ToString() + monthLookUpEdit.EditValue.ToString() + "01");
            //    tdate = int.Parse(tyear.ToString() + monthLookUpEdit.EditValue.ToString() + DateTime.DaysInMonth(tyear, monthno).ToString());
            //}
            //else
            //{
            //    fdate = int.Parse(fyear.ToString() + monthLookUpEdit.EditValue.ToString() + "01");
            //    tdate = int.Parse(fyear.ToString() + monthLookUpEdit.EditValue.ToString() + DateTime.DaysInMonth(tyear, monthno).ToString());
            //}

            using (var _db = new KontoContext())
            {
                _db.Database.CommandTimeout = 0;
                Gstrs = _db.Database.SqlQuery<GstDto>(
                    "dbo.GstReport @CompanyId={0},@FromDate={1},@ToDate={2},@yearid={3}",
                    Convert.ToInt32(KontoGlobals.CompanyId), fdate, tdate, KontoGlobals.YearId).ToList();

                GstrB2CS = _db.Database.SqlQuery<Gstrb2csDto>(
                  "dbo.GstReportBtocs @CompanyId={0},@yearid={1},@FromDate={2},@ToDate={3}", Convert.ToInt32(KontoGlobals.CompanyId), KontoGlobals.YearId, fdate, tdate).ToList();
            }
        }

        private void Gst3bShow()
        {
            int fyear = Convert.ToInt32(KontoGlobals.DFromDate.ToString("yyyy"));

            int tyear = Convert.ToInt32(KontoGlobals.DToDate.ToString("yyyy"));
            //int monthno = int.Parse(monthLookUpEdit.EditValue.ToString());
            var fdate = Convert.ToInt32(fromDateEdit.DateTime.ToString("yyyyMMdd"));
            var tdate = Convert.ToInt32(toDateEdit.DateTime.ToString("yyyyMMdd"));

            //if (monthno < 4)
            //{
            //    fdate = int.Parse(tyear.ToString() + monthLookUpEdit.EditValue.ToString() + "01");
            //    tdate = int.Parse(tyear.ToString() + monthLookUpEdit.EditValue.ToString() + DateTime.DaysInMonth(tyear, monthno).ToString());
            //}
            //else
            //{
            //    fdate = int.Parse(fyear.ToString() + monthLookUpEdit.EditValue.ToString() + "01");
            //    tdate = int.Parse(fyear.ToString() + monthLookUpEdit.EditValue.ToString() + DateTime.DaysInMonth(tyear, monthno).ToString());
            //}


            var w = gridControl1.Sheets[0];

            //w.OperationMode = OperationMode.ReadOnly;
            w.Cells[0, 0, 116, 8].Locked = true;
            //  w.Protect = true;
            KontoContext db = new KontoContext();
            var comp = db.Companies.FirstOrDefault(k => k.Id == KontoGlobals.CompanyId);
            var List = new List<Gst3bDto>();
            if (comp != null)
            {
                // var month = FromDate.ToString("MMMM");
                var month = fromDateEdit.DateTime.ToString("MMMM");
                w.Cells["C" + 5].Value = comp.GstIn;
                w.Cells["C" + 6].Value = comp.CompName;
                w.Cells["G" + 6].Value = month;
            }
            var year = db.FinYears.FirstOrDefault(k => k.Id == KontoGlobals.YearId);
            if (year != null)
            {
                w.Cells["G" + 5].Value = year.YearCode;
            }

            using (var _db = new KontoContext())
            {
                _db.Database.CommandTimeout = 0;
                List = _db.Database.SqlQuery<Gst3bDto>(
                    "dbo.Gst3BReport @CompanyId={0},@FromDate={1},@ToDate={2},@YearId={3}", Convert.ToInt32(KontoGlobals.CompanyId), fdate, tdate, KontoGlobals.YearId).ToList();
            }

            int row = 79;
            foreach (var t in List)
            {
                if (t.TransType == "Outward taxable supplies[Registeres]")
                {
                    w.Cells["C" + 11].Value = t.TaxableValue;
                    w.Cells["D" + 11].Value = t.IGSTAmt;
                    w.Cells["E" + 11].Value = t.CGSTAmt;
                    w.Cells["F" + 11].Value = t.SGSTAmt;
                }
                if (t.TransType == "Outward taxable supplies (zero rated)")
                {
                    w.Cells["C" + 12].Value = t.TaxableValue;
                    w.Cells["D" + 12].Value = t.IGSTAmt;
                    w.Cells["E" + 12].Value = t.CGSTAmt;
                    w.Cells["F" + 12].Value = t.SGSTAmt;
                }
                if (t.TransType == "Outward taxable supplies (Exempted)")
                {
                    w.Cells["C" + 13].Value = t.TaxableValue;
                    w.Cells["D" + 13].Value = t.IGSTAmt;
                    w.Cells["E" + 13].Value = t.CGSTAmt;
                    w.Cells["F" + 13].Value = t.SGSTAmt;
                }
                if (t.TransType == "Inward supplies (liable to reverse charge)")
                {
                    w.Cells["C" + 14].Value = t.TaxableValue;
                    w.Cells["D" + 14].Value = t.IGSTAmt;
                    w.Cells["E" + 14].Value = t.CGSTAmt;
                    w.Cells["F" + 14].Value = t.SGSTAmt;
                }
                if (t.TransType == "Import of goods")
                {
                    w.Cells["C" + 22].Value = t.IGSTAmt;
                    w.Cells["F" + 22].Value = t.CmpIgst;
                }
                if (t.TransType == "Import of services")
                {
                    w.Cells["C" + 23].Value = t.IGSTAmt;
                    w.Cells["F" + 23].Value = t.CmpIgst;
                }
                if (t.TransType == "Inward taxable supplies")
                {
                    w.Cells["C" + 26].Value = t.IGSTAmt;
                    w.Cells["D" + 26].Value = t.CGSTAmt;
                    w.Cells["E" + 26].Value = t.SGSTAmt;
                }
                if (t.TransType == "From a supplier under composition scheme, Exempt and Nil rated supply")
                {
                    w.Cells["D" + 39].Value = t.UrdTaxable;
                    w.Cells["E" + 39].Value = t.CmpTaxable;
                }
                if (t.TransType == "Non GST supply")
                {
                    w.Cells["D" + 40].Value = t.UrdTaxable;
                    w.Cells["E" + 40].Value = t.CmpTaxable;
                }
                if (t.TransType == "Supplies made to Unregister Person")
                {
                    w.Cells["B" + row].Value = t.StateName;
                    w.Cells["C" + row].Value = t.UrdTaxable;
                    w.Cells["D" + row].Value = t.UrdIgst;
                    w.Cells["E" + row].Value = t.CmpTaxable;
                    w.Cells["F" + row].Value = t.CmpIgst;

                    row += 1;
                }

            }

        }
        private void Gen_B2B_Sheet()
        {
            var b2b = gridControl1.Sheets[1];
            b2b.ColumnCount = 15;
            b2b.ColumnHeader.Rows[0].Height = 35;
            b2b.ColumnHeader.Cells[0, 0].Value = "GSTIN/UIN of Recipient";
            b2b.ColumnHeader.Cells[0, 1].Value = "Receiver Name";
            b2b.ColumnHeader.Cells[0, 2].Value = "Invoice Number";
            b2b.ColumnHeader.Cells[0, 3].Value = "Invoice Date";
            b2b.ColumnHeader.Cells[0, 4].Value = "Invoice Value";
            b2b.ColumnHeader.Cells[0, 5].Value = "Place of Supply";
            b2b.ColumnHeader.Cells[0, 6].Value = "Reverse Charge";
            b2b.ColumnHeader.Cells[0, 7].Value = "Applicable % of Tax Rate";
            b2b.ColumnHeader.Cells[0, 8].Value = "Invoice Type";
            b2b.ColumnHeader.Cells[0, 9].Value = "E-Commerce GSTIN";
            b2b.ColumnHeader.Cells[0, 10].Value = "Rate";
            b2b.ColumnHeader.Cells[0, 11].Value = "Taxable Value";
            b2b.ColumnHeader.Cells[0, 12].Value = "Cess Amount";
            b2b.ColumnFooter.Cells[0, 0, 0, 12].BackColor = ColorTranslator.FromHtml("#16A5DC");
            b2b.ColumnHeader.Cells[0, 0, 0, 12].BackColor = ColorTranslator.FromHtml("#16A5DC");
            b2b.ColumnFooter.Cells[0, 0, 0, 12].ForeColor = Color.White;
            b2b.ColumnHeader.Cells[0, 0, 0, 12].ForeColor = Color.White;
            b2b.ColumnFooter.Visible = true;
            b2b.ColumnFooter.RowCount = 1;
            FarPoint.Win.Spread.CellType.DateTimeCellType datecell = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            datecell.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.ShortDate;
            b2b.Columns[3].CellType = datecell;
            FarPoint.Win.Spread.CellType.NumberCellType nmbrcell = new FarPoint.Win.Spread.CellType.NumberCellType();
            nmbrcell.DecimalPlaces = 2;
            b2b.Columns[11].CellType = nmbrcell;
            b2b.Columns[4].CellType = nmbrcell;

            b2b.OperationMode = OperationMode.Normal;
            b2b.Protect = true;

            var w = gridControl1.Sheets[1];

            w.Columns[0].Width = 125;
            w.Columns[1].Width = 200;
            w.Columns[2].Width = 75;
            w.Columns[3].Width = 75;
            w.Columns[4].Width = 100; //invoice
            w.Columns[5].Width = 100;
            w.Columns[6].Width = 70;
            w.Columns[7].Width = 100;
            w.Columns[8].Width = 70;
            w.Columns[9].Width = 100;
            w.Columns[11].Width = 100;
            w.Columns[12].Width = 70;
            w.Columns[13].Width = 0;
            w.Columns[14].Width = 0;

            int row = 1;

            var b2b1 = Gstrs.Where(x => x.IsRevice == 0  && x.VTypeId == 12 && (x.Type=="REG" || x.Type=="CMP")).ToList();

            w.RowCount = row;
            foreach (var t in b2b1)
            {
                w.Cells["A" + row].Value = t.GstIn;//"GSTIN/UIN of Recipient";
                w.Cells["B" + row].Value = t.Account;// "Receiver Name";
                w.Cells["C" + row].Value = t.VoucherNo;// "Invoice Number";
                w.Cells["D" + row].Value = t.VoucherDate;//"Invoice Date";
                w.Cells["E" + row].Value = t.BillAmount;// "Invoice Value";
                w.Cells["F" + row].Value = t.StateName;//"Place of Supply";
                w.Cells["G" + row].Value = "N";//"Reverse Charge"; 
                                               //  w.Cells["H" + row].Value = t.GSTRate;// "Applicable % of Tax Rate";
                w.Cells["I" + row].Value = t.BillType;//"Invoice Type";
                w.Cells["J" + row].Value = "";// "E-Commerce GSTIN";
                w.Cells["K" + row].Value = t.GSTRate;// "Rate";
                w.Cells["L" + row].Value = t.TaxableValue;//"Taxable Value";
                w.Cells["M" + row].Value = t.Cess;// "Cess Amount";
                w.Cells["N" + row].Value = t.VTypeId;
                w.Cells["O" + row].Value = t.Id;
                w.SetRowHeight(row, 25);
                row += 1;
                w.RowCount = row;
            }
            w.Cells[0, 0, row - 1, 12].Locked = true;

            gridControl1.Sheets[1].SetColumnAllowFilter(1, 12, true);
            w.AutoFilterMode = AutoFilterMode.EnhancedContextMenu;
            w.ColumnFooter.SetAggregationType(0, 11, FarPoint.Win.Spread.Model.AggregationType.Sum);
            w.ColumnFooter.Cells[0, 11].CellType = nmbrcell;
            w.ColumnFooter.Cells[0, 11].Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);

        }
        private void B2BA()
        {
            var b2ba = gridControl1.Sheets[2];
            b2ba.ColumnCount = 17;
            b2ba.ColumnHeader.RowCount = 2;

            b2ba.ColumnHeader.Rows[1].Height = 35;
            b2ba.ColumnHeader.Cells[0, 1].ColumnSpan = 3;
            b2ba.ColumnHeader.Cells[0, 2].Value = "Original Details";
            b2ba.ColumnHeader.Cells[0, 4].Value = "Revised Details";
            b2ba.ColumnHeader.Cells[0, 4].ColumnSpan = 8;
            //b2b.ColumnHeader.DefaultStyle.ra = true;
            b2ba.ColumnHeader.Cells[1, 0].Value = "GSTIN/UIN of Recipient";
            b2ba.ColumnHeader.Cells[1, 1].Value = "Receiver Name";
            b2ba.ColumnHeader.Cells[1, 2].Value = "Original Invoice Number";
            b2ba.ColumnHeader.Cells[1, 3].Value = "Original Invoice date";

            b2ba.ColumnHeader.Cells[1, 4].Value = "Revised Invoice Number";
            b2ba.ColumnHeader.Cells[1, 5].Value = "Revised Invoice Date";
            b2ba.ColumnHeader.Cells[1, 6].Value = "Invoice Value";
            b2ba.ColumnHeader.Cells[1, 7].Value = "Place of Supply";
            b2ba.ColumnHeader.Cells[1, 8].Value = "Reverse Charge";
            b2ba.ColumnHeader.Cells[1, 9].Value = "Applicable % of Tax Rate";
            b2ba.ColumnHeader.Cells[1, 10].Value = "Invoice Type";
            b2ba.ColumnHeader.Cells[1, 11].Value = "E-Commerce GSTIN";
            b2ba.ColumnHeader.Cells[1, 12].Value = "Rate";
            b2ba.ColumnHeader.Cells[1, 13].Value = "Taxable Value";
            b2ba.ColumnHeader.Cells[1, 14].Value = "Cess Amount";
            b2ba.ColumnFooter.Cells[0, 0, 0, 14].BackColor = ColorTranslator.FromHtml("#16A5DC");
            b2ba.ColumnHeader.Cells[0, 0, 0, 14].BackColor = ColorTranslator.FromHtml("#16A5DC");
            b2ba.ColumnFooter.Cells[0, 0, 0, 14].ForeColor = Color.White;
            b2ba.ColumnHeader.Cells[0, 0, 0, 14].ForeColor = Color.White;
            b2ba.ColumnFooter.Visible = true;
            b2ba.ColumnFooter.RowCount = 1;

            FarPoint.Win.Spread.CellType.NumberCellType nmbrcell = new FarPoint.Win.Spread.CellType.NumberCellType();
            nmbrcell.DecimalPlaces = 2;

            FarPoint.Win.Spread.CellType.DateTimeCellType datecell = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            datecell.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.ShortDate;

            b2ba.Columns[3].CellType = datecell;
            b2ba.Columns[5].CellType = datecell;

            b2ba.OperationMode = OperationMode.Normal;
            b2ba.Protect = true;

            var w = gridControl1.Sheets[2];

            w.Columns[0].Width = 125;
            w.Columns[1].Width = 120;
            w.Columns[2].Width = 125;
            w.Columns[3].Width = 95;
            w.Columns[4].Width = 125;
            w.Columns[5].Width = 95;
            w.Columns[6].Width = 100;
            w.Columns[7].Width = 100;
            w.Columns[8].Width = 90;
            w.Columns[9].Width = 120;
            w.Columns[10].Width = 90;
            w.Columns[11].Width = 120;
            w.Columns[12].Width = 90;
            w.Columns[13].Width = 90;
            w.Columns[14].Width = 90;
            w.Columns[15].Width = 0;
            w.Columns[16].Width = 0;

            int row = 1;

            var b2b1 = Gstrs.Where(x => x.IsRevice == 1 && (x.GstIn != null || x.GstIn.Length == 15) && (x.Type == "REG" || x.Type == "CMP") && x.VTypeId == 12).ToList();

            w.RowCount = row;
            foreach (var t in b2b1)
            {
                w.Cells["A" + row].Value = t.GstIn;//"GSTIN/UIN of Recipient";
                w.Cells["B" + row].Value = t.Account;//"Receiver Name";
                w.Cells["C" + row].Value = t.OrgBillNo;//Original Invoice Number";
                w.Cells["D" + row].Value = t.OrgBillDate;//Org Bill Date
                w.Cells["E" + row].Value = t.VoucherNo;//Revised Invoice Number";
                w.Cells["F" + row].Value = t.VoucherDate;// Bill Date 
                w.Cells["G" + row].Value = t.BillAmount;//"Invoice Value";
                w.Cells["H" + row].Value = t.StateName;//"Place of Supply";
                w.Cells["I" + row].Value = "";// "Reverse Charge";
                w.Cells["J" + row].Value = "";//"Applicable % of Tax Rate";

                w.Cells["K" + row].Value = t.BillType;// "Invoice Type"; 
                w.Cells["L" + row].Value = "";//"E-Commerce GSTIN";
                w.Cells["M" + row].Value = t.GSTRate;//Rate";
                w.Cells["N" + row].Value = t.TaxableValue;//"Taxable Value";
                w.Cells["O" + row].Value = t.Cess;//"Cess Amount";
                w.Cells["P" + row].Value = t.VTypeId;
                w.Cells["Q" + row].Value = t.Id;

                w.SetRowHeight(row, 25);
                row += 1;
                w.RowCount = row;
            }
            w.Cells[0, 0, row - 1, 12].Locked = true;

            gridControl1.Sheets[2].SetColumnAllowFilter(1, 12, true);
            w.AutoFilterMode = AutoFilterMode.EnhancedContextMenu;
            w.ColumnFooter.SetAggregationType(0, 6, FarPoint.Win.Spread.Model.AggregationType.Sum);
            w.ColumnFooter.SetAggregationType(0, 13, FarPoint.Win.Spread.Model.AggregationType.Sum);
            w.ColumnFooter.Cells[0, 7].CellType = nmbrcell;
            w.ColumnFooter.Cells[0, 7].Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);

        }
        private void B2CL()
        {
            var b2cl = gridControl1.Sheets[3];
            b2cl.ColumnCount = 12;
            b2cl.ColumnHeader.RowCount = 1;

            b2cl.ColumnHeader.Rows[0].Height = 35;
            //b2b.ColumnHeader.DefaultStyle.ra = true;
            b2cl.ColumnHeader.Cells[0, 0].Value = "Invoice Number";
            b2cl.ColumnHeader.Cells[0, 1].Value = "Invoice date";
            b2cl.ColumnHeader.Cells[0, 2].Value = "Invoice Value";
            b2cl.ColumnHeader.Cells[0, 3].Value = "Place Of Supply";

            b2cl.ColumnHeader.Cells[0, 4].Value = "Applicable % of Tax Rate";
            b2cl.ColumnHeader.Cells[0, 5].Value = "Rate";
            b2cl.ColumnHeader.Cells[0, 6].Value = "Taxable Value";
            b2cl.ColumnHeader.Cells[0, 7].Value = "Cess Amount";
            b2cl.ColumnHeader.Cells[0, 8].Value = "E-Commerce GSTIN";
            b2cl.ColumnHeader.Cells[0, 9].Value = "Sale from Bonded WH";
            b2cl.ColumnFooter.Cells[0, 0, 0, 9].BackColor = ColorTranslator.FromHtml("#16A5DC");
            b2cl.ColumnHeader.Cells[0, 0, 0, 9].BackColor = ColorTranslator.FromHtml("#16A5DC");
            b2cl.ColumnFooter.Cells[0, 0, 0, 9].ForeColor = Color.White;
            b2cl.ColumnHeader.Cells[0, 0, 0, 9].ForeColor = Color.White;
            b2cl.ColumnFooter.Visible = true;
            b2cl.ColumnFooter.RowCount = 1;

            FarPoint.Win.Spread.CellType.DateTimeCellType datecell = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            datecell.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.ShortDate;

            b2cl.Columns[1].CellType = datecell;
            FarPoint.Win.Spread.CellType.NumberCellType nmbrcell = new FarPoint.Win.Spread.CellType.NumberCellType();
            nmbrcell.DecimalPlaces = 2;
            b2cl.Columns[2].CellType = nmbrcell;

            b2cl.OperationMode = OperationMode.Normal;
            b2cl.Protect = true;

            var b2b1 = Gstrs.Where(x => x.IsRevice == 0  && x.BillAmount >= 250000 && x.IGSTAmt > 0
                       && (x.Type != "REG" && x.Type !="CMP") && x.VTypeId == 12).ToList();

            var w = gridControl1.Sheets[3];

            w.Columns[0].Width = 125;
            w.Columns[1].Width = 120;
            w.Columns[2].Width = 125;
            w.Columns[3].Width = 95;
            w.Columns[4].Width = 125;
            w.Columns[5].Width = 95;
            w.Columns[6].Width = 100;
            w.Columns[7].Width = 100;
            w.Columns[8].Width = 100;
            w.Columns[9].Width = 120;
            w.Columns[10].Width = 0;
            w.Columns[11].Width = 0;
            //w.Columns[12].Width = 90;
            //w.Columns[13].Width = 90;
            //w.Columns[14].Width = 90;

            int row = 1;

            w.RowCount = row;
            foreach (var t in b2b1)
            {
                w.Cells["A" + row].Value = t.VoucherNo; //"Invoice Number";
                w.Cells["B" + row].Value = t.VoucherDate; //"Invoice date";
                w.Cells["C" + row].Value = t.BillAmount;//"Invoice Value";
                w.Cells["D" + row].Value = t.StateName;//Place Of Supply";
                w.Cells["E" + row].Value = "";//"Applicable % of Tax Rate";
                w.Cells["F" + row].Value = t.GSTRate;// "Rate";
                w.Cells["G" + row].Value = t.TaxableValue;//"Taxable Value";
                w.Cells["H" + row].Value = t.Cess;// "Cess Amount";
                w.Cells["I" + row].Value = "";//"E-Commerce GSTIN";
                w.Cells["J" + row].Value = t.BondedWH;// "Sale from Bonded WH";
                w.Cells["K" + row].Value = t.VTypeId;// "Sale from Bonded WH";
                w.Cells["L" + row].Value = t.Id;// "Sale from Bonded WH";

                w.SetRowHeight(row, 25);
                row += 1;
                w.RowCount = row;
            }
            //w.Cells[0, 0, row - 1, 12].Locked = true;

            gridControl1.Sheets[3].SetColumnAllowFilter(1, 9, true);
            w.AutoFilterMode = AutoFilterMode.EnhancedContextMenu;
            w.ColumnFooter.SetAggregationType(0, 2, FarPoint.Win.Spread.Model.AggregationType.Sum);
            w.ColumnFooter.Cells[0, 2].CellType = nmbrcell;
            w.ColumnFooter.Cells[0, 2].Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
        }
        private void B2CLA()
        {
            var b2cla = gridControl1.Sheets[4];
            b2cla.ColumnCount = 14;
            b2cla.ColumnHeader.RowCount = 2;

            b2cla.ColumnHeader.Rows[1].Height = 35;
            b2cla.ColumnHeader.Cells[0, 1].ColumnSpan = 3;
            b2cla.ColumnHeader.Cells[0, 2].Value = "Original Details";
            b2cla.ColumnHeader.Cells[0, 4].Value = "Revised Details";
            b2cla.ColumnHeader.Cells[0, 4].ColumnSpan = 8;

            b2cla.ColumnHeader.Cells[1, 0].Value = "Original Invoice Number";
            b2cla.ColumnHeader.Cells[1, 1].Value = "Original Invoice date";
            b2cla.ColumnHeader.Cells[1, 2].Value = "Original Place Of Supply";
            b2cla.ColumnHeader.Cells[1, 3].Value = "Revised Invoice Number";

            b2cla.ColumnHeader.Cells[1, 4].Value = "Revised Invoice date";
            b2cla.ColumnHeader.Cells[1, 5].Value = "Invoice Value";
            b2cla.ColumnHeader.Cells[1, 6].Value = "Applicable % of Tax Rate";
            b2cla.ColumnHeader.Cells[1, 7].Value = "Rate";
            b2cla.ColumnHeader.Cells[1, 8].Value = "Taxable Value";
            b2cla.ColumnHeader.Cells[1, 9].Value = "Cess Amount";
            b2cla.ColumnHeader.Cells[1, 10].Value = "E-Commerce GSTIN";
            b2cla.ColumnHeader.Cells[1, 11].Value = "Sale from Bonded WH";
            b2cla.ColumnFooter.Cells[0, 0, 0, 12].BackColor = ColorTranslator.FromHtml("#16A5DC");
            b2cla.ColumnHeader.Cells[0, 0, 0, 12].BackColor = ColorTranslator.FromHtml("#16A5DC");
            b2cla.ColumnFooter.Cells[0, 0, 0, 12].ForeColor = Color.White;
            b2cla.ColumnHeader.Cells[0, 0, 0, 12].ForeColor = Color.White;
            b2cla.ColumnFooter.Visible = true;
            b2cla.ColumnFooter.RowCount = 1;

            FarPoint.Win.Spread.CellType.DateTimeCellType datecell = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            datecell.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.ShortDate;

            FarPoint.Win.Spread.CellType.NumberCellType nmbrcell = new FarPoint.Win.Spread.CellType.NumberCellType();
            nmbrcell.DecimalPlaces = 2;

            b2cla.Columns[1].CellType = datecell;
            b2cla.Columns[4].CellType = datecell;
            b2cla.Columns[5].CellType = nmbrcell;
            b2cla.Columns[8].CellType = nmbrcell;

            b2cla.OperationMode = OperationMode.Normal;
            b2cla.Protect = true;

            var w = gridControl1.Sheets[4];

            w.Columns[0].Width = 125;
            w.Columns[1].Width = 125;
            w.Columns[2].Width = 120;
            w.Columns[3].Width = 120;
            w.Columns[4].Width = 100;
            w.Columns[5].Width = 100;
            w.Columns[6].Width = 100;
            w.Columns[7].Width = 100;
            w.Columns[8].Width = 100;
            w.Columns[9].Width = 100;
            w.Columns[10].Width = 100;
            w.Columns[11].Width = 100;
            w.Columns[12].Width = 0;
            w.Columns[13].Width = 0;

            int row = 1;

            var b2b1 = Gstrs.Where(x => x.IsRevice == 1 && (x.GstIn != null || x.GstIn.Length == 15) && x.BillAmount > 250000 && x.IGSTAmt > 0
                     && (x.Type == "REG" || x.Type == "CMP") && x.VTypeId == 12).ToList();

            w.RowCount = row;
            foreach (var t in b2b1)
            {
                w.Cells["A" + row].Value = t.OrgBillNo; //"Original Invoice Number";
                w.Cells["B" + row].Value = t.OrgBillDate;//"Original Invoice date";
                w.Cells["C" + row].Value = t.OrgStateName;// "Original Place Of Supply";
                w.Cells["D" + row].Value = t.VoucherNo;// "Revised Invoice Number";
                w.Cells["E" + row].Value = t.VoucherDate;// "Revised Invoice date";;
                w.Cells["F" + row].Value = t.BillAmount;//""Invoice Value";
                w.Cells["G" + row].Value = "";//"Applicable % of Tax Rate";
                w.Cells["H" + row].Value = t.GSTRate;// "Rate";
                w.Cells["I" + row].Value = t.TaxableValue;//"Taxable Value";
                w.Cells["J" + row].Value = t.Cess;// "Cess Amount";
                w.Cells["K" + row].Value = "";// "E-Commerce GSTIN";
                w.Cells["L" + row].Value = t.BondedWH;//""Sale from Bonded WH";
                w.Cells["M" + row].Value = t.VTypeId;//""Sale from Bonded WH";
                w.Cells["N" + row].Value = t.Id;//""Sale from Bonded WH";

                w.SetRowHeight(row, 25);
                row += 1;
                w.RowCount = row;
            }
            w.Cells[0, 0, row - 1, 11].Locked = true;

            gridControl1.Sheets[4].SetColumnAllowFilter(1, 12, true);
            w.AutoFilterMode = AutoFilterMode.EnhancedContextMenu;
            w.ColumnFooter.SetAggregationType(0, 5, FarPoint.Win.Spread.Model.AggregationType.Sum);
            w.ColumnFooter.Cells[0, 5].CellType = nmbrcell;
            w.ColumnFooter.Cells[0, 5].Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);

            w.ColumnFooter.SetAggregationType(0, 8, FarPoint.Win.Spread.Model.AggregationType.Sum);
            w.ColumnFooter.Cells[0, 8].CellType = nmbrcell;
            w.ColumnFooter.Cells[0, 8].Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
        }
        void B2CS_Sheet()
        {
            // business to consumer B2CS
            var b2cs = gridControl1.Sheets[5];
            b2cs.ColumnCount = 7;
            b2cs.ColumnHeader.RowCount = 1;
            b2cs.ColumnHeader.Cells[0, 0].Value = "Type";
            b2cs.ColumnHeader.Cells[0, 1].Value = "Place Of Supply";
            b2cs.ColumnHeader.Cells[0, 2].Value = "Applicable % of Tax Rate";
            b2cs.ColumnHeader.Cells[0, 3].Value = "Rate";

            b2cs.ColumnHeader.Cells[0, 4].Value = "Taxable Value";
            b2cs.ColumnHeader.Cells[0, 5].Value = "Cess Amount";
            b2cs.ColumnHeader.Cells[0, 6].Value = "E-Commerce GSTIN";
            b2cs.ColumnFooter.Cells[0, 0, 0, 6].BackColor = ColorTranslator.FromHtml("#16A5DC");
            b2cs.ColumnHeader.Cells[0, 0, 0, 6].BackColor = ColorTranslator.FromHtml("#16A5DC");
            b2cs.ColumnFooter.Cells[0, 0, 0, 6].ForeColor = Color.White;
            b2cs.ColumnHeader.Cells[0, 0, 0, 6].ForeColor = Color.White;
            b2cs.ColumnFooter.Visible = true;
            b2cs.ColumnFooter.RowCount = 1;
            b2cs.ColumnHeader.Rows[0].Height = 35;
            
            b2cs.OperationMode = OperationMode.Normal;
            b2cs.Protect = true;

            var wcs = gridControl1.Sheets[5]; // b2cs;

            wcs.Columns[0].Width = 125;//"Type";
            wcs.Columns[1].Width = 125;//"Place Of Supply";
            wcs.Columns[2].Width = 120;// "Applicable % of Tax Rate";
            wcs.Columns[3].Width = 120;//"Rate";
            wcs.Columns[4].Width = 100;//"Taxable Value";
            wcs.Columns[5].Width = 100;// "Cess Amount";
            wcs.Columns[6].Width = 100;//"E-Commerce GSTIN";

            int row = 1;
            var b2b1 = GstrB2CS.Where(x => x.IsRevice == 0).ToList();

            wcs.RowCount = row;
            foreach (var t in b2b1)
            {
                wcs.Cells["A" + row].Value = t.Type;//"Type";
                wcs.Cells["B" + row].Value = t.StateName;//"Place Of Supply";
                wcs.Cells["C" + row].Value = "";// "Applicable % of Tax Rate";
                wcs.Cells["D" + row].Value = t.GSTRate;//"Rate";
                wcs.Cells["E" + row].Value = t.TaxableValue;//"Taxable Value";
                wcs.Cells["F" + row].Value = t.Cess;// "Cess Amount";
                wcs.Cells["G" + row].Value = t.GstIn;//"E-Commerce GSTIN";

                row += 1;
                wcs.RowCount = row;
            }
            FarPoint.Win.Spread.CellType.NumberCellType nmbrcell = new FarPoint.Win.Spread.CellType.NumberCellType();
            nmbrcell.DecimalPlaces = 2;

            wcs.Cells[0, 0, row - 1, 6].Locked = true;
            gridControl1.Sheets[5].SetColumnAllowFilter(1, 6, true);
            wcs.AutoFilterMode = AutoFilterMode.EnhancedContextMenu;
            wcs.ColumnFooter.SetAggregationType(0, 4, FarPoint.Win.Spread.Model.AggregationType.Sum);
            wcs.ColumnFooter.Cells[0, 4].CellType = nmbrcell;
            wcs.ColumnFooter.Cells[0, 4].Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            //foreach (FarPoint.Win.Spread.Column col in wcs.Columns)
            //{
            //    col.Width = col.GetPreferredWidth();
            //    col.AllowAutoSort = true;
            //}
        }
        private void B2CSA()
        {
            // business to consumer B2CSA - amendment
            var b2csa = gridControl1.Sheets[6];
            b2csa.ColumnCount = 9;
            b2csa.ColumnHeader.RowCount = 1;
            b2csa.ColumnHeader.Cells[0, 0].Value = "Financial Year";
            b2csa.ColumnHeader.Cells[0, 1].Value = "Original Month";
            b2csa.ColumnHeader.Cells[0, 2].Value = "Place Of Supply";
            b2csa.ColumnHeader.Cells[0, 3].Value = "Type";
            b2csa.ColumnHeader.Cells[0, 4].Value = "Applicable % of Tax Rate";
            b2csa.ColumnHeader.Cells[0, 5].Value = "Rate";
            b2csa.ColumnHeader.Cells[0, 6].Value = "Taxable Value";
            b2csa.ColumnHeader.Cells[0, 7].Value = "Cess Amount";
            b2csa.ColumnHeader.Cells[0, 8].Value = "E-Commerce GSTIN";
            b2csa.ColumnFooter.Cells[0, 0, 0, 8].BackColor = ColorTranslator.FromHtml("#16A5DC");
            b2csa.ColumnHeader.Cells[0, 0, 0, 8].BackColor = ColorTranslator.FromHtml("#16A5DC");
            b2csa.ColumnFooter.Cells[0, 0, 0, 8].ForeColor = Color.White;
            b2csa.ColumnHeader.Cells[0, 0, 0, 8].ForeColor = Color.White;
            b2csa.ColumnFooter.Visible = true;
            b2csa.ColumnFooter.RowCount = 1;
            b2csa.ColumnHeader.Rows[0].Height = 35;

            var w = gridControl1.Sheets[6];

            w.Columns[0].Width = 100;
            w.Columns[1].Width = 100;
            w.Columns[2].Width = 105;
            w.Columns[3].Width = 75;
            w.Columns[4].Width = 100;
            w.Columns[5].Width = 100;
            w.Columns[6].Width = 70;
            w.Columns[7].Width = 100;
            w.Columns[8].Width = 100;

            int row = 1;

            var b2b1 = GstrB2CS.Where(x => x.IsRevice == 1).ToList();

            w.RowCount = row;
            foreach (var t in b2b1)
            {
                //w.Cells["A" + row].Value = t.ye;// "Financial Year";
                //w.Cells["B" + row].Value = t.mo;// "Original Month";
                w.Cells["C" + row].Value = t.StateName;//  "Place Of Supply";
                w.Cells["D" + row].Value = t.Type;//"Type";
                w.Cells["E" + row].Value = "";// "Applicable % of Tax Rate";
                w.Cells["F" + row].Value = t.GSTRate;//"Rate";
                w.Cells["G" + row].Value = t.TaxableValue;// "Taxable Value";
                w.Cells["H" + row].Value = t.Cess;//  "Cess Amount";
                w.Cells["I" + row].Value = t.GstIn;// "E-Commerce GSTIN"; 

                w.SetRowHeight(row, 25);
                row += 1;
                w.RowCount = row;
            }
            w.Cells[0, 0, row - 1, 8].Locked = true;

            gridControl1.Sheets[1].SetColumnAllowFilter(1, 12, true);
            w.AutoFilterMode = AutoFilterMode.EnhancedContextMenu;
            w.ColumnFooter.SetAggregationType(0, 11, FarPoint.Win.Spread.Model.AggregationType.Sum);
        }

        void CDNR_Sheet()
        {
            //CDNR credit Debit Note
            var cdnr = gridControl1.Sheets[7];
            cdnr.ColumnCount = 16;
            cdnr.ColumnHeader.Rows[0].Height = 35;

            //b2b.ColumnHeader.DefaultStyle.ra = true;
            cdnr.ColumnHeader.Cells[0, 0].Value = "GSTIN/UIN of Recipient";
            cdnr.ColumnHeader.Cells[0, 1].Value = "Receiver Name";
            cdnr.ColumnHeader.Cells[0, 2].Value = "Invoice/Advance Receipt Number";
            cdnr.ColumnHeader.Cells[0, 3].Value = "Invoice/Advance Receipt date";
            cdnr.ColumnHeader.Cells[0, 4].Value = "Note/Refund Voucher Number";
            cdnr.ColumnHeader.Cells[0, 5].Value = "Note/Refund Voucher date";
            cdnr.ColumnHeader.Cells[0, 6].Value = "Document Type";
            cdnr.ColumnHeader.Cells[0, 7].Value = "Place of Supply";
            cdnr.ColumnHeader.Cells[0, 8].Value = "Note/Refund Voucher Value";
            cdnr.ColumnHeader.Cells[0, 9].Value = "Applicable % of Tax Rate";
            cdnr.ColumnHeader.Cells[0, 10].Value = "Rate";
            cdnr.ColumnHeader.Cells[0, 11].Value = "Taxable Value";
            cdnr.ColumnHeader.Cells[0, 12].Value = "Cess Amount";
            cdnr.ColumnHeader.Cells[0, 13].Value = "Pre GST";

            cdnr.ColumnFooter.Cells[0, 0, 0, 13].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cdnr.ColumnHeader.Cells[0, 0, 0, 13].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cdnr.ColumnFooter.Cells[0, 0, 0, 13].ForeColor = Color.White;
            cdnr.ColumnHeader.Cells[0, 0, 0, 13].ForeColor = Color.White;
            cdnr.ColumnFooter.Visible = true;
            cdnr.ColumnFooter.RowCount = 1;

            // cdnr.Columns[3].CellType = datecell;
            //  cdnr.Columns[5].CellType = datecell;
            //  cdnr.Columns[11].CellType = nmbrcell;
            // cdnr.Columns[8].CellType = nmbrcell;
            FarPoint.Win.Spread.CellType.DateTimeCellType datecell = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            datecell.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.ShortDate;
            cdnr.Columns[3].CellType = datecell;
            cdnr.Columns[5].CellType = datecell;

            cdnr.OperationMode = OperationMode.Normal;
            cdnr.Protect = true;

            var wcdnr = gridControl1.Sheets[7]; // CDNR

            wcdnr.Columns[0].Width = 125;
            wcdnr.Columns[1].Width = 220;
            wcdnr.Columns[2].Width = 125;
            wcdnr.Columns[3].Width = 125;
            wcdnr.Columns[4].Width = 125;
            wcdnr.Columns[5].Width = 115;
            wcdnr.Columns[6].Width = 100;
            wcdnr.Columns[7].Width = 130;
            wcdnr.Columns[8].Width = 120;
            wcdnr.Columns[9].Width = 120;
            wcdnr.Columns[10].Width = 90;
            wcdnr.Columns[11].Width = 120;
            wcdnr.Columns[12].Width = 90;
            wcdnr.Columns[13].Width = 90;
            wcdnr.Columns[14].Width = 0;
            wcdnr.Columns[15].Width = 0;

            var cdnrs = Gstrs.Where(x => x.IsRevice == 0 && x.VTypeId != 12 && (x.Type == "REG" || x.Type == "CMP")).ToList();

            int row = 1;
            wcdnr.RowCount = row;
            foreach (var t in cdnrs)
            {
               // if (t.GstIn != "")
               // {
                    wcdnr.Cells["A" + row].Value = t.GstIn;
                    wcdnr.Cells["B" + row].Value = t.Account;//"Receiver Name";
                    wcdnr.Cells["C" + row].Value = t.InvoiceNo;//"Invoice/Advance Receipt Number";
                    wcdnr.Cells["D" + row].Value = t.InvoiceDate;//"Invoice/Advance Receipt date";
                    wcdnr.Cells["E" + row].Value = t.VoucherNo;//"Note/Refund Voucher Number";
                    wcdnr.Cells["F" + row].Value = t.VoucherDate;// "Note/Refund Voucher date";
                    wcdnr.Cells["G" + row].Value = t.NoteType;// "Document Type";
                    wcdnr.Cells["H" + row].Value = t.StateName;// "Place of Supply";
                    wcdnr.Cells["I" + row].Value = t.BillAmount;//"Note/Refund Voucher Value";
                    wcdnr.Cells["J" + row].Value = "";// "Applicable % of Tax Rate";
                    wcdnr.Cells["K" + row].Value = t.GSTRate;// "Rate";
                    wcdnr.Cells["L" + row].Value = t.TaxableValue;//"Taxable Value";
                    wcdnr.Cells["M" + row].Value = t.Cess;//"Cess Amount";
                    wcdnr.Cells["N" + row].Value = "N";//"Pre GST";
                    wcdnr.Cells["O" + row].Value = t.VTypeId;
                    wcdnr.Cells["P" + row].Value = t.Id;

                    row += 1;
                    wcdnr.RowCount = row;
               // }
            }
            wcdnr.Cells[0, 0, row - 1, 13].Locked = true;

            gridControl1.Sheets[7].SetColumnAllowFilter(1, 13, true);
            wcdnr.AutoFilterMode = AutoFilterMode.EnhancedContextMenu;
            wcdnr.ColumnFooter.SetAggregationType(0, 11, FarPoint.Win.Spread.Model.AggregationType.Sum);

            wcdnr.ColumnFooter.SetAggregationType(0, 8, FarPoint.Win.Spread.Model.AggregationType.Sum);

        }
        private void CDNRA_Sheet()
        {
            //CDNR credit Debit Note
            var cdnra = gridControl1.Sheets[8];
            cdnra.ColumnCount = 18;
            cdnra.ColumnHeader.Rows[0].Height = 55;

            //b2b.ColumnHeader.DefaultStyle.ra = true;
            cdnra.ColumnHeader.Cells[0, 0].Value = "GSTIN/UIN of Recipient";
            cdnra.ColumnHeader.Cells[0, 1].Value = "Receiver Name";
            cdnra.ColumnHeader.Cells[0, 2].Value = "Original Note/Refund Voucher Number";
            cdnra.ColumnHeader.Cells[0, 3].Value = "Original Note/Refund Voucher date";
            cdnra.ColumnHeader.Cells[0, 4].Value = "Original Invoice/Advance Receipt Number";
            cdnra.ColumnHeader.Cells[0, 5].Value = "Original Invoice/Advance Receipt date";
            cdnra.ColumnHeader.Cells[0, 6].Value = "Revised Note/Refund Voucher Number";
            cdnra.ColumnHeader.Cells[0, 7].Value = "Revised Note/Refund Voucher date";
            cdnra.ColumnHeader.Cells[0, 8].Value = "Document Type";
            cdnra.ColumnHeader.Cells[0, 9].Value = "Supply Type";
            cdnra.ColumnHeader.Cells[0, 10].Value = "Note/Refund Voucher Value";
            cdnra.ColumnHeader.Cells[0, 11].Value = "Applicable % of Tax Rate";
            cdnra.ColumnHeader.Cells[0, 12].Value = "Rate";
            cdnra.ColumnHeader.Cells[0, 13].Value = "Taxable Value";
            cdnra.ColumnHeader.Cells[0, 14].Value = "Cess Amount";
            cdnra.ColumnHeader.Cells[0, 15].Value = "Pre GST";

            cdnra.ColumnFooter.Cells[0, 0, 0, 15].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cdnra.ColumnHeader.Cells[0, 0, 0, 15].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cdnra.ColumnFooter.Cells[0, 0, 0, 15].ForeColor = Color.White;
            cdnra.ColumnHeader.Cells[0, 0, 0, 15].ForeColor = Color.White;
            cdnra.ColumnFooter.Visible = true;
            cdnra.ColumnFooter.RowCount = 1;

            FarPoint.Win.Spread.CellType.NumberCellType nmbrcell = new FarPoint.Win.Spread.CellType.NumberCellType();
            nmbrcell.DecimalPlaces = 2;

            cdnra.Columns[13].CellType = nmbrcell;
            cdnra.Columns[14].CellType = nmbrcell;
            cdnra.OperationMode = OperationMode.Normal;
            cdnra.Protect = true;

            var wcdnrA = gridControl1.Sheets[8]; // CDNR

            wcdnrA.Columns[0].Width = 125;//"GSTIN/UIN of Recipient";
            wcdnrA.Columns[1].Width = 220;//"Receiver Name";
            wcdnrA.Columns[2].Width = 135;// "Original Note/Refund Voucher Number";
            wcdnrA.Columns[3].Width = 135;// "Original Note/Refund Voucher date";
            wcdnrA.Columns[4].Width = 135;// "Original Invoice/Advance Receipt Number";
            wcdnrA.Columns[5].Width = 135;//"Original Invoice/Advance Receipt date";
            wcdnrA.Columns[6].Width = 135;// "Revised Note/Refund Voucher Number";
            wcdnrA.Columns[7].Width = 130;//"Revised Note/Refund Voucher date";
            wcdnrA.Columns[8].Width = 120;//"Document Type";
            wcdnrA.Columns[9].Width = 120;// "Supply Type";
            wcdnrA.Columns[10].Width = 90;//"Note/Refund Voucher Value";
            wcdnrA.Columns[11].Width = 120;// "Applicable % of Tax Rate";
            wcdnrA.Columns[12].Width = 90;// "Rate";
            wcdnrA.Columns[13].Width = 90;//"Taxable Value";
            wcdnrA.Columns[14].Width = 90;// "Cess Amount";
            wcdnrA.Columns[15].Width = 90;// "Pre GST";
            wcdnrA.Columns[16].Width = 0;//
            wcdnrA.Columns[17].Width = 0;//

            var cdnrs = Gstrs.Where(x => x.IsRevice == 1 && x.VTypeId != 12 && (x.Type == "REG" || x.Type == "CMP") &&
            (x.GstIn != null || x.GstIn.Length == 15)).ToList();

            int row = 1;
            wcdnrA.RowCount = row;
            foreach (var t in cdnrs)
            {
                if (t.GstIn != "")
                {
                    wcdnrA.Cells["A" + row].Value = t.GstIn;//"GSTIN/UIN of Recipient";
                    wcdnrA.Cells["B" + row].Value = t.Account;//"Receiver Name";
                    wcdnrA.Cells["C" + row].Value = t.OrgBillNo;// "Original Note/Refund Voucher Number";
                    wcdnrA.Cells["D" + row].Value = t.OrgBillDate;// "Original Note/Refund Voucher date";
                    wcdnrA.Cells["E" + row].Value = t.InvoiceNo;// "Original Invoice/Advance Receipt Number";
                    wcdnrA.Cells["F" + row].Value = t.InvoiceDate;//"Original Invoice/Advance Receipt date";
                    wcdnrA.Cells["G" + row].Value = t.VoucherNo;// "Revised Note/Refund Voucher Number";
                    wcdnrA.Cells["H" + row].Value = t.VoucherDate;//"Revised Note/Refund Voucher date";
                    wcdnrA.Cells["I" + row].Value = t.BillType;//"Document Type";
                    wcdnrA.Cells["J" + row].Value = "";// "Supply Type";
                    wcdnrA.Cells["K" + row].Value = t.BillAmount;//"Note/Refund Voucher Value";
                    wcdnrA.Cells["L" + row].Value = "";// "Applicable % of Tax Rate";
                    wcdnrA.Cells["M" + row].Value = t.GSTRate;// "Rate";
                    wcdnrA.Cells["N" + row].Value = t.TaxableValue;//"Taxable Value";
                    wcdnrA.Cells["O" + row].Value = t.Cess;// "Cess Amount";
                    wcdnrA.Cells["P" + row].Value = "";// "Pre GST";
                    wcdnrA.Cells["Q" + row].Value = t.VTypeId;//
                    wcdnrA.Cells["R" + row].Value = t.Id; //

                    row += 1;
                    wcdnrA.RowCount = row;
                }
            }
            wcdnrA.Cells[0, 0, row - 1, 13].Locked = true;

            gridControl1.Sheets[8].SetColumnAllowFilter(1, 13, true);
            wcdnrA.AutoFilterMode = AutoFilterMode.EnhancedContextMenu;

            wcdnrA.ColumnFooter.SetAggregationType(0, 13, FarPoint.Win.Spread.Model.AggregationType.Sum);
            wcdnrA.ColumnFooter.Cells[0, 13].CellType = nmbrcell;
            wcdnrA.ColumnFooter.Cells[0, 13].Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);

            wcdnrA.ColumnFooter.SetAggregationType(0, 14, FarPoint.Win.Spread.Model.AggregationType.Sum);
            wcdnrA.ColumnFooter.Cells[0, 14].CellType = nmbrcell;
            wcdnrA.ColumnFooter.Cells[0, 14].Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
        }

        private void CDNUR_Sheet()
        {
            //CDNUR credit Debit Note
            var cdnur = gridControl1.Sheets[9];
            cdnur.ColumnCount = 15;
            cdnur.ColumnHeader.Rows[0].Height = 55;

            //b2b.ColumnHeader.DefaultStyle.ra = true;
            cdnur.ColumnHeader.Cells[0, 0].Value = "UR Type";
            cdnur.ColumnHeader.Cells[0, 1].Value = "Note/Refund Voucher Number";
            cdnur.ColumnHeader.Cells[0, 2].Value = "Note/Refund Voucher date";
            cdnur.ColumnHeader.Cells[0, 3].Value = "Document Type";
            cdnur.ColumnHeader.Cells[0, 4].Value = "Invoice/Advance Receipt Number";
            cdnur.ColumnHeader.Cells[0, 5].Value = "Invoice/Advance Receipt date";
            cdnur.ColumnHeader.Cells[0, 6].Value = "Place Of Supply";
            cdnur.ColumnHeader.Cells[0, 7].Value = "Note/Refund Voucher Value";
            cdnur.ColumnHeader.Cells[0, 8].Value = "Applicable % of Tax Rate";
            cdnur.ColumnHeader.Cells[0, 9].Value = "Rate";
            cdnur.ColumnHeader.Cells[0, 10].Value = "Taxable Value";
            cdnur.ColumnHeader.Cells[0, 11].Value = "Cess Amount";
            cdnur.ColumnHeader.Cells[0, 12].Value = "Pre GST";

            cdnur.ColumnFooter.Cells[0, 0, 0, 12].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cdnur.ColumnHeader.Cells[0, 0, 0, 12].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cdnur.ColumnFooter.Cells[0, 0, 0, 12].ForeColor = Color.White;
            cdnur.ColumnHeader.Cells[0, 0, 0, 12].ForeColor = Color.White;
            cdnur.ColumnFooter.Visible = true;
            cdnur.ColumnFooter.RowCount = 1;

            FarPoint.Win.Spread.CellType.DateTimeCellType datecell = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            datecell.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.ShortDate;
            cdnur.Columns[2].CellType = datecell;
            cdnur.Columns[5].CellType = datecell;

            FarPoint.Win.Spread.CellType.NumberCellType nmbrcell = new FarPoint.Win.Spread.CellType.NumberCellType();
            nmbrcell.DecimalPlaces = 2;
            cdnur.Columns[4].CellType = nmbrcell;
            cdnur.Columns[10].CellType = nmbrcell;
            cdnur.Columns[11].CellType = nmbrcell;

            cdnur.OperationMode = OperationMode.Normal;
            cdnur.Protect = true;

            var w = gridControl1.Sheets[9];

            w.Columns[0].Width = 125;
            w.Columns[1].Width = 120;
            w.Columns[2].Width = 115;
            w.Columns[3].Width = 115;
            w.Columns[4].Width = 120;
            w.Columns[5].Width = 130;
            w.Columns[6].Width = 150;
            w.Columns[7].Width = 130;
            w.Columns[8].Width = 120;
            w.Columns[9].Width = 120;
            w.Columns[10].Width = 120;
            w.Columns[11].Width = 100;
            w.Columns[12].Width = 100;
            w.Columns[13].Width = 0;
            w.Columns[14].Width = 0;

            int row = 1;
           // int CRDRVType = (int)VoucherTypeEnum.DebitCreditNote;
            
            var b2b1 = Gstrs.Where(x => x.IsRevice == 0 && (x.Type!="REG" && x.Type!="CMP") && x.BillAmount >= 250000 
            && x.VTypeId !=12).ToList();

            w.RowCount = row;
            foreach (var t in b2b1)
            {
                w.Cells["A" + row].Value = t.GstIn;//"UR Type";
                w.Cells["B" + row].Value = t.Account;//  "Note/Refund Voucher Number";
                w.Cells["C" + row].Value = t.VoucherNo;// "Note/Refund Voucher date";
                w.Cells["D" + row].Value = t.VoucherDate;//"Document Type";
                w.Cells["E" + row].Value = t.BillAmount;// "Invoice/Advance Receipt Number";
                w.Cells["F" + row].Value = t.StateName;//"Invoice/Advance Receipt date";
                w.Cells["G" + row].Value = "N";// "Place Of Supply";
                w.Cells["H" + row].Value = "";// "Note/Refund Voucher Value"; 
                w.Cells["I" + row].Value = t.BillType;//"Applicable % of Tax Rate";
                w.Cells["J" + row].Value = t.GSTRate;// "Rate";
                w.Cells["K" + row].Value = t.TaxableValue;// "Taxable Value";
                w.Cells["L" + row].Value = t.Cess;//" "Cess Amount";
                w.Cells["M" + row].Value = "";// "Pre GST"; 
                w.Cells["N" + row].Value = t.VTypeId;//
                w.Cells["O" + row].Value = t.Id;//

                w.SetRowHeight(row, 25);
                row += 1;
                w.RowCount = row;
            }
            w.Cells[0, 0, row - 1, 12].Locked = true;

            gridControl1.Sheets[9].SetColumnAllowFilter(1, 12, true);
            w.AutoFilterMode = AutoFilterMode.EnhancedContextMenu;
            w.ColumnFooter.SetAggregationType(0, 11, FarPoint.Win.Spread.Model.AggregationType.Sum);
            w.ColumnFooter.Cells[0, 11].CellType = nmbrcell;
            w.ColumnFooter.Cells[0, 11].Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);

            w.ColumnFooter.SetAggregationType(0, 4, FarPoint.Win.Spread.Model.AggregationType.Sum);
            w.ColumnFooter.Cells[0, 4].CellType = nmbrcell;
            w.ColumnFooter.Cells[0, 4].Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);

            w.ColumnFooter.SetAggregationType(0, 10, FarPoint.Win.Spread.Model.AggregationType.Sum);
            w.ColumnFooter.Cells[0, 10].CellType = nmbrcell;
            w.ColumnFooter.Cells[0, 10].Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);

        }
        private void CDNURA_Sheet()
        {
            //CDNURA credit Debit Note Unregistered Ammendment
            var cdnura = gridControl1.Sheets[10];
            cdnura.ColumnCount = 17;
            cdnura.ColumnHeader.Rows[0].Height = 55;

            //b2b.ColumnHeader.DefaultStyle.ra = true;
            cdnura.ColumnHeader.Cells[0, 0].Value = "UR Type";
            cdnura.ColumnHeader.Cells[0, 1].Value = "Original Note/Refund Voucher Number";
            cdnura.ColumnHeader.Cells[0, 2].Value = "Original Note/Refund Voucher date";
            cdnura.ColumnHeader.Cells[0, 3].Value = "Original Invoice/Advance Receipt Number";
            cdnura.ColumnHeader.Cells[0, 4].Value = "Original Invoice/Advance Receipt date";
            cdnura.ColumnHeader.Cells[0, 5].Value = "Revised Note/Refund Voucher Number";
            cdnura.ColumnHeader.Cells[0, 6].Value = "Revised Note/Refund Voucher date";
            cdnura.ColumnHeader.Cells[0, 7].Value = "Document Type";
            cdnura.ColumnHeader.Cells[0, 8].Value = "Supply Type";
            cdnura.ColumnHeader.Cells[0, 9].Value = "Note/Refund Voucher Value";
            cdnura.ColumnHeader.Cells[0, 10].Value = "Applicable % of Tax Rate";
            cdnura.ColumnHeader.Cells[0, 11].Value = "Rate";
            cdnura.ColumnHeader.Cells[0, 12].Value = "Taxable Value";
            cdnura.ColumnHeader.Cells[0, 13].Value = "Cess Amount";
            cdnura.ColumnHeader.Cells[0, 14].Value = "Pre GST";

            cdnura.ColumnFooter.Cells[0, 0, 0, 14].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cdnura.ColumnHeader.Cells[0, 0, 0, 14].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cdnura.ColumnFooter.Cells[0, 0, 0, 14].ForeColor = Color.White;
            cdnura.ColumnHeader.Cells[0, 0, 0, 14].ForeColor = Color.White;
            cdnura.ColumnFooter.Visible = true;
            cdnura.ColumnFooter.RowCount = 1;

            FarPoint.Win.Spread.CellType.NumberCellType nmbrcell = new FarPoint.Win.Spread.CellType.NumberCellType();
            nmbrcell.DecimalPlaces = 2;
            cdnura.Columns[9].CellType = nmbrcell;
            cdnura.Columns[12].CellType = nmbrcell;

            cdnura.OperationMode = OperationMode.Normal;
            cdnura.Protect = true;

            var w = gridControl1.Sheets[10];

            w.Columns[0].Width = 125;
            w.Columns[1].Width = 140;
            w.Columns[2].Width = 140;
            w.Columns[3].Width = 140;
            w.Columns[4].Width = 140;
            w.Columns[5].Width = 140;
            w.Columns[6].Width = 150;
            w.Columns[7].Width = 130;
            w.Columns[8].Width = 120;
            w.Columns[9].Width = 120;
            w.Columns[10].Width = 120;
            w.Columns[11].Width = 100;
            w.Columns[12].Width = 100;
            w.Columns[13].Width = 100;
            w.Columns[14].Width = 100;
            w.Columns[15].Width = 0;
            w.Columns[16].Width = 0;

            int row = 1;
            int CRDRVType = (int)VoucherTypeEnum.DebitCreditNote;
            var b2b1 = Gstrs.Where(x => x.IsRevice == 1 && (x.GstIn == null || x.GstIn.Length == 0) && x.BillAmount > 250000
             && (x.Type == "REG" || x.Type == "CMP") && x.VTypeId == CRDRVType).ToList();
            w.RowCount = row;
            foreach (var t in b2b1)
            {
                w.Cells["A" + row].Value = t.GstIn;//"UR Type";
                w.Cells["B" + row].Value = t.Account;//"Original Note/Refund Voucher Number";
                w.Cells["C" + row].Value = t.VoucherNo;// "Original Note/Refund Voucher date";
                w.Cells["D" + row].Value = t.OrgBillNo;//"Original Invoice/Advance Receipt Number";
                w.Cells["E" + row].Value = t.OrgBillDate;// "Original Invoice/Advance Receipt date";
                w.Cells["F" + row].Value = t.VoucherNo;// "Revised Note/Refund Voucher Number";
                w.Cells["G" + row].Value = t.VoucherDate;// "Revised Note/Refund Voucher date";
                w.Cells["H" + row].Value = "";//  "Document Type";
                w.Cells["I" + row].Value = t.BillType;//Supply Type";
                w.Cells["J" + row].Value = t.BillAmount;// "Note/Refund Voucher Value";
                w.Cells["K" + row].Value = "";// "Applicable % of Tax Rate";
                w.Cells["L" + row].Value = t.GSTRate;// "Rate";
                w.Cells["M" + row].Value = t.TaxableValue;//  "Taxable Value";
                w.Cells["N" + row].Value = t.Cess;// "Cess Amount";
                w.Cells["O" + row].Value = t.Cess;//  "Pre GST";
                w.Cells["P" + row].Value = t.VTypeId;//
                w.Cells["Q" + row].Value = t.Id;//

                w.SetRowHeight(row, 25);
                row += 1;
                w.RowCount = row;
            }
            w.Cells[0, 0, row - 1, 12].Locked = true;

            gridControl1.Sheets[10].SetColumnAllowFilter(1, 12, true);
            w.AutoFilterMode = AutoFilterMode.EnhancedContextMenu;
            w.ColumnFooter.SetAggregationType(0, 9, FarPoint.Win.Spread.Model.AggregationType.Sum);
            w.ColumnFooter.SetAggregationType(0, 12, FarPoint.Win.Spread.Model.AggregationType.Sum);
        }
        private void Export_Sheet()
        {
            // Export
            var exp = gridControl1.Sheets[11];
            exp.ColumnCount = 13;
            exp.ColumnHeader.Rows[0].Height = 35;

            //b2b.ColumnHeader.DefaultStyle.ra = true;
            exp.ColumnHeader.Cells[0, 0].Value = "Export Type";
            exp.ColumnHeader.Cells[0, 1].Value = "Invoice Number";
            exp.ColumnHeader.Cells[0, 2].Value = "Invoice date";
            exp.ColumnHeader.Cells[0, 3].Value = "Invoice Value";
            exp.ColumnHeader.Cells[0, 4].Value = "Port Code";
            exp.ColumnHeader.Cells[0, 5].Value = "Shipping Bill Number";
            exp.ColumnHeader.Cells[0, 6].Value = "Shipping Bill Date";
            exp.ColumnHeader.Cells[0, 7].Value = "Applicable % of Tax Rate";
            exp.ColumnHeader.Cells[0, 8].Value = "Rate";
            exp.ColumnHeader.Cells[0, 9].Value = "Taxable Value";
            exp.ColumnHeader.Cells[0, 10].Value = "Cess Amount";

            exp.ColumnFooter.Cells[0, 0, 0, 10].BackColor = ColorTranslator.FromHtml("#16A5DC");
            exp.ColumnHeader.Cells[0, 0, 0, 10].BackColor = ColorTranslator.FromHtml("#16A5DC");
            exp.ColumnFooter.Cells[0, 0, 0, 10].ForeColor = Color.White;
            exp.ColumnHeader.Cells[0, 0, 0, 10].ForeColor = Color.White;
            exp.ColumnFooter.Visible = true;
            exp.ColumnFooter.RowCount = 1;

            exp.OperationMode = OperationMode.Normal;
            exp.Protect = true;

            var w = gridControl1.Sheets[11];

            w.Columns[0].Width = 125;
            w.Columns[1].Width = 125;
            w.Columns[2].Width = 120;
            w.Columns[3].Width = 120;
            w.Columns[4].Width = 100;
            w.Columns[5].Width = 100;
            w.Columns[6].Width = 120;
            w.Columns[7].Width = 100;
            w.Columns[8].Width = 100;
            w.Columns[9].Width = 100;
            w.Columns[10].Width = 100;
            w.Columns[11].Width = 0;
            w.Columns[12].Width = 0;

            int row = 1;

            var explist = Gstrs.Where(x => x.IsRevice == 0 && (x.BillType == "SEZ Supplies with Payment" || x.BillType == "SEZ Supplies without Payment"
                || x.BillType == "Deemed Exp") && x.VTypeId == 12).ToList();

            //        row = 5;
            foreach (var t in explist)
            {
                var type = "";
                if (t.BillType == "SEZ Supplies with Payment")
                {
                    type = "WPAY";
                }
                else if (t.BillType == "SEZ Supplies without Payment")
                {
                    type = "WOPAY";
                }
                else
                {
                    type = "WPAY";
                }

                w.Cells["A" + row].Value = type;//""Export Type";
                w.Cells["B" + row].Value = t.VoucherNo;//  "Invoice Number";
                w.Cells["C" + row].Value = t.VoucherDate;// "Invoice date";
                w.Cells["D" + row].Value = t.NoteType;//""Invoice Value";
                w.Cells["E" + row].Value = t.PortCode;// "Port Code";
                w.Cells["F" + row].Value = "";//"Shipping Bill Number";
                w.Cells["G" + row].Value = "";// "Shipping Bill Date";
                w.Cells["H" + row].Value = "";// "Applicable % of Tax Rate";
                w.Cells["I" + row].Value = t.GSTRate;//"Rate"; 
                w.Cells["J" + row].Value = t.TaxableValue;// "Taxable Value";
                w.Cells["K" + row].Value = t.Cess;//" "Cess Amount";
                w.Cells["L" + row].Value = t.VTypeId;//" 
                w.Cells["M" + row].Value = t.Id;//" 

                w.SetRowHeight(row, 25);
                row += 1;
                w.RowCount = row;
            }
            w.Cells[0, 0, row - 1, 12].Locked = true;

            gridControl1.Sheets[11].SetColumnAllowFilter(1, 12, true);
            w.AutoFilterMode = AutoFilterMode.EnhancedContextMenu;
            w.ColumnFooter.SetAggregationType(0, 11, FarPoint.Win.Spread.Model.AggregationType.Sum);
        }

        private void ExpA_Sheet()
        {
            // export amendment
            var expa = gridControl1.Sheets[12];
            expa.ColumnCount = 15;
            expa.ColumnHeader.Rows[0].Height = 35;

            //b2b.ColumnHeader.DefaultStyle.ra = true;
            expa.ColumnHeader.Cells[0, 0].Value = "Export Type";
            expa.ColumnHeader.Cells[0, 1].Value = "Original Invoice Number";
            expa.ColumnHeader.Cells[0, 2].Value = "Original Invoice date";

            expa.ColumnHeader.Cells[0, 3].Value = "Revised Invoice Number";
            expa.ColumnHeader.Cells[0, 4].Value = "Revised Invoice date";
            expa.ColumnHeader.Cells[0, 5].Value = "Invoice Value";
            expa.ColumnHeader.Cells[0, 6].Value = "Port Code";
            expa.ColumnHeader.Cells[0, 7].Value = "Shipping Bill Number";
            expa.ColumnHeader.Cells[0, 8].Value = "Shipping Bill Date";
            expa.ColumnHeader.Cells[0, 9].Value = "Applicable % of Tax Rate";
            expa.ColumnHeader.Cells[0, 10].Value = "Rate";
            expa.ColumnHeader.Cells[0, 11].Value = "Taxable Value";
            expa.ColumnHeader.Cells[0, 12].Value = "Cess Amount";

            expa.ColumnFooter.Cells[0, 0, 0, 10].BackColor = ColorTranslator.FromHtml("#16A5DC");
            expa.ColumnHeader.Cells[0, 0, 0, 10].BackColor = ColorTranslator.FromHtml("#16A5DC");
            expa.ColumnFooter.Cells[0, 0, 0, 10].ForeColor = Color.White;
            expa.ColumnHeader.Cells[0, 0, 0, 10].ForeColor = Color.White;
            expa.ColumnFooter.Visible = true;
            expa.ColumnFooter.RowCount = 1;

            expa.OperationMode = OperationMode.Normal;
            expa.Protect = true;

            var w = gridControl1.Sheets[12];

            w.Columns[0].Width = 125;
            w.Columns[1].Width = 125;
            w.Columns[2].Width = 120;
            w.Columns[3].Width = 120;
            w.Columns[4].Width = 100;
            w.Columns[5].Width = 100;
            w.Columns[6].Width = 120;
            w.Columns[7].Width = 100;
            w.Columns[8].Width = 100;
            w.Columns[9].Width = 100;
            w.Columns[10].Width = 100;
            w.Columns[11].Width = 100;
            w.Columns[12].Width = 100;
            w.Columns[13].Width = 0;
            w.Columns[14].Width = 0;

            int row = 1;

            var explist = Gstrs.Where(x => x.IsRevice == 1 && (x.BillType == "SEZ Supplies with Payment" || x.BillType == "SEZ Supplies without Payment"
                || x.BillType == "Deemed Exp") && x.VTypeId == 12).ToList();

            //          row = 5;
            foreach (var t in explist)
            {
                var type = "";
                if (t.BillType == "SEZ Supplies with Payment")
                {
                    type = "WPAY";
                }
                else if (t.BillType == "SEZ Supplies without Payment")
                {
                    type = "WOPAY";
                }
                else
                {
                    type = "WPAY";
                }

                w.Cells["A" + row].Value = type;//""Export Type";
                w.Cells["B" + row].Value = t.OrgBillNo;//  "Original Invoice Number";
                w.Cells["C" + row].Value = t.OrgBillDate;// "Original Invoice date";
                w.Cells["D" + row].Value = t.VoucherNo;//  "Invoice Number";
                w.Cells["E" + row].Value = t.VoucherDate;// "Invoice date";
                w.Cells["F" + row].Value = t.NoteType;//""Invoice Value";
                w.Cells["G" + row].Value = t.PortCode;// "Port Code";
                w.Cells["H" + row].Value = "";//"Shipping Bill Number";
                w.Cells["I" + row].Value = "";// "Shipping Bill Date";
                w.Cells["J" + row].Value = "";// "Applicable % of Tax Rate";
                w.Cells["K" + row].Value = t.GSTRate;//"Rate"; 
                w.Cells["L" + row].Value = t.TaxableValue;// "Taxable Value";
                w.Cells["M" + row].Value = t.Cess;//" "Cess Amount";
                w.Cells["N" + row].Value = t.VTypeId;//" "Cess Amount";
                w.Cells["O" + row].Value = t.Id;//" "Cess Amount";

                w.SetRowHeight(row, 25);
                row += 1;
                w.RowCount = row;
            }
            w.Cells[0, 0, row - 1, 12].Locked = true;

            gridControl1.Sheets[12].SetColumnAllowFilter(1, 12, true);
            w.AutoFilterMode = AutoFilterMode.EnhancedContextMenu;
            w.ColumnFooter.SetAggregationType(0, 11, FarPoint.Win.Spread.Model.AggregationType.Sum);
        }
        private void HSN_Sheet()
        {
            // Hsn Summary
            var hsn = gridControl1.Sheets[13];
            hsn.ColumnCount = 12;
            hsn.ColumnHeader.Rows[0].Height = 35;

            //b2b.ColumnHeader.DefaultStyle.ra = true;
            hsn.ColumnHeader.Cells[0, 0].Value = "HSN";
            hsn.ColumnHeader.Cells[0, 1].Value = "Description";
            hsn.ColumnHeader.Cells[0, 2].Value = "UQC";
            hsn.ColumnHeader.Cells[0, 3].Value = "Total Quantity";
            hsn.ColumnHeader.Cells[0, 4].Value = "Total Value";
            hsn.ColumnHeader.Cells[0, 5].Value = "Taxable Value";
            hsn.ColumnHeader.Cells[0, 6].Value = "Integrated Tax Amount";
            hsn.ColumnHeader.Cells[0, 7].Value = "Central Tax Amount";
            hsn.ColumnHeader.Cells[0, 8].Value = "State/UT Tax Amount";
            hsn.ColumnHeader.Cells[0, 9].Value = "Cess Amount";
            hsn.ColumnFooter.Cells[0, 0, 0, 9].BackColor = ColorTranslator.FromHtml("#16A5DC");
            hsn.ColumnHeader.Cells[0, 0, 0, 9].BackColor = ColorTranslator.FromHtml("#16A5DC");
            hsn.ColumnFooter.Cells[0, 0, 0, 9].ForeColor = Color.White;
            hsn.ColumnHeader.Cells[0, 0, 0, 9].ForeColor = Color.White;
            hsn.ColumnFooter.Visible = true;
            hsn.ColumnFooter.RowCount = 1;
            hsn.OperationMode = OperationMode.Normal;
            hsn.Protect = true;

            var w = gridControl1.Sheets[13];

            w.Columns[0].Width = 125;
            w.Columns[1].Width = 200;
            w.Columns[2].Width = 105;
            w.Columns[3].Width = 75;
            w.Columns[4].Width = 100;
            w.Columns[5].Width = 100;
            w.Columns[6].Width = 100;
            w.Columns[7].Width = 100;
            w.Columns[8].Width = 100;
            w.Columns[9].Width = 100;
            w.Columns[10].Width = 110;

            int row = 1;
            int fyear = Convert.ToInt32(KontoGlobals.DFromDate.ToString("yyyy"));

            int tyear = Convert.ToInt32(KontoGlobals.DToDate.ToString("yyyy"));
            //int monthno = int.Parse(monthLookUpEdit.EditValue.ToString());
            var fdate = Convert.ToInt32(fromDateEdit.DateTime.ToString("yyyyMMdd"));
            var tdate = Convert.ToInt32(toDateEdit.DateTime.ToString("yyyyMMdd"));

            //if (monthno < 4)
            //{
            //    fdate = int.Parse(tyear.ToString() + monthLookUpEdit.EditValue.ToString() + "01");
            //    tdate = int.Parse(tyear.ToString() + monthLookUpEdit.EditValue.ToString() + DateTime.DaysInMonth(tyear, monthno).ToString());
            //}
            //else
            //{
            //    fdate = int.Parse(fyear.ToString() + monthLookUpEdit.EditValue.ToString() + "01");
            //    tdate = int.Parse(fyear.ToString() + monthLookUpEdit.EditValue.ToString() + DateTime.DaysInMonth(tyear, monthno).ToString());
            //}

            List<Gstr1HsnDto> b2b1 = new List<Gstr1HsnDto>();
            using (var _db = new KontoContext())
            {

                _db.Database.CommandTimeout = 0;
                b2b1 = _db.Database.SqlQuery<Gstr1HsnDto>(
                     "dbo.HsnSummary @CompanyId={0},@FromDate={1},@ToDate={2}",
                     Convert.ToInt32(KontoGlobals.CompanyId), fdate, tdate).ToList();
            }
            w.RowCount = row;
            
            string _uqc = "NA";

            if (System.Configuration.ConfigurationManager.AppSettings["Uqc"]!=null)
                _uqc = System.Configuration.ConfigurationManager.AppSettings["Uqc"];

            foreach (var t in b2b1)
            {
                w.Cells["A" + row].Value = t.HsnCode;//"GSTIN/UIN of Recipient";
                w.Cells["B" + row].Value = t.Description;

                if(_uqc!="NA")
                    w.Cells["C" + row].Value = _uqc;
                else
                   w.Cells["C" + row].Value = t.Uqc;

                w.Cells["D" + row].Value = t.TotalQty;
                w.Cells["E" + row].Value = t.BillAmount;
                w.Cells["F" + row].Value = t.TaxableValue;
                w.Cells["G" + row].Value = t.IgstAmt;
                w.Cells["H" + row].Value = t.CgstAmt;
                w.Cells["I" + row].Value = t.SgstAmt;
                w.Cells["J" + row].Value = t.Cess;
                w.Cells["K" + row].Value = t.UomId;
                w.Cells["L" + row].Value = t.GroupId;
                w.SetRowHeight(row, 25);
                row += 1;
                w.RowCount = row;
            }

            w.Cells[0, 0, row - 1, 9].Locked = true;

            gridControl1.Sheets[13].SetColumnAllowFilter(1, 12, true);
            w.AutoFilterMode = AutoFilterMode.EnhancedContextMenu;
            w.ColumnFooter.SetAggregationType(0, 3, FarPoint.Win.Spread.Model.AggregationType.Sum);
            w.ColumnFooter.SetAggregationType(0, 4, FarPoint.Win.Spread.Model.AggregationType.Sum);
            w.ColumnFooter.SetAggregationType(0, 5, FarPoint.Win.Spread.Model.AggregationType.Sum);
            w.ColumnFooter.SetAggregationType(0, 6, FarPoint.Win.Spread.Model.AggregationType.Sum);
            w.ColumnFooter.SetAggregationType(0, 7, FarPoint.Win.Spread.Model.AggregationType.Sum);
            w.ColumnFooter.SetAggregationType(0, 8, FarPoint.Win.Spread.Model.AggregationType.Sum);
            w.ColumnFooter.SetAggregationType(0, 9, FarPoint.Win.Spread.Model.AggregationType.Sum);

        }
        private void Doc_Sheet()
        {
            var doc = gridControl1.Sheets[14];
            doc.ColumnCount = 6;
            doc.ColumnHeader.Rows[0].Height = 35;
            doc.ColumnHeader.Cells[0, 0].Value = "Nature of Document";
            doc.ColumnHeader.Cells[0, 1].Value = "Sr. No. From";
            doc.ColumnHeader.Cells[0, 2].Value = "Sr. No. To";
            doc.ColumnHeader.Cells[0, 3].Value = "Total Number";
            doc.ColumnHeader.Cells[0, 4].Value = "Cancelled";

            doc.ColumnFooter.Visible = true;
            doc.ColumnFooter.RowCount = 1;
            doc.OperationMode = OperationMode.Normal;
            doc.Protect = true;

            var w = gridControl1.Sheets[14];

            w.Columns[0].Width = 125;
            w.Columns[1].Width = 200;
            w.Columns[2].Width = 105;
            w.Columns[3].Width = 75;
            w.Columns[4].Width = 100;
            w.Columns[5].Width = 0; 

            int row = 1;
            int fyear = Convert.ToInt32(KontoGlobals.DFromDate.ToString("yyyy"));

            int tyear = Convert.ToInt32(KontoGlobals.DToDate.ToString("yyyy"));
            //  int monthno = int.Parse(monthLookUpEdit.EditValue.ToString());
            var fdate = Convert.ToInt32(fromDateEdit.DateTime.ToString("yyyyMMdd"));
            var tdate = Convert.ToInt32(toDateEdit.DateTime.ToString("yyyyMMdd"));

            //if (monthno < 4)
            //{
            //    fdate = int.Parse(tyear.ToString() + monthLookUpEdit.EditValue.ToString() + "01");
            //    tdate = int.Parse(tyear.ToString() + monthLookUpEdit.EditValue.ToString() + DateTime.DaysInMonth(tyear, monthno).ToString());
            //}
            //else
            //{
            //    fdate = int.Parse(fyear.ToString() + monthLookUpEdit.EditValue.ToString() + "01");
            //    tdate = int.Parse(fyear.ToString() + monthLookUpEdit.EditValue.ToString() + DateTime.DaysInMonth(tyear, monthno).ToString());
            //}

            int compId = Convert.ToInt32(KontoGlobals.CompanyId);
            List<gstr1_docDto> b2b1 = new List<gstr1_docDto>();
            using (var _db = new KontoContext())
            {
                _db.Database.CommandTimeout = 0;
                b2b1 = _db.Database.SqlQuery<gstr1_docDto>(
                      "dbo.gstr1_doc @fromdate={0},@todate={1},@companyid={2}",
                      fdate, tdate, compId).ToList();
            }
            w.RowCount = row;
            foreach (var t in b2b1)
            {
                w.Cells["A" + row].Value = t.DocType; //"Nature of Document";
                w.Cells["B" + row].Value = t.StratBill;//"Sr. No. From";
                w.Cells["C" + row].Value = t.EndBill;//"Sr. No. To";
                w.Cells["D" + row].Value = t.Total;//"Total Number";
                w.Cells["E" + row].Value = t.TotalCancel;//"Cancelled";
                w.Cells["F" + row].Value = t.VoucherID;//"VoucherId";

                w.SetRowHeight(row, 25);
                row += 1;
                w.RowCount = row;
            }

            w.Cells[0, 0, row - 1, 4].Locked = true;

            gridControl1.Sheets[13].SetColumnAllowFilter(1, 12, true);
            w.AutoFilterMode = AutoFilterMode.EnhancedContextMenu;
            w.ColumnFooter.SetAggregationType(0, 11, FarPoint.Win.Spread.Model.AggregationType.Sum);

        }

        private void summarySimpleButton_Click(object sender, EventArgs e)
        {
            var frm = new GstSummaryView();
            frm._fromDate = this.fromDateEdit.DateTime;
            frm._ToDate = this.toDateEdit.DateTime;
            var _tab = this.Parent.Parent as TabControlAdv;
            if (_tab == null) return;
            var pg1 = new TabPageAdv();
            pg1.Text = "Gst Summary";
            _tab.TabPages.Add(pg1);
            _tab.SelectedTab = pg1;
            frm.WindowState = FormWindowState.Maximized;
            frm.TopLevel = false;
            frm.Parent = pg1;
            frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
            frm.Show();// = true;
        }


        protected virtual bool IsFileLocked(FileInfo file)
        {
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }

            //file is not locked
            return false;
        }

       
    }
}

