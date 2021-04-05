

IF object_id('[dbo].[BrokerageDetailList]') IS NULL 
EXEC ('CREATE PROC [dbo].[BrokerageDetailList] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[BrokerageDetailList]
@VTypeId int,
 @CompanyId int,
  @BranchId int,
 @YearId int,
 @FromDate int,
 @ToDate int
AS
BEGIN 
	  SELECT 
	  CASE WHEN bm.VoucherDate>10101 THEN ISNULL(CONVERT(Date,Convert(varchar(8),bm.VoucherDate),112),'')  end as VoucherDate,	
	  bm.VoucherNo,
	  bm.Id,
	  bm.VoucherId,
	  v.VoucherName,
	  bacc.AccName As BookName,
	  agent.AccName As BrokerageParty,
	  tdsacc.AccName as TdsAccount,
	  ac.AccName PARTY,
	  bm.tdsper As TDSPer,
	  bm.TdsAmt as TDSAmount,
	  bm.CustomP as BrokerPer,
	  bm.TotalAmount Amount,
	  bm.Remarks
 	  FROM BillMain bm
	  LEFT OUTER JOIN Acc ac on bm.AgentId =ac.AgentId 
      LEFT OUTER JOIN Acc tr on bm.TransId = tr.Id
	  left outer join acc bacc on bacc.id=bm.bookAcId
      LEFT OUTER JOIN Acc Agent on bm.AccId = Agent.Id
	  left outer join acc tdsacc on tdsacc.Id=HasteId
      LEFT OUTER JOIN  Voucher v on bm.VoucherId =v.Id
      WHERE 
	  bm.IsActive=1 and bm.IsDeleted=0 AND bm.CompId = @CompanyId AND bm.YearId = @YearId
     AND (bm.VoucherDate  between @FromDate and @ToDate) and v.VTypeId=@VTypeId
	-- v.VTypeId=49
      ORDER BY bm.VoucherDate desc, bm.VoucherNo desc
    
END
GO

