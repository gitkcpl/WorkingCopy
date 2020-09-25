update dbo.ErpModule set AssemblyName='Konto.Shared.Masters.Voucher.VoucherIndex' where id=127

update dbo.ErpModule set moduledesc= 'Mill Receipt Challan',Visible=0 where id=356 -- mill receipt challan

update dbo.ErpModule set moduledesc= 'Job Receipt Challan',Visible=0 where id=319 -- job receipt challan

update dbo.ErpModule set Title = 'Mill Return', AssemblyName ='Konto.Trading.MillReturn.MrIndex', MainAssembly='Konto.Trading' where id=361

update dbo.ErpModule set Title = 'Ledger Report', AssemblyName ='Konto.Reporting.Para.Ledger.LedgerMainView',MainAssembly='Konto.Reporting' where id=801

update dbo.ErpModule set Title = 'Order Report', AssemblyName ='Konto.Reporting.Para.OrdPara.OrdParaMainView',MainAssembly='Konto.Reporting' where id=828

update dbo.ErpModule set Title = 'Stock Report', AssemblyName ='Konto.Reporting.Para.Stock.StockMainView',MainAssembly='Konto.Reporting' where id=1058

update ErpModule set Title = 'Bank Reconciliation', AssemblyName='Konto.Reporting.Para.Reoncile.BRMainView', MainAssembly='Konto.Reporting' where Id=376
update ErpModule set Title = 'Taka Opening', AssemblyName='Konto.Weaves.TakaOp.TakaOpIndex', MainAssembly='Konto.Weaves'  where Id=143
update ErpModule set Title = 'Gstr1 Report',  AssemblyName='Konto.Reporting.Para.Gst.GstMainView', MainAssembly='Konto.Reporting'  where Id=830
update ErpModule set Title = 'Gst 3B Report',  AssemblyName='Konto.Reporting.Para.Gst.GstMainView', MainAssembly='Konto.Reporting'  where Id=832

update ErpModule set  AssemblyName='Konto.Shared.Masters.Item.ProdOpBalBulkEditView', MainAssembly='Konto.Shared'  where Id=144
update ErpModule set Title='Trial Balance', AssemblyName='Konto.Reporting.Para.TrialBalance.TrialMainView', MainAssembly='Konto.Reporting'  where Id=803
update ErpModule set Title='Balance Sheet', AssemblyName='Konto.Reporting.Para.BlSheet.BlMainView', MainAssembly='Konto.Reporting'  where Id=804

update ErpModule set Title='Order Register',AssemblyName ='Konto.Reporting.Para.OrdPara.OrdParaMainView', MainAssembly='Konto.Reporting' where id=828
update ErpModule set Title='Outward Register',AssemblyName ='Konto.Reporting.Para.ChlPara.ChlParaMainView', MainAssembly='Konto.Reporting' where id=813
update ErpModule set Title='Gray Issue Register',AssemblyName ='Konto.Reporting.Para.ChlPara.ChlParaMainView', MainAssembly='Konto.Reporting' where id=818
update ErpModule set Title='Mill Receipt Register', AssemblyName ='Konto.Reporting.Para.ChlPara.ChlParaMainView', MainAssembly='Konto.Reporting' where id=819
update ErpModule set Title='Job Issue Register', AssemblyName ='Konto.Reporting.Para.ChlPara.ChlParaMainView', MainAssembly='Konto.Reporting' where id=823 
update ErpModule set Title='Purchase Return Register',  AssemblyName ='Konto.Reporting.Para.BillPara.ParaMainView', MainAssembly='Konto.Reporting' where id=811
update ErpModule set Title='Sales Register',  AssemblyName ='Konto.Reporting.Para.BillPara.ParaMainView', MainAssembly='Konto.Reporting' where id=814
update ErpModule set Title='Sales Return Register',  AssemblyName ='Konto.Reporting.Para.BillPara.ParaMainView', MainAssembly='Konto.Reporting' where id=816
update ErpModule set Title='Cr/Dr Register', AssemblyName ='Konto.Reporting.Para.BillPara.ParaMainView', MainAssembly='Konto.Reporting' where id=827
update ErpModule set Title='Beam Loading', AssemblyName ='Konto.Weaves.BeamLoading.BeamLoadingIndex', MainAssembly='Konto.Weaves' where id=368
update ErpModule set Title='Opening Balance Edit', AssemblyName='Konto.Shared.Masters.Acc.OpBalBulkEditView', MainAssembly='Konto.Shared'  where Id=142
update ErpModule set Title='Rec/Pay Setting', AssemblyName='Konto.Shared.Masters.Recpayset.RecPayIndex', MainAssembly='Konto.Shared'  where Id=908
update ErpModule set Title='Opening Mill Issue',  AssemblyName='Konto.Trading.OpMillIssue.OpMiIndex', MainAssembly='Konto.Trading'  where Id=145
update ErpModule set Title='Opening Job Issue', AssemblyName='Konto.Trading.OpJobIssue.OpJobIndex', MainAssembly='Konto.Trading'  where Id=146
update ErpModule set Title='Gstr2 Report', AssemblyName='Konto.Reporting.Para.Gstr2.GsttwoMainView', MainAssembly='Konto.Reporting'  where Id=831
update ErpModule set Title='Beam Register', AssemblyName='Konto.Weaves.Para.BeamParaMainView', MainAssembly='Konto.Weaves'  where Id=839
update ErpModule set Title='Taka Register', AssemblyName='Konto.Weaves.Para.ParaMainView', MainAssembly='Konto.Weaves'  where Id=840
update ErpModule set Title='Yarn Register', AssemblyName='Konto.Yarn.Para.YarnParaMainView', MainAssembly='Konto.Yarn'  where Id=845

