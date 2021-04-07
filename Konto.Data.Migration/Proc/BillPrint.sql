IF object_id('[dbo].[BillPrint]') IS NULL 
EXEC ('CREATE PROC [dbo].[BillPrint] AS SELECT 1 AS Id') 
GO 

ALTER PROCEDURE [dbo].[BillPrint] 
@id INT = 0,
  @Bill varchar (1)='N',
      @reportid int=0
AS
BEGIN
     SELECT bm.Id,
	       bm.BillType,
           bm.CompId,
           cm.CompName,
           bm.VoucherId,
           v.VoucherName,
           bm.VoucherDate,
           CONVERT(DATETIME2, CONVERT(VARCHAR(8), bm.VoucherDate), 112) InvoiceDate,
           bm.VoucherNo,
           bm.BillNo ChallanNo,
		   bm.RcdDate AS ChallanDate,
		   bm.Extra2,
           bm.Extra1,
           bm.AccId,
           ac.AccName Party,
		   ac.GstIn,
		   dlv.GstIn delGstIn,
		   dlv.AccName AS DelvParty,
           tr.AccName Transport,
           ag.AccName Agent,
		   hst.HasteName AS Haste,
		   bm.SpecialNotes AS Reason,
           bt.Id DetailId,
           p.ProductName,
           p.ProductDesc,
           p.ProductCode,
           p.BarCode,
           cl.ColorName,
           g.GradeName,
           dm.ProductName DesignNo,
           ISNULL(P.HsnCode, bt.HsnCode) HsnCode,
           bt.Cut,
           bt.Pcs,
           bt.Qty,
           bt.Rate,
           bt.Total,
           um.UnitCode,
           um.UnitName,
           um.GSTUnit,
           bt.Disc,
           bt.DiscAmt,
           bt.FreightRate,
           bt.Freight,
           bm.TcsPer,
           bm.TcsAmt,
           bt.OtherAdd,
           bt.OtherLess,
           bt.OceanFrt ItemOceanFrt,
           bt.CustomD ItemCustomDuty,
           bt.SgstPer,
           bt.Sgst,
           bt.CgstPer,
           bt.Cgst,
           bt.IgstPer,
           bt.Igst,
           bt.NetTotal,
           bt.Remark ItemRemark,
           bm.Duedays,
           bm.VehicleNo,
           bm.DocNo,
           bm.DocDate,
           bm.Remarks,
           bm.EwayBillNo,
           bm.TotalAmount,
           bm.CustomA,
           bm.CustomP,
           bm.OceanFrtP,
           bm.OceanFrtA,
           bm.GrossAmount,
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
           vst.InvoiceHeading,Od.PONo,
		   abl.Bal,
		   bm.RoundOff,
           bt.RefId,
           bm.RefNo as OrderNo,
           devabl.mobileno as DevMobileNo,
           pp.Mrp,ps.SizeName,bk.AccName as BookName,
            bm.RequireDate,tr.GstIn TransGstIn,
            cmp.FAddress1,cmp.FAddress2,fcty.CityName FCity,
            fst.StateName FState
    FROM dbo.BillMain bm
        INNER JOIN dbo.BillTrans bt
            ON bm.Id = bt.BillId
        LEFT OUTER JOIN dbo.Acc ac
            ON ac.Id = bm.AccId
        LEFT OUTER JOIN dbo.Acc bk
            ON bk.Id = bm.BookAcId
        LEFT OUTER JOIN dbo.Acc ag
            ON ag.Id = bm.AgentId
        LEFT OUTER JOIN dbo.Acc tr
            ON tr.Id = bm.TransId
		left OUTER JOIN dbo.AccBal abl ON bm.AccId = abl.AccId AND bm.CompId = abl.CompId AND bm.YearId = abl.YearId 
		LEFT OUTER JOIN dbo.AccAddress acb
			ON acb.Id = abl.AddressId
        LEFT OUTER JOIN dbo.Acc dlv
            ON dlv.Id = bm.DelvAccId
        LEFT OUTER JOIN dbo.Product p
            ON p.Id = bt.ProductId
        LEFT OUTER JOIN dbo.ProductPrice pp
            ON p.Id = pp.ProductId
        left outer join Psize ps on ps.id = p.sizeid
        LEFT OUTER JOIN dbo.Grade g
            ON g.Id = bt.GradeId
        LEFT OUTER JOIN dbo.Color cl
            ON cl.Id = bt.ColorId
        LEFT OUTER JOIN dbo.Product dm
            ON dm.Id = bt.DesignId
        LEFT OUTER JOIN dbo.Uom um
            ON um.Id = bt.UomId
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

        LEFT OUTER JOIN dbo.City fcty
            ON fcty.Id = cm.FCityId
        LEFT OUTER JOIN dbo.[State] fst
            ON fst.Id = cm.FStateId

		LEFT OUTER JOIN dbo.[Haste] hst
            ON hst.Id = bm.HasteId
		LEFT OUTER JOIN dbo.[Challan] ch
			 on ch.Id=bt.RefId
        LEFT OUTER JOIN ( SELECT ct.ChallanId,o.RefNo as PONo FROM ChallanTrans ct
								LEFT OUTER JOIN ( SELECT so.RefNo, ot.Id FROM  dbo.OrdTrans ot 
													LEFT outer join ord so on so.Id=ot.OrdId 
													GROUP by so.RefNo, ot.Id
												) o on o.Id =ct.RefId  
												                  
						group by ct.ChallanId, o.RefNo
				) Od on OD.ChallanId=ch.Id
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

