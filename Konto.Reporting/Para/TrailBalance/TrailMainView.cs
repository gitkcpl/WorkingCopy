using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Data.Models.Reports;
using Konto.Reporting.Para.TrailBalance;
using Serilog;
using Syncfusion.GridHelperClasses;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Reporting.Para.TrialBalance
{
    public partial class TrialMainView : KontoForm
    {
        List<TrialDto> Trans = new List<TrialDto>();
        public int AcGroupId { get; set; }
        public DateTime _fromDate { get; set; }
        public DateTime _Todate { get; set; }
        public TrialMainView()
        {
            InitializeComponent();
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Horizontal-F", "HF"),
                new ComboBoxPairs("Vertical-F", "VF"),

            };
            fromatLookUpEdit.Properties.DataSource = cbp;

            List<ComboBoxPairs> cbt = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Closing", "CL"),
                new ComboBoxPairs("Opening", "OP"),

            };
            viewLookUpEdit.Properties.DataSource = cbt;

            fDateEdit.DateTime = KontoGlobals.DFromDate;
            tDateEdit.DateTime = KontoGlobals.DToDate;

           
            okSimpleButton.Click += OkSimpleButton_Click;
            cancelSimpleButton.Click += CancelSimpleButton_Click;
            printSimpleButton.Click += PrintSimpleButton_Click;
            cellgrid.CurrentCellKeyDown += Cellgrid_CurrentCellKeyDown;
            this.FormClosed += TrialMainView_FormClosed;
            this.exportSimpleButton.Click += ExportSimpleButton_Click;
            ZoomGrid zoom = new ZoomGrid(this.cellgrid);

            //Zoom the grid with the specific percentage
            zoom.zoomGrid("120");
            this.Load += TrailMainView_Load;

            this.FirstActiveControl = fDateEdit;
        }

        
        private void ExportSimpleButton_Click(object sender, EventArgs e)
        {
            Syncfusion.GridExcelConverter.GridExcelConverterControl excelConverter = new Syncfusion.GridExcelConverter.GridExcelConverterControl();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Files(*.xlsx)|*.xlsx";
            saveFileDialog.DefaultExt = ".xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                excelConverter.GridToExcel(this.cellgrid.Model, saveFileDialog.FileName);
                if (MessageBox.Show("Do you wish to open the xls file now?", "Export to Excel", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Process proc = new Process();
                    proc.StartInfo.FileName = saveFileDialog.FileName;
                    proc.Start();
                }
            }
        }

        private void TrialMainView_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage != null)
            {
                var tabcontrol = tabpage.Parent as TabControlAdv;
                if (tabcontrol != null)
                    tabcontrol.TabPages.Remove(tabpage);
            }
        }

        private void PrintSimpleButton_Click(object sender, EventArgs e)
        {
            GrapeCity.ActiveReports.PageReport _pageReport = new GrapeCity.ActiveReports.PageReport();
            var frmp = new TbParaView();
            if (fromatLookUpEdit.EditValue.ToString() == "HF")
                _pageReport.Load(new System.IO.FileInfo("Outs\\tb_details_ar.rdlx"));
            else
            {
                if (frmp.ShowDialog() != DialogResult.OK) return;
                _pageReport.Load(new System.IO.FileInfo("Outs\\tb_details_ar_tf.rdlx"));
            }

            GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(_pageReport);
            _pageReport.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

            doc.Parameters["inclop"].CurrentValue = "Y";

            if (viewLookUpEdit.EditValue.ToString() == "CL")
                doc.Parameters["closingtb"].CurrentValue = "Y";
            else
                doc.Parameters["closingtb"].CurrentValue = "N";

            if (checkEdit1.Checked)
                doc.Parameters["showall"].CurrentValue = "Y";
            doc.Parameters["companyid"].CurrentValue = KontoGlobals.CompanyId;
            doc.Parameters["yearid"].CurrentValue = KontoGlobals.YearId;
            doc.Parameters["fromdate"].CurrentValue = Convert.ToInt32(fDateEdit.DateTime.ToString("yyyyMMdd"));
            doc.Parameters["todate"].CurrentValue = Convert.ToInt32(tDateEdit.DateTime.ToString("yyyyMMdd"));

            doc.Parameters["report_title"].CurrentValue = "Trail Balance For The Period " + fDateEdit.DateTime.ToString("dd/MM/yyyy") + " To " + tDateEdit.DateTime.ToString("dd/MM/yyyy");

            if (fromatLookUpEdit.EditValue.ToString() == "VF")
            {
                doc.Parameters["format"].CurrentValue = frmp.radioGroup1.EditValue;
            }
            try
            {
                var frm = new KontoRepViewer(doc);
                frm.Text = "Trail Balance";
                
              
               
                if (this.Parent == null || this.Parent.Parent == null)
                {
                    frm.WindowState = FormWindowState.Maximized;
                    frm.ShowDialog();
                }
                else
                {
                    var _tab = this.Parent.Parent as TabControlAdv;
                    if (_tab == null) return;
                    frm.TopLevel = false;
                    var pg1 = new TabPageAdv();
                    pg1.Text = "Trail balance";
                    frm.Parent = pg1;
                    _tab.TabPages.Add(pg1);
                    _tab.SelectedTab = pg1;
                 
                 //   frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                    frm.Show();// = true;
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void Cellgrid_CurrentCellKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {

                // Setting the Dialog sink.
                GridFindReplaceDialogSink findReplaceSink = new GridFindReplaceDialogSink(this.cellgrid);

                // Setting the Dialog.
                GridFindReplaceDialog findReplaceDialog = GridFindReplaceDialog.Instance;
                findReplaceDialog.ShowReplaceButton = false;

                // Setting the sink to dialog.
                findReplaceDialog.ActiveSink = findReplaceSink;
                findReplaceDialog.ShowDialog();
            }
            if(e.KeyCode == Keys.Space)
            {
                Audit();
            }
            if(e.KeyCode == Keys.Enter)
            {
                ShowLedger();
            }
            if(e.KeyCode == Keys.Escape)
            {
                cancelSimpleButton.PerformClick();
            }
        }
        private void Audit()
        {
            try
            {
                int id = 0;
                if (fromatLookUpEdit.EditValue.ToString() == "VF")
                {
                    if (cellgrid.CurrentCell.ColIndex != 3 && cellgrid.CurrentCell.ColIndex != 6) return;
                    if (cellgrid.CurrentCell.ColIndex == 3)
                        id = Convert.ToInt32(cellgrid.Model[cellgrid.CurrentCell.RowIndex, 7].CellValue);
                    else
                        id = Convert.ToInt32(cellgrid.Model[cellgrid.CurrentCell.RowIndex, 8].CellValue);
                }
                else
                {
                    if (cellgrid.CurrentCell.ColIndex != 1) return;
                    id = Convert.ToInt32(cellgrid.Model[cellgrid.CurrentCell.RowIndex, 9].CellValue);
                }
                if (id == 0) return;
                var cell = cellgrid.Model[cellgrid.CurrentCell.RowIndex, cellgrid.CurrentCell.ColIndex];

                if ((bool) cell.CellValue == false)
                    cell.CellValue = true;
                else
                    cell.CellValue = false;
                using (var db = new KontoContext())
                {
                    var acb = db.AccBals.FirstOrDefault(x => x.AccId == id && x.CompId == KontoGlobals.CompanyId
                    && x.YearId == KontoGlobals.YearId);
                    if (acb != null)
                    {
                        acb.Audit = (bool)cell.CellValue;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Trail Balance Audits");

            }
        }
        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormDescription("Generating Trail balance..");

                DetailView();

                if (fromatLookUpEdit.EditValue.ToString() == "VF")
                    GenerateVerticalFormat();
                else
                    GenrateHorzontalFormat();

                splashScreenManager1.CloseWaitForm();
                cellgrid.Focus();
            }
            catch (Exception ex)
            {
                if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                Log.Error(ex, "trail balance");
                MessageBox.Show(ex.ToString());
                
            }
            

            //BackgroundWorker bw = new BackgroundWorker();
            //bw.DoWork += bw_DoWork;
            //bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            //bw.RunWorkerAsync();
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }

        private void GenrateHorzontalFormat()
        {
            try
            {
                
              
                decimal opDebit = 0, opCredit = 0, trDebit = 0, trCredit = 0, clDebit = 0, clCredit = 0;

              
               // cellgrid.Model.Model.ColWidths.ResizeToFit();

                cellgrid.Model.Options.ExcelLikeCurrentCell = true;
                cellgrid.ExcelLikeAlignment = true;

                //cellgrid.Model.Options. = GridControlLengthUnitType.Auto;
                cellgrid.Model.TableStyle.ReadOnly = true;
              //  cellgrid.ZoomScale = 1.2;
                cellgrid.Model.Rows.RemoveRange(0, cellgrid.Model.RowCount);
                cellgrid.Model.Cols.RemoveRange(0, cellgrid.Model.ColCount);
                cellgrid.Model.RowCount = 1;
                cellgrid.Model.ColCount = 9;

                cellgrid.ColStyles[1].HorizontalAlignment = GridHorizontalAlignment.Center;
                cellgrid.ColStyles[1].VerticalAlignment = GridVerticalAlignment.Middle;

                    this.cellgrid.Model.Options.NumberedRowHeaders = false;
                
                cellgrid.Model.Cols.Hidden[0] = true;
                cellgrid.Model.Cols.Hidden[9] = true;

                //cellgrid.Model.ColumnWidths[5] = 0;
                cellgrid.Model[0, 1].CellValue = "Audit";
                cellgrid.Model[0, 1].BackColor = ColorTranslator.FromHtml("#16A5DC");
                cellgrid.Model[0, 1].TextColor = Color.White;

                cellgrid.Model[0, 2].CellValue = "Particulars";
                cellgrid.Model[0, 2].BackColor = ColorTranslator.FromHtml("#16A5DC");
                cellgrid.Model[0, 2].TextColor = Color.White;

                cellgrid.Model[0, 3].CellValue = "Op.Credit";
                cellgrid.Model[0, 3].BackColor = ColorTranslator.FromHtml("#16A5DC");
                cellgrid.Model[0, 3].TextColor = Color.White;
                cellgrid.Model[0, 4].CellValue = "Op.Debit";
                cellgrid.Model[0, 4].BackColor = ColorTranslator.FromHtml("#16A5DC");
                cellgrid.Model[0, 4].TextColor = Color.White;

                cellgrid.Model[0, 6].CellValue = "Tr.Debit";
                cellgrid.Model[0, 6].BackColor = ColorTranslator.FromHtml("#16A5DC");
                cellgrid.Model[0, 6].TextColor = Color.White;
                cellgrid.Model[0, 5].CellValue = "Tr.Credit";
                cellgrid.Model[0, 5].BackColor = ColorTranslator.FromHtml("#16A5DC");
                cellgrid.Model[0, 5].TextColor = Color.White;

                cellgrid.Model[0, 8].CellValue = "Cl.Debit";
                cellgrid.Model[0, 8].BackColor = ColorTranslator.FromHtml("#16A5DC");
                cellgrid.Model[0, 8].TextColor = Color.White;
                cellgrid.Model[0, 7].CellValue = "Cl.Credit";
                cellgrid.Model[0, 7].BackColor = ColorTranslator.FromHtml("#16A5DC");
                cellgrid.Model[0, 7].TextColor = Color.White;

                //set width fo column
                cellgrid.Model.ColWidths[1] = 50;
                cellgrid.Model.ColWidths[2] = 200;
                cellgrid.Model.ColWidths[3] = 100;
                cellgrid.Model.ColWidths[4] = 100;
                cellgrid.Model.ColWidths[5] = 100;
                cellgrid.Model.ColWidths[6] = 100;
                cellgrid.Model.ColWidths[7] = 100;
                cellgrid.Model.ColWidths[8] = 100;

                this.cellgrid.ColWidths.ResizeToFit(GridRangeInfo.Cols(1, 8), GridResizeToFitOptions.NoShrinkSize);
                var grp = from p in Trans
                          group p by p.GroupName into g
                          select new { GroupName = g.Key, count = g.Count() };
                int X = 1;
                foreach (var item in grp)
                {
                    var lst = Trans.Where(x => x.GroupName == item.GroupName).OrderBy(x => x.Party);

                    cellgrid.Model.Rows.InsertRange(X, 1);
                    cellgrid.Model[X, 2].CellValue = item.GroupName;
                    cellgrid.Model[X, 2].TextColor = ColorTranslator.FromHtml("#e3165b");
                    cellgrid.Model[X, 2].Font.Bold =true;
                    cellgrid.Model.CoveredRanges.Add(GridRangeInfo.Cells(X, 2, X, 8));

                    // cellgrid.tab
                    X = X + 1;
                    foreach (var _lst in lst)
                    {
                        cellgrid.Model.Rows.InsertRange(X, 1);
                        //audit colum
                        cellgrid.Model[X, 1].CellType = GridCellTypeName.CheckBox; ;
                        cellgrid.Model[X, 1].CellValue = _lst.Audit;
                        this.cellgrid[X, 1].CheckBoxOptions.CheckedValue = "true";
                        this.cellgrid[X, 1].CheckBoxOptions.UncheckedValue = "false";
                        this.cellgrid[X, 1].HorizontalAlignment = GridHorizontalAlignment.Center;
                        this.cellgrid[X, 1].VerticalAlignment = GridVerticalAlignment.Middle;
                        //Sets the type of the cell value as bool.
                        this.cellgrid[X, 1].CellValueType = typeof(bool);

                        cellgrid.Model[X, 2].CellValue = "  " + _lst.Party;
                        //op balance
                        if (_lst.OpCredit > 0)
                            cellgrid.Model[X, 3].CellValue = _lst.OpCredit;
                        cellgrid.Model[X, 3].HorizontalAlignment = GridHorizontalAlignment.Right;
                        if (_lst.OpDebit > 0)
                            cellgrid.Model[X, 4].CellValue = _lst.OpDebit;
                        cellgrid.Model[X, 4].HorizontalAlignment = GridHorizontalAlignment.Right;
                        opDebit = opDebit + _lst.OpDebit;
                        opCredit = opCredit + _lst.OpCredit;

                        //Transaction Entry
                        if (_lst.ClCredit > 0)
                            cellgrid.Model[X, 5].CellValue = _lst.ClCredit;
                        cellgrid.Model[X, 5].HorizontalAlignment = GridHorizontalAlignment.Right;
                        if (_lst.ClDebit > 0)
                            cellgrid.Model[X, 6].CellValue = _lst.ClDebit;
                        cellgrid.Model[X, 6].HorizontalAlignment = GridHorizontalAlignment.Right;

                        trDebit = trDebit + _lst.ClDebit;
                        trCredit = trCredit + _lst.ClCredit;

                        //closing Balance Entry
                        if (_lst.Credit > 0)
                            cellgrid.Model[X, 7].CellValue = _lst.Credit;
                        cellgrid.Model[X, 7].HorizontalAlignment = GridHorizontalAlignment.Right;
                        if (_lst.Debit > 0)
                            cellgrid.Model[X, 8].CellValue = _lst.Debit;
                        cellgrid.Model[X, 8].HorizontalAlignment = GridHorizontalAlignment.Right;
                        clDebit = clDebit + _lst.Debit;
                        clCredit = clCredit + _lst.Credit;


                        //account id
                        cellgrid.Model[X, 9].CellValue = _lst.AcId;

                        //CreditTotal = CreditTotal + _lst.Credit;

                        X++;
                    }

                    cellgrid.Model.Rows.InsertRange(X, 1);
                    // sum opening balance
                    cellgrid.Model[X, 3].HorizontalAlignment = GridHorizontalAlignment.Right;
                    cellgrid.Model[X, 3].CellValue = lst.Sum(x => x.OpCredit);
                    cellgrid.Model[X, 3].Font.Bold = true;
                    cellgrid.Model[X, 3].Borders.Top = new GridBorder(GridBorderStyle.Solid, Color.Blue, GridBorderWeight.Thin);
                    cellgrid.Model[X, 4].HorizontalAlignment = GridHorizontalAlignment.Right;
                    cellgrid.Model[X, 4].CellValue = lst.Sum(x => x.OpDebit);
                    cellgrid.Model[X, 4].Font.Bold = true;
                    cellgrid.Model[X, 4].Borders.Top = new GridBorder(GridBorderStyle.Solid, Color.Blue, GridBorderWeight.Thin);

                    // sum Transaction balance balance
                    cellgrid.Model[X, 5].HorizontalAlignment = GridHorizontalAlignment.Right;
                    cellgrid.Model[X, 5].CellValue = lst.Sum(x => x.ClCredit);
                    cellgrid.Model[X, 5].Font.Bold = true;
                    cellgrid.Model[X, 5].Borders.Top = new GridBorder(GridBorderStyle.Solid, Color.Blue, GridBorderWeight.Thin);
                    cellgrid.Model[X, 6].HorizontalAlignment = GridHorizontalAlignment.Right;
                    cellgrid.Model[X, 6].CellValue = lst.Sum(x => x.ClDebit);
                    cellgrid.Model[X, 6].Font.Bold = true;
                    cellgrid.Model[X, 6].Borders.Top = new GridBorder(GridBorderStyle.Solid, Color.Blue, GridBorderWeight.Thin);

                    // sum Closing  balance balance
                    cellgrid.Model[X, 7].HorizontalAlignment = GridHorizontalAlignment.Right;
                    cellgrid.Model[X, 7].CellValue = lst.Sum(x => x.Credit);
                    cellgrid.Model[X, 7].Font.Bold = true;
                    cellgrid.Model[X, 7].Borders.Top = new GridBorder(GridBorderStyle.Solid, Color.Blue, GridBorderWeight.Thin);
                    cellgrid.Model[X, 8].HorizontalAlignment = GridHorizontalAlignment.Right;
                    cellgrid.Model[X, 8].CellValue = lst.Sum(x => x.Debit);
                    cellgrid.Model[X, 8].Font.Bold = true;
                    cellgrid.Model[X, 8].Borders.Top = new GridBorder(GridBorderStyle.Solid, Color.Blue, GridBorderWeight.Thin);


                    X++;
                }

                // final total
                cellgrid.Model.Rows.InsertRange(X, 1);
                cellgrid.Model[X, 2].CellValue = "Total : ";

                cellgrid.Model[X, 3].CellValue = opCredit;
                cellgrid.Model[X, 4].CellValue = opDebit;

                cellgrid.Model[X, 5].CellValue = trCredit;
                cellgrid.Model[X, 6].CellValue = trDebit;

                cellgrid.Model[X, 7].CellValue = clCredit;
                cellgrid.Model[X, 8].CellValue = clDebit;


                for (int i = 2; i < 9; i++)
                {
                    cellgrid.Model[X, i].HorizontalAlignment = GridHorizontalAlignment.Right;
                    cellgrid.Model[X, i].BackColor = ColorTranslator.FromHtml("#16A5DC");
                    cellgrid.Model[X, i].TextColor = Color.Red;
                    cellgrid.Model[X, i].Font.Bold = true;
                }
                //cellgrid.Model.f = 1;

               
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Trail Balance HF");
                if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                MessageBox.Show(ex.ToString());

            }
        }

        private void GenerateVerticalFormat()
        {
            try
            {
               
            
               
                cellgrid.Model.Options.ExcelLikeCurrentCell = true;

               
                cellgrid.Model.TableStyle.ReadOnly = true;
             
                cellgrid.Model.Rows.RemoveRange(0, cellgrid.Model.RowCount);
                cellgrid.Model.Cols.RemoveRange(0, cellgrid.Model.ColCount);
                cellgrid.Model.RowCount = 1;
                cellgrid.Model.ColCount = 8; // Trans.GetType().GetGenericArguments()[0].GetProperties().Length;
                // cellgrid.Model.ColumnWidths.SetHidden(0, 0, true);
                //cellgrid.Model.HeaderColumns = 0;
                cellgrid.Model.Cols.Hidden[8] = true;
                cellgrid.Model.Cols.Hidden[7] = true;

                

                //cellgrid.Model.ColumnWidths[5] = 0;
                cellgrid.Model[0, 1].CellValue = "Credit";
                cellgrid.Model[0, 1].BackColor = ColorTranslator.FromHtml("#16A5DC");
                cellgrid.Model[0, 1].TextColor = Color.White;
                cellgrid.Model[0, 2].CellValue = "Particulars";
                cellgrid.Model[0, 2].BackColor = ColorTranslator.FromHtml("#16A5DC");
                cellgrid.Model[0, 2].TextColor = Color.White;

                cellgrid.Model[0, 4].CellValue = "Debit";
                cellgrid.Model[0, 4].BackColor = ColorTranslator.FromHtml("#16A5DC");
                cellgrid.Model[0, 4].TextColor = Color.White;

                cellgrid.Model[0, 5].CellValue = "Particulars";
                cellgrid.Model[0, 5].BackColor = ColorTranslator.FromHtml("#16A5DC");
                cellgrid.Model[0, 5].TextColor = Color.White;

                cellgrid.Model.Cols.Hidden[0] = true;
                cellgrid.Model.ColWidths[1] = 125;
                cellgrid.Model.ColWidths[2] = 250;
                cellgrid.Model.ColWidths[3] = 20;
                cellgrid.Model.ColWidths[4] = 125;
                cellgrid.Model.ColWidths[5] = 250;
                cellgrid.Model.ColWidths[6] = 20;

                this.cellgrid.ColWidths.ResizeToFit(GridRangeInfo.Cols(1, 8), GridResizeToFitOptions.NoShrinkSize);

                var grp = from p in Trans
                          where p.Credit > 0
                          group p by p.GroupName into g
                          select new { GroupName = g.Key, count = g.Count() };
                int X = 1;
                decimal CreditTotal = 0;
                foreach (var item in grp)
                {
                    var lst = Trans.Where(x => x.GroupName == item.GroupName && x.Credit > 0).OrderBy(x => x.Party);

                    cellgrid.Model.Rows.InsertRange(X, 1);
                    cellgrid.Model[X, 2].CellValue = item.GroupName;
                    cellgrid.Model[X, 2].TextColor = ColorTranslator.FromHtml("#e3165b");
                    cellgrid.Model[X, 2].Font.Bold = true;
                    X = X + 1;
                    foreach (var _lst in lst)
                    {
                        cellgrid.Model.Rows.InsertRange(X, 1);
                        cellgrid.Model[X, 2].CellValue = "  " + _lst.Party;
                        cellgrid.Model[X, 1].CellValue = _lst.Credit;
                        cellgrid.Model[X, 1].HorizontalAlignment = GridHorizontalAlignment.Right;
                        cellgrid.Model[X, 3].CellType = "CheckBox";
                        this.cellgrid[X, 3].CheckBoxOptions.CheckedValue = "true";
                        this.cellgrid[X, 3].CheckBoxOptions.UncheckedValue = "false";
                        this.cellgrid[X, 3].HorizontalAlignment = GridHorizontalAlignment.Center;
                        this.cellgrid[X, 3].VerticalAlignment = GridVerticalAlignment.Middle;
                        //Sets the type of the cell value as bool.
                        this.cellgrid[X,3].CellValueType = typeof(bool);

                        cellgrid.Model[X, 3].CellValue = _lst.Audit;
                        CreditTotal = CreditTotal + _lst.Credit;

                        cellgrid.Model[X, 7].CellValue = _lst.AcId;
                        //var combo1 = cellgrid.Model[X, 2];

                        X++;
                    }

                    cellgrid.Model.Rows.InsertRange(X, 1);
                    cellgrid.Model[X, 1].HorizontalAlignment = GridHorizontalAlignment.Right;
                    cellgrid.Model[X, 1].CellValue = lst.Sum(x => x.Credit);
                    cellgrid.Model[X, 1].Font.Bold = true;
                    //cellgrid.Model[X, 0].Font. = FontStyles.i
                    cellgrid.Model[X, 1].Borders.Top = new GridBorder(GridBorderStyle.Solid, Color.Blue, GridBorderWeight.Thin);
                    X++;
                }
                var grpdr = from p in Trans
                            where p.Debit > 0
                            group p by p.GroupName into g
                            select new { GroupName = g.Key, count = g.Count() };

                X = 1;
                decimal DebitTotal = 0;
                foreach (var item in grpdr)
                {
                    var lst = Trans.Where(x => x.GroupName == item.GroupName && x.Debit > 0).OrderBy(x => x.Party);

                    if (X == cellgrid.Model.RowCount)
                        cellgrid.Model.Rows.InsertRange(X, 1);


                    cellgrid.Model[X, 5].CellValue = item.GroupName;
                    cellgrid.Model[X, 5].TextColor = ColorTranslator.FromHtml("#e3165b");
                    cellgrid.Model[X, 5].Font.Bold = true;
                    X = X + 1;
                    foreach (var _lst in lst)
                    {
                        if (X == cellgrid.Model.RowCount)
                            cellgrid.Model.Rows.InsertRange(X, 1);

                        cellgrid.Model[X, 5].CellValue = "  " + _lst.Party;
                        cellgrid.Model[X, 4].CellValue = _lst.Debit;
                        cellgrid.Model[X, 4].HorizontalAlignment = GridHorizontalAlignment.Right;
                        DebitTotal = DebitTotal + _lst.Debit;
                        cellgrid.Model[X, 6].CellType = GridCellTypeName.CheckBox;
                        this.cellgrid[X, 6].CheckBoxOptions.CheckedValue = "true";
                        this.cellgrid[X, 6].CheckBoxOptions.UncheckedValue = "false";
                        this.cellgrid[X, 6].HorizontalAlignment = GridHorizontalAlignment.Center;
                        this.cellgrid[X, 6].VerticalAlignment = GridVerticalAlignment.Middle;
                        //Sets the type of the cell value as bool.
                        this.cellgrid[X, 6].CellValueType = typeof(bool);
                        cellgrid.Model[X, 6].CellValue = _lst.Audit;
                        cellgrid.Model[X, 8].CellValue = _lst.AcId; // account id for account
                        X++;
                    }
                    if (X == cellgrid.Model.RowCount)
                        cellgrid.Model.Rows.InsertRange(X, 1);
                    cellgrid.Model[X, 4].HorizontalAlignment = GridHorizontalAlignment.Right;
                    cellgrid.Model[X, 4].CellValue = lst.Sum(x => x.Debit);
                    cellgrid.Model[X, 4].Font.Bold = true;
                    cellgrid.Model[X, 4].Borders.Top = new GridBorder(GridBorderStyle.Solid, Color.Blue, GridBorderWeight.Thin);
                    X++;
                }

                //cellgrid.Model.ColumnWidths[0] = 50;
                //cellgrid.Model.ColumnWidths[1] = 100;

                //cellgrid.Model.ColumnWidths[2] = 50;
                //cellgrid.Model.ColumnWidths[3] = 100;
                 X = cellgrid.Model.RowCount;

                cellgrid.Model.Rows.InsertRange(X, 1);
                cellgrid.Model[X, 4].CellValue = DebitTotal;
                cellgrid.Model[X, 1].CellValue = CreditTotal;

                cellgrid.Model[X, 2].CellValue = "Total";
                cellgrid.Model[X, 5].CellValue = "Total";
                cellgrid.Model[X, 4].HorizontalAlignment = GridHorizontalAlignment.Right;
                cellgrid.Model[X, 1].HorizontalAlignment = GridHorizontalAlignment.Right;
                cellgrid.Model[X, 1].Font.Bold = true;
                cellgrid.Model[X, 4].Font.Bold = true;
                for (int i = 1; i < 6; i++)
                {

                    cellgrid.Model[X, i].BackColor = ColorTranslator.FromHtml("#16A5DC");
                    cellgrid.Model[X, i].TextColor = Color.White;

                }

                //cellgrid.Model.FooterRows = 1;

            }
            catch (Exception ex)
            {

                Log.Error(ex, "Trail Balance HF");
                if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                MessageBox.Show(ex.ToString());
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            DetailView();
        }

        private void DetailView()
        {
            var fdate = Convert.ToInt32(fDateEdit.DateTime.ToString("yyyyMMdd"));
            var tdate = Convert.ToInt32(tDateEdit.DateTime.ToString("yyyyMMdd"));
            //int typeid = 0;

            if (fdate != 0 && tdate != 0)
            {
                string _showall = "N";
                string _closingTb = "Y";

                if (checkEdit1.Checked && fromatLookUpEdit.EditValue.ToString() != "VF")
                    _showall = "Y";

                if (viewLookUpEdit.EditValue.ToString() == "CL")
                    _closingTb = "Y";
                else
                    _closingTb = "N";

                using (var db = new KontoContext()) {
                    db.Database.CommandTimeout = 0;
                    Trans = db.Database.SqlQuery<TrialDto>(
                        "dbo.TrialBalanceReport @CompanyId={0},@FromDate={1},@ToDate={2},@inclop={3},@showall={4},@closingtb={5},@year={6},@groupid={7}",
                        Convert.ToInt32(KontoGlobals.CompanyId), fdate, tdate, "Y", _showall, _closingTb, KontoGlobals.YearId, this.AcGroupId).ToList();
                }
                if (Trans.Count == 0)
                {
                    MessageBox.Show("Record Not Found!!");
                }
                //KontoUtils.LoadDxGridLayout(gridControl1, "Outs\\" + KontoGlobals.TrialBalance_layout);

            }
        }

        private void TrailMainView_Load(object sender, EventArgs e)
        {
            fromatLookUpEdit.EditValue = "VF";
            viewLookUpEdit.EditValue = "CL";

            if (this.AcGroupId > 0)
            {
                this.fDateEdit.DateTime = this._fromDate;
                this.tDateEdit.DateTime = this._Todate;
                this.okSimpleButton.PerformClick();
                cellgrid.Focus();
               
            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Audit();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if(cellgrid.RowCount > 0)
            ShowLedger();
        }
        private void ShowLedger()
        {
            try
            {
                int id = 0;
                if (fromatLookUpEdit.EditValue.ToString() == "VF")
                {
                    if (cellgrid.CurrentCell.ColIndex <= 2)
                    {
                        if (!string.IsNullOrEmpty(cellgrid.Model[cellgrid.CurrentCell.RowIndex, 7].CellValue.ToString()))
                            id = Convert.ToInt32(cellgrid.Model[cellgrid.CurrentCell.RowIndex, 7].CellValue);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(cellgrid.Model[cellgrid.CurrentCell.RowIndex, 8].CellValue.ToString()))
                            id = Convert.ToInt32(cellgrid.Model[cellgrid.CurrentCell.RowIndex, 8].CellValue);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(cellgrid.Model[cellgrid.CurrentCell.RowIndex, 9].CellValue.ToString()))
                        id = Convert.ToInt32(cellgrid.Model[cellgrid.CurrentCell.RowIndex, 9].CellValue);
                }

                if (id == 0) return;

                var frm = new Ledger.LedgerMainView();
                frm.AccId = id;
                frm._fromDate = this.fDateEdit.DateTime;
                frm._toDate = this.tDateEdit.DateTime;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
