using DevExpress.XtraReports.UI;
using System;


namespace Konto.Reporting.XReport.XReg
{
    public partial class RegXRep : DevExpress.XtraReports.UI.XtraReport
    {
        private decimal tcsGp1, tcsGp2, tcsTotal, tdsGp1, tdsGp2, tdsTotal;
        public RegXRep()
        {
            InitializeComponent();
            GroupFooter3.BeforePrint += GroupFooter3_BeforePrint;
            ReportFooter.BeforePrint += ReportFooter_BeforePrint;
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            billAmtxrTableCell.Text = BillAmt.ToString("F");
            tcsTotalXrTableCell.Text = tcsTotal.ToString("F");
            tdsTotalxrTableCell.Text = tdsTotal.ToString("F");
        }

        private void GroupFooter3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            billAmtGp2xrTableCell.Text = BillAmtGp2.ToString("F");
            BillAmtGp2 = 0;

            tcsGp2XrTableCell.Text = tcsGp2.ToString("F");
            tdsGp2xrTableCell.Text = tdsGp2.ToString("F");
            
            tcsGp2 = 0;
            tdsGp2 = 0;

        }

        private void ChallanXRep_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int[] parcols = (int[]) this.Parameters["report_cols"].Value;

            //details
            foreach (XRTableCell cls in dataXrTableRow.Cells)
            {
                cls.Visible = false;
                
            }
            //header

            foreach (XRTableCell cls in headXrTableRow.Cells)
            {
                cls.Visible = false;
            }

            //group 1
            foreach (XRTableCell cls in gp1TableRow.Cells)
            {
                cls.Visible = false;
            }
            //group 2
            foreach (XRTableCell cls in gp2TableRow.Cells)
            {
                cls.Visible = false;
            }
            // total
            foreach (XRTableCell cls in totalTableRow.Cells)
            {
                cls.Visible = false;
            }

            foreach (int column in parcols)
            {
                // detail data
                var cels = dataXrTableRow.Cells[column];
                cels.Visible = true;
                
                // header
                var hcels = headXrTableRow.Cells[column];
                hcels.Visible = true;

                //group 1
                var gcels = gp1TableRow.Cells[column];
                gcels.Visible = true;

                //group 2
                var g2cels = gp2TableRow.Cells[column];
                g2cels.Visible = true;

                // total
                var tcels = totalTableRow.Cells[column];
                tcels.Visible = true;
            }
        }

        private decimal BillAmtGp1=0,BillAmtGp2 = 0,BillAmt=0;
        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
            BillAmtGp1 = BillAmtGp1 + Convert.ToDecimal(this.GetCurrentColumnValue("BillAmount"));
            BillAmtGp2 = BillAmtGp2 + Convert.ToDecimal(this.GetCurrentColumnValue("BillAmount"));
            BillAmt = BillAmt + Convert.ToDecimal(this.GetCurrentColumnValue("BillAmount"));

            tcsGp1 = tcsGp1 + Convert.ToDecimal(this.GetCurrentColumnValue("TcsAmt"));
            tcsGp2 = tcsGp2 + Convert.ToDecimal(this.GetCurrentColumnValue("TcsAmt"));
            tcsTotal = tcsTotal + Convert.ToDecimal(this.GetCurrentColumnValue("TcsAmt"));

            tdsGp1 = tdsGp1 + Convert.ToDecimal(this.GetCurrentColumnValue("TdsAmt"));
            tdsGp2 = tdsGp2 + Convert.ToDecimal(this.GetCurrentColumnValue("TdsAmt"));
            tdsTotal = tdsTotal + Convert.ToDecimal(this.GetCurrentColumnValue("TdsAmt"));
        }

        private void GroupFooter2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (GroupFooter2.Visible)
                billAmtGp1XrTableCell.Text = BillAmtGp1.ToString("F");
            BillAmtGp1 = 0;

            tcsGp1XrTableCell.Text = tcsGp1.ToString("F");
            tdsGp1xrTableCell.Text = tdsGp1.ToString("F");
            tcsGp1 = 0;
            tdsGp1 = 0;
        }
    }
}
