

/*-- =============================================
-- Author:		<Author,,Name>
  Create date: <Create Date,,>
  Description:	<Description,,>
  exec PendingJOBonChallan 1,25,5,'Inward from Job'

  select * from voucher
-- =============================================*/

CREATE PROCEDURE [dbo].[PendingJOBonChallan]
 	@CompanyId int,
	@AccountId int,
	@VoucherTypeID int,
	@ChallanType int,
	@RefVoucherId int
AS
BEGIN
	select c.VoucherNo,c.VoucherDate ,ISNULL(v.RefVoucherId,0) AS RefVoucherId,c.VoucherId,
	ct.Pcs AS TotalPcs,CAST(ct.Qty as numeric(18,2)) as TotalQty,
	CAST((ct.Qty-ISNULL(pen.Qty,0))  AS numeric(18,2)) AS PendingQty,
	(ct.Pcs-ISNULL(pen.Pcs,0))  AS PendingPcs, ac.AccName AS Weaver,ct.ColorId,
	CAST(ISNULL(ct.Cops,0.00) as numeric(6,2)) Cut,
	ct.DesignId,ct.GradeId,	p.ProductName as Product,ct.ProductId,
	ct.Gross as Total,
	CAST(ISNULL(ct.SgstPer ,0.00) as numeric(18,2)) as Sgst,
	CAST(ISNULL(ct.Sgst,0.00) as numeric(18,2)) SgstAmt,
	ISNULL(ct.CgstPer,0.00) Cgst,
	ISNULL(ct.Cgst,0.00) CgstAmt,
	ISNULL(ct.IgstPer,0.00) Igst,
	ISNULL(ct.Igst,0.00) IgstAmt,
	ISNULL(ct.FreightRate,0.00) FreightRate,
	ISNULL(ct.Freight,0.00) Freight, 
	ISNULL(ct.OtherAdd,0.00) OtherAdd,
	ISNULL(ct.OtherLess,0.00) OtherLess, 
	ISNULL(ct.CessPer,0.00) Cess, 
	ISNULL(ct.Cess,0.00) CessAmt,
	ISNULL(ct.DiscPer,0.00) Disc, 
	ISNULL(ct.Disc,0.00) DiscAmt,
	ct.UomId,ct.Pcs,CAST(ct.Total as numeric(18,2)) as NetTotal,
	ct.LotNo AS LotNo,CAST(ct.Qty  as numeric(18,2)) as Qty, 
	ct.Pcs AS Pcs
	,CAST(ct.Rate as numeric(18,2)) as rate
	,ct.Id as TransId,c.Id ,c.VoucherId ,ISNULL(pen.Qty,0.00) RcptQty
    FROM Challan c
    LEFT outer join ChallanTrans ct ON ct.ChallanId=c.Id
    LEFT outer join Product p  ON p.Id=ct.ProductId
    LEFT outer join Division d ON d.Id=c.DivId 
    LEFT outer join Voucher v  ON v.Id=c.VoucherId
	LEFT OUTER JOIN dbo.Acc ac ON ac.Id = c.AccId
	LEFT outer join ( SELECT sum(Qty)as Qty, SUM(Pcs) AS Pcs , ct.RefId, ct.RefVoucherId FROM ChallanTrans ct
		    WHERE ct.IsActive=1 and ct.IsDeleted=0
		    GROUP BY ct.RefId, ct.RefVoucherId
	)pen on pen.RefId=ct.Id AND pen.RefVoucherId = c.VoucherId
	
     WHERE c.ChallanType=@ChallanType and (c.VoucherId=@VoucherTypeID or v.RefVoucherId is null)
	 AND v.VTypeId=@VoucherTypeID
	 AND c.AccId=@AccountId
	 AND ((isnull(ct.Qty,0)-isnull(pen.Qty,0)) >0) AND c.IsDeleted = 0 AND c.IsActive =1
END
GO

