CREATE PROCEDURE [dbo].[ChequeLetterPrint] 
@VoucherID INT = 0,
@FrmBill varchar (50) =0,
@ToBill varchar(50) =0,
@Bill VARCHAR(1) = 'N',
@reportid INT = 0


AS
BEGIN
     SELECT  
	  CASE WHEN bm.VoucherDate>10101 THEN ISNULL(CONVERT(Date,Convert(varchar(8),bm.VoucherDate),112),'')  END as InvoiceDate,bm.VoucherNo,
	  CASE WHEN bbm.VoucherDate>10101 THEN ISNULL(CONVERT(Date,Convert(varchar(8),bbm.VoucherDate),112),'')  END as BillDate,
	  bbm.BillNo As BillNo,
	  bbm.TotalAmount As BillAmount,
	  isnull(ret.RetAmount,0) As ReturnAmount,
	  ISNULL(advpay.AdvPayAmt,0) As AdvAmt,
	  cm.CompName,
	  ac.AccName Party,
	  cm.Address1  As CAddress1,
	  cm.Address2  As CAddress2,	  
	  ct.CityName As CCity,
	  sts.StateName aS CStateName,
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
	  bm.VoucherNo InvoiceNo,
	  ac.AccName Cash_Bank,
	  bt.Remark Narration ,
	  bt.RPType,
	  Toacc.AccName as Particulars,
	  b.Amount As NetTotal,bt.ChequeNo,
	  bt.NetTotal As ChequeAmount,
	  bt.ChequeDate,
	  RBank.AccName RefBank,
	  bt.Sgst,bt.Cgst,bt.Igst,
	  bm.Remarks,
	  bm.Id,bm.VoucherId, bm.IsDeleted, vst.InvoiceHeading
 	 FROM BillMain bm
	left outer join Company cm	on cm.Id = bm.CompId
     left outer join City ct ON ct.Id = cm.CityId
	 left outer join State sts on sts.Id=cm.StateId
	 LEFT OUTER JOIN Acc ac on bm.AccId =ac.Id 
     LEFT OUTER JOIN BillTrans bt on bt.BillId=bm.Id
     LEFT OUTER JOIN Acc ToAcc on ToAcc.Id=bt.ToAccId
     LEFT OUTER JOIN Acc RBank on RBank.Id=bt.RefBankId 
     LEFT OUTER JOIN Voucher v on v.Id=bm.VoucherId	 
     LEFT OUTER JOIN dbo.VchSetup vst  ON vst.VoucherId = v.Id AND vst.CompId = bm.CompId 
	 left outer join BtoB b on b.RefTransId = bt.Id and b.RefVoucherId = bm.VoucherId	
	 left outer join BillMain bbm on bbm.Id = b.BillId  and bbm.VoucherId = b.BillVoucherId
	 left outer join (
						select br.BillId,br.BillVoucherId,Sum(br.Amount) RetAmount from BtoB br
						where br.TransType = 'Return' and br.Amount >0.00
						group by br.BillId,br.BillVoucherId 
	 )ret on ret.BillId = bbm.Id and ret.BillVoucherId = bbm.VoucherId

	 
	 left outer join (
						select br.BillId,br.BillVoucherId,Sum(br.Amount) AdvPayAmt from BtoB br
						where br.TransType = 'Payment' and br.Amount >0.00
						group by br.BillId,br.BillVoucherId 
	 )advpay on advpay.BillId = bbm.Id and advpay.BillVoucherId = bbm.VoucherId

	 WHERE bt.IsDeleted=0
	 	AND (@Bill ='N' OR  EXISTs(SELECT 1
							 FROM dbo.ReportPara RP
							 WHERE RP.ParameterValue = bt.Id
							AND RP.ReportId=@reportid
							AND RP.ParameterName='BillId'))
	--	AND bm.VoucherId =@VoucherID and bm.VoucherNo between @FrmBill and @ToBill
     --WHERE bm.IsActive=1 and bm.IsDeleted=0 AND bt.IsDeleted = 0
     --AND (bm.VoucherDate  between @FromDate and @ToDate) 
     --AND v.VTypeId=@VTypeId AND bm.CompId = @CompanyId AND bm.YearId = @YearId
     ORDER by bm.VoucherDate desc, bm.VoucherNo desc
END
GO

