IF object_id('[dbo].[gstr1_doc]') IS NULL 
EXEC ('CREATE PROC [dbo].[gstr1_doc] AS SELECT 1 AS Id') 
GO

ALTER  PROC [dbo].[gstr1_doc]
    @fromdate INT = 20170701 ,
    @todate INT = 20170731 , 
    @companyid INT = 0
   AS
    BEGIN
  
        WITH    cr_min
                  AS ( SELECT m.VoucherId VoucherID ,
                                m.VoucherNo BillNo ,
                                ROW_NUMBER() OVER ( PARTITION BY m.VoucherID ORDER BY (m.VoucherNo) ASC ) AS rn
                       FROM     dbo.BillMain m --salesRet AS m
					   LEFT outer join voucher v on v.Id=m.VoucherId
                       WHERE    (VoucherDate BETWEEN @fromdate AND @todate)  AND CompID=@companyid
								and m.IsActive=1 and m.IsDeleted=0
								and v.VTypeId=19 --19=Sales Return
                     ),
                cr_max
                  AS ( SELECT  m.VoucherId VoucherID ,
                                m.VoucherNo BillNo ,
                                ROW_NUMBER() OVER ( PARTITION BY m.VoucherID ORDER BY (m.VoucherNo) DESC ) AS rn
                       FROM    dbo.BillMain m --salesRet AS m
					   LEFT outer join voucher v on v.Id=m.VoucherId
                       WHERE    (VoucherDate BETWEEN @fromdate AND @todate)  AND CompID=@companyid
					   and m.IsActive=1 and m.IsDeleted=0
								and v.VTypeId=19 
                     ),
                cn_min
                  AS ( SELECT   m.VoucherId VoucherID ,
                                m.VoucherNo BillNo ,
                                ROW_NUMBER() OVER ( PARTITION BY m.VoucherID ORDER BY (m.VoucherNo) ASC ) AS rn
                        FROM     dbo.BillMain m--CrDrNote AS m
					   left outer join Voucher v on v.Id=m.VoucherId
                       WHERE    (VoucherDate BETWEEN @fromdate AND @todate) AND CompID=@companyid
					   and m.IsActive=1 and m.IsDeleted=0
							and m.Extra1='SALE' and m.BillType='CREDIT NOTE' and v.VTypeId=24  -- TransType='SALES' AND CrDrType ='CREDIT NOTE'
                     ),
                cn_max
                  AS ( SELECT   m.VoucherId VoucherID ,
                                m.VoucherNo BillNo ,
                                ROW_NUMBER() OVER ( PARTITION BY m.VoucherID ORDER BY (m.VoucherNo) DESC ) AS rn
                       FROM      dbo.BillMain m--CrDrNote AS m
					   left outer join Voucher v on v.Id=m.VoucherId
                       WHERE    (VoucherDate BETWEEN @fromdate AND @todate) AND CompID=@companyid
					   and m.IsActive=1 and m.IsDeleted=0
								AND m.Extra1='SALE' and m.BillType='CREDIT NOTE' and v.VTypeId=24  -- AND TransType='SALES'  AND CrDrType ='CREDIT NOTE'
                     ),
                dn_min
                  AS ( SELECT   m.VoucherId VoucherID ,
                                m.VoucherNo BillNo ,
                                ROW_NUMBER() OVER ( PARTITION BY m.VoucherID ORDER BY (m.VoucherNo) ASC ) AS rn
                       FROM     dbo.BillMain m--CrDrNote AS m
					   left outer join Voucher v on v.Id=m.VoucherId
                       WHERE    (VoucherDate BETWEEN @fromdate AND @todate) AND CompID=@companyid
					   and m.IsActive=1 and m.IsDeleted=0
								AND m.Extra1='SALE' and m.BillType='DEBIT NOTE' and v.VTypeId=24  -- AND TransType='SALES' AND CrDrType ='DEBIT NOTE'
                     ),
                dn_max
                  AS ( SELECT  m.VoucherId VoucherID ,
                                m.VoucherNo BillNo ,
                                ROW_NUMBER() OVER ( PARTITION BY m.VoucherID ORDER BY (m.VoucherNo) DESC ) AS rn
                       FROM      dbo.BillMain m--CrDrNote AS m
					   left outer join Voucher v on v.Id=m.VoucherId
                       WHERE    (VoucherDate BETWEEN @fromdate AND @todate) AND CompID=@companyid
					   and m.IsActive=1 and m.IsDeleted=0
								AND m.Extra1='SALE' and m.BillType='DEBIT NOTE' and v.VTypeId=24  -- AND TransType='SALES'  AND CrDrType ='DEBIT NOTE'
                     ),
                min_bill
                  AS ( SELECT  m.VoucherID VoucherID ,
                                m.VoucherNo BillNo ,
                                ROW_NUMBER() OVER ( PARTITION BY m.VoucherID ORDER BY (m.VoucherNo) ASC ) AS rn
                       FROM      dbo.BillMain m
					   left outer join Voucher v on v.Id=m.VoucherId
                       WHERE    (VoucherDate BETWEEN @fromdate AND @todate) AND CompID=@companyid
					   and m.IsActive=1 and m.IsDeleted=0
						and v.VTypeId=12
                     ),
                max_bill
                  AS ( SELECT  m.VoucherID VoucherID ,
                                m.VoucherNo BillNo ,
                                ROW_NUMBER() OVER ( PARTITION BY m.VoucherID ORDER BY (m.BillNo) DESC ) AS rn
                       FROM      dbo.BillMain m
					   left outer join Voucher v on v.Id=m.VoucherId
                       WHERE    VoucherDate BETWEEN @fromdate AND @todate AND CompID=@companyid
					   and m.IsActive=1 and m.IsDeleted=0
						and v.VTypeId=12
                     )
					 ,  Can_Bill
                  AS ( SELECT  count(bm.Id)TotalCancel ,bm.VoucherId
									from BillMain bm
									left outer join Voucher v on v.Id=bm.VoucherId
									where bm.IsActive=0 and  bm.IsDeleted=0 and  VoucherDate BETWEEN @fromdate AND @todate AND CompID=@companyid
									group by bm.VoucherId
                     )
            SELECT  s.VoucherID ,
                    'Invoices for outward supply' AS DocType ,
                    MIN(s.BillNo) [From] ,
                    MAX(s.BillNo) [To] ,
                    cast(COUNT(s.VoucherNo) as varchar(50)) Total ,
                    min_bill.BillNo StratBill ,
                    mb.BillNo EndBill,isnull( cl.TotalCancel,0)TotalCancel
            FROM    dbo.BillMain s
					   left outer join Voucher v on v.Id=s.VoucherId
                    LEFT OUTER JOIN min_bill  ON min_bill.VoucherID =s.VoucherId
                    LEFT OUTER JOIN max_bill mb ON mb.VoucherID =s.VoucherId
					LEFT OUTER JOIN  Can_Bill CL on cl.VoucherId=s.VoucherId

            WHERE   (VoucherDate BETWEEN @fromdate AND @todate) AND S.CompID=@companyid
                    AND min_bill.rn = 1
                    AND mb.rn = 1 and v.VTypeId=12
					and s.IsActive=1 and s.IsDeleted=0
            GROUP BY s.VoucherID ,
                    min_bill.BillNo ,
                    mb.BillNo,(cl.TotalCancel)
					 
            UNION ALL
            SELECT  saleRet.VoucherID ,
                    'Sale Return' AS DocType ,
                    MIN(saleRet.BillNo) [From] ,
                    MAX((saleRet.BillNo)) [To] ,
                   cast( COUNT(saleRet.VoucherNo) as varchar(50)) Total ,
                    cr_min.BillNo StratBill ,
                    mb.BillNo EndBill,isnull( cl.TotalCancel,0)TotalCancel
            FROM    dbo.BillMain saleRet
			left outer join voucher v on v.Id =saleRet.VoucherId
                    LEFT OUTER JOIN cr_min ON cr_min.VoucherID = saleRet.VoucherId
                    LEFT OUTER JOIN cr_max mb ON mb.VoucherID =saleRet.VoucherId
					 LEFT OUTER JOIN  Can_Bill CL on cl.VoucherId=saleRet.VoucherId
            WHERE   VoucherDate BETWEEN @fromdate AND @todate AND saleRet.CompID=@companyid
                    AND cr_min.rn = 1
                    AND mb.rn = 1 and v.VTypeId=19		
					and saleRet.IsActive=1 and saleRet.IsDeleted=0			 
            GROUP BY saleRet.VoucherID ,
                    cr_min.BillNo ,
                    mb.BillNo,(cl.TotalCancel)
					 
            UNION ALL
            SELECT  s.VoucherID ,
                    'Credit Note' AS DocType ,
                    MIN((s.BillNo)) [From] ,
                    MAX((s.BillNo)) [To] ,
                    cast(COUNT(s.VoucherNo) as varchar(50)) Total ,
                    cn_min.BillNo StratBill ,
                    mb.BillNo EndBill,isnull( cl.TotalCancel,0)TotalCancel
            FROM    dbo.BillMain s --CrDrNote s
			left outer join Voucher v on v.Id=s.VoucherId
                    LEFT OUTER JOIN cn_min ON cn_min.VoucherID =s.VoucherId
                    LEFT OUTER JOIN cn_max mb ON mb.VoucherID = s.VoucherId
				  LEFT OUTER JOIN  Can_Bill CL on cl.VoucherId=s.VoucherId
            WHERE   VoucherDate BETWEEN @fromdate AND @todate AND S.CompID=@companyid
                    AND cn_min.rn = 1 and v.VTypeId=24
                    AND mb.rn = 1 
					and s.Extra1='SALE'  AND s.BillType='CREDIT NOTE'
					 and s.IsActive=1 and s.IsDeleted=0
            GROUP BY s.VoucherID ,
                    cn_min.BillNo ,
                    mb.BillNo,(cl.TotalCancel)
					
            UNION ALL
            SELECT  s.VoucherID ,
                    'Debit Note' AS DocType ,
                    MIN((s.BillNo)) [From] ,
                    MAX((s.BillNo)) [To] ,
                    cast(COUNT(s.VoucherNo) as varchar(50)) Total ,
                    dn_min.BillNo StratBill ,
                    mb.BillNo EndBill,isnull( cl.TotalCancel,0)TotalCancel
            FROM    dbo.BillMain s--CrDrNote s
			left outer join Voucher v on v.Id =s.VoucherId
                    LEFT OUTER JOIN dn_min ON dn_min.VoucherID =s.VoucherId
                    LEFT OUTER JOIN dn_max mb ON mb.VoucherID =s.VoucherId
					  LEFT OUTER JOIN  Can_Bill CL on cl.VoucherId=s.VoucherId
            WHERE   VoucherDate BETWEEN @fromdate AND @todate AND S.CompID=@companyid
                    AND dn_min.rn = 1 and v.VTypeId=24
                    AND mb.rn = 1 AND s.Extra1='SALE'  AND s.BillType='DEBIT NOTE'
					and s.IsActive=1 and s.IsDeleted=0
            GROUP BY s.VoucherID ,
                    dn_min.BillNo ,
                    mb.BillNo,(cl.TotalCancel);
    END;
    GO