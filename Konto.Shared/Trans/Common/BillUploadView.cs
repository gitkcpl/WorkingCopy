using Konto.Core.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.Cells;
using Konto.Data;
using Konto.Data.Models.Masters.Dtos;
using Konto.Data.Models.Transaction;
using Syncfusion.Windows.Forms;
using Konto.App.Shared;
using Serilog;
using Konto.App.Shared.Para;
using Konto.Data.Models.Masters;
using AutoMapper;
using System.Globalization;
using Konto.Data.Models.Transaction.Dtos;
using DevExpress.Office.NumberConverters;
using DevExpress.XtraEditors.Filtering.Templates;

namespace Konto.Shared.Trans.Common
{
    public partial class BillUploadView : KontoForm
    {
        List<BillModel> BillList = new List<BillModel>();
        List<BillTransModel> BillTrans = new List<BillTransModel>();

        List<ChallanModel> ChallanList = new List<ChallanModel>();
        List<ChallanTransModel> ChallanTrans = new List<ChallanTransModel>();
        public int VType { get; set; }
        DataTable DTUpload;
        int colBill = 0;
        int colQty = 0;
        public BillUploadView()
        {
            InitializeComponent();
            FillLookup();
            this.Load += BillUploadView_Load;
        }
        private void BillUploadView_Load(object sender, EventArgs e)
        {
            okSimpleButton.Enabled = false;
            okSimpleButton.AllowFocus = false;
            SaveSimpleButton.Enabled = false;
            SaveSimpleButton.AllowFocus = false;
        }
        public void FillTemplate(int VTypeId)
        {
            using (var db = new KontoContext())
            {
                var TemplateAsList = (from p in db.Templates
                                      where p.VTypeId == VTypeId && p.IsDeleted == false
                                      select new BaseLookupDto()
                                      {
                                          DisplayText = p.Descriptions,
                                          Id = p.Id
                                      }).ToList();
                TemplateAsLookUpEdit.Properties.DataSource = TemplateAsList;

                this.voucherLookup11.VTypeId = (VoucherTypeEnum) VTypeId;
                this.bookLookup.VoucherType = (VoucherTypeEnum)VTypeId;
                this.accLookup.VoucherType = (VoucherTypeEnum)VTypeId;
            }
        }
        private void FillLookup()
        {
            using (var db = new KontoContext())
            {
                var TemplateAsList = (from p in db.Templates
                                      where p.VTypeId == VType && p.IsDeleted == false
                                      select new BaseLookupDto()
                                      {
                                          DisplayText = p.Descriptions,
                                          Id = p.Id
                                      }).ToList();
                TemplateAsLookUpEdit.Properties.DataSource = TemplateAsList;

                var vtype = (from p in db.VoucherTypes
                             where !p.IsDeleted && p.IsActive
                             orderby p.TypeName
                             select new BaseLookupDto()
                             {
                                 DisplayText = p.TypeName,
                                 Id = p.Id
                             }).ToList();


                // typeLookUpEdit.Properties.DataSource = vtype;
            }
        }
        private void okSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.Title = "Open Bill Excel File";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.CheckFileExists = true;
                openFileDialog1.CheckPathExists = true;
                openFileDialog1.ShowDialog();
                string filePath = openFileDialog1.FileName;

                if (string.IsNullOrEmpty(filePath)) return;

                //DXSplashScreen.Show<ProgressView>();
                splashScreenManager1.ShowWaitForm();
                //process = true;
                Workbook workbook = new Workbook(filePath);
                Worksheet worksheet = workbook.Worksheets[0];
                var exp = new ExportTableOptions
                {
                    ExportAsString = true,
                    ExportColumnName = true
                };
                worksheet.Cells.DeleteBlankColumns();
                worksheet.Cells.DeleteBlankRows();

                var db = new KontoContext();
                int TemplateId = Convert.ToInt32(TemplateAsLookUpEdit.EditValue);
                var TModel = db.Templates.FirstOrDefault(k => k.Id == TemplateId);

                var dt = worksheet.Cells.ExportDataTable((int)TModel.StartRowNo - 1, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1, exp);
                DTUpload = new DataTable();
                int NullProducts = 0;
                int NullAccs = 0;

                db.Configuration.AutoDetectChangesEnabled = false;
                db.Configuration.ValidateOnSaveEnabled = false;

                var fieldLists = (from t in db.Templatetrans
                                  join tf in db.TempFields on t.TempFieldId equals tf.Id into join_tf
                                  from tf in join_tf.DefaultIfEmpty()
                                  where t.TemplateId == TModel.Id && t.ColNo > 0 && !t.IsDeleted
                                  select new TempTransDto
                                  {
                                      TempFieldId = t.TempFieldId,
                                      FieldName = tf.FieldName,
                                      ColNo = t.ColNo,
                                      Id = t.Id
                                  }).ToList();

