using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Data.Models.Reports;
using Konto.Data.Models.Transaction;
using Konto.Reporting.Para.TrialBalance;
using Serilog;
using Syncfusion.GridHelperClasses;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Reporting.Para.BlSheet
{
    public partial class BlMainView : KontoForm
    {
        List<BalDto> Trans = new List<BalDto>();
        
        decimal pnl,gpl;
        string Print = "B";
        public BlMainView()
        {
            InitializeComponent();
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Horizontal-F", "HF"),
                new ComboBoxPairs("Vertical-F", "VF"),

            };
            fromatLookUpEdit.Properties.DataSource = cbp;

           
            fDateEdit.DateTime = KontoGlobals.DFromDate;
            tDateEdit.DateTime = KontoGlobals.DToDate;

            this.Load += TrailMainView_Load;
            okSimpleButton.Click += OkSimpleButton_Click;
            cancelSimpleButton.Click += CancelSimpleButton_Click;
            printSimpleButton.Click += PrintSimpleButton_Click;
            cellgrid.CurrentCellKeyDown += Cellgrid_CurrentCellKeyDown;
            this.FormClosed += TrialMainView_FormClosed;
            this.exportSimpleButton.Click += ExportSimpleButton_Click;
            this.trSimpleButton.Click += TrSimpleButton_Click;
            this.plSimpleButton.Click += PlSimpleButton_Click;
            this.bsSimpleButton.Click += BsSimpleButton_Click;
            ZoomGrid zoom = new ZoomGrid(this.cellgrid);

            this.FirstActiveControl = fDateEdit;

            //Zoom the grid with the specific percentage
            zoom.zoomGrid("120");
        }

        private void BsSimpleButton_Click(object sender, EventArgs e)
        {
            Print = "B";
           
            if (fromatLookUpEdit.EditValue.ToString() == "VF")
                Balsheet_VF();
            else
                Balsheet_HF();

            cellgrid.Focus();
        }
        void Balsheet_VF()
        {
            ExecuteProcedure();

          
           
            cellgrid.Model.Options.ExcelLikeCurrentCell = true;

         
            cellgrid.Model.TableStyle.ReadOnly = true;
            
            cellgrid.Model.Rows.RemoveRange(0, cellgrid.Model.RowCount);
            cellgrid.Model.Cols.RemoveRange(0, cellgrid.Model.ColCount);
            cellgrid.Model.ColCount = 8;
            cellgrid.Model.RowCount = 1;
           
            cellgrid.Model.Cols.Hidden[7]=true;
            cellgrid.Model.Cols.Hidden[8] = true;

            cellgrid.Model[0, 1].CellValue = "Amount";
            cellgrid.Model[0, 1].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cellgrid.Model[0, 1].TextColor = Color.White;
            cellgrid.Model[0, 2].CellValue = "LiabLities";
            cellgrid.Model[0, 2].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cellgrid.Model[0, 2].TextColor = Color.White;

            cellgrid.Model[0, 4].CellValue = "Amount";
            cellgrid.Model[0, 4].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cellgrid.Model[0, 4].TextColor = Color.White;

            cellgrid.Model[0, 5].CellValue = "Assets";
            cellgrid.Model[0, 5].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cellgrid.Model[0, 5].TextColor = Color.White;

            cellgrid.Model.ColWidths[1] = 125;
            cellgrid.Model.ColWidths[2] = 250;
            cellgrid.Model.ColWidths[3] = 20;
            cellgrid.Model.ColWidths[4] = 125;
            cellgrid.Model.ColWidths[5] = 250;
            cellgrid.Model.ColWidths[6] = 20;


            // get details trading Date from          balsheet 
            var tr = Trans.Where(x => x.TransType == 3);

            var grp = from p in tr
                      where p.Bal < 0
                      orderby p.BlSort, p.Acgroup
                      group p by p.Acgroup into g
                      select new { GroupName = g.Key, count = g.Count() };
            int X = 1;


            foreach (var item in grp)
            {
                var lst = tr.Where(x => x.Acgroup == item.GroupName && x.Bal < 0).OrderBy(x => x.BlSort).OrderBy(x => x.AccountName);

                cellgrid.Model.Rows.InsertRange(X, 1);
                cellgrid.Model[X, 2].CellValue = item.GroupName;
                cellgrid.Model[X, 2].TextColor = ColorTranslator.FromHtml("#e3165b");
                cellgrid.Model[X, 2].Font.Bold = true;
                X = X + 1;
                foreach (var _lst in lst)
                {
                    cellgrid.Model.Rows.InsertRange(X, 1);
                    cellgrid.Model[X, 2].CellValue = "  " + _lst.AccountName;
                    cellgrid.Model[X, 1].CellValue = _lst.Diff;
                    cellgrid.Model[X, 1].HorizontalAlignment = GridHorizontalAlignment.Right;
                    cellgrid.Model[X, 3].CellType = "CheckBox";
                    cellgrid.Model[X, 3].CellValue = _lst.Audit;
                    cellgrid.Model[X, 7].CellValue = _lst.AcId;

                    X++;
                }

                cellgrid.Model.Rows.InsertRange(X, 1);
                cellgrid.Model[X, 1].HorizontalAlignment = GridHorizontalAlignment.Right;
                cellgrid.Model[X, 1].CellValue = lst.Sum(x => x.Diff);
                cellgrid.Model[X, 1].Font.Bold = true;
                cellgrid.Model[X, 1].Borders.Top = new GridBorder(GridBorderStyle.Solid, Color.IndianRed, GridBorderWeight.Thin);
                X++;
            }
            // income
            var grpdr = from p in tr
                        where p.Bal > 0
                        orderby p.BlSort, p.Acgroup
                        group p by p.Acgroup into g
                        select new { GroupName = g.Key, count = g.Count() };

            X = 2;

            foreach (var item in grpdr)
            {
                var lst = tr.Where(x => x.Acgroup == item.GroupName && x.Bal > 0).OrderBy(x => x.BlSort).OrderBy(x => x.AccountName);

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

                    cellgrid.Model[X, 5].CellValue = "  " + _lst.AccountName;
                    cellgrid.Model[X, 4].CellValue = _lst.Diff;
                    cellgrid.Model[X, 4].HorizontalAlignment = GridHorizontalAlignment.Right;

                    cellgrid.Model[X, 6].CellType = "CheckBox";
                    cellgrid.Model[X, 6].CellValue = _lst.Audit;
                    cellgrid.Model[X, 8].CellValue = _lst.AcId; // account id for account
                    X++;
                }
                if (X == cellgrid.Model.RowCount)
                    cellgrid.Model.Rows.InsertRange(X, 1);
                cellgrid.Model[X, 4].HorizontalAlignment = GridHorizontalAlignment.Right;
                cellgrid.Model[X, 4].CellValue = lst.Sum(x => x.Diff);
                cellgrid.Model[X, 4].Font.Bold = true;
                cellgrid.Model[X, 4].Borders.Top = new GridBorder(GridBorderStyle.Solid, Color.IndianRed, GridBorderWeight.Thin);
                X++;
            }
            int Y = 0;

            X = cellgrid.Model.RowCount;
            cellgrid.Model.Rows.InsertRange(X, 1);
            //}
            //X++;

            decimal np = Trans.Where(x => x.TransType != 3 && x.Bal < 0).Sum(x => x.Diff)
                - Trans.Where(x => x.TransType != 3 && x.Bal > 0).Sum(x => x.Diff);

            //  cellgrid.Model.InsertRows(X, 1);
            // profit from np;
            decimal debittotal = tr.Where(x => x.Bal > 0).Sum(x => x.Diff); //expense
            decimal credittotal = tr.Where(x => x.Bal < 0).Sum(x => x.Diff); //icome
                                           
            X++;
            cellgrid.Model.Rows.InsertRange(X, 1);

            //X++;
            cellgrid.Model[X, 1].CellValue = credittotal;
            cellgrid.Model[X, 4].CellValue = debittotal;

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

            
        }

        void Balsheet_HF()
        {
            ExecuteProcedure();

           
           
            cellgrid.Model.Options.ExcelLikeCurrentCell = true;

          
            cellgrid.Model.TableStyle.ReadOnly = true;
           
            cellgrid.Model.Rows.RemoveRange(0, cellgrid.Model.RowCount);
            cellgrid.Model.Cols.RemoveRange(0, cellgrid.Model.ColCount);
            cellgrid.Model.ColCount = 6;
            cellgrid.Model.RowCount = 1;
            
            cellgrid.Model.Cols.Hidden[6]= true;

            cellgrid.Model.ColWidths[1] = 20;
            cellgrid.Model.ColWidths[2] = 200;
            cellgrid.Model.ColWidths[3] = 100;
            cellgrid.Model.ColWidths[4] = 100;
            cellgrid.Model.ColWidths[5] = 100;

            cellgrid.Model[0, 2].CellValue = "Particulars";
            cellgrid.Model[0, 2].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cellgrid.Model[0, 2].TextColor = Color.White;

            cellgrid.Model[0, 3].CellValue = "Amount as C-FY";
            cellgrid.Model[0, 3].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cellgrid.Model[0, 3].TextColor = Color.White;

            cellgrid.Model[0, 4].CellValue = "Amount as P-FY";
            cellgrid.Model[0, 4].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cellgrid.Model[0, 4].TextColor = Color.White;

            cellgrid.Model[0, 5].CellValue = "Changes in %";
            cellgrid.Model[0, 5].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cellgrid.Model[0, 5].TextColor = Color.White;

            var tr = Trans.Where(x => x.TransType == 3);

            var grp = from p in tr
                      orderby p.Acgroup
                      group p by p.Acgroup into g
                      select new { GroupName = g.Key, count = g.Count() };
            int X = 1;



            foreach (var item in grp)
            {
                var lst = tr.Where(x => x.Acgroup == item.GroupName).OrderBy(x => x.AccountName);
                cellgrid.Model.Rows.InsertRange(X, 1);
                cellgrid.Model[X, 2].CellValue = item.GroupName;
                cellgrid.Model[X, 2].TextColor = ColorTranslator.FromHtml("#e3165b");
                cellgrid.Model[X, 2].Font.Bold = true;
                
                cellgrid.Model.CoveredRanges.Add(GridRangeInfo.Cells(X, 2, X, 6));

                
                
                X = X + 1;
                foreach (var _lst in lst)
                {
                    cellgrid.Model.Rows.InsertRange(X, 1);
                    //audit colum
                    cellgrid.Model[X, 1].CellType = "CheckBox";
                    cellgrid.Model[X, 1].CellValue = _lst.Audit;
                    cellgrid.Model[X, 6].CellValue = _lst.AcId;
                    cellgrid.Model[X, 2].CellValue = "  " + _lst.AccountName;
                    if (_lst.Nature == "LIABILITIES")
                    {
                        if (_lst.Bal > 0)
                        {
                            cellgrid.Model[X, 3].CellValue = "(-) " + _lst.Diff.ToString();
                            cellgrid.Model[X, 4].CellValue = "(-) " + _lst.PreDiff;
                        }
                        else
                        {
                            cellgrid.Model[X, 3].CellValue = _lst.Diff;
                            cellgrid.Model[X, 4].CellValue = _lst.PreDiff;
                        }
                    }
                    else
                    {
                        if (_lst.Bal > 0)
                        {
                            cellgrid.Model[X, 3].CellValue = _lst.Diff;
                            cellgrid.Model[X, 4].CellValue = _lst.PreDiff;
                        }
                        else
                        {
                            cellgrid.Model[X, 3].CellValue = "(-) " + _lst.Diff.ToString();
                            cellgrid.Model[X, 4].CellValue = "(-) " + _lst.PreDiff;
                        }

                    }
                    cellgrid.Model[X, 3].HorizontalAlignment = GridHorizontalAlignment.Right;
                    cellgrid.Model[X, 4].HorizontalAlignment = GridHorizontalAlignment.Right;
                    X++;
                }
                cellgrid.Model.Rows.InsertRange(X, 1);
                // sum opening balance
                cellgrid.Model[X, 3].HorizontalAlignment = GridHorizontalAlignment.Right;
                cellgrid.Model[X, 3].CellValue = Math.Abs(lst.Sum(x => x.Bal));
                cellgrid.Model[X, 3].Font.Bold = true;
                cellgrid.Model[X, 3].Borders.Top = new GridBorder(GridBorderStyle.Solid, Color.IndianRed, GridBorderWeight.Thin);
                cellgrid.Model[X, 4].HorizontalAlignment = GridHorizontalAlignment.Right;
                cellgrid.Model[X, 4].CellValue = Math.Abs(lst.Sum(x => x.preBal));
                cellgrid.Model[X, 4].Font.Bold = true;
                cellgrid.Model[X, 4].Borders.Top = new GridBorder(GridBorderStyle.Solid, Color.IndianRed, GridBorderWeight.Thin);
                X++;
            }
            cellgrid.Model.Rows.InsertRange(X, 1);

            //calculte gross profit

            decimal gp = Trans.Where(x => x.TransType != 3 && x.Bal < 0).Sum(x => x.Diff)
                - Trans.Where(x => x.TransType != 3 && x.Bal > 0).Sum(x => x.Diff);

            decimal pregp = Trans.Where(x => x.TransType != 3 && x.preBal < 0).Sum(x => x.PreDiff)
                - Trans.Where(x => x.TransType != 0 && x.preBal > 0).Sum(x => x.PreDiff); // previous year

            cellgrid.Model[X, 3].HorizontalAlignment = GridHorizontalAlignment.Right;
            cellgrid.Model[X, 4].HorizontalAlignment = GridHorizontalAlignment.Right;

            

        }
        private void PlSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Print = "P";

                if (fromatLookUpEdit.EditValue.ToString() == "VF")
                    ProftiOrLoss_VF();
                else
                    ProftiOrLoss_HF();

                cellgrid.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                Log.Error(ex, "Profit Or Loss");
            }
           
        }
        void ProftiOrLoss_VF()
        {
            ExecuteProcedure();

          
            cellgrid.Model.Options.ExcelLikeCurrentCell = true;

            
            cellgrid.Model.TableStyle.ReadOnly = true;
            
            cellgrid.Model.Rows.RemoveRange(0, cellgrid.Model.RowCount);
            cellgrid.Model.Cols.RemoveRange(0, cellgrid.Model.ColCount);
            cellgrid.Model.ColCount = 8;
            cellgrid.Model.RowCount = 1;

            cellgrid.Model.Cols.Hidden[7] = true;
            cellgrid.Model.Cols.Hidden[8] = true;

            cellgrid.Model[0, 1].CellValue = "Amount";
            cellgrid.Model[0, 1].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cellgrid.Model[0, 1].TextColor = Color.White;
            cellgrid.Model[0, 2].CellValue = "Expense";
            cellgrid.Model[0, 2].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cellgrid.Model[0, 2].TextColor = Color.White;

            cellgrid.Model[0, 4].CellValue = "Amount";
            cellgrid.Model[0, 4].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cellgrid.Model[0, 4].TextColor = Color.White;

            cellgrid.Model[0, 5].CellValue = "Income";
            cellgrid.Model[0, 5].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cellgrid.Model[0, 5].TextColor = Color.White;

            cellgrid.Model.ColWidths[1] = 125;
            cellgrid.Model.ColWidths[2] = 250;
            cellgrid.Model.ColWidths[3] = 20;
            cellgrid.Model.ColWidths[4] = 125;
            cellgrid.Model.ColWidths[5] = 250;
            cellgrid.Model.ColWidths[6] = 20;


            // get details trading Date from          balsheet 
            var tr = Trans.Where(x => x.TransType == 1);

            var grp = from p in tr
                      where p.Bal > 0
                      group p by p.Acgroup into g
                      select new { GroupName = g.Key, count = g.Count() };
            int X = 1;

            decimal gp = Trans.Where(x => x.TransType == 0 && x.Bal < 0).Sum(x => x.Diff)
                - Trans.Where(x => x.TransType == 0 && x.Bal > 0).Sum(x => x.Diff);
            cellgrid.Model.Rows.InsertRange(X, 1);

            if (gp > 0)
            {
                cellgrid.Model[X, 4].CellValue = Math.Abs(gp);
                cellgrid.Model[X, 5].CellValue = "Gross Profit from Trading A/c";
                cellgrid.Model[X, 4].TextColor =Color.Blue;
                cellgrid.Model[X, 4].HorizontalAlignment = GridHorizontalAlignment.Right;

            }
            else
            {
                cellgrid.Model[X, 1].HorizontalAlignment = GridHorizontalAlignment.Right;
                cellgrid.Model[X, 1].CellValue = Math.Abs(gp);
                cellgrid.Model[X, 2].CellValue = "Gross Loss from Trading A/c";
                cellgrid.Model[X, 2].TextColor = Color.Red;
            }
            X++;
            foreach (var item in grp)
            {
                var lst = tr.Where(x => x.Acgroup == item.GroupName && x.Bal > 0).OrderBy(x => x.AccountName);

                cellgrid.Model.Rows.InsertRange(X, 1);
                cellgrid.Model[X, 2].CellValue = item.GroupName;
                cellgrid.Model[X, 2].TextColor = ColorTranslator.FromHtml("#e3165b");
                cellgrid.Model[X, 2].Font.Bold = true;
                X = X + 1;
                foreach (var _lst in lst)
                {
                    cellgrid.Model.Rows.InsertRange(X, 1);
                    cellgrid.Model[X, 2].CellValue = "  " + _lst.AccountName;
                    cellgrid.Model[X, 1].CellValue = _lst.Diff;
                    cellgrid.Model[X, 1].HorizontalAlignment = GridHorizontalAlignment.Right;
                    cellgrid.Model[X, 3].CellType = "CheckBox";
                    cellgrid.Model[X, 3].CellValue = _lst.Audit;
                    cellgrid.Model[X, 7].CellValue = _lst.AcId;

                    X++;
                }

                cellgrid.Model.Rows.InsertRange(X, 1);
                cellgrid.Model[X, 1].HorizontalAlignment = GridHorizontalAlignment.Right;
                cellgrid.Model[X, 1].CellValue = lst.Sum(x => x.Diff);
                cellgrid.Model[X, 1].Font.Bold = true;
                cellgrid.Model[X, 1].Borders.Top = new GridBorder(GridBorderStyle.Solid, Color.IndianRed, GridBorderWeight.Thin);
                X++;
            }
            // income
            var grpdr = from p in tr
                        where p.Bal < 0
                        group p by p.Acgroup into g
                        select new { GroupName = g.Key, count = g.Count() };

            X = 2;

            foreach (var item in grpdr)
            {
                var lst = tr.Where(x => x.Acgroup == item.GroupName && x.Bal < 0).OrderBy(x => x.AccountName);

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

                    cellgrid.Model[X, 5].CellValue = "  " + _lst.AccountName;
                    cellgrid.Model[X, 4].CellValue = _lst.Diff;
                    cellgrid.Model[X, 4].HorizontalAlignment = GridHorizontalAlignment.Right;

                    cellgrid.Model[X, 6].CellType = "CheckBox";
                    cellgrid.Model[X, 6].CellValue = _lst.Audit;
                    cellgrid.Model[X, 8].CellValue = _lst.AcId; // account id for account
                    X++;
                }
                if (X == cellgrid.Model.RowCount)
                    cellgrid.Model.Rows.InsertRange(X, 1);
                cellgrid.Model[X, 4].HorizontalAlignment = GridHorizontalAlignment.Right;
                cellgrid.Model[X, 4].CellValue = lst.Sum(x => x.Diff);
                cellgrid.Model[X, 4].Font.Bold = true;
                cellgrid.Model[X, 4].Borders.Top = new GridBorder(GridBorderStyle.Solid, Color.IndianRed, GridBorderWeight.Thin);
                X++;
            }
            int Y = 0;

            X = cellgrid.Model.RowCount;
            cellgrid.Model.Rows.InsertRange(X, 2);
            //}
            X++;
            decimal debittotal = tr.Where(x => x.Bal > 0).Sum(x => x.Diff); //expense
            decimal credittotal = tr.Where(x => x.Bal < 0).Sum(x => x.Diff); //icome
            decimal gpPer = 0;
            decimal saletotal = Trans.Where(x => x.AcgroupId == 9 && x.Bal < 0).Sum(x => x.Diff);
            if (gp > 0)
            {
                credittotal = credittotal + gp;
            }
            else
            {
                debittotal = debittotal + (-1 * gp);
            }

            if (debittotal > credittotal)
            {
                cellgrid.Model[X, 4].CellValue = debittotal - credittotal;
                cellgrid.Model[X, 4].HorizontalAlignment = GridHorizontalAlignment.Right;
                if (saletotal > 0)
                    gpPer = ((debittotal - credittotal) / saletotal) * 100;
                cellgrid.Model[X, 5].CellValue = "Net Loss : " + gpPer.ToString("F") + " %";
            }
            else
            {
                cellgrid.Model[X, 1].CellValue = credittotal - debittotal;
                if (saletotal > 0)
                    gpPer = ((credittotal - debittotal) / saletotal) * 100;
                cellgrid.Model[X, 1].HorizontalAlignment = GridHorizontalAlignment.Right;
                cellgrid.Model[X, 2].CellValue = "Net Profit : " + gpPer.ToString("F") + " %";
                cellgrid.Model[X, 1].TextColor = Color.Blue;
                cellgrid.Model[X, 1].Font.Bold = true;
            }
            X++;
            cellgrid.Model.Rows.InsertRange(X, 2);
            if (debittotal > credittotal)
                credittotal = credittotal + debittotal - credittotal;
            else
                debittotal = debittotal + credittotal - debittotal;
            X++;
            cellgrid.Model[X, 1].CellValue = debittotal;
            cellgrid.Model[X, 4].CellValue = credittotal;

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

        void ProftiOrLoss_HF()
        {
            ExecuteProcedure();

           
            cellgrid.Model.Options.ExcelLikeCurrentCell = true;

           
            cellgrid.Model.TableStyle.ReadOnly = true;
          
            cellgrid.Model.Rows.RemoveRange(0, cellgrid.Model.RowCount);
            cellgrid.Model.Cols.RemoveRange(0, cellgrid.Model.ColCount);
            cellgrid.Model.ColCount = 6;
            cellgrid.Model.RowCount = 1;
            

            cellgrid.Model.Cols.Hidden[6]=true;

            cellgrid.Model.ColWidths[1] = 20;
            cellgrid.Model.ColWidths[2] = 200;
            cellgrid.Model.ColWidths[3] = 100;
            cellgrid.Model.ColWidths[4] = 100;
            cellgrid.Model.ColWidths[5] = 100;

            cellgrid.Model[0, 2].CellValue = "Particulars";
            cellgrid.Model[0, 2].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cellgrid.Model[0, 2].TextColor = Color.White;

            cellgrid.Model[0, 3].CellValue = "Amount as C-FY";
            cellgrid.Model[0, 3].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cellgrid.Model[0, 3].TextColor = Color.White;

            cellgrid.Model[0, 4].CellValue = "Amount as P-FY";
            cellgrid.Model[0, 4].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cellgrid.Model[0, 4].TextColor = Color.White;

            cellgrid.Model[0, 5].CellValue = "Changes in %";
            cellgrid.Model[0, 5].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cellgrid.Model[0, 5].TextColor = Color.White;

            var tr = Trans.Where(x => x.TransType == 1);

            var grp = from p in tr
                      orderby p.Nature descending, p.Acgroup
                      group p by p.Acgroup into g
                      select new { GroupName = g.Key, count = g.Count() };
            int X = 1;

            //calculte gross profit

            decimal gp = Trans.Where(x => x.TransType == 0 && x.Bal < 0).Sum(x => x.Diff)
                - Trans.Where(x => x.TransType == 0 && x.Bal > 0).Sum(x => x.Diff);

            decimal pregp = Trans.Where(x => x.TransType == 0 && x.preBal < 0).Sum(x => x.PreDiff)
                - Trans.Where(x => x.TransType == 0 && x.preBal > 0).Sum(x => x.PreDiff); // previous year

            cellgrid.Model.Rows.InsertRange(X, 1);
            cellgrid.Model[X, 3].HorizontalAlignment = GridHorizontalAlignment.Right;
            cellgrid.Model[X, 4].HorizontalAlignment = GridHorizontalAlignment.Right;
            if (gp > 0)
            {
                cellgrid.Model[X, 3].CellValue = Math.Abs(gp);
                cellgrid.Model[X, 2].CellValue = "Gross Profit from Trading A/c";
                cellgrid.Model[X, 3].TextColor = Color.Blue;
            }
            else
            {
                cellgrid.Model[X, 3].CellValue = "(-) " + Math.Abs(gp).ToString("F");
                cellgrid.Model[X, 2].CellValue = "Gross Loss from Trading A/c";
                cellgrid.Model[X, 3].TextColor = Color.Red;
            }
            //previous year
            if (pregp > 0)
            {
                cellgrid.Model[X, 4].CellValue = Math.Abs(pregp) + " (P)";
                cellgrid.Model[X, 4].TextColor = Color.Blue;
            }
            else
            {
                cellgrid.Model[X, 4].CellValue = Math.Abs(pregp).ToString("F") + " (L)";
                cellgrid.Model[X, 4].TextColor = Color.Red;
            }


            X++;

            foreach (var item in grp)
            {
                var lst = tr.Where(x => x.Acgroup == item.GroupName).OrderBy(x => x.AccountName);
                cellgrid.Model.Rows.InsertRange(X, 1);
                cellgrid.Model[X, 2].CellValue = item.GroupName;
                cellgrid.Model[X, 2].TextColor = ColorTranslator.FromHtml("#e3165b");
                cellgrid.Model[X, 2].Font.Bold = true;
                cellgrid.Model.CoveredRanges.Add(GridRangeInfo.Cells(X, 1, X, 6));

                
                X = X + 1;
                foreach (var _lst in lst)
                {
                    cellgrid.Model.Rows.InsertRange(X, 1);
                    //audit colum
                    cellgrid.Model[X, 1].CellType = "CheckBox";
                    cellgrid.Model[X, 1].CellValue = _lst.Audit;
                    cellgrid.Model[X, 6].CellValue = _lst.AcId;
                    cellgrid.Model[X, 2].CellValue = "  " + _lst.AccountName;
                    if (_lst.Nature == "INCOME")
                    {
                        if (_lst.Bal > 0)
                        {
                            cellgrid.Model[X, 3].CellValue = "(-) " + _lst.Diff.ToString();
                            cellgrid.Model[X, 4].CellValue = "(-) " + _lst.PreDiff;
                        }
                        else
                        {
                            cellgrid.Model[X, 3].CellValue = _lst.Diff;
                            cellgrid.Model[X, 4].CellValue = _lst.PreDiff;
                        }
                    }
                    else
                    {
                        if (_lst.Bal > 0)
                        {
                            cellgrid.Model[X, 3].CellValue = _lst.Diff;
                            cellgrid.Model[X, 4].CellValue = _lst.PreDiff;
                        }
                        else
                        {
                            cellgrid.Model[X, 3].CellValue = "(-) " + _lst.Diff.ToString();
                            cellgrid.Model[X, 4].CellValue = "(-) " + _lst.PreDiff;
                        }

                    }
                    cellgrid.Model[X, 3].HorizontalAlignment = GridHorizontalAlignment.Right;
                    cellgrid.Model[X, 4].HorizontalAlignment = GridHorizontalAlignment.Right;
                    X++;
                }
                cellgrid.Model.Rows.InsertRange(X, 1);
                // sum opening balance
                cellgrid.Model[X, 3].HorizontalAlignment = GridHorizontalAlignment.Right;
                cellgrid.Model[X, 3].CellValue = Math.Abs(lst.Sum(x => x.Bal));
                cellgrid.Model[X, 3].Font.Bold = true;
                cellgrid.Model[X, 3].Borders.Top = new GridBorder(GridBorderStyle.Solid, Color.IndianRed, GridBorderWeight.Thin);
                cellgrid.Model[X, 4].HorizontalAlignment = GridHorizontalAlignment.Right;
                cellgrid.Model[X, 4].CellValue = Math.Abs(lst.Sum(x => x.preBal));
                cellgrid.Model[X, 4].Font.Bold = true;
                cellgrid.Model[X, 4].Borders.Top = new GridBorder(GridBorderStyle.Solid, Color.IndianRed, GridBorderWeight.Thin);
                X++;
            }
            cellgrid.Model.Rows.InsertRange(X, 1);

            decimal debittotal = tr.Where(x => x.Bal > 0).Sum(x => x.Diff); //expense
            decimal credittotal = tr.Where(x => x.Bal < 0).Sum(x => x.Diff); //icome
            decimal gpPer = 0;
            decimal saletotal = Trans.Where(x => x.AcgroupId == 9 && x.Bal < 0).Sum(x => x.Diff);

            decimal pdebittotal = tr.Where(x => x.preBal > 0).Sum(x => x.PreDiff); //expense
            decimal pcredittotal = tr.Where(x => x.preBal < 0).Sum(x => x.PreDiff); //icome
            decimal pgpPer = 0;
            decimal psaletotal = Trans.Where(x => x.AcgroupId == 9 && x.preBal < 0).Sum(x => x.PreDiff);

            if (gp > 0)
            {
                credittotal = credittotal + gp;
            }
            else
            {
                debittotal = debittotal + (-1 * gp);
            }
            if (pregp > 0)
            {
                pcredittotal = pcredittotal + pregp;
            }
            else
            {
                pdebittotal = pdebittotal + pregp;
            }

            if (debittotal > credittotal)
            {
                if (saletotal > 0)
                    gpPer = ((debittotal - credittotal) / saletotal) * 100;
                cellgrid.Model[X, 2].CellValue = "Net Loss : " + gpPer.ToString("F") + " %";
                cellgrid.Model[X, 3].TextColor = Color.Red;
            }
            else
            {
                if (saletotal > 0)
                    gpPer = ((credittotal - debittotal) / saletotal) * 100;
                cellgrid.Model[X, 2].CellValue = "Net Profit : " + gpPer.ToString("F") + " %";
                cellgrid.Model[X, 3].TextColor = Color.Blue;
            }

            cellgrid.Model[X, 3].CellValue = Math.Abs(debittotal - credittotal);
            cellgrid.Model[X, 3].HorizontalAlignment = GridHorizontalAlignment.Right;

            cellgrid.Model[X, 3].Font.Bold = true;


            //previous p FY
            if (psaletotal > 0)
            {
                if (pdebittotal > pcredittotal)
                {
                    if (psaletotal > 0)
                        pgpPer = ((pdebittotal - pcredittotal) / psaletotal) * 100;
                }
                else
                {
                    if (psaletotal > 0)
                        pgpPer = ((pcredittotal - pdebittotal) / psaletotal) * 100;
                }
            }

            cellgrid.Model[X, 4].CellValue = Math.Abs(pdebittotal - pcredittotal);
            cellgrid.Model[X, 4].HorizontalAlignment = GridHorizontalAlignment.Right;

            cellgrid.Model[X, 4].TextColor = Color.Blue;
            cellgrid.Model[X, 4].Font.Bold = true;


        }

        void ExecuteProcedure()
        {
            var fdate = Convert.ToInt32(fDateEdit.DateTime.ToString("yyyyMMdd"));
            var tdate = Convert.ToInt32(tDateEdit.DateTime.ToString("yyyyMMdd"));
            using (var _db = new KontoContext())
            {
                Trans = _db.Database.SqlQuery<BalDto>(
                   "dbo.Bal_sheet @CompanyId={0},@FromDate={1},@ToDate={2},@YearId={3},@Summary={4}",
                   Convert.ToInt32(KontoGlobals.CompanyId), fdate, tdate, KontoGlobals.YearId, "N").ToList();
            }

        }
        private void TrSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Print = "T";
                if (fromatLookUpEdit.EditValue.ToString() == "VF")
                    Trading_VF();
                else
                    Trading_HF();

                cellgrid.Focus();
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.ToString());
                Log.Error(ex, "Trading Account");
            }
           
        }
        void Trading_VF()
        {
            ExecuteProcedure();

          
            cellgrid.Model.Options.ExcelLikeCurrentCell = true;

           
            cellgrid.Model.TableStyle.ReadOnly = true;
            cellgrid.Model.Rows.RemoveRange(0, cellgrid.Model.RowCount);
            cellgrid.Model.Cols.RemoveRange(0, cellgrid.Model.ColCount);
            cellgrid.Model.ColCount = 8;
            cellgrid.Model.RowCount = 1;

            cellgrid.Model.Cols.Hidden[7] = true;
            cellgrid.Model.Cols.Hidden[8] = true;

            cellgrid.Model[0, 1].CellValue = "Amount";
            cellgrid.Model[0, 1].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cellgrid.Model[0, 1].TextColor = Color.White;
            cellgrid.Model[0, 2].CellValue = "Trading Expense";
            cellgrid.Model[0, 2].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cellgrid.Model[0, 2].TextColor = Color.White;

            cellgrid.Model[0, 4].CellValue = "Amount";
            cellgrid.Model[0, 4].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cellgrid.Model[0, 4].TextColor = Color.White;

            cellgrid.Model[0, 5].CellValue = "Trading Income";
            cellgrid.Model[0, 5].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cellgrid.Model[0, 5].TextColor = Color.White;

            cellgrid.Model.ColWidths[1] = 125;
            cellgrid.Model.ColWidths[2] = 250;
            cellgrid.Model.ColWidths[3] = 20;
            cellgrid.Model.ColWidths[4] = 125;
            cellgrid.Model.ColWidths[5] = 250;
            cellgrid.Model.ColWidths[6] = 20;


            // get details trading Date from          balsheet 
            var tr = Trans.Where(x => x.TransType == 0);

            var grp = from p in tr
                      where p.Bal > 0
                      group p by p.Acgroup into g
                      select new { GroupName = g.Key, count = g.Count() };
            int X = 1;

            foreach (var item in grp)
            {
                var lst = tr.Where(x => x.Acgroup == item.GroupName && x.Bal > 0).OrderBy(x => x.AccountName);

                cellgrid.Model.Rows.InsertRange(X, 1);
                cellgrid.Model[X, 2].CellValue = item.GroupName;
                cellgrid.Model[X, 2].TextColor = ColorTranslator.FromHtml("#e3165b");
                cellgrid.Model[X, 2].Font.Bold = true;
                X = X + 1;
                foreach (var _lst in lst)
                {
                    cellgrid.Model.Rows.InsertRange(X, 1);
                    cellgrid.Model[X, 2].CellValue = "  " + _lst.AccountName;
                    cellgrid.Model[X, 1].CellValue = _lst.Diff;
                    cellgrid.Model[X, 1].HorizontalAlignment = GridHorizontalAlignment.Right;
                    cellgrid.Model[X, 3].CellType = "CheckBox";
                    cellgrid.Model[X, 3].CellValue = _lst.Audit;
                    cellgrid.Model[X, 7].CellValue = _lst.AcId;

                    X++;
                }

                cellgrid.Model.Rows.InsertRange(X, 1);
                cellgrid.Model[X, 1].HorizontalAlignment = GridHorizontalAlignment.Right;
                cellgrid.Model[X, 1].CellValue = lst.Sum(x => x.Diff);
                cellgrid.Model[X, 1].Font.Bold = true;
                cellgrid.Model[X, 1].Borders.Top = new GridBorder(GridBorderStyle.Solid, Color.IndianRed, GridBorderWeight.Thin);
                X++;
            }
            // income
            var grpdr = from p in tr
                        where p.Bal < 0
                        group p by p.Acgroup into g
                        select new { GroupName = g.Key, count = g.Count() };

            X = 1;

            foreach (var item in grpdr)
            {
                var lst = tr.Where(x => x.Acgroup == item.GroupName && x.Bal < 0).OrderBy(x => x.AccountName);

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

                    cellgrid.Model[X, 5].CellValue = "  " + _lst.AccountName;
                    cellgrid.Model[X, 4].CellValue = _lst.Diff;
                    cellgrid.Model[X, 4].HorizontalAlignment = GridHorizontalAlignment.Right;

                    cellgrid.Model[X, 6].CellType = "CheckBox";
                    cellgrid.Model[X, 6].CellValue = _lst.Audit;
                    cellgrid.Model[X, 8].CellValue = _lst.AcId; // account id for account
                    X++;
                }
                if (X == cellgrid.Model.RowCount)
                    cellgrid.Model.Rows.InsertRange(X, 1);
                cellgrid.Model[X, 4].HorizontalAlignment = GridHorizontalAlignment.Right;
                cellgrid.Model[X, 4].CellValue = lst.Sum(x => x.Diff);
                cellgrid.Model[X, 4].Font.Bold = true;
                cellgrid.Model[X, 4].Borders.Top = new GridBorder(GridBorderStyle.Solid, Color.IndianRed, GridBorderWeight.Thin);
                X++;
            }
            int Y = 0;
            //if (X > cellgrid.Model.RowCount)
            //cellgrid.Model.InsertRows(X, 1);
            ///else
            //{
            //Y = X;
            X = cellgrid.Model.RowCount;
            cellgrid.Model.Rows.InsertRange(X, 2);
            //}
            X++;
            decimal debittotal = tr.Where(x => x.Bal > 0).Sum(x => x.Diff); //expense
            decimal credittotal = tr.Where(x => x.Bal < 0).Sum(x => x.Diff); //icome
            decimal gpPer = 0;
            decimal saletotal = tr.Where(x => x.AcgroupId == 9 && x.Bal < 0).Sum(x => x.Diff);
            if (debittotal > credittotal)
            {
                cellgrid.Model[X, 4].CellValue = debittotal - credittotal;
                cellgrid.Model[X, 4].HorizontalAlignment = GridHorizontalAlignment.Right;
                if(saletotal > 0)
                gpPer = ((debittotal - credittotal) / saletotal) * 100;
                cellgrid.Model[X, 5].CellValue = "Gross Loss : " + gpPer.ToString("F") + " %";
            }
            else
            {
                cellgrid.Model[X, 1].CellValue = credittotal - debittotal;
                var value = credittotal - debittotal;
                if (value == 0)
                {
                    gpPer = 0;
                }
                else
                {
                    if(saletotal > 0)
                    gpPer = ((credittotal - debittotal) / saletotal) * 100;
                }

                cellgrid.Model[X, 1].HorizontalAlignment = GridHorizontalAlignment.Right;
                cellgrid.Model[X, 2].CellValue = "Gross Profit : " + gpPer.ToString("F") + " %";
                cellgrid.Model[X, 1].TextColor = Color.Blue;
                cellgrid.Model[X, 1].Font.Bold = true;
            }
            X++;
            cellgrid.Model.Rows.InsertRange(X, 2);
            if (debittotal > credittotal)
                credittotal = credittotal + debittotal - credittotal;
            else
                debittotal = debittotal + credittotal - debittotal;
            X++;
            cellgrid.Model[X, 1].CellValue = debittotal;
            cellgrid.Model[X, 4].CellValue = credittotal;

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

           // cellgrid.Model.FooterRows = 1;
        }
        void Trading_HF()
        {
            ExecuteProcedure();

           
            cellgrid.Model.Options.ExcelLikeCurrentCell = true;

            
            cellgrid.Model.TableStyle.ReadOnly = true;
           
            cellgrid.Model.Rows.RemoveRange(0, cellgrid.Model.RowCount);
            cellgrid.Model.Cols.RemoveRange(0, cellgrid.Model.ColCount);
            cellgrid.Model.ColCount = 6;
            cellgrid.Model.RowCount = 1;

            cellgrid.Model.Cols.Hidden[6] = true;

            cellgrid.Model.ColWidths[1] = 20;
            cellgrid.Model.ColWidths[2] = 200;
            cellgrid.Model.ColWidths[3] = 100;
            cellgrid.Model.ColWidths[4] = 100;
            cellgrid.Model.ColWidths[5] = 100;

            cellgrid.Model[0, 2].CellValue = "Particulars";
            cellgrid.Model[0, 2].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cellgrid.Model[0, 2].TextColor = Color.White;

            cellgrid.Model[0, 3].CellValue = "Amount as C-FY";
            cellgrid.Model[0, 3].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cellgrid.Model[0, 3].TextColor = Color.White;

            cellgrid.Model[0, 4].CellValue = "Amount as P-FY";
            cellgrid.Model[0, 4].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cellgrid.Model[0, 4].TextColor = Color.White;

            cellgrid.Model[0, 5].CellValue = "Changes in %";
            cellgrid.Model[0, 5].BackColor = ColorTranslator.FromHtml("#16A5DC");
            cellgrid.Model[0, 5].TextColor = Color.White;

            var tr = Trans.Where(x => x.TransType == 0);

            var grp = from p in tr
                      orderby p.Nature descending, p.Acgroup
                      group p by p.Acgroup into g
                      select new { GroupName = g.Key, count = g.Count() };
            int X = 1;

            foreach (var item in grp)
            {
                var lst = tr.Where(x => x.Acgroup == item.GroupName).OrderBy(x => x.AccountName);
                cellgrid.Model.Rows.InsertRange(X, 1);
                cellgrid.Model[X, 2].CellValue = item.GroupName;
                cellgrid.Model[X, 2].TextColor = ColorTranslator.FromHtml("#e3165b");
                cellgrid.Model[X, 2].Font.Bold = true;
                cellgrid.Model.CoveredRanges.Add(GridRangeInfo.Cells(X, 1, X, 2));
                
                
                X = X + 1;
                foreach (var _lst in lst)
                {
                    cellgrid.Model.Rows.InsertRange(X, 1);
                    //audit colum
                    cellgrid.Model[X, 1].CellType = "CheckBox";
                    cellgrid.Model[X, 1].CellValue = _lst.Audit;
                    cellgrid.Model[X, 6].CellValue = _lst.AcId;
                    cellgrid.Model[X, 2].CellValue = "  " + _lst.AccountName;
                    if (_lst.Nature == "TRADING INCOME")
                    {
                        if (_lst.Bal > 0)
                        {
                            cellgrid.Model[X, 3].CellValue = "(-) " + _lst.Diff.ToString();
                            cellgrid.Model[X, 4].CellValue = "(-) " + _lst.PreDiff;
                        }
                        else
                        {
                            cellgrid.Model[X, 3].CellValue = _lst.Diff;
                            cellgrid.Model[X, 4].CellValue = _lst.PreDiff;
                        }
                    }
                    else
                    {
                        if (_lst.Bal > 0)
                        {
                            cellgrid.Model[X, 3].CellValue = _lst.Diff;
                            cellgrid.Model[X, 4].CellValue = _lst.PreDiff;
                        }
                        else
                        {
                            cellgrid.Model[X, 3].CellValue = "(-) " + _lst.Diff.ToString();
                            cellgrid.Model[X, 4].CellValue = "(-) " + _lst.PreDiff;
                        }

                    }
                    cellgrid.Model[X, 3].HorizontalAlignment = GridHorizontalAlignment.Right;
                    cellgrid.Model[X, 4].HorizontalAlignment = GridHorizontalAlignment.Right;
                    X++;
                }
                cellgrid.Model.Rows.InsertRange(X, 1);
                // sum opening balance
                cellgrid.Model[X, 3].HorizontalAlignment = GridHorizontalAlignment.Right;
                cellgrid.Model[X, 3].CellValue = Math.Abs(lst.Sum(x => x.Bal));
                cellgrid.Model[X, 3].Font.Bold = true;
                cellgrid.Model[X, 3].Borders.Top = new GridBorder(GridBorderStyle.Solid, Color.IndianRed, GridBorderWeight.Thin);
                cellgrid.Model[X, 4].HorizontalAlignment = GridHorizontalAlignment.Right;
                cellgrid.Model[X, 4].CellValue = Math.Abs(lst.Sum(x => x.preBal));
                cellgrid.Model[X, 4].Font.Bold = true;
                cellgrid.Model[X, 4].Borders.Top = new GridBorder(GridBorderStyle.Solid, Color.IndianRed, GridBorderWeight.Thin);
                X++;
            }
            cellgrid.Model.Rows.InsertRange(X, 1);

            decimal debittotal = tr.Where(x => x.Bal > 0).Sum(x => x.Diff); //expense
            decimal credittotal = tr.Where(x => x.Bal < 0).Sum(x => x.Diff); //icome
            decimal gpPer = 0;
            decimal saletotal = tr.Where(x => x.AcgroupId == 9 && x.Bal < 0).Sum(x => x.Diff);

            decimal pdebittotal = tr.Where(x => x.preBal > 0).Sum(x => x.PreDiff); //expense
            decimal pcredittotal = tr.Where(x => x.preBal < 0).Sum(x => x.PreDiff); //icome
            decimal pgpPer = 0;
            decimal psaletotal = tr.Where(x => x.AcgroupId == 9 && x.preBal < 0).Sum(x => x.PreDiff);


            if (debittotal > credittotal)
            {
                gpPer = ((debittotal - credittotal) / saletotal) * 100;
                cellgrid.Model[X, 2].CellValue = "Gross Loss : " + gpPer.ToString("F") + " %";
                cellgrid.Model[X, 3].TextColor = Color.Red;
            }
            else
            {
                var value = credittotal - debittotal;
                if (value == 0)
                {
                    gpPer = 0;
                }
                else
                {
                    gpPer = ((credittotal - debittotal) / saletotal) * 100;
                }

                cellgrid.Model[X, 2].CellValue = "Gross Profit : " + gpPer.ToString("F") + " %";
                cellgrid.Model[X, 3].TextColor = Color.Blue;
            }

            cellgrid.Model[X, 3].CellValue = Math.Abs(debittotal - credittotal);
            cellgrid.Model[X, 3].HorizontalAlignment = GridHorizontalAlignment.Right;

            cellgrid.Model[X, 3].Font.Bold = true;


            //previous p FY
            if (psaletotal > 0)
            {
                if (pdebittotal > pcredittotal)
                {
                    pgpPer = ((pdebittotal - pcredittotal) / psaletotal) * 100;
                }
                else
                {
                    pgpPer = ((pcredittotal - pdebittotal) / psaletotal) * 100;
                }
            }
            cellgrid.Model[X, 3].CellValue = Math.Abs(pdebittotal - pcredittotal);
            cellgrid.Model[X, 3].HorizontalAlignment = GridHorizontalAlignment.Right;

            cellgrid.Model[X, 3].TextColor = Color.Blue;
            cellgrid.Model[X, 3].Font.Bold = true;
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

            if (Print == "B" || Print == "D")
            {
                if (fromatLookUpEdit.EditValue.ToString() == "VF")
                {
                    string dr = "Outs\\bl_details_ar_tf.rdlx";
                    string title = "Balance Sheet";
                    Prints(dr, title);
                }
                else
                {
                    string dr = "Outs\\bl_details_ar_tf.rdlx";
                    string title = "Balance Sheet";
                    Prints(dr, title);
                }
            }
            else if (Print == "P")
            {
                string dr = "Outs\\pl_details_ar_tf.rdlx";
                string title = "Profit & Loss";
                Prints(dr, title);
            }
            else
            {
                string dr = "Outs\\trading_details_ar_tf.rdlx";
                string title = "Trading A/c";
                Prints( dr, title);
            }

          

            
        }
        private void Prints( string dr, string title)
        {
            GrapeCity.ActiveReports.PageReport _pageReport = new GrapeCity.ActiveReports.PageReport();
            _pageReport.Load(new System.IO.FileInfo(dr));

            GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(_pageReport);

            _pageReport.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

            if (pnl != 0)
            {
                doc.Parameters["pnl"].CurrentValue = pnl;
            }

            if (gpl != 0)
            {
                doc.Parameters["gpl"].CurrentValue = gpl;
            }

            doc.Parameters["summary"].CurrentValue = "N";
            doc.Parameters["companyid"].CurrentValue = KontoGlobals.CompanyId;
            doc.Parameters["yearid"].CurrentValue = KontoGlobals.YearId;
            doc.Parameters["fromdate"].CurrentValue = Convert.ToInt32(fDateEdit.DateTime.ToString("yyyyMMdd"));
            doc.Parameters["todate"].CurrentValue = Convert.ToInt32(tDateEdit.DateTime.ToString("yyyyMMdd"));

            doc.Parameters["report_title"].CurrentValue = title + " For The Period " + fDateEdit.DateTime.ToString("dd/MM/yyyy") + " To " + tDateEdit.DateTime.ToString("dd/MM/yyyy");


            try
            {
                var _tab = this.Parent.Parent as TabControlAdv;
                if (_tab == null) return;
                var frm = new KontoRepViewer(doc);
                frm.Text = title;

                var pg1 = new TabPageAdv();
                pg1.Text = title;
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
            if (e.KeyCode == Keys.Escape)
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
            //BackgroundWorker bw = new BackgroundWorker();
            //bw.DoWork += bw_DoWork;
            //bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            //bw.RunWorkerAsync();

            var fdate = Convert.ToInt32(fDateEdit.DateTime.ToString("yyyyMMdd"));
            var tdate = Convert.ToInt32(tDateEdit.DateTime.ToString("yyyyMMdd"));
            Print = "D";
            List<BalSumDto> Lst = new List<BalSumDto>();
            using (var db = new KontoContext())
            {
                Lst = db.Database.SqlQuery<BalSumDto>(
                    "dbo.Bal_sheet @CompanyId={0},@FromDate={1},@ToDate={2},@YearId={3},@Summary={4}",
                    Convert.ToInt32(KontoGlobals.CompanyId), fdate, tdate, KontoGlobals.YearId, "Y").ToList();
            }
            var tr = Lst.Where(x => x.BalType == "TR").ToList();
            var pl = Lst.Where(x => x.BalType == "PL").ToList();
            var bl = Lst.Where(x => x.BalType == "BL").ToList();
            var netsale = Lst.FirstOrDefault(x => x.RGroupId == 9);

            cellgrid.Model.RowCount = 0;
            cellgrid.Model.ColCount = 0;

            cellgrid.Model.Options.ExcelLikeCurrentCell = true;

           
            cellgrid.Model.TableStyle.ReadOnly = true;
            
            cellgrid.Model.Rows.RemoveRange(0, cellgrid.Model.RowCount);
            cellgrid.Model.Cols.RemoveRange(0, cellgrid.Model.RowCount);
            cellgrid.Model.RowCount = Lst.Count + 12;
            cellgrid.Model.ColCount = 6;
            //cellgrid.Model.ColWidths.IsHidden[0] = true;
            cellgrid.Model.ColWidths.IsHidden[6] = true;
           // cellgrid.Model.ColWidths.IsHidden[7] = true;

            cellgrid.Model[0, 1].CellValue = "Particulars";
            cellgrid.Model[0, 2].CellValue = "Amount";

            cellgrid.Model[0, 4].CellValue = "Particulars";
            cellgrid.Model[0, 5].CellValue = "Amount";
            
            cellgrid.Model.RowHeights[1] = 20;
            cellgrid.Model.CoveredRanges.Add(GridRangeInfo.Cells(1, 1, 1, 2));

            //cellgrid.CoveredCells.Add(new Syncfusion.Windows.Controls.Cells.CoveredCellInfo(1, 1, 1, 2));
            
            cellgrid.Model[1, 1].HorizontalAlignment = GridHorizontalAlignment.Center;
            cellgrid.Model[1, 1].Font.Size = 14;
            cellgrid.Model[1, 1].Font.Bold = true;
            cellgrid.Model[1, 1].CellValue = "EXPENSES";
            cellgrid.Model[1, 1].BackColor = Color.BlanchedAlmond;

            cellgrid.Model.CoveredRanges.Add(GridRangeInfo.Cells(1, 4, 1, 5));
            //cellgrid.CoveredCells.Add(new Syncfusion.Windows.Controls.Cells.CoveredCellInfo(1, 4, 1, 5));
            cellgrid.Model[1, 4].HorizontalAlignment = GridHorizontalAlignment.Center;
            cellgrid.Model[1, 4].CellValue = "INCOMES";
            cellgrid.Model[1, 4].Font.Size = 14;
            cellgrid.Model[1, 4].Font.Bold = true;
            cellgrid.Model[1, 4].BackColor = Color.BlanchedAlmond;



            
            cellgrid.Model.ColWidths[1] = 400;

            cellgrid.Model.ColWidths[4] = 300;
            cellgrid.Model.ColWidths[2] = 100;
            cellgrid.Model.ColWidths[5] = 200;

            int rowindex = 2;
            //trading
            var ltindex = rowindex;
            var rtindex = rowindex;
            var ltl = tr.Where(k => k.LGroupId != null).ToList();
            foreach (var item in tr)
            {
                cellgrid.Model[ltindex, 2].HorizontalAlignment = GridHorizontalAlignment.Right;

                cellgrid.Model[ltindex, 1].CellValue = item.LGroup;
                cellgrid.Model[ltindex, 2].CellValue = item.LDiff;
                cellgrid.Model[ltindex, 3].CellValue = item.LGroupId;
                cellgrid.Model.Cols.Hidden[3] = true;

                ltindex++;
            }
            var rtl = tr.Where(k => k.RGroupId != null).ToList();
            foreach (var item in rtl)
            {
                cellgrid.Model[rtindex, 5].HorizontalAlignment = GridHorizontalAlignment.Right;

                cellgrid.Model[rtindex, 4].CellValue = item.RGroup;
                cellgrid.Model[rtindex, 5].CellValue = item.RDiff;
                cellgrid.Model[rtindex, 6].CellValue = item.RGroupId;
                cellgrid.Model.Cols.Hidden[6] = true;

                rtindex++;
            }
            ltindex = rowindex;
            if (rtindex > ltindex)
                rowindex = rtindex;


            //profit Loss
            decimal _gross = 0;
            _gross = Convert.ToDecimal(tr.Sum(x => x.RDiff) - tr.Sum(x => x.LDiff));
            decimal? gp = 0;
            decimal? gl = 0;
            if (tr.Sum(x => x.LDiff) - tr.Sum(x => x.RDiff) > 0) //losee
            {
                decimal gpper = 0;
                if (netsale!=null && netsale.RDiff != 0)
                    gpper = Convert.ToDecimal(((tr.Sum(x => x.LDiff) - tr.Sum(x => x.RDiff)) / netsale.RDiff) * 100);

                cellgrid.Model[rowindex + 1, 4].CellValue = "Gross Loss : " + gpper.ToString("F") + "%";

                cellgrid.Model[rowindex + 1, 5].CellValue = tr.Sum(x => x.LDiff) - tr.Sum(x => x.RDiff);

                cellgrid.Model[rowindex + 1, 4].TextColor = Color.Red;
                cellgrid.Model[rowindex + 1, 5].TextColor = Color.Red;
                cellgrid.Model[rowindex + 1, 4].Font.Bold = true;


                //gp setting in pl
                cellgrid.Model[rowindex + 3, 1].CellValue = "Gross Loss from Trading A/c";
                cellgrid.Model[rowindex + 3, 1].TextColor = Color.Red;
                cellgrid.Model[rowindex + 3, 2].CellValue = tr.Sum(x => x.LDiff) - tr.Sum(x => x.RDiff);
                cellgrid.Model[rowindex + 3, 2].TextColor = Color.Red;
                cellgrid.Model[rowindex + 3, 2].HorizontalAlignment = GridHorizontalAlignment.Right;

                pnl = Convert.ToDecimal( tr.Sum(x => x.LDiff) - tr.Sum(x => x.RDiff));
                gp = pnl;
            }
            else //gross profit
            {
                decimal gpper = 0;
                if (tr.Sum(x => x.RDiff) > 0)
                    gpper = Convert.ToDecimal(((tr.Sum(x => x.RDiff) - tr.Sum(x => x.LDiff)) / tr.Sum(x => x.RDiff)) * 100);

                cellgrid.Model[rowindex + 1, 1].TextColor = Color.Blue;
                cellgrid.Model[rowindex + 1, 1].CellValue = "Gross Profit : " + gpper.ToString("F") + "%";

                cellgrid.Model[rowindex + 1, 2].CellValue = tr.Sum(x => x.RDiff) - tr.Sum(x => x.LDiff);
                cellgrid.Model[rowindex + 1, 2].TextColor = Color.Blue;
                cellgrid.Model[rowindex + 1, 2].Font.Bold = true;

                //gp in pl
                cellgrid.Model[rowindex + 3, 4].TextColor = Color.Blue;
                cellgrid.Model[rowindex + 3, 5].TextColor = Color.Blue;
                cellgrid.Model[rowindex + 3, 5].HorizontalAlignment = GridHorizontalAlignment.Right;
                cellgrid.Model[rowindex + 3, 4].CellValue = "Gross Profit From Trading A/c";
                cellgrid.Model[rowindex + 3, 5].CellValue = tr.Sum(x => x.RDiff) - tr.Sum(x => x.LDiff);

                pnl = Convert.ToDecimal( tr.Sum(x => x.RDiff) - tr.Sum(x => x.LDiff));
                gl = pnl;
                pnl = -1 * pnl;

            }

            cellgrid.Model[rowindex + 1, 2].HorizontalAlignment = GridHorizontalAlignment.Right;
            cellgrid.Model[rowindex + 1, 5].HorizontalAlignment = GridHorizontalAlignment.Right;


            cellgrid.Model[rowindex + 2, 2].HorizontalAlignment = GridHorizontalAlignment.Right;
            cellgrid.Model[rowindex + 2, 5].HorizontalAlignment = GridHorizontalAlignment.Right;
            cellgrid.Model[rowindex + 2, 2].Font.Bold = true;
            cellgrid.Model[rowindex + 2, 5].Font.Bold = true;

            cellgrid.Model[rowindex + 2, 1].CellValue = "Total";
            cellgrid.Model[rowindex + 2, 2].CellType = "FormulaCell";
            cellgrid.Model[rowindex + 2, 2].CellValue = tr.Sum(k => k.LDiff) + gl;


            cellgrid.Model[rowindex + 2, 4].CellValue = "Total";
            cellgrid.Model[rowindex + 2, 5].CellType = "FormulaCell";
            cellgrid.Model[rowindex + 2, 5].CellValue = tr.Sum(k => k.RDiff) + gp;

            for (int i = 1; i < cellgrid.Model.ColCount - 1; i++)
            {
                cellgrid.Model[rowindex + 2, i].Borders.Bottom = new GridBorder(GridBorderStyle.Solid, Color.RoyalBlue, GridBorderWeight.Thin);
                cellgrid.Model[rowindex + 2, i].Borders.Top = new GridBorder(GridBorderStyle.Solid, Color.RoyalBlue, GridBorderWeight.Thin);
                cellgrid.Model[rowindex + 2, i].Borders.Right = new GridBorder(GridBorderStyle.Solid, Color.RoyalBlue, GridBorderWeight.Thin);
                cellgrid.Model[rowindex + 2, i].Borders.Left = new GridBorder(GridBorderStyle.Solid, Color.RoyalBlue, GridBorderWeight.Thin);

            }

            //profit or loss account


            rowindex = rowindex + 4;
            var lpindex = rowindex;
            var rpindex = rowindex;
            var lpl = pl.Where(k => k.LGroupId != null).ToList();
            foreach (var item in lpl) // profit or loss
            {
                cellgrid.Model[lpindex, 2].HorizontalAlignment = GridHorizontalAlignment.Right;

                cellgrid.Model[lpindex, 1].CellValue = item.LGroup;
                cellgrid.Model[lpindex, 2].CellValue = item.LDiff;
                cellgrid.Model[lpindex, 3].CellValue = item.LGroupId;
                cellgrid.Model.Cols.Hidden[3]=true;
                lpindex++;
            }
            var rpl = pl.Where(k => k.RGroupId != null).ToList();
            foreach (var item in rpl)
            {
                cellgrid.Model[rpindex, 5].HorizontalAlignment = GridHorizontalAlignment.Right;

                cellgrid.Model[rpindex, 4].CellValue = item.RGroup;
                cellgrid.Model[rpindex, 5].CellValue = item.RDiff;
                cellgrid.Model[rpindex, 6].CellValue = item.RGroupId;
                cellgrid.Model.Cols.Hidden[6] = true;
                rpindex++;
            }
            rowindex = lpindex;
            if (rpindex > lpindex)
                rowindex = rpindex;

            //np
            decimal? np = 0;
            decimal? nl = 0;
            if (pl.Sum(x => x.RDiff) + _gross - pl.Sum(x => x.LDiff) < 0) //net loss
            {
                decimal npper = 0;
                if (netsale!=null && netsale.RDiff > 0)
                    npper = Convert.ToDecimal((pl.Sum(x => x.LDiff) - pl.Sum(x => x.RDiff) - _gross) / netsale.RDiff) * 100;
                nl = pl.Sum(x => x.LDiff) - pl.Sum(x => x.RDiff) - _gross;
                cellgrid.Model[rowindex + 1, 4].CellValue = "Net Loss : " + npper.ToString("F") + "%";
                cellgrid.Model[rowindex + 1, 5].CellValue = pl.Sum(x => x.LDiff) - pl.Sum(x => x.RDiff) - _gross;
                cellgrid.Model[rowindex + 1, 4].TextColor = Color.Red;
                cellgrid.Model[rowindex + 1, 5].TextColor = Color.Red;
                cellgrid.Model[rowindex + 1, 5].Font.Bold = true;
                gpl = Convert.ToDecimal( pl.Sum(x => x.RDiff) - _gross - pl.Sum(x => x.LDiff));
                gpl = -1 * gpl;
            }
            else //net profit
            {
                decimal npper = 0;
                if (netsale != null)
                {
                    if (netsale.RDiff > 0)
                        npper = Convert.ToDecimal((pl.Sum(x => x.RDiff) - pl.Sum(x => x.LDiff) + _gross) / netsale.RDiff) * 100;
                    np = pl.Sum(x => x.RDiff) - pl.Sum(x => x.LDiff) + _gross;
                }

                cellgrid.Model[rowindex + 1, 1].CellValue = "Net Profit : " + npper.ToString("F") + "%";
                cellgrid.Model[rowindex + 1, 2].CellValue = pl.Sum(x => x.RDiff) - pl.Sum(x => x.LDiff) + _gross;
                cellgrid.Model[rowindex + 1, 1].TextColor = Color.Blue;
                cellgrid.Model[rowindex + 1, 2].TextColor = Color.Blue;
                cellgrid.Model[rowindex + 1, 2].Font.Bold = true;
                gpl = Convert.ToDecimal(pl.Sum(x => x.RDiff) + _gross - pl.Sum(x => x.LDiff));
            }

            cellgrid.Model[rowindex + 1, 2].HorizontalAlignment = GridHorizontalAlignment.Right;
            cellgrid.Model[rowindex + 1, 5].HorizontalAlignment = GridHorizontalAlignment.Right;


            cellgrid.Model[rowindex + 2, 2].HorizontalAlignment = GridHorizontalAlignment.Right;
            cellgrid.Model[rowindex + 2, 5].HorizontalAlignment = GridHorizontalAlignment.Right;
            cellgrid.Model[rowindex + 2, 2].Font.Bold = true;
            cellgrid.Model[rowindex + 2, 5].Font.Bold = true;

            cellgrid.Model[rowindex + 2, 1].CellValue = "Total";
            cellgrid.Model[rowindex + 2, 2].CellType = "FormulaCell";
            cellgrid.Model[rowindex + 2, 2].CellValue = pl.Sum(k => k.LDiff) + np + gp;


            cellgrid.Model[rowindex + 2, 4].CellValue = "Total";
            cellgrid.Model[rowindex + 2, 5].CellType = "FormulaCell";
            cellgrid.Model[rowindex + 2, 5].CellValue = pl.Sum(k => k.RDiff) + nl + gl;

            for (int i = 1; i < cellgrid.Model.ColCount - 1; i++)
            {
                cellgrid.Model[rowindex + 2, i].Borders.Bottom = new GridBorder(GridBorderStyle.Solid, Color.RoyalBlue, GridBorderWeight.Thin);
                cellgrid.Model[rowindex + 2, i].Borders.Top = new GridBorder(GridBorderStyle.Solid, Color.RoyalBlue, GridBorderWeight.Thin);
                cellgrid.Model[rowindex + 2, i].Borders.Right = new GridBorder(GridBorderStyle.Solid, Color.RoyalBlue, GridBorderWeight.Thin);
                cellgrid.Model[rowindex + 2, i].Borders.Left = new GridBorder(GridBorderStyle.Solid, Color.RoyalBlue, GridBorderWeight.Thin);

            }

            // bal sheet
            rowindex = rowindex + 3;
            cellgrid.Model.RowHeights[1] = 20;
            cellgrid.Model.CoveredRanges.Add(GridRangeInfo.Cells(rowindex, 1, rowindex, 2));
            //cellgrid.CoveredCells.Add(new Syncfusion.Windows.Controls.Cells.CoveredCellInfo(rowindex, 1, rowindex, 2));
            cellgrid.Model[rowindex, 1].HorizontalAlignment = GridHorizontalAlignment.Center;
            cellgrid.Model[rowindex, 1].Font.Size = 14;
            cellgrid.Model[rowindex, 1].Font.Bold = true;
            cellgrid.Model[rowindex, 1].CellValue = "LIABILITES";
            cellgrid.Model[rowindex, 1].BackColor = Color.LightSalmon;

            cellgrid.Model.CoveredRanges.Add(GridRangeInfo.Cells(rowindex, 4, rowindex, 5));
            //cellgrid.CoveredCells.Add(new Syncfusion.Windows.Controls.Cells.CoveredCellInfo(rowindex, 4, rowindex, 5));

            cellgrid.Model[rowindex, 4].HorizontalAlignment = GridHorizontalAlignment.Center;
            cellgrid.Model[rowindex, 4].CellValue = "ASSETS";
            cellgrid.Model[rowindex, 4].Font.Size = 14;
            cellgrid.Model[rowindex, 4].Font.Bold = true;
            cellgrid.Model[rowindex, 4].BackColor = Color.LightSalmon;

            rowindex = rowindex + 1;

            var lindex = rowindex;
            var rindex = rowindex;
            var lbl = bl.Where(k => k.LGroupId != null).ToList();
            foreach (var item in lbl)
            {
                cellgrid.Model[lindex, 2].HorizontalAlignment = GridHorizontalAlignment.Right;
                cellgrid.Model[lindex, 1].CellValue = item.LGroup;
                cellgrid.Model[lindex, 2].CellValue = item.LDiff;
                cellgrid.Model[lindex, 3].CellValue = item.LGroupId;
                cellgrid.Model.Cols.Hidden[3]=true;
                lindex++;
            }

            var rbl = bl.Where(k => k.RGroupId != null).ToList();
            foreach (var item in rbl) // balancesheet
            {

                cellgrid.Model[rindex, 5].HorizontalAlignment = GridHorizontalAlignment.Right;
                cellgrid.Model[rindex, 4].CellValue = item.RGroup;
                cellgrid.Model[rindex, 5].CellValue = item.RDiff;
                cellgrid.Model[rindex, 6].CellValue = item.RGroupId;
                cellgrid.Model.Cols.Hidden[6] = true;
                rindex++;
            }
            rowindex = lindex;
            if (rindex > lindex)
            {
                rowindex = rindex;
            }

            cellgrid.Model[rowindex + 1, 2].HorizontalAlignment = GridHorizontalAlignment.Right;
            cellgrid.Model[rowindex + 1, 5].HorizontalAlignment = GridHorizontalAlignment.Right;


            cellgrid.Model[rowindex + 1, 2].Font.Bold = true;
            cellgrid.Model[rowindex + 1, 5].Font.Bold = true;

            cellgrid.Model[rowindex + 1, 1].CellValue = "Total";
            cellgrid.Model[rowindex + 1, 2].CellType = "FormulaCell";
            cellgrid.Model[rowindex + 1, 2].CellValue = bl.Sum(k => k.LDiff);


            cellgrid.Model[rowindex + 1, 4].CellValue = "Total";
            cellgrid.Model[rowindex + 1, 5].CellType = "FormulaCell";
            cellgrid.Model[rowindex + 1, 5].CellValue = bl.Sum(k => k.RDiff);

            for (int i = 1; i < cellgrid.Model.ColCount - 1; i++)
            {
                cellgrid.Model[rowindex + 1, i].Borders.Bottom = new GridBorder(GridBorderStyle.Solid, Color.RoyalBlue, GridBorderWeight.Thin);
                cellgrid.Model[rowindex + 1, i].Borders.Top = new GridBorder(GridBorderStyle.Solid, Color.RoyalBlue, GridBorderWeight.Thin);
                cellgrid.Model[rowindex + 1, i].Borders.Right = new GridBorder(GridBorderStyle.Solid, Color.RoyalBlue, GridBorderWeight.Thin);
                cellgrid.Model[rowindex + 1, i].Borders.Left = new GridBorder(GridBorderStyle.Solid, Color.RoyalBlue, GridBorderWeight.Thin);

            }
            //cellgrid.Model.FooterRows = 1;

            try
            {
                var accId = Convert.ToInt32(ConfigurationManager.AppSettings["PlId"]);
                using (var _db = new KontoContext())
                {
                    var ledger = _db.Ledgers.Where(k => k.AccountId == accId && k.RefAccountId == null && k.YearId == KontoGlobals.YearId && k.CompanyId == KontoGlobals.CompanyId && k.IsActive && k.IsDeleted == false).ToList();
                    if (ledger.Count > 0)
                    {
                        _db.Ledgers.RemoveRange(ledger);
                    }
                    _db.SaveChanges();
                    decimal amount = (decimal)nl;
                    decimal billamt = (decimal)nl;
                    if (np > 0)
                    {
                        amount = -1 * (decimal)np;
                        billamt = (decimal)np;
                    }
                    var date = _db.FinYears.FirstOrDefault(k => k.Id == KontoGlobals.YearId);

                    LedgerTransModel ld = new LedgerTransModel();
                    {
                        ld.CompanyId = KontoGlobals.CompanyId;
                        ld.YearId = KontoGlobals.YearId;
                        ld.VoucherDate = date.ToDate;
                        ld.TransDate = date.TDate;
                        ld.VoucherNo = "None";
                        ld.BillNo = "None";
                        ld.AccountId = accId;
                        ld.Debit = (decimal)nl;
                        ld.Credit = (decimal)np;
                        ld.Amount = amount;
                        ld.BilllAmount = billamt;
                        ld.IsActive = true;
                        ld.IsDeleted = false;
                        ld.CreateDate = DateTime.Now;
                        ld.CreateUser = KontoGlobals.UserName;

                    }
                    _db.Ledgers.Add(ld);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            cellgrid.Focus();
            //if (r == 1)
            //{
            //    r += 1;
            //    DetailView(obj as Syncfusion.Windows.Controls.Grid.GridControl);

            //}

        }

        private void TrailMainView_Load(object sender, EventArgs e)
        {
            fromatLookUpEdit.EditValue = "VF";
           
           
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
                string _groupname = "";
                if (Print == "D")
                {
                    if (cellgrid.CurrentCell.ColIndex <= 2)
                    {
                        if (!string.IsNullOrEmpty(cellgrid.Model[cellgrid.CurrentCell.RowIndex, 3].CellValue.ToString()))
                        {
                            id = Convert.ToInt32(cellgrid.Model[cellgrid.CurrentCell.RowIndex, 3].CellValue);
                            _groupname = cellgrid.Model[cellgrid.CurrentCell.RowIndex, 1].CellValue.ToString();
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(cellgrid.Model[cellgrid.CurrentCell.RowIndex, 6].CellValue.ToString()))
                        {
                            id = Convert.ToInt32(cellgrid.Model[cellgrid.CurrentCell.RowIndex, 6].CellValue);
                            _groupname = cellgrid.Model[cellgrid.CurrentCell.RowIndex, 4].CellValue.ToString();
                        }
                    }
                    if (id >= 0)
                    {
                        var frmt = new TrialMainView();
                        frmt.AcGroupId = id;
                        frmt._fromDate = this.fDateEdit.DateTime;
                        frmt._Todate = this.tDateEdit.DateTime;
                        frmt.Text = _groupname;
                        frmt.ShowDialog();
                        
                    }
                    return;
                }
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
                frm._fromDate = this.fDateEdit.DateTime;
                frm._toDate = this.tDateEdit.DateTime;
                frm.AccId = id;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
