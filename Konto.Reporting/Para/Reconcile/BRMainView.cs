using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Data.Models.Reports;
using Konto.Data.Models.Transaction;
using Konto.Reporting.Para.Reconcile;
using Serilog;
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

namespace Konto.Reporting.Para.Reoncile
{
    public partial class BRMainView : KontoForm
    {
        decimal OpBal = 0;
        public BRMainView()
        {
            InitializeComponent();
            okSimpleButton.Click += OkSimpleButton_Click;
            this.detailsGridControl1.ProcessGridKey += DetailsGridControl1_ProcessGridKey;
            this.gridView1.ValidatingEditor += GridView1_ValidatingEditor;
            this.gridView1.InvalidValueException += GridView1_InvalidValueException;
            printSimpleButton.Click += PrintSimpleButton_Click;
            this.gridView1.RowUpdated += GridView1_RowUpdated;
            fDateEdit.EditValue = KontoGlobals.DFromDate;
            tDateEdit.EditValue = KontoGlobals.DToDate;
        }

        private void GridView1_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void GridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (gridView1.FocusedColumn.FieldName != "BankDate") return;
            var dr = gridView1.GetRow(gridView1.FocusedRowHandle) as ReconsileDTO;
            if(dr.VoucherDate > Convert.ToDateTime(e.Value))
            {
                e.Valid = false;
                e.ErrorText = "Bank Date can not be less than voucher date";
            }
        }

        private void GridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var dr = gridView1.GetRow(gridView1.FocusedRowHandle) as ReconsileDTO;
            using (var db  = new KontoContext())
            {
               
                BillModel _bm = new BillModel(); 
                if(dr.TransCode== null)
                {
                    _bm = db.Bills.FirstOrDefault(x => x.RowId == dr.RefId);
                }

                BillTransModel _bt = new BillTransModel();

                if(dr.TransCode!= null)
                    _bt = db.BillTrans.FirstOrDefault(x => x.RowId == dr.TransCode);
                else
                    _bt = db.BillTrans.FirstOrDefault(x => x.BillId == _bm.Id);

                if (_bt != null)
                {
                    if (dr.BankDate != null)
                    {
                        _bt.BankDate = Convert.ToInt32( Convert.ToDateTime(dr.BankDate).ToString("yyyyMMdd"));
                    }
                    else
                        _bt.BankDate = null;

                    db.SaveChanges();
                }
                //else
                //{
                //    var bt = db.BillTrans.Where(x=>x.RefTransId == dr.)
                //}
            }

