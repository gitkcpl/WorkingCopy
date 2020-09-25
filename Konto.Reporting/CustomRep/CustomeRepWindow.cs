using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Design;
using GrapeCity.ActiveReports.PageReportModel;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Data.Models.Masters;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace Konto.Reporting.CustomRep
{
    public partial class CustomeRepWindow : KontoForm
    {
        public List<SpParaModel> SpParaList = new List<SpParaModel>();
        public List<CustomRepModel> CustomList = new List<CustomRepModel>();
        public string ReportType;
        public string SPName;
        public string FileName;
        public int VTypeId;
        public CustomeRepWindow()
        {
            InitializeComponent();
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("GRID","GRID"),
                new ComboBoxPairs("DOCUMENT", "DOCUMENT"),
                new ComboBoxPairs("OTHER","OTHER"),
            };
            RTypeLookUpEdit.Properties.DataSource = cbp;
            List<ComboBoxPairs> ort = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("LANDSCAP","LANDSCAP"),
                new ComboBoxPairs("PORTRAIT", "PORTRAIT")
            };
            orientationLookUpEdit.Properties.DataSource = ort;
        }

        private void CustomeRepWindow_Load(object sender, System.EventArgs e)
        {
            ReportShow();
        }

        private void ReportShow()
        {
            using (KontoContext db = new KontoContext())
            {
                string sql = SPName;
                DataTable DtCriterias = new DataTable();
                try
                {
                    using (var con = new SqlConnection(KontoGlobals.Conn))
                    using (var cmd = new SqlCommand(sql, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        DtCriterias.Load(cmd.ExecuteReader());
                    }
                    CustomRepModel CustModel;
                    foreach (var item in DtCriterias.Columns)
                    {
                        CustModel = new CustomRepModel();
                        CustModel.FieldName = item.ToString();
                        CustModel.Heading = item.ToString();
                        CustomList.Add(CustModel);
                    }
                    CustomList = new List<CustomRepModel>(
                                CustomList.OrderBy(k => k.FieldName).ToList());

                    gridControl.DataSource = CustomList;
                }
                catch (Exception ex)
                {
                    //serilog.Log.Error(ex, "GRN List Error");
                }
            }
        }

        private void okSimpleButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                using (KontoContext db = new KontoContext())
                {
                    // if (Model.Id == 0)
                    {
                        ReportTypeModel reportModel = new ReportTypeModel();
                        if (!string.IsNullOrEmpty(ReportNametextEdit.Text))
                        {
                            var _find = db.ReportTypes.FirstOrDefault(k => k.ReportName == ReportNametextEdit.Text);
                            if (_find == null)
                            {
                                reportModel.ReportName = ReportNametextEdit.Text;
                                reportModel.CreateDate = DateTime.Now;
                                reportModel.CreateUser = KontoGlobals.UserName;

                                decimal rep = db.ReportTypes.Max(k => k.Id);
                                if (rep != null)
                                {
                                    rep = rep + 1;
                                }
                                reportModel.FileName = FileName + rep.ToString() + ".rdlx";
                                reportModel.SpName = SPName;
                                reportModel.IsActive = true;
                                reportModel.IsDeleted = false;
                                reportModel.ReportTypes = ReportType;
                                reportModel.VoucherTypeId = VTypeId;

                                db.ReportTypes.Add(reportModel);
                                db.SaveChanges();

                                var custList = CustomList.Where(k => k.Show == true).ToList();
                                foreach (var item in custList)
                                {
                                    item.RepId = reportModel.Id;
                                    item.ReportTypes = RTypeLookUpEdit.Text;
                                    //item.HeaderText = Model.HeaderText;
                                    //item.FooterText = Model.FooterText;
                                    if (!string.IsNullOrEmpty(orientationLookUpEdit.Text))
                                        item.Appearance = orientationLookUpEdit.Text;
                                    else
                                        item.Appearance = "Portrait";
                                }
                                db.Customreps.AddRange(custList);
                                db.SaveChanges();
                            }
                            //else
                            //{
                            //    reportModel = _find;
                            //    reportModel.ReportName = ReportName;
                            //    db.Entry(reportModel).State = System.Data.Entity.EntityState.Modified;
                            //}

                        }

                        if (RTypeLookUpEdit.Text.ToUpper() != "GRID")
                        {
                            PageReport _pageReport = new PageReport();
                            // _pageReport.Load(new System.IO.FileInfo(System.Windows.Forms.Application.StartupPath + "\\reg\\Ord\\CustReport.rdlx"));

                            GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(_pageReport);

                            // Add Parameters to report document

                            foreach (var item in SpParaList)
                            {
                                ReportParameter param = new ReportParameter();
                                param.Name = item.ParaName;

                                if (item.ParaType == "int")
                                {
                                    param.DataType = ReportParameterDataType.Integer;
                                    param.DefaultValue.Values.Add(item.DefaultValue);
                                }
                                else if (item.ParaType == "string" || item.ParaType.ToUpper().Contains("VARCHAR"))
                                {
                                    param.DataType = ReportParameterDataType.String;
                                    param.DefaultValue.Values.Add(item.DefaultValue);
                                }
                                else if (item.ParaType == "bool")
                                {
                                    param.DataType = ReportParameterDataType.Boolean;
                                    param.DefaultValue.Values.Add(item.DefaultValue);
                                }
                                _pageReport.Report.ReportParameters.Add(param);
                            }

                            ReportParameter param1 = new ReportParameter();
                            param1.Name = "report_title";
                            param1.DataType = ReportParameterDataType.String;
                            _pageReport.Report.ReportParameters.Add(param1);

                            ReportParameter param2 = new ReportParameter();
                            param2.Name = "report_footer";
                            param2.DataType = ReportParameterDataType.String;
                            _pageReport.Report.ReportParameters.Add(param2);

                            ReportParameter param3 = new ReportParameter();
                            param3.Name = "GroupOn";
                            param3.DataType = ReportParameterDataType.String;
                            _pageReport.Report.ReportParameters.Add(param3);

                            ReportParameter param4 = new ReportParameter();
                            param4.Name = "keycon";
                            param4.DataType = ReportParameterDataType.String;
                            _pageReport.Report.ReportParameters.Add(param4);

                            _pageReport = ReportLayoutBuilder.AddDataSetDataSource(_pageReport, SPName, reportModel.Id, "Detail", SpParaList);//Adding DataSources to the PageReport object


                            _pageReport = ReportLayoutBuilder.BuildReportLayout1(_pageReport, reportModel.Id, "Detail");//Loading the layout to a PageReport object

                            MemoryStream reportStream = ReportLayoutBuilder.LoadReportToStream(_pageReport);//Loading the PageReport object to a stream
                            reportStream.Position = 0;

                            //Loading the stream to the designer via a  XmlReader
                            //reportDesigner.LoadReport(XmlReader.Create(reportStream), DesignerReportType.Page);//Loading the stream to the designer

                            Designer reportDesigner = new Designer();
                            reportDesigner.LoadReport(XmlReader.Create(reportStream), DesignerReportType.Page);//Loading the stream to the designer

                            _pageReport.Dispose();
                            reportStream.Dispose();
                            //reportDesigner.SaveReport(new FileInfo(System.Windows.Forms.Application.StartupPath + "\\" + FileName + reportModel.Id.ToString() + ".rdlx"));
                            reportDesigner.SaveReport(new FileInfo(System.Windows.Forms.Application.StartupPath + "\\" + reportModel.FileName));
                            reportDesigner.IsDirty = false;
                            ////_pageReport.Run();

                            var win = new KontoArDesignerView();
                            //var dc = win.HostedControlElement as KontoArDesigner;
                            //dc.Height = System.Windows.SystemParameters.PrimaryScreenHeight - 100;
                            //dc.Width = System.Windows.SystemParameters.PrimaryScreenWidth - 50;
                            //dc.RepFile = "\\"+ FileName + reportModel.Id.ToString() + ".rdlx";
                            win.endUserDesigner1._reportName =  FileName + reportModel.Id.ToString() + ".rdlx"; 
                            win.endUserDesigner1.reportDesigner.LoadReport(new FileInfo(FileName + reportModel.Id.ToString() + ".rdlx"));
                            win.Text = "Konto Designer - " + FileName + reportModel.Id.ToString() + ".rdlx";
                            //win.ShowDialog();
                        }
                    }
                }
                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage + "... ReOpen the Page for load the Changes!!!!", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                 
            }
        }

        private void gridControl_Click(object sender, EventArgs e)
        {

        }
    }
}