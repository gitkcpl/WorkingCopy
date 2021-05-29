using AutoMapper;
using DevExpress.XtraEditors;
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

using Konto.Shared.Masters.Color;
using Konto.Shared.Masters.Design;
using Konto.Shared.Masters.Item;
using Konto.Trading.MillReceipt;
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
using System.Windows.Forms;
using ExpressionBuilder = Konto.Core.Shared.Libs.ExpressionBuilder;

namespace Konto.Trading.MillReturn
{
    public partial class MrIndex : KontoMetroForm
    {
        private List<ChallanModel> FilterView = new List<ChallanModel>();
        private List<MiTransDto> DelTrans = new List<MiTransDto>();
        private List<ProdOutModel> prodDtos = new List<ProdOutModel>();
        private List<ProdOutModel> DelProd = new List<ProdOutModel>();

        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
       
        public MrIndex()
        {
            InitializeComponent();
           
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            gridControl1.ProcessGridKey += GridControl1_ProcessGridKey;
            productRepositoryItemButtonEdit.ButtonClick += ProductRepositoryItemButtonEdit_ButtonClick;
            colorRepositoryItemButtonEdit.ButtonClick += ColorRepositoryItemButtonEdit_ButtonClick;
            designRepositoryItemButtonEdit.ButtonClick += DesignRepositoryItemButtonEdit_ButtonClick;
            accLookup1.SelectedValueChanged += AccLookup1_SelectedValueChanged;
            gridView1.InitNewRow += GridView1_InitNewRow;
            gridView1.CellValueChanged += GridView1_CellValueChanged;
            gridView1.KeyDown += GridView1_KeyDown;
            gridControl1.Enter += GridControl1_Enter;
            gridView1.CustomDrawRowIndicator += GridView1_CustomDrawRowIndicator;
            gridView1.ShowingEditor += GridView1_ShowingEditor;
            gridView1.MouseUp += GridView1_MouseUp;
            lotNoRepositoryItemButtonEdit.ButtonClick += LotNoRepositoryItemButtonEdit_ButtonClick;
            challanNoRrepositoryItemButtonEdit.ButtonClick += ChallanNoRrepositoryItemButtonEdit_ButtonClick;
            gridView1.DoubleClick += GridView1_DoubleClick;
            this.MainLayoutFile = KontoFileLayout.Mrv_Index;
            this.GridLayoutFile = KontoFileLayout.Mrv_Trans;
            
            FillLookup();
          
            headerEdit.Hide();
            headerEdit.Parent = this.gridControl1;
            headerEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            headerEdit.Leave += new EventHandler(headerEdit_Leave);
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

        #region UDF

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
           
           

            var dr = uomRepositoryItemLookUpEdit.GetDataSourceRowByKeyValue(er.UomId) as UomLookupDto;
            
            if (dr!=null && dr.RateOn == "N")
            {
                er.Gross = decimal.Round(er.Pcs * er.Rate, 2);
            }
            else
            {
                er.Gross = decimal.Round( er.Qty * er.Rate, 2);
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

          //  er.Cess = decimal.Round(er.Qty * er.CessPer, 2, MidpointRounding.AwayFromZero);
            er.Total = gross + er.Sgst + er.Cgst + er.Igst;

            gridView1.UpdateCurrentRow();

            FinalTotal();
        }
        private void FinalTotal()
        {
           
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

         

        }
       
        private void OpenItemLookup(int _selvalue, MiTransDto er)
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
                gridView1.FocusedColumn = gridView1.VisibleColumns[gridView1.FocusedColumn.VisibleIndex + 1];
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
             
               // invTypeLookUpEdit.Properties.DataSource = _divLists;
                storeLookUpEdit.Properties.DataSource = _storeLists;
            }
        }

