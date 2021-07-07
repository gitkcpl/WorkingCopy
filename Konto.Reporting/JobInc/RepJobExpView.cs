using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Masters.Dtos;
using Konto.Data.Models.Reports;
using Konto.Data.Models.Transaction;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Reporting.JobInc
{
    public partial class RepJobExpView : KontoForm
    {
        public bool MultiView { get; set; }
        //public string ReportFilterType { get; set; }
        public RepJobExpView()
        {
            InitializeComponent();
            this.Load += LedgerParaView_Load;
            this.FormClosed += ChlParaMainView_FormClosed;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.fDateEdit.EditValue = KontoGlobals.DFromDate;
            this.tDateEdit.EditValue = KontoGlobals.DToDate;
            this.ledgerGridView.RowStyle += LedgerGridView_RowStyle;
            this.ledgerGridView.RowCellStyle += LedgerGridView_RowCellStyle;
            this.customSimpleButton.Click += CustomSimpleButton_Click;
            this.repGridView1.FocusedRowChanged += RepGridView1_FocusedRowChanged;
          

            this.FirstActiveControl = fDateEdit;

            
        }

        private void RepGridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var rw = repGridView1.GetRow(repGridView1.FocusedRowHandle) as ReportTypeModel;
            if (rw == null)
                rw = repGridView1.GetRow(0) as ReportTypeModel;

            List<ComboBoxPairs> cbg = new List<ComboBoxPairs>();
            if (rw.Remarks != null)
            {
                var _groupons = rw.Remarks.Split('-');

                foreach (var item in _groupons)
                {
                    cbg.Add(new ComboBoxPairs(item, item));
                }
            }

            using (var db = new KontoContext())
            {
                var cols = db.RepCols.Where(x => x.FileName == rw.FileName).OrderBy(x => x.UserOrder).ToList();
                colsGridControl.DataSource = cols;
            }

            groupOnLookUpEdit.Properties.DataSource = cbg;

            groupOn2lookUpEdit.Properties.DataSource = cbg;

            if (!string.IsNullOrEmpty(rw.LastGroup1))
                groupOnLookUpEdit.EditValue = rw.LastGroup1;
            else
                groupOnLookUpEdit.EditValue = "Month";
            if (!string.IsNullOrEmpty(rw.LastGroup2))
                groupOn2lookUpEdit.EditValue = rw.LastGroup2;
            else
                groupOn2lookUpEdit.EditValue = "None";
        }

        private void CustomSimpleButton_Click(object sender, EventArgs e)
        {

        }
        private void ChlParaMainView_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage != null)
            {
                var tabcontrol = tabpage.Parent as TabControlAdv;
                if (tabcontrol != null)
                    tabcontrol.TabPages.Remove(tabpage);
            }
        }

        

        private void LedgerGridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView gridView = (GridView)sender;
            if (e.RowHandle == gridView.FocusedRowHandle && e.Column == gridView.FocusedColumn)
            {
                e.Appearance.BackColor = gridView.Appearance.FocusedCell.BackColor;
            }
        }

        private void LedgerGridView_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedRowHandle == e.RowHandle)
                e.Appearance.Assign(view.PaintAppearance.SelectedRow);
            else
                e.Appearance.Assign(view.PaintAppearance.Row);
            e.HighPriority = true;
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            if (this.MultiView)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            try
            {
                Show_X_Report();
            }
            catch (Exception wx)
            {

                MessageBox.Show(wx.ToString());
            }
            

        }

       

        private int SetCheckedParameters(XtraReport xrep)
        {
            int _ReportId = 0;
            var _paraList = new List<ReportParaModel>();
            using (var _db = new KontoContext())
            {
                var reportid = _db.ReportParas.DefaultIfEmpty().Max(k => k == null ? 0 : k.ReportId);
                _ReportId = reportid + 1;

                // product
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, itemGridView, "item", _ReportId));
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, ledgerGridView, "party", _ReportId));
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, pgGridView1, "party_group", _ReportId));
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, agentGridView1, "agent", _ReportId));
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, colorGridView, "color", _ReportId));
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, processGridView, "process", _ReportId));
                _db.ReportParas.AddRange(_paraList);
                _db.SaveChanges();
            }
            return _ReportId;

        }


    
        private void Show_X_Report()
        {
            if (repGridView1.RowCount == 0)
            {
                MessageBox.Show("No Report Exist from Preview..");
                return;
            }

            var rw = repGridView1.GetRow(repGridView1.FocusedRowHandle) as ReportTypeModel;
            string dr = "";

            if (rw == null)
                rw = repGridView1.GetRow(0) as ReportTypeModel;
            dr = rw.FileName;
            string objectToInstantiate = dr + ",Konto.Reporting";
            var objectType = Type.GetType(objectToInstantiate);

            var xrep = Activator.CreateInstance(objectType) as XtraReport;
            if (xrep == null)
            {
                MessageBox.Show("Report Does not exist");
                return;
            }
            xrep.Parameters["comp_id"].Value = KontoGlobals.CompanyId;

            //xrep.Parameters["year_id"].Value = KontoGlobals.YearId;

            xrep.Parameters["from_date"].Value = Convert.ToInt32(fDateEdit.DateTime.ToString("yyyyMMdd"));

            xrep.Parameters["to_date"].Value = Convert.ToInt32(tDateEdit.DateTime.ToString("yyyyMMdd"));

            int _ReportId = SetCheckedParameters(xrep);
            xrep.Parameters["report_id"].Value = _ReportId;

            if (Convert.ToInt32(voucherLookup1.SelectedValue) > 0)
            {
                xrep.Parameters["voucher_id"].Value = Convert.ToInt32(voucherLookup1.SelectedValue);
            }
            
            if (Convert.ToInt32(this.Tag) == MenuId.Grey_Issue_To_Mill_Report && Convert.ToInt32(challanTypeLookUpEdit.EditValue)
                != (int)ChallanTypeEnum.ISSUE_FOR_JOB)
                xrep.Parameters["vtype_id"].Value = (int)VoucherTypeEnum.MillIssue;
            else if(Convert.ToInt32(this.Tag) == MenuId.Job_Issue_Report || Convert.ToInt32(challanTypeLookUpEdit.EditValue)
                == (int) ChallanTypeEnum.ISSUE_FOR_JOB)
                xrep.Parameters["vtype_id"].Value = (int)VoucherTypeEnum.JobIssue;


            if (!string.IsNullOrEmpty(branchLookUpEdit1.Text))
            {
                xrep.Parameters["branch_id"].Value = Convert.ToInt32(branchLookUpEdit1.EditValue);
            }

            if (!string.IsNullOrEmpty(divLookUpEdit.Text))
            {

                xrep.Parameters["div_id"].Value = Convert.ToInt32(divLookUpEdit.EditValue);
            }

            if(Convert.ToInt32(groupLookup1.SelectedValue) > 0)
            {
                xrep.Parameters["item_group_id"].Value = Convert.ToInt32(groupLookup1.SelectedValue);
            }
            
            if(Convert.ToInt32(accLookup1.SelectedValue) > 0)
            {
                xrep.Parameters["trans_id"].Value = Convert.ToInt32(accLookup1.SelectedValue);
            }
            if (!string.IsNullOrEmpty(challanTypeLookUpEdit.Text))
            {
                xrep.Parameters["challan_type"].Value = Convert.ToInt32(challanTypeLookUpEdit.EditValue);
            }
           
            string _title = challanTypeLookUpEdit.Text + " " + rw.ReportName + " Register For "
                                          + fDateEdit.DateTime.ToString("dd/MM/yyyy") + " To " + tDateEdit.DateTime.ToString("dd/MM/yyyy");
            xrep.Parameters["report_title"].Value = _title;

           

            if (groupOnLookUpEdit.EditValue!=null)
            {

                xrep.Parameters["group_on1"].Value = groupOnLookUpEdit.EditValue;
            }
            if (groupOn2lookUpEdit.EditValue!=null)
            {

                xrep.Parameters["group_on2"].Value = groupOn2lookUpEdit.EditValue;
            }
            //xrep.Parameters["show_pending"].Value = checkEdit2.Checked;
            if (checkEdit2.Checked)
                xrep.FilterString = "PendQty >0 ";
            
            colsGridView.UpdateCurrentRow();

            var cols = colsGridView.GetSelectedRows();
            List<int> show_cols = new List<int>();

            //update last group & columns
            using (var db = new KontoContext())
            {
                for (int i = 0; i < colsGridView.RowCount - 1; i++)
                {
                    var row = colsGridView.GetRow(i) as RepColumn;
                    var col = db.RepCols.Find(row.Id);
                    col.Show = row.Show;
                }

                var rept = db.ReportTypes.Find(rw.Id);
                if (rept != null)
                {
                    rept.LastGroup1 = groupOnLookUpEdit.Text;
                    rept.LastGroup2 = groupOn2lookUpEdit.Text;
                }

                db.SaveChanges();
            }

            foreach (int r in cols)
            {
                var cl = colsGridView.GetRow(r) as RepColumn;
                show_cols.Add(cl.UserOrder);
            }

            xrep.Parameters["report_cols"].Value = show_cols.ToArray();

            if (radioGroup1.SelectedIndex == 0)
                xrep.Landscape = false;
            else
                xrep.Landscape = true;

            var sqlDataSource = (xrep.DataSource as SqlDataSource);
            sqlDataSource.ConnectionParameters = new CustomStringConnectionParameters(KontoGlobals.sqlConnectionString.ConnectionString);
            var frm = new RepXViewer();
            frm.Text = _title;
            frm.RepSource = xrep;
            frm.Show();
        }
        private void LedgerParaView_Load(object sender, EventArgs e)
        {
            using (var db = new KontoContext())
            {

                var reptype = db.ReportTypes
              .Where(k => k.IsActive && !k.IsDeleted && k.ReportTypes == "Job_Expense"
              && (k.PackageId==0 || k.PackageId == KontoGlobals.PackageId))
              .ToList();

                if (reptype.Count == 0) return;
                reportTypeModelBindingSource.DataSource = reptype;

                var rep = reptype.FirstOrDefault();
                var AccList = db.Database.SqlQuery<AccLookupDto>("dbo.acclookup @groupid={0},@companyid={1},@yearid={2},@taxtype={3},@nature={4},@fillparty={5},@vouchertypeid={6}"
                        , 0, KontoGlobals.CompanyId, KontoGlobals.YearId, "N", "ALL", "Y", 0).ToList();

                ledgerGridControl.DataSource = AccList;


                var agentlist = (from ac in db.Accs
                                 join pd in db.AccBals on ac.Id equals pd.AccId into join_pd
                                 from pd in join_pd.DefaultIfEmpty()
                                 where pd.CompId == KontoGlobals.CompanyId && pd.YearId == KontoGlobals.YearId
                                 && pd.GroupId == 31 && !ac.IsDeleted
                                 orderby ac.AccName
                                 select new BaseLookupDto { DisplayText = ac.AccName, Id = ac.Id }
                                 ).ToList();

                var pglist = db.PartyGroups.Where(x => !x.IsDeleted)
                                .OrderBy(x => x.GroupName)
                                .Select(x => new BaseLookupDto { DisplayText = x.GroupName, Id = x.Id })
                                .ToList();
              
                var divList = db.Divisions.Where(x => !x.IsDeleted)
                                .OrderBy(x => x.DivisionName)
                                .Select(x => new BaseLookupDto { DisplayText = x.DivisionName, Id = x.Id })
                                .ToList();

                var branchList = db.Branches.Where(x => !x.IsDeleted)
                                    .OrderBy(x => x.BranchName)
                                    .Select(x => new BaseLookupDto { DisplayText = x.BranchName, Id = x.Id })
                                    .ToList();

                var colorList = db.ColorModels.Where(x => !x.IsDeleted)
                                    .OrderBy(x => x.ColorName)
                                    .Select(x => new BaseLookupDto { DisplayText = x.ColorName, Id = x.Id })
                                    .ToList();

                var itemlist = db.Products.Where(x => !x.IsDeleted && x.PTypeId == (int)ProductTypeEnum.GREY)
                                .OrderBy(x => x.ProductName)
                                .Select(x => new BaseLookupDto { DisplayText = x.ProductName, Id = x.Id })
                                .ToList();
                
                var process = db.Process.Where(x => !x.IsDeleted)
                       .OrderBy(x => x.ProcessName)
                       .Select(x => new BaseLookupDto { DisplayText = x.ProcessName, Id = x.Id })
                       .ToList();

                var TransTypeList = new List<TransType>();

                TransTypeList = db.transTypes.
                Where(k => k.IsActive && !k.IsDeleted && (k.Category == "JOBISSUE" || k.Category=="MILLISSUE")
               )
                .ToList();

                //var designlist = db.Products.Where(x => !x.IsDeleted && x.ItemType =="D")
                //                .OrderBy(x => x.ProductName)
                //                .Select(x => new BaseLookupDto { DisplayText = x.ProductName, Id = x.Id })
                //                .ToList();

                itemGridControl.DataSource = itemlist;
                pgGridControl1.DataSource = pglist;
                //designGridControl1.DataSource = designlist;
                agentGridControl1.DataSource = agentlist;
                divLookUpEdit.Properties.DataSource = divList;
                branchLookUpEdit1.Properties.DataSource = branchList;
                colorGridControl.DataSource = colorList;
                processGridControl.DataSource = process;
                challanTypeLookUpEdit.Properties.DataSource = TransTypeList;
               

            }
           

            if (Convert.ToInt32(this.Tag) == (int)MenuId.Job_Issue_Report)
                challanTypeLookUpEdit.EditValue = (int) ChallanTypeEnum.ISSUE_FOR_JOB;
            else if (Convert.ToInt32(this.Tag) == (int)MenuId.Grey_Issue_To_Mill_Report)
                challanTypeLookUpEdit.EditValue = (int) ChallanTypeEnum.MILL_ISSUE;
        }

        private void acGroupGridControl_Click(object sender, EventArgs e)
        {
            


        }
    }
}
