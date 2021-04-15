using AutoMapper;
using DevExpress.XtraEditors;
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
using Konto.Shared.Masters.Color;
using Konto.Shared.Masters.Design;
using Konto.Shared.Masters.Grade;
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

namespace Konto.Shared.Trans.ST
{
    public partial class StIndex : KontoMetroForm
    {
        private List<ChallanModel> FilterView = new List<ChallanModel>();
        private List<GrnTransDto> DelTrans = new List<GrnTransDto>();
        

        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;

        public StIndex()
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
            
           
            gridView1.InitNewRow += GridView1_InitNewRow;
            gridView1.CellValueChanged += GridView1_CellValueChanged;
            gridView1.KeyDown += GridView1_KeyDown;
            gridControl1.Enter += GridControl1_Enter;
            gridView1.CustomDrawRowIndicator += GridView1_CustomDrawRowIndicator;
            gridView1.ShowingEditor += GridView1_ShowingEditor;
            gridView1.MouseUp += GridView1_MouseUp;
            gridView1.ValidatingEditor += GridView1_ValidatingEditor;
            gridView1.InvalidValueException += GridView1_InvalidValueException;
          
            this.Shown += ScIndex_Shown;
            gridView1.DoubleClick += GridView1_DoubleClick;
            this.MainLayoutFile = KontoFileLayout.Stock_Transfer_Index;
            this.GridLayoutFile = KontoFileLayout.Stock_Transfer_Trans;
            
            FillLookup();
          //  SetParameter();
            SetGridColumn();

            headerEdit.Hide();
            headerEdit.Parent = this.gridControl1;
            headerEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            headerEdit.Leave += new EventHandler(headerEdit_Leave);
            voucherLookup1.SelectedValueChanged += VoucherLookup1_SelectedValueChanged;

            this.FirstActiveControl = voucherLookup1;
        }

