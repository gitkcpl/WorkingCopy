using AutoMapper;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
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

using Konto.Shared.Masters.Color;
using Konto.Shared.Masters.Design;
using Konto.Shared.Masters.Grade;
using Konto.Shared.Masters.Item;
using Konto.Shared.Reports;
using Konto.Shared.Trans.Common;
using Serilog;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ExpressionBuilder = Konto.Core.Shared.Libs.ExpressionBuilder;

namespace Konto.Trading.GP
{
    public partial class GPIndex : KontoMetroForm
    {
        private List<ChallanModel> FilterView = new List<ChallanModel>();
        private List<GrnTransDto> DelTrans = new List<GrnTransDto>();
        private List<GrnProdDto> prodDtos = new List<GrnProdDto>();
        private List<GrnProdDto> DelProd = new List<GrnProdDto>();

        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        private bool IsLoadingData = false;
        public GPIndex()
        {
            InitializeComponent();

            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            gridControl1.ProcessGridKey += GridControl1_ProcessGridKey;
            productRepositoryItemButtonEdit.ButtonClick += ProductRepositoryItemButtonEdit_ButtonClick;
            colorRepositoryItemButtonEdit.ButtonClick += ColorRepositoryItemButtonEdit_ButtonClick;
            gradeRepositoryItemButtonEdit.ButtonClick += GradeRepositoryItemButtonEdit_ButtonClick;
            designRepositoryItemButtonEdit.ButtonClick += DesignRepositoryItemButtonEdit_ButtonClick;
            accLookup1.SelectedValueChanged += AccLookup1_SelectedValueChanged;
            repositoryItemHyperLinkEdit1.OpenLink += RepositoryItemHyperLinkEdit1_OpenLink;
            gridView1.InitNewRow += GridView1_InitNewRow;
            gridView1.CellValueChanged += GridView1_CellValueChanged;
            gridView1.KeyDown += GridView1_KeyDown;
            gridControl1.Enter += GridControl1_Enter;
            gridView1.CustomDrawRowIndicator += GridView1_CustomDrawRowIndicator;
            gridView1.ShowingEditor += GridView1_ShowingEditor;
            gridView1.MouseUp += GridView1_MouseUp;
            lotNoRepositoryItemButtonEdit.ButtonClick += LotNoRepositoryItemButtonEdit_ButtonClick;
            this.accLookup1.ShownPopup += AccLookup1_ShownPopup;
            gridView1.DoubleClick += GridView1_DoubleClick;
            this.MainLayoutFile = KontoFileLayout.Gp_Index;
            this.GridLayoutFile = KontoFileLayout.Gp_Trans;
            this.challanNotextEdit.TextChanged += ChallanNotextEdit_TextChanged; ;

            FillLookup();
            SetParameter();
            SetGridColumn();

            headerEdit.Hide();
            headerEdit.Parent = this.gridControl1;
            headerEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            headerEdit.Leave += new EventHandler(headerEdit_Leave);

            this.gridView1.ValidatingEditor += GridView1_ValidatingEditor;
            this.gridView1.InvalidValueException += GridView1_InvalidValueException;
            this.gridView1.ValidateRow += GridView1_ValidateRow;
            this.gridView1.InvalidRowException += GridView1_InvalidRowException;
            voucherLookup1.SelectedValueChanged += VoucherLookup1_SelectedValueChanged;
           
            this.Load += GPIndex_Load;

            tcsPerTextEdit.EditValueChanged += TcsPerTextEdit_EditValueChanged;
            tcsAmtTextEdit.EditValueChanged += TcsAmtTextEdit_EditValueChanged;
            this.voucherDateEdit.EditValueChanged += VoucherDateEdit_EditValueChanged;
            this.FirstActiveControl = voucherLookup1;
            
        }

        private void VoucherDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (this.PrimaryKey == 0)
            {
                billDateEdit.EditValue = voucherDateEdit.EditValue;
                lrDateEdit.EditValue = voucherDateEdit.EditValue;
            }
        }

