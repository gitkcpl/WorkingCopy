IF object_id('[dbo].[ChallanDetailList]') IS NULL 
EXEC ('CREATE PROC [dbo].[ChallanDetailList] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[ChallanDetailList]
@VTypeId int,
 @CompanyId int,
  @BranchId int,
 @YearId int,
 @FromDate int,
 @ToDate int
AS
BEGIN 
	 SELECT dv.DivisionName ,tt.TypeName,
	 ISNULL(CONVERT(Date,Convert(varchar(8),c.VoucherDate),112),'')  as ChallanDate, 
	 c.VoucherNo ChallanNo,
	 c.ChallanNo RefNo,
	 ac.PrintName Party,
	 v.VoucherName VoucherName,
	 p.ProductName Product,
	 design.ProductName Design,
	 col.ColorName Color,
	 g.GradeName Grade,
	 ct.Pcs,
	-- ct.Pcs - ISNULL(b.Pcs,0) PendingPcs,
	 ct.Cops, 
	 ct.Qty,
	-- ct.Qty- ISNULL(b.qty,0) PendingQty,
	 ct.Rate,
	 ct.SgstPer,
	 ct.Gross,
	 ct.DiscPer,
	 ct.Disc DiscAmt,
	 ct.OtherAdd,ct.OtherLess,
	 ct.CgstPer,ct.Cgst CgstAmt,
	 ct.SgstPer,ct.Sgst SgstAmt, 
	 ct.IgstPer,ct.Igst IgstAmt,
	 ct.CessPer,ct.Cess,
	 ct.Total,
	 Agent.PrintName,
	 c.BillNo, 
	 c.DName Driver,
	 c.DocDate LrDate,
	 c.DocNo LrNo,
	 tr.PrintName as Transport, 
	 o.OrderNo OrderNo, 
	 CASE WHEN o.OrdDate >10101 THEN
	 ISNULL(CONVERT(Date,Convert(varchar(8),o.OrdDate),112),'') end  as OrderDate,
	 ct.IssuePcs,
	 ct.IssueQty,ct.LotNo
	 ,ct.Remark,ct.Weight,c.Id,ct.Id as TransId,
	 c.VoucherId,
	 c.IsDeleted
	 FROM Challan c
	 LEFT OUTER JOIN dbo.TransType tt ON tt.Id = c.ChallanType
	 LEFT OUTER JOIN dbo.Division dv ON dv.Id = c.DivId
	 LEFT OUTER JOIN Acc ac ON  c.AccId =ac.Id 
     LEFT OUTER JOIN Acc tr ON c.TransId = tr.Id
     LEFT OUTER JOIN Acc Agent ON c.AgentId = Agent.Id
     LEFT OUTER JOIN Voucher v ON c.VoucherId =v.Id
     LEFT OUTER JOIN AccAddress adr ON c.DelvAdrId =adr.Id
     LEFT OUTER JOIN ChallanTrans ct ON ct.ChallanId=c.Id
     LEFT OUTER JOIN Product p ON p.Id=ct.ProductId
     LEFT OUTER JOIN Product design ON design.Id=ct.DesignId
     LEFT OUTER JOIN grade g ON g.Id=ct.GradeId
     LEFT OUTER JOIN color col ON col.Id=ct.ColorId
     LEFT OUTER JOIN ( SELECT o.VoucherNo as OrderNo,o.VoucherDate AS OrdDate,Id AS OrdId 
			FROM Ord o WHERE o.IsActive=1 and o.IsDeleted=0 
			GROUP BY o.Id ,o.VoucherNo,o.VoucherDate
			)o on o.OrdId=ct.MiscId 
   --         LEFT outer join  ( SELECT bt.RefTransId, SUM(bt.Pcs) Pcs, SUM(bt.Qty) Qty
			--FROM BillTrans bt
			--WHERE bt.IsActive=1 and bt.IsDeleted=0  
			--GROUP BY bt.RefTransId
			--) b on b.RefTransId =ct.Id  

      WHERE c.IsActive=1 and c.IsDeleted=0 AND ct.IsDeleted = 0
      AND (c.VoucherDate  between @FromDate and @ToDate) and v.VTypeId=@VTypeId
      ORDER BY c.VoucherDate desc
    
END
GO