        private bool ValidateData()
        {
            var dt = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
          
             if (Convert.ToInt32(voucherLookup1.SelectedValue) == 0)
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
          
         
            else if (dt > KontoGlobals.ToDate || dt < KontoGlobals.FromDate)
            {
                MessageBoxAdv.Show(this, "Voucher date out of financial range", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherDateEdit.Focus();
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
           
            //check for duplicate bill no
            using(var db = new KontoContext())
            {
                var accid = Convert.ToInt32(accLookup1.SelectedValue);
               

                 var find1 = db.Challans.FirstOrDefault(
               x => x.AccId == accid && !x.IsDeleted && x.ChallanNo == challanNotextEdit.Text.Trim() && x.CompId == KontoGlobals.CompanyId
               && x.YearId == KontoGlobals.YearId && x.Id!= this.PrimaryKey);

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
            this.PrimaryKey = model.Id;
            voucherLookup1.SelectedValue = model.VoucherId;
            voucherLookup1.SetGroup(model.VoucherId);

            voucherDateEdit.EditValue = KontoUtils.IToD(model.VoucherDate);
            voucherNoTextEdit.Text = model.VoucherNo;

            accLookup1.SelectedValue = model.AccId;
            accLookup1.SetAcc(model.AccId);
            challanNotextEdit.Text = model.ChallanNo;
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
                              join cl in _context.ColorModels on ct.ColorId equals cl.Id into join_cl
                              from cl in join_cl.DefaultIfEmpty()
                              join dm in _context.Products on ct.DesignId equals dm.Id into join_dm
                              from dm in join_dm.DefaultIfEmpty()
                            
                             
                              orderby ct.Id
                              where ct.IsActive == true && ct.IsDeleted == false &&
                              ct.ChallanId == model.Id
                              select new MiTransDto()
                             {
                                  Id = ct.Id,Cess=ct.Cess,CessPer=ct.CessPer,Cgst=ct.Cgst,CgstPer=ct.CgstPer,
                                  ChallanId=ct.ChallanId,ColorId=ct.ColorId.HasValue ? (int)ct.ColorId: 1,ColorName=cl.ColorName,
                                  DesignId= ct.DesignId.HasValue? (int)ct.DesignId:1,DesignNo=dm.ProductCode,Disc=ct.Disc,DiscPer=ct.DiscPer,Freight=ct.Freight,
                                  FreightRate=ct.FreightRate,
                                  Gross=ct.Gross,Igst=ct.Igst,
                                  IgstPer=ct.IgstPer,LotNo=ct.LotNo,MiscId=ct.MiscId,OtherAdd=ct.OtherAdd,OtherLess=ct.OtherLess,
                                  Pcs=ct.Pcs,ProductId=(int)ct.ProductId,ProductName=pd.ProductName,Qty=ct.Qty,Rate=ct.Rate,RefId=ct.RefId,
                                  RefVoucherId=ct.RefVoucherId,Remark=ct.Remark,Sgst=ct.Sgst,SgstPer=ct.SgstPer,Total=ct.Total,UomId=(int)ct.UomId,
                                 RefNo = ct.RefNo
                              }).ToList();

                prodDtos = _context.ProdOuts.Where(x => x.RefId == model.Id && !x.IsDeleted).ToList();
                                        

                this.grnTransDtoBindingSource1.DataSource = _list;
            }


            FinalTotal();
            this.Text = "Mill Return [View/Modify]";

        }

        int cid = 0;
        private void ShowPendingLot(MiTransDto ct)
        {
            var frm = new PendingMillIssueView();
            frm.AccId = Convert.ToInt32(this.accLookup1.SelectedValue);
            
            if (frm.ShowDialog() != DialogResult.OK) return;

            var ord = frm.SelectedRow;
            if (ord == null) return;
            cid--;
            ct.Id = cid;
            ct.ProductId = Convert.ToInt32(ord.ProductId);
            ct.ProductName = ord.GreyQuality;
            ct.FinishQuality = ord.GreyQuality;
            ct.Pcs = Convert.ToInt32(ord.PendingPcs);
            ct.Qty = ord.PendingQty != 0 ? (decimal)ord.PendingQty : 0;
            ct.LotNo = ord.LotNo;
            ct.RefNo = ord.RefNo;
            ct.Rate = ord.Rate != null ? (decimal)ord.Rate : 0;
            ct.Sgst = 0;
            ct.SgstPer = ord.Sgst != null ? (decimal)ord.Sgst : 0;
            ct.Cgst = 0;
            ct.CgstPer = ord.Cgst;
            ct.Igst = 0;
            ct.IgstPer = ord.Igst;
            ct.Disc = 0;
            ct.DiscPer = 0;
            ct.Gross = 0;
            ct.UomId = Convert.ToInt32(ord.UomId);
            ct.DesignId = Convert.ToInt32(ord.DesignId);
            ct.ColorId = Convert.ToInt32( ord.ColorId);

            ct.RefNo = ord.VoucherNo;
            
            ct.RefId = ord.TransId;
            ct.MiscId = ord.Id;
            ct.RefVoucherId = ord.VoucherId;

            gridControl1.RefreshDataSource();

        }
        private void ShowItemDetail(MiTransDto er)
        {
            
            var frm = new MrTakaDetails();
            frm.AccId = Convert.ToInt32(this.accLookup1.SelectedValue);
            frm.ChallanId = Convert.ToInt32( er.MiscId);
            frm.ChallanTransId = Convert.ToInt32( er.RefId);
            frm.ItemId = er.ProductId;
            frm.prodOutModelBindingSource.DataSource =this.prodDtos.Where(x=>x.TransId == er.Id).ToList();
            if (frm.ShowDialog() != DialogResult.OK) return;
            var tempprod = (frm.prodOutModelBindingSource.DataSource as List<ProdOutModel>).ToList();
          
            er.Qty = Convert.ToDecimal(tempprod.Sum(x=>x.GrayMtr));
            er.Pcs = tempprod.Count();
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
                pro.ProductId = er.ProductId;
                this.prodDtos.Add(pro);
            }
            foreach (var pro in frm.DelProd)
            {
                this.prodDtos.Remove(pro);
                this.DelProd.Add(pro);
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
            if (!"Pcs,Qty".Contains(gridView1.FocusedColumn.FieldName)) return;
            if (!(gridView1.GetFocusedRow() is MiTransDto itm)) return;
            if (this.prodDtos.Any(x => x.TransId == itm.Id))
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
            gridView1.FocusedColumn = gridView1.VisibleColumns[0];
        }
        private void GridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == null) return;
            if (!(gridView1.GetRow(e.RowHandle) is MiTransDto er)) return;
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
                if(gridView1.FocusedColumn.FieldName == "RefNo")
                {

                    if (e.KeyCode == Keys.Return)
                    {
                        if (Convert.ToInt32( dr.RefId) == 0)
                        {
                            ShowPendingLot(dr);
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        ShowPendingLot(dr);
                        e.Handled = true;
                    }
                }
                else if (gridView1.FocusedColumn.FieldName == "ProductName")
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
                Log.Error(ex, "Mill Return GridControl KeyDown");
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
        private void ChallanNoRrepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
                ShowPendingLot(dr);
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
            if (tabControlAdv1.SelectedIndex == 1 && tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as MrListView;
                _list.ActiveControl = _list.KontoGrid;
                this.Text = "Mill Return [View]";
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new MrListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Mill Return [View]";

            }
            else if (tabControlAdv1.SelectedIndex == 3)
            {
                if (tabPageAdv4.Controls.Count > 0) return;
                // var _frm = new ChlParaMainView();
                var _frm = Activator.CreateInstance("Konto.Reporting", "Konto.Reporting.Para.ChlPara.ChlParaMainView").Unwrap() as KontoForm;

                _frm.TopLevel = false;
                _frm.Parent = tabPageAdv4;
                _frm.ReportFilterType = "MillRet";
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

                Log.Error(ex, "Mill Return Save");
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
                Log.Error(ex, "Mill Return print");
                MessageBoxAdv.Show(this, "Error While Print !!", "Exception ", ex.ToString());

            }
        }
        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<ChallanModel>();
            this.Text = "Mill Return [Add New]";
           
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
           
