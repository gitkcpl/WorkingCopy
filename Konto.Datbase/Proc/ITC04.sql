create PROCEDURE [dbo].[ITC04]
 @CompanyId INT ,
 @FromDate  INT ,
 @ToDate INT,
 @yearid INT
as
BEGIN
select * from (
	select a.GstIn,
	st.GstCode + '-' + st.StateName as StateName,
	v.VoucherName AS JobType,
	v.VTypeId,
		c.BillType As JobWorkersType,
		c.VoucherNo As ChallanNo,
		c.VoucherDate,
		ISNULL(CONVERT(Date,Convert(varchar(8),c.VoucherDate),112),'') as ChallanDate,
		c.Itc As TypesOfGoods,
		p.ProductName as Descriptionofgoods,
		u.UnitName As UQC,
		sum(ct.Qty) as Quality,
		SUM(ct.Total) - SUM(ct.Sgst) - SUM(ct.Cgst) - SUM(ct.Igst) AS TaxableValue,
		ct.sgstper,
		ct.CgstPer,
		ct.IgstPer ,
		SUM(ct.Cess) AS Cess,
		CASE WHEN c.IsDeleted=0 THEN 'ADD' ELSE 'DELETE' END  AS Actions ,
		CASE when v.VTypeId = 38 Then 'JobIssue' else 'MillIssue' End Types,
		null OriClnDate,
		null OriClnNo,
		0 PendingQty
		 from challan  c

			left outer join ChallanTrans ct 
				on ct.ChallanId = c.Id
			inner join Voucher v
				on v.Id = c.VoucherId and  (v.VTypeId = 38 or v.VTypeId=37)

			left outer join acc a 
				on a.Id = c.AccId

			left outer join AccBal ab
				on ab.AccId = a.Id

			left outer join City cty
				on cty.Id = ab.CityId

			left outer join State st
				on st.Id = cty.StateId

			left outer join Product p
				on p.Id = ct.ProductId

			left outer join Uom u
				on u.Id = ct.UomId
				where c.CompId = @CompanyId and c.VoucherDate between @FromDate and @ToDate and ab.YearId = @yearid and 
				c.IsActive = 1 and c.IsDeleted = 0
				group by a.GstIn,st.StateName,c.BillType,c.VoucherNo,
				c.Itc,u.UnitName,ct.sgstper,ct.CgstPer,
				ct.IgstPer,p.ProductName,st.GstCode,c.VoucherDate,c.IsDeleted,v.VTypeId,c.ChallanNo ,
		c.RcdDate ,v.VoucherName,v.VTypeId

		Union all

		select a.GstIn,
	st.GstCode + '-' + st.StateName as StateName,
	v.VoucherName AS JobType,
	v.VTypeId,
		c.BillType As JobWorkersType,
		c.VoucherNo As ChallanNo,
		c.VoucherDate,
		ISNULL(CONVERT(Date,Convert(varchar(8),c.VoucherDate),112),'') as ChallanDate,
		c.Itc As TypesOfGoods,
		p.ProductName as Descriptionofgoods,
		u.UnitName As UQC,
		sum(ct.Qty) as Quality,
		SUM(ct.Total) - SUM(ct.Sgst) - SUM(ct.Cgst) - SUM(ct.Igst) AS TaxableValue,
		ct.sgstper,
		ct.CgstPer,
		ct.IgstPer ,
		SUM(ct.Cess) AS Cess,
		CASE WHEN c.IsDeleted=0 THEN 'ADD' ELSE 'DELETE' END  AS Actions ,
		CASE when v.VTypeId = 38 Then 'JobIssue' else 'JobReceipt' End Types,
		jr.OriClnDate OriClnDate,
		jr.OriClnNo OriClnNo,
		SUM(jr.PendingMeters) PendingQty
		 from challan  c

			left outer join ChallanTrans ct 
				on ct.ChallanId = c.Id
			inner join Voucher v
				on v.Id = c.VoucherId and  (v.VTypeId = 8)

			left outer join acc a 
				on a.Id = c.AccId

			left outer join AccBal ab
				on ab.AccId = a.Id

			left outer join City cty
				on cty.Id = ab.CityId

			left outer join State st
				on st.Id = cty.StateId

			left outer join Product p
				on p.Id = ct.ProductId

			left outer join Uom u
				on u.Id = ct.UomId
		inner join 	(select jr.ChallanId,jr.VoucherId,cts.VoucherNo As OriClnNo,
		ISNULL(CONVERT(Date,Convert(varchar(8),cts.VoucherDate),112),'') as OriClnDate
		,isnull(ctc.Qty,0) -isnull(jr.Qty,0) As PendingMeters from JobReceipt jr									
									left outer join Challan cts 
										on cts.id=jr.RefId
									left outer join ChallanTrans ctc
										on ctc.Id = jr.RefTransId
										 where jr.IsActive = 1 and jr.IsDeleted=0  and cts.VoucherDate is not null) as jr on jr.ChallanId=c.Id and jr.VoucherId=c.VoucherId
where c.CompId = @CompanyId and c.VoucherDate between @FromDate and @ToDate and ab.YearId = @yearid and 
				c.IsActive = 1 and c.IsDeleted = 0
				group by a.GstIn,st.StateName,c.BillType,c.VoucherNo,
				c.Itc,u.UnitName,ct.sgstper,ct.CgstPer,
				ct.IgstPer,p.ProductName,st.GstCode,c.VoucherDate,c.IsDeleted,v.VTypeId,c.ChallanNo ,
		c.RcdDate,jr.OriClnDate,jr.OriClnNo ,v.VoucherName,v.VTypeId

		union all

		select a.GstIn,
	st.GstCode + '-' + st.StateName as StateName,
	jr.VoucherName AS JobType,
	v.VTypeId,
		c.BillType As JobWorkersType,
		jr.ReceiptVNo As ChallanNo,
		c.VoucherDate,
		jr.ReceiptDate as ChallanDate,
		c.Itc As TypesOfGoods,
		p.ProductName as Descriptionofgoods,
		u.UnitName As UQC,
		sum(ct.Qty) as Quality,
		SUM(ct.Total) - SUM(ct.Sgst) - SUM(ct.Cgst) - SUM(ct.Igst) AS TaxableValue,
		ct.sgstper,
		ct.CgstPer,
		ct.IgstPer ,
		SUM(ct.Cess) AS Cess,
		CASE WHEN c.IsDeleted=0 THEN 'ADD' ELSE 'DELETE' END  AS Actions ,
		jr.VType as Types,
		ISNULL(CONVERT(Date,Convert(varchar(8),c.VoucherDate),112),'') as OriClnDate,
		VoucherNo as  OriClnNo,
		sum(isnull((ct.Qty-jr.FinishMtr),0)) as  PendingQty
		 from challan  c

			left outer join ChallanTrans ct 
				on ct.ChallanId = c.Id
			inner join Voucher v
				on v.Id = c.VoucherId and  (v.VTypeId = 37)

			left outer join acc a 
				on a.Id = c.AccId

			left outer join AccBal ab
				on ab.AccId = a.Id

			left outer join City cty
				on cty.Id = ab.CityId

			left outer join State st
				on st.Id = cty.StateId

			left outer join Product p
				on p.Id = ct.ProductId

			left outer join Uom u
				on u.Id = ct.UomId
		inner join 	(
		
		select cts.RefId as IssueTRansID,cts.MiscId As IssueID,cts.RefVoucherId As IssueVoucherID ,v.VoucherName,
		CASE when v.VTypeId = 7 Then 'MillReceipt' else 'JobReceipt' End VType,
		ISNULL(CONVERT(Date,Convert(varchar(8),cs.VoucherDate),112),'') as ReceiptDate,
		cs.VoucherNo as ReceiptVNo,cts.Qty As FinishMtr
		from ChallanTrans cts  
left outer join Challan cs on cs.Id = cts.ChallanId 
			inner join Voucher v
				on v.Id = cs.VoucherId 
where cs.IsActive = 1 and cs.IsDeleted = 0 and cs.VoucherId = 7

) as jr on jr.IssueID=c.Id and jr.IssueTRansID=ct.Id and jr.IssueVoucherID = c.VoucherId 
where c.CompId = @CompanyId and c.VoucherDate between @FromDate and @ToDate and ab.YearId = @yearid and 
				c.IsActive = 1 and c.IsDeleted = 0
				group by a.GstIn,st.StateName,c.BillType,c.VoucherNo,
				c.Itc,u.UnitName,ct.sgstper,ct.CgstPer,
				ct.IgstPer,p.ProductName,st.GstCode,c.VoucherDate,c.IsDeleted,v.VTypeId,c.ChallanNo ,
		c.RcdDate,jr.ReceiptDate,jr.ReceiptVNo ,v.VoucherName,v.VTypeId,jr.VType,jr.VoucherName



		)s
END