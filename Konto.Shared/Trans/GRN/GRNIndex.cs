using Aspose.Cells;
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
using Konto.Shared.Trans.Po;
using Serilog;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ExpressionBuilder = Konto.Core.Shared.Libs.ExpressionBuilder;

namespace Konto.Shared.Trans.GRN
{
    public partial class GRNIndex : KontoMetroForm
    {
        private List<GrnDto> FilterView = new List<GrnDto>();
        private List<GrnTransDto> DelTrans = new List<GrnTransDto>();
        private List<GrnProdDto> prodDtos = new List<GrnProdDto>();
        private List<GrnProdDto> DelProd = new List<GrnProdDto>();
        private int GateId = 0;
        private int GateVoucherId = 0;

        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        private string _barcodeDate;

        public GRNIndex()
        {
            InitializeComponent();
            this.Load += GrnIndex_Load;
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
            this.MainLayoutFile = KontoFileLayout.Grn_Index;
            this.GridLayoutFile = KontoFileLayout.Grn_Trans;
            
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

            this.FirstActiveControl = grnTypeLookUpEdit;
        }

        private void VoucherLookup1_SelectedValueChanged(object sender, EventArgs e)
        {
            if(this.PrimaryKey == 0 && Convert.ToInt32(voucherLookup1.SelectedValue) > 0)
            {
                voucherNoTextEdit.Text = "New-" +  DbUtils.NextSerialNo(Convert.ToInt32(voucherLookup1.SelectedValue), 1);
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
           if (view.FocusedColumn.FieldName == "Qty")
            {
                decimal _qty = Convert.ToDecimal(e.Value);
                if (_qty <= 0)
                {
                    e.Value = false;
                    e.ErrorText = "Qty must be greater than zero";
                }
            }
            else if (view.FocusedColumn.FieldName == "ProductId")
            {
                decimal _rate = Convert.ToInt32(e.Value);
                if (_rate <= 0)
                {
                    e.Value = false;
                    e.ErrorText = "Invalid Item Name";
                }
            }
            else if (view.FocusedColumn.FieldName == "UomId")
            {
                decimal _rate = Convert.ToInt32(e.Value);
                if (_rate <= 0)
                {
                    e.Value = false;
                    e.ErrorText = "Invalid Unit Name";
                }
            }
        }

        private void RepositoryItemHyperLinkEdit1_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            if (grnTypeLookUpEdit.Text != "Purchase") return;
            var er = gridView1.GetFocusedRow() as GrnTransDto;
            var view = new PoIndex();
            
            int id = Convert.ToInt32(er.MiscId);
            if (id == 0) return;
            var window = new PoIndex();
            window.Tag = MenuId.Purchase_Order;
            window.EditKey = id;
            window.ShowDialog();
            
        }

