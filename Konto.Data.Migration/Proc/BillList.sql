IF object_id('[dbo].[BillList]') IS NULL 
EXEC ('CREATE PROC [dbo].[BillList] AS SELECT 1 AS Id') 
GO 

ALTER PROCEDURE [dbo].[BillList]
@VTypeId int,
 @CompanyId int,
 @BranchId int,
 @YearId int,
 @FromDate int,
 @ToDate INT,
 @Cancelled INT= 1,
 @Deleted INT = 0

AS
BEGIN  
	  SELECT  bm.VoucherNo,
	  v.VoucherName,
	  CASE WHEN bm.VoucherDate>10101 THEN ISNULL(CONVERT(Date,Convert(varchar(8),bm.VoucherDate),112),'') END AS BillDate,
	  bm.BillNo,
	  bk.AccName Book,
	  ac.AccName PARTY,
	  ISNULL(ag.AccName,Agent.AccName) Agent,
	  bm.TotalPcs,bm.TotalQty,
	  bm.GrossAmount,ISNULL(bt.Taxable,0) TaxableAmt,
	  bt.Cgst,
	  bt.Sgst,
	  bt.Igst,
	  bm.TotalAmount,
	  ISNULL(bl.Pay,0) PaidAmt,
	  ISNULL(bl.Rg,0) RetAmt,
	  CASE WHEN 
	  bm.TotalAmount - ISNULL(bl.Pay,0)- ISNULL(bl.Rg,0) - ISNULL(bp1.CashAmt,0)-ISNULL(bp1.CardAmt,0)-ISNULL(bp1.WalletAmt,0)-ISNULL(bp1.OthersAmt,0)-ISNULL(bp1.PostDisc,0) = 0 THEN 'PAID' ELSE 'UNPAID' END AS Status,
	  bm.BillType, bm.Extra1 Against ,
	  bm.DName Driver,
	  bm.DocDate LrDate,
	  bm.DocNo LrNo,
	  bm.Duedays,bm.EwayBillNo,
	  bm.ExchRate,
	  bm.Itc,bm.ModeofTrans,
	  bm.PortCode,bm.RcdDate,
	  bm.Rcm,
	  bm.RefNo,bm.Remarks,
	  bm.RequireDate ,
	  bm.SpecialNotes CancelReason, 
	  bm.TcsPer,
	  bm.TcsAmt TotalTcsAmt,
	  bm.TdsPer,
	  bm.TdsAmt TotalTdsAmt,
	  bm.VDate,
	  bm.VehicleNo,
	  st.StateName as Pos,
	  bm.Currency,
	  bm.CustomA CustomAmount,
	  bm.OceanFrtA,
	  bm.OceanFrtP,
	  bm.CreateUser,
	  bm.CreateDate,
	  bm.ModifyUser,
	  bm.ModifyDate,
	  bm.VoucherId,bm.Id,
	  bm.IsActive,
	  bm.IsDeleted,
	  ac.GstIn,
	   case when isnumeric(bm.Extra2)=1 then cast(bm.extra2 as numeric(18,2)) else 0 end Boxes,
	   bm.Extra2,at.AccName Transport, cc.HeadName CostCenter,
	   ISNULL(bp1.CashAmt,0) CashAmt,ISNULL(bp1.CardAmt,0) CardAmt,
	   ISNULL(bp1.WalletAmt,0)WalletAmt,ISNULL(bp1.OthersAmt,0) Others,
	   ISNULL(bp1.PostDisc,0) PostDisc,
	   bm.TotalAmount - ISNULL(bl.Pay,0)- ISNULL(bl.Rg,0) - ISNULL(bp1.CashAmt,0)-ISNULL(bp1.CardAmt,0)-ISNULL(bp1.WalletAmt,0)-ISNULL(bp1.OthersAmt,0) -ISNULL(bp1.PostDisc,0) PendAmt, 
	   cast( CASe when gstr.BillId is null then 0 else 1 end as bit)  Gstr2A,
	   ct.CityName City,
	   isnull(adr.[Address1],'NA') + ' ' +isnull(adr.[Address2],'NA') + ' ' + isnull(ar.AreaName,'NA')  FullAddress
 	 FROM dbo.BillMain bm
	  LEFT OUTER JOIN (SELECT BillId, SUM(DiscAmt) disc,SUM(Cess) AS Cess, 
		SUM(OtherAdd) AS OtherA, SUM(OtherLess) AS OtherL, SUM(Cgst) Cgst, SUM(Sgst) Sgst, SUM(Igst) Igst,
		SUM(NetTotal-ISNULL(Sgst,0)-ISNULL(Cgst,0)-ISNULL(Igst,0)-ISNULL(Cess,0)) Taxable FROM dbo.BillTrans
		WHERE IsDeleted=0 GROUP BY BillId) bt ON bt.BillId = bm.Id
	 LEFT OUTER JOIN  dbo.Acc ac on bm.AccId =ac.Id 
	 LEFT OUTER JOIN dbo.cost_heads cc on cc.Id = bm.CostHeadId
	 LEFT OUTER JOIN dbo.Acc bk ON bk.Id = bm.BookAcId
     LEFT OUTER JOIN dbo.Acc Agent on ac.AgentId = Agent.Id
	 LEFT OUTER JOIN dbo.Acc ag ON ag.Id = bm.AgentId
	  LEFT OUTER JOIN dbo.Acc at ON at.Id = bm.TransId
     LEFT OUTER JOIN  dbo.Voucher v on bm.VoucherId =v.Id
     LEFT OUTER JOIN dbo.AccAddress adr on bm.DelvAdrId =adr.Id
     LEFT OUTER JOIN dbo.Area AS ar ON adr.AreaId = ar.Id
     LEFT OUTER JOIN dbo.City AS ct  ON adr.CityId = ct.Id
	 LEFT OUTER JOIN dbo.[State] st on st.Id = bm.StateId
	 Left outer join dbo.gstr2a_dump gstr on gstr.BillId = bm.Id
	 LEFT OUTER JOIN(SELECT bp.BillId,
	 SUM(bp.Pay1Amt-bp.ChangeAmt) AS CashAmt,
	 SUM(bp.DiscAmt) PostDisc,
	SUM(CASE WHEN ISNULL(h2.PanNo,'X')='CARD' THEN bp.Pay2Amt
	 WHEN ISNULL(h3.PanNo,'X')='CARD' THEN bp.Pay3Amt END) CardAmt,
	 SUM(CASE WHEN ISNULL(h2.PanNo,'X')='WALLET' THEN bp.Pay2Amt
	 WHEN ISNULL(h3.PanNo,'X')='WALLET' THEN bp.Pay3Amt END) WalletAmt,
	 SUM(CASE WHEN ISNULL(h2.PanNo,'X')='OTHERS' THEN bp.Pay2Amt
	 WHEN ISNULL(h3.PanNo,'X')='OTHERS' THEN bp.Pay3Amt END) OthersAmt
	  FROM bill_pays AS bp
	  LEFT OUTER JOIN Haste AS h2 ON bp.Pay2Id = h2.id 
	  LEFT OUTER JOIN Haste AS h3 ON bp.Pay3Id = h3.id
		GROUP BY bp.BillId
	  )bp1 ON bp1.BillId = bm.id	
	  
	 LEFT OUTER JOIN (SELECT BillId, 
				SUM(CASE WHEN TransType = 'Payment' THEN Amount + ISNULL(Adla1,0) + ISNULL(Adla2,0) +ISNULL(Adla3,0) +ISNULL(Adla4,0) +ISNULL(Adla5,0) +ISNULL(Adla6,0) +ISNULL(Adla7,0) +ISNULL(Adla8,0) +ISNULL(Adla9,0) +ISNULL(Adla10,0) ELSE 0 END) Pay , 
				SUM(CASE WHEN TransType = 'Return' THEN Amount + ISNULL(Adla1,0) + ISNULL(Adla2,0) +ISNULL(Adla3,0) +ISNULL(Adla4,0) +ISNULL(Adla5,0) +ISNULL(Adla6,0) +ISNULL(Adla7,0) +ISNULL(Adla8,0) +ISNULL(Adla9,0) +ISNULL(Adla10,0) ELSE 0 END) Rg  FROM dbo.BtoB GROUP BY BillId) bl ON bl.BillId = bm.Id
 
    WHERE (bm.IsDeleted=@Deleted AND bm.IsActive = @Cancelled) AND bm.CompId = @CompanyId AND bm.YearId = @YearId
    AND (bm.VoucherDate  between @FromDate and @ToDate) and v.VTypeId=@VTypeId
   AND (@BranchId=0 OR  (bm.BranchId is null or bm.BranchId=@BranchId)) 

    ORDER by bm.VoucherDate DESC, CAST(bm.BillNo AS VARBINARY(MAX)) DESC
    
END
Go
