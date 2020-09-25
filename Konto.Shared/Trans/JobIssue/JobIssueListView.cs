using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Konto.Core.Shared.Frms;
using Konto.App.Shared;
using Konto.Data;
using Konto.Core.Shared.Libs;
using Syncfusion.Windows.Forms;
using Serilog;
using System.Data.SqlClient;
using Konto.Shared.Reports;
using Konto.Data.Models.Transaction;

namespace Konto.Shared.Trans.JobIssue
{
    public partial class JobIssueListView : ListBaseView
    {
        //private List<OpBillListDto> _modelList = new List<OpBillListDto>();

        public JobIssueListView()

        {
            InitializeComponent();
            this.listDateRange1.GetButtonClick += ListDateRange1_GetButtonClick;
            //  this.GridLayoutFileName = KontoFileLayout.Op_Bill_List;

            this.ReportPrint = true;
            listAction1.EditDeleteDisabled(false);
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
                        cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = KontoGlobals.BranchId;
                        cmd.Parameters.Add("@YearId", SqlDbType.Int).Value = KontoGlobals.YearId;
                        cmd.Parameters.Add("@VTypeId", SqlDbType.Int).Value = (int)VoucherTypeEnum.JobIssue;
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
                Log.Error(ex, "Job Issue List Error");
                MessageBoxAdv.Show(this, "Error While Generating List !!", "Exception ", ex.ToString());
            }
        }

       
        public override void DeleteRec()
        {
            base.DeleteRec();

            if (customGridView1.FocusedRowHandle < 0) return;
            try
            {
                var _id = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "Id"));
                var _vid = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "VoucherId"));
                var _deleted = Convert.ToBoolean(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "IsDeleted"));
                if (_deleted)
                {
                    MessageBoxAdv.Show("Record Already in Deleted State", "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBoxAdv.Show(KontoGlobals.DeleteBeforeMsg, "Delete ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                using (var db = new KontoContext())
                {
                    using(var _tran = db.Database.BeginTransaction())
                    {
                        try
                        {
                            //var exist = db.ChallanTranses.Any(x => x.MiscId == _id && x.RefVoucherId == _vid && x.IsDeleted == false && x.IsActive == true);
                            //if (exist)
                            //{
                            //    MessageBoxAdv.Show("Outward Challan Exists.. Can not Delete Job Issue", "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    return;
                            //}
                            var exist = db.JobReceipts.Any(x => x.RefId == _id && !x.IsDeleted);
                            if (exist)
                            {
                                MessageBoxAdv.Show("Job Receipt Exists.. Can not Delete Job Issue", "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            var model = db.Challans.Find(_id);

                            var stk = db.StockTranses.Where(k => k.MasterRefId == model.RowId).ToList();

                            if (stk != null)
                                db.StockTranses.RemoveRange(stk);

                            var Trans = db.ChallanTranses.Where(x => x.ChallanId == model.Id).ToList();

                            foreach (var ord in Trans)
                            {
                                
                                //Challan Trans
                                ord.IsDeleted = true;

                                var delProdOut = db.ProdOuts.Where(p => p.TransId == ord.Id &&
                                 p.RefId == model.Id).ToList();

                                foreach (var poitem in delProdOut)
                                {
                                    ProdModel pitem = db.Prods.Find(poitem.ProdId);
                                    if (pitem != null && pitem.RefId != model.Id && pitem.TransId != ord.Id && pitem.VoucherId != model.VoucherId)
                                    {
                                        pitem.ProdStatus = "STOCK";
                                    }

                                    poitem.IsDeleted = true;
                                }
                            }

                            model.IsDeleted = true;
                            db.SaveChanges();
                            _tran.Commit();
                            customGridView1.DeleteRow(customGridView1.FocusedRowHandle);
                            MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            _tran.Rollback();
                            Log.Error(ex, "Job Issue delete");
                            MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
                        }
                    }
                  

                }
            }
            catch (Exception ex)
            {
               Log.Error(ex, "Job Issue delete");
                MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
            }
        }

        public override void Print()
        {
            base.Print();
            if (this.customGridView1.FocusedRowHandle <= -1) return;
            if (KontoView.Columns.ColumnByFieldName("Id") != null)
            {
                if (KontoView.Columns.ColumnByFieldName("IsDeleted") != null)
                {
                    if (Convert.ToBoolean(this.KontoView.GetRowCellValue(this.KontoView.FocusedRowHandle, "IsDeleted")))
                    {
                        return;
                    }
                }
                var id = this.KontoView.GetRowCellValue(this.KontoView.FocusedRowHandle, "VoucherNo").ToString();

                var frm = new DocPrintParaView(VoucherTypeEnum.JobIssue, "Job Issue Challan", id, id, "JIB", "ChallanId");
                frm.EditKey = Convert.ToInt32(this.KontoView.GetRowCellValue(this.KontoView.FocusedRowHandle, "Id"));
                frm.ShowDialog();


            }


        }
    }
}
