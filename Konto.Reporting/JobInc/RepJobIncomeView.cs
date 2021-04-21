using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.Enterprise.Data.DataEngine.Expressions;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Masters.Dtos;
using Konto.Data.Models.Transaction;
using Konto.Reporting.CustomRep;
using Serilog;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Reporting.JobInc
{
    public partial class RepJobIncomeView : KontoForm
    {
        public bool MultiView { get; set; }
        //public string ReportFilterType { get; set; }
        public RepJobIncomeView()
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
           
            //List<ComboBoxPairs> cbp = new List<ComboBoxPairs>
            //{
            //    new ComboBoxPairs("Outward", "SCHALLAN"),
            //    new ComboBoxPairs("Inward", "PCHALLAN"),
            //    new ComboBoxPairs("Mill Issue", "MillIssue"),
            //    new ComboBoxPairs("Job Issue", "JobIssue"),
            //    new ComboBoxPairs("Mill Receipt", "MillRec"),
            //    new ComboBoxPairs("Job Receipt", "JobRec"),
            //    new ComboBoxPairs("Mill Return", "MillRet"),
            //};

            //typeLookUpEdit.Properties.DataSource = cbp;

            this.FirstActiveControl = fDateEdit;

            
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
            Show_X_Report();

        }

       

        private int SetCheckedParameters(XtraReport xrep)
        {
            int _ReportId = 0;
            var _paraList = new List<ReportParaModel>();
            using (var _db = new KontoContext())
            {
                var reportid = _db.ReportParas.DefaultIfEmpty().Max(k => k == null ? 0 : k.ReportId);
              _ReportId = reportid + 1;

                ReportParaModel ModelReport;

                if (ledgerGridView.SelectedRowsCount > 0)
                {
                    xrep.Parameters["party"].Value = "Y";

                    foreach (var item in ledgerGridView.GetSelectedRows())
                    {
                        var _acc = ledgerGridView.GetRow(item) as AccLookupDto;
                        ModelReport = new ReportParaModel();
                        ModelReport.ReportId = _ReportId;
                        ModelReport.ParameterName = "party";
                        ModelReport.ParameterValue = _acc.Id;
                        _paraList.Add(ModelReport);
                    }
                }

                if (itemGridView.SelectedRowsCount > 0)
                {
                    xrep.Parameters["item"].Value = "Y";
                    foreach (var item in itemGridView.GetSelectedRows())
                    {
                        var _acc = itemGridView.GetRow(item) as BaseLookupDto;
                        ModelReport = new ReportParaModel();
                        ModelReport.ReportId = _ReportId;
                        ModelReport.ParameterName = "product";
                        ModelReport.ParameterValue = _acc.Id;
                        _paraList.Add(ModelReport);
                    }
                }

                if (pgGridView1.SelectedRowsCount > 0)
                {
                    xrep.Parameters["party_group"].Value = "Y";

                    foreach (var item in pgGridView1.GetSelectedRows())
                    {
                        var _acc = pgGridView1.GetRow(item) as BaseLookupDto;
                        ModelReport = new ReportParaModel();
                        ModelReport.ReportId = _ReportId;
                        ModelReport.ParameterName = "party_group";
                        ModelReport.ParameterValue = _acc.Id;
                        _paraList.Add(ModelReport);
                    }
                }

                if (agentGridView1.SelectedRowsCount > 0)
                {
                    xrep.Parameters["agent"].Value = "Y";

                    foreach (var item in agentGridView1.GetSelectedRows())
                    {
                        var _acc = agentGridView1.GetRow(item) as BaseLookupDto;
                        ModelReport = new ReportParaModel();
                        ModelReport.ReportId = _ReportId;
                        ModelReport.ParameterName = "agent";
                        ModelReport.ParameterValue = _acc.Id;
                        _paraList.Add(ModelReport);
                    }
                }

                if (pgGridView1.SelectedRowsCount > 0)
                {
                    xrep.Parameters["party_group"].Value = "Y";

                    foreach (var item in pgGridView1.GetSelectedRows())
                    {
                        var _acc = pgGridView1.GetRow(item) as BaseLookupDto;
                        ModelReport = new ReportParaModel();
                        ModelReport.ReportId = _ReportId;
                        ModelReport.ParameterName = "party_group";
                        ModelReport.ParameterValue = _acc.Id;
                        _paraList.Add(ModelReport);
                    }
                }

                if (colorGridView.SelectedRowsCount > 0)
                {
                    xrep.Parameters["color"].Value = "Y";

                    foreach (var item in colorGridView.GetSelectedRows())
                    {
                        var _acc = colorGridView.GetRow(item) as BaseLookupDto;
                        ModelReport = new ReportParaModel();
                        ModelReport.ReportId = _ReportId;
                        ModelReport.ParameterName = "color";
                        ModelReport.ParameterValue = _acc.Id;
                        _paraList.Add(ModelReport);
                    }
                }

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
            xrep.Parameters["vtype_id"].Value = (int)VoucherTypeEnum.Inward;

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

            //if (!string.IsNullOrEmpty(titleTextEdit.Text))
            //{

            //    xrep.Parameters["report_title"].Value = "Job Work Register For "
            //         + fDateEdit.DateTime.ToString("dd/MM/yyyy") + " To " + tDateEdit.DateTime.ToString("dd/MM/yyyy");
            //}

            xrep.Parameters["report_title"].Value = rw.ReportName  +" Register For "
                     + fDateEdit.DateTime.ToString("dd/MM/yyyy") + " To " + tDateEdit.DateTime.ToString("dd/MM/yyyy");

            //if (!string.IsNullOrEmpty(footerTextEdit.Text))
            //{

            //    _rep["report_footer"] = footerTextEdit.Text;
            //}

            if (!string.IsNullOrEmpty(groupOnLookUpEdit.Text))
            {

                xrep.Parameters["group_on1"].Value = groupOnLookUpEdit.EditValue;
            }
            if (!string.IsNullOrEmpty(groupOn2lookUpEdit.Text))
            {

                xrep.Parameters["group_on2"].Value = groupOn2lookUpEdit.EditValue;
            }
            //xrep.Parameters["show_pending"].Value = checkEdit2.Checked;
            if (checkEdit2.Checked)
                xrep.FilterString = "PendQty >0 ";

            var sqlDataSource = (xrep.DataSource as SqlDataSource);
            sqlDataSource.ConnectionParameters = new CustomStringConnectionParameters(KontoGlobals.sqlConnectionString.ConnectionString);
            xrep.ShowRibbonPreview();
        }
        private void LedgerParaView_Load(object sender, EventArgs e)
        {
            using (var db = new KontoContext())
            {

                var reptype = db.ReportTypes
              .Where(k => k.IsActive && !k.IsDeleted && k.ReportTypes == "Job_Income")
              .ToList();

                if (reptype.Count == 0) return;
                reportTypeModelBindingSource.DataSource = reptype;

                var rep = reptype.FirstOrDefault();
                var AccList = db.Database.SqlQuery<AccLookupDto>("dbo.acclookup @groupid={0},@companyid={1},@yearid={2},@taxtype={3},@nature={4},@fillparty={5},@vouchertypeid={6}"
                        , 0, KontoGlobals.CompanyId, KontoGlobals.YearId, "N", "ALL", "Y", 6).ToList();

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
            }
            List<ComboBoxPairs> cbg = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("None", "None"),
                new ComboBoxPairs("Division", "Division"),
                new ComboBoxPairs("Branch", "Branch"),
                new ComboBoxPairs("Party", "Party"),
                new ComboBoxPairs("PartyGroup", "PartyGroup"),
                new ComboBoxPairs("Item", "Item"),
                new ComboBoxPairs("Agent", "Agent"),
                new ComboBoxPairs("Voucher", "Voucher"),
                
               
            };
            groupOnLookUpEdit.Properties.DataSource = cbg;
            groupOnLookUpEdit.EditValue = "None";


            groupOn2lookUpEdit.Properties.DataSource = cbg;
            groupOn2lookUpEdit.EditValue = "None";
        }

        private void acGroupGridControl_Click(object sender, EventArgs e)
        {
            


        }
    }
}
