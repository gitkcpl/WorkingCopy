using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using GrapeCity.ActiveReports;
using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Data.Models.Transaction.Dtos;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Konto.Data.Models.Apparel;
using Konto.Data.Models.Apparel.Dtos;
using Konto.Data.Models.Transaction;

namespace Konto.Apparel.Out
{
    public partial class OutwardIndex : KontoMetroForm
    {
        private List<BarcodeTransDto> barcodelist = new List<BarcodeTransDto>();
        private List<BarcodeTrans> barlist = new List<BarcodeTrans>();

        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
      
        private BarcodeModel barcode;

        public OutwardIndex()
        {
            InitializeComponent();
            FillLookup();

            this.Load += StoreIssueIndex_Load;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

          //  this.MainLayoutFile = KontoFileLayout.StoreIssue_Index;
          //  this.GridLayoutFile = KontoFileLayout.StoreIssue_Trans;

            headerEdit.Hide();
            headerEdit.Parent = this.gridControl1;
            headerEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            headerEdit.Leave += new EventHandler(headerEdit_Leave);
            qtyTextEdit.KeyDown += QtyTextEdit_KeyDown;
        }

        private void QtyTextEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
           
            if(barcode==null)
            {
                MessageBox.Show("Barcode Not Found..");
                return;
            }
            UpdateBarcodeData(barcode);
            e.Handled = true;
        }

