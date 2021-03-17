using DevExpress.XtraGrid.Views.Grid;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Masters.Dtos;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
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
    public partial class TdsPrintView : KontoForm
    {
        public TdsPrintView()
        {
            InitializeComponent();
            this.FormClosed += TdsPrintView_FormClosed;
            this.ledgerGridView.RowCellStyle += LedgerGridView_RowCellStyle;
            this.ledgerGridView.RowStyle += LedgerGridView_RowStyle;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.Load += TdsPrintView_Load;
            this.fDateEdit.EditValue = KontoGlobals.DFromDate;
            this.tDateEdit.EditValue = KontoGlobals.DToDate;

            this.FirstActiveControl = fDateEdit;
        }

        private void TdsPrintView_Load(object sender, EventArgs e)
        {
            using (var db = new KontoContext())
            {


                var reptype = db.ReportTypes
                  .Where(k => k.IsActive && !k.IsDeleted && k.ReportTypes == "Tds")
                  .ToList();

                reportTypeModelBindingSource.DataSource = reptype;


                var pglist = db.PartyGroups.Where(x => !x.IsDeleted)
                                .OrderBy(x => x.GroupName)
                                .Select(x => new BaseLookupDto { DisplayText = x.GroupName, Id = x.Id })
                                .ToList();


                var branchList = db.Branches.Where(x => !x.IsDeleted)
                                    .OrderBy(x => x.BranchName)
                                    .Select(x => new BaseLookupDto { DisplayText = x.BranchName, Id = x.Id })
                                    .ToList();

               
                var AccList = db.Database.SqlQuery<AccLookupDto>("dbo.acclookup @groupid={0},@companyid={1},@yearid={2},@taxtype={3},@nature={4},@fillparty={5},@vouchertypeid={6}"
                        , 0, KontoGlobals.CompanyId, KontoGlobals.YearId, "N", "ALL", "Y", 0).ToList();


                var booklist = db.Database.SqlQuery<AccLookupDto>("dbo.acclookup @groupid={0},@companyid={1},@yearid={2},@taxtype={3},@nature={4},@fillparty={5},@vouchertypeid={6}"
                        , 0, KontoGlobals.CompanyId, KontoGlobals.YearId, "TDS", "ALL", "N", 0).ToList();

                
                ledgerGridControl.DataSource = AccList;
                bookGridControl1.DataSource = booklist;

                pgGridControl1.DataSource = pglist;
               
                branchLookUpEdit1.Properties.DataSource = branchList;
                
            }
            List<ComboBoxPairs> cbg = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("None", "None"),
                new ComboBoxPairs("Party", "Party"),
                new ComboBoxPairs("Tds", "Tds"),
              
            };
            gpOn1LookUpEdit.Properties.DataSource = cbg;
            gpOn1LookUpEdit.EditValue = "Tds";

            gpOn2LookUpEdit.Properties.DataSource = cbg;
            gpOn2LookUpEdit.EditValue = "None";
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            if (repGridView1.RowCount == 0)
            {
                MessageBox.Show("No Report Exist from Preview..");
                return;
            }

            if(gpOn1LookUpEdit.Text == gpOn2LookUpEdit.Text)
            {
                MessageBox.Show("Both Group can not be Same.. Please Change Group");
                gpOn1LookUpEdit.Focus();
                return;
            }
            if (gpOn1LookUpEdit.Text == "None")
            {
                MessageBox.Show("Top Level Group can Not Be None");
                gpOn1LookUpEdit.Focus();
            }


            var rw = repGridView1.GetRow(repGridView1.FocusedRowHandle) as ReportTypeModel;
            string dr = "";

            if (rw == null)
                rw = repGridView1.GetRow(0) as ReportTypeModel;
            dr = rw.FileName;


            if (dr.Substring(dr.Length - 3, 3) == "mrt")
            {
                ShowStimulSoft(dr);
                return;
            }
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void LedgerGridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedRowHandle == e.RowHandle)
                e.Appearance.Assign(view.PaintAppearance.SelectedRow);
            else
                e.Appearance.Assign(view.PaintAppearance.Row);
            e.HighPriority = true;
        }

        private void LedgerGridView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView gridView = (GridView)sender;
            if (e.RowHandle == gridView.FocusedRowHandle && e.Column == gridView.FocusedColumn)
            {
                e.Appearance.BackColor = gridView.Appearance.FocusedCell.BackColor;
            }
        }

        private void TdsPrintView_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage != null)
            {
                var tabcontrol = tabpage.Parent as TabControlAdv;
                if (tabcontrol != null)
                    tabcontrol.TabPages.Remove(tabpage);
            }
        }

        private void ShowStimulSoft(string _filename)
        {
            try
            {

                StiReport _rep = new StiReport();
                _rep.Load(_filename);
                _rep.Compile();

                int _ReportId = SetCheckedParameters( _rep);

                _rep["companyid"] = KontoGlobals.CompanyId;

                _rep["yearid"] = KontoGlobals.YearId;

                _rep["fromdate"] = Convert.ToInt32(fDateEdit.DateTime.ToString("yyyyMMdd"));

                _rep["todate"] = Convert.ToInt32(tDateEdit.DateTime.ToString("yyyyMMdd"));

                _rep["reportid"] = _ReportId;


               if(Convert.ToInt32(ledgerGroupLookup1.SelectedValue) > 0)
                {
                    _rep["groupid"] = Convert.ToInt32(ledgerGroupLookup1.SelectedValue);
                }

                if (!string.IsNullOrEmpty(branchLookUpEdit1.Text))
                {
                    _rep["branchid"] = Convert.ToInt32(branchLookUpEdit1.EditValue);
                }

                

                if (!string.IsNullOrEmpty(titleTextEdit.Text))
                {

                    _rep["report_title"] = titleTextEdit.Text;
                }
                if (!string.IsNullOrEmpty(footerTextEdit.Text))
                {

                    _rep["report_footer"] = footerTextEdit.Text;
                }
                if (!string.IsNullOrEmpty(gpOn2LookUpEdit.Text))
                {

                    _rep["gp1"] = gpOn1LookUpEdit.EditValue;
                }

                if (!string.IsNullOrEmpty(gpOn2LookUpEdit.Text))
                {

                    _rep["gp2"] = gpOn2LookUpEdit.EditValue;
                }


                StiSqlDatabase sql = new StiSqlDatabase("cnn", KontoGlobals.sqlConnectionString.ConnectionString);
                sql.Alias = "cnn";
                _rep.CompiledReport.Dictionary.Databases.Clear();
                _rep.CompiledReport.Dictionary.Databases.Add(sql);

                _rep.ShowWithRibbonGUI();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }


        private int SetCheckedParameters(
              Stimulsoft.Report.StiReport _rep)
        {
            int _ReportId = 0;
            var _paraList = new List<ReportParaModel>();
            using (var _db = new KontoContext())
            {
                var reportid = _db.ReportParas.DefaultIfEmpty().Max(k => k == null ? 0 : k.ReportId);
                _ReportId = reportid + 1;

                ReportParaModel ModelReport;

                
                if (ledgerGridView.SelectedRowsCount > 1)
                {
                     _rep["party"] = "Y";

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
                     _rep["party"] = "N";
                    if (ledgerGridView.GetSelectedRows().Count() == 1)
                    {
                        var _acc = ledgerGridView.GetRow(ledgerGridView.GetSelectedRows()[0]) as AccLookupDto;
                        _rep["acid"] = _acc.Id;
                    }
                }


                if (pgGridView1.SelectedRowsCount > 1)
                {
                        _rep["party_group"] = "Y";

                    foreach (var item in pgGridView1.GetSelectedRows())
                    {
                        var _acc = pgGridView1.GetRow(item) as PgLookupDto;
                        ModelReport = new ReportParaModel();
                        ModelReport.ReportId = _ReportId;
                        ModelReport.ParameterName = "party_group";
                        ModelReport.ParameterValue = _acc.Id;
                        _paraList.Add(ModelReport);
                    }
                }
                else
                {
                    _rep["party_group"] = "N";
                    if (pgGridView1.GetSelectedRows().Count() == 1)
                    {
                        var _acc = pgGridView1.GetRow(pgGridView1.GetSelectedRows()[0]) as BaseLookupDto;
                        _rep["pgid"] = _acc.Id;
                    }
                }

                if (bookGridView1.SelectedRowsCount > 1)
                {
                    _rep["book"] = "Y";

                    foreach (var item in bookGridView1.GetSelectedRows())
                    {
                        var _acc = bookGridView1.GetRow(item) as AccLookupDto;
                        ModelReport = new ReportParaModel();
                        ModelReport.ReportId = _ReportId;
                        ModelReport.ParameterName = "book";
                        ModelReport.ParameterValue = _acc.Id;
                        _paraList.Add(ModelReport);
                    }
                }
                else
                {
                    _rep["book"] = "N";
                    if (bookGridView1.GetSelectedRows().Count() == 1)
                    {
                        var _acc = bookGridView1.GetRow(bookGridView1.GetSelectedRows()[0]) as AccLookupDto;
                        _rep["tdsacid"] = _acc.Id;
                    }
                }

                if (_paraList.Count > 1)
                {
                    _db.ReportParas.AddRange(_paraList);
                    _db.SaveChanges();
                }
            }
            return _ReportId;

        }
    }
}
