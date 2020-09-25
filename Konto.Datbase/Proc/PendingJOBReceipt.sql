
GO
/*-- =============================================
-- Author:		<Author,,Name>
  Create date: <Create Date,,>
  Description:	<Description,,>
  exec PendingJOBReceipt 1,25,6,'Issue for Job'
   
  select * from challan 
  DELETE FROM Challan where id=
-- =============================================*/

create PROCEDURE [dbo].[PendingJOBReceipt]
 	@CompanyId int,
	@AccountId int,
	@VoucherTypeID int,
	@FromDate int,
	@ToDate int
AS
BEGIN
	select ct.Id as TransId,c.Id ,c.VoucherId ,c.VoucherNo ,

p.ProductName as Product,ct.ProductId,ct.ColorId,cast(ct.Qty  as numeric(18,2)) as Qty,
0.00 as PendQty,isnull(pen.Qty,0.00) RcptQty,cast(ct.rate as numeric(18,2)) as Rate,
ct.Gross,cast(c.TotalQty as numeric(18,2)) as TotalQty,c.TotalAmount as TotalAmt,
ct.DesignId ,ct.GradeId,0.00 AvgWt,cast(isnull(ct.Cops,0.00) as numeric(18,2)) Cut,
0.00 as Width,ct.LotNo ,ct.UomId,cast(ct.Total as numeric(18,2)) as Total, 
	c.VoucherDate OrdDate,
	cast((c.TotalQty-isnull(pen.Qty,0))  as numeric(18,2)) as PendingQty,
	isnull(pen.Qty,0) as IssueQty, 

	isnull(ct.CgstPer,0.00) Cgst,isnull(ct.Cgst,0.00) CgstAmt,
	
	isnull(ct.IgstPer,0.00) Igst,isnull(ct.Igst,0.00) IgstAmt,
	ct.Pcs ,
	cast(isnull(ct.SgstPer ,0.00) as numeric(18,2)) as Sgst
	,cast(isnull(ct.Sgst,0.00) as numeric(18,2)) SgstAmt

	from Challan c
	left outer join ChallanTrans ct on ct.ChallanId=c.Id
	left outer join Product p on p.Id=ct.ProductId
	left outer join Division d on d.Id=c.DivId 
	left outer join Voucher v on v.Id=c.VoucherId
	left outer join (select sum(Qty)as Qty,sum(Pcs) as Pcs,ProductId , ct.RefId from ChallanTrans ct
			where ct.IsActive=1 and ct.IsDeleted=0
			group by ct.RefId,ProductId
	)pen on pen.RefId=ct.Id
	 
	where v.VTypeId=@VoucherTypeID	 and c.AccId=@AccountId	and ((isnull(c.TotalQty,0)-isnull(pen.Qty,0)) >0) 
	and c.IsActive =1 and c.IsDeleted=0
END

 
