if NOT exists (select 1 from ErpModule em where em.Id=100)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(100,0,'Masters',1,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
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
if NOT exists (select 1 from ErpModule em where em.Id=101)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(101,100,'Location',1,null,null,0,
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
		   values(103,101,'State',2,null,null,0,
		   null,null,'State','Konto.Shared.Masters.State.StateIndex','Konto.Shared',
		   null,null,'State',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=104)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(104,101,'City',3,null,null,0,
		   null,null,'City','Konto.Shared.Masters.City.CityIndex','Konto.Shared',
		   null,null,'City',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=105)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(105,101,'Area',4,null,null,0,
		   null,null,'Area','Konto.Shared.Masters.Area.AreaIndex','Konto.Shared',
		   null,null,'Area',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end


if NOT exists (select 1 from ErpModule em where em.Id=106)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(106,100,'Product Section',2,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
if NOT exists (select 1 from ErpModule em where em.Id=149)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(149,106,'-',11,null,null,0,
		   null,null,'-',null,null,
		   null,null,'-',1,null,0,
		   0,0,1,null,null,1,GETDATE(),GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=107)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(107,106,'Product Type',1,null,null,0,
		   null,null,'ProductType','Konto.Shared.Masters.ProductType.PTypeIndex','Konto.Shared',
		   null,null,'Product Type',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
 

if NOT exists (select 1 from ErpModule em where em.Id=108)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(108,106,'Brand',3,null,null,0,
		   null,null,'Brand','Konto.Shared.Masters.Brand.BrandIndex','Konto.Shared',
		   null,null,'Brand',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=109)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(109,106,'Unit',2,null,null,0,
		   null,null,'Unit','Konto.Shared.Masters.Uom.UomIndex','Konto.Shared',
		   null,null,'Unit',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=110)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(110,106,'Group',4,null,null,0,
		   null,null,'Group','Konto.Shared.Masters.ProductGroup.GroupIndex','Konto.Shared',
		   null,null,'Group',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=111)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(111,106,'Sub Group',5,null,null,0,
		   null,null,'Sub Group','Konto.Shared.Masters.SubGroup.SubGroupIndex','Konto.Shared',
		   null,null,'Sub Group',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=112)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(112,106,'Size Master',6,null,null,0,
		   null,null,'Size','Konto.Shared.Masters.Size.SizeIndex','Konto.Shared',
		   null,null,'Size',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=113)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(113,106,'Color Master',7,null,null,0,
		   null,null,'Color','Konto.Shared.Masters.Color.ColorIndex','Konto.Shared',
		   null,null,'Color',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=114)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(114,106,'Category Master',8,null,null,0,
		   null,null,'Category','Konto.Shared.Masters.Category.CategoryIndex','Konto.Shared',
		   null,null,'Category',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=115)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(115,106,'Style Master',9,null,null,0,
		   null,null,'Style','Konto.Shared.Masters.Style.StyleIndex','Konto.Shared',
		   null,null,'Style',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=116)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(116,106,'-',10,null,null,0,
		   null,null,'-',null,null,
		   null,null,'-',1,null,0,
		   0,0,1,null,null,1,GETDATE(),GETDATE())
end

IF NOT exists (select 1 from ErpModule em where em.Id=117)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(117,106,'GST Slab',10,null,null,0,
		   null,null,'GST SLAB','Konto.Shared.Masters.Tax.TaxIndex','Konto.Shared',
		   null,null,'GST SLAB',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

IF NOT exists (select 1 from ErpModule em where em.Id=118)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(118,106,'Product Master',13,null,null,0,
		   null,null,'Product','Konto.Shared.Masters.Item.ProductIndex','Konto.Shared',
		   null,null,'Product',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=120)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(120,140,'Salesman',3,null,null,0,
		   null,null,'Salesman','Konto.Shared.Masters.Emp.EmpIndex','Konto.Shared',
		   null,null,'Salesman',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end


-- Ledger Group seprator
if NOT exists (select 1 from ErpModule em where em.Id=121)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(121,100,'-',4,null,null,0,
		   null,null,'-',Null,NUll,
		   null,null,'-',1,null,0,
		   0,0,1,null,null,1,GETDATE(),GETDATE())
end

-- Ledger Group
if NOT exists (select 1 from ErpModule em where em.Id=122)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(122,140,'Ledger Group',1,null,null,0,
		   null,null,'Ledger Group','Konto.Shared.Masters.LedgerGroup.AcGroupIndex','Konto.Shared',
		   null,null,'Ledger Group',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

IF NOT exists (select 1 from ErpModule em where em.Id=123)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(123,140,'Party Group',2,null,null,0,
		   null,null,'Party Group','Konto.Shared.Masters.PG.PgIndex','Konto.Shared',
		   null,null,'Party Group',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

IF NOT exists (select 1 from ErpModule em where em.Id=124)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(124,140,'Account Master',4,null,null,0,
		   null,null,'Account Master','Konto.Shared.Masters.Acc.AccIndex','Konto.Shared',
		   null,null,'Account Master',7,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

---seprator
if NOT exists (select 1 from ErpModule em where em.Id=125)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(125,100,'-',8,null,null,0,
		   null,null,'-',Null,NUll,
		   null,null,'-',1,null,0,
		   0,0,1,null,null,1,GETDATE(),GETDATE())
end

IF NOT exists (select 1 from ErpModule em where em.Id=126)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(126,100,'Voucher Type',5,null,null,0,
		   null,null,'Voucher Type','Konto.Shared.Masters.VType.VTypeIndex','Konto.Shared',
		   null,null,'Voucher Type',9,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

IF NOT exists (select 1 from ErpModule em where em.Id=127)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(127,100,'Voucher',6,null,null,0,
		   null,null,'Voucher','Konto.Shared.Masters.Voucher.VoucherIndex','Konto.Shared',
		   null,null,'Voucher',10,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
update dbo.ErpModule set AssemblyName='Konto.Shared.Masters.Voucher.VoucherIndex' where id=127
--seprator
if NOT exists (select 1 from ErpModule em where em.Id=128)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(128,100,'-',11,null,null,0,
		   null,null,'-',Null,NUll,
		   null,null,'-',1,null,0,
		   0,0,1,null,null,1,GETDATE(),GETDATE())
end

IF NOT exists (select 1 from ErpModule em where em.Id=129)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(129,100,'Narration',7,null,null,0,
		   null,null,'Narration','Konto.Shared.Masters.Nar.NarIndex','Konto.Shared',
		   null,null,'Narration',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

IF NOT exists (select 1 from ErpModule em where em.Id=130)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(130,101,'Store',7,null,null,0,
		   null,null,'Store','Konto.Shared.Masters.Store.StoreIndex','Konto.Shared',
		   null,null,'Store',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=131)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(131,140,'Haste',5,null,null,0,
		   null,null,'Haste','Konto.Shared.Masters.Haste.HasteIndex','Konto.Shared',
		   null,null,'Haste',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=132)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(132,106,'Design Master',14,null,null,0,
		   null,null,'Design Master','Konto.Shared.Masters.Design.DesignIndex','Konto.Shared',
		   null,null,'Design',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
go


if NOT exists (select 1 from ErpModule em where em.Id=133)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(133,106,'Catalog',12,null,null,0,
		   null,null,'catalog','Konto.Shared.Masters.Catalog.CatalogIndex','Konto.Shared',
		   null,null,'Catalog',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
go


if NOT exists (select 1 from ErpModule em where em.Id=134)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(134,100,'Machine Master',1,null,null,0,
		   null,null,'Machine Master','Konto.Shared.Masters.MachineMaster.MachineIndex','Konto.Shared',
		   null,null,'Machine',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
go


if NOT exists (select 1 from ErpModule em where em.Id=135)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(135,100,'Grade Master',1,null,null,0,
		   null,null,'Grade Master','Konto.Shared.Masters.Grade.GradeIndex','Konto.Shared',
		   null,null,'Grade',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
go


if NOT exists (select 1 from ErpModule em where em.Id=136)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(136,100,'PackingType Master',1,null,null,0,
		   null,null,'PackingType Master','Konto.Shared.Masters.PackingType.PackingTypeIndex','Konto.Shared',
		   null,null,'PackingType',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
go 

if NOT exists (select 1 from ErpModule em where em.Id=138)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(138,900,'Company',1,null,null,0,
		   null,null,'Company','Konto.Shared.Masters.Comp.CompIndex','Konto.Shared',
		   null,null,'Company',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
go
if NOT exists (select 1 from ErpModule em where em.Id=139)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(139,141,'Sale/Purchase Op Bill',1,null,null,0,
		   null,null,'Sale/Purchase Op Bill','Konto.Shared.OpBill.SPOpBillIndex','Konto.Shared',
		   null,null,'Sale/Purchase Op Bill',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
go
 
if NOT exists (select 1 from ErpModule em where em.Id=300)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(300,0,'Transaction',2,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=301)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(301,300,'Purchase',0,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=302)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(302,300,'Account',5,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end


if NOT exists (select 1 from ErpModule em where em.Id=303)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(303,300,'Mill',2,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end


if NOT exists (select 1 from ErpModule em where em.Id=304)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(304,300,'Job',3,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

IF NOT exists (select 1 from ErpModule em where em.Id=306)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(306,301,'Purchase Order',5,null,null,0,
		   null,null,'Purchase Order','Konto.Shared.Trans.Po.PoIndex','Konto.Shared',
		   null,null,'Purchase Order',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

IF NOT exists (select 1 from ErpModule em where em.Id=307)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(307,301,'PO Approval',6,null,null,0,
		   null,null,'PO Approval','Konto.Shared.Trans.Po.PoApproveIndex','Konto.Shared',
		   null,null,'PO Approval',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

IF NOT exists (select 1 from ErpModule em where em.Id=308)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(308,301,'Inward (GRN)',7,null,null,0,
		   null,null,'Challan','Konto.Shared.Trans.GRN.GRNIndex','Konto.Shared',
		   null,null,'Inward (GRN)',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

IF NOT exists (select 1 from ErpModule em where em.Id=309)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(309,301,'Purchase Bill',8,null,null,0,
		   null,null,'BillMain','Konto.Shared.Trans.PInvoice.PInvoiceIndex','Konto.Shared',
		   null,null,'Purchase Bill',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end


IF NOT exists (select 1 from ErpModule em where em.Id=310)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(310,303,'Mill Issue',1,null,null,0,
		   null,null,'Mill Issue','Konto.Shared.Trans.SalesChallan.ScIndex','Konto.Shared',
		   null,null,'Mill Issue',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
IF NOT exists (select 1 from ErpModule em where em.Id=311)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(311,303,'Mill Issue Lot No',8,null,null,0,
		   null,null,'Mill Issue Lot No','Konto.Shared.Trans.SalesChallan.MILotIndex','Konto.Shared',
		   null,null,'Mill Issue Lot No',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
IF NOT exists (select 1 from ErpModule em where em.Id=312)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(312,301,'Gray Purchase',2,null,null,0,
		   null,null,'Gray Purchase','Konto.Trading.GP.GPIndex','Konto.Trading',
		   null,null,'Gray Purchase',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

IF NOT exists (select 1 from ErpModule em where em.Id=314)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(314,303,'Mill Programm',2,null,null,0,
		   null,null,'Mill Programm',NULL,NULL,
		   null,null,'Mill Programm',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end


IF NOT exists (select 1 from ErpModule em where em.Id=317)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(317,303,'Cutting/Folding',6,null,null,0,
		   null,null,'Cutting','Konto.Trading.Cutting.CuttingIndex','Konto.Trading',
		   null,null,'Cutting/Folding',1,null,0, 0,0,0,null,null,1,GETDATE(),GETDATE())
end


IF NOT exists (select 1 from ErpModule em where em.Id=319)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(319,304,'Job Receipt',3,null,null,0,
		   null,null,'Job Receipt','Konto.Trading.JobReceipt.JobReceiptIndex','Konto.Trading',
		   null,null,'Job Receipt',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

IF NOT exists (select 1 from ErpModule em where em.Id=321)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(321,302,'Journal Voucher',4,null,null,0,
		   null,null,'BillMain','Konto.Shared.Account.Jv.JvIndex','Konto.Shared',
		   null,null,'Journal Voucher',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end


IF NOT exists (select 1 from ErpModule em where em.Id=322)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(322,302,'Receipt Voucher',1,null,null,0,
		   null,null,'BillMain','Konto.Shared.Account.Receipt.ReceiptIndex','Konto.Shared',
		   null,null,'Receipt Voucher',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end


IF NOT exists (select 1 from ErpModule em where em.Id=324)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(324,301,'General Expense',1,null,null,0,
		   null,null,'General Expense','Konto.Shared.Account.GenExpense.GenExpIndex','Konto.Shared',
		   null,null,'General Expense',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

IF NOT exists (select 1 from ErpModule em where em.Id=325)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(325,302,'Cr/Dr Note',3,null,null,0,
		   null,null,'Cr/Dr Note','Konto.Shared.Account.DRCRNote.DRCRNoteIndex','Konto.Shared',
		   null,null,'Cr/Dr Note',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

IF NOT exists (select 1 from ErpModule em where em.Id=326)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(326,302,'Petty Cash',6,null,null,0,
		   null,null,'Petty Cash',NULL,NULL,
		   null,null,'Petty Cash',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
IF NOT exists (select 1 from ErpModule em where em.Id=327)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(327,302,'Fast Cash',7,null,null,0,
		   null,null,'Fast Cash',NULL,NULL,
		   null,null,'Fast Cash',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end


IF NOT exists (select 1 from ErpModule em where em.Id=328)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(328,302,'Payment Voucher',2,null,null,0,
		   null,null,'BillMain','Konto.Shared.Account.Payment.PaymentIndex','Konto.Shared',
		   null,null,'Payment Voucher',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

IF NOT exists (select 1 from ErpModule em where em.Id=333)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(333,301,'Grey Purchase Return',3,null,null,0,
		   null,null,'Challan',null,null,
		   null,null,'Gery purchase Return',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
IF NOT exists (select 1 from ErpModule em where em.Id=334)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(334,301,'Return Challan',10,null,null,0,
		   null,null,'Challan',null,null,
		   null,null,'Return Challan',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

--IF NOT exists (select 1 from ErpModule em where em.Id=328)
--begin
--insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
--           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
--           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
--           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
--		   [CreateDate],[ModifyDate])
--		   values(328,302,'Payment Voucher',1,null,null,0,
--		   null,null,'BillMain','Konto.Shared.Account.Payment.PaymentIndex','Konto.Shared',
--		   null,null,'Payment Voucher',1,null,0,
--		   0,0,0,null,null,1,GETDATE(),GETDATE())
--end

--IF NOT exists (select 1 from ErpModule em where em.Id=328)
--begin
--insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
--           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
--           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
--           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
--		   [CreateDate],[ModifyDate])
--		   values(328,302,'Payment Voucher',1,null,null,0,
--		   null,null,'BillMain','Konto.Shared.Account.Payment.PaymentIndex','Konto.Shared',
--		   null,null,'Payment Voucher',1,null,0,
--		   0,0,0,null,null,1,GETDATE(),GETDATE())
--end

--IF NOT exists (select 1 from ErpModule em where em.Id=328)
--begin
--insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
--           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
--           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
--           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
--		   [CreateDate],[ModifyDate])
--		   values(328,302,'Payment Voucher',1,null,null,0,
--		   null,null,'BillMain','Konto.Shared.Account.Payment.PaymentIndex','Konto.Shared',
--		   null,null,'Payment Voucher',1,null,0,
--		   0,0,0,null,null,1,GETDATE(),GETDATE())
--end

--IF NOT exists (select 1 from ErpModule em where em.Id=328)
--begin
--insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
--           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
--           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
--           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
--		   [CreateDate],[ModifyDate])
--		   values(328,302,'Payment Voucher',1,null,null,0,
--		   null,null,'BillMain','Konto.Shared.Account.Payment.PaymentIndex','Konto.Shared',
--		   null,null,'Payment Voucher',1,null,0,
--		   0,0,0,null,null,1,GETDATE(),GETDATE())
--end


if NOT exists (select 1 from ErpModule em where em.Id=351)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(351,300,'Sales',4,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

IF NOT exists (select 1 from ErpModule em where em.Id=352)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(352,351,'Sales Order',1,null,null,0,
		   null,null,'Sales Order','Konto.Shared.Trans.SO.SoIndex','Konto.Shared',
		   null,null,'Sales Order',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

IF NOT exists (select 1 from ErpModule em where em.Id=353)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(353,351,'Sale Approval',2,null,null,0,
		   null,null,'Sale Approval','Konto.Shared.Trans.Po.PoApproveIndex','Konto.Shared',
		   null,null,'Sale Approval',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end


IF NOT exists (select 1 from ErpModule em where em.Id=355)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(355,351,'Sales Challan',3,null,null,0,
		   null,null,'Outward','Konto.Shared.Trans.SalesChallan.ScIndex','Konto.Shared',
		   null,null,'Outward',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end


IF NOT exists (select 1 from ErpModule em where em.Id=356)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(356,303,'MillReceipt',3,null,null,0,
		   null,null,'Mill Receipt','Konto.Trading.MillReceipt.MillReceiptIndex','Konto.Trading',
		   null,null,'Mill Receipt',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end


IF NOT exists (select 1 from ErpModule em where em.Id=358)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(358,351,'Sale Invoice',4,null,null,0,
		   null,null,'BillMain','Konto.Shared.Trans.SInvoice.SInvoiceIndex','Konto.Shared',
		   null,null,'Sale Invoice',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

IF NOT exists (select 1 from ErpModule em where em.Id=359)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(359,304,'Job Issue',1,null,null,0,
		   null,null,'Challan','Konto.Shared.Trans.SalesChallan.ScIndex','Konto.Shared',
		   null,null,'Job Issue',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end
IF NOT exists (select 1 from ErpModule em where em.Id=360)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(360,304,'Job Bill',3,null,null,0,
		   null,null,'BillMain','Konto.Trading.JobReceipt.JobReceiptIndex','Konto.Trading',
		   null,null,'Job Bill',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end


IF NOT exists (select 1 from ErpModule em where em.Id=361)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(361,303,'Mill Return',5,null,null,0,
		   null,null,'Challan','Konto.Shared.Trans.GRN.GRNIndex','Konto.Shared',
		   null,null,'MillReturn',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end


IF NOT exists (select 1 from ErpModule em where em.Id=362)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(362,351,'Sale Return',6,null,null,0,
		   null,null,'BillMain','Konto.Shared.Trans.SReturn.SReturnIndex','Konto.Shared',
		   null,null,'Sale Return',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

IF NOT exists (select 1 from ErpModule em where em.Id=363)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(363,301,'Purchase Return',11,null,null,0,
		   null,null,'BillMain','Konto.Shared.Trans.PReturn.PReturnIndex','Konto.Shared',
		   null,null,'Purchase Return',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=364)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(364,378,'Store Issue',7,null,null,0,
		   null,null,'StoreIssue','Konto.Shared.Trans.StoreIssue.StoreIssueIndex','Konto.Shared',
		   null,null,'Store Issue',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=365)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(365,300,'Design Mapping',6,null,null,0,
		   null,null,'Design Mapping','Konto.Trading.DesignMapping.DesignMappingIndex','Konto.Trading',
		   null,null,'Design Mapping',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=366)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(366,100,'RefBank',8,null,null,0,
		   null,null,'RefBank','Konto.Shared.Masters.RefBank.RefBankIndex','Konto.Shared',
		   null,null,'RefBank',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 
if NOT exists (select 1 from ErpModule em where em.Id=367)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(367,378,'Beam Production',8,null,null,0,
		   null,null,'Beam Production','Konto.Weaves.BeamProduction.BeamProdIndex','Konto.Weaves',
		   null,null,'Beam Prod',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=368)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(368,378,'Beam Loading',9,null,null,0,
		   null,null,'Beam Loading','Konto.Weaves.BeamLoading.BeamLoadingListView','Konto.Weaves',
		   null,null,'Beam Loading',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end


if NOT exists (select 1 from ErpModule em where em.Id=369)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(369,378,'Taka Production',10,null,null,0,
		   null,null,'Taka Production','Konto.Weaves.TakaProduction.TakaProdIndex','Konto.Weaves',
		   null,null,'Taka Production',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 
if NOT exists (select 1 from ErpModule em where em.Id=370)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(370,300,'Batch Master',11,null,null,0,
		   null,null,'Batch Master','Konto.Yarn.BatchMaster.BatchIndex','Konto.Yarn',
		   null,null,'Batch Master',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=371)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(371,300,'Yarn Production',12,null,null,0,
		   null,null,'Yarn Production (Packing List)','Konto.Yarn.YarnProduction.YarnProdIndex','Konto.Yarn',
		   null,null,'Yarn',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end


if NOT exists (select 1 from ErpModule em where em.Id=372)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(372,300,'Taka Opening',13,null,null,0,
		   null,null,'Taka Opening','Konto.Weaves.TakaOp.TakaOpIndex','Konto.Weaves',
		   null,null,'Taka Opening',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=373)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(373,301,'Gray Order',1,null,null,0,
		   null,null,'Order','Konto.Trading.GreyOrder.GreyOrderIndex','Konto.Trading',
		   null,null,'Gray order',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=374)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(374,378,'Store Issue Return',14,null,null,0,
		   null,null,'challan','Konto.Shared.Trans.StoreIssueReturn.SIReturnIndex','Konto.Shared',
		   null,null,'Store Issue Return',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

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
		   null,null,'Return Challan',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
if NOT exists (select 1 from ErpModule em where em.Id=376)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(376,302,'BRS',5,null,null,0,
		   null,null,'challan',null,null,
		   null,null,'BRS',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

IF NOT exists (select 1 from ErpModule em where em.Id=377)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(377,303,'Mill Receipt Voucher',4,null,null,1,
		   null,null,'BillMain','Konto.Trading.MillReceipt.MillReceiptIndex','Konto.Trading',
		   null,null,'Mill Receipt Voucher',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

IF NOT exists (select 1 from ErpModule em where em.Id=378)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(378,300,'Production',2,null,null,1,
		   null,null,null,null,null,
		   null,null,'Production',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

--- Tools
if NOT exists (select 1 from ErpModule em where em.Id=700)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(700,0,'Tools',10,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=701)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(701,700,'Backup / Restore',1,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=702)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(702,700,'Change Password',2,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

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
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=705)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(705,700,'Account Merge',5,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=706)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(706,700,'Voucher Reindex',6,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

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
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=709)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(709,700,'Cash Adjustment',9,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

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
		   values(711,700,'Bulk Delete',11,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end


--Reports
if NOT exists (select 1 from ErpModule em where em.Id=800)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(800,0,'Reports',10,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
if NOT exists (select 1 from ErpModule em where em.Id=801)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(801,800,'Account Ledger',1,null,null,0,
		   null,null,'Ledger','Konto.Shared.Account.LedgerReport.LedgerMainView','Konto.Shared',
		   null,null,'Account Ledger',1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=802)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(802,800,'Outstanding',2,null,null,0,
		   null,null,'BillMain','Konto.Shared.Outstanding.OutsMainView','Konto.Shared',
		     null,null,'Outstanding Report',1,null,0,
		   0,0,0,null,null,1,GETDATE()) 
end

if NOT exists (select 1 from ErpModule em where em.Id=803)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(803,800,'Trial Balance',4,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=804)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(804,800,'Balance Sheet',5,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 
if NOT exists (select 1 from ErpModule em where em.Id=805)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(805,800,'Inventory Register',6,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=806)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(806,805,'GRN',1,null,null,0,
		   null,null,null,'Konto.Reporting.Para.ChlPara.ChlParaMainView','Konto.Reporting',
		   null,null,'Inward/Outward Reg',1,null,0,
		   0,0,0,null,null,1,GETDATE())
END

update dbo.ErpModule set AssemblyName='Konto.Reporting.Para.ChlPara.ChlParaMainView' ,MainAssembly='Konto.Reporting',title='Inward/Outward Reg' where id=806

if NOT exists (select 1 from ErpModule em where em.Id=807)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(807,805,'Purchase',2,null,null,0,
		   null,null,null,'Konto.Reporting.Para.ParaMainView','Konto.Reporting',
		   null,null,'Purchase Register',1,null,0,
		   0,0,0,null,null,1,GETDATE())

end

UPDATE dbo.ErpModule SET AssemblyName='Konto.Reporting.Para.ParaMainView',MainAssembly='Konto.Reporting',Title='Purchase Register' WHERE id=807

if NOT exists (select 1 from ErpModule em where em.Id=808)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(808,805,'Grey Purchse',3,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=809)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(809,805,'Grey Purchase Return',4,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=810)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(810,805,'Purchase Return Challan',5,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=811)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(811,805,'Purchase Return',6,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

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
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=814)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(814,805,'Sales',9,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=815)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(815,805,'Sales Return Challan',10,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

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
		   values(818,805,'Grey Issue',13,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=819)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(819,805,'Mill Receipt Challan',14,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=820)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(820,805,'Mill Receipt Invoice',15,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

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
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=824)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(824,805,'Job Receipt Challan',19,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=825)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(825,805,'Job Receipt Voucher',20,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=826)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(826,805,'Cutting / Folding',21,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

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

if NOT exists (select 1 from ErpModule em where em.Id=828)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(828,800,'Order Register',8,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=829)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(829,800,'Tax Register',9,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=830)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(830,829,'GSTR-1',1,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 if NOT exists (select 1 from ErpModule em where em.Id=831)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(831,829,'GSTR-2',2,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 if NOT exists (select 1 from ErpModule em where em.Id=832)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(832,829,'GSTR-3B',3,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 if NOT exists (select 1 from ErpModule em where em.Id=833)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(833,829,'GSTR-4A (Job Work)',4,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 if NOT exists (select 1 from ErpModule em where em.Id=834)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(834,829,'GSTR-2 Reconcile',5,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
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
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 if NOT exists (select 1 from ErpModule em where em.Id=837)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(837,829,'TCS',8,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 
 if NOT exists (select 1 from ErpModule em where em.Id=838)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(838,800,'Production',8,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 
 if NOT exists (select 1 from ErpModule em where em.Id=839)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(839,838,'Beam Register',1,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 
 if NOT exists (select 1 from ErpModule em where em.Id=840)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(840,838,'Taka Prod Register',2,null,null,0,
		   null,null,null,'Konto.Weaves.Para.ParaMainView','Konto.Weaves',
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 
 if NOT exists (select 1 from ErpModule em where em.Id=841)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(841,838,'Salary / Patia register',3,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 
 if NOT exists (select 1 from ErpModule em where em.Id=842)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(842,838,'Store Issue',4,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 
 if NOT exists (select 1 from ErpModule em where em.Id=843)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(843,838,'Store Return',5,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 
 if NOT exists (select 1 from ErpModule em where em.Id=844)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(844,838,'Taka Tracker',6,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end
 
--- setup
if NOT exists (select 1 from ErpModule em where em.Id=900)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],CreateDate)
		   values(900,0,'Setup',10,null,null,0,
		   null,null,null,null,null,
		   null,null,null,1,null,0,
		   0,0,0,null,null,1,GETDATE())
end

IF NOT exists (select 1 from ErpModule em where em.Id=901)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(901,900,'User Role',2,null,null,0,
		   null,null,'User Role','Konto.Shared.Security.RoleIndex','Konto.Shared',
		   null,null,'User Role',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

IF NOT exists (select 1 from ErpModule em where em.Id=902)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(902,900,'User Master',4,null,null,0,
		   null,null,'User Master','Konto.Shared.Security.UserIndex','Konto.Shared',
		   null,null,'User Master',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end 

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
		   null,null,'Menu Setup',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end 
 
IF NOT exists (select 1 from ErpModule em where em.Id=905)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(905,900,'Report Setup',7,null,null,0,
		   null,null,'Report Setup',null,null,
		   null,null,'Report Setup',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end 
 
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

IF NOT exists (select 1 from ErpModule em where em.Id=1051)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(1051,101,'Division',6,null,null,0,
		   null,null,'Division','Konto.Shared.Masters.Div.DivIndex','Konto.Shared',
		   null,null,'Division',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

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
		   null,null,'Branch',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=1057)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(1057,302,'Ledger Report',7,null,null,0,
		   null,null,'BillMain','Konto.Shared.Account.LedgerReport.LedgerMainView','Konto.Shared',
		   null,null,'Ledger Report',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=1058)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(1058,800,'Stock Report',3,null,null,0,
		   null,null,'BillMain','Konto.Shared.StockReport.StockMainView','Konto.Shared',
		   null,null,'Stock Report',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end  

if NOT exists (select 1 from ErpModule em where em.Id=1059)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(1059,300,'Gst Report',17,null,null,0,
		   null,null,'BillMain','Konto.Shared.GstReport.GstMainView','Konto.Shared',
		   null,null,'Gst Report',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end

if NOT exists (select 1 from ErpModule em where em.Id=1060)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(1060,300,'Outstanding Report',17,null,null,0,
		   null,null,'BillMain','Konto.Shared.Outstanding.OutsMainView','Konto.Shared',
		   null,null,'Outstanding Report',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end 


if NOT exists (select 1 from ErpModule em where em.Id=140)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(140,100,'Account Section',3,null,null,0,
		   null,null,'AccountSection',null,null,
		   null,null,'AccountSection',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end  
if NOT exists (select 1 from ErpModule em where em.Id=141)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(141,100,'Opening',4,null,null,0,
		   null,null,'Opening',null,null,
		   null,null,'Opening',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end  
if NOT exists (select 1 from ErpModule em where em.Id=142)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(142,141,'Op. Account Balance',2,null,null,0,
		   null,null,'Op. Account Balance',null,null,
		   null,null,'Op. Account Balance',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end  
if NOT exists (select 1 from ErpModule em where em.Id=143)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(143,141,'Op. Grey Stock',3,null,null,0,
		   null,null,'Op. Grey Stock',null,null,
		   null,null,'Op. Grey Stock',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end  
if NOT exists (select 1 from ErpModule em where em.Id=144)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(144,141,'Op. Finish Stock',4,null,null,0,
		   null,null,'Op. Finish Stock',null,null,
		   null,null,'Op. Finish Stock',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end  
if NOT exists (select 1 from ErpModule em where em.Id=145)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(145,141,'Op. Mill Stock',5,null,null,0,
		   null,null,'Op. Mill Stock',null,null,
		   null,null,'Op. Mill Stock',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end  
if NOT exists (select 1 from ErpModule em where em.Id=146)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(146,141,'Op. Job Issue',6,null,null,0,
		   null,null,'Op. Job Issue',null,null,
		   null,null,'Op. Job Issue',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end  
if NOT exists (select 1 from ErpModule em where em.Id=147)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(147,141,'Part Payment',7,null,null,0,
		   null,null,'Part Payment',null,null,
		   null,null,'Part Payment',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end  

if NOT exists (select 1 from ErpModule em where em.Id=148)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(148,141,'Opening Cheque',8,null,null,0,
		   null,null,'Opening Cheque',null,null,
		   null,null,'Opening Cheque',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end  

if NOT exists (select 1 from ErpModule em where em.Id=150)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(150,141,'Opening Taka Stock',8,null,null,0,
		   null,null,'Opening Taka Stock',null,null,
		   null,null,'Opening Taka Stock',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end  
if NOT exists (select 1 from ErpModule em where em.Id=151)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(151,141,'Opening Beam Stock',8,null,null,0,
		   null,null,'Opening Beam Stock',null,null,
		   null,null,'Opening Beam Stock',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end  
 
if NOT exists (select 1 from ErpModule em where em.Id=152)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(152,141,'Opening Stock',8,null,null,0,
		   null,null,'Opening Stock',null,null,
		   null,null,'Opening Stock',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end  

if NOT exists (select 1 from ErpModule em where em.Id=153)
begin
insert into ErpModule ([Id],[ParentId],[ModuleDesc],[OrderIndex],[LinkButton],[ShortCutKey],[PackageId]
           ,[DefaultReport],[DefaultLayout],[TableName],[AssemblyName],[MainAssembly]
           ,[ListAssembly],[MDI],[Title],[Visible],[IconPath],[CheckRight]
           ,[VisibleOnDashBoard],[VisibleOnSideBar],[IsSeprator],[Extra1],[Extra2],[Offline],
		   [CreateDate],[ModifyDate])
		   values(153,106,'Warp Item',8,null,null,0,
		   null,null,'Warp Item','Konto.Shared.Masters.WarpItem.WarpItemIndex','Konto.Shared',
		   null,null,'Warp Item',1,null,0,
		   0,0,0,null,null,1,GETDATE(),GETDATE())
end