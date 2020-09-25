using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Extensibility;
using Konto.App.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Shared
{
    //internal sealed class MySubreportLocator : ResourceLocator
    //{
    //    private const string UriSchemeMySubreports = "MySubreport:";
    //    /// <summary>
    //    /// Obtain and return the resource. 
    //    /// </summary>
    //    /// <param name="resourceInfo">The information about the resource to be obtained. </param>
    //    /// <returns>The resource, null if it was not found. </returns>
    //    public override Resource GetResource(ResourceInfo resourceInfo)
    //    {
    //        Resource resource;
    //        try
    //        {
               
    //            string name = resourceInfo.Name;
    //            if (string.IsNullOrEmpty(name)) return resource;

    //            Uri uri = new Uri(new System.IO.FileInfo("reg//doc//" + name).FullName);
    //            //if (uri.GetLeftPart(UriPartial.Scheme).StartsWith(UriSchemeMySubreports, true, CultureInfo.InvariantCulture))
    //            if (!string.IsNullOrEmpty(name))
    //            {
    //                //PageReport pr = new PageReport(new System.IO.FileInfo(name.Replace(UriSchemeMySubreports, "").Trim()));
    //                PageReport pr = new PageReport(new System.IO.FileInfo("reg//doc//" + name));
    //                pr.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;
    //                //pr.Report.DataSources[0].ConnectionProperties.ConnectString = @"data source=C:\Users\stduser\Documents\ComponentOne Samples\ActiveReports 8\Data\reels.MDB;provider=Microsoft.Jet.OLEDB.4.0;";

    //                System.IO.MemoryStream memory_stream = new System.IO.MemoryStream();
    //                using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(memory_stream))
    //                {
    //                    pr.Save(writer);
    //                }
    //                memory_stream.Position = 0;
    //                resource = new Resource(memory_stream, uri);
    //            }
    //            else
    //            {

    //            }
    //            return resource;
    //        }
    //        catch (Exception ex)
    //        {
    //            return new resourceInfo;

    //        }
            
    //    }
    //}
}