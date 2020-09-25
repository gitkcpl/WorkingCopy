using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.Export;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Core.Shared.Export
{
	class SectionExport
	{
		public string DisplayName { get; set; }
		public string Filter { get; set; }
		public string DefaultFileName { get; set; }
		public IDocumentExportEx Settings { get; set; }
		public void Export(SectionDocument sectionDocument, string filePath)
		{
			Settings.Export(sectionDocument, filePath);
		}
	}
	class PageExport
	{
		public string DisplayName { get; set; }
		public string Filter { get; set; }
		public IRenderingExtension RenderingExtension { get; set; }
		public string DefaultFileName { get; set; }
		public ISettings Settings { get; set; }
	}
}
