using AutoMapper;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using GrapeCity.ActiveReports;
using Konto.App.Shared;
using Konto.App.Shared.Para;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters.Dtos;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using Konto.Shared.Masters.Acc;
using Konto.Shared.Trans.Common;
using Konto.Shared.Trans.PInvoice;
using Serilog;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ExpressionBuilder = Konto.Core.Shared.Libs.ExpressionBuilder;

namespace Konto.Shared.Account.GenExpense
{
    public partial class GenExpIndex : KontoMetroForm
    {
        private List<BillModel> FilterView = new List<BillModel>();
        private List<ExpTransDto> DelTrans = new List<ExpTransDto>();
        private List<PendBillListDto> DelBill = new List<PendBillListDto>();
        private List<PendBillListDto> BillList = new List<PendBillListDto>();
        private List<PendBillListDto> AllBill = new List<PendBillListDto>();
        private List<AttachmentModel> _DelFile = new List<AttachmentModel>();
        private List<AttachmentModel> _TransFile = new List<AttachmentModel>();
        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        private bool isImortOrSez = false;
        private bool IsLoadData = false;
        public GenExpIndex()
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
            
            gridView1.DoubleClick += GridView1_DoubleClick;
            this.MainLayoutFile = KontoFileLayout.Gen_Expense_Index;
            this.GridLayoutFile = KontoFileLayout.Gen_Expense_Trans;
            this.challanNotextEdit.TextChanged += ChallanNotextEdit_TextChanged;
            this.invTypeLookUpEdit.EditValueChanged += InvTypeLookUpEdit_EditValueChanged;
            this.rcmLookUpEdit.EditValueChanged += RcmLookUpEdit_EditValueChanged;
            this.productRepositoryItemButtonEdit.ButtonClick += ProductRepositoryItemButtonEdit_ButtonClick;
            FillLookup();
            SetParameter();


            headerEdit.Hide();
            headerEdit.Parent = this.gridControl1;
            headerEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            headerEdit.Leave += new EventHandler(headerEdit_Leave);

            tdsPerTextEdit.EditValueChanged += TdsPerTextEdit_EditValueChanged;
            tdsAmtTextEdit.EditValueChanged += TdsAmtTextEdit_EditValueChanged;
            billAdjustSimpleButton.Click += BillAdjustSimpleButton_Click;
            voucherLookup1.SelectedValueChanged += VoucherLookup1_SelectedValueChanged;
            this.Load += GenExpIndex_Load;
            tcsPerTextEdit.EditValueChanged += TcsPerTextEdit_EditValueChanged;
            tcsAmtTextEdit.EditValueChanged += TcsAmtTextEdit_EditValueChanged;
            this.voucherDateEdit.EditValueChanged += VoucherDateEdit_EditValueChanged;
            this.FirstActiveControl = invTypeLookUpEdit;

            //this.accLookup1.ShownPopup += AccLookup1_ShownPopup;
        }

