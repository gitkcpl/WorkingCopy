SET IDENTITY_INSERT dbo.TransType ON
 
IF NOT EXISTS (SELECT 1 FROM dbo.TransType WHERE Id=1)
BEGIN
	INSERT INTO dbo.TransType(Id,TypeName,Category,Extra1,Extra2,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(1,'Purchase','Inward','','',GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.TransType WHERE Id=2)
BEGIN
	INSERT INTO dbo.TransType(Id,TypeName,Category,Extra1,Extra2,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(2,'Inward for Job','Inward','','',GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.TransType WHERE Id=3)
BEGIN
	INSERT INTO dbo.TransType(Id,TypeName,Category,Extra1,Extra2,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(3,'Sales Return','Inward','','',GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.TransType WHERE Id=4)
BEGIN
	INSERT INTO dbo.TransType(Id,TypeName,Category,Extra1,Extra2,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(4,'Transfer In','Inward','','',GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.TransType WHERE Id=5)
BEGIN
	INSERT INTO dbo.TransType(Id,TypeName,Category,Extra1,Extra2,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(5,'Others',NULL,'','',GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.TransType WHERE Id=6)
BEGIN
	INSERT INTO dbo.TransType(Id,TypeName,Category,Extra1,Extra2,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(6,'Sales Challan','OutWard','','',GETDATE(),'Admin',1,0,NEWID())
END


IF NOT EXISTS (SELECT 1 FROM dbo.TransType WHERE Id=7)
BEGIN
	INSERT INTO dbo.TransType(Id,TypeName,Category,Extra1,Extra2,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(7,'Mill Issue','MILLISSUE','','',GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.TransType WHERE Id=8)
BEGIN
	INSERT INTO dbo.TransType(Id,TypeName,Category,Extra1,Extra2,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(8,'Issue For Job','JOBISSUE','','',GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.TransType WHERE Id=9)
BEGIN
	INSERT INTO dbo.TransType(Id,TypeName,Category,Extra1,Extra2,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(9,'Sales Job','OutWard','','',GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.TransType WHERE Id=10)
BEGIN
	INSERT INTO dbo.TransType(Id,TypeName,Category,Extra1,Extra2,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(10,'Transfer Out','OutWard','','',GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.TransType WHERE Id=11)
BEGIN
	INSERT INTO dbo.TransType(Id,TypeName,Category,Extra1,Extra2,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(11,'Refinish Issue','MILLISSUE','','',GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.TransType WHERE Id=12)
BEGIN
	INSERT INTO dbo.TransType(Id,TypeName,Category,Extra1,Extra2,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(12,'Purchase Return','OutWard','','',GETDATE(),'Admin',1,0,NEWID())
END
 
IF NOT EXISTS (SELECT 1 FROM dbo.TransType WHERE Id=13)
BEGIN
	INSERT INTO dbo.TransType(Id,TypeName,Category,Extra1,Extra2,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(13,'INWARD FROM JOB','INWARD','','',GETDATE(),'Admin',1,0,NEWID())
END

IF NOT EXISTS (SELECT 1 FROM dbo.TransType WHERE Id=14)
BEGIN
	INSERT INTO dbo.TransType(Id,TypeName,Category,Extra1,Extra2,CreateDate,CreateUser,IsActive,IsDeleted,RowId)
	VALUES(14,'All','Report','','',GETDATE(),'Admin',1,0,NEWID())
END

SET IDENTITY_INSERT dbo.TransType OFF