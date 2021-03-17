using DevExpress.XtraGrid.Views.Grid;
using GrapeCity.ActiveReports;
using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Data.Models.Apparel.Dtos;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Transaction;
using Serilog;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Apparel.BC
{
    public partial class BarcodeIndex : KontoMetroForm
    {
        private List<ProductModel> FilterView = new List<ProductModel>();
        private BindingList<PImageModel> _trans = new BindingList<PImageModel>();
        private List<PImageModel> _delImg = new List<PImageModel>();

        private BarcodeModel _bc;

        public BarcodeIndex()
        {
            InitializeComponent();
            tabControlAdv1.TabPages[2].TabVisible = false;
            tabControlAdv1.TabPages[3].TabVisible = false;

            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;
            barcodeTextEdit.KeyDown += BarcodeTextEdit_KeyDown;
            barcodeTextEdit.ButtonClick += BarcodeTextEdit_ButtonClick;

          //  this.MainLayoutFile = KontoFileLayout.Barcode_Index_Layout;

            //FillLookup();
        }

        private void BarcodeTextEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            GetBarcodeData();
        }

        private void BarcodeTextEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            GetBarcodeData();
        }


        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                return;
            }
            else if (tabControlAdv1.SelectedIndex == 1)
            {
                if (tabPageAdv2.Controls.Count > 0)
                {
                    var _list = tabPageAdv2.Controls[0] as BarcodeListView;
                    _list.ActiveControl = _list.KontoGrid;
                    this.Text = "Barcode List [View]";
                    return;
                }
                var _ListView = new BarcodeListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Barcode List [View]";

            }
        }

        private void GridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
            {
                if (MessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo) !=
                  DialogResult.Yes)
                    return;
                GridView view = sender as GridView;
                var row = view.GetRow(view.FocusedRowHandle) as PImageModel;
                view.DeleteRow(view.FocusedRowHandle);
                _delImg.Add(row);
            }

        }

        private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            view.SetRowCellValue(e.RowHandle, view.Columns["Img"], null);
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveDataAsync(true);
               // this.ResetPage();
            }
            catch (Exception ex)
            {

                Log.Error(ex, "product Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

       
        private void BranchIndex_Load(object sender, EventArgs e)
        {
            try
            {
                NewRec();
                if (this.PrimaryKey == 0)
                {
                }

            }
            catch (Exception ex)
            {

                Log.Error(ex, "product Load");
                MessageBox.Show(ex.ToString());
            }
        }

        public override void ResetPage()
        {
            base.ResetPage();
            barcodeTextEdit.Text = string.Empty;
            partyLabelControl.Text = string.Empty;
            productLabelControl.Text = string.Empty;
            qtySpinEdit.Value = 0;
            stockLabelControl.Text = string.Empty;

        }

        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var model = db.Products.Find(_key);
                LoadData(model);
            }

        }

        private void LoadData(ProductModel model)
        {
           
            //codeTextBoxExt.Focus();
        }


        public override void FirstRec()
        {
            base.FirstRec();
            var model = FilterView[RecordNo];
            LoadData(model);
        }
        public override void NextRec()
        {
            base.NextRec();

            LoadData(FilterView[this.RecordNo]);

        }
        public override void PrevRec()
        {
            base.PrevRec();

            LoadData(FilterView[this.RecordNo]);
        }
        public override void LastRec()
        {
            base.LastRec();
            LoadData(FilterView[this.RecordNo]);
        }

        public override void SaveDataAsync(bool newmode)
        {
            try
            {

                if (_bc == null) return;
                if (qtySpinEdit.Value == 0) return;

                var st = Convert.ToDecimal(stockLabelControl.Text);

                if(st-qtySpinEdit.Value < 0)
                {
                    MessageBox.Show("Can not generate Barcode For Extra Qty");
                    return;
                }
                
                using(var db =new KontoContext())
                {
                    
                    var repid = db.Barcodes.DefaultIfEmpty().Max(x => x == null ? 0 : x.ReportId) + 1;
                    var barcode = Convert.ToInt32( GetNextBarcodeNo(db));
                    for (int i = 0; i < qtySpinEdit.Value; i++)
                    {
                        var bm = new BarcodeModel()
                        {
                            CompId = KontoGlobals.CompanyId,
                            IsActive = true,
                            IsDeleted = false,
                            OrderTransId = _bc.OrderTransId,
                            PcsNo = 1,
                            AccId = _bc.AccId,
                            Qty = 1,
                            ProductId = _bc.ProductId,
                            ReportId = repid,
                            RefBarcodeId= _bc.Id
                        };
                        if (i == 0)
                            bm.BarcodeNo = barcode.ToString();
                        else
                        {
                            barcode = barcode + 1;
                            bm.BarcodeNo = barcode.ToString();
                        }//String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000);  //KontoUtils.GetUniqueKey(8)).tos;

                        db.Barcodes.Add(bm);
                    }

                    db.SaveChanges();
                    _bc = null;

                    PageReport rpt = new PageReport();

                    rpt.Load(new FileInfo("reg\\doc\\Outwardbarcode.rdlx"));

                    rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

                    GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);

                    doc.Parameters["Party"].CurrentValue = partyLabelControl.Text;
                    doc.Parameters["ReportId"].CurrentValue = repid;
                    doc.Parameters["BarcodeNo"].CurrentValue = "0";
                    var frm = new KontoRepViewer(doc);
                    frm.Text = "Barcode";
                    var _tab = this.Parent.Parent as TabControlAdv;
                    if (_tab == null) return;
                    var pg1 = new TabPageAdv();
                    pg1.Text = "Barcode Print";
                    _tab.TabPages.Add(pg1);
                    _tab.SelectedTab = pg1;
                    frm.TopLevel = false;
                    frm.Parent = pg1;
                    frm.Location = new System.Drawing.Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                    frm.Show();// = true;
                }


                this.ResetPage();
                barcodeTextEdit.Focus();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Barcode Save");
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnget_Click(object sender, EventArgs e)
        {
            GetBarcodeData();  
        }

        private void GetBarcodeData()
        {
            try
            {
                
                if (string.IsNullOrEmpty(barcodeTextEdit.Text.Trim()))
                {
                    MessageBox.Show("Invalid Barcode");
                    barcodeTextEdit.Focus();
                    return;
                }
                using (var _db = new KontoContext())
                {
                    var bd = new BarcodeTransDto();


                    _bc = _db.Barcodes.Include("Product").Include("Acc").FirstOrDefault(p => p.BarcodeNo == barcodeTextEdit.Text);
                    if (_bc == null)
                    {
                        MessageBox.Show("Invalid Barcode..");
                        ResetPage();
                        barcodeTextEdit.Focus();
                        return;
                    }


                    //check for layer qc entry
                    var btdata = _db.BarcodeTrans.FirstOrDefault(p => p.BarcodeId == _bc.Id && p.TransType == 1 && p.QcPassed);

                    if (btdata == null)
                    {
                        MessageBox.Show("Barcode not found in layer QC");
                        ResetPage();
                        barcodeTextEdit.Focus();
                        _bc = null;
                        return;
                    }

                    // check for generated Barcode...
                    var sum = _db.Barcodes.Where(x => x.RefBarcodeId == _bc.Id).Count();

                    if (sum == _bc.Qty)
                    {
                        MessageBox.Show("Barcode Already Generated");
                        ResetPage();
                        barcodeTextEdit.Focus();
                        _bc = null;
                        return;
                    }


                    qtySpinEdit.Value = _bc.Qty - sum;

                    if (_bc.Acc != null)
                        partyLabelControl.Text = _bc.Acc.AccName;

                    productLabelControl.Text = _bc.Product.ProductName;
                    stockLabelControl.Text = (_bc.Qty - sum).ToString();
                    qtySpinEdit.Focus();

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Barcode find");
                MessageBox.Show(ex.ToString());
            }
        }

        private string GetNextBarcodeNo(KontoContext db)
        {
            var lstBarcode = db.Barcodes.OrderByDescending(x => x.Id).FirstOrDefault();
            if (lstBarcode == null || lstBarcode.BarcodeNo == null)
            {
                return "10000";
            }
            var invoiceNumberPostfix = Convert.ToInt32(lstBarcode.BarcodeNo) + 1;
            return invoiceNumberPostfix.ToString();
        }
    }
}