        private void TcsAmtTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            FinalTotal();
        }

        private void TcsPerTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            FinalTotal();
        }



        private void GPIndex_Load(object sender, EventArgs e)
        {
            colSgstAmt.OptionsColumn.AllowFocus = true;
            colSgstAmt.OptionsColumn.AllowEdit = true;
            colCgstAmt.OptionsColumn.AllowFocus = true;
            colCgstAmt.OptionsColumn.AllowEdit = true;
            colIgstAmt.OptionsColumn.AllowFocus = true;
            colIgstAmt.OptionsColumn.AllowEdit = true;
        }

        private void VoucherLookup1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.PrimaryKey == 0 && Convert.ToInt32(voucherLookup1.SelectedValue) > 0)
            {
                voucherNoTextEdit.Text = "New-" + DbUtils.NextSerialNo(Convert.ToInt32(voucherLookup1.SelectedValue), 1);
            }
            if (voucherLookup1.GroupDto != null && voucherLookup1.GroupDto.ManualSeries)
            {
                voucherNoTextEdit.Enabled = true;
            }
            else
            {
                voucherNoTextEdit.Enabled = false;
            }
        }

        private void ChallanNotextEdit_TextChanged(object sender, EventArgs e)
        {
            billNoTextEdit.Text = challanNotextEdit.Text.Trim();
        }
        private void RepositoryItemHyperLinkEdit1_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {

            var er = gridView1.GetFocusedRow() as GrnTransDto;
            int id = Convert.ToInt32(er.MiscId);
            if (id == 0) return;
            var window = new GreyOrder.GreyOrderIndex();
            window.Tag = MenuId.Grey_Order;
            window.ViewOnlyMode = true;
            window.EditKey = id;
            window.ShowDialog();


        }

        #region UDF
        private void SetGridColumn()
        {
            //colColorName.Visible = GRNPara.Color_Required;
            //colDesignNo.Visible = GRNPara.Design_Required;
            //colGradeName.Visible = GRNPara.Grade_Required;
            ////colCess.Visible = GRNPara.Cess_Required;
            ////colCessAmt.Visible = GRNPara.Cess_Required;
            //colCops.Visible = GRNPara.Cut_Required;
            //colFreight.Visible = GRNPara.Freight_Required;
            //colFreightRate.Visible = GRNPara.Freight_Required;
            //colLotNo.Visible = GRNPara.LotNo_Required;
            ////colOtherAdd.Visible = GRNPara.OtherAdd_Required;
            ////colOtherLess.Visible = GRNPara.OtherLess_Required;

            //if (KontoGlobals.PackageId == 1)
            //{
            //    colCops.Caption = "Cut";
            //    colQty.Caption = "Meters";
            //}
        }
        private GrnTransDto PreOpenLookup()
        {
            if (Convert.ToInt32(accLookup1.SelectedValue) == 0) return null;
            gridView1.GetRow(gridView1.FocusedRowHandle);
            if (gridView1.GetRow(gridView1.FocusedRowHandle) == null)
            {
                gridView1.AddNewRow();
            }
            var dr = (GrnTransDto)gridView1.GetRow(gridView1.FocusedRowHandle);
            return dr;
        }

        public void GridCalculation(GrnTransDto er,bool isGstAmountChanged=false)
        {

            decimal Fold;
            decimal tRate;
            decimal tQty;
            if (er.CessPer > 0)
            {
                if (GrayPara.Calculate_Fold_On == "METER")
                {
                    Fold = (er.Qty) * (er.CessPer) / 100;
                    er.Cess = Fold;
                    tQty = er.Qty - Fold;
                    tRate = er.Rate;
                }
                else
                {
                    Fold = (er.Rate) * (er.CessPer) / 100;
                    er.Cess = Fold;
                    tRate = er.Rate - Fold;
                    tQty = er.Qty;
                }
            }
            else
            {
                tQty = er.Qty;
                tRate = er.Rate;
            }


            var dr = uomRepositoryItemLookUpEdit.GetDataSourceRowByKeyValue(er.UomId) as UomLookupDto;

            if (er.UomId == 20)
            {
                er.Gross = decimal.Round(er.Cops * tRate, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                er.Gross = decimal.Round(tQty * tRate, 2, MidpointRounding.AwayFromZero);
            }

            if (er.DiscPer > 0)
                er.Disc = decimal.Round(er.Gross * er.DiscPer / 100, 2, MidpointRounding.AwayFromZero);
            decimal gross = er.Gross - er.Disc;

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
            er.Total = gross + er.Sgst + er.Cgst + er.Igst;

            gridView1.UpdateCurrentRow();

            FinalTotal();
        }
        private void FinalTotal()
        {
            if (IsLoadingData) return;
            gridView1.UpdateTotalSummary();
            var ntotal = Convert.ToDecimal(colNetTotal.SummaryItem.SummaryValue);

            if (tcsPerTextEdit.Value > 0) // tcs applicable
            {
                if (GrayPara.Tcs_Round_Off)
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
        private void SetParameter()
        {
            using (var db = new KontoContext())
            {
                var _paralists = db.CompParas.Include("SysPara")
                              .Where(x => x.SysPara.Category == "GrayPurchase" && x.CompId == KontoGlobals.CompanyId)
                             .ToList();

                foreach (var item in _paralists)
                {
                    var value = item.ParaValue;
                    switch (item.ParaId)
                    {

                        case 107:
                            {
                                GrayPara.Calculate_Fold_On = value;
                                break;
                            }
                        case 234:
                            {
                                GrayPara.Tcs_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 235:
                            {
                                GrayPara.Tcs_Round_Off = (value == "Y") ? true : false;
                                break;
                            }
                    }
                }
            }

        }
        private void OpenItemLookup(int _selvalue, GrnTransDto er)
        {
            var frm = new ProductLkpWindow();
            frm.Tag = MenuId.Product_Master;
            frm.SelectedValue = _selvalue;
            frm.PTypeId = ProductTypeEnum.GREY;
            frm.VoucherType = VoucherTypeEnum.GreyOrder;

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                er.ProductId = frm.SelectedValue;
                er.ProductName = frm.SelectedTex;
                var model = frm.SelectedItem as ProductLookupDto;
                er.UomId = model.PurUomId;
                er.Rate = model.DealerPrice;
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
        private void OpenColorLookup(int _selvalue, GrnTransDto er)
        {
            var frm = new ColorLkpWindow();
            frm.Tag = MenuId.Color;
            frm.SelectedValue = _selvalue;
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
        private void OpenGradeLookup(int _selvalue, GrnTransDto er)
        {
            var frm = new GradeLkpWindow();
            frm.Tag = MenuId.Grade;
            frm.SelectedValue = _selvalue;
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
        private void OpenDesignLookup(int _selvalue, GrnTransDto er)
        {
            var frm = new DesignLkpWindow();
            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Design_Master;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                gridView1.BeginDataUpdate();
                er.DesignId = frm.SelectedValue;
                er.DesignNo = frm.SelectedTex;
                gridView1.EndDataUpdate();
                gridView1.FocusedColumn = gridView1.GetVisibleColumn(colDesignNo.VisibleIndex + 1);
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
                new ComboBoxPairs("Ineligible","Ineligible")
            };
            itcLookUpEdit.Properties.DataSource = ibp;

            using (var db = new KontoContext())
            {


                //var _divLists = (from p in db.Divisions
                //                 where p.IsActive && !p.IsDeleted
                //                 select new BaseLookupDto()
                //                 {
                //                     DisplayText = p.DivisionName,
                //                     Id = p.Id
                //                 }).ToList();

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
                                    Id = p.Id,
                                    RateOn = p.RateOn
                                }).ToList();



                uomRepositoryItemLookUpEdit.DataSource = _uomlist;

                // invTypeLookUpEdit.Properties.DataSource = _divLists;
                storeLookUpEdit.Properties.DataSource = _storeLists;
            }
        }

        private bool ValidateData()
        {
            var dt = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
            var reqdt = Convert.ToInt32(billDateEdit.DateTime.ToString("yyyyMMdd"));
            if (string.IsNullOrEmpty(invTypeLookUpEdit.Text))
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
            else if (Convert.ToInt32(bookLookup.SelectedValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Purchase Book", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bookLookup.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(challanNotextEdit.Text.Trim()))
            {
                MessageBoxAdv.Show(this, "Invalid Challan No.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                challanNotextEdit.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(billNoTextEdit.Text.Trim()))
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
            else if (reqdt < dt)
            {
                MessageBoxAdv.Show(this, "Bill Date Cant Not be Less than Voucher date", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                billDateEdit.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(storeLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Store", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                storeLookUpEdit.Focus();
                return false;
            }
            else if (gridView1.RowCount == 1)
            {
                MessageBoxAdv.Show(this, "At Least One Product Should be Entered", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }
            var trans = grnTransDtoBindingSource1.DataSource as List<GrnTransDto>;
            if (trans.Any(x => x.Pcs == 0))
            {
                MessageBoxAdv.Show(this, "Invalid Pcs/Taka", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }

            if (trans.Any(x => x.Qty == 0))
            {
                MessageBoxAdv.Show(this, "Invalid Meters", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }
            if (trans.Any(x => x.Rate == 0))
            {
                MessageBoxAdv.Show(this, "Invalid Rate", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }
            
            if (trans.Any(x => x.Sgst != x.Cgst))
            {
                MessageBoxAdv.Show(this, "Invalid Gst Amt, Sgst & Cgst doest Not Match", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }

            //check for duplicate bill no
            using (var db = new KontoContext())
            {

                // check for mill issue
                if (this.PrimaryKey != 0)
                {
                    var _vid = Convert.ToInt32(voucherLookup1.SelectedValue);
                    var exist = db.ChallanTranses.Any(x => x.MiscId == this.PrimaryKey && x.RefVoucherId == _vid  && x.IsDeleted == false && x.IsActive == true);
                    if (exist)
                    {
                        MessageBoxAdv.Show("Can not Modify, Already Issued To Next level", "Acess Denied !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }

                var accid = Convert.ToInt32(accLookup1.SelectedValue);
                var find1 = db.Challans.FirstOrDefault(
               x => x.AccId == accid && !x.IsDeleted && x.BillNo == billNoTextEdit.Text.Trim() && x.CompId == KontoGlobals.CompanyId
               && x.YearId == KontoGlobals.YearId && x.Id != this.PrimaryKey);

                if (find1 != null)
                {
                    MessageBox.Show("Entered Bill No Already Exists for this Party");
                    billNoTextEdit.Focus();
                    return false;
                }

                find1 = db.Challans.FirstOrDefault(
              x => x.AccId == accid && !x.IsDeleted && x.ChallanNo == challanNotextEdit.Text.Trim() && x.CompId == KontoGlobals.CompanyId
              && x.YearId == KontoGlobals.YearId && x.Id != this.PrimaryKey);

                if (find1 != null)
                {
                    MessageBox.Show("Entered Challan No Already Exists for this Party");
                    challanNotextEdit.Focus();
                    return false;
                }

            }

            return true;
        }

        private void LoadData(ChallanModel model)
        {
            this.ResetPage();
            IsLoadingData = true;
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
           
            challanNotextEdit.Text = model.ChallanNo;
            billNoTextEdit.Text = model.BillNo;
            billDateEdit.EditValue = model.RcdDate;

            if (Convert.ToInt32(model.AgentId) != 0)
            {
                agentLookup.SelectedValue = model.AgentId;
                agentLookup.SetAcc(Convert.ToInt32(model.AgentId));
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
            lrNotextEdit.Text = model.DocNo;
            lrDateEdit.EditValue = model.DocDate;
            remarkTextEdit.Text = model.Remark;
            ewayTextEdit.Text = model.Extra1;
            billAmtSpinEdit.Value = model.TotalAmount;
            roundoffSpinEdit.Value = Convert.ToDecimal(model.RoundOff);

            tcsPerTextEdit.Value = model.TcsPer;
            tcsAmtTextEdit.Value = model.TcsAmt;

            createdLabelControl.Text = "Created By: " + model.CreateUser + " [ " + model.CreateDate + " ]";
            modifyLabelControl.Text = "Modified By: " + model.ModifyUser + " [ " + model.ModifyDate ?? string.Empty + " ]";

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProdModel, GrnProdDto>();
            });

            using (var _context = new KontoContext())
            {
                var _list = (from ct in _context.ChallanTranses
                             join pd in _context.Products on ct.ProductId equals pd.Id into join_pd
                             from pd in join_pd.DefaultIfEmpty()
                             join ot in _context.OrdTranses on ct.RefId equals ot.Id into join_ot
                             from ot in join_ot.DefaultIfEmpty()
                             join o in _context.Ords on ot.OrdId equals o.Id into join_o
                             from o in join_o.DefaultIfEmpty()
                             join cl in _context.ColorModels on ct.ColorId equals cl.Id into join_cl
                             from cl in join_cl.DefaultIfEmpty()
                             join dm in _context.Products on ct.DesignId equals dm.Id into join_dm
                             from dm in join_dm.DefaultIfEmpty()
                             join np in _context.Products on ct.NProductId equals np.Id into join_np
                             from np in join_np.DefaultIfEmpty()
                             join grd in _context.Grades on ct.GradeId equals grd.Id into join_grd
                             from grd in join_grd.DefaultIfEmpty()
                             join acc in _context.Accs on o.AccId equals acc.Id into join_acc
                             from acc in join_acc.DefaultIfEmpty()
                             orderby ct.Id
                             where ct.IsActive == true && ct.IsDeleted == false &&
                             ct.ChallanId == model.Id
                             select new GrnTransDto()
                             {
                                 Id = ct.Id,
                                 Cess = ct.Cess,
                                 CessPer = ct.CessPer,
                                 Cgst = ct.Cgst,
                                 CgstPer = ct.CgstPer,
                                 ChallanId = ct.ChallanId,
                                 ColorId = ct.ColorId.HasValue ? (int)ct.ColorId : 1,
                                 ColorName = cl.ColorName,
                                 Cops = ct.Cops,
                                 DesignId = ct.DesignId.HasValue ? (int)ct.DesignId : 1,
                                 DesignNo = dm.ProductCode,
                                 Disc = ct.Disc,
                                 DiscPer = ct.DiscPer,
                                 Freight = ct.Freight,
                                 FreightRate = ct.FreightRate,
                                 GradeId = ct.GradeId.HasValue ? (int)ct.GradeId : 1,
                                 GradeName = grd.GradeName,
                                 Gross = ct.Gross,
                                 Igst = ct.Igst,
                                 IgstPer = ct.IgstPer,
                                 LotNo = ct.LotNo,
                                 MiscId = ct.MiscId,
                                 OtherAdd = ct.OtherAdd,
                                 OtherLess = ct.OtherLess,
                                 Pcs = ct.Pcs,
                                 ProductId = (int)ct.ProductId,
                                 ProductName = pd.ProductName,
                                 Qty = ct.Qty,
                                 Rate = ct.Rate,
                                 RefId = ct.RefId,
                                 RefVoucherId = ct.RefVoucherId,
                                 Remark = ct.Remark,
                                 Sgst = ct.Sgst,
                                 SgstPer = ct.SgstPer,
                                 Total = ct.Total,
                                 UomId = (int)ct.UomId,
                                 OrdNo = o.VoucherNo,
                                 ODate = o.VoucherDate
                             }).ToList();

                prodDtos = _context.Prods.Where(x => x.RefId == model.Id && !x.IsDeleted)
                                         .ProjectToList<GrnProdDto>(config);

                this.grnTransDtoBindingSource1.DataSource = _list;
            }

            
          //  FinalTotal();
            IsLoadingData = false;
            this.Text = "Grey Purchase [View/Modify]";

        }

        private void ShowItemDetail(GrnTransDto er)
        {
            ProductModel prod = null;
            using (var db = new KontoContext())
            {
                prod = db.Products.Include("PType").SingleOrDefault(x => x.Id == er.ProductId);

            }
            if (prod == null || prod.SerialReq == "No") return;

            var frm = new ItemDetailView();
            frm.MetersPerKgs = metersKgsSpinEdit.Value;
            frm.TypeEnum = (ProductTypeEnum)prod.PTypeId;

            if (prod.PType.TypeName.ToUpper() == "YARN" || prod.PType.TypeName.ToUpper() == "POY")
            {
                frm.GridLayoutFileName = KontoFileLayout.GRN_Yarn_Item_Details;
                frm.Text = "Box Details";
            }
            else if (prod.PType.TypeName.ToUpper() == "GREY")
            {
                frm.GridLayoutFileName = KontoFileLayout.GRN_Grey_Item_Details;
                frm.Text = "Taka Details";
            }
            else if (prod.PType.TypeName.ToUpper() == "BEAM")
            {
                frm.GridLayoutFileName = KontoFileLayout.GRN_Beam_Item_Details;
                frm.Text = "Beam Details";
            }
            else
            {
                frm.Text = "Product Details";
                frm.GridLayoutFileName = KontoFileLayout.GRN_Finish_Item_Details;
            }
            frm.prodDtos = new BindingList<GrnProdDto>(this.prodDtos.Where(x => x.TransId == er.Id).ToList());
            if (frm.ShowDialog() != DialogResult.OK) return;
            var tempprod = frm.gridControl1.DataSource as BindingList<GrnProdDto>;

            //remove existing entry
            foreach (var po in tempprod)
            {
                this.prodDtos.Remove(po);
            }

            foreach (var pro in tempprod)
            {
                pro.RefId = this.PrimaryKey;
                pro.TransId = er.Id;
                pro.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);
                this.prodDtos.Add(pro);
            }
            foreach (var pro in frm.DelProd)
            {
                this.prodDtos.Remove(pro);
                this.DelProd.Add(pro);
            }
            er.Qty = tempprod.Sum(x => x.NetWt);
            er.Cops = tempprod.Sum(x => x.GrossWt);
            var sumPcs = tempprod.Sum(x => x.Tops);
            if (sumPcs > 0)
            {
                er.Pcs = sumPcs;
            }
            else
            {
                er.Pcs = tempprod.Count();
            }

            GridCalculation(er);
        }

        #endregion

        #region GridView
        private void GridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        private void GridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var view = sender as GridView;
            var pcs = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, colPcs));
            var qty = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, colQty));
            var rate = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, colRate));
            if (pcs <= 0)
            {
                view.SetColumnError(colPcs, "Pcs Must be greater than zero");
                e.Valid = false;
            }
            if (qty <= 0)
            {
                view.SetColumnError(colQty, "Meters Must be greater than zero");
                e.Valid = false;
            }
            if (rate <= 0)
            {
                view.SetColumnError(colRate, "Rate Must be greater than zero");
                e.Valid = false;
            }
        }

        private void GridView1_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            MessageBox.Show(this, e.ErrorText, "Invalid Value", MessageBoxButtons.OK, MessageBoxIcon.Error);
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        private void GridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            var view = sender as GridView;
            if (view.FocusedColumn.FieldName == "Pcs")
            {
                int _pcs = Convert.ToInt32(e.Value);
                if (_pcs <=0)
                {
                    e.Value = false;
                    e.ErrorText = "Pcs must be greater than zero";
                }
            }
            else if (view.FocusedColumn.FieldName == "Qty")
            {
                decimal _qty = Convert.ToDecimal(e.Value);
                if (_qty <= 0)
                {
                    e.Value = false;
                    e.ErrorText = "Meters must be greater than zero";
                }
            }
            else if (view.FocusedColumn.FieldName == "Rate")
            {
                decimal _rate = Convert.ToDecimal(e.Value);
                if (_rate <= 0)
                {
                    e.Value = false;
                    e.ErrorText = "Rate must be greater than zero";
                }
            }
        }

        private void GridView1_MouseUp(object sender, MouseEventArgs e)
        {
            DevExpress.Utils.DXMouseEventArgs args = (e as DevExpress.Utils.DXMouseEventArgs);
            GridView view = sender as GridView;
            GridHitInfo hi = view.CalcHitInfo(args.Location);
            if (hi.InColumn)
            {
                GridViewInfo ViewInfo = view.GetViewInfo() as GridViewInfo;
                GridState prevState = view.State;
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
        private void GridView1_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!"Pcs,Qty,Cops,ProductName".Contains(gridView1.FocusedColumn.FieldName)) return;
            var itm = gridView1.GetFocusedRow() as GrnTransDto;
            if (itm == null) return;
            if ("Pcs,Qty,Cops".Contains(gridView1.FocusedColumn.FieldName) && this.prodDtos.Any(x => x.TransId == itm.Id))
                e.Cancel = true;
            else if (gridView1.FocusedColumn.FieldName == "ProductName" && Convert.ToInt32(itm.RefId) > 0)
                e.Cancel = true;
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
            gridView1.FocusedColumn = gridView1.Columns["ProductName"];
        }
        private void GridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == null) return;
            var er = gridView1.GetRow(e.RowHandle) as GrnTransDto;
            if (er == null) return;
            if(e.Column == colSgstAmt || e.Column== colCgstAmt || e.Column == colIgstAmt)
                GridCalculation(er,true);
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
                var row = view.GetRow(view.FocusedRowHandle) as GrnTransDto;
                view.DeleteRow(view.FocusedRowHandle);
                DelTrans.Add(row);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                GridView view = sender as GridView;
                if (gridView1.FocusedColumn.FieldName == "ColorName")
                {
                    view.SetRowCellValue(view.FocusedRowHandle, colColorName, string.Empty);
                    view.SetRowCellValue(view.FocusedRowHandle, colColorId, 0);
                }
                else if (gridView1.FocusedColumn.FieldName == "DesignNo")
                {
                    view.SetRowCellValue(view.FocusedRowHandle, colDesignNo, string.Empty);
                    view.SetRowCellValue(view.FocusedRowHandle, colDesignId, 0);
                }
            }
        }

        private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var rw = gridView1.GetRow(e.RowHandle) as GrnTransDto;
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
                    else if (e.KeyCode == Keys.F1 && Convert.ToInt32(dr.RefId) == 0)
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
                else if (gridView1.FocusedColumn.FieldName == "DesignNo")
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
                else if (gridView1.FocusedColumn.FieldName == "LotNo")
                {
                    if (e.KeyCode == Keys.Return)
                    {
                        ShowItemDetail(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Grey Purchase GridControl KeyDown");
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
        private void LotNoRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
                ShowItemDetail(dr);
        }

        #endregion

        void headerEdit_Leave(object sender, EventArgs e)
        {
            activeCol.Caption = headerEdit.Text;
            headerEdit.Hide();
        }

        private void AccLookup1_ShownPopup(object sender, EventArgs e)
        {
            if (Convert.ToInt32(accLookup1.SelectedValue) == 0 || this.PrimaryKey != 0) return;

            var ordfrm = new PendingGoWindow();
            ordfrm.VoucherType = VoucherTypeEnum.GreyOrder;
            ordfrm.AccId = Convert.ToInt32(accLookup1.SelectedValue);
            if (ordfrm.ShowDialog() != DialogResult.OK) return;

            if (ordfrm.gridView1.FocusedRowHandle < 0) return;
            List<GrnTransDto> transDtos = new List<GrnTransDto>();
            int id = 0;
            var ord = ordfrm.gridView1.GetRow(ordfrm.gridView1.FocusedRowHandle) as GreyPendingOrderDto;
            GrnTransDto ct = new GrnTransDto();

            id = id - 1;
            ct.ProductId = Convert.ToInt32(ord.ProductId);
            ct.ProductName = ord.Product;
            ct.Pcs = Convert.ToInt32(ord.PendingQty);
            ct.Cops = 0;// ord.Cut != 0 ? ord.Cut : 0;
            ct.Qty = Convert.ToDecimal(ord.Cut) * ord.NoOfLot; // ord.PendingQty;
            ct.Rate = ord.rate;
            ct.DiscPer = ord.Disc;
            ct.Disc = 0;
            ct.Sgst = 0;
            ct.Cgst = 0;
            ct.Igst = 0;
            if (accLookup1.LookupDto.IsGst)
            {
                ct.SgstPer = ord.Sgst;
                ct.CgstPer = ord.Cgst;
                ct.Igst = 0;
            }
            else
            {
                ct.IgstPer = ord.Igst;
                ct.CgstPer = 0;
                ct.SgstPer = 0;
            }
            ct.UomId = Convert.ToInt32(ord.UomId);
            ct.DesignId = Convert.ToInt32(ord.DesignId);
            ct.ColorId = Convert.ToInt32(ord.ColorId);
            ct.GradeId = Convert.ToInt32(ord.GradeId);
            ct.OrdNo = ord.orderNo;
            //ct.OrdDate = ord.OrderDate;
            ct.ODate = Convert.ToInt32(ord.OrderDate.ToString("yyyyMMdd"));
            ct.RefId = ord.TransId;
            ct.MiscId = ord.Id;
            ct.RefVoucherId = ord.VoucherId;
            //  ct.ColorName = ord.ColorName;
            //ct.DesignNo = ord.DesignNo;
            //ct.GradeName = ord.GradeName;

            ct.Gross = decimal.Round(ct.Qty * ct.Rate, 2);

            decimal gross = ct.Gross - ct.Disc + ct.OtherAdd - ct.OtherLess + ct.Freight;

            ct.Sgst = decimal.Round(gross * ct.SgstPer / 100, 2);
            ct.Cgst = decimal.Round(gross * ct.CgstPer / 100, 2);//, MidpointRounding.AwayFromZero);
            ct.Igst = decimal.Round(gross * ct.IgstPer / 100, 2);//, MidpointRounding.AwayFromZero);

            ct.Total = gross + ct.Sgst + ct.Cgst + ct.Igst;
            transDtos.Add(ct);


            grnTransDtoBindingSource1.DataSource = transDtos;
            FinalTotal();
        }
        private void AccLookup1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (accLookup1.LookupDto == null) return;

            if (accLookup1.LookupDto.IsGst)
            {
                colSgst.Visible = true;
                colSgstAmt.Visible = true;
                colCgst.Visible = true;
                colCgstAmt.Visible = true;
                colIgst.Visible = false;
                colIgstAmt.Visible = false;
                for (int i = 0; i < gridView1.RowCount - 1; i++)
                {
                    var rw = gridView1.GetRow(i) as GrnTransDto;
                    var gst = rw.Igst + rw.Cgst + rw.Sgst;
                    var gstPer = rw.IgstPer + rw.CgstPer + rw.SgstPer;
                    rw.Igst = 0;
                    rw.IgstPer = 0;
                    rw.Sgst = gst / 2;
                    rw.Cgst = gst / 2;
                    rw.SgstPer = gstPer / 2;
                    rw.CgstPer = gstPer / 2;
                }
            }
            else if (accLookup1.LookupDto.IsIgst)
            {
                colSgst.Visible = false;
                colSgstAmt.Visible = false;
                colCgst.Visible = false;
                colCgstAmt.Visible = false;
                colIgst.Visible = true;
                colIgstAmt.Visible = true;
                for (int i = 0; i < gridView1.RowCount - 1; i++)
                {
                    var rw = gridView1.GetRow(i) as GrnTransDto;
                    var gst = rw.Igst + rw.Cgst + rw.Sgst;
                    var gstPer = rw.IgstPer + rw.CgstPer + rw.SgstPer;
                    rw.IgstPer = gstPer;
                    rw.Igst = gst;
                    rw.Sgst = 0;
                    rw.Cgst = 0;
                    rw.SgstPer = 0;
                    rw.CgstPer = 0;
                }
            }

            if (this.PrimaryKey == 0 && Convert.ToInt32(this.accLookup1.SelectedValue) > 0)
            {
               
                if (this.accLookup1.LookupDto.TcsReq.ToUpper() == "YES")
                    tcsPerTextEdit.Value = accLookup1.LookupDto.TcsPer;

            }



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

        }

        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                voucherLookup1.Focus();
                return;
            }
           
            if (tabControlAdv1.SelectedIndex == 1)
            {
                if(tabPageAdv2.Controls.Count > 0)
                {
                    var _list = tabPageAdv2.Controls[0] as GPListView;
                    _list.ActiveControl = _list.KontoGrid;
                    this.Text = "Grey Purchase [View]";
                    return;
                }
                var _ListView = new GPListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Grey Purchase [View]";

            }
            if (tabControlAdv1.SelectedIndex == 2)
            {
                if (tabPageAdv3.Controls.Count > 0) return;
                var _frm = new AnalysisUserControl(VoucherTypeEnum.GrayPurchaseInvoice,true);
                _frm.AnaDataTable = GetPurchaseTable();
                _frm.Dock = DockStyle.Fill;
                tabPageAdv3.Controls.Add(_frm);
                this.Text = "Grey Purchase Anlysis";
            }
            if (tabControlAdv1.SelectedIndex == 3)
            {
                if (tabPageAdv4.Controls.Count > 0) return;
                //var _frm = new ParaMainView();
                var _frm = Activator.CreateInstance("Konto.Reporting", "Konto.Reporting.Para.BillPara.ParaMainView").Unwrap() as KontoForm;

                _frm.TopLevel = false;
                _frm.Parent = tabPageAdv4;
                _frm.ReportFilterType = "GPURCHASE";
                _frm.Location = new Point(tabPageAdv4.Location.X + tabPageAdv4.Width / 2 - _frm.Width / 2, tabPageAdv4.Location.Y + tabPageAdv4.Height / 2 - _frm.Height / 2);
                _frm.Show();// = true;

            }
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if(this.PrimaryKey == 0)
                    SaveDataAsync(true);
                else
                    SaveDataAsync(false);
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Grey Purchase Save");
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
            this.IsLoadingData = false;
            this.FilterView = new List<ChallanModel>();
            this.Text = "Grey Purchase [Add New]";
            rcmLookUpEdit.EditValue = "NO";
            invTypeLookUpEdit.EditValue = "Regular";
            itcLookUpEdit.EditValue = "Inputs";
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
            if (voucherLookup1.GroupDto!=null && Convert.ToInt32(voucherLookup1.GroupDto.AccId) > 0)
            {
                bookLookup.SelectedValue = voucherLookup1.GroupDto.AccId;
                bookLookup.SetAcc(Convert.ToInt32(voucherLookup1.GroupDto.AccId));
            }

            DelTrans = new List<GrnTransDto>();
            DelProd = new List<GrnProdDto>();
            prodDtos = new List<GrnProdDto>();
            this.grnTransDtoBindingSource1.DataSource = new List<GrnTransDto>();

        }
        public override void ResetPage()
        {
            base.ResetPage();
            this.IsLoadingData = false;
            accLookup1.SetEmpty();
            bookLookup.SetEmpty();
            challanNotextEdit.Text = string.Empty;
            billNoTextEdit.Text = string.Empty;
            voucherDateEdit.DateTime = DateTime.Now;
            billDateEdit.DateTime = DateTime.Now;
            voucherNoTextEdit.Text = string.Empty;
            agentLookup.SetEmpty();
            transportLookup.SetEmpty();
            empLookup1.SetEmpty();
            lrNotextEdit.Text = string.Empty;
            lrDateEdit.EditValue = DateTime.Now;
            remarkTextEdit.Text = string.Empty;
            ewayTextEdit.Text = string.Empty;
            roundoffSpinEdit.Value = 0;
            billAmtSpinEdit.Value = 0;
            tcsPerTextEdit.Value = 0;
            tcsAmtTextEdit.Value = 0;
            DelTrans = new List<GrnTransDto>();
            DelProd = new List<GrnProdDto>();
            prodDtos = new List<GrnProdDto>();

        }
        public override void EditPage(int _key)
        {
           
            base.EditPage(_key);
            this.PrimaryKey = _key;


            using (var db = new KontoContext())
            {
               
                if (this.OpenForLookup)
                {
                    var bl = db.BillTrans.FirstOrDefault(x=>x.BillId == _key);
                    
                    this.PrimaryKey = Convert.ToInt32(bl.RefId);
                }
                _key = this.PrimaryKey;
                var bill = db.Challans.Find(_key);
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
            if (!string.IsNullOrEmpty(challanNotextEdit.Text.Trim()))
            {
                filter.Add(new Filter { PropertyName = "ChallanNo", Operation = Op.Equals, Value = challanNotextEdit.Text.Trim() });
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
                FilterView = db.Challans.Where(ExpressionBuilder.GetExpression<ChallanModel>(filter))
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

            var _find = new ChallanModel();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GrnTransDto, ChallanTransModel>().ForMember(x => x.Id, p => p.Ignore());
                cfg.CreateMap<GrnProdDto, ProdModel>().ForMember(x => x.Id, p => p.Ignore());
            });

            var _translist = grnTransDtoBindingSource1.DataSource as List<GrnTransDto>;
            List<ChallanTransModel> Trans = new List<ChallanTransModel>();
            List<ProdModel> ProdList = new List<ProdModel>();
            using (var db = new KontoContext())
            {
                using (var _tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        string createuser = KontoGlobals.UserName;
                        DateTime createdate = DateTime.Now;
                        if (this.PrimaryKey != 0)
                        {
                            _find = db.Challans.Find(this.PrimaryKey);
                        }
                        _find.TotalQty = _translist.Sum(x => x.Qty);
                        _find.TotalPcs = _translist.Sum(x => x.Pcs);
                        _find.TotalAmount = _translist.Sum(x => x.Total);

                        if(!UpdateChallan(db, _find))
                        {
                            _tran.Rollback();
                            return;
                        }

                        var map = new Mapper(config);


                        foreach (var item in _translist)
                        {
                            var transid = item.Id;
                            item.ChallanId = _find.Id;
                            var tranModel = new ChallanTransModel();
                            if (item.Id > 0)
                            {
                                tranModel = db.ChallanTranses.Find(item.Id);
                            }
                            map = new Mapper(config);
                            map.Map(item, tranModel);

                            if (tranModel.Id <= 0)
                            {
                                db.ChallanTranses.Add(tranModel);
                                db.SaveChanges();

                            }
                            item.Id = tranModel.Id;
                            Trans.Add(tranModel);
                            // add subdetails item details
                            var prlist = prodDtos.Where(k => (k.TransId == transid && k.VoucherId == _find.VoucherId)).ToList();

                            foreach (var p in prlist)
                            {
                                p.ProdStatus = "STOCK";
                                p.RefId = _find.Id;
                                p.TransId = tranModel.Id;
                                p.LotNo = tranModel.LotNo;
                                p.YearId = (int)KontoGlobals.YearId;
                                p.CompId = KontoGlobals.CompanyId;
                                p.BranchId = KontoGlobals.BranchId;
                                p.ProductId = tranModel.ProductId;
                                p.VoucherDate = _find.VoucherDate;
                                
                                if (tranModel.ColorId != null && tranModel.ColorId != 0)
                                    p.ColorId = tranModel.ColorId;
                                else if (p.ColorId == 0)
                                    p.ColorId = 1;

                                if (tranModel.GradeId != null && tranModel.GradeId != 0)
                                    p.GradeId = tranModel.GradeId;
                                else if (p.GradeId == 0)
                                    p.GradeId = 1;

                                if (tranModel.DesignId != null && tranModel.DesignId != 0)
                                    p.PlyProductId = tranModel.DesignId;
                                else if (p.PlyProductId == 0)
                                    p.PlyProductId = 1;

                                ProdModel prodModel = new ProdModel();
                                if (p.Id > 0)
                                {
                                    prodModel = db.Prods.Find(p.Id);
                                }
                                map.Map(p, prodModel);
                                
                               


                                if (p.Id <= 0)
                                {
                                    prodModel.CProductId = prodModel.ProductId;
                                    db.Prods.Add(prodModel);
                                    db.SaveChanges();
                                }
                                ProdList.Add(prodModel);
                            }

                        }
                        //delete item from ord trans
                        foreach (var item in DelTrans)
                        {
                            if (item.Id == 0) continue;
                            var _model = db.ChallanTranses.Find(item.Id);
                            _model.IsDeleted = true;

                            var delprod = db.Prods.Where(p => p.TransId == item.Id).ToList();
                            foreach (var item1 in delprod)
                            {
                                item1.IsDeleted = true;
                            }
                        }

                        // delete from item details
                        foreach (var p in DelProd)
                        {
                            if (p.Id == 0) continue;
                            var prd = db.Prods.Find(p.Id);
                            if (prd != null)
                                prd.IsDeleted = true;
                        }

                        //sotock effect
                        var stk = db.StockTranses.Where(k => k.MasterRefId == _find.RowId).ToList();
                        if (stk != null)
                            db.StockTranses.RemoveRange(stk);

                        foreach (var item in Trans)
                        {
                            string TableName = "grn";
                            var stockReq = db.Products.FirstOrDefault(k => k.Id == item.ProductId).StockReq;
                            if (stockReq == "No") continue;

                            //var prList = ProdList.Where(x => x.TransId == item.Id).ToList();
                            //if (prList.Count > 0)
                            //{
                            //    foreach (var prod in prList)
                            //    {
                            //        StockEffect.StockTransChlnProdEntry(_find, item, false, TableName, KontoGlobals.UserName, db, prod, false);
                            //    }
                            //}
                            //else
                            //{
                                StockEffect.StockTransChlnEntry(_find, item, false, TableName, KontoGlobals.UserName, db);
                            //}
                        }

                       if(! UpdateBill(db, _find))
                        {
                            _tran.Rollback();
                            return;
                        }

                        db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Grn Save");
                        MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());

                    }
                }
            }



            if (IsSaved)
            {
                // NewRec();

                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage + " Voucher No.: " + _find.VoucherNo, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (!this.OpenForLookup)
                {
                    if (newmode && this.prodDtos.Count > 0 && MessageBoxAdv.Show(this, "Issue to mill ?", "Mill Issue", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        var win = new MiOptionView();
                        win.Model = _find;
                        win.TranModel = Trans.FirstOrDefault();
                        win.MIProdList = ProdList.Where(x => x.TransId == win.TranModel.Id).ToList();
                        if (win.ShowDialog() == DialogResult.OK && !win.IsOpenIssued)
                        {
                            if (MessageBox.Show("Print Mill Issue?", "Confirmation !!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                var frm = new DocPrintParaView(VoucherTypeEnum.MillIssue, "Mill Challan", win.IssueList[0].ChallanNo,
                                    win.IssueList[win.IssueList.Count - 1].ChallanNo, "MI", "ChallanId");
                                frm.EditKey = Convert.ToInt32(win.IssueList[0].Id);
                                frm.ShowDialog();
                            }
                        }
                    }
                        base.SaveDataAsync(newmode);
                        this.ResetPage();
                        this.NewRec();
                        voucherLookup1.buttonEdit1.Focus();

                    
                }
                else if (this.OpenForLookup)
                {
                    this.Close();
                    this.Dispose();
                }
            }
        }

        private bool UpdateChallan(KontoContext db, ChallanModel model)
        {

            model.DivId = 1;
            model.BillType = invTypeLookUpEdit.EditValue.ToString();
            model.Rcm = rcmLookUpEdit.EditValue.ToString();
            model.Itc = itcLookUpEdit.EditValue.ToString();

            model.ChallanType = (int)ChallanTypeEnum.PURCHASE;
            model.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);
            model.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));

            model.AccId = Convert.ToInt32(accLookup1.SelectedValue);
            model.BookAcId = Convert.ToInt32(bookLookup.SelectedValue);
            model.RcdDate = billDateEdit.DateTime;
            model.VoucherNo = voucherNoTextEdit.Text.Trim();

            model.AgentId = Convert.ToInt32(agentLookup.SelectedValue);
            model.ChallanNo = challanNotextEdit.Text.Trim();
            model.BillNo = billNoTextEdit.Text.Trim();

            model.EmpId = Convert.ToInt32(empLookup1.SelectedValue);
            model.StoreId = Convert.ToInt32(storeLookUpEdit.EditValue);

            model.Remark = remarkTextEdit.Text.Trim();
            model.TransId = Convert.ToInt32(transportLookup.SelectedValue);
            model.DocNo = lrNotextEdit.Text.Trim();
            model.DocDate = Convert.ToDateTime(lrDateEdit.EditValue);
            model.TypeId = (int)VoucherTypeEnum.GrayPurchaseChallan;
            model.CompId = KontoGlobals.CompanyId;
            model.YearId = KontoGlobals.YearId;
            model.BranchId = KontoGlobals.BranchId;
            model.RoundOff = roundoffSpinEdit.Value;

            var _translist = grnTransDtoBindingSource1.DataSource as List<GrnTransDto>;
            model.TotalAmount = billAmtSpinEdit.Value;

            model.TotalQty = _translist.Sum(x => x.Qty);
            model.TotalPcs = _translist.Sum(x => x.Pcs);
            model.IsActive = true;
            model.Extra1 = ewayTextEdit.Text.Trim();
            model.TcsPer = tcsPerTextEdit.Value;
            model.TcsAmt = tcsAmtTextEdit.Value;

            if (model.Id == 0)
            {
                if(!voucherLookup1.GroupDto.ManualSeries)
                    model.VoucherNo = DbUtils.NextSerialNo(model.VoucherId, db);

                if (DbUtils.CheckExistChllanVoucherNo(model.VoucherId, model.VoucherNo, db, model.Id))
                {
                    MessageBox.Show("Duplicate Voucher No Not Allowed");
                    return false;
                }
                db.Challans.Add(model);
                db.SaveChanges();
            }
            return true;

        }

        private bool UpdateBill(KontoContext db, ChallanModel model)
        {
            BillModel billModel = new BillModel();

            var _translist = grnTransDtoBindingSource1.DataSource as List<GrnTransDto>;

            if (this.PrimaryKey != 0)
            {
                billModel = db.Bills.FirstOrDefault(x => x.RefId == model.Id && x.RefVoucherId == model.VoucherId);

            }
            billModel.CompId = KontoGlobals.CompanyId;
            billModel.YearId = KontoGlobals.YearId;
            billModel.BranchId = KontoGlobals.BranchId;
            billModel.TypeId = (int)VoucherTypeEnum.GrayPurchaseInvoice;
            billModel.BookAcId = model.BookAcId;
            billModel.AccId = model.AccId;
            billModel.VoucherDate = model.VoucherDate;
            billModel.BillNo = model.BillNo;
            billModel.RcdDate = model.RcdDate;
            billModel.GrossAmount = _translist.Sum(x => x.Gross);
            billModel.TotalAmount = billAmtSpinEdit.Value;
            billModel.TotalQty = _translist.Sum(x => x.Qty);
            billModel.TotalPcs = _translist.Sum(x => x.Pcs);
            billModel.RoundOff = roundoffSpinEdit.Value;
            billModel.EwayBillNo = ewayTextEdit.Text.Trim();
            billModel.AgentId = model.AgentId;
            billModel.RefNo = model.ChallanNo;
            billModel.StoreId = model.StoreId;
            billModel.BillType = model.BillType;
            billModel.EmpId = model.EmpId;
            billModel.Rcm = model.Rcm;
            billModel.Itc = model.Itc;
            billModel.TransId = model.TransId;
            billModel.DocNo = model.DocNo;
            billModel.DocDate = model.DocDate;
            billModel.VehicleNo = model.VehicleNo;
            billModel.Remarks = model.Remark;
            billModel.DivisionId = model.DivId;
            billModel.RefId = model.Id;
            billModel.RefVoucherId = model.VoucherId;
            billModel.TcsPer = tcsPerTextEdit.Value;
            billModel.TcsAmt = tcsAmtTextEdit.Value;
            billModel.IsActive = true;

            if (this.PrimaryKey == 0)
            {
                int vtypeid = (int)VoucherTypeEnum.GrayPurchaseInvoice;
                var vouchr = db.Vouchers.FirstOrDefault(k => k.VTypeId == vtypeid);
                billModel.VoucherId = vouchr.Id;
                billModel.VoucherNo = model.VoucherNo;  //DbUtils.NextSerialNo((int)billModel.VoucherId, db);

                //if (DbUtils.CheckExistVoucherNo(billModel.VoucherId, billModel.VoucherNo, db, billModel.Id))
                //{
                //    MessageBox.Show("Duplicate Voucher No Not Allowed");
                //    return false;
                //}

                db.Bills.Add(billModel);
                db.SaveChanges();
            }
            else
            {
                var bt = db.BillTrans.Where(x => x.BillId == billModel.Id);
                db.BillTrans.RemoveRange(bt);
            }
            List<BillTransModel> billTrans = new List<BillTransModel>();
            foreach (var ctrModel in _translist)
            {
                var btModel = new BillTransModel();
                btModel.ProductId = ctrModel.ProductId;
                btModel.ColorId = ctrModel.ColorId;
                btModel.GradeId = ctrModel.GradeId;
                btModel.DesignId = ctrModel.DesignId;
                btModel.UomId = ctrModel.UomId;
                btModel.LotNo = ctrModel.LotNo;
                btModel.Pcs = ctrModel.Pcs;
                btModel.Qty = ctrModel.Qty != 0 ? (decimal)ctrModel.Qty : 0;
                btModel.Rate = ctrModel.Rate != 0 ? (decimal)ctrModel.Rate : 0;
                btModel.Sgst = ctrModel.Sgst != 0 ? (decimal)ctrModel.Sgst : 0;
                btModel.SgstPer = ctrModel.SgstPer != 0 ? (decimal)ctrModel.SgstPer : 0;
                btModel.Cgst = ctrModel.Cgst != 0 ? (decimal)ctrModel.Cgst : 0;
                btModel.CgstPer = ctrModel.CgstPer != 0 ? (decimal)ctrModel.CgstPer : 0;
                btModel.Igst = ctrModel.Igst != 0 ? (decimal)ctrModel.Igst : 0;
                btModel.IgstPer = ctrModel.IgstPer != 0 ? (decimal)ctrModel.IgstPer : 0;
                btModel.Disc = ctrModel.DiscPer != 0 ? (decimal)ctrModel.DiscPer : 0;
                btModel.DiscAmt = ctrModel.Disc != 0 ? (decimal)ctrModel.Disc : 0;
               

                btModel.RefId = model.Id;
                btModel.RefTransId = ctrModel.Id;
                btModel.RefVoucherId = model.VoucherId;

                btModel.FreightRate = ctrModel.FreightRate != 0 ? (decimal)ctrModel.FreightRate : 0;
                btModel.Freight = ctrModel.Freight != 0 ? (decimal)ctrModel.Freight : 0;
                btModel.OtherAdd = ctrModel.OtherAdd != 0 ? (decimal)ctrModel.OtherAdd : 0;
                btModel.OtherLess = ctrModel.OtherLess != 0 ? (decimal)ctrModel.OtherLess : 0;
                btModel.CessPer = ctrModel.CessPer != 0 ? (decimal)ctrModel.CessPer : 0;
                btModel.Cess = ctrModel.Cess != 0 ? (decimal)ctrModel.Cess : 0;

                btModel.Total = ctrModel.Gross;
                btModel.DiscAmt = btModel.Total * btModel.Disc / 100;
                if (btModel.DiscAmt == 0)
                    btModel.DiscAmt = ctrModel.Disc;

                //  decimal gross = btModel.Total - btModel.DiscAmt + btModel.Freight + btModel.OtherAdd - btModel.OtherLess;

                //    btModel.Sgst = decimal.Round(gross * btModel.SgstPer / 100, 2, MidpointRounding.AwayFromZero);
                //  btModel.Cgst = decimal.Round(gross * btModel.CgstPer / 100, 2, MidpointRounding.AwayFromZero); //, MidpointRounding.AwayFromZero);
                // btModel.Igst = decimal.Round(gross * btModel.IgstPer / 100, 2, MidpointRounding.AwayFromZero); //, MidpointRounding.AwayFromZero);

                btModel.NetTotal = ctrModel.Total;
                btModel.BillId = billModel.Id;
                db.BillTrans.Add(btModel);
                billTrans.Add(btModel);
            }


            LedgerEff.BillRefEntry("Credit", billModel, _translist.FirstOrDefault().ProductId, db);

            LedgerEff.LedgerTransEntry("Credit", billModel, db, billTrans);
            return true;
        }
        #endregion


        private DataTable GetPurchaseTable()
        {
            using (var con = new SqlConnection(KontoGlobals.sqlConnectionString.ConnectionString))
            {

                using (var cmd = new SqlCommand("dbo.gp_analysis", con))
                {
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@fromDate", SqlDbType.Int).Value = KontoGlobals.FromDate;
                    cmd.Parameters.Add("@ToDate", SqlDbType.Int).Value = KontoGlobals.ToDate;
                    cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = KontoGlobals.CompanyId;
                    cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = KontoGlobals.BranchId;
                    cmd.Parameters.Add("@YearId", SqlDbType.Int).Value = KontoGlobals.YearId;
                    cmd.Parameters.Add("@VTypeId", SqlDbType.Int).Value = (int)VoucherTypeEnum.GrayPurchaseChallan;
                    
                    
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    var DtCriterias = new DataTable();
                    DtCriterias.Load(cmd.ExecuteReader());
                    con.Close();
                    return DtCriterias;
                }
            }
        }

      

        private void roundoffSpinEdit_ValueChanged(object sender, EventArgs e)
        {
            if (!roundoffSpinEdit.ContainsFocus) return;
            gridView1.UpdateTotalSummary();
            var ntotal = Convert.ToDecimal(colNetTotal.SummaryItem.SummaryValue);

            ntotal = ntotal + roundoffSpinEdit.Value;

            billAmtSpinEdit.Value = ntotal;
        }
    }
}
