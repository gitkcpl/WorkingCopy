IF object_id('[dbo].[OrderPrint]') IS NULL 
EXEC ('CREATE PROC [dbo].[OrderPrint] AS SELECT 1 AS Id') 
GO

ALTER PROCEDURE [dbo].[OrderPrint]
  @id INT = 0,
    @compid INT,
    @Ord VARCHAR(1) = 'N',
    @ReportId INT = 0
AS
BEGIN
   SELECT  Ord.Id,
           Ord.CompId,
           Ord.YearId,
           Ord.VoucherId,
           Voucher.VoucherName,
           CONVERT(DATETIME2, CONVERT(VARCHAR(8), Ord.VoucherDate), 112) VoucherDate,
           Voucher.SortName,
           VoucherType.TypeName,
           VoucherType.Terms,
           vc.InvoiceHeading,
           Ord.VoucherDate IntVDate,
           Ord.VoucherNo,
           Ord.RefNo,
           Ord.AccId,
           acc.AccName,
           acc.PrintName,
           acc.GstIn P_GstIn,
           acc.PanNo P_PanNo,
           abl.Address1 P_Address1,
           abl.Address2 P_Address2,
           abl.CityId,
           abl.AreaId,
           ar.AreaName p_Area,
           ct.CityName p_City,
           st.StateName p_State,
           st.GstCode p_GstCode,
		   ISNULL(abl.Address1,'') + ISNULL(abl.Address2,'')  + ISNULL(ar.AreaName,'')  + ISNULL(ct.CityName,'') + ISNULL(st.StateName,'') +'-' + ISNULL(st.GstCode,'') AS PAddress,
           Ord.EmpId,
           Emp.EmpName,
           Ord.Currency,
           Ord.ExchRate,
           Ord.TransportId,
           tr.AccName AS Transport,
           Ord.RequireDate,
           Ord.Remarks,
           Ord.SpecialNotes,
           PayDescr,
           Ord.PayTermsId,
           Ord.TotalPcs,
           Ord.TotalQty,
           Ord.TotalAmount,
           Ord.Extra1,
           Ord.Extra2,
           ot.ProductId,
           p.ProductName,
		   p.HsnCode,
           ot.ColorId,
           cl.ColorName,
           ot.DesignId,
           dsgn.ProductName AS Design,
           ot.GradeId,
           gd.GradeName,
           ot.AvgWt,
           ot.Cut,
           ot.Width,
           ot.NoOfLot,
           ot.LotPcs,
           ot.Qty,
           ot.UomId,
           um.UnitName,
           ot.Total,
           ot.Rate,
           ot.Disc,
           ot.DiscAmt,
           ot.Sgst,
           ot.SgstAmt,
           ot.Cgst,
           ot.CgstAmt,
           ot.Igst,
           ot.IgstAmt,
           ot.Cess,
           ot.CessAmt,
           ot.NetTotal,
           ot.Remark,
           ot.CommDescr,
           ot.OrdStatus,
           ot.Priority, cmp.HolyWorld,
		   CMP.CompName,CMP.AadharNo cAadharNo ,cmp.AcNo cAcNo
		   ,cmp.Address1 CAddress1,cmp.Address2 CAddress2,CMP.BankName CBankName,
		   cct.CityName as CCityName,
		   cst.StateName as CStateName,
		   cmp.FAddress1 fAddress1, cmp.Address2 fAddress2,
	       fcty.CityName FCity, fst.StateName FState,
		   CMP.Email CEmail,CMP.FPincode CFPincode,CMP.GstIn CGstIn,CMP.IfsCode CIfsCode,
		   CMP.LogoPath LogoPath,CMP.PanNo CPanNo
		   ,CMP.Mobile CMobile,CMP.Phone CPhone,cmp.Pincode CPincode, ag.AccName AS Agent
   FROM dbo.Ord Ord
   INNER JOIN dbo.Voucher Voucher ON Voucher.Id = Ord.VoucherId
   INNER JOIN dbo.Acc acc ON Ord.AccId = acc.Id
   LEFT OUTER JOIN dbo.VchSetup vc ON vc.VoucherId = Ord.VoucherId AND vc.CompId = Ord.CompId
   INNER JOIN dbo.VoucherType ON Voucher.VTypeId = dbo.VoucherType.Id
   LEFT OUTER JOIN dbo.OrdTrans ot ON ot.OrdId = Ord.Id
   LEFT OUTER JOIN dbo.Emp Emp  ON Emp.Id = Ord.EmpId
   LEFT OUTER JOIN dbo.Product AS p ON ot.ProductId = p.Id
   LEFT OUTER JOIN dbo.Uom um ON ot.UomId = um.Id
   LEFT OUTER JOIN dbo.Product AS dsgn ON ot.DesignId = dsgn.Id
   LEFT OUTER JOIN dbo.PayTerms AS ptms ON Ord.PayTermsId = ptms.Id
   LEFT OUTER JOIN dbo.Acc AS tr ON Ord.TransportId = tr.Id
   LEFT OUTER JOIN dbo.AccBal abl ON ord.AccId = abl.AccId AND ord.CompId = abl.CompId AND ord.YearId = abl.YearId 
   LEFT OUTER JOIN dbo.AccAddress addr ON addr.Id = abl.AddressId
   LEFT OUTER JOIN dbo.Color AS cl ON cl.Id = ot.ColorId
   LEFT OUTER JOIN dbo.Grade AS gd ON gd.Id = ot.GradeId
   LEFT OUTER JOIN dbo.City AS ct ON abl.CityId = ct.Id
   LEFT OUTER JOIN dbo.State AS st ON ct.StateId = st.Id  
   LEFT OUTER JOIN dbo.Area AS ar ON abl.AreaId = ar.Id
   LEFT OUTER JOIN dbo.Acc ag ON ag.Id = acc.AgentId 
   LEFT OUTER JOIN dbo.Company CMP ON cmp.Id=Ord.CompId
   LEFT OUTER JOIN dbo.City AS cct ON cmp.CityId = cct.Id
   LEFT OUTER JOIN dbo.State AS cst ON cct.StateId = cst.Id
   LEFT OUTER JOIN dbo.City fcty ON fcty.Id = cmp.FCityId
   LEFT OUTER JOIN dbo.[State] fst ON fst.Id = cmp.FStateId         
   WHERE ot.IsDeleted = 0 AND (
              @id = 0
              OR Ord.Id = @id
          )
          AND Ord.CompId = @compid
          AND
          (
              @Ord = 'N'
              OR EXISTS
    (
        SELECT 1
        FROM ReportPara RP
        WHERE RP.ParameterValue = Ord.Id
              AND RP.ReportId = @ReportId
              AND RP.ParameterName = 'OrdId'
    )
          )

END
GO

