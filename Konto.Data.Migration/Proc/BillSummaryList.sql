IF object_id('[dbo].[BillSummaryList]') IS NULL 
EXEC ('CREATE PROC [dbo].[BillSummaryList] AS SELECT 1 AS Id') 
GO 

ALTER PROCEDURE [dbo].[BillSummaryList]
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
set @sql=@sql+'  sum(bt.Pcs) FinPcs,
  sum(bt.Qty) FinMtrs,
  sum(bt.NetTotal) TotalAmt,
  sum(bt.AvgWt) GreyMtrs,
	sum(bt.Width) GreyPcs,
  sum(bt.NetTotal- bt.Cgst-bt.Sgst-bt.Igst) TaxableValue,
	sum(bt.Cgst) CgstAmt,
  sum(bt.Sgst) SgstAmt,
  sum(bt.Igst) IgstAmt
	from BillMain bm
	left outer join BillTrans bt on bt.BillId=bm.Id  
	left outer join Product pd on pd.Id=bt.ProductId
	left outer join Acc ac on ac.Id=bm.AccId  
	left outer join Acc Toac on Toac.Id=bt.ToAccId  
	left outer join Voucher v on v.id=bm.VoucherId
	where bm.IsActive=1 and bm.IsDeleted=0
	and v.VTypeId= ' +str(@VTypeId)+
	' and bm.CompId=' + str(@CompanyId) 
	 + ' and bm.BranchId= ' + str(@BranchId)
	+' and (bm.VoucherDate between '+ str(@fromDate) + ' and '+ str(@ToDate) +' )'
 
 
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
