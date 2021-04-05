IF object_id('[dbo].[MiList]') IS NULL 
EXEC ('CREATE PROC [dbo].[MiList] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE dbo.MiList
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
   c.TotalPcs - ISNULL(mr.Pcs,0) PendPcs,
   CAST(c.TotalQty AS NUMERIC(18,3)) AS TotalMtrs,
    c.TotalQty - ISNULL(mr.Qty,0) PendMtrs,
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
  LEFT OUTER JOIN (SELECT mrt.MiscId,mrt.RefVoucherId,SUM(mrt.Pcs) Pcs,SUM(mrt.Qty)Qty FROM Challan mr 
    INNER JOIN ChallanTrans  mrt ON mr.Id = mrt.ChallanId
    WHERE mr.IsDeleted = 0 AND mrt.IsDeleted=0
    GROUP BY mrt.MiscId,mrt.RefVoucherId)mr ON c.Id = mr.MiscId AND c.VoucherId = mr.RefVoucherId
   WHERE c.IsActive=1 and c.IsDeleted=@Deleted AND c.CompId = @CompanyId AND c.YearId = @YearId
   AND (c.VoucherDate  between @FromDate and @ToDate) and v.VTypeId=@VTypeId
   ORDER BY  c.VoucherDate desc, c.VoucherNo desc
  
END
GO