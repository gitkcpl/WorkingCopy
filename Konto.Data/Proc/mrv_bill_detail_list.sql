IF object_id('[dbo].[mrv_bill_detail_list]') IS NULL 
EXEC ('CREATE PROC [dbo].[mrv_bill_detail_list] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE dbo.mrv_bill_detail_list
@VTypeId int,
 @CompanyId int,
  @BranchId int,
 @YearId int,
 @FromDate int,
 @ToDate int
AS
BEGIN 
	  SELECT bm.BillType,
	  bm.BillNo, bm.VoucherNo,
      bm.RefNo ChallanNo,
	  v.VoucherName,
	  CASE WHEN bm.VoucherDate>10101 THEN ISNULL(CONVERT(Date,Convert(varchar(8),bm.VoucherDate),112),'')  end as BillDate,
	  ac.AccName Party,
	  p.ProductName,
	  p.HsnCode,
	  design.ProductName as DesignName,
	  col.ColorName,
	  bt.Pcs FinPcs,
	  bt.Qty FinMtrs,
	  bt.Rate, 
	  um.UnitName,
	  bt.Total,
	  bt.Disc DiscPer,
	  bt.DiscAmt,
	  bt.CgstPer,
	  bt.OtherAdd,
	  bt.OtherLess,
    bt.NetTotal- bt.Cgst-bt.Sgst-bt.Igst TaxableValue,
	  bt.Cgst CgstAmt,
	  bt.SgstPer,
	  bt.Sgst SgstAmt,
	  bt.IgstPer,
	  bt.Igst IgstAmt,
	  bt.NetTotal,
	  bt.AvgWt GreyMtrs,
	  bt.Width GreyPcs,
	  bt.Freight,bt.FreightRate
	  ,bm.Remarks,
	  bt.LotNo,bt.Remark,
	  bm.RefId as Id,
      bm.Id BillId,
      bm.RefVoucherId,
      bm.VoucherId,
	  bm.IsDeleted
 	  FROM BillMain bm
	  LEFT OUTER JOIN Acc ac on bm.AccId =ac.Id 
      LEFT OUTER JOIN Acc tr on bm.TransId = tr.Id
      LEFT OUTER JOIN Acc Agent on bm.AgentId = Agent.Id
      LEFT OUTER JOIN  Voucher v on bm.VoucherId =v.Id
      LEFT OUTER JOIN AccAddress adr on bm.DelvAdrId =adr.Id
      LEFT OUTER JOIN BillTrans bt on bt.BillId=bm.Id
      LEFT OUTER JOIN Product p on p.Id=bt.ProductId
      LEFT OUTER JOIN Product design on design.Id=bt.DesignId
      LEFT OUTER JOIN grade g on g.Id=bt.GradeId
      LEFT OUTER JOIN color col on col.Id=bt.ColorId
	  LEFT OUTER JOIN dbo.Uom um ON um.Id = bt.UomId
     
      WHERE bm.IsActive=1 and bm.IsDeleted=0 AND bt.IsDeleted =0 AND bm.CompId = @CompanyId AND bm.YearId = @YearId
      AND (bm.VoucherDate  between @FromDate and @ToDate) and v.VTypeId=@VTypeId
      ORDER BY bm.VoucherDate desc, bm.VoucherNo desc
    
END
GO

