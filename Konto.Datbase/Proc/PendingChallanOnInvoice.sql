CREATE PROCEDURE [dbo].[PendingChallanOnInvoice]
 	@CompanyId INT,
	@AccountId INT,
	@VoucherTypeID INT,
	@ChallanTypeId INT
AS
BEGIN
	SELECT CASE WHEN ch.ChallanNo = 'NA' THEN ch.VoucherNo ELSE ch.ChallanNo END AS ChallanNo,ch.VoucherDate AS ChlnDate, 
	 ISNULL(CONVERT(Date,Convert(varchar(8),ch.VoucherDate),112),'')  as ChallanDate,
	ch.BillNo, ch.RcdDate, ch.TransId AS TransportId,
	ISNULL(ch.TotalPcs,0) TotalPcs,
	CAST(ct.InwPcs AS numeric(18,2)) AS InwPcs,
	CAST(ct.InwQty AS numeric(18,2)) AS InwQty,
	CAST(ch.TotalQty AS numeric(18,2)) AS TotalQty,
	CAST(ch.TotalAmount AS numeric(18,2)) AS NetTotal,
	ch.Id ,ch.VoucherId, 
	ch.DocNo, ch.DocDate,ch.VehicleNo,ch.Remark  
	from dbo.Challan ch
	LEFT OUTER JOIN dbo.Voucher v on v.Id=ch.VoucherId 
	LEFT OUTER JOIN (SELECT SUM(ISNULL(ct.IssueQty,0)) InwQty, SUM(ISNULL(ct.IssuePcs,0)) InwPcs, ct.ChallanId FROM ChallanTrans ct GROUP BY ct.ChallanId) ct ON ct.ChallanId = ch.Id
	where  ch.AccId=@AccountId AND NOT EXISTS ( SELECT 1 FROM dbo.BillTrans bt WHERE bt.RefId = ch.Id and IsActive = 1 AND IsDeleted = 0 ) 
	AND ch.IsActive = 1 AND ch.IsDeleted = 0 AND  (v.VTypeId=@VoucherTypeID)  --IN (7,8) OR ISNULL(ch.ChallanType ,0) in (1,6,9))
	and ch.CompId=@companyid
END
GO

