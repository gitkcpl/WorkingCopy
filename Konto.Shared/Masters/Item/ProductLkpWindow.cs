using AutoMapper;
using AutoMapper.QueryableExtensions;
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

namespace Konto.Shared.Masters.Item
{
    public partial class ProductLkpWindow : LookupForm
    {
        List<ProductLookupDto> _modelList = new List<ProductLookupDto>();
        public VoucherTypeEnum VoucherType { get; set; }

       // public ProductLookupDto SelectedProduct { get; set; }
        public ProductTypeEnum PTypeId { get; set; }

        
        public ProductLkpWindow()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.Product_Lookup_Layout;
            this.FormClassName = "Konto.Shared.Masters.Item.ProductIndex";
            this.AsemblyName = "Konto.Shared";
            
        }
        public override void LoadData()
        {
            base.LoadData();

            //var configuration = new MapperConfiguration(cfg =>
            //    cfg.CreateMap<AcGroupModel, LedgerGroupLookupDto>()
            //    .ForMember(x => x.DisplayText, p => p.MapFrom(x => x.GroupName))
            //    );
            int ptid = Convert.ToInt32(this.PTypeId);
            using (var _context = new KontoContext())
            {
                _context.Database.CommandTimeout = 0;
                _modelList = (from pd in _context.Products
                              
                            join bal in ( from sb in _context.StockBals
                                          where sb.CompanyId == KontoGlobals.CompanyId
                                              && sb.YearId == KontoGlobals.YearId
                                              group sb by sb.ProductId into g
                                          select new
                                          {
                                              ProductId = g.Key,
                                              OpPcs = g.Sum(x=>x.OpNos),
                                              OpQty = g.Sum(x=>x.OpQty)
                                          }
                                
                                ) on pd.Id equals bal.ProductId

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
                              join sz in _context.SizeModels on pd.SizeId equals sz.Id into sz_join
                              from sz1 in sz_join.DefaultIfEmpty()
                              join ac in _context.Accs on pd.VendorId equals ac.Id into ac_join
                              from ac in ac_join.DefaultIfEmpty()
                              join st in (from stt in _context.StockTranses
                                      where stt.CompanyId== KontoGlobals.CompanyId
                                          && stt.YearId  == KontoGlobals.YearId && !stt.IsDeleted
                                      group stt by stt.ItemId into  g
                                      select new
                                      {
                                          ItemId =g.Key,
                                          StockPcs = g.Sum(model =>model.Pcs ),
                                          StockQty  = g.Sum(y=> y.Qty)
                                      }
                                          ) on pd.Id equals st.ItemId into st_join
                              from st in st_join.DefaultIfEmpty()
                              orderby pd.ProductName
                              where 
                              !pd.IsDeleted && (this.PTypeId == 0 || pd.PTypeId == ptid)
                                && pd.ItemType == "I"
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
                                  OpPcs = bal.OpPcs,
                                  OpQty = bal.OpQty,
                                  ProductCode = pd.ProductCode,
                                  ProductType = pt.TypeName,
                                  SaleRate = pr.SaleRate,
                                  StockPcs = (st== null ? 0 : st.StockPcs ) + bal.OpPcs,
                                  StockQty = (st== null ? 0 :st.StockQty) + bal.OpQty,
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
                                  SerialReq = pd.SerialReq,
                                  Cut = pd.Cut,
                                  TaxId = pd.TaxId, SaleRateTaxInc = pd.SaleRateTaxInc, SizeName = sz1.SizeName,
                                  PurDisc = pd.PurDisc, SaleDisc = pd.SaleDisc, RatePerQty = pd.Price2, Mrp = pr.Mrp,
                                  Rate1 = pr.Rate1,Rate2= pr.Rate2


                              }
                ).ToList();


            }

            customGridControl1.DataSource = _modelList;

            if (string.IsNullOrEmpty(this.GridLayoutFileName) || this.KontoView == null) return;

            KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, this.KontoView);

            this.ActiveControl = customGridControl1;
            customGridView1.FocusedColumn = customGridView1.VisibleColumns[0];

        }

        private void AreaLkpWindow_Shown(object sender, EventArgs e)
        {
            if (this.SelectedValue <= 0)
            {
                if (!string.IsNullOrEmpty(this.SearchText))
                    customGridView1.StartIncrementalSearch(this.SearchText);

                return;

            }
            
            var item = _modelList.FirstOrDefault(x => x.Id == this.SelectedValue);
            var index = _modelList.IndexOf(item);
            if (index >= 0)
            {
                customGridView1.FocusedRowHandle = index;
            }
        }
    }
}
