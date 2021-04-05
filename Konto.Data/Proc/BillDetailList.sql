IF object_id('[dbo].[BillDetailList]') IS NULL 
EXEC ('CREATE PROC [dbo].[BillDetailList] AS SELECT 1 AS Id') 
GO 

ALTER PROCEDURE [dbo].[BillDetailList]
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
	  v.VoucherName,
	  CASE WHEN bm.VoucherDate>10101 THEN ISNULL(CONVERT(Date,Convert(varchar(8),bm.VoucherDate),112),'')  end as BillDate,
	  ac.AccName PARTY,
	  p.ProductName,
	  p.HsnCode,
	  design.ProductName as DesignName,
	  col.ColorName,
	  bt.Pcs,
	  bt.Cut,
	  bt.Qty,
	  bt.Rate, 
	  um.UnitName,
	  bt.Total GrossAmt,
	  bt.Disc DiscPer,
	  bt.DiscAmt,
	  bt.CgstPer,
	  bt.OceanFrt,
	  bt.OtherAdd,
	  bt.OtherLess,
	  bt.NetTotal- bt.sgst-bt.cgst-bt.igst-bt.cess as TaxableAmt,
	  bt.Cgst CgstAmt,
	  bt.SgstPer,
	  bt.Sgst SgstAmt,
	  bt.IgstPer,
	  bt.Igst IgstAmt,
	  bt.NetTotal,
	  bt.AvgWt,
	  bt.Cess CessAmount,
	  bt.CustomD CustomDuty,
	  bt.Freight,bt.FreightRate,
	  bm.Currency,
	  bm.DName Driver,
	  bm.DocDate LrDate,
	  bm.DocNo LrNo,
	  bm.Duedays,
	  bm.EwayBillNo,
	  bm.ExchRate,
	  bm.GrossAmount,
	  bm.Itc,bm.ModeofTrans,
	  bm.PortCode,bm.RcdDate,
	  bm.Rcm,
	  st.StateName as Pos,
	  bm.RefNo,bm.Remarks,bm.RequireDate,
	  bm.SpecialNotes, 
	  bm.VehicleNo,
	  bm.VoucherId,
	  bt.LotNo,bt.Remark,
	  bt.TcsAmt,bt.TcsPer,bt.Width
      , bm.Id,
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
	  LEFT OUTER JOIN dbo.[State] st on st.Id = bm.StateId
      LEFT OUTER JOIN (SELECT o.VoucherNo as ChallanNo,o.VoucherDate as ClnDate,Id as ChallanId 
			           FROM Challan o where o.IsActive=1 and o.IsDeleted=0 group by o.Id ,o.VoucherNo,o.VoucherDate
			          )o on o.ChallanId=bt.RefId
 
      WHERE bm.IsActive=1 and bm.IsDeleted=0 AND bt.IsDeleted =0 AND bm.CompId = @CompanyId AND bm.YearId = @YearId
      AND (bm.VoucherDate  between @FromDate and @ToDate) and v.VTypeId=@VTypeId
      ORDER BY bm.VoucherDate desc, bm.VoucherNo desc
    
END
GO