        private void VoucherDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (this.PrimaryKey != 0) return;
            billDateEdit.EditValue = voucherDateEdit.EditValue;
            lrDateEdit.EditValue = voucherDateEdit.EditValue;
        }

        private void TcsAmtTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            FinalTotal();
        }

        private void TcsPerTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            FinalTotal();
        }
        private void ProductRepositoryItemButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            OpenAccLookup(dr.TdsAcId, dr);
        }

        private void RcmLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            UpdateGst();
        }

        private void AccLookup1_ShownPopup(object sender, EventArgs e)
        {
            if (Convert.ToInt32(accLookup1.SelectedValue) == 0 || this.PrimaryKey != 0) return;
            var grnfrm = new PendingGrnForPurchase();
            grnfrm.VoucherType = VoucherTypeEnum.Inward;
            grnfrm.ChallanType = ChallanTypeEnum.PURCHASE;
            grnfrm.AccId = Convert.ToInt32(accLookup1.SelectedValue);
            if (grnfrm.ShowDialog() != DialogResult.OK) return;

            Int32[] selectedRowHandles = grnfrm.SelectedRows;
            if (selectedRowHandles == null || selectedRowHandles.Count() == 0) return;

            List<BillTransDto> transDtos = new List<BillTransDto>();

            var db = new KontoContext();
            foreach (var item in selectedRowHandles)
            {
                var chln = grnfrm.gridView1.GetRow(item) as PendingChallanOnInvoiceDto;

                var prlist = new List<PendingChallanOnInvoiceDetDto>();
                var spcol = db.SpCollections.FirstOrDefault(k => k.Id ==
                            (int)SpCollectionEnum.PendingChallanOnInvoiceDet);
                if (spcol == null)
                {
                    prlist = db.Database.SqlQuery<PendingChallanOnInvoiceDetDto>(
                    "dbo.PendingChallanOnInvoiceDet @CompanyId={0},@ChallanID={1}",
                    Convert.ToInt32(KontoGlobals.CompanyId), (int)chln.Id).ToList();
                }
                else
                {
                    prlist = db.Database.SqlQuery<PendingChallanOnInvoiceDetDto>(
                     spcol.Name + "  @CompanyId={0},@ChallanID={1}",
                      Convert.ToInt32(KontoGlobals.CompanyId), (int)chln.Id).ToList();
                }
                foreach (var ch in prlist)
                {
                    BillTransDto ct = new BillTransDto();
                    ct.ProductId = ch.ProductId;
                    ct.ProductName = ch.Product;
                    ct.Pcs = Convert.ToInt32(ch.Pcs);
                    ct.Qty = ch.Qty != null ? (decimal)ch.Qty : 0;
                    ct.Rate = ch.Rate != null ? (decimal)ch.Rate : 0;
                    ct.Disc = ch.Disc;
                    ct.DiscAmt = ch.DiscAmt;
                    ct.UomId = ch.UomId;
                    ct.DesignId = Convert.ToInt32(ch.DesignId);
                    ct.ColorId = Convert.ToInt32(ch.ColorId);
                    ct.GradeId = Convert.ToInt32(ch.GradeId);
                    ct.DesignName = ch.DesignNo;
                    ct.ColorName = ch.ColorName;
                    ct.GradeName = ch.GradeName;

                    ct.ChallanNo = ch.ChallanNo;
                    ct.ChallanDate = ch.ChallanDate;
                    ct.RefId = ch.Id;
                    ct.RefTransId = ch.TransId;
                    ct.RefVoucherId = ch.VoucherId;
                    ct.OrderNo = ch.OrderNO;
                    ct.OrderDate = ch.OrderDate;
                    ct.OrdId = ch.OrdId;
                    ct.FreightRate = ch.FreightRate != 0 ? ch.FreightRate : 0;
                    ct.Freight = ch.Freight != 0 ? ch.Freight : 0;
                    ct.OtherAdd = ch.OtherAdd;
                    ct.OtherLess = ch.OtherLess;

                    ct.Total = Convert.ToInt32(ch.Total);
                    ct.DiscAmt = ct.DiscAmt;
                    if (accLookup1.LookupDto.IsGst && !isImortOrSez)
                    {
                        ct.SgstPer = ch.Sgst;
                        ct.CgstPer = ch.Cgst;
                        ct.IgstPer = 0;
                        ct.Igst = 0;
                    }
                    else
                    {
                        ct.Sgst = 0;
                        ct.SgstPer = 0;
                        ct.Cgst = 0;
                        ct.CgstPer = 0;
                        ct.IgstPer = ch.Igst;
                    }
                    var gross = ct.Total - ct.DiscAmt + ct.Freight + ct.OtherAdd - ct.OtherLess;
                    if (PurchasePara.Tax_RoundOff)
                    {
                        ct.Sgst = decimal.Round(gross * ct.SgstPer / 100, 0);
                        ct.Cgst = decimal.Round(gross * ct.CgstPer / 100, 0); //, MidpointRounding.AwayFromZero);
                        ct.Igst = decimal.Round(gross * ct.IgstPer / 100, 0); //, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        ct.Sgst = decimal.Round(gross * ct.SgstPer / 100, 2);
                        ct.Cgst = decimal.Round(gross * ct.CgstPer / 100, 2); //, MidpointRounding.AwayFromZero);
                        ct.Igst = decimal.Round(gross * ct.IgstPer / 100, 2); //, MidpointRounding.AwayFromZero);
                    }
                    ct.Cess = decimal.Round(ct.Qty * ct.CessPer, 2);

                    if (rcmLookUpEdit.EditValue.ToString() == "YES" || isImortOrSez)
                    {
                        ct.NetTotal = gross;
                    }
                    else if (!isImortOrSez)
                    {
                        ct.NetTotal = gross + ct.Sgst + ct.Cgst + ct.Igst + ct.Cess; // ct er.CessAmt; 
                    }
                    transDtos.Add(ct);
                }

            }
            grnTransDtoBindingSource1.DataSource = transDtos;
            FinalTotal();
        }

        private void GenExpIndex_Load(object sender, EventArgs e)
        {
            colSgst.OptionsColumn.AllowFocus = true;
            colSgst.OptionsColumn.AllowEdit = true;
            colCgst.OptionsColumn.AllowFocus = true;
            colCgst.OptionsColumn.AllowEdit = true;
            colIgst.OptionsColumn.AllowFocus = true;
            colIgst.OptionsColumn.AllowEdit = true;

            SetGridColumn();
        }

        private void VoucherLookup1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.PrimaryKey == 0 && Convert.ToInt32(voucherLookup1.SelectedValue) > 0)
            {
                voucherNoTextEdit.Text = "New-" + DbUtils.NextSerialNo(Convert.ToInt32(voucherLookup1.SelectedValue), 1);
            }
        }

        private void BillAdjustSimpleButton_Click(object sender, EventArgs e)
        {
            string type = "DEBIT";
            if (this.billAmtSpinEdit.Value == 0) return;
            var frm = new PendingBillViewWindow("EXP", Convert.ToInt32(accLookup1.SelectedValue),
                (int)VoucherTypeEnum.GenExpense, type, this.PrimaryKey, this.PrimaryKey,
                (int)voucherLookup1.SelectedValue);

            frm.AllBill.AddRange(this.AllBill);
            frm.TotalAmount = this.billAmtSpinEdit.Value;

            if (frm.ShowDialog() == DialogResult.OK)
            {
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
            int batch = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, colBatchId));
            int unit = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, colUomId));
            decimal qty = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, colQty));
            decimal rate = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, colRate));

            if (hsncode == null || string.IsNullOrEmpty(hsncode.ToString()))
            {
                e.Valid = false;
                view.SetColumnError(colHsnCode, "Hsn Code Not Blank");
            }
            else if (batch == 0)
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
        private void UpdateGst()
        {
            if (invTypeLookUpEdit.EditValue == null) return;
            
            if (invTypeLookUpEdit.EditValue.ToString() == "Import" || invTypeLookUpEdit.EditValue.ToString() == "Received from SEZ")
            {

                isImortOrSez = true;
            }
            else
                isImortOrSez = false;

            if (accLookup1.LookupDto != null && accLookup1.LookupDto.IsGst && !isImortOrSez)
            {
                colSgst.Visible = true;
                colSgstPer.Visible = true;
                colCgst.Visible = true;
                colCgstPer.Visible = true;
                colIgst.Visible = false;
                colIgstPer.Visible = false;
                for (int i = 0; i < gridView1.RowCount - 1; i++)
                {
                    var rw = gridView1.GetRow(i) as ExpTransDto;
                    var gst = rw.Igst + rw.Cgst + rw.Sgst;
                    var gstPer = rw.IgstPer + rw.CgstPer + rw.SgstPer;
                    rw.Igst = 0;
                    rw.IgstPer = 0;
                    rw.Sgst = gst / 2;
                    rw.Cgst = gst / 2;
                    rw.SgstPer = gstPer / 2;
                    rw.CgstPer = gstPer / 2;
                    decimal gross = rw.Total - rw.DiscAmt + rw.Freight + rw.OtherAdd - rw.OtherLess;
                    if (rcmLookUpEdit.EditValue.ToString() == "YES")
                        rw.NetTotal = gross;
                    else
                        rw.NetTotal = gross + rw.Sgst + rw.Cgst + rw.Igst + rw.Cess;
                }
            }
            else
            {
                colSgst.Visible = false;
                colSgstPer.Visible = false;
                colCgst.Visible = false;
                colCgstPer.Visible = false;
                colIgst.Visible = true;
                colIgstPer.Visible = true;
                for (int i = 0; i < gridView1.RowCount - 1; i++)
                {
                    var rw = gridView1.GetRow(i) as ExpTransDto;
                    var gst = rw.Igst + rw.Cgst + rw.Sgst;
                    var gstPer = rw.IgstPer + rw.CgstPer + rw.SgstPer;
                    rw.IgstPer = gstPer;
                    rw.Igst = gst;
                    rw.Sgst = 0;
                    rw.Cgst = 0;
                    rw.SgstPer = 0;
                    rw.CgstPer = 0;
                    decimal gross = rw.Total - rw.DiscAmt + rw.Freight + rw.OtherAdd - rw.OtherLess;
                    if (rcmLookUpEdit.EditValue.ToString() == "YES")
                        rw.NetTotal = gross;
                    else
                        rw.NetTotal = gross + rw.Sgst + rw.Cgst + rw.Igst + rw.Cess;
                }
            }

            gridControl1.RefreshDataSource();
            FinalTotal();
        }
        private void InvTypeLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            UpdateGst();
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
            //Rate Decimal Settings
            var repo1 = new RepositoryItemTextEdit();
            repo1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            repo1.Mask.EditMask = "n" + GenExpPara.Rate_Decimal.ToString();
            colRate.ColumnEdit = repo1;
            colRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colRate.DisplayFormat.FormatString = "n" + GenExpPara.Rate_Decimal.ToString();

            //Qty Decimal Settings
            repositoryItemTextEdit1.Mask.EditMask = "n" + GenExpPara.Qty_Decimal.ToString();
            colQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colQty.DisplayFormat.FormatString = "n" + GenExpPara.Qty_Decimal.ToString();

            colParticular.VisibleIndex = -1;
            colTdsAmt.VisibleIndex = -1;
            colTdsPer.VisibleIndex = -1;

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
                if ((this.accLookup1.LookupDto.VatTds == "REG" || this.accLookup1.LookupDto.VatTds=="ECOM") || rcmLookUpEdit.Text.ToUpper() == "YES")
                {
                    if (this.accLookup1.LookupDto.IsGst)
                    {
                        er.SgstPer = dr.Sgst;
                        er.CgstPer = dr.Cgst;
                        er.IgstPer = 0;
                        er.Igst = 0;
                    }
                    else
                    {
                        er.IgstPer = dr.Igst;
                        er.SgstPer = 0;
                        er.CgstPer = 0;
                        er.Cgst = 0;
                        er.Sgst = 0;
                    }
                }
                else
                {
                    er.SgstPer = 0;
                    er.Sgst = 0;
                    er.CgstPer = 0;
                    er.Cgst = 0;
                    er.IgstPer = 0;
                    er.Igst = 0;
                    er.Cess = 0;
                    er.CessPer = 0;
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
            if (rcmLookUpEdit.EditValue.ToString() == "YES" || isImortOrSez)
            {
                er.NetTotal = gross;
            }
            else if (!isImortOrSez)
            {
                er.NetTotal = gross + er.Sgst + er.Cgst + er.Igst + er.Cess;
            }

            gridView1.UpdateCurrentRow();

            FinalTotal();
        }
        private void FinalTotal()
        {
            if (IsLoadData) return;
            var Trans = grnTransDtoBindingSource1.DataSource as List<ExpTransDto>;
            if (Trans == null) return;
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


            if (tcsPerTextEdit.Value > 0) // tcs applicable
            {
                if (GenExpPara.Tcs_Round_Off)
                    tcsAmtTextEdit.Value = decimal.Round((ntotal * tcsPerTextEdit.Value / 100) + (decimal)0.01);
                else
                    tcsAmtTextEdit.Value = decimal.Round(ntotal * tcsPerTextEdit.Value / 100, 2);
            }

            ntotal = ntotal + tcsAmtTextEdit.Value;

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
            paybleTextEdit.Text = (ntotal - (tdsAmtTextEdit.Value + Convert.ToDecimal(colTdsAmt.SummaryItem.SummaryValue))).ToString("F");

        }
        private void SetParameter()
        {
            using (var db = new KontoContext())
            {
                var _paralists = db.CompParas.Include("SysPara")
                              .Where(x => x.SysPara.Category == "GenExp" && x.CompId == KontoGlobals.CompanyId)
                             .ToList();

                foreach (var item in _paralists)
                {
                    var value = item.ParaValue;
                    switch (item.ParaId)
                    {

                        case 147:
                            {
                                GenExpPara.Freight_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 148:
                            {
                                GenExpPara.Tcs_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 149:
                            {
                                GenExpPara.Tds_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 150:
                            {
                                GenExpPara.TDS_RoundOff = (value == "Y") ? true : false;
                                break;
                            }
                        case 151:
                            {
                                GenExpPara.Tax_RoundOff = (value == "Y") ? true : false;
                                break;
                            }
                        case 212:
                            {

                                if (!string.IsNullOrEmpty(value) && Convert.ToInt32(value) >= 2 && Convert.ToInt32(value) <= 4)
                                    GenExpPara.Rate_Decimal = Convert.ToInt32(value);
                                break;
                            }
                        case 213:
                            {

                                if (!string.IsNullOrEmpty(value) && Convert.ToInt32(value) >= 2 && Convert.ToInt32(value) <= 3)
                                    GenExpPara.Qty_Decimal = Convert.ToInt32(value);
                                break;
                            }

                        case 224:
                            {

                                GenExpPara.Tds_On_Line_Level = (value == "Y") ? true : false;
                                break;
                                
                            }
                    }
                }
            }

        }
        
        private void FillLookup()
        {

            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Regular","Regular"),
                new ComboBoxPairs("Import", "Import"),
                new ComboBoxPairs("Received from SEZ","Received from SEZ"),

            };
            invTypeLookUpEdit.Properties.DataSource = cbp;
            List<ComboBoxPairs> rbp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("NO", "NO"),
                new ComboBoxPairs("YES", "YES"),
            };
            rcmLookUpEdit.Properties.DataSource = rbp;
            
            List<ComboBoxPairs> ibp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Inputs", "Inputs"),
                new ComboBoxPairs("Capital Goods", "Capital Goods"),
                new ComboBoxPairs("Input Services", "Input Services"),
                new ComboBoxPairs("Inputs Ineligible", "Inputs Ineligible"),
                new ComboBoxPairs("Capital Goods Ineligible", "Capital Goods Ineligible"),
                new ComboBoxPairs("Input Services Ineligible", "Input Services Ineligible"),
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

                var _costlists = (from p in db.CostHeads
                                  where p.IsActive && !p.IsDeleted
                                  select new BaseLookupDto()
                                  {
                                      DisplayText = p.HeadName,
                                      Id = p.Id
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
                costLookUpEdi.Properties.DataSource = _costlists;
            }
        }

        private bool ValidateData()
        {
            var dt = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));

            var trans = grnTransDtoBindingSource1.DataSource as List<ExpTransDto>;
            var Sum = BillList.Sum(k => k.Amount);
            var Amount = trans.Sum(k => k.NetTotal);

            if ( string.IsNullOrEmpty(invTypeLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Invoice Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                invTypeLookUpEdit.Focus();
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
                MessageBoxAdv.Show(this, "Invalid Expense Ledger", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bookLookup.Focus();
                return false;
            }
            else if ( string.IsNullOrEmpty(billNoTextEdit.Text.Trim()))
            {
                MessageBoxAdv.Show(this, "Invalid Bill No.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                billNoTextEdit.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(rcmLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Rcm Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rcmLookUpEdit.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(itcLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Itc Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                itcLookUpEdit.Focus();
                return false;
            }
            else if (dt > KontoGlobals.ToDate || dt < KontoGlobals.FromDate)
            {
                MessageBoxAdv.Show(this, "Voucher date out of financial range", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherDateEdit.Focus();
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
            else if (trans.Any(x => x.Rate < 0))
            {
                MessageBoxAdv.Show(this, "Rate Can Not Be Less than zero", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }
            else if( tdsAmtTextEdit.Value > 0 && Convert.ToInt32(tdsAccLookup.SelectedValue) == 0)
            { 
                MessageBoxAdv.Show(this, "Tds Account Must be Selected", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tdsAmtTextEdit.Focus();
                return false;
            }
            else if (Convert.ToDecimal(colNetTotal.SummaryItem.SummaryValue) == 0)
            {
                MessageBoxAdv.Show(this, "Zero Total Not accepted", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }
            else if( trans.Any(x=>x.UomId == null || x.UomId == 0))
            {
                MessageBoxAdv.Show(this, "Empty unit not accepted", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }
            else if (trans.Any(x => x.BatchId == null || x.BatchId == 0))
            {
                MessageBoxAdv.Show(this, "Empty tax slab not accepted", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }
            else if (trans.Any(x => x.HsnCode.Trim().Length==0))
            {
                MessageBoxAdv.Show(this, "Empty hsn Code not valid", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }
            else if (Sum > 0 && Sum > Amount)
            {
                MessageBox.Show("Entry Not Possible. Adjusted Amount is more than Actual Amount");
                return false;
            }
            //check for duplicate bill no
            using (var db = new KontoContext())
            {

                bool result = LedgerEff.DataFreezeStatus(dt, (int)VoucherTypeEnum.GenExpense,db);
                if (result == false)
                {
                    MessageBox.Show(KontoGlobals.SaveFreezeWarning);
                    return false;
                }

                var accid = Convert.ToInt32(accLookup1.SelectedValue);
                var find1 = db.Bills.FirstOrDefault(
               x => x.AccId == accid && !x.IsDeleted && x.BillNo == billNoTextEdit.Text.Trim() && x.CompId == KontoGlobals.CompanyId 
               && x.YearId == KontoGlobals.YearId && x.Id!= this.PrimaryKey
               && x.TypeId ==(int) VoucherTypeEnum.GenExpense);

                if ( find1 != null)
                {
                    MessageBox.Show("Entered Bill No Already Exists for this Party");
                    billNoTextEdit.Focus();
                    return false;
                }

             

            }

            return true;
        }

        private void LoadData(BillModel model)
        {
           
            this.ResetPage();
            IsLoadData = true;
            this.PrimaryKey = model.Id;
            invTypeLookUpEdit.EditValue = model.BillType;
            rcmLookUpEdit.EditValue = model.Rcm;
            itcLookUpEdit.EditValue = model.Itc;
            voucherLookup1.SelectedValue = model.VoucherId;
            voucherLookup1.SetGroup(model.VoucherId);

            bookLookup.SelectedValue = model.BookAcId;
            bookLookup.SetAcc(Convert.ToInt32(model.BookAcId));
            voucherDateEdit.EditValue = KontoUtils.IToD(model.VoucherDate);
            voucherNoTextEdit.Text = model.VoucherNo;

            accLookup1.SetAcc(model.AccId);
            accLookup1.SelectedValue = model.AccId;
          
            challanNotextEdit.Text = model.RefNo;
            billNoTextEdit.Text = model.BillNo;
            billDateEdit.EditValue = model.RcdDate;
           
            if (Convert.ToInt32(model.EmpId) != 0)
            {
                empLookup1.SelectedValue = model.EmpId;
                empLookup1.SetGroup();
            }
            storeLookUpEdit.EditValue = model.StoreId;

            if (Convert.ToInt32(model.TransId) != 0)
            {
                transportLookup.SelectedValue = model.TransId;
                transportLookup.SetAcc((int)model.TransId);
            }
            lrNotextEdit.Text = model.DocNo;
            lrDateEdit.EditValue = model.DocDate;
            remarkTextEdit.Text = model.Remarks;

            if (Convert.ToInt32(model.HasteId) != 0)
            {
                tdsAccLookup.SelectedValue = model.HasteId;
                tdsAccLookup.SetAcc((int)model.HasteId);
            }

           
            createdLabelControl.Text = "Created By: " + model.CreateUser + " [ " + model.CreateDate + " ]";
            modifyLabelControl.Text = "Modified By: " + model.ModifyUser + " [ " + model.ModifyDate ?? string.Empty  + " ]";


            if(model.TdsAmt > 0)
            {
                colTdsPer.VisibleIndex = -1;
                colTdsAmt.VisibleIndex = -1;
                colParticular.VisibleIndex = -1;
                tdsAcLayoutControlItem.ContentVisible = true;
                tdsPerLayoutControlItem.ContentVisible = true;
                tdsAmtLayoutControlItem.ContentVisible = true;
            }

            if (model.CostHeadId != 0)
            {
                costLookUpEdi.EditValue = model.CostHeadId;
            }
            dueDaysTextEdit.Value = Convert.ToDecimal(model.Duedays);

            tcsPerTextEdit.Value = model.TcsPer;
            tcsAmtTextEdit.Value = model.TcsAmt;



            using (var _context = new KontoContext())
            {

                var _lst = (from bt in _context.BillTrans
                            join rb in _context.TaxMasters on bt.BatchId equals rb.Id into joinRb
                            from rb in joinRb.DefaultIfEmpty()
                            join um in _context.Uoms on bt.UomId equals um.Id into joinum
                            from um in joinum.DefaultIfEmpty()
                            join ac in _context.Accs on bt.TdsAcId equals ac.Id into joinac
                            from ac in joinac.DefaultIfEmpty()
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
                                Total = bt.Total,
                                TdsAcId = bt.TdsAcId,
                                TdsPer = bt.TdsPer,
                                TdsAmt = bt.TdsAmt,
                                Particular = ac.AccName  // tds account
                            }
                             ).ToList();

                grnTransDtoBindingSource1.DataSource = _lst;

                tdsPerTextEdit.Value = model.TdsPer;

                billAmtSpinEdit.Value = model.TotalAmount;
                roundoffSpinEdit.Value = Convert.ToDecimal(model.RoundOff);
                paybleTextEdit.EditValue = model.TotalAmount - model.TdsAmt;
                tdsAmtTextEdit.Value = model.TdsAmt;

                var paid = _context.BtoBs.Where(x => x.BillId == model.Id && x.BillVoucherId == model.VoucherId
                                            && !x.IsDeleted)
                                          .Sum(x => x.Amount);

                _TransFile = _context.Attachments.Where(x => x.RefVoucherId == model.Id && x.VoucherId == model.VoucherId && !x.IsDeleted).ToList();


                if (paid >0)
                {
                    okSimpleButton.Enabled = false;
                    if (model.TotalAmount-paid-model.TdsAmt == 0)
                        paidLabel.Text = "PAID";
                    else
                        paidLabel.Text = "PARTAIL-PAID";
                }
                else
                {
                    paidLabel.Text = "UN-PAID";
                }

                IsLoadData = false;
            }

           // FinalTotal();
            this.Text = "General Expense [View/Modify]";

        }
        private DataTable GetPurchaseTable()
        {
            using (var con = new SqlConnection(KontoGlobals.Conn))
            {

                using (var cmd = new SqlCommand("dbo.bill_analysis", con))
                {
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@fromDate", SqlDbType.Int).Value = KontoGlobals.FromDate;
                    cmd.Parameters.Add("@ToDate", SqlDbType.Int).Value = KontoGlobals.ToDate;
                    cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = KontoGlobals.CompanyId;
                    cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = KontoGlobals.BranchId;
                    cmd.Parameters.Add("@YearId", SqlDbType.Int).Value = KontoGlobals.YearId;
                    cmd.Parameters.Add("@VTypeId", SqlDbType.Int).Value = (int)VoucherTypeEnum.GenExpense;


                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    var DtCriterias = new DataTable();
                    DtCriterias.Load(cmd.ExecuteReader());
                    con.Close();
                    return DtCriterias;
                }
            }
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

            if (e.Column == colSgst || e.Column == colCgst || e.Column == colIgst)
                GridCalculation(er, true);
            else
                GridCalculation(er, false);

            if(e.Column == colTdsPer)
            {
                decimal gross = er.Total - er.DiscAmt+ er.Freight + er.OtherAdd - er.OtherLess;
                if (er.TdsPer > 0)
                {
                    er.TdsAmt = decimal.Round(gross * er.TdsPer / 100, 2);
                }

                if (GenExpPara.TDS_RoundOff)
                    er.TdsAmt = decimal.Round(er.TdsAmt + (decimal)0.01);
            }
            if (e.Column == colTdsPer || e.Column == colTdsAmt)
                FinalTotal();
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
                FinalTotal();
            }
            
        }

        private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var rw = gridView1.GetRow(e.RowHandle) as ExpTransDto;
            rw.Id = -1 * gridView1.RowCount;

            if(accLookup1.LookupDto!=null && GenExpPara.Tds_On_Line_Level && 
                    accLookup1.LookupDto.TdsReq.ToUpper() == "YES")
            {
                rw.TdsAcId = Convert.ToInt32(accLookup1.LookupDto.TdsAccId);
                rw.TdsPer = accLookup1.LookupDto.TdsPer;
                if (rw.TdsAcId > 0)
                {
                    using (var db = new KontoContext())
                    {
                        rw.Particular = db.Accs.Find(rw.TdsAcId).AccName;
                    }
                }
            }
        }

        private void GridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(accLookup1.SelectedValue) == 0) return;
               var dr = PreOpenLookup();
                if (dr == null) return;
                
                 if (gridView1.FocusedColumn.FieldName == "Particular")
                {

                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.TdsAcId == 0)
                        {
                            OpenAccLookup(dr.TdsAcId, dr);
                            // e.Handled = false;
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        OpenAccLookup(dr.TdsAcId, dr);
                       // e.Handled = true;
                    }
                    else if(e.KeyCode == Keys.Delete)
                    {
                        dr.TdsAcId = 0;
                        dr.Particular = string.Empty;
                        gridView1.UpdateCurrentRow();
                    }
                }



            }
            catch (Exception ex)
            {
                Log.Error(ex, "Gen Expense GridControl KeyDown");
                MessageBoxAdv.Show(this, "Error Lookup Setup !!", "Exception ", ex.ToString());

            }

        }

        private void OpenAccLookup(int _selvalue, ExpTransDto er)
        {
            var frm = new AccLkpWindow();
            frm.Tag = MenuId.Account;
            frm.SelectedValue = _selvalue;
            frm.VoucherType = VoucherTypeEnum.None;
            frm.TaxType = "TDS";
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                er.TdsAcId = frm.SelectedValue;
                er.Particular = frm.SelectedTex;
                gridView1.UpdateCurrentRow();
                //gridView1.FocusedColumn = gridView1.get .GetNearestCanFocusedColumn(gridView1.FocusedColumn);
            }

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

            
            dueDaysTextEdit.Text = this.accLookup1.LookupDto.CrDays.ToString();

            if (!GenExpPara.Tds_On_Line_Level && accLookup1.LookupDto.TdsReq.ToUpper() == "YES" && this.PrimaryKey==0)
            {
                tdsPerTextEdit.Value = accLookup1.LookupDto.TdsPer;

                if ( Convert.ToInt32(accLookup1.LookupDto.TdsAccId) > 0)
                {
                    tdsAccLookup.SelectedValue = accLookup1.LookupDto.TdsAccId;
                    tdsAccLookup.SetAcc(Convert.ToInt32(accLookup1.LookupDto.TdsAccId));
                }


            }

            if (this.PrimaryKey == 0)
            {
                if (this.accLookup1.LookupDto.TcsReq.ToUpper() == "YES")
                    tcsPerTextEdit.Value = accLookup1.LookupDto.TcsPer;
            }


            if (GenExpPara.Tds_On_Line_Level && accLookup1.LookupDto.TdsReq.ToUpper() == "YES")
            {
                colTdsPer.VisibleIndex = gridView1.VisibleColumns.Count-2;
                colTdsAmt.VisibleIndex = gridView1.VisibleColumns.Count-1;
                colParticular.VisibleIndex = gridView1.VisibleColumns.Count;
                tdsAcLayoutControlItem.ContentVisible = false;
                tdsPerLayoutControlItem.ContentVisible = false;
                tdsAmtLayoutControlItem.ContentVisible = false;
            }

            else if(accLookup1.LookupDto.TdsReq.ToUpper() == "YES")
            {
                colTdsPer.VisibleIndex = -1;
                colTdsAmt.VisibleIndex = -1;
                colParticular.VisibleIndex = -1;
                tdsAcLayoutControlItem.ContentVisible = true;
                tdsPerLayoutControlItem.ContentVisible = true;
                tdsAmtLayoutControlItem.ContentVisible = true;
            }
            else
            {
                colTdsPer.VisibleIndex = -1;
                colTdsAmt.VisibleIndex = -1;
                colParticular.VisibleIndex = -1;
                tdsAcLayoutControlItem.ContentVisible = false;
                tdsPerLayoutControlItem.ContentVisible = false;
                tdsAmtLayoutControlItem.ContentVisible = false;
            }


            //tcs
            if (this.accLookup1.LookupDto.TcsReq.ToUpper() == "YES")
            {
                if (tcsPerlayoutControlItem.IsHidden)
                    tcsPerlayoutControlItem.RestoreFromCustomization();

                if (tcsAmountlayoutControlItem.IsHidden)
                    tcsAmountlayoutControlItem.RestoreFromCustomization();

                tcsPerlayoutControlItem.ContentVisible = true;
                tcsAmountlayoutControlItem.ContentVisible = true;
            }
            else
            {
                tcsPerlayoutControlItem.ContentVisible = false;
                tcsAmountlayoutControlItem.ContentVisible = false;
            }


            UpdateGst();
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
                var _list = tabPageAdv2.Controls[0] as GenExpenseListView;
                _list.ActiveControl = _list.KontoGrid;
                this.Text = "General Expense [View]";
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new GenExpenseListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "General Expense [View]";

            }
            if (tabControlAdv1.SelectedIndex == 2)
            {
                if (tabPageAdv3.Controls.Count > 0) return;
                var _frm = new AnalysisUserControl(VoucherTypeEnum.GenExpense, true);
                _frm.AnaDataTable = GetPurchaseTable();
                _frm.Dock = DockStyle.Fill;
                tabPageAdv3.Controls.Add(_frm);
                this.Text = "Expense Anlysis";
            }
            if (tabControlAdv1.SelectedIndex == 3)
            {
                if (tabPageAdv4.Controls.Count > 0) return;
                var _frm = Activator.CreateInstance("Konto.Reporting", "Konto.Reporting.Para.BillPara.ParaMainView").Unwrap() as KontoForm;

                _frm.TopLevel = false;
                _frm.Parent = tabPageAdv4;
                _frm.ReportFilterType = "GEXPENSE";
                _frm.Location = new Point(tabPageAdv4.Location.X + tabPageAdv4.Width / 2 - _frm.Width / 2, tabPageAdv4.Location.Y + tabPageAdv4.Height / 2 - _frm.Height / 2);
                _frm.Show();// = true;

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

                Log.Error(ex, "General Expense Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }


        #region Parent Function

        public override void Print()
        {
            base.Print();
            try
            {
                MessageBoxAdv.Show("Not Implemented");
                return;
                if (this.PrimaryKey == 0) return;

                PageReport rpt = new PageReport();

                rpt.Load(new FileInfo("reg\\doc\\grn.rdlx"));

                rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.Conn;

                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);

                doc.Parameters["id"].CurrentValue = this.PrimaryKey;
                doc.Parameters["Ord"].CurrentValue = "N";
                doc.Parameters["reportid"].CurrentValue = 0;
                var frm = new KontoRepViewer(doc);
                frm.Text = "Purchase Order";
                var _tab = this.Parent.Parent as TabControlAdv;
                if (_tab == null) return;
                var pg1 = new TabPageAdv();
                pg1.Text = "Order Print";
                _tab.TabPages.Add(pg1);
                _tab.SelectedTab = pg1;
                frm.TopLevel = false;
                frm.Parent = pg1;
                frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                frm.Show();// = true;

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Grn print");
                MessageBoxAdv.Show(this, "Error While Print !!", "Exception ", ex.ToString());

            }
        }
        public override void NewRec()
        {
            base.NewRec();
            IsLoadData = false;
            this.FilterView = new List<BillModel>();
            this.Text = "Gen Expense [Add New]";
            rcmLookUpEdit.EditValue = "NO";
            invTypeLookUpEdit.EditValue = "Regular";
            itcLookUpEdit.EditValue = "Inputs";
            storeLookUpEdit.EditValue = 1;
            voucherNoTextEdit.Text = "New";
            voucherDateEdit.EditValue = DateTime.Now;
            billDateEdit.EditValue = DateTime.Now;
            empLookup1.SelectedValue = 1;
            empLookup1.SetGroup();
            costLookUpEdi.EditValue = 1;
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
            this.ActiveControl = voucherLookup1.buttonEdit1;
            voucherLookup1.SetDefault();
            paidLabel.Text = "UN-PAID";
            if (Convert.ToInt32(voucherLookup1.GroupDto.AccId) > 0)
            {
                bookLookup.SelectedValue = voucherLookup1.GroupDto.AccId;
                bookLookup.SetAcc(Convert.ToInt32(voucherLookup1.GroupDto.AccId));
            }

            DelTrans = new List<ExpTransDto>();
            
            DelBill = new List<PendBillListDto>();
            BillList = new List<PendBillListDto>();
            AllBill = new List<PendBillListDto>();
            _TransFile = new List<AttachmentModel>();
            _DelFile = new List<AttachmentModel>();

            this.grnTransDtoBindingSource1.DataSource = new List<ExpTransDto>();
            
        }
        public override void ResetPage()
        {
            base.ResetPage();
            IsLoadData = false;
            accLookup1.SetEmpty();
            bookLookup.SetEmpty();
            challanNotextEdit.Text = string.Empty;
            billNoTextEdit.Text = string.Empty;
            voucherDateEdit.DateTime = DateTime.Now;
            billDateEdit.DateTime = DateTime.Now;
            voucherNoTextEdit.Text = string.Empty;
            costLookUpEdi.EditValue = 1;
            transportLookup.SetEmpty();
            empLookup1.SetEmpty();
            lrNotextEdit.Text = string.Empty;
            lrDateEdit.EditValue = DateTime.Now;
            remarkTextEdit.Text = string.Empty;
            tdsAccLookup.SetEmpty();
            tdsPerTextEdit.Value = 0;
            tdsAmtTextEdit.Value = 0;
            paybleTextEdit.Text = "0";
            tcsPerTextEdit.Value = 0;
            tcsAmtTextEdit.Value = 0;
            roundoffSpinEdit.Value = 0;
            billAmtSpinEdit.Value = 0;
            dueDaysTextEdit.Value = 0;
            DelTrans = new List<ExpTransDto>();
            DelBill = new List<PendBillListDto>();
            BillList = new List<PendBillListDto>();
            AllBill = new List<PendBillListDto>();
            _TransFile = new List<AttachmentModel>();
            _DelFile = new List<AttachmentModel>();

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

                       if(! UpdateBill(db, _find))
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

                        //attachment
                        foreach (var item in _TransFile)
                        {

                            item.RefVoucherId = _find.Id;
                            item.VoucherId = _find.VoucherId;
                            if (item.Id == 0)
                            {
                                string fpath = item.FilePath;// uploadedFile.FileNameInStorage.ToString(); 
                                int lst = fpath.LastIndexOf(".");
                                string xtn = fpath.Substring(lst);

                                var _file = string.Format(@"{0}." + xtn, DateTime.Now.Ticks + "__" + _find.Id);

                                var _filePath = "\\attachment\\" + _file;

                                string destFile = (System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)) + _filePath;
                                System.IO.File.Copy(fpath, destFile, true);

                                item.FilePath = _file;

                                db.Attachments.Add(item);
                            }
                            else
                            {
                                var _at = db.Attachments.Find(item.Id);
                                _at.FileDescr = item.FileDescr;
                                // _at.FilePath = item.FilePath;
                            }
                        }
                        
                        // delete from Attachment
                        foreach (var item in _DelFile)
                        {
                            if (item.Id == 0) continue;
                            var _model = db.Attachments.Find(item.Id);
                            _model.IsDeleted = true;

                        }

                        //delete item from  trans
                        foreach (var item in DelTrans)
                        {
                            if (item.Id <= 0) continue;
                            var _model = db.BillTrans.Find(item.Id);
                            _model.IsDeleted = true;
                        }

                        //Bill Reference Update
                         LedgerEff.BillRefEntry("Credit",_find,0,db);       //Insert or update in Billref table

                        //Insert or update in LedgerTrans table
                        LedgerEff.LedgerTransEntry("Credit", _find, db, Trans);

                        // Insert in BtoB for BillAdjustment
                         LedgerEff.BtoBEntry("Payment", _find.Id, _find, db, BillList);

                        //if (this.PrimaryKey == 0)
                        //    DbUtils.UsedSerial(_find.VoucherId, _SerialValue, db);

                        db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Gen Expense" +" Save");
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
           
            model.BillType = invTypeLookUpEdit.EditValue.ToString();
            model.Rcm = rcmLookUpEdit.EditValue.ToString();
            model.Itc = itcLookUpEdit.EditValue.ToString();

        
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
            model.TransId = Convert.ToInt32(transportLookup.SelectedValue);
            model.DocNo = lrNotextEdit.Text.Trim();
            model.DocDate = Convert.ToDateTime(lrDateEdit.EditValue);
            model.TypeId = (int)VoucherTypeEnum.GenExpense;
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

            model.TcsPer = tcsPerTextEdit.Value;
            model.TcsAmt = tcsAmtTextEdit.Value;
            model.Duedays = Convert.ToInt32(dueDaysTextEdit.Value);

            if (!string.IsNullOrEmpty(costLookUpEdi.Text))
            {
                model.CostHeadId = Convert.ToInt32(costLookUpEdi.EditValue);
            }


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

        private void attachSimpleButton_Click(object sender, EventArgs e)
        {
            var frm = new AttachmentView();
            frm.Trans = this._TransFile;
            frm.DelTrans = this._DelFile;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this._TransFile = frm.Trans;
                this._DelFile = frm.DelTrans;
            }
        }
    }
}
