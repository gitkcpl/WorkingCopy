

update dbo.ErpModule set [PackageId]=0


if NOT exists (select 1 from ErpModule em where em.Id=100) --Masters
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate,ImageIndex,
		   IsActive,IsDeleted,RowId
		   )
		   values(100,0,'Masters',1,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,'/Konto.Wpf;component/MenuIcon/master.png',0,
		   0,0,0,null,'1/2/3/4/5/6/7/8/9/10/11',1,GETDATE(),8,1,0,NEWID()) 
end

update dbo.ErpModule set Extra2=',1,2,3,4,5,6,7,8,9,10,11,' where id=100

go
if NOT exists (select 1 from ErpModule em where em.Id=1)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate,
		   IsActive,IsDeleted,RowId)
		   values(1,100,'-',2,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,1,null,null,1,GETDATE(),1,0,NEWID())
end
go

if NOT exists (select 1 from ErpModule em where em.Id=2)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate,
		   IsActive,IsDeleted,RowId)
		   values(2,150,'-',2,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,1,null,null,1,GETDATE(),1,0,NEWID())
end
go

if NOT exists (select 1 from ErpModule em where em.Id=101) -- Location
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate,ImageIndex,
		   IsActive,IsDeleted,RowId)
		   values(101,100,'Location',1,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,'/Konto.Wpf;component/MenuIcon/group.png',0,
		   0,0,0,null,'1/2/3/4/5/6/7/8/9/10/11',1,GETDATE(),6,1,0,NEWID())
end
go		  
update dbo.ErpModule set Extra2=',1,2,3,4,5,6,7,8,9,10,11,' where id=101

if NOT exists (select 1 from ErpModule em where em.Id=102)-- country
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,
		   IsActive,IsDeleted,RowId)
		   values(102,101,'Country',1,null,null,0,
		   null,null,'country','Konto.Shared.Masters.Country.CountryView','Konto.Shared',
		   null,null,'Country',1,'/Konto.Wpf;component/MenuIcon/location.png',0,
		   0,0,0,null,'1/2/3/4/5/6/7/8/9/10/11',1,GETDATE(),GETDATE(),7,1,0,NEWID())
end
go

update dbo.ErpModule set Extra2=',1,2,3,4,5,6,7,8,9,10,11,' where id=102

if NOT exists (select 1 from ErpModule em where em.Id=103) -- states
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,
		   IsActive,IsDeleted,RowId)
		   values(103,101,'State',2,null,null,0,
		   null,null,'State','Konto.Shared.Masters.State.StateIndex','Konto.Shared',
		   null,null,'State',1,'/Konto.Wpf;component/MenuIcon/location.png',0,
		   0,0,0,null,'1/2/3/4/5/6/7/8/9/10/11',1,GETDATE(),GETDATE(),7,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,5,6,7,8,9,10,11,' where id=103


if NOT exists (select 1 from ErpModule em where em.Id=104) -- City
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,
		   IsActive,IsDeleted,RowId)
		   values(104,101,'City',3,null,null,0,
		   null,null,'City','Konto.Shared.Masters.City.CityIndex','Konto.Shared',
		   null,null,'City',1,'/Konto.Wpf;component/MenuIcon/location.png',0,
		   0,0,0,null,'1/2/3/4/5/6/7/8/9/10/11',1,GETDATE(),GETDATE(),7,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,5,6,7,8,9,10,11,' where id=104			

if NOT exists (select 1 from ErpModule em where em.Id=105) -- Area
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,
		   IsActive,IsDeleted,RowId)
		   values(105,101,'Area',4,null,null,0,
		   null,null,'Area','Konto.Shared.Masters.Area.AreaIndex','Konto.Shared',
		   null,null,'Area',1,'/Konto.Wpf;component/MenuIcon/location.png',0,
		   0,0,0,null,'1/2/3/4/5/6/7/9/10/11',1,GETDATE(),GETDATE(),7,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,5,6,7,8,9,10,11,' where id=105		


if NOT exists (select 1 from ErpModule em where em.Id=106)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate,ImageIndex,
		   IsActive,IsDeleted,RowId)
		   values(106,100,'Product Section',2,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,'/Konto.Wpf;component/MenuIcon/group.png',0,
		   0,0,0,null,null,1,GETDATE(),6,1,0,NEWID())
end

update dbo.ErpModule set Extra2=',1,2,3,4,5,6,7,8,9,10,11,' where id=106


if NOT exists (select 1 from ErpModule em where em.Id=149)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],IsActive,IsDeleted,RowId)
		   values(149,106,'-',11,null,null,0,
		   null,null,'-',null,null,
		   null,null,'-',1,null,0,
		   0,0,1,null,null,1,GETDATE(),GETDATE(),1,0,NEWID())
end
--update dbo.ErpModule set Extra2=',1,2,3,4,5,6,7,8,9,10,11,' where id=149


