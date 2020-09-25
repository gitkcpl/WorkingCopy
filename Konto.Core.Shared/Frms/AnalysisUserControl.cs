using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Konto.App.Shared;
using System.Xml;
using DevExpress.XtraEditors;

namespace Konto.Core.Shared.Frms
{
    public partial class AnalysisUserControl : UserControl
    {
        public DataTable AnaDataTable { get; set; }
        public VoucherTypeEnum VoucherType { get; set; }
        public bool IsAnalysis { get; set; }

        public AnalysisUserControl(VoucherTypeEnum vtype, bool isana)
        {
           
            InitializeComponent();
            this.listDateRange1.VoucherType = vtype;
            this.listDateRange1.IsAnalysis = isana;
            this.Load += AnalysisUserControl_Load;
            this.listDateRange1.GetButtonClick += ListDateRange1_GetButtonClick;

        }

        private void ListDateRange1_GetButtonClick(object sender, EventArgs e)
        {
            try
            {
                c1FlexPivotPage1.FlexPivotEngine.BeginUpdate();
                //set predefined view
                string xmlString = System.IO.File.ReadAllText(listDateRange1.SelectedItem.LayoutFile);
                // XmlDocument views = new XmlDocument();
                //  views.LoadXml(listDateRange1.SelectedItem.LayoutFile);
                //XmlNode nd = views.SelectSingleNode(string.Format("FlexPivotViews/C1FlexPivot[@id='{0}']", comboBox1.SelectedItem));
                c1FlexPivotPage1.FlexPivotPanel.ViewDefinition = xmlString;
                c1FlexPivotPage1.FlexPivotEngine.EndUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                
            }
          

        }

        public AnalysisUserControl()
        {
            InitializeComponent();
            this.Load += AnalysisUserControl_Load;
        }

        private void AnalysisUserControl_Load(object sender, EventArgs e)
        {
            
            this.c1FlexPivotPage1.DataSource = this.AnaDataTable;
            var btn = listDateRange1.Controls["getSimpleButton"] as SimpleButton;

            btn.PerformClick();
        }

        private void c1FlexPivotPage1_Load(object sender, EventArgs e)
        {

        }
    }
}
