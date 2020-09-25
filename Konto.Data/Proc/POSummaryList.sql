IF object_id('[dbo].[POSummaryList]') IS NULL 
EXEC ('CREATE PROC [dbo].[POSummaryList] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[POSummaryList]
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
set @sql=@sql+'   sum(ot.Qty) TotalQty,
              sum(o.TotalAmount) TotalAmt
	from OrdTrans ot
	left outer join Ord o on o.Id=ot.OrdId
	left outer join Product pd on pd.Id=ot.ProductId
	left outer join Acc ac on ac.Id=o.AccId 
	left outer join Voucher v on v.id=o.VoucherId
	where o.IsActive=1 and o.IsDeleted=0
	and v.VTypeId= ' +str(@VTypeId)+
	' and o.CompId=' + str(@CompanyId) 
	 + ' and o.BranchId= ' + str(@BranchId)
	+' and (o.VoucherDate between '+ str(@fromDate) + ' and '+ str(@ToDate) +' )'
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

