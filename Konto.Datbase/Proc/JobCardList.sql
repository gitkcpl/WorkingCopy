CREATE PROCEDURE [dbo].[JobCardList]
 	@CompanyId int,
	@VoucherTypeID int
AS
BEGIN
	SELECT o.VoucherNo,v.VoucherName,o.ProgramNo OrderNo, o.OrdDate AS OrderDate, o.CarrierNo BeamNo,
	CONVERT(Date,Convert(varchar(8),o.VoucherDate),112)as VouchDate, em.EmpName IssueBy
	 ,acc.AccName Party,p.ProductName ,o.Qty TotalQty, o.VoucherId	,o.Id 
	from dbo.JobCard o 
	LEFT OUTER JOIN dbo.Acc acc ON acc.Id = o.AccountId 	
	LEFT OUTER JOIN Voucher v on v.Id = o.VoucherId
	LEFT OUTER JOIN dbo.Product p ON p.Id = o.ProductId
	LEFT OUTER JOIN dbo.Color cl ON cl.Id = o.ColorId
	LEFT OUTER JOIN dbo.Emp em ON em.Id = o.BatchId     
	WHERE  v.VTypeId=@VoucherTypeID AND o.CompanyId = @CompanyId
	AND o.IsActive =1 AND o.IsDeleted = 0 
	
END
GO