                try
                {
                    int intpro = 0;
                    int colno = 0;

                    int AccId = TModel.AccId != null ? (int)TModel.AccId : 0;
                    int stId = 0;
                    int VTypeId = (int)TModel.VTypeId;
                    string item = string.Empty;
                    string Qty = string.Empty;
                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        //Show Progress
                        intpro++;
                        double pro = ((double)intpro * 100) / (double)dt.Rows.Count;

                        //DXSplashScreen.Progress(pro);
                        //DXSplashScreen.SetState(string.Format("Completed {0} of {1}", intpro, dt.Rows.Count));
                        if (splashScreenManager1.IsSplashFormVisible)
                        {
                            splashScreenManager1.SetWaitFormDescription("Completed " + intpro + " of " + DTUpload.Rows.Count);
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Qty") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Qty").ColNo) - 1;  //tempRepo.GetColNo(TempModel.Descriptions, (int)TempModel.VTypeId, AccId, "Qty");
                            Qty = dr[colno].ToString();
                            if (string.IsNullOrEmpty(Qty))
                            {
                                if (splashScreenManager1.IsSplashFormVisible)
                                    splashScreenManager1.CloseWaitForm();
                                MessageBoxAdv.Show(this, "Can not Save upload bills!!", "Exception ", "Product can not be null!!!");
                                return;
                            }
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Item") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Item").ColNo) - 1;
                            item = dr[colno].ToString();
                        }
                        if (!string.IsNullOrEmpty(item) || !string.IsNullOrEmpty(Qty))
                        {
                            // Set for transaction  
                            var p = db.Products.FirstOrDefault(k => k.ProductName == item);
                            if (p == null)
                            {
                                //NullProducts++;
                                //dr[colno] = "";
                                if (splashScreenManager1.IsSplashFormVisible)
                                    splashScreenManager1.CloseWaitForm();
                                MessageBoxAdv.Show(this, "Can not Save upload bills!!", "Exception ", "Product can not be null!!!");
                                return;
                            }

                            if (AccId == null || AccId <= 0)
                            {
                                colno = 0;
                                if (fieldLists.FirstOrDefault(k => k.FieldName == "Party") != null)
                                {
                                    colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Party").ColNo) - 1;
                                    var book = dr[colno].ToString();

                                    var a = db.Accs.FirstOrDefault(k => k.AccName == book);
                                    if (a == null)
                                    {
                                        //NullAccs++;
                                        //dr[colno] = "";
                                        if (splashScreenManager1.IsSplashFormVisible)
                                            splashScreenManager1.CloseWaitForm();
                                        MessageBoxAdv.Show(this, "Can not Save upload bills!!", "Exception ", "Party can not be null!!!");
                                        return;
                                    }
                                    else
                                    {
                                        AccId = a.Id;
                                    }
                                }
                                else if (!string.IsNullOrEmpty(accLookup.SelectedText))
                                {
                                    AccId = (int)accLookup.SelectedValue;
                                }
                                else if (TModel.AccId > 0 || TModel.AccId != null)
                                {
                                    AccId = (int)TModel.AccId;
                                }
                            }

                            if (string.IsNullOrEmpty(bookLookup.SelectedText))
                            {
                                colno = 0;
                                if (fieldLists.FirstOrDefault(k => k.FieldName == "BookId") != null)
                                {
                                    colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "BookId").ColNo) - 1;

                                    var book = dr[colno].ToString();

                                    var a = db.Accs.FirstOrDefault(k => k.AccName == book);
                                    if (a == null)
                                    {
                                        if (splashScreenManager1.IsSplashFormVisible)
                                            splashScreenManager1.CloseWaitForm();
                                        //dr[colno] = "";
                                        MessageBoxAdv.Show(this, "Can not Save upload bills!!", "Exception ", "Book Can Not Null!!!");
                                        return;
                                    }
                                }
                                else
                                {
                                    if (splashScreenManager1.IsSplashFormVisible)
                                        splashScreenManager1.CloseWaitForm();
                                    MessageBoxAdv.Show(this, "Can not Save upload bills!!", "Exception ", "Book Can Not Null!!!");
                                    return;
                                }
                            }
                            colno = 0;

                            //if (fieldLists.FirstOrDefault(k => k.FieldName == "DeliveryParty") != null)
                            //{
                            //    colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "DeliveryParty").ColNo) - 1;
                            //    if (!string.IsNullOrEmpty(dr[colno].ToString()))
                            //    {
                            //        string delv = dr[colno].ToString();
                            //        var delvAcc = db.Accs.FirstOrDefault(k => k.AccName == delv);
                            //        if (delvAcc == null)
                            //        {
                            //            if (splashScreenManager1.IsSplashFormVisible)
                            //                splashScreenManager1.CloseWaitForm();
                            //            MessageBoxAdv.Show(this, "Can not Save upload bills!!", "Exception ", "Delivery Party Can Not Null!!!");
                            //            return;
                            //        }
                            //    }
                            //    else
                            //    {
                            //        if (splashScreenManager1.IsSplashFormVisible)
                            //            splashScreenManager1.CloseWaitForm();

                            //        MessageBoxAdv.Show(this, "Can not Save upload bills!!", "Exception ", "Delivery Party Can Not Null!!!");
                            //        return;
                            //    }
                            //}
                            //else
                            //{
                            //    if (splashScreenManager1.IsSplashFormVisible)
                            //        splashScreenManager1.CloseWaitForm();

                            //    MessageBoxAdv.Show(this, "Can not Save upload bills!!", "Exception ", "Book Can Not Null!!!");
                            //    return;
                            //}

                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "StateName") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "StateName").ColNo) - 1;
                                string stname = dr[colno].ToString();

                                if (!string.IsNullOrEmpty(stname))
                                {
                                    var st = db.States.FirstOrDefault(x => x.StateName == stname.ToUpper());
                                    if (st == null)
                                    {
                                        if (splashScreenManager1.IsSplashFormVisible)
                                            splashScreenManager1.CloseWaitForm();
                                        MessageBoxAdv.Show(this, "Can not Save upload bills!!", "Exception ", "StateName not Matched from system State List");
                                        return;
                                    }
                                }
                            }
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "BillAmount") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "BillAmount").ColNo) - 1;
                                colBill = colno;
                                string tot = dr[colno].ToString();
                                if (colno > 0)
                                    if (tot == "0" || string.IsNullOrEmpty(tot))
                                    {
                                        if (splashScreenManager1.IsSplashFormVisible)
                                            splashScreenManager1.CloseWaitForm();
                                        //DXSplashScreen.Close(); 
                                        MessageBoxAdv.Show(this, "Can not upload bills!!", "Exception ", "Please check Bill Amount is O for the party ");
                                        return;
                                    }
                            }
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Qty").ColNo) - 1;
                            colQty = colno;
                            colno = 0;


                            if (fieldLists.FirstOrDefault(k => k.FieldName == "OrderNo") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "OrderNo").ColNo) - 1;
                                string OrderNo = dr[colno].ToString();
                                if (string.IsNullOrEmpty(OrderNo))
                                {
                                    if (splashScreenManager1.IsSplashFormVisible)
                                        splashScreenManager1.CloseWaitForm();
                                    MessageBoxAdv.Show(this, "Can not upload bills!!", "Exception ", "BillNo not found for the item " + item);
                                    //process = false;
                                    return;
                                }

                                //check for duplicate order

                                //var bills = db.Bills.FirstOrDefault(k => k.AccId == AccId && k.RefNo == OrderNo && k.IsDeleted == false)
                                 

                                //if (bills != null)
                                //{
                                //    var vtypedata = db.Vouchers.FirstOrDefault(k => k.Id == bills.VoucherId);

                                //    if (((vtypedata.VTypeId == (int)VoucherTypeEnum.SaleInvoice) && VTypeId == (int)VoucherTypeEnum.SaleInvoice)
                                //        || ((vtypedata.VTypeId == (int)VoucherTypeEnum.SaleReturn) && VTypeId == (int)VoucherTypeEnum.SaleReturn))
                                //    {
                                //        if (splashScreenManager1.IsSplashFormVisible)
                                //            splashScreenManager1.CloseWaitForm();
                                //        //DXSplashScreen.Close();
                                //        MessageBoxAdv.Show(this, "Can not upload bills!!", "Exception ", "Entry Already Exist for this party");
                                //        //process = false;
                                //        return;
                                //    }
                                //}
                            }
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Unit") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Unit").ColNo) - 1;
                                if (colno > 0)
                                {
                                    var unit = dr[colno].ToString();
                                    var u = db.Uoms.FirstOrDefault(k => k.UnitName == unit);
                                    if (u == null)
                                    {
                                        //splashScreenManager1.CloseWaitForm();
                                        dr["Unit"] = "";
                                        // MessageBoxAdv.Show(this, "Can not upload bills!!", "Exception ", "Unit Can not be null!!");
                                        //process = false;
                                        //return;
                                    }
                                }
                            }
                        }
                    }
                    DTUpload = dt;
                    customGridControl1.DataSource = DTUpload;

                    colno = 0;
                    if (fieldLists.FirstOrDefault(k => k.FieldName == "Item") != null)
                    {
                        colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Item").ColNo) - 1;
                        dt.Columns[colno].ColumnName = "Product";
                    }
                    colno = 0;
                    if (fieldLists.FirstOrDefault(k => k.FieldName == "Party") != null)
                    {
                        colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Party").ColNo) - 1;
                        if (colno > 0)
                            dt.Columns[colno].ColumnName = "Party";
                    }
                    //DXSplashScreen.Close();
                    if (splashScreenManager1.IsSplashFormVisible)
                        splashScreenManager1.CloseWaitForm();

                    MessageBoxAdv.Show(this, "Bill Upload successfully", "Upload !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SaveSimpleButton.Enabled = true;
                    SaveSimpleButton.AllowFocus = true;

                    customGridView1.Columns[colBill].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    customGridView1.Columns[colQty].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    //process = false;
                }
                catch (Exception ex)
                {
                    if (splashScreenManager1.IsSplashFormVisible)
                        splashScreenManager1.CloseWaitForm();
                    //DXSplashScreen.Close();
                    MessageBoxAdv.Show(this, "Can not upload bills!!", "Exception ", ex.ToString());

                    Log.Error(ex, ex.ToString() + "Upload ViewModel Upload product List.");
                    //process = false;
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManager1.IsSplashFormVisible)
                    splashScreenManager1.CloseWaitForm();
                Log.Error(ex, "Upload Fils");
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void TemplateAsLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (TemplateAsLookUpEdit.EditValue != DBNull.Value)
            {
                int tempId = Convert.ToInt32(TemplateAsLookUpEdit.EditValue);
                if (tempId != 0 || tempId != null)
                {
                    var db = new KontoContext();
                    var temp = db.Templates.FirstOrDefault(k => k.Id == tempId);

                    //typeLookUpEdit.EditValue = temp.VTypeId;
                    //typeLookUpEdit.Enabled = false;

                    if (temp.VoucherId > 0)
                    {
                        voucherLookup11.SelectedValue = temp.VoucherId;
                        voucherLookup11.SetGroup((int)temp.VoucherId);
                    }
                    if (temp.AccId != null || temp.AccId > 0)
                    {
                        accLookup.SelectedValue = temp.AccId;
                        accLookup.SetAcc(Convert.ToInt32(temp.AccId));
                    }
                    okSimpleButton.Enabled = true;
                    okSimpleButton.AllowFocus = true;
                }
            }
        }

        private void SaveSimpleButton_Click(object sender, EventArgs e)
        {
            int vid = Convert.ToInt32(voucherLookup11.SelectedValue);

            int vtypeid = 0;
            using (var db = new KontoContext())
            {
                vtypeid = db.Vouchers.FirstOrDefault(k => k.Id == vid).VTypeId;
            }
            if (vtypeid == (int)VoucherTypeEnum.SaleInvoice)
            {
                SaveDataInSaleInvoice();
            }
            else if (vtypeid == (int)VoucherTypeEnum.SaleReturn)
            {
                SaveDataInSaleRet();
            }
            else if (vtypeid == (int)VoucherTypeEnum.PurchaseInvoice)
            {
                SaveDataInPI();
            }
            else if (vtypeid == (int)VoucherTypeEnum.SalesChallan)
            {
                SaveDataInSC();
            }
        }

        private void SaveDataInSC()
        {
            splashScreenManager1.ShowWaitForm();

            KontoContext _db = new KontoContext();
            var pid = -1;
            ChallanModel bill = new ChallanModel();
            ChallanTransModel bt = new ChallanTransModel();

            int StateID = 0;
            int colno = 0;

            var TModel = _db.Templates.FirstOrDefault(k => k.Id == (int)TemplateAsLookUpEdit.EditValue);
            int AccId = TModel.AccId != null ? (int)TModel.AccId : 0;

            var fieldLists = (from t in _db.Templatetrans
                              join tf in _db.TempFields on t.TempFieldId equals tf.Id into join_tf
                              from tf in join_tf.DefaultIfEmpty()
                              where t.TemplateId == TModel.Id && t.IsDeleted == false && t.ColNo > 0
                              select new TempTransDto
                              {
                                  TempFieldId = t.TempFieldId,
                                  FieldName = tf.FieldName,
                                  ColNo = t.ColNo,
                                  Id = t.Id
                              }).ToList();

            _db.Configuration.AutoDetectChangesEnabled = false;
            _db.Configuration.ValidateOnSaveEnabled = false;
            string Qty = string.Empty;
            string item = string.Empty;
            var intpro = 0;

            try
            {
                foreach (System.Data.DataRow dr in DTUpload.Rows)
                {
                    //Show Progress
                    intpro++;
                    double pro = ((double)intpro * 100) / (double)DTUpload.Rows.Count;

                    if (splashScreenManager1.IsSplashFormVisible)
                    {
                        splashScreenManager1.SetWaitFormDescription("Completed " + intpro + " of " + DTUpload.Rows.Count);
                    }
                    pid = pid - 1;

                    bill = new ChallanModel();
                    bt = new ChallanTransModel();

                    colno = 0;
                    if (fieldLists.FirstOrDefault(k => k.FieldName == "InvoiceNo") != null)
                    {
                        colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "InvoiceNo").ColNo) - 1;
                        bill.BillNo = dr[colno].ToString();
                    }
                    colno = 0;
                    if (fieldLists.FirstOrDefault(k => k.FieldName == "Item") != null)
                    {
                        colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Item").ColNo) - 1;
                        item = dr[colno].ToString();
                    }
                    colno = 0;
                    if (fieldLists.FirstOrDefault(k => k.FieldName == "Qty") != null)
                    {
                        colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Qty").ColNo) - 1;
                        Qty = dr[colno].ToString();
                    }
                    //if (!string.IsNullOrEmpty(dr[colno].ToString()))
                    if (!string.IsNullOrEmpty(item) || !string.IsNullOrEmpty(Qty))
                    {
                        if (!string.IsNullOrEmpty(accLookup.SelectedText))
                        {
                            bill.AccId = (int)accLookup.SelectedValue;
                            bill.DelvAccId = (int)accLookup.SelectedValue;
                        }
                        else if (TModel.AccId == null || TModel.AccId <= 0)
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Party") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Party").ColNo) - 1;
                                var book = dr[colno].ToString();

                                var a = _db.Accs.FirstOrDefault(k => k.AccName == book);
                                if (a != null)
                                {
                                    bill.AccId = a.Id;
                                    bill.DelvAccId = a.Id;
                                    
                                }
                            }
                        }
                        else if (TModel.AccId > 0 || TModel.AccId != null)
                        {
                            bill.AccId = (int)TModel.AccId;
                            bill.DelvAccId = (int)TModel.AccId;
                        }

                        if (fieldLists.FirstOrDefault(k => k.FieldName == "OrderDate") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "OrderDate").ColNo) - 1;
                            //if (TempModel.Descriptions == "MEESHO")
                            //{
                            //    var datestr = dr[colno].ToString();
                            //    datestr = datestr.Substring(0, 12);
                            //    if (!string.IsNullOrEmpty(datestr))
                            //        bill.VDate = Convert.ToDateTime(datestr);
                            //}
                            //else
                            var datestr = dr[colno].ToString();
                          //  datestr = datestr.Substring(0, 10);

                            if (!string.IsNullOrEmpty(datestr))
                            {
                                DateTime vd = Convert.ToDateTime(datestr);
                                bill.VDate = vd;
                                bill.VoucherDate = Convert.ToInt32(vd.ToString("yyyyMMdd"));
                            }
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "BillAmount") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "BillAmount").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bill.TotalAmount = Convert.ToDecimal(dr[colno].ToString());
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "DeliveryParty") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "DeliveryParty").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                            {
                                string delv = dr[colno].ToString();
                                var delvAcc = _db.Accs.FirstOrDefault(k => k.AccName == delv);
                                if (delvAcc != null)
                                    bill.DelvAccId = Convert.ToInt32(delvAcc.Id);
                            }
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "OrderNo") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "OrderNo").ColNo) - 1;
                            bill.ChallanNo = dr[colno].ToString();
                        }
                        //colno = 0;
                        //if (fieldLists.FirstOrDefault(k => k.FieldName == "OrderNo") != null)
                        //{
                        //    colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "OrderNo").ColNo) - 1;
                        //    bill.RefNo = dr[colno].ToString();
                        //}

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Agent") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Agent").ColNo) - 1;
                            string agent = dr[colno].ToString();
                            var ac = _db.Accs.FirstOrDefault(k => k.AccName == agent);
                            if (ac != null)
                                bill.AgentId = ac.Id;

                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "LrNo") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "LrNo").ColNo) - 1;
                            bill.DocNo = dr[colno].ToString();
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "LrDate") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "LrDate").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bill.DocDate = Convert.ToDateTime(dr[colno].ToString());
                        }
                        else
                        {
                            bill.DocDate = DateTime.Now;
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Transport") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Transport").ColNo) - 1;
                            string agent = dr[colno].ToString();
                            var ac = _db.Accs.FirstOrDefault(k => k.AccName == agent);
                            if (ac != null)
                                bill.TransId = ac.Id;
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "VehicleNo") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "VehicleNo").ColNo) - 1;
                            bill.VehicleNo = dr[colno].ToString();
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Remark") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Remark").ColNo) - 1;
                            bill.Remark = dr[colno].ToString();
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "StateName") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "StateName").ColNo) - 1;
                            string stname = dr[colno].ToString();

                            var st = _db.States.FirstOrDefault(x => x.StateName == stname.ToUpper());
                            StateID = st.Id;
                        }

                        bill.VoucherId = Convert.ToInt32(voucherLookup11.SelectedValue);



                        bill.DivId = 1;
                        bill.TypeId = (int)VoucherTypeEnum.SalesChallan;
                        bill.YearId = KontoGlobals.YearId;
                        bill.CompId = KontoGlobals.CompanyId;
                        bill.IsActive = true;
                        bill.BranchId = KontoGlobals.BranchId;
                        bill.IsDeleted = false;
                        bill.CreateUser = KontoGlobals.UserName;
                        bill.CreateDate = DateTime.Now;
                        bill.ChallanType = 6;//Sale Challan
                        bill.StoreId =1;//Sale Challan
                       
                        // Set for transaction 
                        var p = _db.Products.FirstOrDefault(k => k.ProductName == item);
                        bt.ProductId = p.Id;

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Color") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Color").ColNo) - 1;
                            string find = dr[colno].ToString();
                            var data = _db.ColorModels.FirstOrDefault(k => k.ColorName == find);
                            if (data != null)
                                bt.ColorId = data.Id;
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Design") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Design").ColNo) - 1;
                            string find = dr[colno].ToString();
                            var data = _db.Products.FirstOrDefault(k => k.ProductName == find);
                            if (data != null)
                                bt.DesignId = data.Id;
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Grade") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Grade").ColNo) - 1;
                            string find = dr[colno].ToString();
                            var data = _db.Grades.FirstOrDefault(k => k.GradeName == find);
                            if (data != null)
                                bt.GradeId = data.Id;
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "ItemRemark") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "ItemRemark").ColNo) - 1;
                            bt.Remark = dr[colno].ToString();
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Qty") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Qty").ColNo) - 1;
                            bt.Qty = Convert.ToDecimal(dr[colno].ToString());
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Unit") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Unit").ColNo) - 1;
                            var unit = dr[colno].ToString();
                            var u = _db.Uoms.FirstOrDefault(k => k.UnitName == unit);
                            if (u != null)
                            {
                                bt.UomId = u.Id;
                            }
                            else
                            {
                                u = _db.Uoms.FirstOrDefault(k => k.Id == p.UomId);
                                if (u != null)
                                {
                                    bt.UomId = u.Id;
                                }
                            }
                        }
                        else
                        {
                            var u = _db.Uoms.FirstOrDefault(k => k.Id == p.UomId);
                            if (u != null)
                            {
                                bt.UomId = u.Id;
                            }
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Rate") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Rate").ColNo) - 1;
                            if (colno > 0)
                            {
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                {
                                    bt.Rate = Convert.ToDecimal(dr[colno].ToString());
                                    bt.Gross = decimal.Round(bt.Qty * bt.Rate, 2, MidpointRounding.AwayFromZero);
                                }
                                else
                                {
                                    colno = 0;
                                    if (fieldLists.FirstOrDefault(k => k.FieldName == "Gross") != null)
                                    {
                                        colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Gross").ColNo) - 1;
                                        if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                        {
                                            bt.Gross = Convert.ToDecimal(dr[colno].ToString());
                                            bt.Rate = decimal.Round(bt.Gross / bt.Qty, 2, MidpointRounding.AwayFromZero);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Gross") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Gross").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                {
                                    bt.Gross = Convert.ToDecimal(dr[colno].ToString());
                                    bt.Rate = decimal.Round(bt.Gross / bt.Qty, 2, MidpointRounding.AwayFromZero);
                                }
                            }
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Disc%") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Disc%").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bt.DiscPer = Convert.ToDecimal(dr[colno].ToString());
                        }
                        if (bt.DiscPer <= 0)
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Discount") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Discount").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                    bt.Disc = Convert.ToDecimal(dr[colno].ToString());
                            }
                        }
                        else
                        {
                            bt.Disc = decimal.Round(bt.Gross * bt.DiscPer / 100, 2, MidpointRounding.AwayFromZero);
                        }
                        decimal gross = bt.Gross - bt.Disc;

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "OtherAdd") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "OtherAdd").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bt.OtherAdd = Convert.ToDecimal(dr[colno].ToString());
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "FreightRate") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "FreightRate").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bt.FreightRate = Convert.ToDecimal(dr[colno].ToString());
                        }

                        if (bt.FreightRate > 0)
                        {
                            bt.Freight = decimal.Round(bt.Qty * bt.FreightRate, 2, MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Freight") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Freight").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                    bt.Freight = Convert.ToDecimal(dr[colno].ToString());
                            }
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "OtherLess") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "OtherLess").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bt.OtherLess = Convert.ToDecimal(dr[colno].ToString());
                        }
                        gross = (gross + bt.Freight + bt.OtherAdd - bt.OtherLess);

                        var comp = _db.Companies.FirstOrDefault(k => k.Id == KontoGlobals.CompanyId);
                        decimal CGST = 0, SGST = 0, IGST = 0, CGSTAmt = 0, SGSTAmt = 0, IGSTAmt = 0;

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Cgst%") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Cgst%").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                            {
                                CGST = Convert.ToDecimal(dr[colno].ToString());
                                CGSTAmt = gross * CGST / 100;
                            }
                            else
                            {
                                colno = 0;
                                if (fieldLists.FirstOrDefault(k => k.FieldName == "CgstAmt") != null)
                                {
                                    colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "CgstAmt").ColNo) - 1;

                                    if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                        CGSTAmt = Convert.ToDecimal(dr[colno].ToString());
                                }
                            }
                        }
                        else
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "CgstAmt") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "CgstAmt").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                    CGSTAmt = Convert.ToDecimal(dr[colno].ToString());
                            }
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Sgst%") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Sgst%").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                            {
                                SGST = Convert.ToDecimal(dr[colno].ToString());
                                SGSTAmt = gross * SGST / 100;
                            }
                            else
                            {
                                colno = 0;
                                if (fieldLists.FirstOrDefault(k => k.FieldName == "SgstAmt") != null)
                                {
                                    colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "SgstAmt").ColNo) - 1;
                                    if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                        SGSTAmt = Convert.ToDecimal(dr[colno].ToString());
                                }
                            }
                        }
                        else
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "SgstAmt") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "SgstAmt").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                    SGSTAmt = Convert.ToDecimal(dr[colno].ToString());
                            }
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Igst%") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Igst%").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                            {
                                IGST = Convert.ToDecimal(dr[colno].ToString());
                                IGSTAmt = gross * IGST / 100;
                            }
                            else
                            {
                                colno = 0;
                                if (fieldLists.FirstOrDefault(k => k.FieldName == "IgstAmt") != null)
                                {
                                    colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "IgstAmt").ColNo) - 1;
                                    if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                        IGSTAmt = Convert.ToDecimal(dr[colno].ToString());
                                }
                            }
                        }
                        else
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "IgstAmt") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "IgstAmt").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                    IGSTAmt = Convert.ToDecimal(dr[colno].ToString());
                            }
                        }

                        if (StateID <= 0)
                        {
                            if (StateID == comp.StateId)
                            {
                                if (CGST > 0)
                                    bt.CgstPer = CGST;
                                else if (IGST > 0)
                                {
                                    bt.CgstPer = IGST / 2;
                                    CGSTAmt = gross * bt.CgstPer / 100;
                                }
                                bt.Cgst = decimal.Round(CGSTAmt, 2, MidpointRounding.AwayFromZero);
                                bt.SgstPer = bt.CgstPer;
                                bt.Sgst = decimal.Round(CGSTAmt, 2, MidpointRounding.AwayFromZero);
                            }
                            else
                            {
                                if (IGST > 0)
                                {
                                    bt.IgstPer = IGST;
                                }
                                else
                                {
                                    if (CGST > 0)
                                    {
                                        bt.IgstPer = CGST * 2;
                                        IGSTAmt = gross * bt.IgstPer / 100;
                                    }
                                }
                                bt.Igst = decimal.Round(IGSTAmt, 2, MidpointRounding.AwayFromZero);
                            }
                        }
                        else
                        {
                            if (accLookup.LookupDto.StateId == comp.StateId)
                            {
                                if (CGST > 0)
                                    bt.CgstPer = CGST;
                                else if (IGST > 0)
                                {
                                    bt.CgstPer = IGST / 2;
                                    CGSTAmt = gross * bt.CgstPer / 100;
                                }
                                bt.Cgst = decimal.Round(CGSTAmt, 2, MidpointRounding.AwayFromZero);
                                bt.SgstPer = bt.CgstPer;
                                bt.Sgst = decimal.Round(CGSTAmt, 2, MidpointRounding.AwayFromZero);
                            }
                            else
                            {
                                if (IGST > 0)
                                {
                                    bt.IgstPer = IGST;
                                }
                                else
                                {
                                    if (CGST > 0)
                                    {
                                        bt.IgstPer = CGST * 2;
                                        IGSTAmt = gross * bt.IgstPer / 100;
                                    }
                                }
                                bt.Igst = decimal.Round(IGSTAmt, 2, MidpointRounding.AwayFromZero);
                            }
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "CessPer") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "CessPer").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bt.CessPer = Convert.ToDecimal(dr[colno].ToString());
                        }
                        if (bt.CessPer > 0)
                            bt.Cess = decimal.Round(bt.Qty * bt.CessPer, 2, MidpointRounding.AwayFromZero);
                        else
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Cess") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Cess").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                    bt.Cess = Convert.ToDecimal(dr[colno].ToString());
                            }
                        }
                        bt.Total = gross + bt.Sgst + bt.Cgst + bt.Igst + bt.Cess;

                        var orderexist = ChallanList.FirstOrDefault(k => k.ChallanNo == bill.ChallanNo && k.IsDeleted == false);
                        if (orderexist == null)
                        {
                            bill.Id = pid;
                            ChallanList.Add(bill);

                            bt.ChallanId = bill.Id;
                            bt.IsActive = true;
                            bt.IsDeleted = false;
                            bt.CreateUser = KontoGlobals.UserName;
                            bt.CreateDate = DateTime.Now;
                            ChallanTrans.Add(bt);
                            //  await transrepo.AddAsyn(bt);
                        }
                        else
                        {
                            bt.ChallanId = orderexist.Id;
                            bt.CreateUser = KontoGlobals.UserName;
                            bt.CreateDate = DateTime.Now;
                            ChallanTrans.Add(bt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ChallanList = new List<ChallanModel>();
                ChallanTrans = new List<ChallanTransModel>();

                if (splashScreenManager1.IsSplashFormVisible)
                    splashScreenManager1.CloseWaitForm();
                MessageBoxAdv.Show(this, "Error found while uploading Excel", "Exception ", ex.ToString());
                Log.Error(ex, "Upload ViewModel Upload product List.");
                //process = false; 
            }
            if (ChallanList.Count > 0)
            {
                using (var _tran = _db.Database.BeginTransaction())
                {
                    try
                    {
                        ChallanList = new List<ChallanModel>(ChallanList.OrderBy(k => k.VDate));

                        string firstVNo = "";
                        string LstVNo = "";
                        decimal totalQty = 0;
                        decimal TotalAmount = 0;

                        ChallanModel bm;
                        var config = new MapperConfiguration(cfg =>
                        {
                            cfg.CreateMap<ChallanModel, ChallanModel>().ForMember(x => x.Id, p => p.Ignore());

                        });
                        var map = new Mapper(config);
                        foreach (var b in ChallanList)
                        {
                            b.VoucherNo = DbUtils.NextSerialNo(b.VoucherId, _db);
                            b.BillNo = b.VoucherNo;

                            if (string.IsNullOrEmpty(firstVNo))
                                firstVNo = b.VoucherNo;

                            LstVNo = b.VoucherNo;

                            //Sum for multiple order of same bill
                            var btrans = ChallanTrans.Where(k => k.ChallanId == b.Id).ToList();
                            var totl = btrans.Sum(k => k.Total);

                            var nettotl = btrans.Sum(k => k.Total);


                            var totlQty = btrans.Sum(k => k.Qty);
                            b.TotalQty = Convert.ToDecimal(totlQty);

                            totalQty = totalQty + totalQty;
                            TotalAmount = TotalAmount + nettotl;

                            var x1 = nettotl - Math.Truncate(nettotl);

                            bool isEven = false;
                            if (x1 == (decimal)0.5)
                            {
                                nettotl = nettotl + (decimal)0.01;
                                isEven = true;
                            }

                            var round = decimal.Round(nettotl, 0) - nettotl;
                            if (isEven)
                            {
                                round = (decimal)0.5;
                                nettotl = nettotl + (decimal)0.49;
                            }
                            else
                            {
                                nettotl = nettotl + round;
                            }

                            b.RoundOff = round;
                            b.TotalAmount = Convert.ToDecimal(nettotl);

                            bm = new ChallanModel();
                            map = new Mapper(config);
                            map.Map(b, bm);

                            _db.Challans.Add(bm);
                            _db.SaveChanges();

                            foreach (var t in btrans)
                            {
                                t.ChallanId = bm.Id;
                                _db.ChallanTranses.Add(t);
                            }
                            _db.SaveChanges();

                            foreach (var ctran in btrans)
                            {
                                string TableName = "grn";
                                var stockReq = _db.Products.FirstOrDefault(k => k.Id == ctran.ProductId).StockReq;
                                if (stockReq == "No") continue;

                                StockEffect.StockTransChlnEntry(bm, ctran, true, TableName, KontoGlobals.UserName, _db);
                                _db.SaveChanges();
                            }
                        }
                        _db.SaveChanges();

                        _tran.Commit();
                        // DXSplashScreen.Close();
                        if (splashScreenManager1.IsSplashFormVisible)
                        {
                            splashScreenManager1.CloseWaitForm();
                        }

                        MessageBoxAdv.Show(this, "Upload successfully with voucher No from:" + firstVNo + " to: "
                        + LstVNo + ". Total Qty is:" + totalQty + ". Total Amount is " + TotalAmount, "Save !!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        // NullProducts = BillTrans.Where(k => k.ProductId == null).Count();
                        if (splashScreenManager1.IsSplashFormVisible)
                        {
                            splashScreenManager1.CloseWaitForm();
                        }
                        ChallanList = new List<ChallanModel>();
                        ChallanTrans = new List<ChallanTransModel>();

                        MessageBoxAdv.Show(this, "Error found while uploading Excel", "Exception ", ex.ToString());
                        Log.Error(ex, "Upload ViewModel Upload product List.");
                        // process = false;
                    }
                }
            }
        }

        private void SaveDataInPI()
        {
            splashScreenManager1.ShowWaitForm();

            KontoContext _db = new KontoContext();
            _db.Configuration.AutoDetectChangesEnabled = false;
            _db.Configuration.ValidateOnSaveEnabled = false;
            try
            {

                var pid = 0;

                BillModel bill = new BillModel();
                BillTransModel bt = new BillTransModel();
                var TModel = _db.Templates.FirstOrDefault(k => k.Id == (int)TemplateAsLookUpEdit.EditValue);
                int AccId = TModel.AccId != null ? (int)TModel.AccId : 0;
                //if (VoucherId>0)
                //{

                //}
                //else if (TModel.VoucherId != null || TModel.VoucherId > 0)
                //{
                //    VoucherId = (int)TModel.VoucherId;
                //}
                //else
                //{
                //    var v = _db.Vouchers.FirstOrDefault(k => k.VTypeId == (int)TModel.VTypeId);
                //    if (v != null)
                //    {
                //        VoucherId = v.Id;
                //    }
                //}
                var fieldLists = (from t in _db.Templatetrans
                                  join tf in _db.TempFields on t.TempFieldId equals tf.Id into join_tf
                                  from tf in join_tf.DefaultIfEmpty()
                                  where t.TemplateId == TModel.Id && t.IsDeleted == false && t.ColNo > 0
                                  select new TempTransDto
                                  {
                                      TempFieldId = t.TempFieldId,
                                      FieldName = tf.FieldName,
                                      ColNo = t.ColNo,
                                      Id = t.Id
                                  }).ToList();

                var intpro = 0;
                int colno = 0;
                string item = string.Empty;
                string Qty = string.Empty;

                foreach (System.Data.DataRow dr in DTUpload.Rows)
                {
                    //Show Progress
                    intpro++;
                    double pro = ((double)intpro * 100) / (double)DTUpload.Rows.Count;

                    if (splashScreenManager1.IsSplashFormVisible)
                    {
                        splashScreenManager1.SetWaitFormDescription("Completed " + intpro + " of " + DTUpload.Rows.Count);
                    }
                    pid = pid - 1;

                    colno = 0;
                    if (fieldLists.FirstOrDefault(k => k.FieldName == "Item") != null)
                    {
                        colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Item").ColNo) - 1;
                        item = dr[colno].ToString();
                    }

                    colno = 0;
                    if (fieldLists.FirstOrDefault(k => k.FieldName == "Qty") != null)
                    {
                        colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Qty").ColNo) - 1;
                        Qty = dr[colno].ToString();
                    }
                    //if (!string.IsNullOrEmpty(dr[colno].ToString()))
                    if (!string.IsNullOrEmpty(item) || !string.IsNullOrEmpty(Qty))
                    {
                        bill = new BillModel();
                        bt = new BillTransModel();

                        if (TModel.AccId == null || TModel.AccId <= 0)
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Party") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Party").ColNo) - 1;
                                var book = dr[colno].ToString();

                                var a = _db.Accs.FirstOrDefault(k => k.AccName == book);
                                if (a != null)
                                {
                                    bill.AccId = a.Id;
                                }
                            }
                            else if (!string.IsNullOrEmpty(accLookup.SelectedText))
                            {
                                bill.AccId = (int)accLookup.SelectedValue;
                            }
                        }
                        else
                        {
                            bill.AccId = (int)TModel.AccId;
                        }


                        if (!string.IsNullOrEmpty(bookLookup.SelectedText))
                        {
                            bill.BookAcId = (int)bookLookup.SelectedValue;
                        }
                        else
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "BookId") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "BookId").ColNo) - 1;

                                var book = dr[colno].ToString();

                                var a = _db.Accs.FirstOrDefault(k => k.AccName == book);
                                if (a != null)
                                {
                                    bill.BookAcId = a.Id;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Book Not Found");
                                return;
                            }
                        }

                        //if (TModel.VoucherId != null)
                        //{
                        //    bill.VoucherId = (int)TModel.VoucherId;
                        //}
                        //else
                        //{
                        //    var vchr = _db.Vouchers.FirstOrDefault(k => k.VTypeId == TModel.VTypeId);
                        //    if (vchr != null)
                        //        bill.VoucherId = vchr.Id;
                        //}
                        bill.VoucherId = Convert.ToInt32(voucherLookup11.SelectedValue);
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "InvoiceNo") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "InvoiceNo").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bill.BillNo = dr[colno].ToString();
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "OrderDate") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "OrderDate").ColNo) - 1;
                            //if (TempModel.Descriptions == "MEESHO")
                            //{
                            //    var datestr = dr[colno].ToString();
                            //    datestr = datestr.Substring(0, 12);
                            //    if (!string.IsNullOrEmpty(datestr))
                            //        bill.VDate = Convert.ToDateTime(datestr);
                            //}
                            //else
                            var datestr = dr[colno].ToString();
                          //  datestr = datestr.Substring(0, 10);

                            if (!string.IsNullOrEmpty(datestr))
                                bill.VDate = Convert.ToDateTime(datestr);

                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "BillAmount") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "BillAmount").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bill.TotalAmount = Convert.ToDecimal(dr[colno].ToString());
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "OrderNo") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "OrderNo").ColNo) - 1;
                            bill.RefNo = dr[colno].ToString();
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Remark") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Remark").ColNo) - 1;
                            bill.Remarks = dr[colno].ToString();
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "StateName") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "StateName").ColNo) - 1;
                            string stname = dr[colno].ToString();

                            var st = _db.States.FirstOrDefault(x => x.StateName == stname.ToUpper());
                            bill.StateId = st.Id;
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Agent") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Agent").ColNo) - 1;
                            string agent = dr[colno].ToString();
                            var ac = _db.Accs.FirstOrDefault(k => k.AccName == agent);
                            if (ac != null)
                                bill.AgentId = ac.Id;

                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "LrNo") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "LrNo").ColNo) - 1;
                            bill.DocNo = dr[colno].ToString();
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "LrDate") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "LrDate").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bill.DocDate = Convert.ToDateTime(dr[colno].ToString());
                        }
                        else
                        {
                            bill.DocDate = DateTime.Now;
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Transport") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Transport").ColNo) - 1;
                            string agent = dr[colno].ToString();
                            var ac = _db.Accs.FirstOrDefault(k => k.AccName == agent);
                            if (ac != null)
                                bill.TransId = ac.Id;
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "PortCode") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "PortCode").ColNo) - 1;
                            bill.PortCode = dr[colno].ToString();
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "DueDays") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "DueDays").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bill.Duedays = Convert.ToInt32(dr[colno].ToString());
                        }

                        if (string.IsNullOrEmpty(bill.BillNo))
                            bill.BillNo = "NA";

                        var bills = _db.Bills.FirstOrDefault(k => k.AccId == bill.AccId && k.BillNo == bill.BillNo && k.IsDeleted == false);
                        if (bills == null)
                        {
                            //var vchr = _db.Vouchers.FirstOrDefault(k => k.VTypeId == 13);
                            //bill.VoucherId = vchr.Id;

                            bill.VoucherDate = Convert.ToInt32(bill.VDate.ToString("yyyyMMdd"));

                            bill.DivisionId = 1;
                            bill.YearId = KontoGlobals.YearId;
                            bill.CompId = KontoGlobals.CompanyId;
                            bill.IsActive = true;
                            bill.IsDeleted = false;
                            bill.CreateUser = KontoGlobals.UserName;
                            bill.CreateDate = DateTime.Now;
                            bill.BranchId = KontoGlobals.BranchId;
                            bill.BillType = "Regular";
                            bill.Rcm = "NO";
                            bill.RcdDate = DateTime.Now.Date;
                            bill.Itc = "Inputs";
                            bill.VehicleNo = "NA";
                            bill.EwayBillNo = "NA";
                            // bill.HasteId = 28;
                            bill.TypeId = (int)VoucherTypeEnum.PurchaseInvoice;
                            // Set for transaction 

                            var p = _db.Products.FirstOrDefault(k => k.ProductName == item);
                            bt.ProductId = p.Id;

                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Color") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Color").ColNo) - 1;
                                string find = dr[colno].ToString();
                                var data = _db.ColorModels.FirstOrDefault(k => k.ColorName == find);
                                if (data != null)
                                    bt.ColorId = data.Id;
                            }

                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Design") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Design").ColNo) - 1;
                                string find = dr[colno].ToString();
                                var data = _db.Products.FirstOrDefault(k => k.ProductName == find);
                                if (data != null)
                                    bt.DesignId = data.Id;
                            }

                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Grade") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Grade").ColNo) - 1;
                                string find = dr[colno].ToString();
                                var data = _db.Grades.FirstOrDefault(k => k.GradeName == find);
                                if (data != null)
                                    bt.GradeId = data.Id;
                            }

                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "ItemRemark") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "ItemRemark").ColNo) - 1;
                                bt.Remark = dr[colno].ToString();
                            }

                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Qty") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Qty").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                    bt.Qty = Convert.ToDecimal(dr[colno].ToString());
                            }
                            else
                            {
                                colno = 0;
                                if (fieldLists.FirstOrDefault(k => k.FieldName == "Cut") != null)
                                {
                                    colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Cut").ColNo) - 1;
                                    if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                        bt.Cut = Convert.ToDecimal(dr[colno].ToString());
                                }
                                colno = 0;
                                if (fieldLists.FirstOrDefault(k => k.FieldName == "Pcs") != null)
                                {
                                    colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Pcs").ColNo) - 1;
                                    if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                        bt.Pcs = Convert.ToInt32(dr[colno].ToString());
                                }
                                if (bt.Cut > 0 && bt.Pcs > 0)
                                    bt.Qty = decimal.Round(bt.Pcs * bt.Cut, 2, MidpointRounding.AwayFromZero);

                            }
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Unit") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Unit").ColNo) - 1;

                                var unit = dr[colno].ToString();
                                var u = _db.Uoms.FirstOrDefault(k => k.UnitName == unit);
                                if (u != null)
                                {
                                    bt.UomId = u.Id;
                                }
                                else
                                {
                                    u = _db.Uoms.FirstOrDefault(k => k.Id == p.UomId);
                                    if (u != null)
                                    {
                                        bt.UomId = u.Id;
                                    }
                                }
                            }
                            else
                            {
                                var u = _db.Uoms.FirstOrDefault(k => k.Id == p.UomId);
                                if (u != null)
                                {
                                    bt.UomId = u.Id;
                                }
                            }
                            colno = 0;
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Rate") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Rate").ColNo) - 1;
                                if (colno > 0)
                                {
                                    if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                    {
                                        bt.Rate = Convert.ToDecimal(dr[colno].ToString());
                                        bt.Total = decimal.Round(bt.Qty * bt.Rate, 2, MidpointRounding.AwayFromZero);
                                    }
                                    else
                                    {
                                        colno = 0;
                                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Gross") != null)
                                        {
                                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Gross").ColNo) - 1;
                                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                            {
                                                bt.Total = Convert.ToDecimal(dr[colno].ToString());
                                                bt.Rate = decimal.Round(bt.Total / bt.Qty, 2, MidpointRounding.AwayFromZero);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                colno = 0;
                                if (fieldLists.FirstOrDefault(k => k.FieldName == "Gross") != null)
                                {
                                    colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Gross").ColNo) - 1;
                                    if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                    {
                                        bt.Total = Convert.ToDecimal(dr[colno].ToString());
                                        bt.Rate = decimal.Round(bt.Total / bt.Qty, 2, MidpointRounding.AwayFromZero);
                                    }
                                }
                            }

                            var uom = _db.Uoms.FirstOrDefault(k => k.Id == bt.UomId);

                            if (uom != null && uom.RateOn == "N" && bt.Qty > 0)
                            {
                                bt.Total = decimal.Round(bt.Pcs * bt.Rate, 2, MidpointRounding.AwayFromZero);
                            }
                            else
                            {
                                bt.Total = decimal.Round(bt.Qty * bt.Rate, 2, MidpointRounding.AwayFromZero);
                            }

                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Discount") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Discount").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                    bt.DiscAmt = Convert.ToDecimal(dr[colno].ToString());
                            }
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Disc%") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Disc%").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                {
                                    bt.Disc = Convert.ToDecimal(dr[colno].ToString());
                                    bt.DiscAmt = decimal.Round(bt.Total * bt.Disc / 100, 2, MidpointRounding.AwayFromZero);
                                }
                            }
                            decimal gross = bt.Total - bt.DiscAmt;

                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "OtherAdd") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "OtherAdd").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                    bt.OtherAdd = Convert.ToDecimal(dr[colno].ToString());
                            }
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Freight") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Freight").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                    bt.Freight = Convert.ToDecimal(dr[colno].ToString());
                            }
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "FreightRate") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "FreightRate").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                {
                                    bt.FreightRate = Convert.ToDecimal(dr[colno].ToString());
                                    if (bt.FreightRate > 0)
                                    {
                                        bt.Freight = decimal.Round(bt.Qty * bt.FreightRate, 2, MidpointRounding.AwayFromZero);
                                    }
                                }
                            }
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "TcsAmt") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "TcsAmt").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                    bt.TcsAmt = Convert.ToDecimal(dr[colno].ToString());
                            }
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "TcsPer") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "TcsPer").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                {
                                    bt.TcsPer = Convert.ToDecimal(dr[colno].ToString());

                                    if (bt.TcsPer > 0)
                                        bt.TcsAmt = decimal.Round(gross * bt.TcsPer / 100, MidpointRounding.AwayFromZero);
                                }
                            }

                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "OtherLess") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "OtherLess").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                    bt.OtherLess = Convert.ToDecimal(dr[colno].ToString());
                            }
                            gross = (gross + bt.Freight + bt.OtherAdd - bt.OtherLess + bt.TcsAmt);

                            var comp = _db.Companies.FirstOrDefault(k => k.Id == KontoGlobals.CompanyId);
                            decimal CGST = 0, SGST = 0, IGST = 0, CGSTAmt = 0, SGSTAmt = 0, IGSTAmt = 0;


                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Cgst%") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Cgst%").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                {
                                    CGST = Convert.ToDecimal(dr[colno].ToString());
                                    CGSTAmt = gross * CGST / 100;
                                }
                                else
                                {
                                    colno = 0;
                                    if (fieldLists.FirstOrDefault(k => k.FieldName == "CgstAmt") != null)
                                    {
                                        colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "CgstAmt").ColNo) - 1;

                                        if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                            CGSTAmt = Convert.ToDecimal(dr[colno].ToString());
                                    }
                                }
                            }
                            else
                            {
                                colno = 0;
                                if (fieldLists.FirstOrDefault(k => k.FieldName == "CgstAmt") != null)
                                {
                                    colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "CgstAmt").ColNo) - 1;
                                    if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                        CGSTAmt = Convert.ToDecimal(dr[colno].ToString());
                                }
                            }

                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Sgst%") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Sgst%").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                {
                                    SGST = Convert.ToDecimal(dr[colno].ToString());
                                    SGSTAmt = gross * SGST / 100;
                                }
                                else
                                {
                                    colno = 0;
                                    if (fieldLists.FirstOrDefault(k => k.FieldName == "SgstAmt") != null)
                                    {
                                        colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "SgstAmt").ColNo) - 1;
                                        if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                            SGSTAmt = Convert.ToDecimal(dr[colno].ToString());
                                    }
                                }
                            }
                            else
                            {
                                colno = 0;
                                if (fieldLists.FirstOrDefault(k => k.FieldName == "SgstAmt") != null)
                                {
                                    colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "SgstAmt").ColNo) - 1;
                                    if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                        SGSTAmt = Convert.ToDecimal(dr[colno].ToString());
                                }
                            }

                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Igst%") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Igst%").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                {
                                    IGST = Convert.ToDecimal(dr[colno].ToString());
                                    IGSTAmt = gross * IGST / 100;
                                }
                                else
                                {
                                    colno = 0;
                                    if (fieldLists.FirstOrDefault(k => k.FieldName == "IgstAmt") != null)
                                    {
                                        colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "IgstAmt").ColNo) - 1;
                                        if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                            IGSTAmt = Convert.ToDecimal(dr[colno].ToString());
                                    }
                                }
                            }
                            else
                            {
                                colno = 0;
                                if (fieldLists.FirstOrDefault(k => k.FieldName == "IgstAmt") != null)
                                {
                                    colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "IgstAmt").ColNo) - 1;
                                    if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                        IGSTAmt = Convert.ToDecimal(dr[colno].ToString());
                                }
                            }
                            if (PurchasePara.Tax_RoundOff)
                            {
                                if (bill.StateId != null)
                                {
                                    if (bill.StateId == comp.StateId)
                                    {
                                        if (CGST > 0)
                                            bt.CgstPer = CGST;
                                        else if (IGST > 0)
                                        {
                                            bt.CgstPer = bt.IgstPer / 2;
                                            CGSTAmt = gross * bt.CgstPer / 100;
                                        }
                                        bt.Cgst = decimal.Round(CGSTAmt, 0, MidpointRounding.AwayFromZero);
                                        bt.SgstPer = bt.CgstPer;
                                        bt.Sgst = decimal.Round(CGSTAmt, 0, MidpointRounding.AwayFromZero);
                                    }
                                    else
                                    {
                                        if (IGST > 0)
                                        {
                                            bt.IgstPer = IGST;
                                        }
                                        else
                                        {
                                            if (CGST > 0)
                                            {
                                                bt.IgstPer = CGST * 2;
                                                IGSTAmt = gross * bt.IgstPer / 100;
                                            }
                                        }
                                        bt.Igst = decimal.Round(IGSTAmt, 0, MidpointRounding.AwayFromZero);
                                    }
                                }
                                else
                                {
                                    var accState = (from ac in _db.Accs
                                                    join ad in _db.AccBals on ac.Id equals ad.AccId into join_ad
                                                    from ad in join_ad.DefaultIfEmpty()
                                                    join ct in _db.Cities on ad.CityId equals ct.Id into join_ct
                                                    from ct in join_ct.DefaultIfEmpty()
                                                    where ac.Id == bill.AccId
                                                    select new stateId
                                                    {
                                                        stateid = ct.StateId
                                                    }).FirstOrDefault();

                                    if (accState.stateid == comp.StateId)
                                    {
                                        if (CGST > 0)
                                            bt.CgstPer = CGST;
                                        else if (IGST > 0)
                                        {
                                            bt.CgstPer = IGST / 2;
                                            CGSTAmt = gross * bt.CgstPer / 100;
                                        }
                                        bt.Cgst = decimal.Round(CGSTAmt, 0, MidpointRounding.AwayFromZero);
                                        bt.SgstPer = bt.CgstPer;
                                        bt.Sgst = decimal.Round(CGSTAmt, 0, MidpointRounding.AwayFromZero);
                                    }
                                    else
                                    {
                                        if (IGST > 0)
                                        {
                                            bt.IgstPer = IGST;
                                        }
                                        else
                                        {
                                            if (CGST > 0)
                                            {
                                                bt.IgstPer = CGST * 2;
                                                IGSTAmt = gross * bt.IgstPer / 100;
                                            }
                                        }
                                        bt.Igst = decimal.Round(IGSTAmt, 0, MidpointRounding.AwayFromZero);
                                    }
                                }
                            }
                            else
                            {
                                if (bill.StateId != null)
                                {
                                    if (bill.StateId == comp.StateId)
                                    {
                                        if (CGST > 0)
                                            bt.CgstPer = CGST;
                                        else if (IGST > 0)
                                        {
                                            bt.CgstPer = IGST / 2;
                                            CGSTAmt = gross * bt.CgstPer / 100;
                                        }
                                        bt.Cgst = decimal.Round(CGSTAmt, 2, MidpointRounding.AwayFromZero);
                                        bt.SgstPer = bt.CgstPer;
                                        bt.Sgst = decimal.Round(CGSTAmt, 2, MidpointRounding.AwayFromZero);
                                    }
                                    else
                                    {
                                        if (IGST > 0)
                                        {
                                            bt.IgstPer = IGST;
                                        }
                                        else
                                        {
                                            if (CGST > 0)
                                            {
                                                bt.IgstPer = CGST * 2;
                                                IGSTAmt = gross * bt.IgstPer / 100;
                                            }
                                        }
                                        bt.Igst = decimal.Round(IGSTAmt, 2, MidpointRounding.AwayFromZero);
                                    }
                                }
                                else
                                {
                                    var accState = (from ac in _db.Accs
                                                    join ad in _db.AccBals on ac.Id equals ad.AccId into join_ad
                                                    from ad in join_ad.DefaultIfEmpty()
                                                    join ct in _db.Cities on ad.CityId equals ct.Id into join_ct
                                                    from ct in join_ct.DefaultIfEmpty()
                                                    where ac.Id == bill.AccId
                                                    select new stateId
                                                    {
                                                        stateid = ct.StateId
                                                    }).FirstOrDefault();

                                    if (accState.stateid == comp.StateId)
                                    {
                                        if (CGST > 0)
                                            bt.CgstPer = CGST;
                                        else if (IGST > 0)
                                        {
                                            bt.CgstPer = IGST / 2;
                                            CGSTAmt = gross * bt.CgstPer / 100;
                                        }
                                        bt.Cgst = decimal.Round(CGSTAmt, 2, MidpointRounding.AwayFromZero);
                                        bt.SgstPer = bt.CgstPer;
                                        bt.Sgst = decimal.Round(CGSTAmt, 2, MidpointRounding.AwayFromZero);
                                    }
                                    else
                                    {
                                        if (IGST > 0)
                                        {
                                            bt.IgstPer = IGST;
                                        }
                                        else
                                        {
                                            if (CGST > 0)
                                            {
                                                bt.IgstPer = CGST * 2;
                                                IGSTAmt = gross * bt.IgstPer / 100;
                                            }
                                        }
                                        bt.Igst = decimal.Round(IGSTAmt, 2, MidpointRounding.AwayFromZero);
                                    }
                                }
                            }


                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "CessPer") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "CessPer").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                {
                                    bt.CessPer = Convert.ToDecimal(dr[colno].ToString());
                                    bt.Cess = decimal.Round(bt.Qty * bt.CessPer, 2, MidpointRounding.AwayFromZero);
                                }
                            }
                            if (bt.CessPer > 0)
                                bt.Cess = decimal.Round(bt.Qty * bt.CessPer, 2, MidpointRounding.AwayFromZero);
                            else
                            {
                                colno = 0;
                                if (fieldLists.FirstOrDefault(k => k.FieldName == "Cess") != null)
                                {
                                    colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Cess").ColNo) - 1;
                                    if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                        bt.Cess = Convert.ToDecimal(dr[colno].ToString());
                                }
                            }

                            bool isImortOrSez;
                            if (bill.BillType == "Import" || bill.BillType == "Received from SEZ")
                            {
                                isImortOrSez = true;
                            }
                            else
                                isImortOrSez = false;

                            if ((bill.Rcm == "YES" || isImortOrSez))
                            {
                                bt.NetTotal = gross;
                            }
                            else if (!isImortOrSez)
                            {
                                bt.NetTotal = gross + bt.Sgst + bt.Cgst + bt.Igst + bt.Cess + bt.OtherAdd - bt.OtherLess; // + er.CessAmt; 
                            }

                            var orderexist = BillList.FirstOrDefault(k => k.RefNo == bill.RefNo && k.IsDeleted == false);
                            if (orderexist == null)
                            {
                                bill.Id = pid;
                                BillList.Add(bill);

                                bt.BillId = bill.Id;
                                bt.IsActive = true;
                                bt.IsDeleted = false;
                                bt.CreateUser = KontoGlobals.UserName;
                                bt.CreateDate = DateTime.Now;
                                BillTrans.Add(bt);
                            }
                            else
                            {
                                bt.BillId = orderexist.Id;
                                bt.CreateUser = KontoGlobals.UserName;
                                bt.CreateDate = DateTime.Now;
                                BillTrans.Add(bt);
                            }
                        }
                        else
                        {
                            BillList = new List<BillModel>();
                            BillTrans = new List<BillTransModel>();
                            if (splashScreenManager1.IsSplashFormVisible)
                                splashScreenManager1.CloseWaitForm();
                            MessageBoxAdv.Show(this, "Can not upload bills!!", "Exception ", "Entry Already Exist for this party");
                            return;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BillList = new List<BillModel>();
                BillTrans = new List<BillTransModel>();

                if (splashScreenManager1.IsSplashFormVisible)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                MessageBoxAdv.Show(this, "Error found while uploading Excel", "Exception ", ex.ToString());

                Log.Error(ex, "Upload ViewModel Upload product List.");
            }
            using (var _tran = _db.Database.BeginTransaction())
            {
                try
                {
                    BillList = new List<BillModel>(BillList.OrderBy(k => k.VDate));
                    string FirstVNo = "";
                    string LstVNo = "";
                    decimal TotalQty = 0;
                    decimal TotalAmt = 0;
                    BillModel bm;
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<BillModel, BillModel>().ForMember(x => x.Id, p => p.Ignore());

                    });
                    var map = new Mapper(config);
                    foreach (var b in BillList)
                    {
                        //Show Progress
                        //intpro++;
                        //double pro = ((double)intpro * 100) / (double)BillList.Count;
                        ////inc = inc + pro;
                        //DXSplashScreen.Progress(pro);
                        //DXSplashScreen.SetState(string.Format("Completed {0} of {1}", intpro, BillList.Count));

                        b.VoucherNo = DbUtils.NextSerialNo(b.VoucherId, _db);

                        if (string.IsNullOrEmpty(FirstVNo))
                            FirstVNo = b.VoucherNo;

                        LstVNo = b.VoucherNo;
                        var btrans = BillTrans.Where(k => k.BillId == b.Id).ToList();
                        var totl = btrans.Sum(k => k.Total);
                        b.GrossAmount = Convert.ToDecimal(totl);

                        var nettotl = btrans.Sum(k => k.NetTotal);

                        var totlQty = btrans.Sum(k => k.Qty);
                        b.TotalQty = Convert.ToDecimal(totlQty);
                        b.TotalPcs = btrans.Sum(k => k.Qty);
                        TotalAmt = TotalAmt + nettotl;
                        TotalQty = TotalQty + totlQty;

                        var x1 = nettotl - Math.Truncate(nettotl);



                        bool isEven = false;
                        if (x1 == (decimal)0.5)
                        {
                            nettotl = nettotl + (decimal)0.01;
                            isEven = true;
                        }

                        var round = decimal.Round(nettotl, 0) - nettotl;
                        if (isEven)
                        {
                            round = (decimal)0.5;
                            nettotl = nettotl + (decimal)0.49;
                        }
                        else
                        {
                            nettotl = nettotl + round;
                        }

                        b.RoundOff = round;
                        b.TotalAmount = nettotl;

                        bm = new BillModel();
                        map = new Mapper(config);
                        map.Map(b, bm);

                        _db.Bills.Add(bm);
                        _db.SaveChanges();

                        foreach (var t in btrans)
                        {
                            t.BillId = bm.Id;
                            _db.BillTrans.Add(t);
                        }
                        _db.SaveChanges();

                        //Bill Reference Update
                        LedgerEff.BillRefEntry("Credit", bm, 0, _db);       //Insert or update in Billref table

                        //Insert or update in LedgerTrans table
                        LedgerEff.LedgerTransEntry("Credit", bm, _db, btrans);

                        _db.SaveChanges();
                    }

                    _db.SaveChanges();

                    _tran.Commit();

                    if (splashScreenManager1.IsSplashFormVisible)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }
                    MessageBoxAdv.Show(this, "Upload successfully with voucher No from:" + FirstVNo + " to: "
                    + LstVNo + ". Total Qty is:" + TotalQty + ". Total Amount is " + TotalAmt, "Save !!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    BillList = new List<BillModel>();
                    BillTrans = new List<BillTransModel>();

                    if (splashScreenManager1.IsSplashFormVisible)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }
                    MessageBoxAdv.Show(this, "Error found while uploading Excel", "Exception ", ex.ToString());
                    _tran.Rollback();

                    Log.Error(ex, "Upload ViewModel Upload product List.");
                }
            }
        }

        private void SaveDataInSaleRet()
        {
            //DXSplashScreen.Show<ProgressView>();
            splashScreenManager1.ShowWaitForm();
            KontoContext _db = new KontoContext();
            var pid = -1;
            //var pd = _db.Bills.FirstOrDefault(k => k.Id > 0);
            //if (pd != null)
            //{
            //    pid = _db.Bills.Max(k => k.Id);
            //}

            BillModel bill = new BillModel();
            BillTransModel bt = new BillTransModel();
            // BillRefModel billr = new BillRefModel();
            int colno = 0;

            var TModel = _db.Templates.FirstOrDefault(k => k.Id == (int)TemplateAsLookUpEdit.EditValue);
            int AccId = TModel.AccId != null ? (int)TModel.AccId : 0;

            var fieldLists = (from t in _db.Templatetrans
                              join tf in _db.TempFields on t.TempFieldId equals tf.Id into join_tf
                              from tf in join_tf.DefaultIfEmpty()
                              where t.TemplateId == TModel.Id && t.IsDeleted == false && t.ColNo > 0
                              select new TempTransDto
                              {
                                  TempFieldId = t.TempFieldId,
                                  FieldName = tf.FieldName,
                                  ColNo = t.ColNo,
                                  Id = t.Id
                              }).ToList();

            _db.Configuration.AutoDetectChangesEnabled = false;
            _db.Configuration.ValidateOnSaveEnabled = false;
            string Qty = string.Empty;
            string item = string.Empty;
            var intpro = 0;

            try
            {
                foreach (System.Data.DataRow dr in DTUpload.Rows)
                {
                    //Show Progress
                    intpro++;
                    double pro = ((double)intpro * 100) / (double)DTUpload.Rows.Count;
                    //DXSplashScreen.Progress(pro);
                    //DXSplashScreen.SetState(string.Format("Completed {0} of {1}", intpro, DTUpload.Rows.Count));
                    if (splashScreenManager1.IsSplashFormVisible)
                    {
                        splashScreenManager1.SetWaitFormDescription("Completed " + intpro + " of " + DTUpload.Rows.Count);
                    }
                    pid = pid - 1;

                    bill = new BillModel();
                    bt = new BillTransModel();
                    // billr = new BillRefModel();

                    bill.BillType = "Regular";

                    colno = 0;
                    if (fieldLists.FirstOrDefault(k => k.FieldName == "InvoiceNo") != null)
                    {
                        colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "InvoiceNo").ColNo) - 1;
                        bill.BillNo = dr[colno].ToString();
                    }
                    colno = 0;
                    if (fieldLists.FirstOrDefault(k => k.FieldName == "Item") != null)
                    {
                        colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Item").ColNo) - 1;
                        item = dr[colno].ToString();
                    }
                    colno = 0;
                    if (fieldLists.FirstOrDefault(k => k.FieldName == "Qty") != null)
                    {
                        colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Qty").ColNo) - 1;
                        Qty = dr[colno].ToString();
                    }
                    //if (!string.IsNullOrEmpty(dr[colno].ToString()))
                    if (!string.IsNullOrEmpty(item) || !string.IsNullOrEmpty(Qty))
                    {
                        if (TModel.AccId == null || TModel.AccId <= 0)
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Party") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Party").ColNo) - 1;
                                var book = dr[colno].ToString();

                                var a = _db.Accs.FirstOrDefault(k => k.AccName == book);
                                if (a != null)
                                {
                                    bill.AccId = a.Id;
                                }
                            }
                            else if (!string.IsNullOrEmpty(accLookup.SelectedText))
                            {
                                bill.AccId = (int)accLookup.SelectedValue;
                            }
                        }
                        else if (!string.IsNullOrEmpty(accLookup.SelectedText))
                        {
                            bill.AccId = (int)accLookup.SelectedValue;
                        }
                        else if (TModel.AccId > 0 || TModel.AccId != null)
                        {
                            bill.AccId = (int)TModel.AccId;
                        }

                        if (!string.IsNullOrEmpty(bookLookup.SelectedText))
                        {
                            bill.BookAcId = (int)bookLookup.SelectedValue;
                        }
                        else
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "BookId") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "BookId").ColNo) - 1;

                                var book = dr[colno].ToString();

                                var a = _db.Accs.FirstOrDefault(k => k.AccName == book);
                                if (a != null)
                                {
                                    bill.BookAcId = a.Id;
                                }
                            }
                        }

                        if (fieldLists.FirstOrDefault(k => k.FieldName == "OrderDate") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "OrderDate").ColNo) - 1;
                            //if (TempModel.Descriptions == "MEESHO")
                            //{
                            //    var datestr = dr[colno].ToString();
                            //    datestr = datestr.Substring(0, 12);
                            //    if (!string.IsNullOrEmpty(datestr))
                            //        bill.VDate = Convert.ToDateTime(datestr);
                            //}
                            //else
                            var datestr = dr[colno].ToString();
                           // datestr = datestr.Substring(0, 10);

                            if (!string.IsNullOrEmpty(datestr))
                                bill.VDate = Convert.ToDateTime(datestr);

                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "BillAmount") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "BillAmount").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bill.TotalAmount = Convert.ToDecimal(dr[colno].ToString());
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "OrderNo") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "OrderNo").ColNo) - 1;
                            bill.RefNo = dr[colno].ToString();
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Agent") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Agent").ColNo) - 1;
                            string agent = dr[colno].ToString();
                            var ac = _db.Accs.FirstOrDefault(k => k.AccName == agent);
                            if (ac != null)
                                bill.AgentId = ac.Id;

                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "EwayBillNo") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "EwayBillNo").ColNo) - 1;
                            bill.EwayBillNo = dr[colno].ToString();
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "LrNo") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "LrNo").ColNo) - 1;
                            bill.DocNo = dr[colno].ToString();
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "LrDate") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "LrDate").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bill.DocDate = Convert.ToDateTime(dr[colno].ToString());
                        }
                        else
                            bill.DocDate = DateTime.Now;

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Transport") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Transport").ColNo) - 1;
                            string agent = dr[colno].ToString();
                            var ac = _db.Accs.FirstOrDefault(k => k.AccName == agent);
                            if (ac != null)
                                bill.TransId = ac.Id;
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "PortCode") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "PortCode").ColNo) - 1;
                            bill.PortCode = dr[colno].ToString();
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "DueDays") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "DueDays").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bill.Duedays = Convert.ToInt32(dr[colno].ToString());
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "VehicleNo") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "VehicleNo").ColNo) - 1;
                            bill.VehicleNo = dr[colno].ToString();
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Remark") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Remark").ColNo) - 1;
                            bill.Remarks = dr[colno].ToString();
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "StateName") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "StateName").ColNo) - 1;
                            string stname = dr[colno].ToString();

                            var st = _db.States.FirstOrDefault(x => x.StateName == stname.ToUpper());
                            //if (st != null)
                            //{
                            bill.StateId = st.Id;
                            //}
                            //else
                            //{
                            //    MessageBoxAdv.Show(this, "Can not Save upload bills!!", "Exception ", "StateName not Matched from system State List");
                            //    // DXSplashScreen.Close();
                            //    return;
                            //}
                        }

                        //if (TModel.VoucherId != null)
                        //{
                        //    bill.VoucherId = (int)TModel.VoucherId;
                        //}
                        //else
                        //{
                        //    var vchr = _db.Vouchers.FirstOrDefault(k => k.VTypeId == TModel.VTypeId);
                        //    if (vchr != null)
                        //        bill.VoucherId = vchr.Id;
                        //}
                        bill.VoucherId = Convert.ToInt32(voucherLookup11.SelectedValue);

                        bill.VoucherDate = Convert.ToInt32(bill.VDate.ToString("yyyyMMdd"));

                        bill.DivisionId = 1;
                        bill.YearId = KontoGlobals.YearId;
                        bill.CompId = KontoGlobals.CompanyId;
                        bill.BranchId = KontoGlobals.BranchId;
                        bill.IsActive = true;
                        bill.IsDeleted = false;
                        bill.CreateUser = KontoGlobals.UserName;
                        bill.CreateDate = DateTime.Now;
                        bill.TypeId = (int)VoucherTypeEnum.SaleReturn;
                        // Set for transaction 

                        var p = _db.Products.FirstOrDefault(k => k.ProductName == item);
                        bt.ProductId = p.Id;


                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Color") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Color").ColNo) - 1;
                            string find = dr[colno].ToString();
                            var data = _db.ColorModels.FirstOrDefault(k => k.ColorName == find);
                            if (data != null)
                                bt.ColorId = data.Id;
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Design") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Design").ColNo) - 1;
                            string find = dr[colno].ToString();
                            var data = _db.Products.FirstOrDefault(k => k.ProductName == find);
                            if (data != null)
                                bt.DesignId = data.Id;
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Grade") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Grade").ColNo) - 1;
                            string find = dr[colno].ToString();
                            var data = _db.Grades.FirstOrDefault(k => k.GradeName == find);
                            if (data != null)
                                bt.GradeId = data.Id;
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "ItemRemark") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "ItemRemark").ColNo) - 1;
                            bt.Remark = dr[colno].ToString();
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Qty") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Qty").ColNo) - 1;
                            bt.Qty = Convert.ToDecimal(dr[colno].ToString());
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Unit") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Unit").ColNo) - 1;
                            var unit = dr[colno].ToString();
                            var u = _db.Uoms.FirstOrDefault(k => k.UnitName == unit);
                            if (u != null)
                            {
                                bt.UomId = u.Id;
                            }
                            else
                            {
                                u = _db.Uoms.FirstOrDefault(k => k.Id == p.UomId);
                                if (u != null)
                                {
                                    bt.UomId = u.Id;
                                }
                            }
                        }
                        else
                        {
                            var u = _db.Uoms.FirstOrDefault(k => k.Id == p.UomId);
                            if (u != null)
                            {
                                bt.UomId = u.Id;
                            }
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Rate") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Rate").ColNo) - 1;
                            if (colno > 0)
                            {
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                {
                                    bt.Rate = Convert.ToDecimal(dr[colno].ToString());
                                    bt.Total = decimal.Round(bt.Qty * bt.Rate, 2, MidpointRounding.AwayFromZero);
                                }
                                else
                                {
                                    colno = 0;
                                    if (fieldLists.FirstOrDefault(k => k.FieldName == "Gross") != null)
                                    {
                                        colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Gross").ColNo) - 1;
                                        if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                        {
                                            bt.Total = Convert.ToDecimal(dr[colno].ToString());
                                            bt.Rate = decimal.Round(bt.Total / bt.Qty, 2, MidpointRounding.AwayFromZero);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Gross") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Gross").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                {
                                    bt.Total = Convert.ToDecimal(dr[colno].ToString());
                                    bt.Rate = decimal.Round(bt.Total / bt.Qty, 2, MidpointRounding.AwayFromZero);
                                }
                            }
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Disc%") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Disc%").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bt.Disc = Convert.ToDecimal(dr[colno].ToString());
                        }
                        if (bt.Disc <= 0)
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Discount") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Discount").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                    bt.DiscAmt = Convert.ToDecimal(dr[colno].ToString());
                            }
                        }
                        else
                        {
                            bt.DiscAmt = decimal.Round(bt.Total * bt.Disc / 100, 2, MidpointRounding.AwayFromZero);
                        }
                        decimal gross = bt.Total - bt.DiscAmt;

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "OtherAdd") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "OtherAdd").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bt.OtherAdd = Convert.ToDecimal(dr[colno].ToString());
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "FreightRate") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "FreightRate").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bt.FreightRate = Convert.ToDecimal(dr[colno].ToString());
                        }

                        if (bt.FreightRate > 0)
                        {
                            bt.Freight = decimal.Round(bt.Qty * bt.FreightRate, 2, MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Freight") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Freight").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                    bt.Freight = Convert.ToDecimal(dr[colno].ToString());
                            }
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "TcsPer") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "TcsPer").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bt.TcsPer = Convert.ToDecimal(dr[colno].ToString());
                        }
                        if (bt.TcsPer > 0)
                        {
                            bt.TcsAmt = decimal.Round(gross * bt.TcsPer / 100, MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "TcsAmt") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "TcsAmt").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                    bt.TcsAmt = Convert.ToDecimal(dr[colno].ToString());
                            }
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "OtherLess") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "OtherLess").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bt.OtherLess = Convert.ToDecimal(dr[colno].ToString());
                        }
                        gross = (gross + bt.Freight + bt.OtherAdd - bt.OtherLess + bt.TcsAmt);

                        var comp = _db.Companies.FirstOrDefault(k => k.Id == KontoGlobals.CompanyId);
                        decimal CGST = 0, SGST = 0, IGST = 0, CGSTAmt = 0, SGSTAmt = 0, IGSTAmt = 0;

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Cgst%") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Cgst%").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                            {
                                CGST = Convert.ToDecimal(dr[colno].ToString());
                                CGSTAmt = gross * CGST / 100;
                            }
                            else
                            {
                                colno = 0;
                                if (fieldLists.FirstOrDefault(k => k.FieldName == "CgstAmt") != null)
                                {
                                    colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "CgstAmt").ColNo) - 1;

                                    if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                        CGSTAmt = Convert.ToDecimal(dr[colno].ToString());
                                }
                            }
                        }
                        else
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "CgstAmt") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "CgstAmt").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                    CGSTAmt = Convert.ToDecimal(dr[colno].ToString());
                            }
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Sgst%") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Sgst%").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                            {
                                SGST = Convert.ToDecimal(dr[colno].ToString());
                                SGSTAmt = gross * SGST / 100;
                            }
                            else
                            {
                                colno = 0;
                                if (fieldLists.FirstOrDefault(k => k.FieldName == "SgstAmt") != null)
                                {
                                    colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "SgstAmt").ColNo) - 1;
                                    if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                        SGSTAmt = Convert.ToDecimal(dr[colno].ToString());
                                }
                            }
                        }
                        else
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "SgstAmt") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "SgstAmt").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                    SGSTAmt = Convert.ToDecimal(dr[colno].ToString());
                            }
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Igst%") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Igst%").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                            {
                                IGST = Convert.ToDecimal(dr[colno].ToString());
                                IGSTAmt = gross * IGST / 100;
                            }
                            else
                            {
                                colno = 0;
                                if (fieldLists.FirstOrDefault(k => k.FieldName == "IgstAmt") != null)
                                {
                                    colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "IgstAmt").ColNo) - 1;
                                    if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                        IGSTAmt = Convert.ToDecimal(dr[colno].ToString());
                                }
                            }
                        }
                        else
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "IgstAmt") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "IgstAmt").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                    IGSTAmt = Convert.ToDecimal(dr[colno].ToString());
                            }
                        }
                        if (BillPara.Tax_RoundOff)
                        {
                            if (bill.StateId != null)
                            {
                                if (bill.StateId == comp.StateId)
                                {
                                    if (CGST > 0)
                                        bt.CgstPer = CGST;
                                    else if (IGST > 0)
                                    {
                                        bt.CgstPer = IGST / 2;
                                        CGSTAmt = gross * bt.CgstPer / 100;
                                    }
                                    bt.Cgst = decimal.Round(CGSTAmt, 0, MidpointRounding.AwayFromZero);
                                    bt.SgstPer = bt.CgstPer;
                                    bt.Sgst = decimal.Round(CGSTAmt, 0, MidpointRounding.AwayFromZero);
                                }
                                else
                                {
                                    if (IGST > 0)
                                    {
                                        bt.IgstPer = IGST;
                                    }
                                    else
                                    {
                                        if (CGST > 0)
                                        {
                                            bt.IgstPer = CGST * 2;
                                            IGSTAmt = gross * bt.IgstPer / 100;
                                        }
                                    }
                                    bt.Igst = decimal.Round(IGSTAmt, 0, MidpointRounding.AwayFromZero);
                                }
                            }
                            else
                            {

                                var accState = (from ac in _db.Accs
                                                join ad in _db.AccBals on ac.Id equals ad.AccId into join_ad
                                                from ad in join_ad.DefaultIfEmpty()
                                                join ct in _db.Cities on ad.CityId equals ct.Id into join_ct
                                                from ct in join_ct.DefaultIfEmpty()
                                                where ac.Id == bill.AccId
                                                select new stateId
                                                {
                                                    stateid = ct.StateId
                                                }
                                                ).FirstOrDefault();

                                if (accState.stateid == comp.StateId)
                                {
                                    if (CGST > 0)
                                        bt.CgstPer = CGST;
                                    else if (IGST > 0)
                                    {
                                        bt.CgstPer = IGST / 2;
                                        CGSTAmt = gross * bt.CgstPer / 100;
                                    }
                                    bt.Cgst = decimal.Round(CGSTAmt, 0, MidpointRounding.AwayFromZero);
                                    bt.SgstPer = bt.CgstPer;
                                    bt.Sgst = decimal.Round(CGSTAmt,0, MidpointRounding.AwayFromZero);
                                }
                                else
                                {
                                    if (IGST > 0)
                                    {
                                        bt.IgstPer = IGST;
                                    }
                                    else
                                    {
                                        if (CGST > 0)
                                        {
                                            bt.IgstPer = CGST * 2;
                                            IGSTAmt = gross * bt.IgstPer / 100;
                                        }
                                    }
                                    bt.Igst = decimal.Round(IGSTAmt, 0, MidpointRounding.AwayFromZero);
                                }
                            }
                        }
                        else
                        {
                            if (bill.StateId != null)
                            {
                                if (bill.StateId == comp.StateId)
                                {
                                    if (CGST > 0)
                                        bt.CgstPer = CGST;
                                    else if (IGST > 0)
                                    {
                                        bt.CgstPer = IGST / 2;
                                        CGSTAmt = gross * bt.CgstPer / 100;
                                    }
                                    bt.Cgst = decimal.Round(CGSTAmt, 2, MidpointRounding.AwayFromZero);
                                    bt.SgstPer = bt.CgstPer;
                                    bt.Sgst = decimal.Round(CGSTAmt, 2, MidpointRounding.AwayFromZero);
                                }
                                else
                                {
                                    if (IGST > 0)
                                    {
                                        bt.IgstPer = IGST;
                                    }
                                    else
                                    {
                                        if (CGST > 0)
                                        {
                                            bt.IgstPer = CGST * 2;
                                            IGSTAmt = gross * bt.IgstPer / 100;
                                        }
                                    }
                                    bt.Igst = decimal.Round(IGSTAmt, 2, MidpointRounding.AwayFromZero);
                                }
                            }
                            else
                            {
                                var accState = (from ac in _db.Accs
                                                join ad in _db.AccBals on ac.Id equals ad.AccId into join_ad
                                                from ad in join_ad.DefaultIfEmpty()
                                                join ct in _db.Cities on ad.CityId equals ct.Id into join_ct
                                                from ct in join_ct.DefaultIfEmpty()
                                                where ac.Id == bill.AccId
                                                select new stateId
                                                {
                                                    stateid = ct.StateId
                                                }).FirstOrDefault();

                                if (accState.stateid == comp.StateId)
                                {
                                    if (CGST > 0)
                                        bt.CgstPer = CGST;
                                    else if (IGST > 0)
                                    {
                                        bt.CgstPer = IGST / 2;
                                        CGSTAmt = gross * bt.CgstPer / 100;
                                    }
                                    bt.Cgst = decimal.Round(CGSTAmt, 2, MidpointRounding.AwayFromZero);
                                    bt.SgstPer = bt.CgstPer;
                                    bt.Sgst = decimal.Round(CGSTAmt, 2, MidpointRounding.AwayFromZero);
                                }
                                else
                                {
                                    if (IGST > 0)
                                    {
                                        bt.IgstPer = IGST;
                                    }
                                    else
                                    {
                                        if (CGST > 0)
                                        {
                                            bt.IgstPer = CGST * 2;
                                            IGSTAmt = gross * bt.IgstPer / 100;
                                        }
                                    }
                                    bt.Igst = decimal.Round(IGSTAmt, 2, MidpointRounding.AwayFromZero);
                                }
                            }
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "CessPer") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "CessPer").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bt.CessPer = Convert.ToDecimal(dr[colno].ToString());
                        }
                        if (bt.CessPer > 0)
                            bt.Cess = decimal.Round(bt.Qty * bt.CessPer, 2, MidpointRounding.AwayFromZero);
                        else
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Cess") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Cess").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                    bt.Cess = Convert.ToDecimal(dr[colno].ToString());
                            }
                        }
                        bt.NetTotal = gross + bt.Sgst + bt.Cgst + bt.Igst + bt.Cess; // + er.CessAmt; 
                                                                                     //colno = 0;
                                                                                     //colno = tempRepo.GetColNo(TempModel.Descriptions, VTypeId, Model.AccId, "TotalAmt");
                                                                                     //if (colno > 0)
                                                                                     //    bt.NetTotal = Convert.ToDecimal(dr[colno].ToString());

                        var orderexist = BillList.FirstOrDefault(k => k.RefNo == bill.RefNo && k.IsDeleted == false);
                        if (orderexist == null)
                        {
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
                        else
                        {
                            bt.BillId = orderexist.Id;
                            bt.CreateUser = KontoGlobals.UserName;
                            bt.CreateDate = DateTime.Now;
                            BillTrans.Add(bt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // NullProducts = BillTrans.Where(k => k.ProductId == null).Count();

                BillList = new List<BillModel>();
                BillTrans = new List<BillTransModel>();
                // BillRef = new List<BillRefModel>();
                //DXSplashScreen.Close(); 
                MessageBoxAdv.Show(this, "Error found while uploading Excel", "Exception ", ex.ToString());
                Log.Error(ex, "Upload ViewModel Upload product List.");
                //process = false;
            }
            using (var _tran = _db.Database.BeginTransaction())
            {
                try
                {
                    BillList = new List<BillModel>(BillList.OrderBy(k => k.VDate));

                    string firstVNo = "";
                    string LstVNo = "";
                    decimal totalQty = 0;
                    decimal TotalAmount = 0;
                    List<BillTransModel> Trans = new List<BillTransModel>();

                    BillModel bm;

                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<BillModel, BillModel>().ForMember(x => x.Id, p => p.Ignore());

                    });
                    var map = new Mapper(config);

                    foreach (var b in BillList)
                    {
                        //Show Progress
                        //intpro++;
                        //double pro = ((double)intpro * 100) / (double)BillList.Count;
                        //DXSplashScreen.Progress(pro);
                        //DXSplashScreen.SetState(string.Format("Completed {0} of {1}", intpro, BillList.Count));

                        b.VoucherNo = DbUtils.NextSerialNo(b.VoucherId, _db);

                        if (string.IsNullOrEmpty(firstVNo))
                            firstVNo = b.VoucherNo;

                        LstVNo = b.VoucherNo;

                        //Sum for multiple order of same bill
                        var btrans = BillTrans.Where(k => k.BillId == b.Id).ToList();
                        var totl = btrans.Sum(k => k.Total);
                        b.GrossAmount = Convert.ToDecimal(totl);

                        var nettotl = btrans.Sum(k => k.NetTotal);
                        b.TotalAmount = Convert.ToDecimal(nettotl);

                        var totlQty = btrans.Sum(k => k.Qty);
                        b.TotalQty = Convert.ToDecimal(totlQty);

                        totalQty = totalQty + totlQty;
                        TotalAmount = TotalAmount + nettotl;

                        var x1 = nettotl - Math.Truncate(nettotl);

                        bool isEven = false;
                        if (x1 == (decimal)0.5)
                        {
                            nettotl = nettotl + (decimal)0.01;
                            isEven = true;
                        }

                        var round = decimal.Round(nettotl, 0) - nettotl;
                        if (isEven)
                        {
                            round = (decimal)0.5;
                            nettotl = nettotl + (decimal)0.49;
                        }
                        else
                        {
                            nettotl = nettotl + round;
                        }

                        b.RoundOff = round;
                        b.TotalAmount = Convert.ToDecimal(nettotl);

                        //find original bill for btob adjust and original bill number and date
                        int vtypeid = (int)VoucherTypeEnum.SaleInvoice;
                        BillModel OrgBill = _db.Bills.FirstOrDefault(k => k.RefNo == b.RefNo
                                            && k.TypeId == vtypeid);

                        BillRefModel billRefModel = new BillRefModel();
                        if (OrgBill != null)
                        {
                            b.BillNo = OrgBill.VoucherNo;
                            b.RcdDate = DateTime.ParseExact(OrgBill.VoucherDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture);

                            billRefModel = _db.BillRefs.FirstOrDefault(k => k.BillId == OrgBill.Id
                                            && k.BillVoucherId == OrgBill.VoucherId && k.IsDeleted == false
                                            && k.IsActive && k.VoucherNo == OrgBill.VoucherNo);

                        }
                        else
                            b.RcdDate = DateTime.Now;

                        bm = new BillModel();
                        map = new Mapper(config);
                        map.Map(b, bm);

                        _db.Bills.Add(bm);

                        _db.SaveChanges();
                        foreach (var t in btrans)
                        {
                            t.BillId = bm.Id;
                            _db.BillTrans.Add(t);
                        }

                        _db.SaveChanges();

                        //Bill Reference Update
                        LedgerEff.BillRefEntry("Credit", bm, 0, _db);       //Insert or update in Billref table

                        //Insert or update in LedgerTrans table
                        LedgerEff.LedgerTransEntry("Credit", bm, _db, btrans);

                        // Insert in BtoB for BillAdjustment
                        if (OrgBill != null && billRefModel!=null)
                            LedgerEff.BtoBEntryUpload("Return", bm.Id, bm, _db, OrgBill, billRefModel.RowId);

                        ////Transaction entry
                        //foreach (var t in btrans)
                        //{
                        //    bool IsIssue = false;
                        //    string TableName = "SaleReturn";
                        //    var stockReq = _db.Products.FirstOrDefault(k => k.Id == t.ProductId).StockReq;
                        //    if (stockReq == "No")
                        //    {
                        //        continue;
                        //    }
                        //    StockEffect.StockTransBillEntry(bm, t, IsIssue, TableName, _db);
                        //}

                        _db.SaveChanges();
                    }

                    _db.SaveChanges();

                    _tran.Commit();
                    // DXSplashScreen.Close();
                    if (splashScreenManager1.IsSplashFormVisible)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }
                    MessageBoxAdv.Show(this, "Upload successfully with voucher No from:" + firstVNo + " to: "
                        + LstVNo + ". Total Qty is:" + totalQty + ". Total Amount is " + TotalAmount, "Save !!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    // NullProducts = BillTrans.Where(k => k.ProductId == null).Count();
                    if (splashScreenManager1.IsSplashFormVisible)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }
                    BillList = new List<BillModel>();
                    BillTrans = new List<BillTransModel>();
                    // BillRef = new List<BillRefModel>();
                    //DXSplashScreen.Close(); 
                    MessageBoxAdv.Show(this, "Error found while uploading Excel", "Exception ", ex.ToString());
                    _tran.Rollback();
                    Log.Error(ex, "Upload ViewModel Upload product List.");
                    // process = false;
                }
            }
        }
        private void SaveDataInSaleInvoice()
        {
            //DXSplashScreen.Show<ProgressView>();
            splashScreenManager1.ShowWaitForm();

            KontoContext _db = new KontoContext();
            var pid = -1;
            //var pd = _db.Bills.FirstOrDefault(k => k.Id > 0);
            //if (pd != null)
            //{
            //    pid = _db.Bills.Max(k => k.Id);
            //}

            BillModel bill = new BillModel();
            BillTransModel bt = new BillTransModel();
            // BillRefModel billr = new BillRefModel();
            int colno = 0;

            var TModel = _db.Templates.FirstOrDefault(k => k.Id == (int)TemplateAsLookUpEdit.EditValue);
            int AccId = TModel.AccId != null ? (int)TModel.AccId : 0;

            var fieldLists = (from t in _db.Templatetrans
                              join tf in _db.TempFields on t.TempFieldId equals tf.Id into join_tf
                              from tf in join_tf.DefaultIfEmpty()
                              where t.TemplateId == TModel.Id && t.IsDeleted == false && t.ColNo > 0
                              select new TempTransDto
                              {
                                  TempFieldId = t.TempFieldId,
                                  FieldName = tf.FieldName,
                                  ColNo = t.ColNo,
                                  Id = t.Id
                              }).ToList();

            _db.Configuration.AutoDetectChangesEnabled = false;
            _db.Configuration.ValidateOnSaveEnabled = false;
            string Qty = string.Empty;
            string item = string.Empty;
            var intpro = 0;

            try
            {

                foreach (System.Data.DataRow dr in DTUpload.Rows)
                {
                    //Show Progress
                    intpro++;
                    double pro = ((double)intpro * 100) / (double)DTUpload.Rows.Count;
                    //DXSplashScreen.Progress(pro);
                    //DXSplashScreen.SetState(string.Format("Completed {0} of {1}", intpro, DTUpload.Rows.Count));

                    if (splashScreenManager1.IsSplashFormVisible)
                    {
                        splashScreenManager1.SetWaitFormDescription("Completed " + intpro + " of " + DTUpload.Rows.Count);
                    }
                    pid = pid - 1;

                    bill = new BillModel();
                    bt = new BillTransModel();
                    // billr = new BillRefModel();

                    bill.BillType = "Regular";

                    colno = 0;
                    if (fieldLists.FirstOrDefault(k => k.FieldName == "InvoiceNo") != null)
                    {
                        colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "InvoiceNo").ColNo) - 1;
                        bill.BillNo = dr[colno].ToString();
                    }
                    colno = 0;
                    if (fieldLists.FirstOrDefault(k => k.FieldName == "Item") != null)
                    {
                        colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Item").ColNo) - 1;
                        item = dr[colno].ToString();
                    }
                    colno = 0;
                    if (fieldLists.FirstOrDefault(k => k.FieldName == "Qty") != null)
                    {
                        colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Qty").ColNo) - 1;
                        Qty = dr[colno].ToString();
                    }
                    //if (!string.IsNullOrEmpty(dr[colno].ToString()))
                    if (!string.IsNullOrEmpty(item) || !string.IsNullOrEmpty(Qty))
                    {
                        if (TModel.AccId == null || TModel.AccId <= 0)
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Party") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Party").ColNo) - 1;
                                var book = dr[colno].ToString();

                                var a = _db.Accs.FirstOrDefault(k => k.AccName == book);
                                if (a != null)
                                {
                                    bill.AccId = a.Id;
                                    bill.DelvAccId = a.Id;
                                }
                            }
                            else if (!string.IsNullOrEmpty(accLookup.SelectedText))
                            {
                                bill.AccId = (int)accLookup.SelectedValue;
                                bill.DelvAccId = (int)accLookup.SelectedValue;
                            }
                        }
                        else if (!string.IsNullOrEmpty(accLookup.SelectedText))
                        {
                            bill.AccId = (int)accLookup.SelectedValue;
                            bill.DelvAccId = (int)accLookup.SelectedValue;
                        }
                        else if (TModel.AccId > 0 || TModel.AccId != null)
                        {
                            bill.AccId = (int)TModel.AccId;
                            bill.DelvAccId= (int)TModel.AccId;
                        }
                        
                        if (!string.IsNullOrEmpty(bookLookup.SelectedText))
                        {
                            bill.BookAcId = (int)bookLookup.SelectedValue;
                        }
                        else
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "BookId") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "BookId").ColNo) - 1;

                                var book = dr[colno].ToString();

                                var a = _db.Accs.FirstOrDefault(k => k.AccName == book);
                                if (a != null)
                                {
                                    bill.BookAcId = a.Id;
                                }
                            }
                            else
                            {
                                if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                                MessageBox.Show("Invalid Sales Book Selection or Mapping");
                                return;
                            }
                        }

                        if (fieldLists.FirstOrDefault(k => k.FieldName == "OrderDate") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "OrderDate").ColNo) - 1;
                            //if (TempModel.Descriptions == "MEESHO")
                            //{
                            //    var datestr = dr[colno].ToString();
                            //    datestr = datestr.Substring(0, 12);
                            //    if (!string.IsNullOrEmpty(datestr))
                            //        bill.VDate = Convert.ToDateTime(datestr);
                            //}
                            //else
                            var datestr = dr[colno].ToString();
                           // datestr = datestr.Substring(0, 10);

                            if (!string.IsNullOrEmpty(datestr))
                                bill.VDate = Convert.ToDateTime(datestr);

                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "BillAmount") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "BillAmount").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bill.TotalAmount = Convert.ToDecimal(dr[colno].ToString());
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "OrderNo") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "OrderNo").ColNo) - 1;
                            bill.RefNo = dr[colno].ToString();
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Agent") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Agent").ColNo) - 1;
                            string agent = dr[colno].ToString();
                            var ac = _db.Accs.FirstOrDefault(k => k.AccName == agent);
                            if (ac != null)
                                bill.AgentId = ac.Id;

                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "EwayBillNo") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "EwayBillNo").ColNo) - 1;
                            bill.EwayBillNo = dr[colno].ToString();
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "LrNo") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "LrNo").ColNo) - 1;
                            bill.DocNo = dr[colno].ToString();
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "LrDate") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "LrDate").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bill.DocDate = Convert.ToDateTime(dr[colno].ToString());
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Transport") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Transport").ColNo) - 1;
                            string agent = dr[colno].ToString();
                            var ac = _db.Accs.FirstOrDefault(k => k.AccName == agent);
                            if (ac != null)
                                bill.TransId = ac.Id;
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "PortCode") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "PortCode").ColNo) - 1;
                            bill.PortCode = dr[colno].ToString();
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "DueDays") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "DueDays").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bill.Duedays = Convert.ToInt32(dr[colno].ToString());
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "VehicleNo") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "VehicleNo").ColNo) - 1;
                            bill.VehicleNo = dr[colno].ToString();
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Remark") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Remark").ColNo) - 1;
                            bill.Remarks = dr[colno].ToString();
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "StateName") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "StateName").ColNo) - 1;
                            string stname = dr[colno].ToString();

                            var st = _db.States.FirstOrDefault(x => x.StateName == stname.ToUpper());
                            //if (st != null)
                            //{
                            bill.StateId = st.Id;
                            //}
                            //else
                            //{
                            //    MessageBoxAdv.Show(this, "Can not Save upload bills!!", "Exception ", "StateName not Matched from system State List");
                            //    // DXSplashScreen.Close();
                            //    return;
                            //}
                        }

                        //if (TModel.VoucherId != null)
                        //{
                        //    bill.VoucherId = (int)TModel.VoucherId;
                        //}
                        //else
                        //{
                        //    var vchr = _db.Vouchers.FirstOrDefault(k => k.VTypeId == TModel.VTypeId);
                        //    if (vchr != null)
                        //        bill.VoucherId = vchr.Id;
                        //}

                        bill.VoucherId = Convert.ToInt32(voucherLookup11.SelectedValue);

                        bill.VoucherDate = Convert.ToInt32(bill.VDate.ToString("yyyyMMdd"));

                        bill.DivisionId = 1;
                        bill.TypeId = (int)VoucherTypeEnum.SaleInvoice;
                        bill.YearId = KontoGlobals.YearId;
                        bill.CompId = KontoGlobals.CompanyId;
                        bill.IsActive = true;
                        bill.BranchId = KontoGlobals.BranchId;
                        bill.IsDeleted = false;
                        bill.CreateUser = KontoGlobals.UserName;
                        bill.CreateDate = DateTime.Now;

                        // Set for transaction 

                        var p = _db.Products.FirstOrDefault(k => k.ProductName == item);
                        bt.ProductId = p.Id;


                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Color") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Color").ColNo) - 1;
                            string find = dr[colno].ToString();
                            var data = _db.ColorModels.FirstOrDefault(k => k.ColorName == find);
                            if (data != null)
                                bt.ColorId = data.Id;
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Design") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Design").ColNo) - 1;
                            string find = dr[colno].ToString();
                            var data = _db.Products.FirstOrDefault(k => k.ProductName == find);
                            if (data != null)
                                bt.DesignId = data.Id;
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Grade") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Grade").ColNo) - 1;
                            string find = dr[colno].ToString();
                            var data = _db.Grades.FirstOrDefault(k => k.GradeName == find);
                            if (data != null)
                                bt.GradeId = data.Id;
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "ItemRemark") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "ItemRemark").ColNo) - 1;
                            bt.Remark = dr[colno].ToString();
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Qty") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Qty").ColNo) - 1;
                            bt.Qty = Convert.ToDecimal(dr[colno].ToString());
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Unit") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Unit").ColNo) - 1;
                            var unit = dr[colno].ToString();
                            var u = _db.Uoms.FirstOrDefault(k => k.UnitName == unit);
                            if (u != null)
                            {
                                bt.UomId = u.Id;
                            }
                            else
                            {
                                u = _db.Uoms.FirstOrDefault(k => k.Id == p.UomId);
                                if (u != null)
                                {
                                    bt.UomId = u.Id;
                                }
                            }
                        }
                        else
                        {
                            var u = _db.Uoms.FirstOrDefault(k => k.Id == p.UomId);
                            if (u != null)
                            {
                                bt.UomId = u.Id;
                            }
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Rate") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Rate").ColNo) - 1;
                            if (colno > 0)
                            {
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                {
                                    bt.Rate = Convert.ToDecimal(dr[colno].ToString());
                                    bt.Total = decimal.Round(bt.Qty * bt.Rate, 2, MidpointRounding.AwayFromZero);
                                }
                                else
                                {
                                    colno = 0;
                                    if (fieldLists.FirstOrDefault(k => k.FieldName == "Gross") != null)
                                    {
                                        colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Gross").ColNo) - 1;
                                        if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                        {
                                            bt.Total = Convert.ToDecimal(dr[colno].ToString());
                                            bt.Rate = decimal.Round(bt.Total / bt.Qty, 2, MidpointRounding.AwayFromZero);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Gross") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Gross").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                {
                                    bt.Total = Convert.ToDecimal(dr[colno].ToString());
                                    bt.Rate = decimal.Round(bt.Total / bt.Qty, 2, MidpointRounding.AwayFromZero);
                                }
                            }
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Disc%") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Disc%").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bt.Disc = Convert.ToDecimal(dr[colno].ToString());
                        }
                        if (bt.Disc <= 0)
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Discount") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Discount").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                    bt.DiscAmt = Convert.ToDecimal(dr[colno].ToString());
                            }
                        }
                        else
                        {
                            bt.DiscAmt = decimal.Round(bt.Total * bt.Disc / 100, 2, MidpointRounding.AwayFromZero);
                        }
                        decimal gross = bt.Total - bt.DiscAmt;

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "OtherAdd") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "OtherAdd").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bt.OtherAdd = Convert.ToDecimal(dr[colno].ToString());
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "FreightRate") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "FreightRate").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bt.FreightRate = Convert.ToDecimal(dr[colno].ToString());
                        }

                        if (bt.FreightRate > 0)
                        {
                            bt.Freight = decimal.Round(bt.Qty * bt.FreightRate, 2, MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Freight") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Freight").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                    bt.Freight = Convert.ToDecimal(dr[colno].ToString());
                            }
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "TcsPer") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "TcsPer").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bt.TcsPer = Convert.ToDecimal(dr[colno].ToString());
                        }
                        if (bt.TcsPer > 0)
                        {
                            bt.TcsAmt = decimal.Round(gross * bt.TcsPer / 100, MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "TcsAmt") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "TcsAmt").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                    bt.TcsAmt = Convert.ToDecimal(dr[colno].ToString());
                            }
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "OtherLess") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "OtherLess").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bt.OtherLess = Convert.ToDecimal(dr[colno].ToString());
                        }
                        gross = (gross + bt.Freight + bt.OtherAdd - bt.OtherLess + bt.TcsAmt);

                        var comp = _db.Companies.FirstOrDefault(k => k.Id == KontoGlobals.CompanyId);
                        decimal CGST = 0, SGST = 0, IGST = 0, CGSTAmt = 0, SGSTAmt = 0, IGSTAmt = 0;

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Cgst%") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Cgst%").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                            {
                                CGST = Convert.ToDecimal(dr[colno].ToString());
                                CGSTAmt = gross * CGST / 100;
                            }
                            else
                            {
                                colno = 0;
                                if (fieldLists.FirstOrDefault(k => k.FieldName == "CgstAmt") != null)
                                {
                                    colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "CgstAmt").ColNo) - 1;

                                    if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                        CGSTAmt = Convert.ToDecimal(dr[colno].ToString());
                                }
                            }
                        }
                        else
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "CgstAmt") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "CgstAmt").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                    CGSTAmt = Convert.ToDecimal(dr[colno].ToString());
                            }
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Sgst%") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Sgst%").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                            {
                                SGST = Convert.ToDecimal(dr[colno].ToString());
                                SGSTAmt = gross * SGST / 100;
                            }
                            else
                            {
                                colno = 0;
                                if (fieldLists.FirstOrDefault(k => k.FieldName == "SgstAmt") != null)
                                {
                                    colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "SgstAmt").ColNo) - 1;
                                    if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                        SGSTAmt = Convert.ToDecimal(dr[colno].ToString());
                                }
                            }
                        }
                        else
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "SgstAmt") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "SgstAmt").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                    SGSTAmt = Convert.ToDecimal(dr[colno].ToString());
                            }
                        }
                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "Igst%") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Igst%").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                            {
                                IGST = Convert.ToDecimal(dr[colno].ToString());
                                IGSTAmt = gross * IGST / 100;
                            }
                            else
                            {
                                colno = 0;
                                if (fieldLists.FirstOrDefault(k => k.FieldName == "IgstAmt") != null)
                                {
                                    colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "IgstAmt").ColNo) - 1;
                                    if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                        IGSTAmt = Convert.ToDecimal(dr[colno].ToString());
                                }
                            }
                        }
                        else
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "IgstAmt") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "IgstAmt").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                    IGSTAmt = Convert.ToDecimal(dr[colno].ToString());
                            }
                        }
                        if (BillPara.Tax_RoundOff)
                        {
                            if (bill.StateId != null)
                            {
                                if (bill.StateId == comp.StateId)
                                {
                                    if (CGST > 0)
                                        bt.CgstPer = CGST;
                                    else if (IGST > 0)
                                    {
                                        bt.CgstPer = IGST / 2;
                                        CGSTAmt = gross * bt.CgstPer / 100;
                                    }
                                    bt.Cgst = decimal.Round(CGSTAmt, 0, MidpointRounding.AwayFromZero);
                                    bt.SgstPer = bt.CgstPer;
                                    bt.Sgst = decimal.Round(CGSTAmt, 0, MidpointRounding.AwayFromZero);
                                }
                                else
                                {
                                    if (IGST > 0)
                                    {
                                        bt.IgstPer = IGST;
                                    }
                                    else
                                    {
                                        if (CGST > 0)
                                        {
                                            bt.IgstPer = CGST * 2;
                                            IGSTAmt = gross * bt.IgstPer / 100;
                                        }
                                    }
                                    bt.Igst = decimal.Round(IGSTAmt, 0, MidpointRounding.AwayFromZero);
                                }
                            }
                            else
                            {

                                //var accState = (from ac in _db.Accs
                                //                join ad in _db.AccBals on ac.Id equals ad.AccId into join_ad
                                //                from ad in join_ad.DefaultIfEmpty()
                                //                join ct in _db.Cities on ad.CityId equals ct.Id into join_ct
                                //                from ct in join_ct.DefaultIfEmpty()
                                //                where ac.Id == bill.AccId && ad.CompId == KontoGlobals.CompanyId &&
                                //                ad.YearId == KontoGlobals.YearId
                                //                select new stateId
                                //                {
                                //                    stateid = ct.StateId
                                //                }
                                //                ).FirstOrDefault();

                                if (accLookup.LookupDto.StateId == comp.StateId)
                                {
                                    if (CGST > 0)
                                        bt.CgstPer = CGST;
                                    else if (IGST > 0)
                                    {
                                        bt.CgstPer = IGST / 2;
                                        CGSTAmt = gross * bt.CgstPer / 100;
                                    }
                                    bt.Cgst = decimal.Round(CGSTAmt, 0, MidpointRounding.AwayFromZero);
                                    bt.SgstPer = bt.CgstPer;
                                    bt.Sgst = decimal.Round(CGSTAmt, 0, MidpointRounding.AwayFromZero);
                                }
                                else
                                {
                                    if (IGST > 0)
                                    {
                                        bt.IgstPer = IGST;
                                    }
                                    else
                                    {
                                        if (CGST > 0)
                                        {
                                            bt.IgstPer = CGST * 2;
                                            IGSTAmt = gross * bt.IgstPer / 100;
                                        }
                                    }
                                    bt.Igst = decimal.Round(IGSTAmt, 0, MidpointRounding.AwayFromZero);
                                }
                            }
                        }
                        else
                        {
                            if (bill.StateId != null)
                            {
                                if (bill.StateId == comp.StateId)
                                {
                                    if (CGST > 0)
                                        bt.CgstPer = CGST;
                                    else if (IGST > 0)
                                    {
                                        bt.CgstPer = IGST / 2;
                                        CGSTAmt = gross * bt.CgstPer / 100;
                                    }
                                    bt.Cgst = decimal.Round(CGSTAmt, 2, MidpointRounding.AwayFromZero);
                                    bt.SgstPer = bt.CgstPer;
                                    bt.Sgst = decimal.Round(CGSTAmt, 2, MidpointRounding.AwayFromZero);
                                }
                                else
                                {
                                    if (IGST > 0)
                                    {
                                        bt.IgstPer = IGST;
                                    }
                                    else
                                    {
                                        if (CGST > 0)
                                        {
                                            bt.IgstPer = CGST * 2;
                                            IGSTAmt = gross * bt.IgstPer / 100;
                                        }
                                    }
                                    bt.Igst = decimal.Round(IGSTAmt, 2, MidpointRounding.AwayFromZero);
                                }
                            }
                            else
                            {
                                //var accState = (from ac in _db.Accs
                                //                join ad in _db.AccBals on ac.Id equals ad.AccId into join_ad
                                //                from ad in join_ad.DefaultIfEmpty()
                                //                join ct in _db.Cities on ad.CityId equals ct.Id into join_ct
                                //                from ct in join_ct.DefaultIfEmpty()
                                //                where ac.Id == bill.AccId
                                //                select new stateId
                                //                {
                                //                    stateid = ct.StateId
                                //                }).FirstOrDefault();

                                if (accLookup.LookupDto.StateId == comp.StateId)
                                {
                                    if (CGST > 0)
                                        bt.CgstPer = CGST;
                                    else if (IGST > 0)
                                    {
                                        bt.CgstPer = IGST / 2;
                                        CGSTAmt = gross * bt.CgstPer / 100;
                                    }
                                    bt.Cgst = decimal.Round(CGSTAmt, 2, MidpointRounding.AwayFromZero);
                                    bt.SgstPer = bt.CgstPer;
                                    bt.Sgst = decimal.Round(CGSTAmt, 2, MidpointRounding.AwayFromZero);
                                }
                                else
                                {
                                    if (IGST > 0)
                                    {
                                        bt.IgstPer = IGST;
                                    }
                                    else
                                    {
                                        if (CGST > 0)
                                        {
                                            bt.IgstPer = CGST * 2;
                                            IGSTAmt = gross * bt.IgstPer / 100;
                                        }
                                    }
                                    bt.Igst = decimal.Round(IGSTAmt, 2, MidpointRounding.AwayFromZero);
                                }
                            }
                        }

                        colno = 0;
                        if (fieldLists.FirstOrDefault(k => k.FieldName == "CessPer") != null)
                        {
                            colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "CessPer").ColNo) - 1;
                            if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                bt.CessPer = Convert.ToDecimal(dr[colno].ToString());
                        }
                        if (bt.CessPer > 0)
                            bt.Cess = decimal.Round(bt.Qty * bt.CessPer, 2, MidpointRounding.AwayFromZero);
                        else
                        {
                            colno = 0;
                            if (fieldLists.FirstOrDefault(k => k.FieldName == "Cess") != null)
                            {
                                colno = Convert.ToInt32(fieldLists.FirstOrDefault(k => k.FieldName == "Cess").ColNo) - 1;
                                if (!string.IsNullOrEmpty(dr[colno].ToString()))
                                    bt.Cess = Convert.ToDecimal(dr[colno].ToString());
                            }
                        }
                        bt.NetTotal = gross + bt.Sgst + bt.Cgst + bt.Igst + bt.Cess; // + er.CessAmt; 
                                                                                     //colno = 0;
                                                                                     //colno = tempRepo.GetColNo(TempModel.Descriptions, VTypeId, Model.AccId, "TotalAmt");
                                                                                     //if (colno > 0)
                                                                                     //    bt.NetTotal = Convert.ToDecimal(dr[colno].ToString());

                        var orderexist = BillList.FirstOrDefault(k => k.RefNo == bill.RefNo && k.IsDeleted == false);
                        if (orderexist == null)
                        {
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
                        else
                        {
                            bt.BillId = orderexist.Id;
                            bt.CreateUser = KontoGlobals.UserName;
                            bt.CreateDate = DateTime.Now;
                            BillTrans.Add(bt);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                // NullProducts = BillTrans.Where(k => k.ProductId == null).Count();

                BillList = new List<BillModel>();
                BillTrans = new List<BillTransModel>();
                // BillRef = new List<BillRefModel>();
                //DXSplashScreen.Close(); 
                MessageBoxAdv.Show(this, "Error found while uploading Excel", "Exception ", ex.ToString());
                Log.Error(ex, "Upload ViewModel Upload product List.");
                //process = false;
            }
            using (var _tran = _db.Database.BeginTransaction())
            {
                try
                {
                    BillList = new List<BillModel>(BillList.OrderBy(k => k.VDate));

                    string firstVNo = "";
                    string LstVNo = "";
                    decimal totalQty = 0;
                    decimal TotalAmount = 0;

                    BillModel bm;
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<BillModel, BillModel>().ForMember(x => x.Id, p => p.Ignore());

                    });
                    var map = new Mapper(config);
                    foreach (var b in BillList)
                    {
                        //Show Progress
                        //intpro++;
                        //double pro = ((double)intpro * 100) / (double)BillList.Count;
                        //DXSplashScreen.Progress(pro);
                        //DXSplashScreen.SetState(string.Format("Completed {0} of {1}", intpro, BillList.Count));

                        var _acbal = _db.AccBals.FirstOrDefault(x => x.CompId == KontoGlobals.CompanyId
                        && x.YearId == KontoGlobals.YearId && x.AccId == b.AccId);
                        if(_acbal!=null)
                        {
                            b.DelvAccId = b.AccId;
                            b.DelvAdrId = _acbal.AddressId;
                        }

                        b.VoucherNo = DbUtils.NextSerialNo(b.VoucherId, _db);
                        b.BillNo = b.VoucherNo;

                        if (string.IsNullOrEmpty(firstVNo))
                            firstVNo = b.VehicleNo;

                        LstVNo = b.VoucherNo;

                        //Sum for multiple order of same bill
                        var btrans = BillTrans.Where(k => k.BillId == b.Id).ToList();
                        var totl = btrans.Sum(k => k.Total);
                        b.GrossAmount = Convert.ToDecimal(totl);

                        var nettotl = btrans.Sum(k => k.NetTotal);


                        var totlQty = btrans.Sum(k => k.Qty);
                        b.TotalQty = Convert.ToDecimal(totlQty);

                        totalQty = totalQty + totlQty;
                        TotalAmount = TotalAmount + nettotl;

                        var x1 = nettotl - Math.Truncate(nettotl);

                        bool isEven = false;
                        if (x1 == (decimal)0.5)
                        {
                            nettotl = nettotl + (decimal)0.01;
                            isEven = true;
                        }

                        var round = decimal.Round(nettotl, 0) - nettotl;
                        if (isEven)
                        {
                            round = (decimal)0.5;
                            nettotl = nettotl + (decimal)0.49;
                        }
                        else
                        {
                            nettotl = nettotl + round;
                        }

                        b.RoundOff = round;
                        b.TotalAmount = Convert.ToDecimal(nettotl);

                        bm = new BillModel();
                        map = new Mapper(config);
                        map.Map(b, bm);

                        _db.Bills.Add(bm);

                        //var Billr = new BillRefModel();
                        ////Insert or update in Billref table
                        //LedgerEff.BillRefEntry("Debit", KontoGlobals.UserName, b.Id, b, Billr, _db, KontoGlobals.CompanyId, KontoGlobals.FromDate, KontoGlobals.ToDate, KontoGlobals.BranchId);
                        ////Insert or update in Ledgertrans
                        //Trans = new List<BillTransModel>(btrans);
                        //LedgerEff.LedgerTransEntry("Debit", KontoGlobals.UserName, b, _db, Trans, "Sales", KontoGlobals.CompanyId, KontoGlobals.FromDate, KontoGlobals.ToDate);

                        _db.SaveChanges();
                        foreach (var t in btrans)
                        {
                            t.BillId = bm.Id;
                            if (t.Pcs == 0)
                                t.Pcs = Convert.ToInt32(t.Qty);

                            _db.BillTrans.Add(t);
                        }

                        _db.SaveChanges();

                        //Bill Reference Update
                        LedgerEff.BillRefEntry("Debit", bm, 0, _db);       //Insert or update in Billref table

                        //Insert or update in LedgerTrans table
                        LedgerEff.LedgerTransEntry("Debit", bm, _db, btrans);
                        _db.SaveChanges();
                    }

                    _db.SaveChanges();

                    _tran.Commit();
                    // DXSplashScreen.Close();
                    if (splashScreenManager1.IsSplashFormVisible)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }

                    MessageBoxAdv.Show(this, "Upload successfully with voucher No from:" + firstVNo + " to: "
                    + LstVNo + ". Total Qty is:" + totalQty + ". Total Amount is " + TotalAmount, "Save !!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    _tran.Rollback();
                    // NullProducts = BillTrans.Where(k => k.ProductId == null).Count();
                    if (splashScreenManager1.IsSplashFormVisible)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }
                    BillList = new List<BillModel>();
                    BillTrans = new List<BillTransModel>();
                    // BillRef = new List<BillRefModel>();
                    //DXSplashScreen.Close(); 
                    MessageBoxAdv.Show(this, "Error found while uploading Excel", "Exception ", ex.ToString());

                    Log.Error(ex, "Upload ViewModel Upload product List.");
                    // process = false;
                }
            }
        }
    }
    public class stateId
    {
        public int? stateid { get; set; }
    }
}