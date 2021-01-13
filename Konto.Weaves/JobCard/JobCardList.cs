using GrapeCity.ActiveReports;
using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Transaction.Dtos;
using Serilog;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Weaves.JobCard
{
    public partial class JobCardList : ListBaseView
    {
        private List<JobCardDto> _modelList = new List<JobCardDto>();
        public JobCardList()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.JobCard_List;
            this.listDateRange1.GetButtonClick += ListDateRange1_GetButtonClick;
        }
        private void ListDateRange1_GetButtonClick(object sender, EventArgs e)
        {
            this.GridLayoutFileName = listDateRange1.SelectedItem.LayoutFile;
            var DtCriterias = new DataTable();
            try
            {
                var db = new KontoContext();
                using (var con = new SqlConnection(db.Database.Connection.ConnectionString))
                {
                    using (var cmd = new SqlCommand(listDateRange1.SelectedItem.SpName, con))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Add("@fromDate", SqlDbType.Int).Value = listDateRange1.FromDate;
                        cmd.Parameters.Add("@ToDate", SqlDbType.Int).Value = listDateRange1.ToDate;
                        cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = KontoGlobals.CompanyId;
                      //  cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = KontoGlobals.BranchId;
                        cmd.Parameters.Add("@YearId", SqlDbType.Int).Value = KontoGlobals.YearId;
                        cmd.Parameters.Add("@VTypeId", SqlDbType.Int).Value = (int)VoucherTypeEnum.JobCard;
                        if (listDateRange1.SelectedItem.Extra1 == "Deleted")
                        {
                            cmd.Parameters.Add("@Deleted", SqlDbType.Int).Value = 1;
                        }
                        if (listDateRange1.SelectedItem.GroupCol != null)
                        {
                            string grpCol = listDateRange1.SelectedItem.GroupCol;
                            cmd.Parameters.Add("@GrpBy", SqlDbType.Text).Value = listDateRange1.SelectedItem.GroupCol;
                        }
                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();
                        DtCriterias.Load(cmd.ExecuteReader());
                        con.Close();
                        customGridView1.ShowLoadingPanel();
                        customGridView1.Columns.Clear();
                        customGridControl1.DataSource = DtCriterias;
                        customGridView1.HideLoadingPanel();
                    }
                }
                if (string.IsNullOrEmpty(this.GridLayoutFileName) || this.KontoView == null) return;

                KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, this.KontoView);

                this.ActiveControl = customGridControl1;


                if (DtCriterias.Rows.Count == 0)
                    listAction1.EditDeleteDisabled(false);
                else
                {
                    if (customGridView1.Columns.ColumnByFieldName("Id") != null && customGridView1.Columns.ColumnByFieldName("VoucherId") != null)
                        listAction1.EditDeleteDisabled(true);
                    else
                        listAction1.EditDeleteDisabled(false);
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Grn List Error");
                MessageBoxAdv.Show(this, "Error While Generating List !!", "Exception ", ex.ToString());
            }
        }


        //public override void RefreshGrid()
        //{
        //    base.RefreshGrid();
        //    //var configuration = new MapperConfiguration(cfg =>
        //    //    cfg.CreateMap<DivisionModel, DivListDto>());

        //    using (var _db = new KontoContext())
        //    {
        //        var spcol = _db.SpCollections.FirstOrDefault(k => k.Id == (int)SpCollectionEnum.JobCardList);
        //        if (spcol == null)
        //        {
        //            _modelList = new List<JobCardDto>(_db.Database.SqlQuery<JobCardDto>(
        //                "dbo.JobCardList @VoucherTypeID={0}, @CompanyId={1}",
        //                (int)VoucherTypeEnum.JobCard, KontoGlobals.CompanyId).ToList());
        //        }
        //        else
        //        {
        //            _modelList = new List<JobCardDto>(_db.Database.SqlQuery<JobCardDto>(spcol.Name + " @VoucherTypeID={0}, @CompanyId={1}",
        //                    (int)VoucherTypeEnum.JobCard, KontoGlobals.CompanyId).ToList());
        //        }
        //    }

        //    customGridControl1.DataSource = _modelList;
        //    if (string.IsNullOrEmpty(this.GridLayoutFileName) || this.KontoView == null) return;

        //    KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, this.KontoView);

        //    this.ActiveControl = customGridControl1;
        //    if (_modelList.Count == 0)
        //        listAction1.EditDeleteDisabled(false);
        //    else
        //        listAction1.EditDeleteDisabled(true);
        //}

        public override void DeleteRec()
        {
            base.DeleteRec();

            if (customGridView1.FocusedRowHandle < 0) return;
            try
            {
                var _id = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "Id"));

                if (MessageBoxAdv.Show(KontoGlobals.DeleteBeforeMsg, "Delete ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                using (var db = new KontoContext())
                {
                    var Model  = db.jobCards.Find(_id);
                    var Trans = db.jobCardTrans.Where(k => k.JobCardId==_id && k.IsDeleted == false).ToList();
                    foreach (var item in Trans)
                    {
                        item.IsDeleted = true;
                    }

                    Model.IsDeleted = true;

                    db.SaveChanges();
                    customGridView1.DeleteRow(customGridView1.FocusedRowHandle);
                    MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "JobCard delete");
                MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
            }
        }

        public override void Print()
        {
            base.Print();
            if (customGridView1.FocusedRowHandle < 0) return;
            try
            {
                var _id = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "Id"));


                PageReport rpt = new PageReport();

                rpt.Load(new FileInfo("reg\\JobCardPrint.rdlx"));

                rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);
                doc.Parameters["id"].CurrentValue =_id;

                KontoContext db = new KontoContext();
                var ItemTrans = db.jobCardTrans.Where(k => k.JobCardId == _id).ToList();
                var shadelst = ItemTrans.GroupBy(k => k.ItemId).ToList();
                int i = 0;
                foreach (var item in shadelst)
                {
                    i = i + 1;
                    doc.Parameters["ShadeId" + i].CurrentValue = item.Key;
                }

                //rpt.ResourceLocator = new MySubreportLocator();


                var frm = new KontoRepViewer(doc);
                frm.Text = "Job Card Print";
                var _tab = this.Parent.Parent as TabControlAdv;
                if (_tab == null) return;
                var pg1 = new TabPageAdv();
                pg1.Text = "Job Card Print";
                _tab.TabPages.Add(pg1);
                _tab.SelectedTab = pg1;
                frm.TopLevel = false;
                frm.Parent = pg1;
                frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                frm.Show();// = true;

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Job Card print");
                MessageBoxAdv.Show(this, "Error While Print !!", "Exception ", ex.ToString());

            }
        }
    }
}