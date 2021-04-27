using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.XtraReports.UI;
using Konto.App.Shared;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using  System.Linq;


namespace Konto.Reporting.XReport.XChallan
{
    public partial class ChallanXRep : DevExpress.XtraReports.UI.XtraReport
    {
        public ChallanXRep()
        {
            InitializeComponent();
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
    }
}
