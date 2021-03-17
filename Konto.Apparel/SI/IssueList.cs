using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Transaction;
using Konto.Shared.Reports;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Apparel.SI
{
    public partial class IssueList : ListBaseView
    {
        public IssueList()
        {
            InitializeComponent();
            this.listDateRange1.GetButtonClick += ListDateRange1_GetButtonClick;
            listAction1.EditDeleteDisabled(false);

            this.ReportPrint = true;
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
                        cmd.Parameters.Add("@VTypeId", SqlDbType.Int).Value = (int)VoucherTypeEnum.StoreIssue;
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

                if (DtCriterias.Rows.Count == 0)
                    listAction1.EditDeleteDisabled(false);
                else
                {
                    if (customGridView1.Columns.ColumnByFieldName("Id") != null && customGridView1.Columns.ColumnByFieldName("VoucherId") != null)
                        listAction1.EditDeleteDisabled(true);
                    else
                        listAction1.EditDeleteDisabled(false);
                }

                if (string.IsNullOrEmpty(this.GridLayoutFileName) || this.KontoView == null) return;

                KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, this.KontoView);
                this.ActiveControl = customGridControl1;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "SJ List Error");
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
                //         var _status = this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "Status").ToString();
                if (_deleted)
                {
                    MessageBoxAdv.Show("Record Already in Deleted State", "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBoxAdv.Show(KontoGlobals.DeleteBeforeMsg, "Delete ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                using (var db = new KontoContext())
                {
                    var model = db.Challans.Find(_id);
                    bool result = LedgerEff.DataFreezeStatus(model.VoucherDate, model.TypeId, db);
                    if (result == false)
                    {
                        MessageBox.Show(KontoGlobals.DeleteFreezeWarning);
                        return;
                    }

                    var trans = db.ChallanTranses.Where(x => x.ChallanId == _id).ToList();
                    
                    foreach (var ct in trans)
                    {
                        var delProdOut = db.ProdOuts.Where(p => p.TransId == ct.Id && p.RefId == model.Id).ToList();

                        //Challan Trans
                        ct.IsDeleted = true;

                        foreach (var poitem in delProdOut)
                        {
                            ProdModel pitem = db.Prods.Find(poitem.ProdId);
                            if (pitem != null && pitem.RefId != model.Id && pitem.TransId != ct.Id && pitem.VoucherId != model.VoucherId)
                            {
                                pitem.ProdStatus = "STOCK";
                            }
                            else
                            {
                                pitem.IsDeleted = true;
                            }
                            poitem.IsDeleted = true;
                        }

                        ct.IsDeleted = true;
                    }

                    var stk = db.StockTranses.Where(k => k.MasterRefId == model.RowId).ToList();
                    if (stk != null)
                    {
                        db.StockTranses.RemoveRange(stk);
                    }
                    model.IsDeleted = true;
                    db.SaveChanges();
                    customGridView1.DeleteRow(customGridView1.FocusedRowHandle);
                    MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Store Issue delete");
                MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
            }
        }
        public override void Print()
        {
            base.Print();
            if (this.customGridView1.FocusedRowHandle <= 0) return;
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

                var frm = new DocPrintParaView(VoucherTypeEnum.StoreIssue, "StoreIssue Print", id, id, "ORD", "OrdId");
                frm.ShowDialog();
            }
        }
    }
}
