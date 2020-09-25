/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

:r .\PostScript\Statelist.sql

go

if NOT exists (select 1 from ErpModule em where em.Id=1)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(1,100,'-',2,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,1,null,null,1,GETDATE())
end
go
if NOT exists (select 1 from ErpModule em where em.Id=2)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(2,150,'-',2,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,1,null,null,1,GETDATE())
end
go
if NOT exists (select 1 from ErpModule em where em.Id=100)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(100,0,'Masters',0,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
go
if NOT exists (select 1 from ErpModule em where em.Id=101)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(101,100,'Location',0,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
go
if NOT exists (select 1 from ErpModule em where em.Id=102)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(102,101,'Country',1,null,null,0,
		   null,null,'country','Konto.Shared.Masters.Country.CountryView','Konto.Shared',
		   null,null,'Country',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
go

if NOT exists (select 1 from ErpModule em where em.Id=103)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(103,101,'State',1,null,null,0,
		   null,null,'State','Konto.Shared.Masters.State.StateIndex','Konto.Shared',
		   null,null,'State',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end