using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Transaction;
using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaxProEInvoice.API;

namespace Konto.Shared.Trans.Common
{
    public partial class EinvView : KontoForm
    {
        eInvoiceSession eInvSession = null;
        public BillModel BModel { get; set; }
        public List<BillTransModel> TModel { get; set; }
        public int RefId { get; set; }
        public EInv EinvoiceModel { get; set; }
        public int VoucherId { get; set; }
        public EinvView()
        {
            InitializeComponent();
            this.Load += EinvView_Load;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.Shown += EinvView_Shown;
        }

        private void EinvView_Shown(object sender, EventArgs e)
        {
            if (BModel == null) return;

            
            using (var db = new KontoContext())
            {
                var _einv = db.EInvs.SingleOrDefault(x => x.RefId == this.RefId && x.RefVoucherId == this.VoucherId);
                if (_einv != null)
                {
                    transCatLookUpEdit.EditValue = _einv.TransType;
                    eInvBillkontoTextEdit.Text = _einv.Irn;
                    pictureEdit1.EditValue = _einv.QrCodeImage;
                    okSimpleButton.Enabled = false;
                }
            }


        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void OkSimpleButton_Click(object sender, EventArgs e)
        {
            await GenerateEInvoiceAsync();
        }

        private void EinvView_Load(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            eInvSession = new eInvoiceSession();
            eInvSession.RefreshAuthTokenCompleted += EInvSession_RefreshAuthTokenCompleted;

            GetLoginDetails();
            FillLookup();
            transCatLookUpEdit.EditValue = "B2B";
        }
        private void FillLookup()
        {
            List<ComboBoxPairs> _transcats = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("B2B", "B2B"),
                new ComboBoxPairs("EXPWP", "EXPWP"),
                new ComboBoxPairs("EXPWOP", "EXPWOP"),
                new ComboBoxPairs("SEZWP", "SEZWP"),
                new ComboBoxPairs("SEZWOP", "SEZWOP"),
                new ComboBoxPairs("DEXP", "DEXP"),
            };
            transCatLookUpEdit.Properties.DataSource = _transcats;

        }
        private void EInvSession_RefreshAuthTokenCompleted(object sender, EventArgs e)
        {
            // save refreshed authentication in db for selected company
            using (var db = new KontoContext())
            {
                var _cmp = db.Companies.Find(KontoGlobals.CompanyId);
                _cmp.AppKey = eInvSession.eInvApiLoginDetails.AppKey;
                _cmp.SEK = eInvSession.eInvApiLoginDetails.Sek;
                _cmp.AuthToken = eInvSession.eInvApiLoginDetails.AuthToken;
                _cmp.TokenExp = eInvSession.eInvApiLoginDetails.E_InvoiceTokenExp;
                db.SaveChanges();
            }
        }
        private void GetLoginDetails()
        {

            eInvSession.eInvApiSetting.AspUserId = SysParameter.AspUserId;
            eInvSession.eInvApiSetting.AspPassword = SysParameter.AspUserPass;
            eInvSession.eInvApiSetting.GSPName = SysParameter.AspGspName;
            eInvSession.eInvApiSetting.BaseUrl = SysParameter.AspApiBaseUrl + "/eicore/v1.03";
            eInvSession.eInvApiSetting.AuthUrl = SysParameter.AspApiBaseUrl + "/eivital/v1.03";
            eInvSession.eInvApiSetting.EwbByIRN = SysParameter.AspApiBaseUrl + "/eiewb/v1.03";


            using (var db = new KontoContext())
            {
                var _TaxPayer = db.Companies.Find(KontoGlobals.CompanyId);

                eInvSession.eInvApiLoginDetails = new eInvoiceAPILoginDetails();
                eInvSession.eInvApiLoginDetails.GSTIN = _TaxPayer.GstIn;
                eInvSession.eInvApiLoginDetails.UserName = _TaxPayer.EwayBillUserId;
                eInvSession.eInvApiLoginDetails.Password = _TaxPayer.EwayBillPassword;
                eInvSession.eInvApiLoginDetails.AppKey = _TaxPayer.AppKey;
                eInvSession.eInvApiLoginDetails.Sek = _TaxPayer.SEK;
                eInvSession.eInvApiLoginDetails.E_InvoiceTokenExp = _TaxPayer.TokenExp;
                eInvSession.eInvApiLoginDetails.AuthToken = _TaxPayer.AuthToken;
            }


        }

