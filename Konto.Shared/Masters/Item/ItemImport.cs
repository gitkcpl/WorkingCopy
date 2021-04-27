using Aspose.Cells;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Masters;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Masters.Item
{
    public partial class ItemImport : KontoForm
    {
        DataTable _dataTable;
        
        public ItemImport()
        {
            InitializeComponent();
            excelSimpleButton.Click += ExcelSimpleButton_Click;
            okSimpleButton.Click += OkSimpleButton_Click;
            cancelSimpleButton.Click += CancelSimpleButton_Click;
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
                if (_dataTable == null || _dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("No Data Found for Import");
                    return;
                }
                var impList = new List<ProductModel>();
                var PriceList = new List<PriceModel>();
                var StockList = new List<StockBalModel>();
                using (var db = new KontoContext())
                {

                    var pid = db.Products.Max(k => k.Id);
                    int ProdctId = 0;
                    if (pid != 0)
                        ProdctId = pid;
                    else
                        ProdctId = 0;

                    foreach (DataRow item in _dataTable.Rows)
                    {
                        var _imp = new ProductModel();
                        
                        string _name = item[1].ToString().ToUpper();
                        var exist = db.Products.FirstOrDefault(x => x.ProductName == _name);
                        
                        if (exist!=null) // check for exist product
                        {
                            _imp = exist;
                        }

                        if (item[1].ToString().Length < 2)
                            continue;

                        
                        _imp.ProductName = item[1].ToString().ToUpper();
                        _imp.ProductCode = item[0].ToString().ToUpper();


                        
                        if(string.IsNullOrEmpty(item[15].ToString()))
                            _imp.ProductDesc = item[1].ToString().ToUpper();
                        else
                            _imp.ProductDesc = item[15].ToString().ToUpper();


                        string typName = item[2].ToString();
                        var typ = db.ProductTypes.FirstOrDefault(k => k.TypeName.ToUpper() == typName.ToUpper());
                        if (typ != null)
                        {
                            _imp.PTypeId = typ.Id;
                        }
                        else
                        {
                            _imp.PTypeId = 1;
                        }
                        _imp.HsnCode = item[3].ToString(); // hsncode
                        
                        string unit = item[5].ToString(); //unit
                        var uom = db.Uoms.FirstOrDefault(k => k.UnitName.ToUpper() == unit.ToUpper());
                        if (uom != null)
                        {
                            _imp.PurUomId = uom.Id;
                            _imp.UomId = uom.Id;
                        }
                        else
                        {
                            _imp.PurUomId = 1;
                            _imp.UomId = 1;
                        }

                        string tax = item[4].ToString(); //gst
                        var gst = db.TaxMasters.FirstOrDefault(k => k.TaxName.ToUpper() == tax.ToUpper());
                        if (gst != null)
                            _imp.TaxId = gst.Id;
                        else
                            _imp.TaxId = 6; // nil rated

                        //ProdctId = ProdctId + 1;
                       // _imp.Id = ProdctId;
                        _imp.ItemType = "I";
                        string _group = item[6].ToString();
                        
                        var _grp = db.PGroups.FirstOrDefault(x => x.GroupName.ToUpper() == _group.ToUpper());
                        if (_grp != null)
                           _imp.GroupId = _grp.Id;
                        else
                            _imp.GroupId = 1;


                        var _subname = item[7].ToString();
                        if (!string.IsNullOrEmpty(_subname))
                        {
                            var _subgrp = db.PSubGroups.FirstOrDefault(x => x.SubName.ToUpper() == _subname.ToUpper());
                            if (_subgrp == null)
                            {
                                _subgrp = new PSubGroupModel();
                                _subgrp.IsActive = true;
                                _subgrp.SubName = _subname;
                                _subgrp.PGroupId = _imp.GroupId;
                                db.PSubGroups.Add(_subgrp);
                                db.SaveChanges();
                            }
                            _imp.SubGroupId = _subgrp.Id;
                        }
                        
                        if(_imp.SubGroupId==0)
                            _imp.SubGroupId = 1;


                        var _category = item[8].ToString();
                        if (!string.IsNullOrEmpty(_category))
                        {
                            var _cat = db.CategroyModels.FirstOrDefault(x => x.CatName.ToUpper() == _category.ToUpper());
                            if (_cat == null && _category.Length>1)
                            {
                                _cat = new PCategroyModel();
                                _cat.CatName = _category;
                                _cat.IsActive = true;
                                db.CategroyModels.Add(_cat);
                                db.SaveChanges();
                            }
                                _imp.CategoryId = _cat.Id;
                        }

                        if(_imp.CategoryId==0)
                            _imp.CategoryId = 1;
                        
                      
                        var _color = item[16].ToString();
                        if (!string.IsNullOrEmpty(_color))
                        {
                            var _col = db.ColorModels.FirstOrDefault(x => x.ColorName.ToUpper() == _color.ToUpper());
                            if (_col == null && _color.Length>1)
                            {
                                _col = new ColorModel();
                                _col.ColorName = _color;
                                _col.IsActive = true;
                                db.ColorModels.Add(_col);
                                db.SaveChanges();
                            }
                                _imp.ColorId = _col.Id;
                        }

                        if(_imp.ColorId==0)
                            _imp.ColorId = 1;

                        var _brand = item[17].ToString();

                        if (!string.IsNullOrEmpty(_brand))
                        {
                            var br = db.Brands.FirstOrDefault(x => x.BrandName.ToUpper() == _brand.ToUpper());
                            if (br == null && _brand.Length >1)
                            {
                                br = new BrandModel();
                                br.BrandName = _brand;
                                br.IsActive = true;
                                db.Brands.Add(br);
                                db.SaveChanges();
                            
                            }
                            _imp.BrandId = br.Id;
                        }

                        if(_imp.BrandId==0)
                            _imp.BrandId = 1;

                        var _size = item[18].ToString();

                        if (!string.IsNullOrEmpty(_size)){
                            var _sz= db.SizeModels.FirstOrDefault(x => x.SizeName.ToUpper() == _size.ToUpper());
                            if (_sz == null)
                            {
                                _sz = new PSizeModel();
                                _sz.SizeName = _size;
                                db.SizeModels.Add(_sz);
                                db.SaveChanges();
                            }
                                _imp.SizeId = _sz.Id;
                        }

                        if(_imp.SizeId==0)
                            _imp.SizeId = 1;

                        if (item[19] != null)
                            _imp.Cut = Convert.ToInt32(item[19].ToString()); //pcs per pack
                        else
                            _imp.Cut = 0;

                        _imp.StyleId = 1;
                        
                        _imp.ItemType = "I";
                        _imp.ActualCost = Convert.ToDecimal(item[12]);
                        _imp.IsActive = true;

                        if(_imp.Id == 0)
                            impList.Add(_imp);
                        else
                        {
                            var price = db.Prices.FirstOrDefault(x => x.ProductId == _imp.Id);


                            if (price != null)
                            {
                                price.Mrp = Convert.ToDecimal(item[9]);
                                price.DealerPrice = Convert.ToDecimal(item[10]);
                                price.SaleRate = Convert.ToDecimal(item[11]);
                                price.Rate1 = Convert.ToDecimal(item[13]);
                                price.Rate2 = Convert.ToDecimal(item[14]);

                                price.IssueQty = 0;
                                price.Qty = 0;
                                price.BranchId = KontoGlobals.BranchId;
                             //   price.CreateUser = KontoGlobals.UserName;
                            }
                        }

                        

                    }
                    if (impList.Count > 0)
                    {
                        string itemname = "";
                        using (var trans = db.Database.BeginTransaction())
                        {
                            try
                            {
                                db.Products.AddRange(impList);

                                db.SaveChanges();
                                foreach (var item in impList)
                                {
                                    itemname = item.ProductName;
                                    //var _Dt = _dataTable .Select("ProductName='" + item.ProductName + "'");

                                    var _dt = _dataTable.AsEnumerable().Where(x => x.Field<string>("PRODUCTNAME") == item.ProductName).FirstOrDefault();
                                    var price = new PriceModel();
                                    price.ProductId = item.Id;
                                    if (_dt != null)
                                    {
                                        price.Mrp = Convert.ToDecimal(_dt[9]);
                                        price.DealerPrice = Convert.ToDecimal(_dt[10]);
                                        price.SaleRate = Convert.ToDecimal(_dt[11]);
                                        price.Rate1 = Convert.ToDecimal(_dt[13]);
                                        price.Rate2 = Convert.ToDecimal(_dt[14]);
                                    }
                                    price.IssueQty = 0;
                                    price.Qty = 0;
                                    price.BranchId = KontoGlobals.BranchId;
                                    price.CreateUser = KontoGlobals.UserName;
                                    PriceList.Add(price);

                                    //ProductBal

                                    var complist = db.Companies.Where(p => p.IsActive && !p.IsDeleted).ToList();
                                    var yearlist = db.FinYears.Where(x => x.IsActive == true && x.IsDeleted == false).ToList();
                                    var storelist = db.Stores.Where(x => x.IsActive && !x.IsDeleted).ToList();
                                    var branchlist = db.Branches.Where(x => x.IsActive && !x.IsDeleted).ToList();
                                    foreach (var comp in complist)
                                    {
                                        foreach (var yr in yearlist)
                                        {
                                            foreach (var branch in branchlist)
                                            {
                                                // foreach (var store in storelist)
                                                // {
                                                StockBalModel _model = new StockBalModel();

                                                _model.ProductId = item.Id;
                                                _model.ItemCode = item.RowId;
                                                _model.BalQty = 0;
                                                _model.CompanyId = comp.Id;
                                                _model.YearId = yr.Id;
                                                _model.BranchId = branch.Id;
                                                _model.GodownId = KontoGlobals.GodownId;
                                                _model.RowId = Guid.NewGuid();
                                                _model.CreateUser = KontoGlobals.UserName;
                                                _model.CreateDate = DateTime.Now;
                                                _model.OpNos = 0;
                                                _model.OpQty = 0;

                                                StockList.Add(_model);
                                                //}
                                            }
                                        }
                                    }
                                   
                                }
                                db.Prices.AddRange(PriceList);

                                db.StockBals.AddRange(StockList);
                                db.SaveChanges();
                                trans.Commit();
                                
                            }
                            catch (Exception ex)
                            {
                                trans.Rollback();
                                Log.Error(ex, "product imprt under transaction" + itemname);
                            }
                            
                        }
                    }
                    else
                   // {
                        //MessageBox.Show("No Record Found to be import or already Exists");
                   // }

                    db.SaveChanges();
                    MessageBox.Show("Imported Successfully");//
                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "ac group import");
                MessageBox.Show(ex.ToString());
            }
        }

        private void ExcelSimpleButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Open Account Excel File";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.ShowDialog();
            string filePath = openFileDialog1.FileName;
            if (string.IsNullOrEmpty(filePath)) return;

            Workbook workbook = new Workbook(filePath);
            Worksheet worksheet = workbook.Worksheets[0];
            var exp = new ExportTableOptions
            {
                ExportAsString = true,

                ExportColumnName = true
            };
            worksheet.Cells.DeleteBlankColumns();
            worksheet.Cells.DeleteBlankRows();
            _dataTable = worksheet.Cells.ExportDataTable(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, exp);
            this.gridControl1.DataSource = _dataTable;
        }
    }
}
