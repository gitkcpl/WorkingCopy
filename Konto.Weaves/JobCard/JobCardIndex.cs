using AutoMapper;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters.Dtos;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using Konto.Shared.Masters.Color;
using Konto.Shared.Masters.Design;
using Konto.Shared.Masters.Item;
using Konto.Weaves.BeamLoading;
using Serilog;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Konto.Shared;
using GrapeCity.ActiveReports;
using System.IO;

namespace Konto.Weaves.JobCard
{
    public partial class JobCardIndex : KontoMetroForm
    {
        private List<JobCardModel> FilterView = new List<JobCardModel>();
        private List<JobCardTransDto> DelOrder = new List<JobCardTransDto>();
        private List<JobCardTransDto> DelYarnDetail = new List<JobCardTransDto>();

        private int OrderId = 0;
        private int MachineId = 0;
        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        public JobCardIndex()
        {
            InitializeComponent();

            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            FillLookup();

            this.Load += JobCardIndex_Load;
            this.MainLayoutFile = KontoFileLayout.JobCard_Index;
            this.GridLayoutFile = KontoFileLayout.JobCard_Trans;

            OrderDetailgridView.InitNewRow += ProdgridView_InitNewRow;
            OrderDetailgridView.KeyDown += ProdgridView_KeyDown;
            OrderDetailgridControl.Enter += ProdGridControl_Enter;
            OrderDetailgridView.MouseUp += ProdgridView_MouseUp;
            OrderDetailgridView.InvalidRowException += ProdgridView_InvalidRowException;
            OrderDetailgridView.ShowingEditor += ProdgridView_ShowingEditor;
            OrderDetailgridView.DoubleClick += ProdgridView_DoubleClick;
            OrderDetailgridView.CustomDrawRowIndicator += ProdgridView_CustomDrawRowIndicator;
            OrderDetailgridControl.ProcessGridKey += OrderDetailgridControl_ProcessGridKey;

            yarngridView.InitNewRow += YarngridView_InitNewRow;
            yarngridView.KeyDown += YarngridView_KeyDown;
            yarngridControl.Enter += YarngridControl_Enter;
            yarngridView.MouseUp += YarngridView_MouseUp;
            yarngridView.InvalidRowException += YarngridView_InvalidRowException;
            yarngridView.ShowingEditor += YarngridView_ShowingEditor;
            yarngridView.DoubleClick += YarngridView_DoubleClick;
            yarngridView.CustomDrawRowIndicator += YarngridView_CustomDrawRowIndicator;
            yarngridControl.ProcessGridKey += YarngridControl_ProcessGridKey;

            productRepositoryItemButtonEdit.ButtonClick += ProductRepositoryItemButtonEdit_ButtonClick;
            colorRepositoryItemButtonEdit.ButtonClick += ColorRepositoryItemButtonEdit_ButtonClick;

            ProductrepositoryItemButtonEdit2.ButtonClick += ProductrepositoryItemButtonEdit2_ButtonClick;
            ColorrepositoryItemButtonEdit2.ButtonClick += ColorrepositoryItemButtonEdit2_ButtonClick;
            ShaderepositoryItemButtonEdit.ButtonClick += ShaderepositoryItemButtonEdit_ButtonClick;
            SelectSimpleButton.Click += SelectSimpleButton_Click;
        }

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

