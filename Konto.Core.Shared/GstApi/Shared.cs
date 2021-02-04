using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaxProGST.API;

namespace TaxProGSTApiWinFormsDemo
{
    public static class Shared
    {
        /// <summary>
        /// Save API Setting Details as 'ApiSetting.json' in program folder
        /// </summary>
        /// <param name="ApiSetting">Session for Connectivity with API</param>
        /// <param name="FileName">ApiSetting.json</param>
        public static void SaveAPISetting(APISetting ApiSetting, string FileName = "ApiSetting.json")
        {
            //Save ApiSetting in Json File
            File.WriteAllText(FileName, JsonConvert.SerializeObject(ApiSetting, Formatting.Indented));
            //For Saving APISetting in Database you can write your code
        }

        /// <summary>
        /// Save API Setting Details as 'ApiSetting.json' in program folder
        /// </summary>
        /// <param name="ApiLoginDetails">Session for Connectivity with API</param>
        /// <param name="FileName">ApiLoginDetails.json</param>
        public static void SaveAPILoginDetails(APILoginDetails ApiLoginDetails, string FileName = "ApiLoginDetails.json")
        {
            //Save ApiLoginDetails in Json File
            File.WriteAllText(FileName, JsonConvert.SerializeObject(ApiLoginDetails, Formatting.Indented));
            //For Saving ApiLoginDetails in Database you can write your code
        }

        /// <summary>
        /// Load API Setting Details
        /// </summary>
        /// <param name="FileName">File Name from which details Load</param>
        /// <returns></returns>
        public static APISetting LoadAPISetting(string FileName = "ApiSetting.json")
        {
            #region Load API Setting from json file
            APISetting apiSetting = new APISetting();
            //Read Settings from Json File
            if(File.Exists(FileName))
                apiSetting = JsonConvert.DeserializeObject<APISetting>(File.ReadAllText(FileName));

            if(File.Exists("AspDsc.pfx"))
                apiSetting.AspDscPfx = File.ReadAllBytes("AspDsc.pfx");
            if(string.IsNullOrEmpty(apiSetting.AspDscPassword))
                apiSetting.AspDscPassword = "taxpro1234";
            return apiSetting;
            #endregion
            #region Load API Setting from Database or Others or Manually
            ////API Session Consist of mainly 
            ////APISetting - Common for All TaxPayer
            ////APILoginDetails - TaxPayer Specific 
            ////And Other fields like ReturnPeriod,TxnID,UserIP
            ////Provide Values for API Settings - Alternatively load Settings from DB/XML config file/Json config file, etc
            //APISetting myApiSetting = new APISetting();
            //myApiSetting.ASPName = "TaxPro_Sandbox";             //Use TaxPro ASP for Production Testing
            //myApiSetting.IsActive = true;
            //myApiSetting.IsDefault = true;
            //myApiSetting.AspWebsite = "www.taxprogsp.co.in";
            //myApiSetting.AspUserId = "xxxxxxxxxx";      //Use Your 10 digit TaxPro ASP ID.
            //myApiSetting.AspPassword = "================";             //ASPSecretKey
            //myApiSetting.AspAccountPassword = "------------";
            //myApiSetting.UrlAuth = "https://api.taxprogsp.co.in/taxpayerapi/v1.0/authenticate";
            //myApiSetting.UrlReturn = "https://api.taxprogsp.co.in/taxpayerapi/v0.3/returns";
            //myApiSetting.UrlLedger = "https://api.taxprogsp.co.in/taxpayerapi/v0.2/ledgers";
            //myApiSetting.UrlAspUtil = "https://api.taxprogsp.co.in/aspapi/v1.0";
            //if(File.Exists("AspDsc.pfx"))
            //    myApiSetting.AspDscPfx = File.ReadAllBytes("AspDsc.pfx");
            //myApiSetting.AspDscPassword = "taxpro1234";
            //myApiSetting.AspEK = null;
            //myApiSetting.AspSessionID = null;
            //myApiSetting.AspSessionValidityMins = 0;
            //myApiSetting.AspSessionExp = null;
            //return myApiSetting;

            #endregion

        }

        /// <summary>
        /// Load API Login Details
        /// </summary>
        /// <param name="FileName">File Name from which details Load</param>
        /// <returns></returns>
        public static APILoginDetails LoadAPILoginDetails(string FileName = "ApiLoginDetails.json")
        {
            #region Load Settings from Json File
            if(File.Exists(FileName))
                return JsonConvert.DeserializeObject<APILoginDetails>(File.ReadAllText(FileName));
            else
                return new APILoginDetails();
            #endregion
            #region Load API Login Details from Database or Others or Manually
            //APILoginDetails myLoginDetails = new APILoginDetails();
            //myLoginDetails.AppKey = "";//Load Previously Stored AppKey
            //myLoginDetails.AuthToken = "";//Load Previously Stored AuthToken
            //myLoginDetails.SessionEK = "";//Load Previously Stored SessionEK
            //myLoginDetails.GSTIN = "";//TaxPayer GSTIN
            //myLoginDetails.GstPortalUserID = "";//TaxPayer GST Portal User ID
            //myLoginDetails.TokenExp = DateTime.Now;//Load Previously Stored TokenExp to handle RefreshToken
            //return myLoginDetails;
            #endregion
        }
    }
}
