IF object_id('[dbo].[gst_summary_cross]') IS NULL 
EXEC ('CREATE PROC [dbo].[gst_summary_cross] AS SELECT 1 AS Id') 
GO

ALTER PROC dbo.gst_summary_cross 
@fromdate INT,
@todate INT,
@compid INT
AS

BEGIN

  SET NOCOUNT ON
  SELECT
	CASE WHEN vt.Id IN (12,19) or (vt.Id=24 OR ISNULL(BM.Extra1, 'NA') = 'SALES') THEN '1.Outward Supply'
	ELSE CASE WHEN ISNULL(bm.Rcm,'NA')='YES' THEN '3.Inward Supply Under RCM' ELSE '2.Inward Supply' END END SupplyType,
    vt.TypeName TransType
   ,b.AccName Books
   ,ISNULL(bt.HsnCode, p.HsnCode) Hsn
   ,ISNULL(bt.CgstPer, 0) + ISNULL(bt.SgstPer, 0) + ISNULL(bt.IgstPer, 0) TaxPer
   ,CASE
      WHEN vt.Id IN (12, 13, 23) OR
        (vt.Id = 24 AND
        ISNULL(bm.BillType, 'NA') = 'DEBIT NOTE' AND
        ISNULL(BM.Extra1, 'NA') = 'SALES') OR
        (vt.Id = 24 AND
        ISNULL(bm.BillType, 'NA') = 'CREDIT NOTE' AND
        ISNULL(BM.Extra1, 'NA') = 'PURCHASE') THEN bt.NetTotal - bt.Cgst - bt.Sgst - bt.Igst
      ELSE -1 * (bt.NetTotal - bt.Cgst - bt.Sgst - bt.Igst)
    END Taxable
   ,CASE
      WHEN vt.Id IN (12, 13, 23) OR
        (vt.Id = 24 AND
        ISNULL(bm.BillType, 'NA') = 'DEBIT NOTE' AND
        ISNULL(BM.Extra1, 'NA') = 'SALES') OR
        (vt.Id = 24 AND
        ISNULL(bm.BillType, 'NA') = 'CREDIT NOTE' AND
        ISNULL(BM.Extra1, 'NA') = 'PURCHASE') THEN bt.Cgst
      ELSE -1 * bt.Cgst
    END Cgst
   ,CASE
      WHEN vt.Id IN (12, 13, 23) OR
        (vt.Id = 24 AND
        ISNULL(bm.BillType, 'NA') = 'DEBIT NOTE' AND
        ISNULL(BM.Extra1, 'NA') = 'SALES') OR
        (vt.Id = 24 AND
        ISNULL(bm.BillType, 'NA') = 'CREDIT NOTE' AND
        ISNULL(BM.Extra1, 'NA') = 'PURCHASE') THEN bt.Sgst
      ELSE -1 * bt.Sgst
    END Sgst
   ,CASE
      WHEN vt.Id IN (12, 13, 23) OR
        (vt.Id = 24 AND
        ISNULL(bm.BillType, 'NA') = 'DEBIT NOTE' AND
        ISNULL(BM.Extra1, 'NA') = 'SALES') OR
        (vt.Id = 24 AND
        ISNULL(bm.BillType, 'NA') = 'CREDIT NOTE' AND
        ISNULL(BM.Extra1, 'NA') = 'PURCHASE') THEN bt.Igst
      ELSE -1 * bt.Igst
    END Igst
   ,CASE
      WHEN vt.Id IN (12, 13, 23) OR
        (vt.Id = 24 AND
        ISNULL(bm.BillType, 'NA') = 'DEBIT NOTE' AND
        ISNULL(BM.Extra1, 'NA') = 'SALES') OR
        (vt.Id = 24 AND
        ISNULL(bm.BillType, 'NA') = 'CREDIT NOTE' AND
        ISNULL(BM.Extra1, 'NA') = 'PURCHASE') THEN bt.NetTotal
      ELSE -1 * bt.NetTotal
    END NetTotal
   ,CASE
      WHEN a.VatTds = 'REG' THEN 'Regular'
      WHEN A.VatTds = 'ECOM' THEN 'Ecommerce-Op'
      WHEN A.VatTds = 'CON' THEN 'Consumer'
      WHEN A.VatTds = 'URD' THEN 'Un-Register'
      WHEN A.VatTds = 'CMP' THEN 'Composition'
      ELSE 'NA'
    END RegType
   ,CASE
      WHEN ISNULL(bm.StateId, ct.StateId) = c.StateId THEN 'GST'
      ELSE 'IGST'
    END GstType
   ,ISNULL(stb.StateName, st.StateName) Pos
   ,bm.VoucherDate
   ,bm.VoucherNo
   ,bm.BillNo
   ,a.AccName Party
   ,a.GstIn
   ,v.VoucherName
   ,p1.ProductName
   ,bt.Remark
   ,BM.Id
   ,BT.Id TranId
  FROM dbo.BillMain bm
  LEFT OUTER JOIN dbo.BillTrans bt
    ON bm.Id = bt.BillId
  LEFT OUTER JOIN DBO.Product p1
    ON bt.ProductId = p1.Id
  LEFT OUTER JOIN dbo.Voucher v
    ON bm.VoucherId = v.Id
  LEFT OUTER JOIN dbo.VoucherType vt
    ON v.VTypeId = vt.Id
  LEFT OUTER JOIN dbo.Acc a
    ON bm.AccId = a.Id
  LEFT OUTER JOIN dbo.Acc b
    ON bm.BookAcId = b.Id
  LEFT OUTER JOIN dbo.AccBal ab
    ON bm.AccId = ab.
      AccId
      AND bm.YearId = ab.YearId
      AND bm.CompId = ab.CompId
  LEFT OUTER JOIN dbo.City ct
    ON ab.cityid = ct.id
  LEFT OUTER JOIN dbo.[state] st
    ON ct.StateId = st.Id
  LEFT OUTER JOIN dbo.[state] stb
    ON bm.StateId = stb.Id
  LEFT OUTER JOIN dbo.Company c
    ON bm.CompId = c.Id
  LEFT OUTER JOIN dbo.Product p
    ON bt.ProductId = p.Id
  WHERE vt.id IN (12, 13, 23, 24, 19, 18)
  AND bm.Voucherdate BETWEEN @fromdate AND @todate
  AND bm.compid = @compid

END
GO

