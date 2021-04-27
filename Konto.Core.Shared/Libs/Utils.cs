using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraVerticalGrid;
using Konto.Data.Models.Masters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using Konto.Data;

namespace Konto.Core.Shared.Libs
{
    public static class KontoUtils
    {
        public static CompModel Company { get; set; }

        public static string ZeroIfEmpty(this string s)
        {
            return string.IsNullOrEmpty(s) ? "0" : s;
        }
        public static DateTime IToD(int date)
        {
            DateTime dt;
            if (DateTime.TryParseExact(date.ToString(), "yyyyMMdd",
                          CultureInfo.InvariantCulture,
                          DateTimeStyles.None, out dt))
            {
                return dt;
            }
            return DateTime.Now;
        }
        public static string Encrypt(string input, string key)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string input, string key)
        {
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        public static void SaveMainFormLayout(string _filename, object _lyt)
        {
            try
            {
                string UserFileName = "userxml\\" + _filename;

                var gv = (LayoutControl)_lyt;

                gv.SaveLayoutToXml(UserFileName);

                MessageBox.Show("Successfully Saved Main Layout!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public static void RestoreMainFormLayout(string _filename, object _lyt)
        {
            string UserFileName = "userxml\\" + _filename;
            string SystemFileName = "sysxml\\" + _filename;
            var gv = (LayoutControl)_lyt;
            if (File.Exists(UserFileName))
                gv.RestoreLayoutFromXml(UserFileName);
            else if (File.Exists(SystemFileName))
                gv.RestoreLayoutFromXml(SystemFileName);
        }
        public static void ExportGridToExcel(GridView GridControl1, SplashScreenManager splashScreenManager1)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Files(*.XLS)|*.XLSX";
                saveFileDialog.AddExtension = true;
                saveFileDialog.DefaultExt = ".XLSX";


                if (saveFileDialog.ShowDialog() == DialogResult.OK && saveFileDialog.CheckPathExists)
                {

                    if (splashScreenManager1 != null)
                    {


                        splashScreenManager1.ShowWaitForm();
                        splashScreenManager1.SetWaitFormDescription("Exporting..Please Wait");
                    }
                    GridControl1.ExportToXlsx(saveFileDialog.FileName);

                    if (splashScreenManager1 != null)
                        splashScreenManager1.CloseWaitForm();

                    if (MessageBox.Show(@"Do you wish to open the xls file now?", @"Export to Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Process proc = new Process();
                        proc.StartInfo.FileName = saveFileDialog.FileName;
                        proc.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManager1 != null && splashScreenManager1.IsSplashFormVisible)
                    splashScreenManager1.CloseWaitForm();


                MessageBox.Show(ex.ToString());
            }

        }
        public static void ExportGridToPDF(GridView GridControl1, SplashScreenManager splashScreenManager1)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Files(*.PDF)|*.PDF";
                saveFileDialog.AddExtension = true;
                saveFileDialog.DefaultExt = ".PDF";


                if (saveFileDialog.ShowDialog() == DialogResult.OK && saveFileDialog.CheckPathExists)
                {

                    if (splashScreenManager1 != null)
                    {


                        splashScreenManager1.ShowWaitForm();
                        splashScreenManager1.SetWaitFormDescription("Exporting..Please Wait");
                    }
                    GridControl1.ExportToPdf(saveFileDialog.FileName);

                    if (splashScreenManager1 != null)
                        splashScreenManager1.CloseWaitForm();

                    if (MessageBox.Show(@"Do you wish to open the PDF file now?", @"Export to PDF", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Process proc = new Process();
                        proc.StartInfo.FileName = saveFileDialog.FileName;
                        proc.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManager1 != null && splashScreenManager1.IsSplashFormVisible)
                    splashScreenManager1.CloseWaitForm();


                MessageBox.Show(ex.ToString());
            }

        }
        public static void ExportGridToWord(GridView GridControl1, SplashScreenManager splashScreenManager1)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Files(*.RTF)|*.RTF";
                saveFileDialog.AddExtension = true;
                saveFileDialog.DefaultExt = ".RTF";


                if (saveFileDialog.ShowDialog() == DialogResult.OK && saveFileDialog.CheckPathExists)
                {

                    if (splashScreenManager1 != null)
                    {


                        splashScreenManager1.ShowWaitForm();
                        splashScreenManager1.SetWaitFormDescription("Exporting..Please Wait");
                    }
                    GridControl1.ExportToRtf(saveFileDialog.FileName);

                    if (splashScreenManager1 != null)
                        splashScreenManager1.CloseWaitForm();

                    if (MessageBox.Show(@"Do you wish to open the RTF file now?", @"Export to RTF", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Process proc = new Process();
                        proc.StartInfo.FileName = saveFileDialog.FileName;
                        proc.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManager1 != null && splashScreenManager1.IsSplashFormVisible)
                    splashScreenManager1.CloseWaitForm();


                MessageBox.Show(ex.ToString());
            }

        }

        public static void LoadGridProperty(PanelControl panelControl1, object gv, ListAction ribbonControl1=null)
        {
            if (panelControl1.Controls.Count > 0)
                panelControl1.Controls.RemoveAt(0);

            PropertyGridControl propertyGridControl1 =
                new PropertyGridControl();

            ((ISupportInitialize)(propertyGridControl1)).BeginInit();
            propertyGridControl1.Dock = DockStyle.Fill;
            panelControl1.Width = 340;
            panelControl1.Controls.Add(propertyGridControl1);
            panelControl1.Visible = true;
            ((ISupportInitialize)(propertyGridControl1)).EndInit();
            propertyGridControl1.Visible = true;
            propertyGridControl1.FindPanelVisible = true;
            propertyGridControl1.SelectedObject = gv;
            propertyGridControl1.RetrieveFields();
            if (ribbonControl1 != null)
            ribbonControl1.SettingsButtonEnable(false);

            //foreach (var item in ribbonControl1.Items)
            //{
            //    if (item.ToString() == "DevExpress.XtraBars.BarButtonItem" ||
            //        item.ToString() == "DevExpress.XtraBars.BarSubItem")
            //    {
            //        if (item.ToString() == "DevExpress.XtraBars.BarSubItem")
            //        {
            //            var it = (BarSubItem)item;
            //            if (it.Name != "customizedBarSubItem")
            //                it.Enabled = false;
            //        }
            //        else
            //        {
            //            var it1 = (BarButtonItem)item;

            //            if (@"gridBarButtonItem,columnBarButtonItem,resetGridBarButtonItem,cancelSettingsBarButtonItem,closeBarButtonItem".IndexOf(it1.Name, StringComparison.Ordinal) <
            //                0)
            //            {
            //                it1.Enabled = false;
            //            }

            //        }
            //    }
            //}
        }

        public static void HideGridProperty(PanelControl panelControl1, ListAction ribbonControl1=null)
        {
            panelControl1.Visible = false;
            if(ribbonControl1!=null)
            ribbonControl1.SettingsButtonEnable(true);
        }

        public static void RestoreLayoutGrid(string _filename, object _gv)
        {
            string UserFileName = "userxml\\" + _filename;
            string SystemFileName = "sysxml\\" + _filename;

            var gv = (GridView)_gv;

            if (File.Exists(UserFileName))
                gv.RestoreLayoutFromXml(UserFileName);
            else if (File.Exists(SystemFileName))
                gv.RestoreLayoutFromXml(SystemFileName);

        }

        public static void SaveLayoutGrid(string _filename, object _gv)
        {
                string UserFileName = "userxml\\" + _filename;
                var gv = (GridView)_gv;
                gv.ClearGrouping();
                gv.ClearSorting();
                gv.ActiveFilterString = string.Empty;
                gv.SaveLayoutToXml(UserFileName);

                MessageBox.Show("Successfully Saved Grid Layout !!");
        }
        public static void ResetGridLayout(string _filename, object _gv)
        {
            
                ((GridView)_gv).Columns.Clear();
                ((GridView)_gv).PopulateColumns();
                ((GridView)_gv).BestFitColumns(true);
           
        }

        public static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[36];
            chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        public static string TruncateLongString(this string str, int maxLength)
        {
            if (string.IsNullOrEmpty(str)) return str;

            return str.Substring(0, Math.Min(str.Length, maxLength));
        }

        public static List<ReportParaModel> SetReportParameter(XtraReport xrep, GridView vw, string rep_para, int _ReportId)
        {
            var _paraList = new List<ReportParaModel>();

            if (vw.SelectedRowsCount > 0)
            {
                xrep.Parameters[rep_para].Value = "Y";

                foreach (var item in vw.GetSelectedRows())
                {
                    var _acc = vw.GetRow(item) as BaseLookupDto;
                    var ModelReport = new ReportParaModel
                    {
                        ReportId = _ReportId,
                        ParameterName = rep_para,
                        ParameterValue = _acc.Id
                    };
                    _paraList.Add(ModelReport);
                }
            }

            return _paraList;
        }
    }
}