        private void GridView1_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.DisplayError;
        }

        private void GridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if(gridView1.FocusedColumn.FieldName=="Qty")
            {
                var _fromStock = Convert.ToDecimal(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, colFromStock));
                var _qty = Convert.ToDecimal(e.Value);
                if(_qty>_fromStock)
                {
                    e.ErrorText = "Issue Qty Can No be greater than Stock Qty";
                    e.Valid = false;
                }
            }
        }

        private void ScIndex_Shown(object sender, EventArgs e)
        {
            // colSgst.OptionsColumn.AllowFocus = true;
            colSgst.OptionsColumn.AllowFocus = true;
            colSgst.OptionsColumn.AllowEdit = true;
            //colCgst.OptionsColumn.AllowFocus = true;
            colCgst.OptionsColumn.AllowFocus = true;
            colCgst.OptionsColumn.AllowEdit = true;
            // colIgst.OptionsColumn.AllowFocus = true;
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



       



        #region UDF
        private void SetGridColumn()
        {
            //colColorName.Visible = SCPara.Color_Required;
            //colDesignNo.Visible = SCPara.Design_Required;
            //colGradeName.Visible = SCPara.Grade_Required;
            //colCops.Visible = SCPara.Cut_Required;
            //colFreight.Visible = SCPara.Freight_Required;
            //colFreightRate.Visible = SCPara.Freight_Required;
            //colLotNo.Visible = SCPara.LotNo_Required;
            
            if (KontoGlobals.PackageId == 1)
            {
                colCops.Caption = "Cut";
                colQty.Caption = "Meters";
            }
            
        }
        private GrnTransDto PreOpenLookup()
        {
           
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
                              .Where(x => x.SysPara.Category == "Outward" && x.CompId == KontoGlobals.CompanyId)
                             .ToList();

                foreach (var item in _paralists)
                {
                    var value = item.ParaValue;
                    switch (item.ParaId)
                    {
                        case 43://Sales Challan
                            {
                                SCPara.Color_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 44:
                            {
                                SCPara.Batch_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 45:
                            {
                                SCPara.LotNo_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 46:
                            {
                                SCPara.Design_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 47:
                            {
                                SCPara.Grade_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 48:
                            {
                                SCPara.Cut_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 49:
                            {
                                SCPara.Freight_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 50:
                            {
                                SCPara.Auto_Book_Id = Convert.ToInt32(value);
                                break;
                            }
                        case 51:
                            {
                                SCPara.Auto_Voucher_Id = Convert.ToInt32(value);
                                break;
                            }
                        case 53:
                            {
                                SCPara.Auto_Bill_Generate_Required = (value == "Y") ? true : false;
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

            frm.VoucherType = VoucherTypeEnum.PurchaseOrder;

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                er.ProductId = frm.SelectedValue;
                er.ProductName = frm.SelectedTex;
                var model = frm.SelectedItem as ProductLookupDto;
                er.UomId = model.UomId;
                er.Rate = model.DealerPrice;
                     er.SgstPer = model.Sgst;
                    er.CgstPer = model.Cgst;
                    er.IgstPer = 0;
                    er.Igst = 0;
               
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
            frm.Tag = MenuId.Design_Master;
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
               

                var _divLists = (from p in db.Divisions
                                 where p.IsActive && !p.IsDeleted
                                 select new BaseLookupDto()
                                 {
                                     DisplayText = p.DivisionName,
                                     Id = p.Id
                                 }).ToList();

                var _storeLists = (from p in db.Branches
                                 where p.IsActive && !p.IsDeleted && p.CompId==KontoGlobals.CompanyId
                                 select new BaseLookupDto()
                                 {
                                     DisplayText = p.BranchName,
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
              
                divLookUpEdit.Properties.DataSource = _divLists;
                fromBranchLookUpEdit.Properties.DataSource = _storeLists;
                toBranchLookUpEdit.Properties.DataSource = _storeLists;
            }
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
            else if (Convert.ToInt32(voucherLookup1.SelectedValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Voucher", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherLookup1.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(fromBranchLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid from Branch", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fromBranchLookUpEdit.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(toBranchLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid To_Branch", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                toBranchLookUpEdit.Focus();
                return false;
            }
            else if(fromBranchLookUpEdit.EditValue.ToString() == toBranchLookUpEdit.EditValue.ToString())
            {
                MessageBoxAdv.Show(this, "Send & Receiving Branch Can Not Be same", "Invalid Branch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrEmpty(challanNotextEdit.Text.Trim()))
            {
                MessageBoxAdv.Show(this, "Invalid Ref No.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                challanNotextEdit.Focus();
                return false;
            }
            
            else if (dt > KontoGlobals.ToDate || dt < KontoGlobals.FromDate)
            {
                MessageBoxAdv.Show(this, "Challan date out of financial range", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherDateEdit.Focus();
                return false;
            }
           
            else if (gridView1.RowCount == 1)
            {
                MessageBoxAdv.Show(this, "At Least One Product Should be Entered", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }

            return true;
        }

        private void LoadData(ChallanModel model)
        {
            try
            {
                this.ResetPage();
                this.PrimaryKey = model.Id;
                divLookUpEdit.EditValue = model.DivId;
             
                voucherLookup1.SelectedValue = model.VoucherId;
                voucherLookup1.SetGroup(model.VoucherId);
                voucherDateEdit.EditValue = KontoUtils.IToD(model.VoucherDate);

                voucherNoTextEdit.Text = model.VoucherNo;


                fromBranchLookUpEdit.EditValue = model.BranchId;
                toBranchLookUpEdit.EditValue = model.ToBranchId;

                transportModeComboBoxEdit.EditValue = model.Extra1; // mode of transport;
                ewayBillNoTextEdit.Text = model.Extra2; // ewaybill no
               

                driverTextEdit.Text = model.DName;

               
                challanNotextEdit.Text = model.ChallanNo;


                if (Convert.ToInt32(model.EmpId) != 0)
                {
                    empLookup1.SelectedValue = model.EmpId;
                    empLookup1.SetGroup();
                }
                fromBranchLookUpEdit.EditValue = model.StoreId;

                if (Convert.ToInt32(model.TransId) != 0)
                {
                    transportLookup.SelectedValue = model.TransId;
                    transportLookup.SetAcc((int)model.TransId);
                }
                lrNotextEdit.Text = model.DocNo;
                lrDateEdit.EditValue = model.DocDate;
                remarkTextEdit.Text = model.Remark;

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
                                     ODate = o.VoucherDate,BarcodeNo = pd.BarCode
                                 }).ToList();

                    
                    this.grnTransDtoBindingSource1.DataSource = _list;
                }



                this.Text = "Stock Transfer [View/Modify]";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                Log.Error(ex, "stock transfer edit");
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
            
             if (gridView1.FocusedColumn.FieldName == "ProductName" && Convert.ToInt32(itm.RefId) > 0) 
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
            gridView1.FocusedColumn = gridView1.Columns["BarcodeNo"];
        }
        private void GridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column == null) return;
                var er = gridView1.GetRow(e.RowHandle) as GrnTransDto;

                if (er == null) return;

                if (string.IsNullOrEmpty(fromBranchLookUpEdit.Text) || string.IsNullOrEmpty(toBranchLookUpEdit.Text)) return;

                if (e.Column.FieldName == "BarcodeNo" && e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
                {
                    var pos = DbUtils.GetProductDetails(e.Value.ToString());

                    if (pos == null)
                    {
                        MessageBox.Show("Barcode Not Found..");
                        return;
                    }
                    er.FromStock = DbUtils.GetCurrentStock(pos.Id, Convert.ToInt32(fromBranchLookUpEdit.EditValue));
                    er.ToStock = DbUtils.GetCurrentStock(pos.Id, Convert.ToInt32(toBranchLookUpEdit.EditValue));
                    er.ProductName = pos.ProductName;
                    er.ProductId = pos.Id;
                    er.UomId = pos.PurUomId;
                    er.Rate = pos.DealerPrice;
                    er.SgstPer = pos.Sgst;
                    er.CgstPer = pos.Cgst;
                    er.IgstPer = 0;
                    er.Igst = 0;

                }

                GridCalculation(er);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                Log.Error(ex,"st barcode");
            }
            
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
       

        #endregion

        void headerEdit_Leave(object sender, EventArgs e)
        {
            activeCol.Caption = headerEdit.Text;
            headerEdit.Hide();
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
                var _list = tabPageAdv2.Controls[0] as StListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new StListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Stock Transfer [View]";

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
        }


        #region Parent Function

        public override void Print()
        {
            base.Print();
            try
            {
                if (this.PrimaryKey == 0) return;

                PageReport rpt = new PageReport();

                rpt.Load(new FileInfo("reg\\doc\\stock_transfer_challan.rdlx"));

                rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);

                doc.Parameters["id"].CurrentValue = this.PrimaryKey;
                doc.Parameters["challan"].CurrentValue = "N";
                doc.Parameters["reportid"].CurrentValue = 0;
                var frm = new KontoRepViewer(doc);
                frm.Text = "Transfer Challan";
                var _tab = this.Parent.Parent as TabControlAdv;
                if (_tab == null) return;
                var pg1 = new TabPageAdv();
                pg1.Text = "Transfer Challan Print";
                _tab.TabPages.Add(pg1);
                _tab.SelectedTab = pg1;
                frm.TopLevel = false;
                frm.Parent = pg1;
                frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                frm.Show();// = true;

            }
            catch (Exception ex)
            {
                Log.Error(ex, "st print");
                MessageBoxAdv.Show(this, "Error While Print !!", "Exception ", ex.ToString());

            }
        }
        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<ChallanModel>();
            this.Text = "Stock Transfer [Add New]";
           
            
            divLookUpEdit.EditValue = 1;
            fromBranchLookUpEdit.EditValue = 1;
            voucherNoTextEdit.Text = "New";
            voucherDateEdit.EditValue = DateTime.Now;
            fromBranchLookUpEdit.EditValue = KontoGlobals.BranchId;
            empLookup1.SelectedValue = 1;
            empLookup1.SetGroup();
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
            this.ActiveControl = voucherLookup1.buttonEdit1;
            voucherLookup1.SetDefault();
            challanNotextEdit.Text = "NA";
            this.grnTransDtoBindingSource1.DataSource = new List<GrnTransDto>();
            
        }
        public override void ResetPage()
        {
            base.ResetPage();
            
            
            challanNotextEdit.Text = string.Empty;
           
            voucherDateEdit.DateTime = DateTime.Now;
           
            voucherNoTextEdit.Text = string.Empty;
            toBranchLookUpEdit.EditValue = DBNull.Value;
            fromBranchLookUpEdit.EditValue = DBNull.Value;
            transportLookup.SetEmpty();
            empLookup1.SetEmpty();
            lrNotextEdit.Text = string.Empty;
            lrDateEdit.EditValue = DateTime.Now;
            remarkTextEdit.Text = string.Empty;
            DelTrans = new List<GrnTransDto>();
            
        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

          
            using (var db = new KontoContext())
            {
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
          
           

            filter.Add(new Filter { PropertyName = "CompId", Operation = Op.Equals, Value = KontoGlobals.CompanyId });
            filter.Add(new Filter { PropertyName = "YearId", Operation = Op.Equals, Value = KontoGlobals.YearId });
            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });

           

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
            List<ProdOutModel> ProdList = new List<ProdOutModel>();
            using (var db = new KontoContext())
            {
                using (var _tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (this.PrimaryKey != 0)
                        {
                            _find = db.Challans.Find(this.PrimaryKey);
                        }
                        UpdateChallan(_find);
                        _find.TotalQty = _translist.Sum(x => x.Qty);
                        _find.TotalPcs = _translist.Sum(x => x.Pcs);
                        _find.TotalAmount = _translist.Sum(x => x.Total);
                        _find.TypeId = (int)VoucherTypeEnum.SalesChallan;
                        if (this.PrimaryKey == 0)
                        {
                            _find.VoucherNo = DbUtils.NextSerialNo(_find.VoucherId, db);
                            if (DbUtils.CheckExistVoucherNo(_find.VoucherId, _find.VoucherNo, db, _find.Id))
                            {
                                MessageBox.Show("Duplicate Voucher No Not Allowed");
                               
                                _tran.Rollback();
                                return;
                            }
                            db.Challans.Add(_find);
                            db.SaveChanges();
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
                            var map = new Mapper(config);
                            map.Map(item, tranModel);

                            if (item.Id <= 0)
                            {
                                db.ChallanTranses.Add(tranModel);
                                db.SaveChanges();
                               
                            }
                            Trans.Add(tranModel);
                           
                          

                        }
                        //delete item from ord trans
                        //delete item fro trans table entry
                        foreach (var item in DelTrans)
                        {
                            if (item.Id == 0) continue;
                            var _model = db.ChallanTranses.Find(item.Id);
                            _model.IsDeleted = true;

                           
                        }

                        

                        //sotock effect
                        var stk = db.StockTranses.Where(k => k.MasterRefId == _find.RowId).ToList();
                        if (stk != null)
                            db.StockTranses.RemoveRange(stk);

                        foreach (var item in Trans)
                        {
                            string TableName = "st";
                            var stockReq = db.Products.FirstOrDefault(k => k.Id == item.ProductId).StockReq;
                            if (stockReq == "No") continue;
                            
                            
                                StockEffect.StockTransChlnEntry(_find, item,true, TableName, KontoGlobals.UserName, db);
                           
                        }
                        
                        

                        db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "st Save");
                        MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());

                    }
                }
            }
               

            
            if (IsSaved)
            {
                NewRec();
                base.SaveDataAsync(newmode);
                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage +" Voucher No.: " + _find.VoucherNo, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (!this.OpenForLookup && newmode)
                {
                    if (this.voucherLookup1.GroupDto.PrintAfterSave && MessageBox.Show("Print Transfer Challan ?", "Print", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        this.PrimaryKey = _find.Id;
                        Print();
                        this.PrimaryKey = 0;
                    }
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

        private void UpdateChallan(ChallanModel model)
        {
            model.DivId = Convert.ToInt32(divLookUpEdit.EditValue);
            model.ChallanType = (int) ChallanTypeEnum.TRANSFER_OUT;
            model.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);
            model.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
          

            model.VoucherNo = voucherNoTextEdit.Text.Trim();

           
            model.ChallanNo = challanNotextEdit.Text.Trim();

            model.VehicleNo = vehicleTextEdit.Text.Trim();
            model.EmpId = Convert.ToInt32(empLookup1.SelectedValue);
            model.StoreId = Convert.ToInt32(fromBranchLookUpEdit.EditValue);
            model.DName = driverTextEdit.Text.Trim();
            model.Remark = remarkTextEdit.Text.Trim();
            model.TransId = Convert.ToInt32(transportLookup.SelectedValue);
            model.DocNo = lrNotextEdit.Text.Trim();
            model.DocDate = Convert.ToDateTime(lrDateEdit.EditValue);
            model.TypeId = (int)TypeEnum.Outward;
            model.CompId = KontoGlobals.CompanyId;
            model.YearId = KontoGlobals.YearId;
            model.BranchId = Convert.ToInt32(fromBranchLookUpEdit.EditValue);
            model.ToBranchId = Convert.ToInt32(toBranchLookUpEdit.EditValue);

            model.Extra1 = transportModeComboBoxEdit.Text; // mode of transport
            model.Extra2 = ewayBillNoTextEdit.Text.Trim(); // eway bill no
            
            model.IsActive = true;
        }


        #endregion
       
    }
}
