using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using GrapeCity.ActiveReports;
using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Konto.Data.Models.Apparel.Dtos;
using Konto.Data.Models.Apparel;

namespace Konto.Apparel.Qc
{
    public partial class QualityControlIndex : KontoMetroForm
    {
        private List<BarcodeTransDto> barcodelist = new List<BarcodeTransDto>();
       // private List<Barcode_TransModel> barlist = new List<Barcode_TransModel>();

        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;

        private BarcodeModel barcode;
        public QualityControlIndex()
        {
            InitializeComponent();
            FillLookup();

            this.Load += StoreIssueIndex_Load;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;
            okSimpleButton.Enabled = false;
            saveSimpleButton.Click += SaveSimpleButton_Click;
           // this.MainLayoutFile = KontoFileLayout.StoreIssue_Index;
           // this.GridLayoutFile = KontoFileLayout.StoreIssue_Trans;

            
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("NA", "NA"),
                new ComboBoxPairs("Cut piece in Over Lock", "Cut piece In Over Lock"),
                new ComboBoxPairs("Cut Piece in", "Cut Piece in"),
                new ComboBoxPairs("Wrong Neck", "Wrong Neck"),
                new ComboBoxPairs("Diff. size", "Diff. size"),
                new ComboBoxPairs("Stain Oil", "Stain Oil"),
                new ComboBoxPairs("Stain Dirt", "Stain Dirt"),
                new ComboBoxPairs("Stain Food", "Stain Food"),
                new ComboBoxPairs("Other", "Other"),
                new ComboBoxPairs("OPEN SEAM", "OPEN SEAM"),
                new ComboBoxPairs("RUN OF STICTH/ UTHRA HUA SILAI", "RUN OF STICTH/ UTHRA HUA SILAI"),
                new ComboBoxPairs("MEASUREMENT( ALL )", "MEASUREMENT( ALL )"),
                new ComboBoxPairs("BARCODE & DESIGN NOT MATCHING", "BARCODE & DESIGN NOT MATCHING"),
                new ComboBoxPairs("SIZE LABAL", "SIZE LABAL"),
                new ComboBoxPairs("BARCODE  AND SIZE LABEL MATCHING", "BARCODE  AND SIZE LABEL MATCHING"),
                new ComboBoxPairs("INSIDE  STITCHING  AND OVER LOCK", "INSIDE  STITCHING  AND OVER LOCK"),
                new ComboBoxPairs("NECK FINISHING", "NECK FINISHING"),
                new ComboBoxPairs("DAMAGE/ CUT /HOLE", "DAMAGE/ CUT /HOLE"),
                new ComboBoxPairs("STITCH PER INCH ", "STITCH PER INCH "),
                new ComboBoxPairs("WRONG CUTTING STITCHING", "WRONG CUTTING STITCHING"),
                new ComboBoxPairs("WRONG DESIGN", "WRONG DESIGN"),
                new ComboBoxPairs("SLANTED/ TEDA SILAI", "SLANTED/ TEDA SILAI"),
                new ComboBoxPairs("UNEVEN FOLD MARGIN", "UNEVEN FOLD MARGIN"),
                new ComboBoxPairs("BOTTOM UP DOWN", "BOTTOM UP DOWN"),
                new ComboBoxPairs("STAIN", "STAIN"),
                new ComboBoxPairs("THREADS/DHAGA", "THREADS/DHAGA"),
                new ComboBoxPairs("MISPRINT", "MISPRINT"),
                new ComboBoxPairs("ALL MEASUREMENT", "ALL MEASUREMENT"),
                new ComboBoxPairs("DAMAGE", "DAMAGE"),
                new ComboBoxPairs("BARCODE AND SIZE LABAL MATCHING", "BARCODE AND SIZE LABAL MATCHING"),
                new ComboBoxPairs("POOR JOINTING STICH", "POOR JOINTING STICH"),
                new ComboBoxPairs("PUCKERING/ WRINKLE", "PUCKERING/ WRINKLE"),
                new ComboBoxPairs("GATHERING", "GATHERING"),
                new ComboBoxPairs("PLEATING ", "PLEATING "),
                new ComboBoxPairs("LOOSE STITCH", "LOOSE STITCH "),
                new ComboBoxPairs("RAW EDGE", "RAW EDGE"),
                new ComboBoxPairs("TANKI", "TANKI"),
            };
            remarkLookUpEdit.Properties.DataSource = cbp;
            
