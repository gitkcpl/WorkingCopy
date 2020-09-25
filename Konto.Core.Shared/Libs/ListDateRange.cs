using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Konto.App.Shared;
using DevExpress.XtraGrid;
using Konto.Data;
using Konto.Data.Models.Admin;

namespace Konto.Core.Shared.Libs
{
    public partial class ListDateRange : UserControl
    {
        public int FromDate { get; set; }
        public int ToDate { get; set; }
        public VoucherTypeEnum VoucherType { get; set; }
        public ListPageModel SelectedItem { get; set; }
        public GridControl KontoGrid { get; set; }

        public bool IsAnalysis { get; set; }

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler GetButtonClick;
        private List<ListPageModel> listPageModels = new List<ListPageModel>();
        public ListDateRange()
        {
            InitializeComponent();
            
            this.Load += ListDateRange_Load;

        }
       
        private void ListDateRange_Load(object sender, EventArgs e)
        {
            if (this.DesignMode) return;
            FdateEdit1.DateTime = DateTime.ParseExact(KontoGlobals.FromDate.ToString(), "yyyyMMdd",
                                 System.Globalization.CultureInfo.CurrentCulture);
            TdateEdit2.DateTime = DateTime.ParseExact(KontoGlobals.ToDate.ToString(), "yyyyMMdd",
                      System.Globalization.CultureInfo.CurrentCulture);

            using (KontoContext db = new KontoContext())
            {
                if(!IsAnalysis)
                    listPageModels = db.ListPages.Where(k => k.VTypeId == (int)this.VoucherType).ToList();
                else
                    listPageModels = db.ListPages.Where(k => k.VTypeId == (int)this.VoucherType && k.Extra1=="analysis").ToList();

                lookUpEdit1.Properties.DataSource = listPageModels;
            }


            if (listPageModels.Count > 0)
                lookUpEdit1.EditValue = listPageModels[0].Id;
        }

        private void getSimpleButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.lookUpEdit1.Text))
            {
                MessageBox.Show("Plese select a View Type");
                lookUpEdit1.Focus();
            }
            this.FromDate = Convert.ToInt32(FdateEdit1.DateTime.ToString("yyyyMMdd"));
            this.ToDate = Convert.ToInt32(TdateEdit2.DateTime.ToString("yyyyMMdd"));
            this.SelectedItem = lookUpEdit1.Properties.GetDataSourceRowByKeyValue(lookUpEdit1.EditValue) as ListPageModel;
            this.GetButtonClick?.Invoke(this, e);
        }
    }
}
