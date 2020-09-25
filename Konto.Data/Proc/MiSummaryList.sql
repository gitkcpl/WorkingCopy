IF object_id('[dbo].[MiSummaryList]') IS NULL 
EXEC ('CREATE PROC [dbo].[MiSummaryList] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE dbo.MiSummaryList
	@fromDate int ,
	@ToDate int,
	@CompanyId int,
	@YearId int,
 	@VTypeId int,
	@BranchId int,
	@GrpBy varchar(50)=null
AS
BEGIN

declare @sql as varchar(max)

set @sql = 'select '
 IF @GrpBy is not null
		begin
			SET @sql = @sql + @GrpBy +','
		END
set @sql=@sql+'   sum(ct.Qty) TotalMtrs,
  sum(ct.Pcs) TotalPcs,
  sum(ct.Pcs-isnull(mr.Pcs,0)) PendPcs,
  sum(ct.Qty - isnull(mr.qty,0)) PendMtrs,
              sum(c.TotalAmount) TotalAmt
	from ChallanTrans ct
	left outer join Product pd on pd.Id=ct.ProductId
	left outer join Challan c on c.Id=ct.ChallanId
		left outer join Acc ac on ac.Id=c.AccId  
	left outer join Voucher v on v.id=c.VoucherId
     LEFT OUTER JOIN (SELECT mrt.MiscId,mrt.RefId, mrt.RefVoucherId,SUM(mrt.Pcs) Pcs,SUM(mrt.Qty)Qty FROM Challan mr 
    INNER JOIN ChallanTrans  mrt ON mr.Id = mrt.ChallanId
    WHERE mr.IsDeleted = 0 AND mrt.IsDeleted=0
    GROUP BY mrt.MiscId,mrt.RefVoucherId,mrt.RefId)mr ON c.Id = mr.MiscId AND c.VoucherId = mr.RefVoucherId AND ct.Id = mr.RefId

	where ct.IsActive=1 and ct.IsDeleted=0
	and v.VTypeId= ' +str(@VTypeId)+
	' and c.CompId=' + str(@CompanyId) 
	 + ' and c.BranchId= ' + str(@BranchId)
	+' and (c.VoucherDate between '+ str(@fromDate) + ' and '+ str(@ToDate) +' )'
 --+' and bm.YearId= '+ str(@YearId)
  IF @GrpBy is not null
		begin
			SET @sql = @sql + ' group by '+@GrpBy
		END
		   
	print @sql
		 -- PRINT @sql
	EXECUTE sp_sqlexec @sql IF @@ERROR <> 0 GOTO ErrorHandler
                -- set noCount OFF
				
    RETURN (@@ERROR) ErrorHandler: RETURN (@sql)
END
GO
