using Konto.Data.Models.Apparel;
using Konto.Data.Models.Masters;
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

        public static decimal GetStock(int productId)
        {
            using(var db = new KontoContext())
            {
                var st = db.StockTranses
                        .Where(x => x.ItemId == productId)
                        .Select(x=>x.Qty)
                        .DefaultIfEmpty(0)
                        .Sum();

                return st;
            }

        }

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

        public static bool Stock_Bom_Prod_Entry(BomModel Model, KontoContext _db)
        {
            StockTransModel stock = new StockTransModel();

            stock.CompanyId = Convert.ToInt16(Model.CompId);
            stock.YearId = (int)Model.YearId;

            stock.BranchId = Model.BranchId;
            stock.GodownId = 1;
            stock.LotNo = Model.VoucherNo;
            stock.RefId = Model.RowId;
            stock.MasterRefId = Model.RowId;
            stock.VoucherDate = Model.VoucherDate;
            //stock.TransDate = Model.VDate;
            stock.VoucherId = Model.VoucherId;
            stock.BillNo = Model.VoucherNo;
            stock.VoucherNo = Model.VoucherNo;

            stock.DivId = Model.DivisionId;
            stock.ItemId = Model.ProductId;
            stock.IsActive = true;
            stock.IsDeleted = false;

            stock.Narration = Model.Remark;

            stock.RcptQty = Model.TargetQty;
            stock.Qty = Model.TargetQty;

            _db.StockTranses.Add(stock);

            return true;

        }
        public static bool Stock_Bom_Issue_Entry(BomModel Model, KontoContext _db, BOMTransModel item)
        {
            StockTransModel stock = new StockTransModel();

            stock.CompanyId = Convert.ToInt16(Model.CompId);
            stock.YearId = (int)Model.YearId;

            stock.BranchId = Model.BranchId;
            stock.GodownId = 1;
            stock.LotNo = Model.VoucherNo;
            stock.RefId = item.RowId;
            stock.MasterRefId = Model.RowId;
            stock.VoucherDate = Model.VoucherDate;
            //stock.TransDate = Model.VDate;
            stock.VoucherId = Model.VoucherId;
            stock.BillNo = Model.VoucherNo;
            stock.VoucherNo = Model.VoucherNo;

            stock.DivId = Model.DivisionId;
            stock.ItemId = item.ProductId;
            stock.IsActive = true;
            stock.IsDeleted = false;

            stock.Narration = Model.Remark;

            stock.IssueQty = item.RequireQty;
            stock.Qty = -1 * item.RequireQty;

            _db.StockTranses.Add(stock);

            return true;
        }

            public static bool StockTransChlnEntry(ChallanModel Model, ChallanTransModel item,
                 bool IsIssue, string tableName, string userName, KontoContext _db,bool _stockForReturnProduct=false)
               
        {

          
            StockTransModel stock = new StockTransModel();
           
            stock.CompanyId = Convert.ToInt16(Model.CompId);
            stock.YearId = (int)Model.YearId;

            stock.BranchId = Model.BranchId != null ? Convert.ToInt32(Model.BranchId) : 0;
            stock.GodownId = Model.StoreId != null ? (int)(Model.StoreId) : 0;

            if (!_stockForReturnProduct)
            {
                if (item.ColorId != null && item.ColorId != 0)
                    stock.ColorId = (int)item.ColorId;

                if (item.BatchId != null && item.BatchId != 0)
                    stock.BatchId = (int)item.BatchId;

                if (item.GradeId != null && item.GradeId != 0)
                    stock.GradeId = (int)item.GradeId;

                if (item.DesignId != null && item.DesignId != 0)
                    stock.DesignId = (int)item.DesignId;
            }

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

            stock.VoucherId = Model.VoucherId;
            stock.BillNo = Model.ChallanNo;
            stock.VoucherNo = Model.VoucherNo;
            
            if (!_stockForReturnProduct)
                stock.ItemId = item.ProductId != null ? Convert.ToInt32(item.ProductId) : 0;
            else
                stock.ItemId = item.NProductId != null ? Convert.ToInt32(item.NProductId) : 0;

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
                
                stock.IssueQty = item.Qty;
                stock.Qty = (-1 * item.Qty);

                if (tableName == "Cutting")
                {
                    stock.Pcs = -1;
                    stock.IssueNos = 1;
                }
                else
                {
                    stock.IssueNos = 1;
                    stock.Pcs = -1 * item.Pcs;
                }
            }
            else
            {
                if (!_stockForReturnProduct)
                {
                    stock.RcptNos = item.Pcs;
                    stock.RcptQty = item.Qty;
                    stock.Qty = item.Qty;
                    stock.Pcs = item.Pcs;
                }
                else
                {
                    stock.RcptNos = item.PlainPcs;
                    stock.RcptQty = item.PlainQty;
                    stock.Qty = item.PlainQty;
                    stock.Pcs = item.PlainPcs;
                }
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

            stock.DesignId = item.DesignId;

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


        public static bool StockTransProdEntry(ProdModel Model, bool IsIssue,
            decimal RcptQty, decimal IssueQty, decimal qty, int pcs,
           string tableName, KontoContext _db, int ProductId=0)
        {

            StockTransModel stock = new StockTransModel();

            stock.CompanyId = Convert.ToInt16(Model.CompId);
            stock.YearId = Model.YearId != null ? (int)Model.YearId : 0;

            stock.BranchId = Model.BranchId != null ? Convert.ToInt32(Model.BranchId) : 0;
            //stock.GodownId = Model.StoreId != null ? (int)(Model.StoreId) : 0;

            if (Model.ColorId != null && Model.ColorId != 0)
                stock.ColorId = (int)Model.ColorId;

            if (Model.BatchId != null && Model.BatchId != 0)
                stock.BatchId = (int)Model.BatchId;

            if (Model.GradeId != null && Model.GradeId != 0)
                stock.GradeId = (int)Model.GradeId;

            if (Model.Cops != 0)
                stock.Cut = (int)Model.Cops;

            stock.RefId = Model.RowId;
            stock.MasterRefId = Model.RowId;
            stock.VoucherDate = Model.VoucherDate != 0 ? (int)Model.VoucherDate : 0;

            stock.VoucherId = Model.VoucherId != null ? Convert.ToInt32(Model.VoucherId) : 0;
            stock.BillNo = Model.VoucherNo;
            stock.VoucherNo = Model.VoucherNo;
            stock.DivId = Model.DivId;

            stock.VoucherDate = Model.VoucherDate != 0 ? (int)Model.VoucherDate : 0;
            
            if (ProductId == 0)
                stock.ItemId = (int)Model.ProductId;
            else
                stock.ItemId = ProductId;

            stock.TableName = tableName;
            stock.KeyFldValue = Model.Id;
            stock.Narration = Model.Remark;

            if (IsIssue)
            {
                stock.IssueNos = pcs;
                stock.IssueQty = IssueQty;
                stock.Qty = (-1 * (qty));
                stock.Pcs = -1 * pcs;
            }
            else
            {
                stock.RcptNos = pcs;
                stock.RcptQty = RcptQty;
                stock.Qty = qty;
                stock.Pcs = pcs;
            }

            stock.TransDateTime = DateTime.Now;
            stock.CreateDate = DateTime.Now;
            stock.TransDateTime = DateTime.Now;

            _db.StockTranses.Add(stock);

            return true;

        }


        public static bool StockTransProdOutEntry(ChallanModel Model, ProdModel item,
       bool IsIssue, string tableName, string userName, KontoContext _db, ProdOutModel pOutModel)
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


            stock.Cut = (int)item.Cops;

            stock.RefId = pOutModel.RowId;
            stock.MasterRefId = Model.RowId;
            stock.VoucherDate = Model.VoucherDate;
            stock.TransDate = Model.VDate;
            stock.DivId = Model.DivId;
            stock.AccountId = Model.AccId;

            stock.VoucherId = Model.VoucherId != null ? Convert.ToInt32(Model.VoucherId) : 0;
            stock.BillNo = item.VoucherNo;
            stock.VoucherNo = Model.VoucherNo;
            stock.ItemId = item.ProductId != null ? Convert.ToInt32(item.ProductId) : 0;

            stock.AgentId = Model.AgentId != null ? Convert.ToInt32(Model.AgentId) : 0;

            stock.TableName = tableName;
            stock.KeyFldValue = Model.Id;
            stock.Narration = item.Remark;

            stock.IsActive = true;
            stock.IsDeleted = false;
            stock.Pcs = 1;
            if (IsIssue)
            {
                stock.IssueNos = 1;
                stock.IssueQty = pOutModel.Qty != 0 ? ((decimal)pOutModel.Qty * -1) : 0;
                stock.Qty = Convert.ToDecimal(pOutModel.Qty);
            }
            else
            {
                stock.RcptNos = 1;
                stock.RcptQty = pOutModel.Qty != 0 ? ((decimal)pOutModel.Qty * -1) : 0;
                stock.Qty = Convert.ToDecimal(pOutModel.Qty);
            }

            stock.TransDateTime = DateTime.Now;

            stock.CreateDate = DateTime.Now;
            stock.CreateUser = userName;

            _db.StockTranses.Add(stock);


            return true;
        }


        //used while prod main table and save data from  prodOut
        public static bool StockTransProdProdOutEntry(ProdModel Model, ProdOutModel POut,
                   bool IsIssue, string tableName, string userName, KontoContext _db, decimal Qty)
        {
            StockTransModel stock = new StockTransModel();

            stock.CompanyId = Convert.ToInt16(Model.CompId);
            stock.YearId = Model.YearId != 0 ? (int)Model.YearId : 0;

            stock.BranchId = Model.BranchId != null ? Convert.ToInt32(Model.BranchId) : 0;
            //stock.GodownId = Model.StoreId != null ? (int)(Model.StoreId) : 0;

            if (Model.ColorId != null && Model.ColorId != 0)
                stock.ColorId = (int)Model.ColorId;

            if (Model.BatchId != null && Model.BatchId != 0)
                stock.BatchId = (int)Model.BatchId;

            if (Model.GradeId != null && Model.GradeId != 0)
                stock.GradeId = (int)Model.GradeId;

            if (Model.Cops != 0)
                stock.Cut = (int)Model.Cops;

            stock.RefId = Model.RowId;
            stock.MasterRefId = POut.RowId;
            stock.KeyFldValue = POut.Id;

            stock.VoucherDate = Model.VoucherDate != 0 ? (int)Model.VoucherDate : 0;

            stock.VoucherId = POut.VoucherId != null ? Convert.ToInt32(POut.VoucherId) : 0;
            stock.BillNo = Model.VoucherNo;
            stock.VoucherNo = Model.VoucherNo;
            stock.DivId = Model.DivId;
            stock.ItemId = (int)Model.ProductId;

            stock.TableName = tableName;
            stock.Narration = Model.Remark;

            if (IsIssue)
            {
                stock.IssueNos = 1;
                stock.IssueQty = Qty;
                stock.Qty = (-1 * (Qty));
                stock.Pcs = -1;
            }
            else
            {
                stock.RcptNos = 1;
                stock.RcptQty = Qty;
                stock.Qty = Qty;
                stock.Pcs = 1;
            }

            stock.TransDateTime = DateTime.Now;
            stock.CreateDate = DateTime.Now;
            stock.CreateUser = userName;

            _db.StockTranses.Add(stock);
            return true;
        }



        public static async Task<bool> StockTransProdEntry(ProdModel Model, int productId,
            decimal RcptQty, decimal IssueQty, decimal qty, int pcs,
                     bool IsIssue, string tableName, string userName, KontoContext _db)
            
        {


         
            StockTransModel stock = new StockTransModel();
           

            stock.CompanyId = Convert.ToInt16(Model.CompId);
            stock.YearId = Model.YearId != null ? (int)Model.YearId : 0;

            stock.BranchId = Model.BranchId != null ? Convert.ToInt32(Model.BranchId) : 0;
           

            if (Model.ColorId != null && Model.ColorId != 0)
                stock.ColorId = (int)Model.ColorId;

            if (Model.BatchId != null && Model.BatchId != 0)
                stock.BatchId = (int)Model.BatchId;

            if (Model.GradeId != null && Model.GradeId != 0)
                stock.GradeId = (int)Model.GradeId;

            if (Model.Cops != 0)
                stock.Cut = (int)Model.Cops;

            stock.RefId = Model.RowId;
            stock.MasterRefId = Model.RowId;
            stock.VoucherDate = Model.VoucherDate != 0 ? (int)Model.VoucherDate : 0;
           
            stock.VoucherId = Model.VoucherId != null ? Convert.ToInt32(Model.VoucherId) : 0;
            stock.BillNo = Model.VoucherNo;
            stock.VoucherNo = Model.VoucherNo;
            stock.DivId = Model.DivId;

           
            stock.ItemId = productId;

          

            stock.TableName = tableName;
            stock.KeyFldValue = Model.Id;
            stock.Narration = Model.Remark;

            if (IsIssue)
            {
                stock.IssueNos = pcs;
                stock.IssueQty = IssueQty;
                stock.Qty = (-1 * (qty));
                stock.Pcs = -1 * pcs;
            }
            else
            {
                stock.RcptNos = pcs;
                stock.RcptQty = RcptQty;
                stock.Qty = qty;
                stock.Pcs = pcs;
            }



            stock.TransDateTime = DateTime.Now;
            stock.CreateDate = DateTime.Now;
            stock.CreateUser = userName;

           
            _db.StockTranses.Add(stock);
          

           
            return true;

        }

    }
}
