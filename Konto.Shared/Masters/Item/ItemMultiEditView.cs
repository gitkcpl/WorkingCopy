using DevExpress.XtraEditors.Repository;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Masters.Dtos;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Shared.Masters.Item
{
    public partial class ItemMultiEditView : KontoForm
    {
        RepositoryItemLookUpEdit subGroupRepoLookup = new RepositoryItemLookUpEdit();
        RepositoryItemLookUpEdit brandRepoLookup = new RepositoryItemLookUpEdit();
        RepositoryItemLookUpEdit sizeRepoLookup = new RepositoryItemLookUpEdit();
        RepositoryItemLookUpEdit catRepoLookup = new RepositoryItemLookUpEdit();
        RepositoryItemLookUpEdit colorRepoLookup = new RepositoryItemLookUpEdit();
        List<RateChangeDto> _UpdatedItem = new List<RateChangeDto>();
        public ItemMultiEditView()
        {
            InitializeComponent();
            this.Load += ItemMultiEditView_Load;
            this.applySimpleButton.Click += ApplySimpleButton_Click;
            this.gridView1.CellValueChanged += GridView1_CellValueChanged;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                using(var db = new KontoContext())
                {
                    db.Database.CommandTimeout = 0;

                    var rws = gridView1.GetSelectedRows();
                    foreach (var rw in rws)
                    {
                        var row = gridView1.GetRow(rw) as RateChangeDto;
                        if (row == null) continue;
                        var pd = db.Prices.SingleOrDefault(x => x.ProductId == row.Id);
                        pd.SaleRate = row.SaleRate;
                        pd.Rate1 = row.BulkRate;
                        pd.Rate2 = row.SemiBulkRate;
                    }

                    foreach (var row in _UpdatedItem)
                    {
                        var pd = db.Products.Find(row.Id);
                        pd.ProductCode = row.ProductCode;
                        pd.ProductName = row.ProductName;
                        pd.BarCode = row.BarCode;
                        pd.BatchReq = row.BatchReq;
                        pd.BrandId = row.Brand;
                        pd.CategoryId = row.Category;
                        pd.CheckNegative = row.ChkNegative;
                        pd.ColorId = row.Color;
                        pd.Cut = row.PackQty;
                        pd.HsnCode = row.HsnCode;
                        pd.MaxLevel = row.MaxLevel;
                        pd.MaxOrdQty = row.MaxOrdQty;
                        pd.MinOrdQty = row.MinOrdQty;
                        pd.Price1 = row.ProfitPer;
                        pd.PurDisc = row.PurDisc;
                        pd.Rol = row.Rol;
                        pd.SaleDisc = row.SaleDisc;
                        pd.SaleRateTaxInc = row.SaleRateTaxInc;
                        pd.SerialReq = row.SerialReq;
                        pd.SizeId = row.Size;
                        pd.SubGroupId = row.SubGroup;
                        pd.TaxId = row.TaxType;

                        var pr = db.Prices.SingleOrDefault(x => x.ProductId == row.Id);
                        pr.BatchNo = row.StyleNo;
                        pr.DealerPrice = row.DealerPrice;
                        pr.SaleRate = row.SaleRate;
                        pr.Mrp = row.Mrp;
                        pr.Rate1 = row.BulkRate;
                        pr.Rate2 = row.SemiBulkRate;

                        var bchs = db.ItemBatches.Where(x => x.ProductId == row.Id).ToList(); ;

                        if (bchs.Count==1)
                        {
                            var bch = bchs.FirstOrDefault();
                            bch.BulkRate = row.BulkRate;
                            bch.SemiBulkRate = row.SemiBulkRate;
                            bch.DealerPrice = row.DealerPrice;
                            bch.SaleRate = row.SaleRate;
                        }

                    }

                    db.SaveChanges();

                    MessageBox.Show("Product Updated !!");
                    _UpdatedItem = new List<RateChangeDto>();
                   
                }
            }
            catch (Exception ex)
            {
                
                Log.Error(ex, "rate change");
                MessageBox.Show(ex.ToString());
            }
        }

        private void GridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            _UpdatedItem.Add(gridView1.GetRow(e.RowHandle) as RateChangeDto);
        }

        private void ApplySimpleButton_Click(object sender, EventArgs e)
        {
            if (kontoSpinEdit1.Value == 0) return;

            var rws = gridView1.GetSelectedRows();
            foreach (var rw in rws)
            {
                var row = gridView1.GetRow(rw) as RateChangeDto;
                if (radioGroup1.EditValue.ToString() == "SR")
                    row.SaleRate = decimal.Round(row.DealerPrice + (row.DealerPrice * kontoSpinEdit1.Value / 100), 2, MidpointRounding.AwayFromZero);
                else if(radioGroup1.EditValue.ToString()=="BR")
                    row.BulkRate = decimal.Round(row.DealerPrice + (row.DealerPrice * kontoSpinEdit1.Value / 100), 2, MidpointRounding.AwayFromZero);
                else
                    row.SemiBulkRate = decimal.Round(row.DealerPrice + (row.DealerPrice * kontoSpinEdit1.Value / 100), 2, MidpointRounding.AwayFromZero);
            }

            gridControl1.RefreshDataSource();
        }

        private void ItemMultiEditView_Load(object sender, EventArgs e)
        {
            try
            {
                List<RateChangeDto> _rates = new List<RateChangeDto>();

                using(var _context = new KontoContext())
                {
                    _context.Database.CommandTimeout = 0;

                    // tax types
                   



                    _rates = (from pd in _context.Products
                              join pr in _context.Prices on pd.Id equals pr.ProductId
                              orderby pd.BarCode,pd.ProductName
                              select new RateChangeDto() { 
                              BarCode = pd.BarCode,BatchReq= pd.BatchReq,Brand= pd.BrandId,
                              BulkRate= pr.Rate1,Category= pd.CategoryId,ChkNegative= pd.CheckNegative,
                              Color= pd.ColorId,DealerPrice= pr.DealerPrice,Group= pd.GroupId,
                              HsnCode = pd.HsnCode,MaxLevel= pd.MaxLevel,MaxOrdQty= pd.MaxOrdQty,Id=pd.Id,
                              MinLevel= pd.MinLevel,MinOrdQty= pd.MinOrdQty,Mrp= pr.Mrp,PackQty= pd.Cut,
                              ProductCode= pd.ProductCode,ProductName= pd.ProductName,ProductType= pd.PTypeId,
                              ProfitPer= pd.Price1,PurDisc= pd.PurDisc,RatePerQty= pd.Price2,Rol= pd.Rol,
                              SaleDisc= pd.SaleDisc,SaleRate= pr.SaleRate,SaleRateTaxInc= pd.SaleRateTaxInc,
                              SemiBulkRate= pr.Rate2,SerialReq= pd.SerialReq,Size= pd.StyleId,
                              StyleNo= pr.BatchNo,SubGroup= pd.SubGroupId,TaxType= pd.TaxId
                              }).ToList();
                    
                    rateChangeDtoBindingSource.DataSource = _rates;

                    SetRepositoryLookup();

                    var _taxes = (from tx in _context.TaxMasters
                                  orderby tx.TaxName
                                  select new BaseLookupDto { DisplayText = tx.TaxName, Id = tx.Id }).ToList();

                    taxRepositoryItemLookUpEdit.DataSource = _taxes;

                    // product types
                    var _types = (from tx in _context.ProductTypes
                                  orderby tx.TypeName
                                  select new BaseLookupDto { DisplayText = tx.TypeName, Id = tx.Id }).ToList();
                    typeRepositoryItemLookUpEdit.DataSource = _types;

                    // product group
                    var _groups = (from tx in _context.PGroups
                                  orderby tx.GroupName
                                  select new BaseLookupDto { DisplayText = tx.GroupName, Id = tx.Id }).ToList();
                    groupRepositoryItemLookUpEdit.DataSource = _groups;

                    // product sub group
                    var _subgroups = (from tx in _context.PSubGroups
                                   orderby tx.SubName
                                   select new BaseLookupDto { DisplayText = tx.SubName, Id = tx.Id }).ToList();
                    subGroupRepoLookup.DataSource = _subgroups;

                    // product brand
                    var _brands = (from tx in _context.Brands
                                   orderby tx.BrandName
                                   select new BaseLookupDto { DisplayText = tx.BrandName, Id = tx.Id }).ToList();
                    brandRepoLookup.DataSource = _brands;

                    // product color
                    var _colors = (from tx in _context.ColorModels
                                   orderby tx.ColorName
                                   select new BaseLookupDto { DisplayText = tx.ColorName, Id = tx.Id }).ToList();
                    colorRepoLookup.DataSource = _colors;

                    // product category
                    var _cats = (from tx in _context.CategroyModels
                                   orderby tx.CatName
                                   select new BaseLookupDto { DisplayText = tx.CatName, Id = tx.Id }).ToList();
                    catRepoLookup.DataSource = _cats;

                    // product size
                    var _sizes = (from tx in _context.SizeModels
                                   orderby tx.SizeName
                                   select new BaseLookupDto { DisplayText = tx.SizeName, Id = tx.Id }).ToList();
                    sizeRepoLookup.DataSource = _sizes;
                }

                
            }
            catch (Exception ex)
            {

                
            }
        }

        private void SetRepositoryLookup()
        {
            subGroupRepoLookup.ImmediatePopup = true;
            subGroupRepoLookup.ShowFooter = false;
            subGroupRepoLookup.ShowHeader = false;
            subGroupRepoLookup.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DisplayText"));
            subGroupRepoLookup.DisplayMember = "DisplayText";
            subGroupRepoLookup.ValueMember = "Id";
            subGroupRepoLookup.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            subGroupRepoLookup.AppearanceDropDown.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridView1.Columns["SubGroup"].ColumnEdit = subGroupRepoLookup;


            brandRepoLookup.ImmediatePopup = true;
            brandRepoLookup.ShowFooter = false;
            brandRepoLookup.ShowHeader = false;
            brandRepoLookup.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DisplayText"));
            brandRepoLookup.DisplayMember = "DisplayText";
            brandRepoLookup.ValueMember = "Id";
            brandRepoLookup.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            brandRepoLookup.AppearanceDropDown.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridView1.Columns["Brand"].ColumnEdit = brandRepoLookup;

            catRepoLookup.ImmediatePopup = true;
            catRepoLookup.ShowFooter = false;
            catRepoLookup.ShowHeader = false;
            catRepoLookup.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DisplayText"));
            catRepoLookup.DisplayMember = "DisplayText";
            catRepoLookup.ValueMember = "Id";
            catRepoLookup.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            catRepoLookup.AppearanceDropDown.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridView1.Columns["Category"].ColumnEdit = catRepoLookup;

            sizeRepoLookup.ImmediatePopup = true;
            sizeRepoLookup.ShowFooter = false;
            sizeRepoLookup.ShowHeader = false;
            sizeRepoLookup.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DisplayText"));
            sizeRepoLookup.DisplayMember = "DisplayText";
            sizeRepoLookup.ValueMember = "Id";
            sizeRepoLookup.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            sizeRepoLookup.AppearanceDropDown.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridView1.Columns["Size"].ColumnEdit = sizeRepoLookup;

            colorRepoLookup.ImmediatePopup = true;
            colorRepoLookup.ShowFooter = false;
            colorRepoLookup.ShowHeader = false;
            colorRepoLookup.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DisplayText"));
            colorRepoLookup.DisplayMember = "DisplayText";
            colorRepoLookup.ValueMember = "Id";
            colorRepoLookup.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            colorRepoLookup.AppearanceDropDown.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridView1.Columns["Color"].ColumnEdit = colorRepoLookup;

        }
    }
}
