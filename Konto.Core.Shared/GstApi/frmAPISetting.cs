using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaxProGST.API;

namespace TaxProGSTApiWinFormsDemo
{
    public partial class frmAPISetting : Form
    {
        GSTSession GstSession = new GSTSession();
        public frmAPISetting()
        {
            InitializeComponent();
            GstSession.ApiSetting = Shared.LoadAPISetting();
            if(GstSession.ApiSetting != null)
            {
                txtDSCPassword.Text = GstSession.ApiSetting?.AspDscPassword ?? "";
                txtAspUserId.Text = GstSession.ApiSetting?.AspUserId ?? "";
                txtAspAcPassword.Text = GstSession.ApiSetting?.AspAccountPassword ?? "";
                txtAspPassword.Text = GstSession.ApiSetting?.AspPassword ?? ""; //ASP Secret Key
                txtASPName.Text = GstSession.ApiSetting?.ASPName ?? "";
                txtAspWebsite.Text = GstSession.ApiSetting?.AspWebsite ?? "";
                txtUrlAuth.Text = GstSession.ApiSetting?.UrlAuth ?? "";
                txtUrlReturn.Text = GstSession.ApiSetting?.UrlReturn ?? "";
                txtUrlLedger.Text = GstSession.ApiSetting?.UrlLedger ?? "";
                txtUrlAspUtil.Text = GstSession.ApiSetting?.UrlAspUtil ?? "";
            }
        }

        private void btnGenASPDSC_Click(object sender, EventArgs e)
        {
            GenerateAspDsc();
        }
        private bool GenerateAspDsc()
        {

            string strKeyPath = AppDomain.CurrentDomain.BaseDirectory + @"AspDsc.key";
            string strCertPath = AppDomain.CurrentDomain.BaseDirectory + @"AspDsc.cer";
            string strPfxPath = AppDomain.CurrentDomain.BaseDirectory + @"AspDsc.pfx";
            string strUtility = AppDomain.CurrentDomain.BaseDirectory + @"openssl\openssl.exe ";
            if(File.Exists(strPfxPath))
            {
                if(MessageBox.Show("ASP Certificate already Exists. This certificate may be registered with GSP API Server. Are you are sure you want to re-generate ASP Certificate?", "Regenerate ASP DSC?",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
                    return false;
            }

            bool IsGenDscSuccess = true;
            if(!Directory.Exists("Settings"))
                Directory.CreateDirectory("Settings");
            ////Run openssl to create Private Key and Public Key
            try
            {
                string strCommandLine = @"req -x509 -nodes -sha256 -days 3650 -newkey rsa:2048 -keyout """ + strKeyPath + @""" -out """ + strCertPath + @""" -config openssl.cnf -subj ""/CN=" + GstSession.ApiSetting.ASPName + @"/C=IN/OU=GST""";
                Process oPro = new Process();
                oPro.StartInfo.UseShellExecute = true;
                oPro.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory + "openssl";
                oPro.StartInfo.Arguments = strCommandLine;
                oPro.StartInfo.FileName = strUtility;
                oPro.Start();
                oPro.WaitForExit();
            }
            catch(Exception ex)
            {
                IsGenDscSuccess = false;
                throw ex;
            }
            if(IsGenDscSuccess)
            {
                try
                {
                    //string strKeyPath1 = AppDomain.CurrentDomain.BaseDirectory + @"Settings\AspDsc.key";
                    //string strCertPath1 = AppDomain.CurrentDomain.BaseDirectory + @"Settings\AspDsc.cer";
                    //string strProvider1 = "Microsoft Enhanced RSA and AES Cryptographic Provider";
                    string strUtility1 = AppDomain.CurrentDomain.BaseDirectory + @"openssl\openssl.exe ";
                    string strCommandLine1 = @"pkcs12 -inkey """ + strKeyPath + @""" -in """ + strCertPath + @""" -export -out """ + strPfxPath + @""" -password pass:" + GstSession.ApiSetting.AspDscPassword + @" -name """ + GstSession.ApiSetting.ASPName + @""" -CSP ""Microsoft Enhanced RSA and AES Cryptographic Provider""";
                    Process oPro1 = new Process();
                    oPro1.StartInfo.UseShellExecute = true;
                    oPro1.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory + "openssl";
                    oPro1.StartInfo.Arguments = strCommandLine1;
                    oPro1.StartInfo.FileName = strUtility1;
                    oPro1.StartInfo.ErrorDialog = true;
                    oPro1.Start();
                    oPro1.WaitForExit();
                }
                catch(Exception ex)
                {
                    IsGenDscSuccess = false;
                    throw ex;
                }
            }
            if(IsGenDscSuccess)
            {
                lblDSCMsg.Text = "ASP DSC Generated Successfully";
                Shared.SaveAPISetting(GstSession.ApiSetting);
            }
            return IsGenDscSuccess;
        }

        private void lnkReqASPAc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(string.IsNullOrEmpty(GstSession.ApiSetting.AspWebsite))
            {
                MessageBox.Show("Please specify ASP Website or Host name");
                return;
            }
            string strCertPath = AppDomain.CurrentDomain.BaseDirectory + @"AspDsc.cer";
            X509Certificate2 PubCert = new X509Certificate2(strCertPath);
            string AspAcReqUrl = "https://crm." + GetHostName(GstSession.ApiSetting.AspWebsite) + "/AspRegistration?AcType=ASP&cer=" + RestSharp.Extensions.MonoHttp.HttpUtility.UrlEncode(Convert.ToBase64String(PubCert.RawData));
            Process.Start(new ProcessStartInfo(AspAcReqUrl));
        }
        private string GetHostName(string Website)
        {
            string HostName = Website.ToLower();
            if(HostName.StartsWith("www."))
            {
                HostName = HostName.Replace("www.", "");
            }
            return HostName;
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtEncASPSecret.Text))
            {
                MessageBox.Show("Please provide Encrypted ASP Secret Key you received from GSP in e-mail.");
                return;
            }
            try
            {
                GstSession.ApiSetting.AspPassword = TPCrypto.DecryptUsingPvtKey(txtEncASPSecret.Text, GstSession.ApiSetting.AspDscPfx, GstSession.ApiSetting.AspDscPassword);
                txtAspPassword.Text = GstSession.ApiSetting.AspPassword;
                MessageBox.Show("ASP Sec Key Decrypted & Saved to API Settings.");
                Shared.SaveAPISetting(GstSession.ApiSetting);
            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
          
        }

