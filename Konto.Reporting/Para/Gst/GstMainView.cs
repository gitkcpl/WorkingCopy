using Aspose.Cells;
using DevExpress.Spreadsheet;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Data.Models.Admin.Dtos;
using Konto.Data.Models.Reports;
using Konto.Data.Models.Transaction.Dtos;
using Konto.Pos.Sales;
using Konto.Shared.Account.DRCRNote;
using Konto.Shared.Trans.SInvoice;
using Konto.Shared.Trans.SReturn;
using Serilog;
using Syncfusion.GridExcelConverter;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace Konto.Reporting.Para.Gst
{
    public partial class GstMainView : KontoForm
    {
       // FarPoint.Win.Spread.FpSpread gridControl1;
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
         //   this.gridControl1 = new FpSpread();
            this.Load += GstMainView_Load;
            fromDateEdit.EditValue = KontoGlobals.DFromDate;
            toDateEdit.EditValue = KontoGlobals.DToDate;

            b2bGridControl.ProcessGridKey += B2bGridControl_ProcessGridKey;
            b2bAGridControl.ProcessGridKey += B2bGridControl_ProcessGridKey;
            
            b2clGridControl.ProcessGridKey += B2bGridControl_ProcessGridKey;
            b2claGridControl.ProcessGridKey += B2bGridControl_ProcessGridKey;
            
            expGridControl.ProcessGridKey += B2bGridControl_ProcessGridKey;
            expaGridControl.ProcessGridKey += B2bGridControl_ProcessGridKey;

            cdnrGridControl.ProcessGridKey += B2bGridControl_ProcessGridKey;
            cdnraGridControl.ProcessGridKey += B2bGridControl_ProcessGridKey;

            cdnurGridControl.ProcessGridKey += B2bGridControl_ProcessGridKey;
            cdnuraGridControl.ProcessGridKey += B2bGridControl_ProcessGridKey;

            b2csGridControl.ProcessGridKey += B2csGridControl_ProcessGridKey;

            hsnGridControl.ProcessGridKey += HsnGridControl_ProcessGridKey;

            docGridControl.ProcessGridKey += DocGridControl_ProcessGridKey;
            spreadsheetControl1.PreviewKeyDown += SpreadsheetControl1_PreviewKeyDown;

            this.FirstActiveControl = fromDateEdit;
        }

        private void SpreadsheetControl1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData != Keys.Enter) return;
            
            int fdate = Convert.ToInt32(fromDateEdit.DateTime.ToString("yyyyMMdd"));
            int tdate = Convert.ToInt32(toDateEdit.DateTime.ToString("yyyyMMdd"));
            
            DevExpress.Spreadsheet.IWorkbook WK = spreadsheetControl1.Document;
            var w = WK.Worksheets[0];
            var vw = new HSNDetailListView();
            KontoContext db = new KontoContext();

            string _FilterType = "X"; //a/b/c/cd
            string _title = "TEST";
            //(a) Outward Taxable  supplies  (other than zero rated, nil rated and exempted)

            if (this.spreadsheetControl1.ActiveCell.RowIndex == 10)
            {
                _FilterType = "A";
                _title = w.Cells[spreadsheetControl1.ActiveCell.RowIndex, 1].Value.ToString();
            }
            //(b) Outward Taxable  supplies  (zero rated )

            else if (this.spreadsheetControl1.ActiveCell.RowIndex == 11)
            {
                _FilterType = "B";
                _title = w.Cells[spreadsheetControl1.ActiveCell.RowIndex, 1].Value.ToString();

            }
            //(c) Other Outward Taxable  supplies (Nil rated, exempted)

            else if (this.spreadsheetControl1.ActiveCell.RowIndex == 12)
            {
                _FilterType = "C";
                _title = w.Cells[spreadsheetControl1.ActiveCell.RowIndex, 1].Value.ToString();
            }
            //(d) Inward supplies (liable to reverse charge) 

            else if (this.spreadsheetControl1.ActiveCell.RowIndex == 13)
            {
                _FilterType = "D";
                _title = w.Cells[spreadsheetControl1.ActiveCell.RowIndex, 1].Value.ToString();
            }
            else if (this.spreadsheetControl1.ActiveCell.RowIndex == 21) // imports of  goods
            {
                _FilterType = "E";
                _title = w.Cells[spreadsheetControl1.ActiveCell.RowIndex, 1].Value.ToString();

            }
            else if (this.spreadsheetControl1.ActiveCell.RowIndex == 22) // imports of  Service
            {
                _FilterType = "F";
                _title = w.Cells[spreadsheetControl1.ActiveCell.RowIndex, 1].Value.ToString();
            }
            else if (this.spreadsheetControl1.ActiveCell.RowIndex == 25) // All Other Itc
            {
                _FilterType = "G";
                _title = w.Cells[spreadsheetControl1.ActiveCell.RowIndex, 1].Value.ToString();
            }
            
            //From a supplier under composition scheme, Exempt  and Nil rated supply

            else if (this.spreadsheetControl1.ActiveCell.RowIndex == 38) 
            {
                _FilterType = "H";
                _title = w.Cells[spreadsheetControl1.ActiveCell.RowIndex, 1].Value.ToString();
            }

