IF object_id('[dbo].[TemplateList]') IS NULL 
EXEC ('CREATE PROC [dbo].[TemplateList] AS SELECT 1 AS Id') 
GO



ALTER proc dbo.TemplateList
as
begin
select   t.Descriptions TemplateName,vt.TypeName VoucherType,
t.Id,v.VoucherName Voucher,a.AccName as Party,t.StartRowNo ,t.Id
from Template t
left outer join acc a on a.Id=t.AccId
left outer join VoucherType vt on vt.Id=t.VTypeId
left outer join Voucher v on v.Id=t.VoucherId
where  t.IsDeleted=0 
end

Go