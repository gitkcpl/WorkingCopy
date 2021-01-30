using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using GrapeCity.ActiveReports;
using Konto.App.Shared;
using Konto.App.Shared.Para;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Data.Models.Masters.Dtos;
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

namespace Konto.Apparel.Inw
{
    public partial class InwardIndex : KontoMetroForm
    {
        private List<BarcodeTransDto> barcodelist = new List<BarcodeTransDto>();
        private List<BarcodeTrans> barlist = new List<BarcodeTrans>();

        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        private int OrderId;
        private int OrderVoucherId;
        private int OrderTransId;
        private decimal OrderQty;
        private BarcodeModel barcode;

        public InwardIndex()
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
                    var _list = tabPageAdv2.Controls[0] as InwardList;
                    _list.ActiveControl = _list.KontoGrid;
                    return;
                }
                if (tabControlAdv1.SelectedIndex == 1)
                {
                    var _ListView = new InwardList();
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
                                 where p.IsActive && !p.IsDeleted
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
            this.Text = "Inward [Add New]";

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
           // else if (string.IsNullOrEmpty(RemarkTextEdit1.Text)) 
            //{
            //    MessageBoxAdv.Show(this, "Please select remark for further process...", "Invalid Remark", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    RemarkTextEdit1.Focus();
            //    return false;
            //}
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
                            TransType = 0,Remarks = RemarkTextEdit1.Text.Trim(),
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
                            Qty = bt.Qty,
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
                            ProductId = bt.ProductId, ProductName = bcm.Product.ProductName, Qty = bt.Qty, TrnasType = 0, VoucherDate = bt.VoucherDate,
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


                    var dv1 = db.Divisions.Find(divid);
                    if (barcode.IsLayer)
                    {
                        
                        if (dv1 != null && dv1.Priority != 1)
                        {
                            MessageBox.Show("Entered Barcode can only issued in Layer/Cutting Division");
                            barcode = null;
                            return;
                        }
                    }
                    else
                    {
                        if(dv1.Priority == 1)
                        {
                            MessageBox.Show("Entered Barcode can not be  issued in Layer/Cutting Division");
                            barcodeNoTextEdit.Text = string.Empty;
                            barcode = null;
                            barcodeNoTextEdit.Focus();
                            e.Handled = true;
                            return;
                        }
                    }

                    //var bcs = db.BarcodeStocks.Where(x => x.BarcodeNo == barcode.BarcodeNo)
                    //                .GroupBy(x => x.DivId)
                    //                .Where(g => g.Sum(p => p.Qty) > 0)
                    //                .Select(g => new {divId= g.Key,Qty= g.Sum(x => x.Qty) });
                    //  join bt in _db.BOMTranses on b.Id equals bt.BOMId

                    var bcs = (from p in db.BarcodeStocks
                               join dv in db.Divisions on p.DivId equals dv.Id
                               where p.BarcodeId == barcode.Id
                               group p by new { dv.Id, dv.DivisionName } into g
                               where g.Sum(x => x.Qty) > 0
                               from p in g
                               select new { id = g.Key.Id, Div = g.Key.DivisionName, Qty = g.Sum(x => x.Qty) }).FirstOrDefault();


                    if (bcs == null)
                    {
                          stockTextEdit.EditValue = barcode.Qty;
                          qtyTextEdit.EditValue = barcode.Qty ;
                         UpdateBarcodeData(barcode);
                        e.Handled = true;
                    }
                    else
                    {

                        MessageBox.Show("Barcode Already Exist in Division : " + bcs.Div);
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
            

            //using (var db = new KontoContext())
            //{
            //    try
            //    {
            //        int divid = Convert.ToInt32(divLookUpEdit.EditValue.ToString());
            //        barlist = db.BarcodeTrans.Where(p => p.DivId == divid && p.BarcodeNo == barcodeNoTextEdit.EditValue).ToList();
            //        int barcodeqty = Convert.ToInt32(barlist.Sum(p => p.Qty));
            //        if (barcodeqty == 0)
            //        {
            //            var bt = new BarcodeTrans();
            //            var selectedbarcode = db.Database.SqlQuery<BarcodeTransDto>("BarcodeSelected").FirstOrDefault(bs => bs.BarcodeNo == barcodeNoTextEdit.EditValue.ToString());
            //            if (selectedbarcode == null)
            //            {
            //                MessageBox.Show("Invalid Barcode");
            //                barcodeNoTextEdit.Focus();
            //                return;
            //            }
            //            else
            //            {
            //                bt.BarcodeNo = selectedbarcode.BarcodeNo;
            //                bt.ProductName = selectedbarcode.ProductName;
            //                bt.OrderId = selectedbarcode.OrderId;
            //                bt.Qty = selectedbarcode.Qty;
            //                bt.Remark = RemarkTextEdit1.EditValue.ToString()=="" ? "" : RemarkTextEdit1.EditValue.ToString();
            //                bt.CreateUser = KontoGlobals.UserName;
            //                bt.CreateDate = DateTime.Now;
            //                bt.EmployeeName = selectedbarcode.EmployeeName;
            //                bt.EmployeeId = selectedbarcode.EmployeeId;
            //                bt.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
            //                bt.CompId = KontoGlobals.CompanyId;
            //                bt.YearId = KontoGlobals.YearId;
            //                bt.IsActive = true;
            //                bt.IsDeleted = false;
            //                bt.DivId = Convert.ToInt32(divLookUpEdit.EditValue);
            //                bt.AccId = selectedbarcode.AccId;
            //                bt.ProductId = selectedbarcode.ProductId;
            //                bt.AccName = selectedbarcode.AccName;
            //                bt.TransType = 3;//(Inward)
            //                barcodelist.Add(bt);
            //                this.barcodeTransModelBindingSource.DataSource = barcodelist;
            //                gridControl1.RefreshDataSource();

            //                db.barcode_trans.Add(bt);
            //                bt = new Barcode_TransModel();
            //                db.SaveChanges();
            //                RemarkTextEdit1.EditValue = "";
            //                barcodeNoTextEdit.EditValue = "";
            //            }
            //        }
            //        else
            //        {

            //            MessageBox.Show("Barcode not in stock please go for next process");
            //            barcodeNoTextEdit.Focus();
            //            return;
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        Log.Error(ex, "Apparel Inward Error");
            //        MessageBoxAdv.Show(this, "Error While Generating Apparel Inward !!", "Exception ", ex.ToString());
            //    }

            //}

        }
    }
}

#endregion