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
using Konto.Data.Models.Transaction.TradingDto;
using Konto.Shared.Masters.Color;
using Konto.Shared.Masters.Design;
using Konto.Shared.Masters.Grade;
using Konto.Shared.Masters.Item;
using Konto.Shared.Trans.Common;
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

namespace Konto.Shared.Trans.StoreIssue
{
    public partial class StoreIssueIndex : KontoMetroForm
    {
        private List<GrnDto> FilterView = new List<GrnDto>();
        private List<GrnTransDto> DelTrans = new List<GrnTransDto>();
        private List<GrnProdDto> prodDtos = new List<GrnProdDto>();
        private List<GrnProdDto> DelProd = new List<GrnProdDto>();
        private List<JobIssueBarcodeDto> barcodeDtos = new List<JobIssueBarcodeDto>();

        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        private int OrderId;
        private int OrderVoucherId;
        private int OrderTransId;
        private decimal OrderQty;

        public StoreIssueIndex()
        {
            InitializeComponent();

            SetParameter();
            FillLookup();

            this.Load += StoreIssueIndex_Load;
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            this.MainLayoutFile = KontoFileLayout.StoreIssue_Index;
            this.GridLayoutFile = KontoFileLayout.StoreIssue_Trans;

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
            lotNoRepositoryItemButtonEdit.ButtonClick += LotNoRepositoryItemButtonEdit_ButtonClick;

            barcodeSimpleButton.Click += BarcodeSimpleButton_Click;
            gridView1.DoubleClick += GridView1_DoubleClick;

            headerEdit.Hide();
            headerEdit.Parent = this.gridControl1;
            headerEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            headerEdit.Leave += new EventHandler(headerEdit_Leave);

            RefNobuttonEdit.ButtonClick += RefNobuttonEdit_ButtonClick;
            RefNobuttonEdit.KeyDown += RefNobuttonEdit_KeyDown;
            voucherLookup1.SelectedValueChanged += VoucherLookup1_SelectedValueChanged;
        }

