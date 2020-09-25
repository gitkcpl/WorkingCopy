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

namespace Konto.Shared.Trans.GRN
{
    public partial class GRNListView : ListBaseView
    {
        //private List<OpBillListDto> _modelList = new List<OpBillListDto>();

        public GRNListView()

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
                        cmd.Parameters.Add("@VTypeId", SqlDbType.Int).Value = (int)VoucherTypeEnum.Inward;
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

        public override void RefreshGrid()
        {
          
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
                    var model = db.Challans.Find(_id);
                    var exist = db.ChallanTranses.Any(x => x.MiscId == _id && x.RefVoucherId == _vid && x.IsDeleted == false && x.IsActive == true);
                    if (exist)
                    {
                        MessageBoxAdv.Show("Outward Exist.. Can not Delete Order", "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    exist = db.BillTrans.Any(x => x.RefId == _id && x.RefVoucherId == _vid && x.IsDeleted == false && x.IsActive == true);
                    if (exist)
                    {
                        MessageBoxAdv.Show("Invoice Exist.. Can not Delete Order", "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    bool CanDelet = true;
                    var takadata = db.Prods.Where(k => k.RefId == model.Id && k.VoucherId == model.VoucherId
                                    && k.IsActive && !k.IsDeleted).ToList();
                    foreach (var item in takadata)
                    {
                        var usedTaka = db.ProdOuts.FirstOrDefault(k => k.ProdId == item.Id && k.IsActive && !k.IsDeleted);
                        if (usedTaka != null)
                        {
                            CanDelet = false;
                            break;
                        }
                    }

                    if (CanDelet)
                    {
                        var trans = db.ChallanTranses.Where(x => x.ChallanId == model.Id).ToList();
                        var stk = db.StockTranses.Where(k => k.MasterRefId == model.RowId).ToList();

                        if (stk != null)
                            db.StockTranses.RemoveRange(stk);
                        foreach (var ord in trans)
                        {
                            ord.IsDeleted = true;
                            var delProdOut = db.Prods.Where(p => p.TransId == ord.Id &&
                                 p.RefId == model.Id && p.VoucherId == model.VoucherId).ToList();
                            foreach (var item in delProdOut)
                            {
                                item.IsDeleted = true;
                            }
                        }

                        model.IsDeleted = true;
                        db.SaveChanges();
                        customGridView1.DeleteRow(customGridView1.FocusedRowHandle);
                        MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                   

                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Po delete");
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

                var frm = new DocPrintParaView(VoucherTypeEnum.Inward, "Grn Print",id,id, "GRN", "Id");
                frm.EditKey = Convert.ToInt32(this.KontoView.GetRowCellValue(this.KontoView.FocusedRowHandle, "Id"));
                frm.ShowDialog();


            }


        }
        public override void ImportExcel()
        {
            base.ImportExcel();
            var _imp = new GrnImport();
            _imp.ShowDialog();

        }

    }
}
