﻿CREATE  TRIGGER [dbo].[update_acbal_update]
	ON [dbo].[LedgerTrans]
	AFTER UPDATE
	AS
	BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	IF UPDATE(Debit) OR UPDATE(Credit)
	BEGIN
		DECLARE 
			@bal NUMERIC(18,2) =0,
			@acid INT=0,
			@yrid INT=0,
			@compid INT=0,
			@dr NUMERIC(18,2)=0,
			@cr NUMERIC(18,2)=0,
			@opbal NUMERIC(18,2)=0,
			@syid int=0

			-- Old  vale
			SELECT  @acid=ld.AccountId,@compid=ld.CompanyId,@yrid=ld.YearId FROM Deleted ld
			 
			--- balance for this current year
			SELECT @bal = @bal +  ISNULL(SUM(debit-ld.Credit),0)  FROM dbo.LedgerTrans ld WHERE ld.IsDeleted = 0 AND  ld.YearId = @yrid AND ld.CompanyId=@compid AND ld.AccountId=@acid
			 
			UPDATE dbo.AccBal SET Bal = @bal +OpBal WHERE AccId=@acid AND @compid =@compid AND YearId=@yrid
			 
			SELECT  @acid=ld.AccountId,@compid=ld.CompanyId,@yrid=ld.YearId FROM Inserted ld
			 
			SELECT @bal = @bal +  ISNULL(SUM(debit-ld.Credit),0)  FROM dbo.LedgerTrans ld WHERE ld.IsDeleted = 0 AND ld.YearId = @yrid AND ld.CompanyId=@compid AND ld.AccountId=@acid
			

			UPDATE dbo.AccBal SET Bal = @bal + OpBal WHERE AccId=@acid AND @compid =@compid AND YearId=@yrid
	END
	 
	END
GO

ALTER TABLE [dbo].[LedgerTrans] ENABLE TRIGGER [update_acbal_update]
GO

