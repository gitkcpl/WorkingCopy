IF object_id('[dbo].[ChallanList]') IS NULL 
EXEC ('CREATE PROC [dbo].[ChallanList] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[ChallanList]
@VTypeId int,
 @CompanyId int,
 @YearId int,
 @FromDate int,
 @ToDate int,
 @Type int =1,
 @BranchId int,
 @Deleted INT = 0
AS
BEGIN 
	 SELECT div.DivisionName, 
	 tp.TypeName ChallanType,
	 c.VoucherNo,
	 v.VoucherName,
	 ISNULL(CONVERT(Date,Convert(varchar(8),c.VoucherDate),112),'')  as ChallanDate, 
	 c.ChallanNo ChallanNo,
	 ac.PrintName Party,
	 del.AccName DelParty,
	 c.TotalPcs,
	-- c.TotalPcs - ISNULL(ct.Pcs,0) PendingPcs,
	 CAST(c.TotalQty AS NUMERIC(18,3)) AS TotalQty,
	-- c.TotalQty - ISNULL(ct.Qty,0) PendingQty, 
	 c.DName AS DriverName,c.DocDate AS Lr_Date
	 ,c.DocNo AS Lr_No,Agent.AccName AS Agent,
	 c.BillNo ,
	 tr.AccName as Transport,
	 CAST(c.TotalAmount AS NUMERIC(18,2) ) AS TotalAmount,
	 c.Remark,
	 c.CreateUser,
	 c.CreateDate,
	 c.ModifyUser,
	 c.ModifyDate,
	 c.VoucherId,c.Id, c.IsDeleted  
	FROM Challan c
	LEFT OUTER JOIN Acc ac on c.AccId =ac.Id 
    LEFT OUTER JOIN Acc tr on c.TransId = tr.Id
	LEFT OUTER JOIN Acc del on del.Id = c.DelvAccId
    LEFT OUTER JOIN Acc Agent on ac.AgentId = Agent.Id
    LEFT OUTER JOIN Voucher v on c.VoucherId =v.Id
    LEFT OUTER JOIN AccAddress adr on c.DelvAdrId =adr.Id 
    LEFT OUTER JOIN dbo.Division div ON div.Id = c.DivId
	LEFT OUTER JOIN dbo.TransType tp ON tp.Id = c.ChallanType
 --   LEFT OUTER JOIN (	
	--				SELECT ct.ChallanId, SUM(bt.Qty) Qty, SUM(bt.Pcs) Pcs FROM dbo.ChallanTrans ct
	--				LEFT OUTER JOIN dbo.BillTrans bt ON bt.RefTransId = ct.Id
	--				WHERE bt.Isdeleted = 0 AND bt.IsActive = 1 AND ct.Isdeleted = 0 AND ct.IsActive = 1
	--				GROUP BY ct.ChallanId
	--) ct ON ct.ChallanId = c.Id
   WHERE c.IsActive=1 and c.IsDeleted=@Deleted AND c.CompId = @CompanyId AND c.YearId = @YearId
   AND (c.VoucherDate  between @FromDate and @ToDate) and v.VTypeId=@VTypeId
   ORDER BY  c.VoucherDate desc, c.VoucherNo desc
  
END
GO

