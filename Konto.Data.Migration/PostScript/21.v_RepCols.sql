SET IDENTITY_INSERT dbo.rep_cols ON

IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=1)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(1, 15, 'Date', 0, 0, 'Date', 'Date', CONVERT(bit, 'True'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=2)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(2, 15, 'Bill No', 1, 1, 'Bill No', 'Bill No', CONVERT(bit, 'True'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=3)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(3, 15, 'Challan No', 2, 2, 'ChalNo', 'Chal No', CONVERT(bit, 'False'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=4)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(4, 15, 'Order No', 3, 3, 'Ord No', 'Ord No', CONVERT(bit, 'False'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=5)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(5, 15, 'Party', 4, 4, 'Party', 'Party', CONVERT(bit, 'True'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=6)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(6, 15, 'GstIn', 5, 5, 'Gstin', 'Gstin', CONVERT(bit, 'False'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=7)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(7, 15, 'Pcs', 6, 6, 'Pcs', 'Pcs', CONVERT(bit, 'True'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=8)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(8, 15, 'Qty', 7, 7, 'Qty', 'Qty', CONVERT(bit, 'True'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=9)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(9, 15, 'Taxable', 10, 10, 'Taxable', 'Taxable', CONVERT(bit, 'True'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=10)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(10, 15, 'Igst', 11, 11, 'Igst', 'Igst', CONVERT(bit, 'True'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=11)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(11, 15, 'Cgst', 12, 12, 'Cgst', 'Cgst', CONVERT(bit, 'True'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=12)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(12, 15, 'Sgst', 13, 13, 'Sgst', 'Sgst', CONVERT(bit, 'True'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=13)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(13, 15, 'Cess', 14, 14, 'Cess', 'Cess', CONVERT(bit, 'False'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=14)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(14, 15, 'Tcs', 15, 15, 'Tcs', 'Tcs', CONVERT(bit, 'True'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=15)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(15, 15, 'Total', 16, 16, 'Total', 'Total', CONVERT(bit, 'True'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=16)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(16, 15, 'Trasnport', 17, 17, 'Transport', 'Transport', CONVERT(bit, 'False'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=18)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(18, 15, 'Agent', 18, 18, 'Agent', 'Agent', CONVERT(bit, 'False'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=19)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(19, 15, 'Remark', 19, 19, 'Remark', 'Remark', CONVERT(bit, 'False'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=20)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(20, 15, 'Freight', 9, 9, 'Freight', 'Freight', CONVERT(bit, 'False'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=21)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(21, 15, 'Discount', 8, 8, 'Discount', 'Discount', CONVERT(bit, 'False'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=22)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(22, 12, 'Date', 0, 0, 'Date', 'Date', CONVERT(bit, 'True'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=23)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(23, 12, 'Bill No', 1, 1, 'Bill No', 'Bill No', CONVERT(bit, 'True'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=24)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(24, 12, 'Challan No', 2, 2, 'ChalNo', 'ChalNo', CONVERT(bit, 'False'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=25)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(25, 12, 'Party', 3, 3, 'Party', 'Party', CONVERT(bit, 'True'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=26)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(26, 12, 'Product', 4, 4, 'Product', 'Product', CONVERT(bit, 'True'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=27)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(27, 12, 'Pcs', 5, 5, 'Pcs', 'Pcs', CONVERT(bit, 'True'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=28)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(28, 12, 'Qty', 6, 6, 'Qty', 'Qty', CONVERT(bit, 'True'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=29)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(29, 12, 'Rate', 7, 7, 'Rate', 'Rate', CONVERT(bit, 'True'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=30)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(30, 12, 'Discount', 8, 8, 'Disc', 'Disc', CONVERT(bit, 'False'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=21)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(31, 12, 'Freight', 9, 9, 'Freight', 'Freight', CONVERT(bit, 'False'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=32)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(32, 12, 'Gst Rate', 10, 10, 'Gst Rate', 'Gst Rate', CONVERT(bit, 'True'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=33)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(33, 12, 'Taxable Value', 11, 11, 'Taxable Value', 'Taxable Value', CONVERT(bit, 'True'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=34)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(34, 12, 'Igst Amt', 12, 12, 'Igst Amt', 'Igst Amt', CONVERT(bit, 'True'), 0.00)
GO
IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=53)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(35, 12, 'Cgst Amt', 13, 13, 'Cgst Amt', 'Cgst Amt', CONVERT(bit, 'True'), 0.00)
GO

IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=36)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(36, 12, 'Sgst Amt', 14, 14, 'Sgst Amt', 'Sgst Amt', CONVERT(bit, 'True'), 0.00)
GO

IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=37)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(37, 12, 'Cess', 15, 15, 'Cess', 'Cess', CONVERT(bit, 'False'), 0.00)
GO

IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=38)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(38, 12, 'Total', 16, 16, 'Total', 'Total', CONVERT(bit, 'True'), 0.00)
GO

IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=39)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(39, 12, 'Design', 17, 17, 'Design', 'Design', CONVERT(bit, 'False'), 0.00)
GO

IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=40)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(40, 12, 'Color', 18, 18, 'Color', 'Color', CONVERT(bit, 'False'), 0.00)
GO

IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=41)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(41, 12, 'Grade', 19, 19, 'Grade', 'Grade', CONVERT(bit, 'False'), 0.00)
GO

IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=42)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(42, 12, 'Lot No', 20, 20, 'Lot No', 'Lot No', CONVERT(bit, 'False'), 0.00)
GO

IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=43)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(43, 13, 'Pcs', 1, 1, 'Pcs', 'Pcs', CONVERT(bit, 'True'), 0.00)
GO

IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=44)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(44, 13, 'Qty', 2, 2, 'Qty', 'Qty', CONVERT(bit, 'True'), 0.00)
GO

IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=45)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(45, 13, 'Discount', 3, 3, 'Discount', 'Discount', CONVERT(bit, 'False'), 0.00)
GO

IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=46)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(46, 13, 'Freight', 4, 4, 'Freight', 'Freight', CONVERT(bit, 'False'), 0.00)
GO

IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=47)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(47, 13, 'Taxable Value', 5, 5, 'Taxable Value', 'Taxable Value', CONVERT(bit, 'True'), 0.00)
GO

IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=48)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(48, 13, 'Igst Amt', 6, 6, 'Igst Amt', 'Igst Amt', CONVERT(bit, 'True'), 0.00)
GO

IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=49)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(49, 13, 'Cgst Amt', 7, 7, 'Cgst Amt', 'Cgst Amt', CONVERT(bit, 'True'), 0.00)
GO

IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=52)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(52, 13, 'Sgst Amt', 8, 8, 'Sgst Amt', 'Sgst Amt', CONVERT(bit, 'True'), 0.00)
GO

IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=53)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(53, 13, 'Cess', 9, 9, 'Cess', 'Cess', CONVERT(bit, 'False'), 0.00)
GO

IF NOT EXISTS (select 1 from dbo.rep_cols em where em.Id=54)
INSERT INTO dbo.rep_cols(Id, ReportId, ColName, SysOrder, UserOrder, SysHead, UserHead, Show, Width) VALUES(54, 13, 'Total', 10, 10, 'Total', 'Total', CONVERT(bit, 'True'), 0.00)
GO
SET IDENTITY_INSERT dbo.rep_cols off