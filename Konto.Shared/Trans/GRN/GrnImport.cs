using Aspose.Cells;
using AutoMapper;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Transaction;
using Konto.Shared.Trans.Common;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Trans.GRN
{
    public partial class GrnImport : KontoForm
    {
        DataTable _dataTable;

        public GrnImport()
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
                              group row by row.Field<string>("InwardID") into grp
                              select new
                              {
                                  Id = grp.Key
                              }).ToList();


                // var impList = new List<CityModel>();
                using (var db = new KontoContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = false;
                    db.Configuration.ValidateOnSaveEnabled = false;

                    int? compStateId = db.Companies.FirstOrDefault(k => k.Id == KontoGlobals.CompanyId).StateId;
                    var challanModel = new ChallanModel();
                    var ctrans = new ChallanTransModel();
                    var Prod = new ProdModel();
                    int srNo = 0;
                    int? StateId = 0;
                    int ProductID = 0;
                    int cid = 0;
                    List<ChallanModel> challanlist = new List<ChallanModel>();
                    List<ChallanTransModel> ctlist = new List<ChallanTransModel>();
                    List<ProdModel> prodlist = new List<ProdModel>();
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

                                var dtdet = _dataTable.Select("InwardID='" + item1.Id + "'");

                                challanModel = new ChallanModel();
                                StateId = 0;
                                var model = dtdet.FirstOrDefault();

                                challanModel.VoucherNo = model["VoucherNo"].ToString();
                                cid = cid - 1;
                                challanModel.Id = cid;

                                challanModel.VoucherId = db.Vouchers.FirstOrDefault(l => l.VTypeId == (int)VoucherTypeEnum.Inward).Id;
                                if (model["InwardDate"] != null)
                                    challanModel.VoucherDate =Convert.ToInt32(  model["InwardDate"].ToString());
                                challanModel.ChallanType = 2;// Inward For Job
                                challanModel.TypeId = (int)VoucherTypeEnum.Inward;
                                challanModel.CompId = KontoGlobals.CompanyId;
                                challanModel.YearId = KontoGlobals.YearId;
                                challanModel.BranchId = KontoGlobals.BranchId;
                                challanModel.DivId = 1;
                                challanModel.StoreId = 1;
                                challanModel.IsActive = true;
                                challanModel.DocDate = DateTime.Now;

                                if (model["Party"] != null || !string.IsNullOrEmpty(model["Party"].ToString()))
                                {
                                    string accName = model["Party"].ToString().ToUpper();
                                    var acc = db.Accs.FirstOrDefault(k => k.AccName.ToUpper() == accName);

                                    if (acc != null)
                                    {
                                        challanModel.AccId = acc.Id;
                                        var accState = (from ac in db.Accs
                                                        join ad in db.AccBals on ac.Id equals ad.AccId into join_ad
                                                        from ad in join_ad.DefaultIfEmpty()
                                                        join ct in db.Cities on ad.CityId equals ct.Id into join_ct
                                                        from ct in join_ct.DefaultIfEmpty()
                                                        where ac.Id == challanModel.AccId
                                                        select new stateId
                                                        {
                                                            stateid = ct.StateId
                                                        }).FirstOrDefault();
                                        StateId = accState.stateid;

                                        if (model["ChallanDate"] != null)
                                            challanModel.RcdDate = KontoUtils.IToD(Convert.ToInt32(model["ChallanDate"]));

                                        if (model["challanNo"] != null)
                                            challanModel.ChallanNo = model["ChallanNo"].ToString();

                                        challanlist.Add(challanModel);

                                        foreach (DataRow dr in dtdet)
                                        {
                                            if (dr["Quality"] != null)
                                            {
                                                string pname = dr["Quality"].ToString();
                                                var Product = db.Products.FirstOrDefault(k => k.ProductName == pname);
                                                if (Product != null)
                                                {
                                                    ProductID = Product.Id;
                                                }
                                                else
                                                {
                                                    ProductID = 1;
                                                }
                                                var find = ctlist.FirstOrDefault(k => k.ProductId == ProductID && k.ChallanId == challanModel.Id);
                                                // var find = db.ChallanTranses.FirstOrDefault(k => k.ChallanId == challanModel.Id && k.ProductId == Product.Id);
                                                if (find == null)
                                                {
                                                    srNo = 0;
                                                    ctrans = new ChallanTransModel();
                                                    ctrans.ChallanId = challanModel.Id;

                                                    if (dr["LotNo"] != null)
                                                        ctrans.LotNo = dr["LotNo"].ToString();

                                                    ctrans.ProductId = Product.Id;
                                                    ctrans.UomId = 24;//mtr
                                                    ctrans.Rate = 0;// db.Prices.FirstOrDefault(k => k.ProductId == Product.Id).DealerPrice;
                                                    ctrans.Cgst = 0;
                                                    ctrans.CgstPer = 0;
                                                    ctrans.Sgst = 0;
                                                    ctrans.SgstPer = 0;
                                                    ctrans.Igst = 0;
                                                    ctrans.IgstPer = 0;

                                                    if (dr["PendPcs"] != null)
                                                    {
                                                        ctrans.Pcs = Convert.ToInt32(dr["PendPcs"].ToString());
                                                    }
                                                    if (dr["PendMtrs"] != null)
                                                        ctrans.Qty = Convert.ToDecimal(dr["PendMtrs"].ToString());

                                                    ctrans.CreateDate = DateTime.Now;
                                                    ctrans.CreateUser = KontoGlobals.UserName;

                                                    //db.ChallanTranses.Add(ctrans);
                                                    ctlist.Add(ctrans);
                                                }
                                                srNo = srNo + 1;
                                                Prod = new ProdModel();
                                                Prod.ProdStatus = "STOCK";
                                                Prod.RefId = challanModel.Id;
                                                Prod.TransId = ctrans.Id;
                                                Prod.LotNo = ctrans.LotNo;
                                                Prod.YearId = (int)KontoGlobals.YearId;
                                                Prod.CompId = KontoGlobals.CompanyId;
                                                Prod.BranchId = KontoGlobals.BranchId;
                                                Prod.ProductId = Product.Id;
                                                Prod.VoucherId = challanModel.VoucherId;
                                                Prod.VoucherDate = challanModel.VoucherDate;
                                                Prod.VoucherNo = srNo.ToString();
                                                Prod.SrNo = srNo;

                                                if (dr["graymeter"] != null)
                                                    Prod.NetWt = Convert.ToDecimal(dr["graymeter"].ToString());

                                                prodlist.Add(Prod);
                                                //db.Prods.Add(Prod);
                                                //db.SaveChanges();

                                            }
                                        }
                                        //db.SaveChanges();

                                    }
                                }
                            }

                            var config = new MapperConfiguration(cfg =>
                            {
                                cfg.CreateMap<ChallanModel, ChallanModel>().ForMember(x => x.Id, p => p.Ignore());
                            });
                            ChallanModel cm;
                            var map = new Mapper(config);
                            foreach (var cln in challanlist)
                            {
                                var ctranslist = ctlist.Where(k => k.ChallanId == cln.Id).ToList();
                                cln.TotalPcs = ctranslist.Sum(k => k.Pcs);
                                cln.TotalQty = ctranslist.Sum(k => k.Qty);

                                cm = new ChallanModel();
                                map = new Mapper(config);
                                map.Map(cln, cm);

                                db.Challans.Add(cm);
                                db.SaveChanges();

                                foreach (var ctitem in ctranslist)
                                {
                                    var prolist = prodlist.Where(k => k.RefId == cln.Id && k.TransId == ctitem.Id).ToList();
                                    ctitem.ChallanId = cm.Id;
                                    db.ChallanTranses.Add(ctitem);
                                    db.SaveChanges();

                                    foreach (var proditem in prolist)
                                    {
                                        proditem.RefId = cm.Id;
                                        proditem.TransId = ctitem.Id;
                                        db.Prods.Add(proditem);
                                        db.SaveChanges();
                                    }
                                }
                            }
                            db.SaveChanges();
                            tran.Commit();
                            splashScreenManager1.CloseWaitForm();
                            MessageBox.Show("Save Successfully!!!!");

                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
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
