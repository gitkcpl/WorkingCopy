using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Masters.Dtos;
using Serilog;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Reporting.Para.Go
{
    public partial class GoParaView : KontoForm
    {
        public bool MultiView { get; set; }
        
        public GoParaView()
        {
            InitializeComponent();
            this.Load += LedgerParaView_Load;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.fDateEdit.EditValue = KontoGlobals.DFromDate;
            this.tDateEdit.EditValue = KontoGlobals.DToDate;

            List<ComboBoxPairs> sts = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("All", "All"),
                new ComboBoxPairs("PENDING", "PENDING"),
                new ComboBoxPairs("APPROVED", "APPROVED"),
                new ComboBoxPairs("CANCELED", "CANCELED")
            };
            statusLookUpEdit.Properties.DataSource = sts;

            List<ComboBoxPairs> cbg = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("None", "None"),
                new ComboBoxPairs("Party Group", "Party Group"),
                new ComboBoxPairs("Item", "Item"),
                new ComboBoxPairs("Party + Item", "Party + Item"),
            };
            groupOnLookUpEdit.Properties.DataSource = cbg;
            
        }

      

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            if (this.MultiView)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            ShowReport();

        }
        private void ShowReport()
        {

            if(repGridView1.RowCount ==0)
            {
                MessageBox.Show("No Report Exist from Preview..");
                return;
            }
            try
            {
                GrapeCity.ActiveReports.PageReport _pageReport = new GrapeCity.ActiveReports.PageReport();
                string dr = "";

                var rw = repGridView1.GetRow(repGridView1.FocusedRowHandle) as ReportTypeModel;
                if (rw == null)
                    rw = repGridView1.GetRow(0) as ReportTypeModel;
                dr = rw.FileName;


                _pageReport.Load(new System.IO.FileInfo(dr));

                _pageReport.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;



                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(_pageReport);
                _pageReport.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;
                var _paraList = new List<ReportParaModel>();
                int _ReportId = 0;
                using (var _db = new KontoContext())
                {
                    var reportid = _db.ReportParas.Max(k => k.ReportId);
                    _ReportId = reportid + 1;

                    ReportParaModel ModelReport;

                    if (pgGridView1.SelectedRowsCount > 1)
                    {
                        doc.Parameters["party_group"].CurrentValue = "Y";
                        foreach (var item in pgGridView1.GetSelectedRows())
                        {
                            var _acc = pgGridView1.GetRow(item) as BaseLookupDto;
                            ModelReport = new ReportParaModel();
                            ModelReport.ReportId = _ReportId;
                            ModelReport.ParameterName = "party_group";
                            ModelReport.ParameterValue = _acc.Id;
                            _paraList.Add(ModelReport);
                        }
                    }
                    

                   


                    if (agentGridView1.SelectedRowsCount > 0)
                    {
                        var SelectedAgent = agentGridView1.GetSelectedRows();
                        foreach (var item in SelectedAgent)
                        {
                            var agp = agentGridView1.GetRow(item) as BaseLookupDto;
                            doc.Parameters["agent"].CurrentValue = "Y";
                            ModelReport = new ReportParaModel();
                            ModelReport.ReportId = _ReportId;
                            ModelReport.ParameterName = "agent";
                            ModelReport.ParameterValue = agp.Id;
                        }

                    }
                    else
                    {
                        doc.Parameters["agent"].CurrentValue = "N";
                    }
                    _db.ReportParas.AddRange(_paraList);
                    _db.SaveChanges();
                }

              
               
               
                doc.Parameters["branchid"].CurrentValue = 0;
                doc.Parameters["companyid"].CurrentValue = KontoGlobals.CompanyId;
                doc.Parameters["yearid"].CurrentValue = KontoGlobals.YearId;
                doc.Parameters["fromdate"].CurrentValue = Convert.ToInt32(this.fDateEdit.DateTime.ToString("yyyyMMdd"));
                doc.Parameters["todate"].CurrentValue = Convert.ToInt32(this.tDateEdit.DateTime.ToString("yyyyMMdd"));

              

                doc.Parameters["reportid"].CurrentValue = _ReportId;
                doc.Parameters["report_title"].CurrentValue = rw.ReportName + " For The Period " + this.fDateEdit.DateTime.ToShortDateString() + " To " +
                                                              this.tDateEdit.DateTime.ToShortDateString();

                if (doc.Parameters.Contains("range1")) // add parameter for ageing report
                {

                }
                doc.Parameters["keycon"].CurrentValue = KontoGlobals.sqlConnectionString.ConnectionString;
                var _tab = this.ParentForm.Parent.Parent as TabControlAdv;
                if (_tab == null) return;
                var frm = new KontoRepViewer(doc);
                frm.Text = "Oustanding Print Preview";

                var pg1 = new TabPageAdv();
                pg1.Text = "Oustanding Print";
                _tab.TabPages.Add(pg1);
                _tab.SelectedTab = pg1;
                frm.TopLevel = false;
                frm.Parent = pg1;
                frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                frm.Show();// = true;

            }
            catch (Exception ex)
            {
                Log.Error(ex, "ledger report");
                MessageBox.Show(ex.ToString());
            }

        }
        private void LedgerParaView_Load(object sender, EventArgs e)
        {
            using (var db = new KontoContext())
            {
                var AccList = db.Database.SqlQuery<AccLookupDto>("dbo.acclookup @groupid={0},@companyid={1},@yearid={2},@taxtype={3},@nature={4},@fillparty={5},@vouchertypeid={6}"
                        , 0, KontoGlobals.CompanyId, KontoGlobals.YearId, "N", "ALL", "N", 0).ToList();

                var GroupTrans = db.AcGroupModels
                    .OrderBy(x => x.GroupName)
                    .Where(x=>!x.IsDeleted)
                    .Select(x => new BaseLookupDto { DisplayText = x.GroupName, Id = x.Id }).ToList();

                var agentlist = (from ac in db.Accs
                                 join pd in db.AccBals on ac.Id equals pd.AccId into join_pd
                                 from pd in join_pd.DefaultIfEmpty()
                                 where pd.CompId == KontoGlobals.CompanyId && pd.YearId == KontoGlobals.YearId
                                 && pd.GroupId == 31 && !ac.IsDeleted
                                 orderby ac.AccName
                                 select new BaseLookupDto { DisplayText = ac.AccName, Id = ac.Id }
                                 ).ToList();
                var pglist = db.PartyGroups.Where(x => !x.IsDeleted)
                                .OrderBy(x => x.GroupName)
                                .Select(x => new BaseLookupDto { DisplayText = x.GroupName, Id = x.Id })
                                .ToList();

                var emplist = db.Emps.Where(x => !x.IsDeleted)
                                .OrderBy(x => x.EmpName)
                                .Select(x => new BaseLookupDto { DisplayText = x.EmpName, Id = x.Id })
                                .ToList();

              
                pgGridControl1.DataSource = pglist;
               
                agentGridControl1.DataSource = agentlist;

               
            }

        }

        private void customGridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
