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
using Konto.Data.Models.Masters;
using Konto.Data.Models.Masters.Dtos;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using Konto.Shared.Masters.Color;
using Konto.Shared.Masters.Design;
using Konto.Shared.Masters.Item;
using Konto.Shared.Trans.Common;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ExpressionBuilder = Konto.Core.Shared.Libs.ExpressionBuilder;

namespace Konto.Trading.OpJobIssue
{
    public partial class OpJobIndex : KontoMetroForm
    {
        private List<GrnDto> FilterView = new List<GrnDto>();
        private List<MiTransDto> DelTrans = new List<MiTransDto>();
        private List<GrnProdDto> prodDtos = new List<GrnProdDto>();
        private List<GrnProdDto> DelProd = new List<GrnProdDto>();

        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;

        public OpJobIndex()
        {
            InitializeComponent();

            tabControlAdv1.TabPages[2].TabVisible = false;
            tabControlAdv1.TabPages[3].TabVisible = false;

            this.Load += GrnIndex_Load;
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            gridControl1.ProcessGridKey += GridControl1_ProcessGridKey;
            productRepositoryItemButtonEdit.ButtonClick += ProductRepositoryItemButtonEdit_ButtonClick;
            colorRepositoryItemButtonEdit.ButtonClick += ColorRepositoryItemButtonEdit_ButtonClick;
           
            designRepositoryItemButtonEdit.ButtonClick += DesignRepositoryItemButtonEdit_ButtonClick;
            
           
            gridView1.InitNewRow += GridView1_InitNewRow;
            gridView1.CellValueChanged += GridView1_CellValueChanged;
            gridView1.KeyDown += GridView1_KeyDown;
            gridControl1.Enter += GridControl1_Enter;
            gridView1.CustomDrawRowIndicator += GridView1_CustomDrawRowIndicator;
            gridView1.ShowingEditor += GridView1_ShowingEditor;
            gridView1.MouseUp += GridView1_MouseUp;
            lotNoRepositoryItemButtonEdit.ButtonClick += LotNoRepositoryItemButtonEdit_ButtonClick;
           
            gridView1.DoubleClick += GridView1_DoubleClick;

            this.MainLayoutFile = KontoFileLayout.Op_Job_Issue_Index;
            this.GridLayoutFile = KontoFileLayout.Op_Job_Issue_Trans;
            
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
            this.FirstActiveControl = voucherLookup1;
        }

