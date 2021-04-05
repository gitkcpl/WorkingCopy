IF object_id('[dbo].[AccLookup]') IS NULL 
EXEC ('CREATE PROC [dbo].[AccLookup] AS SELECT 1 AS Id') 
GO 

ALTER PROCEDURE [dbo].[AccLookup]
	 @vouchertypeid INT = 0 ,
    @groupid INT = 0 ,
    @companyid INT = 0 ,
    @yearid INT = 0 ,
    @fillparty VARCHAR(1) = 'Y' ,
    @taxtype VARCHAR(5) = 'N',
    @accountid INT=0,
    @nature VARCHAR(25) ='ALL',
    @outstype VARCHAR(1)='N'
AS
    BEGIN
         SELECT  am.Id ,
                am.AccName ,
                ab.GroupId,
                lg.GroupName ,
                CASE WHEN ab.Bal > 0
                     THEN CAST(ISNULL(ab.Bal, 0) AS VARCHAR(25)) + ' Dr'
                     ELSE CAST(-1*ISNULL(ab.Bal, 0) AS VARCHAR(25)) + ' Cr'
                END AS Balance ,
                am.AgentId ,
                am.TransportId ,
                am.EmpId ,
                am.PGroupId,
                ag.AccName Agent ,
                at1.AccName Transport ,
                pg.GroupName PartyGroup ,
                am.CrDays ,
                am.CrLimit ,ISNULL(ab.AddressId,0) AS AddressId,
                isnull(ab.[Address1],'NA') + ' ' +isnull(ab.[Address2],'NA') + ' ' + isnull(ar.AreaName,'NA') + ' ' + isnull(ct.CityName,'NA') + ' Pin:' +
                isnull(ab.PinCode,'NA') + ' Mob:' + isnull(ab.MobileNo,'NA') + ' Email:' + isnull(ab.email,'NA') FullAddress,
                ab.Address1,
				ab.Address2,
                ar.AreaName ,
                ct.CityName ,
                ab.PinCode ,
                ab.MobileNo ,
                ab.email ,
                am.GSTIN ,
                am.AadharNo ,
                am.PanNo ,
                ab.CityId ,
                ab.AreaId ,
                ct.StateId ,
                st.StateName ,
                st.GstCode GSTCode ,
                CASE WHEN ISNULL(ct.StateId, 0) = ISNULL(cm.StateId, 0)
                     THEN 'IGST'
                     ELSE 'GST'
                END GstType ,
				CAST( (CASE WHEN ISNULL(ct.StateId, 0) = ISNULL(cm.StateId, 0)
                     THEN 0
                     ELSE 1
                END) AS bit) IsIGST,

				CAST( (CASE WHEN ISNULL(ct.StateId, 0) = ISNULL(cm.StateId, 0)
                     THEN 1
                     ELSE 0
                END) AS bit) IsGST,
                am.VatTds ,
                am.BToB ,
                atds.TdsAccId,
				atds.TcsAccId,
				ISNULL(atds.TdsPer, 0) TdsPer,
                ISNULL(atds.TcsPer, 0) TcsPer,
				am.TdsReq, am.TcsReq,am.DiscPer,
                ISNULL(am.Extra2,'NA') RateType
        FROM    dbo.Acc am
                LEFT OUTER JOIN dbo.AccBal ab ON ab.AccId = am.Id
                LEFT OUTER JOIN dbo.PartyGroup pg ON pg.Id = am.PGroupId
                LEFT OUTER JOIN dbo.AcGroup lg ON lg.Id = ab.GroupId
                LEFT OUTER JOIN dbo.Emp e ON e.Id = am.EmpId
                LEFT OUTER JOIN dbo.Acc ag ON ag.Id = am.AgentId
                LEFT OUTER JOIN dbo.Acc at1 ON at1.Id = am.TransportId
                LEFT OUTER JOIN dbo.Area ar ON ar.Id = ab.AreaId
                LEFT OUTER JOIN dbo.City ct ON ct.Id = ab.CityId
                LEFT OUTER JOIN dbo.[State] st ON st.Id = ct.StateId
                LEFT OUTER JOIN dbo.Company cm ON cm.Id = ab.CompId
                LEFT OUTER JOIN dbo.[State] gst ON gst.Id = cm.StateId
                LEFT OUTER JOIN dbo.AccOther atds ON atds.AccId = am.Id
          
        WHERE   ( @groupid = 0
                  OR lg.Id = @groupid
                )
                AND ( ab.CompId = @companyid
                      AND ab.YearId = @yearid
                    )
    
                    
                    AND( ( @vouchertypeid=0
                      OR (@fillparty='N' AND EXISTS ( SELECT    1
                                  FROM      dbo.Voucher_Book vp
                                  WHERE     vp.GroupId = lg.Id AND vp.VoucherTypeId=@vouchertypeid ))
                    ) 
                    
               or ( @vouchertypeid=0
                      OR (@fillparty='Y' AND EXISTS ( SELECT    1
                                  FROM      dbo.Voucher_Party vp
                                  WHERE     vp.GroupId = lg.Id AND vp.VoucherTypeId=@vouchertypeid ))
                    ) )
               
                AND ( @taxtype = 'N'
                      OR am.VatTds = @taxtype
                    )
                AND (@accountid=0 OR am.id=@accountid)
				AND (am.IsDeleted = 0) AND (@nature='ALL' OR LG.Nature=@nature)
                AND (@outstype='N' OR ISNULL(lg.Extra1,'N')=@outstype)
         order BY am.AccName
    END
		
GO



