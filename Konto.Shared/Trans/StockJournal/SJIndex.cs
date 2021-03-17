using AutoMapper;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Konto.App.Shared;
using Konto.Core.Shared.Frms;
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
using Konto.Core.Shared.Libs;

namespace Konto.Shared.Trans.StockJournal
{
    public partial class SJIndex : KontoMetroForm
    {
        private List<SJDto> DelTrans = new List<SJDto>();
        private List<ChallanModel> FilterView = new List<ChallanModel>();
        public SJIndex()
        {
            InitializeComponent();
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            productRepositoryItemButtonEdit.ButtonClick += ProductRepositoryItemButtonEdit_ButtonClick;
            gridControl1.ProcessGridKey += GridControl1_ProcessGridKey;
            gridView1.InitNewRow += GridView1_InitNewRow;
            gridView1.CellValueChanged += GridView1_CellValueChanged;
            gridView1.KeyDown += GridView1_KeyDown;
            gridView1.ValidatingEditor += gridView1_ValidatingEditor;
            gridControl1.Enter += GridControl1_Enter;
            gridView1.CustomDrawRowIndicator += GridView1_CustomDrawRowIndicator;
            gridView1.ValidateRow += GridView1_ValidateRow;
            // gridView1.MouseUp += GridView1_MouseUp;
            gridView1.InvalidRowException += GridView1_InvalidRowException;
            gridView1.ShowingEditor += GridView1_ShowingEditor;

            this.MainLayoutFile = KontoFileLayout.StockJournal_Index;
            this.GridLayoutFile = KontoFileLayout.StockJournal_Trans;
            FillLookup();

            this.FirstActiveControl = voucherLookup;
        }

