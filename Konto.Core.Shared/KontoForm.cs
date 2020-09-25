using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Core.Shared
{
    public partial class KontoForm : MetroForm
    {
        public bool Create_Permission { get; set; }
        public bool Modify_Permission { get; set; }
        public bool Delete_Permission { get; set; }
        public bool View_Permission { get; set; }
        public bool Print_Permission { get; set; }
        public bool Export_Permission { get; set; }
        public string SettingCategroy { get; set; }
        public bool Settings_Permission { get; set; }
        public int EditKey { get; set; }
        public string ReportFilterType { get; set; }
        public KontoForm()
        {
            InitializeComponent();
        }
    }
}
