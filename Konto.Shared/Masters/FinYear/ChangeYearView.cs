using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Reports;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Shared.Masters.FinYear
{
    public partial class ChangeYearView : KontoForm
    {
        public ChangeYearView()
        {
            InitializeComponent();
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.Load += ChangeYearView_Load;
            this.FormClosed += ChangeYearView_FormClosed;
        }

        private void ChangeYearView_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage != null)
            {
                var tabcontrol = tabpage.Parent as TabControlAdv;
                if (tabcontrol != null)
                    tabcontrol.TabPages.Remove(tabpage);
            }
        }

        private void ChangeYearView_Load(object sender, EventArgs e)
        {
            fromDateEdit.EditValue = KontoGlobals.DToDate.AddDays(1);
            toDateEdit.EditValue = KontoGlobals.DToDate.AddYears(1);

        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {

            using (var db = new KontoContext())
            {
                using (var _trn = db.Database.BeginTransaction())
                {

                    try
                    {

                        if(db.FinYears.Any(x=> x.PrevYearId == KontoGlobals.YearId))
                        {
                            MessageBox.Show("Financial Year Already Exists");
                            _trn.Rollback();
                            return;
                        }

                        var fy = new FinYearModel();

                        fy.FromDate = Convert.ToInt32(fromDateEdit.DateTime.ToString("yyyyMMdd"));
                        fy.ToDate = Convert.ToInt32(toDateEdit.DateTime.ToString("yyyyMMdd"));

                        fy.FDate = fromDateEdit.DateTime;
                        fy.TDate = toDateEdit.DateTime;

                        fy.YearCode = fromDateEdit.DateTime.Year.ToString() + "-" + toDateEdit.DateTime.ToString("yy");

                        fy.IsActive = true;
                        fy.IsDeleted = false;
                        fy.PrevYearId = KontoGlobals.YearId;

                        db.FinYears.Add(fy);
                        db.SaveChanges();

                        // transfer account in new year

                        //gettting balance from balsheet

                        var cmps = db.Companies.Where(x=>!x.IsDeleted && x.IsActive).ToList();

                        List<AccBalModel> bals = new List<AccBalModel>();
                        List<StockBalModel> sbs = new List<StockBalModel>();

                        foreach (var cmp in cmps)
                        {



                            var Lst = db.Database.SqlQuery<BalDto>(
                                    "dbo.Bal_sheet @CompanyId={0},@FromDate={1},@ToDate={2},@YearId={3},@Summary={4},@zero={5}",
                                    cmp.Id, KontoGlobals.FromDate, KontoGlobals.ToDate,
                                    KontoGlobals.YearId, "N", 1).ToList().Where(X => X.TransType == 3);


                            var acbals = db.AccBals.Where(k => k.CompId == cmp.Id && k.YearId == KontoGlobals.YearId).ToList();

                         

                            foreach (var ab in acbals)
                            {

                                var acbalModel = new AccBalModel();


                                var address1 = "NA";
                                var address2 = "NA";
                                var cityid = 1;


                                address1 = ab.Address1;
                                address2 = ab.Address2;
                                if (ab.CityId != null)
                                    cityid = (int)ab.CityId;


                                acbalModel.CompId = cmp.Id;
                                acbalModel.AccId = ab.AccId;
                                acbalModel.AccRowId = ab.AccRowId;
                                acbalModel.GroupId = ab.GroupId;

                                acbalModel.Address1 = address1;
                                acbalModel.Address2 = address2;
                                acbalModel.CityId = cityid;
                                acbalModel.AreaId = ab.AreaId;
                                acbalModel.PinCode = ab.PinCode;
                                acbalModel.RouteId = ab.RouteId;
                                acbalModel.AddressId = ab.AddressId;
                                acbalModel.Phone = ab.Phone;
                                acbalModel.Others = ab.Others;
                                acbalModel.Share = ab.Share;
                                acbalModel.Website = ab.Website;
                                acbalModel.Email = ab.Email;
                                acbalModel.ContactPerson = ab.ContactPerson;


                                var cl = Lst.FirstOrDefault(x => x.AcId == ab.AccId);

                                if (cl != null)
                                {
                                    if (cl.Bal > 0)
                                        acbalModel.OpDebit = cl.Bal;
                                    else
                                        acbalModel.OpCredit = -1 * cl.Bal;

                                    acbalModel.OpBal = cl.Bal;
                                }
                                else
                                {
                                    acbalModel.Bal = 0;
                                }
                                acbalModel.YearId = fy.Id;

                                bals.Add(acbalModel);



                            }

                            // transfer stock to next year
                            var stockBals = db.StockBals.Where(k => k.CompanyId == cmp.Id && k.YearId == KontoGlobals.YearId).ToList();

                            foreach (var prod in stockBals)
                            {
                               var SBModel = new StockBalModel();

                                SBModel.CompanyId = cmp.Id;
                                SBModel.ProductId = prod.ProductId;
                                SBModel.ItemCode = prod.ItemCode;
                                SBModel.BranchId = prod.BranchId;
                                SBModel.GodownId = prod.GodownId;
                                
                                SBModel.OpNos = prod.BalNos;
                                SBModel.OpQty = prod.BalQty;
                                SBModel.Rate = prod.Rate;
                                

                                SBModel.StockValue = 0;
                                SBModel.CreateDate = DateTime.Now;
                                SBModel.CreateUser = KontoGlobals.UserName;
                                SBModel.YearId = fy.Id;

                                SBModel.RcptQty = 0;
                                SBModel.RcptNos = 0;
                                SBModel.IssueQty = 0;
                                SBModel.IssueNo = 0;

                                sbs.Add(SBModel);

                            }



                        }

                        db.AccBals.AddRange(bals);

                        db.StockBals.AddRange(sbs);

                        db.SaveChanges();
                        _trn.Commit();

                        MessageBox.Show("Year Created Successfully");

                    }
                    catch (Exception ex)
                    {
                        _trn.Rollback();
                        MessageBox.Show(ex.ToString());
                        
                    }
                }
            }
            
        }

        
    }
}