        private async void btnDownLoadApiSetting_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(GstSession.ApiSetting.AspWebsite))
                    GstSession.ApiSetting.AspWebsite = "www.taxprogsp.co.in";

                TxnResp txnResp = await GspAPI.DownLoadAPISettingsAsync(GstSession);

                //Force refresh of ApiSetting
                GSTSession gs = GstSession;
                GstSession = null;
                GstSession = gs;
                txtASPName.Text = GstSession?.ApiSetting?.ASPName ?? "";
                txtAspWebsite.Text = GstSession?.ApiSetting?.AspWebsite ?? "";
                txtUrlAuth.Text = GstSession?.ApiSetting?.UrlAuth ?? "";
                txtUrlReturn.Text = GstSession?.ApiSetting?.UrlReturn ?? "";
                txtUrlAspUtil.Text = GstSession?.ApiSetting?.UrlAspUtil ?? "";
                lblAPIMsg.Text = txnResp.TxnOutcome;
            }
            catch(Exception ex)
            {
                lblAPIMsg.Text = ex.Message;
            }
        }

        private  void btnSandboxSetting_Click(object sender, EventArgs e)
        {
            try
            {
                txtASPName.Text = "TaxPro_Sandbox";
                txtAspWebsite.Text = "www.taxprogsp.co.in";
                txtUrlAuth.Text = "https://gstsandbox.charteredinfo.com/taxpayerapi/v1.0/authenticate";
                txtUrlReturn.Text = "https://gstsandbox.charteredinfo.com/taxpayerapi/v0.3/returns";
                txtUrlLedger.Text = "https://gstsandbox.charteredinfo.com/taxpayerapi/v0.3/ledgers";
                txtUrlAspUtil.Text = "https://gstapi.charteredinfo.com/aspapi/v1.0";
                lblAPIMsg.Text = "Sandbox Setting Loaded Successfully";
            }
            catch(Exception ex)
            {
                lblAPIMsg.Text = ex.Message;
            }
        }

        
        private async void btnRegenerateAspDscCertificate_Click(object sender, EventArgs e)
        {
            if(GenerateAspDsc())
            {
                try
                {
                    string strCertPath = AppDomain.CurrentDomain.BaseDirectory + @"AspDsc.cer";
                    TxnResp txnResp = await GspAPI.SetAspPublicCertAsync(GstSession, strCertPath);
                    lblAPIMsg.Text = txnResp.TxnOutcome;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private async void btnGetApiBal_Click(object sender, EventArgs e)
        {
            try
            {
                TxnRespWithObj<AspApiBalance> txnResp = await GspAPI.GetAspApiBalanceAsync(GstSession);
                lblAPIMsg.Text = txnResp.TxnOutcome;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSaveSetting_Click(object sender, EventArgs e)
        {
            try
            {
                if(GstSession.ApiSetting == null)
                    GstSession.ApiSetting = new APISetting();
                GstSession.ApiSetting.AspDscPassword = txtDSCPassword.Text;
                GstSession.ApiSetting.AspUserId = txtAspUserId.Text;
                GstSession.ApiSetting.AspAccountPassword = txtAspAcPassword.Text;
                GstSession.ApiSetting.AspPassword = txtAspPassword.Text;
                GstSession.ApiSetting.ASPName = txtASPName.Text;
                GstSession.ApiSetting.AspWebsite = txtAspWebsite.Text;
                GstSession.ApiSetting.UrlAuth = txtUrlAuth.Text;
                GstSession.ApiSetting.UrlReturn = txtUrlReturn.Text;
                GstSession.ApiSetting.UrlLedger = txtUrlLedger.Text;
                GstSession.ApiSetting.UrlAspUtil = txtUrlAspUtil.Text;
                Shared.SaveAPISetting(GstSession.ApiSetting);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
