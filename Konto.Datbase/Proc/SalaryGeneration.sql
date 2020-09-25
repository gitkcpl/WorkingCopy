CREATE PROCEDURE [dbo].[SalaryGeneration] 
@companyid int,
@empid int,
@fromdate int,
@todate int,
@yearid int
as
BEGIN
    select p.Id,
           CONVERT(datetime2, CONVERT(VARCHAR(8), p.VoucherDate), 112) As InvoiceDate,
		   pe.proddate,
		   p.voucherno ,
		   p.CompId ,
		   p.LoadingDate ,
		   p.WarpingDate ,
		   pe.TotalMtrs , 
		   pe.NightMtrs , 
		   pe.DayMtrs , 
		   bp.TotalTaka,	
		   bp.Lengths,
		   bp.Ends,
		   bp.BeamNo,
		   p.ProductId,
		   pm.ProductName As Quality,
		   p.NetWt As TakaMeters,
		   p.GrossWt As TakaWeight,
		   p.GradeId,
		   p.ColorId,
		   p.VoucherNo As Takano,
		   p.BoxRate As JobRate,
		   p.MacId As MachineID,
		   mm.MachineName MachineNo,
		   p.PackEmpId as FolderEmpID,
		   e.EmpName As FolderName,
		   p.NetWt * p.Cops As FolderSalary,
		   p.JobId As MendorEmpID,
		   ee.EmpName As MendorName,
		   p.NetWt * p.BoxRate As MendorSalary,
		   p.CheckEmpId,
		   ems.EmpName CheckerName,
		   p.NetWt * p.FinQty As CheckerSalary,
		   pe.EmpId, 
		   epe.EmpName As WorkerName,
		 --  pe.ProdDate,
		  pe.TotalMtrs workermetr,
		   pe.Rate,
		   pe.Amount,
		   pe.Id As PeId
		    from Prod  p
	inner join Prod_Emp pe
		on pe.ProdId = p.Id
	left outer  join Voucher v
		on p.VoucherId = v.Id and v.VTypeId = 17
		left outer join 
		( 
SELECT CONVERT(datetime2, CONVERT(VARCHAR(8), pd.VoucherDate), 112)  FoldDate,
nm.MachineName,
pd.VoucherNo BeamNo, 
tb.Qty TakaQty,
pd.NetWt TakaMtr,
tb.BeamId,
pd.Cops As TotalTaka,	
		   pd.Ply As Lengths,
		   pd.Tops As Ends,
		   tb.ProdID
from  dbo.Prod pd 
inner join  (

select max(id) As Id,BeamId,Qty,max(ProdId) As ProdID  from TakaBeam group by BeamId, Qty
)tb on tb.BeamId = pd.Id
LEFT OUTER JOIN MachineMaster nm ON pd.MacId=nm.Id
WHERE pd.IsActive=1 and pd.IsDeleted=0

)bp on bp.ProdId = p.Id
	left outer join Prod_Weft pw
		on pw.ProdId = p.Id 
	left outer join Emp e 
		on e.Id = p.PackEmpId
	left outer join emp ee
		on ee.Id = p.JobId
	left outer join emp ems
		on ems.Id = p.CheckEmpId
	left outer join emp epe
		on epe.Id = pe.EmpId
	left outer join MachineMaster mm
		on mm.Id = p.MacId
	left outer join Product pm
		on pm.Id = p.ProductId
		where p.IsDeleted = 0 and p.IsActive = 1 and ISNULL(pe.TotalMtrs,0) >0 
		and p.CompId = @companyid  and isnull(cast(convert(varchar(8),proddate,112) as int),0) between @fromdate and @todate and (epe.Id = @empid or @empid = 0 )
		and p.YearId = @yearid
END
GO

