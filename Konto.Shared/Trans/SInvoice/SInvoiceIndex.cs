﻿using AutoMapper;
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
using Konto.Data.Models.Masters;
using Konto.Data.Models.Masters.Dtos;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using Konto.Shared.Account;
using Konto.Shared.Masters.Color;
using Konto.Shared.Masters.Design;
using Konto.Shared.Masters.Grade;
using Konto.Shared.Masters.Item;
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

namespace Konto.Shared.Trans.SInvoice
{
    public partial class SInvoiceIndex : KontoMetroForm
    {
        private List<BillModel> FilterView = new List<BillModel>();
        private List<BillTransDto> DelTrans = new List<BillTransDto>();
        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        private bool isImortOrSez = false;
       
        private int compStateId = 24;
        private bool isGst = true;
        private decimal _DiscPer = 0;
        private List<PendBillListDto> DelBill = new List<PendBillListDto>();
        private List<PendBillListDto> BillList = new List<PendBillListDto>();
        private List<PendBillListDto> AllBill = new List<PendBillListDto>();
        private bool IsLoadData = false;

        private List<SerialBatchDto> Serials = new List<SerialBatchDto>();
        private List<SerialBatchDto> DelSerials = new List<SerialBatchDto>();

        public SInvoiceIndex()
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
            gridView1.ShowingEditor += GridView1_ShowingEditor;
            productRepositoryItemButtonEdit.ButtonClick += ProductRepositoryItemButtonEdit_ButtonClick;
            colorRepositoryItemButtonEdit.ButtonClick += ColorRepositoryItemButtonEdit_ButtonClick;
            gradeRepositoryItemButtonEdit.ButtonClick += GradeRepositoryItemButtonEdit_ButtonClick;
            designRepositoryItemButtonEdit.ButtonClick += DesignRepositoryItemButtonEdit_ButtonClick;
            gridView1.DoubleClick += GridView1_DoubleClick;
            this.MainLayoutFile = KontoFileLayout.Sales_Index;
            this.GridLayoutFile = KontoFileLayout.Sales_Trans;
           
            this.invTypeLookUpEdit.EditValueChanged += InvTypeLookUpEdit_EditValueChanged;
            this.accLookup1.ShownPopup += AccLookup1_ShownPopup;
            FillLookup();
            SetParameter();
           

            headerEdit.Hide();
            headerEdit.Parent = this.gridControl1;
            headerEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            headerEdit.Leave += new EventHandler(headerEdit_Leave);
            billAdjustSimpleButton.Click += BillAdjustSimpleButton_Click;
            delvLookup.SelectedValueChanged += DelvLookup_SelectedValueChanged;
            addressLookup1.SelectedValueChanged += AddressLookup1_SelectedValueChanged;
            stateLookUpEdit.EditValueChanged += StateLookUpEdit_EditValueChanged;
            
            voucherLookup1.SelectedValueChanged += VoucherLookup1_SelectedValueChanged;
            tcsPerTextEdit.EditValueChanged += TcsPerTextEdit_EditValueChanged;
            tcsAmtTextEdit.EditValueChanged += TcsAmtTextEdit_EditValueChanged;
            this.Shown += SInvoiceIndex_Shown;

            this.FirstActiveControl = voucherLookup1;
            this.voucherDateEdit.EditValueChanged += VoucherDateEdit_EditValueChanged;

        }

