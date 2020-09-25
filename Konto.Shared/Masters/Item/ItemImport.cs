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
                        var exist = db.Products.Any(x => x.ProductName == _name);
                        if (exist)
                            continue;
                        if (item[1].ToString().Length < 2)
                            continue;

                        _imp.ProductName = item[1].ToString().ToUpper();
                        _imp.ProductCode = item[0].ToString().ToUpper();
                        _imp.ProductDesc = item[1].ToString().ToUpper();
                        
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

                        ProdctId = ProdctId + 1;
                        _imp.Id = ProdctId;
                        _imp.ItemType = "I";
                        string _group = item[6].ToString();
                        
                        var _grp = db.PGroups.FirstOrDefault(x => x.GroupName.ToUpper() == _group.ToUpper());
                        if (_grp != null)
                           _imp.GroupId = _grp.Id;
                        else
                            _imp.GroupId = 1;

                        _imp.SubGroupId = 1;
                        _imp.CategoryId = 1;
                        _imp.BrandId = 1;
                        _imp.ColorId = 1;
                        _imp.StyleId = 1;
                        _imp.SizeId = 1;
                         _imp.StyleId = 1;
                        _imp.ItemType = "I";
                        //_imp.GroupCode = "00";
                        _imp.IsActive = true;
                        impList.Add(_imp);

                    }
                    if (impList.Count > 0)
                    {
                        db.Products.AddRange(impList);
                        using (var trans = db.Database.BeginTransaction())
                        {
                            try
                            {
                                db.SaveChanges();
                                foreach (var item in impList)
                                {
                                    var price = new PriceModel();
                                    price.ProductId = item.Id;
                                    price.DealerPrice = 0;
                                    price.IssueQty = 0;
                                    price.Mrp = 0;
                                    price.Qty = 0;
                                    price.BranchId = KontoGlobals.BranchId;
                                    price.CreateUser = KontoGlobals.UserName;
                                    PriceList.Add(price);

                                    //ProductBal
                                    var bal = new StockBalModel();
                                    bal.ProductId = item.Id;
                                    bal.ItemCode = item.RowId;
                                    bal.BalQty = 0;
                                    bal.CompanyId = KontoGlobals.CompanyId;
                                    bal.YearId = KontoGlobals.YearId;
                                    bal.BranchId = KontoGlobals.BranchId;
                                   // bal.RowId = Guid.NewGuid();
                                    //CreateUser = KontoGlobals.UserName,
                                    bal.OpNos = 0;
                                    bal.OpQty = 0;

                                    StockList.Add(bal);
                                }
                                db.Prices.AddRange(PriceList);

                                db.StockBals.AddRange(StockList);
                                db.SaveChanges();
                                trans.Commit();
                                MessageBox.Show("Imported Successfully");
                            }
                            catch (Exception ex)
                            {
                                trans.Rollback();
                                Log.Error(ex, "product imprt under transaction");
                            }
                            
                        }
                    }
                    else
                    {
                        MessageBox.Show("No Record Found to be import or already Exists");
                    }
                        
                    
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