            DelTrans = new List<MiTransDto>();
            DelProd = new List<ProdOutModel>();
            prodDtos = new List<ProdOutModel>();
            this.grnTransDtoBindingSource1.DataSource = new List<MiTransDto>();
            
        }
        public override void ResetPage()
        {
            base.ResetPage();
            
            accLookup1.SetEmpty();
           
            challanNotextEdit.Text = string.Empty;
            voucherDateEdit.DateTime = DateTime.Now;
            billDateEdit.DateTime = DateTime.Now;
            voucherNoTextEdit.Text = string.Empty;
           
            transportLookup.SetEmpty();
            empLookup1.SetEmpty();
            lrNotextEdit.Text = string.Empty;
            lrDateEdit.EditValue = DateTime.Now;
            remarkTextEdit.Text = string.Empty;
            
            DelTrans = new List<MiTransDto>();
            DelProd = new List<ProdOutModel>();
            prodDtos = new List<ProdOutModel>();
            
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
                cfg.CreateMap<MiTransDto, ChallanTransModel>().ForMember(x => x.Id, p => p.Ignore());
                cfg.CreateMap<ProdOutModel, ProdOutModel>().ForMember(x => x.Id, p => p.Ignore());
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
                        }
                        _find.TotalQty = _translist.Sum(x => x.Qty);
                        _find.TotalPcs = _translist.Sum(x => x.Pcs);
                        _find.TotalAmount = _translist.Sum(x => x.Total);
                        UpdateChallan(db, _find);
                  
                        var map = new Mapper(config);
                      
                        
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
                            tranModel.IssuePcs = item.Pcs;
                            tranModel.IssueQty = item.Qty;
                            if (tranModel.Id <= 0)
                            {
                                db.ChallanTranses.Add(tranModel);
                                db.SaveChanges();
                               
                            }
                            item.Id = tranModel.Id;
                            Trans.Add(tranModel);
                            // add subdetails item details
                            var prlist = prodDtos.Where(k => k.TransId == transid).ToList();
                           

                            foreach (var p in prlist)
                            {
                                var prod = db.Prods.Find(p.ProdId);
                                prod.ProdStatus = "Stock";
                                prod.CProductId = item.ProductId;
                                var pOut = new ProdOutModel();
                                if (p.Id > 0)
                                {
                                    pOut = db.ProdOuts.Find(p.Id);
                                    pOut.TakaStatus = "MillRet";
                                    pOut.IsOk = true;
                                    pOut.Qty = p.FinMrt;
                                    pOut.GrayMtr = p.FinMrt;
                                    pOut.FinMrt = p.FinMrt;
                                    pOut.TP1 = p.TP1;
                                    pOut.ProductId = p.ProductId;
                                    pOut.ColorId = p.ColorId;
                                    pOut.GradeId = p.GradeId;
                                    pOut.IsActive = true;
                                   // map.Map(p, pOut);
                                }

                                if(p.Id==0)
                                {
                                    ProdOutModel Out = new ProdOutModel();
                                    Out.ProductId = p.ProductId;
                                    Out.ColorId = p.ColorId;
                                    Out.GradeId = p.GradeId;
                                    Out.CompId =_find.CompId;
                                    Out.YearId = _find.YearId;
                                    Out.TakaStatus = "MillRet";
                                    Out.SrNo = p.SrNo;
                                    Out.Qty = p.FinMrt;
                                    Out.GrayMtr = p.FinMrt;
                                    Out.FinMrt = p.FinMrt;
                                    Out.IsOk = true;
                                    Out.TP1 = p.TP1;
                                    Out.ProdId = p.ProdId;
                                    Out.RefId = _find.Id;
                                    Out.VoucherId = _find.VoucherId;
                                    Out.VoucherNo = p.VoucherNo;
                                    Out.TransId = tranModel.Id;
                                    Out.IsActive = true;
                                    db.ProdOuts.Add(Out);
                                }
                               
                            }

                        }
                        //delete item from ord trans
                        foreach (var item in DelTrans)
                        {
                            if (item.Id == 0) continue;
                            var _model = db.ChallanTranses.Find(item.Id);
                            _model.IsDeleted = true;

                            var delProdOut = db.ProdOuts.Where(p => p.TransId == item.Id).ToList();
                            foreach (var poitem in delProdOut)
                            {
                                if (poitem.Id != 0)
                                {
                                    ProdOutModel pOut = db.ProdOuts.Find(poitem.Id);
                                    if (pOut != null)
                                    {
                                        ProdModel pitem = db.Prods.Find(pOut.ProdId);
                                        if (pitem != null)
                                        {
                                            pitem.ProdStatus = "Issue";
                                        }

                                        pOut.IsDeleted = true;
                                    }
                                }
                            }
                        }

                        // delete from item details
                        foreach (var p in DelProd)
                        {
                            if (p.Id == 0) continue;
                             var prd = db.ProdOuts.Find(p.Id);
                             if(prd!=null)
                                prd.IsDeleted = true;
                            var pro = db.Prods.Find(p.ProdId);
                            pro.ProdStatus = "Issue";
                        }

                        //sotock effect
                        var stk = db.StockTranses.Where(k => k.MasterRefId == _find.RowId).ToList();
                        if (stk != null)
                            db.StockTranses.RemoveRange(stk);

                        foreach (var item in Trans)
                        {
                            string TableName = "Mill Return";
                            var stockReq = db.Products.FirstOrDefault(k => k.Id == item.ProductId).StockReq;
                            if (stockReq == "No") continue;
                         //   var prList = ProdList.Where(x => x.TransId == item.Id).ToList();
                            StockEffect.StockTransChlnEntry(_find, item,false, TableName, KontoGlobals.UserName, db);
                            
                        }

                         db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Mill Return Save");
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

        private bool UpdateChallan(KontoContext db,ChallanModel model)
        {
           
            model.DivId = 1;
           

            model.ChallanType = (int)ChallanTypeEnum.INWARD_FROM_JOB;
            model.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);
            model.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));

            model.AccId = Convert.ToInt32(accLookup1.SelectedValue);
           
            model.RcdDate = billDateEdit.DateTime;
            model.VoucherNo = voucherNoTextEdit.Text.Trim();

          
            model.ChallanNo = challanNotextEdit.Text.Trim();
           
            model.EmpId = Convert.ToInt32(empLookup1.SelectedValue);
            model.StoreId = Convert.ToInt32(storeLookUpEdit.EditValue);

            model.Remark = remarkTextEdit.Text.Trim();
            model.TransId = Convert.ToInt32(transportLookup.SelectedValue);
            model.DocNo = lrNotextEdit.Text.Trim();
            model.DocDate = Convert.ToDateTime(lrDateEdit.EditValue);
            model.TypeId = (int)VoucherTypeEnum.MillReturn;
            model.CompId = KontoGlobals.CompanyId;
            model.YearId = KontoGlobals.YearId;
            model.BranchId = KontoGlobals.BranchId;
           
            var _translist = grnTransDtoBindingSource1.DataSource as List<MiTransDto>;
            model.TotalAmount = _translist.Sum(x=>x.Total);
          
            model.TotalQty = _translist.Sum(x => x.Qty);
            model.TotalPcs = _translist.Sum(x => x.Pcs);
            model.IsActive = true;
           
            if (model.Id == 0)
            {
                model.VoucherNo = DbUtils.NextSerialNo(model.VoucherId, db);
                
                if (DbUtils.CheckExistVoucherNo(model.VoucherId, model.VoucherNo, db, model.Id))
                {
                    MessageBox.Show("Duplicate Voucher No Not Allowed");
                    return false;
                }

                db.Challans.Add(model);
                db.SaveChanges();
            }

            return true;

        }
    
        #endregion
       
    }
}
