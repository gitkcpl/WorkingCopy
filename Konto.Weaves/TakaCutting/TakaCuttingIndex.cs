using AutoMapper;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using GrapeCity.ActiveReports;
using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters.Dtos;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using Konto.Shared.Masters.Color;
using Konto.Shared.Masters.Item;
using Konto.Weaves.BeamLoading;
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

namespace Konto.Weaves.TakaCutting
{
    public partial class TakaCuttingIndex : KontoMetroForm
    {
        private List<ProdOutModel> FilterView = new List<ProdOutModel>();
        private List<BeamProdDto> DelBeamProd = new List<BeamProdDto>();
        private int ProdId = 0;
        private int? Colorid;

        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        public TakaCuttingIndex()
        {
            InitializeComponent();

            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            this.Load += TakaConvIndex_Load;

            this.MainLayoutFile = KontoFileLayout.TakaCutting_Index;
            this.GridLayoutFile = KontoFileLayout.TakaCutting_Trans;

            ProdgridView.InitNewRow += ProdgridView_InitNewRow;
            ProdgridView.KeyDown += ProdgridView_KeyDown;
            prodgridControl.Enter += ProdGridControl_Enter;
            //  gridView1.ValidateRow += GridView1_ValidateRow;
            ProdgridView.MouseUp += ProdgridView_MouseUp;
            ProdgridView.InvalidRowException += ProdgridView_InvalidRowException;
            ProdgridView.ShowingEditor += ProdgridView_ShowingEditor;
            ProdgridView.DoubleClick += ProdgridView_DoubleClick;
            ProdgridView.CustomDrawRowIndicator += ProdgridView_CustomDrawRowIndicator;

            colorRepositoryItemButtonEdit.ButtonClick += ColorRepositoryItemButtonEdit_ButtonClick;

            SelectSimpleButton.Click += SelectSimpleButton_Click;
        }


        #region UDF

        private BeamProdDto PreOpenLookup()
        {
            ProdgridView.GetRow(ProdgridView.FocusedRowHandle);
            if (ProdgridView.GetRow(ProdgridView.FocusedRowHandle) == null)
            {
                ProdgridView.AddNewRow();
            }
            var dr = (BeamProdDto)ProdgridView.GetRow(ProdgridView.FocusedRowHandle);
            return dr;
        }

        private void OpenItemLookup(int _selvalue, BeamProdDto er)
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
        private void OpenColorLookup(int _selvalue, BeamProdDto er)
        {
            var frm = new ColorLkpWindow();
            frm.SelectedValue = _selvalue;
            frm.Tag = MenuId.Color;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                ProdgridView.BeginDataUpdate();
                er.ColorId = frm.SelectedValue;
                er.ColorName = frm.SelectedTex;

                ProdgridView.EndDataUpdate();
                ProdgridView.FocusedColumn = ProdgridView.GetVisibleColumn(colColorName.VisibleIndex + 1);
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
            else if (e.KeyCode == Keys.Delete)
            {
                GridView view = sender as GridView;
                if (ProdgridView.FocusedColumn.FieldName == "ColorName")
                {
                    view.SetRowCellValue(view.FocusedRowHandle, colColorName, string.Empty);
                    view.SetRowCellValue(view.FocusedRowHandle, colColorId, 0);
                }
            }
        }
        private void ProdgridView_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var rw = ProdgridView.GetRow(e.RowHandle) as BeamProdDto;
            rw.Id = -1 * ProdgridView.RowCount;
            rw.VoucherNo = TakaNoTextEdit.Text + "/" + ProdgridView.RowCount;
            rw.ProductId = Convert.ToInt32(productLookup1.SelectedValue);
            rw.ColorId = this.Colorid;
            rw.Remark = PoNoTextEdit.Text;
        }

        private void ProdgridView_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
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
                var _ListView = new TakaCuttingList();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Taka Cutting [View]";
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
                Log.Error(ex, "Taka Cutting Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }
        private void ProductRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
                OpenItemLookup((int)dr.ProductId, dr);
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

        private void SelectSimpleButton_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(productLookup1.SelectedValue) == 0) return;

            var stf = new PendingBeamLoadingView();
            KontoContext _db = new KontoContext();

            var _list = new List<BeamProdDto>();

