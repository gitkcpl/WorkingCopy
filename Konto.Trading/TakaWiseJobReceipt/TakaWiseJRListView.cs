﻿using System;
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
using Konto.App.Shared.Para;

namespace Konto.Trading.TakaWiseJobReceipt
{
    public partial class TakaWiseJRListView : ListBaseView
    {
        //private List<OpBillListDto> _modelList = new List<OpBillListDto>();

        public TakaWiseJRListView()

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
                        cmd.Parameters.Add("@VTypeId", SqlDbType.Int).Value = (int)VoucherTypeEnum.TakaWiseJobReceipt;
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
                Log.Error(ex, "Takawise Job Receipt List Error");
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
                string _status = "UNPAID";
                     if (customGridView1.Columns.ColumnByName("colStatus") != null)
                    _status=  this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "Status").ToString();

                if (_status != "UNPAID")
                {
                    MessageBoxAdv.Show("Can Not Delete,Payment Ref Exists", "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                int _bid = 0;
                if (customGridView1.Columns.ColumnByName("colBillId") != null)
                    _bid = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "BillId"));

                var _deleted = Convert.ToBoolean(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "IsDeleted"));
                if (_deleted)
                {
                    MessageBoxAdv.Show("Record Already in Deleted State", "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBoxAdv.Show(KontoGlobals.DeleteBeforeMsg, "Delete ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                using (var db = new KontoContext())
                {
                    if (TakaJobRecPara.Challan_Required)
                    {
                        var exist = db.BillTrans.Any(x => x.RefId == _id && x.RefVoucherId == _vid && x.IsDeleted == false && x.IsActive == true);
                        if (exist)
                        {
                            MessageBoxAdv.Show("Challan Exist In Invoice.. Can not Delete Order", "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    //main delete
                    var model = db.Challans.Find(_id);
                    model.IsDeleted = true;

                    //delete from stock
                    var st = db.StockTranses.Where(
                                  k => k.MasterRefId == model.RowId
                                && k.IsActive == true && k.IsDeleted == false).ToList();

                    db.StockTranses.RemoveRange(st);

                   //delete from challan trans
                    var trans = db.ChallanTranses.Where(x => x.ChallanId == _id).ToList();
                    foreach (var item in trans)
                    {
                        item.IsDeleted = true;
                        var prodlist = db.ProdOuts.Where(x => x.TransId == item.Id && x.RefId == item.ChallanId).ToList();
                        foreach (var pd in prodlist)
                        {
                            pd.IsDeleted = true;
                        }
                    }
                     
                    //delete from bill & bill trans
                    if (_bid > 0)
                    {
                        var bill = db.Bills.Find(_bid);
                        bill.IsDeleted = true;
                        var _btrans = db.BillTrans.Where(x => x.BillId == _bid).ToList();
                        foreach (var item in _btrans)
                        {
                            item.IsDeleted = true;
                        }

                        //delete from ledger bill to bill & bill ref
                        LedgerEff.DeleLedgEffect(bill, db);
                    }

                    db.SaveChanges();
                    customGridView1.DeleteRow(customGridView1.FocusedRowHandle);
                    MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            { 
                Log.Error(ex, "Taka wise Job Receipt delete");
                MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
            }
        }

        public override void Print()
        {
            base.Print();
          
          

        }
    }
}
