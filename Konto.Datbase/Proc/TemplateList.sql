create proc TemplateList
as
begin
select   t.Descriptions,t.TemplateId,vt.TypeName
from Template t
left outer join VoucherType vt on vt.Id=t.VTypeId
where  t.IsDeleted=0 -- and TempFieldId=1 
group by  t.Descriptions,t.TemplateId,vt.TypeName 
end