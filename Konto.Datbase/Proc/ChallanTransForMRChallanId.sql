CREATE PROCEDURE [dbo].[ChallanTransForMRChallanId]
	@id INT = 0
AS
BEGIN
    SELECT ct.ChallanId,
           ct.ProductId,
           ct.Id,
           pd.ProductName,
           ct.Sgst,
           ct.Qty,
           ct.LotNo,
		   ct.RefNo,
           ct.Pcs,
           ct.Cops,
           ct.Rate,
           ct.UomId,
           ct.Gross,
           ct.Disc,
           ct.DiscPer,
           ct.FreightRate,
           ct.Freight,
           ct.Igst,
           ct.IgstPer,
           ct.Cgst,
           ct.CgstPer,
           ct.SgstPer,
           ct.Total,
           ct.ColorId,
           ct.DesignId,
           ct.GradeId,
           ct.IsActive,
           cl.ColorName,
		   gd.GradeName,
           ct.CreateDate,
           ct.CreateUser,
           ct.IpAddress,
           ct.IsDeleted,
           ct.ModifyDate,
           ct.ModifyUser,
           ct.RefId,
           ct.RefVoucherId,
           ct.Remark,
           ct.RowId,
           ct.OtherAdd,
           ct.OtherLess,
           ct.IssuePcs,
           ct.IssueQty,
           DesignNo = dm.ProductName,
           gi.VoucherNo OrdNo,
		   CONVERT(Date,Convert(varchar(8),gi.VoucherDate),112) OrdDate,
		   ct.MiscId, isp.ProductName GreyItem
    FROM dbo.Challan c
        LEFT OUTER JOIN dbo.ChallanTrans ct
            ON ct.ChallanId = c.Id
        LEFT OUTER JOIN dbo.Product pd
            ON pd.Id = ct.ProductId
        LEFT OUTER JOIN dbo.Color cl
            ON cl.Id = ct.ColorId
        LEFT OUTER JOIN dbo.Product dm
            ON dm.Id = ct.DesignId
        LEFT OUTER JOIN dbo.Grade gd
            ON gd.Id = ct.GradeId
        LEFT OUTER JOIN dbo.ChallanTrans git
            ON ct.RefId = git.Id
        LEFT OUTER JOIN dbo.Challan gi
            ON git.ChallanId = gi.Id
               AND ct.RefVoucherId = gi.VoucherId
			   left outer join dbo.Product isp on isp.Id = git.ProductId 
    WHERE ct.ChallanId = @id
          AND ct.IsDeleted = 0;

END;
GO

