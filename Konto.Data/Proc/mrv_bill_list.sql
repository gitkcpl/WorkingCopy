IF object_id('[dbo].[mrv_bill_list]') IS NULL 
EXEC ('CREATE PROC [dbo].[mrv_bill_list] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE dbo.mrv_bill_list
 @VTypeId int,
 @CompanyId int,
 @BranchId int,
 @YearId int,
 @FromDate int,
 @ToDate INT,
 @Cancelled INT= 1,
 @Deleted INT = 0

AS
BEGIN  
	  SELECT  bm.VoucherNo,
	  v.VoucherName,
	  CASE WHEN bm.VoucherDate>10101 THEN ISNULL(CONVERT(Date,Convert(varchar(8),bm.VoucherDate),112),'') END AS BillDate,
	  bm.BillNo,
	  bk.AccName Book,
	  ac.AccName Party,
	  ISNULL(ag.AccName,Agent.AccName) Agent,
	  bm.TotalPcs FinPcs,bm.TotalQty FinMtrs,
    bt.GreyPcs,
    bt.GreyMtrs,
	  bm.TotalAmount-bt.Sgst-bt.Cgst-bt.Igst AS TaxableAmt ,
	  bt.Cgst,
	  bt.Sgst,
	  bt.Igst,
	  bm.TotalAmount BillAmt,
	  ISNULL(bl.Pay,0) PaidAmt,
    bm.TotalAmount - ISNULL(bl.Pay,0) PendAmt,
	  CASE WHEN bm.TotalAmount-bl.Pay- bl.Rg = 0 THEN 'PAID' ELSE 'UNPAID' END AS Status,
	  bm.BillType,
	  bm.DName Driver,
	  bm.DocDate LrDate,
	  bm.DocNo LrNo,
	  bm.Duedays,
    bm.Itc,bm.RcdDate,
	  bm.Rcm,
	  bm.RefNo,
    bm.Remarks,
	  bm.RequireDate ,
	  bm.TcsAmt TotalTcsAmt,
	  bm.TdsAmt TotalTdsAmt,
	  bm.VehicleNo,
	  bm.CreateUser,
	  bm.CreateDate,
	  bm.ModifyUser,
	  bm.ModifyDate,
	  bm.VoucherId,
	  bm.IsActive,
	  bm.IsDeleted,bm.RefId Id,bm.RefVoucherId,
    bm.Id BillId
 	 FROM dbo.BillMain bm
	 LEFT OUTER JOIN (SELECT BillId, SUM(AvgWt)GreyMtrs,SUM(Width)GreyPcs,   SUM(DiscAmt) disc, SUM(OtherAdd) AS OtherA, SUM(OtherLess) AS OtherL,
      SUM(Cgst) Cgst, SUM(Sgst) Sgst, SUM(Igst) Igst FROM dbo.BillTrans 
      WHERE IsDeleted=0 AND IsActive=1
      GROUP BY BillId) bt ON bt.BillId = bm.Id
	 LEFT OUTER JOIN  dbo.Acc ac on bm.AccId =ac.Id 
	 LEFT OUTER JOIN dbo.Acc bk ON bk.Id = bm.BookAcId
     LEFT OUTER JOIN dbo.Acc Agent on ac.AgentId = Agent.Id
	 LEFT OUTER JOIN dbo.Acc ag ON ag.Id = bm.AgentId
     LEFT OUTER JOIN  dbo.Voucher v on bm.VoucherId =v.Id
     LEFT OUTER JOIN dbo.AccAddress adr on bm.DelvAdrId =adr.Id
	 LEFT OUTER JOIN (SELECT BillId, 
				SUM(CASE WHEN TransType = 'Payment' THEN Amount + ISNULL(Adla1,0) + ISNULL(Adla2,0) +ISNULL(Adla3,0) +ISNULL(Adla4,0) +ISNULL(Adla5,0) +ISNULL(Adla6,0) +ISNULL(Adla7,0) +ISNULL(Adla8,0) +ISNULL(Adla9,0) +ISNULL(Adla10,0) ELSE 0 END) Pay , 
				SUM(CASE WHEN TransType = 'Return' THEN Amount + ISNULL(Adla1,0) + ISNULL(Adla2,0) +ISNULL(Adla3,0) +ISNULL(Adla4,0) +ISNULL(Adla5,0) +ISNULL(Adla6,0) +ISNULL(Adla7,0) +ISNULL(Adla8,0) +ISNULL(Adla9,0) +ISNULL(Adla10,0) ELSE 0 END) Rg  FROM dbo.BtoB GROUP BY BillId) bl ON bl.BillId = bm.Id
 
    WHERE (bm.IsDeleted=@Deleted AND bm.IsActive = @Cancelled) AND bm.CompId = @CompanyId AND bm.YearId = @YearId
    AND (bm.VoucherDate  between @FromDate and @ToDate) and v.VTypeId=@VTypeId 

    ORDER by bm.VoucherDate DESC, bm.VoucherNo DESC
    
END
GO
