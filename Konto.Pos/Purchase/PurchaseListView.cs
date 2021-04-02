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
using Konto.Shared.Trans.Common;
using DevExpress.XtraGrid.Views.Grid;
using Konto.Data.Models.Transaction.Dtos;
using Konto.Shared.Trans.PInvoice;

namespace Konto.Pos.Purchase
{
    public partial class PurchaseListView : ListBaseView
    {
        //private List<OpBillListDto> _modelList = new List<OpBillListDto>();

        public PurchaseListView()

        {
            InitializeComponent();
            this.listDateRange1.GetButtonClick += ListDateRange1_GetButtonClick;
            //  this.GridLayoutFileName = KontoFileLayout.Op_Bill_List;
            this.customGridView1.FocusedRowChanged += CustomGridView1_FocusedRowChanged;
            this.ReportPrint = true;
            listAction1.EditDeleteDisabled(false);
            this.attachSimpleButton.Click += AttachSimpleButton_Click;
        }

        private void CustomGridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (customGridView1.FocusedRowHandle < 0) return;
                var row = customGridView1.GetDataRow(customGridView1.FocusedRowHandle);

                var _id = Convert.ToInt32(row["Id"]);
                using (var db = new KontoContext())
                {
                    var lst = (from p in db.BtoBs
                               join o2 in db.BillTrans
                               on new { A = p.RefId, B = (int)p.RefTransId } equals new { A = o2.BillId, B = o2.Id } into LEFTJOIN
                               from result in LEFTJOIN.DefaultIfEmpty()
                               join o1 in db.Bills on result.BillId equals o1.Id
                               join v1 in db.Vouchers on o1.VoucherId equals v1.Id
                               join br in db.BillRefs on p.RefCode equals br.RowId
                               where !br.IsDeleted && !o1.IsDeleted && !p.IsDeleted
                                && br.BillId == _id && p.TransType == "Payment"
                               select new PaymentHistoryDto()
                               {
                                   VoucherNo = o1.VoucherNo,
                                   ChlnDate = o1.VoucherDate,
                                   Amount = (decimal)p.Amount,
                                   Remark = result.Remark,
                                   ChqNo = result.ChequeNo,
                                   Type = v1.VoucherName

                               }
                               ).ToList();
                    customGridView2.Columns.Clear();
                    customGridControl2.DataSource = lst;
                    customGridView2.PopulateColumns();
                    customGridView2.Columns["ChlnDate"].VisibleIndex = -1;
                    customGridView2.BestFitColumns();

                    //var lstRet =
                    var lstRet = (from p in db.BtoBs
                                  join o1 in db.Bills on p.RefId equals o1.Id
                                  join v1 in db.Vouchers on o1.VoucherId equals v1.Id
                                  join br in db.BillRefs on p.RefCode equals br.RowId
                                  where !br.IsDeleted && !o1.IsDeleted && !p.IsDeleted
                                   && br.BillId == _id && p.TransType == "Return"
                                  select new PaymentHistoryDto()
                                  {
                                      VoucherNo = o1.VoucherNo,
                                      ChlnDate = o1.VoucherDate,
                                      Amount = (decimal)p.Amount,
                                      Remark = o1.Remarks,
                                      Type = v1.VoucherName

                                  }
                               ).ToList();

                    customGridView3.Columns.Clear();
                    customGridControl3.DataSource = lstRet;
                    customGridView3.PopulateColumns();
                    customGridView3.Columns["ChlnDate"].VisibleIndex = -1;
                    customGridView3.Columns["ChqNo"].VisibleIndex = -1;
                    customGridView3.BestFitColumns();
                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "purchase list");
            }
        }

        private void AttachSimpleButton_Click(object sender, EventArgs e)
        {
            if (customGridView1.FocusedRowHandle < 0) return;
            var row = customGridView1.GetDataRow(customGridView1.FocusedRowHandle);

           
            if (KontoView.Columns.ColumnByFieldName("Id") != null)
            {
                var _id = Convert.ToInt32(row["Id"]);
                var _vid = Convert.ToInt32(row["VoucherId"]);
                using (var db = new KontoContext())
                {
                    var _atc = db.Attachments.Where(x => x.RefVoucherId == _id && x.VoucherId == _vid && !x.IsDeleted).ToList();
                    if(_atc.Count()==0)
                    {
                        MessageBox.Show("No Attachemnt Found..");
                        return;
                    }
                    var frm = new AttachmentView();
                    frm.Trans = _atc;
                    frm.ShowDialog();
                }

            }


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
                        cmd.Parameters.Add("@VTypeId", SqlDbType.Int).Value = (int)VoucherTypeEnum.PurchaseInvoice;
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


                customGridView1.OptionsSelection.MultiSelect = true;
                customGridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Gp List Error");
                MessageBoxAdv.Show(this, "Error While Generating List !!", "Exception ", ex.ToString());
            }
        }

        public override void RefreshGrid()
        {
          
        }

        public override void DeleteRec()
        {
            base.DeleteRec();

            if (customGridView1.SelectedRowsCount <= 0) return;
            var drs = customGridView1.GetSelectedRows();
            if (MessageBoxAdv.Show(KontoGlobals.DeleteBeforeMsg, "Delete ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            using (var db = new KontoContext())
            {
                using (var _tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var rowno in drs)
                        {
                            var row = customGridView1.GetDataRow(rowno);

                            var _id = Convert.ToInt32(row["Id"]);  
                            var _vid = Convert.ToInt32(row["VoucherId"]);
                            var _deleted = Convert.ToBoolean(row["IsDeleted"]);
                            var _status = row["Status"].ToString(); 
                            //  var _id = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "Id"));
                            //var _vid = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "VoucherId"));
                            //var _deleted = Convert.ToBoolean(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "IsDeleted"));
                            //var _status = this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "Status").ToString();
                            if (_status != "UNPAID")
                            {
                                MessageBoxAdv.Show("Can Not Delete,Payment Ref Exists", "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            if (_deleted)
                            {
                                MessageBoxAdv.Show("Record Already in Deleted State", "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            var model = db.Bills.Find(_id);

                            bool result = LedgerEff.DataFreezeStatus(model.VoucherDate, model.TypeId, db);
                            if (result == false)
                            {
                                MessageBox.Show(KontoGlobals.DeleteFreezeWarning);
                                return;
                            }

                            var trans = db.BillTrans.Where(x => x.BillId == _id).ToList();
                            foreach (var item in trans)
                            {
                                item.IsDeleted = true;
                            }


                            model.IsDeleted = true;

                            var stk = db.StockTranses.Where(k => k.MasterRefId == model.RowId).ToList();

                            if (stk != null)
                            {
                                db.StockTranses.RemoveRange(stk);
                            }

                            LedgerEff.DeleLedgEffect(model, db);
                        }
                        customGridView1.DeleteSelectedRows();
                        db.SaveChanges();
                        _tran.Commit();
                        MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Purchase Invoice delete");
                        MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
                    }
                }
            }
        }

        public override void Print()
        {
            base.Print();
            if (this.customGridView1.FocusedRowHandle < 0) return;
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

                var frm = new DocPrintParaView(VoucherTypeEnum.PurchaseInvoice, "Purchase Print", id, id, "PURBILL", "BillId");
                frm.EditKey = Convert.ToInt32(this.KontoView.GetRowCellValue(this.KontoView.FocusedRowHandle, "Id"));
                frm.ShowDialog();
               


            }


        }
        public override void ImportExcel()
        {
            base.ImportExcel();
            if (MessageBox.Show("Import For Template.. ?", "Import", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var frm = new BillUploadView();
                frm.FillTemplate((int)VoucherTypeEnum.PurchaseInvoice);
                frm.ShowDialog();
            }
            else
            {
                var _exp = new PInvoiceImport();
                _exp.ShowDialog();
            }
        }
    }
}
