
SET IDENTITY_INSERT dbo.AcGroup on

IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=1)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(1,'00','CAPITAL ACCOUNT',0,'CAPITAL ACCOUNT','LIABILITIES',
0,0,0,0,1,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=2)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(2,'01','LOAN (LIABILITY)',0,'LOAN (LIABILITY)','LIABILITIES',
0,0,0,0,1,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=3)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(3,'02','CURRENT LIABLITIES',0,'CURRENT LIABILITIES','LIABILITIES',
0,0,0,0,1,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())


IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=4)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(4,'03','FIXED ASSETS',0,'FIXED ASSETS','ASSETS',
0,0,0,0,1,0,0,0,
0,0,0,0,0,0,0,
1,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())


IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=5)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(5,'04','INVESTMENTS',0,'INVESTMENTS','ASSETS',
0,0,0,0,1,0,0,0,
0,0,0,0,0,0,1,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=6)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(6,'05','CURRENT ASSETS',0,'CURRENT ASSETS','ASSETS',
0,0,0,0,1,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())


IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=7)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(7,'06','MISC. EXPENSES (ASSET)',0,'MISC. EXPENSES (ASSET)','ASSETS',
0,0,0,0,1,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=8)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(8,'07','SUSPENSE A/C',0,'SUSPENSE A/C','LIABILITIES',
0,0,0,0,1,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())


IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=9)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(9,'08','SALES ACCOUNTS',0,'SALES ACCOUNTS','TRADING INCOME',
0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())


IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=10)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(10,'09','PURCHASE ACCOUNTS',0,'PURCHASE ACCOUNTS','TRADING EXPENSE',
0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())


IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=11)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(11,'10','DIRECT INCOMES',0,'DIRECT INCOMES','INCOME',
0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())


IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=12)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(12,'11','DIRECT EXPENSES',0,'DIRECT EXPENSES','EXPENSE',
0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())


IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=13)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(13,'12','INDIRECT EXPENSES',0,'INDIRECT EXPENSES','EXPENSE',
0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())


IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=14)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(14,'13','INDIRECT INCOMES',0,'INDIRECT INCOMES','INCOME',
0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())


IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=15)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(15,'14','RESERVES & SURPLUS',0,'RESERVES & SURPLUS','LIABILITIES',
0,0,0,0,1,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())


IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=16)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(16,'15','BANK OD A/C',0,'BANK OD A/C','LIABILITIES',
0,0,0,0,1,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())


IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=17)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(17,'16','SECURED LOAN',0,'SECURED LOAN','LIABILITIES',
0,0,0,0,1,0,0,0,
0,0,0,0,0,0,1,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())


IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=18)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(18,'17','UNSECURED LOAN',0,'UNSECURED LOAN','LIABILITIES',
0,0,0,0,1,0,0,0,
0,0,0,0,0,0,1,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())


IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=19)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(19,'18','DUTIES & TAXES',0,'DUTIES & TAXES','LIABILITIES',
0,0,0,0,1,0,0,0,
0,0,0,0,0,0,0,
0,0,0,1,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())


IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=20)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(20,'19','PROVISIONS',0,'PROVISIONS','LIABILITIES',
0,0,0,0,1,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())


IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=21)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(21,'20','SUNDRY CREDITORS',0,'SUNDRY CREDITORS','LIABILITIES',
0,0,0,0,1,0,0,1,
1,0,1,1,0,0,0,
0,0,1,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())


IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=22)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Extra1
)
values(22,'21','STOCK-IN-HAND',0,'STOCK-IN-HAND','ASSETS',
0,0,0,0,1,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID(),'P')

IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=23)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(23,'22','DEPOSITS (ASSET)',0,'DEPOSITS (ASSET)','ASSETS',
0,0,0,0,1,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())


IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=24)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(24,'23','LOANS & ADVANCES (ASSET)',0,'LOANS & ADVANCES (ASSET)','ASSETS',
0,0,0,0,1,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())


IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=25)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId,Extra1
)
values(25,'24','SUNDRY DEBTORS',0,'SUNDRY DEBTORS','ASSETS',
0,0,0,0,1,0,0,1,
1,0,0,1,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID(),'R')


IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=26)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(26,'25','CASH ACCOUNTS',0,'CASH ACCOUNTS','ASSETS',
0,0,0,0,1,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=27)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(27,'26','BANK ACCOUNTS',0,'BANK ACCOUNTS','ASSETS',
0,0,0,0,1,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=28)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(28,'27','PRE-OPERATIVE EXPENSES',0,'PRE-OPERATIVE EXPENSES','EXPENSE',
0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())


IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=29)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(29,'28','TRADING EXPENSE',0,'TRADING EXPENSE','TRADING EXPENSE',
0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())


IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=30)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(30,'29','TRADING INCOME',0,'TRADING INCOME','TRADING INCOME',
0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())


IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=31)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(31,'30','CREDITORS FOR BROKERAGE',0,'CREDITORS FOR BROKERAGE','LIABILITIES',
0,0,0,0,1,0,0,0,
1,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=32)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(32,'31','CREDITORS FOR TRANPORTATION',0,'CREDITORS FOR TRANPORTATION','LIABILITIES',
0,0,0,0,1,0,0,0,
1,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())


IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=33)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(33,'32','EXPS.PL',0,'EXPS.PL','EXPENSE',
0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=34)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(34,'33','WAGES',0,'WAGES','EXPENSE',
0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=35)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(35,'34','ADMINSTRATIVE SELING & OTHER EXPENSE',0,'ADMINSTRATIVE SELING & OTHER EXPENSE','EXPENSE',
0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())


IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=36)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(36,'35','LIABLITIES (BL)',0,'LIABLITIES (BL)','LIABILITIES',
0,0,0,0,1,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())

IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=37)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(37,'36','PROVISION FOR TAXTAION',0,'PROVISION FOR TAXTAION','LIABILITIES',
0,0,0,0,1,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())


IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=38)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(38,'37','FACTORY LAND & BUILDING',0,'FACTORY LAND & BUILDING','ASSETS',
0,0,0,0,1,0,0,0,
0,0,0,0,0,0,0,
0,0,0,0,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())


IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=39)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(39,'38','T.D.S',0,'T.D.S','LIABILITIES',
0,0,0,0,1,0,0,0,
0,0,0,0,0,0,0,
0,0,0,1,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())


IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=40)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(40,'39','OPENING STOCK',0,'OPENING STOCK','TRADING EXPENSE',
0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,
0,0,0,1,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())



IF NOT EXISTS(SELECT 1 FROM dbo.AcGroup WHERE Id=41)
INSERT INTO dbo.AcGroup
(	Id,GroupCode,GroupName,UnderGroupId,OppSideName,Nature,
    BlSort,TbSort,TrSort,OnlyTotal,OpBalanceReq,AgentReq,TransportReq,AddressReq,
	TaxationReq,SalesmanReq,BankDetailReq,PartyGroupReq,PriceLevelReq,CollDaysReq,IntAccountReq,
    DeprAccountReq,TcsReq,TdsReq,TaxTypeReq,CrLimitReq,GradeReq,
    IsActive,IsDeleted,CreateDate,CreateUser,IpAddress,RowId
)
values(41,'40','CLOSING STOCK (TRADING)',0,'CLOSING STOCK (TRADING)','TRADING INCOME',
0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,
0,0,0,1,0,0,
1,0,GETDATE(),'Admin','NA',NEWID())


SET IDENTITY_INSERT dbo.AcGroup off