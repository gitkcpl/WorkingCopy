CREATE PROCEDURE [dbo].[PoSummaryList]
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
set @sql=@sql+'   sum(ct.Qty) TotalQty,
              sum(c.TotalAmount) TotalAmt
	from OrdTrans ct
	left outer join Product pd on pd.Id=ct.ProductId
	left outer join Ord c on c.Id=ct.OrdId
		left outer join Acc ac on ac.Id=c.AccId  
	left outer join Voucher v on v.id=c.VoucherId
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

