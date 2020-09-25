CREATE   PROC [dbo].[docDetailList]
    @fromdate INT = 20170701 ,
    @todate INT = 20170731 ,
    @companyid INT = 0,
	@NOB varchar(50)
   AS
    BEGIN
	select bm.VoucherNo,
convert(date,convert(varchar(8),bm.voucherDate),112) as VoucherDate ,
bm.BillNo,a.AccName Party,bm.SpecialNotes, 
a.GstIn ,ISNULL(bt.Qty, bm.TotalQty)TotalQty
,ISNULL(bt.Pcs, bm.TotalPcs) TotalPcs,isnull(bt.TdsAmt, bm.TdsAmt)TdsAmt
,isnull(bt.NetTotal, bm.TotalAmount)TotalAmount
,isnull(bt.Total, bm.GrossAmount) GrossAmount,
isnull(bt.TaxableValue,0.00)TaxableValue,
bt.GSTRate
from BillMain bm
left outer join voucher v on v.Id=bm.VoucherId
LEFT OUTER JOIN Acc a on a.Id=bm.AccId
left outer join (select bt.BillId,sum(isnull(bt.Igst,0)) Igst
					,sum(isnull(bt.Sgst,0)) Sgst,
					sum(isnull(bt.Cgst,0)) Cgst,
					sum(isnull(bt.Qty,0)) Qty,
					sum(isnull(bt.NetTotal,0)) NetTotal,
					sum(isnull(bt.Total,0))Total
					,sum(isnull(cast(bt.Pcs as numeric(18,4)),0.00)) Pcs
					,sum(isnull(bt.TdsAmt,0)) TdsAmt,
					CASE WHEN sum(bt.IgstPer) = 0 THEN sum(isnull(bt.CgstPer,0)) + sum(isnull(bt.SgstPer,0)) ELSE sum(isnull(bt.IgstPer,0)) END  AS GSTRate,
					SUM(isnull(bt.NetTotal,0)) - SUM(isnull(bt.Sgst,0)) - SUM(isnull(bt.Cgst,0)) - SUM(isnull(bt.Igst,0)) AS TaxableValue
					 from BillTrans bt 
				where bt.IsActive=1 and bt.IsDeleted =0
				group by bt.BillId)bt on bt.billid=bm.id
where (bm.VoucherDate between @fromDate and @todate)
and bm.CompId=@companyid
and bm.IsActive=1 and bm.IsDeleted=0
and
 v.VTypeId =case when @NOB='Invoices for outward supply' then  12 
				when @NOB='Credit Note' AND bm.Extra1='SALE'  AND bm.BillType='CREDIT NOTE' then 24
				when @nob='Sale Return'   then 19
				when @NOB= 'Debit Note'   AND bm.Extra1='SALE'  AND bm.BillType='DEBIT NOTE' then 24
else  0
end
 -- and bm.BillType='DEBIT NOTE' 
end