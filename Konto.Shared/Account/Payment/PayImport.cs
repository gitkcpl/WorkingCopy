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

namespace Konto.Shared.Account.Payment
{
    public partial class PayImport : KontoForm
    {
        DataTable _dataTable;
        
        public PayImport()
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

           
                using (var db = new KontoContext())
                {
                    var pid = 0;
                    var pd = db.Bills.Where(k => k.Id > 0).ToList();
                    if (pd.Count > 0)
                    {
                        pid = db.Bills.Max(k => k.Id);
                    }

                    BillModel bill = new BillModel();
                    BillTransModel bt = new BillTransModel();
                    BillRefModel billr = new BillRefModel();
                    var BillTrans = new List<BillTransModel>();
                    var BillList = new List<BillModel>();
                    var BillRef = new List<BillRefModel>();
                    db.Configuration.AutoDetectChangesEnabled = false;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    using(var tran = db.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (System.Data.DataRow dr in _dataTable.Rows)
                            {
                                pid = pid + 1;

                                bill = new BillModel();
                                bt = new BillTransModel();
                                billr = new BillRefModel();

                                var acc = dr["BankCash"].ToString();
                                var acId = db.Accs.FirstOrDefault(k => k.AccName == acc).Id;
                                if (acId > 0)
                                {
                                    bill.AccId = acId;
                                }
                                else
                                {
                                    BillList = new List<BillModel>();
                                    BillTrans = new List<BillTransModel>();
                                    BillRef = new List<BillRefModel>();
                                    MessageBox.Show("Account " + acc + " Not Exist In Database!");
                                    return;
                                }

                                bill.BillType = "Regular";
                                bill.VoucherNo = dr["VoucherNo"].ToString();
                                bill.BillNo = dr["VoucherNo"].ToString();
                              //  bill.VDate = Convert.ToDateTime(dr["Date"].ToString());
                                bill.TotalAmount = Convert.ToDecimal(dr["Amount"].ToString());
                                //bill.Remarks = dr["Remark"].ToString();
                                //      bill.RefNo = dr["VoucherNo"].ToString();

                                if (bill.TotalAmount == 0)
                                {
                                    BillList = new List<BillModel>();
                                    BillTrans = new List<BillTransModel>();
                                    BillRef = new List<BillRefModel>();
                                    MessageBox.Show("Please check Amount is O for the party ");
                                    return;
                                }


                                var vchr = db.Vouchers.FirstOrDefault(k => k.VTypeId == (int)VoucherTypeEnum.PaymentVoucher);
                                bill.VoucherId = vchr.Id;
                                if (bill.VoucherNo == "")
                                {
                                    bill.VoucherNo = DbUtils.NextSerialNo(bill.VoucherId, db);
                                }


                                bill.VoucherDate = Convert.ToInt32(dr["Date"]);

                                bill.DivisionId = 1;
                                bill.YearId = KontoGlobals.YearId;
                                bill.CompId = KontoGlobals.CompanyId;
                                bill.IsActive = true;
                                bill.IsDeleted = false;
                                bill.CreateUser = KontoGlobals.UserName;
                                bill.CreateDate = DateTime.Now;
                                bill.TypeId = (int)VoucherTypeEnum.PaymentVoucher;

                                // Set for transaction 
                                var ac = dr["Account"].ToString();

                                var p = db.Accs.FirstOrDefault(k => k.AccName == ac);
                                if (p != null)
                                {
                                    bt.ToAccId = p.Id;
                                }
                                else
                                {
                                    BillList = new List<BillModel>();
                                    BillTrans = new List<BillTransModel>();
                                    BillRef = new List<BillRefModel>();
                                    //DXSplashScreen.Close();
                                    MessageBox.Show("Account " + ac + " Not Exist in Database!");

                                    return;
                                }

                                bt.Qty = 1;
                                bt.Rate = 1;
                                bt.RpType = "Account";  //dr["PayType"].ToString();
                                bt.ChequeNo = dr["ChequeNo"].ToString();
                                bt.RefBankId =1;
                                //bt.ChequeDate = Convert.ToDateTime(dr["ChequeDate"].ToString());
                                //var refb = dr["ChequeBank"].ToString();
                                //if (refb != "")
                                //{
                                //    var refbId = db.RefBanks.FirstOrDefault(k => k.BankName == refb).Id;
                                //    if (refbId > 0)
                                //    {
                                //        bt.RefBankId = refbId;
                                //    }
                                //}

                                bt.Remark = dr["Narration"].ToString();
                                bt.Total = Convert.ToDecimal(dr["Amount"].ToString());
                                bt.NetTotal = Convert.ToDecimal(dr["Amount"].ToString());


                                bill.Id = pid;
                                BillList.Add(bill);

                                bt.BillId = bill.Id;
                                bt.IsActive = true;
                                bt.IsDeleted = false;
                                bt.CreateUser = KontoGlobals.UserName;
                                bt.CreateDate = DateTime.Now;
                                BillTrans.Add(bt);
                                //  await transrepo.AddAsyn(bt);


                            }
                            splashScreenManager1.ShowWaitForm();
                            var intpro = 0;
                            foreach (var b in BillList)
                            {
                                //Show Progress
                                intpro++;
                                double pro = ((double)intpro * 100) / (double)BillList.Count;
                                //inc = inc + pro;
                               
                                splashScreenManager1.SetWaitFormDescription(string.Format("Completed {0} of {1}", intpro, BillList.Count));

                                //Sum for multiple order of same bill
                                var btrans = BillTrans.Where(k => k.BillId == b.Id).ToList();
                                var totl = btrans.Sum(k => k.Total);
                                b.GrossAmount = Convert.ToDecimal(totl);

                                var nettotl = btrans.Sum(k => k.NetTotal);
                                b.TotalAmount = Convert.ToDecimal(nettotl);

                                var totlQty = btrans.Sum(k => k.Qty);
                                b.TotalQty = Convert.ToDecimal(totlQty);
                                db.Bills.Add(b);
                                db.SaveChanges();

                                var Billr = new BillRefModel();

                                //var Trans = new List<BillTransModel>(btrans);

                                //Insert or update in Billref table for Bill entry
                               

                                foreach (var t in btrans)
                                {
                                    t.BillId = b.Id;
                                    db.BillTrans.Add(t);
                                }
                                db.SaveChanges();
                                LedgerEff.BillRefEntrypayrec("Debit", b, btrans, new List<Data.Models.Transaction.Dtos.BankTransDto>(), db);
                                //Insert or update in LedgerTrans table
                                LedgerEff.LedgerTransEntryRecPay("Debit", b, db, btrans, new List<Data.Models.Transaction.Dtos.PendBillListDto>());
                            }

                            db.SaveChanges();

                            tran.Commit();
                            if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                            MessageBox.Show("Imported Successfully");
                        }
                        catch (Exception ex)
                        {
                            if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                            Log.Error(ex, "Receipt Import");
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
            openFileDialog1.Title = "Open Sales Return Excel File";
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
