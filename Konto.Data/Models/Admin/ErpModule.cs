
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Admin
{
    [Table("ErpModule")]
    public class ErpModule : BaseEntity
    {
        
        [Display(Name = "Parent Id")]
        public int? ParentId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Module Desc")]
        public string ModuleDesc { get; set; }

        [Display(Name = "Order Index")]
        public int? OrderIndex { get; set; }

        [MaxLength(50)]
        [Display(Name = "Link Button")]
        public string LinkButton { get; set; }

        [MaxLength(25)]
        [Display(Name = "Short Cut Key")]
        public string ShortCutKey { get; set; }

        [Display(Name = "Package Id")]
        public int? PackageId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Default Report")]
        public string DefaultReport { get; set; }

        [MaxLength(50)]
        [Display(Name = "Default Layout")]
        public string DefaultLayout { get; set; }

        [MaxLength(50)]
        [Display(Name = "Table Name")]
        public string TableName { get; set; }

        [MaxLength(75)]
        [Display(Name = "Assembly Name")]
        public string AssemblyName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Main Assembly")]
        public string MainAssembly { get; set; }

        [MaxLength(75)]
        [Display(Name = "List Assembly")]
        public string ListAssembly { get; set; }

        [Display(Name = "MDI")]
        public bool? MDI { get; set; }

        [MaxLength(50)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Visible")]
        public bool Visible { get; set; }

        [MaxLength(50)]
        [Display(Name = "Icon Path")]
        public string IconPath { get; set; }

        [Display(Name = "Check Right")]
        public bool? CheckRight { get; set; }

        [Display(Name = "Visible On Dash Board")]
        public bool? VisibleOnDashBoard { get; set; }

        [Display(Name = "Visible On Side Bar")]
        public bool? VisibleOnSideBar { get; set; }

        [Display(Name = "Is Seprator")]
        public bool IsSeprator { get; set; }

        [Display(Name = "Offline")]
        public bool Offline { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }

        public int? ImageIndex { get; set; }
        
    }

}
