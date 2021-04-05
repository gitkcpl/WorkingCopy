
IF object_id('[dbo].[update_stockbal_insert]') IS NOT NULL 
EXEC ('DROP TRIGGER [dbo].[update_stockbal_insert]') 
GO

CREATE  TRIGGER [dbo].[update_stockbal_insert] 
   ON  [dbo].[StockTrans]
   AFTER INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	IF UPDATE(RcptQty) OR UPDATE(IssueQty)
	BEGIN
		DECLARE 
			@bal NUMERIC(18,2) =0,
			@pid INT=0,
			@yrid INT=0,
			@compid INT=0,
			@dr NUMERIC(18,2)=0,
			@cr NUMERIC(18,2)=0,
			@opbal NUMERIC(18,2)=0,
			@syid int=0

			SELECT  @pid=ld.ItemId,@compid=ld.CompanyId,@yrid=ld.YearId FROM Inserted ld
			 
		 		SELECT @bal = @bal +  ISNULL(SUM(ld.RcptQty-ld.IssueQty),0)  FROM dbo.StockTrans ld WHERE ld.IsDeleted = 0 AND ld.YearId = @yrid AND ld.CompanyId=@compid AND ld.ItemId=@pid
			 
			UPDATE dbo.ProductBal SET BalQty = @bal + OpQty WHERE ProductId=@pid AND CompanyId =@compid AND YearId=@yrid
 

	END 
END
GO

ALTER TABLE [dbo].[StockTrans] ENABLE TRIGGER [update_stockbal_insert]
GO

