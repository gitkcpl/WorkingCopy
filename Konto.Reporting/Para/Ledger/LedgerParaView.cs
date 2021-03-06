﻿using DevExpress.XtraGrid.Views.Grid;
using GrapeCity.ActiveReports.PageReportModel;
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Reporting.Para.Ledger
{
    public partial class LedgerParaView : KontoForm
    {
        public bool MultiView { get; set; }
        
        public LedgerParaView()
        {
            InitializeComponent();
            this.Load += LedgerParaView_Load;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.fDateEdit.EditValue = KontoGlobals.DFromDate;
            this.tDateEdit.EditValue = KontoGlobals.DToDate;
            this.ledgerGridView.RowStyle += LedgerGridView_RowStyle;
            this.ledgerGridView.RowCellStyle += LedgerGridView_RowCellStyle;
            this.customSimpleButton.Click += CustomSimpleButton_Click;
            this.intSmpleButton.Click += IntSmpleButton_Click;
        }

        private void IntSmpleButton_Click(object sender, EventArgs e)
        {
            var frmview = new InterestLedgerView();

            int _ReportId = 0;
            var _paraList = new List<ReportParaModel>();
            using (var _db = new KontoContext())
            {
                var reportid = _db.ReportParas.DefaultIfEmpty().Max(k => k == null ? 0 : k.ReportId);
                _ReportId = reportid + 1;
                frmview.ReportId = _ReportId;
                ReportParaModel ModelReport;

                if (ledgerGridView.SelectedRowsCount > 1)
                {
                    frmview.Party = "Y";
                    foreach (var item in ledgerGridView.GetSelectedRows())
                    {
                        var _acc = ledgerGridView.GetRow(item) as AccLookupDto;
                        ModelReport = new ReportParaModel();
                        ModelReport.ReportId = _ReportId;
                        ModelReport.ParameterName = "party";
                        ModelReport.ParameterValue = _acc.Id;
                        _paraList.Add(ModelReport);
                    }
                }
                else
                {
                    frmview.Party = "N";
                    if (ledgerGridView.GetSelectedRows().Count() == 1)
                    {
                        var _acc = ledgerGridView.GetRow(ledgerGridView.GetSelectedRows()[0]) as AccLookupDto;
                        frmview.AccId = _acc.Id;
                    }
                }

                if (acGroupGridView.SelectedRowsCount > 0)
                {
                    frmview.AcGroup = "Y";
                    foreach (var item in acGroupGridView.GetSelectedRows())
                    {
                        var _grp = acGroupGridView.GetRow(item) as AcGroupDto;
                        ModelReport = new ReportParaModel();
                        ModelReport.ReportId = _ReportId;

                        ModelReport.ParameterName = "acgroup";
                        ModelReport.ParameterValue = _grp.Id;
                        _paraList.Add(ModelReport);
                    }

                }
                else
                {
                    frmview.AcGroup = "N";
                }
                _db.ReportParas.AddRange(_paraList);
                _db.SaveChanges();
            }
            frmview.FromDate = Convert.ToInt32(this.fDateEdit.DateTime.ToString("yyyyMMdd"));
            frmview.ToDate = Convert.ToInt32(this.tDateEdit.DateTime.ToString("yyyyMMdd"));

            frmview.ShowDialog();
        }

        private void CustomSimpleButton_Click(object sender, EventArgs e)
        {
            if (repGridView1.RowCount == 0) return;
            var rep = repGridView1.GetRow(repGridView1.FocusedRowHandle) as ReportTypeModel;
            if (rep == null)
                rep = repGridView1.GetRow(0) as ReportTypeModel;
            var frmd = new KontoArDesignerView();
            frmd.endUserDesigner1._reportName = rep.FileName;
            frmd.endUserDesigner1.reportDesigner.LoadReport(new FileInfo(rep.FileName));
            frmd.Text = "Konto Designer - " + rep.FileName;
            frmd.Show();
        }
        private void LedgerGridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView gridView = (GridView)sender;
            if (e.RowHandle == gridView.FocusedRowHandle && e.Column == gridView.FocusedColumn)
            {
                e.Appearance.BackColor = gridView.Appearance.FocusedCell.BackColor;
            }
        }

        private void LedgerGridView_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedRowHandle == e.RowHandle)
                e.Appearance.Assign(view.PaintAppearance.SelectedRow);
            else
                e.Appearance.Assign(view.PaintAppearance.Row);
            e.HighPriority = true;
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

                if (checkEdit1.CheckState == CheckState.Checked)
                {
                    var tble = _pageReport.Report.Body.ReportItems["Table1"] as GrapeCity.ActiveReports.PageReportModel.Table;
                    if (tble == null) return;
                    var grp = tble.TableGroups[0] as TableGroup;
                    grp.Grouping.PageBreakAtEnd = true;
                }

                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(_pageReport);
                _pageReport.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;
                var _paraList = new List<ReportParaModel>();
                int _ReportId = 0;
                using (var _db = new KontoContext())
                {
                    var reportid = _db.ReportParas.DefaultIfEmpty().Max(k => k == null ? 0 : k.ReportId);
                    _ReportId = reportid + 1;

                    ReportParaModel ModelReport;

                    if (ledgerGridView.SelectedRowsCount > 1)
                    {
                        doc.Parameters["party"].CurrentValue = "Y";
                        foreach (var item in ledgerGridView.GetSelectedRows())
                        {
                            var _acc = ledgerGridView.GetRow(item) as AccLookupDto;
                            ModelReport = new ReportParaModel();
                            ModelReport.ReportId = _ReportId;
                            ModelReport.ParameterName = "party";
                            ModelReport.ParameterValue = _acc.Id;
                            _paraList.Add(ModelReport);
                        }
                    }
                    else
                    {
                        doc.Parameters["party"].CurrentValue = "N";
                        if (ledgerGridView.GetSelectedRows().Count() == 1)
                        {
                            var _acc = ledgerGridView.GetRow(ledgerGridView.GetSelectedRows()[0]) as AccLookupDto;
                            doc.Parameters["partyid"].CurrentValue = _acc.Id;
                        }
                    }

                    if (acGroupGridView.SelectedRowsCount > 0)
                    {
                        doc.Parameters["acgroup"].CurrentValue = "Y";
                        foreach (var item in acGroupGridView.GetSelectedRows())
                        {
                            var _grp = acGroupGridView.GetRow(item) as AcGroupDto;
                            ModelReport = new ReportParaModel();
                            ModelReport.ReportId = _ReportId;

                            ModelReport.ParameterName = "acgroup";
                            ModelReport.ParameterValue = _grp.Id;
                            _paraList.Add(ModelReport);
                        }

                    }
                    else
                    {
                        doc.Parameters["acgroup"].CurrentValue = "N";
                    }
                    _db.ReportParas.AddRange(_paraList);
                    _db.SaveChanges();
                }

                // report parameter

                doc.Parameters["agentid"].CurrentValue = Convert.ToInt32(agentLookup1.SelectedValue);
                doc.Parameters["partygroupid"].CurrentValue = Convert.ToInt32(pgLookup1.SelectedValue);
                doc.Parameters["cityid"].CurrentValue = Convert.ToInt32(cityLookup1.SelectedValue);
                doc.Parameters["areaid"].CurrentValue = Convert.ToInt32(areaLookup1.SelectedValue);
                doc.Parameters["branchid"].CurrentValue = 0;

                doc.Parameters["companyid"].CurrentValue = KontoGlobals.CompanyId;
                doc.Parameters["yearid"].CurrentValue = KontoGlobals.YearId;
                doc.Parameters["fromdate"].CurrentValue = Convert.ToInt32(this.fDateEdit.DateTime.ToString("yyyyMMdd"));
                doc.Parameters["todate"].CurrentValue = Convert.ToInt32(this.tDateEdit.DateTime.ToString("yyyyMMdd"));
                if (doc.Parameters.Contains("from_date"))
                    doc.Parameters["from_date"].CurrentValue = this.fDateEdit.DateTime.ToString("dd-MM-yyyy");

                if (doc.Parameters.Contains("to_date"))
                    doc.Parameters["to_date"].CurrentValue = this.tDateEdit.DateTime.ToString("dd-MM-yyyy");

                doc.Parameters["keycon"].CurrentValue = KontoGlobals.sqlConnectionString.ConnectionString;
                doc.Parameters["reportid"].CurrentValue = _ReportId;
                doc.Parameters["report_title"].CurrentValue = rw.ReportName + " For The Period " + this.fDateEdit.DateTime.ToShortDateString() + " To " +
                                                              this.tDateEdit.DateTime.ToShortDateString();

                var frm = new KontoRepViewer(doc);
                frm.Text = "Ledger Print Preview";
                if (this.ParentForm.Parent == null)
                {
                    frm.WindowState = FormWindowState.Maximized;
                    frm.MinimizeBox = true;
                    frm.MaximizeBox = true;
                    frm.Show();

                }
                else
                {
                    var _tab = this.ParentForm.Parent.Parent as TabControlAdv;
                    if (_tab == null) return;

                    var pg1 = new TabPageAdv();
                    pg1.Text = "Ledger Print";
                    _tab.TabPages.Add(pg1);
                  
                    frm.TopLevel = false;
                    frm.Parent = pg1;
                    _tab.SelectedTab = pg1;
                    //frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                    frm.Show();// = true;
                }
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
                    .Select(x => new AcGroupDto { GroupName = x.GroupName, Id = x.Id }).ToList();

                ledgerGridControl.DataSource = AccList;
                acGroupGridControl.DataSource = GroupTrans;

                var reptype = db.ReportTypes
                    .Where(k => k.IsActive && !k.IsDeleted && k.ReportTypes == "LEDGER")
                    .ToList();
                reportTypeModelBindingSource.DataSource = reptype;
            }
        }

       
    }
}
