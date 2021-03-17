using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Transaction.Dtos;
using Konto.Shared.Trans.Common;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Weaves.TakaConv
{
    public partial class TakaConvIndex : KontoForm
    {
        public DetailStockDto model=null;
        public TakaConvIndex()
        {
            InitializeComponent();
            this.Load += TakaConvIndex_Load;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.FormClosed += TakaConvIndex_FormClosed;

            this.FirstActiveControl = ProductLookup1;
        }

        private void TakaConvIndex_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage != null)
            {
                var tabcontrol = tabpage.Parent as TabControlAdv;
                if (tabcontrol != null)
                    tabcontrol.TabPages.Remove(tabpage);
            }
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void TakaConvIndex_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.ProductLookup1;
        }

        private void SelectSimpleButton_Click(object sender, EventArgs e)
        {
            if (ProductLookup1.SelectedValue == null) return;
            var proddto = new List<BeamProdDto>();
            var frm = new PendingStockView();
            frm.ItemId = (int)ProductLookup1.SelectedValue;
            frm.StockType = "Stock";
            frm.ProductType =0;
            if (frm.ShowDialog()!= DialogResult.OK) return;
            
           model = frm.list.Where(x => x.IsSelected).FirstOrDefault();
    
            takaNotextEdit.Text = model.VoucherNo;
            mtrstextEdit.Text = model.Qty.ToString();
            wtTextEdit.Text = model.GrossWt.ToString();

        }

        private void okSimpleButton_Click(object sender, EventArgs e)
        {
            if(Convert.ToInt32( productLookup2.SelectedValue) == 0)
            {
                MessageBox.Show("Please Enter New Product Name");
                productLookup2.Focus();
                return;
            }

            if (model == null)
            {
                MessageBox.Show("Please Select A taka to change Quality");
                ProductLookup1.Focus();
                return;

            }

            using(var db = new KontoContext())
            {
                var _tk = db.Prods.FirstOrDefault(x => x.Id == model.Id && x.VTypeId == (int)VoucherTypeEnum.TakaProd);
                if (_tk != null)
                {
                    _tk.ProductId = Convert.ToInt32(productLookup2.SelectedValue);
                    _tk.CProductId = _tk.ProductId;
                   
                    if (designLookup1.SelectedValue != null)
                        _tk.PlyProductId = Convert.ToInt32(designLookup1.SelectedValue);

                    if (colorLookup1.SelectedValue != null)
                        _tk.ColorId = Convert.ToInt32(colorLookup1.SelectedValue);

                    if (gradeLookup1.SelectedValue != null)
                        _tk.GradeId = Convert.ToInt32(gradeLookup1.SelectedValue);

                    db.SaveChanges();
                    model = null;
                    MessageBox.Show("Taka Updated Successfully");
                    ProductLookup1.SetEmpty();
                    productLookup2.SetEmpty();
                    designLookup1.SetEmpty();
                    colorLookup1.SetEmpty();
                    gradeLookup1.SetEmpty();
                    mtrstextEdit.Text = string.Empty;
                    wtTextEdit.Text = string.Empty;
                    takaNotextEdit.Text = string.Empty;
                    ProductLookup1.Focus();
                }
                else
                {
                    model = null;
                    MessageBox.Show("Sekected Taka Not belongs to production");
                    ProductLookup1.Focus();
                    return;
                }
            }
        }
    }
}
