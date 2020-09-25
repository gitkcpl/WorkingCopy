CREATE PROCEDURE [dbo].[GPList]
@VTypeId int,
 @CompanyId int,
  @BranchId int,
 @YearId int,
 @FromDate int,
 @ToDate int
AS
BEGIN 
	select 
	-- c.VoucherDate ChlnDate,
	 ISNULL(CONVERT(Date,Convert(varchar(8),c.VoucherDate),112),'')  as VoucherDate,
	 c.RcdDate ChallanDate,
	-- ISNULL(CONVERT(Date,Convert(varchar(8),c.VoucherDate),112),'')  as ChallanDate,
	 c.VoucherNo VoucherNo,
	 c.ChallanNo,
	 c.BillNo,
	 ac.PrintName Supplier,
	 v.VoucherName VoucherName,
	 p.ProductName Product,col.ColorName Color,
	 ct.LotNo LotNo,
	 ct.RefNo Panna,
	 ct.CessPer Folding,
	 g.GradeName Grade
	 ,ct.Qty,
	 Agent.PrintName,
	 c.ChallanType,c.DName,c.DocDate
	 ,c.DocNo,c.IsActive,c.IsDeleted,c.TotalPcs,c.TotalQty,tr.PrintName as Transport
	 , c.TotalAmount, o.OrderNo OrderNo, 
	 CASE when o.OrdDate >10101 then ISNULL(CONVERT(Date,Convert(varchar(8),o.OrdDate),112),'') end  as OrderDate,
	  case when b.BillDate>10101 then ISNULL(CONVERT(Date,Convert(varchar(8),b.BillDate),112),'')  end as InvoiceDate,
	  c.Id,ct.Id as TransId,c.VoucherId,
    b.TaxableValue,b.Sgst,b.Cgst,b.Igst,b.BillId
	--select *
	 from Challan c
	 left outer join Acc ac on c.AccId =ac.Id 
     LEFT outer  join Acc tr on c.TransId = tr.Id
     LEFT outer  join Acc Agent on c.AgentId = Agent.Id
     LEFT outer join  Voucher v on c.VoucherId =v.Id
     LEFT outer join ChallanTrans ct on ct.ChallanId=c.Id
     LEFT outer join Product p on p.Id=ct.ProductId
     LEFT outer join grade g on g.Id=ct.GradeId
     LEFT outer join color col on col.Id=ct.ColorId
     LEFT outer join (select o.VoucherNo as OrderNo,o.VoucherDate as OrdDate,Id as OrdId 
			from Ord o where o.IsActive=1 and o.IsDeleted=0 group by o.Id ,o.VoucherNo,o.VoucherDate
			)o on o.OrdId=ct.MiscId 
  left outer join  (select b.VoucherNo as BillNo,b.VoucherDate as BillDate,b.Id as BillId,bt.RefTransId,
    SUM(bt.Sgst)Sgst,SUM(bt.Cgst) Cgst, SUM(bt.Igst) Igst,SUM(bt.NetTotal-bt.Sgst-bt.Cgst-bt.Igst)TaxableValue
			from BillTrans bt
			left outer join BillMain b on b.Id =bt.BillId
    LEFT OUTER JOIN voucher v ON v.Id = b.VoucherId
			 where b.IsActive=1 and b.IsDeleted=0  AND bt.IsDeleted=0 AND v.VTypeId=36
      group by b.Id ,b.VoucherNo,b.VoucherDate,bt.RefTransId
			) b on b.RefTransId =ct.Id  

 
 where c.IsActive=1 and c.IsDeleted=0 
 and (c.VoucherDate  between @FromDate and @ToDate) and v.VTypeId=@VTypeId
 order by c.VoucherDate DESC,c.Id desc
    
END


