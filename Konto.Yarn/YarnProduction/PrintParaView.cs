using GrapeCity.ActiveReports;
using Konto.App.Shared;
using Konto.App.Shared.Para;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Transaction.Dtos;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Yarn.YarnProduction
{
    public partial class PrintParaView : KontoForm
    {
        public VoucherTypeEnum VoucherType { get; set; }  
        public string FromVoucherNo { get; set; }
        public string ToVoucherNo { get; set; } 
        public int VoucherId { get; set; }  
      
        public PrintParaView()
        {
            InitializeComponent();
            this.Load += DocPrintParaView_Load;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click; 
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
         
        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            try
            { 
                this.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue); 
                this.FromVoucherNo = fromTextEdit.Text.Trim();
                this.ToVoucherNo = toTextEdit.Text.Trim();   
                  
                PageReport rpt = new PageReport();
                rpt.Load(new FileInfo("reg\\Doc\\PackingSlip.rdlx"));
                rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;
                
                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);
                 
                doc.Parameters["FrmBill"].CurrentValue = FromVoucherNo;
                doc.Parameters["ToBill"].CurrentValue = ToVoucherNo;
                doc.Parameters["VoucherID"].CurrentValue = VoucherId;

                var frm = new KontoRepViewer(doc);
                frm.Text = "Packing Slip Print";
                frm.ShowDialog(this);

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Packing Slip  Print");
                MessageBox.Show(ex.ToString());
            }
            
        } 
        public PrintParaView(VoucherTypeEnum _vtype,string _title,string fromno, string tono)
        {
            InitializeComponent();
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
        
            this.Load += DocPrintParaView_Load;
            this.VoucherType = _vtype;
            this.Text = _title;
            
            fromTextEdit.Text = fromno;
            toTextEdit.Text = tono; 
        }

        private void DocPrintParaView_Load(object sender, EventArgs e)
        { 
            this.voucherLookup1.VTypeId = this.VoucherType; 
            this.voucherLookup1.SetDefault();
            //FillVoucher();
        }

      
        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    } 
}