            var Trans = this.bindingSource1.DataSource as List<ReconsileDTO>;
           // var tdate = Convert.ToInt32(tDateEdit.DateTime.ToString("yyyyMMdd"));
            var credit = Trans.Where(x => x.BankDate == null || x.BankDate > tDateEdit.DateTime).Sum(k => k.Credit);
            var debit = Trans.Where(x => x.BankDate == null || x.BankDate > tDateEdit.DateTime).Sum(k => k.Debit);
            bankBalTextEdit.Text = Convert.ToDecimal( Convert.ToDecimal(balanceTextEdit.Text) - (debit - credit)).ToString("F");
            amtTextEdit.Text = (debit - credit).ToString("F");
        }

        private void PrintSimpleButton_Click(object sender, EventArgs e)
        {
            GrapeCity.ActiveReports.PageReport _pageReport = new GrapeCity.ActiveReports.PageReport();
            string dr = "Reg\\BankReconsile.rdlx";

            _pageReport.Load(new System.IO.FileInfo(dr));

            GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(_pageReport);
            doc.Parameters["keycon"].CurrentValue = KontoGlobals.sqlConnectionString.ConnectionString;



            doc.Parameters["companyid"].CurrentValue = KontoGlobals.CompanyId;
            doc.Parameters["fromdate"].CurrentValue = Convert.ToInt32(fDateEdit.DateTime.ToString("yyyyMMdd"));
            doc.Parameters["todate"].CurrentValue = Convert.ToInt32(tDateEdit.DateTime.ToString("yyyyMMdd"));
            doc.Parameters["AccId"].CurrentValue = accLookup1.SelectedValue;
            

            try
            {
                doc.Parameters["Balance"].CurrentValue = Convert.ToDecimal(balanceTextEdit.Text);
                doc.Parameters["ClearBalance"].CurrentValue = Convert.ToDecimal(bankBalTextEdit.Text);
                doc.Parameters["NotClearBalance"].CurrentValue = Convert.ToDecimal(amtTextEdit.Text);

                var _tab = this.Parent.Parent as TabControlAdv;
                if (_tab == null) return;
                var frm = new KontoRepViewer(doc);
                frm.Text = "Bank reconciliation";

                var pg1 = new TabPageAdv();
                pg1.Text = "BRS";
                _tab.TabPages.Add(pg1);
                _tab.SelectedTab = pg1;
                frm.TopLevel = false;
                frm.Parent = pg1;
                frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                frm.Show();// = true;

            }
            catch (Exception ex)
            {
                Log.Error(ex,"reco ");
                MessageBox.Show(ex.ToString());
            }
        }

        private void DetailsGridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete && gridView1.FocusedColumn == colBankDate)
                {
                    gridView1.SetRowCellValue(gridView1.FocusedRowHandle, colBankDate, null);
                    return;
                }
                if (e.KeyCode == Keys.F2)
                {
                    gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "BankDate",
                                            gridView1.GetRowCellValue(gridView1.FocusedRowHandle, colVoucherDate));

                }

                if (gridView1.FocusedColumn == colBankDate && (e.KeyCode.ToString() == "Add" || e.KeyCode.ToString() == "Subtract"))
                {
                    DateTime _dt;

                    if (gridView1.GetRowCellValue(gridView1.FocusedRowHandle, colBankDate) == null ||
                           string.IsNullOrEmpty(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, colBankDate).ToString()))
                        _dt = Convert.ToDateTime(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, colVoucherDate));
                    else
                        _dt = Convert.ToDateTime(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, colBankDate));

                    if (e.KeyCode.ToString() == "Add")
                        gridView1.SetRowCellValue(gridView1.FocusedRowHandle, colBankDate, _dt.Add(new TimeSpan(1, 0, 0, 0)));
                    else
                        gridView1.SetRowCellValue(gridView1.FocusedRowHandle, colBankDate,
                                                _dt.Add(new TimeSpan(-1, 0, 0, 0)));

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
           
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(accLookup1.SelectedValue) == 0)
            {
                MessageBox.Show("Select Account Name");
                accLookup1.Focus();
                return;
            }
            var AccId = Convert.ToInt32(accLookup1.SelectedValue);
            var fdate = Convert.ToInt32(this.fDateEdit.DateTime.ToString("yyyyMMdd"));
            var tdate = Convert.ToInt32(this.tDateEdit.DateTime.ToString("yyyyMMdd"));
            using (var db = new KontoContext()) {
                var Trans = db.Database.SqlQuery<ReconsileDTO>(
                                              "dbo.ReconsileList @FromDate={0},@ToDate={1}," +
                                              "@AccId={2},@CompId={3}",fdate,
                                              tdate, AccId, KontoGlobals.CompanyId).ToList();

                bindingSource1.DataSource = Trans;
                //decimal opbal = accLookup1.LookupDto.Balance
                decimal credit = Trans.Where(x=>x.BankDate==null).Sum(k => k.Credit);
                decimal debit = Trans.Where(x=>x.BankDate==null).Sum(k => k.Debit);

                //opening balance for reconcile
                //var opbalRec = db.Database.SqlQuery<ReconsileDTO>(
                //                             "dbo.ReconsileList @FromDate={0},@ToDate={1}," +
                //                             "@AccId={2},@CompId={3},@OpBal={4}", fdate,
                //                             tdate, AccId, KontoGlobals.CompanyId,1).ToList();

                var ledgerBal = db.Database.SqlQuery<decimal>(
                                             "dbo.get_account_bal @todate={0}," +
                                             "@accid={1},@compid={2},@yearid={3}",
                                             tdate, AccId, KontoGlobals.CompanyId, KontoGlobals.YearId).FirstOrDefault();

                balanceTextEdit.Text = ledgerBal.ToString("F");
                var opledger = db.AccBals.Where(x => x.AccId == AccId && x.CompId == KontoGlobals.CompanyId
                                            && x.YearId == KontoGlobals.YearId).FirstOrDefault();
                //bank balance
                bankBalTextEdit.Text = Convert.ToDecimal(ledgerBal -( debit - credit)).ToString("F");

                
                amtTextEdit.Text = (debit - credit).ToString("F");

               
            }
        }

        private void detailsGridControl1_Click(object sender, EventArgs e)
        {

        }

        private void stmtSimpleButton_Click(object sender, EventArgs e)
        {

            var op = new BankOpBal();
            if (op.ShowDialog() != DialogResult.OK) return;
            
            GrapeCity.ActiveReports.PageReport _pageReport = new GrapeCity.ActiveReports.PageReport();
            string dr = "Reg\\BankStatement.rdlx";

            _pageReport.Load(new System.IO.FileInfo(dr));

            GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(_pageReport);
            _pageReport.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;


            doc.Parameters["companyid"].CurrentValue = KontoGlobals.CompanyId;
            doc.Parameters["fromdate"].CurrentValue = Convert.ToInt32(fDateEdit.DateTime.ToString("yyyyMMdd"));
            doc.Parameters["todate"].CurrentValue = Convert.ToInt32(tDateEdit.DateTime.ToString("yyyyMMdd"));
            doc.Parameters["AccId"].CurrentValue = Convert.ToInt32(accLookup1.SelectedValue);

            doc.Parameters["OpBal"].CurrentValue = op.OpBal;
            try
            {
                var _tab = this.Parent.Parent as TabControlAdv;
                if (_tab == null) return;
                var frm = new KontoRepViewer(doc);
                frm.Text = "Bank Statement";

                var pg1 = new TabPageAdv();
                pg1.Text = "BRS Stmt";
                _tab.TabPages.Add(pg1);
                _tab.SelectedTab = pg1;
                frm.TopLevel = false;
                frm.Parent = pg1;
                frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                frm.Show();// = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