        private void VoucherDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (this.PrimaryKey != 0) return;
            rcdDateEdit1.EditValue = voucherDateEdit.EditValue;
        }

        private void TcsAmtTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            FinalTotal();
        }

        private void TcsPerTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            FinalTotal();
        }

        private void AddressLookup1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (addressLookup1.LookupDto == null) return;
            stateLookUpEdit.EditValue = addressLookup1.LookupDto.StateId;
        }

        private void SInvoiceIndex_Shown(object sender, EventArgs e)
        {
           // colSgst.OptionsColumn.AllowFocus = true;
            colSgstPer.OptionsColumn.AllowFocus = true;
            //colCgst.OptionsColumn.AllowFocus = true;
            colCgstPer.OptionsColumn.AllowFocus = true;
           // colIgst.OptionsColumn.AllowFocus = true;
            colIgstPer.OptionsColumn.AllowFocus = true;
            SetGridColumn();

            colProductName.OptionsColumn.ReadOnly = true;

            if (!BillPara.Party_Wise_Challan)
            {
                GetPendingChallan(0);
            }
        }

        private void VoucherLookup1_SelectedValueChanged(object sender, EventArgs e)
        {
            if(this.PrimaryKey == 0 && Convert.ToInt32(voucherLookup1.SelectedValue) > 0 )
            {
                
                using (var db = new KontoContext())
                {
                    voucherNoTextEdit.Text = "New-" +  DbUtils.NextSerialNo(Convert.ToInt32(voucherLookup1.SelectedValue), db,1);
                }
                if (voucherLookup1.GroupDto != null && Convert.ToInt32(voucherLookup1.GroupDto.AccId) > 0)
                {
                    bookLookup.SelectedValue = voucherLookup1.GroupDto.AccId;
                    bookLookup.SetAcc(Convert.ToInt32(voucherLookup1.GroupDto.AccId));
                }
            }

            if(voucherLookup1.GroupDto!=null && voucherLookup1.GroupDto.ManualSeries)
            {
                voucherNoTextEdit.Enabled = true;
            }
            else
            {
                voucherNoTextEdit.Enabled = false;
            }
        }

        private void StateLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(stateLookUpEdit.Text)) return;

            var _stid = Convert.ToInt32(stateLookUpEdit.EditValue);
            if (compStateId != _stid)
                isGst = false;
            else
                isGst = true;

            UpdateGst();
        }

        private void DelvLookup_SelectedValueChanged(object sender, EventArgs e)
        {
            if (delvLookup.LookupDto == null) return;
            //if (this.PrimaryKey == 0)
           // {
                //addressLookup1.sel
                addressLookup1.SetValue(this.delvLookup.LookupDto.AddressId);
                addressLookup1.SelectedValue = this.delvLookup.LookupDto.AddressId;
                stateLookUpEdit.EditValue = delvLookup.LookupDto.StateId;
           // }
           
        }

        private void BillAdjustSimpleButton_Click(object sender, EventArgs e)
        {
            string type = "CREDIT";
            if (this.billAmtSpinEdit.Value == 0) return;
            var frm = new PendingBillViewWindow("SALE", Convert.ToInt32(accLookup1.SelectedValue),
                (int)VoucherTypeEnum.SaleInvoice, type, this.PrimaryKey, this.PrimaryKey,
                (int)voucherLookup1.SelectedValue);

            frm.AllBill.AddRange(this.AllBill);
            frm.TotalAmount = this.billAmtSpinEdit.Value;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.AllBill = frm.AllBill;

                this.DelBill.AddRange(frm.DelBillList);

                var plist = frm.BillList.Where(k => k.Amount > 0).ToList();

                this.BillList.AddRange(plist);

                //if (plist.Count > 0)
                //{
                //    billNoTextEdit.Text = plist[0].BillNo;
                //    billDateEdit.EditValue = plist[0].ChallanDate;
                //}

            }
        }

        private void AccLookup1_ShownPopup(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(accLookup1.SelectedValue) == 0 || this.PrimaryKey != 0) return;
                
                if (BillPara.Order_Required)
                {
                    GetOrderPending(accLookup1.LookupDto);
                    return;
                }
                GetPendingChallan(accLookup1.LookupDto.Id);
                
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Sales account popup");
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void GetPendingChallan(int accid)
        {
            var grnfrm = new PendingGrnForPurchase();
            grnfrm.VoucherType = VoucherTypeEnum.SalesChallan;
            grnfrm.ChallanType = ChallanTypeEnum.SALES_CHALLAN;
            grnfrm.ChallanTypeId = "6,9";
            grnfrm.AccId = accid;
            grnfrm.Text = "Select Pending Challan";
            if (grnfrm.ShowDialog() != DialogResult.OK) return;

            Int32[] selectedRowHandles = grnfrm.SelectedRows;
            if (selectedRowHandles == null || selectedRowHandles.Count() == 0) return;

            List<BillTransDto> transDtos = new List<BillTransDto>();

            var db = new KontoContext();
            string bill = string.Empty, ordr = string.Empty;

            // if not party wise pending
            if (selectedRowHandles.Length > 0 && !BillPara.Party_Wise_Challan)
            {
                var row = grnfrm.gridView1.GetRow(selectedRowHandles[0]) as PendingChallanOnInvoiceDto;
                if (row != null)
                {
                    accLookup1.SetAcc(row.AccId);
                    accLookup1.SelectedValue = row.AccId;
                    challanNotextEdit.Text = row.ChallanNo;
                    rcdDateEdit1.DateTime = row.ChallanDate;
                    
                }
            }

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
                    ct.LotNo = ch.LotNo;
                    ct.DesignId = Convert.ToInt32(ch.DesignId);
                    ct.ColorId = Convert.ToInt32(ch.ColorId);
                    ct.GradeId = Convert.ToInt32(ch.GradeId);
                    ct.DesignName = ch.DesignNo;
                    ct.ColorName = ch.ColorName;
                    ct.GradeName = ch.GradeName;
                    if (ch.ChallanNo != null && !bill.Contains(ch.ChallanNo))
                    {
                        if (string.IsNullOrEmpty(bill))
                            bill = ch.ChallanNo;
                        else
                            bill = bill + "," + ch.ChallanNo;
                    }
                    if (ch.OrderNO != null && !ordr.Contains(ch.OrderNO))
                    {
                        if (string.IsNullOrEmpty(ordr))
                            ordr = ch.OrderNO;
                        else
                            ordr = ordr + "," + ch.OrderNO;
                    }
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

                    ct.Total = Convert.ToDecimal(ch.Total);
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
                    if (BillPara.Tax_RoundOff)
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

                    if (isImortOrSez)
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
            refNoTextEdit.Text = ordr;
            challanNotextEdit.Text = bill;
            grnTransDtoBindingSource1.DataSource = transDtos;
            FinalTotal();
        }
        private void GetOrderPending(AccLookupDto dto )
        {
            var ordfrm = new PendingOrderView();
            ordfrm.VoucherType = VoucherTypeEnum.SalesOrder;
            ordfrm.AccId = Convert.ToInt32(accLookup1.SelectedValue);
            if (ordfrm.ShowDialog() != DialogResult.OK) return;

            Int32[] selectedRowHandles = ordfrm.SelectedRows;
            if (selectedRowHandles == null || selectedRowHandles.Count() == 0) return;
            List<BillTransDto> transDtos = new List<BillTransDto>();
            int id = 0;
            string ordr = string.Empty;
            foreach (var item in selectedRowHandles)
            {
                var ch = ordfrm.gridView1.GetRow(item) as PendingOrderDto;
                BillTransDto ct = new BillTransDto();
                id = id - 1;

                ct.ProductId = Convert.ToInt32(ch.ProductId);
                ct.ProductName = ch.Product;
                ct.Pcs = ch.TotalPcs != null ? (int)ch.TotalPcs : 0;
                ct.Qty = ch.PendingQty != null ? (decimal)ch.PendingQty : 0;
                ct.Rate = ch.rate != null ? (decimal)ch.rate : 0;
                ct.Disc = ch.Disc != null ? (decimal)ch.Disc : 0;
                ct.DiscAmt = ch.DiscAmt != null ? (decimal)ch.DiscAmt : 0;
                ct.UomId = ch.UomId;
               // ct.LotNo = ch.LotNo;
                ct.DesignId = Convert.ToInt32(ch.DesignId);
                ct.ColorId = Convert.ToInt32(ch.ColorId);
                ct.GradeId = Convert.ToInt32(ch.GradeId);
                ct.DesignName = ch.DesignNo;
                ct.ColorName = ch.ColorName;
                ct.GradeName = ch.GradeName;
               
                if (ch.VoucherNo != null && !ordr.Contains(ch.VoucherNo))
                {
                    if (string.IsNullOrEmpty(ordr))
                        ordr = ch.VoucherNo;
                    else
                        ordr = ordr + "," + ch.VoucherNo;
                }
                
                
                ct.RefId = ch.Id;
                ct.RefTransId = ch.TransId;
                ct.RefVoucherId = ch.VoucherId;
                ct.OrderNo = ch.VoucherNo;
                ct.OrderDate = ch.VouchDate;
                //ct.OrdId = ch.OrdId;
                
                ct.Total = Convert.ToDecimal(ch.Total);
                ct.DiscAmt = ct.DiscAmt;

                if (accLookup1.LookupDto.IsGst && !isImortOrSez)
                {
                    ct.SgstPer = ch.Sgst != null ? (decimal)ch.Sgst : 0;
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

                if (BillPara.Tax_RoundOff)
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

                if (isImortOrSez)
                {
                    ct.NetTotal = gross;
                }
                else if (!isImortOrSez)
                {
                    ct.NetTotal = gross + ct.Sgst + ct.Cgst + ct.Igst + ct.Cess; // ct er.CessAmt; 
                }
                transDtos.Add(ct);
            }
            refNoTextEdit.Text = ordr;
            grnTransDtoBindingSource1.DataSource = transDtos;
            FinalTotal();
        }

        private void GridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        private void GridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            GridView view = sender as GridView;
           
          
            int product = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, colProductId));
            int unit = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, colUomId));
            decimal qty = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, colQty));
            decimal rate = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, colRate));

            if (product == 0)
            {
                e.Valid = false;
                view.SetColumnError(colProductName, "Invalid Product");
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
            //else if (rate == 0)
            //{
            //    e.Valid = false;
            //    view.SetColumnError(colRate, "Invalid Rate");
            //}
        }


        private void InvTypeLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (invTypeLookUpEdit.EditValue.ToString() == "Regular" || invTypeLookUpEdit.EditValue.ToString() == "Sale from Bonded WH")
            {

                isImortOrSez = false;
            }
            else
                isImortOrSez = true;


            UpdateGst();
        }


        #region UDF
        private void UpdateGst()
        {
            if (isGst && !isImortOrSez)
            {
                colSgst.Visible = true;
                colSgstPer.Visible = true;
                colCgst.Visible = true;
                colCgstPer.Visible = true;
                colIgst.Visible = false;
                colIgstPer.Visible = false;
                gridView1.BeginDataUpdate();
                for (int i = 0; i < gridView1.RowCount - 1; i++)
                {
                    var rw = gridView1.GetRow(i) as BillTransDto;
                    var gst = rw.Igst + rw.Cgst + rw.Sgst;
                    var gstPer = rw.IgstPer + rw.CgstPer + rw.SgstPer;
                    rw.Igst = 0;
                    rw.IgstPer = 0;
                    rw.Sgst = gst / 2;
                    rw.Cgst = gst / 2;
                    rw.SgstPer = gstPer / 2;
                    rw.CgstPer = gstPer / 2;

                }
                gridView1.EndDataUpdate();
            }
            else
            {
                colSgst.Visible = false;
                colSgstPer.Visible = false;
                colCgst.Visible = false;
                colCgstPer.Visible = false;
                colIgst.Visible = true;
                colIgstPer.Visible = true;
                
                gridView1.BeginDataUpdate();
                for (int i = 0; i < gridView1.RowCount - 1; i++)
                {
                    var rw = gridView1.GetRow(i) as BillTransDto;
                    var gst = rw.Igst + rw.Cgst + rw.Sgst;
                    var gstPer = rw.IgstPer + rw.CgstPer + rw.SgstPer;
                    rw.IgstPer = gstPer;
                    rw.Igst = gst;
                    rw.Sgst = 0;
                    rw.Cgst = 0;
                    rw.SgstPer = 0;
                    rw.CgstPer = 0;
                }
                gridView1.EndDataUpdate();
            }
        }
        private void OpenItemLookup(int _selvalue, BillTransDto er)
        {
            var frm = new ProductLkpWindow();
            frm.Tag = MenuId.Product_Master;
            frm.SelectedValue = _selvalue;
            //frm.PTypeId = ProductTypeEnum.;
            frm.VoucherType = VoucherTypeEnum.SaleInvoice;

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                er.ProductId = frm.SelectedValue;
                er.ProductName = frm.SelectedTex;
                var model = frm.SelectedItem as ProductLookupDto;
                er.UomId = model.UomId;
                er.HsnCode = model.HsnCode;
                er.RatePerQty = model.RatePerQty;
                er.Barcode = model.BarCode;

                if (accLookup1.LookupDto.RateType == "BLK")
                {
                    er.SaleRate = model.Rate1;
                }
                else if (accLookup1.LookupDto.RateType == "SMBLK")
                    er.SaleRate = model.Rate2;
                else
                    er.SaleRate =  model.SaleRate;

                if((int)accLookup1.LookupDto.GroupId == (int) LedgerGroupEnum.SUNDRY_DEBTORS)
                {
                    er.FreightRate = BillPara.Default_Freight_Rate;
                }
                

                if (model.SaleRateTaxInc)
                {
                    

                    //er.SaleRate = model.SaleRate;
                    var rt = decimal.Round((model.SaleRate * 100) / (100 + (model.Sgst + model.Cgst)), 2, MidpointRounding.AwayFromZero);
                    er.Rate = rt;
                }
                else
                {

                    er.Rate = er.SaleRate;
                    er.SaleRate = decimal.Round((model.SaleRate + (model.SaleRate * model.Igst / 100)), 2, MidpointRounding.AwayFromZero);
                    
                }

                er.Cut = model.Cut;
                
                er.Disc = _DiscPer;
                
                if (er.Disc == 0)
                    er.Disc = model.SaleDisc;

                if (isGst)
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

                if (colBarcode.Visible)
                    gridView1.FocusedColumn = gridView1.GetVisibleColumn(colProductName.VisibleIndex + 1);
                else
                    gridView1.FocusedColumn = gridView1.GetNearestCanFocusedColumn(colProductName);

            }
            GridCalculation(er);
        }
        private void OpenColorLookup(int _selvalue, BillTransDto er)
        {
            var frm = new ColorLkpWindow();
            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Color;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                gridView1.BeginDataUpdate();
                er.ColorId = frm.SelectedValue;
                er.ColorName = frm.SelectedTex;
                gridView1.EndDataUpdate();
                gridView1.FocusedColumn = gridView1.GetVisibleColumn(colColorName.VisibleIndex + 1);
            }

        }
        private void OpenGradeLookup(int _selvalue, BillTransDto er)
        {
            var frm = new GradeLkpWindow();
            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Grade;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                gridView1.BeginDataUpdate();
                er.GradeId = frm.SelectedValue;
                er.GradeName = frm.SelectedTex;
                gridView1.EndDataUpdate();
                gridView1.FocusedColumn = gridView1.GetVisibleColumn(colGradeName.VisibleIndex + 1);
            }

        }
        private void OpenDesignLookup(int _selvalue, BillTransDto er)
        {
            var frm = new DesignLkpWindow();
            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Design_Master;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                gridView1.BeginDataUpdate();
                er.DesignId = frm.SelectedValue;
                er.DesignName = frm.SelectedTex;
                gridView1.EndDataUpdate();
                gridView1.FocusedColumn = gridView1.GetVisibleColumn(colDesignName.VisibleIndex + 1);
            }

        }
        private void SetGridColumn()
        {

            colFreight.Visible = BillPara.Freight_Required;
            colFreightRate.Visible = BillPara.Freight_Required;
            colColorName.Visible = BillPara.Color_Required;
            colDesignName.Visible = BillPara.Design_Required;
            colGradeName.Visible = BillPara.Grade_Required;
            colCut.Visible = BillPara.Cut_Required;
            colHsnCode.Visible = BillPara.HsnCode_Required;

            colBarcode.Visible = BillPara.Barcode_Required;
            colOtherAdd.Visible = BillPara.OtherAdd_Required;
            colOtherLess.Visible = BillPara.OtherLess_Required;
            colCess.Visible = BillPara.Cess_Required;
            colCessPer.Visible = BillPara.Cess_Required;

            if (BillPara.Barcode_Required)
            {
                colProductName.VisibleIndex = -1;
                
                colBarcode.VisibleIndex = 0;
                colBarcode.Fixed = FixedStyle.Left;
                colProductName.VisibleIndex = 1;
            }
            else
            {
                colProductName.VisibleIndex = 0;
            }

            //Rate Decimal Settings
            var repo1 = new RepositoryItemTextEdit();
            repo1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            repo1.Mask.EditMask = "n" + BillPara.Rate_Decimal.ToString();
            colRate.ColumnEdit = repo1;
            colRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colRate.DisplayFormat.FormatString = "n" + BillPara.Rate_Decimal.ToString();

            //Qty Decimal Settings
            repositoryItemTextEdit1.Mask.EditMask = "n" + BillPara.Qty_Decimal.ToString();
            colQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colQty.DisplayFormat.FormatString = "n" + BillPara.Qty_Decimal.ToString();


        }
        private BillTransDto PreOpenLookup()
        {
            if (Convert.ToInt32(accLookup1.SelectedValue) == 0) return null;
            gridView1.GetRow(gridView1.FocusedRowHandle);
            if (gridView1.GetRow(gridView1.FocusedRowHandle) == null)
            {
                gridView1.AddNewRow();
            }
            var dr = (BillTransDto)gridView1.GetRow(gridView1.FocusedRowHandle);
            return dr;
        }
      
        public void GridCalculation(BillTransDto er, string fldName="NA")
        {
            if (fldName == "SaleRate")
            {
                er.Rate = decimal.Round((er.SaleRate * 100) / (100 + (er.SgstPer + er.CgstPer + er.IgstPer)), 2, MidpointRounding.AwayFromZero);
            }
            
            else if (fldName == "Rate")
            {
                er.SaleRate = decimal.Round((er.Rate + (er.Rate * (er.SgstPer + er.CgstPer + er.IgstPer) / 100)), 2, MidpointRounding.AwayFromZero);
            }


            if (er.Cut > 0 && er.Pcs > 0)
                er.Qty = decimal.Round(er.Pcs * er.Cut, 2, MidpointRounding.AwayFromZero);


            if(er.RatePerQty >0 )

                er.Total = decimal.Round( er.Qty * er.Rate/er.RatePerQty, 2);
            else
                er.Total = decimal.Round(er.Qty * er.Rate, 2);


            var uom = uomRepositoryItemLookUpEdit.GetDataSourceRowByKeyValue(er.UomId) as UomLookupDto;

            if (uom != null && uom.RateOn == "N" && er.Pcs > 0)
            {
                if (er.RatePerQty > 0)
                    er.Total = decimal.Round(er.Pcs * er.Rate / er.RatePerQty, 2, MidpointRounding.AwayFromZero);
                else
                    er.Total = decimal.Round(er.Pcs * er.Rate, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                if (er.RatePerQty > 0)
                    er.Total = decimal.Round(er.Qty * er.Rate/er.RatePerQty, 2, MidpointRounding.AwayFromZero);
                else
                    er.Total = decimal.Round(er.Qty * er.Rate, 2, MidpointRounding.AwayFromZero);
            }

            decimal gross = 0;

            if (BillPara.Use_OtherLess_As_RateDiff)
                gross = er.Total - er.Qty * er.OtherLess;
            else
                gross = er.Total;


            if (er.Disc > 0)
                er.DiscAmt = decimal.Round(gross * er.Disc / 100, 2, MidpointRounding.AwayFromZero);
            
               gross= gross - er.DiscAmt;


            if (er.FreightRate > 0 && fldName != "Freight")
            {
                if(BillPara.Freight_On_Qty)
                    er.Freight = decimal.Round(er.Qty * er.FreightRate, 2, MidpointRounding.AwayFromZero);
                else
                    er.Freight = decimal.Round(er.Pcs * er.FreightRate, 2, MidpointRounding.AwayFromZero);
            }

            if (!BillPara.Use_OtherLess_As_RateDiff)
                gross = gross + er.Freight + er.OtherAdd - er.OtherLess;
            else
                gross = gross + er.Freight + er.OtherAdd;


            if (BillPara.Tax_RoundOff)
            {
                if(fldName!="Sgst" && fldName!="Cgst")
                    er.Sgst = decimal.Round(gross * er.SgstPer / 100, 0, MidpointRounding.AwayFromZero);
                if (fldName != "Sgst" && fldName != "Cgst")
                    er.Cgst = decimal.Round(gross * er.CgstPer / 100, 0, MidpointRounding.AwayFromZero);
                if (fldName != "Igst")
                    er.Igst = decimal.Round(gross * er.IgstPer / 100, 0, MidpointRounding.AwayFromZero);
            }
            else
            {
                if (fldName != "Sgst" && fldName != "Cgst")
                    er.Sgst = decimal.Round(gross * er.SgstPer / 100, 2, MidpointRounding.AwayFromZero);
                if (fldName != "Sgst" && fldName != "Cgst")
                    er.Cgst = decimal.Round(gross * er.CgstPer / 100, 2, MidpointRounding.AwayFromZero);
                if (fldName != "Igst")
                    er.Igst = decimal.Round(gross * er.IgstPer / 100, 2, MidpointRounding.AwayFromZero);
            }


            if (er.CessPer > 0 &&  fldName != "Cess")
                er.Cess = decimal.Round(er.Qty * er.CessPer, 2, MidpointRounding.AwayFromZero);

            if ( isImortOrSez)
            {
                er.NetTotal = gross;
            }
            else if(!isImortOrSez)
            {
                er.NetTotal = gross + er.Sgst + er.Cgst + er.Igst;
            }
            gridView1.UpdateCurrentRow();

            FinalTotal();
        }
        private void FinalTotal()
        {
            if (IsLoadData) return;
            var Trans = grnTransDtoBindingSource1.DataSource as List<BillTransDto>;
            var gross = Trans.Sum(x => x.NetTotal) - Trans.Sum(x => x.Cgst) - Trans.Sum(x => x.Sgst) -
                Trans.Sum(x => x.Igst) - Trans.Sum(x => x.Cess);

          
            gridView1.UpdateTotalSummary();
            var ntotal = Convert.ToDecimal(colNetTotal.SummaryItem.SummaryValue);


            if(tcsPerTextEdit.Value > 0) // tcs applicable
            {
                if (BillPara.Tcs_Round_Off)
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
           


        }
        private void 
            SetParameter()
        {
            using (var db = new KontoContext())
            {
                var _paralists = db.CompParas.Include("SysPara")
                              .Where(x => x.SysPara.Category == "SaleInvoice" && x.CompId == KontoGlobals.CompanyId)
                             .ToList();

                foreach (var item in _paralists)
                {
                    var value = item.ParaValue;
                    switch (item.ParaId)
                    {

                        case 59:
                            {
                                BillPara.Color_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 60:
                            {
                                BillPara.Grade_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 61:
                            {
                                BillPara.Design_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 62:
                            {
                                BillPara.Cut_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 63:
                            {
                                BillPara.Freight_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 64:
                            {
                                BillPara.Tcs_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 136:
                            {
                                BillPara.Default_Invoice_Print = value;
                                break;
                            }
                        case 174:
                            {
                                BillPara.Allow_Duplicate_Order_Ecommerce = (value == "Y") ? true : false;
                                break;
                            }
                        case 178:
                            {
                                BillPara.Ask_For_Voucher_Selection = (value == "Y") ? true : false;
                                break;
                            }
                        case 216:
                            {

                                if (!string.IsNullOrEmpty(value) && Convert.ToInt32(value) >= 2 && Convert.ToInt32(value) <= 4)
                                    BillPara.Rate_Decimal = Convert.ToInt32(value);
                                break;
                            }
                        case 217:
                            {

                                if (!string.IsNullOrEmpty(value) && Convert.ToInt32(value) >= 2 && Convert.ToInt32(value) <= 3)
                                    BillPara.Qty_Decimal = Convert.ToInt32(value);
                                break;
                            }

                        case 225:
                            {
                                BillPara.Tcs_Round_Off = (value == "Y") ? true : false;
                                break;
                            }
                        case 236:
                            {
                                BillPara.Order_Required = (value == "Y") ? true : false;
                                break;
                            }

                        case 239:
                            {
                                BillPara.HsnCode_Required = (value == "Y") ? true : false;
                                break;
                            }

                        case 248:
                            {
                                BillPara.Use_OtherLess_As_RateDiff = (value == "Y") ? true : false;
                                break;
                            }
                        case 297:
                            {
                                BillPara.Party_Wise_Challan = (value == "Y") ? true : false;
                                break;
                            }
                        case 298:
                            {
                                BillPara.Barcode_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 304:
                        {
                            BillPara.OtherAdd_Required = (value == "Y") ? true : false;
                            break;
                        }
                        case 305:
                        {
                            BillPara.OtherLess_Required = (value == "Y") ? true : false;
                            break;
                        }
                        case 312:
                            {
                                BillPara.Freight_On_Qty = (value == "Y") ? true : false;
                                break;
                            }

                        case 313:
                            {
                                if (!string.IsNullOrEmpty(value))
                                    BillPara.Default_Freight_Rate = Convert.ToDecimal(value);
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
                new ComboBoxPairs("Regular", "Regular"),
                new ComboBoxPairs("SEZ Supplies with Payment", "SEZ Supplies with Payment"),
                new ComboBoxPairs("SEZ Supplies without Payment", "SEZ Supplies without Payment"),
                new ComboBoxPairs("Deemed Exp", "Deemed Exp"),
                new ComboBoxPairs("Sale from Bonded WH", "Sale from Bonded WH")

            };

            invTypeLookUpEdit.Properties.DataSource = cbp;
          

            using (var db = new KontoContext())
            {


                compStateId = (int) db.Companies.FirstOrDefault(x => x.Id == KontoGlobals.CompanyId).StateId;

                var _storeLists = (from p in db.Stores
                                 where p.IsActive && !p.IsDeleted
                                 select new BaseLookupDto()
                                 {
                                     DisplayText = p.StoreName,
                                     Id = p.Id
                                 }).ToList();
                var _statelist = (from p in db.States
                                  where p.IsActive && !p.IsDeleted
                                  select new BaseLookupDto()
                                  {
                                      DisplayText = p.StateName,
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

                //var gstList = (from p in db.TaxMasters
                //              where !p.IsDeleted && p.IsActive
                //              orderby p.TaxName
                //              select new TaxLookupDto
                //              {
                //                  DisplayText = p.TaxName,
                //                  Id = p.Id,
                //                  TaxType = p.TaxType,
                //                  Cgst = p.Cgst,
                //                  Sgst = p.Sgst,
                //                  Igst = p.Igst
                //              }).ToList();
               
                uomRepositoryItemLookUpEdit.DataSource = _uomlist;

               // taxRepositoryItemLookUpEdit.DataSource = gstList;
                storeLookUpEdit.Properties.DataSource = _storeLists;
                stateLookUpEdit.Properties.DataSource = _statelist;
            }
        }

        private bool ValidateData()
        {
            var dt = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
            
            var trans = grnTransDtoBindingSource1.DataSource as List<BillTransDto>;

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
            if (string.IsNullOrEmpty(voucherNoTextEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Invoice No.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherNoTextEdit.Focus();
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
                MessageBoxAdv.Show(this, "Invalid Sales Ledger", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bookLookup.Focus();
                return false;
            }
          
            else if (dt > KontoGlobals.ToDate || dt < KontoGlobals.FromDate)
            {
                MessageBoxAdv.Show(this, "Voucher date out of financial range", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherDateEdit.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(stateLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Destination State", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                stateLookUpEdit.Focus();
                return false;
            }
            else if (gridView1.RowCount == 1)
            {
                MessageBoxAdv.Show(this, "At Least One Product Should be Entered", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }
            else if (Convert.ToDecimal(colNetTotal.SummaryItem.SummaryValue) == 0)
            {
                MessageBoxAdv.Show(this, "Zero Total Not accepted", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }
           
            else if (trans.Any(x => x.ProductId == 0))
            {
                MessageBoxAdv.Show(this, "Invalid Product Selection", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }
            else if (trans.Any(x => x.UomId == 0))
            {
                MessageBoxAdv.Show(this, "Invalid Unit Selection", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }
            else if (trans.Sum(x => x.Pcs) == 0 && trans.Sum(x => x.Qty) == 0)
            {
                MessageBoxAdv.Show(this, "At least one pcs or qty should be entered", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }
            //check for duplicate bill no
            using(var db = new KontoContext())
            {
                db.Database.CommandTimeout = 0;
                bool result = LedgerEff.DataFreezeStatus(dt, (int)VoucherTypeEnum.SaleInvoice, db);
                if (result == false)
                {
                    MessageBox.Show(KontoGlobals.SaveFreezeWarning);
                    return false;
                }
                var _vid = Convert.ToInt32(voucherLookup1.SelectedValue);
                if(DbUtils.CheckExistVoucherNo(_vid, voucherNoTextEdit.Text.Trim(), db,this.PrimaryKey))
                {
                    MessageBox.Show("Invoice No. Already exists");
                    voucherNoTextEdit.Focus();
                    return false;
                }

                if (!trans.Any(x => x.RefId > 0)){
                    var groupbyItem = trans.GroupBy(k => k.ProductId).ToList();
                    foreach (var item in groupbyItem)
                    {
                        var checkforstock = db.Products.FirstOrDefault(k => k.Id == item.Key);

                        if (!checkforstock.CheckNegative) continue;

                        var Qty = trans.Where(k => k.ProductId == checkforstock.Id).Sum(k => k.Qty);

                        var stockBal = DbUtils.GetCurrentStock(checkforstock.Id, 0);

                        if (Qty > stockBal)
                        {
                            MessageBox.Show("Stock not available of Item " + checkforstock.ProductName + " Available Stock Only " + stockBal);
                            //     IsSaveComplete = true;
                            return false;
                        }
                    }
                }

                //    var accid = Convert.ToInt32(accLookup1.SelectedValue);
                //    var find1 = db.Bills.FirstOrDefault(
                //   x => x.AccId == accid && !x.IsDeleted && x.VoucherNo == voucherNoTextEdit.Text.Trim() && x.CompId == KontoGlobals.CompanyId 
                //   && x.YearId == KontoGlobals.YearId && x.Id!= this.PrimaryKey);

                //    if ( find1 != null)
                //    {
                //        MessageBox.Show("Entered Bill No Already Exists for this Party");
                //        voucherNoTextEdit.Focus();
                //        return false;
            }



            //}

            return true;
        }

        private void LoadData(BillModel model)
        {
            this.ResetPage();
            IsLoadData = true;
            this.PrimaryKey = model.Id;
            invTypeLookUpEdit.EditValue = model.BillType;
          
            voucherLookup1.SelectedValue = model.VoucherId;
            voucherLookup1.SetGroup(model.VoucherId);

            bookLookup.SelectedValue = model.BookAcId;
            bookLookup.SetAcc(Convert.ToInt32(model.BookAcId));
            voucherDateEdit.EditValue = KontoUtils.IToD(model.VoucherDate);
            voucherNoTextEdit.Text = model.VoucherNo;

            accLookup1.SetAcc(model.AccId);
            accLookup1.SelectedValue = model.AccId;
            extra2TextEdit.Text = model.Extra2;

            challanNotextEdit.Text = model.BillNo;
            refNoTextEdit.Text = model.RefNo;

            delvLookup.SelectedValue = model.DelvAccId;
            
            delvLookup.SetAcc(Convert.ToInt32(model.DelvAccId));
            
            if (Convert.ToInt32(model.DelvAdrId) != 0)
            {
                addressLookup1.SelectedValue = model.DelvAdrId;
                addressLookup1.SetValue(Convert.ToInt32(model.DelvAdrId));
            }

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
            if (Convert.ToInt32(model.AgentId) != 0)
            {
                agentLookup.SelectedValue = model.AgentId;
                agentLookup.SetAcc((int)model.AgentId);
            }
            stateLookUpEdit.EditValue = model.StateId;
            lrNotextEdit.Text = model.DocNo;
            lrDateEdit.EditValue = model.DocDate;
            remarkTextEdit.Text = model.Remarks;
            vehicleNoTextEdit.Text = model.VehicleNo;
            ewayBillTextEdit.Text = model.EwayBillNo;
            dueDaysTextEdit.Text = model.Duedays.ToString();
            extra1TextEdit.Text = model.Extra1; // others reference no
           

            if(model.RcdDate!=null)
                rcdDateEdit1.DateTime = Convert.ToDateTime(model.RcdDate);
            else
                rcdDateEdit1.DateTime = KontoUtils.IToD(model.VoucherDate);

            createdLabelControl.Text = "Created By: " + model.CreateUser + " [ " + model.CreateDate + " ]";
            modifyLabelControl.Text = "Modified By: " + model.ModifyUser + " [ " + model.ModifyDate ?? string.Empty  + " ]";

            tcsPerTextEdit.Value = model.TcsPer;
            tcsAmtTextEdit.Value = model.TcsAmt;

            using (var _context = new KontoContext())
            {

                _context.Database.CommandTimeout = 0;

                var _lst = (from bt in _context.BillTrans
                            join p in _context.Products on bt.ProductId equals p.Id
                            join um in _context.Uoms on bt.UomId equals um.Id
                            join cl in _context.ColorModels on bt.ColorId equals cl.Id into join_color
                            from cl in join_color.DefaultIfEmpty()
                            join dm in _context.Products on bt.DesignId equals dm.Id into join_design
                            from dm in join_design.DefaultIfEmpty()
                            join gd in _context.Grades on bt.GradeId equals gd.Id into join_grade
                            from gd in join_grade.DefaultIfEmpty()
                            join chln in _context.ChallanTranses on bt.RefTransId equals chln.Id into joinChln
                            from chln in joinChln.DefaultIfEmpty()
                            join chl in _context.Challans on chln.ChallanId equals chl.Id into joinChl
                            from chl in joinChl.DefaultIfEmpty()
                            join ort in _context.OrdTranses on chln.RefId equals ort.Id into joinOrt
                            from ort in joinOrt.DefaultIfEmpty()
                            join or in _context.Ords on ort.OrdId equals or.Id into joinOr
                            from or in joinOr.DefaultIfEmpty()
                            orderby bt.Id
                            where bt.BillId == model.Id && !bt.IsDeleted && bt.IsActive
                            select new BillTransDto
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
                                Total = bt.Total,
                                GradeId = bt.GradeId != null ? (int)bt.GradeId : 0,
                                DesignId = bt.DesignId != null ? (int)bt.DesignId : 0,
                                ChallanNo = chl.VoucherNo,
                                ColorId = bt.ColorId != null ? (int)bt.ColorId : 0,
                                ChDate = chl.VoucherDate,
                                ColorName = cl.ColorName,
                                Cut = bt.Cut,
                                DesignName = dm.ProductCode,
                                GradeName = gd.GradeName,
                                LotNo = bt.LotNo,
                                OrdDate = or.VoucherDate,
                                OrderNo = or.VoucherNo,
                                OrdId = or.Id,
                                Pcs = bt.Pcs,
                                ProductId = (int)bt.ProductId,
                                ProductName = p.ProductName,
                                RefId = bt.RefId,
                                RefTransId = bt.RefTransId,
                                RefVoucherId = bt.RefVoucherId,
                                SaleRate=bt.SaleRate,HsnCode = p.HsnCode,RatePerQty= p.Price2,
                                Barcode = p.BarCode
                            }
                            ).ToList();

                this.grnTransDtoBindingSource1.DataSource = _lst;

                var paid = _context.BtoBs.Where(x => x.BillId == model.Id && x.BillVoucherId == model.VoucherId
                                          && !x.IsDeleted)
                                        .Sum(x => x.Amount);
                if (paid > 0)
                {
                    if(KontoGlobals.UserRoleId!=1)
                        okSimpleButton.Enabled = false;
                    if (model.TotalAmount - paid - model.TdsAmt == 0)
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


            FinalTotal();
            this.Text = "Sales Voucher [View/Modify]";

        }

        private DataTable GetPurchaseTable()
        {
            using (var con = new SqlConnection(KontoGlobals.sqlConnectionString.ConnectionString))
            {

                using (var cmd = new SqlCommand("dbo.bill_analysis", con))
                {
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@fromDate", SqlDbType.Int).Value = KontoGlobals.FromDate;
                    cmd.Parameters.Add("@ToDate", SqlDbType.Int).Value = KontoGlobals.ToDate;
                    cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = KontoGlobals.CompanyId;
                    cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = KontoGlobals.BranchId;
                    cmd.Parameters.Add("@YearId", SqlDbType.Int).Value = KontoGlobals.YearId;
                    cmd.Parameters.Add("@VTypeId", SqlDbType.Int).Value = (int)VoucherTypeEnum.SaleInvoice;


                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    var DtCriterias = new DataTable();
                    DtCriterias.Load(cmd.ExecuteReader());
                    con.Close();
                    return DtCriterias;
                }
            }
        }

        private void GridFocus() // for focus setting in gridview
        {
            //if (PosPara.Move_Next_Barcode)
            //{
            //    gridView1.UpdateCurrentRow();
            //    gridView1.FocusedRowHandle = GridControl.NewItemRowHandle;

            //    BeginInvoke(new MethodInvoker(() =>
            //    {
            //        gridView1.FocusedColumn = colBarcode;
            //    }));

            //}
            //else
            //{
                gridView1.FocusedColumn = gridView1.GetVisibleColumn(colProductName.VisibleIndex);
            //}
        }
        #endregion

        #region GridView
        private void GridView1_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var itm = gridView1.GetFocusedRow() as BillTransDto;
            if (itm == null) return;
            if (!"ProductName,ColorName,GradeName,DesignName".Contains(gridView1.FocusedColumn.FieldName)) return;
            if (Convert.ToInt32(itm.RefId) > 0)
                e.Cancel = true;
        }
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
            if (!(gridView1.GetRow(e.RowHandle) is BillTransDto model)) return;

            if (e.Column.FieldName == "Barcode" && e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
            {
                var pos = DbUtils.GetProductDetails(e.Value.ToString());

                if (pos == null)
                {
                    MessageBox.Show("Barcode Not Found..");

                    if (gridView1.ActiveEditor.OldEditValue != null)
                        model.Barcode = gridView1.ActiveEditor.OldEditValue.ToString();
                    else
                        model.Barcode = string.Empty;

                    BeginInvoke(new MethodInvoker(() =>
                    {
                        gridView1.FocusedColumn = colBarcode;
                    }));

                    return;
                }

                //// merge same barcode/product qty
                //if (PosPara.Merge_Qty_For_Same_Barcode && gridView1.RowCount > 1)
                //{
                //    var _details = grnTransDtoBindingSource1.DataSource as List<BillTransDto>;
                //    var pmodel = _details.FirstOrDefault(x => x.Barcode == e.Value.ToString() && x.Id != model.Id);
                //    if (pmodel != null)
                //    {
                //        pmodel.Qty = pmodel.Qty + 1;
                //        pmodel.Pcs = pmodel.Pcs + 1;
                //        gridView1.DeleteRow(e.RowHandle);
                //        gridControl1.RefreshDataSource();
                //        GridCalculation(pmodel, e.Column.FieldName);
                //        BeginInvoke(new MethodInvoker(() =>
                //        {
                //            gridView1.FocusedColumn = colBarcode;
                //        }));

                //        return;
                //    }

                //}
                model.ProductId = pos.Id;
                model.EmpId = Convert.ToInt32(empLookup1.SelectedValue);
                model.Salesman = empLookup1.SelectedText;

                model.ChkNegative = pos.CheckNegative;
                model.Barcode = pos.BarCode;

                model.HsnCode = pos.HsnCode;

                model.ProductName = pos.ProductName;

                model.Stock = Convert.ToDecimal(pos.StockQty);
                model.ColorId = pos.ColorId;

                model.Cut = pos.Cut;
                model.ColorName = pos.ColorName;

                if ((int)accLookup1.LookupDto.GroupId == (int)LedgerGroupEnum.SUNDRY_DEBTORS)
                {
                    model.FreightRate = BillPara.Default_Freight_Rate;
                }

                model.UomId = pos.PurUomId;
                model.RatePerQty = pos.RatePerQty;
                model.Disc = _DiscPer;

                if (model.Disc == 0)
                    model.Disc = pos.SaleDisc;

                //  model.ProfitPer = pos.ProfitPer; // price profit %


                if (accLookup1.LookupDto.RateType == "BLK")
                {
                    model.SaleRate = pos.Rate1;
                }
                else if (accLookup1.LookupDto.RateType == "SMBLK")
                    model.SaleRate = pos.Rate2;
                else
                    model.SaleRate = pos.SaleRate;

                if (model.SaleRate == 0)
                    model.SaleRate = pos.SaleRate;

                if (accLookup1.LookupDto.IsGst && !isImortOrSez)
                {
                    model.SgstPer = pos.Sgst;
                    model.CgstPer = pos.Cgst;
                    model.IgstPer = 0;
                    model.Igst = 0;
                }
                else
                {
                    model.SgstPer = 0;
                    model.Sgst = 0;
                    model.CgstPer = 0;
                    model.Cgst = 0;
                    model.IgstPer = pos.Igst;
                }
                model.CessPer = pos.Cess;

                if (pos.SaleRateTaxInc)
                    model.Rate = decimal.Round((model.SaleRate * 100) / (100 + (model.SgstPer + model.CgstPer + model.IgstPer)), 2, MidpointRounding.AwayFromZero);
                else
                    model.Rate = model.SaleRate;

                model.Qty = 1;
                model.Pcs = 1;

                model.Mrp = pos.Mrp;

                // for moving focus after scannig barcode
                GridFocus();
            }

            GridCalculation(model,e.Column.FieldName);
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
                var row = view.GetRow(view.FocusedRowHandle) as BillTransDto;
                view.DeleteRow(view.FocusedRowHandle);
                DelTrans.Add(row);
                FinalTotal();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                GridView view = sender as GridView;
                if (gridView1.FocusedColumn.FieldName == "ColorName")
                {
                    view.SetRowCellValue(view.FocusedRowHandle, colColorName, string.Empty);
                    view.SetRowCellValue(view.FocusedRowHandle, colColorId, 0);
                }
                else if (gridView1.FocusedColumn.FieldName == "DesignName")
                {
                    view.SetRowCellValue(view.FocusedRowHandle, colDesignName, string.Empty);
                    view.SetRowCellValue(view.FocusedRowHandle, colDesignId, 0);
                }
            }

        }

        private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var rw = gridView1.GetRow(e.RowHandle) as BillTransDto;
            rw.Id = -1 * gridView1.RowCount;
        }

        private void GridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(accLookup1.SelectedValue) == 0) return;
                var dr = PreOpenLookup();
                if (dr == null) return;
                if (gridView1.FocusedColumn.FieldName == "ProductName")
                {

                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.ProductId == 0)
                        {
                            OpenItemLookup(dr.ProductId, dr);
                            // e.Handled = true;
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        OpenItemLookup(dr.ProductId, dr);
                        e.Handled = true;
                    }
                }
                else if (gridView1.FocusedColumn.FieldName == "ColorName")
                {
                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.ColorId == 0)
                        {
                            OpenColorLookup(dr.ColorId, dr);
                            // e.Handled = true;
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        OpenColorLookup(dr.ProductId, dr);
                        e.Handled = true;
                    }
                }
                else if (gridView1.FocusedColumn.FieldName == "GradeName")
                {
                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.GradeId == 0)
                        {
                            OpenGradeLookup(dr.GradeId, dr);
                            // e.Handled = true;
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        OpenGradeLookup(dr.GradeId, dr);
                        e.Handled = true;
                    }
                }
                else if (gridView1.FocusedColumn.FieldName == "DesignName")
                {
                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.DesignId == 0)
                        {
                            OpenDesignLookup(dr.DesignId, dr);
                            // e.Handled = true;
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        OpenDesignLookup(dr.DesignId, dr);
                        e.Handled = true;
                    }
                }
                else if (e.KeyCode == Keys.Enter && gridView1.FocusedColumn.FieldName == "LotNo")
                {
                    ProductModel prod;
                    using (var db = new KontoContext())
                    {
                        prod = db.Products.Find(dr.ProductId);

                    }
                    if (prod.SerialReq == "Yes" && prod.PTypeId == (int)ProductTypeEnum.FINISH)
                    {
                        var frms = new SerialNoView();
                        frms.IsStockSelection = true;
                        frms._BillTrans = grnTransDtoBindingSource1.DataSource as List<BillTransDto>;
                        frms.ProductId = dr.ProductId;
                        frms.RefTransId = dr.Id;
                        if (frms.ShowDialog() == DialogResult.OK)
                        {
                            this.gridView1.FocusedColumn = gridView1.GetVisibleColumn(colQty.VisibleIndex);
                            dr.Qty = 1;
                            dr.LotNo = frms.SelectedSerial.SerialNo;
                           // dr.DesignId = frms.SelectedSerial.Id;
                            GridCalculation(dr, "Qty");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, "sales GridControl KeyDown");
                MessageBoxAdv.Show(this, "Error Lookup Setup !!", "Exception ", ex.ToString());

            }
        }

        private void ProductRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
                OpenItemLookup(dr.ProductId, dr);

        }
        private void ColorRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            var dr = PreOpenLookup();
            if (dr != null)
                OpenColorLookup(dr.ColorId, dr);
        }
        private void GradeRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
                OpenGradeLookup(dr.GradeId, dr);
        }
        private void DesignRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
                OpenDesignLookup(dr.DesignId, dr);
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
            _DiscPer = this.accLookup1.LookupDto.DiscPer;
            if (this.PrimaryKey==0 && Convert.ToInt32(this.accLookup1.SelectedValue) > 0)
            {
                this.delvLookup.LookupDto = this.accLookup1.LookupDto;
                this.delvLookup.SelectedValue = this.accLookup1.SelectedValue;
                this.delvLookup.buttonEdit1.Text = this.accLookup1.SelectedText;
                this.delvLookup.SelectedText = this.accLookup1.SelectedText;
               // addressLookup1.SetValue(this.delvLookup.LookupDto.AddressId);
               // addressLookup1.SelectedValue = this.delvLookup.LookupDto.AddressId;
                //addressLookup1.buttonEdit1.Text = this.delvLookup.LookupDto.FullAddress;
                dueDaysTextEdit.Text = this.accLookup1.LookupDto.CrDays.ToString();
                if (this.accLookup1.LookupDto.TcsReq.ToUpper() == "YES")
                    tcsPerTextEdit.Value = accLookup1.LookupDto.TcsPer;

            }
            if (this.accLookup1.LookupDto.TcsReq.ToUpper() == "YES")
            {
                if(tcsPerlayoutControlItem.IsHidden)
                    tcsPerlayoutControlItem.RestoreFromCustomization();
                
                if(tcsAmountlayoutControlItem.IsHidden)
                    tcsAmountlayoutControlItem.RestoreFromCustomization();

                tcsPerlayoutControlItem.ContentVisible = true;
                tcsAmountlayoutControlItem.ContentVisible = true;
            }
            else
            {
                tcsPerlayoutControlItem.ContentVisible = false;
                tcsAmountlayoutControlItem.ContentVisible = false;
            }
           
           
        }

        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                voucherLookup1.Focus();
                return;
            }
           
            else if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new SInvoiceListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Sales Invoices [View]";

            }
            else if (tabControlAdv1.SelectedIndex == 2)
            {
                if (tabPageAdv3.Controls.Count > 0) return;
                var _frm = new AnalysisUserControl(VoucherTypeEnum.SaleInvoice, true);
                _frm.AnaDataTable = GetPurchaseTable();
                _frm.Dock = DockStyle.Fill;
                tabPageAdv3.Controls.Add(_frm);
                this.Text = "Sales Anlysis";
            }
            else if (tabControlAdv1.SelectedIndex == 3)
            {
                if (tabPageAdv4.Controls.Count > 0) return;
                this.Text = "Sales Register";
                var _frm = Activator.CreateInstance("Konto.Reporting", "Konto.Reporting.Para.BillPara.ParaMainView").Unwrap() as KontoForm;

                _frm.TopLevel = false;
                _frm.Parent = tabPageAdv4;
                _frm.ReportFilterType = "SALES";
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

                Log.Error(ex, "Sales Invoice Save");
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

                rpt.Load(new FileInfo("reg\\doc\\" + BillPara.Default_Invoice_Print + ""));

                rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;
                
                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);
                doc.Parameters["id"].CurrentValue = this.PrimaryKey;
                
                doc.Parameters["Bill"].CurrentValue = "N";
                doc.Parameters["reportid"].CurrentValue = 0;
                if (doc.Parameters.Contains("todate"))
                    doc.Parameters["todate"].CurrentValue = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
                if (doc.Parameters.Contains("compid"))
                    doc.Parameters["compid"].CurrentValue = KontoGlobals.CompanyId;
                if (doc.Parameters.Contains("accid"))
                    doc.Parameters["accid"].CurrentValue = Convert.ToInt32(accLookup1.SelectedValue);
                
              //  rpt.ResourceLocator = new MySubreportLocator();
                //var subrep = rpt.Report.Body.ReportItems["Page1"] as GrapeCity.ActiveReports.PageReportModel.Container;
                //if (subrep != null)
                //{
                //    var _subrpt = new PageReport();
                //   // _subrpt.Load(new FileInfo("reg\\doc\\" + subrep.ReportName));
                //    //_subrpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;
                    
                //}
                //var db = new KontoContext();
                //var RefId = db.BillTrans.FirstOrDefault(k => k.BillId == this.PrimaryKey).RefId;
                //if (RefId != null)
                //{
                //    if (doc.Parameters["ChallanId"] != null)
                //        doc.Parameters["ChallanId"].CurrentValue = (int)RefId;
                //    if (doc.Parameters["Challan"] != null)
                //        doc.Parameters["Challan"].CurrentValue = "N";
                //}

                var frm = new KontoRepViewer(doc);
                frm.ToMailId = accLookup1.LookupDto.Email;
                frm.VoucherType = VoucherTypeEnum.SaleInvoice;
               
                frm.Text = "Invoice Print";
                var _tab = this.Parent.Parent as TabControlAdv;
                if (_tab == null) return;
                var pg1 = new TabPageAdv();
                pg1.Text = "Bill Print";
                _tab.TabPages.Add(pg1);
               
                frm.TopLevel = false;
                frm.Parent = pg1;
                _tab.SelectedTab = pg1;
                //frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                frm.Show();// = true;

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Sales print");
                MessageBoxAdv.Show(this, "Error While Print !!", "Exception ", ex.ToString());

            }
        }
        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<BillModel>();
            this.Text = "Sales Voucher [Add New]";
            IsLoadData = false;
            paidLabel.Text = "UN-PAID";

            invTypeLookUpEdit.EditValue = "Regular";
            
            storeLookUpEdit.EditValue = 1;
            voucherNoTextEdit.Text = "New";
            voucherDateEdit.EditValue = DateTime.Now;
            
            empLookup1.SelectedValue = 1;
            empLookup1.SetGroup();
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
            this.ActiveControl =voucherDateEdit;

            if (!BillPara.Ask_For_Voucher_Selection)
                voucherLookup1.SetDefault();
            else
                voucherLookup1.SetEmpty();

            if (voucherLookup1.GroupDto != null && Convert.ToInt32(voucherLookup1.GroupDto.AccId) > 0)
            {
                bookLookup.SelectedValue = voucherLookup1.GroupDto.AccId;
                bookLookup.SetAcc(Convert.ToInt32(voucherLookup1.GroupDto.AccId));
            }

            DelTrans = new List<BillTransDto>();
            this.grnTransDtoBindingSource1.DataSource = new List<BillTransDto>();

            DelBill = new List<PendBillListDto>();
            BillList = new List<PendBillListDto>();
            AllBill = new List<PendBillListDto>();

        }
        public override void ResetPage()
        {
            base.ResetPage();
            IsLoadData = false;
            accLookup1.SetEmpty();
            bookLookup.SetEmpty();
            delvLookup.SetEmpty();
            addressLookup1.SetEmpty();

            challanNotextEdit.Text = string.Empty;
            
            voucherDateEdit.DateTime = DateTime.Now;
            rcdDateEdit1.DateTime = DateTime.Now;

            voucherNoTextEdit.Text = string.Empty;
            refNoTextEdit.Text = string.Empty;
            transportLookup.SetEmpty();
            empLookup1.SetEmpty();
            lrNotextEdit.Text = string.Empty;
            lrDateEdit.EditValue = null;
            remarkTextEdit.Text = string.Empty;
            dueDaysTextEdit.Text = string.Empty;
            vehicleNoTextEdit.Text = string.Empty;
            ewayBillTextEdit.Text = string.Empty;
            roundoffSpinEdit.Value = 0;
            billAmtSpinEdit.Value = 0;
            tcsPerTextEdit.Value = 0;
            tcsAmtTextEdit.Value = 0;
            
            extra1TextEdit.Text = string.Empty;
            extra2TextEdit.Text = string.Empty;
            
            DelTrans = new List<BillTransDto>();
            DelBill = new List<PendBillListDto>();
            BillList = new List<PendBillListDto>();
            AllBill = new List<PendBillListDto>();

        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

           // DbUtils.UnHoldSerial(Convert.ToInt32(voucherLookup1.SelectedValue), _SerialValue);
          
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
           
            if (Convert.ToInt32(accLookup1.SelectedValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "AccId", Operation = Op.Equals, Value = Convert.ToInt32(accLookup1.SelectedValue) });
            }
            //if(!string.IsNullOrEmpty(voucherNoTextEdit.Text) && voucherNoTextEdit.Text != "New")
            //{
            //    filter.Add(new Filter { PropertyName = "VoucherNo", Operation = Op.Equals, Value = voucherNoTextEdit.Text });
            //}

            filter.Add(new Filter { PropertyName = "CompId", Operation = Op.Equals, Value = KontoGlobals.CompanyId });
            filter.Add(new Filter { PropertyName = "YearId", Operation = Op.Equals, Value = KontoGlobals.YearId });
            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });

         

            using (var db = new KontoContext())
            {
                FilterView = db.Bills.Where(ExpressionBuilder.GetExpression<BillModel>(filter))
                    .OrderBy(x => x.VoucherDate).ThenBy(x => x.Id).ToList();
                   

                if (FilterView.Count == 0)
                {
                    MessageBoxAdv.Show(this, "No Record Found", "Find !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ResetPage();
                    this.NewRec();
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
                cfg.CreateMap<BillTransDto, BillTransModel>().ForMember(x => x.Id, p => p.Ignore());
               
            });
            
            var _translist = grnTransDtoBindingSource1.DataSource as List<BillTransDto>;
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
                            if (item.Id <= 0) continue;
                            var _model = db.BillTrans.Find(item.Id);
                            _model.IsDeleted = true;



                            var stockReq = db.Products.FirstOrDefault(k => k.Id == item.ProductId);

                            //update serial sotck
                            if (stockReq.SerialReq == "Yes" && stockReq.PTypeId == (int)ProductTypeEnum.FINISH && !string.IsNullOrEmpty(item.LotNo))
                            {
                                var sr = db.ItemSerials.SingleOrDefault(x=>x.SerialNo== item.LotNo);
                                if (sr != null)
                                {
                                    sr.IsActive = true; // remove stock of serials
                                }
                            }
                        }
                        
                        //if(!string.IsNullOrEmpty(bill))
                        //_find.BillNo = bill;

                        //if (!string.IsNullOrEmpty(ordr))
                        //    _find.RefNo = ordr;

                        //Bill Reference Update
                        LedgerEff.BillRefEntry("Debit",_find,0,db);       //Insert or update in Billref table

                        //Insert or update in LedgerTrans table
                        LedgerEff.LedgerTransEntry("Debit", _find, db, Trans);


                        // Insert in BtoB for BillAdjustment
                        LedgerEff.BtoBEntry("SInvoice", _find.Id, _find, db, BillList);

                        var stk = db.StockTranses.Where(k => k.MasterRefId == _find.RowId).ToList();
                        if (stk != null)
                            db.StockTranses.RemoveRange(stk);

                        //stock effect
                        if (!Trans.Any(x => x.RefId > 0) || BillPara.Order_Required)
                        {
                            foreach (var item in Trans)
                            {
                                bool IsIssue = true;
                                string TableName = "SaleInvoice";

                                var stockReq = db.Products.FirstOrDefault(k => k.Id == item.ProductId);

                                    //update serial sotck
                                if(stockReq.SerialReq=="Yes" && stockReq.PTypeId == (int)ProductTypeEnum.FINISH)
                                {
                                    if (string.IsNullOrEmpty(item.LotNo)) continue;
                                    var sr = db.ItemSerials.SingleOrDefault(x=>x.SerialNo==item.LotNo);
                                    if (sr != null)
                                    {
                                        sr.IsActive = false; // remove stock of serials
                                    }
                                }

                                if (stockReq.StockReq == "No")
                                {
                                    continue;
                                }
                                StockEffect.StockTransBillEntry(_find, item, IsIssue, TableName, db);
                            }
                        }

                        //if(this.PrimaryKey==0)
                        //    DbUtils.UsedSerial(_find.VoucherId, _SerialValue, db);

                        db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Sales Voucher " +" Save");
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
                    if (this.voucherLookup1.GroupDto.PrintAfterSave && MessageBox.Show("Print Bill ?", "Print", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        this.PrimaryKey = _find.Id;
                        Print();
                        this.PrimaryKey = 0;
                    }

                    base.SaveDataAsync(newmode);
                    this.ResetPage();
                    this.NewRec();
                    if (!BillPara.Party_Wise_Challan)
                    {
                        GetPendingChallan(0);
                    }
                    voucherDateEdit.Focus();

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
         
            model.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);
            model.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));

            model.AccId = Convert.ToInt32(accLookup1.SelectedValue);
            model.BookAcId = Convert.ToInt32(bookLookup.SelectedValue);
            
            model.DelvAccId = Convert.ToInt32(delvLookup.SelectedValue);
            model.DelvAdrId = Convert.ToInt32(addressLookup1.SelectedValue);

            model.VoucherNo = voucherNoTextEdit.Text.Trim();
            
            model.AgentId = Convert.ToInt32(agentLookup.SelectedValue);
            model.RefNo = refNoTextEdit.Text.Trim();
            model.BillNo = challanNotextEdit.Text.Trim();
          

            model.EmpId = Convert.ToInt32(empLookup1.SelectedValue);
            model.StoreId = Convert.ToInt32(storeLookUpEdit.EditValue);

            model.Remarks = remarkTextEdit.Text.Trim();
            model.TransId = Convert.ToInt32(transportLookup.SelectedValue);
            model.DocNo = lrNotextEdit.Text.Trim();

            if (lrDateEdit.EditValue == null || lrDateEdit.EditValue == DBNull.Value)
                model.DocDate = null;
            else
                model.DocDate = Convert.ToDateTime(lrDateEdit.EditValue);
            
            model.TypeId = (int)VoucherTypeEnum.SaleInvoice;
            model.CompId = KontoGlobals.CompanyId;
            model.YearId = KontoGlobals.YearId;
            model.BranchId = KontoGlobals.BranchId;
            model.RoundOff = roundoffSpinEdit.Value;
            model.EwayBillNo = ewayBillTextEdit.Text;
            model.RcdDate = rcdDateEdit1.DateTime;

            var _translist = grnTransDtoBindingSource1.DataSource as List<BillTransDto>;
            model.GrossAmount = _translist.Sum(x => x.NetTotal) - _translist.Sum(x => x.Cgst) - _translist.Sum(x => x.Sgst) -
                _translist.Sum(x => x.Igst) - _translist.Sum(x => x.Cess);
            model.TotalAmount = billAmtSpinEdit.Value;
            model.TotalQty = _translist.Sum(x => x.Qty);
            model.TotalPcs = _translist.Sum(x => x.Pcs);
            model.RoundOff = roundoffSpinEdit.Value;
            model.IsActive = true;
            model.StateId = Convert.ToInt32(stateLookUpEdit.EditValue);
            model.VoucherNo = voucherNoTextEdit.Text.Trim();
            model.Extra2 = extra2TextEdit.Text.Trim();
            model.Extra1 = extra1TextEdit.Text.Trim();
            model.Duedays = Convert.ToInt32(dueDaysTextEdit.Value);
            model.VehicleNo = vehicleNoTextEdit.Text.Trim();
            model.TcsPer = tcsPerTextEdit.Value;
            model.TcsAmt = tcsAmtTextEdit.Value;

            if (model.Id == 0)
            {
                
                if(!voucherLookup1.GroupDto.ManualSeries)
                    model.VoucherNo = DbUtils.NextSerialNo(model.VoucherId, db,0);

                if (DbUtils.CheckExistVoucherNo(model.VoucherId, model.VoucherNo, db, model.Id))
                {
                    MessageBox.Show("Duplicate Voucher No Not Allowed");
                    return false;
                }
                db.Bills.Add(model);
                db.SaveChanges();
            }

            return true;

        }
      
        #endregion

    }
}
