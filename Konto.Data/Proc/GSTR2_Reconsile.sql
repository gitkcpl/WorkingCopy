IF object_id('[dbo].[GSTR2_Reconsile]') IS NULL 
EXEC ('CREATE PROC [dbo].[GSTR2_Reconsile] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[GSTR2_Reconsile]
	@CompanyId int,
	@FromDate int,
	@ToDate int, 
	@status varchar(50)=null,
 	@YearId INT
	
AS
BEGIN
	select 
	bm.Id ,
	bm.VoucherNo As BillNo,
	bm.Extra2 As Status,
	bm.RefNo as StatusDate,
	CONVERT(Date,Convert(varchar(8),bm.VoucherDate),112) as VoucherDate,
	bm.AccId,
	a.AccName As AccountName,
	bm.TotalQty,
	a.GstIn As GSTNo,
	bm.TotalAmount,
	sum(isnull(bt.Sgst,0)) As SGST,
	sum(isnull(bt.cgst,0)) As CGST,
	sum(isnull(bt.Igst,0)) As IGST,
	(bm.TotalAmount - sum(isnull(bt.Sgst,0))-sum(isnull(bt.cgst,0)) -sum(isnull(bt.Igst,0))) as TaxableValue ,
	case
		when v.VTypeId = 13 then 'Purchase'
		when v.VTypeId = 23 then 'Generale Expence'
		when v.VTypeId = 24 then 'CR/DR'
	end	 As BillType
	 from BillMain bm  
		left outer join BillTrans bt
			on bt.BillId = bm.Id
		left outer join Voucher v
			on v.Id =bm.VoucherId
		left outer join Acc  a
			on  a.Id = bm.AccId
			where (v.VTypeId = 13 or v.VTypeId = 23 or v.VTypeId = 24) and 
				bm.IsActive = 1 and bm.IsDeleted = 0 and 
				(bm.Extra2 = @status or @status is null) and 
				bm.CompId = @CompanyId and
				bm.YearId = @YearId and 
				bm.VoucherDate between @FromDate and @ToDate
				group by bm.Id ,
						bm.Extra2,
						bm.AccId,
	a.AccName ,
	bm.TotalQty,
	a.GstIn ,
	bm.TotalAmount,bm.RefNo,bm.VoucherDate,v.VTypeId,
	bm.VoucherNo
end
GO
