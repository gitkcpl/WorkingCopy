using AutoMapper;
using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Weaves.TakaConv
{
    public partial class TakaConvList : ListBaseView
    {
        private List<BeamProdDto> _modelList = new List<BeamProdDto>();
        public TakaConvList()
        {
            InitializeComponent();
            this.listDateRange1.GetButtonClick += ListDateRange1_GetButtonClick;
            listAction1.EditDeleteDisabled(false);
             this.GridLayoutFileName = KontoFileLayout.TakaConv_List; 
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
                        cmd.Parameters.Add("@VTypeId", SqlDbType.Int).Value = (int)VoucherTypeEnum.TakaConv;
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
                Log.Error(ex, "Taka Conv List Error");
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

                if (MessageBoxAdv.Show(KontoGlobals.DeleteBeforeMsg, "Delete ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                using (var db = new KontoContext())
                {
                    var Modelcln = db.Challans.Find(_id);
                    var Trans = db.ChallanTranses.Where(k => k.ChallanId == _id && k.IsDeleted==false).ToList();
                    foreach (var item in Trans)
                    {
                        item.IsDeleted = true;
                    }
                    var prodTrans= db.ProdOuts.Where(k => k.RefId == _id && k.IsDeleted==false).ToList();
                    foreach (var item in prodTrans)
                    {
                        item.IsDeleted = true;

                        var pr = db.Prods.Find(item.ProdId);
                        pr.ProdStatus = "STOCK";
                    }

                    //Stock Trans
                    var stk = db.StockTranses.Where(k => k.MasterRefId == Modelcln.RowId).ToList();
                    if (stk != null)
                        db.StockTranses.RemoveRange(stk);
                       
                    Modelcln.IsDeleted = true;
                    Modelcln.ModifyUser = KontoGlobals.UserName;

                    db.SaveChanges();
                    customGridView1.DeleteRow(customGridView1.FocusedRowHandle);
                    MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Taka Convertsion delete");
                MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
            }
        }
    }
}