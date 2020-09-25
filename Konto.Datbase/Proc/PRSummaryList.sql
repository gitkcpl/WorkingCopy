CREATE  PROCEDURE [dbo].[PRSummaryList]
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
set @sql=@sql+'   sum(bm.TotalQty) TotalQty,
              sum(bm.TotalAmount) TotalAmt
	from BillMain bm
	left outer join BillTrans bt on bt.BillId=bm.Id  
	left outer join Acc ac on ac.Id=bm.AccId  
	left outer join Acc Toac on Toac.Id=bt.ToAccId  
	left outer join Voucher v on v.id=bm.VoucherId
	where bm.IsActive=1 and bm.IsDeleted=0
	and v.VTypeId= ' +str(@VTypeId)+
	' and bm.CompId=' + str(@CompanyId) 
	 + ' and bm.BranchId= ' + str(@BranchId)
	+' and (bm.VoucherDate between '+ str(@fromDate) + ' and '+ str(@ToDate) +' )'
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