if NOT exists (select 1 from ErpModule em where em.Id=107)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(107,106,'Product Type',1,null,null,0,
		   null,null,'ProductType','Konto.Shared.Masters.ProductType.PTypeIndex','Konto.Shared',
		   null,null,'Product Type',1,'/Konto.Wpf;component/MenuIcon/product.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),10,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,5,6,7,8,9,10,11,' where id=107


if NOT exists (select 1 from ErpModule em where em.Id=108)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(108,106,'Brand',3,null,null,0,
		   null,null,'Brand','Konto.Shared.Masters.Brand.BrandIndex','Konto.Shared',
		   null,null,'Brand',1,'/Konto.Wpf;component/MenuIcon/product.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),10,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,5,6,7,8,9,10,11,' where id=108
		

if NOT exists (select 1 from ErpModule em where em.Id=109)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(109,106,'Unit',2,null,null,0,
		   null,null,'Unit','Konto.Shared.Masters.Uom.UomIndex','Konto.Shared',
		   null,null,'Unit',1,'/Konto.Wpf;component/MenuIcon/product.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),10,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,5,6,7,8,9,10,11,' where id=109

if NOT exists (select 1 from ErpModule em where em.Id=110)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(110,106,'Group',4,null,null,0,
		   null,null,'Group','Konto.Shared.Masters.ProductGroup.GroupIndex','Konto.Shared',
		   null,null,'Group',1,'/Konto.Wpf;component/MenuIcon/product.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),10,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,5,6,7,8,9,10,11,' where id=110

if NOT exists (select 1 from ErpModule em where em.Id=111)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(111,106,'Sub Group',5,null,null,0,
		   null,null,'Sub Group','Konto.Shared.Masters.SubGroup.SubGroupIndex','Konto.Shared',
		   null,null,'Sub Group',1,'/Konto.Wpf;component/MenuIcon/product.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),10,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,5,6,7,8,9,10,11,' where id=111


if NOT exists (select 1 from ErpModule em where em.Id=112)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(112,106,'Size Master',6,null,null,0,
		   null,null,'Size','Konto.Shared.Masters.Size.SizeIndex','Konto.Shared',
		   null,null,'Size',1,'/Konto.Wpf;component/MenuIcon/product.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),10,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,5,6,7,8,9,10,11,' where id=112		  

if NOT exists (select 1 from ErpModule em where em.Id=113)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(113,106,'Color Master',7,null,null,0,
		   null,null,'Color','Konto.Shared.Masters.Color.ColorIndex','Konto.Shared',
		   null,null,'Color',1,'/Konto.Wpf;component/MenuIcon/product.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),10,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,5,6,7,8,9,10,11,' where id=113		   

if NOT exists (select 1 from ErpModule em where em.Id=114)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(114,106,'Category Master',8,null,null,0,
		   null,null,'Category','Konto.Shared.Masters.Category.CategoryIndex','Konto.Shared',
		   null,null,'Category',1,'/Konto.Wpf;component/MenuIcon/product.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),10,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,5,6,7,8,9,10,11,' where id=114		   

if NOT exists (select 1 from ErpModule em where em.Id=115)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(115,106,'Style Master',9,null,null,0,
		   null,null,'Style','Konto.Shared.Masters.Style.StyleIndex','Konto.Shared',
		   null,null,'Style',1,'/Konto.Wpf;component/MenuIcon/product.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),10,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,5,6,7,8,9,10,11,' where id=115		  

if NOT exists (select 1 from ErpModule em where em.Id=116)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],IsActive,IsDeleted,RowId)
		   values(116,106,'-',10,null,null,0,
		   null,null,'-',null,null,
		   null,null,'-',1,null,0,
		   0,0,1,null,null,1,GETDATE(),GETDATE(),1,0,NEWID())
end

IF NOT exists (select 1 from ErpModule em where em.Id=117)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(117,106,'GST Slab',10,null,null,0,
		   null,null,'GST SLAB','Konto.Shared.Masters.Tax.TaxIndex','Konto.Shared',
		   null,null,'GST SLAB',1,'/Konto.Wpf;component/MenuIcon/product.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),10,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,5,6,7,9,10,11,' where id=117		   

IF NOT exists (select 1 from ErpModule em where em.Id=118)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(118,106,'Product Master',13,null,null,0,
		   null,null,'Product','Konto.Shared.Masters.Item.ProductIndex','Konto.Shared',
		   null,null,'Product',1,'/Konto.Wpf;component/MenuIcon/product.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),10,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,5,6,7,9,10,11,' where id=118		   


IF NOT exists (select 1 from ErpModule em where em.Id=119)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(119,106,'Barcode Printing',14,null,null,0,
		   null,null,'Product','Konto.Shared.Masters.Item.BarcodeView','Konto.Shared',
		   null,null,'Product',1,'/Konto.Wpf;component/MenuIcon/product.png',0,
		   0,0,1,null,',1,2,3,4,5,6,7,9,10,11,',1,GETDATE(),GETDATE(),10,1,0,NEWID())
end




if NOT exists (select 1 from ErpModule em where em.Id=120)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(120,140,'Salesman',3,null,null,0,
		   null,null,'Salesman','Konto.Shared.Masters.Emp.EmpIndex','Konto.Shared',
		   null,null,'Salesman',1,'/Konto.Wpf;component/MenuIcon/account.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),0,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,5,6,7,9,10,11,' where id=120		   


-- Ledger Group seprator
if NOT exists (select 1 from ErpModule em where em.Id=121)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],IsActive,IsDeleted,RowId)
		   values(121,100,'-',4,null,null,0,
		   null,null,'-',Null,NUll,
		   null,null,'-',1,null,0,
		   0,0,1,null,null,1,GETDATE(),GETDATE(),1,0,NEWID())
end

-- Ledger Group
if NOT exists (select 1 from ErpModule em where em.Id=122)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(122,140,'Ledger Group',1,null,null,0,
		   null,null,'Ledger Group','Konto.Shared.Masters.LedgerGroup.AcGroupIndex','Konto.Shared',
		   null,null,'Ledger Group',1,'/Konto.Wpf;component/MenuIcon/account.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),0,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,5,6,7,9,10,11,' where id=122		  

IF NOT exists (select 1 from ErpModule em where em.Id=123)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(123,140,'Party Group',2,null,null,0,
		   null,null,'Party Group','Konto.Shared.Masters.PG.PgIndex','Konto.Shared',
		   null,null,'Party Group',1,'/Konto.Wpf;component/MenuIcon/account.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),0,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,5,6,7,9,10,11,' where id=123


IF NOT exists (select 1 from ErpModule em where em.Id=124)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(124,140,'Account Master',4,null,null,0,
		   null,null,'Account Master','Konto.Shared.Masters.Acc.AccIndex','Konto.Shared',
		   null,null,'Account Master',7,'/Konto.Wpf;component/MenuIcon/account.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),0,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,5,6,7,9,10,11,' where id=124


---seprator
if NOT exists (select 1 from ErpModule em where em.Id=125)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],IsActive,IsDeleted,RowId)
		   values(125,100,'-',8,null,null,0,
		   null,null,'-',Null,NUll,
		   null,null,'-',1,null,0,
		   0,0,1,null,null,1,GETDATE(),GETDATE(),1,0,NEWID())
end

IF NOT exists (select 1 from ErpModule em where em.Id=126)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(126,100,'Voucher Type',5,null,null,0,
		   null,null,'Voucher Type','Konto.Shared.Masters.VType.VTypeIndex','Konto.Shared',
		   null,null,'Voucher Type',9,'/Konto.Wpf;component/MenuIcon/voucher.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),21,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,5,6,7,9,10,11,' where id=126		   

IF NOT exists (select 1 from ErpModule em where em.Id=127)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(127,100,'Voucher',6,null,null,0,
		   null,null,'Voucher','Konto.Shared.Masters.Voucher.VoucherIndex','Konto.Shared',
		   null,null,'Voucher',10,'/Konto.Wpf;component/MenuIcon/voucher.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),21,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,5,6,7,9,10,11,' where id=127

--seprator
if NOT exists (select 1 from ErpModule em where em.Id=128)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],IsActive,IsDeleted,RowId)
		   values(128,100,'-',11,null,null,0,
		   null,null,'-',Null,NUll,
		   null,null,'-',1,null,0,
		   0,0,1,null,null,1,GETDATE(),GETDATE(),1,0,NEWID())
end

IF NOT exists (select 1 from ErpModule em where em.Id=129)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(129,100,'Narration',7,null,null,0,
		   null,null,'Narration','Konto.Shared.Masters.Nar.NarIndex','Konto.Shared',
		   null,null,'Narration',1,'/Konto.Wpf;component/MenuIcon/voucher.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),21,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,5,6,7,9,10,11,' where id=129		   

IF NOT exists (select 1 from ErpModule em where em.Id=130)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(130,101,'Store',7,null,null,0,
		   null,null,'Store','Konto.Shared.Masters.Store.StoreIndex','Konto.Shared',
		   null,null,'Store',1,'/Konto.Wpf;component/MenuIcon/location.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),7,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=130		   

if NOT exists (select 1 from ErpModule em where em.Id=131)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(131,140,'Common Master',5,null,null,0,
		   null,null,'Common Master','Konto.Shared.Masters.Haste.HasteIndex','Konto.Shared',
		   null,null,'Common Master',0,'/Konto.Wpf;component/MenuIcon/account.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),0,1,0,NEWID())
end
update dbo.ErpModule set ModuleDesc = 'Common Master', Title = 'Common Master', Extra2=',1,2,3,4,5,6,7,9,10,11,' where id=131


if NOT exists (select 1 from ErpModule em where em.Id=132)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(132,106,'Design Master',14,null,null,0,
		   null,null,'Design Master','Konto.Shared.Masters.Design.DesignIndex','Konto.Shared',
		   null,null,'Design',1,'/Konto.Wpf;component/MenuIcon/design.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),4,1,0,NEWID())
end
go
update dbo.ErpModule set Extra2=',1,3,7,11,' where id=132

if NOT exists (select 1 from ErpModule em where em.Id=133)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(133,106,'Catalog',12,null,null,0,
		   null,null,'catalog','Konto.Shared.Masters.Catalog.CatalogIndex','Konto.Shared',
		   null,null,'Catalog',1,'/Konto.Wpf;component/MenuIcon/design.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),4,1,0,NEWID())
end
go

update dbo.ErpModule set Extra2=',1,3,7,11,' where id=133


if NOT exists (select 1 from ErpModule em where em.Id=134)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(134,100,'Machine Master',1,null,null,0,
		   null,null,'Machine Master','Konto.Shared.Masters.MachineMaster.MachineIndex','Konto.Shared',
		   null,null,'Machine',1,'/Konto.Wpf;component/MenuIcon/design.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),4,1,0,NEWID())
end
go
update dbo.ErpModule set Extra2=',3,4,7,11,' where id=134


if NOT exists (select 1 from ErpModule em where em.Id=135)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(135,100,'Grade Master',1,null,null,0,
		   null,null,'Grade Master','Konto.Shared.Masters.Grade.GradeIndex','Konto.Shared',
		   null,null,'Grade',1,'/Konto.Wpf;component/MenuIcon/voucher.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),21,1,0,NEWID())
end
go
update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=135	   

if NOT exists (select 1 from ErpModule em where em.Id=1071)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(1071,100,'SubGrade Master',2,null,null,0,
		   null,null,'SubGrade Master','Konto.Shared.Masters.SubGrade.SubGradeIndex','Konto.Shared',
		   null,null,'SubGrade',1,'/Konto.Wpf;component/MenuIcon/voucher.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),21,1,0,NEWID())
end
go
update dbo.ErpModule set Extra2=',4,11,' where id=1071

if NOT exists (select 1 from ErpModule em where em.Id=136)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(136,100,'Packing Type Master',1,null,null,0,
		   null,null,'PackingType Master','Konto.Shared.Masters.PackingType.PackingTypeIndex','Konto.Shared',
		   null,null,'PackingType',1,'/Konto.Wpf;component/MenuIcon/voucher.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),21,1,0,NEWID())
end
go 
update dbo.ErpModule set Extra2=',3,4,11,' where id=136	

if NOT exists (select 1 from ErpModule em where em.Id=138)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(138,900,'Company',1,null,null,0,
		   null,null,'Company','Konto.Shared.Masters.Comp.CompIndex','Konto.Shared',
		   null,null,'Company',1,'/Konto.Wpf;component/MenuIcon/company.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),3,1,0,NEWID())
end
go
update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=138

if NOT exists (select 1 from ErpModule em where em.Id=139)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(139,141,'Sale/Purchase Op Bill',1,null,null,0,
		   null,null,'Sale/Purchase Op Bill','Konto.Shared.OpBill.SPOpBillIndex','Konto.Shared',
		   null,null,'Sale/Purchase Op Bill',1,'/Konto.Wpf;component/MenuIcon/opening.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),9,1,0,NEWID())
end
go
update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=139 	   


if NOT exists (select 1 from ErpModule em where em.Id=300)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate,
		   ImageIndex,IsActive,IsDeleted,RowId)
		   values(300,0,'Transaction',2,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,'/Konto.Wpf;component/MenuIcon/transaction.png',0,
		   0,0,0,null,null,1,GETDATE(),19,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=300


if NOT exists (select 1 from ErpModule em where em.Id=301)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate,
		   ImageIndex,IsActive,IsDeleted,RowId)
		   values(301,300,'Purchase',0,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,'/Konto.Wpf;component/MenuIcon/group.png',0,
		   0,0,0,null,null,1,GETDATE(),12,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=301


if NOT exists (select 1 from ErpModule em where em.Id=302)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate,
		   ImageIndex,IsActive,IsDeleted,RowId)
		   values(302,300,'Account',5,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,'/Konto.Wpf;component/MenuIcon/group.png',0,
		   0,0,0,null,null,1,GETDATE(),6,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=302


if NOT exists (select 1 from ErpModule em where em.Id=303)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate,
		   ImageIndex,IsActive,IsDeleted,RowId)
		   values(303,300,'Mill',2,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,'/Konto.Wpf;component/MenuIcon/group.png',0,
		   0,0,0,null,null,1,GETDATE(),6,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,3,7,' where id=303


if NOT exists (select 1 from ErpModule em where em.Id=304)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate,
		   ImageIndex,IsActive,IsDeleted,RowId)
		   values(304,300,'Job',3,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,'/Konto.Wpf;component/MenuIcon/group.png',0,
		   0,0,0,null,null,1,GETDATE(),6,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,3,6,4,7,11,' where id=304

IF NOT exists (select 1 from ErpModule em where em.Id=306)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(306,301,'Purchase Order',1,null,null,0,
		   null,null,'Purchase Order','Konto.Shared.Trans.Po.PoIndex','Konto.Shared',
		   null,null,'Purchase Order',1,'/Konto.Wpf;component/MenuIcon/purchase.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),12,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=306 	   


IF NOT exists (select 1 from ErpModule em where em.Id=307)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(307,301,'PO Approval',5,null,null,0,
		   null,null,'PO Approval','Konto.Shared.Trans.Po.PoApproveIndex','Konto.Shared',
		   null,null,'PO Approval',1,'/Konto.Wpf;component/MenuIcon/purchase.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),12,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=307 	   

IF NOT exists (select 1 from ErpModule em where em.Id=308)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(308,301,'Inward (GRN)',4,null,null,0,
		   null,null,'Challan','Konto.Shared.Trans.GRN.GRNIndex','Konto.Shared',
		   null,null,'Inward (GRN)',1,'/Konto.Wpf;component/MenuIcon/purchase.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),12,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=308 	   

IF NOT exists (select 1 from ErpModule em where em.Id=309)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(309,301,'Purchase Bill',8,null,null,0,
		   null,null,'BillMain','Konto.Shared.Trans.PInvoice.PInvoiceIndex','Konto.Shared',
		   null,null,'Purchase Bill',1,'/Konto.Wpf;component/MenuIcon/purchase.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),12,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=309


IF NOT exists (select 1 from ErpModule em where em.Id=310)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(310,303,'Mill Issue',1,null,null,0,
		   null,null,'Mill Issue','Konto.Trading.MillIssue.MillIssueIndex','Konto.Trading',
		   null,null,'Mill Issue',1,'/Konto.Wpf;component/MenuIcon/gray.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),5,1,0,NEWID())
end
update dbo.ErpModule set AssemblyName ='Konto.Trading.MillIssue.MillIssueIndex', MainAssembly = 'Konto.Trading', IconPath='/Konto.Wpf;component/MenuIcon/gray.png' where id=310

update dbo.ErpModule set Extra2=',1,3,7,11,' where id=310


IF NOT exists (select 1 from ErpModule em where em.Id=311)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(311,303,'Mill Issue Lot No',8,null,null,0,
		   null,null,'Mill Issue Lot No','Konto.Shared.Trans.SalesChallan.MILotIndex','Konto.Shared',
		   null,null,'Mill Issue Lot No',1,'/Konto.Wpf;component/MenuIcon/gray.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),5,1,0,NEWID())
end
update dbo.ErpModule set Extra2='' where id=311


IF NOT exists (select 1 from ErpModule em where em.Id=312)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(312,301,'Gray Purchase',6,null,null,0,
		   null,null,'Gray Purchase','Konto.Trading.GP.GPIndex','Konto.Trading',
		   null,null,'Gray Purchase',1,'/Konto.Wpf;component/MenuIcon/purchase.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),12,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,3,7,11,' where id=312


IF NOT exists (select 1 from ErpModule em where em.Id=314)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(314,303,'Mill Programm',2,null,null,0,
		   null,null,'Mill Programm',NULL,NULL,
		   null,null,'Mill Programm',0,'/Konto.Wpf;component/MenuIcon/gray.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),5,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,3,7,11,' where id=314


IF NOT exists (select 1 from ErpModule em where em.Id=317)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(317,303,'Taka Folding',6,null,null,0,
		   null,null,'Cutting','Konto.Trading.Cutting.CuttingIndex','Konto.Trading',
		   null,null,'Taka Folding',1,'/Konto.Wpf;component/MenuIcon/gray.png',0, 
		   0,0,0,null,null,1,GETDATE(),GETDATE(),5,1,0,NEWID())
end
 	   update dbo.ErpModule set Extra2=',1,3,7,11,', ModuleDesc = 'Taka Folding', Title = 'Taka Folding', IconPath='/Konto.Wpf;component/MenuIcon/gray.png' where id=317


IF NOT exists (select 1 from ErpModule em where em.Id=318)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(318,303,'Lot Allocation',9,null,null,0,
		   null,null,'Lot Allocation','Konto.Trading.LotAssign.LotAssignView','Konto.Trading',
		   null,null,'Lot Allocation',1,'/Konto.Wpf;component/MenuIcon/gray.png',0, 
		   0,0,0,null,null,1,GETDATE(),GETDATE(),5,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,3,7,11,', AssemblyName = 'Konto.Trading.LotAssign.LotAssignView', MainAssembly ='Konto.Trading' , IconPath='/Konto.Wpf;component/MenuIcon/gray.png' where id=318


IF NOT exists (select 1 from ErpModule em where em.Id=319)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(319,304,'Job Receipt',3,null,null,0,
		   null,null,'Job Receipt','Konto.Trading.JobReceipt.JobReceiptIndex','Konto.Trading',
		   null,null,'Job Receipt',0,'/Konto.Wpf;component/MenuIcon/gray.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),5,1,0,NEWID())
end

update dbo.ErpModule set Extra2=',1,3,4,6,7,9,11,' where id=319 	 

IF NOT exists (select 1 from ErpModule em where em.Id=321)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(321,302,'Journal Voucher',4,null,null,0,
		   null,null,'BillMain','Konto.Shared.Account.Jv.JvIndex','Konto.Shared',
		   null,null,'Journal Voucher',1,'/Konto.Wpf;component/MenuIcon/cash.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),2,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,6,7,10,11,' where id=321


IF NOT exists (select 1 from ErpModule em where em.Id=322)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(322,302,'Receipt Voucher',1,null,null,0,
		   null,null,'BillMain','Konto.Shared.Account.Receipt.ReceiptIndex','Konto.Shared',
		   null,null,'Receipt Voucher',1,'/Konto.Wpf;component/MenuIcon/cash.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),2,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,6,7,10,11,' where id=322


IF NOT exists (select 1 from ErpModule em where em.Id=324)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(324,301,'General Expense',9,null,null,0,
		   null,null,'General Expense','Konto.Shared.Account.GenExpense.GenExpIndex','Konto.Shared',
		   null,null,'General Expense',1,'/Konto.Wpf;component/MenuIcon/purchase.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),12,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,6,7,10,11,' where id=324 	   

IF NOT exists (select 1 from ErpModule em where em.Id=329)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(329,301,'General Exp Return',12,null,null,0,
		   null,null,'General Expense','Konto.Shared.Account.RGenExpense.RGenExpIndex','Konto.Shared',
		   null,null,'General Exp Return',0,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),12,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,6,7,10,11,' where id=329 	   

IF NOT exists (select 1 from ErpModule em where em.Id=325)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(325,302,'Cr/Dr Note',3,null,null,0,
		   null,null,'Cr/Dr Note','Konto.Shared.Account.DRCRNote.DRCRNoteIndex','Konto.Shared',
		   null,null,'Cr/Dr Note',1,'/Konto.Wpf;component/MenuIcon/cash.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),2,1,0,NEWID())
end
 	  update dbo.ErpModule set Extra2=',1,2,3,4,6,7,10,11,' where id=325

IF NOT exists (select 1 from ErpModule em where em.Id=326)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(326,302,'Petty Cash',6,null,null,0,
		   null,null,'Petty Cash',NULL,NULL,
		   null,null,'Petty Cash',1,'/Konto.Wpf;component/MenuIcon/cash.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),2,1,0,NEWID())
end
 	   ---update dbo.ErpModule set Extra2=',1,2,3,4,6,7,10,11,' where id=326

IF NOT exists (select 1 from ErpModule em where em.Id=327)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(327,302,'Fast Cash',7,null,null,0,
		   null,null,'Fast Cash',NULL,NULL,
		   null,null,'Fast Cash',1,'/Konto.Wpf;component/MenuIcon/cash.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),2,1,0,NEWID())
end
 	  -- update dbo.ErpModule set Extra2=',1,2,3,4,6,7,10,11,' where id=327

IF NOT exists (select 1 from ErpModule em where em.Id=328)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(328,302,'Payment Voucher',2,null,null,0,
		   null,null,'BillMain','Konto.Shared.Account.Payment.PaymentIndex','Konto.Shared',
		   null,null,'Payment Voucher',1,'/Konto.Wpf;component/MenuIcon/cash.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),2,1,0,NEWID())
end
 	   update dbo.ErpModule set Extra2=',1,2,3,4,6,7,10,11,' where id=328

IF NOT exists (select 1 from ErpModule em where em.Id=333)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],[ImageIndex],IsActive,IsDeleted,RowId)
		   values(333,301,'Gray Purchase Return',3,null,null,0,
		   null,null,'Challan',null,null,
		   null,null,'Gray purchase Return',0,'/Konto.Wpf;component/MenuIcon/purchase.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),12,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,3,7,11,' where id=333 	   


IF NOT exists (select 1 from ErpModule em where em.Id=334)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(334,301,'Return Challan',10,null,null,0,
		   null,null,'Challan',null,null,
		   null,null,'Return Challan',0,'/Konto.Wpf;component/MenuIcon/purchase.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),12,1,0,NEWID())
end
 	  

IF NOT exists (select 1 from ErpModule em where em.Id=335)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(335,304,'Outward Job Challan',3,null,null,0,
		   null,null,'Outward Job Challan','Konto.Trading.OutJobChallan.OJCIndex','Konto.Trading',
		   null,null,'Outward Job Challan',1,'/Konto.Wpf;component/MenuIcon/gray.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),5,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,10,' where id=335


IF NOT exists (select 1 from ErpModule em where em.Id=336)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],IsActive,IsDeleted,RowId)
		   values(336,304,'JobRec Against Po',4,null,null,0,
		   null,null,'JobRec Against Po','Konto.Trading.JobReceipt.JobReceiptIndex','Konto.Trading',
		   null,null,'JobRec Against Po',0,'/Konto.Wpf;component/MenuIcon/gray.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),1,0,NEWID())
end


if NOT exists (select 1 from ErpModule em where em.Id=351)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate,
		   IsActive,IsDeleted,RowId)
		   values(351,300,'Sales',4,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,'/Konto.Wpf;component/MenuIcon/group.png',0,
		   0,0,0,null,null,1,GETDATE(),1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,6,7,10,11,' where id=351


IF NOT exists (select 1 from ErpModule em where em.Id=352)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],IsActive,IsDeleted,RowId)
		   values(352,351,'Sales Order',1,null,null,0,
		   null,null,'Sales Order','Konto.Shared.Trans.SO.SoIndex','Konto.Shared',
		   null,null,'Sales Order',1,'/Konto.Wpf;component/MenuIcon/sale.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,6,7,10,11,' where id=352

IF NOT exists (select 1 from ErpModule em where em.Id=353)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],IsActive,IsDeleted,RowId)
		   values(353,351,'Sales Approval',2,null,null,0,
		   null,null,'Sale Approval','Konto.Shared.Trans.Po.PoApproveIndex','Konto.Shared',
		   null,null,'Sales Approval',1,'/Konto.Wpf;component/MenuIcon/sale.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),1,0,NEWID())
end
update dbo.ErpModule set Extra2=',1,2,3,4,6,7,10,11,' where id=353 	   

IF NOT exists (select 1 from ErpModule em where em.Id=354)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],IsActive,IsDeleted,RowId)
		   values(354,351,'Job Challan',3,null,null,0,
		   null,null,'Outward','Konto.Shared.Trans.JobChallan.JobIndex','Konto.Shared',
		   null,null,'Job Challan',1,'/Konto.Wpf;component/MenuIcon/sale.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),1,0,NEWID())
end
 	   update dbo.ErpModule set IconPath='/Konto.Wpf;component/MenuIcon/sale.png' where id=354

IF NOT exists (select 1 from ErpModule em where em.Id=355)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(355,351,'Sales Challan',4,null,null,0,
		   null,null,'Outward','Konto.Shared.Trans.SalesChallan.ScIndex','Konto.Shared',
		   null,null,'Outward',1,'/Konto.Wpf;component/MenuIcon/sale.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
update dbo.ErpModule set Extra2=',1,2,3,4,6,7,10,11,' where id=355 	   

IF NOT exists (select 1 from ErpModule em where em.Id=356)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(356,303,'Mill Receipt',5,null,null,0,
		   null,null,'Mill Receipt','Konto.Trading.MillReceipt.MillReceiptIndex','Konto.Trading',
		   null,null,'Mill Receipt',1,'/Konto.Wpf;component/MenuIcon/gray.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
update dbo.ErpModule set Extra2=',1,3,7,11,' where id=356

IF NOT exists (select 1 from ErpModule em where em.Id=358)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(358,351,'Sales Invoice',6,null,null,0,
		   null,null,'BillMain','Konto.Shared.Trans.SInvoice.SInvoiceIndex','Konto.Shared',
		   null,null,'Sales Invoice',1,'/Konto.Wpf;component/MenuIcon/sale.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
update dbo.ErpModule set Extra2=',1,2,3,4,6,7,10,11,' where id=358 	   

IF NOT exists (select 1 from ErpModule em where em.Id=359)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(359,304,'Job Issue',1,null,null,0,
		   null,null,'Challan','Konto.Shared.Trans.JobIssue.JobIssueIndex','Konto.Shared',
		   null,null,'Job Issue',1,'/Konto.Wpf;component/MenuIcon/gray.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
 	   update dbo.ErpModule set AssemblyName = 'Konto.Shared.Trans.JobIssue.JobIssueIndex', IconPath='/Konto.Wpf;component/MenuIcon/gray.png' where id=359

	   update dbo.ErpModule set Extra2=',1,3,4,6,7,10,11,' where id=359

IF NOT exists (select 1 from ErpModule em where em.Id=360)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(360,304,'Job Bill',3,null,null,0,
		   null,null,'BillMain','Konto.Trading.JobReceipt.JrIndex','Konto.Trading',
		   null,null,'Job Bill',1,'/Konto.Wpf;component/MenuIcon/gray.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

 update dbo.ErpModule set Extra2=',1,3,4,6,7,10,11,' where id=360
 	   update dbo.ErpModule set Title = 'Job Receipt Voucher', AssemblyName ='Konto.Trading.JobReceipt.JrIndex',moduledesc='Job Receipt Voucher' where id=360
	 

IF NOT exists (select 1 from ErpModule em where em.Id=361)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(361,303,'Mill Return',5,null,null,0,
		   null,null,'Challan','Konto.Trading.MillReturn.MrIndex','Konto.Trading',
		   null,null,'MillReturn',1,'/Konto.Wpf;component/MenuIcon/gray.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
update dbo.ErpModule set Extra2=',1,3,7,11,' where id=361 	   

IF NOT exists (select 1 from ErpModule em where em.Id=362)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(362,351,'Sales Return',7,null,null,0,
		   null,null,'BillMain','Konto.Shared.Trans.SReturn.SReturnIndex','Konto.Shared',
		   null,null,'Sales Return',1,'/Konto.Wpf;component/MenuIcon/sale.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
 	   update dbo.ErpModule set Extra2=',1,2,3,4,6,7,10,11,' where id=362

IF NOT exists (select 1 from ErpModule em where em.Id=363)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(363,301,'Purchase Return',11,null,null,0,
		   null,null,'BillMain','Konto.Shared.Trans.PReturn.PReturnIndex','Konto.Shared',
		   null,null,'Purchase Return',1,'/Konto.Wpf;component/MenuIcon/purchase.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
 	   update dbo.ErpModule set Extra2=',1,2,3,4,6,7,10,11,' where id=363

if NOT exists (select 1 from ErpModule em where em.Id=364)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(364,378,'Store Issue',7,null,null,0,
		   null,null,'StoreIssue','Konto.Shared.Trans.StoreIssue.StoreIssueIndex','Konto.Shared',
		   null,null,'Store Issue',1,'/Konto.Wpf;component/MenuIcon/gray.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	 update dbo.ErpModule  set packageid=1, Extra2=',1,3,4,7,10,11,' where id=364

if NOT exists (select 1 from ErpModule em where em.Id=365)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(365,300,'Design Mapping',6,null,null,0,
		   null,null,'Design Mapping','Konto.Trading.DesignMapping.DesignMappingIndex','Konto.Trading',
		   null,null,'Design Mapping',0,'/Konto.Wpf;component/MenuIcon/gray.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	   update dbo.ErpModule set Extra2=',1,3,4,7,11,' where id=365


if NOT exists (select 1 from ErpModule em where em.Id=366)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(366,100,'Cheque Bank',8,null,null,0,
		   null,null,'RefBank','Konto.Shared.Masters.RefBank.RefBankIndex','Konto.Shared',
		   null,null,'Cheque Bank',1,'/Konto.Wpf;component/MenuIcon/gray.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	  update dbo.ErpModule set Extra2=',1,2,3,4,6,7,10,11,' where id=366
 
if NOT exists (select 1 from ErpModule em where em.Id=367)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(367,378,'Beam Production',8,null,null,0,
		   null,null,'Beam Production','Konto.Weaves.BeamProduction.BeamProdIndex','Konto.Weaves',
		   null,null,'Beam Prod',1,'/Konto.Wpf;component/MenuIcon/gray.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	  update dbo.ErpModule set Extra2=',3,7,11,' where id=367


if NOT exists (select 1 from ErpModule em where em.Id=368)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(368,378,'Beam Loading',9,null,null,0,
		   null,null,'Beam Loading','Konto.Weaves.BeamLoading.BeamLoadingListView','Konto.Weaves',
		   null,null,'Beam Loading',1,'/Konto.Wpf;component/MenuIcon/gray.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	  update dbo.ErpModule set Extra2=',3,7,11,' where id=368

if NOT exists (select 1 from ErpModule em where em.Id=369)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(369,378,'Taka Production',10,null,null,0,
		   null,null,'Taka Production','Konto.Weaves.TakaProduction.TakaProdIndex','Konto.Weaves',
		   null,null,'Taka Production',1,'/Konto.Wpf;component/MenuIcon/gray.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	  update dbo.ErpModule set Extra2=',3,7,11,' where id=369
 
if NOT exists (select 1 from ErpModule em where em.Id=370)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(370,378,'Batch Master',11,null,null,0,
		   null,null,'Batch Master','Konto.Yarn.BatchMaster.BatchIndex','Konto.Yarn',
		   null,null,'Batch Master',1,'/Konto.Wpf;component/MenuIcon/gray.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	  update dbo.ErpModule set Extra2=',4,7,11,' where id=370

if NOT exists (select 1 from ErpModule em where em.Id=371)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(371,378,'Production',12,null,null,0,
		   null,null,'Production (Packing List)','Konto.Yarn.YarnProduction.YarnProdIndex','Konto.Yarn',
		   null,null,'Production',1,'/Konto.Wpf;component/MenuIcon/gray.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	  update dbo.ErpModule set Extra2=',4,7,11,' where id=371

if NOT exists (select 1 from ErpModule em where em.Id=372)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(372,300,'Taka Opening',13,null,null,0,
		   null,null,'Taka Opening','Konto.Weaves.TakaOp.TakaOpIndex','Konto.Weaves',
		   null,null,'Taka Opening',1,'/Konto.Wpf;component/MenuIcon/gray.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	  update dbo.ErpModule set Extra2=',3,7,11,' where id=372

if NOT exists (select 1 from ErpModule em where em.Id=373)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(373,301,'Gray Order',2,null,null,0,
		   null,null,'Order','Konto.Trading.GreyOrder.GreyOrderIndex','Konto.Trading',
		   null,null,'Gray order',1,'/Konto.Wpf;component/MenuIcon/purchase.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	   update dbo.ErpModule set Extra2=',1,' where id=373


if NOT exists (select 1 from ErpModule em where em.Id=374)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(374,378,'Store Issue Return',14,null,null,0,
		   null,null,'challan','Konto.Shared.Trans.StoreIssueReturn.SIReturnIndex','Konto.Shared',
		   null,null,'Store Issue Return',1,'/Konto.Wpf;component/MenuIcon/gray.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	   update dbo.ErpModule set Extra2=',1,3,4,7,9,10,11,' where id=374

if NOT exists (select 1 from ErpModule em where em.Id=379)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(379,378,'-',14,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,1,null,null,1,GETDATE())
end


if NOT exists (select 1 from ErpModule em where em.Id=375)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(375,351,'Return Challan',5,null,null,0,
		   null,null,'challan',null,null,
		   null,null,'Return Challan',0,null,0,
		   0,0,0,null,null,1,GETDATE())
end

--update ErpModule set Visible = 0 where id = 375

if NOT exists (select 1 from ErpModule em where em.Id=376)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(376,302,'Bank Reconciliation',5,null,null,0,
		   null,null,'challan','Konto.Reporting.Para.Reoncile.BRMainView','Konto.Reporting',
		   null,null,'Bank Reconciliation',1,'/Konto.Wpf;component/MenuIcon/cash.png',0,
		   0,0,0,null,null,1,GETDATE())
end
update dbo.ErpModule set Extra2=',1,2,3,4,6,7,10,11,' where id=376

IF NOT exists (select 1 from ErpModule em where em.Id=377)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(377,303,'Mill Receipt Voucher',4,null,null,1,
		   null,null,'BillMain','Konto.Trading.MillReceipt.MrvIndex','Konto.Trading',
		   null,null,'Mill Receipt Voucher',1,'/Konto.Wpf;component/MenuIcon/gray.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
 	
	   update dbo.ErpModule set extra2=',1,3,7,11,', Title = 'Mill Receipt Voucher', AssemblyName ='Konto.Trading.MillReceipt.MrvIndex' where id=377


IF NOT exists (select 1 from ErpModule em where em.Id=378)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(378,300,'Production',2,null,null,1,
		   null,null,null,null,null,
		   null,null,'Production',1,'/Konto.Wpf;component/MenuIcon/production.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
 	 update dbo.ErpModule set Extra2=',1,3,4,7,11,' where id=378


if NOT exists (select 1 from ErpModule em where em.Id=380)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(380,378,'Taka Conversion',10,null,null,0,
		   null,null,'Challan','Konto.Weaves.TakaConv.TakaConvIndex','Konto.Weaves',
		   null,null,'Taka Conversion',1,'/Konto.Wpf;component/MenuIcon/gray.png',0,
		   0,0,0,null,null,1,GETDATE())
end
update dbo.ErpModule set Extra2=',3,7,11,' where id=380 	 


 if NOT exists (select 1 from ErpModule em where em.Id=381)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(381,378,'Taka Cutting',10,null,null,0,
		   null,null,'Challan','Konto.Weaves.TakaCutting.TakaCuttingIndex','Konto.Weaves',
		   null,null,'Taka Cutting',1,'/Konto.Wpf;component/MenuIcon/gray.png',0,
		   0,0,0,null,null,1,GETDATE())
end
update dbo.ErpModule set Extra2=',3,7,' where id=381 	   

--- Tools
if NOT exists (select 1 from ErpModule em where em.Id=700)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(700,0,'Tools',10,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,'/Konto.Wpf;component/MenuIcon/tools.png',0,
		   0,0,0,null,null,1,GETDATE())
end
update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=700 	   

if NOT exists (select 1 from ErpModule em where em.Id=701)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(701,700,'Backup / Restore',1,null,null,0,
		   null,null,null,'Konto.Shared.BackupData.BackupMainView','Konto.Shared',
		   null,null,'Backup',1,'/Konto.Wpf;component/MenuIcon/backup.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	   update dbo.ErpModule set Title = 'Backup / Restore', AssemblyName = 'Konto.Shared.BackupData.BackupMainView', MainAssembly='Konto.Shared',IconPath='/Konto.Wpf;component/MenuIcon/backup.png' where id=701

	   update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=701

if NOT exists (select 1 from ErpModule em where em.Id=702)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(702,700,'Change Password',2,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,'/Konto.Wpf;component/MenuIcon/user.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	   update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=702

if NOT exists (select 1 from ErpModule em where em.Id=703)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(703,700,'-',3,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,1,null,null,1,GETDATE())
end


if NOT exists (select 1 from ErpModule em where em.Id=704)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(704,700,'Data Freeze',4,null,null,0,
		   null,null,null,'Konto.Shared.DataFreeze.DataFreezeDetail','Konto.Shared',
		   null,null,'Data Freeze',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

update ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,', Title = 'Data Freeze', AssemblyName ='Konto.Shared.DataFreeze.DataFreezeDetail', MainAssembly = 'Konto.Shared' where id = 704

if NOT exists (select 1 from ErpModule em where em.Id=705)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(705,700,'Account/Product Merge',5,null,null,0,
		   null,null,null,'Konto.Shared.Masters.Merge.MergeIndex','Konto.Shared',
		   null,null,'Account/Product Merge',1,'/Konto.Wpf;component/MenuIcon/setup.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,',  Title = 'Account/Product Merge', AssemblyName= 'Konto.Shared.Masters.Merge.MergeIndex',MainAssembly = 'Konto.Shared' ,IconPath='/Konto.Wpf;component/MenuIcon/setup.png' where id=705

--if NOT exists (select 1 from ErpModule em where em.Id=706)
--begin
--insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
--           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
--           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
--           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
--		   values(706,700,'Voucher Reindex',6,null,null,0,
--		   null,null,null,null,null,
--		   null,null,null,1,null,0,
--		   0,0,0,null,null,1,GETDATE())
--end

if NOT exists (select 1 from ErpModule em where em.Id=707)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(707,700,'-',7,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,1,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=708)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(708,700,'Depericiation Posting',8,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,'/Konto.Wpf;component/MenuIcon/setup.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=708


if NOT exists (select 1 from ErpModule em where em.Id=709)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(709,700,'Cash Adjustment',9,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,'/Konto.Wpf;component/MenuIcon/setup.png',0,
		   0,0,0,null,null,1,GETDATE())
end

 update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=709

if NOT exists (select 1 from ErpModule em where em.Id=710)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(710,700,'-',10,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,1,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=711)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(711,700,'Ledger Audit',11,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,'/Konto.Wpf;component/MenuIcon/setup.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=711


IF NOT exists (select 1 from ErpModule em where em.Id=1066)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(1066,700,'Balance Carryforward to Next Year',12,null,null,0,
		   null,null,'Balance Carryforward to Next Year','Konto.Shared.BalTransfer.BalTransferIndex','Konto.Shared',
		   null,null,'Balance Carryforward to Next Year',1,'/Konto.Wpf;component/MenuIcon/tax.png',0,
		   0,0,0,null,null,1,GETDATE())
END

 update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=1066
 update dbo.ErpModule set ModuleDesc = 'Balance Carryforward to Next Year', AssemblyName = 'Konto.Shared.BalTransfer.BalTransferIndex', Title = 'Balance Carryforward to Next Year', IconPath='/Konto.Wpf;component/MenuIcon/tools.png' where id=1066


--Reports
if NOT exists (select 1 from ErpModule em where em.Id=800)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(800,0,'Reports',10,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,'/Konto.Wpf;component/MenuIcon/report.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	   update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=800

if NOT exists (select 1 from ErpModule em where em.Id=801)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(801,800,'Account Ledger',1,null,null,0,
		   null,null,'Ledger','Konto.Reporting.Para.Ledger.LedgerMainView','Konto.Reporting',
		   null,null,'Account Ledger',1,'/Konto.Wpf;component/MenuIcon/reportview.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	  update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=801

if NOT exists (select 1 from ErpModule em where em.Id=802)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(802,800,'Outstanding',2,null,null,0,
		   null,null,'BillMain','Konto.Reporting.Para.Outs.OutsMainView','Konto.Reporting',
		     null,null,'Outstanding Report',1,'/Konto.Wpf;component/MenuIcon/reportview.png',0,
		   0,0,0,null,null,1,GETDATE()) 
end
 	   update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=802

if NOT exists (select 1 from ErpModule em where em.Id=803)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(803,800,'Trial Balance',4,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,'/Konto.Wpf;component/MenuIcon/reportview.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	    update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=803

if NOT exists (select 1 from ErpModule em where em.Id=804)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(804,800,'Balance Sheet',5,null,null,0,
		   null,null,'BalanceSheet','Konto.Reporting.Para.BlSheet.BlMainView','Konto.Reporting',
		   null,null,'Balance Sheet',1,'/Konto.Wpf;component/MenuIcon/reportview.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	  update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=804
 
if NOT exists (select 1 from ErpModule em where em.Id=805)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(805,800,'Inventory Register',6,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,'/Konto.Wpf;component/MenuIcon/group.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	    update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=805

if NOT exists (select 1 from ErpModule em where em.Id=806)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(806,805,'GRN',1,null,null,0,
		   null,null,'BillMain','Konto.Reporting.Para.ChlPara.ChlParaMainView','Konto.Reporting',
		   null,null,'Challan Register',1,'/Konto.Wpf;component/MenuIcon/reportview.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	   update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=806

if NOT exists (select 1 from ErpModule em where em.Id=807)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(807,805,'Purchase',2,null,null,0,
		   null,null,'BillMain','Konto.Reporting.Para.BillPara.ParaMainView','Konto.Reporting',
		   null,null,'Bill Register',1,'/Konto.Wpf;component/MenuIcon/reportview.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	   update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=807

if NOT exists (select 1 from ErpModule em where em.Id=808)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(808,805,'Gray Purchse',3,null,null,0,
		   null,null,null,'Konto.Reporting.Para.ChlPara.ChlParaMainView','Konto.Reporting',
		   null,null,'Gray Purchase',1,'/Konto.Wpf;component/MenuIcon/reportview.png',0,
		   0,0,0,null,null,1,GETDATE())
end
update dbo.ErpModule set Extra2=',1,' where id=808

 	   update dbo.ErpModule set Title = 'Gray Purchase', MainAssembly = 'Konto.Reporting', AssemblyName = 'Konto.Reporting.Para.ChlPara.ChlParaMainView', IconPath='/Konto.Wpf;component/MenuIcon/reportview.png' where id=808

if NOT exists (select 1 from ErpModule em where em.Id=809)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(809,805,'Gray Purchase Return',4,null,null,0,
		   null,null,null,null,null,
		   null,null,null,0,'/Konto.Wpf;component/MenuIcon/reportview.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	  update dbo.ErpModule set Extra2=',1,' where id=809

if NOT exists (select 1 from ErpModule em where em.Id=810)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(810,805,'Purchase Return Challan',5,null,null,0,
		   null,null,null,null,null,
		   null,null,null,0,'/Konto.Wpf;component/MenuIcon/reportview.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	 update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=810

if NOT exists (select 1 from ErpModule em where em.Id=811)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(811,805,'Purchase Return',6,null,null,0,
		   null,null,null,null,null,
		   null,null,null,0,'/Konto.Wpf;component/MenuIcon/reportview.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	  update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=811

if NOT exists (select 1 from ErpModule em where em.Id=812)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(812,805,'-',7,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,1,null,null,1,GETDATE())
end


if NOT exists (select 1 from ErpModule em where em.Id=813)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(813,805,'Sales Challan',8,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,'/Konto.Wpf;component/MenuIcon/reportview.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	  update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=813

if NOT exists (select 1 from ErpModule em where em.Id=814)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(814,805,'Sales',9,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,'/Konto.Wpf;component/MenuIcon/reportview.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	   update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=814

if NOT exists (select 1 from ErpModule em where em.Id=815)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(815,805,'Sales Return Challan',10,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,'/Konto.Wpf;component/MenuIcon/reportview.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	   update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=815

if NOT exists (select 1 from ErpModule em where em.Id=816)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(816,805,'Sales Return',11,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 	  update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=816

if NOT exists (select 1 from ErpModule em where em.Id=817)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(817,805,'-',12,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,1,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=818)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(818,805,'Gray Issue',13,null,null,0,
		   null,null,null,'Konto.Reporting.Para.ChlPara.ChlParaMainView','Konto.Reporting',
		   null,null,null,1,'/Konto.Wpf;component/MenuIcon/reportview.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	  update dbo.ErpModule set Extra2=',1,3,7,11,' where id=818

if NOT exists (select 1 from ErpModule em where em.Id=819)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(819,805,'Mill Receipt Challan',14,null,null,0,
		   null,null,null,null,null,
		   null,null,null,0,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 update dbo.ErpModule set Extra2=',1,3,7,11,' where id=819
 	  -- update dbo.ErpModule set IconPath='/Konto.Wpf;component/MenuIcon/reportview.png' where id=819

if NOT exists (select 1 from ErpModule em where em.Id=820)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(820,805,'Mill Receipt Bill',15,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 	  update dbo.ErpModule set Extra2=',1,3,7,11,' where id=820

if NOT exists (select 1 from ErpModule em where em.Id=821)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(821,805,'Mill Return',16,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 	    update dbo.ErpModule set Extra2=',1,3,7,11,' where id=821

if NOT exists (select 1 from ErpModule em where em.Id=822)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(822,805,'-',17,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,1,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=823)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(823,805,'Job Issue',18,null,null,0,
		   null,null,null,'Konto.Reporting.Para.ChlPara.ChlParaMainView','Konto.Reporting',
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 	   update dbo.ErpModule set Extra2=',1,3,4,6,7,11,' where id=823

if NOT exists (select 1 from ErpModule em where em.Id=824)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(824,805,'Job Receipt Challan Report',19,null,null,0,
		   null,null,null,'Konto.Reporting.Para.ChlPara.ChlParaMainView','Konto.Reporting',
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 	   update dbo.ErpModule set Extra2=',1,3,4,6,7,11,' where id=824

if NOT exists (select 1 from ErpModule em where em.Id=825)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(825,805,'Job Receipt Bill Report',20,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 	 update dbo.ErpModule set Extra2=',1,3,4,6,7,11,' where id=825

if NOT exists (select 1 from ErpModule em where em.Id=826)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(826,805,'Folding Report',21,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 	update dbo.ErpModule set Extra2=',1,3,7,11,' where id=826

if NOT exists (select 1 from ErpModule em where em.Id=1067)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(1067,805,'Lot No Track',22,null,null,0,
		   null,null,'LotTracker','Konto.Reporting.Para.LotTrack.LotTrackMainView','Konto.Reporting',
		   null,null,'Lot No Track',1,'/Konto.Wpf;component/MenuIcon/reportview.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	   update dbo.ErpModule set IconPath='/Konto.Wpf;component/MenuIcon/reportview.png' where id=1067

if NOT exists (select 1 from ErpModule em where em.Id=827)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(827,800,'Cr/Dr Note',7,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 	  update dbo.ErpModule set Extra2=',1,2,3,4,6,7,10,11,' where id=827

if NOT exists (select 1 from ErpModule em where em.Id=828)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(828,800,'Order Register',8,null,null,0,
		   null,null,'Order Register','Konto.Reporting.Para.OrdPara.OrdParaMainView','Konto.Reporting',
		   null,null,'Order Register',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 	   update dbo.ErpModule set Extra2=',1,2,3,4,6,7,10,11,' where id=828


if NOT exists (select 1 from ErpModule em where em.Id=829)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(829,800,'Tax Register',9,null,null,0,
		   null,null,null,null,null,
		   null,null,'Tax Register',1,'/Konto.Wpf;component/MenuIcon/group.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	   update dbo.ErpModule set Extra2=',1,2,3,4,6,7,10,11,' where id=829

if NOT exists (select 1 from ErpModule em where em.Id=830)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(830,829,'GSTR-1',1,null,null,0,
		   null,null,null,null,null,
		   null,null,'Gstr-1',1,'/Konto.Wpf;component/MenuIcon/tax.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	    update dbo.ErpModule set Extra2=',1,2,3,4,6,7,10,11,' where id=830

 if NOT exists (select 1 from ErpModule em where em.Id=831)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(831,829,'GSTR-2',2,null,null,0,
		   null,null,null,null,null,
		   null,null,'Gstr-2',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 	   update dbo.ErpModule set Extra2=',1,2,3,4,6,7,10,11,' where id=831

 if NOT exists (select 1 from ErpModule em where em.Id=832)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(832,829,'GSTR-3B',3,null,null,0,
		   null,null,null,null,null,
		   null,null,'Gstr-3B',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 	   update dbo.ErpModule set Extra2=',1,2,3,4,6,7,10,11,' where id=832

 if NOT exists (select 1 from ErpModule em where em.Id=833)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(833,829,'GSTR-4A (Job Work)',4,null,null,0,
		   null,null,null,null,null,
		   null,null,'Gstr-4A',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 	   update dbo.ErpModule set Extra2=',1,2,3,4,6,7,10,11,' where id=833

 if NOT exists (select 1 from ErpModule em where em.Id=834)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(834,829,'GSTR-2 Reconcile',5,null,null,0,
		   null,null,null,'Konto.Reporting.Para.Gstr2Reconcile.GsttwoReconcileMainView','Konto.Reporting',
		   null,null,'Gstr-2 Reconcile',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 	   
update dbo.ErpModule set [AssemblyName] = 'Konto.Reporting.Para.Gstr2.Gst2Reconcile' where id=834
 update dbo.ErpModule set Extra2=',1,2,3,4,6,7,10,11,' where id=834

 if NOT exists (select 1 from ErpModule em where em.Id=835)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(835,829,'-',6,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,1,null,null,1,GETDATE())
end
 if NOT exists (select 1 from ErpModule em where em.Id=836)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(836,829,'TDS',7,null,null,0,
		   null,null,null,'Konto.Reporting.Para.TDSPara.TDSMainView','Konto.Reporting',
		   null,null,'Tds',1,'',0,
		   0,0,0,null,null,1,GETDATE())
end
 	   update dbo.ErpModule set Extra2=',1,2,3,4,6,7,10,11,' where id=836

 if NOT exists (select 1 from ErpModule em where em.Id=837)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(837,829,'TCS',8,null,null,0,
		   null,null,null,null,null,
		   null,null,'Tcs',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 	  update dbo.ErpModule set Extra2=',1,2,3,4,6,7,10,11,' where id=837

if NOT exists (select 1 from ErpModule em where em.Id=1072)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(1072,829,'GST Print',8,null,null,0,
		   null,null,null,null,null,
		   null,null,'Gst Print',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 	  --update dbo.ErpModule set Extra2=',1,2,3,4,6,7,10,11,' where id=1072
 
 if NOT exists (select 1 from ErpModule em where em.Id=838)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(838,800,'Production',8,null,null,0,
		   null,null,null,null,null,
		   null,null,'Production',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 	   update dbo.ErpModule set Extra2=',1,3,4,7,11,' where id=838

 
if NOT exists (select 1 from ErpModule em where em.Id=839)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(839,838,'Beam Register',1,null,null,0,
		   null,null,null,null,null,
		   null,null,'Beam Register',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

update erpmodule set [AssemblyName]='Konto.Reporting.Para.BeamProdPara.BPParaMainView',[MainAssembly]='Konto.Reporting' where id=839
 update dbo.ErpModule set Extra2=',3,7,11,' where id=839
 	  
 
 if NOT exists (select 1 from ErpModule em where em.Id=840)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(840,838,'Taka Prod Register',2,null,null,0,
		   null,null,null,'Konto.Reporting.Para.TakaPara.TakaParaMainView','Konto.Reporting',
		   null,null,'Taka Prod Register',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 	   update dbo.ErpModule set Extra2=',3,7,11,' where id=840
update erpmodule set [AssemblyName]='Konto.Reporting.Para.TakaPara.TakaParaMainView', [MainAssembly]='Konto.Reporting' where id=840
 
 if NOT exists (select 1 from ErpModule em where em.Id=841)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(841,838,'Salary/Patia register',3,null,null,0,
		   null,null,null,null,null,
		   null,null,'Salary/Patia',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
update dbo.ErpModule set Extra2=',3,7,11,' where id=841
update ErpModule set Title ='Salary/Patia', AssemblyName ='Konto.Reporting.Wvs.SalaryGenView', MainAssembly='Konto.Reporting',IconPath='/Konto.Wpf;component/MenuIcon/reportview.png' where id=841

	 
 if NOT exists (select 1 from ErpModule em where em.Id=842)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(842,838,'Store Issue',4,null,null,0,
		   null,null,null,null,null,
		   null,null,'Store Issue',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end


update dbo.ErpModule set Extra2=',1,4,3,7,9,10,11,' where id=842
 
 if NOT exists (select 1 from ErpModule em where em.Id=843)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(843,838,'Store Return',5,null,null,0,
		   null,null,null,null,null,
		   null,null,'Store Return',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 	   update dbo.ErpModule set Extra2=',4,3,7,9,10,11,' where id=843

 if NOT exists (select 1 from ErpModule em where em.Id=844)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(844,838,'Taka Tracker',6,null,null,0,
		   null,null,null,null,null,
		   null,null,'Taka Tracker',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 	   update dbo.ErpModule set Extra2=',3,7,11,' where id=844
 
 if NOT exists (select 1 from ErpModule em where em.Id=845)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(845,838,'Yarn Prod. Reg',6,null,null,0,
		   null,null,null,null,null,
		   null,null,'Yarn Prod. Reg',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 	 update dbo.ErpModule set Extra2=',4,7,11,' where id=845

 
--if NOT exists (select 1 from ErpModule em where em.Id=846)
--begin
--insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
--           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
--           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
--           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
--		   values(846,800,'Interest Ledger',1,null,null,0,
--		   null,null,'Int. Ledger','Konto.Reporting.Para.Ledger.IntLedgerMainView','Konto.Reporting',
--		   null,null,'Interest Ledger',1,'/Konto.Wpf;component/MenuIcon/reportview.png',0,
--		   0,0,0,null,null,1,GETDATE())
--end



Update erpModule set Visible=0 where id=846

--- setup
if NOT exists (select 1 from ErpModule em where em.Id=900)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(900,0,'Setup',10,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,'/Konto.Wpf;component/MenuIcon/setup.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	   update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=900

IF NOT exists (select 1 from ErpModule em where em.Id=901)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(901,900,'User Role',2,null,null,0,
		   null,null,'User Role','Konto.Shared.Security.RoleIndex','Konto.Shared',
		   null,null,'User Role',1,'/Konto.Wpf;component/MenuIcon/user.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
 	  update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=901

IF NOT exists (select 1 from ErpModule em where em.Id=902)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(902,900,'User Master',4,null,null,0,
		   null,null,'User Master','Konto.Shared.Security.UserIndex','Konto.Shared',
		   null,null,'User Master',1,'/Konto.Wpf;component/MenuIcon/user.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end 
 	   update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=902

IF NOT exists (select 1 from ErpModule em where em.Id=903)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(903,900,'-',5,null,null,0,
		   null,null,'-',null,null,
		   null,null,'-',1,null,0,
		   0,0,1,null,null,1,GETDATE(),GETDATE())
end 

IF NOT exists (select 1 from ErpModule em where em.Id=904)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(904,900,'Menu Setup',6,null,null,0,
		   null,null,'Menu Setup','Konto.Shared.Security.MenuIndex','Konto.Shared',
		   null,null,'Menu Setup',1,'/Konto.Wpf;component/MenuIcon/setup.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end 
 	  update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=904
 
--IF NOT exists (select 1 from ErpModule em where em.Id=905)
--begin
--insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
--           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
--           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
--           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
--		   [CreateDate],[ModifyDate])
--		   values(905,900,'Report Setup',7,null,null,0,
--		   null,null,'Report Setup',null,null,
--		   null,null,'Report Setup',1,null,0,
--		   0,0,0,null,null,1,GETDATE(),GETDATE())
--end 
-- update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=905

IF NOT exists (select 1 from ErpModule em where em.Id=906)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(906,900,'Report Designer',8,null,null,0,
		   null,null,'Report Designer',null,null,
		   null,null,'Report Designer',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end 

update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=906
 
IF NOT exists (select 1 from ErpModule em where em.Id=907)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(907,900,'-',9,null,null,0,
		   null,null,'-',null,null,
		   null,null,'-',1,null,0,
		   0,0,1,null,null,1,GETDATE(),GETDATE())
end 
 
IF NOT exists (select 1 from ErpModule em where em.Id=908)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(908,900,'Payment/Receipt Setup',10,null,null,0,
		   null,null,'Payment/Receipt Setup',null,null,
		   null,null,'Payment/Receipt Setup',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end 

  update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=908
 
IF NOT exists (select 1 from ErpModule em where em.Id=909)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(909,900,'Global Setup',11,null,null,0,
		   null,null,'Global Setup',null,null,
		   null,null,'Global Setup',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end 

update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=909


IF NOT exists (select 1 from ErpModule em where em.Id=910)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(910,900,'Global Company Setup',12,null,null,0,
		   null,null,'Global Company Setup',null,null,
		   null,null,'Global Company Setup',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end 

update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=910
 
 IF NOT exists (select 1 from ErpModule em where em.Id=911)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(911,900,'Report Setup',13,null,null,0,
		   null,null,'Report Setup','Konto.Shared.Security.ReportSetupMainView','Konto.Shared',
		   null,null,'Report Setup',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=911

 IF NOT exists (select 1 from ErpModule em where em.Id=913)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(913,900,'Group Setup',13,null,null,0,
		   null,null,'Group Setup','Konto.Shared.Masters.LogIn.GroupSetupMainView','Konto.Shared',
		   null,null,'Group Setup',1,'/Konto.Wpf;component/MenuIcon/setup.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=913

IF NOT exists (select 1 from ErpModule em where em.Id=1056)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(1056,303,'Process Bill',7,null,null,0,
		   null,null,'Bill Main','Konto.Shared.Trans.PInvoice.PInvoiceIndex','Konto.Shared',
		   null,null,'Process Bill',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

update dbo.ErpModule set Extra2='' where id=1056

IF NOT exists (select 1 from ErpModule em where em.Id=1051)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(1051,101,'Division',6,null,null,0,
		   null,null,'Division','Konto.Shared.Masters.Div.DivIndex','Konto.Shared',
		   null,null,'Division',1,'/Konto.Wpf;component/MenuIcon/location.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
 	   update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=1051

if NOT exists (select 1 from ErpModule em where em.Id=1052)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(1052,106,'-',9,null,null,0,
		   null,null,'-',Null,NUll,
		   null,null,'-',1,null,0,
		   0,0,1,null,null,1,GETDATE(),GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=1050)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(1050,101,'Branch',6,null,null,0,
		   null,null,'Branch','Konto.Shared.Masters.Branch.BranchIndex','Konto.Shared',
		   null,null,'Branch',1,'/Konto.Wpf;component/MenuIcon/location.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
 	 update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=1050

if NOT exists (select 1 from ErpModule em where em.Id=1057)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(1057,302,'Ledger Report',7,null,null,0,
		   null,null,'BillMain','Konto.Shared.Account.LedgerReport.LedgerMainView','Konto.Shared',
		   null,null,'Ledger Report',1,'/Konto.Wpf;component/MenuIcon/tax.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
 	 --- update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=1057

if NOT exists (select 1 from ErpModule em where em.Id=1058)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(1058,800,'Stock Report',3,null,null,0,
		   null,null,'BillMain','Konto.Reporting.Para.Stock.StockMainView','Konto.Reporting',
		   null,null,'Stock Report',1,'/Konto.Wpf;component/MenuIcon/reportview.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
 	  update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=1058



if NOT exists (select 1 from ErpModule em where em.Id=847)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(847,800,'Design Stock Report',3,null,null,0,
		   null,null,'BillMain','Konto.Reporting.Para.Stock.DesignStockView','Konto.Reporting',
		   null,null,'Design Stock Report',1,'/Konto.Wpf;component/MenuIcon/reportview.png',0,
		   0,0,0,null,',1,3,11,',1,GETDATE(),GETDATE())
end
 	  


if NOT exists (select 1 from ErpModule em where em.Id=1059)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(1059,829,'GST Register',17,null,null,0,
		   null,null,'BillMain','Konto.Reporting.Para.Gst.GstRegView','Konto.Reporting',
		   null,null,'Gst Report',1,'/Konto.Wpf;component/MenuIcon/tax.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
 	   update dbo.ErpModule set [ParentId]=829, [ModuleDesc]='GST Register', 
		IconPath='/Konto.Wpf;component/MenuIcon/tax.png',
		[AssemblyName]='Konto.Reporting.Para.Gst.GstRegView',
		[MainAssembly]='Konto.Reporting',Extra2=',1,2,3,4,6,7,9,10,11,'
		where id=1059

if NOT exists (select 1 from ErpModule em where em.Id=1060)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(1060,300,'Outstanding Report',17,null,null,0,
		   null,null,'BillMain','Konto.Shared.Outstanding.OutsMainView','Konto.Shared',
		   null,null,'Outstanding Report',1,'/Konto.Wpf;component/MenuIcon/tax.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end 
 	    --update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=1060

if NOT exists (select 1 from ErpModule em where em.Id=140)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(140,100,'Account Section',3,null,null,0,
		   null,null,'AccountSection',null,null,
		   null,null,'AccountSection',1,'/Konto.Wpf;component/MenuIcon/group.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end  
 	    update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=140

if NOT exists (select 1 from ErpModule em where em.Id=141)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(141,100,'Opening',4,null,null,0,
		   null,null,'Opening',null,null,
		   null,null,'Opening',1,'/Konto.Wpf;component/MenuIcon/group.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end  
 	   update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=141

if NOT exists (select 1 from ErpModule em where em.Id=142)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(142,141,'Op. Account Balance',2,null,null,0,
		   null,null,'Op. Account Balance',null,null,
		   null,null,'Op. Account Balance',1,'/Konto.Wpf;component/MenuIcon/opening.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end  
 	    update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=142

if NOT exists (select 1 from ErpModule em where em.Id=143)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(143,141,'Op. Gray Stock',3,null,null,0,
		   null,null,'Op. Gray Stock',null,null,
		   null,null,'Op. Gray Stock',1,'/Konto.Wpf;component/MenuIcon/opening.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end  
 	    update dbo.ErpModule set Extra2=',3,7,11,' where id=143

if NOT exists (select 1 from ErpModule em where em.Id=144)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(144,141,'Op. Finish Stock',4,null,null,0,
		   null,null,'Op. Finish Stock',null,null,
		   null,null,'Op. Finish Stock',1,'/Konto.Wpf;component/MenuIcon/opening.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end  
 	   update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=144

if NOT exists (select 1 from ErpModule em where em.Id=145)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(145,141,'Op. Mill Issue',5,null,null,0,
		   null,null,'Op. Mill Stock',null,null,
		   null,null,'Op. Mill Issue',1,'/Konto.Wpf;component/MenuIcon/opening.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end  
 	   update dbo.ErpModule set Extra2=',1,3,7,11,' where id=145

if NOT exists (select 1 from ErpModule em where em.Id=146)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(146,141,'Op. Job Issue',6,null,null,0,
		   null,null,'Op. Job Issue',null,null,
		   null,null,'Op. Job Issue',1,'/Konto.Wpf;component/MenuIcon/opening.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end  
 	   update dbo.ErpModule set Extra2=',1,3,4,6,7,11,' where id=146

if NOT exists (select 1 from ErpModule em where em.Id=147)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(147,141,'Part Payment',7,null,null,0,
		   null,null,'Part Payment',null,null,
		   null,null,'Part Payment',0,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end  
 update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=147

if NOT exists (select 1 from ErpModule em where em.Id=148)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(148,141,'Opening Cheque',8,null,null,0,
		   null,null,'Opening Cheque',null,null,
		   null,null,'Opening Cheque',0,'/Konto.Wpf;component/MenuIcon/opening.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end  
 	  update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=148

if NOT exists (select 1 from ErpModule em where em.Id=150)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(150,141,'Opening Taka Stock',8,null,null,0,
		   null,null,'Opening Taka Stock',null,null,
		   null,null,'Opening Taka Stock',0,'/Konto.Wpf;component/MenuIcon/opening.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end  
 	   update dbo.ErpModule set Extra2=',3,7,11,' where id=150

if NOT exists (select 1 from ErpModule em where em.Id=151)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(151,141,'Opening Beam Stock',8,null,null,0,
		   null,null,'Opening Beam Stock',null,null,
		   null,null,'Opening Beam Stock',0,'/Konto.Wpf;component/MenuIcon/opening.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end  
 	  update dbo.ErpModule set Extra2=',3,7,11,' where id=151
 
if NOT exists (select 1 from ErpModule em where em.Id=152)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(152,141,'Opening Stock',8,null,null,0,
		   null,null,'Opening Stock',null,null,
		   null,null,'Opening Stock',1,'/Konto.Wpf;component/MenuIcon/opening.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end  
 	   update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=152

if NOT exists (select 1 from ErpModule em where em.Id=153)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(153,106,'Warp Item',8,null,null,0,
		   null,null,'Warp Item','Konto.Shared.Masters.WarpItem.WarpItemIndex','Konto.Shared',
		   null,null,'Warp Item',1,'/Konto.Wpf;component/MenuIcon/opening.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
 	   update dbo.ErpModule set Extra2=',3,7,11,' where id=153

if NOT exists (select 1 from ErpModule em where em.Id=1061)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(1061,378,'Jacquard Weaving Details',15,null,null,0,
		   null,null,'WeftItem','Konto.Weaves.ColorMatching.ColorMIndex','Konto.Weaves',
		   null,null,'jacquard Weaving Details',1,'/Konto.Wpf;component/MenuIcon/gray.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
End
 	   update dbo.ErpModule set Visible=1, AssemblyName = 'Konto.Weaves.ColorMatching.ColorMIndex', MainAssembly='Konto.Weaves', IconPath='/Konto.Wpf;component/MenuIcon/gray.png' where id=1061
	    update dbo.ErpModule set Extra2=',3,7,11,' where id=1061

if NOT exists (select 1 from ErpModule em where em.Id=154)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(154,100,'Machine Position',8,null,null,0,
		   null,null,'Machine Position','Konto.Shared.Masters.Position.PositionIndex','Konto.Shared',
		   null,null,'Machine Position',1,'/Konto.Wpf;component/MenuIcon/opening.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
 update dbo.ErpModule set Extra2=',3,7,11,' where id=154

-- cost center/head
if NOT exists (select 1 from ErpModule em where em.Id=155)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(155,100,'Cost Center/Head',8,null,null,0,
		   null,null,'Cost Center','Konto.Shared.Masters.CH.CostHeadIndex','Konto.Shared',
		   null,null,'Cost Center',1,'/Konto.Wpf;component/MenuIcon/opening.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end 

update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=155


if NOT exists (select 1 from ErpModule em where em.Id=1062)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(1062,378,'Weaving Job Card',15,null,null,0,
		   null,null,'Job Card','Konto.Weaves.JobCard.JobCardIndex','Konto.Weaves',
		   null,null,'Weaving Job Card',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
End

 update dbo.ErpModule set Extra2=',3,7,11,' where id=1062
 	   update dbo.ErpModule set visible=1,  AssemblyName=  'Konto.Weaves.JobCard.JobCardIndex', IconPath='/Konto.Wpf;component/MenuIcon/gray.png' where id=1062

if NOT exists (select 1 from ErpModule em where em.Id=1063)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(1063,351,'Brokerage',8,null,null,0,
		   null,null,'Brokerage','Konto.Shared.Trans.Brokerage.BrokerageIndex','Konto.Shared',
		   null,null,'Brokerage',1,'/Konto.Wpf;component/MenuIcon/sale.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
End
 update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=1058



if NOT exists (select 1 from ErpModule em where em.Id=1064)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(1064,100,'Process',9,null,null,0,
		   null,null,'Process','Konto.Shared.Masters.Process.ProcessIndex','Konto.Shared',
		   null,null,'Process',1,'/Konto.Wpf;component/MenuIcon/gray.png',0,
		   0,0,0,null,null,1,GETDATE())
end
 	   update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=1058

IF NOT exists (select 1 from ErpModule em where em.Id=1065)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(1065,304,'Taka Wise Job Receipt Chalan',5,null,null,0,
		   null,null,'Job Receipt','Konto.Trading.TakaWiseJobReceipt.TakaWiseJobReceiptIndex','Konto.Trading',
		   null,null,'Taka Wise Job Receipt',1,'/Konto.Wpf;component/MenuIcon/tax.png',0,
		   0,0,0,null,null,1,GETDATE())
END
 update dbo.ErpModule set Extra2=',1,3,4,7,9,10,11,' where id=1065


IF NOT exists (select 1 from ErpModule em where em.Id=1068)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(1068,378,'Color Recipe',15,null,null,0,
		   null,null,'challan','Konto.Yarn.Color.ColorRecipeIndex','Konto.Yarn',
		   null,null,'Color Recipe',1,'/Konto.Wpf;component/MenuIcon/gray.png',0,
		   0,0,0,null,null,1,GETDATE())
END

 update dbo.ErpModule set Extra2=',4,11,' where id=1068

IF NOT exists (select 1 from ErpModule em where em.Id=1069)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(1069,378,'Chemical Formula',16,null,null,0,
		   null,null,'challan','Konto.Yarn.Color.ColorFormulaIndex','Konto.Yarn',
		   null,null,'Chemical Formula',1,'/Konto.Wpf;component/MenuIcon/gray.png',0,
		   0,0,0,null,null,1,GETDATE())
END
 update dbo.ErpModule set Extra2=',4,11,' where id=1069

update ErpModule set ModuleDesc = 'Chemical Formula', Title = 'Chemical Formula' where id = 1069

IF NOT exists (select 1 from ErpModule em where em.Id=1070)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(1070,378,'Job Card',17,null,null,0,
		   null,null,'challan','Konto.Yarn.JobCard.JobCardIndex','Konto.Yarn',
		   null,null,'Job Card',1,'/Konto.Wpf;component/MenuIcon/gray.png',0,
		   0,0,0,null,null,1,GETDATE())
END
 update dbo.ErpModule set Extra2=',4,11,' where id=1070

 	   update dbo.ErpModule set  AssemblyName=  'Konto.Yarn.JobCard.JobCardIndex' where id=1070

--IF NOT exists (select 1 from ErpModule em where em.Id=1066)
--begin
--insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
--           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
--           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
--           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
--		   values(1066,304,'Job Sales',6,null,null,0,
--		   null,null,'Job Sales','Konto.Trading.JobSales.JobSalesIndex','Konto.Trading',
--		   null,null,'Job Sales',1,'/Konto.Wpf;component/MenuIcon/tax.png',0,
--		   0,0,0,null,null,1,GETDATE())
--END
-- update dbo.ErpModule set IconPath='/Konto.Wpf;component/MenuIcon/gray.png' where id=1066

IF NOT exists (select 1 from ErpModule em where em.Id=337)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(337,300,'Stock Journal',10,null,null,0,
		   null,null,'Stock Journal','Konto.Shared.Trans.StockJournal.SJIndex','Konto.Shared',
		   null,null,'Stock Journal',1,'/Konto.Wpf;component/MenuIcon/account.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
 update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=337

 IF NOT exists (select 1 from ErpModule em where em.Id=912)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(912,900,'Template Setup',13,null,null,0,
		   null,null,'Template Setup','Konto.Shared.Trans.Template.TemplateIndex','Konto.Shared',
		   null,null,'Template Setup',1,'/Konto.Wpf;component/MenuIcon/setup.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
 update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=912

-- Gst payment Assitant
if NOT exists (select 1 from ErpModule em where em.Id=1073)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(1073,829,'Gst Payment Assist',2,null,null,0,
		   null,null,null,null,null,
		   null,null,'Gst',1,'/Konto.Wpf;component/MenuIcon/tax.png',0,
		   0,0,0,null,null,1,GETDATE())
end

 update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where id=1073

IF NOT exists (select 1 from ErpModule em where em.Id=305)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(305,301,'Purchase Indent/Request',-1,null,null,0,
		   null,null,'Purchase Request','Konto.Shared.Trans.Request.PrIndex','Konto.Shared',
		   null,null,'Purchase Request',1,'/Konto.Wpf;component/MenuIcon/purchase.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),12,1,0,NEWID())
end
update dbo.ErpModule set PackageId=1,Extra2=',1,2,3,4,6,7,9,10,11,' where Id=305


IF NOT exists (select 1 from ErpModule em where em.Id=382)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(382,301,'Indent/Request Approval',0,null,null,0,
		   null,null,'Request Approval','Konto.Shared.Trans.Request.ReqApproveIndex','Konto.Shared',
		   null,null,'Request Approval',1,'/Konto.Wpf;component/MenuIcon/purchase.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),12,1,0,NEWID())
end


 
update dbo.ErpModule set PackageId=1 ,Extra2=',1,2,3,4,6,7,9,10,11,' where Id=382
-- GATE ENTRY

IF NOT exists (select 1 from ErpModule em where em.Id=383)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(383,301,'Gate Entry',3,null,null,0,
		   null,null,'Gate Entry','Konto.Shared.Trans.Gate.GateInwardIndex','Konto.Shared',
		   null,null,'Gate Entry',1,'/Konto.Wpf;component/MenuIcon/purchase.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),12,1,0,NEWID())
end
update dbo.ErpModule set PackageId=1,Extra2=',1,2,3,4,6,7,9,10,11,' where Id=383



if NOT exists (select 1 from ErpModule em where em.Id=1100)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate,
		   ImageIndex,IsActive,IsDeleted,RowId)
		   values(1100,0,'Apparel',3,null,null,1,
		   null,null,null,null,null,
		   null,null,null,1,'/Konto.Wpf;component/MenuIcon/transaction.png',0,
		   0,0,0,null,null,1,GETDATE(),19,1,0,NEWID())
end

update dbo.ErpModule set PackageId=1,Extra2=',1,' where Id=1100

IF NOT exists (select 1 from ErpModule em where em.Id=1101)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],IsActive,IsDeleted,RowId)
		   values(1101,1100,'BOM',0,null,null,1,
		   null,null,'BOM','Konto.Apparel.BOM.BOMIndex','Konto.Apparel',
		   null,null,'BOM',1,'/Konto.Wpf;component/MenuIcon/setup.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),1,0,NEWID())
end
update dbo.ErpModule set PackageId=1,Extra2=',1,' where Id=1101



-- for accounting edition = 2  textile,pos,accounts,inventory
IF NOT exists (select 1 from ErpModule em where em.Id=384)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],IsActive,IsDeleted,RowId)
		   values(384,300,'BOM',14,null,null,2,
		   null,null,'BOM','Konto.Apparel.BOM.BOMIndex','Konto.Apparel',
		   null,null,'BOM',1,'/Konto.Wpf;component/MenuIcon/setup.png',0,
		   0,0,0,null,',1,2,6,9,',1,GETDATE(),GETDATE(),1,0,NEWID())
end


 IF NOT exists (select 1 from ErpModule em where em.Id=1102)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(1102,1100,'Inward',1,null,null,1,
		   null,null,'Inward','Konto.Apparel.Inw.InwardIndex','Konto.Apparel',
		   null,null,'Inward',1,'/Konto.Wpf;component/MenuIcon/setup.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
update dbo.ErpModule set PackageId=1,Extra2=',1,' where Id=1102

IF NOT exists (select 1 from ErpModule em where em.Id=1103)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(1103,1100,'Quality Control',2,null,null,1,
		   null,null,'QualityControl','Konto.Apparel.Qc.QualityControlIndex','Konto.Apparel',
		   null,null,'QualityControl',1,'/Konto.Wpf;component/MenuIcon/setup.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
update dbo.ErpModule set PackageId=1,Extra2=',1,' where Id=1103

IF NOT exists (select 1 from ErpModule em where em.Id=1104)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(1104,1100,'Barcode',3,null,null,1,
		   null,null,'Barcode','Konto.Apparel.BC.BarcodeIndex','Konto.Apparel',
		   null,null,'Barcode',1,'/Konto.Wpf;component/MenuIcon/setup.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
update dbo.ErpModule set PackageId=1,Extra2=',1,' where Id=1104

IF NOT exists (select 1 from ErpModule em where em.Id=1105)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(1105,1100,'Outward',4,null,null,1,
		   null,null,'Outward','Konto.Apparel.Out.OutwardIndex','Konto.Apparel',
		   null,null,'Outward',1,'/Konto.Wpf;component/MenuIcon/setup.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
update dbo.ErpModule set PackageId=1,Extra2=',1,' where Id=1105

IF NOT exists (select 1 from ErpModule em where em.Id=712)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(712,700,'Change Financial Year',12,null,null,0,
		   null,null,'Change Financial Year','Konto.Shared.Masters.FinYear.ChangeYearView','Konto.Shared',
		   null,null,'Change Financial Year',1,'/Konto.Wpf;component/MenuIcon/tax.png',0,
		   0,0,0,null,null,1,GETDATE())
END
update dbo.ErpModule set Extra2=',1,2,3,4,6,7,9,10,11,' where Id=712


IF NOT exists (select 1 from ErpModule em where em.Id=860) --job work income register
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(860,805,'Job Work Income Register',23,null,null,0,
		   null,null,'Job Work Income Register','Konto.Reporting.JobInc.RepJobIncomeView','Konto.Reporting',
		   null,null,'Job Work Income Register',1,'/Konto.Wpf;component/MenuIcon/tax.png',0,
		   0,0,1,null,',1,2,3,4,6,7,9,10,11,',1,GETDATE())
END


IF NOT exists (select 1 from ErpModule em where em.Id=861) --Gate Entery
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(861,805,'Gate Entry Register',23,null,null,0,
		   null,null,'Job Work Income Register','Konto.Reporting.GE.RepGateEntryView','Konto.Reporting',
		   null,null,'GateEntry Report',1,'/Konto.Wpf;component/MenuIcon/tax.png',0,
		   0,0,0,null,',1,2,3,4,6,7,9,10,11,',1,GETDATE())
END

