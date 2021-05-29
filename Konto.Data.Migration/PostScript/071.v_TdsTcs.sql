
--'4,'Interest on Securities'
--2,'Artificial Judicial Person'
IF NOT EXISTS(SELECT 1 FROM dbo.tds_tcs WHERE Id=1)
INSERT INTO dbo.tds_tcs(Id,NopId,DeducteeId,AppliedDate,TaxLimit,TaxPer,SurChargeLimit,
SurChargePer,EduCessPer,SecEduCessPer,TaxRateIfNoPan)
values(1,38,2,20201001,5000000,0.075,0,0,0,0,0)
--3,'Association of Person'

IF NOT EXISTS(SELECT 1 FROM dbo.tds_tcs WHERE Id=2)
INSERT INTO dbo.tds_tcs(Id,NopId,DeducteeId,AppliedDate,TaxLimit,TaxPer,SurChargeLimit,
SurChargePer,EduCessPer,SecEduCessPer,TaxRateIfNoPan)
values(2,38,3,20201001,5000000,0.0075,0,0,0,0,0)

--4,'Body of Individuals'
IF NOT EXISTS(SELECT 1 FROM dbo.tds_tcs WHERE Id=3)
INSERT INTO dbo.tds_tcs(Id,NopId,DeducteeId,AppliedDate,TaxLimit,TaxPer,SurChargeLimit,
SurChargePer,EduCessPer,SecEduCessPer,TaxRateIfNoPan)
values(3,38,4,20201001,5000000,0.075,0,0,0,0,0)

--5,'Co-operative Society'
IF NOT EXISTS(SELECT 1 FROM dbo.tds_tcs WHERE Id=4)
INSERT INTO dbo.tds_tcs(Id,NopId,DeducteeId,AppliedDate,TaxLimit,TaxPer,SurChargeLimit,
SurChargePer,EduCessPer,SecEduCessPer,TaxRateIfNoPan)
values(4,38,5,20201001,5000000,0.075,0,0,0,0,0)

--6,'Company Non-Resident'
IF NOT EXISTS(SELECT 1 FROM dbo.tds_tcs WHERE Id=5)
INSERT INTO dbo.tds_tcs(Id,NopId,DeducteeId,AppliedDate,TaxLimit,TaxPer,SurChargeLimit,
SurChargePer,EduCessPer,SecEduCessPer,TaxRateIfNoPan)
values(5,38,6,20201001,5000000,0.075,0,0,0,0,0)

--7,'Company Resident'
IF NOT EXISTS(SELECT 1 FROM dbo.tds_tcs WHERE Id=6)
INSERT INTO dbo.tds_tcs(Id,NopId,DeducteeId,AppliedDate,TaxLimit,TaxPer,SurChargeLimit,
SurChargePer,EduCessPer,SecEduCessPer,TaxRateIfNoPan)
values(6,38,7,20201001,5000000,0.075,0,0,0,0,0)

--9,'Individual/HUF-Non Resident'
IF NOT EXISTS(SELECT 1 FROM dbo.tds_tcs WHERE Id=7)
INSERT INTO dbo.tds_tcs(Id,NopId,DeducteeId,AppliedDate,TaxLimit,TaxPer,SurChargeLimit,
SurChargePer,EduCessPer,SecEduCessPer,TaxRateIfNoPan)
values(7,38,9,20201001,5000000,0.075,0,0,0,0,0)

--10,'Local Authority'
IF NOT EXISTS(SELECT 1 FROM dbo.tds_tcs WHERE Id=8)
INSERT INTO dbo.tds_tcs(Id,NopId,DeducteeId,AppliedDate,TaxLimit,TaxPer,SurChargeLimit,
SurChargePer,EduCessPer,SecEduCessPer,TaxRateIfNoPan)
values(8,38,10,20201001,5000000,0.075,0,0,0,0,0)

--11,'Partnership Firm'
IF NOT EXISTS(SELECT 1 FROM dbo.tds_tcs WHERE Id=9)
INSERT INTO dbo.tds_tcs(Id,NopId,DeducteeId,AppliedDate,TaxLimit,TaxPer,SurChargeLimit,
SurChargePer,EduCessPer,SecEduCessPer,TaxRateIfNoPan)
values(9,38,11,20201001,5000000,0.075,0,0,0,0,0)