//            Non GST supply

            else if (this.spreadsheetControl1.ActiveCell.RowIndex == 39) 
            {
                _FilterType = "I";
                _title = w.Cells[spreadsheetControl1.ActiveCell.RowIndex, 1].Value.ToString();
            }
            else if (this.spreadsheetControl1.ActiveCell.RowIndex >= 78 && this.spreadsheetControl1.ActiveCell.RowIndex <=114) 
            {
                _FilterType = "J";
                _title = w.Cells[spreadsheetControl1.ActiveCell.RowIndex, 1].Value.ToString();
                if (string.IsNullOrEmpty(_title)) return;
                var list1 = db.Database.SqlQuery<HsnSmryDetail>("dbo.GSTR3BDetails @CompanyId={0},@FromDate={1}," +
                                    "@ToDate={2},@YearId={3}, @Type={4},@StateName={5}", KontoGlobals.CompanyId, fdate, tdate, KontoGlobals.YearId, "J", _title.Substring(3)).ToList();
                if (list1.Count == 0) return;
                vw.RefreshGrid(list1, _title, "HSN");
                vw.ShowDialog();
                return;
            }

            if (_FilterType == "X") return;
            var list = db.Database.SqlQuery<HsnSmryDetail>("dbo.GSTR3BDetails @CompanyId={0},@FromDate={1}," +
                                    "@ToDate={2},@YearId={3}, @Type={4}", KontoGlobals.CompanyId, fdate, tdate, KontoGlobals.YearId, _FilterType).ToList();
            if (list.Count == 0) return;
            
            vw.RefreshGrid(list, _title, "HSN");
            vw.ShowDialog();

        }

        private void DocGridControl_ProcessGridKey(object sender, KeyEventArgs e)
        {

            if (docGridView.FocusedRowHandle < 0) return;
            if (e.KeyCode != Keys.Enter) return;
            var row = docGridView.GetRow(docGridView.FocusedRowHandle) as gstr1_docDto;
            if (row == null) return;

            int fdate = Convert.ToInt32(fromDateEdit.DateTime.ToString("yyyyMMdd"));
            int tdate = Convert.ToInt32(toDateEdit.DateTime.ToString("yyyyMMdd"));
            var vw = new HSNDetailListView();

            KontoContext db = new KontoContext();

            
                
                var list = db.Database.SqlQuery<docDetailList>("dbo.docDetailList @fromdate={0},@todate={1}," +
                    "@companyid={2},@NOB={3},@voucherId={4}", fdate, tdate, KontoGlobals.CompanyId, row.DocType, row.VoucherID).ToList();

                vw.RefreshDocDetailGrid(list, "Doc Detail", "Doc");
                vw.ShowDialog();
            
        }

        private void HsnGridControl_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (hsnGridView.FocusedRowHandle < 0) return;
            if (e.KeyCode != Keys.Enter) return;
            var row = hsnGridView.GetRow(hsnGridView.FocusedRowHandle) as Gstr1HsnDto;
            if (row == null) return;

            int fdate = Convert.ToInt32(fromDateEdit.DateTime.ToString("yyyyMMdd"));
            int tdate = Convert.ToInt32(toDateEdit.DateTime.ToString("yyyyMMdd"));

            var vw = new HSNDetailListView();

            int UnitId = 0;
            if (row.UomId!= null)
                UnitId = (int) row.UomId;

            KontoContext db = new KontoContext();

            var list = db.Database.SqlQuery<HsnSmryDetail>("dbo.HsnSmryDetail @CompanyId={0},@FromDate={1}," +
                "@ToDate={2},@YearId={3},@HsnCode={4},@UnitId={5},@groupid={6}", KontoGlobals.CompanyId,
                fdate, tdate, KontoGlobals.YearId, row.HsnCode, UnitId, row.GroupId).ToList();

            vw.RefreshGrid(list, "HSN Detail", "HSN");
            vw.ShowDialog();
        }

        

       
        private void CdnrZoom(GstDto row)
        {
            var frm = new KontoMetroForm();
            if(row.VTypeId == (int) VoucherTypeEnum.SaleReturn)
            {
                frm = new SReturnIndex();

            }
            else if(row.VTypeId == (int) VoucherTypeEnum.DebitCreditNote)
            {
                frm = new DRCRNoteIndex();
            }
            else
            {
                if (KontoGlobals.PackageId == (int)PackageType.POS)
                    frm = new SalesIndex();
                else
                    frm = new SInvoiceIndex();
            }

            frm.tabControlAdv1.SelectedIndex = 0;
            frm.EditKey = row.Id;
            frm.OpenForLookup = true;
            frm.ShowDialog();
        }

        private void B2csGridControl_ProcessGridKey(object sender, KeyEventArgs e)
        {

            if (b2csGridView.FocusedRowHandle < 0) return;
            if (e.KeyCode != Keys.Enter) return;
            var row = b2csGridView.GetRow(b2csGridView.FocusedRowHandle) as Gstrb2csDto;
            if (row == null) return;

            int fdate = Convert.ToInt32(fromDateEdit.DateTime.ToString("yyyyMMdd"));
            int tdate = Convert.ToInt32(toDateEdit.DateTime.ToString("yyyyMMdd"));

            var vw = new HSNDetailListView();
            KontoContext db = new KontoContext();

            
            var list = db.Database.SqlQuery<HsnSmryDetail>("dbo.GSTR3BDetails @CompanyId={0},@FromDate={1}," +
                            "@ToDate={2},@YearId={3}, @Type={4},@StateName={5},@gstin={6},@taxrate={7}",
                            KontoGlobals.CompanyId, fdate, tdate, KontoGlobals.YearId, "J",
                            row.StateName.Substring(3), row.GstIn, row.GSTRate).ToList();
            if (list.Count == 0) return;
            vw.RefreshGrid(list, row.StateName, "HSN");
            vw.ShowDialog(); ;
        }

        private void B2bGridControl_ProcessGridKey(object sender, KeyEventArgs e)
        {
            var gd = sender as GridControl;
            var vw = gd.MainView as GridView;
            if (vw.FocusedRowHandle < 0) return;
            if (e.KeyCode != Keys.Enter) return;
            var row = vw.GetRow(vw.FocusedRowHandle) as GstDto;
            if (row == null) return;
            CdnrZoom(row);
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
            wd.Workbook = new Aspose.Cells.Workbook("Excel\\GSTR1_Excel_Workbook_Template_V1.7.xlsx");

            Aspose.Cells.Worksheet w = wd.Workbook.Worksheets[1];

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

            if (File.Exists("Excel\\GSTR1_Excel_Workbook_Template_V1.7.xlsx"))
            {
                _db.Database.CommandTimeout = 0;

                ///////////////////////////////////B2B///////////////////////////////////////////////////////////////////////////////////                    
                lst = _db.Database.SqlQuery<GstDto>(
                    "dbo.GstReport @CompanyId={0},@FromDate={1},@ToDate={2},@yearid={3}",
                    Convert.ToInt32(KontoGlobals.CompanyId), fdate, tdate, KontoGlobals.YearId).ToList();



                int row = 5;

                var b2b = lst.Where(x => x.IsRevice == 0 && x.VTypeId == 12 && x.BillType =="Regular" && x.GSTRate > 0 && (x.Type == "REG")).ToList();

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
                       && (x.Type != "REG") && x.BillType=="Regular" && x.VTypeId == 12).ToList();
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
                 //   w.Cells["J" + row].PutValue(t.BondedWH);

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
                var cdnr = lst.Where(x => x.IsRevice == 0 && x.VTypeId != 12 && x.GSTRate > 0 && (x.Type == "REG")).ToList();
                w = wd.Workbook.Worksheets[7];
                row = 5;
                foreach (var t in cdnr)
                {
                    if (t.GstIn != "")
                    {
                        w.Cells["A" + row].PutValue(t.GstIn);
                        w.Cells["B" + row].PutValue(t.Account);
                        w.Cells["C" + row].PutValue(t.VoucherNo);
                        w.Cells["D" + row].PutValue(t.VoucherDate);
                        w.Cells["E" + row].PutValue(t.NoteType);
                        w.Cells["F" + row].PutValue(t.StateName);
                        w.Cells["G" + row].PutValue("N");
                        w.Cells["H" + row].PutValue("Regular");
                        w.Cells["I" + row].PutValue(t.BillAmount);
                        w.Cells["J" + row].PutValue("");
                        w.Cells["K" + row].PutValue(t.GSTRate);
                        w.Cells["L" + row].PutValue(t.TaxableValue);
                        w.Cells["M" + row].PutValue(t.Cess);
                        // 
                        //w.Cells["E" + row].PutValue(t.VoucherNo);
                        //w.Cells["F" + row].PutValue(t.VoucherDate);
                        //w.Cells["N" + row].PutValue("N");

                        row += 1;
                    }
                }

                ///////////////////////////////////CDNUR///////////////////////////////////////////////////////////////////////////////////
                //List = new ObservableCollection<GstDto>(_db.Database.SqlQuery<GstDto>(
                //    "dbo.GstReport @CompanyId={0},@TransTypeId={1},@TransTypeId1={2},@FromDate={3},@ToDate={4}", Convert.ToInt32(KontoGlobals.CompanyId), VoucherTypeEnum.DebitCreditNote, VoucherTypeEnum.SaleReturn, fdate, tdate).ToList());
                var cdnur = lst.Where(x => x.IsRevice == 0 && (x.Type != "REG") && x.BillAmount >= 250000
            && x.VTypeId != 12 && x.IGSTAmt > 0).ToList(); ;
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
                    w.Cells["E" + row].PutValue(t.StateName);
                    w.Cells["F" + row].PutValue(t.BillAmount);
                    w.Cells["H" + row].PutValue(t.GSTRate);
                    w.Cells["I" + row].PutValue(t.TaxableValue);

                    w.Cells["J" + row].PutValue(t.Cess);

                    //w.Cells["E" + row].PutValue(t.InvoiceNo);
                    //w.Cells["F" + row].PutValue(t.InvoiceDate);
                    
                    
                    //w.Cells["I" + row].PutValue("");
                    
                   
                  //  w.Cells["M" + row].PutValue("N");

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
                // Exempted
                w = wd.Workbook.Worksheets[17];
                var exms = exemptedGridControl.DataSource as List<GstrExempted>;
                row = 5;
                foreach (var exm in exms)
                {
                    w.Cells["A" + row].PutValue(exm.Descriptions);
                    w.Cells["B" + row].PutValue(exm.NilRated);
                    w.Cells["C" + row].PutValue(exm.Exempted);
                    w.Cells["D" + row].PutValue(exm.NonGst);
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

                Aspose.Cells.Worksheet w = wd.Workbook.Worksheets[1];

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
            
           

            GenerateData();

            var f = "excel\\GSTR3B_Excel_Utility_V4.1.xlsm";

            
            //ExcelEngine excelEngine = new ExcelEngine();
           // IWorkbook workbook = excelEngine.Excel.Workbooks.Open("excel\\gs3bview.xlsx");
            
            
          //  IWorksheet sheet = workbook.Worksheets[0];
          //
            GridExcelConverterControl excelConverter = new GridExcelConverterControl();
           // excelConverter.ExcelToGrid(sheet, this.gridControl2.Model);

            spreadsheetControl1.LoadDocument(f, DocumentFormat.Xlsm);

            DevExpress.Spreadsheet.IWorkbook workbook = spreadsheetControl1.Document;
            workbook.Worksheets.RemoveAt(0);


           // System.IO.FileStream s = new System.IO.FileStream(f, FileMode.Open, FileAccess.ReadWrite);

           // gridControl1.Sheets[0].OpenExcel(f, 1);
           // FarPoint.Win.Spread.SheetView newsheet = new FarPoint.Win.Spread.SheetView();
          //  s.Close();
            splashScreenManager1.SetWaitFormDescription("Generating 3b Register...");
            Gst3bShow();

          
          

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
            Exempted();
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

            DevExpress.Spreadsheet.IWorkbook WK = spreadsheetControl1.Document;
            var w = WK.Worksheets[0];

  //          var w = gridControl1.Sheets[0];

            //w.OperationMode = OperationMode.ReadOnly;
//            w.Cells[0, 0, 116, 8].Locked = true;
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

            var b2b1 = Gstrs.Where(x => x.IsRevice == 0  && x.VTypeId == 12 
                        && x.BillType == "Regular" && x.GSTRate > 0 && (x.Type=="REG")).ToList();
            b2bGridControl.DataSource = b2b1;
            b2bGridControl.RefreshDataSource();
        }

        private void Exempted()
        {
            var fdate = Convert.ToInt32(fromDateEdit.DateTime.ToString("yyyyMMdd"));
            var tdate = Convert.ToInt32(toDateEdit.DateTime.ToString("yyyyMMdd"));

            var exmps = new List<GstrExempted>();
            using (var db = new KontoContext())
            {
                db.Database.CommandTimeout = 0;
                exmps = db.Database.SqlQuery<GstrExempted>("dbo.gstr1_exempted @CompanyId={0},@FromDate={1},@ToDate={2},@yearid={3}",
                    KontoGlobals.CompanyId, fdate, tdate, KontoGlobals.YearId).ToList();
               
            }
            exemptedGridControl.DataSource = exmps.ToList();
            exemptedGridControl.RefreshDataSource();
        }

        private void B2BA()
        {
            

            var b2b1 = Gstrs.Where(x => x.IsRevice == 1 && (x.GstIn != null || x.GstIn.Length == 15) && (x.Type == "REG" || x.Type == "CMP") && x.VTypeId == 12).ToList();

            b2bAGridControl.DataSource = b2b1;
            b2bAGridControl.RefreshDataSource();

           
        }
        private void B2CL()
        {
            
            var b2b1 = Gstrs.Where(x => x.IsRevice == 0  && x.BillAmount >= 250000 && x.IGSTAmt > 0
                       && (x.Type != "REG" ) &&  x.BillType == "Regular" && x.VTypeId == 12).ToList();

            b2clGridControl.DataSource = b2b1;
            b2clGridControl.RefreshDataSource();

            
          
        }
        private void B2CLA()
        {
            
            var b2b1 = Gstrs.Where(x => x.IsRevice == 1 && (x.GstIn != null || x.GstIn.Length == 15) && x.BillAmount > 250000 && x.IGSTAmt > 0
                     && (x.Type == "REG" || x.Type == "CMP") && x.VTypeId == 12).ToList();

            b2claGridControl.DataSource = b2b1;
            b2claGridControl.RefreshDataSource();
           
        }
        void B2CS_Sheet()
        {
            // business to consumer B2CS
          
            var b2b1 = GstrB2CS.Where(x => x.IsRevice == 0).ToList();

            b2csGridControl.DataSource = b2b1;
            b2csGridControl.RefreshDataSource();
           
        }
        private void B2CSA()
        {
           

            var b2b1 = GstrB2CS.Where(x => x.IsRevice == 1).ToList();

            b2csaGridControl.DataSource = b2b1;
            b2csaGridControl.RefreshDataSource();
          


        }

        void CDNR_Sheet()
        {

            var cdnrs = Gstrs.Where(x => x.IsRevice == 0 && x.VTypeId != 12 && x.GSTRate > 0 && (x.Type == "REG")).ToList();

            cdnrGridControl.DataSource = cdnrs;
            cdnrGridControl.RefreshDataSource();
         

        }
        private void CDNRA_Sheet()
        {
            

            var cdnrs = Gstrs.Where(x => x.IsRevice == 1 && x.VTypeId != 12 && (x.Type == "REG" || x.Type == "CMP") &&
            (x.GstIn != null || x.GstIn.Length == 15)).ToList();

            cdnraGridControl.DataSource = cdnrs;
            cdnraGridControl.RefreshDataSource();
        }

        private void CDNUR_Sheet()
        {
            
            var b2b1 = Gstrs.Where(x => x.IsRevice == 0 && (x.Type!="REG" ) && x.IGSTPer > 0 && x.BillAmount >= 250000 
            && x.VTypeId !=12).ToList();


            cdnurGridControl.DataSource = b2b1;
            cdnurGridControl.RefreshDataSource();
        }
        private void CDNURA_Sheet()
        {
            
            int CRDRVType = (int)VoucherTypeEnum.DebitCreditNote;
            var b2b1 = Gstrs.Where(x => x.IsRevice == 1 && (x.GstIn == null || x.GstIn.Length == 0) && x.BillAmount > 250000
             && (x.Type == "REG" || x.Type == "CMP") && x.VTypeId == CRDRVType).ToList();

            cdnuraGridControl.DataSource = b2b1;
            cdnuraGridControl.RefreshDataSource();
            
        }
        private void Export_Sheet()
        {
            // Export
            

            var explist = Gstrs.Where(x => x.IsRevice == 0 && (x.BillType == "SEZ Supplies with Payment" || x.BillType == "SEZ Supplies without Payment"
                || x.BillType == "Deemed Exp") && x.VTypeId == 12).ToList();

            expGridControl.DataSource = explist;
            expGridControl.RefreshDataSource();

        }

        private void ExpA_Sheet()
        {
           
            var explist = Gstrs.Where(x => x.IsRevice == 1 && (x.BillType == "SEZ Supplies with Payment" || x.BillType == "SEZ Supplies without Payment"
                || x.BillType == "Deemed Exp") && x.VTypeId == 12).ToList();


            expaGridControl.DataSource = explist;
            expaGridControl.RefreshDataSource();

        }
        private void HSN_Sheet()
        {
            
            int fyear = Convert.ToInt32(KontoGlobals.DFromDate.ToString("yyyy"));

            int tyear = Convert.ToInt32(KontoGlobals.DToDate.ToString("yyyy"));
            //int monthno = int.Parse(monthLookUpEdit.EditValue.ToString());
            var fdate = Convert.ToInt32(fromDateEdit.DateTime.ToString("yyyyMMdd"));
            var tdate = Convert.ToInt32(toDateEdit.DateTime.ToString("yyyyMMdd"));

           
            List<Gstr1HsnDto> b2b1 = new List<Gstr1HsnDto>();
            using (var _db = new KontoContext())
            {

                _db.Database.CommandTimeout = 0;
                b2b1 = _db.Database.SqlQuery<Gstr1HsnDto>(
                     "dbo.HsnSummary @CompanyId={0},@FromDate={1},@ToDate={2}",
                     Convert.ToInt32(KontoGlobals.CompanyId), fdate, tdate).ToList();
            }

            hsnGridControl.DataSource = b2b1;
            hsnGridControl.RefreshDataSource();

        }
        private void Doc_Sheet()
        {
            
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

            docGridControl.DataSource = b2b1;
            docGridControl.RefreshDataSource();
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

        private void tabBarPage2_Click(object sender, EventArgs e)
        {

        }

        private void b2csGridControl_Click(object sender, EventArgs e)
        {

        }
    }
}

