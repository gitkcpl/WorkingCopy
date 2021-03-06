﻿ create PROCEDURE [dbo].[SJSummaryList]
	@fromDate int ,
	@ToDate int,
	@CompanyId int,
	@YearId int,
 	@VTypeId int,
	@BranchId int
AS
BEGIN 
   select  pd.ProductName,    sum(ct.Qty) TotalQty,
              sum(c.TotalAmount) TotalAmt
	from ChallanTrans ct
	left outer join Product pd on pd.Id=ct.ProductId
	left outer join Challan c on c.Id=ct.ChallanId 
	left outer join Voucher v on v.id=c.VoucherId
	where ct.IsActive=1 and ct.IsDeleted=0
	and v.VTypeId=  @VTypeId 
	 and c.CompId= @CompanyId
	 and c.BranchId=  @BranchId 
	and (c.VoucherDate between @fromDate and @ToDate )
	group by pd.ProductName
END