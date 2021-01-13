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
using Konto.Shared.Masters.Emp;
using Konto.Shared.Masters.Grade;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Weaves.TakaConv
{
    public partial class TakaConvIndex : KontoMetroForm
    {
        private List<GrnDto> FilterView = new List<GrnDto>();
        private List<BeamProdDto> DelBeamProd = new List<BeamProdDto>();
        private List<GrnTransDto> DelCT = new List<GrnTransDto>();

        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        public TakaConvIndex()
        {
            InitializeComponent();

            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            this.Load += TakaConvIndex_Load;

            this.MainLayoutFile = KontoFileLayout.TakaConv_Index;
            this.GridLayoutFile = KontoFileLayout.TakaConv_Trans;

            CTgridControl.ProcessGridKey += CTgridControl_ProcessGridKey;
            CTgridView.InitNewRow += CTgridView_InitNewRow;
            CTgridView.CellValueChanged += CTgridView_CellValueChanged;
            CTgridView.KeyDown += CTgridView_KeyDown;
            CTgridControl.Enter += CTgridControl_Enter;
            //  gridView1.ValidateRow += GridView1_ValidateRow;
            CTgridView.MouseUp += CTgridView_MouseUp;
            CTgridView.InvalidRowException += CTgridView_InvalidRowException;
            CTgridView.ShowingEditor += CTgridView_ShowingEditor;
            CTgridView.DoubleClick += CTgridView_DoubleClick;
            CTgridView.CustomDrawRowIndicator += CTgridView_CustomDrawRowIndicator;

            ProdgridView.InitNewRow += ProdgridView_InitNewRow;
            ProdgridView.KeyDown += ProdgridView_KeyDown;
            prodgridControl.Enter += ProdGridControl_Enter;
            //  gridView1.ValidateRow += GridView1_ValidateRow;
            ProdgridView.MouseUp += ProdgridView_MouseUp;
            ProdgridView.InvalidRowException += ProdgridView_InvalidRowException;
            ProdgridView.ShowingEditor += ProdgridView_ShowingEditor;
            ProdgridView.DoubleClick += ProdgridView_DoubleClick;
            ProdgridView.CustomDrawRowIndicator += ProdgridView_CustomDrawRowIndicator;

            productRepositoryItemButtonEdit.ButtonClick += ProductRepositoryItemButtonEdit_ButtonClick;
            colorRepositoryItemButtonEdit.ButtonClick += ColorRepositoryItemButtonEdit_ButtonClick;
            designRepositoryItemButtonEdit.ButtonClick += DesignRepositoryItemButtonEdit_ButtonClick;

            SelectSimpleButton.Click += SelectSimpleButton_Click;
        }


        #region UDF

        private GrnTransDto PreOpenLookup()
        {
            CTgridView.GetRow(CTgridView.FocusedRowHandle);
            if (CTgridView.GetRow(CTgridView.FocusedRowHandle) == null)
            {
                CTgridView.AddNewRow();
            }
            var dr = (GrnTransDto)CTgridView.GetRow(CTgridView.FocusedRowHandle);
            return dr;
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

                ProdgridView.FocusedColumn = ProdgridView.GetNearestCanFocusedColumn(ProdgridView.FocusedColumn);
            }
        }
        private void OpenColorLookup(int _selvalue, GrnTransDto er)
        {
            var frm = new ColorLkpWindow();
            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Color;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                CTgridView.BeginDataUpdate();
                er.ColorId = frm.SelectedValue;
                er.ColorName = frm.SelectedTex;

                CTgridView.EndDataUpdate();
                CTgridView.FocusedColumn = CTgridView.GetVisibleColumn(ColColorName.VisibleIndex + 1);
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
                CTgridView.BeginDataUpdate();
                er.GradeId = frm.SelectedValue;
                er.GradeName = frm.SelectedTex;
                CTgridView.EndDataUpdate();
                CTgridView.FocusedColumn = CTgridView.GetVisibleColumn(colGradeName.VisibleIndex + 1);
            }

        }
        private void OpenDesignLookup(int _selvalue, GrnTransDto er)
        {
            var frm = new DesignLkpWindow();
            frm.SelectedValue = _selvalue;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                CTgridView.BeginDataUpdate();
                er.DesignId = frm.SelectedValue;
                er.DesignNo = frm.SelectedTex;
                CTgridView.EndDataUpdate();
                CTgridView.FocusedColumn = CTgridView.GetVisibleColumn(ColDesign.VisibleIndex + 1);
            }

        }
         
        #endregion
        #region ProdGridView
        private void ProdgridView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var itm = ProdgridView.GetFocusedRow() as BeamProdDto;
            if (itm == null) return;
            if (!"VoucherNo,Extra1".Contains(ProdgridView.FocusedColumn.FieldName)) return;

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
            ProdgridView.FocusedColumn = ProdgridView.VisibleColumns[0];
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
                var row = view.GetRow(view.FocusedRowHandle) as BeamProdDto;
                view.DeleteRow(view.FocusedRowHandle);
                DelBeamProd.Add(row);
            }
        }
        private void ProdgridView_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var rw = ProdgridView.GetRow(e.RowHandle) as BeamProdDto;
            rw.Id = -1 * ProdgridView.RowCount;
        }
        private void ProdgridView_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        #endregion

        #region CTgridView
        private void CTgridView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var itm = CTgridView.GetFocusedRow() as ProdWeftItemDTO;
            if (itm == null) return;
            if (!"Qty,ProductName".Contains(CTgridView.FocusedColumn.FieldName)) return;
        }
        private void CTgridView_MouseUp(object sender, MouseEventArgs e)
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
        private void CTgridView_DoubleClick(object sender, EventArgs e)
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
        private void CTgridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.Appearance.ForeColor = Color.FromArgb(227, 22, 91);
            if (e.RowHandle < 0)
                e.Info.DisplayText = "";
            else
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        private void CTgridControl_Enter(object sender, EventArgs e)
        {
            CTgridView.FocusedColumn = CTgridView.VisibleColumns[0];
        }
        private void CTgridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == null) return;
            if (!(CTgridView.GetRow(e.RowHandle) is ProdWeftItemDTO er)) return;

        }
        private void CTgridView_KeyDown(object sender, KeyEventArgs e)
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
                DelCT.Add(row);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                GridView view = sender as GridView;
                if (CTgridView.FocusedColumn.FieldName == "ColorName")
                {
                    view.SetRowCellValue(view.FocusedRowHandle, ColColorName, string.Empty);
                    view.SetRowCellValue(view.FocusedRowHandle, ColColorId, 0);
                }
                if (CTgridView.FocusedColumn.FieldName == "DesignNo")
                {
                    view.SetRowCellValue(view.FocusedRowHandle, ColDesign, string.Empty);
                    view.SetRowCellValue(view.FocusedRowHandle, ColDesignId, 0);
                }
            }
        }
        private void CTgridView_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var rw = CTgridView.GetRow(e.RowHandle) as GrnTransDto;
            rw.Id = -1 * CTgridView.RowCount;
        }
        private void CTgridControl_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                var dr = PreOpenLookup();
                if (dr == null) return;
                if (CTgridView.FocusedColumn.FieldName == "ProductName")
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        if (dr.ProductId == 0 || dr.ProductId == null)
                        {
                            OpenItemLookup(0, dr);
                            // e.Handled = true;
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        OpenItemLookup((int)dr.ProductId, dr);
                        e.Handled = true;
                    }
                }
                else if (CTgridView.FocusedColumn.FieldName == "ColorName")
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
                else if (CTgridView.FocusedColumn.FieldName == "GradeName")
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
                else if (CTgridView.FocusedColumn.FieldName == "DesignNo")
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
                Log.Error(ex, "Taka Prod GridControl KeyDown");
                MessageBoxAdv.Show(this, "Error Lookup Setup !!", "Exception ", ex.ToString());
            }
        }
        private void CTgridView_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        #endregion
        #region Event

        private void TakaConvIndex_Load(object sender, EventArgs e)
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
                var _ListView = new TakaConvList();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Taka Convertion [View]";
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
                Log.Error(ex, "Taka Conversion Invoice Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
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

        private void SelectSimpleButton_Click(object sender, EventArgs e)
        {
            if (ProductLookup.SelectedValue == null) return;
            var proddto = new List<BeamProdDto>();
            var frm = new PendingStockView();
            frm.ItemId = (int)ProductLookup.SelectedValue;
            frm.StockType = "Stock";
            frm.ProductType = ProductTypeEnum.GREY;
            frm.ShowDialog();

            var selpd = frm.list.Where(x => x.IsSelected).ToList();

            foreach (var _taka in selpd)
            {
                var ptrans = new BeamProdDto();
                ptrans.RefId = 0;
                ptrans.TransId = 0;
                ptrans.ProductId = _taka.ProductId;
                ptrans.ColorId = _taka.ColorId;
                ptrans.GradeId = _taka.GradeId;
                ptrans.Id = _taka.Id;
                ptrans.SrNo = _taka.SrNo;
                ptrans.ProdOutId = 0;
                ptrans.NetWt = Convert.ToDecimal(_taka.Qty);
                ptrans.Qty = Convert.ToDecimal(_taka.Qty);
                ptrans.GrossWt = Convert.ToDecimal(_taka.GrossWt);
                ptrans.Cops = Convert.ToInt32(_taka.Cops);
                ptrans.Ply = Convert.ToInt32(_taka.Ply);
                ptrans.Tops = Convert.ToInt32(_taka.Tops);
                ptrans.TareWt = Convert.ToDecimal(_taka.TareWt);
                ptrans.VoucherNo = _taka.VoucherNo;
                ptrans.Weaver = _taka.Weaver;
                ptrans.ChallanNo = _taka.InwardNo;
                ptrans.VoucherDate = Convert.ToInt32(_taka.VoucherDate);

                proddto.Add(ptrans);
            }
            this.ProdbindingSource.DataSource = proddto;
        }
        #endregion
        #region Parent Function

        public override void Print()
        {
            base.Print();
        }
        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<GrnDto>();
            this.Text = "Taka Conversion [Add New]";

            this.ActiveControl = voucherLookup.buttonEdit1;
            voucherLookup.SetDefault();
            voucherDateEdit.EditValue = DateTime.Now;

            ProductLookup.SetEmpty();

            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;

            ProdbindingSource.DataSource = new List<BeamProdDto>();
            CTbindingSource.DataSource = new List<GrnTransDto>();

            DelBeamProd = new List<BeamProdDto>();
            DelCT = new List<GrnTransDto>();
            this.SelectSimpleButton.Enabled = true;

            voucherLookup.Focus();
        }
        public override void ResetPage()
        {
            base.ResetPage();

            voucherLookup.SetDefault();
            voucherDateEdit.EditValue = DateTime.Now;
            ProductLookup.SetEmpty();

            ProdbindingSource.DataSource = new List<BeamProdDto>();
            CTbindingSource.DataSource = new List<GrnTransDto>();

            DelBeamProd = new List<BeamProdDto>();
            DelCT = new List<GrnTransDto>();

            this.SelectSimpleButton.Enabled = true;
            voucherLookup.Focus();
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
                var pdata = db.Challans.Find(_key);
                var model = new GrnDto();
                var mapper = new Mapper(config);
                mapper.Map(pdata, model);
                LoadData(model);
            }

            this.SelectSimpleButton.Enabled = false;
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

        public override void SaveDataAsync(bool newmode)
        {
            bool IsSaved = false;
            if (!ValidateData()) return;

            ChallanModel _find = new ChallanModel();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BeamProdDto, ProdModel>().ForMember(x => x.Id, p => p.Ignore());
                cfg.CreateMap<GrnTransDto, ChallanTransModel>().ForMember(x => x.Id, p => p.Ignore());
            });

            var _prodlist = ProdbindingSource.DataSource as List<BeamProdDto>;
            var _ctlist = CTbindingSource.DataSource as List<GrnTransDto>;
            List<ChallanTransModel> Trans = new List<ChallanTransModel>();

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
                            createdate = Convert.ToDateTime(_find.CreateDate);
                        }

                        _find.VoucherId = Convert.ToInt32(voucherLookup.SelectedValue);
                        _find.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));

                        _find.TotalAmount = _ctlist.Sum(k => k.Qty);
                        _find.TotalQty = _ctlist.Sum(k => k.Qty);
                        _find.TotalPcs = _ctlist.Sum(k => k.Pcs);

                        _find.CompId = KontoGlobals.CompanyId;
                        _find.YearId = KontoGlobals.YearId;
                        _find.BranchId = KontoGlobals.BranchId;

                        var map = new Mapper(config);
                        if (this.PrimaryKey == 0)
                        {
                            var _srno = DbUtils.NextSerialNo((int)_find.VoucherId, db, 0);
                            _find.VoucherNo = _srno;
                            _find.ChallanNo = "NA";

                            db.Challans.Add(_find);
                            db.SaveChanges();
                        }
                        else
                        {
                            _find.CreateDate = createdate;
                            _find.CreateUser = createuser;
                        }

                        // for transaction saved
                        foreach (var item in _ctlist)
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

                            if (item.Id <= 0)
                            {
                                db.ChallanTranses.Add(tranModel);
                                db.SaveChanges();
                            }
                            Trans.Add(tranModel);

                        }

                        //delete item from ChallanTrans
                        foreach (var item in DelCT)
                        {
                            if (item.Id == 0) continue;
                            var _model = db.ChallanTranses.Find(item.Id);
                            _model.IsDeleted = true;
                        }

                        //DELETED ENTRY FROM DATABASE

                        foreach (var p in DelBeamProd)
                        {
                            //delete ProdOut
                            ProdOutModel pOut = db.ProdOuts.Find(p.ProdOutId);
                            if (pOut != null)
                            {
                                var pro = db.Prods.FirstOrDefault(k => k.Id == p.Id);
                                pro.ProdStatus = "STOCK";

                                pro.ModifyDate = DateTime.Now;
                                pro.ModifyUser = KontoGlobals.UserName;

                                pOut.IsDeleted = true;
                                pOut.ModifyDate = DateTime.Now;
                                pOut.ModifyUser = KontoGlobals.UserName;
                            }
                        }

                        //sotock effect
                        var stk = db.StockTranses.Where(k => k.MasterRefId == _find.RowId).ToList();
                        if (stk != null)
                            db.StockTranses.RemoveRange(stk);

                        bool IsIssue = true;
                        string TableName = "TakaConv";

                        //Stock Effect from Production out
                        foreach (var item in _prodlist)
                        {
                            var pro = db.Prods.FirstOrDefault(k => k.Id == item.Id);
                            pro.ProdStatus = "Issue";

                            pro.ModifyDate = DateTime.Now;
                            pro.ModifyUser = KontoGlobals.UserName;

                            ProdOutModel pOut = db.ProdOuts.Find(item.ProdOutId);
                            if (pOut != null)
                            {
                                pOut.Qty = (item.Qty * -1);

                                pOut.ModifyDate = DateTime.Now;
                                pOut.ModifyUser = KontoGlobals.UserName;
                            }
                            else
                            {
                                pOut = new ProdOutModel();
                                pOut.ProductId = item.ProductId;
                                pOut.CompId = _find.CompId;
                                pOut.YearId = _find.YearId;
                                pOut.SrNo = item.SrNo;
                                pOut.Qty = (item.Qty * -1);
                                pOut.ProdId = item.Id;
                                pOut.RefId = _find.Id;
                                pOut.VoucherId = _find.VoucherId;
                                pOut.VoucherNo = item.VoucherNo;
                                //        pOut.TransId = _find.Id;
                                pOut.CreateDate = DateTime.Now;
                                pOut.CreateUser = KontoGlobals.UserName;

                                db.ProdOuts.Add(pOut);
                                db.SaveChanges();
                            }

                            StockEffect.StockTransProdOutEntry(_find, pro, IsIssue, TableName, KontoGlobals.UserName, db, pOut);
                        }

                        // STOCK EFFECT from challan Tranns
                        IsIssue = false;
                        foreach (var item in Trans)
                        {
                            StockEffect.StockTransChlnEntry(_find, item, IsIssue, TableName, KontoGlobals.UserName, db);
                        }

                        db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Taka Conv Save");
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
                    if (this.voucherLookup.GroupDto.PrintAfterSave && MessageBox.Show("Print Bill ?", "Print", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        this.PrimaryKey = _find.Id;
                        Print();
                        this.PrimaryKey = 0;
                    }

                    base.SaveDataAsync(newmode);
                    this.ResetPage();
                    this.NewRec();
                    voucherLookup.Focus();
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
            var prds = ProdbindingSource.DataSource as List<BeamProdDto>;
            var ct = CTbindingSource.DataSource as List<GrnTransDto>;

            if (Convert.ToInt32(voucherLookup.SelectedValue) <= 0)
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
            else if (Convert.ToInt32(ProductLookup.SelectedValue) <= 0 && this.PrimaryKey <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Product", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ProductLookup.Focus();
                return false;
            }
            else if (prds.Count <= 0)
            {
                MessageBoxAdv.Show(this, "Atleast one transaction must be entered!", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ProdgridView.Focus();
                return false;
            }
            else if (ct.Count <= 0)
            {
                MessageBoxAdv.Show(this, "Atleast one transaction must be entered!", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CTgridView.Focus();
                return false;
            }
            return true;
        }

        private void LoadData(GrnDto pdata)
        {
            KontoContext db = new KontoContext();

            voucherLookup.SelectedValue = pdata.VoucherId;
            voucherDateEdit.EditValue = KontoUtils.IToD(pdata.VoucherDate);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProdModel, BeamProdDto>();
            });

            var _ctlist = (from ct in db.ChallanTranses
                           join pd in db.Products on ct.ProductId equals pd.Id into join_pd
                           from pd in join_pd.DefaultIfEmpty()
                           join cl in db.ColorModels on ct.ColorId equals cl.Id into join_cl
                           from cl in join_cl.DefaultIfEmpty()
                           join dm in db.Products on ct.DesignId equals dm.Id into join_dm
                           from dm in join_dm.DefaultIfEmpty()
                           join np in db.Products on ct.NProductId equals np.Id into join_np
                           from np in join_np.DefaultIfEmpty()
                           join grd in db.Grades on ct.GradeId equals grd.Id into join_grd
                           from grd in join_grd.DefaultIfEmpty()
                           orderby ct.Id
                           where ct.IsActive == true && ct.IsDeleted == false &&
                           ct.ChallanId == pdata.Id
                           select new GrnTransDto()
                           {
                               Id = ct.Id,
                               Cess = ct.Cess,
                               CessPer = ct.CessPer,
                               ChallanId = ct.ChallanId,
                               ColorName = cl.ColorName,
                               Cops = ct.Cops,
                               ColorId = ct.ColorId.HasValue ? (int)ct.ColorId : 1,
                               DesignId = ct.DesignId.HasValue ? (int)ct.DesignId : 1,
                               GradeId = ct.GradeId.HasValue ? (int)ct.GradeId : 1,
                               DesignNo = dm.ProductCode,
                               Disc = ct.Disc,
                               DiscPer = ct.DiscPer,
                               Freight = ct.Freight,
                               FreightRate = ct.FreightRate,
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
                               UomId = (int)ct.UomId
                           }).ToList();
            var ProdList = (db.Database.SqlQuery<BeamProdDto>(
                                         "dbo.OutwardprodList @CompanyId={0},@VoucherID={1},@RefId={2}",
                                         KontoGlobals.CompanyId, (int)VoucherTypeEnum.TakaConv, pdata.Id).ToList());

            ProdbindingSource.DataSource = ProdList;
            CTbindingSource.DataSource = _ctlist;

            DelBeamProd = new List<BeamProdDto>();
            DelCT = new List<GrnTransDto>();

            voucherLookup.Focus();
        }

        #endregion
    }
}