using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Reports;
using Serilog;
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

namespace Konto.Reporting.Para.Gst
{
    public partial class PaymentAssist : KontoForm
    {
        public PaymentAssist()
        {
            InitializeComponent();
            this.getSimpleButton.Click += GetSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            exportSimpleButton.Click += ExportSimpleButton_Click;
            this.FormClosed += PaymentAssist_FormClosed;

            this.FirstActiveControl = fromDateEdit;
        }

        private void PaymentAssist_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage != null)
            {
                var tabcontrol = tabpage.Parent as TabControlAdv;
                if (tabcontrol != null)
                    tabcontrol.TabPages.Remove(tabpage);
            }
        }

        private void ExportSimpleButton_Click(object sender, EventArgs e)
        {
            Syncfusion.GridExcelConverter.GridExcelConverterControl excelConverter = new Syncfusion.GridExcelConverter.GridExcelConverterControl();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Files(*.xlsx)|*.xlsx";
            saveFileDialog.DefaultExt = ".xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                excelConverter.GridToExcel(this.cellGrid.Model, saveFileDialog.FileName,Syncfusion.GridExcelConverter.ConverterOptions.ColumnHeaders);
                if (MessageBox.Show("Do you wish to open the xls file now?", "Export to Excel", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Process proc = new Process();
                    proc.StartInfo.FileName = saveFileDialog.FileName;
                    proc.Start();
                }
            }
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void GetSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                var fdate = Convert.ToInt32(fromDateEdit.DateTime.ToString("yyyyMMdd"));
                var tdate = Convert.ToInt32(toDateEdit.DateTime.ToString("yyyyMMdd"));
                var Trans = new List<TrialDto>();
                using (var db = new KontoContext())
                {
                    db.Database.CommandTimeout = 0;
                     Trans = db.Database.SqlQuery<TrialDto>(
                        "dbo.TrialBalanceReport @CompanyId={0},@FromDate={1},@ToDate={2},@inclop={3}," +
                        "@showall={4},@closingtb={5},@year={6},@groupid={7}",
                        Convert.ToInt32(KontoGlobals.CompanyId), fdate, tdate, "Y", "Y", 
                        "Y", KontoGlobals.YearId, 19).ToList();

                }

                //get output tax
                var igstOutput = Trans.FirstOrDefault(x => x.AcId == 18);
                if (igstOutput != null)
                    cellGrid[1, 2].CellValue = igstOutput.ClCredit - igstOutput.ClDebit;
                else
                    cellGrid[1, 2].CellValue = 0;

                var cgstOutput = Trans.FirstOrDefault(x => x.AcId == 17);
                if (cgstOutput != null)
                    cellGrid[1, 3].CellValue = cgstOutput.ClCredit - cgstOutput.ClDebit;
                else
                    cellGrid[1, 3].CellValue = 0;

                var sgstOutput = Trans.FirstOrDefault(x => x.AcId == 16);
                if (sgstOutput != null)
                    cellGrid[1, 4].CellValue = sgstOutput.ClCredit - sgstOutput.ClDebit;
                else
                    cellGrid[1, 4].CellValue = 0;

                var cessOutput = Trans.FirstOrDefault(x => x.AcId == 19);
                if (cessOutput != null)
                    cellGrid[1, 5].CellValue = cessOutput.ClCredit - cessOutput.ClDebit;
                else
                    cellGrid[1, 5].CellValue = 0;

                // RCM CGST PAYBLE
                var rmcCgstOutput = Trans.FirstOrDefault(x => x.AcId == 25);
                if (rmcCgstOutput != null)
                {
                    cellGrid[2, 3].CellValue = rmcCgstOutput.ClCredit - rmcCgstOutput.ClDebit;
                    cellGrid[19, 3].CellValue = cellGrid[2, 3].CellValue;
                }
                else
                {
                    cellGrid[2, 3].CellValue = 0;
                    cellGrid[19, 3].CellValue = 0;
                }

                // RCM SGST PAYBLE
                var rmcSgstOutput = Trans.FirstOrDefault(x => x.AcId == 24);
                if (rmcSgstOutput != null)
                {
                    cellGrid[2, 4].CellValue = rmcSgstOutput.ClCredit - rmcSgstOutput.ClDebit;
                    cellGrid[19, 4].CellValue = cellGrid[2, 4].CellValue;
                }
                else
                {
                    cellGrid[2, 4].CellValue = 0;
                    cellGrid[19, 4].CellValue = 0;
                }

                //input tax credit
                var igstInput = Trans.FirstOrDefault(x => x.AcId == 14);
                if (igstInput != null)
                    cellGrid[3, 2].CellValue = igstInput.Debit - igstInput.Credit;
                else
                    cellGrid[3, 2].CellValue = 0;

                var cgstInput = Trans.FirstOrDefault(x => x.AcId == 13);
                if (cgstInput != null)
                    cellGrid[3, 3].CellValue = cgstInput.Debit - cgstInput.Credit;
                else
                    cellGrid[3, 3].CellValue = 0;

                var sgstInput = Trans.FirstOrDefault(x => x.AcId == 12);
                if (sgstInput != null)
                    cellGrid[3, 4].CellValue = sgstInput.Debit - sgstInput.Credit;
                else
                    cellGrid[3, 4].CellValue = 0;

                var cessInput = Trans.FirstOrDefault(x => x.AcId == 15);
                if (cessInput != null)
                    cellGrid[3, 5].CellValue = cessInput.Debit - cessInput.Credit;
                else
                    cellGrid[3, 5].CellValue = 0;

                //tax payble excluding reverse charge

                cellGrid[5, 2].CellValue = cellGrid[1, 2].CellValue;
                cellGrid[5, 3].CellValue = cellGrid[1, 3].CellValue;
                cellGrid[5, 4].CellValue = cellGrid[1, 4].CellValue;
                cellGrid[5, 5].CellValue = cellGrid[1, 5].CellValue;

                //igst used for igts
                var _IgstTotal = Convert.ToDecimal(cellGrid[3, 2].CellValue);
                if (Convert.ToDecimal(cellGrid[1, 2].CellValue) >= _IgstTotal)
                {
                    cellGrid[7, 2].CellValue = _IgstTotal; // Used Igst Credit
                    _IgstTotal = 0;
                }
                else
                {
                    cellGrid[7, 2].CellValue = cellGrid[1, 2].CellValue; // used IGST credit
                    _IgstTotal = _IgstTotal - Convert.ToDecimal(cellGrid[1, 2].CellValue); // balance credit
                }

                // used IGST For CGST
                if(_IgstTotal > 0)
                {
                    if (Convert.ToDecimal(cellGrid[1, 3].CellValue) >= _IgstTotal)
                    {
                        cellGrid[8, 3].CellValue = _IgstTotal;
                        _IgstTotal = 0;
                    }
                    else
                    {
                        cellGrid[8, 3].CellValue = cellGrid[1, 3].CellValue; // used IGST credit
                        _IgstTotal = _IgstTotal - Convert.ToDecimal(cellGrid[1, 3].CellValue);
                    }

                }
                else
                {
                    cellGrid[8, 3].CellValue = 0;
                }

                // used IGST For SGST
                if (_IgstTotal > 0)
                {
                    if (Convert.ToDecimal(cellGrid[1, 4].CellValue) >= _IgstTotal)
                    {
                        cellGrid[9, 4].CellValue = _IgstTotal;
                        _IgstTotal = 0;
                    }
                    else
                    {
                        cellGrid[9, 4].CellValue = cellGrid[1, 4].CellValue; // used IGST credit
                        _IgstTotal = _IgstTotal - Convert.ToDecimal(cellGrid[1, 4].CellValue);
                    }

                }
                else
                {
                    cellGrid[9, 4].CellValue = 0;
                }


                cellGrid[12, 2].CellValue = _IgstTotal;

                // Set Off from input Tax Credit
                //CGST FOR CGST
                //cellGrid.Refresh();
                var _cgstTotal = Convert.ToDecimal(cellGrid.Model[12, 3].FormattedText);

                if(Convert.ToDecimal(cellGrid[11, 3].FormattedText) >= _cgstTotal)
                {
                   cellGrid[14, 3].CellValue = _cgstTotal;
                    _cgstTotal = 0;
                }
                else
                {
                    cellGrid[14, 3].CellValue = cellGrid[11, 3].FormattedText;
                    _cgstTotal = _cgstTotal - Convert.ToDecimal(cellGrid[11, 3].FormattedText);
                }

                //15 - CGST FOR ISGT
                if(Convert.ToDecimal(cellGrid[11,2].FormattedText) > 0 && _cgstTotal > 0)
                {
                    if (Convert.ToDecimal(cellGrid[11, 2].FormattedText) >= _cgstTotal)
                    {
                        cellGrid[15, 2].CellValue = _cgstTotal;
                        _cgstTotal = 0;
                    }
                    else
                    {
                        cellGrid[15, 2].CellValue = cellGrid[11, 2].FormattedText;
                        _cgstTotal = _cgstTotal - Convert.ToDecimal(cellGrid[11, 2].FormattedText);
                    }
                }
                else
                {
                    cellGrid[15, 2].CellValue = 0;
                }

                //16 SGST FOR SGST
                var _sgstTotal = Convert.ToDecimal(cellGrid[12, 4].FormattedText.ZeroIfEmpty());

                if (Convert.ToDecimal(cellGrid[11, 4].FormattedText) > 0 && _sgstTotal > 0)
                {
                    if (Convert.ToDecimal(cellGrid[11, 4].FormattedText) >= _sgstTotal)
                    {
                        cellGrid[16, 4].CellValue = _sgstTotal;
                        _sgstTotal = 0;
                    }
                    else
                    {
                        cellGrid[16, 4].CellValue = cellGrid[11, 4].FormattedText;
                        _sgstTotal = _sgstTotal - Convert.ToDecimal(cellGrid[11, 4].FormattedText);
                    }
                }
                else
                {
                    cellGrid[16, 4].CellValue = 0;
                }

                //17 SGST FOr IGST
                var _balIgst = Convert.ToDecimal(cellGrid[11, 2].FormattedText) - Convert.ToDecimal(cellGrid[15, 2].CellValue.ToString().ZeroIfEmpty());
                if (_balIgst > 0)
                {
                    if(_balIgst > _sgstTotal)
                    {
                        cellGrid[17, 2].CellValue = _sgstTotal;
                        _sgstTotal = 0;
                    }
                    else
                    {
                        cellGrid[17, 2].CellValue = _balIgst;
                        _sgstTotal = _sgstTotal - _balIgst;
                    }
                }
                else
                {
                    cellGrid[17, 2].CellValue = 0;
                }


                cellGrid[21, 2].CellValue = _IgstTotal;
                cellGrid[21, 3].CellValue = _cgstTotal;
                cellGrid[21, 4].CellValue = _sgstTotal;

                cellGrid.Focus();
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Gst Payment");
                MessageBox.Show(ex.ToString());
            }
        }

        private void PaymentAssist_Load(object sender, EventArgs e)
        {
            fromDateEdit.EditValue = DateTime.Now;
            toDateEdit.EditValue = DateTime.Now;
            this.ActiveControl = fromDateEdit;
            this.cellGrid.ActivateCurrentCellBehavior = GridCellActivateAction.DblClickOnCell;
            GridFormulaCellRenderer.ForceEditWhenActivated = false;
        }

        private void cellGrid_CellClick(object sender, Syncfusion.Windows.Forms.Grid.GridCellClickEventArgs e)
        {

        }
    }
}
