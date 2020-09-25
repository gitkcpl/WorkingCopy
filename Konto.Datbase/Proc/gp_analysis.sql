Create PROCEDURE dbo.gp_analysis
 @VTypeId int,
 @CompanyId int,
 @BranchId int,
 @YearId int,
 @FromDate int,
 @ToDate int
AS
BEGIN 
	select 
	 ISNULL(CONVERT(Date,Convert(varchar(8),c.VoucherDate),112),'')  as VoucherDate,
    fy.YearCode FY,
   'Q' + CONVERT(VARCHAR(1),DATEPART(QUARTER,CONVERT(Date,Convert(varchar(8),c.VoucherDate),112))) Qtr,
   DATENAME(MONTH, CONVERT(Date,Convert(varchar(8),c.VoucherDate),112)) [Month],
   DATENAME(weekday,CONVERT(Date,Convert(varchar(8),c.VoucherDate),112)) [Day],
	 ac.PrintName Supplier,
   b.Book, 
	 v.VoucherName VoucherName,
	 p.ProductName Product,
    col.ColorName Color,
	 ct.Qty Meters,
    ct.Pcs Pcs,
	 Agent.PrintName Agent,
	 tr.PrintName as Transport
	 ,(b.TaxableValue+b.Sgst+b.Cgst+b.Igst) TotalAmount, 
	  c.Id,ct.Id as TransId,c.VoucherId,
    b.TaxableValue,b.Sgst,b.Cgst,b.Igst,b.BillId
	 from Challan c
	 left outer join Acc ac on c.AccId =ac.Id 
     LEFT outer  join Acc tr on c.TransId = tr.Id
     LEFT outer  join Acc Agent on c.AgentId = Agent.Id
     LEFT outer join  Voucher v on c.VoucherId =v.Id
     LEFT outer join ChallanTrans ct on ct.ChallanId=c.Id
     LEFT outer join Product p on p.Id=ct.ProductId
     LEFT outer join color col on col.Id=ct.ColorId
     LEFT OUTER JOIN FinYear fy ON fy.Id = c.YearId
    
  left outer join  (select b.VoucherNo as BillNo,b.VoucherDate as BillDate,b.Id as BillId,bt.RefTransId,
    SUM(bt.Sgst)Sgst,SUM(bt.Cgst) Cgst, SUM(bt.Igst) Igst,SUM(bt.NetTotal-bt.Sgst-bt.Cgst-bt.Igst)TaxableValue,
    a.AccName Book
			from BillTrans bt
			left outer join BillMain b on b.Id =bt.BillId
    LEFT OUTER JOIN voucher v ON v.Id = b.VoucherId
    LEFT OUTER JOIN Acc a ON a.Id = b.BookAcId
			 where b.IsActive=1 and b.IsDeleted=0  AND bt.IsDeleted=0 AND v.VTypeId=36
      group by b.Id ,b.VoucherNo,b.VoucherDate,bt.RefTransId,a.AccName
			) b on b.RefTransId =ct.Id  

 where c.IsActive=1 and c.IsDeleted=0 
 and (c.VoucherDate  between @FromDate and @ToDate) and v.VTypeId=@VTypeId
 order by c.VoucherDate DESC,c.Id desc
    
END

