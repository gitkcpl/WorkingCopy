using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Windows.Forms;
using Konto.App.Shared;
using AutoUpdaterDotNET;
using System.Configuration;

namespace KontoWin
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                DevExpress.XtraEditors.WindowsFormsSettings.ForceDirectXPaint();

                DevExpress.XtraEditors.WindowsFormsSettings.EnableFormSkins();

                Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHl2AD0gPVknKsaW0un+3PuM6TTcPMUAWEURKXNso0e5OFPaZYasFtsxNoDemsFOXbvf7SIcnyAkFX/4u37NTfx7g+0IqLXw6QIPolr1PvCSZz8Z5wjBNakeCVozGGOiuCOQDy60XNqfbgrOjxgQ5y/u54K4g7R/xuWmpdx5OMAbUbcy3WbhPCbJJYTI5Hg8C/gsbHSnC2EeOCuyA9ImrNyjsUHkLEh9y4WoRw7lRIc1x+dli8jSJxt9C+NYVUIqK7MEeCmmVyFEGN8mNnqZp4vTe98kxAr4dWSmhcQahHGuFBhKQLlVOdlJ/OT+WPX1zS2UmnkTrxun+FWpCC5bLDlwhlslxtyaN9pV3sRLO6KXM88ZkefRrH21DdR+4j79HA7VLTAsebI79t9nMgmXJ5hB1JKcJMUAgWpxT7C7JUGcWCPIG10NuCd9XQ7H4ykQ4Ve6J2LuNo9SbvP6jPwdfQJB6fJBnKg4mtNuLMlQ4pnXDc+wJmqgw25NfHpFmrZYACZOtLEJoPtMWxxwDzZEYYfT";

                Log.Logger = new LoggerConfiguration()
                      .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                      .CreateLogger();

                MessageBoxAdv.Office2016Theme = Office2016Theme.Colorful;

                MessageBoxAdv.MessageBoxStyle = MessageBoxAdv.Style.Office2016;
                MessageBoxAdv.DropShadow = true;
                KontoGlobals.sqlConnectionString = new System.Data.SqlClient.SqlConnectionStringBuilder(KontoGlobals.Conn);

                if (ConfigurationManager.AppSettings["Edition"] == null)
                {
                    Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                    config.AppSettings.Settings.Add("Edition", "0");
                    config.Save(ConfigurationSaveMode.Modified);

                }

                KontoGlobals.Edition = Convert.ToInt32(ConfigurationManager.AppSettings["Edition"].ToString());


                Application.Run(new Form1());
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Startup Error");
            }
            
            
        }

        

       
    }
}
