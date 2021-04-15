IF object_id('[dbo].[ChallanPrint]') IS NULL 
EXEC ('CREATE PROC [dbo].[ChallanPrint] AS SELECT 1 AS Id') 
GO


ALTER   PROC [dbo].[ChallanPrint]
@id INT=0,
 @Challan varchar (1)='N',
 @reportid int=0
AS
BEGIN
SELECT
ROW_NUMBER() OVER( PARTITION BY ct.id ORDER BY cd.Id) AS BoxSr,
 ch.Id,
       ch.CompId,
       cm.CompName,
       ch.VoucherId,
       v.VoucherName,
	   vc.InvoiceHeading,
       ch.VoucherDate,
	   CONVERT(DATETIME2, CONVERT(VARCHAR(8), ch.VoucherDate), 112) ChallanDate,
       ch.VoucherNo,
       ch.ChallanNo,
       ch.AccId,
       ac.AccName Party,
       tr.AccName Transport,
       ag.AccName Agent,
       p.ProductName ProductName,
       isnull(cl.ColorName,cl1.ColorName) as ColorName,
       ISNULL(g.GradeName,g1.GradeName) AS GradeName,
       isnull(ds.ProductName,ds1.ProductName) DesignNo,
	   np.ProductName FinishProduct,
	   ct.Id TransId,
       ct.LotNo LotNo,
       ct.Cops MainCops,
	   CASE WHEN cd.VoucherNo <> '' THEN 1 ELSE ct.Pcs END AS Pcs,
       ct.Qty,
       ct.Rate,
       ct.Gross,
       ct.CgstPer,
       ct.Cgst,
       ct.IgstPer,
       ct.Igst,
       ct.SgstPer,
       ct.Sgst,
       ct.DiscPer,
       ct.Disc,
       ct.FreightRate,
       ct.Freight,
       ct.OtherAdd,
       ct.OtherLess,
       ct.Total,
       ct.Remark,
       uom.UnitName,
       ch.VehicleNo,
       ch.DName,
	   pm.Extra1 BarcodeNo,
       cd.SrNo,
       cd.GrayMtr,
       cd.GrayPcs,
       cd.FinMrt,
	   cd.ShMtr,
	   cd.TP1,
	   cd.TP2,
	   cd.TP3,
	   cd.TP4,
	   cd.TP5,
	   cd.shper,
       pm.VoucherNo BoxNo,
	   beam.VoucherNo BeamNo,
       pm.TwistType,
       pm.Ply,
       pm.Cops,
       pm.CopsWt,
       pm.BoxWt,
       pm.CartnWt,
	   pm.TransId GrnTransId,
    --   pm.GrossWt,
	   CASE WHEN (pm.GrossWt - pm.TareWt) = -1*cd.Qty THEN pm.GrossWt ELSE 0 END AS GrossWt,
    --   pm.TareWt,
	   CASE WHEN (pm.GrossWt - pm.TareWt) = -1*cd.Qty THEN pm.TareWt ELSE 0 END AS TareWt,
       ISNULL(-1*cd.Qty,pm.NetWt) AS NetWt,
       pm.Tops,
       pm.Pallet,
       pm.CurrQty,
       pm.FinQty, 
	   pm.Remark ProdRemark,
	   cm.PrintName PrintCompany,
	   cm.Address1 CAddress,cm.Address2 CAddress2,
	   ccty.CityName CCity,
	   cst.StateName CState,
	   cst.GstCode CGstCode,
	   cm.FAddress1 fAddress1, 
	   cm.Address2 fAddress2,
	   fcty.CityName FCity, 
	   fst.StateName FState,
	   cm.Pincode CPin,
	   cm.Mobile CMobile,
	   cm.Phone CPhone,
	   cm.Email CEmail,
	   cm.Website CWebsite,
	   cm.GstIn CGstIn,
	   cm.PanNo CPanNo,
	   cm.AadharNo CAadharNo,
	   cm.TdsAcNo CTdsAcNo,cm.AcNo CAcNo,
	   cm.BankName CBankName,
	   cm.IfsCode CIfsCode, 
	   cm.Remark CRemark,
	   cm.HolyWorld,
	   vt.Terms,
	   isnull(dadr.Address1,abl.Address1) delAddress1,
	   ISNULL(dadr.Address2,abl.Address2) delAddress2,
	   ISNULL(dar.AreaName,acar.AreaName) delArea,
	   isnull(dct.CityName,acct.CityName) DelCity,
	   isnull(dst.StateName,acst.StateName) DelState,
	   ISNULL(dst.GstCode,acst.GstCode) delGstCode,
	   abl.Address1,abl.Address2,
	   acar.AreaName,acct.CityName,
	   acst.StateName,acst.GstCode,abl.PinCode,abl.ContactPerson,
	   abl.MobileNo,abl.Email,abl.Website,abl.Others,
	   wv.AccName Weaver,cm.LogoPath,ac.GstIn Gstin,dlv.GstIn DelGstIn,dlv.AccName DelvParty
	   ,od.VoucherNo as OrderNo,  CONVERT(DATETIME2, CONVERT(VARCHAR(8), od.VoucherDate), 112)  as OrderDate,
	   od.RefNo as PONo, brc.BranchName,PDes.ProductName as ParentDesign,ch.Remark As ChRemark,
		cmv.ChallanNo PartychallanNo,CONVERT(DATETIME2, CONVERT(VARCHAR(8), cmv.VoucherDate), 112) InwardDate,
		ch.TotalAmount,s.StoreName,pm.LotNo SrLot,p.HsnCode,dadr.MobileNo DevMobileNo,
		ch.Extra2,ch.Extra3,ch.Extra4,ht.HasteName,pro.ProcessName ,dv.DivisionName,
		brt.BranchName ToBranch,p.BarCode, brt.Address1 TbAddress1,
		brt.Address2 TbAddress2
