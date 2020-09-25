CREATE PROCEDURE dbo.bill_analysis
 @VTypeId int,
 @CompanyId int,
 @BranchId int,
 @YearId int,
 @FromDate int,
 @ToDate int
AS
BEGIN 
	 select 
	 ISNULL(CONVERT(Date,Convert(varchar(8),b.VoucherDate),112),'')  as VoucherDate,
    fy.YearCode FY,
   'Q' + CONVERT(VARCHAR(1),DATEPART(QUARTER,CONVERT(Date,Convert(varchar(8),b.VoucherDate),112))) Qtr,
   DATENAME(MONTH, CONVERT(Date,Convert(varchar(8),b.VoucherDate),112)) [Month],
   DATENAME(weekday,CONVERT(Date,Convert(varchar(8),b.VoucherDate),112)) [Day],
	 ac.PrintName Party,
   bk.AccName Book, 
	 v.VoucherName VoucherName,
	 p.ProductName Product,
   col.ColorName Color,
   dm.ProductCode DesignNo,
   g.GradeName Grade,
	 bt.Qty,
   bt.Pcs,
	 Agent.PrintName Agent,
	 tr.PrintName as Transport
	 ,(bt.NetTotal) TotalAmount, 
	 bt.Id,bt.Id TransId,b.VoucherId,
   (bt.NetTotal-bt.Sgst-bt.Cgst-bt.Igst-bt.Cess) TaxableValue
    ,bt.Sgst,bt.Cgst,bt.Igst,bt.Cess
	 from dbo.BillMain b
     left outer join dbo.BillTrans bt ON bt.BillId = b.Id
	 left outer join dbo.Acc ac on b.AccId =ac.Id 
     LEFT outer  join dbo.Acc tr on b.TransId = tr.Id
     LEFT outer  join dbo.Acc Agent on b.AgentId = Agent.Id
    
     LEFT outer join dbo.Product p on p.Id=bt.ProductId
     LEFT outer join dbo.color col on col.Id=bt.ColorId
    LEFT OUTER JOIN dbo.Product dm ON bt.DesignId = dm.Id
    LEFT OUTER JOIN dbo.Grade g ON bt.GradeId = g.Id
     LEFT OUTER JOIN dbo.FinYear fy ON fy.Id = b.YearId
    
	  LEFT OUTER JOIN dbo.voucher v ON v.Id = b.VoucherId
    LEFT OUTER JOIN DBO.Acc bk ON bk.Id = b.BookAcId
		
 where b.IsActive=1 and b.IsDeleted=0 AND bt.IsActive =1 AND bt.IsDeleted=0
 and (b.VoucherDate  between @FromDate and @ToDate) and v.VTypeId=@VTypeId
 order by b.VoucherDate DESC,b.Id desc
    
END
GO