            headerEdit.Hide();
            headerEdit.Parent = this.gridControl1;
            headerEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            headerEdit.Leave += new EventHandler(headerEdit_Leave);
        }

        private void SaveSimpleButton_Click(object sender, EventArgs e)
        {
            if (!ValidateData()) return;

            if (Convert.ToBoolean(radioGroup1.EditValue) == false && string.IsNullOrEmpty(remarkLookUpEdit.Text))
            {
                MessageBox.Show("Please Select Remark.");
                remarkLookUpEdit.Focus();
                return;
            }

            if (barcode == null) return;

            int divid = Convert.ToInt32(divLookUpEdit.EditValue.ToString());

            using (var db = new KontoContext())
            {
                using (var tr = db.Database.BeginTransaction())
                {
                    try
                    {
                        var bt = new BarcodeTrans()
                        {
                            BarcodeId = barcode.Id,
                            DivId = divid,
                            EmpId = Convert.ToInt32(empLookup1.SelectedValue),
                            IsActive = true,
                            IsDeleted = false,
                            BarcodeNo = barcode.BarcodeNo,
                            CompId = KontoGlobals.CompanyId,
                            YearId = KontoGlobals.YearId,
                            ProductId = barcode.ProductId,
                            Qty = barcode.Qty,
                            TransType = 1,
                            Remarks = remarkLookUpEdit.Text,
                            VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd")),
                            QcPassed = Convert.ToBoolean(radioGroup1.EditValue)
                        };
                        db.BarcodeTrans.Add(bt);
                        db.SaveChanges();

                        if (bt.QcPassed && Convert.ToBoolean(divLookUpEdit.GetColumnValue("IsQcOut")))
                        {
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
                                ProductId = bt.ProductId,
                                RefId = bt.Id
                            };
                            db.BarcodeStocks.Add(bts);

                            db.SaveChanges();
                        }

                        tr.Commit();
                        var btDto = new BarcodeTransDto()
                        {
                            BarcodeId = bt.BarcodeId,
                            BarcodeNo = bt.BarcodeNo,
                            DivId = bt.DivId,
                            DivName = divLookUpEdit.Text,
                            EmpId = bt.EmpId,
                            EmpName = empLookup1.SelectedText,
                            Id = bt.Id,
                            ProductId = bt.ProductId,
                            ProductName = barcode.Product.ProductName,
                            Qty = bt.Qty,
                            TrnasType = 1,
                            QcPassed = bt.QcPassed,
                            Remarks = bt.Remarks
                        };

                        barcodelist.Add(btDto);
                        barcodeTransModelBindingSource.DataSource = barcodelist;
                        gridControl1.RefreshDataSource();
                        barcode = null;
                        remarkLookUpEdit.EditValue = "NA";
                        barcodeNoTextEdit.Text = string.Empty;
                        barcodeNoTextEdit.Focus();
                       
                    }
                    catch (Exception ex)
                    {
                        tr.Rollback();
                        Log.Error(ex, "qc saved");
                    }
                }

            }
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
                Log.Error(ex, "Quality Control Load");
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
                    var _list = tabPageAdv2.Controls[0] as QualityControlList;
                    _list.ActiveControl = _list.KontoGrid;
                    return;
                }
                if (tabControlAdv1.SelectedIndex == 1)
                {
                    var _ListView = new QualityControlList();
                    _ListView.Dock = DockStyle.Fill;
                    tabPageAdv2.Controls.Add(_ListView);
                    this.Text = "Quality Control List [View]";
                }
            }
            catch (Exception ex)
            {

                
            }
        }


        private void FillLookup()
        {
            using (var db = new KontoContext())
            {


                var _divLists = (from p in db.Divisions
                                 where p.IsActive && !p.IsDeleted && (p.IsQc || p.IsQcOut)
                                 select p).ToList();

                divLookUpEdit.Properties.DataSource = _divLists;
            }
        }
        public override void NewRec()
        {
            base.NewRec();
            this.Text = "Quality Control [Add New]";

            divLookUpEdit.EditValue = 1;
            voucherDateEdit.EditValue = DateTime.Now;
            //empLookup1.SelectedValue = 1;
           // empLookup1.SetGroup();
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
        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;
        }
        private bool ValidateData()
        {
            

            var dt = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
            if (string.IsNullOrEmpty(divLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Division", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                divLookUpEdit.Focus();
                return false;
            }
            else if (dt > KontoGlobals.ToDate || dt < KontoGlobals.FromDate)
            {
                MessageBoxAdv.Show(this, "Challan date out of financial range", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherDateEdit.Focus();
                return false;
            }

            else if (Convert.ToInt32(empLookup1.SelectedValue) == 0)
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

            return true;
        }

        #endregion

        private void barcodeNoTextEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return) return;

            if (!ValidateData()) return;



            using(var db = new KontoContext())
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

                

                // check for already Qc
                var existqc = db.BarcodeTrans.Any(x => x.BarcodeId == barcode.Id && x.DivId == divid && x.TransType == 1 && x.QcPassed);
                if(existqc)
                {
                    MessageBox.Show("Already Qc Done For This barcode ?");
                    
                        barcodeNoTextEdit.Text = string.Empty;
                        barcodeNoTextEdit.Focus();
                        e.Handled = true;
                    barcode = null;
                        return;
                }

                //check for inward in selected division
                var sum = db.BarcodeStocks.Where(x => x.BarcodeId == barcode.Id && x.DivId == divid)
                   .DefaultIfEmpty()
                   .Sum(x => x == null ? 0 : x.Qty);

                if (sum == 0)
                {
                    MessageBox.Show("Barcode does Not Exist in Selected Division");
                    barcodeNoTextEdit.Text = string.Empty;
                    barcodeNoTextEdit.Focus();
                    e.Handled = true;
                    barcode = null;
                    return;
                }


                divLookUpEdit.Enabled = false;
                productNameLabel.Text = barcode.Product.ProductName;


            }


            //using (var db = new KontoContext())
            //{
            //    try
            //    {
            //        var frm = new ImageWindowFrm();
            //        var selectedbarcode = db.Database.SqlQuery<BarcodeTransDto>("BarcodeSelected").FirstOrDefault(bs => bs.BarcodeNo == barcodeNoTextEdit.EditValue.ToString());
            //        frm.pimagelist = db.PImagies.Where(p => p.ProductId == selectedbarcode.ProductId).ToList();
            //        frm.ShowDialog();
            //        int divid = Convert.ToInt32(divLookUpEdit.EditValue.ToString());
            //        barlist = db.barcode_trans.Where(p => p.DivId == divid && p.BarcodeNo == barcodeNoTextEdit.EditValue).ToList();
            //        int barcodeqty = Convert.ToInt32(barlist.Sum(p => p.Qty));
            //        if (barcodeqty == 0)
            //        {
            //            var bt = new Barcode_TransModel();
            //            //var selectedbarcode = db.Database.SqlQuery<BarcodeTransDto>("BarcodeSelected trans={0},Div={1}", 0, divLookUpEdit.EditValue).FirstOrDefault(bs => bs.BarcodeNo == barcodeNoTextEdit.EditValue.ToString());
            //            //var selectedbarcode = db.Database.SqlQuery<BarcodeTransDto>("BarcodeSelected").FirstOrDefault(bs => bs.BarcodeNo == barcodeNoTextEdit.EditValue.ToString());
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
            //                //bt.Remark = remarkLookUpEdit.EditValue.ToString();
            //                if (frm._responce == false)
            //                {
            //                    bt.IsPass = "No";
            //                }
            //                else if (frm._responce == true)
            //                {
            //                    bt.IsPass = "Yes";
            //                }
            //                else
            //                {
            //                    bt.IsPass = ispassLookUpEdit1.Text;
            //                }

            //                if (frm.remark != "")
            //                {
            //                    bt.Remark = frm.remark;
            //                }
            //                else
            //                {
            //                    bt.Remark = remarkLookUpEdit.EditValue.ToString();
            //                }
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
                         
            //                bt.ProductId = selectedbarcode.ProductId;
            //                bt.AccName = selectedbarcode.AccName;
            //                bt.TransType = 1;//(Inward)
            //                barcodelist.Add(bt);
            //                this.barcodeTransModelBindingSource.DataSource = barcodelist;
            //                gridControl1.RefreshDataSource();

            //                db.barcode_trans.Add(bt);
            //                bt = new Barcode_TransModel();
            //                db.SaveChanges();
            //                remarkLookUpEdit.EditValue = "";
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