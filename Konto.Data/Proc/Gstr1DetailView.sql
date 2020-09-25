IF object_id('[dbo].[Gstr1DetailView]') IS NULL 
EXEC ('CREATE PROC [dbo].[Gstr1DetailView] AS SELECT 1 AS Id') 
GO

ALTER PROC [dbo].[Gstr1DetailView]
    @compid INT,
    @yearid INT,
    @fromdate INT,
    @todate INT,
    @vtypeid INT
AS
BEGIN
    SELECT ac.GstIn,
           ac.AccName ReceiverName,
		   bm.BillNo InvoiceNo,
		   ISNULL(bm.RcdDate,bm.VDate) InvoiceDate,
           bm.VoucherNo AS VoucherNo,
           ISNULL(CONVERT(Date,Convert(varchar(8),bm.VoucherDate),112),'')  VoucherDate,
           bm.TotalAmount InvoiceValue,
           ISNULL(bt.HsnCode, p.HsnCode) HsnCode,
           ISNULL(um.GSTUnit, 'NA') Uqc,
           ISNULL(pg.GroupName, 'NA') [Description],
           bt.NetTotal - bt.Sgst - bt.Cgst - bt.Igst AS TaxableValue,
           bt.CgstPer CgstRate,
           bt.Cgst Cgst,
           bt.SgstPer SgstRate,
           bt.Sgst Sgst,
           bt.IgstPer IgstRate,
           bt.Igst,
           bt.Cess,
           bt.NetTotal Total,
           ISNULL(pos.StateName, st.StateName) StateName,
           bm.BillType InvoiceType,
           ac.VatTds ReceiverType,
           p.ProductName Product,
           vc.VoucherName Voucher,
           bk.AccName Book,
		   bm.SpecialNotes Reason
    FROM dbo.BillMain bm
        LEFT OUTER JOIN dbo.BillTrans bt
            ON bt.BillId = bm.Id
        LEFT OUTER JOIN dbo.Product p
            ON p.Id = bt.ProductId
        LEFT OUTER JOIN dbo.PGroup pg
            ON pg.Id = p.GroupId
        LEFT OUTER JOIN dbo.Uom um
            ON um.Id = bt.UomId
        LEFT OUTER JOIN dbo.Acc ac
            ON ac.Id = bm.AccId
        LEFT OUTER JOIN dbo.Acc bk
            ON bk.Id = bm.BookAcId
        LEFT OUTER JOIN dbo.Voucher vc
            ON vc.Id = bm.VoucherId
        LEFT OUTER JOIN dbo.AccBal adr
            ON adr.AccId = ac.Id
        LEFT OUTER JOIN dbo.City ct
            ON ct.Id = adr.CityId
        LEFT OUTER JOIN dbo.[State] st
            ON st.Id = ct.StateId
        LEFT OUTER JOIN dbo.[State] pos
            ON pos.Id = bm.StateId
    WHERE vc.VTypeId = @vtypeid
          AND bm.CompId = @compid
          AND bm.VoucherDate
          BETWEEN @fromdate AND @todate
          AND adr.CompId = @compid
          AND adr.YearId = @yearid
		  AND bm.IsDeleted =0 AND bt.IsDeleted=0 AND bm.IsActive = 1
		  ORDER BY bm.VoucherDate,bm.Id
END;
GO