        private void GridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        private void GridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var view = sender as GridView;
            var itemid = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, colProductId));
            var qty = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, colQty));
            var unit = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, colUomId));
            if (itemid <= 0)
            {
                view.SetColumnError(colProductId, "Invalid Product");
                e.Valid = false;
            }
            if (qty <= 0)
            {
                view.SetColumnError(colQty, "Meters Must be greater than zero");
                e.Valid = false;
            }
            if (unit <= 0)
            {
                view.SetColumnError(colUomId, "Invalid Unit");
                e.Valid = false;
            }
        }


        #region UDF
        private void SetGridColumn()
        {
            colColorName.Visible = GRNPara.Color_Required;
            colDesignNo.Visible = GRNPara.Design_Required;
            colGradeName.Visible = GRNPara.Grade_Required;
            //colCess.Visible = GRNPara.Cess_Required;
            //colCessAmt.Visible = GRNPara.Cess_Required;
            colCops.Visible = GRNPara.Cut_Required;
            colFreight.Visible = GRNPara.Freight_Required;
            colFreightRate.Visible = GRNPara.Freight_Required;
            colLotNo.Visible = GRNPara.LotNo_Required;
            //colOtherAdd.Visible = GRNPara.OtherAdd_Required;
            //colOtherLess.Visible = GRNPara.OtherLess_Required;

            if (KontoGlobals.PackageId == 1)
            {
                colCops.Caption = "Cut";
                colQty.Caption = "Meters";
            }
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
      
        public void GridCalculation(GrnTransDto er)
        {
            if (er.Pcs > 0 && er.Cops > 0 && KontoGlobals.PackageId==1)
                er.Qty = er.Pcs * er.Cops;

            var dr = uomRepositoryItemLookUpEdit.GetDataSourceRowByKeyValue(er.UomId) as UomLookupDto;
             
            if (dr != null && dr.RateOn == "N" && er.Qty > 0)
            {
                er.Gross = decimal.Round(er.Pcs * er.Rate, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                er.Gross = decimal.Round(er.Qty * er.Rate, 2, MidpointRounding.AwayFromZero);
            }

            if (er.DiscPer > 0)
                er.Disc = decimal.Round(er.Gross * er.DiscPer / 100, 2, MidpointRounding.AwayFromZero);
            decimal gross = er.Gross - er.Disc;

            if (er.FreightRate > 0)
                er.Freight = decimal.Round(er.Qty * er.FreightRate / 100, 2, MidpointRounding.AwayFromZero);

            gross = gross + er.Freight + er.OtherAdd - er.OtherLess;

            er.Sgst = decimal.Round(gross * er.SgstPer / 100, 2, MidpointRounding.AwayFromZero);
            er.Cgst = decimal.Round(gross * er.CgstPer / 100, 2, MidpointRounding.AwayFromZero);
            er.Igst = decimal.Round(gross * er.IgstPer / 100, 2, MidpointRounding.AwayFromZero);

            er.Cess = decimal.Round(er.Qty * er.CessPer, 2, MidpointRounding.AwayFromZero);
            er.Total = gross + er.Sgst + er.Cgst + er.Igst;

            gridView1.UpdateCurrentRow();
        }
        private void SetParameter()
        {
            using (var db = new KontoContext())
            {
                var _paralists = db.CompParas.Include("SysPara")
                              .Where(x => x.SysPara.Category == "GRN" && x.CompId == KontoGlobals.CompanyId)
                             .ToList();

                foreach (var item in _paralists)
                {
                    var value = item.ParaValue;
                    switch (item.ParaId)
                    {
                        case 32://GRN
                            {
                                GRNPara.Color_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 33:
                            {
                                GRNPara.Batch_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 34:
                            {
                                GRNPara.LotNo_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 35:
                            {
                                GRNPara.Design_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 36:
                            {
                                GRNPara.Grade_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 37:
                            {
                                GRNPara.Cut_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 38:
                            {
                                GRNPara.Freight_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 39:
                            {
                                GRNPara.Auto_Book_Id = Convert.ToInt32(value);
                                break;
                            }
                        case 40:
                            {
                                GRNPara.Auto_Voucher_Id = Convert.ToInt32(value);
                                break;
                            }
                        case 41:
                            {
                                GRNPara.Taka_Detail_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 42:
                            {
                                GRNPara.Auto_Bill_Generate_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 54:
                            {
                                GRNPara.Direct_Issue_From_Purchase = (value == "Y") ? true : false;
                                break;
                            }

                        case 175:
                            {
                                GRNPara.Generate_Barcode = (value == "Y") ? true : false;
                                break;
                            }
                    }
                }
            }

        }
        private void OpenItemLookup(int _selvalue, GrnTransDto er)
        {
            var frm = new ProductLkpWindow();

            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Product_Master;
            frm.VoucherType = VoucherTypeEnum.Inward;

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
        private void OpenGradeLookup(int _selvalue, GrnTransDto er)
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
        private void OpenDesignLookup(int _selvalue, GrnTransDto er)
        {
            var frm = new DesignLkpWindow();
            frm.SelectedValue = _selvalue;
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
        
            using (var db = new KontoContext())
            {
               var  TransTypeList = (from p in  db.transTypes
                                        where p.IsActive && !p.IsDeleted && (p.Category.ToUpper() == "INWARD" || p.Id == (int)ChallanTypeEnum.RETURNABLE_ITEM || p.Category == null )
                                     select new BaseLookupDto()
                                     {
                                         DisplayText = p.TypeName,Id = p.Id
                                     }).ToList();

                var _divLists = (from p in db.Divisions
                                 where p.IsActive && !p.IsDeleted
                                 select new BaseLookupDto()
                                 {
                                     DisplayText = p.DivisionName,
                                     Id = p.Id
                                 }).ToList();

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

               
               
                uomRepositoryItemLookUpEdit.DataSource = _uomlist;
                grnTypeLookUpEdit.Properties.DataSource = TransTypeList;
                divLookUpEdit.Properties.DataSource = _divLists;
                storeLookUpEdit.Properties.DataSource = _storeLists;
            }
        }

        private bool ValidateData()
        {

            var trans = grnTransDtoBindingSource1.DataSource as List<GrnTransDto>;

            var dt = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
            var reqdt= Convert.ToInt32(receiveDateEdit.DateTime.ToString("yyyyMMdd"));
            if (string.IsNullOrEmpty(divLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Division", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                divLookUpEdit.Focus();
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
            else if (string.IsNullOrEmpty(challanNotextEdit.Text.Trim()))
            {
                MessageBoxAdv.Show(this, "Invalid Challan No.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                challanNotextEdit.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(grnTypeLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Inward Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                grnTypeLookUpEdit.Focus();
                return false;
            }
            //else if (dt > KontoGlobals.ToDate || dt < KontoGlobals.FromDate)
            //{
            //    MessageBoxAdv.Show(this, "Challan date out of financial range", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    voucherDateEdit.Focus();
            //    return false;
            //}
            else if (reqdt < dt )
            {
                MessageBoxAdv.Show(this, "Receive Date Cant Not be Less than order date", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                receiveDateEdit.Focus();
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
            else if(trans.Sum(x=>x.Qty) ==0)
            {
                MessageBoxAdv.Show(this, "zero Qty Can Not Be accepted", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }
            else if (trans.Any(x => x.ProductId==0))
            {
                MessageBoxAdv.Show(this, "Empty Product Can bot be accepted", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }


            return true;
        }

        private void LoadData(GrnDto model, KontoContext _context =null)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            divLookUpEdit.EditValue = model.DivId;
            grnTypeLookUpEdit.EditValue = model.ChallanType;
            voucherLookup1.SelectedValue = model.VoucherId;
            voucherLookup1.SetGroup(model.VoucherId);
            voucherDateEdit.EditValue = KontoUtils.IToD(model.VoucherDate);

            voucherNoTextEdit.Text = model.VoucherNo;
            accLookup1.SelectedValue = model.AccId;
            accLookup1.SetAcc(model.AccId);
            receiveDateEdit.EditValue = model.RcdDate;

            if (Convert.ToInt32(model.AgentId) != 0)
            {
                agentLookup.SelectedValue = model.AgentId;
                agentLookup.SetAcc(Convert.ToInt32(model.AgentId));
            }
            challanNotextEdit.Text = model.ChallanNo;
            billNoTextEdit.Text = model.BillNo;

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
            gateSrNoTextEdit.Text = model.Extra1;
            this.GateId = model.RefId;
            this.GateVoucherId = model.RefVoucherId;

            createdLabelControl.Text = "Created By: " + model.CreateUser + " [ " + model.CreateDate + " ]";
            modifyLabelControl.Text = "Modified By: " + model.ModifyUser + " [ " + model.ModifyDate ?? string.Empty  + " ]";

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProdModel, GrnProdDto>();
            });


            if (_context == null)
            {
                _context = new KontoContext();
                _context.Database.CommandTimeout = 0;
            }

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
                                  Id = ct.Id,Cess=ct.Cess,CessPer=ct.CessPer,Cgst=ct.Cgst,CgstPer=ct.CgstPer,
                                  ChallanId=ct.ChallanId,ColorId=ct.ColorId.HasValue ? (int)ct.ColorId: 1,ColorName=cl.ColorName,Cops=ct.Cops,
                                  DesignId= ct.DesignId.HasValue? (int)ct.DesignId:1,DesignNo=dm.ProductCode,Disc=ct.Disc,DiscPer=ct.DiscPer,Freight=ct.Freight,
                                  FreightRate=ct.FreightRate,GradeId=ct.GradeId.HasValue ? (int)ct.GradeId:1,
                                  GradeName=grd.GradeName,Gross=ct.Gross,Igst=ct.Igst,
                                  IgstPer=ct.IgstPer,LotNo=ct.LotNo,MiscId=ct.MiscId,OtherAdd=ct.OtherAdd,OtherLess=ct.OtherLess,
                                  Pcs=ct.Pcs,ProductId=(int)ct.ProductId,ProductName=pd.ProductName,Qty=ct.Qty,Rate=ct.Rate,RefId=ct.RefId,
                                  RefVoucherId=ct.RefVoucherId,Remark=ct.Remark,Sgst=ct.Sgst,SgstPer=ct.SgstPer,Total=ct.Total,UomId=(int)ct.UomId,
                                  OrdNo = o.VoucherNo,ODate= o.VoucherDate
                              }).ToList();

                prodDtos = _context.Prods.Where(x => x.RefId == model.Id && !x.IsDeleted)
                                         .ProjectToList<GrnProdDto>(config);


                this.grnTransDtoBindingSource1.DataSource = _list;

            if (DbUtils.CheckExistinBill(model.Id, model.VoucherId, _context))
            {
                okSimpleButton.Enabled = false;
                this.UpdateMessage("Edit Restricted! Invoice has Reference");
               
            }
            if (DbUtils.CheckExistinChallan(model.Id, model.VoucherId, _context))
            {
                okSimpleButton.Enabled = false;
                this.UpdateMessage("Edit Restricted! Issue has Reference");

            }
            

            

            this.Text = "Grn (Inward) [View/Modify]";

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
            frm.prodDtos = new BindingList<GrnProdDto>(this.prodDtos.Where(x=>x.TransId == er.Id).ToList());
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
            er.Qty =tempprod.Sum(x => x.NetWt);
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
            if (!"Pcs,Qty,ProductName".Contains(gridView1.FocusedColumn.FieldName)) return;
            var itm = gridView1.GetFocusedRow() as GrnTransDto;
            if (itm == null) return;
            if ("Pcs,Qty".Contains(gridView1.FocusedColumn.FieldName)  && this.prodDtos.Any(x => x.TransId == itm.Id))
                e.Cancel = true;
            //else if (Convert.ToInt32(itm.RefId) >0 && gridView1.FocusedColumn.FieldName=="ProductName") 
            //    e.Cancel = true;
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
            GridCalculation(er);
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
            rw.LotNo = voucherNoTextEdit.Text.Substring(4, voucherNoTextEdit.Text.Length - 4);
            if(rw.RefId == null && gridView1.RowCount > 1)
            {
                var prw = gridView1.GetRow(gridView1.RowCount - 2) as GrnTransDto;
                if (prw != null)
                {
                    rw.RefId = prw.RefId;
                    rw.RefVoucherId = prw.RefVoucherId;
                    rw.MiscId = prw.MiscId;
                    rw.OrdDate = prw.OrdDate;
                    rw.OrdNo = prw.OrdNo;
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
                if (gridView1.FocusedColumn.FieldName == "ProductName")
                {
                    //if (Convert.ToInt32(dr.RefId) > 0) return;
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
                else if(gridView1.FocusedColumn.FieldName == "LotNo")
                {
                    if (e.KeyCode == Keys.Return)
                    {
                        ShowItemDetail(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "GRN GridControl KeyDown");
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

            if( Convert.ToInt32(grnTypeLookUpEdit.EditValue) == (int)ChallanTypeEnum.RETURNABLE_ITEM)
            {
                GetReturnablePending();
            }

            if (grnTypeLookUpEdit.Text.ToUpper() != "PURCHASE") return;
            var ordfrm = new PendingOrderView();
            ordfrm.VoucherType = VoucherTypeEnum.PurchaseOrder;
            ordfrm.AccId = Convert.ToInt32(accLookup1.SelectedValue);
            if (ordfrm.ShowDialog() != DialogResult.OK) return;

            Int32[] selectedRowHandles = ordfrm.SelectedRows;
            if (selectedRowHandles == null || selectedRowHandles.Count() == 0) return;
            List<GrnTransDto> transDtos = new List<GrnTransDto>();
            int id = 0;
            foreach (var item in selectedRowHandles)
            {
                var ord = ordfrm.gridView1.GetRow(item) as PendingOrderDto;
                GrnTransDto ct = new GrnTransDto();
                id = id - 1;
                ct.ProductId = Convert.ToInt32(ord.ProductId);
                ct.ProductName = ord.Product;
                ct.Pcs = ord.TotalPcs != null ? (int)ord.TotalPcs : 0;
                ct.Cops = 0;// ord.Cut != 0 ? ord.Cut : 0;
                ct.Qty = ord.PendingQty != null ? (decimal)ord.PendingQty : 0;
                ct.Rate = ord.rate != null ? (decimal)ord.rate : 0;
                ct.LotNo = voucherNoTextEdit.Text.Substring(4, voucherNoTextEdit.Text.Length - 4);
                //ct.FreightRate = ord.FreightRate != null ? (decimal)ord.FreightRate : 0;
                //ct.Freight = ord.Freight != null ? (decimal)ord.Freight : 0;
                //ct.OtherAdd = ord.OtherAdd != null ? (decimal)ord.OtherAdd : 0;
                //ct.OtherLess = ord.OtherLess != null ? (decimal)ord.OtherLess : 0;
                ct.DiscPer = ord.Disc != null ? (decimal)ord.Disc : 0;
                ct.Disc = ord.DiscAmt != null ? (decimal)ord.DiscAmt : 0;
                ct.Sgst = ord.SgstAmt != null ? (decimal)ord.SgstAmt : 0;
                ct.SgstPer = ord.Sgst != null ? (decimal)ord.Sgst : 0;
                ct.Cgst = ord.CgstAmt;// != null ? (decimal)ord.CgstAmt : 0;
                ct.CgstPer = ord.Cgst;// != null ? (decimal)ord.Cgst : 0;
                ct.Igst = ord.IgstAmt; // != null ? (decimal)ord.IgstAmt : 0;
                ct.IgstPer = ord.Igst; // != null ? (decimal)ord.Igst : 0;
                                       //ct.CessPer = ord.cess; // != null ? (decimal)ord.Cess : 0;
                                       //  ct.Cess = ord.CessAmt; // != null ? (decimal)ord.CessAmt : 0;
                ct.UomId = Convert.ToInt32(ord.UomId);
                ct.DesignId = Convert.ToInt32(ord.DesignId);
                ct.ColorId = Convert.ToInt32(ord.ColorId);
                ct.GradeId = Convert.ToInt32(ord.GradeId);
                ct.OrdNo = ord.VoucherNo;
                ct.ODate = ord.VoucherDate;
                //ct.OrdDate = ord.VouchDate;
                ct.RefId = ord.TransId;
                ct.MiscId = ord.Id;
                ct.RefVoucherId = ord.VoucherId;
                ct.ColorName = ord.ColorName;
                ct.DesignNo = ord.DesignNo;
                ct.GradeName = ord.GradeName;

                ct.Gross = decimal.Round(ct.Qty * ct.Rate, 2);

                decimal gross = ct.Gross - ct.Disc + ct.OtherAdd - ct.OtherLess + ct.Freight;

                ct.Sgst = decimal.Round(gross * ct.SgstPer / 100, 2);
                ct.Cgst = decimal.Round(gross * ct.CgstPer / 100, 2);//, MidpointRounding.AwayFromZero);
                ct.Igst = decimal.Round(gross * ct.IgstPer / 100, 2);//, MidpointRounding.AwayFromZero);

                ct.Total = gross + ct.Sgst + ct.Cgst + ct.Igst;
                transDtos.Add(ct);
            }
            grnTransDtoBindingSource1.DataSource = transDtos;
        }

        private void GetReturnablePending()
        {
            var ordfrm = new PendingReturnableForGrnView();
            ordfrm.VoucherType = VoucherTypeEnum.SalesChallan;
            ordfrm.AccId = Convert.ToInt32(accLookup1.SelectedValue);
            ordfrm.ChallanType = ChallanTypeEnum.RETURNABLE_ITEM;
            if (ordfrm.ShowDialog() != DialogResult.OK) return;

            Int32[] selectedRowHandles = ordfrm.SelectedRows;
            if (selectedRowHandles == null || selectedRowHandles.Count() == 0) return;
            List<GrnTransDto> transDtos = new List<GrnTransDto>();
            int id = 0;
            foreach (var item in selectedRowHandles)
            {
                var ord = ordfrm.gridView1.GetRow(item) as PendingReturnableDto;
                GrnTransDto ct = new GrnTransDto();
                id = id - 1;
                ct.ProductId = Convert.ToInt32(ord.ProductId);
                ct.ProductName = ord.Product;
                ct.Pcs = ord.PendingPcs != null ? (int)ord.PendingPcs : 0;
                ct.Cops = 0;// ord.Cut != 0 ? ord.Cut : 0;
                ct.Qty = ord.PendingQty != null ? (decimal)ord.PendingQty : 0;
                ct.Rate = ord.rate != null ? (decimal)ord.rate : 0;
                ct.LotNo = voucherNoTextEdit.Text.Substring(4, voucherNoTextEdit.Text.Length - 4);
                //ct.FreightRate = ord.FreightRate != null ? (decimal)ord.FreightRate : 0;
                //ct.Freight = ord.Freight != null ? (decimal)ord.Freight : 0;
                //ct.OtherAdd = ord.OtherAdd != null ? (decimal)ord.OtherAdd : 0;
                //ct.OtherLess = ord.OtherLess != null ? (decimal)ord.OtherLess : 0;
                ct.DiscPer = ord.Disc != null ? (decimal)ord.Disc : 0;
                ct.Disc = ord.DiscAmt != null ? (decimal)ord.DiscAmt : 0;
                ct.Sgst = ord.SgstAmt != null ? (decimal)ord.SgstAmt : 0;
                ct.SgstPer = ord.Sgst != null ? (decimal)ord.Sgst : 0;
                ct.Cgst = ord.CgstAmt;// != null ? (decimal)ord.CgstAmt : 0;
                ct.CgstPer = ord.Cgst;// != null ? (decimal)ord.Cgst : 0;
                ct.Igst = ord.IgstAmt; // != null ? (decimal)ord.IgstAmt : 0;
                ct.IgstPer = ord.Igst; // != null ? (decimal)ord.Igst : 0;
                                       //ct.CessPer = ord.cess; // != null ? (decimal)ord.Cess : 0;
                                       //  ct.Cess = ord.CessAmt; // != null ? (decimal)ord.CessAmt : 0;
                ct.UomId = Convert.ToInt32(ord.UomId);
                ct.DesignId = Convert.ToInt32(ord.DesignId);
                ct.ColorId = Convert.ToInt32(ord.ColorId);
                ct.GradeId = Convert.ToInt32(ord.GradeId);
                ct.OrdNo = ord.VoucherNo;
               // ct.ODate = ord.VoucherDate;
                //ct.OrdDate = ord.VouchDate;
                ct.RefId = ord.TransId;
                ct.MiscId = ord.Id;
                ct.RefVoucherId = ord.VoucherId;
               // ct.ColorName = ord.ColorName;
               // ct.DesignNo = ord.DesignNo;
               // ct.GradeName = ord.GradeName;

                ct.Gross = decimal.Round(ct.Qty * ct.Rate, 2);

                decimal gross = ct.Gross - ct.Disc + ct.OtherAdd - ct.OtherLess + ct.Freight;

                ct.Sgst = decimal.Round(gross * ct.SgstPer / 100, 2);
                ct.Cgst = decimal.Round(gross * ct.CgstPer / 100, 2);//, MidpointRounding.AwayFromZero);
                ct.Igst = decimal.Round(gross * ct.IgstPer / 100, 2);//, MidpointRounding.AwayFromZero);

                ct.Total = gross + ct.Sgst + ct.Cgst + ct.Igst;
                transDtos.Add(ct);
            }
            grnTransDtoBindingSource1.DataSource = transDtos;
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
            }
            else if(accLookup1.LookupDto.IsIgst)
            {
                colSgst.Visible = false;
                colSgstAmt.Visible = false;
                colCgst.Visible = false;
                colCgstAmt.Visible = false;
                colIgst.Visible = true;
                colIgstAmt.Visible = true;
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
                var _list = tabPageAdv2.Controls[0] as GRNListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new GRNListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "GRN [View]";

            }
            if (tabControlAdv1.SelectedIndex == 3)
            {
                if (tabPageAdv4.Controls.Count > 0) return;
                var _frm = Activator.CreateInstance("Konto.Reporting", "Konto.Reporting.Para.ChlPara.ChlParaMainView").Unwrap() as KontoForm;

                _frm.TopLevel = false;
                _frm.Parent = tabPageAdv4;
                _frm.ReportFilterType = "PCHALLAN";
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

                Log.Error(ex, "GRN Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private void GrnIndex_Load(object sender, EventArgs e)
        {
            try
            {
                this.ResetPage();
                NewRec();
            }
            catch (Exception ex)
            {

                Log.Error(ex, "GRN Load");
                MessageBox.Show(ex.ToString());
            }
            //var temp = Guid.NewGuid().ToString().Replace("-", string.Empty);
            //var barcode = Regex.Replace(temp, "[a-zA-Z]", string.Empty).Substring(0, 12);
            //MessageBox.Show(barcode);
        }


        #region Parent Function

        public override void Print()
        {
            base.Print();
            try
            {
                if (this.PrimaryKey == 0) return;

                if (GRNPara.Generate_Barcode)
                {
                    PageReport rpt = new PageReport();

                    rpt.Load(new FileInfo("reg\\doc\\GRNBarcode.rdlx"));

                    rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

                    GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);

                    doc.Parameters["id"].CurrentValue = this.PrimaryKey;
                    doc.Parameters["challan"].CurrentValue = "N";
                    doc.Parameters["reportid"].CurrentValue = 0;
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
                    frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                    frm.Show();// = true;
                }
                PageReport rpt1 = new PageReport();

                rpt1.Load(new FileInfo("reg\\doc\\GrnInwardChallan.rdlx"));

                rpt1.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

                GrapeCity.ActiveReports.Document.PageDocument doc1 = new GrapeCity.ActiveReports.Document.PageDocument(rpt1);

                doc1.Parameters["id"].CurrentValue = this.PrimaryKey;
                doc1.Parameters["challan"].CurrentValue = "N";
                doc1.Parameters["reportid"].CurrentValue = 0;
                var frm1 = new KontoRepViewer(doc1);
                frm1.Text = "Grn Print";
                var _tab1 = this.Parent.Parent as TabControlAdv;
                if (_tab1 == null) return;
                var pg2 = new TabPageAdv();
                pg2.Text = "Grn Print";
                _tab1.TabPages.Add(pg2);
                _tab1.SelectedTab = pg2;
                frm1.TopLevel = false;
                frm1.Parent = pg2;
                frm1.Location = new Point(pg2.Location.X + pg2.Width / 2 - frm1.Width / 2, pg2.Location.Y + pg2.Height / 2 - frm1.Height / 2);
                frm1.Show();// = true;

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
            this.FilterView = new List<GrnDto>();
            this.Text = "Grn [Add New]";
            
            this.UpdateMessage(string.Empty);

            grnTypeLookUpEdit.EditValue = 1;
            divLookUpEdit.EditValue = 1;
            storeLookUpEdit.EditValue = 1;
            voucherNoTextEdit.Text = "New";
            voucherDateEdit.EditValue = DateTime.Now;
            receiveDateEdit.EditValue = DateTime.Now;
            empLookup1.SelectedValue = 1;
            empLookup1.SetGroup();
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
            this.ActiveControl = grnTypeLookUpEdit;
            voucherLookup1.SetDefault();
            DelTrans = new List<GrnTransDto>();
            DelProd = new List<GrnProdDto>();
            this.grnTransDtoBindingSource1.DataSource = new List<GrnTransDto>();
            if (this.Create_Permission)
                okSimpleButton.Enabled = true;
            grnTypeLookUpEdit.Focus();

            this.GateId = 0;
            this.GateVoucherId = 0;
        }
        public override void ResetPage()
        {
            
            base.ResetPage();
            this.UpdateMessage(string.Empty);
            
            if (this.Modify_Permission)
                okSimpleButton.Enabled = true;
            
            accLookup1.SetEmpty();
            challanNotextEdit.Text = string.Empty;
            billNoTextEdit.Text = string.Empty;
            voucherDateEdit.DateTime = DateTime.Now;
            receiveDateEdit.DateTime = DateTime.Now;
            voucherNoTextEdit.Text = string.Empty;
            agentLookup.SetEmpty();
            transportLookup.SetEmpty();
            empLookup1.SetEmpty();
            lrNotextEdit.Text = string.Empty;
            lrDateEdit.EditValue = DateTime.Now;
            remarkTextEdit.Text = string.Empty;
            DelTrans = new List<GrnTransDto>();
            DelProd = new List<GrnProdDto>();
            this.GateId = 0;
            this.GateVoucherId = 0;
        }
        public override void EditPage(int _key)
        {
            try
            {
                base.EditPage(_key);
                this.PrimaryKey = _key;

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ChallanModel, GrnDto>();
                });

                using (var db = new KontoContext())
                {
                    db.Database.CommandTimeout = 0;
                    var bill = db.Challans.Find(_key);
                    var model = new GrnDto();
                    var mapper = new Mapper(config);
                    mapper.Map(bill, model);
                    LoadData(model, db);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "grn edit");
                MessageBox.Show(ex.ToString());
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
                    .OrderBy(x => x.VoucherDate).ThenBy(x=>x.Id)
                    .ProjectToList<GrnDto>(config);

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
            GrnDto model = new GrnDto();
            model.DivId = Convert.ToInt32(divLookUpEdit.EditValue);
            model.ChallanType = Convert.ToInt32(grnTypeLookUpEdit.EditValue);
            model.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);
            model.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));

            model.AccId = Convert.ToInt32(accLookup1.SelectedValue);
            model.RcdDate = receiveDateEdit.DateTime;
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
            model.TypeId = (int)VoucherTypeEnum.Inward ;
            model.CompId = KontoGlobals.CompanyId;
            model.YearId = KontoGlobals.YearId;
            model.BranchId = KontoGlobals.BranchId;
            model.IsActive = true;
            model.RefId = this.GateId;
            model.RefVoucherId = this.GateVoucherId;
            model.Extra1 = gateSrNoTextEdit.Text.Trim();

            var _find = new ChallanModel();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GrnDto, ChallanModel>().ForMember(x => x.Id, p => p.Ignore()
                );
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
                            createuser = _find.CreateUser;
                            createdate = Convert.ToDateTime( _find.CreateDate);
                        }

                        var map = new Mapper(config);
                        map.Map(model, _find);
                        _find.TotalQty = _translist.Sum(x => x.Qty);
                        _find.TotalPcs = _translist.Sum(x => x.Pcs);
                        _find.TotalAmount = _translist.Sum(x => x.Total);
                        if (this.PrimaryKey == 0)
                        {
                            var _srno = DbUtils.NextSerialNo(_find.VoucherId, db,0);
                            _find.VoucherNo = _srno;  //DbUtils.NextSerialNo(_find.VoucherId, db);
                             model.VoucherNo = _find.VoucherNo;
                            if (DbUtils.CheckExistVoucherNo(_find.VoucherId, _find.VoucherNo, db, _find.Id))
                            {
                                MessageBox.Show("Duplicate Voucher No Not Allowed");
                                //voucherNoTextEdit.Focus();
                                _tran.Rollback();
                                return;
                            }
                            _find.TypeId = (int)VoucherTypeEnum.Inward;
                            db.Challans.Add(_find);
                            db.SaveChanges();
                        }
                        else
                        {
                            _find.CreateDate = createdate;
                            _find.CreateUser = createuser;
                        }
                        
                        foreach (var item in _translist)
                        {
                            var transid = item.Id;
                            item.ChallanId = _find.Id;
                            var tranModel = new ChallanTransModel();
                            if(item.Id > 0)
                            {
                                tranModel = db.ChallanTranses.Find(item.Id);
                            }
                            map = new Mapper(config);
                            map.Map(item, tranModel);

                            if (item.Id <= 0)
                            {
                                db.ChallanTranses.Add(tranModel);
                                db.SaveChanges();
                               
                            }
                            Trans.Add(tranModel);
                            // add subdetails item details
                            var prlist = prodDtos.Where(k => (k.TransId == transid && k.VoucherId == _find.VoucherId)).ToList();
                           
                            var _time = DateTime.Now.ToString("ddMMyyHHmmss");
                            _barcodeDate = string.Empty;
                            if( prlist.Any(x=>x.Extra1!=null && x.Extra1.Length > 5))
                            {
                                _barcodeDate = prlist.FirstOrDefault(x => x.Extra1!= null && x.Extra1.Length > 5).Extra1.Substring(1, 12);
                                _time = _barcodeDate;
                            }
                            foreach (var p in prlist)
                             {
                                p.ProdStatus = "STOCK";
                                p.RefId = _find.Id;
                                p.TransId = tranModel.Id;
                                
                                if(string.IsNullOrEmpty(p.LotNo))
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
                                
                                if (prodModel.CProductId == null || prodModel.CProductId == 0)
                                    prodModel.CProductId = tranModel.ProductId;


                                if (p.Id <= 0)
                                {
                                   
                                    if (GRNPara.Generate_Barcode)
                                    {
                                        _time= DateTime.Now.ToString("ddMMyyHHmmss");
                                        prodModel.Extra1 = _time + p.SrNo.ToString().PadLeft(2,'0');
                                    }
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
                        }

                        // delete from item details
                        foreach (var p in DelProd)
                        {
                            if (p.Id == 0) continue;
                             var prd = db.Prods.Find(p.Id);
                             if(prd!=null)
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
                                StockEffect.StockTransChlnEntry(_find, item,false, TableName, KontoGlobals.UserName, db);
                            //}
                        }

                        //if (this.PrimaryKey == 0)
                        //    DbUtils.UsedSerial(_find.VoucherId, _SerialValue, db);

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
                
                base.SaveDataAsync(newmode);
                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage +" Voucher No.: " + model.VoucherNo, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (!this.OpenForLookup && newmode)
                {
                    if (GRNPara.Generate_Barcode && MessageBox.Show("Print Barcode ??..","Barcode", MessageBoxButtons.YesNo)== DialogResult.Yes)
                    {
                        this.PrimaryKey = _find.Id;
                        Print();
                        this.PrimaryKey = 0;
                    }
                    
                }
                if (!this.OpenForLookup)
                {
                    if (Trans.Count == 1 && this.prodDtos.Count > 0 && GRNPara.Direct_Issue_From_Purchase && MessageBox.Show("Create Lot?", "Lot", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        var win = new LotOptionView();
                        win.Model = _find;
                        win.TranModel = Trans[0];
                        win.NewMode = newmode;
                        win.MIProdList = ProdList;
                        if (win.ShowDialog() == DialogResult.OK && newmode)
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
                }
                if(this.OpenForLookup)
                {
                    this.Close();
                    this.Dispose();
                }
                this.ResetPage();
                this.NewRec();
            }
        }


        #endregion

        private void gateSrNoTextEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (Convert.ToInt32(accLookup1.SelectedValue) == 0) return;
            var frm = new PendingGateInwardView();
            frm.AccId = Convert.ToInt32(accLookup1.SelectedValue);
            if(frm.ShowDialog()== DialogResult.OK)
            {
                var rw = frm.gridView1.GetRow(frm.gridView1.FocusedRowHandle) as PendingGateDto;
                this.GateId = rw.Id;
                this.GateVoucherId = rw.VoucherId;
                this.gateSrNoTextEdit.Text = rw.SrNo;
            }
            
        }
    }
}
