IF object_id('[dbo].[HsnSmryDetail]') IS NULL 
EXEC ('CREATE PROC [dbo].[HsnSmryDetail] AS SELECT 1 AS Id') 
GO

ALTER  PROCEDURE [dbo].[HsnSmryDetail]
    @CompanyId INT =1,
	@FromDate  INT,
	@ToDate INT, 
	@YearId int,
	@HsnCode varchar(200),
	@UnitId int
	 
AS
BEGIN
select ac.GstIn,
--CASE WHEN ac.VatTds='ECOM' THEN  ac.GstIn ELSE '' END GstIn,
 voucher.VoucherName VoucherName, bm.VoucherNo VoucherNo,bm.VDate VoucherDate,
ISNULL(bm.BillNo,bm.VoucherNo) BillNo ,ac.AccName Party,p.ProductCode,p.ProductName,
isnull(p.HsnCode,bt.hsncode) HsnCode,
bt.TdsAmt ,

case when voucher.vtypeid=19 then (-1 *bt.NetTotal)
when voucher.vtypeid=24 and bm.BillType='CREDIT NOTE' AND ISNULL(bm.Extra1,'X')='SALE'  then (-1 *bt.NetTotal)
else bt.NetTotal end  TotalAmount,


CASE WHEN bt.IgstPer = 0 THEN bt.CgstPer + bt.SgstPer ELSE bt.IgstPer END  AS GSTRate,

bt.NetTotal - (bt.Sgst) - (bt.Cgst) - (bt.Igst) AS TaxableValue, 

case when voucher.vtypeid=19 then (-1 * isnull(bt.Qty,0))
when voucher.vtypeid=24 and bm.BillType='CREDIT NOTE' and ISNULL(bm.Extra1,'X')='SALE' then (-1 *isnull(bt.Qty,0))
else isnull(bt.Qty,0) end TotalQty

,case when voucher.vtypeid=19 then (-1 *bt.Rate)
when voucher.vtypeid=24 and bm.BillType='CREDIT NOTE' AND ISNULL(bm.Extra1,'X')='SALE' then (-1 *bt.Rate)
else bt.Rate end Rate,

case when voucher.vtypeid=19 then (-1 *bt.Total)
when voucher.vtypeid=24 and bm.BillType='CREDIT NOTE' AND ISNULL(bm.Extra1,'X')='SALE' then (-1 *bt.Total)
else bt.Total end GrossAmount,

case when voucher.vtypeid=19 then (-1 *bt.Cgst)
when voucher.vtypeid=24 and bm.BillType='CREDIT NOTE' AND ISNULL(bm.Extra1,'X')='SALE'  then (-1 *bt.Cgst)
else bt.Cgst end  CgstAmt,

case when voucher.vtypeid=19 then (-1 *bt.Sgst)
when voucher.vtypeid=24 and bm.BillType='CREDIT NOTE' AND ISNULL(bm.Extra1,'X')='SALE'  then (-1 *bt.Sgst)
else bt.Sgst end   SgstAmt

,case when voucher.vtypeid=19 then (-1 *bt.Igst)
when voucher.vtypeid=24 and bm.BillType='CREDIT NOTE'  AND ISNULL(bm.Extra1,'X')='SALE' then (-1 *bt.Igst)
else bt.Igst end  IgstAmt,

case when voucher.vtypeid=19 then (-1 *bt.DiscAmt)
when voucher.vtypeid=24 and bm.BillType='CREDIT NOTE' AND ISNULL(bm.Extra1,'X')='SALE'  then (-1 *bt.DiscAmt)
else bt.DiscAmt end   DiscAmt,

case when voucher.vtypeid=19 then (-1 *bt.NetTotal)
when voucher.vtypeid=24 and bm.BillType='CREDIT NOTE' AND ISNULL(bm.Extra1,'X')='SALE'  then (-1 *bt.NetTotal)
else bt.NetTotal end  NetTotal,

ac.GstIn 
from dbo.BillTrans bt
left outer join dbo.BillMain bm on bm.Id =bt.BillId
left outer join dbo.Product p on p.Id =bt.ProductId
left outer join dbo.acc bk on bk.Id=bm.BookAcId 
left outer join dbo.Voucher voucher on voucher.Id=bm.VoucherId 
left outer join dbo.Acc ac on ac.Id=bm.AccId
where (@HsnCode = case when p.HsnCode  is null then bt.HsnCode else  p.HsnCode end)
and (bt.uomid=@UnitId or (@UnitId=0 and bt.UomId is null)) and (bm.VoucherDate between @FromDate and @ToDate)
and bm.CompId=@CompanyId and bm.YearId=@YearId and bm.IsDeleted =0 and bm.IsActive=1
and bt.IsDeleted =0 and bt.IsActive=1
and 
((voucher.VTypeId = 12) OR(voucher.VTypeId=24 AND ISNULL(bm.Extra1,'X')='SALE' AND ISNULL(bm.BillType,'X')='DEBIT NOTE')
or	((voucher.VTypeId=24 AND ISNULL(bm.Extra1,'X')='SALE' AND ISNULL(bm.BillType,'X')='CREDIT NOTE')) or (voucher.VTypeId = 19)  )
END
GO

