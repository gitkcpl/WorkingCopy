using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Transaction;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaxProEWB.API;

namespace Konto.Shared.Trans.Common
{
    public partial class EwayBillView : KontoForm
    {
        EWBSession EwbSession = null;
        public int RefId { get; set; }
        public int VoucherId { get; set; }

        
        public BillModel BModel { get; set; }
        public List<BillTransModel> TModel { get; set; }

        public EwayBillView()
        {
            InitializeComponent();
            this.Load += EwayBillView_Load;
            this.Shown += EwayBillView_Shown;
            this.okSimpleButton.Click += OkSimpleButton_ClickAsync;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void OkSimpleButton_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateEwb()) return;

                var status = await GenerateEwbAsync();

                if (!status) return;

                using (var db = new KontoContext())
                {

                    var _ewb = db.Ewbs.SingleOrDefault(x => x.RefId == this.RefId && x.RefVoucherId == this.VoucherId);
                    if (_ewb == null)
                        _ewb = new Ewb();

                    _ewb.DespatchFromId = Convert.ToInt32(despFromLookupEdit.EditValue);
                    _ewb.Distance = Convert.ToInt32(distanceSpinEdit.Value);
                    _ewb.RefRowId = BModel.RowId;
                    _ewb.RefId = this.RefId;
                    _ewb.RefVoucherId = this.VoucherId;
                    _ewb.SubType = subTypeLookUpEdit.EditValue.ToString();
                    _ewb.TransactionType = transTypelookUpEdit.EditValue.ToString();
                    if (accLookup1.LookupDto != null)
                        _ewb.TransId = Convert.ToInt32(accLookup1.SelectedValue);
                    else
                        _ewb.TransId = null;

                    _ewb.VehicleNo = vehicleNoKontoTextEdit.Text.Trim();
                    _ewb.VehicleType = vehicleTypelookUpEdit.Text.Trim();
                    _ewb.ModeOfTrans = modeLookUpEdit.EditValue.ToString();
                    _ewb.DocDate = docDateEdit.DateTime.ToString("dd/MM/yyyy");
                    _ewb.DocNo = docNokontoTextEdit.Text.Trim();
                    _ewb.DocType = docTypeLookUpEdit.EditValue.ToString();
                    _ewb.IsActive = true;
                    _ewb.EwbNo = ewayBillkontoTextEdit.Text;

                    if (_ewb.Id == 0)
                        db.Ewbs.Add(_ewb);

                    db.SaveChanges();
                    okSimpleButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "eway Bill");
                MessageBox.Show(ex.ToString());
                
            }
            
        }

        private bool ValidateEwb()
        {
            if(modeLookUpEdit.Text=="Road" && string.IsNullOrEmpty(vehicleNoKontoTextEdit.Text))
            {
                if (Convert.ToInt32(accLookup1.SelectedValue)!=0)
                {
                    MessageBox.Show("Please Select Transport Name");
                    accLookup1.Focus();
                    return false;
                }
                if (accLookup1.LookupDto.GSTIN.Length != 15)
                {
                    MessageBox.Show("Please Select Transport Name");
                    accLookup1.Focus();
                    return false;
                }
            }
            if(modeLookUpEdit.Text=="Road" &&  Convert.ToInt32(accLookup1.SelectedValue)==0)
            {
                if (string.IsNullOrEmpty(vehicleNoKontoTextEdit.Text))
                {
                    MessageBox.Show("Please Enter Vehicle No.");
                    vehicleNoKontoTextEdit.Focus();
                    return false;
                }
            }

            if(modeLookUpEdit.Text!="Road" && string.IsNullOrEmpty(docNokontoTextEdit.Text))
            {
                MessageBox.Show("Invalid Document No");
                docNokontoTextEdit.Focus();
                return false;
            }
            

            return true;
        }
        private async Task<bool> GenerateEwbAsync()
        {
            ReqGenEwbPl ewbGen = new ReqGenEwbPl();

            var db = new KontoContext();
            var acc = DbUtils.AccDetails(BModel.AccId);
            var st =db.States.Find(acc.StateId);

            if(BModel.TypeId == (int) VoucherTypeEnum.SaleInvoice)
                ewbGen.supplyType = "O";

            ewbGen.subSupplyType = subTypeLookUpEdit.EditValue.ToString();
            ewbGen.subSupplyDesc = "";
            ewbGen.docType = docTypeLookUpEdit.EditValue.ToString(); ;
            ewbGen.docNo = BModel.VoucherNo;
            ewbGen.docDate =Convert.ToDateTime(BModel.CreateDate).ToString("dd/MM/yyyy");

            
            ewbGen.fromGstin = KontoUtils.Company.GstIn; // "32AACCC1596Q002";//"07AACCC1596Q1Z4";
            ewbGen.fromTrdName = KontoUtils.Company.CompName; //  "welton";
            ewbGen.fromAddr1 = KontoUtils.Company.Address1;
            ewbGen.fromAddr2 = KontoUtils.Company.Address2;

            var ct = db.Cities.Include("State").FirstOrDefault(x => x.Id == KontoUtils.Company.CityId);
            ewbGen.fromPlace = ct.CityName;

            ewbGen.fromPincode = Convert.ToInt32(KontoUtils.Company.Pincode);
            ewbGen.fromStateCode = Convert.ToInt32(ct.State.GstCode);


            // despatch from state code
            ewbGen.actFromStateCode = Convert.ToInt32(despFromLookupEdit.GetColumnValue("GstCode"));


            ewbGen.toGstin = acc.GSTIN;
            ewbGen.toTrdName = acc.AccName;
            ewbGen.toAddr1 = acc.Address1;
            ewbGen.toAddr2 = acc.Address2;
            ewbGen.toPlace = acc.CityName;
            ewbGen.toPincode = Convert.ToInt32(acc.PinCode);
            ewbGen.toStateCode = Convert.ToInt32(st.GstCode);

            //despatch to sate
            ewbGen.actToStateCode = Convert.ToInt32(st.GstCode); 


            ewbGen.transactionType = Convert.ToInt32(transTypelookUpEdit.EditValue);


            //ewbGen.dispatchFromGSTIN = ""; /*29AAAAA1303P1ZV*/
            //ewbGen.dispatchFromTradeName = "ABC Traders";

            //if ship to different from bill to
            if (transTypelookUpEdit.EditValue.ToString() != "1")
            {
                var shp = db.Accs.Find((int) BModel.DelvAccId);
                
                var shpAdr = db.AccAddresses.Include("City").Include("State").SingleOrDefault(x=>x.Id== BModel.DelvAdrId);

                
                ewbGen.shipToGSTIN = shp.GstIn;
                ewbGen.shipToTradeName = shp.AccName;
                ewbGen.toAddr1 = shpAdr.Address1;
                ewbGen.toAddr2 = shpAdr.Address2;
                ewbGen.toPlace = shpAdr.City.CityName;
                ewbGen.toPincode = Convert.ToInt32(shpAdr.PinCode);
                ewbGen.toStateCode = Convert.ToInt32(shpAdr.City.State.GstCode);

                
            }

            ewbGen.otherValue =Convert.ToDouble(BModel.TcsAmt);

            //taxable value

            ewbGen.totalValue = Convert.ToDouble(TModel.Sum(x => x.NetTotal) - TModel.Sum(x => x.Cgst) - TModel.Sum(x => x.Sgst) - TModel.Sum(x => x.Igst)
                - TModel.Sum(x => x.Cess));

            ewbGen.cgstValue = Convert.ToDouble(TModel.Sum(x=>x.Cgst));
            ewbGen.sgstValue = Convert.ToDouble(TModel.Sum(x => x.Sgst));
            ewbGen.igstValue = Convert.ToDouble(TModel.Sum(x => x.Igst));
            //ewbGen.cessValue = Convert.ToDouble(TModel.Sum(x => x.CessPer));


            ewbGen.cessNonAdvolValue = Convert.ToDouble(TModel.Sum(x => x.Cess));

            if(modeLookUpEdit.Text=="Road" && string.IsNullOrEmpty(vehicleNoKontoTextEdit.Text))
                ewbGen.transporterId = accLookup1.LookupDto.GSTIN;
            else
                ewbGen.transporterId = "";

            ewbGen.transporterName = accLookup1.SelectedText;
            
            if(modeLookUpEdit.EditValue.ToString()!="1")
                ewbGen.transDocNo = docNokontoTextEdit.Text;

            ewbGen.totInvValue =Convert.ToDouble(BModel.TotalAmount);
            ewbGen.transMode = modeLookUpEdit.EditValue.ToString();//1
            ewbGen.transDistance = "0"; /*1200*/
            ewbGen.transDocDate = docDateEdit.DateTime.ToString("dd/MM/yyyy");


            if(modeLookUpEdit.EditValue.ToString()=="Road" && (accLookup1.LookupDto==null ||  accLookup1.LookupDto.GSTIN.Length!=15))
                    ewbGen.vehicleNo = "PVC1234";//PVC1234
            
            ewbGen.vehicleType = vehicleTypelookUpEdit.EditValue.ToString();//R

            foreach (var item in TModel)
            {
                var pd = db.Products.Find(item.ProductId);
                var um = db.Uoms.Find(item.UomId);

                ewbGen.itemList.Add(new ReqGenEwbPl.ItemListInReqEWBpl
                {
                    productName = pd.ProductName,
                    productDesc = pd.ProductName,
                    hsnCode = Convert.ToInt32(pd.HsnCode),
                    quantity = Convert.ToDouble(item.Qty),
                    qtyUnit = um.UnitCode,
                    cgstRate = Convert.ToDouble(item.CgstPer),
                    sgstRate = Convert.ToDouble(item.SgstPer),
                    igstRate = Convert.ToDouble(item.IgstPer),
                    cessRate = 0,
                    cessNonAdvol = Convert.ToDouble(item.Cess),
                    taxableAmount = Convert.ToDouble(item.NetTotal-item.Cess-item.Cgst-item.Sgst-item.Igst)
                }
              );
            }

            TxnRespWithObjAndInfo<RespGenEwbPl> TxnResp = await EWBAPI.GenEWBAsync(EwbSession, ewbGen);

            if (TxnResp.IsSuccess)
            {
                ewayBillkontoTextEdit.Text = TxnResp.RespObj.ewayBillNo;
                errorkontoTextEdit.Text = "Generated Successfully";
                return true;
            }
            else
            {
                errorkontoTextEdit.Text = TxnResp.TxnOutcome +"-" + TxnResp.RespObj.alert; ;
            }
            return false;
        }

        private void EwayBillView_Shown(object sender, EventArgs e)
        {
            if  (BModel == null) return;

            var _ewb = new Ewb();
            using (var db = new KontoContext())
            {
                _ewb = db.Ewbs.SingleOrDefault(x => x.RefId == this.RefId && x.RefVoucherId == this.VoucherId);

            }
            
                
            if (_ewb!=null)
            {
                despFromLookupEdit.EditValue = _ewb.DespatchFromId;
                modeLookUpEdit.EditValue = _ewb.ModeOfTrans;
                subTypeLookUpEdit.EditValue = _ewb.SubType;
                docTypeLookUpEdit.EditValue = _ewb.DocType;
                if (_ewb.TransId != null)
                    accLookup1.SetAcc(Convert.ToInt32(_ewb.TransId));
                distanceSpinEdit.Value = _ewb.Distance;
                docDateEdit.EditValue = _ewb.DocDate;
                docNokontoTextEdit.Text = _ewb.DocNo;
                vehicleNoKontoTextEdit.Text = _ewb.VehicleNo;
                vehicleTypelookUpEdit.EditValue = _ewb.VehicleType;
                transTypelookUpEdit.EditValue = _ewb.TransactionType;
                ewayBillkontoTextEdit.Text = _ewb.EwbNo;

                okSimpleButton.Enabled = false;
                return;
            }

                if (Convert.ToInt32(BModel.TransId) > 0)
                {
                    accLookup1.SetAcc(Convert.ToInt32(BModel.TransId));
                    accLookup1.SelectedValue = BModel.TransId;
                }
            docNokontoTextEdit.Text = BModel.DocNo;
            docDateEdit.EditValue = BModel.DocDate;
            
            if(BModel.TypeId == (int)VoucherTypeEnum.SaleInvoice)
            {
                subTypeLookUpEdit.EditValue = "1";
                docTypeLookUpEdit.EditValue = "INV";
                modeLookUpEdit.EditValue = "1";
                vehicleTypelookUpEdit.EditValue = "R";
            }
            despFromLookupEdit.EditValue = KontoUtils.Company.StateId;
            ewayBillkontoTextEdit.Text = BModel.EwayBillNo;
        }

        private void EwayBillView_Load(object sender, EventArgs e)
        {
            EwbSession = new EWBSession();
            
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            
            EwbSession.RefreshAuthTokenCompleted += RefreshLoginDetailsDisplay;

            GetLoginDetails();

            FillLookup();
        }
        private void GetLoginDetails()
        {

            EwbSession.EwbApiSetting.AspUserId = SysParameter.AspUserId;
            EwbSession.EwbApiSetting.AspPassword = SysParameter.AspUserPass;
            EwbSession.EwbApiSetting.GSPName = SysParameter.AspGspName;
            EwbSession.EwbApiSetting.BaseUrl = SysParameter.AspApiBaseUrl + "/v1.03";


            using (var db = new KontoContext())
            {
                var _TaxPayer = db.Companies.Find(KontoGlobals.CompanyId);
                
                EwbSession.EwbApiLoginDetails = new EWBAPILoginDetails();
                EwbSession.EwbApiLoginDetails.EwbGstin = _TaxPayer.GstIn;
                EwbSession.EwbApiLoginDetails.EwbUserID = _TaxPayer.EwayBillUserId;
                EwbSession.EwbApiLoginDetails.EwbPassword = _TaxPayer.EwayBillPassword;
                EwbSession.EwbApiLoginDetails.EwbAppKey = _TaxPayer.AppKey;
                EwbSession.EwbApiLoginDetails.EwbSEK = _TaxPayer.SEK;
                EwbSession.EwbApiLoginDetails.EwbTokenExp = _TaxPayer.TokenExp;
                EwbSession.EwbApiLoginDetails.EwbAuthToken = _TaxPayer.AuthToken;
            }


        }
        private void RefreshLoginDetailsDisplay(object sender, EventArgs e)
        {
            // save refreshed authentication in db for selected company
            using(var db = new KontoContext())
            {
                var _cmp = db.Companies.Find(KontoGlobals.CompanyId);
                _cmp.AppKey = EwbSession.EwbApiLoginDetails.EwbAppKey;
                _cmp.SEK =  EwbSession.EwbApiLoginDetails.EwbSEK;
                _cmp.AuthToken = EwbSession.EwbApiLoginDetails.EwbAuthToken;
                _cmp.TokenExp = EwbSession.EwbApiLoginDetails.EwbTokenExp;
                db.SaveChanges();
            }
            
        }

        private void FillLookup()
        {
            using (var db = new KontoContext())
            {
                var cnt = db.States.Where(x => !x.IsDeleted && x.IsActive).OrderBy(x => x.StateName).ToList();
                despFromLookupEdit.Properties.DataSource = cnt;
            }

            List<ComboBoxPairs> _mode = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Road", "1"),
                new ComboBoxPairs("Rail", "2"),
                new ComboBoxPairs("Air", "3"),
                new ComboBoxPairs("Ship", "4"),
            };
            modeLookUpEdit.Properties.DataSource = _mode;

            List<ComboBoxPairs> _sub = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Supply", "1"),
                new ComboBoxPairs("Import", "2"),
                new ComboBoxPairs("Export", "3"),
                new ComboBoxPairs("Job Work", "4"),
                new ComboBoxPairs("For OWn User", "5"),
                new ComboBoxPairs("Job Work Returns", "6"),
                new ComboBoxPairs("Sale Return", "7"),
                new ComboBoxPairs("Others", "8"),
                new ComboBoxPairs("SKD/CKD/Lots", "9"),
                new ComboBoxPairs("Line Sales", "10"),
                new ComboBoxPairs("Recepient Not Known", "11"),
                new ComboBoxPairs("Exibitions or Fairs", "12"),
            };
            subTypeLookUpEdit.Properties.DataSource = _sub;

            List<ComboBoxPairs> _docs = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Tax Invoice", "INV"),
                new ComboBoxPairs("Bill of Supplye", "BIL"),
                new ComboBoxPairs("Bill of Entry", "BOE"),
                new ComboBoxPairs("Delivery Challan", "CHL"),
                new ComboBoxPairs("Others", "OTH"),
            };

            docTypeLookUpEdit.Properties.DataSource = _docs;

            List<ComboBoxPairs> _vts = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Regular", "R"),
                new ComboBoxPairs("ODC", "O"),
            };

            vehicleTypelookUpEdit.Properties.DataSource = _vts;

            List<ComboBoxPairs> _transtype = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Regular", "1"),
                new ComboBoxPairs("Bill To-Ship To", "2"),
                new ComboBoxPairs("Bill From-Despatch From", "3"),
                new ComboBoxPairs("Combination of 2 & 3", "4"),
            };

            transTypelookUpEdit.Properties.DataSource = _transtype;
        }

        
    }
}
