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
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.OpBill
{
    public partial class OpBillImport : KontoForm
    {
        DataTable _dataTable;
        
        public OpBillImport()
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
                var BillList = new List<BillModel>();
                var BillRef = new List<BillRefModel>();
                

                using (var db = new KontoContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = false;
                    db.Configuration.ValidateOnSaveEnabled = false;

                    using (var _trans = db.Database.BeginTransaction()){

                        try
                        {
                            var intpro = 0;
                            splashScreenManager1.ShowWaitForm();
                            foreach (DataRow item in _dataTable.Rows)
                            {
                                intpro++;
                                double pro = ((double)intpro * 100) / (double) _dataTable.Rows.Count;
                                splashScreenManager1.SetWaitFormDescription(string.Format("Completed {0} of {1}", intpro, _dataTable.Rows.Count));

                                var bill = new BillModel();
                                string partyname = item[1].ToString();

                                bill.BillNo = item[2].ToString();

                                var p = db.Accs.FirstOrDefault(k => k.AccName == partyname);
                                if (p != null)
                                {
                                    bill.AccId = p.Id;
                                }
                                else
                                {
                                    continue;
                                }

                                //var exist = db.Bills.Any(k => k.AccId == bill.AccId && k.BillNo == bill.BillNo && k.IsDeleted == false);
                                //if (exist)
                                //    continue;

                                var agent = item[6].ToString();
                                var ag = db.Accs.FirstOrDefault(k => k.AccName == agent);
                                if (ag != null)
                                {
                                    bill.AgentId = ag.Id;
                                }
                                bill.BillType = item[0].ToString();
                                bill.TotalAmount = Convert.ToDecimal(item[9]);
                                if (bill.TotalAmount == 0) continue;
                                var v = item[3].ToString();
                                if (bill.BillNo == "")
                                {
                                    bill.BillNo = "NA";
                                }
                                
                                 var vchr = db.Vouchers.FirstOrDefault(k => k.VTypeId == 26);

                                bill.VoucherId = vchr.Id;
                                bill.VoucherNo = DbUtils.NextSerialNo(bill.VoucherId, db);

                                bill.VDate = Convert.ToDateTime(item[3].ToString());
                                bill.VoucherDate = Convert.ToInt32(bill.VDate.ToString("yyyyMMdd"));
                                bill.RefNo = bill.BillNo;
                                bill.TotalQty = Convert.ToDecimal(item[7]);
                                bill.ExchRate = Convert.ToDecimal(item[8]);
                                bill.GrossAmount = Convert.ToDecimal(item[10]);
                                bill.TotalPcs = Convert.ToDecimal(item[11]);
                                bill.TdsAmt = Convert.ToDecimal(item[12]);
                                bill.DivisionId = 1;
                                bill.YearId = KontoGlobals.YearId;
                                bill.CompId = KontoGlobals.CompanyId;
                                bill.IsActive = true;
                                db.Bills.Add(bill);
                                db.SaveChanges();

                                string Type = "Debit";
                                if (bill.BillType.ToUpper() == "SALES" || bill.BillType.ToUpper() == "DRNOTE" || bill.BillType.ToUpper() == "RECEIPT")
                                {
                                    Type = "Debit";
                                }
                                else if (bill.BillType.ToUpper() == "PURCHASE" || bill.BillType.ToUpper() == "CRNOTE" || bill.BillType.ToUpper() == "PAYMENT")
                                {
                                    Type = "Credit";
                                }


                                var billr = new BillRefModel();
                                billr.BillId = bill.Id;
                                billr.CompanyId = KontoGlobals.CompanyId;
                                billr.YearId = KontoGlobals.YearId;
                                billr.BranchId = KontoGlobals.BranchId;
                                billr.AccountId = bill.AccId;
                                billr.GrossAmt = bill.TotalAmount;
                                billr.BillAmt = bill.TotalAmount;
                                billr.AdjustAmt = bill.GrossAmount;
                                billr.RetAmt = (decimal)bill.TotalPcs;
                                billr.TdsAmt = bill.TdsAmt;
                                billr.BillTransId = bill.Id;
                                billr.BillVoucherId = bill.VoucherId;

                                billr.BillNo = bill.BillNo;
                                billr.VoucherNo = bill.VoucherNo;
                                billr.VoucherDate = bill.VoucherDate;
                                billr.RefType = Type;

                                billr.CreateDate = DateTime.Now;
                                billr.CreateUser = KontoGlobals.UserName;
                                db.BillRefs.Add(billr);
                                db.SaveChanges();


                            }
                            _trans.Commit();
                            if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                            MessageBox.Show("Imported Successfully");
                        }
                        catch (Exception ex)
                        {
                            if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                            Log.Error(ex, "op bill import");
                            _trans.Rollback();
                            MessageBox.Show(ex.ToString());
                            
                        }
                        
                    }

                    //if (BillList.Count > 0)
                    //{
                    //   // db.Bills.AddRange(BillList);
                    //    //db.BillRefs.AddRange(BillRef);
                    //   // db.SaveChanges();
                       
                    //}
                    //else
                    //{
                    //    MessageBox.Show("No Record Found to be import or already Exists");
                    //}
                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "bill import");
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
