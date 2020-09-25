using System;
using System.Windows.Forms;
using AutoMapper;
using Konto.App.Shared;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Masters.Dtos;
using System.Linq;

namespace Konto.Shared.Masters.Item
{
    public partial class ProductLookup : LookupBase
    {
        public ProductLookupDto GroupDto { get; set; }
        public VoucherTypeEnum VoucherType { get; set; }
        public ProductTypeEnum PTypeId { get; set; }
        public ProductLookup()
        {
            InitializeComponent();
            this.SelectedValueChanged += ProductLookup_SelectedValueChanged;
        }

        private void ProductLookup_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.SelectedValue == null)
            {
                this.buttonEdit1.Text = string.Empty;
            }
            if(Convert.ToInt32(this.SelectedValue)==0)
            {
                this.GroupDto = null;
                return;
            }
          
        }
        
        public void SetGroup(int id)
        {
           
            using (var _context = new KontoContext())
            {
                
                var model = _context.Products.Find(id);
                if (model != null)
                {
                    GroupDto = new ProductLookupDto();

                    (from pd in _context.Products
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
                     !pd.IsDeleted && pd.ItemType == "I" && pd.Id == model.Id
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
                 ).FirstOrDefault();

                    this.SelectedText = model.ProductName;
                    buttonEdit1.Text = model.ProductName;
                }
            }
        }
        public void SetEmpty()
        {
            this.SelectedValue = null;
            this.buttonEdit1.Text = string.Empty;
        }
        private void ShowList()
        {
            var frm = new ProductLkpWindow
            {
                SelectedTex = this.SelectedText,
                SelectedValue = Convert.ToInt32(this.SelectedValue),
                Tag = MenuId.Product_Master,
                PTypeId = this.PTypeId
            };
            frm.ShowDialog(this.Parent.Parent.Parent);
            if (frm.DialogResult == DialogResult.OK)
            {
                this.GroupDto = frm.customGridView1.GetRow(frm.customGridView1.FocusedRowHandle) as ProductLookupDto;
                this.SelectedText = GroupDto.ProductName;
                this.SelectedValue = frm.SelectedValue;
                this.PrimaryKey = frm.SelectedValue;
                this.buttonEdit1.Text = this.SelectedText;
                
            }
            this.Parent.SelectNextControl(this, true, true, true,false);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Delete && !this.RequiredField)
            {
                this.SelectedValue = null;
                this.buttonEdit1.Text = string.Empty;
                return true;

            }
            else if (keyData == Keys.Return)
            {
                if (Convert.ToInt32(this.SelectedValue) == 0 && this.RequiredField)
                {
                    ShowList();
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void buttonEdit1_Enter(object sender, EventArgs e)
        {
              this.buttonEdit1.SelectAll();
        }

        

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ShowList();
        }
    }
}
