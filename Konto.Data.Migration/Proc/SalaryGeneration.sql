IF object_id('[dbo].[SalaryGeneration]') IS NULL 
EXEC ('CREATE PROC [dbo].[SalaryGeneration] AS SELECT 1 AS Id') 
GO


ALTER PROCEDURE [dbo].[SalaryGeneration] 
@companyid int,
@empid int,
@fromdate int,
@todate int,
@yearid int
as
BEGIN
    select p.Id,
           CONVERT(datetime2, CONVERT(VARCHAR(8), p.VoucherDate), 112) As EntryDate,
		   pe.ProdDate,
		   p.Voucherno TakaNo ,
		   p.CompId ,
		   pe.TotalMtrs , 
		   pe.NightMtrs , 
		   pe.DayMtrs , 
		   bm.Cops TotalTaka,	
		   bm.Ply Lengths,
		   bm.Tops Ends,
		   bm.VoucherNo BeamNo,
		   p.ProductId,
		   pm.ProductName As Quality,
		   p.NetWt As TakaMeters,
		   p.GrossWt As TakaWeight,
		   p.GradeId,
		   p.ColorId,
		   --p.BoxRate As MendorRate,
		   p.MacId As MachineID,
		   mm.MachineName MachineNo,
		   --p.PackEmpId as FolderEmpID,
		   --e.EmpName As FolderName,
		   --p.NetWt * p.Cops As FolderSalary,
		   --p.JobId As MendorEmpID,
		   --ee.EmpName As MendorName,
		   --p.NetWt * p.BoxRate As MendorSalary,
		   --p.CheckEmpId,
		   --ems.EmpName CheckerName,
		   --p.NetWt * p.FinQty As CheckerSalary,
		   pe.EmpId, 
		   epe.EmpName As WorkerName,
		 --  pe.ProdDate,
		 -- pe.TotalMtrs workermetr,
		   pe.Rate JobRate,
		   pe.Amount,
		   pe.Id As PeId
		    from Prod  p
	inner join Prod_Emp pe
		on pe.ProdId = p.Id
	left outer  join Voucher v
		on p.VoucherId = v.Id and v.VTypeId = 17
		left outer join 
		prod bm on p.BatchId  = bm.Id -- beam
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
		where p.IsDeleted = 0 and p.IsActive = 1 and pe.IsDeleted=0 and ISNULL(pe.TotalMtrs,0) >0 
		and p.CompId = @companyid  and isnull(cast(convert(varchar(8),proddate,112) as int),0) between @fromdate and @todate and ( @empid = 0 or epe.Id = @empid)
		and p.YearId = @yearid
END
GO

