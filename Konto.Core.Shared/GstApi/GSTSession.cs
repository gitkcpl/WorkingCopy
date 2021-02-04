using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxProGST.API;

namespace TaxProGSTApiWinFormsDemo
{
    public class GSTSession : APISession
    {

        public GSTSession()
        {
            UserIP = "DESKTOPAPP";////Use "DESKTOPAPP" & TaxPro API server will pickup IP of connecting user
            EnableAutoRefreshTokenOnApiCall = true;
            //Event Handler for StartAPI Transaction : You can check Licensing or anything that you want to do before starting API Transaction
            StartingApiTxn += StartAPITransactionHandler;
            //Event to Handle GSP Refresh Session RefreshSessionCompleted
            RefreshSessionCompleted += StoreRefreshedSession;
            //Event to Handle AuthToken Completed Event
            RefreshAuthTokenCompleted += StoreRefreshedAuth;
            //Event to handle Get OTP and Authorization Token after expiry
            RequiresReEstablisingOTPandAuthToken += ReEstablisingOTPandAuthToken;
        }
        
        private async void StoreRefreshedSession(object sender, EventArgs e)
        {
            //Update SessionID and ASPEk to ApiSettings
            Shared.SaveAPISetting(ApiSetting);

        }
        private async void StoreRefreshedAuth(object sender, EventArgs e)
        {
            //Update Refresh Token Information for TaxPayer like AuthToken, SEK,TokenExp and AppKey
            Shared.SaveAPILoginDetails(ApiLoginDetails);
        }

        private void StartAPITransactionHandler(object sender, EventArgs e)
        {
            //Event Handler for StartAPI Transaction : You can check Licensing or anything that you want to do before starting API Transaction
            //if(true )
            //{
            //    CancleApiTxn = true;
            //    CancelApiMsg = "API Cancel";
            //}
        }
        public async void ReEstablisingOTPandAuthToken(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// Login Details of API Transaction
        /// </summary>
        /// <param name="e"></param>
        public override void LogAPITxn(APITxnLogArgs e)
        {
            string LogLine = DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " ";
            LogLine += e.TxnID + " ";
            LogLine += e.FormNo + " ";
            LogLine += e.ApiAction + " ";
            LogLine += e.ApiTitle + " ";
            LogLine += e.OutcomeMsg + " ";
            LogLine += e.ErrCode + " ";
            LogLine += e.Info2 + " ";
            File.WriteAllText("GstApiTxnLog.txt", LogLine);
        }
       
    }
}
