using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Konto.Core.Shared.Frms;
using Konto.App.Shared;
using Konto.Data;
using Konto.Core.Shared.Libs;
using Syncfusion.Windows.Forms;
using Serilog;
using Konto.Data.Models.Transaction.Dtos;
using DevExpress.XtraGrid.Views.Grid;

namespace Konto.Shared.OpBill
{
    public partial class SPOpBillListView : ListBaseView
    {
        private List<OpBillListDto> _modelList = new List<OpBillListDto>();
        public SPOpBillListView()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.Op_Bill_List;

            
        }

    
        public override void RefreshGrid()
        {
            base.RefreshGrid();
            
            using (var _context = new KontoContext())
            {
                _modelList = (from o in _context.Bills
                              join ac in _context.Accs on o.AccId equals ac.Id into joinAc
                              from ac in joinAc.DefaultIfEmpty()
                              join bk in _context.Accs on o.BookAcId equals bk.Id into joinbk
                              from bk in joinbk.DefaultIfEmpty()
                              join ag in _context.Accs on o.AgentId equals ag.Id into joinAg
                              from ag in joinAg.DefaultIfEmpty()
                              join voucher in _context.Vouchers on o.VoucherId equals voucher.Id into joinVoucher
                              from voucher in joinVoucher.DefaultIfEmpty()
                              orderby o.VoucherDate descending, o.Id descending
                              where o.CompId == KontoGlobals.CompanyId && o.YearId == KontoGlobals.YearId && !o.IsDeleted
                              && voucher.VTypeId == (int)VoucherTypeEnum.SalePurchaseOpBill
                              select new OpBillListDto()
                              {
                                  AgentName = ag.AccName,
                                  BillAmt=o.TotalAmount,
                                  BillNo = o.BillNo,
                                  BillType = o.BillType,
                                  VoucherDate = o.VoucherDate,
                                  CreateDate = o.CreateDate,
                                  CreateUser= o.CreateUser,
                                  Id=o.Id,
                                  IsActive=o.IsActive,
                                  ModifyDate=o.ModifyDate,
                                  ModifyUser=o.ModifyUser,
                                  PartPayment=o.GrossAmount,
                                  PartyName=ac.AccName,
                                  PendingAmt=o.TotalAmount - o.GrossAmount - o.TotalPcs - o.TdsAmt,
                                  Qty=o.TotalQty,
                                  Rate= o.ExchRate,
                                  ReturnGoods=o.TotalPcs,
                                  Tds= o.TdsAmt,
                                  VoucherNo=o.VoucherNo,
                                  Book = bk.AccName
                              }
                              ).ToList();

            }

            customGridControl1.DataSource = _modelList;
            if (string.IsNullOrEmpty(this.GridLayoutFileName) || this.KontoView == null) return;

            KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, this.KontoView);

            this.ActiveControl = customGridControl1;
            if (_modelList.Count == 0)
                listAction1.EditDeleteDisabled(false);
            else
                listAction1.EditDeleteDisabled(true);

            customGridView1.OptionsSelection.MultiSelect = true;
            customGridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
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
                            var row = customGridView1.GetRow(rowno) as OpBillListDto;
                            if (row == null) return;
                            var _id = row.Id;
                           // var _id = Convert.ToInt32(row["Id"]);  //Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "Id"));
                         //   var _vid = Convert.ToInt32(row["VoucherId"]); //Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "VoucherId"));
                           // var _deleted = Convert.ToBoolean(row["IsDeleted"]);  //Convert.ToBoolean(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "IsDeleted"));
                           // var _status = row["Status"].ToString(); //this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "Status").ToString();
                            //if (_status != "UNPAID")
                            //{
                            //    MessageBoxAdv.Show("Can Not Delete,Payment Ref Exists", "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    return;
                            //}
                            //if (_deleted)
                            //{
                            //    MessageBoxAdv.Show("Record Already in Deleted State", "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    return;
                            //}


                            var model = db.Bills.Find(_id);
                            bool result = LedgerEff.DataFreezeStatus(model.VoucherDate, model.TypeId, db);
                            if (result == false)
                            {
                                MessageBox.Show(KontoGlobals.DeleteFreezeWarning);
                                return;
                            }
                           

                            var billr = db.BillRefs.FirstOrDefault(k => k.BillId == model.Id);
                            billr.IsDeleted = true;

                            model.IsDeleted = true;

                        }

                        customGridView1.DeleteSelectedRows();
                        db.SaveChanges();
                        _tran.Commit();
                        MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Opening Bill delete");
                        MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
                    }
                }


            }

        }
      

        private void excelSimpleButton_Click(object sender, EventArgs e)
        {
            var frm = new OpBillImport();
            frm.ShowDialog();
        }
    }
}
