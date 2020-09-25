using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Konto.Core.Shared.Frms;
using Konto.Data.Models.Masters.Dtos;
using Konto.App.Shared;
using Konto.Data;
using Konto.Core.Shared.Libs;
using Syncfusion.Windows.Forms;
using Serilog;

namespace Konto.Shared.Masters.Design
{
    public partial class DesignListView : ListBaseView
    {

        private List<ProductListDto> _modelList = new List<ProductListDto>();
        public DesignListView()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.Design_List;
            this.Load += ProductListView_Load;
        }

        private void ProductListView_Load(object sender, EventArgs e)
        {
            //this.RefreshGrid();
        }

        public override void RefreshGrid()
        {
            base.RefreshGrid();
           

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
                              select new ProductListDto()
                              {
                                  CheckNegative = pd.CheckNegative,
                                  BarCode = pd.BarCode,
                                  CatName = cat.CatName,
                                  DealerPrice = pr.DealerPrice,
                                  GroupName = grp.GroupName,
                                  ProductName = pd.ProductName,
                                  HsnCode = pd.HsnCode,
                                  Id = pd.Id,
                                  IsActive = pd.IsActive,
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
                                  Vendor = ac.AccName,
                                  Sgst = tx.Sgst,
                                  Cgst = tx.Cgst,
                                  Igst = tx.Igst,
                                  Cess = tx.CessRate,
                                  CreateUser = pd.CreateUser,
                                  CreateDate = pd.CreateDate,
                                  ModifyDate = pd.ModifyDate,
                                  ModifyUser = pd.ModifyUser,
                                  SerialReq = pd.SerialReq
                              }
                ).ToList();


            }

            customGridControl1.DataSource = _modelList;
            if (string.IsNullOrEmpty(this.GridLayoutFileName) || this.KontoView == null) return;

            KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, this.KontoView);

            this.ActiveControl = customGridControl1;
            if (_modelList.Count == 0)
                listAction1.EditDeleteDisabled(false);
            else
                listAction1.EditDeleteDisabled(true);
        }

        public override void DeleteRec()
        {
            base.DeleteRec();

            if (customGridView1.FocusedRowHandle < 0) return;
            try
            {
                var _id = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "Id"));

                if (MessageBoxAdv.Show(KontoGlobals.DeleteBeforeMsg, "Delete ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                using (var db = new KontoContext())
                {
                    var model = db.Products.Find(_id);
                    model.IsDeleted = true;
                    db.SaveChanges();
                    customGridView1.DeleteRow(customGridView1.FocusedRowHandle);
                    MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "product delete");
                MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
            }
        }
    }
}
