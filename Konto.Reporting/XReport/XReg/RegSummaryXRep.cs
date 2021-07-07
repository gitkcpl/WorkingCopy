using DevExpress.XtraReports.UI;
using System;


namespace Konto.Reporting.XReport.XReg
{
    public partial class RegSummaryXRep : DevExpress.XtraReports.UI.XtraReport
    {
        private decimal tcsGp1, tcsGp2, tcsTotal, tdsGp1, tdsGp2, tdsTotal;
        public RegSummaryXRep()
        {
            InitializeComponent();
           
        }

       

      

        private void ChallanXRep_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int[] parcols = (int[]) this.Parameters["report_cols"].Value;

           
           
            //header

            foreach (XRTableCell cls in headXrTableRow.Cells)
            {
                if (cls.Index == 0) continue;
                cls.Visible = false;
            }

            //group 1
            foreach (XRTableCell cls in gp1TableRow.Cells)
            {
                if (cls.Index == 0) continue;
                cls.Visible = false;
            }
            //group 2
            foreach (XRTableCell cls in gp2TableRow.Cells)
            {
                if (cls.Index == 0) continue;
                cls.Visible = false;
            }
            // total
            foreach (XRTableCell cls in totalTableRow.Cells)
            {
                if (cls.Index == 0) continue;
                cls.Visible = false;
            }

            foreach (int column in parcols)
            {
                
                
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

       
       

       
    }
}
