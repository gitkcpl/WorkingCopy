
IF object_id('[dbo].[beam_status_by_machine]') IS NULL 
EXEC ('CREATE PROC [dbo].[beam_status_by_machine] AS SELECT 1 AS Id') 
GO 

ALTER PROCEDURE [dbo].[beam_status_by_machine]
 	@CompanyId int,
	@vtypeid int,
	@Status varchar(50),
	@MachineId int
AS
BEGIN

select p.Id,p.RowId,p.TransId,p.SrNo,
		p.LoadingDate as LoadingDate,
		p.VoucherNo,
		case when bp.id is null then p.TwistType else bp.PositionName end TwistType,
		p.Cops,p.NetWt,
		cast(isnull(tp.TakaQty,0) as numeric(18,3)) ProdWt,
		cast(p.NetWt-isnull(tp.TakaQty,0) as numeric(18,3)) as BalWt,
	   tp.Taka as TakaProd, 
	   cast(p.Ply as numeric(18,2)) Mtrs, cast( isnull(tp.TakaMtr,0) as numeric(18,2)) ProdMtrs,
	  cast( p.Ply-isnull(tp.TakaMtr,0) as numeric(18,2)) BalMtrs,
	   p.BoxProductId GreyProductId
    
from Prod p
left outer join Voucher v on v.Id = p.VoucherId
left outer join Position bp on p.SubGradeId = bp.Id
left outer join (SELECT sum(per/100) Taka, sum(Qty)TakaQty,sum(Mtr) TakaMtr,BeamId from TakaBeam tb
				where tb.IsActive=1 and tb.IsDeleted=0 
				group by BeamId)tp on tp.BeamId=p.Id

where p.IsActive = 1 and p.IsDeleted=0 
	and p.IsClose=0  
	and v.VTypeId=@vtypeid
	and p.ProdStatus=@Status
	and p.CompId=@CompanyId -- and (p.VoucherDate between @FromDate and @ToDate)
	and p.MacId=@MachineId

END

GO


