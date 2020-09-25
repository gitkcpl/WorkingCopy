CREATE PROC [dbo].[OpBalSp]
@companyid INT=1,
@yearid INT=1
AS
SELECT  am.AccName ,
        lg.GroupName ,
        CASE WHEN ISNULL(ab.OpBal, 0) < 0 THEN -1 * ab.OpBal
             ELSE 0
        END OpCredit ,
        CASE WHEN ISNULL(ab.OpBal, 0) > 0 THEN ab.OpBal
             ELSE 0
        END OpDebit ,
        CASE WHEN ab.Bal > 0
             THEN CAST(ISNULL(ab.Bal, 0) AS VARCHAR(25)) + ' Dr'
             ELSE CAST(-1 * ISNULL(ab.Bal, 0) AS VARCHAR(25)) + ' Cr'
        END AS CurBal ,
        ISNULL(ab.OpBal,0)OpBal,ISNULL(ab.Bal,0)Balance,
        am.GstIn ,
        am.PanNo ,
        am.AadharNo ,
        ab.Address1 Address ,
        ct.CityName,am.Id AccountId,ab.GroupId,ab.AddressId,ab.CompId,ab.YearId,ab.Id BalId
FROM    dbo.Acc am
        LEFT OUTER JOIN dbo.AccBal ab ON ab.AccId = am.Id
        LEFT OUTER JOIN dbo.AcGroup lg ON lg.Id = ab.GroupId
        LEFT OUTER JOIN dbo.AccAddress aa ON aa.Id= ab.AddressId
        LEFT OUTER JOIN dbo.City ct ON ct.Id = aa.CityId
        WHERE (lg.Nature IN ('ASSETS','LIABILITIES'))
        AND ab.CompId=@companyid AND ab.YearId=@yearid
        ORDER BY am.AccName
GO