                divLookUpEdit.Properties.DataSource = _divLists;
            }
        }
        private JobCardTransDto PreOpenLookup()
        {
            OrderDetailgridView.GetRow(OrderDetailgridView.FocusedRowHandle);
            if (OrderDetailgridView.GetRow(OrderDetailgridView.FocusedRowHandle) == null)
            {
                OrderDetailgridView.AddNewRow();
            }
            var dr = (JobCardTransDto)OrderDetailgridView.GetRow(OrderDetailgridView.FocusedRowHandle);
            return dr;
        }
        private void OpenItemLookup(int _selvalue, JobCardTransDto er)
        {
            var frm = new ProductLkpWindow();

            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Product_Master;
            frm.VoucherType = VoucherTypeEnum.JobCard;

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                er.ItemId = frm.SelectedValue;
                er.ItemType = frm.SelectedTex;
                var model = frm.SelectedItem as ProductLookupDto;

                OrderDetailgridView.FocusedColumn = OrderDetailgridView.GetNearestCanFocusedColumn(OrderDetailgridView.FocusedColumn);
            }
        }
        private void OpenColorLookup(int _selvalue, JobCardTransDto er)
        {
            var frm = new ColorLkpWindow();
            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Color;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                OrderDetailgridView.BeginDataUpdate();
                er.ColorId = frm.SelectedValue;
                er.ColorCategory = frm.SelectedTex;

                OrderDetailgridView.EndDataUpdate();
                OrderDetailgridView.FocusedColumn = OrderDetailgridView.GetVisibleColumn(colColorName.VisibleIndex + 1);
            }
        }
        private void OpenDesignLookup(int _selvalue, JobCardTransDto er)
        {
            var frm = new DesignLkpWindow();
            frm.Tag = MenuId.Design_Master;
            frm.SelectedValue = _selvalue;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                OrderDetailgridView.BeginDataUpdate();
                er.DesignId = frm.SelectedValue;
                er.DesignName = frm.SelectedTex;
                OrderDetailgridView.EndDataUpdate();
                OrderDetailgridView.FocusedColumn = OrderDetailgridView.GetVisibleColumn(colDesignNo.VisibleIndex + 1);
            }

        }

        private JobCardTransDto PreOpenYarnLookup()
        {
            yarngridView.GetRow(yarngridView.FocusedRowHandle);
            if (yarngridView.GetRow(yarngridView.FocusedRowHandle) == null)
            {
                yarngridView.AddNewRow();
            }
            var dr = (JobCardTransDto)yarngridView.GetRow(yarngridView.FocusedRowHandle);
            return dr;
        }
        private void OpenItemYarnLookup(int _selvalue, JobCardTransDto er)
        {
            var frm = new ProductLkpWindow();

            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Product_Master;
            frm.VoucherType = VoucherTypeEnum.JobCard;

            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {
                er.DesignId = frm.SelectedValue;
                er.DesignName = frm.SelectedTex;
                var model = frm.SelectedItem as ProductLookupDto;

                //yarngridView.FocusedColumn = yarngridView.GetNearestCanFocusedColumn(yarngridView.FocusedColumn);
                yarngridView.FocusedColumn = yarngridView.GetVisibleColumn(colDesignName1.VisibleIndex + 1);
            }
        }
        private void OpenColorYarnLookup(int _selvalue, JobCardTransDto er)
        {
            var frm = new ColorLkpWindow();
            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Color;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                yarngridView.BeginDataUpdate();
                er.ColorId = frm.SelectedValue;
                er.ColorCategory = frm.SelectedTex;

                yarngridView.EndDataUpdate();
                yarngridView.FocusedColumn = yarngridView.GetVisibleColumn(colColorName1.VisibleIndex + 1);
            }
        }
        private void OpenShadeYarnLookup(int _selvalue, JobCardTransDto er)
        {
            var frm = new ColorLkpWindow();
            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Color;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                yarngridView.BeginDataUpdate();
                er.ItemId = frm.SelectedValue;
                er.ItemType = frm.SelectedTex;

                yarngridView.EndDataUpdate();
                yarngridView.FocusedColumn = yarngridView.GetVisibleColumn(colItemType1.VisibleIndex + 1);
            }
        }
        private void OpenPendingJobCard()
        {

            var stf = new PendingOrderJobCardView();
            KontoContext db = new KontoContext();
            var list = new List<JobCardDto>();
            var spcol = db.SpCollections.FirstOrDefault(k => k.Id == (int)SpCollectionEnum.PendingJobCard);
            int VTypeId = (int)VoucherTypeEnum.SalesOrder;
            if (spcol == null)
            {
                list = db.Database.SqlQuery<JobCardDto>(
                   "dbo.PendingJobCard @CompanyId={0},@VoucherTypeID={1}",
        KontoGlobals.CompanyId, VTypeId).ToList();
            }
            else
            {
                list = db.Database.SqlQuery<JobCardDto>(
                 spcol.Name + " @CompanyId={0},@VoucherTypeID={1}",
                 KontoGlobals.CompanyId, VTypeId).ToList();
            }

            if (list != null)
            {
                if (list.Count > 0)
                {
                    stf.ItemList = list;

                    if (stf.ShowDialog() != DialogResult.OK) return;

                    Int32[] selectedRowHandles = stf.SelectedRows;
                    if (selectedRowHandles == null || selectedRowHandles.Count() == 0) return;

                    List<JobCardTransDto> Trans = new List<JobCardTransDto>();
                    List<JobCardTransDto> ItemTrans = new List<JobCardTransDto>();
                    int id = 0;
                    foreach (var item in selectedRowHandles)
                    {
                        var ord = stf.gridView1.GetRow(item) as JobCardDto;

                        JobCardModel j = new JobCardModel();

                        OrderNoTextEdit.Text = ord.VoucherNo;
                        OrderDateEdit.DateTime = ord.VouchDate;
                        accLookup1.SelectedValue = ord.PartyId;
                        accLookup1.SetAcc((int)ord.PartyId);
                        ProductLookup.SelectedValue = ord.ProductId;
                        ProductLookup.SetGroup((int)ord.ProductId);
                        OrderId = ord.Id;

                        JobCardTransDto jc = new JobCardTransDto();
                        jc.RefId = ord.TransId;
                        jc.ItemId = ord.ProductId;
                        jc.ItemType = ord.ProductName;
                        jc.ColorCategory = ord.ColorName;
                        jc.DesignName = ord.Design;
                        jc.DesignId = ord.DesignId;
                        jc.Ply = ord.ColorId;
                        jc.ColorPer = ord.TotalQty;
                        jc.Rate = ord.PendingQty;
                        jc.ConsumeQty = ord.PendingQty;
                        jc.Remark = ord.Remark;
                        jc.Id = -1 * id;
                        id = id + 1;
                        Trans.Add(jc);

                        if(ord.ColorId==null)
                        {
                            ord.ColorId = 1;
                        }
                        var yarnlist = db.WeftItems.Where(k => k.IsDeleted == false &&
                                    k.ProductId == ord.ProductId && k.MColorId == ord.ColorId
                                    && k.TypeId > 100).ToList();

                        int yId = 0;
                        foreach (var y in yarnlist)
                        {
                            JobCardTransDto yr = new JobCardTransDto();
                            yr.Id = -1 * yId;
                            yId += 1;
                            yr.RefId = 0;
                            yr.LotNo = y.IType;
                            yr.Remark = ord.Remark;
                            yr.GMeter = ord.TotalQty > 0 ? (decimal)ord.TotalQty : 0;
                            yr.Meter = ord.PendingQty > 0 ? (decimal)ord.PendingQty : 0;
                            yr.DesignId = y.ProductId;
                            yr.DesignName = db.Products.FirstOrDefault(k => k.Id == y.ProductId).ProductName;
                            yr.ItemId = y.MColorId;
                            yr.ItemType = db.ColorModels.FirstOrDefault(k => k.Id == y.MColorId).ColorName;
                            yr.Ply = y.ColorId;
                            yr.ColorCategory = db.ColorModels.FirstOrDefault(k => k.Id == y.ColorId).ColorName;
                            yr.ColorPer = y.Denier;
                            yr.ConsumeQty = y.PI;
                            yr.Rate = y.RS;

                            yr.Amount = ((((y.Denier * y.PI * y.RS) / 9000) + (((y.Denier * y.PI * y.RS) / 9000) * 10 / 100)) * ord.PendingQty) / 1000;

                            if (y.Totcard != 0 && y.TotPick != 0)
                            {
                                yr.Amount = y.Qty;
                            }
                            //      yr.Amount = y.Qty;
                            ItemTrans.Add(yr);
                        }
                    }
                    JobTransbindingSource.DataSource = Trans;
                    YarnbindingSource.DataSource = ItemTrans;
                }
            }
            divLookUpEdit.Focus();
        }

        #endregion
        #region Yarn
        private void YarngridControl_Enter(object sender, EventArgs e)
        {
            yarngridView.FocusedColumn = yarngridView.VisibleColumns[0];
        }
        private void YarngridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.Appearance.ForeColor = Color.FromArgb(227, 22, 91);
            if (e.RowHandle < 0)
                e.Info.DisplayText = "";
            else
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        private void YarngridView_DoubleClick(object sender, EventArgs e)
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
        private void YarngridView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var itm = yarngridView.GetFocusedRow() as JobCardTransDto;
            if (itm == null) return;
            if (!"VoucherNo,Extra1".Contains(yarngridView.FocusedColumn.FieldName)) return;
        }
        private void YarngridView_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }
        private void YarngridView_MouseUp(object sender, MouseEventArgs e)
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
        private void YarngridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete) return;

            if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
            {
                if (MessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo) !=
                  DialogResult.Yes)
                    return;
                GridView view = sender as GridView;
                var row = view.GetRow(view.FocusedRowHandle) as JobCardTransDto;
                view.DeleteRow(view.FocusedRowHandle);
                DelYarnDetail.Add(row);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                GridView view = sender as GridView;
                if (yarngridView.FocusedColumn.FieldName == "ColorName")
                {
                    view.SetRowCellValue(view.FocusedRowHandle, colColorName1, string.Empty);
                    view.SetRowCellValue(view.FocusedRowHandle, colColorId1, 0);
                }
            }
        }
        private void YarngridView_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            var rw = yarngridView.GetRow(e.RowHandle) as JobCardTransDto;
            rw.Id = -1 * yarngridView.RowCount;
        }
        private void YarngridControl_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(accLookup1.SelectedValue) == 0) return;
                var dr = PreOpenYarnLookup();
                if (dr == null) return;
                if (yarngridView.FocusedColumn.FieldName == "DesignName")
                {

                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.DesignId == null)
                        {
                            OpenItemYarnLookup(0, dr);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenItemYarnLookup((int)dr.DesignId, dr);
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        if (dr.DesignId == null)
                        {
                            OpenItemYarnLookup(0, dr);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenItemYarnLookup((int)dr.DesignId, dr);
                        }
                        e.Handled = true;
                    }
                }
                else if (yarngridView.FocusedColumn.FieldName == "ColorCategory")
                {
                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.ColorId == null)
                        {
                            OpenColorYarnLookup(0, dr);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenColorYarnLookup((int)dr.ColorId, dr);
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        if (dr.ColorId == null)
                        {
                            OpenColorYarnLookup(0, dr);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenColorYarnLookup((int)dr.ColorId, dr);
                        }
                        e.Handled = true;
                    }
                }
                else if (yarngridView.FocusedColumn.FieldName == "ItemType")
                {
                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.ItemId == null)
                        {
                            OpenShadeYarnLookup(0, dr);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenShadeYarnLookup((int)dr.ItemId, dr);
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        if (dr.ItemId == null)
                        {
                            OpenShadeYarnLookup(0, dr);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenShadeYarnLookup((int)dr.ItemId, dr);
                        }
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "JobCard GridControl KeyDown");
                MessageBoxAdv.Show(this, "Error Lookup Setup !!", "Exception ", ex.ToString());
            }
        }

        #endregion
        #region OrderGridView
        private void ProdgridView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var itm = OrderDetailgridView.GetFocusedRow() as JobCardTransDto;
            if (itm == null) return;
            if (!"VoucherNo,Extra1".Contains(OrderDetailgridView.FocusedColumn.FieldName)) return;
        }
        private void ProdgridView_MouseUp(object sender, MouseEventArgs e)
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
        private void ProdgridView_DoubleClick(object sender, EventArgs e)
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
        private void ProdgridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.Appearance.ForeColor = Color.FromArgb(227, 22, 91);
            if (e.RowHandle < 0)
                e.Info.DisplayText = "";
            else
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        private void ProdGridControl_Enter(object sender, EventArgs e)
        {
            OrderDetailgridView.FocusedColumn = OrderDetailgridView.VisibleColumns[0];
        }
        private void ProdgridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete) return;

            if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
            {
                if (MessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo) !=
                  DialogResult.Yes)
                    return;
                GridView view = sender as GridView;
                var row = view.GetRow(view.FocusedRowHandle) as JobCardTransDto;
                view.DeleteRow(view.FocusedRowHandle);
                DelOrder.Add(row);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                GridView view = sender as GridView;
                if (OrderDetailgridView.FocusedColumn.FieldName == "ColorName")
                {
                    view.SetRowCellValue(view.FocusedRowHandle, colColorName, string.Empty);
                    view.SetRowCellValue(view.FocusedRowHandle, colColorId, 0);
                }
            }
        }
        private void ProdgridView_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var rw = OrderDetailgridView.GetRow(e.RowHandle) as JobCardTransDto;
            rw.Id = -1 * OrderDetailgridView.RowCount;
        }
        private void ProdgridView_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }
        private void OrderDetailgridControl_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(accLookup1.SelectedValue) == 0) return;
                var dr = PreOpenLookup();
                if (dr == null) return;
                if (OrderDetailgridView.FocusedColumn.FieldName == "ItemType")
                {

                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.DesignId == null)
                        {
                            OpenItemLookup(0, dr);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenItemLookup((int)dr.DesignId, dr);
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        if (dr.DesignId == null)
                        {
                            OpenItemLookup(0, dr);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenItemLookup((int)dr.DesignId, dr);
                        }
                        e.Handled = true;
                    }
                }
                else if (OrderDetailgridView.FocusedColumn.FieldName == "ColorCategory")
                {
                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.ColorId == null)
                        {
                            OpenColorLookup(0, dr);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenColorLookup((int)dr.ColorId, dr);
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        if (dr.ColorId == null)
                        {
                            OpenColorLookup(0, dr);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenColorLookup((int)dr.ColorId, dr);
                        }
                        e.Handled = true;
                    }
                }
                else if (OrderDetailgridView.FocusedColumn.FieldName == "DesignName")
                {
                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.DesignId == null)
                        {
                            OpenDesignLookup(0, dr);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenDesignLookup((int)dr.DesignId, dr);
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        if (dr.DesignId == null)
                        {
                            OpenDesignLookup(0, dr);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenDesignLookup((int)dr.DesignId, dr);
                        }
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "JobCard Order Detail GridControl KeyDown");
                MessageBoxAdv.Show(this, "Error Lookup Setup !!", "Exception ", ex.ToString());
            }
        }

        #endregion

        #region Event
        private void JobCardIndex_Load(object sender, EventArgs e)
        {

        }
        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                voucherLookup.Focus();
                return;
            }
            else if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new JobCardList();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "JobCard [View]";
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
                Log.Error(ex, "JobCard Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }
        private void ProductRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
                OpenItemLookup((int)dr.ItemId, dr);
        }
        private void ColorRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
            {
                if (dr.ColorId != null && dr.ColorId > 0)
                    OpenColorLookup((int)dr.ColorId, dr);
                else
                    OpenColorLookup(0, dr);
            }
        }
        private void ColorrepositoryItemButtonEdit2_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var dr = PreOpenYarnLookup();
            if (dr != null)
            {
                if (dr.ColorId != null && dr.ColorId > 0)
                    OpenColorYarnLookup((int)dr.ColorId, dr);
                else
                    OpenColorYarnLookup(0, dr);
            }
        }
        private void ProductrepositoryItemButtonEdit2_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var dr = PreOpenYarnLookup();
            if (dr != null)
            {
                if (dr.DesignId != null)
                    OpenItemYarnLookup((int)dr.DesignId, dr);
                else
                    OpenItemYarnLookup(0, dr);
            }
        }

        private void ShaderepositoryItemButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var dr = PreOpenYarnLookup();
            if (dr != null)
            {
                if (dr.ItemId != null)
                    OpenShadeYarnLookup((int)dr.ItemId, dr);
                else
                    OpenShadeYarnLookup(0, dr);
            }
        }

        private void SelectSimpleButton_Click(object sender, EventArgs e)
        {
            var stf = new PendingBeamLoadingView();
            KontoContext _db = new KontoContext();

            var _list = new List<BeamProdDto>();

            var spcol = _db.SpCollections.FirstOrDefault(k => k.Id ==
                        (int)SpCollectionEnum.OutwardBeamProd);
            if (spcol == null)
            {
                _list = (_db.Database.SqlQuery<BeamProdDto>(
                            "dbo.OutwardBeamProd @CompanyId={0} ,@ProductId={1},@IsOk={2}",
                            KontoGlobals.CompanyId, Convert.ToInt32(ProductLookup.SelectedValue), 1).ToList());
            }
            else
            {
                _list = (_db.Database.SqlQuery<BeamProdDto>(
                    spcol.Name + " @CompanyId={0} ,@ProductId={1},@IsOk={2}",
                            KontoGlobals.CompanyId, Convert.ToInt32(ProductLookup.SelectedValue), 1).ToList());
            }

            if (_list.Count == 0) return;

            stf.ItemList = _list;

            var showDialog = stf.ShowDialog();

            if (stf.SelectedRow != null)
            {
                BeamNoTextEdit.Text = stf.SelectedRow.VoucherNo;
                //Model.MachineId = dc.TransModel.Id;
                MachineId = stf.SelectedRow.Id;
            }
            else
            {
                MessageBox.Show("Please Select Taka!!!!");
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

                rpt.Load(new FileInfo("reg\\JobCardPrint.rdlx"));

                rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);
                doc.Parameters["id"].CurrentValue = this.PrimaryKey;

                KontoContext db = new KontoContext();
                var ItemTrans = db.jobCardTrans.Where(k => k.JobCardId == this.PrimaryKey).ToList();
                var shadelst = ItemTrans.GroupBy(k => k.ItemId).ToList();
                int i = 0;
                foreach (var item in shadelst)
                {
                    i = i + 1;
                    doc.Parameters["ShadeId" + i].CurrentValue = item.Key;
                }

                //rpt.ResourceLocator = new MySubreportLocator();


                var frm = new KontoRepViewer(doc);
                frm.Text = "Job Card Print";
                var _tab = this.Parent.Parent as TabControlAdv;
                if (_tab == null) return;
                var pg1 = new TabPageAdv();
                pg1.Text = "Job Card Print";
                _tab.TabPages.Add(pg1);
                _tab.SelectedTab = pg1;
                frm.TopLevel = false;
                frm.Parent = pg1;
                frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                frm.Show();// = true;

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Job Card print");
                MessageBoxAdv.Show(this, "Error While Print !!", "Exception ", ex.ToString());

            }
        }
        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<JobCardModel>();
            this.Text = "JobCard [Add New]";

            this.ActiveControl = voucherLookup.buttonEdit1;
            voucherLookup.SetDefault();
            voucherDateEdit.EditValue = DateTime.Now;
            VoucherNotextEdit.Text = "New";

            accLookup1.SetEmpty();
            ProductLookup.SetEmpty();
            empLookup1.SetEmpty();
            OrderNoTextEdit.Text = string.Empty;
            BeamNoTextEdit.Text = string.Empty;

            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;

            MachineId = 0;
            OrderId = 0;
            JobTransbindingSource.DataSource = new List<JobCardTransDto>();
            YarnbindingSource.DataSource = new List<JobCardTransDto>();

            DelOrder = new List<JobCardTransDto>();
            DelYarnDetail = new List<JobCardTransDto>();

            this.SelectSimpleButton.Enabled = true;

            OpenPendingJobCard();

            divLookUpEdit.Focus();
        }

        public override void ResetPage()
        {
            base.ResetPage();
            VoucherNotextEdit.Text = "New";

            voucherLookup.SetDefault();
            voucherDateEdit.EditValue = DateTime.Now;

            divLookUpEdit.EditValue = null;
            OrderNoTextEdit.Text = string.Empty;
            OrderDateEdit.DateTime = DateTime.Now;
            accLookup1.SetEmpty();
            ProductLookup.SetEmpty();
            empLookup1.SetEmpty();
            BeamNoTextEdit.Text = string.Empty;

            OrderId = 0;
            JobTransbindingSource.DataSource = new List<JobCardTransDto>();
            YarnbindingSource.DataSource = new List<JobCardTransDto>();

            DelOrder = new List<JobCardTransDto>();
            DelYarnDetail = new List<JobCardTransDto>();

            this.SelectSimpleButton.Enabled = true;

            divLookUpEdit.Focus();
        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var pdata = db.jobCards.Find(_key);

                LoadData(pdata);
                createdLabelControl.Text = "Create By: " + pdata.CreateUser;
            }

            this.SelectSimpleButton.Enabled = false;
            this.Text = "JobCard [Edit New]";

            modifyLabelControl.Text = "Modified By: " + KontoGlobals.UserName;
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
             
            if (Convert.ToInt32(voucherLookup.SelectedValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "VoucherId", Operation = Op.Equals, Value = Convert.ToInt32(voucherLookup.SelectedValue) });
            }

            if (Convert.ToInt32(ProductLookup.SelectedValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "ProductId", Operation = Op.Equals, Value = Convert.ToInt32(ProductLookup.SelectedValue) });
            }

            filter.Add(new Filter { PropertyName = "CompanyId", Operation = Op.Equals, Value = KontoGlobals.CompanyId }); 
            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });

            using (var db = new KontoContext())
            {
                FilterView = db.jobCards.Where(ExpressionBuilder.GetExpression<JobCardModel>(filter))
                    .OrderBy(x => x.Id).ToList();

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

            JobCardModel _find = new JobCardModel();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<JobCardTransDto, JobCardTransModel>().ForMember(x => x.Id, p => p.Ignore());
            });

            var Trans = JobTransbindingSource.DataSource as List<JobCardTransDto>;
            var ItemTrans = YarnbindingSource.DataSource as List<JobCardTransDto>;

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
                            _find = db.jobCards.Find(this.PrimaryKey);
                            createuser = _find.CreateUser;
                            createdate = Convert.ToDateTime(_find.CreateDate);
                        }

                        var totlQty = Trans.Sum(k => k.ConsumeQty);
                        if (totlQty != 0)
                            _find.Qty = Convert.ToDecimal(totlQty);

                        _find.DivId = Convert.ToInt32(divLookUpEdit.EditValue);
                        _find.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
                        _find.CompanyId = KontoGlobals.CompanyId;

                        _find.VoucherId = Convert.ToInt32(voucherLookup.SelectedValue);
                        _find.ProductId = Convert.ToInt32(ProductLookup.SelectedValue);

                        _find.ProgramNo = OrderNoTextEdit.Text;
                        _find.OrdDate = OrderDateEdit.DateTime;
                        _find.AccountId = Convert.ToInt32(accLookup1.SelectedValue);
                        _find.ProductId = Convert.ToInt32(ProductLookup.SelectedValue);

                        if (!string.IsNullOrEmpty(empLookup1.SelectedText))
                            _find.BatchId = Convert.ToInt32(empLookup1.SelectedValue);

                        _find.CarrierNo = BeamNoTextEdit.Text;
                        _find.OrderId = OrderId;
                        _find.MachineId = MachineId;
                        if (this.PrimaryKey == 0)
                        {
                            var _srno = DbUtils.NextSerialNo((int)_find.VoucherId, db, 0);
                            _find.VoucherNo = _srno;

                            db.jobCards.Add(_find);
                            db.SaveChanges();
                        }
                        else
                        {
                            _find.CreateDate = createdate;
                            _find.CreateUser = createuser;
                        }

                        //Order Detail 
                        var map = new Mapper(config);
                        JobCardTransModel tranModel;
                        foreach (var item in Trans)
                        {
                            var transid = item.Id;
                            item.JobCardId = _find.Id;
                            tranModel = new JobCardTransModel();
                            if (item.Id > 0)
                            {
                                tranModel = db.jobCardTrans.Find(item.Id);
                            }
                            map = new Mapper(config);
                            map.Map(item, tranModel);

                            if (item.Id <= 0)
                            {
                                db.jobCardTrans.Add(tranModel);
                                db.SaveChanges();
                            }
                        }

                        foreach (var item in ItemTrans)
                        {
                            var transid = item.Id;
                            item.JobCardId = _find.Id;
                            tranModel = new JobCardTransModel();
                            if (item.Id > 0)
                            {
                                tranModel = db.jobCardTrans.Find(item.Id);
                            }
                            map = new Mapper(config);
                            map.Map(item, tranModel);

                            if (item.Id <= 0)
                            {
                                tranModel.RefId = 0;
                                db.jobCardTrans.Add(tranModel);
                                db.SaveChanges();
                            }
                        }

                        //DELETED ENTRY FROM DATABASE order

                        foreach (var p in DelOrder)
                        {
                            var pro = db.jobCardTrans.FirstOrDefault(k => k.Id == p.Id);
                            if (pro != null)
                                pro.IsDeleted = true;
                        }
                        //DELETED ENTRY FROM DATABASE Yarn
                        foreach (var p in DelYarnDetail)
                        {
                            var pro = db.jobCardTrans.FirstOrDefault(k => k.Id == p.Id);
                            if (pro != null)
                                pro.IsDeleted = true;
                        }

                        db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "JobCard Save");
                        MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
                    }
                }
            }

            if (IsSaved)
            {
                // NewRec();

                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage + " Voucher No.: " + _find.VoucherNo, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (!this.OpenForLookup && newmode)
                {
                    if (this.voucherLookup.GroupDto.PrintAfterSave && MessageBox.Show("Print JobCard ?", "Print", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        this.PrimaryKey = _find.Id;
                        Print();
                        this.PrimaryKey = 0;
                    }

                    base.SaveDataAsync(newmode);
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

        private bool ValidateData()
        {
            var dt = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
            var orddt = Convert.ToInt32(OrderDateEdit.DateTime.ToString("yyyyMMdd"));
            var trans = JobTransbindingSource.DataSource as List<JobCardTransDto>;
            var yarns = YarnbindingSource.DataSource as List<JobCardTransDto>;

            if (Convert.ToInt32(divLookUpEdit.EditValue) <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Division", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                divLookUpEdit.Focus();
                return false;
            }
           else if (Convert.ToInt32(voucherLookup.SelectedValue) <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Voucher", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherLookup.Focus();
                return false;
            }
            else if (dt > KontoGlobals.ToDate || dt < KontoGlobals.FromDate)
            {
                MessageBoxAdv.Show(this, "Date out of financial range", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherDateEdit.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(OrderNoTextEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Order No", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                OrderNoTextEdit.Focus();
                return false;
            }
            else if(string.IsNullOrEmpty( OrderDateEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Order Date", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                OrderDateEdit.Focus();
                return false;
            }
            else if (orddt > KontoGlobals.ToDate ||  orddt < KontoGlobals.FromDate)
            {
                MessageBoxAdv.Show(this, "Order Date out of financial range", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                OrderDateEdit.Focus();
                return false;
            }
            else if (orddt > dt)
            {
                MessageBoxAdv.Show(this, "Order Date cannot grater than JobCard Date", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                OrderDateEdit.Focus();
                return false;
            }
            else if (Convert.ToInt32(ProductLookup.SelectedValue) <= 0 && this.PrimaryKey <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Product", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ProductLookup.Focus();
                return false;
            }
            else if (Convert.ToInt32(accLookup1.SelectedValue) <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Party", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                accLookup1.Focus();
                return false;
            }
            else if (trans.Count <= 0)
            {
                MessageBoxAdv.Show(this, "Atleast one transaction must be entered!", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                OrderDetailgridView.Focus();
                return false;
            }
            else if (yarns.Count <= 0)
            {
                MessageBoxAdv.Show(this, "Atleast one YARN transaction must be entered!", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                yarngridView.Focus();
                return false;
            }

            return true;
        }

        private void LoadData(JobCardModel pdata)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<JobCardTransModel, JobCardTransDto>();
            });

            divLookUpEdit.EditValue = pdata.DivId;
            voucherDateEdit.DateTime = KontoUtils.IToD(pdata.VoucherDate);
            voucherLookup.SelectedValue = pdata.VoucherId;
            OrderNoTextEdit.Text = pdata.ProgramNo;
            VoucherNotextEdit.Text = pdata.VoucherNo;
            if (pdata.ProductId > 0)
            {
                ProductLookup.SelectedValue = pdata.ProductId;
                ProductLookup.SetGroup((int)pdata.ProductId);
            }

            if (pdata.OrdDate != null)
                OrderDateEdit.DateTime = (DateTime)pdata.OrdDate;

            accLookup1.SelectedValue = pdata.AccountId;
            accLookup1.SetAcc((int)pdata.AccountId);

            empLookup1.SelectedValue = pdata.BatchId;
            empLookup1.SetGroup();

            BeamNoTextEdit.Text = pdata.CarrierNo;

            if (pdata.OrderId != null)
                OrderId = (int)pdata.OrderId;

            if (pdata.MachineId != null)
                MachineId = (int)pdata.MachineId;

            KontoContext db = new KontoContext();
            //var trans1 = db.jobCardTrans.Where(k => k.JobCardId == pdata.Id && k.RefId > 0
            //            && k.IsDeleted == false).ProjectToList<JobCardTransDto>(config);

            var trans = (from jc in db.jobCardTrans
                         join pd in db.Products on jc.ItemId equals pd.Id into join_pd
                         from pd in join_pd.DefaultIfEmpty()
                             //join ot in db.OrdTranses on jc.RefId equals ot.Id into join_ot
                             //from ot in join_ot.DefaultIfEmpty()
                         join o in db.Ords on jc.RefId equals o.Id into join_o
                         from o in join_o.DefaultIfEmpty()
                         join cl in db.ColorModels on jc.ColorId equals cl.Id into join_cl
                         from cl in join_cl.DefaultIfEmpty()
                         join dm in db.Products on jc.DesignId equals dm.Id into join_dm
                         from dm in join_dm.DefaultIfEmpty()
                         orderby jc.Id
                         where jc.IsActive == true && jc.JobCardId == pdata.Id && jc.RefId > 0
                       && jc.IsDeleted == false
                         select new JobCardTransDto()
                         {
                             Id = jc.Id,
                             JobCardId = jc.JobCardId,
                             ColorId = jc.ColorId.HasValue ? (int)jc.ColorId : 1,
                             ColorCategory = cl.ColorName,
                             DesignId = jc.DesignId.HasValue ? (int)jc.DesignId : 1,
                             DesignName = dm.ProductCode,
                             LotNo = jc.LotNo,
                             Rate = jc.Rate,
                             RefId = jc.RefId,
                             Remark = jc.Remark,
                             Amount = jc.Amount,
                             ColorPer = jc.ColorPer,
                             ConsumeQty = jc.ConsumeQty,
                             Description = jc.Description,
                             GMeter = jc.GMeter,
                             ItemId = jc.ItemId,
                             ItemType = pd.ProductName,
                             Meter = jc.Meter,
                             Per = jc.Per,
                             Ply = jc.Ply
                         }).ToList();

            //var itrans1 = db.jobCardTrans.Where(k => k.JobCardId == pdata.Id && k.RefId < 1
            //            && k.IsDeleted == false).ProjectToList<JobCardTransDto>(config);

            var itrans = (from jc in db.jobCardTrans
                          join pd in db.Products on jc.DesignId equals pd.Id into join_pd
                          from pd in join_pd.DefaultIfEmpty()
                          join mcl in db.ColorModels on jc.ItemId equals mcl.Id into join_MColor
                          from mcl in join_MColor.DefaultIfEmpty()
                          join cl in db.ColorModels on jc.ColorId equals cl.Id into join_cl
                          from cl in join_cl.DefaultIfEmpty()
                          orderby jc.Id
                          where jc.JobCardId == pdata.Id && jc.RefId < 1
                        && jc.IsDeleted == false
                          select new JobCardTransDto()
                          {
                              Id = jc.Id,
                              JobCardId = jc.JobCardId,
                              ColorId = jc.ColorId.HasValue ? (int)jc.ColorId : 1,
                              ColorCategory = cl.ColorName,
                              DesignId = jc.DesignId.HasValue ? (int)jc.DesignId : 1,
                              DesignName = pd.ProductName,
                              LotNo = jc.LotNo,
                              Rate = jc.Rate,
                              RefId = jc.RefId,
                              Remark = jc.Remark,
                              Amount = jc.Amount,
                              ColorPer = jc.ColorPer,
                              ConsumeQty = jc.ConsumeQty,
                              Description = jc.Description,
                              GMeter = jc.GMeter,
                              ItemId = jc.ItemId,
                              ItemType = mcl.ColorName,
                              Meter = jc.Meter,
                              Per = jc.Per,
                              Ply = jc.Ply
                          }).ToList();

            JobTransbindingSource.DataSource = trans;
            YarnbindingSource.DataSource = itrans;

            DelYarnDetail = new List<JobCardTransDto>();
            DelOrder = new List<JobCardTransDto>();
            divLookUpEdit.Focus();
        }

        #endregion
    }
}