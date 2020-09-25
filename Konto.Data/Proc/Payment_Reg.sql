IF object_id('[dbo].[Payment_Reg]') IS NULL 
EXEC ('CREATE PROC [dbo].[Payment_Reg] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[Payment_Reg]
    @fromdate INT = 20190401,
    @todate INT = 20200331,
    @companyid INT = 22,
    @reportid INT = 0,
    @party VARCHAR(1) = 'N'

AS
SELECT br.VoucherNo ,ac.AccName ,
CONVERT(DATETIME2, CONVERT(VARCHAR(8), ISNULL(br.VoucherDate,0)), 112) AS [Date], 
b.AvgDays AvgDays,
br.TotalAmount TotalAmount ,
b.PaidAmt,
b.Rg,
b.Rg/br.TotalAmount*100 Rgper
FROM dbo.BillMain br
LEFT OUTER JOIN dbo.Voucher vc ON vc.Id = br.VoucherId
LEFT OUTER JOIN dbo.Acc ac ON ac.Id = br.AccId
LEFT OUTER JOIN (SELECT b.BillId,AVG(DATEDIFF(D, bt.VDate, bm.VDate)) AvgDays,SUM(CASE WHEN b.Transtype = 'Payment' THEN b.Amount + ISNULL(b.Adla1,0) + ISNULL(b.Adla2,0) + ISNULL(b.Adla3,0) + ISNULL(b.Adla4,0) + ISNULL(b.Adla5,0) + ISNULL(b.Adla6,0) + ISNULL(b.Adla7,0) + ISNULL(b.Adla8,0) + ISNULL(b.Adla9,0) + ISNULL(b.Adla10,0) ELSE 0 END) PaidAmt ,
SUM(CASE WHEN b.Transtype = 'Return' THEN b.Amount ELSE 0 END) Rg 
FROM  dbo.BtoB b
LEFT OUTER JOIN dbo.BillMain bm ON bm.Id = b.RefId
LEFT OUTER JOIN dbo.BillMain bt ON bt.Id = b.BillId
WHERE bm.IsDeleted = 0 AND bm.IsActive = 1 AND bt.IsDeleted = 0 AND bt.IsActive = 1
GROUP BY b.BillId ) b ON b.BillId = br.Id
WHERE vc.VTypeId = 12 AND br.IsDeleted = 0 AND br.IsActive = 1


GO