        private void VoucherLookup1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.PrimaryKey == 0 && Convert.ToInt32(voucherLookup1.SelectedValue) > 0)
            {
                voucherNoTextEdit.Text = "New-" + DbUtils.NextSerialNo(Convert.ToInt32(voucherLookup1.SelectedValue), 1);
            }
        }
        private void GridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        private void GridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var view = sender as GridView;
            var pcs = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, colPcs));
            var qty = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, colQty));

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
                if (_pcs <= 0)
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

        }


        #region UDF
        private void SetGridColumn()
        {
            colColorName.Visible = JobIssPara.Color_Required;
            colDesignNo.Visible = JobIssPara.Design_Required;
             colQty.Caption = "Meters";
            
        }
        private MiTransDto PreOpenLookup()
        {
            if (Convert.ToInt32(accLookup1.SelectedValue) == 0) return null;
            gridView1.GetRow(gridView1.FocusedRowHandle);
            if (gridView1.GetRow(gridView1.FocusedRowHandle) == null)
            {
                gridView1.AddNewRow();
            }
            var dr = (MiTransDto)gridView1.GetRow(gridView1.FocusedRowHandle);
            return dr;
        }
      
        public void GridCalculation(MiTransDto er)
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
                              .Where(x => x.SysPara.Category == "JobIssue" && x.CompId == KontoGlobals.CompanyId)
                             .ToList();

                foreach (var item in _paralists)
                {
                    var value = item.ParaValue;
                    switch (item.ParaId)
                    {
                        case 122://JOb Issue
                            {
                                JobIssPara.Color_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 123:
                            {
                                JobIssPara.Batch_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 124:
                            {
                                JobIssPara.LotNo_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 126:
                            {
                                JobIssPara.Design_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 125:
                            {
                                JobIssPara.Grade_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 127:
                            {
                                JobIssPara.Cut_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 128:
                            {
                                JobIssPara.Freight_Required = (value == "Y") ? true : false;
                                break;
                            }
                        case 129:
                            {
                                JobIssPara.Auto_Book_Id = Convert.ToInt32(value);
                                break;
                            }
                        case 130:
                            {
                                JobIssPara.Auto_Voucher_Id = Convert.ToInt32(value);
                                break;
                            }
                        case 135:
                            {
                                JobIssPara.Finish_Product_Requred = (value == "Y") ? true : false;
                                break;
                            }
                    }
                }
            }

        }
        private void OpenItemLookup(int _selvalue, MiTransDto er)
        {
            var frm = new ProductLkpWindow();
            frm.Tag = MenuId.Product_Master;
            frm.SelectedValue = _selvalue;

            frm.VoucherType = VoucherTypeEnum.OpJobIssue;

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                if (gridView1.FocusedColumn.FieldName == "FinishQuality")
                {
                    er.NProductId = frm.SelectedValue;
                    er.FinishQuality = frm.SelectedTex;
                    gridView1.UpdateCurrentRow();
                    return;
                }
                er.ProductId = frm.SelectedValue;
                er.ProductName = frm.SelectedTex;
                if (string.IsNullOrEmpty(er.FinishQuality))
                {
                    er.NProductId = frm.SelectedValue;
                    er.FinishQuality = frm.SelectedTex;
                }
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
        private void OpenColorLookup(int _selvalue, MiTransDto er)
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
       
        private void OpenDesignLookup(int _selvalue, MiTransDto er)
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
        
            using (var db = new KontoContext())
            {
               

                var _divLists = (from p in db.Divisions
                                 where p.IsActive && !p.IsDeleted
                                 select new BaseLookupDto()
                                 {
                                     DisplayText = p.DivisionName,
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
               
            }
        }

        private bool ValidateData()
        {
            //var dt = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
           
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
            else if (string.IsNullOrEmpty(voucherNoTextEdit.Text.Trim()))
            {
                MessageBoxAdv.Show(this, "Invalid Challan no", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherNoTextEdit.Focus();
                return false;
            }
            else if(Convert.ToInt32(processLookup1.SelectedValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Process/Job Type Selection", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                processLookup1.Focus();
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

        private void LoadData(GrnDto model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            divLookUpEdit.EditValue = model.DivId;
           
            voucherLookup1.SelectedValue = model.VoucherId;
            voucherLookup1.SetGroup(model.VoucherId);
            voucherDateEdit.EditValue = KontoUtils.IToD(model.VoucherDate);

            voucherNoTextEdit.Text = model.VoucherNo;
            accLookup1.SelectedValue = model.AccId;
            accLookup1.SetAcc(model.AccId);
            
            if (Convert.ToInt32(model.EmpId) != 0)
            {
                empLookup1.SelectedValue = model.EmpId;
                empLookup1.SetGroup();
            }
            if (Convert.ToInt32(model.ProcessId) != 0)
            {
                processLookup1.SelectedValue = model.ProcessId;
                processLookup1.SetValue();
            }

            remarkTextEdit.Text = model.Remark;

            createdLabelControl.Text = "Created By: " + model.CreateUser + " [ " + model.CreateDate + " ]";
            modifyLabelControl.Text = "Modified By: " + model.ModifyUser + " [ " + model.ModifyDate ?? string.Empty  + " ]";

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
                              //join grd in _context.Grades on ct.GradeId equals grd.Id into join_grd
                              //from grd in join_grd.DefaultIfEmpty()
                              join acc in _context.Accs on o.AccId equals acc.Id into join_acc
                              from acc in join_acc.DefaultIfEmpty()
                              orderby ct.Id
                              where ct.IsActive == true && ct.IsDeleted == false &&
                              ct.ChallanId == model.Id
                              select new MiTransDto()
                             {
                                  Id = ct.Id,Cess=ct.Cess,CessPer=ct.CessPer,Cgst=ct.Cgst,CgstPer=ct.CgstPer,
                                  ChallanId=ct.ChallanId,ColorId=ct.ColorId.HasValue ? (int)ct.ColorId: 1,ColorName=cl.ColorName,Cops=ct.Cops,
                                  DesignId= ct.DesignId.HasValue? (int)ct.DesignId:1,DesignNo=dm.ProductCode,Disc=ct.Disc,DiscPer=ct.DiscPer,Freight=ct.Freight,
                                  FreightRate=ct.FreightRate,NProductId =ct.NProductId.HasValue ? (int)ct.NProductId : 0,FinishQuality= np.ProductName,RefNo=ct.RefNo,
                                  Gross=ct.Gross,Igst=ct.Igst,
                                  IgstPer=ct.IgstPer,LotNo=ct.LotNo,MiscId=ct.MiscId,OtherAdd=ct.OtherAdd,OtherLess=ct.OtherLess,
                                  Pcs=ct.Pcs,ProductId=(int)ct.ProductId,ProductName=pd.ProductName,Qty=ct.Qty,Rate=ct.Rate,RefId=ct.RefId,
                                  RefVoucherId=ct.RefVoucherId,Remark=ct.Remark,Sgst=ct.Sgst,SgstPer=ct.SgstPer,Total=ct.Total,UomId=(int)ct.UomId,
                                  
                              }).ToList();

                prodDtos = _context.Prods.Where(x => x.RefId == model.Id && !x.IsDeleted)
                                         .ProjectToList<GrnProdDto>(config);

                this.grnTransDtoBindingSource1.DataSource = _list;
            }

            

            this.Text = "Op Job Issue [View/Modify]";

        }

        private void ShowItemDetail(MiTransDto er)
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
            else if (Convert.ToInt32(itm.RefId) > 0) 
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
            var er = gridView1.GetRow(e.RowHandle) as MiTransDto;
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
                var row = view.GetRow(view.FocusedRowHandle) as MiTransDto;
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
            var rw = gridView1.GetRow(e.RowHandle) as MiTransDto;
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
                else if (gridView1.FocusedColumn.FieldName == "FinishQuality")
                {

                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.NProductId == 0)
                        {
                            OpenItemLookup(dr.NProductId, dr);
                            // e.Handled = true;
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        OpenItemLookup(dr.NProductId, dr);
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
                Log.Error(ex, "Op Mill Issue GridControl KeyDown");
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


        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                voucherLookup1.Focus();
                if(this.PrimaryKey > 0)
                this.Text = "Op Job Issue [View]";
                else
                    this.Text = "Op Job Issue [Add]";
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as OpJobListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new OpJobListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Op Job Issue [View]";

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

                Log.Error(ex, "Op Job Issue Save");
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

                Log.Error(ex, "op job Load");
                MessageBox.Show(ex.ToString());
            }
        }


        #region Parent Function

        public override void Print()
        {
            base.Print();
            MessageBox.Show("Not Implemented");
            //try
            //{
            //    if (this.PrimaryKey == 0) return;

            //    PageReport rpt = new PageReport();

            //    rpt.Load(new FileInfo("reg\\doc\\grn.rdlx"));

            //    rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.Conn;

            //    GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);

            //    doc.Parameters["id"].CurrentValue = this.PrimaryKey;
            //    doc.Parameters["Ord"].CurrentValue = "N";
            //    doc.Parameters["reportid"].CurrentValue = 0;
            //    var frm = new KontoRepViewer(doc);
            //    frm.Text = "Purchase Order";
            //    var _tab = this.Parent.Parent as TabControlAdv;
            //    if (_tab == null) return;
            //    var pg1 = new TabPageAdv();
            //    pg1.Text = "Order Print";
            //    _tab.TabPages.Add(pg1);
            //    _tab.SelectedTab = pg1;
            //    frm.TopLevel = false;
            //    frm.Parent = pg1;
            //    frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
            //    frm.Show();// = true;

            //}
            //catch (Exception ex)
            //{
            //    Log.Error(ex, "Op job print");
            //    MessageBoxAdv.Show(this, "Error While Print !!", "Exception ", ex.ToString());

            //}
        }
        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<GrnDto>();
            this.Text = "Op Job Issue [Add New]";
            divLookUpEdit.EditValue = 1;
           
            voucherDateEdit.EditValue = DateTime.Now;
            empLookup1.SelectedValue = 1;
            empLookup1.SetGroup();
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
            this.ActiveControl = voucherLookup1.buttonEdit1;
            voucherLookup1.SetDefault();
            DelTrans = new List<MiTransDto>();
            DelProd = new List<GrnProdDto>();
            this.grnTransDtoBindingSource1.DataSource = new List<MiTransDto>();
            
        }
        public override void ResetPage()
        {
            base.ResetPage();
            
            accLookup1.SetEmpty();
           
           
            voucherDateEdit.DateTime = DateTime.Now;
           
            voucherNoTextEdit.Text = string.Empty;
           
            empLookup1.SetEmpty();
           
            remarkTextEdit.Text = string.Empty;
            DelTrans = new List<MiTransDto>();
            DelProd = new List<GrnProdDto>();
        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ChallanModel, GrnDto>();
            });

            using (var db = new KontoContext())
            {
                var bill = db.Challans.Find(_key);
                var model = new GrnDto();
                var mapper = new Mapper(config);
                mapper.Map(bill, model);
                LoadData(model);
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
            if (!string.IsNullOrEmpty(voucherNoTextEdit.Text.Trim()))
            {
                filter.Add(new Filter { PropertyName = "VoucherNo", Operation = Op.Equals, Value = voucherNoTextEdit.Text.Trim() });
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
            model.ChallanType = 7;
            model.ChallanNo = "NA";
            
            model.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);
            model.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));

            model.AccId = Convert.ToInt32(accLookup1.SelectedValue);
            model.RcdDate = voucherDateEdit.DateTime;
            model.VoucherNo = voucherNoTextEdit.Text.Trim();
            model.EmpId = Convert.ToInt32(empLookup1.SelectedValue);
            model.StoreId = 1;

            model.Remark = remarkTextEdit.Text.Trim();
            model.TypeId = (int)VoucherTypeEnum.OpJobIssue;
            model.CompId = KontoGlobals.CompanyId;
            model.YearId = KontoGlobals.YearId;
            model.BranchId = KontoGlobals.BranchId;
            model.ProcessId = Convert.ToInt32(processLookup1.SelectedValue);
            model.IsActive = true;

            var _find = new ChallanModel();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GrnDto, ChallanModel>().ForMember(x => x.Id, p => p.Ignore()
                );
                cfg.CreateMap<MiTransDto, ChallanTransModel>().ForMember(x => x.Id, p => p.Ignore());
                cfg.CreateMap<GrnProdDto, ProdModel>().ForMember(x => x.Id, p => p.Ignore());
            });
            
            var _translist = grnTransDtoBindingSource1.DataSource as List<MiTransDto>;
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
                        _find.ChallanType = (int) ChallanTypeEnum.ISSUE_FOR_JOB;
                        if (this.PrimaryKey == 0)
                        {
                          //  _find.VoucherNo = DbUtils.NextSerialNo(_find.VoucherId, db);
                           //  model.VoucherNo = _find.VoucherNo;
                            _find.TypeId = (int)VoucherTypeEnum.OpJobIssue;
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
                            var prlist = prodDtos.Where(k => (k.TransId == item.Id && k.VoucherId == _find.VoucherId)).ToList();

                            foreach (var p in prlist)
                            {
                                p.ProdStatus = "ISSUE";
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
                                    db.Prods.Add(prodModel);
                                    db.SaveChanges();
                                }
                                ProdList.Add(prodModel);

                                ProdOutModel pOut = db.ProdOuts.SingleOrDefault(k => k.ProdId == p.Id);
                                if (pOut == null)
                                {
                                    ProdOutModel Out = new ProdOutModel();
                                    Out.TakaStatus = "ISSUE";
                                    Out.ProductId = p.ProductId;
                                    Out.CompId = KontoGlobals.CompanyId;
                                    Out.YearId = KontoGlobals.YearId;
                                    Out.SrNo = p.SrNo;
                                    Out.Qty = (p.NetWt * -1);
                                    Out.GrayMtr = (p.NetWt * -1);
                                    Out.ProdId = p.Id;
                                    Out.RefId = _find.Id;
                                    Out.VoucherId = _find.VoucherId;
                                    Out.VoucherNo = p.VoucherNo;
                                    Out.TransId = tranModel.Id;

                                    db.ProdOuts.Add(Out);
                                }
                                else
                                {
                                    pOut.GrayMtr = (p.NetWt * -1);
                                    pOut.TakaStatus = "ISSUE";
                                    pOut.Qty = (p.NetWt * -1);
                                }
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

                            ProdOutModel pOut = db.ProdOuts.SingleOrDefault(k => k.ProdId == p.Id);
                            if (pOut != null)
                            {
                                pOut.IsDeleted = true;
                            }
                        }

                     
                         db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "op job Save");
                        MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());

                    }
                }
            }
               

            
            if (IsSaved)
            {
                NewRec();
                base.SaveDataAsync(newmode);
                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage +" Voucher No.: " + model.VoucherNo, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (!this.OpenForLookup && newmode)
                {
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


        #endregion
       
    }
}
