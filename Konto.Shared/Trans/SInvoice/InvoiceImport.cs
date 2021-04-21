using Aspose.Cells;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Transaction;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Trans.SInvoice
{
    public partial class InvoiceImport : KontoForm
    {
        DataTable _dataTable;
        
        public InvoiceImport()
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

                var dtmain = (from row in _dataTable.AsEnumerable()
                              group row by row.Field<string>("BillNo") into grp
                              select new
                              {
                                  Id = grp.Key
                              }).ToList();


                var impList = new List<CityModel>();
                using (var db = new KontoContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = false;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    
                    var bm = new BillModel();
                    using (var tran = db.Database.BeginTransaction())
                    {
                        var intpro = 0;
                        try
                        {
                            splashScreenManager1.ShowWaitForm();
                            foreach (var item1 in dtmain)
                            {
                                intpro++;
                                double pro = ((double)intpro * 100) / (double)dtmain.Count;
                                splashScreenManager1.SetWaitFormDescription(string.Format("Completed {0} of {1}", intpro, dtmain.Count));

                                var dtdet = _dataTable.Select("BillNo='" + item1.Id + "'");
                                int fr = 0;
                                List<BillTransModel> btlist = new List<BillTransModel>();
                                foreach (System.Data.DataRow item in dtdet)
                                {
                                    if (fr == 0)
                                    {
                                        bm = new BillModel();
                                        string vouch = item["Voucher"].ToString();
                                        var vm = db.Vouchers.FirstOrDefault(x => x.VoucherName == vouch);
                                        if (vm == null)
                                        {
                                            MessageBox.Show("Please Create Voucher : " + item["Voucher"]);
                                            tran.Rollback();
                                            splashScreenManager1.CloseWaitForm();
                                            return;
                                        }
                                        string acname = item["Party"].ToString();
                                        var ac = db.Accs.FirstOrDefault(x => x.AccName == acname);
                                        if (ac == null)
                                        {
                                            splashScreenManager1.CloseWaitForm();
                                            MessageBox.Show("Please Create Party : " + item["Party"]);
                                            tran.Rollback();
                                            
                                            return;
                                            //bm.AccId = 4534;
                                            //bm.DelvAccId = 4534;
                                        }
                                        else
                                        {
                                            bm.AccId = ac.Id;
                                            bm.DelvAccId = ac.Id;
                                        }

                                        string bkac = item["Book"].ToString();
                                        var bk = db.Accs.FirstOrDefault(x => x.AccName == bkac);
                                        if (bk == null)
                                        {
                                            MessageBox.Show("Please Create Sales Ledger : " + item["Book"]);
                                            tran.Rollback();
                                            if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                                            return;
                                        }

                                        bm.VoucherId = vm.Id;

                                        bm.BookAcId = bk.Id;
                                        bm.VoucherNo = item["BillNo"].ToString();
                                        bm.BillNo = item["ChallanNo"].ToString();
                                        bm.VoucherDate = Convert.ToInt32(item["VoucherDate"]);
                                        bm.RcdDate = DateTime.ParseExact(item["ChallanDate"].ToString(), "yyyyMMdd", CultureInfo.CurrentCulture);
                                        bm.BillType = "Regular";

                                        bm.CreateUser = KontoGlobals.UserName;
                                        bm.CompId = KontoGlobals.CompanyId;
                                        bm.YearId = KontoGlobals.YearId;
                                        bm.BranchId = KontoGlobals.BranchId;
                                        bm.StoreId = 1;
                                        bm.DivisionId = 1;
                                        bm.TypeId = (int)VoucherTypeEnum.SaleInvoice;
                                        bm.TotalQty = Convert.ToDecimal(item["TotalQty"]);
                                        bm.TotalPcs = Convert.ToDecimal(item["TotalPcs"]);
                                        bm.GrossAmount = Convert.ToDecimal(item["TotalGross"]);
                                        bm.TotalAmount = Convert.ToDecimal(item["BillAmount"]);
                                        bm.Remarks = item["SalesRemark"].ToString();
                                        string stname = item["StateName"].ToString();
                                        if (!string.IsNullOrEmpty(stname))
                                        {
                                            var st = db.States.FirstOrDefault(x => x.StateName == stname);
                                            if (st != null)
                                                bm.StateId = st.Id;
                                        }
                                        db.Bills.Add(bm);
                                        db.SaveChanges();
                                    }

                                    var bt1 = new BillTransModel();
                                    bt1.BillId = bm.Id;
                                    string pname = item["Product"].ToString();
                                    var pd = db.Products.FirstOrDefault(x => x.ProductName == pname);
                                    if (pd == null)
                                    {
                                        // MessageBox.Show("Please Create Product : " + item["Product"]);
                                        //_tran.Rollback();
                                        //DXSplashScreen.Close();
                                        //return;
                                        bt1.ProductId = 1;
                                       // bt1.ProductName = "NA";
                                    }
                                    else
                                    {
                                        bt1.ProductId = pd.Id;
                                       // bt1.ProductName = pd.ProductName;
                                    }

                                    bt1.Pcs = Convert.ToInt32(item["Pcs"]);
                                    bt1.Cut = Convert.ToDecimal(item["Cut"]);
                                    bt1.Qty = Convert.ToDecimal(item["Qty"]);
                                    bt1.Rate = Convert.ToDecimal(item["Rate"]);

                                    string uname = item["Unit"].ToString();
                                    var um = db.Uoms.FirstOrDefault(x => x.UnitName == uname);
                                    if (um == null)
                                    {
                                        MessageBox.Show("Please Create Unit : " + item["Unit"]);
                                        tran.Rollback();
                                        if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                                        return;
                                    }
                                    bt1.UomId = um.Id;
                                    bt1.Total = Convert.ToDecimal(item["Total"]);
                                    bt1.Disc = Convert.ToDecimal(item["DiscPer"]);
                                    bt1.DiscAmt = Convert.ToDecimal(item["DiscAmount"]);
                                    bt1.Freight = Convert.ToDecimal(item["ServiceTaxAmount"]);
                                    bt1.CgstPer = Convert.ToDecimal(item["CgstPer"]);
                                    bt1.Cgst = Convert.ToDecimal(item["Cgst"]);
                                    bt1.SgstPer = Convert.ToDecimal(item["SgstPer"]);
                                    bt1.Sgst = Convert.ToDecimal(item["SgstAmt"]);
                                    bt1.IgstPer = Convert.ToDecimal(item["IgstPer"]);
                                    bt1.Igst = Convert.ToDecimal(item["IgstAmt"]);
                                    bt1.NetTotal = Convert.ToDecimal(item["NetTotal"]);
                                    bt1.Remark = item["ItemRemark"].ToString();
                                    btlist.Add(bt1);
                                    fr++;
                                }
                                db.BillTrans.AddRange(btlist);

                                bm.TotalPcs = btlist.Sum(x => x.Pcs);
                                bm.TotalQty = btlist.Sum(x => x.Qty);
                                db.SaveChanges();

                                var Billr = new BillRefModel();
                                //Insert or update in Billref table
                                LedgerEff.BillRefEntry("Debit", bm,(int) btlist.FirstOrDefault().ProductId, db);
                                //Insert or update in LedgerTrans table
                                LedgerEff.LedgerTransEntry("Debit", bm, db,btlist);


                                foreach (var item in btlist)
                                {
                                    bool IsIssue = true;
                                    string TableName = "SaleInvoice";

                                    var stockReq = db.Products.FirstOrDefault(k => k.Id == item.ProductId);

                                    //update serial sotck
                                    if (stockReq.SerialReq == "Yes" && stockReq.PTypeId == (int)ProductTypeEnum.FINISH)
                                    {
                                        if (string.IsNullOrEmpty(item.LotNo)) continue;
                                        var sr = db.ItemSerials.SingleOrDefault(x => x.SerialNo == item.LotNo);
                                        if (sr != null)
                                        {
                                            sr.IsActive = false; // remove stock of serials
                                        }
                                    }

                                    if (stockReq.StockReq == "No")
                                    {
                                        continue;
                                    }
                                    StockEffect.StockTransBillEntry(bm, item, IsIssue, TableName, db);
                                }

                            }

                            tran.Commit();
                            splashScreenManager1.CloseWaitForm();
                        }
                        catch (Exception ex)
                        {
                          if(splashScreenManager1.IsSplashFormVisible)  splashScreenManager1.CloseWaitForm();
                            Log.Error(ex.ToString(), "Sales Import");
                            MessageBox.Show(ex.ToString());
                            
                        }
                    }
                        
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                Log.Error(ex, "invoice import");
                MessageBox.Show(ex.ToString());
            }
        }

        private void ExcelSimpleButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Open Sales Excel File";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.ShowDialog();
            string filePath = openFileDialog1.FileName;
            if (string.IsNullOrEmpty(filePath)) return;

            Workbook workbook = new Workbook(filePath);
            Worksheet worksheet = workbook.Worksheets[0];
            var exp = new ExportTableOptions();
            exp.ExportAsString = true;

            exp.ExportColumnName = true;
            worksheet.Cells.DeleteBlankColumns();
            worksheet.Cells.DeleteBlankRows();
            _dataTable = worksheet.Cells.ExportDataTable(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, exp);
            this.gridControl1.DataSource = _dataTable;
        }

        
    }
}
