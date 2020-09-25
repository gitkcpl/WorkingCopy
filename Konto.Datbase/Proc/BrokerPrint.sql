CREATE PROCEDURE [dbo].[BrokerPrint] 
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
		   bm.RcdDate AS ChallanDate,
		   bm.Extra2 OrderNo,
           bm.AccId,
           ac.AccName BrokerName,
		   tacc.AccName as TPartyName,
		   ac.GstIn,
		   dlv.GstIn delGstIn,
		   dlv.AccName AS DelvParty,
           tr.AccName Transport,
           ag.AccName Agent,
		   hst.HasteName AS Haste,
		   bm.SpecialNotes AS Reason,
           bt.Id DetailId,      
           bt.Total As ABillAmount,
           bt.Disc,
           bt.DiscAmt,
           bt.FreightRate,
           bt.Freight,
           bt.TcsPer,
           bt.TcsAmt,
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
           bt.NetTotal as BillAmount,
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
		    bt.ChequeDate As BillDate,
		   bt.ChequeNo as BillNo,
		   bt.Disc as DiscPer,
		   bt.DiscAmt as BDiscAmt,
		   bt.TdsAmt as RG,
		   CONVERT(DATETIME2, bts.ChequeDate, 112) as ChequeDate ,
		   CONVERT(DATETIME2, CONVERT(VARCHAR(8), bm.VoucherDate), 112) PayDate,
		   bts.ChequeNo,
		   btb.Amount Cheque_Cash_Amount,
           bt.IgstPer BrokerPer,
           bt.Igst BrokerAmt
			 FROM dbo.BillMain bm
        INNER JOIN dbo.BillTrans bt
            ON bm.Id = bt.BillId
		left outer join acc tacc 
			on tacc.Id = bt.ToAccId
        LEFT OUTER JOIN dbo.Acc ac
            ON ac.Id = bm.AccId
        LEFT OUTER JOIN dbo.Acc bk
            ON bk.Id = bm.BookAcId
        LEFT OUTER JOIN dbo.Acc ag
            ON ag.Id = ac.AgentId
        LEFT OUTER JOIN dbo.Acc tr
            ON tr.Id = bm.TransId
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
		LEFT OUTER JOIN dbo.[Challan] ch
			 on ch.Id=bt.RefId
		left outer join BtoB  btb
			 on btb.BillNo = bt.ChequeNo
			 and (btb.RefVoucherId = 16 or btb.RefVoucherId = 15)
			 left outer join BillTrans bts on bts.Id = btb.RefTransId
			 left outer join BillMain bms on bms.Id = bts.BillId
        LEFT OUTER JOIN ( SELECT ct.ChallanId,o.RefNo as PONo FROM ChallanTrans ct
								LEFT OUTER JOIN ( SELECT so.RefNo, ot.Id FROM  dbo.OrdTrans ot 
													LEFT outer join ord so on so.Id=ot.OrdId 
													GROUP by so.RefNo, ot.Id
												) o on o.Id =ct.RefId  
												                  
						group by ct.ChallanId, o.RefNo
				) Od on OD.ChallanId=ch.Id
		WHERE(@id=0 or bm.Id=@id) AND bt.IsDeleted=0
		AND (@Bill ='N' OR  EXISTs(SELECT 1
							 FROM dbo.ReportPara RP
							 WHERE RP.ParameterValue = bm.id
							AND RP.ReportId=@reportid
							AND RP.ParameterName='BillId'))
END
GO

