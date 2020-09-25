 

/*-- =============================================
-- Author:		<Author,,Name>
  Create date: <Create Date,,>
  Description:	<Description,,>
  exec PendingOrderonChallan 1,21,4
  select * from VoucherType
-- =============================================*/

CREATE PROCEDURE [dbo].[FreeMachineList]

@CompanyID int
--,@VoucherId int
as
BEGIN
select m.MachineName,m.Id,M.CompanyID,
 M.DivId,M.remark,m.RowId,
 M.DivId,m.CreateDate,m.CreateUser,m.IpAddress,m.IsActive,m.IsDeleted
 ,m.ModifyDate,m.ModifyUser,m.remark

  from MachineMaster m
  where m.id not in(
   select MacId id from Prod p where p.IsActive=1 and p.IsDeleted=0 and
    p.IsClose=0 and (p.MacId is not null) and p.CompId=@CompanyID and p.ProdStatus='LOADED')
and m.IsActive=1 and m.IsDeleted=0 and m.CompanyID=@CompanyID

END
select * from Prod
GO