            var spcol = _db.SpCollections.FirstOrDefault(k => k.Id ==
                        (int)SpCollectionEnum.OutwardBeamProd);
            if (spcol == null)
            {
                _list = (_db.Database.SqlQuery<BeamProdDto>(
                            "dbo.OutwardBeamProd @CompanyId={0} ,@ProductId={1},@IsOk={2}",
                            KontoGlobals.CompanyId, Convert.ToInt32(productLookup1.SelectedValue), 1).ToList());
            }
            else
            {
                _list = (_db.Database.SqlQuery<BeamProdDto>(
                    spcol.Name + " @CompanyId={0} ,@ProductId={1},@IsOk={2}",
                            KontoGlobals.CompanyId, Convert.ToInt32(productLookup1.SelectedValue), 1).ToList());
            }

            if (_list.Count == 0) return;

            stf.ItemList = _list;

            var showDialog = stf.ShowDialog();

            if (stf.SelectedRow != null)
            {
                TakaNoTextEdit.Text = stf.SelectedRow.VoucherNo;
                ColorTextEdit.Text = stf.SelectedRow.ColorName;
                QtyTextEdit.Text = stf.SelectedRow.Qty.ToString();

                ProdId = stf.SelectedRow.Id;
                Colorid = stf.SelectedRow.ColorId;
            }
            PoNoTextEdit.Focus();
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

                rpt.Load(new FileInfo("reg\\Outs\\takacuttingsticker.rdlx"));

                rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);
                doc.Parameters["id"].CurrentValue = this.PrimaryKey;

                doc.Parameters["SingleTaka"].CurrentValue = 'N';

                var frm = new KontoRepViewer(doc);
                frm.Text = "Taka Cutting Print";
                var _tab = this.Parent.Parent as TabControlAdv;
                if (_tab == null) return;
                var pg1 = new TabPageAdv();
                pg1.Text = "Taka Cutting Print";
                _tab.TabPages.Add(pg1);
                _tab.SelectedTab = pg1;
                frm.TopLevel = false;
                frm.Parent = pg1;
                frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                frm.Show();// = true;

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Taka Cutting print");
                MessageBoxAdv.Show(this, "Error While Print !!", "Exception ", ex.ToString());

            }
        }
        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<ProdOutModel>();
            this.Text = "Taka Cutting [Add New]";

            this.ActiveControl = voucherLookup.buttonEdit1;
            voucherLookup.SetDefault();
            voucherDateEdit.EditValue = DateTime.Now;
            VoucherNotextEdit.Text = "New";
            productLookup1.SetEmpty();

            PoNoTextEdit.Text = string.Empty;
            TakaNoTextEdit.Text = string.Empty;
            ColorTextEdit.Text = string.Empty;
            QtyTextEdit.Text = string.Empty;

            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;

            ProdbindingSource.DataSource = new List<BeamProdDto>();

            DelBeamProd = new List<BeamProdDto>();
            this.SelectSimpleButton.Enabled = true;
            this.ProdId = 0;
            this.Colorid = 0;
            voucherLookup.Focus();
        }
        public override void ResetPage()
        {
            base.ResetPage();
            VoucherNotextEdit.Text = "New";

            voucherLookup.SetDefault();
            voucherDateEdit.EditValue = DateTime.Now;
            productLookup1.SetEmpty();

            PoNoTextEdit.Text = string.Empty;
            TakaNoTextEdit.Text = string.Empty;
            ColorTextEdit.Text = string.Empty;
            QtyTextEdit.Text = string.Empty;

            ProdId = 0;
            ProdbindingSource.DataSource = new List<BeamProdDto>();

            DelBeamProd = new List<BeamProdDto>();

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
                var pdata = db.ProdOuts.Find(_key);

                LoadData(pdata);
                createdLabelControl.Text = "Create By: " + pdata.CreateUser;
            }

            this.SelectSimpleButton.Enabled = false;
            this.Text = "Taka Cutting [Edit New]";

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

            if (Convert.ToInt32(productLookup1.SelectedValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "ProductId", Operation = Op.Equals, Value = Convert.ToInt32(productLookup1.SelectedValue) });
            }

            filter.Add(new Filter { PropertyName = "CompId", Operation = Op.Equals, Value = KontoGlobals.CompanyId });
            filter.Add(new Filter { PropertyName = "YearId", Operation = Op.Equals, Value = KontoGlobals.YearId });
            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });

            using (var db = new KontoContext())
            {
                FilterView = db.ProdOuts.Where(ExpressionBuilder.GetExpression<ProdOutModel>(filter))
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

            ProdOutModel _find = new ProdOutModel();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BeamProdDto, ProdModel>().ForMember(x => x.Id, p => p.Ignore());
            });

            var _prodlist = ProdbindingSource.DataSource as List<BeamProdDto>;

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
                            _find = db.ProdOuts.Find(this.PrimaryKey);
                            createuser = _find.CreateUser;
                            createdate = Convert.ToDateTime(_find.CreateDate);
                        }

                        ProdModel pm = db.Prods.Find(ProdId);

                        _find.ProdId = ProdId;
                        _find.Qty = -1 * _prodlist.Sum(k => k.NetWt);

                        _find.VoucherId = Convert.ToInt32(voucherLookup.SelectedValue);
                        _find.ProductId = Convert.ToInt32(productLookup1.SelectedValue);
                        _find.ColorId = pm.ColorId;
                        _find.Remark = PoNoTextEdit.Text;
                        _find.TakaStatus = "CUTTING";
                        _find.GrayMtr = -1 * _prodlist.Sum(k => k.NetWt);
                        _find.CompId = KontoGlobals.CompanyId;
                        _find.YearId = KontoGlobals.YearId;

                        var map = new Mapper(config);
                        if (this.PrimaryKey == 0)
                        {
                            var _srno = DbUtils.NextSerialNo((int)_find.VoucherId, db, 0);
                            _find.VoucherNo = _srno;

                            db.ProdOuts.Add(_find);
                            db.SaveChanges();
                        }
                        else
                        {
                            _find.CreateDate = createdate;
                            _find.CreateUser = createuser;
                        }

                        //DELETED ENTRY FROM DATABASE

                        foreach (var p in DelBeamProd)
                        {
                            var pro = db.Prods.FirstOrDefault(k => k.Id == p.Id);
                            pro.IsDeleted = true;

                            pro.ModifyDate = DateTime.Now;
                            pro.ModifyUser = KontoGlobals.UserName;
                        }

                        //sotock effect
                        var stk = db.StockTranses.Where(k => k.MasterRefId == _find.RowId).ToList();
                        if (stk != null)
                            db.StockTranses.RemoveRange(stk);


                        //Stock Effect from Production 
                        string TableName = "TakaCutting";
                        bool IsIssue = false;

                        //Stock Effect from Production out
                        ProdModel pmodel = new ProdModel();
                        int CutvoucherNo = 1;
                        foreach (var item in _prodlist)
                        {
                            var transid = item.Id;

                            if (item.Id > 0)
                                pmodel = db.Prods.Find(item.Id);
                            else
                                pmodel = new ProdModel();

                            map = new Mapper(config);
                            map.Map(item, pmodel);

                            pmodel.IsOk = true;
                            pmodel.BranchId = KontoGlobals.BranchId;
                            pmodel.DivId = pm.DivId;

                            if (item.Id > 0)
                            {
                                pmodel.SrNo = CutvoucherNo;

                                pmodel.NetWt = (decimal)item.NetWt;
                                pmodel.TwistType = item.TwistType;
                                pmodel.CProductId = _find.ProductId;
                                pmodel.ProductId = _find.ProductId;
                                pmodel.ColorId = item.ColorId;
                                pmodel.ModifyUser = KontoGlobals.UserName;
                                pmodel.ModifyDate = DateTime.Now;
                            }
                            else
                            {
                                pmodel = new ProdModel();
                                
                                //pmodel.VoucherNo = item.VoucherNo;
                                pmodel.NetWt = (decimal)item.NetWt;
                                //pmodel.ProductId = item.ProductId;
                                pmodel.VTypeId = (int)VoucherTypeEnum.TakaCutting;
                                pmodel.VoucherId = _find.VoucherId;
                                pmodel.TwistType = item.TwistType;
                                pmodel.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
                                pmodel.CompId = KontoGlobals.CompanyId;
                                pmodel.YearId = KontoGlobals.YearId;
                                pmodel.BranchId = KontoGlobals.BranchId;
                                pmodel.CreateUser = KontoGlobals.UserName;
                                pmodel.CreateDate = DateTime.Now;

                                pmodel.ProductId = (_find.ProductId);
                                pmodel.CProductId = _find.ProductId;
                               
                                pmodel.ColorId = (item.ColorId > 0) ? item.ColorId : _find.ColorId;
                                pmodel.Remark = _find.Remark;
                                pmodel.VoucherNo = _find.VoucherNo + "/" + CutvoucherNo;
                                CutvoucherNo = CutvoucherNo + 1;

                                pmodel.ProdStatus = "STOCK";
                                pmodel.IssueRefId = _find.Id;
                                pmodel.IssueRefVoucherId = _find.VoucherId;

                                db.Prods.Add(pmodel);
                                db.SaveChanges();
                            }

                            StockEffect.StockTransProdProdOutEntry(pmodel, _find, IsIssue, TableName, KontoGlobals.UserName, db, pmodel.NetWt);
                        }

                        // STOCK EFFECT from  ProdOut
                        IsIssue = true;
                        StockEffect.StockTransProdProdOutEntry(pm, _find, IsIssue, TableName, KontoGlobals.UserName, db, Convert.ToDecimal(QtyTextEdit.Text));
                        db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Taka Cutting Save");
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
                    if (this.voucherLookup.GroupDto.PrintAfterSave && MessageBox.Show("Print Cutting ?", "Print", MessageBoxButtons.YesNo) == DialogResult.Yes)
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

            if (Convert.ToInt32(voucherLookup.SelectedValue) <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Voucher", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherLookup.Focus();
                return false;
            }
           else  if (string.IsNullOrEmpty(TakaNoTextEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Taka Selected", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SelectSimpleButton.Focus();
                return false;
            }
            else if (dt > KontoGlobals.ToDate || dt < KontoGlobals.FromDate)
            {
                MessageBoxAdv.Show(this, "Date out of financial range", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherDateEdit.Focus();
                return false;
            }
            else if (Convert.ToInt32(productLookup1.SelectedValue) <= 0 && this.PrimaryKey <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Product", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                productLookup1.Focus();
                return false;
            }
            else if (prds.Count <= 0)
            {
                MessageBoxAdv.Show(this, "Atleast one transaction must be entered!", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ProdgridView.Focus();
                return false;
            }

            else if(Convert.ToDecimal(QtyTextEdit.Text) < Convert.ToDecimal(ProdgridView.Columns["NetWt"].SummaryItem.SummaryValue))
            {
                MessageBox.Show("Mtrs can not be greater parent taka");
                ProdgridView.Focus();
                return false;
            }

            return true;
        }

        private void LoadData(ProdOutModel pdata)
        {
            KontoContext db = new KontoContext();
            voucherLookup.SelectedValue = pdata.VoucherId;
            VoucherNotextEdit.Text = pdata.VoucherNo;
            PoNoTextEdit.Text = pdata.Remark;
            productLookup1.SelectedValue = pdata.ProductId;
            productLookup1.SetGroup(Convert.ToInt32(pdata.ProductId));

            var _list1 = (from pd in db.Prods
                          join v in db.Vouchers on pd.VoucherId equals v.Id into vou_join
                          from vou in vou_join.DefaultIfEmpty()
                          join col in db.ColorModels on pd.ColorId equals col.Id into col_join
                          from col in col_join.DefaultIfEmpty()
                          where pd.IssueRefId == pdata.Id &&
                       pd.IssueRefVoucherId == pdata.VoucherId && pd.IsActive == true && pd.IsDeleted == false
                          select new BeamProdDto()
                          {
                              Id = pd.Id,
                              ColorId = pd.ColorId,
                              ColorName = col.ColorName,
                              CompId = pd.CompId,
                              Cops = pd.Cops,
                              CopsWt = pd.CopsWt,
                              CurrQty = pd.CurrQty,
                              DivId = pd.DivId,
                              FinQty = pd.FinQty,
                              GradeId = pd.GradeId,
                              GrossWt = pd.GrossWt,
                              IsClose = pd.IsClose,
                              IssueRefId = pd.IssueRefId,
                              IssueRefVoucherId = pd.IssueRefVoucherId,
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
                              YearId = pd.YearId,
                              IsActive = pd.IsActive,
                              IsDeleted = pd.IsDeleted
                          }
                 ).ToList();

            ProdbindingSource.DataSource = _list1;
            voucherDateEdit.EditValue = KontoUtils.IToD(_list1.FirstOrDefault().VoucherDate);

            //For Original taka and Qty
            var _prodModel = db.Prods.FirstOrDefault(k => k.Id == pdata.ProdId);
            TakaNoTextEdit.Text = _prodModel.VoucherNo;
            QtyTextEdit.Text = _prodModel.NetWt.ToString();
            productLookup1.SelectedValue = _prodModel.ProductId;

            ProdId = _prodModel.Id;

            if (_prodModel.ColorId > 0)
                ColorTextEdit.Text = db.ColorModels.FirstOrDefault(k => k.Id == _prodModel.ColorId).ColorName;

            DelBeamProd = new List<BeamProdDto>();
            voucherLookup.Focus();
        }

        #endregion
    }
}