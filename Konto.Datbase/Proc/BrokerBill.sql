CREATE PROCEDURE [dbo].[BrokerBill]
 	@CompanyId  INT = 1,
	@AccountId INT = 0,
	@FromDate INT =20180401,
	@ToDate INT = 20190331,
	@AgentID INT=0  
AS
BEGIN
select a.AccName,a.AgentName,a.ToAccId,a.BillId,a.BillNo,a.NetTotal,a.PaidAmt,a.RetAmt,a.Amount,a.BillVoucherId,
ISNULL(CONVERT(Date,Convert(varchar(8),a.ChlnDate),112),'')  as ChlnDate,a.DiscPer,a.DiscAmt from 
(select 
     br.BillId,
	 ISNULL(br.BillTransId,0) AS BillTransId,
	 br.BillVoucherId,br.VoucherNo AS ChallanNo, 
	 br.RowId AS RefCode, br.AccountId AS ToAccId,
	 br.BillNo, 
	 br.VoucherDate AS ChlnDate,
	 v.VoucherName,
	 a.AccName,
	 age.AccName AgentName,
	 dper.DiscPer,
	 dper.DiscAmt,
	 ISNULL(br.GrossAmt,0.00) AS Total,
	 ISNULL(br.BillAmt,0.00) AS NetTotal,
	 ISNULL(adj.Pay,0.00) + ISNULL(selfa.Amount,0) + ISNULL(br.AdjustAmt,0) AS PaidAmt,
	 ISNULL(br.TdsAmt,0.00) AS TdsAmt,
	 (ISNULL(adj.Rg,0.00)+ ISNULL(br.RetAmt,0)) AS RetAmt ,	

	 ROUND(br.BillAmt-ISNULL(br.TdsAmt,0) + ISNULL(br.TcsAmt,0) - ISNULL(selfa.Amount,0) - ISNULL(adj.Rg,0) -ISNULL(adj.Pay,0) +
	 ISNULL(bb.Pay,0) + ISNULL(bb.Rg,0) ,0) - ISNULL(br.RetAmt,0) - ISNULL(br.AdjustAmt,0) AS DueAmt, CAST(bb.Amount AS NUMERIC(18)) AS Amount
 from BillRef br
inner join BillMain bm
on br.BillNo = bm.VoucherNo  and  
  br.BillVoucherId = bm.VoucherId
  left outer join 	Voucher v on bm.VoucherId =v.Id
  left outer join   acc age on age.id=bm.AgentId 		 
  left outer join   acc a on a.Id = bm.AccId 
  left outer  JOIN (
			SELECT SUM(CASE WHEN b.TransType = 'Payment' THEN b.Amount + ISNULL(b.Adla1,0) + ISNULL(b.Adla2,0) + ISNULL(b.Adla3,0) + ISNULL(b.Adla4,0) + ISNULL(b.Adla5,0) + ISNULL(b.Adla6,0) + ISNULL(b.Adla7,0) + ISNULL(b.Adla8,0) + ISNULL(b.Adla9,0) + ISNULL(b.Adla10,0) ELSE 0 END) Pay , SUM(CASE WHEN b.TransType = 'Return' THEN b.Amount ELSE 0 END) Rg ,
                      b.RefCode
					              FROM dbo.BtoB b
								  WHERE b.IsActive = 1 AND b.IsDeleted = 0 
								    GROUP BY  b.RefCode
	) adj ON adj.RefCode = br.RowId 
	LEFT OUTER JOIN (select BillId , sum(Disc) as DiscPer,sum(DiscAmt) as DiscAmt from BillTrans group by BillId) dper on  dper.BillId=br.BillId	

	LEFT OUTER JOIN ( 
				SELECT SUM(bb.Amount) AS Amount,SUM(ISNULL(bb.Adla1,0)) AS Adla1,SUM(ISNULL(bb.Adla2,0)) AS Adla2,SUM(ISNULL(bb.Adla3,0)) AS Adla3,
				SUM(ISNULL(bb.Adla4,0)) AS Adla4,
				SUM(ISNULL(bb.Adla5,0)) AS Adla5,SUM(ISNULL(bb.Adla6,0)) AS Adla6,SUM(ISNULL(bb.Adla7,0)) AS Adla7,SUM(ISNULL(bb.Adla8,0)) AS Adla8,
				SUM(ISNULL(bb.Adla9,0)) AS Adla9,
				SUM(ISNULL(bb.Adla10,0)) AS Adla10,
				SUM(CASE WHEN bb.TransType = 'Payment' THEN bb.Amount ELSE 0 END) Pay , 
				SUM(CASE WHEN bb.TransType = 'Return' THEN bb.Amount ELSE 0 END) Rg ,
				bb.RefCode
				FROM dbo.BtoB bb 
				WHERE bb.IsActive =1 AND bb.IsDeleted = 0 
				GROUP BY bb.RefCode) bb 
								ON bb.RefCode = br.RowId 


				LEFT OUTER JOIN (
					SELECT SUM(selfa.Amount) + SUM(ISNULL(selfa.Adla1,0)) + SUM(ISNULL(selfa.Adla2,0)) + SUM(ISNULL(selfa.Adla3,0)) + SUM(ISNULL(selfa.Adla4,0)) 
					+ SUM(ISNULL(selfa.Adla5,0)) + SUM(ISNULL(selfa.Adla6,0)) + SUM(ISNULL(selfa.Adla7,0)) + SUM(ISNULL(selfa.Adla8,0)) + SUM(ISNULL(selfa.Adla9,0)) 
					+ SUM(ISNULL(selfa.Adla10,0)) AS Amount,selfa.RefId, selfa.RefVoucherId FROM dbo.BtoB selfa 
					WHERE selfa.IsActive =1 AND selfa.IsDeleted = 0 
					GROUP BY selfa.RefId,selfa.RefVoucherId
					) selfa ON selfa.RefId = br.BillId AND selfa.RefVoucherId = br.BillVoucherId					
  where bm.AgentId is not null  and 
  br.VoucherDate between @FromDate and @ToDate and age.Id = @AgentID 
  and (a.Id= @AccountId or @AccountId = 0) 
  and (br.CompanyId = @CompanyId or @CompanyId = 0) and
 (ISNULL(adj.Pay,0.00) + ISNULL(selfa.Amount,0) + ISNULL(br.AdjustAmt,0)) > 0
  )a 

  where a.BillId not in(select ChequeNo  from BillTrans bt left outer join BillMain bm on bm.Id = bt.BillId 
		left outer join Voucher v on v.Id=bm.VoucherId 
		where bt.IsDeleted=0 and bt.IsActive = 1 and v.VTypeId = 49) 
END
GO

