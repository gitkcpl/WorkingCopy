using Konto.Core.Shared.Libs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaxProGST.API;

namespace TaxProGSTApiWinFormsDemo
{
    public partial class frmAuthToken : Form
    {
        GSTSession GstSession = new GSTSession();

        public frmAuthToken()
        {
            InitializeComponent();
            this.Load += FrmAuthToken_Load;
            GstSession.ApiSetting = Shared.LoadAPISetting();
            GstSession.ApiLoginDetails = Shared.LoadAPILoginDetails();
            ShowUpdatedAPILoginDetails();
        }

        private void FrmAuthToken_Load(object sender, EventArgs e)
        {
            txtGSTUserID.Text = KontoUtils.Company.GstInUserId;
            txtGSTIN.Text = KontoUtils.Company.GstIn;
        }

        public void ShowUpdatedAPILoginDetails()
        {
            try
            {
                if(GstSession.ApiLoginDetails != null)
                {
                    txtGSTUserID.Text = GstSession.ApiLoginDetails?.GstPortalUserID ?? "";
                    txtGSTIN.Text = GstSession.ApiLoginDetails?.GSTIN ?? "";
                    txtAppKey.Text = GstSession.ApiLoginDetails?.AppKey ?? "";
                    txtAuthToken.Text = GstSession.ApiLoginDetails?.AuthToken ?? "";
                    if(GstSession.ApiLoginDetails.TokenExp != null)
                        txtExpiry.Text = GstSession.ApiLoginDetails.TokenExp.ToString("dd/MM/yyyy hh:mm:ss tt");
                    else
                        txtExpiry.Text = "";
                    txtSEK.Text = GstSession.ApiLoginDetails?.SessionEK ?? "";
                }
            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
           
        }
        private async void btnGetOTP_Click(object sender, EventArgs e)
        {
            try
            {
                lblOutCome.Text = "Wait...";
                await Task.Delay(200);
                if(GstSession.ApiLoginDetails == null)
                    GstSession.ApiLoginDetails = new APILoginDetails();
                if(GstSession.ApiLoginDetails.GSTIN != txtGSTIN.Text)
                {
                    GstSession.ApiLoginDetails.GSTIN = txtGSTIN.Text;
                    GstSession.ApiLoginDetails.GstPortalUserID = txtGSTUserID.Text;
                }
                TxnRespWithRefAck otpResp = await AuthAPI.GetOTPAsync(GstSession);
                lblOutCome.Text = otpResp.IsSuccess.ToString() + " - " + otpResp.TxnOutcome +
                    "\nAppKey Established (Please Store): " + otpResp.RefOrAckNo;
                //Save Updated Details
                Shared.SaveAPISetting(GstSession.ApiSetting);
                Shared.SaveAPILoginDetails(GstSession.ApiLoginDetails);
                ShowUpdatedAPILoginDetails();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        private async void btnGetAuthToken_Click(object sender, EventArgs e)
        {
            try
            {
                lblOutCome.Text = "Please Wait...";
                await Task.Delay(200);
                if(GstSession.ApiLoginDetails == null)
                    GstSession.ApiLoginDetails = new APILoginDetails();
                if(GstSession.ApiLoginDetails.GSTIN != txtGSTIN.Text)
                {
                    GstSession.ApiLoginDetails.GSTIN = txtGSTIN.Text;
                    GstSession.ApiLoginDetails.GstPortalUserID = txtGSTUserID.Text;
                }
                TxnRespWithObj<APISession> AuthResp = await AuthAPI.GetAuthTokenAsync(GstSession, txtOTP.Text);
                lblOutCome.Text = AuthResp.TxnOutcome + "\nAuth Token (Please Store): " + GstSession.ApiLoginDetails.AuthToken +
                    "\nSession EK: " + GstSession.ApiLoginDetails.SessionEK +
                    "\nExpiary: " + GstSession.ApiLoginDetails.TokenExp;
                //Save Updated Details
                Shared.SaveAPISetting(GstSession.ApiSetting);
                Shared.SaveAPILoginDetails(GstSession.ApiLoginDetails);
                ShowUpdatedAPILoginDetails();

            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.Message); 
            }
          
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                GstSession.ApiLoginDetails.GSTIN = txtGSTIN.Text;
                GstSession.ApiLoginDetails.GstPortalUserID = txtGSTUserID.Text;
                //Save Updated Details
                Shared.SaveAPISetting(GstSession.ApiSetting);
                Shared.SaveAPILoginDetails(GstSession.ApiLoginDetails);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtGSTIN_TextChanged(object sender, EventArgs e)
        {
            txtAppKey.Text = "";
            txtAuthToken.Text = "";
            txtExpiry.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
        }
    }
}
