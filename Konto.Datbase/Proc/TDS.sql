CREATE PROCEDURE [dbo].[TDS]
 @CompanyId INT ,
 @FromDate  INT ,
 @ToDate INT,
 @TDSAcID INT = 0,
 @YearId INT
as
BEGIN
select  case when br.BillNo is null then b.BillNo else br.BillNo end As BillNo,b.RefId,
		 case  when isnull(br.voucherdate,0) = 0 then ISNULL(CONVERT(Date,Convert(varchar(8),b.VoucherDate),112),'')  else  ISNULL(CONVERT(Date,Convert(varchar(8),br.VoucherDate),112),'') end as ChallanDate,
		case when b.Credit = 0.00 then b.Debit else b.Credit end  As TdsAmt,
		pa.PanNo,
		pa.AccName Party ,
		Tds.AccName TDSAccount,
	    case when v.VoucherName is null then vc.VoucherName else v.VoucherName end As voucherName,
		n.Descr,
		case when br.BillAmt IS NULL then b.BilllAmount else br.BillAmt end as TotalAmount,
		case when br.BillAmt is null then CAST(((case when b.Debit = 0.00 then b.Credit else Credit end)* 100/b.BilllAmount) as Decimal(10,2))else cast(((case when b.Debit = 0.00 then b.Credit else Credit end)* 100/br.BillAmt ) as Decimal(10,2)) end as TdsPer
		,case when br.BillAmt is null then CAST(b.BilllAmount-((case when b.Debit = 0.00 then b.Credit else Credit end)) as Decimal(10,2)) else cast(br.BillAmt-((case when b.Debit = 0.00 then b.Credit else Credit end) ) as Decimal(10,2)) end as Payable
		,Tds.Id as HasteId
		,pa.Id As AccountID,
		ag.GroupName
		   from LedgerTrans b
left outer join Acc Tds
	on Tds.Id = b.AccountId 
left outer join BillRef br
	on br.RowId=b.TransCode
left  outer join Acc pa
	on b.RefAccountId=pa.id
left outer join AcGroup ag
	on tds.GroupId = ag.Id
left outer join Voucher vc
	on vc.Id = b.VoucherId
left outer join Voucher v
	on v.Id = br.BillVoucherId
left outer join Nop n
	on n.Id = pa.NopId  where  --pa.TdsReq = 'YES'  AND 
	( @TDSAcID=0 or Tds.Id =@TDSAcID) AND (br.VoucherDate 
			between @FromDate and @ToDate or b.VoucherDate between @FromDate and @ToDate) AND (b.CompanyId = @CompanyId or br.CompanyId =@CompanyId)
			AND b.YearId =@YearId AND b.IsActive = 1 AND b.IsDeleted = 0 and (b.YearId = @YearId or br.YearId=@YearId)
--select b.BillNo,b.Id As BillID,
--		ISNULL(CONVERT(Date,Convert(varchar(8),b.VoucherDate),112),'') as ChallanDate,
--		b.HasteId,
--		b.TotalAmount,
--		a1.PanNo,
--		b.TdsPer,
--		b.TdsAmt,
--		(b.TotalAmount - b.TdsAmt) As Payable,
--		a.AccName As TDSAccount,
--		a1.AccName Party,
--		a1.Id As AccountID,
--		ag.GroupName,
--		v.VoucherName,
--		n.Descr
--	from BillMain b
--			left outer join  acc a1
--					on a1.Id = b.AccId
--			left outer join Acc a
--					on a.Id=b.HasteId
--			left outer join AcGroup ag
--				    on ag.id=a.GroupId
--			left outer join Voucher v
--					on v.Id = b.VoucherId 
--			left outer join Nop n 
--					on n.Id = a1.NopId
--			where  a1.TdsReq = 'YES' AND b.HasteId >0 AND ( @TDSAcID=0 or a.Id =@TDSAcID) AND b.VoucherDate 
--			between @FromDate and @ToDate AND b.CompId = @CompanyId 
--			AND b.YearId =@YearId AND b.IsActive = 1 AND b.IsDeleted = 0
END
GO