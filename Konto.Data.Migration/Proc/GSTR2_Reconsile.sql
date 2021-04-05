IF object_id('[dbo].[gstr2a_reco]') IS NULL 
EXEC ('CREATE PROC [dbo].[gstr2a_reco] AS SELECT 1 AS Id') 
GO

alter proc dbo.gstr2a_reco
@fromdate int=0,
@todate int=0,
@compid int=0,
@match nvarchar(1)='Y',
@prd nvarchar(25)='AUG-20'
as
BEGIN
select
bm.Id,
bm.AccId,
ac.GstIn,
ac.AccName,
bm.BillNo InvoiceNo,
case when bm.RcdDate is null then ISNULL(CONVERT(Date,Convert(varchar(8),bm.Voucherdate),112),'')
else bm.rcdDate end as InvoiceDate,
bm.TotalAmount InvoiceValue,
bb.Cgst,bb.Sgst,bb.Igst,bb.Cess,bb.Taxable,
gt.InvoiceNo as PInvoiceNo,
gt.InvoiceDate PInvoiceDate,
gt.InvoiceValue PInvoiceValue,
gt.Taxable PTaxable,
gt.Cgst PCgst,
gt.Sgst PSgst,
gt.Igst PIgst,
gt.Cess PCess,
bm.TotalAmount- gt.InvoiceValue DInvoiceValue,
bb.Taxable - gt.Taxable DTaxable,
bb.Sgst - gt.Sgst  DSgst,
bb.Cgst-gt.Cgst DCgst,
bb.Igst - gt.Igst DIgst,
bb.Cess-gt.Cess DCess,
v.VTypeId,vt.TypeName VoucherType
 from dbo.BillMain bm
left outer join dbo.Acc ac on ac.id = bm.AccId
left outer join (select bt.BillId,sum(bt.cgst)Cgst,
sum(bt.sgst)Sgst,sum(bt.igst) Igst,sum(bt.cess)Cess,
sum(bt.nettotal- bt.cgst-bt.sgst-bt.igst-bt.cess)Taxable from BillTrans bt 
inner join BillMain b on bt.BillId = b.Id
group by bt.BillId)bb on bb.BillId = bm.Id
left outer join dbo.Voucher v on v.Id = bm.VoucherId
LEFT outer join dbo.Vouchertype vt on v.Vtypeid = vt.id
LEFT OUTER JOIN dbo.gstr2a_dump gt on gt.BillId = bm.Id
where (v.VTypeId in (13,23,36,40,41) or (v.VTypeId=24 and ISNULL(bm.BillType,'CREDIT NOTE') ='CREDIT NOTE'
AND ISNULL(bm.Extra1,'X')='PURCHASE'))
 and bm.compid=@compid  and bm.IsDeleted=0 and bm.IsActive=1 and
((@match ='Y' and gt.BillId is not null and gt.FPrd=@prd) or
 (@match='N' and gt.Id is null and bm.VoucherDate between @fromdate and @todate))
END


Go