        #region Grid 
        #region Event
        private void StoreIssueIndex_Load(object sender, EventArgs e)
        {
            try
            {

                
                this.ResetPage();
                NewRec();

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Inward  Load");
                MessageBox.Show(ex.ToString());
            }
        }
        void headerEdit_Leave(object sender, EventArgs e)
        {
            activeCol.Caption = headerEdit.Text;
            headerEdit.Hide();
        }
        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (tabControlAdv1.SelectedIndex == 0)
                {
                    divLookUpEdit.Focus();
                    return;
                }
                if (tabPageAdv2.Controls.Count > 0)
                {
                    var _list = tabPageAdv2.Controls[0] as OutwardList;
                    _list.ActiveControl = _list.KontoGrid;
                    return;
                }
                if (tabControlAdv1.SelectedIndex == 1)
                {
                    var _ListView = new OutwardList();
                    _ListView.Dock = DockStyle.Fill;
                    tabPageAdv2.Controls.Add(_ListView);
                    this.Text = "Inward List [View]";
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        private void FillLookup()
        {
            using (var db = new KontoContext())
            {

                var _divLists = (from p in db.Divisions
                                 where p.IsActive && !p.IsDeleted && p.IsOutward
                                 select new BaseLookupDto()
                                 {
                                     DisplayText = p.DivisionName,
                                     Id = p.Id
                                 }).ToList();

                divLookUpEdit.Properties.DataSource = _divLists;
            }
        }

        public override void NewRec()
        {
            base.NewRec();
            this.Text = "Outward [Add New]";

            divLookUpEdit.EditValue = 1;
            voucherDateEdit.EditValue = DateTime.Now;
          //  empLookup1.SelectedValue = 1;
          //  empLookup1.SetGroup();
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
            this.SITbindingSource.DataSource = new List<GrnTransDto>();

            divLookUpEdit.Focus();
        }
        public override void ResetPage()
        {
            base.ResetPage();

            voucherDateEdit.DateTime = DateTime.Now;
            barcodeNoTextEdit.Text = string.Empty;
            empLookup1.SetEmpty();
            divLookUpEdit.Focus();
            barcodeTransModelBindingSource.DataSource = barcodelist;
        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;
        }
        private bool ValidateData()
        {
            var trans = SITbindingSource.DataSource as List<GrnTransDto>;

            var dt = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
            if (string.IsNullOrEmpty(divLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Division", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                divLookUpEdit.Focus();
                return false;
            }
            else if(Convert.ToInt32(empLookup1.SelectedValue) == 0)
            {
                MessageBox.Show("Please Select Employee Name");
                empLookup1.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(barcodeNoTextEdit.Text))
            {
                MessageBox.Show("Please Enter valid Barcode No.");
                barcodeNoTextEdit.Focus();
                return false;
            }
          
            else if (dt > KontoGlobals.ToDate || dt < KontoGlobals.FromDate)
            {
                MessageBoxAdv.Show(this, "Inward date out of financial range", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherDateEdit.Focus();
                return false;
            }
          
            return true;
        }

        #endregion


        private void UpdateBarcodeData(BarcodeModel bcm)
        {
            int divid = Convert.ToInt32(divLookUpEdit.EditValue.ToString());
            using (var db = new KontoContext())
            {
                using(var tr = db.Database.BeginTransaction())
                {
                    try
                    {
                        var bt = new BarcodeTrans()
                        {
                            BarcodeId = bcm.Id,
                            DivId = divid,
                            EmpId = Convert.ToInt32(empLookup1.SelectedValue),
                            IsActive = true,
                            IsDeleted = false,
                            BarcodeNo = bcm.BarcodeNo,
                            CompId = KontoGlobals.CompanyId,
                            YearId = KontoGlobals.YearId,
                            ProductId = bcm.ProductId,
                            Qty = Convert.ToDecimal(qtyTextEdit.EditValue),
                            TransType = 2,Remarks = RemarkTextEdit1.Text.Trim(),
                            VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd")),
                        };
                        db.BarcodeTrans.Add(bt);
                        db.SaveChanges();

                        var bts = new BarcodeStock()
                        {
                            BarcodeId = Convert.ToInt32(bt.BarcodeId),
                            BarcodeNo = bt.BarcodeNo,
                            CompId = bt.CompId,
                            DivId = bt.DivId,
                            EmpId = bt.EmpId,
                            IsActive = true,
                            IsDeleted = false,
                            Qty = -1*bt.Qty,
                            VoucherDate = bt.VoucherDate,
                            YearId = bt.YearId,
                            ProductId = bt.ProductId,RefId = bt.Id
                        };
                        db.BarcodeStocks.Add(bts);

                        db.SaveChanges();
                        tr.Commit();

                        var btDto = new BarcodeTransDto()
                        {
                            BarcodeId = bt.BarcodeId, BarcodeNo = bt.BarcodeNo, DivId = bt.DivId, DivName = divLookUpEdit.Text,
                            EmpId = bt.EmpId, EmpName = empLookup1.SelectedText,  Id = bt.Id,
                            ProductId = bt.ProductId, ProductName = bcm.Product.ProductName, Qty = bt.Qty, TrnasType = 2, VoucherDate = bt.VoucherDate,
                            Remarks = bt.Remarks
                        };
                        barcodelist.Add(btDto);
                        barcodeTransModelBindingSource.DataSource = barcodelist;
                        gridControl1.RefreshDataSource();
                        barcode = null;
                        barcodeNoTextEdit.Text = string.Empty;
                        barcodeNoTextEdit.Focus();
                        stockTextEdit.Text = string.Empty;
                        qtyTextEdit.Text = string.Empty;

                    }
                    catch (Exception ex)
                    {

                        tr.Rollback();
                        MessageBox.Show(ex.ToString());
                        Log.Error(ex, "barcode Scan Inward");
                    }
                }
            }
                
        }
        private void barcodeNoTextEdit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode != Keys.Return) return;
                if (!ValidateData()) return;

                using (var db = new KontoContext())
                {
                    int divid = Convert.ToInt32(divLookUpEdit.EditValue.ToString());
                    barcode = db.Barcodes.Include("Product").FirstOrDefault(x => x.BarcodeNo == barcodeNoTextEdit.Text.Trim());

                    if (barcode == null)
                    {
                        barcodeNoTextEdit.Text = string.Empty;
                        MessageBox.Show("Barcode no does not exist..");
                        barcodeNoTextEdit.Focus();
                        e.Handled = true;
                        return;
                    }

                    if (barcode.IsLayer)
                    {
                        MessageBox.Show("Layer/Cutting Division Barcode can not out..");
                        barcode = null;
                        barcodeNoTextEdit.Text = string.Empty;
                        barcodeNoTextEdit.Focus();
                        return;
                    }


                    //var bcs = db.BarcodeStocks.Where(x => x.BarcodeNo == barcode.BarcodeNo)
                    //                .GroupBy(x => x.DivId)
                    //                .Where(g => g.Sum(p => p.Qty) > 0)
                    //                .Select(g => new {divId= g.Key,Qty= g.Sum(x => x.Qty) });
                    //  join bt in _db.BOMTranses on b.Id equals bt.BOMId
                    var empid = Convert.ToInt32(empLookup1.SelectedValue);

                    var bcs = (from p in db.BarcodeStocks
                               join dv in db.Divisions on p.DivId equals dv.Id
                               where p.BarcodeId == barcode.Id && dv.IsOutward && p.EmpId == empid && p.DivId == divid
                               group p by p.BarcodeId into g
                               select new { sum = g.Sum(x => x.Qty) }).FirstOrDefault();

                    if (bcs != null && bcs.sum==1)
                    {
                        stockTextEdit.EditValue = barcode.Qty;
                         qtyTextEdit.EditValue = barcode.Qty ;
                        UpdateBarcodeData(barcode);
                        e.Handled = true;
                    }
                    else
                    {

                        MessageBox.Show("Selected Employee/Divisoin does not have stock");
                        barcodeNoTextEdit.Text = string.Empty;
                        barcodeNoTextEdit.Focus();
                        e.Handled = true;
                        barcode = null;
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                Log.Error(ex, "barcode scan inward");
            }
            

           

        }
    }
}

#endregion