        private void VoucherLookup1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.PrimaryKey == 0 && Convert.ToInt32(voucherLookup1.SelectedValue) > 0)
            {
                voucherNoTextEdit.Text = "New-" + DbUtils.NextSerialNo(Convert.ToInt32(voucherLookup1.SelectedValue), 1);
            }
        }

        #region Grid 
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
        private void GridView1_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!"Pcs,Qty,ProductName".Contains(gridView1.FocusedColumn.FieldName)) return;
            var itm = gridView1.GetFocusedRow() as GrnTransDto;
            if (itm == null) return;
            if ("Pcs,Qty".Contains(gridView1.FocusedColumn.FieldName) && this.prodDtos.Any(x => x.TransId == itm.Id))
                e.Cancel = true;
            else if (Convert.ToInt32(itm.RefId) > 0 && gridView1.FocusedColumn.FieldName == "ProductName")
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
                if (StoreIssuePara.Issue_By_Barcode) return;

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
        private void GridControl1_Enter(object sender, EventArgs e)
        {
            gridView1.FocusedColumn = gridView1.Columns["ProductName"];
        }
        private void GridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            // if (StoreIssuePara.Issue_By_Barcode) return;
            if (e.Column == null) return;
            var er = gridView1.GetRow(e.RowHandle) as GrnTransDto;
            if (er == null) return;
            GridCalculation(er);
        }
        private void GridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (StoreIssuePara.Issue_By_Barcode) return;
            try
            {
                if (Convert.ToInt32(storeLookUpEdit.EditValue) == 0) return;
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
                Log.Error(ex, "Store Issue GridControl KeyDown");
                MessageBoxAdv.Show(this, "Error Lookup Setup !!", "Exception ", ex.ToString());
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
        private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            if (StoreIssuePara.Issue_By_Barcode) return;
            var rw = gridView1.GetRow(e.RowHandle) as GrnTransDto;
            rw.Id = -1 * gridView1.RowCount;
        }
        private void ProductRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (StoreIssuePara.Issue_By_Barcode) return;
            var dr = PreOpenLookup();
            if (dr != null)
                OpenItemLookup(dr.ProductId, dr);

        }
        private void ColorRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (StoreIssuePara.Issue_By_Barcode) return;
            var dr = PreOpenLookup();
            if (dr != null)
                OpenColorLookup(dr.ColorId, dr);
        }
        private void GradeRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (StoreIssuePara.Issue_By_Barcode) return;
            var dr = PreOpenLookup();
            if (dr != null)
                OpenGradeLookup(dr.GradeId, dr);
        }
        private void DesignRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (StoreIssuePara.Issue_By_Barcode) return;
            var dr = PreOpenLookup();
            if (dr != null)
                OpenDesignLookup(dr.DesignId, dr);
        }
        private void LotNoRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (StoreIssuePara.Issue_By_Barcode) return;

            var dr = PreOpenLookup();
            if (dr != null)
                ShowItemDetail(dr);

        }

        #endregion
        #region Event
        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveDataAsync(true);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Store Issue Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }
        private void BarcodeSimpleButton_Click(object sender, EventArgs e)
        {
            if (StoreIssuePara.Store_Issue_Against_Order && string.IsNullOrEmpty(RefNobuttonEdit.Text))
            {
                MessageBox.Show("Select Reference No first!!!");
                return;
            }
            var _trans = this.SITbindingSource.DataSource as List<GrnTransDto>;
            this.barcodeDtos = new List<JobIssueBarcodeDto>();
            foreach (var item in _trans)
            {
                var _takaList = this.prodDtos.Where(x => x.TransId == item.Id).ToList();
                foreach (var _taka in _takaList)
                {
                    var _bc = new JobIssueBarcodeDto()
                    {
                        Barcode = _taka.Extra1,
                        ChallanNo = _taka.ChallanNo,
                        ColorId = item.ColorId,
                        Color = item.ColorName,
                        Id = _taka.Id,
                        LotNo = _taka.LotNo,
                        Product = item.ProductName,
                        ProductId = item.ProductId,
                        Qty = _taka.NetWt,
                        RefId = _taka.RefId,
                        SrNo = _taka.SrNo,
                        TransId = _taka.TransId,
                        VoucherDate = _taka.VoucherDate,
                        VoucherNo = _taka.VoucherNo,
                        Weaver = _taka.Weaver,
                        ProdOutId = _taka.ProdOutId,
                        OrgQty = _taka.NetWt
                    };
                    barcodeDtos.Add(_bc);
                }
            }
            var frm = new StoreIssueBarCode();
            frm.bindingSource1.DataSource = this.barcodeDtos;
            frm.OrderQty = this.OrderQty;
            frm.ShowDialog();
            this.barcodeDtos = frm.bindingSource1.DataSource as List<JobIssueBarcodeDto>;
            List<GrnTransDto> _gridtrans = new List<GrnTransDto>();

            if (this.prodDtos == null)
                this.prodDtos = new List<GrnProdDto>();

            var result = barcodeDtos.GroupBy(x => new { x.ProductId, x.Product, x.ColorId, x.Color })
                              .Select(g => new
                              {
                                  g.Key.ProductId,
                                  g.Key.Product,
                                  g.Key.Color,
                                  g.Key.ColorId,
                                  Qty = g.Sum(x => x.Qty),
                                  Pcs = g.Count()
                              }).ToList();
            int rowid = -1;

            foreach (var pro in frm.DelProdOut)
            {
                var pr = this.prodDtos.FirstOrDefault(k => k.Id == pro.Id);
                if (pr != null)
                {
                    this.DelProd.Add(pr);
                    this.prodDtos.Remove(pr);
                }
            }
            foreach (var item in result)
            {
                var ct = _trans.Find(k => k.ProductId == item.ProductId && k.ColorId == item.ColorId);
                //var ct = _trans.Find(k => k.Id == item.TransId);

                if (ct == null)
                {
                    ct = new GrnTransDto();
                    ct.ProductId = Convert.ToInt32(item.ProductId);
                    ct.ProductName = item.Product;
                    ct.ColorId = Convert.ToInt32(item.ColorId);
                    ct.ColorName = item.Color;
                    ct.Pcs = item.Pcs;
                    ct.Qty = item.Qty;
                    ct.ChallanId = 0;
                    ct.Id = rowid;
                    ct.UomId = 24;
                    _trans.Add(ct);
                    _gridtrans.Add(ct);
                }
                else
                {
                    ct.Pcs = item.Pcs;
                    ct.Qty = item.Qty;

                    rowid = ct.Id;

                    _gridtrans.Add(ct);
                }

                var _takalist = barcodeDtos.Where(x => x.ProductId == ct.ProductId && x.ColorId == ct.ColorId).ToList();
                foreach (var _taka in _takalist)
                {
                    var ptrans = prodDtos.Find(k => k.Id == _taka.Id);

                    if (ptrans == null)
                    {
                        ptrans = new GrnProdDto();
                        ptrans.RefId = 0;
                        ptrans.TransId = rowid;
                        ptrans.ProductId = _taka.ProductId;
                        ptrans.ColorId = _taka.ColorId;
                        ptrans.Id = _taka.Id;
                        ptrans.SrNo = _taka.SrNo;
                        ptrans.ProdOutId = (_taka.ProdOutId == null || _taka.ProdOutId <= 0) ? 0 : (int)_taka.ProdOutId;
                        ptrans.NetWt = Convert.ToDecimal(_taka.Qty);
                        ptrans.VoucherNo = _taka.VoucherNo;
                        ptrans.Weaver = _taka.Weaver;
                        ptrans.ChallanNo = _taka.ChallanNo;
                        ptrans.VoucherDate = Convert.ToInt32(_taka.VoucherDate);
                        ptrans.Extra1 = _taka.Barcode;
                        //ptrans.OrgQty = _taka.OrgQty;
                        this.prodDtos.Add(ptrans);
                    }
                    else
                    {
                        ptrans.NetWt = Convert.ToDecimal(_taka.Qty);
                    }
                }
                rowid--;
            }

            SITbindingSource.DataSource = _gridtrans;
            //grnTransDtoBindingSource1.DataSource = _trans;

        }

        private void StoreIssueIndex_Load(object sender, EventArgs e)
        {
            try
            {

                if (StoreIssuePara.Issue_By_Barcode)
                {
                    barcodeLayoutControlItem.ContentVisible = true;
                    this.gridView1.OptionsBehavior.ReadOnly = true;
                }
                else
                {
                    barcodeLayoutControlItem.ContentVisible = false;
                    this.gridView1.OptionsBehavior.ReadOnly = false;
                }
                this.ResetPage();
                NewRec();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Store Issue Load");
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
            if (tabControlAdv1.SelectedIndex == 0)
            {
                divLookUpEdit.Focus();
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as StoreIssueList;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new StoreIssueList();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Store Issue [View]";
            }
        }
        private void RefNobuttonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (!StoreIssuePara.Store_Issue_Against_Order) return;
            ShowOrderPending();
        }

        private void RefNobuttonEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (!StoreIssuePara.Store_Issue_Against_Order) return;
            if (string.IsNullOrEmpty(RefNobuttonEdit.Text))
            {
                if (e.KeyCode != Keys.Enter) return;
            }
            else
            {
                if (e.KeyCode != Keys.F1) return;
            }
            ShowOrderPending();
        }

        #endregion
        #region UDF
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

                divLookUpEdit.Properties.DataSource = _divLists;
                storeLookUpEdit.Properties.DataSource = _storeLists;
            }
        }
        private GrnTransDto PreOpenLookup()
        {
            if (Convert.ToInt32(storeLookUpEdit.EditValue) == 0) return null;
            gridView1.GetRow(gridView1.FocusedRowHandle);
            if (gridView1.GetRow(gridView1.FocusedRowHandle) == null)
            {
                gridView1.AddNewRow();
            }
            var dr = (GrnTransDto)gridView1.GetRow(gridView1.FocusedRowHandle);
            return dr;
        }
        private void OpenItemLookup(int _selvalue, GrnTransDto er)
        {
            var frm = new ProductLkpWindow();
            frm.Tag = MenuId.Product_Master;
            frm.SelectedValue = _selvalue;

            frm.VoucherType = VoucherTypeEnum.Inward;

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                er.ProductId = frm.SelectedValue;
                er.ProductName = frm.SelectedTex;
                var model = frm.SelectedItem as ProductLookupDto;
                er.UomId = model.PurUomId;
                er.Rate = model.DealerPrice;

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

        private void ShowItemDetail(GrnTransDto er)
        {
            ProductModel prod = null;
            using (var db = new KontoContext())
            {
                prod = db.Products.Include("PType").SingleOrDefault(x => x.Id == er.ProductId);
            }
            if (prod == null || prod.SerialReq == "No") return;
            var frm = new IssueItemDetailView();
            frm.IsEditableQty = StoreIssuePara.Editable_Qty;
            frm.TypeEnum = (ProductTypeEnum)prod.PTypeId;

            if (prod.PType.TypeName.ToUpper() == "YARN" || prod.PType.TypeName.ToUpper() == "POY")
            {
                frm.GridLayoutFileName = KontoFileLayout.Sc_Yarn_Item_Details;
                frm.Text = "Box Details";
            }
            else if (prod.PType.TypeName.ToUpper() == "GREY")
            {
                frm.GridLayoutFileName = KontoFileLayout.Sc_Grey_Item_Details;
                frm.Text = "Taka Details";
            }
            else if (prod.PType.TypeName.ToUpper() == "BEAM")
            {
                frm.GridLayoutFileName = KontoFileLayout.Sc_Beam_Item_Details;
                frm.Text = "Beam Details";
            }
            else
            {
                frm.Text = "Product Details";
                frm.GridLayoutFileName = KontoFileLayout.Sc_Finish_Item_Details;
            }

            
            frm.TransId = Convert.ToInt32(er.Id);
            frm.ItemId = er.ProductId;
            //frm.prodOutModelBindingSource.DataSource = this.prodDtos.Where(x => x.TransId == er.Id || x.RefTransId == er.Id).ToList();
            
            frm.prodDtos = new BindingList<GrnProdDto>(this.prodDtos.Where(x => x.RefTransId == er.Id).ToList());
            
            //frm.prodDtos = new BindingList<GrnProdDto>(this.prodDtos.Where(x => x.TransId == er.Id || x.RefTransId == er.Id).ToList());
            if (frm.ShowDialog() != DialogResult.OK) return;
            var tempprod = frm.gridControl1.DataSource as BindingList<GrnProdDto>;

            //er.Qty = Convert.ToDecimal(tempprod.Sum(x => x.NetWt));
            //er.Pcs = tempprod.Count();
            ////remove existing entry
            //foreach (var po in tempprod)
            //{
            //    this.prodDtos.Remove(po);
            //}

            //foreach (var pro in tempprod)
            //{
            //    pro.RefId = this.PrimaryKey;
            //    pro.TransId = er.Id;
            //    pro.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);
            //    pro.ProductId = er.ProductId;
            //    this.prodDtos.Add(pro);
            //}
            //foreach (var pro in frm.DelProd)
            //{
            //    this.prodDtos.Remove(pro);
            //    this.DelProd.Add(pro);
            //}

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
            er.Qty = tempprod.Sum(x => x.NetWt);
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

        public void GridCalculation(GrnTransDto er)
        {
            if (er.Pcs > 0 && er.Cops > 0 && KontoGlobals.PackageId == 1)
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

            gross = gross + er.Freight;

            er.Total = gross;

            gridView1.UpdateCurrentRow();
        }
        private void SetParameter()
        {
            using (var db = new KontoContext())
            {
                var _paralists = db.CompParas.Include("SysPara")
                              .Where(x => x.SysPara.Category == "StoreIssue" && x.CompId == KontoGlobals.CompanyId)
                             .ToList();

                foreach (var item in _paralists)
                {
                    var value = item.ParaValue;
                    switch (item.ParaId)
                    {
                        case 209://Store Issue
                            {
                                StoreIssuePara.Taka_From_Stock = (value == "Y") ? true : false;
                                break;
                            }
                        case 208:
                            {
                                StoreIssuePara.Issue_By_Barcode = (value == "Y") ? true : false;
                                break;
                            }
                        case 207:
                            {
                                StoreIssuePara.Store_Issue_Against_Order = (value == "Y") ? true : false;
                                break;
                            }
                        case 226:
                            {
                                StoreIssuePara.Editable_Qty = (value == "Y") ? true : false;
                                break;
                            }
                    }
                }
            }
        }
        private void ShowOrderPending()
        {
            try
            {
                if (Convert.ToInt32(storeLookUpEdit.EditValue) == 0 || this.PrimaryKey != 0) return;
                // if (grnTypeLookUpEdit.Text.ToUpper() != "PURCHASE") return;
                var ordfrm = new PendingSingleOrderView();
                ordfrm.VoucherType = (VoucherTypeEnum)voucherLookup1.SelectedValue;
                //ordfrm.AccId =0;
                ordfrm.ShowDialog();
                if (ordfrm.DialogResult != DialogResult.OK) return;

                RefNobuttonEdit.Text = ordfrm.SelectedRow.VoucherNo;
                OrderId = ordfrm.SelectedRow.Id;
                OrderTransId = (int)ordfrm.SelectedRow.TransId;
                OrderVoucherId = (int)ordfrm.SelectedRow.VoucherId;
                OrderQty = (decimal)ordfrm.SelectedRow.PendingQty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region Parent Function

        public override void Print()
        {
            base.Print();
            try
            {
                if (this.PrimaryKey == 0) return;

                PageReport rpt = new PageReport();

                rpt.Load(new FileInfo("reg\\doc\\StoreIssue.rdlx"));

                rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);

                doc.Parameters["id"].CurrentValue = this.PrimaryKey;
                doc.Parameters["challan"].CurrentValue = "N";
                doc.Parameters["reportid"].CurrentValue = 0;
                var frm = new KontoRepViewer(doc);
                frm.Text = "Store Issue Print";
                var _tab = this.Parent.Parent as TabControlAdv;
                if (_tab == null) return;
                var pg1 = new TabPageAdv();
                pg1.Text = "StoreIssue Print";
                _tab.TabPages.Add(pg1);
                _tab.SelectedTab = pg1;
                frm.TopLevel = false;
                frm.Parent = pg1;
                frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                frm.Show();// = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "StoreIssue print");
                MessageBoxAdv.Show(this, "Error While Print !!", "Exception ", ex.ToString());
            }
        }
        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<GrnDto>();
            this.Text = "Store Issue [Add New]";

            divLookUpEdit.EditValue = 1;
            storeLookUpEdit.EditValue = 1;
            voucherNoTextEdit.Text = "New";
            voucherDateEdit.EditValue = DateTime.Now;
            empLookup1.SelectedValue = 1;
            empLookup1.SetGroup();
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
            this.ActiveControl = voucherLookup1.buttonEdit1;
            voucherLookup1.SetDefault();
            DelTrans = new List<GrnTransDto>();
            DelProd = new List<GrnProdDto>();
            this.SITbindingSource.DataSource = new List<GrnTransDto>();

            divLookUpEdit.Focus();
        }
        public override void ResetPage()
        {
            base.ResetPage();

            voucherDateEdit.DateTime = DateTime.Now;
            voucherNoTextEdit.Text = string.Empty;
            RefNobuttonEdit.Text = string.Empty;

            transportLookup.SetEmpty();
            empLookup1.SetEmpty();
            DocNotextEdit.Text = string.Empty;
            DocDateEdit.EditValue = DateTime.Now;
            remarkTextEdit.Text = string.Empty;
            DelTrans = new List<GrnTransDto>();
            DelProd = new List<GrnProdDto>();

            divLookUpEdit.Focus();
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

                divLookUpEdit.Focus();
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
            if (Convert.ToInt32(storeLookUpEdit.EditValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "StoreId", Operation = Op.Equals, Value = Convert.ToInt32(storeLookUpEdit.EditValue) });
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
                    .OrderBy(x => x.VoucherDate).ThenBy(x => x.Id)
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
            else if (Convert.ToInt32(voucherLookup1.SelectedValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Voucher", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherLookup1.Focus();
                return false;
            }
            else if (Convert.ToInt32(storeLookUpEdit.EditValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Store", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                storeLookUpEdit.Focus();
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
            else if (trans.Sum(x => x.Qty) == 0)
            {
                MessageBoxAdv.Show(this, "zero Qty Can Not Be accepted", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }
            else if (trans.Any(x => x.ProductId == 0))
            {
                MessageBoxAdv.Show(this, "Empty Product Can bot be accepted", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }
            return true;
        }

        public override void SaveDataAsync(bool newmode)
        {
            bool IsSaved = false;
            if (!ValidateData()) return;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GrnTransDto, ChallanTransModel>().ForMember(x => x.Id, p => p.Ignore());
                cfg.CreateMap<GrnProdDto, ProdOutModel>().ForMember(x => x.Id, p => p.Ignore());
            });

            var _translist = SITbindingSource.DataSource as List<GrnTransDto>;
            List<ChallanTransModel> Trans = new List<ChallanTransModel>();
            List<ProdOutModel> ProdList = new List<ProdOutModel>();
            ChallanModel model = new ChallanModel();
            using (var db = new KontoContext())
            {
                using (var _tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (this.PrimaryKey != 0)
                        {
                            model = db.Challans.Find(this.PrimaryKey);
                        }

                        model.DivId = Convert.ToInt32(divLookUpEdit.EditValue);
                        var TransTypeList = (from p in db.transTypes
                                             where p.IsActive && !p.IsDeleted && (p.Category == null)
                                             select new BaseLookupDto()
                                             {
                                                 DisplayText = p.TypeName,
                                                 Id = p.Id
                                             }).FirstOrDefault();
                        model.ChallanType = Convert.ToInt32(db.transTypes.FirstOrDefault(p => p.IsActive && !p.IsDeleted && (p.Category == null)).Id);
                        model.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);
                        model.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));

                        //model.AccId = Convert.ToInt32(accLookup1.SelectedValue);
                        model.RcdDate = voucherDateEdit.DateTime;
                        model.VoucherNo = voucherNoTextEdit.Text.Trim();

                        model.ChallanNo = "NA";

                        model.EmpId = Convert.ToInt32(empLookup1.SelectedValue);
                        model.StoreId = Convert.ToInt32(storeLookUpEdit.EditValue);

                        model.Remark = remarkTextEdit.Text.Trim();
                        model.TransId = Convert.ToInt32(transportLookup.SelectedValue);
                        model.DocNo = DocNotextEdit.Text.Trim();
                        model.DocDate = Convert.ToDateTime(DocDateEdit.EditValue);
                        model.TypeId = (int)TypeEnum.StoreIssue;
                        model.CompId = KontoGlobals.CompanyId;
                        model.YearId = KontoGlobals.YearId;
                        model.BranchId = KontoGlobals.BranchId;
                        model.IsActive = true;

                        model.TotalQty = _translist.Sum(x => x.Qty);
                        model.TotalPcs = _translist.Sum(x => x.Pcs);
                        model.TotalAmount = _translist.Sum(x => x.Total);
                        if (model.Id == 0)
                        {
                            model.VoucherNo = DbUtils.NextSerialNo(model.VoucherId, db);
                            if (DbUtils.CheckExistVoucherNo(model.VoucherId, model.VoucherNo, db, model.Id))
                            {
                                MessageBox.Show("Duplicate Voucher No Not Allowed");
                                _tran.Rollback();
                                return;
                            }

                            db.Challans.Add(model);
                            db.SaveChanges();
                        }

                        foreach (var item in _translist)
                        {
                            item.ChallanId = model.Id;
                            var tranModel = new ChallanTransModel();
                            if (item.Id > 0)
                            {
                                tranModel = db.ChallanTranses.Find(item.Id);
                            }
                            var map = new Mapper(config);
                            map.Map(item, tranModel);

                            if (item.Id <= 0)
                            {
                                tranModel.RefId = OrderTransId;
                                tranModel.MiscId = OrderId;
                                tranModel.RefVoucherId = OrderVoucherId;
                                tranModel.RefNo = RefNobuttonEdit.Text;

                                db.ChallanTranses.Add(tranModel);
                                db.SaveChanges();

                            }
                            Trans.Add(tranModel);
                            
                            var prlist = prodDtos.Where(k => k.TransId == item.Id).ToList();

                            foreach (var p in prlist)
                            {
                                //ProdOutModel Out = db.ProdOuts.Find(p.Id);
                                ProdOutModel Out = db.ProdOuts.Find(p.ProdOutId);
                                
                                 var   pm = db.Prods.Find(p.Id);
                                if (pm == null) continue;
                                    pm.ProdStatus = "ISSUE";
                                
                                

                                if (Out == null)
                                    Out = new ProdOutModel();

                                Out.ProductId = p.ProductId;
                                Out.ColorId = p.ColorId;
                                Out.GradeId = p.GradeId;
                                Out.CompId = KontoGlobals.CompanyId;
                                Out.YearId = KontoGlobals.YearId;
                                Out.TakaStatus = "ISSUE";
                                Out.SrNo = p.SrNo;
                                Out.GrayMtr = (p.NetWt * -1);
                                Out.Qty = (p.NetWt * -1);
                                Out.ProdId = pm.Id;
                                Out.RefId = model.Id;
                                Out.VoucherId = model.VoucherId;
                                Out.VoucherNo = p.VoucherNo;
                                Out.TransId = tranModel.Id;
                                Out.IsActive = true;
                                Out.IsDeleted = false;

                                if (Out.Id <= 0)
                                {
                                    db.ProdOuts.Add(Out);
                                    db.SaveChanges();
                                }
                                ProdList.Add(Out);
                            }

                        }
                        //delete item fro trans table entry
                        foreach (var item in DelTrans)
                        {
                            if (item.Id == 0) continue;
                            var _model = db.ChallanTranses.Find(item.Id);
                            _model.IsDeleted = true;

                            var delProdOut = prodDtos.Where(k => k.RefTransId == item.Id).ToList();
                            foreach (var poitem in delProdOut)
                            {
                                if (poitem.ProdOutId > 0)
                                {
                                    ProdOutModel pOut = db.ProdOuts.Find(poitem.ProdOutId);
                                    pOut.IsDeleted = true;
                                }
                                ProdModel pitem = db.Prods.Find(poitem.Id);
                                if(pitem!=null)
                                    pitem.ProdStatus = "STOCK";
                                
                               
                            }
                        }

                        // delete from item details
                        foreach (var p in DelProd)
                        {
                            if (p.Id == 0) continue;
                            var prd = db.Prods.Find(p.Id);
                            if ((prd != null && StoreIssuePara.Taka_From_Stock)
                                    || (prd != null && StoreIssuePara.Issue_By_Barcode))
                            {
                                if (p.ProdOutId > 0)
                                {
                                    var pout = db.ProdOuts.Find(p.ProdOutId);
                                    if (pout == null) continue;

                                    pout.IsDeleted = true; 
                                    prd.ProdStatus = "STOCK";
                                }
                            }
                            else
                            {
                                prd.IsDeleted = true;
                            } 
                        }

                        //sotock effect
                        var stk = db.StockTranses.Where(k => k.MasterRefId == model.RowId).ToList();
                        if (stk != null)
                            db.StockTranses.RemoveRange(stk);

                        foreach (var item in Trans)
                        {
                            string TableName = "StoreIssue";
                            var stockReq = db.Products.FirstOrDefault(k => k.Id == item.ProductId).StockReq;
                            if (stockReq == "No") continue;

                            //var prList = ProdList.Where(x => x.TransId == item.Id).ToList();
                            //if (prList.Count > 0)
                            //{
                            //    foreach (var prod in prList)
                            //    {
                            //       StockEffect.StockTransChlnProdOutEntry(model, item,true, TableName, db, prod,false);
                            //    }
                            //}
                            //else
                            //{
                            StockEffect.StockTransChlnEntry(model, item, true, TableName, KontoGlobals.UserName, db);
                            //}
                        }

                        db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Store Issue Save");
                        MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
                    }
                }
            }

            if (IsSaved)
            {
                base.SaveDataAsync(newmode);
                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage + " Voucher No.: " + model.VoucherNo, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (this.voucherLookup1.GroupDto.PrintAfterSave && MessageBox.Show("Print Issue ?", "Print", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.PrimaryKey = model.Id;
                    Print();
                    this.PrimaryKey = 0;
                }

                if (!this.OpenForLookup && newmode)
                {
                    this.ResetPage();
                    this.NewRec();
                    divLookUpEdit.Focus();
                }
                else
                {
                    this.Close();
                    this.Dispose();
                }
            }
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
            DocNotextEdit.Text = model.DocNo;
            DocDateEdit.EditValue = model.DocDate;
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
                             join cl in _context.ColorModels on ct.ColorId equals cl.Id into join_cl
                             from cl in join_cl.DefaultIfEmpty()
                             join dm in _context.Products on ct.DesignId equals dm.Id into join_dm
                             from dm in join_dm.DefaultIfEmpty()
                             join np in _context.Products on ct.NProductId equals np.Id into join_np
                             from np in join_np.DefaultIfEmpty()
                             join grd in _context.Grades on ct.GradeId equals grd.Id into join_grd
                             from grd in join_grd.DefaultIfEmpty()
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
                                 OtherAdd = ct.OtherAdd,
                                 OtherLess = ct.OtherLess,
                                 Pcs = ct.Pcs,
                                 ProductId = (int)ct.ProductId,
                                 ProductName = pd.ProductName,
                                 Qty = ct.Qty,
                                 Rate = ct.Rate,
                                 RefId = ct.RefId,
                                 MiscId = ct.MiscId,
                                 RefVoucherId = ct.RefVoucherId,
                                 RefNo = ct.RefNo,
                                 Remark = ct.Remark,
                                 Sgst = ct.Sgst,
                                 SgstPer = ct.SgstPer,
                                 Total = ct.Total,
                                 UomId = (int)ct.UomId
                             }).ToList();

                //prodDtos = _context.Prods.Where(x => x.RefId == model.Id && !x.IsDeleted)
                //                         .ProjectToList<GrnProdDto>(config);

                if (StoreIssuePara.Issue_By_Barcode)
                {
                    var lst = (from po in _context.ProdOuts
                               join ct in _context.ChallanTranses on po.TransId equals ct.Id into join_ct
                               from ct in join_ct.DefaultIfEmpty()
                               join c in _context.Challans on ct.ChallanId equals c.Id into join_c
                               from c in join_c.DefaultIfEmpty()
                               join ac in _context.Accs on c.AccId equals ac.Id into join_ac
                               from ac in join_ac.DefaultIfEmpty()
                               join p in _context.Prods on po.ProdId equals p.Id into join_p
                               from p in join_p.DefaultIfEmpty()
                               orderby ct.Id
                               where ct.IsActive == true && ct.IsDeleted == false &&
                               po.RefId == model.Id && po.IsDeleted == false
                               select new GrnProdDto()
                               {
                                   RefId = po.RefId,
                                   TransId = po.TransId,
                                   ProductId = po.ProductId,
                                   ColorId = po.ColorId,
                                   Id = (int)po.ProdId,
                                   SrNo = p.SrNo,
                                   ProdOutId = po.Id,
                                   NetWt = (decimal)(po.Qty * -1),
                                   VoucherNo = po.VoucherNo,
                                   Weaver = ac.AccName,
                                   ChallanNo = c.VoucherNo,
                                   VoucherDate = c.VoucherDate,
                                   Extra1 = p.Extra1,

                               }).ToList();

                    this.prodDtos = lst;
                }
                else
                {
                    var spcol = _context.SpCollections.FirstOrDefault(k => k.Id ==
                                   (int)SpCollectionEnum.OutwardprodList);

                    if (spcol == null)
                    {
                        prodDtos = _context.Database.SqlQuery<GrnProdDto>(
                                   "dbo.OutwardprodList @CompanyId={0},@VoucherId={1}, @RefId={2}",
                                   KontoGlobals.CompanyId, (int)VoucherTypeEnum.StoreIssue, model.Id).ToList();
                    }
                    else
                    {
                        prodDtos = _context.Database.SqlQuery<GrnProdDto>(
                         spcol.Name + " @CompanyId={0},@VoucherId={1}, @RefId={2}",
                         KontoGlobals.CompanyId, (int)VoucherTypeEnum.StoreIssue, model.Id).ToList();
                    }
                }
                this.SITbindingSource.DataSource = _list;
                RefNobuttonEdit.Text = _list.FirstOrDefault().RefNo;
                OrderQty = _list.Sum(k => k.Qty);
                
            }
            this.Text = "Store Issue [View/Modify]";
        }
        #endregion
    }
}