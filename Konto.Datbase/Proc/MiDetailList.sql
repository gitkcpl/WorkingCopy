CREATE PROCEDURE dbo.MiDetailList
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
	 ac.PrintName Party,
	 v.VoucherName VoucherName,
	 p.ProductName Product,
	 design.ProductName Design,
	 col.ColorName Color,
	 ct.Pcs,
	 ct.Pcs - ISNULL(mr.Pcs,0) PendPcs,
	 ct.Cops, 
	 ct.Qty Mtrs,
	 ct.Qty- ISNULL(mr.qty,0) PendMtrs,
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
	 ct.LotNo
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
   LEFT OUTER JOIN color col ON col.Id=ct.ColorId

   LEFT OUTER JOIN (SELECT mrt.MiscId,mrt.RefId, mrt.RefVoucherId,SUM(mrt.Pcs) Pcs,SUM(mrt.Qty)Qty FROM Challan mr 
    INNER JOIN ChallanTrans  mrt ON mr.Id = mrt.ChallanId
    WHERE mr.IsDeleted = 0 AND mrt.IsDeleted=0
    GROUP BY mrt.MiscId,mrt.RefVoucherId,mrt.RefId)mr ON c.Id = mr.MiscId AND c.VoucherId = mr.RefVoucherId AND ct.Id = mr.RefId

      WHERE c.IsActive=1 and c.IsDeleted=0 AND ct.IsDeleted = 0
      AND (c.VoucherDate  between @FromDate and @ToDate) and v.VTypeId=@VTypeId
      ORDER BY c.VoucherDate desc
    
END
GO