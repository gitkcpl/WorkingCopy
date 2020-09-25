using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Windows.Forms;
using Konto.App.Shared;
using AutoUpdaterDotNET;

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
                
                

                Log.Logger = new LoggerConfiguration()
                      .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                      .CreateLogger();

                MessageBoxAdv.Office2016Theme = Office2016Theme.Colorful;

                MessageBoxAdv.MessageBoxStyle = MessageBoxAdv.Style.Office2016;
                MessageBoxAdv.DropShadow = true;
                KontoGlobals.sqlConnectionString = new System.Data.SqlClient.SqlConnectionStringBuilder(KontoGlobals.Conn);
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Startup Error");
            }
            
            
        }

        

       
    }
}