        private async Task<bool> GenerateEInvoiceAsync()
        {
            ReqPlGenIRN reqPlGenIRN = new ReqPlGenIRN();

            reqPlGenIRN.Version = "1.1";
            
            reqPlGenIRN.TranDtls = new ReqPlGenIRN.TranDetails();
            reqPlGenIRN.TranDtls.TaxSch = "GST";
            reqPlGenIRN.TranDtls.SupTyp = transCatLookUpEdit.EditValue.ToString();

            
            reqPlGenIRN.DocDtls = new ReqPlGenIRN.DocSetails();
            
            if (BModel.TypeId == (int)VoucherTypeEnum.SaleInvoice)
                reqPlGenIRN.DocDtls.Typ = "INV";
            else if (BModel.TypeId == (int)VoucherTypeEnum.DebitCreditNote && BModel.BillType=="DEBIT NOTE")
                reqPlGenIRN.DocDtls.Typ = "DBN";
            else
                reqPlGenIRN.DocDtls.Typ = "CRN";

            reqPlGenIRN.DocDtls.No = BModel.VoucherNo;
            
            DateTime time = DateTime.ParseExact(BModel.VoucherDate.ToString(), "yyyyMMdd",
              CultureInfo.InvariantCulture);

            reqPlGenIRN.DocDtls.Dt = time.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            reqPlGenIRN.SellerDtls = new ReqPlGenIRN.SellerDetails();
            reqPlGenIRN.SellerDtls.Gstin = KontoUtils.Company.GstIn;
            reqPlGenIRN.SellerDtls.LglNm = KontoUtils.Company.CompName;
            reqPlGenIRN.SellerDtls.TrdNm = null;
            reqPlGenIRN.SellerDtls.Addr1 = KontoUtils.Company.Address1;
            reqPlGenIRN.SellerDtls.Addr2 = KontoUtils.Company.Address2;
            
            var db = new KontoContext();
            var cct = db.Cities.Include("State").SingleOrDefault(x => x.Id == KontoUtils.Company.CityId);
            reqPlGenIRN.SellerDtls.Loc =cct.CityName;
            reqPlGenIRN.SellerDtls.Pin = Convert.ToInt32(KontoUtils.Company.Pincode);
            reqPlGenIRN.SellerDtls.Stcd = cct.State.GstCode;
            reqPlGenIRN.SellerDtls.Ph = null;
            reqPlGenIRN.SellerDtls.Em = null;

            var ac = DbUtils.AccDetails(BModel.AccId);
            reqPlGenIRN.BuyerDtls = new ReqPlGenIRN.BuyerDetails();
            reqPlGenIRN.BuyerDtls.Gstin = ac.GSTIN;
            reqPlGenIRN.BuyerDtls.LglNm = ac.AccName;
            reqPlGenIRN.BuyerDtls.TrdNm = null;
            reqPlGenIRN.BuyerDtls.Pos = ac.GSTCode;
            reqPlGenIRN.BuyerDtls.Addr1 = ac.Address1;
            reqPlGenIRN.BuyerDtls.Addr2 = ac.Address2;
            reqPlGenIRN.BuyerDtls.Loc = ac.CityName;
            reqPlGenIRN.BuyerDtls.Pin = Convert.ToInt32(ac.PinCode);
            reqPlGenIRN.BuyerDtls.Stcd = ac.GSTCode;
            reqPlGenIRN.BuyerDtls.Ph = null;
            reqPlGenIRN.BuyerDtls.Em = null;

            reqPlGenIRN.DispDtls = new ReqPlGenIRN.DispatchedDetails();
            reqPlGenIRN.DispDtls.Nm = KontoUtils.Company.CompName;
            reqPlGenIRN.DispDtls.Addr1 = KontoUtils.Company.Address1;
            reqPlGenIRN.DispDtls.Addr2 = KontoUtils.Company.Address2;
            reqPlGenIRN.DispDtls.Loc = cct.CityName;
            reqPlGenIRN.DispDtls.Pin = Convert.ToInt32(KontoUtils.Company.Pincode);
            reqPlGenIRN.DispDtls.Stcd = cct.State.GstCode;

            var shp = db.Accs.Find((int)BModel.DelvAccId);

            var shpAdr = db.AccAddresses.Include("City")
                .Include("City.State").SingleOrDefault(x => x.Id == BModel.DelvAdrId);

            reqPlGenIRN.ShipDtls = new ReqPlGenIRN.ShippedDetails();
            reqPlGenIRN.ShipDtls.Gstin = shp.GstIn;
            reqPlGenIRN.ShipDtls.LglNm = shp.AccName;
            reqPlGenIRN.ShipDtls.TrdNm = null;
            reqPlGenIRN.ShipDtls.Addr1 = shpAdr.Address1;
            reqPlGenIRN.ShipDtls.Addr2 = shpAdr.Address2;
            reqPlGenIRN.ShipDtls.Loc = shpAdr.City.CityName;
            reqPlGenIRN.ShipDtls.Pin = Convert.ToInt32(shpAdr.PinCode);
            reqPlGenIRN.ShipDtls.Stcd = shpAdr.City.State.GstCode;

            reqPlGenIRN.ItemList = new List<ReqPlGenIRN.ItmList>();
            var srno = 1;
            foreach (var item in TModel)
            {
                ReqPlGenIRN.ItmList itm = new ReqPlGenIRN.ItmList();

                var it = db.Products.Find(item.ProductId);
                var ut = db.Uoms.Find(item.UomId);

                itm.SlNo = srno.ToString();
                itm.IsServc = "N";
                if (it != null)
                {
                    itm.PrdDesc = it.ProductName;
                    itm.HsnCd = it.HsnCode;
                }

                itm.BchDtls = null;
                itm.Qty = Convert.ToDouble(item.Qty);
                if (ut != null) itm.Unit = ut.UnitCode;
                itm.UnitPrice = Convert.ToDouble(item.Rate);
                itm.TotAmt = Convert.ToDouble(item.Total) + Convert.ToDouble(item.OtherAdd) + Convert.ToDouble(item.Freight);
                itm.Discount = Convert.ToDouble(item.DiscAmt) + Convert.ToDouble(item.OtherLess);
                itm.AssAmt = Convert.ToDouble(item.NetTotal - item.Igst - item.Cgst - item.Sgst - item.Cess);
                itm.GstRt = Convert.ToDouble(item.CgstPer + item.SgstPer + item.IgstPer);
                itm.SgstAmt = Convert.ToDouble(item.Sgst); //sgst
                itm.IgstAmt = Convert.ToDouble(item.Igst); //igst
                itm.CgstAmt = Convert.ToDouble(item.Cgst); //cgst
                itm.CesRt = 0; //Convert.ToDouble(item.ExciseCessPer); //cess rate
                itm.CesAmt = 0; //Convert.ToDouble(item.ExciseCessAmt); // cess amt
                itm.CesNonAdvlAmt = Convert.ToDouble(item.Cess);
                itm.StateCesRt = 0.0;
                itm.StateCesAmt = 0.0;
                itm.StateCesNonAdvlAmt = 0.0;
                itm.OthChrg = 0.0;
                itm.TotItemVal = Convert.ToDouble(item.NetTotal);
                itm.AttribDtls = null;
                reqPlGenIRN.ItemList.Add(itm);
                srno++;
            }

            reqPlGenIRN.PayDtls = null;
            reqPlGenIRN.RefDtls = null;
            reqPlGenIRN.AddlDocDtls = null;
            reqPlGenIRN.ExpDtls = null;

            reqPlGenIRN.ValDtls = new ReqPlGenIRN.ValDetails
            {
                AssVal = Convert.ToDouble(TModel.Sum(x => x.NetTotal - x.Sgst - x.Cgst - x.Igst - x.Cess)),
                CgstVal = Convert.ToDouble(TModel.Sum(x => x.Cgst)),
                SgstVal = Convert.ToDouble(TModel.Sum(x => x.Sgst)),
                IgstVal = Convert.ToDouble(TModel.Sum(x => x.Igst)), // + itemlist.Sum(x => x.VatAmount) + itemlist.Sum(x => x.AdVatAmount)); 
                CesVal = Convert.ToDouble(TModel.Sum(x => x.Cess)),
                StCesVal = 0.0,
                OthChrg = Convert.ToDouble(BModel.TcsAmt),
                RndOffAmt = Convert.ToDouble(BModel.RoundOff),
                TotInvVal = Convert.ToDouble(BModel.TotalAmount)
            };
            try
            {
                //var abc = JsonConvert.SerializeObject(reqPlGenIRN);

                TaxProEInvoice.API.TxnRespWithObj<RespPlGenIRN> txnRespWithObj = await eInvoiceAPI.GenIRNAsync(eInvSession, reqPlGenIRN, 250);

                RespPlGenIRN respPlGenIRN = txnRespWithObj.RespObj;
                string ErrorCodes = "";
                string ErrorDesc = "";
                if (txnRespWithObj.IsSuccess)
                {
                    eInvBillkontoTextEdit.Text = respPlGenIRN.Irn;
                    pictureEdit1.EditValue = respPlGenIRN.QrCodeImage;

                    var einv = new EInv
                    {
                        RefId = this.RefId,
                        RefVoucherId = this.VoucherId,
                        Irn = respPlGenIRN.Irn,
                        JwtIssuer = respPlGenIRN.JwtIssuer,
                        QrCodeImage = ImageToByte(respPlGenIRN.QrCodeImage),
                        SignedInvoice = respPlGenIRN.SignedInvoice,
                        SignedQrCode = respPlGenIRN.SignedQRCode,
                        Status = respPlGenIRN.Status,
                        AckDt = respPlGenIRN.AckDt,
                        AckNo = respPlGenIRN.AckNo,
                        RefRowId = BModel.RowId,
                        TransType = transCatLookUpEdit.EditValue.ToString()
                    };


                    if (einv.Id == 0)
                        db.EInvs.Add(einv);
                    
                    respPlGenIRN.QrCodeImage.Save(@"irn\qr_" + BModel.RefId + ".png");

                
                    db.SaveChanges();

                    this.EinvoiceModel = einv;
                    errorMemoEdit.Text = "E-invoice Generated Successfully";
                    okSimpleButton.Enabled = false;
                    return true;
                }
                else
                {
                    if (txnRespWithObj.ErrorDetails != null)
                    {
                        foreach (RespErrDetailsPl errPl in txnRespWithObj.ErrorDetails)
                        {
                            //Process errPl item here
                            ErrorCodes += errPl.ErrorCode + ",";
                            ErrorDesc += errPl.ErrorCode + ": " + errPl.ErrorMessage + Environment.NewLine;
                            MessageBox.Show(ErrorDesc);
                            errorMemoEdit.Text = ErrorDesc;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Log.Error(ex, "einv gen");

            }
             return false;
        }

        public byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
    }
}
