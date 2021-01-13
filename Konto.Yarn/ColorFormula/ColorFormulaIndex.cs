using AutoMapper;
using C1.Win.C1Input.Enums;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using GrapeCity.Viewer.Common.Model;
using Konto.App.Shared;
using Konto.App.Shared.Para;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters.Dtos;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using Konto.Shared.Masters.Emp;
using Konto.Shared.Masters.Item;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Yarn.ColorFormula
{
    public partial class ColorFormulaIndex : KontoMetroForm
    {
        private List<RCPUIModel> FilterView = new List<RCPUIModel>();
        private List<RcpuiTransDto> DelTrans = new List<RcpuiTransDto>();

        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        ProductTypeEnum ptype;
        public ColorFormulaIndex()
        {
            InitializeComponent();

            if ((int)MenuId.Color_Formula == KontoGlobals.MenuId)
            {
                DescTextBoxExt.Visible = true;
                //colRemark.Visible = true;
                colColorCategory.Visible = true;
                ptype = ProductTypeEnum.COLOR;

                productLookup1.Visible = false;
                colProductId.Visible = false;
                colProductName.Visible = false;
            }
            else
            {
                DescTextBoxExt.Visible = false;
                //colRemark.Visible = false;
                //colColorCategory.Visible = true;
                 
                productLookup1.Visible = true;
                colProductId.Visible = true;
                colProductName.Visible = true;
                ptype = ProductTypeEnum.CHEMICAL;
            }
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            FillLookup();

            this.Load += BatchIndex_Load;

            this.MainLayoutFile = KontoFileLayout.ColorFormula_Index;
            this.GridLayoutFile = KontoFileLayout.ColorFormula_Trans;

            BTransgridControl.ProcessGridKey += BTransgridControl_ProcessGridKey;
            BTranspgridView.InitNewRow += BTranspgridView_InitNewRow;
            BTranspgridView.CellValueChanged += BTranspgridView_CellValueChanged;
            BTranspgridView.KeyDown += BTranspgridView_KeyDown;
            BTransgridControl.Enter += BTransgridControl_Enter;
            //  gridView1.ValidateRow += GridView1_ValidateRow;
            BTranspgridView.MouseUp += BTranspgridView_MouseUp;
            BTranspgridView.InvalidRowException += BTranspgridView_InvalidRowException;
            BTranspgridView.ShowingEditor += BTranspgridView_ShowingEditor;
            BTranspgridView.DoubleClick += BTranspgridView_DoubleClick;
            BTranspgridView.CustomDrawRowIndicator += BTranspgridView_CustomDrawRowIndicator;

            ColorCategoryItemLookUpEdit.ButtonClick += ColorCategoryItemLookUpEdit_ButtonClick;
        }

        private void FillLookup()
        {
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Bleech", "Bleech"),
                new ComboBoxPairs("Dyeing", "Dyeing"),
                new ComboBoxPairs("RC", "RC"),
                new ComboBoxPairs("Washing", "Washing"),
            };
            ColorCategoryItemLookUpEdit.DataSource = cbp;
        }

        #region UDF
        private RcpuiTransDto PreOpenLookup()
        {
            BTranspgridView.GetRow(BTranspgridView.FocusedRowHandle);
            if (BTranspgridView.GetRow(BTranspgridView.FocusedRowHandle) == null)
            {
                BTranspgridView.AddNewRow();
            }
            var dr = (RcpuiTransDto)BTranspgridView.GetRow(BTranspgridView.FocusedRowHandle);
            return dr;
        }
        private void OpenItemLookup(int _selvalue, RcpuiTransDto er)
        {
            var frm = new ProductLkpWindow();

            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Product_Master;

            frm.PTypeId = ptype;
            frm.VoucherType = VoucherTypeEnum.ColorFormula;

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                er.ProductId = frm.SelectedValue;
                er.ProductName = frm.SelectedTex;
                //       var model = frm.SelectedItem as ProductLookupDto;

                BTranspgridView.FocusedColumn = BTranspgridView.GetNearestCanFocusedColumn(BTranspgridView.FocusedColumn);
            }
        }
        #endregion
        #region BTranspgridView
        private void ColorCategoryItemLookUpEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {

        }
        private void BTranspgridView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var itm = BTranspgridView.GetFocusedRow() as RcpuiTransDto;
            if (itm == null) return;
            if (!"Qty,ProductName".Contains(BTranspgridView.FocusedColumn.FieldName)) return;
        }
        private void BTranspgridView_MouseUp(object sender, MouseEventArgs e)
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
        private void BTranspgridView_DoubleClick(object sender, EventArgs e)
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
        private void BTranspgridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.Appearance.ForeColor = Color.FromArgb(227, 22, 91);
            if (e.RowHandle < 0)
                e.Info.DisplayText = "";
            else
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        private void BTransgridControl_Enter(object sender, EventArgs e)
        {
            BTranspgridView.FocusedColumn = BTranspgridView.VisibleColumns[0];
        }
        private void BTranspgridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == null) return;
            if (!(BTranspgridView.GetRow(e.RowHandle) is RcpuiTransDto er)) return;
        }
        private void BTranspgridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete) return;

            if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
            {
                if (MessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo) !=
                  DialogResult.Yes)
                    return;
                GridView view = sender as GridView;
                var row = view.GetRow(view.FocusedRowHandle) as RcpuiTransDto;
                view.DeleteRow(view.FocusedRowHandle);
                DelTrans.Add(row);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                GridView view = sender as GridView;
                if (BTranspgridView.FocusedColumn.FieldName == "ProductName")
                {
                    view.SetRowCellValue(view.FocusedRowHandle, colProductName, string.Empty);
                    view.SetRowCellValue(view.FocusedRowHandle, colProductId, 0);
                }
            }
        }
        private void BTranspgridView_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var rw = BTranspgridView.GetRow(e.RowHandle) as RcpuiTransDto;
            rw.Id = -1 * BTranspgridView.RowCount;
        }
        private void BTransgridControl_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                var dr = PreOpenLookup();
                if (dr == null) return;
                if (BTranspgridView.FocusedColumn.FieldName == "ProductName")
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
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Color Formula GridControl KeyDown");
                MessageBoxAdv.Show(this, "Error Lookup Setup !!", "Exception ", ex.ToString());
            }
        }
        private void BTranspgridView_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }
        #endregion
        #region Event
        private void BatchIndex_Load(object sender, EventArgs e)
        {
        }
        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                //divLookUpEdit.Focus();
                return;
            }
            else if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new ColorFormulaListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Color Formula [View]";
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
                Log.Error(ex, "Batch Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
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
            this.FilterView = new List<RCPUIModel>();
            this.Text = "Color Formula [Add New]";

            this.ActiveControl = voucherLookup.buttonEdit1;
            voucherLookup.SetDefault();
            voucherDateEdit.EditValue = DateTime.Now;

            VoucherNotextEdit.Text = "New";
            productLookup1.SetEmpty();
            DescTextBoxExt.Text = string.Empty;
            RemarkTextBoxExt.Text = string.Empty;
            QtySpinEdit.EditValue = 100;

            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;

            transbindingSource.DataSource = new List<RcpuiTransDto>();

            DelTrans = new List<RcpuiTransDto>();
        }
        public override void ResetPage()
        {
            base.ResetPage();

            voucherLookup.SetDefault();
            voucherDateEdit.EditValue = DateTime.Now;

            VoucherNotextEdit.Text = "New";
            productLookup1.SetEmpty();
            DescTextBoxExt.Text = string.Empty;
            RemarkTextBoxExt.Text = string.Empty;
            QtySpinEdit.EditValue = 100;

            transbindingSource.DataSource = new List<RcpuiTransDto>();

            DelTrans = new List<RcpuiTransDto>();
        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var pdata = db.RCPUIs.Find(_key);
                LoadData(pdata);
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


            if (Convert.ToInt32(voucherLookup.SelectedValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "VoucherId", Operation = Op.Equals, Value = Convert.ToInt32(voucherLookup.SelectedValue) });
            }

            //if (Convert.ToInt32(divLookUpEdit.EditValue) > 0)
            //{
            //    filter.Add(new Filter { PropertyName = "DivId", Operation = Op.Equals, Value = Convert.ToInt32(divLookUpEdit.EditValue) });
            //}
            //if (Convert.ToInt32(ProductLookup.SelectedValue) > 0)
            //{
            //    filter.Add(new Filter { PropertyName = "ProductId", Operation = Op.Equals, Value = Convert.ToInt32(ProductLookup.SelectedValue) });
            //}

            filter.Add(new Filter { PropertyName = "CompId", Operation = Op.Equals, Value = KontoGlobals.CompanyId });
            filter.Add(new Filter { PropertyName = "YearId", Operation = Op.Equals, Value = KontoGlobals.YearId });
            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });

            using (var db = new KontoContext())
            {
                FilterView = db.RCPUIs.Where(ExpressionBuilder.GetExpression<RCPUIModel>(filter))
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

            RCPUIModel _find = new RCPUIModel();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RcpuiTransDto, RCPUITransModel>().ForMember(x => x.Id, p => p.Ignore());
            });

            var _Translist = transbindingSource.DataSource as List<RcpuiTransDto>;

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
                            _find = db.RCPUIs.Find(this.PrimaryKey);
                            createuser = _find.CreateUser;
                            createdate = Convert.ToDateTime(_find.CreateDate);
                        }
                        _find.VoucherId = Convert.ToInt32(voucherLookup.SelectedValue);
                        _find.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
                        _find.Description = DescTextBoxExt.Text;

                        if (!string.IsNullOrEmpty(productLookup1.SelectedText))
                            _find.ProductId = Convert.ToInt32(productLookup1.SelectedValue);

                        if (!string.IsNullOrEmpty(colorLookup1.SelectedText))
                            _find.ColorId = Convert.ToInt32(colorLookup1.SelectedValue);

                        _find.Qty = QtySpinEdit.Value;
                        _find.Remark = RemarkTextBoxExt.Text;

                        _find.CompId = KontoGlobals.CompanyId;
                        _find.YearId = KontoGlobals.YearId;
                        _find.BranchId = KontoGlobals.BranchId;

                        var map = new Mapper(config);
                        if (this.PrimaryKey == 0)
                        {
                            var _srno = DbUtils.NextSerialNo((int)_find.VoucherId, db, 0);
                            _find.VoucherNo = _srno;

                            db.RCPUIs.Add(_find);
                            db.SaveChanges();
                        }
                        else
                        {
                            _find.CreateDate = createdate;
                            _find.CreateUser = createuser;
                        }

                        var transModel = new RCPUITransModel();
                        //Batch Trans
                        foreach (var item in _Translist)
                        {
                            var transid = item.Id;
                            item.RCPUIId = _find.Id;
                            transModel = new RCPUITransModel();
                            if (item.Id > 0)
                            {
                                transModel = db.RcpuiTrans.Find(item.Id);
                            }
                            map = new Mapper(config);
                            map.Map(item, transModel);

                            if (item.Id <= 0)
                            { 
                                db.RcpuiTrans.Add(transModel);
                                db.SaveChanges();
                            }
                        }

                        foreach (var item in DelTrans)
                        {
                            if (item.Id <= 0) continue;
                            var _model = db.RcpuiTrans.Find(item.Id);
                            _model.IsDeleted = true;
                        }

                        db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "color formula Save");
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
                    //divLookUpEdit.Focus();
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
            var trans = transbindingSource.DataSource as List<RcpuiTransDto>;

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
            else if (QtySpinEdit.Value <= 0)
            {
                MessageBoxAdv.Show(this, "Qty must be greater than ZERO", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                QtySpinEdit.Focus();
                return false;
            }
            else if (trans.Count <= 0)
            {
                MessageBoxAdv.Show(this, "At least one transaction should be entered!", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BTranspgridView.Focus();
                return false;
            }

            return true;
        }
        private void LoadData(RCPUIModel pdata)
        {
            KontoContext db = new KontoContext();

            voucherLookup.SelectedValue = pdata.VoucherId;
            voucherDateEdit.EditValue = KontoUtils.IToD((int)pdata.VoucherDate);

            productLookup1.SelectedValue = pdata.ProductId;
            productLookup1.SetGroup((int)pdata.ProductId);
         
            colorLookup1.SelectedValue = pdata.ColorId;
            colorLookup1.SetGroup();
            QtySpinEdit.EditValue = pdata.Qty;
            DescTextBoxExt.Text = pdata.Description;
            RemarkTextBoxExt.Text = pdata.Remark;

            var translist = (from bt in db.RcpuiTrans
                             join pd in db.Products on bt.ProductId equals pd.Id into join_p
                             from pd in join_p.DefaultIfEmpty()
                             where bt.RCPUIId == pdata.Id &&
                           bt.IsActive == true && bt.IsDeleted == false
                             select new RcpuiTransDto()
                             {
                                 ProductId = bt.ProductId,
                                 RCPUIId = bt.RCPUIId,
                                 Id = bt.Id,
                                 ProductName = pd.ProductName,
                                 ColorCategory = bt.ColorCategory,
                                 ColorKgs = bt.ColorKgs,
                                 ColorPer = bt.ColorPer,
                                 Remark = bt.Remark
                             }).ToList();

            transbindingSource.DataSource = translist;
            DelTrans = new List<RcpuiTransDto>();
        }
        #endregion
    }
}