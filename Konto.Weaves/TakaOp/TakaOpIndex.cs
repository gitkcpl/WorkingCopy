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
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Weaves.TakaOp
{
    public partial class TakaOpIndex : KontoMetroForm
    {
        private List<ProdModel> FilterView = new List<ProdModel>();
        private List<BeamProdDto> DelOrder = new List<BeamProdDto>();

        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        public TakaOpIndex()
        {
            InitializeComponent();

            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            FillLookup();

            this.Load += TakaOpIndex_Load;
            this.MainLayoutFile = KontoFileLayout.TakaOp_Index;
            this.GridLayoutFile = KontoFileLayout.TakaOp_Trans;

            OrderDetailgridView.InitNewRow += ProdgridView_InitNewRow;
            OrderDetailgridView.KeyDown += ProdgridView_KeyDown;
            OrderDetailgridControl.Enter += ProdGridControl_Enter;
            OrderDetailgridView.MouseUp += ProdgridView_MouseUp;
            OrderDetailgridView.InvalidRowException += ProdgridView_InvalidRowException;
            OrderDetailgridView.ShowingEditor += ProdgridView_ShowingEditor;
            OrderDetailgridView.DoubleClick += ProdgridView_DoubleClick;
            OrderDetailgridView.CustomDrawRowIndicator += ProdgridView_CustomDrawRowIndicator;
            OrderDetailgridView.CellValueChanged += OrderDetailgridView_CellValueChanged;
            OrderDetailgridControl.ProcessGridKey += OrderDetailgridControl_ProcessGridKey;
            OrderDetailgridView.ValidateRow += OrderDetailgridView_ValidateRow;

            productRepositoryItemButtonEdit.ButtonClick += ProductRepositoryItemButtonEdit_ButtonClick;
            colorRepositoryItemButtonEdit.ButtonClick += ColorRepositoryItemButtonEdit_ButtonClick;
            designRepositoryItemButtonEdit.ButtonClick += DesignRepositoryItemButtonEdit_ButtonClick;

            this.FirstActiveControl = OrderDetailgridControl;
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

                var _macLists = (from p in db.MachineMasters
                                 where p.IsActive && !p.IsDeleted
                                 select new BaseLookupDto()
                                 {
                                     DisplayText = p.MachineName,
                                     Id = p.Id
                                 }).ToList();

                DivrepositoryItemLookUpEdit.DataSource = _divLists;
                MachinerepositoryItemLookUpEdit.DataSource = _macLists;
            }
        }
        private BeamProdDto PreOpenLookup()
        {
            OrderDetailgridView.GetRow(OrderDetailgridView.FocusedRowHandle);
            if (OrderDetailgridView.GetRow(OrderDetailgridView.FocusedRowHandle) == null)
            {
                OrderDetailgridView.AddNewRow();
            }
            var dr = (BeamProdDto)OrderDetailgridView.GetRow(OrderDetailgridView.FocusedRowHandle);
            return dr;
        }
        private void OpenItemLookup(int _selvalue, BeamProdDto er)
        {
            var frm = new ProductLkpWindow();

            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Product_Master;

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                er.ProductId = frm.SelectedValue;
                er.ProductName = frm.SelectedTex;
                var model = frm.SelectedItem as ProductLookupDto;

                OrderDetailgridView.FocusedColumn = OrderDetailgridView.GetVisibleColumn(colProductName.VisibleIndex + 1);
            }
        }

        private void OpenColorLookup(int _selvalue, BeamProdDto er)
        {
            var frm = new ColorLkpWindow();
            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Color;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                OrderDetailgridView.BeginDataUpdate();
                er.ColorId = frm.SelectedValue;
                er.ColorName = frm.SelectedTex;

                OrderDetailgridView.EndDataUpdate();
                OrderDetailgridView.FocusedColumn = OrderDetailgridView.GetVisibleColumn(colColorName.VisibleIndex + 1);
            }
        }
        private void OpenDesignLookup(int _selvalue, BeamProdDto er)
        {
            var frm = new DesignLkpWindow();
            frm.Tag = MenuId.Design_Master;
            frm.SelectedValue = _selvalue;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                OrderDetailgridView.BeginDataUpdate();
                er.PlyProductId = frm.SelectedValue;
                er.DesignNo = frm.SelectedTex;
                OrderDetailgridView.EndDataUpdate();
                OrderDetailgridView.FocusedColumn = OrderDetailgridView.GetVisibleColumn(colDesignNo.VisibleIndex + 1);
            }

        }
        #endregion

        #region OrderGridView
        private void ProdgridView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var itm = OrderDetailgridView.GetFocusedRow() as BeamProdDto;
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
                var row = view.GetRow(view.FocusedRowHandle) as BeamProdDto;
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
            var rw = OrderDetailgridView.GetRow(e.RowHandle) as BeamProdDto;
            rw.Id = -1 * OrderDetailgridView.RowCount;
            rw.VouDate = DateTime.Now.Date;
        }
        private void ProdgridView_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }
        private void OrderDetailgridControl_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                var dr = PreOpenLookup();
                if (dr == null) return;
                if (OrderDetailgridView.FocusedColumn.FieldName == "ProductName")
                {

                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.ProductId == null)
                        {
                            OpenItemLookup(0, dr);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenItemLookup((int)dr.ProductId, dr);
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        if (dr.ProductId == null)
                        {
                            OpenItemLookup(0, dr);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenItemLookup((int)dr.ProductId, dr);
                        }
                        e.Handled = true;
                    }
                }
                else if (OrderDetailgridView.FocusedColumn.FieldName == "ColorName")
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
                else if (OrderDetailgridView.FocusedColumn.FieldName == "DesignNo")
                {
                    if (e.KeyCode == Keys.Return)
                    {
                        if (dr.PlyProductId == null)
                        {
                            OpenDesignLookup(0, dr);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenDesignLookup((int)dr.PlyProductId, dr);
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        if (dr.PlyProductId == null)
                        {
                            OpenDesignLookup(0, dr);
                            // e.Handled = true;
                        }
                        else
                        {
                            OpenDesignLookup((int)dr.PlyProductId, dr);
                        }
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Taka Opening Order Detail GridControl KeyDown");
                MessageBoxAdv.Show(this, "Error Lookup Setup !!", "Exception ", ex.ToString());
            }
        }
        private void OrderDetailgridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == null) return;
            var er = OrderDetailgridView.GetRow(e.RowHandle) as BeamProdDto;
            if (er == null) return;

            if (er.NetWt != 0 && er.TareWt != null && er.TareWt != 0 && er.NetWt != 0 && er.TareWt != 0)
                er.AvgWt = (decimal)(er.NetWt / er.TareWt) * 100;
        }
        private void OrderDetailgridView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var view = sender as GridView;
            var itemid = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, colProductId));
            var netWt = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, colNetWt));
            var voucherNo = view.GetRowCellValue(e.RowHandle, colVoucherNo);

            if (voucherNo == null)
            {
                view.SetColumnError(colVoucherNo, "Invalid Voucher");
                e.Valid = false;
            }
            if (itemid <= 0)
            {
                view.SetColumnError(colProductName, "Invalid Product");
                e.Valid = false;
            }
            if (netWt <= 0)
            {
                view.SetColumnError(colNetWt, "NetWeight Must be greater than zero");
                e.Valid = false;
            }
        }

        #endregion

        #region Event
        private void TakaOpIndex_Load(object sender, EventArgs e)
        {

        }
        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                OrderDetailgridView.Focus();
                return;
            }
            else if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new TakaOpList();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Taka Opening [View]";
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
                Log.Error(ex, "Taka Op Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }
        private void ProductRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
            {
                if (dr.ProductId != null && dr.ProductId > 0)
                    OpenItemLookup((int)dr.ProductId, dr);
                else
                    OpenItemLookup(0, dr);

            }
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
        private void DesignRepositoryItemButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
            {
                if (dr.PlyProductId != null && dr.PlyProductId > 0)
                    OpenDesignLookup((int)dr.PlyProductId, dr);
                else
                    OpenDesignLookup(0, dr);
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
            this.FilterView = new List<ProdModel>();
            this.Text = "Taka Opening [Add New]";

            //this.ActiveControl = OrderDetailgridView.produ.buttonEdit1;

            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;

            OpbindingSource.DataSource = new List<BeamProdDto>();
            DelOrder = new List<BeamProdDto>();
        }
        public override void ResetPage()
        {
            base.ResetPage();
            OpbindingSource.DataSource = new List<BeamProdDto>();
            DelOrder = new List<BeamProdDto>();
        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var pdata = db.Prods.Find(_key);

                LoadData(pdata);
                createdLabelControl.Text = "Create By: " + pdata.CreateUser;
            }

            this.Text = "Taka Op [Edit New]";

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
              
            filter.Add(new Filter { PropertyName = "CompId", Operation = Op.Equals, Value = KontoGlobals.CompanyId });
            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });

            using (var db = new KontoContext())
            {
                int vtypeId = (int)VoucherTypeEnum.OpTaka;
                int VouchrId = 0;
                if (db.Vouchers.FirstOrDefault(k => k.IsActive == true
                                               && k.IsDeleted == false && k.VTypeId == vtypeId) != null)
                {
                    VouchrId = db.Vouchers.FirstOrDefault(k => k.VTypeId == vtypeId).Id;
                }
                filter.Add(new Filter { PropertyName = "VoucherId", Operation = Op.Equals, Value = Convert.ToInt32(VouchrId) });

                FilterView = db.Prods.Where(ExpressionBuilder.GetExpression<ProdModel>(filter))
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

            ProdModel _find = new ProdModel();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BeamProdDto, ProdModel>().ForMember(x => x.Id, p => p.Ignore());
            });

            var Trans = OpbindingSource.DataSource as List<BeamProdDto>;
            List<ProdModel> ProdTrans = new List<ProdModel>();
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
                            _find = db.Prods.Find(this.PrimaryKey);
                            createuser = _find.CreateUser;
                            createdate = Convert.ToDateTime(_find.CreateDate);
                        }
                        //Order Detail 
                        var map = new Mapper(config);
                        ProdModel tranModel;
                        int srno = 0;
                        int vtypeid = (int)VoucherTypeEnum.OpTaka;
                        int VouchrId = 0;
                        if (db.Vouchers.FirstOrDefault(k => k.IsActive == true
                                                && k.IsDeleted == false && k.VTypeId == vtypeid) != null)
                        {
                            VouchrId = db.Vouchers.FirstOrDefault(k => k.VTypeId == vtypeid).Id;
                        }

                        var maxGroid = db.Prods.Where(k => k.VoucherId == VouchrId).Max(k => k.SubGradeId);
                        int subGrdId = maxGroid != null ? (int)maxGroid + 1 : 0;
                        foreach (var item in Trans)
                        {
                            var transid = item.Id;

                            //Check for already present voucher No
                            if (db.Set<ProdModel>().Any(x =>
                                x.Id != item.Id && x.VoucherNo == item.VoucherNo
                                && x.CompId == KontoGlobals.CompanyId && x.YearId == KontoGlobals.YearId &&
                                x.BranchId == KontoGlobals.BranchId && x.VoucherId == item.VoucherId
                                && x.IsDeleted == false && x.IsActive))
                            {
                                MessageBox.Show(KontoGlobals.DuplicateTakaMsg);
                                return;
                            }

                            DateTime vdate = (DateTime)item.VouDate;
                            item.VoucherDate = Convert.ToInt32(vdate.ToString("yyyyMMdd"));
                            item.VoucherId = VouchrId;

                            tranModel = new ProdModel();
                            if (item.Id > 0)
                            {
                                tranModel = db.Prods.Find(item.Id);
                                subGrdId = (int)tranModel.SubGradeId;
                            }
                            map = new Mapper(config);
                            map.Map(item, tranModel);

                            tranModel.VTypeId = (int)VoucherTypeEnum.OpTaka;
                            tranModel.ProdStatus = "STOCK";
                            tranModel.CreateUser = KontoGlobals.UserName;
                            tranModel.CompId = KontoGlobals.CompanyId;
                            tranModel.YearId = KontoGlobals.YearId;
                            tranModel.BranchId = KontoGlobals.BranchId;
                            tranModel.IsOk = true;
                            tranModel.IsActive = true;

                            if (item.Id <= 0)
                            {
                                tranModel.SrNo = srno + 1;
                                srno = srno + 1;
                                tranModel.SubGradeId = subGrdId;
                                db.Prods.Add(tranModel);
                                db.SaveChanges();
                            }
                            ProdTrans.Add(tranModel);
                        }

                        //DELETED ENTRY FROM DATABASE Production
                        foreach (var p in DelOrder)
                        {
                            var pro = db.Prods.FirstOrDefault(k => k.Id == p.Id);
                            if (pro != null)
                            {
                                pro.IsDeleted = true;
                                var stk1 = db.StockTranses.FirstOrDefault(k => k.MasterRefId == pro.RowId);
                                if (stk1 != null)
                                {
                                    db.StockTranses.Remove(stk1);
                                    db.SaveChanges();
                                }
                            }
                        }

                        //sotock effect
                        string TableName = "Taka Op";
                        decimal RcptQty = 0;
                        decimal IssueQty = 0;
                        int pcs = 1;
                        decimal qty = 0;
                        foreach (var item in ProdTrans)
                        {
                            var stk = db.StockTranses.FirstOrDefault(k => k.MasterRefId == item.RowId);
                            if (stk != null)
                            {
                                db.StockTranses.Remove(stk);
                                db.SaveChanges();
                            }
                            RcptQty = item.NetWt != 0 ? (decimal)item.NetWt : 0;
                            qty = item.NetWt != 0 ? (decimal)item.NetWt : 0;

                            var stockReq = db.Products.FirstOrDefault(k => k.Id == item.ProductId).StockReq;
                            if (stockReq == "No") continue;
                            StockEffect.StockTransProdEntry(item, false, RcptQty, IssueQty, qty, pcs, TableName, db);
                        }
                        db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Taka Opening Save");
                        MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
                    }
                }
            }

            if (IsSaved)
            {
                // NewRec();

                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (!this.OpenForLookup && newmode)
                {
                    //if (this.voucherLookup.GroupDto.PrintAfterSave && MessageBox.Show("Print JobCard ?", "Print", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    //{
                    //    this.PrimaryKey = _find.Id;
                    //    Print();
                    //    this.PrimaryKey = 0;
                    //}

                    base.SaveDataAsync(newmode);
                    this.ResetPage();
                    this.NewRec();
                    OrderDetailgridView.Focus();
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
            var trans = OpbindingSource.DataSource as List<BeamProdDto>;

            if (trans.Count <= 0)
            {
                MessageBoxAdv.Show(this, "Atleast one transaction must be entered!", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                OrderDetailgridView.Focus();
                return false;
            }

            return true;
        }
        private void LoadData(ProdModel pdata)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProdModel, BeamProdDto>();
            });

            KontoContext _db = new KontoContext();
            var trans = (from pd in _db.Prods
                         join v in _db.Vouchers on pd.VoucherId equals v.Id into vou_join
                         from vou in vou_join.DefaultIfEmpty()
                         join p in _db.Products on pd.ProductId equals p.Id into pro_join
                         from pro in pro_join.DefaultIfEmpty()
                         join c in _db.ColorModels on pd.ColorId equals c.Id into Color_join
                         from col in Color_join.DefaultIfEmpty()
                         join d in _db.Products on pd.PlyProductId equals d.Id into Design_join
                         from d in Design_join.DefaultIfEmpty()
                         join div in _db.Divisions on pd.DivId equals div.Id into Div_join
                         from div in Div_join.DefaultIfEmpty()
                         join m in _db.MachineMasters on pd.MacId equals m.Id into mac_join
                         from m1 in mac_join.DefaultIfEmpty()
                         where pd.VoucherId == pdata.VoucherId
                       && pd.SubGradeId == pdata.SubGradeId
                       //&& pd.IsActive
                       && !pd.IsDeleted
                         select new BeamProdDto()
                         {
                             Id = pd.Id,
                             BoxProductId = pd.BoxProductId,
                             BoxRate = pd.BoxRate,
                             ColorId = pd.ColorId,
                             CompId = pd.CompId,
                             Cops = pd.Cops,
                             CopsRate = pd.CopsRate,
                             CopsWt = pd.CopsWt,
                             CurrQty = pd.CurrQty,
                             ProductName = pro.ProductName,
                             DesignNo = d.ProductName,
                             PlyProductId = pd.PlyProductId,
                             DivId = pd.DivId,
                             FinQty = pd.FinQty,
                             GradeId = pd.GradeId,
                             GrossWt = pd.GrossWt,
                             IsClose = pd.IsClose,
                             IssueRefId = pd.IssueRefId,
                             IssueRefVoucherId = pd.IssueRefVoucherId,
                             LoadingDate = pd.LoadingDate,
                             MachineName = m1.MachineName,
                             MacId = pd.MacId,
                             NetWt = pd.NetWt,
                             PackId = pd.PackId,
                             Pallet = pd.Pallet,
                             Ply = pd.Ply,
                             ProdStatus = pd.ProdStatus,
                             ProductId = pd.ProductId,
                             RefId = pd.RefId,
                             Remark = pd.Remark,
                             SrNo = pd.SrNo,
                             SubGradeId = pd.SubGradeId,
                             TareWt = pd.TareWt,
                             Tops = pd.Tops,
                             TransId = pd.TransId,
                             TwistType = pd.TwistType,
                             VoucherDate = pd.VoucherDate,
                             VoucherId = pd.VoucherId,
                             VoucherNo = pd.VoucherNo,
                             ColorName = col.ColorName,
                             YearId = pd.YearId,
                             IsActive = pd.IsActive,
                             IsDeleted = pd.IsDeleted,
                             AvgWt = pd.TareWt >0 ? (pd.NetWt / pd.TareWt) * 100 : 0
                         }
               ).ToList();
            foreach (var item in trans)
            {
                item.VouDate = DateTime.ParseExact(item.VoucherDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture);
            }
            OpbindingSource.DataSource = trans;

            DelOrder = new List<BeamProdDto>();
        }
        #endregion
    }
}