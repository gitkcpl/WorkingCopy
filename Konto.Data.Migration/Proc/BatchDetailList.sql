IF object_id('[dbo].[BatchDetailList]') IS NULL 
EXEC ('CREATE PROC [dbo].[BatchDetailList] AS SELECT 1 AS Id') 
GO 

ALTER  PROCEDURE [dbo].[BatchDetailList]
 	 @VTypeId int,
 @CompanyId int,
 @YearId int,
 @fromDate int,
 @ToDate int, 
 @BranchId int =0
AS
BEGIN

select 
d.DivisionName,p.VoucherNo ,v.VoucherName,
 convert(date,convert(varchar(8),p.voucherdate),112) VoucherDate,
pd.ProductName as Quality, c.ColorName as Shade,p.Remark,bt.LotNo,
TrP.ProductName as BatchProduct,bt.Ply as BatchPly,bt.[Description] ItemType
,bt.Per,bt.Remark as ItemRemark,
 p.Id,p.VoucherId
from JobCard p
left outer join JobCardTrans bt on bt.JobCardId =p.Id
left outer join Division d on d.Id=p.DivId
left outer join Voucher v on p.VoucherId=v.Id 
left outer join Product pd on pd.Id=p.ProductId
left outer join Product TrP on trp.Id=bt.ItemId
left outer join Color c on c.Id=p.ColorId
where p.IsActive = 1 and p.IsDeleted=0
	and v.VTypeId=@VTypeId
	and p.CompanyId=@CompanyId 
	and (p.VoucherDate between @FromDate and @ToDate)
	--and p.BranchId=@BranchId
 order by p.VoucherDate desc,p.id desc
END

GO



