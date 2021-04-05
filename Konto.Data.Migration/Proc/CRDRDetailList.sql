IF object_id('[dbo].[CRDRDetailList]') IS NULL 
EXEC ('CREATE PROC [dbo].[CRDRDetailList] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[CRDRDetailList]
@VTypeId int,
 @CompanyId int,
  @BranchId int,
 @YearId int,
 @FromDate int,
 @ToDate int
AS
BEGIN 
	  SELECT bm.BillType,
	  bm.Extra1 Against,
	  bm.VoucherNo,
	  CASE WHEN bm.VoucherDate>10101 THEN ISNULL(CONVERT(Date,Convert(varchar(8),bm.VoucherDate),112),'')  end as Date,
	  ac.AccName PARTY,
	  bk.AccName Book,
	  ISNULL(Agent.AccName,ag.AccName) Agent,
	  bm.SpecialNotes Reason, 
	  bt.Remark Description,
	  bt.HsnCode,
	  bt.Qty,
	  bt.Rate, 
	  um.UnitName,
	  bt.Total GrossAmt,
	  bt.CgstPer,
	  bt.Cgst CgstAmt,
	  bt.SgstPer,
	  bt.Sgst SgstAmt,
	  bt.IgstPer,
	  bt.Igst IgstAmt,
	  bt.NetTotal,
	  bm.Remarks, 
	  bm.VoucherId,
      bm.Id,
	  bm.IsDeleted
 	  FROM BillMain bm
	  LEFT OUTER JOIN Acc ac on bm.AccId =ac.Id 
	  LEFT OUTER JOIN dbo.Acc bk ON bk.Id = bm.BookAcId
      LEFT OUTER JOIN Acc tr on bm.TransId = tr.Id
      LEFT OUTER JOIN Acc Agent on bm.AgentId = Agent.Id
	  LEFT OUTER JOIN dbo.Acc ag ON ag.Id = ac.AgentId
      LEFT OUTER JOIN Voucher v on bm.VoucherId =v.Id
      LEFT OUTER JOIN AccAddress adr on bm.DelvAdrId =adr.Id
      LEFT OUTER JOIN BillTrans bt on bt.BillId=bm.Id
      LEFT OUTER JOIN Product p on p.Id=bt.ProductId
      LEFT OUTER JOIN Product design on design.Id=bt.DesignId
      LEFT OUTER JOIN grade g on g.Id=bt.GradeId
      LEFT OUTER JOIN color col on col.Id=bt.ColorId
	  LEFT OUTER JOIN dbo.Uom um ON um.Id = bt.UomId 
      WHERE bm.IsActive=1 and bm.IsDeleted=0 AND bt.IsDeleted = 0 AND bm.CompId = @CompanyId AND bm.YearId = @YearId
      AND (bm.VoucherDate  between @FromDate and @ToDate) and v.VTypeId=@VTypeId
      ORDER BY bm.VoucherDate DESC, bm.VoucherNo DESC
    
END
GO

