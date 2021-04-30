using DevExpress.XtraGrid.Views.Grid;
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
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.UI;
using Konto.Core.Shared.Libs;

namespace Konto.Reporting.Chal
{
    public partial class RepSalesChallanView : KontoForm
    {
        public bool MultiView { get; set; }
        //public string ReportFilterType { get; set; }
        public RepSalesChallanView()
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
            List<BaseLookupDto> cols = new List<BaseLookupDto>();
            if (repGridView1.FocusedRowHandle == 0)
            {
                cols.Add(new BaseLookupDto(){DisplayText = "Date",Id=0});
                cols.Add(new BaseLookupDto() { DisplayText = "VoucherNo", Id = 1 });
                cols.Add(new BaseLookupDto() { DisplayText = "OrderNo", Id = 3 });
                cols.Add(new BaseLookupDto() { DisplayText = "OrderDate", Id = 4 });
                cols.Add(new BaseLookupDto() { DisplayText = "Party", Id = 5 });
                cols.Add(new BaseLookupDto() { DisplayText = "Pcs", Id = 6 });
                cols.Add(new BaseLookupDto() { DisplayText = "Qty", Id =7 });
                cols.Add(new BaseLookupDto() { DisplayText = "Transport", Id = 8 });
                cols.Add(new BaseLookupDto() { DisplayText = "Agent", Id = 9 });
                cols.Add(new BaseLookupDto() { DisplayText = "Remark", Id = 10 });
                
                colsGridControl.DataSource = cols;

                colsGridView.SelectRows(0,1);
                colsGridView.SelectRows(4, 6);
            }
            else if (repGridView1.FocusedRowHandle == 1)
            {
                cols.Add(new BaseLookupDto() { DisplayText = "Date", Id = 0 });
                cols.Add(new BaseLookupDto() { DisplayText = "VoucherNo", Id = 1 });
                cols.Add(new BaseLookupDto() { DisplayText = "OrderNo", Id = 3 });
                cols.Add(new BaseLookupDto() { DisplayText = "OrderDate", Id = 4 });
                cols.Add(new BaseLookupDto() { DisplayText = "Party", Id = 5 });
                cols.Add(new BaseLookupDto() { DisplayText = "Item", Id = 6 });
                cols.Add(new BaseLookupDto() { DisplayText = "Color", Id = 7 });
                cols.Add(new BaseLookupDto() { DisplayText = "Design", Id = 8 });
                cols.Add(new BaseLookupDto() { DisplayText = "Grade", Id = 9 });
                cols.Add(new BaseLookupDto() { DisplayText = "Lot/Batch", Id = 10 });
                cols.Add(new BaseLookupDto() { DisplayText = "Cut/Cops", Id = 11 });
                cols.Add(new BaseLookupDto() { DisplayText = "Pcs/Box", Id = 12 });
                cols.Add(new BaseLookupDto() { DisplayText = "Qty", Id = 13 });
                cols.Add(new BaseLookupDto() { DisplayText = "Rate", Id = 14 });
                cols.Add(new BaseLookupDto() { DisplayText = "Transport", Id = 15 });
                cols.Add(new BaseLookupDto() { DisplayText = "Agent", Id = 16 });
                cols.Add(new BaseLookupDto() { DisplayText = "Remark", Id = 17 });
                colsGridControl.DataSource = cols;

                colsGridView.SelectRows(0, 1);
                colsGridView.SelectRows(4, 5);
                colsGridView.SelectRows(11, 13);
            }
            else
            {
                colsGridControl.DataSource = cols;
            }


        }
        private void Show_X_Report()
        {
            var fdate = Convert.ToInt32(fDateEdit.DateTime.ToString("yyyyMMdd"));
            var tdate = Convert.ToInt32(tDateEdit.DateTime.ToString("yyyyMMdd"));

            int divid = 0;
            int branchid = 0;

            if (fdate < KontoGlobals.FromDate || fdate > KontoGlobals.ToDate)
            {
                MessageBox.Show("From date out of financial date");
                fDateEdit.Focus();
                return;

            }

            if (tdate > KontoGlobals.ToDate || tdate < KontoGlobals.FromDate)
            {
                MessageBox.Show(@"To date out of financial date");
                tDateEdit.Focus();
                return;
            }
            if (repGridView1.RowCount == 0)
            {
                MessageBox.Show(@"No Report Exist for Preview..");
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
                MessageBox.Show(@"Report Does not exist");
                return;
            }
            string _title = rw.ReportName + " Register For "
                                          + fDateEdit.DateTime.ToString("dd/MM/yyyy") + " To " + tDateEdit.DateTime.ToString("dd/MM/yyyy");

            xrep.Parameters["comp_id"].Value = KontoGlobals.CompanyId;

            //xrep.Parameters["year_id"].Value = KontoGlobals.YearId;

            xrep.Parameters["from_date"].Value = Convert.ToInt32(fDateEdit.DateTime.ToString("yyyyMMdd"));

            xrep.Parameters["to_date"].Value = Convert.ToInt32(tDateEdit.DateTime.ToString("yyyyMMdd"));

            int _ReportId = SetCheckedParameters(xrep);

            xrep.Parameters["report_id"].Value = _ReportId;

            if (!string.IsNullOrEmpty(branchLookUpEdit1.Text))
            {
                xrep.Parameters["branch_id"].Value = Convert.ToInt32(branchLookUpEdit1.EditValue);
                //_title = _title + " " + branchLookUpEdit1.Text;
            }

            if (!string.IsNullOrEmpty(divLookUpEdit.Text))
            {

                xrep.Parameters["div_id"].Value = Convert.ToInt32(divLookUpEdit.EditValue);
               // _title = _title + " " + divLookUpEdit.Text;
            }

            xrep.Parameters["report_title"].Value = _title;

            xrep.Parameters["group_on1"].Value = gp1LookupEdit.EditValue;


            xrep.Parameters["group_on2"].Value = gp2lookUpEdit.EditValue;

            if (!string.IsNullOrEmpty(challanTypeLookUpEdit.Text))
            {
                xrep.Parameters["challan_type"].Value = challanTypeLookUpEdit.EditValue;
            }

            if(radioGroup2.SelectedIndex==1)
                xrep.Parameters["vtype_id"].Value = VoucherTypeEnum.SalesChallan;
            else
                xrep.Parameters["vtype_id"].Value = VoucherTypeEnum.OutJobChallan;

            if (Convert.ToInt32(voucherLookup1.SelectedValue) > 0)
                xrep.Parameters["voucher_id"].Value = voucherLookup1.SelectedValue;

            if (Convert.ToInt32(gradeLookup1.SelectedValue) > 0)
                xrep.Parameters["voucher_id"].Value = gradeLookup1.SelectedValue;


            
            var cols = colsGridView.GetSelectedRows();
            List<int> show_cols = new List<int>();
            foreach (int r in cols)
            {
                var cl = colsGridView.GetRow(r) as BaseLookupDto;
                show_cols.Add(cl.Id);
            }

            xrep.Parameters["report_cols"].Value = show_cols.ToArray();

            if (radioGroup1.SelectedIndex == 0)
                xrep.Landscape = false;
            else
                xrep.Landscape = true;
            //string _filter = string.Empty;


            //xrep.FilterString = _filter;

            var sqlDataSource = (xrep.DataSource as SqlDataSource);
            sqlDataSource.ConnectionParameters = new CustomStringConnectionParameters(KontoGlobals.sqlConnectionString.ConnectionString);
            sqlDataSource.ConnectionOptions.DbCommandTimeout = 0;



            var frm = new RepXViewer();
            frm.Text = _title;
            frm.RepSource = xrep;
            frm.Show();


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

                // product types
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, typeGridView, "p_type", _ReportId));

                //group
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, groupGridView, "item_group", _ReportId));

                
                //color
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, colorGridView, "color", _ReportId));


                //party
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, ledgerGridView, "party", _ReportId));
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, pgGridView1, "party_group", _ReportId));
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, agentGridView1, "agent", _ReportId));
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, designGridView1, "design", _ReportId));
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, catGridView, "category", _ReportId));
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, processGridView, "process", _ReportId));

                _db.ReportParas.AddRange(_paraList);
                _db.SaveChanges();
            }



            return _ReportId;

        }
        private void CustomSimpleButton_Click(object sender, EventArgs e)
        {
            //if(MessageBox.Show("Edit Selected Report","Edit/New",MessageBoxButtons.YesNo) == DialogResult.No)
            //{
            //    var _db = new KontoContext();
            //    var frm = new CustomeRepWindow
            //    {
                    
            //        SpParaList = new List<SpParaModel>(
            //                _db.SpParas.Where(k => k.SpName == "Challan_reg").ToList()),
            //        ReportType = typeLookUpEdit.EditValue.ToString(),
            //        SPName = "dbo.Challan_reg",
            //        FileName = "reg\\CustomReport" + typeLookUpEdit.Text,
            //        VTypeId = (int)VoucherTypeEnum.Inward

            //    };
            //    frm.ShowDialog();
            //    return;
            //}
            //if (repGridView1.RowCount == 0) return;
            //var rep = repGridView1.GetRow(repGridView1.FocusedRowHandle) as ReportTypeModel;
            //if (rep == null)
            //    rep = repGridView1.GetRow(0) as ReportTypeModel;
            //var frmd = new KontoArDesignerView();
            //frmd.endUserDesigner1._reportName = rep.FileName;
            //frmd.endUserDesigner1.reportDesigner.LoadReport(new FileInfo(rep.FileName));
            //frmd.Text = "Konto Designer - " + rep.FileName;
            //frmd.Show();
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

      

        private int SetCheckedParameters(GrapeCity.ActiveReports.Document.PageDocument doc,
                Stimulsoft.Report.StiReport _rep)
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
                    if (doc != null)
                        doc.Parameters["party"].CurrentValue = "Y";
                    else
                      _rep["party"] = "Y";

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
                    if (doc != null)
                        doc.Parameters["item"].CurrentValue = "Y";
                    else
                        _rep["item"] = "Y";
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

                if (designGridView1.SelectedRowsCount > 0)
                {
                    if (doc != null)
                        doc.Parameters["design"].CurrentValue = "Y";
                    else
                        _rep["design"] = "Y";

                    foreach (var item in designGridView1.GetSelectedRows())
                    {
                        var _acc = designGridView1.GetRow(item) as BaseLookupDto;
                        ModelReport = new ReportParaModel();
                        ModelReport.ReportId = _ReportId;
                        ModelReport.ParameterName = "design";
                        ModelReport.ParameterValue = _acc.Id;
                        _paraList.Add(ModelReport);
                    }
                }
                if (colorGridView.SelectedRowsCount > 0)
                {
                    if (doc != null)
                        doc.Parameters["color"].CurrentValue = "Y";
                    else
                        _rep["color"] = "Y";

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


      

        private void LedgerParaView_Load(object sender, EventArgs e)
        {
            using (var db = new KontoContext())
            {

                var AccList = db.Database.SqlQuery<AccLookupDto>("dbo.acclookup @groupid={0},@companyid={1},@yearid={2},@taxtype={3},@nature={4},@fillparty={5},@vouchertypeid={6}"
                    , 0, KontoGlobals.CompanyId, KontoGlobals.YearId, "N", "ALL", "Y", VoucherTypeEnum.SalesChallan).ToList();

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

                var designlist = db.Products.Where(x => !x.IsDeleted && x.ItemType =="D")
                                .OrderBy(x => x.ProductName)
                                .Select(x => new BaseLookupDto { DisplayText = x.ProductName, Id = x.Id })
                                .ToList();

                var groups = db.PGroups.Where(x => !x.IsDeleted)
                    .OrderBy(x => x.GroupName)
                    .Select(x => new BaseLookupDto { DisplayText = x.GroupName, Id = x.Id })
                    .ToList();

                var types = db.ProductTypes.Where(x => !x.IsDeleted)
                    .OrderBy(x => x.TypeName)
                    .Select(x => new BaseLookupDto { DisplayText = x.TypeName, Id = x.Id })
                    .ToList();


                var reptype = db.ReportTypes
               .Where(k => k.IsActive && !k.IsDeleted && k.ReportTypes =="SCHALLAN")
               .ToList();

                
                 reportTypeModelBindingSource.DataSource = reptype;

                

                var TransTypeList = new List<TransType>();
                
                    TransTypeList = db.transTypes.
                    Where(k => k.IsActive && !k.IsDeleted && k.Category=="Outward"
                   )
                    .ToList();

                    var cats = db.CategroyModels.Where(x => !x.IsDeleted)
                        .OrderBy(x => x.CatName)
                        .Select(x => new BaseLookupDto { DisplayText = x.CatName, Id = x.Id })
                        .ToList();

                    var process = db.Process.Where(x => !x.IsDeleted)
                        .OrderBy(x => x.ProcessName)
                        .Select(x => new BaseLookupDto { DisplayText = x.ProcessName, Id = x.Id })
                        .ToList();

                challanTypeLookUpEdit.Properties.DataSource = TransTypeList;

                pgGridControl1.DataSource = pglist;
                agentGridControl1.DataSource = agentlist;

                ledgerGridControl.DataSource = AccList;

                divLookUpEdit.Properties.DataSource = divList;
                branchLookUpEdit1.Properties.DataSource = branchList;

                itemGridControl.DataSource = itemlist;
                designGridControl1.DataSource = designlist;
                colorGridControl.DataSource = colorList;
                groupGridControl1.DataSource = groups;
                typeGridControl.DataSource = types;
                catGridControl.DataSource = cats;
                processGridControl.DataSource = process;

            }
            List<ComboBoxPairs> cbg = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("None", "None"),
                new ComboBoxPairs("Division", "Division"),
                new ComboBoxPairs("Branch", "Branch"),
                new ComboBoxPairs("Process", "Process"),
                new ComboBoxPairs("Party", "Party"),
                new ComboBoxPairs("Agent", "Agent"),
                new ComboBoxPairs("PartyGroup", "PartyGroup"),
                new ComboBoxPairs("Voucher", "Voucher"),
                new ComboBoxPairs("Item", "Item"),
                new ComboBoxPairs("Color", "Color"),
                new ComboBoxPairs("Design", "Design"),
                new ComboBoxPairs("Grade", "Grade"),
                new ComboBoxPairs("Date", "Date"),
                new ComboBoxPairs("Month", "Month"),
                new ComboBoxPairs("Qtr", "Qtr"),
            };

            gp1LookupEdit.Properties.DataSource = cbg;
            gp1LookupEdit.EditValue = "None";
            gp2lookUpEdit.Properties.DataSource = cbg;
            gp2lookUpEdit.EditValue = "None";

            fDateEdit.DateTime = KontoGlobals.DFromDate;
            tDateEdit.DateTime = KontoGlobals.DToDate;
        }

        private void acGroupGridControl_Click(object sender, EventArgs e)
        {
            
        }

        private void customGridControl2_Click(object sender, EventArgs e)
        {

        }

        private void radioGroup2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup2.SelectedIndex == 0)
                voucherLookup1.VTypeId =  VoucherTypeEnum.OutJobChallan;
            else
                voucherLookup1.VTypeId = VoucherTypeEnum.SalesChallan;
        }
    }
}
