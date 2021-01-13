using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Data.Models.Reports;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Reporting.Para.TDSPara
{
    public partial class TDSMainView : KontoForm
    {
        public TDSMainView()
        {
            InitializeComponent();
            this.okSimpleButton.Click += OkSimpleButton_Click;
            fDateEdit.EditValue = KontoGlobals.DFromDate;
            tDateEdit.EditValue = KontoGlobals.DToDate;
            printSimpleButton.Click += PrintSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.FormClosed += TDSMainView_FormClosed;
            this.tdsRegisterSimpleButton.Click += TdsRegisterSimpleButton_Click;
        }

        private void TdsRegisterSimpleButton_Click(object sender, EventArgs e)
        {
            var _tab = this.Parent.Parent as TabControlAdv;
            if (_tab == null) return;
            var frm = new TdsPrintView();
            frm.Text = "Tds Register";

            var pg1 = new TabPageAdv();
            pg1.Text = "Tds Register";
            _tab.TabPages.Add(pg1);
            _tab.SelectedTab = pg1;
            frm.TopLevel = false;
            frm.Parent = pg1;
            frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
            frm.Show();// = true;
        }

        private void TDSMainView_FormClosed(object sender, FormClosedEventArgs e)
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

        private void PrintSimpleButton_Click(object sender, EventArgs e)
        {
            GrapeCity.ActiveReports.PageReport _pageReport = new GrapeCity.ActiveReports.PageReport();
            string dr = "Reg/TDSSummary.rdlx";

            _pageReport.Load(new System.IO.FileInfo(dr));

            GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(_pageReport);
            _pageReport.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;
            var _db = new KontoContext();
            var report = _db.ReportParas.FirstOrDefault();
            int _ReportId = 0;
            if (report == null)
                _ReportId = 1;
            else
            {

                var reportid = _db.ReportParas.Max(k => k.ReportId);
                _ReportId = reportid != 0 ? reportid + 1 : 1;
            }

            if (doc.Parameters["companyid"] != null)
                doc.Parameters["companyid"].CurrentValue = KontoGlobals.CompanyId;

            if (doc.Parameters["yearid"] != null)
                doc.Parameters["yearid"].CurrentValue = KontoGlobals.YearId;

            if (doc.Parameters["fromdate"] != null)
                doc.Parameters["fromdate"].CurrentValue = Convert.ToInt32(fDateEdit.DateTime.ToString("yyyyMMdd"));

            if (doc.Parameters["todate"] != null)
                doc.Parameters["todate"].CurrentValue = Convert.ToInt32(tDateEdit.DateTime.ToString("yyyyMMdd"));

            if (doc.Parameters["TDSAcID"] != null)
                doc.Parameters["TDSAcID"].CurrentValue = Convert.ToInt32( accLookup1.SelectedValue);

            var _tab = this.Parent.Parent as TabControlAdv;
            if (_tab == null) return;
            var frm = new KontoRepViewer(doc);
            frm.Text = "Tds Register";

            var pg1 = new TabPageAdv();
            pg1.Text = "Tds Print";
            _tab.TabPages.Add(pg1);
            _tab.SelectedTab = pg1;
            frm.TopLevel = false;
            frm.Parent = pg1;
            frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
            frm.Show();// = true;
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                //if(Convert.ToInt32(accLookup1.SelectedValue) ==0)
                //{
                //    MessageBox.Show("Invalid Tds Account");
                //    accLookup1.Focus();
                //    return;
                //}
                using (var _db = new KontoContext())
                {
                    _db.Database.CommandTimeout = 0;
                    var Tran =_db.Database.SqlQuery<TDSDto>(
                           "dbo.TDS @CompanyId={0},@fromdate={1},@todate={2}, @TDSAcID={3},@YearId={4}",
                           KontoGlobals.CompanyId, Convert.ToInt32(fDateEdit.DateTime.ToString("yyyyMMdd"))
                           , Convert.ToInt32(tDateEdit.DateTime.ToString("yyyyMMdd")), 
                           Convert.ToInt32 (accLookup1.SelectedValue), KontoGlobals.YearId).ToList();

                    var list = Tran.GroupBy(grp => new { grp.Party, grp.AccountID, grp.PanNo, grp.TDSAccount })
                    .Select(x => new TDsSummaryDto()
                    {
                        Party = x.Key.Party,
                        AccountID = x.Key.AccountID,
                        BillAmount = x.Sum(k => k.TotalAmount),
                        TDSAmount = x.Sum(k => k.TdsAmt),
                        PayRec = x.Sum(k=>k.Payable),
                        TDSAccount = x.Key.TDSAccount,
                        PanNo = x.Key.PanNo,
                        AcsValue = x.Sum(k=>k.AcsValue),
                        TdsList = Tran.Where(p => p.AccountID == x.Key.AccountID).ToList()
                    }).ToList();

                    bindingSource1.DataSource = list;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            
        }
    }
}
