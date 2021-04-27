using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Reports;
using Serilog;
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

namespace Konto.Reporting.Para.Stock
{
    public partial class StockMainView : KontoForm
    {
        public StockMainView()
        {
            InitializeComponent();
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Details", "D"),
                new ComboBoxPairs("Summary", "S"),

            };
            typeLookUpEdit.Properties.DataSource = cbp;
            this.Load += StockMainView_Load;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;
            this.gridControl1.ProcessGridKey += GridControl1_ProcessGridKey;
            this.FormClosed += StockMainView_FormClosed;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;

            this.FirstActiveControl = dateEdit1;
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void GridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            var dr = bandedGridView1.GetRow(bandedGridView1.FocusedRowHandle) as StockDto;
            if (dr == null) return;
            var frm = new StockDetailViewWindow();
            frm._FromDate = this.dateEdit1.DateTime;
            frm._ToDate = this.dateEdit2.DateTime;
            frm._item = "Y";
            frm.BranchId = Convert.ToInt32(branchLookUpEdit.EditValue);
            frm.ProductId = dr.ItemId;
            frm.ShowDialog();
        }

        private void StockMainView_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage != null)
            {
                var tabcontrol = tabpage.Parent as TabControlAdv;
                if (tabcontrol != null)
                    tabcontrol.TabPages.Remove(tabpage);
            }
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
               string ptype = "Y";
                if (Convert.ToInt32(pTypeLookup1.SelectedValue) == 0)
                        ptype = "N";
                var fdate = Convert.ToInt32(dateEdit1.DateTime.ToString("yyyyMMdd"));
                var tdate = Convert.ToInt32(dateEdit2.DateTime.ToString("yyyyMMdd"));

                int divid = 0;
                int branchid = 0;

                if (!string.IsNullOrEmpty(divisionLookUpEdit.Text))
                {
                    divid = Convert.ToInt32(divisionLookUpEdit.EditValue);
                }

                if (!string.IsNullOrEmpty(branchLookUpEdit.Text))
                {
                    branchid = Convert.ToInt32(branchLookUpEdit.EditValue);
                }

                if (fdate < KontoGlobals.FromDate || fdate > KontoGlobals.ToDate)
                {
                    MessageBox.Show("From date out of financial date");
                    dateEdit1.Focus();
                    return;
                    
                }

                if (tdate > KontoGlobals.ToDate || tdate < KontoGlobals.FromDate)
                {
                    MessageBox.Show("To date out of financial date");
                    dateEdit2.Focus();
                    return;
                }

                using (var db = new KontoContext())
                {

                  var  Trans = db.Database.SqlQuery<StockDto>(
                        "dbo.StockReport @CompanyId={0},@PTypeId={1},@DivId={2},@BranchId={3}," +
                        "@FromDate={4},@ToDate={5},@ptype={6},@yearid={7}", 
                        Convert.ToInt32(KontoGlobals.CompanyId), Convert.ToInt32(pTypeLookup1.SelectedValue),
                        divid,branchid,
                        fdate, tdate, ptype,KontoGlobals.YearId).ToList();

                    if(checkEdit1.CheckState != CheckState.Checked)
                    {
                        Trans = Trans.Where(x => x.StockQty > 0).ToList();
                    }
                    stockDtoBindingSource.DataSource = Trans;
                }

                if (this.typeLookUpEdit.EditValue.ToString() == "D")
                    DetailsFormat();
                else
                    SummaryFormat();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Log.Error(ex,"error from stock");
            }
        }

        private void StockMainView_Load(object sender, EventArgs e)
        {
            using(var db = new KontoContext())
            {
                var br = (from b in db.Branches
                          where !b.IsDeleted //&& b.CompId == KontoGlobals.CompanyId
                          orderby b.BranchName
                          select new BaseLookupDto { Id = b.Id, DisplayText = b.BranchName }).ToList();

                var dv = (from b in db.Divisions
                          where !b.IsDeleted 
                          orderby b.DivisionName
                          select new BaseLookupDto { Id = b.Id, DisplayText = b.DivisionName }).ToList();

                branchLookUpEdit.Properties.DataSource = br;
                divisionLookUpEdit.Properties.DataSource = dv;
            }
            typeLookUpEdit.EditValue = "S";
            dateEdit1.DateTime = KontoGlobals.DFromDate;
            dateEdit2.DateTime = KontoGlobals.DToDate;
            this.ActiveControl = dateEdit1;

           // branchLookUpEdit.EditValue = KontoGlobals.BranchId;

        }

        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabPageAdv2.Controls.Count > 0) return;
            var frm = new StockParaMainView();
            frm.TopLevel = false;
            frm.Parent = tabPageAdv2;
            frm.Location = new Point(tabPageAdv2.Location.X + tabPageAdv2.Width / 2 - frm.Width / 2, tabPageAdv2.Location.Y + tabPageAdv2.Height / 2 - frm.Height / 2);
            frm.Show();
        }

        private void SummaryFormat()
        {
            var grd = bandedGridView1;
            grd.Columns["OpPcs"].Visible = true;
            grd.Columns["OpQty"].Visible = true;
            grd.Columns["InwPcs"].Visible = true;
            grd.Columns["InwQty"].Visible = true;
            grd.Columns["OutPcs"].Visible = true;
            grd.Columns["OutQty"].Visible = true;

            grd.Columns["SalePcs"].Visible = false;
            grd.Columns["PurPcs"].Visible = false;
            grd.Columns["SaleQty"].Visible = false;
            grd.Columns["PurQty"].Visible = false;
            grd.Columns["InFromJobPcs"].Visible = false;
            grd.Columns["InFromJobQty"].Visible = false;
            grd.Columns["InForJobPcs"].Visible = false;
            grd.Columns["InForJobQty"].Visible = false;
            grd.Columns["TransInPcs"].Visible = false;
            grd.Columns["TransInQty"].Visible = false;
            grd.Columns["MillPcs"].Visible = false;
            grd.Columns["MillRec"].Visible = false;
            grd.Columns["JobPcs"].Visible = false;
            grd.Columns["JobRec"].Visible = false;
            grd.Columns["ProdPcs"].Visible = false;
            grd.Columns["ProdQty"].Visible = false;
            grd.Columns["SRetPcs"].Visible = false;
            grd.Columns["SRetQty"].Visible = false;
            grd.Columns["StoreIssRetPcs"].Visible = false;
            grd.Columns["StoreIssRetQty"].Visible = false;

            grd.Columns["MillIsPcs"].Visible = false;
            grd.Columns["MillIsQty"].Visible = false;
            grd.Columns["IsForJobPcs"].Visible = false;
            grd.Columns["IsForJobQty"].Visible = false;
            grd.Columns["RefIssPcs"].Visible = false;
            grd.Columns["RefIssQty"].Visible = false;
            grd.Columns["SaleJobPcs"].Visible = false;
            grd.Columns["SaleJobQty"].Visible = false;
            grd.Columns["TransOutPcs"].Visible = false;
            grd.Columns["TransOutQty"].Visible = false;
            grd.Columns["StoreIssPcs"].Visible = false;
            grd.Columns["StoreIssQty"].Visible = false;
            grd.Columns["StoreIssQty"].Visible = false;
            grd.Columns["PRetPcs"].Visible = false;
            grd.Columns["PRetQty"].Visible = false;
        }

        private void DetailsFormat()
        {
            var grd = bandedGridView1;
            grd.Columns["InwPcs"].Visible = false;
            grd.Columns["InwQty"].Visible = false;
            grd.Columns["OutPcs"].Visible = false;
            grd.Columns["OutQty"].Visible = false;

            grd.Columns["OpPcs"].Visible = true;
            grd.Columns["OpQty"].Visible = true;
            grd.Columns["SalePcs"].Visible = true;
            grd.Columns["PurPcs"].Visible = true;
            grd.Columns["SaleQty"].Visible = true;
            grd.Columns["PurQty"].Visible = true;
            grd.Columns["InFromJobPcs"].Visible = true;
            grd.Columns["InFromJobQty"].Visible = true;
            grd.Columns["InForJobPcs"].Visible = true;
            grd.Columns["InForJobQty"].Visible = true;
            grd.Columns["TransInPcs"].Visible = true;
            grd.Columns["TransInQty"].Visible = true;
            grd.Columns["MillPcs"].Visible = true;
            grd.Columns["MillRec"].Visible = true;
            grd.Columns["JobPcs"].Visible = true;
            grd.Columns["JobRec"].Visible = true;
            grd.Columns["ProdPcs"].Visible = true;
            grd.Columns["ProdQty"].Visible = true;
            grd.Columns["SRetPcs"].Visible = true;
            grd.Columns["SRetQty"].Visible = true;
            grd.Columns["StoreIssRetPcs"].Visible = true;
            grd.Columns["StoreIssRetQty"].Visible = true;

            grd.Columns["MillIsPcs"].Visible = true;
            grd.Columns["MillIsQty"].Visible = true;
            grd.Columns["IsForJobPcs"].Visible = true;
            grd.Columns["IsForJobQty"].Visible = true;
            grd.Columns["RefIssPcs"].Visible = true;
            grd.Columns["RefIssQty"].Visible = true;
            grd.Columns["SaleJobPcs"].Visible = true;
            grd.Columns["SaleJobQty"].Visible = true;
            grd.Columns["TransOutPcs"].Visible = true;
            grd.Columns["TransOutQty"].Visible = true;
            grd.Columns["StoreIssPcs"].Visible = true;
            grd.Columns["StoreIssQty"].Visible = true;
            grd.Columns["StoreIssQty"].Visible = true;
            grd.Columns["PRetPcs"].Visible = true;
            grd.Columns["PRetQty"].Visible = true;
        }

        private void excelSimpleButton_Click(object sender, EventArgs e)
        {
            var _file = string.Format(@"{0}.xlsx", DateTime.Now.Ticks);
            bandedGridView1.ExportToXlsx("ExportFile\\" + _file);

            if (MessageBox.Show("File Exported Successfully, Do You want to open File ?", "Export",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string windir = Environment.GetEnvironmentVariable("WINDIR");
                Process prc = new Process();
                prc.StartInfo.FileName = windir + @"\explorer.exe";
                prc.StartInfo.Arguments = "ExportFile\\" + _file; ;
                prc.Start();
            }
        }
    }
}
