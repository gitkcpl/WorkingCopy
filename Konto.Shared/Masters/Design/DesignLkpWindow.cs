using AutoMapper;
using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Masters.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Konto.Shared.Masters.Design
{
    public partial class DesignLkpWindow : LookupForm
    {
        List<ProductLookupDto> _modelList = new List<ProductLookupDto>();
        public VoucherTypeEnum VoucherType { get; set; }

       // public ProductLookupDto SelectedProduct { get; set; }
        public int PTypeId { get; set; }
        public DesignLkpWindow()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.Design_Lookup;
            this.FormClassName = "Konto.Shared.Masters.Design.DesignIndex";
            this.AsemblyName = "Konto.Shared";
            
        }
        public override void Ok()
        {
            base.Ok();
            this.SelectedTex = customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, customGridView1.Columns["ProductCode"]).ToString();
        }
        public override void LoadData()
        {
            base.LoadData();

            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<AcGroupModel, LedgerGroupLookupDto>()
                .ForMember(x => x.DisplayText, p => p.MapFrom(x => x.GroupName))
                );

            using (var _context = new KontoContext())
            {
                _modelList = (from pd in _context.Products
                               join bal in _context.StockBals on pd.Id equals bal.ProductId
                               join pr in _context.Prices on pd.Id equals pr.ProductId
                               join cat in _context.CategroyModels on pd.CategoryId equals cat.Id into cat_join
                               from cat in cat_join.DefaultIfEmpty()
                               join grp in _context.PGroups on pd.GroupId equals grp.Id into grp_join
                               from grp in grp_join.DefaultIfEmpty()
                               join sub in _context.PSubGroups on pd.SubGroupId equals sub.Id into sub_join
                               from sub in sub_join.DefaultIfEmpty()
                               join pt in _context.ProductTypes on pd.PTypeId equals pt.Id
                               join tx in _context.TaxMasters on pd.TaxId equals tx.Id
                               join um in _context.Uoms on pd.UomId equals um.Id
                               join ac in _context.Accs on pd.VendorId equals ac.Id into ac_join
                               from ac in ac_join.DefaultIfEmpty()
                               orderby pd.ProductName
                               where bal.CompanyId == KontoGlobals.CompanyId && bal.BranchId == KontoGlobals.BranchId && bal.YearId == KontoGlobals.YearId &&
                               !pd.IsDeleted
                                 && pd.ItemType == "D"
                               select new ProductLookupDto()
                               {
                                   CheckNegative = pd.CheckNegative,
                                   BarCode = pd.BarCode,
                                   CatName = cat.CatName,
                                   DealerPrice = pr.DealerPrice,
                                   GroupName = grp.GroupName,
                                   ProductName = pd.ProductName,
                                   HsnCode = pd.HsnCode,
                                   Id = pd.Id,
                                   OpPcs = bal.OpNos,
                                   OpQty = bal.OpQty,
                                   ProductCode = pd.ProductCode,
                                   ProductType = pt.TypeName,
                                   SaleRate = pr.SaleRate,
                                   StockPcs = bal.BalNos + bal.OpNos,
                                   StockQty = bal.BalQty + bal.OpQty,
                                   SubName = sub.SubName,
                                   TaxName = tx.TaxName,
                                   UnitName = um.UnitName,
                                   UomId = pd.UomId,
                                   PurUomId = pd.PurUomId,
                                   PTypeId = pd.PTypeId,
                                   Vendor = ac.AccName,
                                   Sgst = tx.Sgst,
                                   Cgst = tx.Cgst,
                                   Igst = tx.Igst,
                                   Cess = tx.CessRate,
                                   SerialReq = pd.SerialReq
                               }
                ).ToList();


            }

            customGridControl1.DataSource = _modelList;

            if (string.IsNullOrEmpty(this.GridLayoutFileName) || this.KontoView == null) return;

            KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, this.KontoView);

            this.ActiveControl = customGridControl1;


        }

        private void AreaLkpWindow_Shown(object sender, EventArgs e)
        {
            if (this.SelectedValue <= 0) return;
            var item = _modelList.FirstOrDefault(x => x.Id == this.SelectedValue);
            var index = _modelList.IndexOf(item);
            if (index >= 0)
            {
                customGridView1.FocusedRowHandle = index;
            }
        }
    }
}
