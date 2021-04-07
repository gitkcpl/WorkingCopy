
if NOT exists (select 1 from ErpModule em where em.Id=1200) --Pos
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate,ImageIndex,
		   IsActive,IsDeleted,RowId
		   )
		   values(1200,0,'Pos',3,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,'/Konto.Wpf;component/MenuIcon/master.png',0,
		   0,0,0,null,null,1,GETDATE(),8,1,0,NEWID()) 
end
Go
update dbo.ErpModule set Extra2=',6,' where Id=1200
--package

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.PackageId=6 and em.MenuId=1200)
--INSERT INTO dbo.Menu_Package
--(
--   PackageId,MenuId
--)values (6,1200) 


if NOT exists (select 1 from ErpModule em where em.Id=1201)-- customer Maser
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,
		   IsActive,IsDeleted,RowId)
		   values(1201,1200,'Customer Master',1,null,null,0,
		   null,null,'Customer','Konto.Shared.Masters.PG.CustomerIndex','Konto.Shared',
		   null,null,'Customer Master',1,'/Konto.Wpf;component/MenuIcon/location.png',0,
		   0,0,0,null,',6,',1,GETDATE(),GETDATE(),7,1,0,NEWID())
end
Go

if NOT exists (select 1 from ErpModule em where em.Id=1202)-- Pay Mode
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,
		   IsActive,IsDeleted,RowId)
		   values(1202,1200,'Pay Mode',2,null,null,0,
		   null,null,'Pay Mode','Konto.Shared.Masters.PayMode.PayModeIndex','Konto.Shared',
		   null,null,'Pay Mode',1,'/Konto.Wpf;component/MenuIcon/location.png',0,
		   0,0,0,null,',6,',1,GETDATE(),GETDATE(),7,1,0,NEWID())
end
Go


--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.PackageId=6 and em.MenuId=1201)
--INSERT INTO dbo.Menu_Package
--(
--   PackageId,MenuId
--)values (6,1201) 

		
--if NOT exists (select 1 from ErpModule em where em.Id=1220)
--begin
--insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
--           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
--           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
--           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate,
--		   IsActive,IsDeleted,RowId)
--		   values(1220,1200,'-',2,null,null,0,
--		   null,null,null,null,null,
--		   null,null,null,1,null,0,
--		   0,0,1,null,null,1,GETDATE(),1,0,NEWID())
--end
--go

--update dbo.ErpModule set Extra2=',6,' where Id=1220

--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.PackageId=6 and em.MenuId=1220)
--INSERT INTO dbo.Menu_Package
--(
--   PackageId,MenuId
--)values (6,1220)


if NOT exists (select 1 from ErpModule em where em.Id=1221)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(1221,1200,'Purchase Invoice',20,null,null,0,
		   null,null,'Purchase Invoice','Konto.Pos.Purchase.PurchaseIndex','Konto.Pos',
		   null,null,'Purchase Invoice',1,'/Konto.Wpf;component/MenuIcon/opening.png',0,
		   0,0,0,null,null,1,GETDATE(),GETDATE(),9,1,0,NEWID())
end
update dbo.ErpModule set Extra2=',6,',[IsSeprator]=1 where Id=1221



--IF NOT EXISTS (select 1 from dbo.Menu_Package em where em.PackageId=6 and em.MenuId=1221)
--INSERT INTO dbo.Menu_Package
--(
--   PackageId,MenuId
--)values (6,1221)


if NOT exists (select 1 from ErpModule em where em.Id=1222)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(1222,1200,'Stock Transfer',21,null,null,0,
		   null,null,'Stock Transfer','Konto.Shared.Trans.ST.StIndex','Konto.Shared',
		   null,null,'Stock Transfer',1,'/Konto.Wpf;component/MenuIcon/opening.png',0,
		   0,0,0,null,',6,',1,GETDATE(),GETDATE(),9,1,0,NEWID())
end

-- purchase return
if NOT exists (select 1 from ErpModule em where em.Id=1224)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(1224,1200,'Purchase Return',22,null,null,0,
		   null,null,'Purchase Return','Konto.Pos.PR.PRIndex','Konto.Pos',
		   null,null,'Purchase Return',1,'/Konto.Wpf;component/MenuIcon/opening.png',0,
		   0,0,0,null,',6,',1,GETDATE(),GETDATE(),9,1,0,NEWID())
end

if NOT exists (select 1 from ErpModule em where em.Id=1223)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(1223,1200,'Pos Invoice',23,null,null,0,
		   null,null,'Pos','Konto.Pos.Sales.SalesIndex','Konto.Pos',
		   null,null,'Pos Invoice',1,'/Konto.Wpf;component/MenuIcon/opening.png',0,
		   0,0,1,null,',6,',1,GETDATE(),GETDATE(),9,1,0,NEWID())
end

update dbo.ErpModule set Extra2=',6,',[IsSeprator]=1, OrderIndex=23 where Id=1223

if NOT exists (select 1 from ErpModule em where em.Id=1225)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(1225,1200,'Sales Return',24,null,null,0,
		   null,null,'Sales Return','Konto.Pos.SR.SRIndex','Konto.Pos',
		   null,null,'Sales Return',1,'/Konto.Wpf;component/MenuIcon/opening.png',0,
		   0,0,0,null,',6,',1,GETDATE(),GETDATE(),9,1,0,NEWID())
end


if NOT exists (select 1 from ErpModule em where em.Id=1240)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate],ImageIndex,IsActive,IsDeleted,RowId)
		   values(1240,1200,'Rate Update',40,null,null,0,
		   null,null,'Rate Update','Konto.Shared.Masters.Item.ItemMultiEditView','Konto.Shared',
		   null,null,'Rate Update',1,'/Konto.Wpf;component/MenuIcon/opening.png',0,
		   0,0,1,null,',6,',1,GETDATE(),GETDATE(),9,1,0,NEWID())
end