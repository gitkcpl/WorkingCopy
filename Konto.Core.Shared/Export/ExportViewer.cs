using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.Export;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using GrapeCity.ActiveReports.Extensibility.Rendering.IO;
using GrapeCity.ActiveReports.Viewer.Win;
using GrapeCity.ActiveReports.Viewer.Win.Internal.Export;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Core.Shared.Export
{
    internal class ExportViewer : IExportViewer
    {
        private readonly Viewer _viewer;
        public bool CanExport
        {
            get
            {
                return this._viewer.CanExport;
            }
        }

        public SectionDocument Document
        {
            get
            {
                return this._viewer.Document;
            }
        }

        public IWin32Window Owner
        {
            get
            {
                return this._viewer;
            }
        }

        public ExportViewer(Viewer viewer)
        {
            this._viewer = viewer;
        }

        public void Export(IDocumentExportEx export, FileInfo file)
        {
            this._viewer.Export(export, file);
        }

        public void Export(IDocumentExportEx export, FileStream stream)
        {
            this._viewer.Export(export, stream);
        }

        public void HandleError(Exception exception)
        {
            this._viewer.HandleError(exception);
        }

        public void Render(IRenderingExtension export, StreamProvider streamProvider, NameValueCollection settings)
        {
            this._viewer.Render(export, streamProvider, settings);
        }
    }
}
