IF object_id('[dbo].[BatchList]') IS NULL 
EXEC ('CREATE PROC [dbo].[BatchList] AS SELECT 1 AS Id') 
GO 

ALTER PROCEDURE [dbo].[BatchList]
 	 @VTypeId int,
 @CompanyId int,
 @YearId int,
 @fromDate int,
 @ToDate int,
 @Deleted INT = 0,
 @BranchId int =0
AS
BEGIN

select 
d.DivisionName,p.VoucherNo ,v.VoucherName,
 convert(date,convert(varchar(8),p.voucherdate),112) VoucherDate,
pd.ProductName as Quality, c.ColorName as Shade,p.Remark,
 p.Id,p.VoucherId,p.CreateDate,P.CreateUser,
 p.ModifyDate,p.ModifyUser
from JobCard p
left outer join Division d on d.Id=p.DivId
left outer join Voucher v on p.VoucherId=v.Id 
left outer join Product pd on pd.Id=p.ProductId
left outer join Color c on c.Id=p.ColorId 
where p.IsActive = 1 and p.IsDeleted=@Deleted
	and v.VTypeId=@VTypeId
	and p.CompanyId=@CompanyId 
	and (p.VoucherDate between @FromDate and @ToDate)
	
 order by p.VoucherDate desc,p.Id desc
END
		
GO



