using AutoMapper;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using GrapeCity.ActiveReports;
using Konto.App.Shared;
using Konto.App.Shared.Para;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters.Dtos;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using Konto.Shared.Masters.Item;
using Serilog;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ExpressionBuilder = Konto.Core.Shared.Libs.ExpressionBuilder;

namespace Konto.Shared.Account.DRCRNote
{
    public partial class DRCRNoteIndex : KontoMetroForm
    {
        private List<BillModel> FilterView = new List<BillModel>();
        private List<ExpTransDto> DelTrans = new List<ExpTransDto>();
        private List<PendBillListDto> DelBill = new List<PendBillListDto>();
        private List<PendBillListDto> BillList = new List<PendBillListDto>();
        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        private bool isImortOrSez = false;
        private bool isGst = true;
        public DRCRNoteIndex()
        {
            InitializeComponent();
           
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            gridControl1.ProcessGridKey += GridControl1_ProcessGridKey;
            accLookup1.SelectedValueChanged += AccLookup1_SelectedValueChanged;
            gridView1.InitNewRow += GridView1_InitNewRow;
            gridView1.CellValueChanged += GridView1_CellValueChanged;
            gridView1.KeyDown += GridView1_KeyDown;
            gridControl1.Enter += GridControl1_Enter;
            gridView1.CustomDrawRowIndicator += GridView1_CustomDrawRowIndicator;
            gridView1.ValidateRow += GridView1_ValidateRow;
            gridView1.MouseUp += GridView1_MouseUp;
            gridView1.InvalidRowException += GridView1_InvalidRowException;
            lotNoRepositoryItemButtonEdit.ButtonClick += LotNoRepositoryItemButtonEdit_ButtonClick;
            gridView1.DoubleClick += GridView1_DoubleClick;
            this.MainLayoutFile = KontoFileLayout.DrCr_Index;
            this.GridLayoutFile = KontoFileLayout.DrCr_Trans;
            this.challanNotextEdit.TextChanged += ChallanNotextEdit_TextChanged;
            this.docTypeLookUpEdit.EditValueChanged += InvTypeLookUpEdit_EditValueChanged;
            
            FillLookup();
           
            SetGridColumn();

            headerEdit.Hide();
            headerEdit.Parent = this.gridControl1;
            headerEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            headerEdit.Leave += new EventHandler(headerEdit_Leave);

            tdsPerTextEdit.EditValueChanged += TdsPerTextEdit_EditValueChanged;
            tdsAmtTextEdit.EditValueChanged += TdsAmtTextEdit_EditValueChanged;
            billNoTextEdit.ButtonClick += BillNoTextEdit_ButtonClick;
            voucherLookup1.SelectedValueChanged += VoucherLookup1_SelectedValueChanged;
            this.Load += DRCRNoteIndex_Load;
            againstLookUpEdit.EditValueChanged += AgainstLookUpEdit_EditValueChanged;

            this.FirstActiveControl = docTypeLookUpEdit;
        }

        private void AgainstLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (againstLookUpEdit.Text == "PURCHASE")
                itcLayoutControlItem10.ContentVisible = true;
            else
                itcLayoutControlItem10.ContentVisible = false;
        }

        private void DRCRNoteIndex_Load(object sender, EventArgs e)
        {
            colSgst.OptionsColumn.AllowFocus = true;
            colSgst.OptionsColumn.AllowEdit = true;
            colCgst.OptionsColumn.AllowFocus = true;
            colCgst.OptionsColumn.AllowEdit = true;
            colIgst.OptionsColumn.AllowFocus = true;
            colIgst.OptionsColumn.AllowEdit = true;
        }

