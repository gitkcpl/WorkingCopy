using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Konto.Core.Shared.Libs;
using System;
using System.Windows.Forms;

namespace Konto.Core.Shared.Frms
{
    public partial class GridPropertView : KontoForm
    {
        public string LayoutFileName { get; set; }
        public GridControl MyGrid { get; set; }
        public GridView MyView { get; set; }
        public GridPropertView()
        {
            InitializeComponent();
            this.Load += GridPropertView_Load;
            this.gridView1.FocusedColumnChanged += MyView_FocusedColumnChanged;
            
        }

        private void MyView_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            if (!checkEdit1.Checked)
            {
                propertyGridControl1.SelectedObject = gridView1.FocusedColumn;
            }
        }

        private void GridPropertView_Load(object sender, EventArgs e)
        {
            
            
            propertyGridControl1.SelectedObject = gridView1;
            propertyGridControl1.RetrieveFields();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if(!checkEdit1.Checked)
            {
                propertyGridControl1.SelectedObject = gridView1.FocusedColumn;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
          //  this.MyView.Assign(this.gridView1, false);
            this.DialogResult = DialogResult.OK;
            
            this.Close();
        }
    }
}
