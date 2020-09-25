using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.PageReportModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Konto.Core.Shared.Export
{
    internal static class ViewerHelper
    {
        public static GrapeCity.ActiveReports.Viewer.Win.Internal.Export.ExportForm.ReportType DetermineReportType(FileInfo fileName)
        {
            // GrapeCity.ActiveReports.Export.Excel.Section.XlsExport
            GrapeCity.ActiveReports.Viewer.Win.Internal.Export.ExportForm.ReportType reportType;
            string lowerInvariant = Path.GetExtension(fileName.FullName).ToLowerInvariant();
            if (lowerInvariant == ".json" || lowerInvariant == ".bson")
            {
                return GrapeCity.ActiveReports.Viewer.Win.Internal.Export.ExportForm.ReportType.PageCpl;
            }
            if (lowerInvariant == ".rdf" || lowerInvariant == ".rpx")
            {
                return GrapeCity.ActiveReports.Viewer.Win.Internal.Export.ExportForm.ReportType.Section;
            }
            try
            {
                PageReport pageReport = new PageReport(fileName);
                if (pageReport == null || pageReport.Report == null || pageReport.Report.Body == null)
                {
                    return GrapeCity.ActiveReports.Viewer.Win.Internal.Export.ExportForm.ReportType.Section;
                }
                ReportItemCollection reportItems = pageReport.Report.Body.ReportItems;
                if (reportItems != null && reportItems.Count == 1 && reportItems[0] is FixedPage)
                {
                    return GrapeCity.ActiveReports.Viewer.Win.Internal.Export.ExportForm.ReportType.PageFpl;
                }
                return GrapeCity.ActiveReports.Viewer.Win.Internal.Export.ExportForm.ReportType.PageCpl;
            }
            catch (ReportException reportException)
            {
                reportType = GrapeCity.ActiveReports.Viewer.Win.Internal.Export.ExportForm.ReportType.Section;
            }
            catch (XmlException xmlException)
            {
                reportType = GrapeCity.ActiveReports.Viewer.Win.Internal.Export.ExportForm.ReportType.Section;
            }
            return reportType;
        }

        public static string GetReportServerUri(FileInfo file)
        {
            XAttribute xAttribute;
            string str = File.ReadAllText(file.FullName);
            XDocument xDocument = XDocument.Load(new StringReader(str));
            if (xDocument.Root == null)
            {
                return string.Empty;
            }
            XNamespace defaultNamespace = xDocument.Root.GetDefaultNamespace();
            XmlNamespaceManager xmlNamespaceManagers = new XmlNamespaceManager(new NameTable());
            xmlNamespaceManagers.AddNamespace("ns", defaultNamespace.NamespaceName);
            XElement xElement = xDocument.XPathSelectElement("/ns:Report/ns:CustomProperties/ns:CustomProperty[ns:Name[text()='ReportServerUri']]/ns:Value", xmlNamespaceManagers);
            if (xElement != null)
            {
                return xElement.Value;
            }
            xElement = xDocument.XPathSelectElement("/ns:ActiveReportsLayout", xmlNamespaceManagers);
            if (xElement != null)
            {
                xAttribute = xElement.Attribute("ReportServerUri");
            }
            else
            {
                xAttribute = null;
            }
            XAttribute xAttribute1 = xAttribute;
            if (xAttribute1 == null)
            {
                return string.Empty;
            }
            return xAttribute1.Value;
        }

        public static bool isRdf(FileInfo fileName)
        {
            return Path.GetExtension(fileName.FullName) == ".rdf";
        }
    }
}