        private void VoucherLookup1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.PrimaryKey == 0 && Convert.ToInt32(voucherLookup1.SelectedValue) > 0)
            {
                voucherNoTextEdit.Text = "New-" + DbUtils.NextSerialNo(Convert.ToInt32(voucherLookup1.SelectedValue), 1);
            }
        }
        private List<PendBillListDto> AllBill = new List<PendBillListDto>();
        private void BillNoTextEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            string type  = docTypeLookUpEdit.EditValue.ToString() == "DEBIT NOTE" ? "CREDIT" : "DEBIT";
            
            var frm = new PendingBillViewWindow("DR",Convert.ToInt32(accLookup1.SelectedValue),
                (int)VoucherTypeEnum.DebitCreditNote,type,this.PrimaryKey,this.PrimaryKey,
                (int)voucherLookup1.SelectedValue);
            frm.AllBill.AddRange(this.AllBill);
            frm.TotalAmount = this.billAmtSpinEdit.Value;

            if(frm.ShowDialog()== DialogResult.OK)
            {
                
                this.BillList = new List<PendBillListDto>();

                this.AllBill = frm.AllBill;

                this.DelBill.AddRange(frm.DelBillList);
                
                var plist = frm.BillList.Where(k => k.Amount > 0).ToList();
                
                this.BillList.AddRange(plist);
                
                if (plist.Count > 0)
                {
                    billNoTextEdit.Text = plist[0].BillNo;
                    billDateEdit.EditValue = plist[0].ChallanDate;
                }
                
            }
        }

        private void GridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        private void GridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            GridView view = sender as GridView;
            var descr = view.GetRowCellValue(e.RowHandle, colRemark);
            var hsncode = view.GetRowCellValue(e.RowHandle, colHsnCode);
            int batch = Convert.ToInt32( view.GetRowCellValue(e.RowHandle, colBatchId));
            int unit = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, colUomId));
            decimal qty = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, colQty));
            decimal rate = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, colRate));
            
            if (hsncode==null || string.IsNullOrEmpty(hsncode.ToString()))
            {
                e.Valid = false;
                view.SetColumnError(colHsnCode, "Hsn Code Not Blank");
            }
            else if (descr == null || string.IsNullOrEmpty(descr.ToString()))
            {
                e.Valid = false;
                view.SetColumnError(colRemark, "Product Description Cant Not Blank");
            }
            else if(batch == 0)
            {
                e.Valid = false;
                view.SetColumnError(colBatchId, "Invalid Tax Slab");
            }
            else if (unit == 0)
            {
                e.Valid = false;
                view.SetColumnError(colUomId, "Invalid Unit");
            }
            else if (qty == 0)
            {
                e.Valid = false;
                view.SetColumnError(colQty, "Invalid Qty");
            }
            else if (rate == 0)
            {
                e.Valid = false;
                view.SetColumnError(colRate, "Invalid Rate");
            }
        }

        private void TdsAmtTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            FinalTotal();
        }

        private void TdsPerTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            FinalTotal();
        }

        private void InvTypeLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (docTypeLookUpEdit.EditValue.ToString() == "Import" || docTypeLookUpEdit.EditValue.ToString() == "Received from SEZ")
            {

                isImortOrSez = true;
            }
            else
                isImortOrSez = false;
        }

        private void ChallanNotextEdit_TextChanged(object sender, EventArgs e)
        {
            billNoTextEdit.Text = challanNotextEdit.Text.Trim();
        }

        #region UDF
        private void SetGridColumn()
        {
            colFreight.Visible = GenExpPara.Freight_Required;
            colFreightRate.Visible = GenExpPara.Freight_Required;

        }
        private ExpTransDto PreOpenLookup()
        {
            if (Convert.ToInt32(accLookup1.SelectedValue) == 0) return null;
            gridView1.GetRow(gridView1.FocusedRowHandle);
            if (gridView1.GetRow(gridView1.FocusedRowHandle) == null)
            {
                gridView1.AddNewRow();
            }
            var dr = (ExpTransDto)gridView1.GetRow(gridView1.FocusedRowHandle);
            return dr;
        }
      
        public void GridCalculation(ExpTransDto er, bool isGstAmountChanged = false)
        {
            var dr = taxRepositoryItemLookUpEdit.GetDataSourceRowByKeyValue(er.BatchId) as TaxLookupDto;

            if (dr != null && this.accLookup1.LookupDto!=null)
            {
                if (isImortOrSez)
                {
                    er.IgstPer = dr.Igst;
                    er.SgstPer = 0;
                    er.CgstPer = 0;
                }
                else if (this.accLookup1.LookupDto.IsGst)
                {
                    er.SgstPer = dr.Sgst;
                    er.CgstPer = dr.Cgst;
                    er.IgstPer = 0;
                }
                else
                {
                    er.IgstPer = dr.Igst;
                    er.SgstPer = 0;
                    er.CgstPer = 0;
                }
            }
            er.Total = decimal.Round( er.Qty * er.Rate, 2);
            

            if (er.Disc > 0)
                er.DiscAmt = decimal.Round(er.Total * er.Disc / 100, 2, MidpointRounding.AwayFromZero);
            decimal gross = er.Total - er.DiscAmt;

            if (er.FreightRate > 0)
                er.Freight = decimal.Round(er.Qty * er.FreightRate / 100, 2, MidpointRounding.AwayFromZero);

            gross = gross + er.Freight + er.OtherAdd - er.OtherLess;

            if (!isGstAmountChanged)
            {

                er.Sgst = decimal.Round(gross * er.SgstPer / 100, 2, MidpointRounding.AwayFromZero);
                er.Cgst = decimal.Round(gross * er.CgstPer / 100, 2, MidpointRounding.AwayFromZero);
                er.Igst = decimal.Round(gross * er.IgstPer / 100, 2, MidpointRounding.AwayFromZero);
            }

            //  er.Cess = decimal.Round(er.Qty * er.CessPer, 2, MidpointRounding.AwayFromZero);
            if (isImortOrSez)
            {
                er.NetTotal = gross;
            }
            else
                er.NetTotal = gross + er.Sgst + er.Cgst + er.Igst;

            gridView1.UpdateCurrentRow();

            FinalTotal();
        }
        private void FinalTotal()
        {
            var Trans = grnTransDtoBindingSource1.DataSource as List<ExpTransDto>;
            var gross = Trans.Sum(x => x.NetTotal) - Trans.Sum(x => x.Cgst) - Trans.Sum(x => x.Sgst) -
                Trans.Sum(x => x.Igst) - Trans.Sum(x => x.Cess);

            if (tdsPerTextEdit.Value > 0)
            {
                if(GenExpPara.TDS_RoundOff)
                    tdsAmtTextEdit.Value = decimal.Round((gross * tdsPerTextEdit.Value / 100)+ (decimal) 0.01);
                else
                    tdsAmtTextEdit.Value = decimal.Round( gross * tdsPerTextEdit.Value / 100,2);
            }

            gridView1.UpdateTotalSummary();
            var ntotal = Convert.ToDecimal(colNetTotal.SummaryItem.SummaryValue);

            var x1 = ntotal - Math.Truncate(ntotal);



            bool isEven = false;
            if (x1 == (decimal)0.5)
            {
                ntotal = ntotal + (decimal)0.01;
                isEven = true;
            }

            var round = decimal.Round(ntotal, 0) - ntotal;
            if (isEven)
            {
                round = (decimal)0.5;
                ntotal = ntotal + (decimal)0.49;
            }
            else
            {
                ntotal = ntotal + round;
            }

            roundoffSpinEdit.Value = round;
            billAmtSpinEdit.Value = ntotal;
            paybleTextEdit.Text = (ntotal - tdsAmtTextEdit.Value).ToString("F");

        }
        
        
        private void FillLookup()
        {

            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>
            {
               new ComboBoxPairs("DEBIT NOTE", "DEBIT NOTE"),
               new ComboBoxPairs("CREDIT NOTE", "CREDIT NOTE"),
            };
            docTypeLookUpEdit.Properties.DataSource = cbp;

            List<ComboBoxPairs> tbp
                 = new List<ComboBoxPairs>
             {
               new ComboBoxPairs("SALE", "SALE"),
               new ComboBoxPairs("PURCHASE", "PURCHASE"),
             };
            againstLookUpEdit.Properties.DataSource = tbp;

            List<ComboBoxPairs> rt = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("01-Sales Return", "01-Sales Return"),
                new ComboBoxPairs("02-Post Sale Discount", "02-Post Sale Discount"),
                new ComboBoxPairs("03-Deficiency in services", "03-Deficiency in services"),
                new ComboBoxPairs("04-Correction in Invoice", "04-Correction in Invoice"),
                new ComboBoxPairs("05-Change In POS", "05-Change In POS"),
                new ComboBoxPairs("06-Finalization of Provisional assesment", "06-Finalization of Provisional assesment"),
                new ComboBoxPairs("07-Others", "07-Others"),
            };
            reasonLookUpEdit.Properties.DataSource = rt;

            List<ComboBoxPairs> ibp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Inputs", "Inputs"),
                new ComboBoxPairs("Capital Goods", "Capital Goods"),
                new ComboBoxPairs("Input Services", "Input Services"),
                new ComboBoxPairs("Ineligible","Ineligible")
            };
            itcLookUpEdit.Properties.DataSource = ibp;

            using (var db = new KontoContext())
            {
               

              
                var _storeLists = (from p in db.Stores
                                 where p.IsActive && !p.IsDeleted
                                 select new BaseLookupDto()
                                 {
                                     DisplayText = p.StoreName,
                                     Id = p.Id
                                 }).ToList();

                var _uomlist = (from p in db.Uoms
                                where !p.IsDeleted & p.IsActive
                                orderby p.UnitName
                                select new UomLookupDto()
                                {
                                    DisplayText = p.UnitName,
                                    Id = p.Id,RateOn = p.RateOn
                                }).ToList();

                var gstList = (from p in db.TaxMasters
                              where !p.IsDeleted && p.IsActive
                              orderby p.TaxName
                              select new TaxLookupDto
                              {
                                  DisplayText = p.TaxName,
                                  Id = p.Id,
                                  TaxType = p.TaxType,
                                  Cgst = p.Cgst,
                                  Sgst = p.Sgst,
                                  Igst = p.Igst
                              }).ToList();
               
                uomRepositoryItemLookUpEdit.DataSource = _uomlist;

                taxRepositoryItemLookUpEdit.DataSource = gstList;
                storeLookUpEdit.Properties.DataSource = _storeLists;
            }
        }

        private bool ValidateData()
        {
            var dt = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
          
            if ( string.IsNullOrEmpty(docTypeLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Invoice Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                docTypeLookUpEdit.Focus();
                return false;
            }
            else if (Convert.ToInt32(voucherLookup1.SelectedValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Voucher", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherLookup1.Focus();
                return false;
            }
            else if (Convert.ToInt32(accLookup1.SelectedValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Party", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                accLookup1.Focus();
                return false;
            }
            else if ( Convert.ToInt32(bookLookup.SelectedValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Book/Expense Ledger", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bookLookup.Focus();
                return false;
            }
            else if ( string.IsNullOrEmpty(billNoTextEdit.Text.Trim()))
            {
                MessageBoxAdv.Show(this, "Invalid Bill No.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                billNoTextEdit.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(againstLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Against", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                againstLookUpEdit.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(reasonLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Reason", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                reasonLookUpEdit.Focus();
                return false;
            }
            else if (dt > KontoGlobals.ToDate || dt < KontoGlobals.FromDate)
            {
                MessageBoxAdv.Show(this, "Voucher date out of financial range", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherDateEdit.Focus();
                return false;
            }
            else if(againstLookUpEdit.Text=="PURCHASE" && string.IsNullOrEmpty(itcLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Itc", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                itcLookUpEdit.Focus();
                return false;
            }
            //else if (string.IsNullOrEmpty(storeLookUpEdit.Text))
            //{
            //    MessageBoxAdv.Show(this, "Invalid Store", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    storeLookUpEdit.Focus();
            //    return false;
            //}
            else if (gridView1.RowCount == 1)
            {
                MessageBoxAdv.Show(this, "At Least One Product Should be Entered", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }
            else if( Convert.ToInt32(tdsAccLookup.SelectedValue) ==0 && tdsAmtTextEdit.Value > 0)
            { 
                MessageBoxAdv.Show(this, "Tds Account Must be Selected", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tdsAmtTextEdit.Focus();
                return false;
            }
            

            return true;
        }

        private void LoadData(BillModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            docTypeLookUpEdit.EditValue = model.BillType;
            itcLookUpEdit.EditValue = model.Itc;
            voucherLookup1.SelectedValue = model.VoucherId;
            voucherLookup1.SetGroup(model.VoucherId);

            bookLookup.SelectedValue = model.BookAcId;
            bookLookup.SetAcc(Convert.ToInt32(model.BookAcId));
            voucherDateEdit.EditValue = KontoUtils.IToD(model.VoucherDate);
            voucherNoTextEdit.Text = model.VoucherNo;

            accLookup1.SelectedValue = model.AccId;
            accLookup1.SetAcc(model.AccId);
            challanNotextEdit.Text = model.RefNo;
            billNoTextEdit.Text = model.BillNo;
            billDateEdit.EditValue = model.RcdDate;
            againstLookUpEdit.EditValue = model.Extra1;
            reasonLookUpEdit.EditValue = model.SpecialNotes;
            if (Convert.ToInt32(model.EmpId) != 0)
            {
                empLookup1.SelectedValue = model.EmpId;
                empLookup1.SetGroup();
            }
            storeLookUpEdit.EditValue = model.StoreId;

            remarkTextEdit.Text = model.Remarks;

            if (Convert.ToInt32(model.HasteId) != 0)
            {
                tdsAccLookup.SelectedValue = model.HasteId;
                tdsAccLookup.SetAcc((int)model.HasteId);
            }

            tdsPerTextEdit.Value = model.TdsPer;
            tdsAmtTextEdit.Value = model.TdsAmt;
            billAmtSpinEdit.Value = model.TotalAmount;
            roundoffSpinEdit.Value = Convert.ToDecimal(model.RoundOff);
            paybleTextEdit.EditValue = model.TotalAmount - model.TdsAmt;
            createdLabelControl.Text = "Created By: " + model.CreateUser + " [ " + model.CreateDate + " ]";
            modifyLabelControl.Text = "Modified By: " + model.ModifyUser + " [ " + model.ModifyDate ?? string.Empty  + " ]";

            using (var _context = new KontoContext())
            {

                var _lst = (from bt in _context.BillTrans
                            join rb in _context.TaxMasters on bt.BatchId equals rb.Id into joinRb
                            from rb in joinRb.DefaultIfEmpty()
                            join um in _context.Uoms on bt.UomId equals um.Id
                            orderby bt.Id
                            where bt.BillId == model.Id && !bt.IsDeleted
                            select new ExpTransDto
                            {
                                Id = bt.Id,
                                BillId = bt.BillId,
                                UomId = bt.UomId,
                                BatchId = bt.BatchId,
                                Cess = bt.Cess,
                                CessPer = bt.CessPer,
                                Cgst = bt.Cgst,
                                CgstPer = bt.CgstPer,
                                Disc = bt.Disc,
                                DiscAmt = bt.DiscAmt,
                                Freight = bt.Freight,
                                FreightRate = bt.FreightRate,
                                HsnCode = bt.HsnCode,
                                Igst = bt.Igst,
                                IgstPer = bt.IgstPer,
                                NetTotal = bt.NetTotal,
                                OtherAdd = bt.OtherAdd,
                                OtherLess = bt.OtherLess,
                                Qty = bt.Qty,
                                Rate = bt.Rate,
                                Remark = bt.Remark,
                                Sgst = bt.Sgst,
                                SgstPer = bt.SgstPer,
                                ToAccId = bt.ToAccId,
                                Total = bt.Total
                            }
                             ).ToList();

                grnTransDtoBindingSource1.DataSource = _lst;
            }


         //   FinalTotal();
            this.Text = "DrCr Note [View/Modify]";

        }
      
        #endregion

        #region GridView
        private void GridView1_MouseUp(object sender, MouseEventArgs e)
        {
            DevExpress.Utils.DXMouseEventArgs args = (e as DevExpress.Utils.DXMouseEventArgs);
            GridView view = sender as GridView;
            GridHitInfo hi = view.CalcHitInfo(args.Location);
            if (hi.InColumn)
            {
                GridViewInfo ViewInfo = view.GetViewInfo() as GridViewInfo;
              
                if ((e.Button & MouseButtons.Left) != 0)
                {
                    if (ViewInfo.ColumnsInfo[hi.Column].CaptionRect.Contains(new Point(e.X, e.Y)))
                    {
                        ViewInfo.SelectionInfo.ClearPressedInfo();
                        args.Handled = true;
                    }
                }
            }
        }

        private void GridView1_DoubleClick(object sender, EventArgs e)
        {
            DevExpress.Utils.DXMouseEventArgs args = (e as DevExpress.Utils.DXMouseEventArgs);
            GridView view = sender as GridView;
            GridHitInfo hi = view.CalcHitInfo(args.Location);
            if (hi.InColumn)
            {
                GridViewInfo vi = view.GetViewInfo() as GridViewInfo;
                Rectangle bounds = vi.ColumnsInfo[hi.Column].Bounds;
                bounds.Width -= 10;
                bounds.Height -= 3;
                bounds.Y += 3;
                headerEdit.SetBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);
                headerEdit.EditValue = hi.Column.Caption;
                headerEdit.Show();
                headerEdit.Focus();
                activeCol = hi.Column;
            }
        }
      
        private void GridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.Appearance.ForeColor = Color.FromArgb(227, 22, 91);
            if (e.RowHandle < 0)
                e.Info.DisplayText = "";
            else
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        private void GridControl1_Enter(object sender, EventArgs e)
        {
            gridView1.FocusedColumn = gridView1.VisibleColumns[0];
        }
        private void GridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == null) return;
            if (!(gridView1.GetRow(e.RowHandle) is ExpTransDto er)) return;

            if (e.Column == colSgst || e.Column == colCgst|| e.Column == colIgst)
                GridCalculation(er, true);
            else
                GridCalculation(er, false);
        }
        private void GridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete) return;

            if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
            {
                if (MessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo) !=
                  DialogResult.Yes)
                    return;
                GridView view = sender as GridView;
                var row = view.GetRow(view.FocusedRowHandle) as ExpTransDto;
                view.DeleteRow(view.FocusedRowHandle);
                DelTrans.Add(row);
            }
            
        }

        private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var rw = gridView1.GetRow(e.RowHandle) as ExpTransDto;
            rw.Id = -1 * gridView1.RowCount;
        }
        private void OpenItemLookup(int _selvalue, ExpTransDto er)
        {
            var frm = new ProductLkpWindow();
            frm.Tag = MenuId.Product_Master;
            frm.SelectedValue = _selvalue;
            //frm.PTypeId = ProductTypeEnum.;
            frm.VoucherType = VoucherTypeEnum.DebitCreditNote;

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                er.ProductId = frm.SelectedValue;
                er.Remark = frm.SelectedTex;
                var model = frm.SelectedItem as ProductLookupDto;
                er.UomId = model.UomId;
                er.Rate = model.SaleRate;
                er.HsnCode = model.HsnCode;
                er.BatchId = model.TaxId;
                if (accLookup1.LookupDto.IsGst)
                {
                    er.SgstPer = model.Sgst;
                    er.CgstPer = model.Cgst;
                    er.IgstPer = 0;
                    er.Igst = 0;
                }
                else
                {
                    er.SgstPer = 0;
                    er.Sgst = 0;
                    er.CgstPer = 0;
                    er.Cgst = 0;
                    er.IgstPer = model.Igst;
                }
                er.CessPer = model.Cess;
                gridView1.FocusedColumn = gridView1.GetNearestCanFocusedColumn(gridView1.FocusedColumn);
            }
            GridCalculation(er);
        }
        private void GridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(accLookup1.SelectedValue) == 0) return;
               var dr = PreOpenLookup();
                if (dr == null) return;

                 if (gridView1.FocusedColumn.FieldName == "Remark")
                {
                    if(string.IsNullOrEmpty(dr.Remark) && e.KeyCode == Keys.Return)
                    {
                        OpenItemLookup(dr.ProductId, dr);
                        e.Handled = true;
                    }
                     if (e.KeyCode == Keys.F1)
                    {
                        OpenItemLookup(dr.ProductId, dr);
                        e.Handled = true;
                    }
                }



            }
            catch (Exception ex)
            {
                Log.Error(ex, "Drcr GridControl KeyDown");
                MessageBoxAdv.Show(this, "Error Lookup Setup !!", "Exception ", ex.ToString());

            }

        }
       
        private void LotNoRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
           // if (dr != null)
               // ShowItemDetail(dr);
        }
      


        #endregion

        void headerEdit_Leave(object sender, EventArgs e)
        {
            activeCol.Caption = headerEdit.Text;
            headerEdit.Hide();
        }
       
        private void AccLookup1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (accLookup1.LookupDto == null) return;
            if (accLookup1.LookupDto.IsGst)
            {
                colSgst.Visible = true;
                colSgstPer.Visible = true;
                colCgst.Visible = true;
                colCgstPer.Visible = true;
                colIgst.Visible = false;
                colIgstPer.Visible = false;
            }
            else if(accLookup1.LookupDto.IsIgst)
            {
                colSgst.Visible = false;
                colSgstPer.Visible = false;
                colCgst.Visible = false;
                colCgstPer.Visible = false;
                colIgst.Visible = true;
                colIgstPer.Visible = true;
            }
            if (accLookup1.LookupDto.TdsReq == "Yes")
            {
                tdsPerTextEdit.Value = accLookup1.LookupDto.TdsPer;
                if (Convert.ToInt32(accLookup1.LookupDto.TdsAccId) > 0)
                {
                    tdsAccLookup.SelectedValue = accLookup1.LookupDto.TdsAccId;
                    tdsAccLookup.SetAcc(Convert.ToInt32(accLookup1.LookupDto.TdsAccId));
                }
            }
        }

        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                voucherLookup1.Focus();
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as DRCRNoteListView;
                _list.ActiveControl = _list.KontoGrid;
                this.Text = "DrCr Note [View]";
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new DRCRNoteListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "DrCr Note [View]";

            }
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveDataAsync(true);
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Drcr Note Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }


        #region Parent Function

        public override void Print()
        {
            base.Print();
            try
            {
                if (this.PrimaryKey == 0) return;

                PageReport rpt = new PageReport();

                rpt.Load(new FileInfo("reg\\doc\\NotePrintTextilesF1.rdlx"));

                rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);

                doc.Parameters["id"].CurrentValue = this.PrimaryKey;
                doc.Parameters["Bill"].CurrentValue = "N";
                doc.Parameters["reportid"].CurrentValue = 0;

                var frm = new KontoRepViewer(doc);
                frm.Text = "Dr/Cr Note";
                var _tab = this.Parent.Parent as TabControlAdv;
                if (_tab == null) return;
                var pg1 = new TabPageAdv();
                pg1.Text = "Dr/Cr Note Print";
                _tab.TabPages.Add(pg1);
                _tab.SelectedTab = pg1;
                frm.TopLevel = false;
                frm.Parent = pg1;
                frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                frm.Show();// = true;

            }
            catch (Exception ex)
            {
                Log.Error(ex, "drcr Note print");
                MessageBoxAdv.Show(this, "Error While Print !!", "Exception ", ex.ToString());

            }
        }
        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<BillModel>();
            this.Text = "DrCr Note [Add New]";
            againstLookUpEdit.EditValue = "SALE";
            docTypeLookUpEdit.EditValue = "DEBIT NOTE";
            reasonLookUpEdit.EditValue = "07-Others";
            storeLookUpEdit.EditValue = 1;
            voucherNoTextEdit.Text = "New";
            voucherDateEdit.EditValue = DateTime.Now;
            billDateEdit.EditValue = DateTime.Now;
            empLookup1.SelectedValue = 1;
            empLookup1.SetGroup();
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
            this.ActiveControl = voucherLookup1.buttonEdit1;
            voucherLookup1.SetDefault();
            if (Convert.ToInt32(voucherLookup1.GroupDto.AccId) > 0)
            {
                bookLookup.SelectedValue = voucherLookup1.GroupDto.AccId;
                bookLookup.SetAcc(Convert.ToInt32(voucherLookup1.GroupDto.AccId));
            }

            DelTrans = new List<ExpTransDto>();
            this.grnTransDtoBindingSource1.DataSource = new List<ExpTransDto>();
            DelBill = new List<PendBillListDto>();
            BillList = new List<PendBillListDto>();
            AllBill = new List<PendBillListDto>();
        }
        public override void ResetPage()
        {
            base.ResetPage();
            
            accLookup1.SetEmpty();
            bookLookup.SetEmpty();
            challanNotextEdit.Text = string.Empty;
            billNoTextEdit.Text = string.Empty;
            voucherDateEdit.DateTime = DateTime.Now;
            billDateEdit.DateTime = DateTime.Now;
            voucherNoTextEdit.Text = string.Empty;
            itcLookUpEdit.EditValue = string.Empty;
           
            empLookup1.SetEmpty();
           
            remarkTextEdit.Text = string.Empty;
            tdsAccLookup.SetEmpty();
            tdsPerTextEdit.Value = 0;
            tdsAmtTextEdit.Value = 0;
            paybleTextEdit.Text = "0";

            roundoffSpinEdit.Value = 0;
            billAmtSpinEdit.Value = 0;
            
            DelTrans = new List<ExpTransDto>();
            DelBill = new List<PendBillListDto>();
            BillList = new List<PendBillListDto>();
            AllBill = new List<PendBillListDto>();

        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

          
            using (var db = new KontoContext())
            {
                var bill = db.Bills.Find(_key);
                LoadData(bill);
            }

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

        public override void FindRec()
        {
            List<Filter> filter = new List<Filter>();
            

            if (Convert.ToInt32(voucherLookup1.SelectedValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "VoucherId", Operation = Op.Equals, Value = Convert.ToInt32(voucherLookup1.SelectedValue) });
            }
            if (!string.IsNullOrEmpty(billNoTextEdit.Text.Trim()))
            {
                filter.Add(new Filter { PropertyName = "BillNo", Operation = Op.Equals, Value = billNoTextEdit.Text.Trim() });
            }
            if (Convert.ToInt32(accLookup1.SelectedValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "AccId", Operation = Op.Equals, Value = Convert.ToInt32(accLookup1.SelectedValue) });
            }

            filter.Add(new Filter { PropertyName = "CompId", Operation = Op.Equals, Value = KontoGlobals.CompanyId });
            filter.Add(new Filter { PropertyName = "YearId", Operation = Op.Equals, Value = KontoGlobals.YearId });
            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ChallanModel, GrnDto>();
            });

            using (var db = new KontoContext())
            {
                FilterView = db.Bills.Where(ExpressionBuilder.GetExpression<BillModel>(filter))
                    .OrderBy(x => x.VoucherDate).ThenBy(x => x.Id).ToList();
                   

                if (FilterView.Count == 0)
                {
                    MessageBoxAdv.Show(this, "No Record Found", "Find !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ResetPage();
                    return;
                }
                this.TotalRecord = FilterView.Count;
                this.RecordNo = 0;
                LoadData(this.FilterView[0]);

            }

        }

        public override void SaveDataAsync(bool newmode)
        {

            bool IsSaved = false;
            if (!ValidateData()) return;
          
            var _find = new BillModel();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ExpTransDto, BillTransModel>().ForMember(x => x.Id, p => p.Ignore());
               
            });
            
            var _translist = grnTransDtoBindingSource1.DataSource as List<ExpTransDto>;
            List<BillTransModel> Trans = new List<BillTransModel>();
            
            using (var db = new KontoContext())
            {
                using (var _tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (this.PrimaryKey != 0)
                        {
                            _find = db.Bills.Find(this.PrimaryKey);
                        }

                        if(!UpdateBill(db, _find))
                        {
                            _tran.Rollback();
                            return;
                        }
                        
                        var map = new Mapper(config);

                        foreach (var item in _translist)
                        {
                            var transid = item.Id;
                            item.BillId = _find.Id;
                            var tranModel = new BillTransModel();
                            if (item.Id > 0)
                            {
                                tranModel = db.BillTrans.Find(item.Id);
                            }
                            map = new Mapper(config);
                            map.Map(item, tranModel);

                            if (tranModel.Id <= 0)
                            {
                                db.BillTrans.Add(tranModel);
                                db.SaveChanges();

                            }
                            item.Id = tranModel.Id;
                            Trans.Add(tranModel);
                           
                        }
                        //delete item from  trans
                        foreach (var item in DelTrans)
                        {
                            if (item.Id == 0) continue;
                            var _model = db.BillTrans.Find(item.Id);
                            _model.IsDeleted = true;
                        }

                        if (_find.BillType == "CREDIT NOTE")
                        {
                            //Bill Reference Update
                            LedgerEff.BillRefEntry("Credit", _find, 0, db);       //Insert or update in Billref table

                            //Insert or update in LedgerTrans table
                            LedgerEff.LedgerTransEntry("Credit", _find, db, Trans);
                        }
                        else
                        {
                            LedgerEff.BillRefEntry("Debit", _find, 0, db);       //Insert or update in Billref table

                            //Insert or update in LedgerTrans table
                            LedgerEff.LedgerTransEntry("Debit", _find, db, Trans);
                        }
                        // Insert in BtoB for BillAdjustment

                       LedgerEff.BtoBEntry("Payment",  _find.Id, _find, db, BillList);
                        
                       

                        db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "DrCr Note" +" Save");
                        MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());

                    }
                }
            }
               

            
            if (IsSaved)
            {
               // NewRec();
               
                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage +" Voucher No.: " + _find.VoucherNo, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (!this.OpenForLookup && newmode)
                {
                    base.SaveDataAsync(newmode);
                    this.ResetPage();
                    this.NewRec();
                    voucherLookup1.buttonEdit1.Focus();
                }
                else
                {
                    this.Close();
                    this.Dispose();
                }
            }
        }

        private bool UpdateBill(KontoContext db,BillModel model)
        {
           
            model.BillType = docTypeLookUpEdit.EditValue.ToString();
            model.Extra1 = againstLookUpEdit.EditValue.ToString();
            model.SpecialNotes = reasonLookUpEdit.EditValue.ToString();
            
            model.Itc = itcLookUpEdit.Text;

            model.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);
            model.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));

            model.AccId = Convert.ToInt32(accLookup1.SelectedValue);
            model.BookAcId = Convert.ToInt32(bookLookup.SelectedValue);
            model.RcdDate = billDateEdit.DateTime;
            model.VoucherNo = voucherNoTextEdit.Text.Trim();

          
            model.RefNo = challanNotextEdit.Text.Trim();
            model.BillNo = billNoTextEdit.Text.Trim();

            model.EmpId = Convert.ToInt32(empLookup1.SelectedValue);
            model.StoreId = Convert.ToInt32(storeLookUpEdit.EditValue);

            model.Remarks = remarkTextEdit.Text.Trim();
           
           
            model.TypeId = (int)VoucherTypeEnum.DebitCreditNote;
            model.CompId = KontoGlobals.CompanyId;
            model.YearId = KontoGlobals.YearId;
            model.BranchId = KontoGlobals.BranchId;
            model.RoundOff = roundoffSpinEdit.Value;

            model.HasteId = Convert.ToInt32(tdsAccLookup.SelectedValue);
            model.TdsPer = tdsPerTextEdit.Value;
            model.TdsAmt = tdsAmtTextEdit.Value;
            var _translist = grnTransDtoBindingSource1.DataSource as List<ExpTransDto>;
            model.GrossAmount = _translist.Sum(x => x.Total);
            model.TotalAmount = billAmtSpinEdit.Value;
            model.TotalQty = _translist.Sum(x => x.Qty);
            model.TotalPcs = 0;
            model.RoundOff = roundoffSpinEdit.Value;
            model.IsActive = true;
           
          

            if (model.Id == 0)
            {
                model.VoucherNo = DbUtils.NextSerialNo(model.VoucherId, db);
                if (DbUtils.CheckExistVoucherNo(model.VoucherId, model.VoucherNo, db, model.Id))
                {
                    MessageBox.Show("Duplicate Voucher No Not Allowed");
                    voucherNoTextEdit.Focus();
                    return false;
                }
                db.Bills.Add(model);
                db.SaveChanges();
            }
            return true;

        }

        #endregion

        private void roundoffSpinEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (!roundoffSpinEdit.ContainsFocus) return;
            gridView1.UpdateTotalSummary();
            var ntotal = Convert.ToDecimal(colNetTotal.SummaryItem.SummaryValue);

            ntotal = ntotal + roundoffSpinEdit.Value;

            billAmtSpinEdit.Value = ntotal;
            paybleTextEdit.Text = (ntotal - tdsAmtTextEdit.Value).ToString("F");
        }
    }
}