FROM dbo.Challan ch
    LEFT OUTER JOIN dbo.ChallanTrans ct   ON ct.ChallanId = ch.Id
    LEFT OUTER JOIN(SELECT * FROM  dbo.ProdOut cd WHERE cd.IsDeleted=0 )cd ON cd.TransId = ct.Id
    LEFT OUTER JOIN dbo.Prod pm ON pm.Id = cd.ProdId
	LEFT OUTER JOIN dbo.TakaBeam tb ON tb.ProdId = pm.Id and tb.IsDeleted = 0
	LEFT OUTER JOIN dbo.Prod beam ON beam.Id = tb.BeamId
	LEFT OUTER JOIN dbo.Challan cmv ON cmv.Id = pm.RefId AND cmv.VoucherId = pm.VoucherId
	LEFT OUTER JOIN dbo.Acc wv ON wv.Id = cmv.AccId
    LEFT OUTER JOIN dbo.Product p ON p.Id = ct.ProductId
	LEFT OUTER JOIN dbo.Product np ON np.Id = ct.NProductId
    LEFT OUTER JOIN dbo.Color cl ON cl.Id = cd.ColorId
	LEFT OUTER JOIN dbo.Color cl1 ON cl1.Id = ct.ColorId
    LEFT OUTER JOIN dbo.Grade g  ON g.Id = cd.GradeId
	LEFT OUTER JOIN dbo.Grade g1  ON g1.Id = ct.GradeId
	LEFT OUTER JOIN dbo.Batch bch ON bch.Id = pm.BatchId
    LEFT OUTER JOIN dbo.Product ds ON ds.Id = pm.PlyProductId
	LEFT OUTER JOIN dbo.Product ds1 ON ds1.Id = ct.DesignId
	LEFT OUTER JOIN dbo.Product PDes  ON ds1.PartyItemId= PDes.Id
    LEFT OUTER JOIN dbo.Acc ac ON ac.Id = ch.AccId
    LEFT OUTER JOIN dbo.Acc dlv ON dlv.Id = ch.DelvAccId
    LEFT OUTER JOIN dbo.Acc tr ON tr.Id = ch.TransId
    LEFT OUTER JOIN dbo.Voucher v ON v.Id = ch.VoucherId
	LEFT OUTER JOIN dbo.VoucherType vt ON vt.Id = V.VTypeId
	LEFT OUTER JOIN dbo.VchSetup vc ON vc.VoucherId = ch.VoucherId AND vc.CompId = ch.CompId AND vc.IsDeleted = 0
    LEFT OUTER JOIN dbo.Division dv ON dv.Id = ch.DivId
    LEFT OUTER JOIN dbo.TransType ctype ON ctype.Id = ch.ChallanType
    LEFT OUTER JOIN dbo.Company cm ON cm.Id = ch.CompId
	LEFT OUTER JOIN dbo.City ccty ON ccty.Id = cm.CityId
	LEFT OUTER JOIN dbo.[State] cst ON cst.Id = cm.StateId
	LEFT OUTER JOIN dbo.City fcty ON fcty.Id = cm.FCityId
	LEFT OUTER JOIN dbo.[State] fst ON fst.Id = cm.FStateId
    LEFT OUTER JOIN dbo.Acc ag ON ag.Id = ac.AgentId
    LEFT OUTER JOIN dbo.Uom uom ON uom.Id = ct.UomId 
	LEFT OUTER JOIN dbo.AccBal dadr ON dadr.AccId = ch.DelvAccId  AND ch.CompId = dadr.CompId AND ch.YearId = dadr.YearId 
	LEFT OUTER JOIN dbo.Area dar ON dar.Id = dadr.AreaId
	LEFT OUTER JOIN dbo.city dct ON dct.Id = dadr.CityId
	LEFT OUTER JOIN dbo.[State] dst ON dst.Id = dct.StateId
	left OUTER JOIN dbo.AccBal abl ON ch.AccId = abl.AccId AND ch.CompId = abl.CompId AND ch.YearId = abl.YearId 
	LEFT OUTER JOIN dbo.AccAddress acadr ON acadr.Id = abl.AddressId
	LEFT OUTER JOIN dbo.Area acar ON acar.Id = acadr.AreaId
	LEFT OUTER JOIN dbo.city acct ON acct.Id = acadr.CityId
	LEFT OUTER JOIN dbo.[State] acst ON acst.Id = acct.StateId	
	LEFT OUTER JOIN dbo.haste ht on ht.id = ch.MasterId
	left outer join dbo.Process pro on pro.Id = ch.ProcessId

	LEFT OUTER JOIN ( SELECT so.RefNo, so.VoucherDate, so.VoucherNo, ot.Id FROM  dbo.OrdTrans ot 
						left outer join dbo.ord so on so.Id=ot.OrdId 
						group by so.RefNo, so.VoucherNo, so.VoucherDate,ot.Id
				) od on od.Id =ct.RefId
	LEFT OUTER JOIN dbo.Branch brc ON brc.Id = ch.BranchId
	LEFT OUTER JOIN dbo.Branch brt ON brt.Id = ch.ToBranchId
	 LEFT OUTER JOIN Store s on s.Id=ch.StoreId
 -- LEFT OUTER JOIN challan inward ON ct.MiscId = inward.Id AND ct.RefVoucherId = inward.VoucherId
    WHERE (@id=0 or ch.Id=@id) AND ct.IsDeleted=0 AND (@Challan='N' OR  EXISTs(SELECT 1
							 FROM dbo.ReportPara RP
							 WHERE RP.ParameterValue = ch.Id
							AND RP.ReportId = @reportid
							AND RP.ParameterName='ChallanId'))
END
GO

