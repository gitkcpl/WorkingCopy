using Konto.Data.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data
{
    public class StockEffect
    {
        public static bool StockTransChlnProdEntry(ChallanModel Model, ChallanTransModel item,
             bool IsIssue, string tableName, string userName, KontoContext _db, ProdModel prodModel, bool Isdelete)
        {
            
            StockTransModel stock = new StockTransModel();
            
            stock.CompanyId = Convert.ToInt16(Model.CompId);
            stock.YearId = (int)Model.YearId;

            stock.BranchId = Model.BranchId != null ? Convert.ToInt32(Model.BranchId) : 1;
            stock.GodownId = Model.StoreId != null ? (int)(Model.StoreId) : 1;

            if (item.ColorId != null && item.ColorId != 0)
                stock.ColorId = (int)item.ColorId;

            if (item.BatchId != null && item.BatchId != 0)
                stock.BatchId = (int)item.BatchId;

            if (item.GradeId != null && item.GradeId != 0)
                stock.GradeId = (int)item.GradeId;

            if (item.DesignId != null && item.DesignId != 0)
                stock.DesignId = item.DesignId;

            stock.ChallanType = Model.ChallanType;
            stock.Cut = item.Cops;
            if (stock.ColorId == null)
            {
                stock.ColorId = prodModel.ColorId;
            }
            stock.LotNo = item.LotNo;
            stock.RefId = prodModel.RowId;
            stock.MasterRefId = Model.RowId;
            stock.VoucherDate = Model.VoucherDate;
            stock.TransDate = Model.VDate;
            stock.DivId = Model.DivId;
            stock.AccountId = Model.AccId;

            //stock.TransDate = DateTime.ParseExact(Model.VoucherDate.ToString(), "yyyyMMdd",
            //     System.Globalization.CultureInfo.CurrentCulture);

            stock.VoucherId = Model.VoucherId != null ? Convert.ToInt32(Model.VoucherId) : 0;
            stock.BillNo = prodModel.VoucherNo;
            stock.VoucherNo = Model.VoucherNo;
            stock.ItemId = item.ProductId != null ? Convert.ToInt32(item.ProductId) : 0;

            stock.Rate = item.Rate;
            stock.Amount = item.Total;
            stock.AgentId = Model.AgentId != null ? Convert.ToInt32(Model.AgentId) : 0;

            stock.TableName = tableName;
            stock.KeyFldValue = Model.Id;
            stock.Narration = item.Remark;

            stock.IsActive = prodModel.IsActive;
            stock.IsDeleted = Isdelete;

            if (IsIssue)
            {
                stock.IssueNos = 1;
                if (prodModel.Tops > 0)
                {
                    stock.IssueNos = prodModel.Tops;
                }

                if (prodModel.NetWt == 0)
                {
                    stock.IssueQty = prodModel.Ply != 0 ? (decimal)prodModel.Ply : 0;
                }
                else
                {
                    stock.IssueQty = prodModel.NetWt != 0 ? (decimal)prodModel.NetWt : 0;
                }

                stock.Qty = (-1 * prodModel.NetWt);
                stock.Pcs = -1;
                if (prodModel.Tops > 0)
                {
                    stock.Pcs = -1 * prodModel.Tops;
                }
            }
            else
            {
                stock.RcptNos = 1;
                if (prodModel.Tops > 0)
                {
                    stock.RcptNos = prodModel.Tops;
                }
                if (prodModel.NetWt == 0)
                {
                    stock.RcptQty = prodModel.Ply != 0 ? (decimal)prodModel.Ply : 0;
                }
                else
                {
                    stock.RcptQty = prodModel.NetWt != 0 ? (decimal)prodModel.NetWt : 0;
                }

                stock.Qty = prodModel.NetWt;
                stock.Pcs = 1;
                if (prodModel.Tops > 0)
                {
                    stock.Pcs = prodModel.Tops;
                }
            }

            stock.TransDateTime = DateTime.Now;
            stock.UomId = item.UomId;

            stock.CreateDate = DateTime.Now;
            stock.CreateUser = userName;

            _db.StockTranses.Add(stock);

            return true;

        }


        public static bool StockTransChlnEntry(ChallanModel Model, ChallanTransModel item,
                 bool IsIssue, string tableName, string userName, KontoContext _db)
               
        {

          
            StockTransModel stock = new StockTransModel();
           
            stock.CompanyId = Convert.ToInt16(Model.CompId);
            stock.YearId = (int)Model.YearId;

            stock.BranchId = Model.BranchId != null ? Convert.ToInt32(Model.BranchId) : 0;
            stock.GodownId = Model.StoreId != null ? (int)(Model.StoreId) : 0;

            if (item.ColorId != null && item.ColorId != 0)
                stock.ColorId = (int)item.ColorId;

            if (item.BatchId != null && item.BatchId != 0)
                stock.BatchId = (int)item.BatchId;

            if (item.GradeId != null && item.GradeId != 0)
                stock.GradeId = (int)item.GradeId;

            if (item.DesignId != null && item.DesignId != 0)
                stock.DesignId = (int)item.DesignId;

            stock.ChallanType = Model.ChallanType;
            stock.Cut = item.Cops;

            stock.LotNo = item.LotNo;
            stock.RefId = item.RowId;
            stock.MasterRefId = Model.RowId;
            stock.VoucherDate = Model.VoucherDate;
            stock.TransDate = Model.VDate;
            stock.DivId = Model.DivId;
            stock.AccountId = Model.AccId;

            //stock.TransDate = DateTime.ParseExact(Model.VoucherDate.ToString(), "yyyyMMdd",
            //     System.Globalization.CultureInfo.CurrentCulture);

            stock.VoucherId = Model.VoucherId != null ? Convert.ToInt32(Model.VoucherId) : 0;
            stock.BillNo = Model.ChallanNo;
            stock.VoucherNo = Model.VoucherNo;
            stock.ItemId = item.ProductId != null ? Convert.ToInt32(item.ProductId) : 0;

            stock.Rate = item.Rate;
            stock.Amount = item.Total;
            stock.AgentId = Model.AgentId != null ? Convert.ToInt32(Model.AgentId) : 0;

            stock.TableName = tableName;
            stock.KeyFldValue = Model.Id;
            stock.Narration = item.Remark;

            stock.IsActive = item.IsActive;
            stock.IsDeleted = item.IsDeleted;

            if (IsIssue)
            {
                stock.IssueNos = item.Pcs;
                stock.IssueQty = item.Qty;
                stock.Qty = (-1 * item.Qty);
                stock.Pcs = -1 * item.Pcs;
            }
            else
            {
                stock.RcptNos = item.Pcs;
                stock.RcptQty = item.Qty;
                stock.Qty = item.Qty;
                stock.Pcs = item.Pcs;
            }

            stock.TransDateTime = DateTime.Now;


            stock.CreateDate = DateTime.Now;
            stock.CreateUser = userName;
            stock.UomId = item.UomId;
            _db.StockTranses.Add(stock);

            return true;

        }


        public static bool StockTransChlnProdOutEntry(ChallanModel Model, ChallanTransModel item,
           bool IsIssue, string tableName, KontoContext _db, ProdOutModel prodModel, bool Isdelete)
        {
           
            StockTransModel stock = new StockTransModel();
           

            stock.CompanyId = Convert.ToInt16(Model.CompId);
            stock.YearId = (int)Model.YearId;

            stock.BranchId = Model.BranchId != null ? Convert.ToInt32(Model.BranchId) : 0;
            stock.GodownId = Model.StoreId != 0 ? (int)Model.StoreId : 0;

            if (item.ColorId != null && item.ColorId != 0)
                stock.ColorId = (int)item.ColorId;

            if (item.BatchId != null && item.BatchId != 0)
                stock.BatchId = (int)item.BatchId;

            if (item.GradeId != null && item.GradeId != 0)
                stock.GradeId = (int)item.GradeId;

            if (stock.ColorId == null)
            {
                stock.ColorId = prodModel.ColorId;
            }
            stock.Cut = item.Cops;

            stock.LotNo = item.LotNo;
            stock.RefId = prodModel.RowId;
            stock.MasterRefId = Model.RowId;
            stock.VoucherDate = Model.VoucherDate;
            stock.TransDate = Model.VDate;
            stock.DivId = Model.DivId;
            stock.AccountId = Model.AccId;

            //stock.TransDate = DateTime.ParseExact(Model.VoucherDate.ToString(), "yyyyMMdd",
            //     System.Globalization.CultureInfo.CurrentCulture);

            stock.VoucherId = Model.VoucherId != null ? Convert.ToInt32(Model.VoucherId) : 0;
            stock.BillNo = prodModel.VoucherNo;
            stock.VoucherNo = Model.VoucherNo;
            stock.ItemId = item.ProductId != null ? Convert.ToInt32(item.ProductId) : 0;

            stock.Rate = item.Rate;
            stock.Amount = item.Total;
            stock.AgentId = Model.AgentId != null ? Convert.ToInt32(Model.AgentId) : 0;

            stock.TableName = tableName;
            stock.KeyFldValue = Model.Id;
            stock.Narration = item.Remark;

            stock.IsActive = prodModel.IsActive;
            stock.IsDeleted = Isdelete;

            if (IsIssue)
            {
                stock.IssueNos = 1;
                if (prodModel.GrayPcs > 0)
                {
                    stock.IssueNos = Convert.ToInt32(prodModel.GrayPcs);
                }
                stock.IssueQty = prodModel.Qty != 0 ? ((decimal)prodModel.Qty * -1) : 0;
                stock.Qty = Convert.ToDecimal(prodModel.Qty);
                stock.Pcs = -1;
                if (prodModel.GrayPcs > 0)
                {
                    stock.Pcs = -1 * Convert.ToInt32(prodModel.GrayPcs);
                }
            }
            else
            {
                stock.RcptNos = 1;
                if (prodModel.GrayPcs > 0)
                {
                    stock.RcptNos = Convert.ToInt32(prodModel.GrayPcs);
                }
                stock.RcptQty = prodModel.Qty != 0 ? ((decimal)prodModel.Qty) : 0;
                stock.Qty = Convert.ToDecimal(prodModel.Qty);
                stock.Pcs = 1;
                if (prodModel.GrayPcs > 0)
                {
                    stock.Pcs = Convert.ToInt32(prodModel.GrayPcs);
                }
            }

            stock.TransDateTime = DateTime.Now;




            _db.StockTranses.Add(stock);

            return true;

        }

        public static bool  StockTransBillEntry(BillModel Model, BillTransModel item,
            bool IsIssue, string tableName,  KontoContext _db)
        {

           
            StockTransModel stock = new StockTransModel();
            

            stock.CompanyId = Convert.ToInt16(Model.CompId);
            stock.YearId = (int)Model.YearId;

            stock.BranchId = Model.BranchId != null ? Convert.ToInt32(Model.BranchId) : 1;
            stock.GodownId = Model.StoreId != 0 ? (int)Model.StoreId : 1;

            if (item.ColorId != null && item.ColorId != 0)
                stock.ColorId = (int)item.ColorId;

            if (item.BatchId != null && item.BatchId != 0)
                stock.BatchId = (int)item.BatchId;

            if (item.GradeId != null && item.GradeId != 0)
                stock.GradeId = (int)item.GradeId;


            stock.Cut = item.Cut;

            stock.RefId = item.RowId;
            stock.MasterRefId = Model.RowId;
            stock.VoucherDate = Model.VoucherDate;
            stock.TransDate = Model.VDate;
            stock.DivId = Model.DivisionId;
            stock.AccountId = Model.AccId;

            //stock.TransDate = DateTime.ParseExact(Model.VoucherDate.ToString(), "yyyyMMdd",
            //     System.Globalization.CultureInfo.CurrentCulture);

            stock.VoucherId = Model.VoucherId != 0 ? Convert.ToInt32(Model.VoucherId) : 0;
            stock.BillNo = Model.BillNo;
            stock.VoucherNo = Model.VoucherNo;
            stock.ItemId = item.ProductId != null ? Convert.ToInt32(item.ProductId) : 0;

            stock.Rate = item.Rate != 0 ? (decimal)item.Rate : 0;
            stock.Amount = item.Total != 0 ? (decimal)item.Total : 0;
            stock.AgentId = Model.AgentId != null ? Convert.ToInt32(Model.AgentId) : 0;

            stock.TableName = tableName;
            stock.KeyFldValue = Model.Id;
            stock.Narration = item.Remark;

            stock.IsActive = item.IsActive;
            stock.IsDeleted = item.IsDeleted;

            if (IsIssue)
            {
                stock.IssueNos = item.Pcs;
                stock.IssueQty = item.Qty;
                stock.Qty = (-1 * item.Qty);
                stock.Pcs = -1 * item.Pcs;
            }
            else
            {
                stock.RcptNos = item.Pcs;
                stock.RcptQty = item.Qty;
                stock.Qty = item.Qty;
                stock.Pcs = item.Pcs;
            }

            stock.TransDateTime = DateTime.Now;
            stock.Pcs = 0;

        
            
                _db.StockTranses.Add(stock);
            //_db.SaveChanges();

            //Stock Bal
            //StockBalModel stockbal = _db.StockBals.FirstOrDefault(p => p.ProductId == item.ProductId
            //&& p.CompanyId == Model.CompId
            //&& p.BranchId == Model.BranchId);

            //var sum = (from p in _db.StockTranses
            //           where p.ItemId == item.ProductId && p.CompanyId == Model.CompId && p.BranchId == Model.BranchId
            //          && p.IsActive && !p.IsDeleted
            //           group p by 1 into g
            //           select new { sumqty = g.Sum(x => x.Qty), sumnos = g.Sum(x => x.Pcs) }).FirstOrDefault();

            //if (stockbal != null)
            //{

            //    stockbal.BalQty = Convert.ToDecimal(sum.sumqty);
            //    stockbal.BalNos = Convert.ToInt32(sum.sumnos);
            //    stockbal.ModifyUser = userName;
            //    stockbal.ModifyDate = DateTime.Now;

            //    await SBalrepo.UpdateAsyn(stockbal, stockbal.Id);

            //    //_db.Entry(stockbal).CurrentValues.SetValues(stockbal);
            //    //_db.SaveChanges();
            //}
            //else
            //{
            //    stockbal = new StockBalModel();
            //    stockbal.BalQty = Convert.ToDecimal(sum.sumqty);
            //    stockbal.BalNos = Convert.ToInt32(sum.sumnos);
            //    stockbal.BranchId = KontoGlobals.BranchId;
            //    stockbal.CompanyId = (int)Model.CompId;
            //    stockbal.DivId = Model.DivisionId;
            //    stockbal.GodownId = Model.StoreId != 0 ? (int)Model.StoreId : 0;
            //    stockbal.IssueNo = 0;
            //    stockbal.IssueQty = 0;
            //    var prod = _db.Products.FirstOrDefault(k => k.Id == item.ProductId);
            //    stockbal.ItemCode = prod.RowId;
            //    stockbal.YearId = Model.YearId != 0 ? (int)Model.YearId : 0;
            //    stockbal.ProductId = (int)item.ProductId;
            //    stockbal.CreateDate = DateTime.Now;
            //    stockbal.CreateUser = userName;

            //    await SBalrepo.AddAsyn(stockbal);

            //    //_db.StockBals.Add(stockbal);
            //    //_db.SaveChanges();
            //}

            return true;

        }
    }
}