        private void FillLookup()
        {
            using (KontoContext db = new KontoContext())
            {
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
            }
        }
        #region Grid
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
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Stock Journal GridControl KeyDown");
                MessageBoxAdv.Show(this, "Error Lookup Setup !!", "Exception ", ex.ToString());
            }
        }
        private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var rw = gridView1.GetRow(e.RowHandle) as SJDto;
            rw.Id = -1 * gridView1.RowCount;
        }
        private void GridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == null) return;
            if (!(gridView1.GetRow(e.RowHandle) is SJDto er)) return;
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
                var row = view.GetRow(view.FocusedRowHandle) as SJDto;
                view.DeleteRow(view.FocusedRowHandle);
                DelTrans.Add(row);
            }
        }
        private void gridView1_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            GridColumn column = (e as EditFormValidateEditorEventArgs)?.Column ?? view.FocusedColumn;
            if (column.Name != "colIssueQty" && column.Name != "colQty") return;
            decimal IssueQty, Qty;
            if (column.Name == "colIssueQty")
            {
                IssueQty = Convert.ToDecimal(e.Value);
                Qty = Convert.ToDecimal(gridView1.GetRowCellValue(view.FocusedRowHandle, "Qty"));

                if (Qty != 0)
                {
                    e.Value = false;
                }
            }
            else
            {
                Qty = Convert.ToDecimal(e.Value);
                IssueQty = Convert.ToDecimal(gridView1.GetRowCellValue(view.FocusedRowHandle, "IssueQty"));

                if (IssueQty != 0)
                {
                    e.Value = false;
                }
            }

        }
        private void GridControl1_Enter(object sender, EventArgs e)
        {
            gridView1.FocusedColumn = gridView1.VisibleColumns[0];
        }
        private void GridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.Appearance.ForeColor = Color.FromArgb(227, 22, 91);
            if (e.RowHandle < 0)
                e.Info.DisplayText = "";
            else
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        private void GridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            GridView view = sender as GridView;

            int product = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, colProductId));
            int unit = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, colUomId));
            decimal receipt = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, colQty));
            decimal issue = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, colIssueQty));

            if (product == 0)
            {
                e.Valid = false;
                view.SetColumnError(colProductName, "Invalid Product");
            }
            else if (unit == 0)
            {
                e.Valid = false;
                view.SetColumnError(colUomId, "Invalid Unit");
            }
            else if (receipt == 0 && issue==0)
            {
                e.Valid = false;
                view.SetColumnError(colQty, "Invalid Qty");
            }
        }
        private void GridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }
        private void GridView1_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var itm = gridView1.GetFocusedRow() as BillTransDto;
            if (itm == null) return;
            if (!"Issue,Qty,ProductName,UomId".Contains(gridView1.FocusedColumn.FieldName)) return;
            if (Convert.ToInt32(itm.RefId) > 0)
                e.Cancel = true;
        }

        private void ProductRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
                OpenItemLookup(dr.ProductId, dr);
        }
        #endregion
        #region UDF
        private SJDto PreOpenLookup()
        {
            gridView1.GetRow(gridView1.FocusedRowHandle);
            if (gridView1.GetRow(gridView1.FocusedRowHandle) == null)
            {
                gridView1.AddNewRow();
            }
            var dr = (SJDto)gridView1.GetRow(gridView1.FocusedRowHandle);
            return dr;
        }
        private void OpenItemLookup(int _selvalue, SJDto er)
        {
            var frm = new ProductLkpWindow();
            frm.Tag = MenuId.Product_Master;
            frm.SelectedValue = _selvalue;
            //frm.PTypeId = ProductTypeEnum.;
            frm.VoucherType = VoucherTypeEnum.PurchaseInvoice;

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
        public void GridCalculation(SJDto er)
        {
            if (er.ReceiveQty > 0)
            {
                er.Gross = decimal.Round(er.ReceiveQty * er.Rate, 2, MidpointRounding.AwayFromZero);
            }
            else if (er.IssueQty > 0)
            {
                er.Gross = decimal.Round(er.IssueQty * er.Rate, 2, MidpointRounding.AwayFromZero);
            }

            er.Total = er.Gross;
        }
        public override void SaveDataAsync(bool newmode)
        {
            bool IsSaved = false;
            if (!ValidateData()) return;

            var _find = new ChallanModel();
            var _translist = SJTbindingSource.DataSource as List<SJDto>;
            List<ChallanTransModel> Trans = new List<ChallanTransModel>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SJDto, ChallanTransModel>().ForMember(x => x.Id, p => p.Ignore());
            });

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
                        _find.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
                        _find.RcdDate = DateTime.Now;
                        _find.VoucherId = Convert.ToInt32(voucherLookup.SelectedValue);
                        _find.ProcessId = Convert.ToInt32(processLookup.SelectedValue);
                        _find.Remark = remarkTextEdit.Text;
                        _find.ChallanNo = "NA";

                        _find.BranchId = KontoGlobals.BranchId;
                        _find.CompId = KontoGlobals.CompanyId;
                        _find.YearId= KontoGlobals.YearId;

                        if (_find.Id == 0)
                        {
                            _find.VoucherNo = DbUtils.NextSerialNo(_find.VoucherId, db);

                            if (DbUtils.CheckExistVoucherNo(_find.VoucherId, _find.VoucherNo, db, _find.Id))
                            {
                                MessageBox.Show("Duplicate Voucher No Not Allowed");
                                _tran.Rollback();
                                return;
                            }
                            _find.CreateDate = DateTime.Now;
                            _find.CreateUser = KontoGlobals.UserName;

                            db.Challans.Add(_find);
                            db.SaveChanges();
                        }
                        var map = new Mapper(config);

                        foreach (var item in _translist)
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

                            tranModel.Qty = item.ReceiveQty;
                            if (tranModel.Id <= 0)
                            {
                                db.ChallanTranses.Add(tranModel);
                                db.SaveChanges();
                            }
                            item.Id = tranModel.Id;
                            Trans.Add(tranModel);
                        }
                        //delete item from  trans
                        foreach (var item in DelTrans)
                        {
                            if (item.Id == 0) continue;
                            var _model = db.BillTrans.Find(item.Id);
                            _model.IsDeleted = true;
                        }

                        var stk = db.StockTranses.Where(k => k.MasterRefId == _find.RowId).ToList();
                        if (stk != null)
                            db.StockTranses.RemoveRange(stk);

                        //stock effect
                        if (!Trans.Any(x => x.RefId > 0))
                        {
                            foreach (var item in Trans)
                            {
                                bool IsIssue = false;
                                string TableName = "StockJournal";
                                var stockReq = db.Products.FirstOrDefault(k => k.Id == item.ProductId).StockReq;
                                if (stockReq == "No")
                                {
                                    continue;
                                }
                                if (item.Qty > 0)
                                    IsIssue = false;
                                else
                                {
                                    IsIssue = true;
                                }
                                StockEffect.StockTransChlnEntry(_find, item, IsIssue, TableName, KontoGlobals.UserName, db);
                            }
                        }

                        db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Purchase Save" + " Save");
                        MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
                    }
                }
            }

            if (IsSaved)
            {
                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage + " Voucher No.: " + _find.VoucherNo, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (!this.OpenForLookup && newmode)
                {
                    base.SaveDataAsync(newmode);
                    this.ResetPage();
                    this.NewRec();
                    voucherDateEdit.Focus();
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
            var trans = SJTbindingSource.DataSource as List<SJDto>;
            if (Convert.ToInt32(voucherLookup.SelectedValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Voucher", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherLookup.Focus();
                return false;
            }
            if (Convert.ToInt32(processLookup.SelectedValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Process", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                processLookup.Focus();
                return false;
            }
            else if (dt > KontoGlobals.ToDate || dt < KontoGlobals.FromDate)
            {
                MessageBoxAdv.Show(this, "Voucher date out of financial range", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherDateEdit.Focus();
                return false;
            }
            else if (gridView1.RowCount == 1)
            {
                MessageBoxAdv.Show(this, "At Least One Product Should be Entered", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }
            else if (trans.Any(x => x.ProductId == 0))
            {
                MessageBoxAdv.Show(this, "Invalid Product Selection", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }
            else if (trans.Any(x => x.UomId == 0))
            {
                MessageBoxAdv.Show(this, "Invalid Unit Selection", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }
            else if (trans.Sum(x => x.ReceiveQty) == 0)
            {
                if (trans.Sum(x => x.IssueQty) == 0)
                {
                    MessageBoxAdv.Show(this, "At least one qty or IssueQty should be entered", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    gridView1.Focus();
                    return false;
                }
            }
            else if (trans.Sum(x => x.IssueQty) == 0)
            {
                if (trans.Sum(x => x.ReceiveQty) == 0)
                {
                    MessageBoxAdv.Show(this, "At least one qty or IssueQty should be entered", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    gridView1.Focus();
                    return false;
                }
            }
            return true;
        }

        #endregion
        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveDataAsync(true);
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Stock Journal Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                voucherDateEdit.Focus();
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1 && tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as SJList;
                _list.ActiveControl = _list.KontoGrid;
                this.Text = "Stock Journal [View]";
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new SJList();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Stock Journal [View]";
            }
        }


        #region Parent Function

        public override void Print()
        {
            base.Print();

        }
        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<ChallanModel>();
            this.Text = "Stock Journal [Add New]";
            voucherNoTextEdit.Text = "New";
            voucherDateEdit.EditValue = DateTime.Now;
          
            remarkTextEdit.Text = string.Empty;
            this.ActiveControl = voucherLookup.buttonEdit1;
            voucherLookup.SetDefault();

            processLookup.SetEmpty();

            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;

            DelTrans = new List<SJDto>();
            this.SJTbindingSource.DataSource = new List<SJDto>();

            voucherDateEdit.Focus();
        }
        public override void ResetPage()
        {
            base.ResetPage();

            voucherDateEdit.DateTime = DateTime.Now;
           
            voucherNoTextEdit.Text = string.Empty;
            remarkTextEdit.Text = string.Empty;

            DelTrans = new List<SJDto>();
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


            if (Convert.ToInt32(voucherLookup.SelectedValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "VoucherId", Operation = Op.Equals, Value = Convert.ToInt32(voucherLookup.SelectedValue) });
            }
            if (!string.IsNullOrEmpty(voucherNoTextEdit.Text.Trim()))
            {
                filter.Add(new Filter { PropertyName = "VoucherNo", Operation = Op.Equals, Value = voucherNoTextEdit.Text.Trim() });
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

        private void LoadData(ChallanModel model)
        {
            voucherLookup.SelectedValue = model.VoucherId;
            voucherLookup.SetGroup(model.VoucherId);
            voucherDateEdit.EditValue = KontoUtils.IToD(model.VoucherDate);

            voucherNoTextEdit.Text = model.VoucherNo; 
          
            processLookup.SelectedValue = model.ProcessId;
            processLookup.SetValue();

            remarkTextEdit.Text = model.Remark;

            createdLabelControl.Text = "Created By: " + model.CreateUser + " [ " + model.CreateDate + " ]";
            modifyLabelControl.Text = "Modified By: " + model.ModifyUser + " [ " + model.ModifyDate ?? string.Empty + " ]";

            using (var _context = new KontoContext())
            {
                var _list = (from ct in _context.ChallanTranses
                             join pd in _context.Products on ct.ProductId equals pd.Id into join_pd
                             from pd in join_pd.DefaultIfEmpty()
                             join u in _context.Uoms on ct.UomId equals u.Id into join_uom
                             from u in join_uom.DefaultIfEmpty()
                             orderby ct.Id
                             where ct.IsActive == true && ct.IsDeleted == false &&
                             ct.ChallanId == model.Id
                             select new SJDto()
                             {
                                 Id = ct.Id,
                                 ChallanId = ct.ChallanId,
                                 Gross = ct.Gross,
                                 ProductId = (int)ct.ProductId,
                                 ProductName = pd.ProductName,
                                 ReceiveQty = ct.Qty,
                                 IssueQty= ct.IssueQty,
                                 Rate = ct.Rate,
                                 Remark = ct.Remark,
                                 Total = ct.Total,
                                 UomId = (int)ct.UomId,
                             }).ToList();
                 
                this.SJTbindingSource.DataSource = _list;
            }
        }

        #endregion
    }
}
