using Aspose.Cells;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Admin.Dtos;
using Konto.Data.Models.Reports;
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

namespace Konto.Reporting.Para.Gstr2
{
    public partial class GsttwoMainView : KontoForm
    {
        public GsttwoMainView()
        {
            InitializeComponent();
            okSimpleButton.Click += OkSimpleButton_Click;
            this.FormClosed += GsttwoMainView_FormClosed;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.gstr2SimpleButton.Click += Gstr2SimpleButton_Click;
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Purchase Invoice", "PINVOICE"),
                new ComboBoxPairs("Purchase Return", "PRETURN"),
                new ComboBoxPairs("General Expense", "GEXPENSE"),
                new ComboBoxPairs("Debit Note", "DEBIT"),
                new ComboBoxPairs("Credit Note", "CREDIT"),
                new ComboBoxPairs("ALL", "ALL"),

            };
            viewLookUpEdit.Properties.DataSource = cbp;

            fDateEdit.EditValue = KontoGlobals.DFromDate;
            tDateEdit.EditValue = KontoGlobals.DToDate;
            viewLookUpEdit.EditValue = "PINVOICE";
        }

        private void Gstr2SimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists("gstr2.xlsx") && this.IsFileLocked(new FileInfo("gstr2.xlsx")))
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
            catch (Exception)
            {

                throw;
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
            if (!File.Exists("Excel\\GSTR2_Excel_Workbook_TemplateNew_V1.1.xlsx"))
            {
                MessageBox.Show("Excel Template File Not Found.. Please Contact You Vendor..");
                return;
            }
            

                var lst = new List<GsttwoDto>();
            WorkbookDesigner wd = new WorkbookDesigner();
            wd.Workbook = new Aspose.Cells.Workbook("Excel\\GSTR2_Excel_Workbook_TemplateNew_V1.1.xlsx");

       

            int fyear = Convert.ToInt32(KontoGlobals.DFromDate.ToString("yyyy"));

            int tyear = Convert.ToInt32(KontoGlobals.DToDate.ToString("yyyy"));
            //int monthno = int.Parse(monthLookUpEdit.EditValue.ToString());
            int fdate = Convert.ToInt32(fDateEdit.DateTime.ToString("yyyyMMdd"));
            int tdate = Convert.ToInt32(tDateEdit.DateTime.ToString("yyyyMMdd"));
            var billtype = "";
            var Billag = "";
            using (var _db = new KontoContext())
            {
                _db.Database.CommandTimeout = 0;

                lst = _db.Database.SqlQuery<GsttwoDto>(
                       "dbo.Gstr2Report @CompanyId={0},@TransTypeId={1},@BillType={2}," +
                       "@FromDate={3},@ToDate={4},@Billag={5},@YearId={6},@TransTypeId1={7}",
                       Convert.ToInt32(KontoGlobals.CompanyId), 0, billtype, fdate, tdate,
                       Billag, KontoGlobals.YearId, 0).ToList();
               
            }

            var b2b = lst.Where(x => x.VTypeId != (int)VoucherTypeEnum.PurchaseReturn && x.VTypeId != (int)VoucherTypeEnum.DebitCreditNote
                         && (x.Type == "REG")).ToList();

           
            // B2B sheer============================================================
            Aspose.Cells.Worksheet w = wd.Workbook.Worksheets[1]; //b2b
            int row = 5;
            foreach (var t in b2b)
            {
                w.Cells["A" + row].PutValue(t.GstIn);
                w.Cells["B" + row].PutValue(t.BillNo);
                w.Cells["C" + row].PutValue(t.Date);
                w.Cells["D" + row].PutValue(t.BillAmount);
                w.Cells["E" + row].PutValue(t.StateName);
                w.Cells["F" + row].PutValue("N");
                w.Cells["G" + row].PutValue(t.BillType);
                w.Cells["H" + row].PutValue(t.GSTRate);
                w.Cells["I" + row].PutValue(t.TaxableValue);

                w.Cells["J" + row].PutValue(t.IGSTAmt);
                w.Cells["K" + row].PutValue(t.CGSTAmt);
                w.Cells["L" + row].PutValue(t.SGSTAmt);
                w.Cells["M" + row].PutValue(t.Cess);

                if( t.Itc.Contains("Ineligible"))
                    w.Cells["N" + row].PutValue("Ineligible");
                else
                {
                    w.Cells["N" + row].PutValue(t.Itc);
                }

                if (!t.Itc.Contains("Ineligible"))
                {
                    w.Cells["O" + row].PutValue(t.IGSTAmt);
                    w.Cells["P" + row].PutValue(t.CGSTAmt);
                    w.Cells["Q" + row].PutValue(t.SGSTAmt);
                    w.Cells["R" + row].PutValue(t.Cess);
                }
                else
                {
                    w.Cells["O" + row].PutValue(0);
                    w.Cells["P" + row].PutValue(0);
                    w.Cells["Q" + row].PutValue(0);
                    w.Cells["R" + row].PutValue(0);
                }

                row += 1;

            }


            // B2b UNREGISTER==========================
            var b2bur = lst.Where(x => x.VTypeId != (int)VoucherTypeEnum.PurchaseReturn && x.VTypeId != (int)VoucherTypeEnum.DebitCreditNote
                         && (x.Type == "URD")).ToList();


            // B2B sheer============================================================
             w = wd.Workbook.Worksheets[2]; //b2bur
             row = 5;
            foreach (var t in b2bur)
            {
                w.Cells["A" + row].PutValue(t.Account);
                w.Cells["B" + row].PutValue(t.BillNo);
                w.Cells["C" + row].PutValue(t.Date);
                w.Cells["D" + row].PutValue(t.BillAmount);
                w.Cells["E" + row].PutValue(t.StateName);
                
                w.Cells["F" + row].PutValue(t.BillType);
                w.Cells["G" + row].PutValue(t.GSTRate);
                w.Cells["H" + row].PutValue(t.TaxableValue);

                w.Cells["I" + row].PutValue(t.IGSTAmt);
                w.Cells["J" + row].PutValue(t.CGSTAmt);
                w.Cells["K" + row].PutValue(t.SGSTAmt);
                w.Cells["L" + row].PutValue(t.Cess);
                
                if (t.Itc.Contains("Ineligible"))
                    w.Cells["M" + row].PutValue("Ineligible");
                else
                {
                    w.Cells["M" + row].PutValue(t.Itc);
                }

                if (t.Itc != "Ineligible")
                {
                    w.Cells["N" + row].PutValue(t.IGSTAmt);
                    w.Cells["O" + row].PutValue(t.CGSTAmt);
                    w.Cells["P" + row].PutValue(t.SGSTAmt);
                    w.Cells["Q" + row].PutValue(t.Cess);
                }
                else
                {
                    w.Cells["N" + row].PutValue(0);
                    w.Cells["O" + row].PutValue(0);
                    w.Cells["P" + row].PutValue(0);
                    w.Cells["Q" + row].PutValue(0);
                }

                row += 1;

            }

            // IMPORT SERVICE==================================

            var imps = lst.Where(x => x.VTypeId != (int)VoucherTypeEnum.PurchaseReturn && x.VTypeId != (int)VoucherTypeEnum.DebitCreditNote
                        && (x.BillType == "Import" || x.BillType== "Received from SEZ") && x.Itc.Contains("Services")).ToList();




            w = wd.Workbook.Worksheets[3]; //import
            row = 5;
            foreach (var t in imps)
            {
              
                w.Cells["A" + row].PutValue(t.BillNo);
                w.Cells["B" + row].PutValue(t.Date);
                w.Cells["c" + row].PutValue(t.BillAmount);
                w.Cells["D" + row].PutValue(t.StateName);

               
                w.Cells["E" + row].PutValue(t.GSTRate);
                w.Cells["F" + row].PutValue(t.TaxableValue);

                w.Cells["G" + row].PutValue(t.IGSTAmt);
                w.Cells["H" + row].PutValue(t.Cess);
                
                w.Cells["I" + row].PutValue(t.Itc);


                if (t.Itc.Contains("Ineligible"))
                    w.Cells["I" + row].PutValue("Ineligible");
                else
                {
                    w.Cells["I" + row].PutValue(t.Itc);
                }

                if (!t.Itc.Contains("Ineligible"))
                {
                    w.Cells["J" + row].PutValue(t.IGSTAmt);
                    w.Cells["K" + row].PutValue(t.Cess);
                }
                else
                {
                    w.Cells["J" + row].PutValue(0);
                    w.Cells["K" + row].PutValue(0);
                }

                row += 1;

            }

            // IMPORT GOODS==================================

            var impgs = lst.Where(x => x.VTypeId != (int)VoucherTypeEnum.PurchaseReturn && x.VTypeId != (int)VoucherTypeEnum.DebitCreditNote
                        && (x.BillType == "Import" || x.BillType == "Received from SEZ") &&! x.Itc.Contains("Services")).ToList();




            w = wd.Workbook.Worksheets[4]; //import GOODS
            row = 5;
            foreach (var t in impgs)
            {
                w.Cells["A" + row].PutValue(t.PortCode);
                w.Cells["B" + row].PutValue(t.BillNo);
                w.Cells["C" + row].PutValue(t.Date);
                w.Cells["D" + row].PutValue(t.BillAmount);
                w.Cells["E" + row].PutValue(t.BillType);
                w.Cells["F" + row].PutValue(t.GstIn);

                w.Cells["G" + row].PutValue(t.GSTRate);
                w.Cells["H" + row].PutValue(t.TaxableValue);

                w.Cells["I" + row].PutValue(t.IGSTAmt);
                w.Cells["J" + row].PutValue(t.Cess);

                if (t.Itc.Contains("Ineligible"))
                    w.Cells["K" + row].PutValue("Ineligible");
                else
                {
                    w.Cells["K" + row].PutValue(t.Itc);
                }

                if (!t.Itc.Contains("Ineligible"))
                {
                    w.Cells["L" + row].PutValue(t.IGSTAmt);
                    w.Cells["M" + row].PutValue(t.Cess);
                }
                else
                {
                    w.Cells["L" + row].PutValue(0);
                    w.Cells["M" + row].PutValue(0);
                }


                row += 1;

            }


            // CDNR sheer============================================================

            var cdnrs = lst.Where(x => (x.VTypeId == (int)VoucherTypeEnum.PurchaseReturn || x.VTypeId == (int)VoucherTypeEnum.DebitCreditNote)
                         && (x.Type == "REG")).ToList();


            
             w = wd.Workbook.Worksheets[5]; //cdnr
                 row = 5;
            foreach (var t in cdnrs)
            {
                w.Cells["A" + row].PutValue(t.GstIn);
                w.Cells["B" + row].PutValue(t.VNo);
                w.Cells["C" + row].PutValue(t.VoucherDate);
                
                w.Cells["D" + row].PutValue(t.BillNo);
                w.Cells["E" + row].PutValue(t.Date);
                w.Cells["F" + row].PutValue("N");
                w.Cells["G" + row].PutValue(t.NoteType);
                w.Cells["H" + row].PutValue(t.Reason);

                if(t.IGSTAmt != 0)
                    w.Cells["I" + row].PutValue("Inter State");
                else
                    w.Cells["I" + row].PutValue("Intra State");


                
                w.Cells["K" + row].PutValue(t.GSTRate);
                if (t.BillAmount < 0)
                {
                    w.Cells["L" + row].PutValue(-1 * t.TaxableValue);
                    w.Cells["J" + row].PutValue(-1 * t.BillAmount);
                    w.Cells["M" + row].PutValue(-1 * t.IGSTAmt);
                    w.Cells["N" + row].PutValue(-1 * t.CGSTAmt);
                    w.Cells["O" + row].PutValue(-1 * t.SGSTAmt);
                    w.Cells["P" + row].PutValue(-1 * t.Cess);
                }
                else
                {
                    w.Cells["L" + row].PutValue(t.TaxableValue);
                    w.Cells["J" + row].PutValue(t.BillAmount);
                    w.Cells["M" + row].PutValue(t.IGSTAmt);
                    w.Cells["N" + row].PutValue( t.CGSTAmt);
                    w.Cells["O" + row].PutValue( t.SGSTAmt);
                    w.Cells["P" + row].PutValue(t.Cess);
                }

                if (t.Itc != null && t.Itc.Contains("Ineligible"))
                    w.Cells["Q" + row].PutValue("Ineligible");
                else
                {
                    w.Cells["Q" + row].PutValue(t.Itc);
                }

                if (t.Itc !=null && !t.Itc.Contains("Ineligible"))
                {
                    if (t.BillAmount < 0)
                    {
                        w.Cells["R" + row].PutValue(-1 * t.IGSTAmt);
                        w.Cells["S" + row].PutValue(-1 * t.CGSTAmt);
                        w.Cells["T" + row].PutValue(-1 * t.SGSTAmt);
                        w.Cells["U" + row].PutValue(-1 * t.Cess);
                    }
                    else
                    {
                        w.Cells["R" + row].PutValue(t.IGSTAmt);
                        w.Cells["S" + row].PutValue(t.CGSTAmt);
                        w.Cells["T" + row].PutValue(t.SGSTAmt);
                        w.Cells["U" + row].PutValue(t.Cess);
                    }
                }
                else
                {
                    w.Cells["R" + row].PutValue(0);
                    w.Cells["S" + row].PutValue(0);
                    w.Cells["T" + row].PutValue(0);
                    w.Cells["U" + row].PutValue(0);
                }

                row += 1;

            }




            // CDNUR sheeT============================================================

            var cdnurs = lst.Where(x => (x.VTypeId == (int)VoucherTypeEnum.PurchaseReturn || x.VTypeId == (int)VoucherTypeEnum.DebitCreditNote)
                         && (x.Type == "URD")).ToList();



            w = wd.Workbook.Worksheets[6]; //cdnur
            row = 5;
            foreach (var t in cdnurs)
            {
                w.Cells["A" + row].PutValue(t.VNo);
                w.Cells["B" + row].PutValue(t.VoucherDate);

                w.Cells["C" + row].PutValue(t.BillNo);
                w.Cells["D" + row].PutValue(t.Date);
                w.Cells["E" + row].PutValue("N");
                w.Cells["F" + row].PutValue(t.NoteType);
                w.Cells["G" + row].PutValue(t.Reason);

                if (t.IGSTAmt > 0)
                    w.Cells["H" + row].PutValue("Inter State");
                else
                    w.Cells["H" + row].PutValue("Intra State");

                if(t.BillType =="Import" || t.BillType== "Received from SEZ")
                    w.Cells["I" + row].PutValue("IMPS");
                else
                    w.Cells["I" + row].PutValue("B2BUR");


                
                w.Cells["K" + row].PutValue(t.GSTRate);

                if (t.BillAmount < 0)
                {
                    w.Cells["L" + row].PutValue(-1 * t.TaxableValue);
                    w.Cells["J" + row].PutValue(-1 * t.BillAmount);

                    w.Cells["M" + row].PutValue(-1 * t.IGSTAmt);
                    w.Cells["N" + row].PutValue(-1 * t.CGSTAmt);
                    w.Cells["O" + row].PutValue(-1 * t.SGSTAmt);
                    w.Cells["P" + row].PutValue(-1 * t.Cess);
                }
                else
                {
                    w.Cells["L" + row].PutValue( t.TaxableValue);
                    w.Cells["J" + row].PutValue( t.BillAmount);

                    w.Cells["M" + row].PutValue( t.IGSTAmt);
                    w.Cells["N" + row].PutValue( t.CGSTAmt);
                    w.Cells["O" + row].PutValue( t.SGSTAmt);
                    w.Cells["P" + row].PutValue(t.Cess);
                }

                if (t.Itc != null && t.Itc.Contains("Ineligible"))
                    w.Cells["Q" + row].PutValue("Ineligible");
                else
                {
                    w.Cells["Q" + row].PutValue(t.Itc);
                }

                if (t.Itc != null && !t.Itc.Contains("Ineligible"))
                {
                    if (t.BillAmount < 0)
                    {
                        w.Cells["R" + row].PutValue(-1 * t.IGSTAmt);
                        w.Cells["S" + row].PutValue(-1 * t.CGSTAmt);
                        w.Cells["T" + row].PutValue(-1 * t.SGSTAmt);
                        w.Cells["U" + row].PutValue(-1 * t.Cess);
                    }
                    else
                    {
                        w.Cells["R" + row].PutValue( t.IGSTAmt);
                        w.Cells["S" + row].PutValue( t.CGSTAmt);
                        w.Cells["T" + row].PutValue( t.SGSTAmt);
                        w.Cells["U" + row].PutValue( t.Cess);
                    }
                }
                else
                {
                    w.Cells["R" + row].PutValue(0);
                    w.Cells["S" + row].PutValue(0);
                    w.Cells["T" + row].PutValue(0);
                    w.Cells["U" + row].PutValue(0);
                }

                row += 1;

            }


            ///////////////////////////////////HsnSummary///////////////////////////////////////////////////////////////////////////////////
            ///

            var hsn = new List<Gstr1HsnDto>();

            using (var db = new KontoContext())
            {
                db.Database.CommandTimeout = 0;

                 hsn = db.Database.SqlQuery<Gstr1HsnDto>(
                    "dbo.Hsn_pur_Summary @CompanyId={0},@FromDate={1},@ToDate={2}",
                    Convert.ToInt32(KontoGlobals.CompanyId), fdate, tdate).ToList();
            }
            
            string _uqc = "NA";

            if (System.Configuration.ConfigurationManager.AppSettings["Uqc"] != null)
                _uqc = System.Configuration.ConfigurationManager.AppSettings["Uqc"];

            w = wd.Workbook.Worksheets[11];
            row = 5;

            foreach (var t in hsn)
            {
                w.Cells["A" + row].PutValue(t.HsnCode);
                w.Cells["B" + row].PutValue(t.Description);

                if (_uqc == "NA") // defaul uqc
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

            wd.Process(true);
            wd.Workbook.Save("gstr2.xlsx");

            DirectoryInfo info = new DirectoryInfo("gstr2.xlsx");
            string windir = Environment.GetEnvironmentVariable("WINDIR");
            Process prc = new Process();
            prc.StartInfo.FileName = windir + @"\explorer.exe";
            prc.StartInfo.Arguments = System.Windows.Forms.Application.StartupPath + "\\gstr2.xlsx";
            prc.Start();

        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void GsttwoMainView_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage != null)
            {
                var tabcontrol = tabpage.Parent as TabControlAdv;
                if (tabcontrol != null)
                    tabcontrol.TabPages.Remove(tabpage);
            }
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                var fdate = Convert.ToInt32(fDateEdit.DateTime.ToString("yyyyMMdd"));
                var tdate = Convert.ToInt32(tDateEdit.DateTime.ToString("yyyyMMdd"));
                int typeid = 0;
                int typeId1 = 0;
                var billtype = "";
                var Billag = "";
                if (viewLookUpEdit.EditValue.ToString() == "PINVOICE")
                {
                    typeid = (int)VoucherTypeEnum.PurchaseInvoice;
                }
                else if (viewLookUpEdit.EditValue.ToString() == "PRETURN")
                {
                    typeid = (int)VoucherTypeEnum.PurchaseReturn;
                }
                else if (viewLookUpEdit.EditValue.ToString() == "GEXPENSE")
                {
                    typeid = (int)VoucherTypeEnum.GenExpense;
                }
                else if (viewLookUpEdit.EditValue.ToString() == "DEBIT")
                {
                    typeid = (int)VoucherTypeEnum.DebitCreditNote;
                    billtype = "DEBIT NOTE";
                    Billag = "PURCHASE";

                }
                else if (viewLookUpEdit.EditValue.ToString() == "CREDIT")
                {
                    typeid = (int)VoucherTypeEnum.DebitCreditNote;
                    billtype = "CREDIT NOTE";
                    Billag = "PURCHASE";
                }
                else
                {
                    typeid = 0;
                    typeId1 = 0;
                }

                using (var _db = new KontoContext())
                {
                    var lst = _db.Database.SqlQuery<GsttwoDto>(
                           "dbo.Gstr2Report @CompanyId={0},@TransTypeId={1},@BillType={2}," +
                           "@FromDate={3},@ToDate={4},@Billag={5},@YearId={6},@TransTypeId1={7}",
                           Convert.ToInt32(KontoGlobals.CompanyId), typeid, billtype, fdate, tdate,
                           Billag, KontoGlobals.YearId, typeId1).ToList();
                    gsttwoDtoBindingSource.DataSource = lst;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
           
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