update ErpModule set Title='Job Receipt Register', AssemblyName ='Konto.Reporting.Para.ChlPara.ChlParaMainView', MainAssembly='Konto.Reporting' where id=825
update ErpModule set Title='Job Receive Register', AssemblyName ='Konto.Reporting.Para.ChlPara.ChlParaMainView', MainAssembly='Konto.Reporting' where id=824
update ErpModule set Title='Mill Receipt Register', AssemblyName ='Konto.Reporting.Para.ChlPara.ChlParaMainView', MainAssembly='Konto.Reporting' where id=819

update ErpModule set Title='Mill Return Register', AssemblyName ='Konto.Reporting.Para.ChlPara.ChlParaMainView', MainAssembly='Konto.Reporting' where id=821

update ErpModule set Title='ITC04 Report', AssemblyName ='Konto.Reporting.Para.ITC04.ITCMainView', MainAssembly='Konto.Reporting' where id=833
update ErpModule set Title='Tds Report', AssemblyName ='Konto.Reporting.Para.TDSPara.TDSMainView', MainAssembly='Konto.Reporting' where id=836

update ErpModule set Title ='Gstr2 Reconcile', AssemblyName ='Konto.Reporting.Para.Gstr2Reconcile.GsttwoReconcileMainView', MainAssembly='Konto.Reporting' where id=834

update ErpModule set AssemblyName ='Konto.Shared.Security.ChangePasswordIndex', MainAssembly='Konto.Shared' where id=702

----Update prodout
--UPDATE dbo.ProdOut SET GrayMtr = Qty WHERE Qty < 0
----Update Pcs, Qty, TotalAmount in Order
--UPDATE md SET md.TotalAmount = ISNULL(x.Total,0), md.TotalQty = x.Qty, md.TotalPcs = ISNULL(x.Pcs,0)
--FROM dbo.Ord AS md 
--LEFT OUTER JOIN ( SELECT OrdId, SUM(Total) AS Total, SUM(Qty) AS Qty, SUM(LotPcs) AS Pcs FROM dbo.OrdTrans WHERE IsDeleted = 0
--GROUP BY OrdId ) x ON x.OrdId = md.Id

---- update Pcs, Qty, TotalAmount in Challan
--UPDATE md SET md.TotalAmount = isnull(x.Total,0), md.TotalQty = isnull(x.Qty,0), md.TotalPcs = isnull(x.Pcs,0)
--FROM dbo.Challan AS md 
--LEFT OUTER JOIN ( SELECT ChallanId, SUM(Total) AS Total, SUM(Qty) AS Qty, SUM(Pcs) AS Pcs FROM dbo.ChallanTrans WHERE IsDeleted = 0
--GROUP BY ChallanId ) x ON x.ChallanId = md.Id


--UPDATE dbo.WeftItem SET Panno =0 , Wasteper = 0, Totcard = 0, TotPick = 0, Total = 0, Card = 0 , Tar = 0, Rate = 0, 
--Weight = 0, NWeight = 0,JobCharge = 0, Costing = 0 WHERE Totcard IS  NULL OR  TotPick IS NULL OR  Total IS NULL OR  Card IS  null OR  Tar IS  NULL OR  Rate IS  NULL OR  
--Weight IS  NULL OR  NWeight IS  NULL OR JobCharge IS  NULL OR  Costing IS NULL OR Wasteper IS NULL or Panno is null

--UPDATE md SET md.BillNo = x.BillNo
--FROM dbo.LedgerTrans AS md 
--INNER JOIN ( SELECT RowId, BillNo FROM dbo.BillMain WHERE IsDeleted = 0 AND VoucherId = 13
-- ) x ON x.RowId = md.RefId
 
-- UPDATE md SET md.BillNo = x.BillNo
--FROM dbo.LedgerTrans AS md 
--INNER JOIN ( SELECT RowId, BillNo FROM dbo.BillMain WHERE IsDeleted = 0 AND VoucherId = 23
-- ) x ON x.RowId = md.RefId

