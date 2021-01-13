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
using Konto.Shared.Masters.Item;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Yarn.BatchMaster
{
    public partial class BatchIndex : KontoMetroForm
    {
        private List<JobCardModel> FilterView = new List<JobCardModel>();
        private List<JobCardTransDto> DelTrans = new List<JobCardTransDto>();

        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        public BatchIndex()
        {
            InitializeComponent();

            FillLookup();
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            this.Load += BatchIndex_Load;

            this.MainLayoutFile = KontoFileLayout.Batch_Index;
            this.GridLayoutFile = KontoFileLayout.Batch_Trans;

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
            this.voucherLookup.SelectedValueChanged += VoucherLookup_SelectedValueChanged;

        }

        private void VoucherLookup_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.PrimaryKey == 0 && Convert.ToInt32(voucherLookup.SelectedValue) > 0)
            {
                VoucherNotextEdit.Text = "New-" + DbUtils.NextSerialNo(Convert.ToInt32(voucherLookup.SelectedValue), 1);
            }
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
                List<ComboBoxPairs> crossbp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Circular", "Circular"),
                new ComboBoxPairs("Triloval", "Triloval"),
                new ComboBoxPairs("Hexagonal", "Hexagonal")
            };

                List<ComboBoxPairs> cbp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Core", "Core"),
                new ComboBoxPairs("Effect", "Effect")
            };

                divLookUpEdit.Properties.DataSource = _divLists;
                CrossSectionlookUpEdit.Properties.DataSource = crossbp;
                ItemTyperepositoryItemLookUpEdit.DataSource = cbp;
            }
        }
        private JobCardTransDto PreOpenLookup()
        {
            BTranspgridView.GetRow(BTranspgridView.FocusedRowHandle);
            if (BTranspgridView.GetRow(BTranspgridView.FocusedRowHandle) == null)
            {
                BTranspgridView.AddNewRow();
            }
            var dr = (JobCardTransDto)BTranspgridView.GetRow(BTranspgridView.FocusedRowHandle);
            return dr;
        }
        private void OpenItemLookup(int _selvalue, JobCardTransDto er)
        {
            var frm = new ProductLkpWindow();

            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Product_Master;
            frm.VoucherType = VoucherTypeEnum.Batch;

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                er.ItemId = frm.SelectedValue;
                er.ItemName = frm.SelectedTex;
                var model = frm.SelectedItem as ProductLookupDto;

                BTranspgridView.FocusedColumn = BTranspgridView.GetNearestCanFocusedColumn(BTranspgridView.FocusedColumn);
            }
        }

        private void PendingLot()
        {
            KontoContext _db = new KontoContext();
            List<PendingLotDto> list;
            var spcol = _db.SpCollections.FirstOrDefault(k => k.Id ==
                                (int)SpCollectionEnum.PendingBatchLot);
            if (spcol == null)
            {
                list = (_db.Database.SqlQuery<PendingLotDto>(
               "dbo.PendingBatchLot @CompanyId={0},@VoucherTypeID={1},@ChallanType={2}",
               KontoGlobals.CompanyId, (int)VoucherTypeEnum.Inward,
               (int)ChallanTypeEnum.PURCHASE).ToList());
            }
            else
            {
                list = (_db.Database.SqlQuery<PendingLotDto>(
                 spcol.Name + " @CompanyId={0},@VoucherTypeID={1},@ChallanType={2}",
                 KontoGlobals.CompanyId, (int)VoucherTypeEnum.Inward,
                (int)ChallanTypeEnum.PURCHASE).ToList());
            }
            if (list.Count == 0) return;

            var stf = new PendingLotView();
            stf.list = list;
            var showDialog = stf.ShowDialog();
            if (stf.DialogResult == DialogResult.OK)
            {
                var id = 0;
                var selpd = stf.list.Where(x => x.IsSelected).ToList();
                // List<JobCardTransDto> Trans = new List<JobCardTransDto>();
                List<JobCardTransDto> Trans = BTbindingSource.DataSource as List<JobCardTransDto>;
                if (Trans == null || ((Trans.Count - 1) == 0))
                    Trans = new List<JobCardTransDto>();
                else
                {
                    Trans.RemoveAt(Trans.Count - 1);
                    id = Trans.Count * -1;
                }
                JobCardTransDto ct;
                foreach (var ord in selpd)
                {
                    if (ord.ProductId > 0)
                    {
                        ct = new JobCardTransDto();
                        id = id - 1;
                        ct.Id = id;

                        ct.ItemId = ord.ProductId;
                        ct.ItemName = ord.ProductName;
                        ct.LotNo = ord.LotNo;
                        ct.Description = "Core";
                        ct.RefId = ord.Id;
                        ct.RefTransId = ord.TransId;

                        Trans.Add(ct);
                    }
                }
                BTbindingSource.DataSource = Trans;
            }
            else
            {
                List<JobCardTransDto> Trans = BTbindingSource.DataSource as List<JobCardTransDto>;
                if (Trans != null || ((Trans.Count - 1) > 0))
                    Trans.RemoveAt(Trans.Count - 1);

                okSimpleButton.Focus();
            }

        }
        #endregion

        #region BTranspgridView
        private void BTranspgridView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var itm = BTranspgridView.GetFocusedRow() as JobCardTransDto;
            if (itm == null) return;
            if (!"Qty,ItemName".Contains(BTranspgridView.FocusedColumn.FieldName)) return;
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
            if (!(BTranspgridView.GetRow(e.RowHandle) is JobCardTransDto er)) return;

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
                var row = view.GetRow(view.FocusedRowHandle) as JobCardTransDto;
                view.DeleteRow(view.FocusedRowHandle);
                DelTrans.Add(row);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                GridView view = sender as GridView;
                if (BTranspgridView.FocusedColumn.FieldName == "ItemName")
                {
                    view.SetRowCellValue(view.FocusedRowHandle, colItemName, string.Empty);
                    view.SetRowCellValue(view.FocusedRowHandle, colItemId, 0);
                }
            }
        }
        private void BTranspgridView_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var rw = BTranspgridView.GetRow(e.RowHandle) as JobCardTransDto;
            rw.Id = -1 * BTranspgridView.RowCount;
        }
        private void BTransgridControl_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                var dr = PreOpenLookup();
                if (dr == null) return;
                if (BTranspgridView.FocusedColumn.FieldName == "LotNo")
                {
                    if (dr.ItemName == null)
                    {
                        if (e.KeyCode == Keys.Enter)
                        {
                            PendingLot();
                        }
                    }
                }
                else if (BTranspgridView.FocusedColumn.FieldName == "Per")
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        var trans = BTbindingSource.DataSource as List<JobCardTransDto>;
                        decimal? per = trans.Sum(k => k.Per);
                        if (per > 100)
                        {
                            dr.Per = 0;
                            BTranspgridView.FocusedColumn = BTranspgridView.GetVisibleColumn(colPer.VisibleIndex);
                        }
                    }
                }
                //else if (BTranspgridView.FocusedColumn.FieldName == "ItemName")
                //{
                //    if (e.KeyCode == Keys.Enter)
                //    {
                //        if (dr.ItemId == 0 || dr.ItemId == null)
                //        {
                //            OpenItemLookup(0, dr);
                //            // e.Handled = true;
                //        }
                //    }
                //    else if (e.KeyCode == Keys.F1)
                //    {
                //        OpenItemLookup((int)dr.ItemId, dr);
                //        e.Handled = true;
                //    }
                //}
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Batch GridControl KeyDown");
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
                divLookUpEdit.Focus();
                return;
            }
            else if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new BatchListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Batch [View]";
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
            this.FilterView = new List<JobCardModel>();
            this.Text = "Batch [Add New]";

            divLookUpEdit.EditValue = 1;
            this.ActiveControl = voucherLookup.buttonEdit1;
           
            voucherDateEdit.EditValue = DateTime.Now;

            
            voucherLookup.SetDefault();

            ProductLookup.SetEmpty();
            colorLookup.SetEmpty();
            colorLookup.SetGroup();
            //      CrossSectioncomboBox.SelectedValue = null;
            RemarkTextBoxExt.Text = string.Empty;

            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;

            BTbindingSource.DataSource = new List<JobCardTransDto>();

            DelTrans = new List<JobCardTransDto>();

            divLookUpEdit.Focus();
        }
        public override void ResetPage()
        {
            base.ResetPage();
            divLookUpEdit.EditValue = 1;

            voucherLookup.SetDefault();
            voucherDateEdit.EditValue = DateTime.Now;

            VoucherNotextEdit.Text = string.Empty;

            ProductLookup.SetEmpty();
            colorLookup.SetEmpty();
            colorLookup.SetGroup();
            CrossSectionlookUpEdit.EditValue = string.Empty;
            RemarkTextBoxExt.Text = string.Empty;

            BTbindingSource.DataSource = new List<JobCardTransDto>();
            DelTrans = new List<JobCardTransDto>();
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
            if (Convert.ToInt32(divLookUpEdit.EditValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "DivId", Operation = Op.Equals, Value = Convert.ToInt32(divLookUpEdit.EditValue) });
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

            JobCardModel _find = new JobCardModel();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<JobCardTransDto, JobCardTransModel>().ForMember(x => x.Id, p => p.Ignore());
            });

            var _Translist = BTbindingSource.DataSource as List<JobCardTransDto>;

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

                        if (divLookUpEdit.EditValue != null)
                            _find.DivId = Convert.ToInt32(divLookUpEdit.EditValue);

                        _find.VoucherId = Convert.ToInt32(voucherLookup.SelectedValue);

                        _find.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));

                        if (ProductLookup.SelectedValue != null)
                            _find.ProductId = Convert.ToInt32(ProductLookup.SelectedValue);

                        if (colorLookup.SelectedValue != null)
                            _find.ColorId = Convert.ToInt32(colorLookup.SelectedValue);

                        if (CrossSectionlookUpEdit.EditValue != null)
                            _find.CarrierNo = CrossSectionlookUpEdit.EditValue.ToString();
                        //if (designLookup.SelectedValue != null)
                        //    _find.PlyProductId = Convert.ToInt32(designLookup.SelectedValue);

                        _find.Remark = RemarkTextBoxExt.Text;

                        _find.CompanyId = KontoGlobals.CompanyId;
                        //_find.YearId = KontoGlobals.YearId;
                        //_find.BranchId = KontoGlobals.BranchId;

                        if (string.IsNullOrEmpty(_find.VoucherNo))
                        {
                            var _srno = DbUtils.NextSerialNo((int)_find.VoucherId, db, 0);
                            _find.VoucherNo = _srno;
                        }
                        var map = new Mapper(config);
                        if (this.PrimaryKey == 0)
                        {
                            db.jobCards.Add(_find);
                            db.SaveChanges();
                        }
                        else
                        {
                            _find.CreateDate = createdate;
                            _find.CreateUser = createuser;
                        }

                        var transModel = new JobCardTransModel();
                        //Batch Trans
                        foreach (var item in _Translist)
                        {
                            var transid = item.Id;
                            item.JobCardId = _find.Id;
                            transModel = new JobCardTransModel();
                            if (item.Id > 0)
                            {
                                transModel = db.jobCardTrans.Find(item.Id);
                            }
                            map = new Mapper(config);
                            map.Map(item, transModel);

                            if (item.Id <= 0)
                            {
                                db.jobCardTrans.Add(transModel);
                                db.SaveChanges();
                            }
                        }

                        foreach (var item in DelTrans)
                        {
                            if (item.Id <= 0) continue;
                            var _model = db.jobCardTrans.Find(item.Id);
                            _model.IsDeleted = true;
                        }

                        db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Batch Save");
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
            var trans = BTbindingSource.DataSource as List<JobCardTransDto>;

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
            else if (Convert.ToInt32(ProductLookup.SelectedValue) <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Product", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ProductLookup.Focus();
                return false;
            }
            //else if (trans.Count <= 0)
            //{
            //    MessageBoxAdv.Show(this, "At least one transaction should be entered!", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    BTranspgridView.Focus();
            //    return false;
            //}
            else if (trans.Count > 0)
            {
                decimal? per = trans.Sum(k => k.Per);
                if (per != 100)
                {
                    MessageBoxAdv.Show(this, "Total Percentage must be 100!", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    BTranspgridView.Focus();
                    return false;
                }
            }
            return true;
        }
         private void LoadData(JobCardModel pdata)
        {
            KontoContext db = new KontoContext();

            VoucherNotextEdit.Text = pdata.VoucherNo;
            divLookUpEdit.EditValue = pdata.DivId;
            voucherLookup.SelectedValue = pdata.VoucherId;
            voucherDateEdit.EditValue = KontoUtils.IToD(pdata.VoucherDate);

            if (pdata.ProductId > 0)
            {
                ProductLookup.SelectedValue = pdata.ProductId;
                ProductLookup.SetGroup((int)pdata.ProductId);
            }

            if (pdata.ColorId > 0)
                colorLookup.SelectedValue = pdata.ColorId ;
            colorLookup.SetGroup();
            CrossSectionlookUpEdit.EditValue = pdata.CarrierNo;
            RemarkTextBoxExt.Text = pdata.Remark;

            createdLabelControl.Text = "Created By: " + pdata.CreateUser + " [ " + pdata.CreateDate + " ]";
            modifyLabelControl.Text = "Modified By: " + pdata.ModifyUser + " [ " + pdata.ModifyDate ?? string.Empty + " ]";

            var translist =
                (from bt in db.jobCardTrans
                 join pd in db.Products on bt.ItemId equals pd.Id into join_p
                 from pd in join_p.DefaultIfEmpty()
                 where bt.JobCardId == pdata.Id &&
               bt.IsActive == true && bt.IsDeleted == false
                 select new JobCardTransDto()
                 {
                     ItemId = (int)bt.ItemId,
                     JobCardId = bt.JobCardId,
                     Id = bt.Id,
                     ItemName = pd.ProductName,
                     Description = bt.Description,
                     LotNo = bt.LotNo,
                     Per = (int)bt.Per,
                     Ply = bt.Ply,
                     RefId = bt.RefId,
                     RefTransId = bt.RefTransId,
                     Remark = bt.Remark
                 }
           ).ToList();

            BTbindingSource.DataSource = translist;
            DelTrans = new List<JobCardTransDto>();

            divLookUpEdit.Focus();
        }
        #endregion
    }
}