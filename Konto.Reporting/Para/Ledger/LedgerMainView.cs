using DevExpress.Data.WcfLinq.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Masters.Dtos;
using Konto.Data.Models.Reports;
using Konto.Pos.PR;
using Konto.Pos.Purchase;
using Konto.Pos.Sales;
using Konto.Pos.SR;
using Konto.Shared.Account.DRCRNote;
using Konto.Shared.Account.GenExpense;
using Konto.Shared.Account.Jv;
using Konto.Shared.Account.Payment;
using Konto.Shared.Account.Receipt;
using Konto.Shared.Trans.PInvoice;
using Konto.Shared.Trans.PReturn;
using Konto.Shared.Trans.SInvoice;
using Konto.Shared.Trans.SReturn;
using Konto.Trading.GP;
using Konto.Trading.JobReceipt;
using Konto.Trading.MillReceipt;
using Serilog;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Reporting.Para.Ledger
{
    public partial class LedgerMainView : KontoForm
    {
        private string GridLayoutFileName { get; set; }
        public int AccId { get; set; }
        public DateTime _fromDate { get; set; }
        public DateTime _toDate { get; set; }

        public LedgerMainView()
        {
            InitializeComponent();
            this.tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;
            fDateEdit.DateTime = KontoGlobals.DFromDate;
            tDateEdit.DateTime = KontoGlobals.DToDate;
            this.GridLayoutFileName = KontoFileLayout.Ledger_Details_View;
            this.monthlyGridView.CustomDrawGroupRow += MonthlyGridView_CustomDrawGroupRow;
            this.gridView1.CustomDrawGroupRow += GridView1_CustomDrawGroupRow;
            this.detailsGridControl1.ProcessGridKey += DetailsGridControl1_ProcessGridKey;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.multiSimpleButton.Click += MultiSimpleButton_Click;
            this.monthlyGridView.KeyDown += MonthlyGridView_KeyDown;
            this.FormClosed += LedgerMainView_FormClosed;
            this.Load += LedgerMainView_Load;
            this.printSimpleButton.Click += PrintSimpleButton_Click;
            outsSimpleButton.Click += OutsSimpleButton_Click;
            gridView1.FocusedRowChanged += GridView1_FocusedRowChanged;
            this.FirstActiveControl = fDateEdit;
            //using(var db = new KontoContext())
            //{
            //    var cmp = (from f in db.Companies
            //               orderby f.CompName
            //               select new BaseLookupDto { DisplayText = f.CompName, Id = f.Id }).ToList();

            //    lookUpEdit1.Properties.DataSource = cmp;

            //}
        }

        private void GridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                var rw = gridView1.GetRow(e.FocusedRowHandle) as LedgertransDto;
                if (rw == null) return;

                if (rw.VTypeId == (int)VoucherTypeEnum.SaleInvoice || rw.VTypeId == (int)VoucherTypeEnum.PurchaseInvoice
                    || rw.VTypeId == (int)VoucherTypeEnum.SaleReturn || rw.VTypeId == (int)VoucherTypeEnum.PurchaseReturn
                    || rw.VTypeId == (int)VoucherTypeEnum.DebitCreditNote || rw.VTypeId == (int)VoucherTypeEnum.GenExpense
                    || rw.VTypeId == (int)VoucherTypeEnum.MillReceiptVoucher || rw.VTypeId == (int)VoucherTypeEnum.JobReceiptVoucher)
                {
                    using(var _context = new KontoContext())
                    {
                        var bll = _context.Bills.FirstOrDefault(x => x.RowId == rw.RefRowID);
                        if (bll == null) return;

                        var _lst = (from bt in _context.BillTrans
                                    join p in _context.Products on bt.ProductId equals p.Id into join_pd
                                    from pd in join_pd.DefaultIfEmpty()
                                    join um in _context.Uoms on bt.UomId equals um.Id
                                    orderby bt.Id
                                    where bt.BillId == bll.Id && !bt.IsDeleted
                                    select new LedgerItemDetailsDto
                                    {
                                        Description = pd.ProductName != null ? pd.ProductName : bt.Remark,
                                        Remark = bt.Remark,
                                        DiscAmt = bt.DiscAmt,
                                        GstAmt = bt.Cgst + bt.Sgst + bt.Igst,
                                        GstRate = bt.CgstPer + bt.SgstPer + bt.IgstPer,
                                        NetTotal = bt.NetTotal,
                                        Pcs = bt.Pcs,
                                        Qty = bt.Qty,
                                        Rate = bt.Rate,
                                        Taxable = bt.NetTotal - bt.Cgst - bt.Sgst - bt.Igst,
                                        Unit = um.UnitName
                                    }).ToList();

                        itemCustomGridControl.DataSource = _lst;
                        itemCustomGridControl.RefreshDataSource();
                        itemCustomGridView.BestFitColumns();
                        
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void OutsSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(accLookup1.SelectedValue) == 0)
                {
                    MessageBox.Show("Please select a account to print..");
                    accLookup1.Focus();
                    return;
                }

                GrapeCity.ActiveReports.PageReport _pageReport = new GrapeCity.ActiveReports.PageReport();

                string dr = "outs\\outs_ar.rdlx";


                _pageReport.Load(new System.IO.FileInfo(dr));

                _pageReport.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;
                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(_pageReport);
                doc.Parameters["partyid"].CurrentValue = Convert.ToInt32(accLookup1.SelectedValue);
                doc.Parameters["GroupOn"].CurrentValue = "None";
                doc.Parameters["paid"].CurrentValue = "UNPAID";
                doc.Parameters["branchid"].CurrentValue = 0;
                doc.Parameters["companyid"].CurrentValue = KontoGlobals.CompanyId;
                doc.Parameters["yearid"].CurrentValue = KontoGlobals.YearId;
                doc.Parameters["fromdate"].CurrentValue = Convert.ToInt32(this.fDateEdit.DateTime.ToString("yyyyMMdd"));
                doc.Parameters["todate"].CurrentValue = Convert.ToInt32(this.tDateEdit.DateTime.ToString("yyyyMMdd"));

                doc.Parameters["payfromdate"].CurrentValue = 20000401;

                doc.Parameters["paytodate"].CurrentValue = Convert.ToInt32(this.tDateEdit.DateTime.ToString("yyyyMMdd"));
                doc.Parameters["reportid"].CurrentValue = 0;
                doc.Parameters["report_title"].CurrentValue = "Outstanding" + " For The Period " + this.fDateEdit.DateTime.ToShortDateString() + " To " +
                                                              this.tDateEdit.DateTime.ToShortDateString();

                var db = new KontoContext();
                var grp = db.AcGroupModels.Find(accLookup1.LookupDto.GroupId);
                if (grp != null)
                {
                    if (grp.Extra1!=null && grp.Extra1 == "R")
                    {
                        doc.Parameters["nature"].CurrentValue = "R";
                    }
                    else
                    {
                        doc.Parameters["nature"].CurrentValue = "P";
                    }
                }

                var _tab = this.Parent.Parent as TabControlAdv;
                if (_tab == null) return;
                var frm = new KontoRepViewer(doc);
                frm.Text = accLookup1.SelectedText; 

                var pg1 = new TabPageAdv();
                pg1.Text = "Oustanding Print";
                _tab.TabPages.Add(pg1);
              
                frm.TopLevel = false;
                frm.Parent = pg1;
                _tab.SelectedTab = pg1;
                //frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                frm.Show();// = true;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                Log.Error(ex.ToString());
            }
        }

        private void PrintSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(accLookup1.SelectedValue) == 0)
                {
                    MessageBox.Show("Please select a account to print..");
                    accLookup1.Focus();
                    return;
                }

                GrapeCity.ActiveReports.PageReport _pageReport = new GrapeCity.ActiveReports.PageReport();

                string dr = "outs\\ledger_details.rdlx";


                _pageReport.Load(new System.IO.FileInfo(dr));

                _pageReport.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;
                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(_pageReport);

                doc.Parameters["partyid"].CurrentValue = Convert.ToInt32(accLookup1.SelectedValue);
                doc.Parameters["branchid"].CurrentValue = 0;

                doc.Parameters["companyid"].CurrentValue = KontoGlobals.CompanyId;
                doc.Parameters["yearid"].CurrentValue = KontoGlobals.YearId;
                doc.Parameters["fromdate"].CurrentValue = Convert.ToInt32(this.fDateEdit.DateTime.ToString("yyyyMMdd"));
                doc.Parameters["todate"].CurrentValue = Convert.ToInt32(this.tDateEdit.DateTime.ToString("yyyyMMdd"));
                if (doc.Parameters.Contains("from_date"))
                    doc.Parameters["from_date"].CurrentValue = this.fDateEdit.DateTime.ToString("dd-MM-yyyy");

                if (doc.Parameters.Contains("to_date"))
                    doc.Parameters["to_date"].CurrentValue = this.tDateEdit.DateTime.ToString("dd-MM-yyyy");

                doc.Parameters["reportid"].CurrentValue = 0;
                doc.Parameters["report_title"].CurrentValue = "Ledger" + " For The Period " + this.fDateEdit.DateTime.ToShortDateString() + " To " +
                                                              this.tDateEdit.DateTime.ToShortDateString();

                var frm = new KontoRepViewer(doc);
               
                    var _tab = this.Parent.Parent as TabControlAdv;
                    if (_tab == null) return;

                    var pg1 = new TabPageAdv();
                    pg1.Text = "Ledger Print";
                    _tab.TabPages.Add(pg1);
                    
                    frm.TopLevel = false;
                    frm.Text = accLookup1.SelectedText;
                    frm.Parent = pg1;
                    _tab.SelectedTab = pg1;
                 //   frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                    frm.Show();// = true;
                    
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                Log.Error(ex.ToString());
            }
           
        }

        private void DetailsGridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Back)
            {
                radioGroup1.EditValue = "M";
                okSimpleButton.PerformClick();
            }

            if (e.KeyCode != Keys.Enter) return;
            var rw = gridView1.GetRow(gridView1.FocusedRowHandle) as LedgertransDto;
            if (rw == null)
                return;

            ShowZoom(rw);
        }
        private void ShowZoom(LedgertransDto err)
        {
            var rowno = gridView1.FocusedRowHandle;
            var db = new KontoContext();
            var bll = db.Bills.FirstOrDefault(x => x.RowId == err.RefRowID);
            if (bll == null) return;
            var vw = new KontoMetroForm();
            if (err.VTypeId == (int)VoucherTypeEnum.SaleInvoice) 
            {
                if (KontoGlobals.PackageId == (int)PackageType.POS)
                    vw = new SalesIndex();
                else
                    vw = new SInvoiceIndex();
            }
            else if (err.VTypeId == (int)VoucherTypeEnum.PurchaseInvoice)
            {
                if (KontoGlobals.PackageId == (int)PackageType.POS)
                    vw = new PurchaseIndex();
                else
                    vw = new PInvoiceIndex();
            }
            else if (err.VTypeId == (int)VoucherTypeEnum.SaleReturn)
            {
                if (KontoGlobals.PackageId == (int)PackageType.POS)
                    vw = new SRIndex();
                else
                    vw = new SReturnIndex();
            }
            else if (err.VTypeId == (int)VoucherTypeEnum.PurchaseReturn)
            {
                if (KontoGlobals.PackageId == (int)PackageType.POS)
                    vw = new PRIndex();
                    else  
                    vw = new PReturnIndex();
            }
            else if (err.VTypeId == (int)VoucherTypeEnum.GrayPurchaseInvoice)
            {
                vw = new GPIndex();
            }
            else if (err.VTypeId == (int)VoucherTypeEnum.ReceiptVoucher)
            {
                vw = new ReceiptIndex();
            }
            else if (err.VTypeId == (int)VoucherTypeEnum.PaymentVoucher)
            {
                vw = new PaymentIndex();
            }
            else if (err.VTypeId == (int)VoucherTypeEnum.JournalVoucher)
            {
                vw = new JvIndex();
            }
            else if (err.VTypeId == (int)VoucherTypeEnum.DebitCreditNote)
            {
                vw = new DRCRNoteIndex();
            }
            else if (err.VTypeId == (int)VoucherTypeEnum.GenExpense)
            {
                vw = new GenExpIndex();
            }
            else if (err.VTypeId == (int)VoucherTypeEnum.MillReceiptVoucher)
            {
                vw = new MrvIndex();
            }
            else if (err.VTypeId == (int)VoucherTypeEnum.JobReceiptVoucher)
            {
                vw = new JrIndex();
            }
           
            vw.OpenForLookup = true;
            vw.EditKey = bll.Id;
            vw.ShowDialog();
            okSimpleButton.PerformClick();
            gridView1.FocusedRowHandle = rowno;
            gridView1.Focus();

        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void LedgerMainView_Load(object sender, EventArgs e)
        {
            if (this.AccId > 0)
            {
                this.fDateEdit.DateTime = this._fromDate;
                this.tDateEdit.DateTime = this._toDate;
                this.accLookup1.SelectedValue = this.AccId;
                this.accLookup1.SetAcc(this.AccId);
                okSimpleButton.PerformClick();
            }
            else
            {
                this.ActiveControl = accLookup1;
            }
        }

        private void LedgerMainView_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage != null)
            {
                var tabcontrol = tabpage.Parent as TabControlAdv;
                if (tabcontrol != null)
                    tabcontrol.TabPages.Remove(tabpage);
            }
        }

        private void GridView1_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            GridGroupRowInfo info = e.Info as GridGroupRowInfo;
            var bal = view.GetGroupRowValue(e.RowHandle, colBal);
            info.GroupText = view.GetGroupRowValue(e.RowHandle, info.Column) + "  <color=#e3165b>" + bal + "</color>";
        }

        private void MonthlyGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (this.monthlyGridView.IsGroupRow(monthlyGridView.FocusedRowHandle))
            {
                var fdate = Convert.ToInt32(this.fDateEdit.DateTime.ToString("yyyyMMdd"));
                var tdate = Convert.ToInt32(this.tDateEdit.DateTime.ToString("yyyyMMdd"));
                var acid = Convert.ToInt32( monthlyGridView.GetGroupRowValue(monthlyGridView.FocusedRowHandle, colAccId));

                GenerateDetails(acid,fdate,tdate);
            }
            else
            {
                if (monthlyGridView.FocusedRowHandle == 0) return;
                var monthno = Convert.ToInt32(monthlyGridView.GetRowCellValue(monthlyGridView.FocusedRowHandle, colFinMonth));
                var yearno = Convert.ToInt32(monthlyGridView.GetRowCellValue(monthlyGridView.FocusedRowHandle, colFinYear));
                var tdate = Convert.ToInt32(yearno.ToString() + monthno.ToString().PadLeft(2, '0') + DateTime.DaysInMonth(yearno, monthno).ToString());
                var fdate = Convert.ToInt32( yearno.ToString() + monthno.ToString().PadLeft(2, '0') + "01");
                var acid = Convert.ToInt32(monthlyGridView.GetRowCellValue(monthlyGridView.FocusedRowHandle, colAccId));
                GenerateDetails(acid, fdate, tdate);
            }

        }
        private void GenerateDetails(int accid,int fdate, int tdate, string _party = "N", string _group = "N",int reportid=0)
        {
            monthlyLayoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            detailsLayoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            itemLayoutControlItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            var Trans = new List<LedgertransDto>();
            radioGroup1.SelectedIndex = 1;
        
            using (var db = new KontoContext())
            {
                db.Database.CommandTimeout = 0;
                Trans = db.Database.SqlQuery<LedgertransDto>(
                       "dbo.Ledger_Reports @companyid={0},@fromdate={1},@todate={2},@party={3}" +
                       ",@reportid={4},@yearid={5},@acgroup={6},@partyid={7}",
                       Convert.ToInt32(KontoGlobals.CompanyId), fdate, tdate,
                       _party, reportid, KontoGlobals.YearId, _group, accid).ToList();
                
            }
            this.ledgertransDtoBindingSource.DataSource = Trans;
          
            gridView1.ExpandAllGroups();
            gridView1.Focus();
        }

        private void MultiSimpleButton_Click(object sender, EventArgs e)
        {
            var frm = new LedgerParaView();
            frm.MultiView = true;
            if (frm.ShowDialog() != DialogResult.OK) return;
           
            int AccId = 0;
            string _party = "N";
            string _group = "N";
            var fdate = Convert.ToInt32(this.fDateEdit.DateTime.ToString("yyyyMMdd"));
            var tdate = Convert.ToInt32(this.tDateEdit.DateTime.ToString("yyyyMMdd"));

            if (frm.acGroupGridView.GetSelectedRows().Count() > 0 || frm.ledgerGridView.GetSelectedRows().Count() > 1)
            {
                if(frm.acGroupGridView.GetSelectedRows().Count() > 0)
                    _group = "Y";

                if (frm.ledgerGridView.GetSelectedRows().Count() > 1)
                    _party = "Y";
               
                List<ReportParaModel> list = new List<ReportParaModel>();
                using (var db = new KontoContext())
                {
                    var reportid = 0;
                    reportid = db.ReportParas.Max(k => k.ReportId) + 1;
                    
                    foreach (var item in frm.ledgerGridView.GetSelectedRows())
                    {
                        var rw = frm.ledgerGridView.GetRow(item) as AccLookupDto;
                        var ModelReport = new ReportParaModel();
                        ModelReport.ReportId = reportid;
                        ModelReport.ParameterName = "party";
                        ModelReport.ParameterValue = rw.Id;
                        list.Add(ModelReport);
                    }

                    foreach (var item in frm.acGroupGridView.GetSelectedRows())
                    {
                        var rw = frm.acGroupGridView.GetRow(item) as AcGroupDto;
                        var ModelReport = new ReportParaModel();
                        ModelReport.ReportId = reportid;

                        ModelReport.ParameterName = "acgroup";
                        ModelReport.ParameterValue = rw.Id;
                        list.Add(ModelReport);
                    }

                    db.ReportParas.AddRange(list);
                    db.SaveChanges();

                    if (radioGroup1.SelectedIndex == 1)
                    {
                        GenerateDetails(AccId, fdate, tdate, _party, _group,reportid);
                    }
                    else
                    {
                        GenerateMonthly(AccId, _party, _group,reportid);
                    }
                    
                }
                

            }
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            if(Convert.ToInt32(accLookup1.SelectedValue)==0)
            {
                MessageBox.Show("Select Account Name");
                accLookup1.Focus();
                return;
            }
            var fdate = Convert.ToInt32(this.fDateEdit.DateTime.ToString("yyyyMMdd"));
            var tdate = Convert.ToInt32(this.tDateEdit.DateTime.ToString("yyyyMMdd"));

            if (fdate > KontoGlobals.ToDate || fdate < KontoGlobals.FromDate)
            {
                MessageBoxAdv.Show(this, "From date out of financial range", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fDateEdit.Focus();
                return;
            }

            if (tdate > KontoGlobals.ToDate || tdate < KontoGlobals.FromDate)
            {
                MessageBoxAdv.Show(this, "To date out of financial range", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tDateEdit.Focus();
                return;
            }

            var AccId = Convert.ToInt32(accLookup1.SelectedValue);
            if (radioGroup1.SelectedIndex == 0)
                GenerateMonthly(AccId);
            else
            {
                
              
                GenerateDetails(AccId,fdate,tdate);
            }
            gridControl1.Focus();
        }

        private void MonthlyGridView_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            GridGroupRowInfo info = e.Info as GridGroupRowInfo;
            var bal =  view.GetGroupRowValue(e.RowHandle, colDiff);
            info.GroupText = view.GetGroupRowValue(e.RowHandle, info.Column) + "  <color=#e3165b>" + bal + "</color>";

        }

        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabPageAdv2.Controls.Count > 0) return;
            var frm = new LedgerParaView();
            frm.TopLevel = false;
            frm.Parent = tabPageAdv2;
            frm.Location = new Point(tabPageAdv2.Location.X + tabPageAdv2.Width / 2 - frm.Width / 2, tabPageAdv2.Location.Y + tabPageAdv2.Height / 2 - frm.Height / 2);
            frm.Show();

        }

        private void GenerateMonthly(int AccId,string _party="N",string _group ="N",int reportid =0)
        {
            radioGroup1.SelectedIndex = 0;
            detailsLayoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            monthlyLayoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

            itemLayoutControlItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            var fdate = Convert.ToInt32(this.fDateEdit.DateTime.ToString("yyyyMMdd"));
            var tdate = Convert.ToInt32(this.tDateEdit.DateTime.ToString("yyyyMMdd"));
           
            var LedgerMonthly = new List<LedgerMonthlyDto>();
            using (var db = new KontoContext())
            {
                db.Database.CommandTimeout = 0;
                 LedgerMonthly = db.Database.SqlQuery<LedgerMonthlyDto>(
                                      "dbo.LedgerShow @companyid={0},@fromdate={1},@todate={2},@party={3}" +
                            ",@reportid={4},@year={5},@acgroup={6},@partyid={7}",
                            Convert.ToInt32(KontoGlobals.CompanyId), fdate, tdate,
                            _party, reportid, KontoGlobals.YearId, _group, AccId).ToList();


            }
            this.ledgerMonthlyDtoBindingSource.DataSource = LedgerMonthly;
            monthlyGridView.ExpandAllGroups();
        }

       

       
    }
}
