IF object_id('[dbo].[JvPrint]') IS NULL 
EXEC ('CREATE PROC [dbo].[JvPrint] AS SELECT 1 AS Id') 
GO 


AlTER PROCEDURE [dbo].[JvPrint] 
@id INT = 0,
@Bill varchar (1)='N',
@reportid int=0

AS

BEGIN
     SELECT bm.Id,
	       bm.CompId,
           cm.CompName,
           bm.VoucherId,
           v.VoucherName,
           bm.VoucherDate,
           CONVERT(DATETIME2, CONVERT(VARCHAR(8), bm.VoucherDate), 112) InvoiceDate,
           bm.VoucherNo,
          bm.SpecialNotes AS Reason,
		  bt.RpType,
		  bt.ToAccId,
         
           ac.AccName Particular,
		   ac.GstIn,
		   dlv.GstIn delGstIn,
		   dlv.AccName AS DelvParty,
         
		   hst.HasteName AS Haste,
		   
           bt.Id DetailId,
          
           bt.Cut,
           bt.Pcs,
           bt.Qty,
           bt.Rate,
           bt.Total CrAmt,
           
           bt.NetTotal DrAmt,
           bt.Remark ItemRemark,
           bm.Duedays,
           bm.VehicleNo,
           bm.DocNo,
           bm.DocDate,
           bm.Remarks,
           bm.EwayBillNo,
           
           cm.PrintName PrintCompany,
           cm.Address1 CAddress,
           cm.Address2 CAddress2,
           ccty.CityName CCity,
           cst.StateName CState,
           cst.GstCode CGstCode,
           cm.Pincode CPin,
           cm.Mobile CMobile,
           cm.Phone CPhone,
           cm.Email CEmail,
           cm.Website CWebsite,
           cm.GstIn CGstIn,
           cm.PanNo CPanNo,
           cm.AadharNo CAadharNo,
           cm.TdsAcNo CTdsAcNo,
           cm.AcNo CAcNo,
           cm.BankName CBankName,
           cm.IfsCode CIfsCode,
           cm.Remark CRemark,
           vtp.Terms,
           isnull(dadr.Address1,abl.Address1) delAddress1,
           isnull(dadr.Address2,abl.Address2) delAddress2,
           isnull(dar.AreaName,acar.AreaName) delArea,
           isnull(dct.CityName,acct.CityName) DelCity,
           isnull(dst.StateName,acst.StateName) DelState,
           isnull(dst.GstCode,acst.GstCode) delGstCode,
           abl.Address1,
           abl.Address2,
           acar.AreaName,
           acct.CityName,
           acst.StateName,
           acst.GstCode AS GstCode,
           abl.PinCode,
           abl.ContactPerson,
           abl.MobileNo,
           abl.Email,
           abl.Website,
           abl.Others,
           cm.HolyWorld,
		   cm.LogoPath,
           vst.InvoiceHeading,
		   abl.Bal,
		   bm.RoundOff,
           bt.RefId,
           bm.RefNo 
           
    FROM dbo.BillMain bm
        INNER JOIN dbo.BillTrans bt
            ON bm.Id = bt.BillId
        LEFT OUTER JOIN dbo.Acc ac
            ON ac.Id = bt.ToAccId
      
		left OUTER JOIN dbo.AccBal abl ON bm.AccId = abl.AccId AND bm.CompId = abl.CompId AND bm.YearId = abl.YearId 
		LEFT OUTER JOIN dbo.AccAddress acb
			ON acb.Id = abl.AddressId
        LEFT OUTER JOIN dbo.Acc dlv
            ON dlv.Id = bm.DelvAccId
       
        LEFT OUTER JOIN dbo.Company cmp
            ON cmp.Id = bm.CompId
        LEFT OUTER JOIN dbo.Voucher v
            ON v.Id = bm.VoucherId
        LEFT OUTER JOIN dbo.VchSetup vst
            ON vst.VoucherId = v.Id AND vst.CompId = bm.CompId 
        LEFT OUTER JOIN dbo.VoucherType vtp
            ON vtp.Id = v.VTypeId
        LEFT OUTER JOIN dbo.AccAddress dadr
            ON dadr.Id = bm.DelvAdrId
        LEFT OUTER JOIN dbo.Area dar
            ON dar.Id = dadr.AreaId
        LEFT OUTER JOIN dbo.City dct
            ON dct.Id = dadr.CityId
        LEFT OUTER JOIN dbo.[State] dst
            ON dst.Id = dct.StateId
        LEFT OUTER JOIN dbo.Area acar
            ON acar.Id = acb.AreaId
        LEFT OUTER JOIN dbo.City acct
            ON acct.Id = acb.CityId
        LEFT OUTER JOIN dbo.[State] acst
            ON acst.Id = acct.StateId
        LEFT OUTER JOIN dbo.Company cm
            ON cm.Id = bm.CompId
        LEFT OUTER JOIN dbo.City ccty
            ON ccty.Id = cm.CityId
        LEFT OUTER JOIN dbo.[State] cst
            ON cst.Id = cm.StateId
		LEFT OUTER JOIN dbo.[Haste] hst
            ON hst.Id = bm.HasteId

	    left OUTER JOIN dbo.AccBal devabl ON bm.DelvAccId = devabl.AccId 
        AND bm.CompId = devabl.CompId AND bm.YearId = devabl.YearId    
		WHERE(@id=0 or bm.Id=@id) AND bt.IsDeleted=0
		AND (@Bill ='N' OR  EXISTs(SELECT 1
							 FROM dbo.ReportPara RP
							 WHERE RP.ParameterValue = bm.Id
							AND RP.ReportId=@reportid
							AND RP.ParameterName='BillId'))
END

GO

