
IF object_id('[dbo].[TDS]') IS NULL 
EXEC ('CREATE PROC [dbo].[TDS] AS SELECT 1 AS Id') 

GO

ALTER PROCEDURE [dbo].[TDS]
 @CompanyId INT ,
 @FromDate  INT ,
 @ToDate INT,
 @TDSAcID INT = 0,
 @YearId INT=0,
 @reportid INT=0,
 @party VARCHAR(1)='N',
 @book VARCHAR(1) ='N',
 @groupid int =0,
 @acid INT =0,
 @party_group VARCHAR(1)='N',
 @pgid INT=0
as
BEGIN
SELECT ISNULL(bm.BillNo,bm.VoucherNo) BillNo,bm.RowId AS RefId,
CONVERT(Date,Convert(varchar(8),bm.VoucherDate),112) ChallanDate,
am.PanNo,
am.AccName Party,
tm.AccName TDSAccount,
v.VoucherName,
nm.Descr Descr,
bm.TotalAmount TotalAmount,
btt.Taxable AcsValue,
bm.TdsPer,
bm.TdsAmt,
bm.HasteId,
bm.AccId AccountID,
bm.TotalAmount-bm.TdsAmt  Payable
 FROM dbo.BillMain bm
LEFT OUTER JOIN (SELECT BillId, SUM(DiscAmt) disc,SUM(Cess) AS Cess, 
		SUM(OtherAdd) AS OtherA, SUM(OtherLess) AS OtherL, SUM(Cgst) Cgst, SUM(Sgst) Sgst, SUM(Igst) Igst,
		SUM(NetTotal-ISNULL(Sgst,0)-ISNULL(Cgst,0)-ISNULL(Igst,0)-ISNULL(Cess,0)) Taxable FROM dbo.BillTrans
		WHERE IsDeleted=0 GROUP BY BillId) btt ON btt.BillId = bm.Id
LEFT OUTER JOIN dbo.Acc am ON am.Id = bm.AccId
LEFT OUTER JOIN dbo.Acc tm ON tm.Id = bm.HasteId
LEFT OUTER JOIN dbo.Voucher v ON v.Id = bm.VoucherId
left outer join dbo.Nop nm ON nm.Id = tm.NopId
WHERE bm.TdsAmt >0 AND ISNULL(bm.HasteId,0)>0 
AND bm.IsActive=1 AND bm.IsDeleted=0
AND bm.VoucherDate BETWEEN @fromdate AND @ToDate AND bm.CompId=@CompanyId
	AND ((@book='N' and (@TDSAcID =0 or bm.HasteId =@TDSAcID))
	OR (EXISTS
    (
        SELECT 1
        FROM dbo.ReportPara rs
        WHERE rs.ReportId = @reportid
              AND tm.Id = rs.ParameterValue
              AND rs.ParameterName = 'book'
    )))
	AND ((@party='N' AND (@acid=0 OR am.Id=@acid))
	OR (EXISTS
    (
        SELECT 1
        FROM dbo.ReportPara rs
        WHERE rs.ReportId = @reportid
              AND tm.Id = rs.ParameterValue
              AND rs.ParameterName = 'party'
    )))
	AND ((@party_group='N' and (@pgid=0 OR am.PGroupId=@pgid)) or
	 EXISTS
    (
        SELECT 1
        FROM dbo.ReportPara rs
        WHERE rs.ReportId = @reportid
              AND am.PGroupId = rs.ParameterValue
              AND rs.ParameterName = 'party_group'
    ))
	AND (@groupid=0 OR am.GroupId=@groupid)

 UNION ALL
 
 SELECT ISNULL(bm.BillNo,bm.VoucherNo) BillNo,bm.RowId AS RefId,
CONVERT(Date,Convert(varchar(8),bm.VoucherDate),112) ChallanDate,
 act.PanNo,
 act.AccName Party,
 tt.AccName TDSAccount,
v.VoucherName,
nt.Descr Descr,
 bt.NetTotal TotalAmount,
 CASE WHEN v.VTypeId = 16 THEN bt.Total ELSE  bt.NetTotal-ISNULL(bt.Sgst,0)-ISNULL(bt.Cgst,0)-ISNULL(bt.Igst,0)-ISNULL(bt.Cess,0) end  AcsValue,
 bt.TdsPer  TdsPer,
 bt.TdsAmt  TdsAmt,
 bt.TdsAcId  HasteId,
 bt.ToAccId  AccountID,
 CASE WHEN v.VTypeId IN (14,16) THEN 0 ELSE bt.NetTotal-ISNULL(bt.Sgst,0)-ISNULL(bt.Cgst,0)-ISNULL(bt.Igst,0)-ISNULL(bt.Cess,0)  END Payable
 FROM dbo.BillMain bm
LEFT OUTER JOIN dbo.BillTrans bt ON bt.BillId = bm.Id
LEFT OUTER JOIN dbo.Acc act ON act.Id = bt.ToAccId
LEFT OUTER JOIN dbo.Acc tt ON tt.Id = bt.TdsAcId
LEFT OUTER JOIN dbo.Voucher v ON v.Id = bm.VoucherId
LEFT OUTER JOIN dbo.Nop nt ON nt.Id = tt.NopId
WHERE bt.TdsAmt >0 AND ISNULL(bt.TdsAcId,0)>0 
AND bm.IsActive=1 AND bm.IsDeleted=0
AND bm.VoucherDate BETWEEN @fromdate AND @ToDate AND bm.CompId=@CompanyId
	AND ((@book='N' and (@TDSAcID =0 or bt.TdsAcId =@TDSAcID))
	OR (EXISTS
    (
        SELECT 1
        FROM dbo.ReportPara rs
        WHERE rs.ReportId = @reportid
              AND tt.Id = rs.ParameterValue
              AND rs.ParameterName = 'book'
    )))
	AND ((@party='N' AND (@acid=0 OR act.Id=@acid))
	OR (EXISTS
    (
        SELECT 1
        FROM dbo.ReportPara rs
        WHERE rs.ReportId = @reportid
              AND act.Id = rs.ParameterValue
              AND rs.ParameterName = 'party'
    )))
	AND ((@party_group='N' and (@pgid=0 OR act.PGroupId=@pgid)) or
	 EXISTS
    (
        SELECT 1
        FROM dbo.ReportPara rs
        WHERE rs.ReportId = @reportid
              AND act.PGroupId = rs.ParameterValue
              AND rs.ParameterName = 'party_group'
    ))
	AND (@groupid=0 OR act.GroupId=@groupid)
 

END
GO