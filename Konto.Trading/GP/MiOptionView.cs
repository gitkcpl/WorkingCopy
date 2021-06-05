using DevExpress.ReportServer.ServiceModel.DataContracts;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Konto.Trading.GP
{
    public partial class MiOptionView : KontoForm
    {
        private bool flag = false;
        public ChallanTransModel TranModel { get; set; }
        public ChallanModel Model { get; set; }
        public List<ProdModel> MIProdList { get; set; }
        private List<MiDto> MillIssueList;
        private List<ProdOutModel> NewProdOutList;
        private List<ProdModel> ProdList;
        public List<ChallanModel> IssueList { get; set; }
        private KontoContext db = new KontoContext();
        public  bool IsOpenIssued { get; set; }
        public MiOptionView()
        {
            InitializeComponent();
            MillIssueList = new List<MiDto>();
            NewProdOutList = new List<ProdOutModel>();
            ProdList = new List<ProdModel>();
            IssueList = new List<ChallanModel>();
            this.Load += MiOptionView_Load;
        }

        private void MiOptionView_Load(object sender, EventArgs e)
        {
            if (TranModel != null)
            {
                textEdit1.Value = TranModel.Pcs;
            }
        }

        private void okSimpleButton_Click(object sender, System.EventArgs e)
        {



            SingleChallan();
            if (SaveDataMillIssueAsync())
            {

                if (textEdit1.Value == TranModel.Pcs)
                {
                    if (MessageBox.Show("Open Issue ?", "Issue", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var frm = new MillIssue.MillIssueIndex();
                        frm.OpenForLookup = true;
                        frm.EditKey = this.IssueList[0].Id;
                        IsOpenIssued = true;
                        frm.ShowDialog();
                    }
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();

            }
        }

        private void SingleChallan()
        {
            flag = false;
            var tmp = MIProdList;
            ProdList = MIProdList;
            if (radioGroup1.SelectedIndex == 0)
                tmp = tmp.OrderBy(k => k.NetWt).ToList();
            else if (radioGroup1.SelectedIndex == 1)
                tmp = tmp.OrderByDescending(k => k.NetWt).ToList();

            var lot = Convert.ToInt32(textEdit1.Value);
            if (lot == 0)
            {
                MessageBox.Show("Please Enter Taka Per Lot");
                textEdit1.Focus();
                return;
            }
            if (Convert.ToInt32(accLookup1.SelectedValue) == 0)
            {
                MessageBox.Show("Invalid Mill Party");
                accLookup1.Focus();
                return;
            }
            if (checkEdit1.Checked && Convert.ToInt32(textEdit1.Value) > 0)
            {
                int totalTaka = MIProdList.Count;
                int numberOfOutputParts = MIProdList.Count / Convert.ToInt32(textEdit1.Value);
                //int numberOfOutputParts = Model.Meter;
                int num = MIProdList.Count % Convert.ToInt32(textEdit1.Value);
                if (num != 0)
                    numberOfOutputParts++;

                List<Part> listOfParts = new List<Part>();

                for (int i = 0; i < numberOfOutputParts; i++)
                {
                    listOfParts.Add(new Part());
                }
                foreach (var item in tmp)
                {
                    decimal? lowestSumFoundSoFar = null;
                    Part lowestValuePartSoFar = null;

                    foreach (Part partToCheck in listOfParts)
                    {
                        if (lowestSumFoundSoFar == null || partToCheck.CurrentSum < lowestSumFoundSoFar)
                        {
                            lowestSumFoundSoFar = partToCheck.CurrentSum;
                            lowestValuePartSoFar = partToCheck;
                        }
                    }

                    // add the value to that Part
                    lowestValuePartSoFar.AddValue(item, item.NetWt);

                }

                if (listOfParts.Count > 0)
                {
                    int id = 0;
                    int vId = 0;
                    MiDto millissue;
                    ProdOutModel pout;

                    var vouchr = db.Vouchers.FirstOrDefault(k => k.VTypeId == (int)VoucherTypeEnum.MillIssue);
                    if (vouchr != null)
                        vId = vouchr.Id;
                    var card = db.Challans.FirstOrDefault(k => k.VoucherId == vId
                                && k.ChallanType == (int)ChallanTypeEnum.MILL_ISSUE);

                    foreach (var listPart in listOfParts)
                    {
                        millissue = new MiDto();

                        millissue.VoucherId = vId;
                        millissue.TotalPc = listPart.Values.Count;
                        millissue.TotalQty = listPart.CurrentSum;
                        millissue.ChallanDate = DateTime.Now;
                        millissue.VoucherNo = DbUtils.NextSerialNo((int)millissue.VoucherId, db);
                        //millissue.CardNo=card.ChallanNo;
                        id = id - 1;
                        millissue.TypeId = (int)TypeEnum.MillIssue;
                        millissue.ChallanType = (int)ChallanTypeEnum.MILL_ISSUE;
                        millissue.Id = id;
                        millissue.AccID = Convert.ToInt32(accLookup1.SelectedValue);

                        // this.grayIssueDataSet.grayissue.AcceptChanges();

                        MillIssueList.Add(millissue);

                        foreach (ProdModel ldr in listPart.Values)
                        {
                            pout = new ProdOutModel();
                            pout.ProdId = ldr.Id;
                            pout.RefId = millissue.Id;
                            NewProdOutList.Add(pout);
                        }
                    }
                }
            }
            else if (lot > 0)
            {
                int numberOfOutputParts = MIProdList.Count / lot;
                List<Part> listOfParts = new List<Part>();
                int num = MIProdList.Count % lot;
                if (num != 0)
                    numberOfOutputParts++;
                for (int i = 0; i < numberOfOutputParts; i++)
                {
                    listOfParts.Add(new Part());
                }
                if (listOfParts.Count > 0)
                {
                    int id = 0;
                    int vId = 0;
                    MiDto millissue;
                    ProdOutModel pout;

                    var vouchr = db.Vouchers.FirstOrDefault(k => k.VTypeId == (int)VoucherTypeEnum.MillIssue);
                    if (vouchr != null)
                        vId = vouchr.Id;
                    //var card = _db.Challans.LastOrDefault(k => k.VoucherId == vId);

                    foreach (var listPart in listOfParts)
                    {
                        millissue = new MiDto();

                        millissue.VoucherId = vId;
                        millissue.ChallanDate = DateTime.Now;
                        millissue.VoucherNo = DbUtils.NextSerialNo((int)millissue.VoucherId, db);
                        //  millissue.CardNo=card.ChallanNo;
                        id = id - 1;
                        millissue.TypeId = (int)TypeEnum.MillIssue;
                        millissue.ChallanType = (int)ChallanTypeEnum.MILL_ISSUE;
                        millissue.Id = id;
                        millissue.AccID = Convert.ToInt32(accLookup1.SelectedValue);

                        // this.grayIssueDataSet.grayissue.AcceptChanges();
                        int pcs = 0;
                        decimal qty = 0;
                        foreach (var ldr in tmp)
                        {
                            if (lot > pcs)
                            {
                                pout = new ProdOutModel();
                                pout.ProdId = ldr.Id;
                                pout.RefId = millissue.Id;

                                NewProdOutList.Add(pout);

                                pcs = pcs + 1;
                                qty = qty + (decimal)ldr.NetWt;
                            }
                        }
                        for (int i = (pcs - 1); i >= 0; i--)
                        {
                            tmp.RemoveAt(i);
                        }

                        millissue.TotalPc = pcs;
                        millissue.TotalQty = qty;

                        MillIssueList.Add(millissue);
                    }
                }
            }

        }
        private bool SaveDataMillIssueAsync()
        {
           
            if (MillIssueList.Count > 0)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        ////////Mill Issue
                        ChallanModel challan;
                        ChallanTransModel trans;
                        ProdModel prod;
                        //  ProdOutModel POut;
                        bool IsSave = false;
                        int MIid = 0;
                        foreach (var item in MillIssueList)
                        {
                            challan = new ChallanModel();
                            challan.AccId = (int)item.AccID;
                            challan.CompId = KontoGlobals.CompanyId;
                            challan.EmpId = KontoGlobals.EmpId;
                            challan.BranchId = KontoGlobals.BranchId;
                            //challan.TotalAmount=item.g
                            challan.TotalPcs = item.TotalPc;
                            challan.TotalQty = item.TotalQty;
                            challan.VDate = DateTime.Now;
                            challan.VoucherDate =Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
                            challan.YearId = KontoGlobals.YearId;
                            challan.VoucherId = (int)item.VoucherId;
                            challan.VoucherNo = item.VoucherNo;
                            challan.ChallanNo = item.VoucherNo;
                            challan.TypeId = (int)TypeEnum.MillIssue;
                            challan.ChallanType = (int)item.ChallanType;
                            challan.DivId = Model.DivId;
                            challan.DocDate = DateTime.Now;
                            challan.StoreId = Model.StoreId;
                            var oid = db.AccBals.FirstOrDefault(k => k.AccId == item.AccID && k.CompId == KontoGlobals.CompanyId && k.YearId == KontoGlobals.YearId);
                            challan.DelvAccId = item.AccID;  // set party to delv party
                            challan.AddrId = oid.AddressId;
                            challan.DelvAdrId = oid.AddressId;

                            db.Challans.Add(challan);
                            db.SaveChanges();

                            trans = new ChallanTransModel();
                            trans.ProductId = TranModel.ProductId;
                            trans.ChallanId = challan.Id;
                            trans.Qty = Convert.ToDecimal(item.TotalQty);
                            trans.Pcs = Convert.ToInt32(item.TotalPc);
                            trans.NProductId = 0;
                            trans.BatchId = TranModel.BatchId;
                            trans.Cess = TranModel.Cess;
                            trans.CessPer = TranModel.CessPer;
                            trans.Disc = TranModel.Disc;
                            trans.DiscPer = TranModel.DiscPer;
                            trans.Freight = TranModel.Freight;
                            trans.FreightRate = TranModel.FreightRate;
                            trans.Gross = TranModel.Gross;

                            trans.IssuePcs = Convert.ToInt32(item.TotalPc);
                            trans.IssueQty = Convert.ToDecimal(item.TotalQty);
                            trans.RefNo = TranModel.RefNo;
                            trans.LotNo = TranModel.RefNo;
                            trans.OtherAdd = TranModel.OtherAdd;
                            trans.OtherLess = TranModel.OtherLess;
                            trans.Rate = TranModel.Rate;
                            trans.Total = TranModel.Total;
                            trans.UomId = TranModel.UomId;
                            trans.Weight = TranModel.Weight;
                            trans.RefId = TranModel.Id;
                            trans.RefVoucherId = Model.VoucherId;
                            trans.MiscId = Model.Id;

                            if (accLookup1.LookupDto.IsIgst)
                            {
                                if (TranModel.Igst <= 0)
                                {
                                    trans.Igst = TranModel.Cgst + TranModel.Sgst;
                                    trans.IgstPer = TranModel.CgstPer + TranModel.SgstPer;
                                }
                                else
                                {
                                    trans.Igst = TranModel.Igst;
                                    trans.IgstPer = TranModel.IgstPer;
                                }
                            }
                            else
                            {
                                trans.Cgst = TranModel.Cgst;
                                trans.CgstPer = TranModel.CgstPer;
                                trans.Sgst = TranModel.Sgst;
                                trans.SgstPer = TranModel.SgstPer;
                            }

                            db.ChallanTranses.Add(trans);
                            db.SaveChanges();
                            bool IsIssue = true;
                            string TableName = "Milll Issue";
                            MIid = MIid - 1;
                            var prodlist = NewProdOutList.Where(k => (k.RefId == MIid || k.RefId == challan.Id)).ToList();
                            trans.Pcs = prodlist.Count();
                            //trans.Qty = Convert.ToDecimal( prodlist.Sum(x => x.n));
                            if (prodlist.Count > 0)
                            {
                                foreach (var ptrans in prodlist)
                                {
                                    prod = new ProdModel();
                                    prod = db.Prods.Find(ptrans.ProdId);

                                    ptrans.RefId = challan.Id;
                                    ptrans.TransId = trans.Id;
                                    ptrans.VoucherId = challan.VoucherId;

                                    ptrans.ProductId = prod.ProductId;
                                    ptrans.ColorId = prod.ColorId;
                                    ptrans.GradeId = prod.GradeId;

                                    ptrans.CompId = challan.CompId;
                                    ptrans.YearId = challan.YearId;
                                    ptrans.SrNo = prod.SrNo;
                                    ptrans.Qty = -1 * prod.NetWt;
                                    ptrans.GrayMtr = -1 * prod.NetWt;
                                    ptrans.VoucherNo = prod.VoucherNo;
                                    db.ProdOuts.Add(ptrans);
                                    prod.ProdStatus = "ISSUE";
                                    prod.ModifyDate = DateTime.Now;
                                    prod.ModifyUser = KontoGlobals.UserName;
                                    db.SaveChanges();
                                    //if (StockEffect.StockTransChlnProdEntry(challan, trans
                                    //          , IsIssue, TableName, KontoGlobals.UserName, db, prod, false))
                                    //    IsSave = true;
                                }
                            }
                            if (StockEffect.StockTransChlnEntry(challan, trans
                                              , IsIssue, TableName, KontoGlobals.UserName, db))

                                IsSave = true;

                            IssueList.Add(challan);
                        }

                        if (IsSave)
                        {
                            db.SaveChanges();
                            transaction.Commit();
                            System.Windows.MessageBox.Show(KontoGlobals.SaveMessage, "Confirmation !!", System.Windows.MessageBoxButton.OK);
                            return true;
                        }
                        else
                            return false;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       MIProdList = ProdList.ToList();
                        Log.Error(ex, "Mill Issue  Save Under Transaction");
                        flag = true;
                        return false;
                    }
                }
            }
            else
            {
                return true;
            }
        }

    }
    public class Part
    {
        public List<object> Values
        {
            get;
            set;
        }

        public decimal CurrentSum
        {
            get;
            set;
        }

        /// <summary>
        /// Default Constructpr
        /// </summary>
        public Part()
        {
            Values = new List<object>();
        }

        public void AddValue(object dr, object netwt)
        {
            Values.Add(dr);

            CurrentSum += Convert.ToDecimal(netwt);
        }
    }
}
