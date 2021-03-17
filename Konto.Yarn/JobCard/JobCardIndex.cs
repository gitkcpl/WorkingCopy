using AutoMapper;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraRichEdit.Model;
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

namespace Konto.Yarn.JobCard
{
    public partial class JobCardIndex : KontoMetroForm
    {
        private List<JobCardModel> FilterView = new List<JobCardModel>();
        private List<JobCardTransDto> DelClrTrans = new List<JobCardTransDto>();
        private List<JobCardTransDto> DelChemicalTrans = new List<JobCardTransDto>();
        private List<JobCardTransDto> DelYarnTrans = new List<JobCardTransDto>();

        //bool IsChemicalchange = true;
        //bool IsColorchange = true;
        //bool IsBatchchange = true;

        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        public JobCardIndex()
        {
            InitializeComponent();

            FillLookup();
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            this.Load += JobCardIndex_Load;

            this.MainLayoutFile = KontoFileLayout.YarnJobCard_Index;
            this.GridLayoutFile = KontoFileLayout.YarnJobCard_Trans;

            ClrConsumptiongridControl.ProcessGridKey += ClrConsumptiongridControl_ProcessGridKey;
            ClrConsumptiongridView.InitNewRow += ClrConsumptiongridView_InitNewRow;
            ClrConsumptiongridView.CellValueChanged += ClrConsumptiongridView_CellValueChanged;
            ClrConsumptiongridView.KeyDown += ClrConsumptiongridView_KeyDown;
            ClrConsumptiongridControl.Enter += ClrConsumptiongridControl_Enter;
            //  gridView1.ValidateRow += GridView1_ValidateRow;
            ClrConsumptiongridView.MouseUp += ClrConsumptiongridView_MouseUp;
            ClrConsumptiongridView.InvalidRowException += ClrConsumptiongridView_InvalidRowException;
            ClrConsumptiongridView.ShowingEditor += ClrConsumptiongridView_ShowingEditor;
            ClrConsumptiongridView.DoubleClick += ClrConsumptiongridView_DoubleClick;
            ClrConsumptiongridView.CustomDrawRowIndicator += ClrConsumptiongridView_CustomDrawRowIndicator;

            OrderNobuttonEdit.ButtonClick += OrderNobuttonEdit_ButtonClick;
            OrderNobuttonEdit.KeyDown += OrderNobuttonEdit_KeyDown;
            GreyLookup.SelectedValueChanged += GreyLookup_SelectedValueChanged;

            BatchNoLookUpEdit.KeyDown += BatchNoLookUpEdit_KeyDown;
            ChemicallookUpEdit.KeyDown += ChemicallookUpEdit_KeyDown;
            colorLookup1.KeyDown += ColorLookup1_KeyDown;
            colorLookup1.ShownPopup += ColorLookup1_ShownPopup;
            QtyspinEdit.KeyDown += QtyspinEdit_KeyDown;
            //  BatchNoLookUpEdit.EditValueChanged += BatchNoLookUpEdit_EditValueChanged;
            //  ChemicallookUpEdit.EditValueChanged += ChemicallookUpEdit_EditValueChanged;
            // colorLookup1.SelectedValueChanged += ColorLookup1_SelectedValueChanged;
            // QtyspinEdit.ValueChanged += QtyspinEdit_ValueChanged;
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
                var _ChemicalLists = (from p in db.RCPUIs
                                      where p.IsActive && !p.IsDeleted
                                      select new BaseLookupDto()
                                      {
                                          DisplayText = p.VoucherNo,
                                          Id = p.Id
                                      }).ToList();
                var _BatchNoLists = (from p in db.jobCards
                                     join v in db.Vouchers on p.VoucherId equals v.Id into join_v
                                     from v in join_v.DefaultIfEmpty()
                                     where p.IsActive && !p.IsDeleted && v.VTypeId == (int)VoucherTypeEnum.Batch
                                     select new BaseLookupDto()
                                     {
                                         DisplayText = p.VoucherNo,
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
                ItemTyperepositoryItemLookUpEdit.DataSource = cbp;
                ChemicallookUpEdit.Properties.DataSource = _ChemicalLists;
                MachineNolookUpEdit.Properties.DataSource = _macLists;
                BatchNoLookUpEdit.Properties.DataSource = _BatchNoLists;
            }
        }
        private JobCardTransDto PreOpenLookup()
        {
            ClrConsumptiongridView.GetRow(ClrConsumptiongridView.FocusedRowHandle);
            if (ClrConsumptiongridView.GetRow(ClrConsumptiongridView.FocusedRowHandle) == null)
            {
                ClrConsumptiongridView.AddNewRow();
            }
            var dr = (JobCardTransDto)ClrConsumptiongridView.GetRow(ClrConsumptiongridView.FocusedRowHandle);
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

                ClrConsumptiongridView.FocusedColumn = ClrConsumptiongridView.GetNearestCanFocusedColumn(ClrConsumptiongridView.FocusedColumn);
            }
        }
        private void ColorChange(decimal qty)
        {

            using (KontoContext db = new KontoContext())
            {
                JobCardModel jobCardModel = new JobCardModel();
                //if (this.PrimaryKey > 0)
                //{
                //    jobCardModel = db.jobCards.FirstOrDefault(k => k.Id == this.PrimaryKey);

                //    if (Convert.ToInt32(ChemicallookUpEdit.EditValue) != jobCardModel.ColorId)
                //        IsColorchange = true;
                //}

                int id = Convert.ToInt32(colorLookup1.SelectedValue);
                var _chemicalLists = (from p in db.RcpuiTrans
                                      join r in db.RCPUIs on p.RCPUIId equals r.Id into join_r
                                      from r in join_r.DefaultIfEmpty()
                                      join pr in db.Products on p.ProductId equals pr.Id into join_bo
                                      from pr in join_bo.DefaultIfEmpty()
                                      where p.IsActive && !p.IsDeleted && r.ColorId == id
                                      select new JobCardTransDto()
                                      {
                                          Id = 0,
                                          RefId = p.RCPUIId,
                                          RefTransId = p.Id,
                                          ItemId = p.ProductId,
                                          ItemName = pr.ProductName,
                                          ColorPer = p.ColorPer,
                                          ConsumeQty = (qty * p.ColorPer) / r.Qty,
                                          //Rate=pr,
                                          //Amount=pr.SaleRateTaxInc * ((qty * p.ColorPer) / r.Qty) ,
                                          LotNo = p.ColorCategory
                                      }).ToList();

                foreach (var item in _chemicalLists)
                {
                    var LatestRate = (from p in db.ChallanTranses
                                      join c in db.Challans on p.ChallanId equals c.Id into join_c
                                      from c in join_c.DefaultIfEmpty()
                                      join v in db.Vouchers on c.VoucherId equals v.Id into join_v
                                      from v in join_v.DefaultIfEmpty()
                                      where p.IsActive && !p.IsDeleted && v.VTypeId == (int)VoucherTypeEnum.Inward
                                      select new GrnTransDto()
                                      {
                                          Id = p.Id,
                                          Rate = p.Rate
                                      }).ToList();

                    if (LatestRate.Count > 0)
                    {
                        var lastId = LatestRate.Max(p => p.Id);
                        item.Rate = LatestRate.FirstOrDefault(k => k.Id == lastId).Rate;
                    }

                    if (item.Rate <= 0 || item.Rate == null)
                    {
                        var lastprice = db.Prices.FirstOrDefault(k => k.ProductId == item.ItemId && k.IsDeleted == false && k.IsActive == true);
                        if (lastprice != null)
                            item.Rate = lastprice.SaleRate;
                    }
                    if (item.Rate > 0 && qty > 0)
                    {
                        item.Amount = item.Rate * item.ConsumeQty;
                    }
                }
                ClrConsbindingSource.DataSource = _chemicalLists;
            }
        }
        private void Chemicalchange(decimal qty)
        {
            using (KontoContext db = new KontoContext())
            {
                JobCardModel jobCardModel = new JobCardModel();

                int id = Convert.ToInt32(ChemicallookUpEdit.EditValue);
                var _chemicalLists = (from p in db.RcpuiTrans
                                      join r in db.RCPUIs on p.RCPUIId equals r.Id into join_r
                                      from r in join_r.DefaultIfEmpty()
                                      join pr in db.Products on p.ProductId equals pr.Id into join_bo
                                      from pr in join_bo.DefaultIfEmpty()
                                      where p.IsActive && !p.IsDeleted && p.RCPUIId == id
                                      select new JobCardTransDto()
                                      {
                                          Id = 0,
                                          RefId = p.RCPUIId,
                                          RefTransId = p.Id,
                                          ItemId = p.ProductId,
                                          ItemName = pr.ProductName,
                                          ConsumeQty = p.ColorPer, //Decimal.Round((decimal)((qty * p.ColorPer) / r.Qty), 4),
                                          //Rate=pr,
                                          //Amount=pr.SaleRateTaxInc * ((qty * p.ColorPer) / r.Qty) ,
                                          LotNo = p.ColorCategory
                                      }).ToList();

                foreach (var item in _chemicalLists)
                {
                    var LatestRate = (from p in db.ChallanTranses
                                      join c in db.Challans on p.ChallanId equals c.Id into join_c
                                      from c in join_c.DefaultIfEmpty()
                                      join v in db.Vouchers on c.VoucherId equals v.Id into join_v
                                      from v in join_v.DefaultIfEmpty()
                                      where p.IsActive && !p.IsDeleted && v.VTypeId == (int)VoucherTypeEnum.Inward
                                      select new GrnTransDto()
                                      {
                                          Id = p.Id,
                                          Rate = p.Rate
                                      }).ToList();

                    if (LatestRate.Count > 0)
                    {
                        var lastId = LatestRate.Max(p => p.Id);
                        item.Rate = LatestRate.FirstOrDefault(k => k.Id == lastId).Rate;
                    }
                    if (item.Rate <= 0 || item.Rate == null)
                    {
                        var lastprice = db.Prices.FirstOrDefault(k => k.ProductId == item.ItemId && k.IsDeleted == false && k.IsActive == true);
                        if (lastprice != null)
                            item.Rate = lastprice.SaleRate;
                    }
                    if (item.Rate > 0 && qty > 0)
                    {
                        item.Amount = Decimal.Round((decimal)(item.Rate * item.ConsumeQty), 2);
                    }
                }
                ChemicalbindingSource.DataSource = _chemicalLists;
            }
        }
        private void BatchChange(decimal qty)
        {
            using (KontoContext _db = new KontoContext())
            {
                JobCardModel jobCardModel = new JobCardModel();
                //if (this.PrimaryKey > 0)
                //{
                //    jobCardModel = _db.jobCards.FirstOrDefault(k => k.Id == this.PrimaryKey);

                //    if (Convert.ToInt32(ChemicallookUpEdit.EditValue) != jobCardModel.BatchId)
                //        IsBatchchange = true;
                //}
                int? jId = Convert.ToInt32(BatchNoLookUpEdit.EditValue);

                //from Batch 
                var ProdTrans = (from jct in _db.jobCardTrans
                                 join jc in _db.jobCards on jct.JobCardId equals jc.Id into join_jct
                                 from jc in join_jct.DefaultIfEmpty()
                                 join bo in _db.Products on jct.ItemId equals bo.Id into join_bo
                                 from bo in join_bo.DefaultIfEmpty()
                                 join v in _db.Vouchers on jc.VoucherId equals v.Id into join_v
                                 from v in join_v.DefaultIfEmpty()
                                 orderby jc.Id
                                 where !jc.IsDeleted
                                 && jc.Id == jId && !jct.IsDeleted
                                 && jct.IsActive == true && v.VTypeId == (int)VoucherTypeEnum.Batch//Batch
                                 select new JobCardTransDto()
                                 {
                                     Id = 0,
                                     RefId = jc.Id,
                                     RefTransId = jct.Id,
                                     ItemId = jct.ItemId,
                                     ItemName = bo.ProductName,
                                     LotNo = jct.LotNo,
                                     Per = jct.Per,
                                     ConsumeQty = Decimal.Round(((decimal)jct.Per * qty) / 100, 4)
                                 }).ToList();
                if (ProdTrans != null)
                    YarnbindingSource.DataSource = ProdTrans;
            }
        }
        private void ShowOrderDetail()
        {
            //Opem Popup
            KontoContext _db = new KontoContext();
            List<PendingOrderDto> list;
            //var spcol = _db.SpCollections.FirstOrDefault(k => k.Id == (int)SpCollectionEnum.PendingOrderonYarnJobCard);
            //if (spcol == null)
            //{
                list = (_db.Database.SqlQuery<PendingOrderDto>(
               "dbo.PendingOrderonYarnJobCard @CompanyId={0},@VoucherTypeID={1}",
               KontoGlobals.CompanyId, (int)VoucherTypeEnum.SalesOrder).ToList());
            //}
            //else
            //{
            //    list = (_db.Database.SqlQuery<PendingOrderDto>(
            //     spcol.Name + " @CompanyId={0},@VoucherTypeID={1}",
            //     KontoGlobals.CompanyId, (int)VoucherTypeEnum.SalesOrder).ToList());
            //}


            if (list.Count == 0) return;

            var stf = new PendingOrderView();
            stf.ItemList = list;
            var showDialog = stf.ShowDialog();
            if (stf.DialogResult == DialogResult.OK)
            {
                //OrderNobuttonEdit.SelectedText = stf.SelectedRow.VoucherNo;
                OrderNobuttonEdit.EditValue = stf.SelectedRow.Id;
                accLookup1.SelectedValue = stf.SelectedRow.DelvAccId;
                accLookup1.SetAcc(stf.SelectedRow.DelvAccId);

                if (stf.SelectedRow.ProductId > 0)
                {
                    FinishproductLookup.SelectedValue = stf.SelectedRow.ProductId;
                    FinishproductLookup.SetGroup((int)stf.SelectedRow.ProductId);
                }
                colorLookup1.SelectedValue = stf.SelectedRow.ColorId;
                colorLookup1.SetGroup();

                if (stf.SelectedRow.TotalQty > 0)
                    OrderQtyspinEdit.Value = (decimal)stf.SelectedRow.TotalQty;

                accLookup1.Enabled = false;
                FinishproductLookup.Enabled = false;
                //colorLookup1.Enabled = false;
                //OrderQtyspinEdit.Enabled = false;
            }
        }
        #endregion

        #region BTranspgridView
        private void ClrConsumptiongridView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var itm = ClrConsumptiongridView.GetFocusedRow() as JobCardTransDto;
            if (itm == null) return;
            if (!"Qty,ProductName".Contains(ClrConsumptiongridView.FocusedColumn.FieldName)) return;
        }
        private void ClrConsumptiongridView_MouseUp(object sender, MouseEventArgs e)
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
        private void ClrConsumptiongridView_DoubleClick(object sender, EventArgs e)
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
        private void ClrConsumptiongridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.Appearance.ForeColor = Color.FromArgb(227, 22, 91);
            if (e.RowHandle < 0)
                e.Info.DisplayText = "";
            else
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        private void ClrConsumptiongridControl_Enter(object sender, EventArgs e)
        {
            ClrConsumptiongridView.FocusedColumn = ClrConsumptiongridView.VisibleColumns[0];
        }
        private void ClrConsumptiongridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == null) return;
            if (!(ClrConsumptiongridView.GetRow(e.RowHandle) is JobCardTransDto er)) return;
        }
        private void ClrConsumptiongridView_KeyDown(object sender, KeyEventArgs e)
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
                DelClrTrans.Add(row);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                GridView view = sender as GridView;
                if (ClrConsumptiongridView.FocusedColumn.FieldName == "ItemName")
                {
                    view.SetRowCellValue(view.FocusedRowHandle, colItemName, string.Empty);
                    view.SetRowCellValue(view.FocusedRowHandle, colItemId, 0);
                }
            }
        }
        private void ClrConsumptiongridView_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var rw = ClrConsumptiongridView.GetRow(e.RowHandle) as JobCardTransDto;
            rw.Id = -1 * ClrConsumptiongridView.RowCount;
        }
        private void ClrConsumptiongridControl_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                var dr = PreOpenLookup();
                if (dr == null) return;
                if (ClrConsumptiongridView.FocusedColumn.FieldName == "LotNo")
                {
                    if (dr.ItemName == null)
                    {
                        if (e.KeyCode == Keys.Enter)
                        {
                        }
                    }
                }
                else if (ClrConsumptiongridView.FocusedColumn.FieldName == "Per")
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        var trans = ClrConsbindingSource.DataSource as List<JobCardTransDto>;
                        decimal? per = trans.Sum(k => k.Per);
                        if (per > 100)
                        {
                            dr.Per = 0;
                            ClrConsumptiongridView.FocusedColumn = ClrConsumptiongridView.GetVisibleColumn(colColorPer.VisibleIndex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "JobCard GridControl KeyDown");
                MessageBoxAdv.Show(this, "Error Lookup Setup !!", "Exception ", ex.ToString());
            }
        }
        private void ClrConsumptiongridView_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
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
                divLookUpEdit.Focus();
                return;
            }
            else if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new JobCardListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "JobCard [View]";
            }
        }
        private void OrderNobuttonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            ShowOrderDetail();
        }

        private void OrderNobuttonEdit_KeyDown(object sender, KeyEventArgs e)
        {
            ShowOrderDetail();
        }

        private void QtyspinEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (QtyspinEdit.EditValue == null) return;

            decimal qty = QtyspinEdit.Value;

            if (ChemicallookUpEdit.EditValue != null)
            {
                Chemicalchange(qty);

            }
            if (colorLookup1.SelectedValue != null)
            {
                ColorChange(qty);
            }
            if (BatchNoLookUpEdit.EditValue != null || Convert.ToInt32(BatchNoLookUpEdit.EditValue) > 0)
            {
                BatchChange(qty);
            }
        }

        private void ColorLookup1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (colorLookup1.SelectedValue == null) return;
            decimal qty = 0;
            //if (QtyspinEdit.Value == null || QtyspinEdit.Value == 0) return;

            qty = Convert.ToDecimal(QtyspinEdit.Value);
            ColorChange(qty);

        }

        private void ColorLookup1_ShownPopup(object sender, EventArgs e)
        {
            if (colorLookup1.SelectedValue == null) return;
            decimal qty = 0;
            //if (QtyspinEdit.Value == null || QtyspinEdit.Value == 0) return;

            qty = Convert.ToDecimal(QtyspinEdit.Value);
            ColorChange(qty);
        }

        private void GreyLookup_SelectedValueChanged(object sender, EventArgs e)
        {
            if (GreyLookup.SelectedValue == null) return;
            using (KontoContext db = new KontoContext())
            {
                int productid = Convert.ToInt32(GreyLookup.SelectedValue);
                var _LotNoLists = (from p in db.Prods
                                   where p.IsActive && !p.IsDeleted && p.ProductId == productid && !string.IsNullOrEmpty(p.LotNo)
                                   select new BaseLookupDto()
                                   {
                                       DisplayText = p.LotNo,
                                       Id = p.Id
                                   }).ToList();
                LotNoLookUpEdit.Properties.DataSource = _LotNoLists;
            }
        }

        private void ChemicallookUpEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (ChemicallookUpEdit.EditValue == null) return;
            decimal qty = 0;
            // if (QtyspinEdit.Value == null || QtyspinEdit.Value == 0) return;

            qty = Convert.ToDecimal(QtyspinEdit.Value);
            Chemicalchange(qty);
        }

        private void BatchNoLookUpEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (BatchNoLookUpEdit.EditValue == null) return;
            decimal qty = 0;
            //if (QtyspinEdit.Value == null || QtyspinEdit.Value == 0) return;

            qty = Convert.ToDecimal(QtyspinEdit.Value);
            BatchChange(qty);
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveDataAsync(true);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Jobcard Save");
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
            this.Text = "JobCard [Add New]";

            TyperadioGroup.SelectedIndex = 0;
            divLookUpEdit.EditValue = 1;
            this.ActiveControl = voucherLookup.buttonEdit1;
            voucherLookup.SetDefault();
            voucherDateEdit.EditValue = DateTime.Now;

            VoucherNotextEdit.Text = "New";

            DyeingTyperadioGroup.SelectedIndex = 0;
            MachineNolookUpEdit.EditValue = null;
            ChemicallookUpEdit.EditValue = null;

            OrderNobuttonEdit.EditValue = null;
            accLookup1.SetEmpty();
            FinishproductLookup.SetEmpty();
            colorLookup1.SetEmpty();
            colorLookup1.SetGroup();
            OrderQtyspinEdit.Value = 0;
            QtyspinEdit.Value = 0;

            GreyLookup.SetEmpty();
            LotNoLookUpEdit.EditValue = null;
            BatchNoLookUpEdit.EditValue = null;
            gradeLookup1.SetEmpty();
            NoOfConesspinEdit.Value = 0;
            QtyspinEdit.Value = 0;
            RemarkTextBoxExt.Text = string.Empty;
            TolPerSpinEdit.Value = 0;
            TolPerSpinEdit.Value = 0;
            accLookup1.Enabled = true;
            FinishproductLookup.Enabled = true;
            colorLookup1.Enabled = true;
            OrderQtyspinEdit.Enabled = true;

            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;

            ClrConsbindingSource.DataSource = new List<JobCardTransDto>();
            ChemicalbindingSource.DataSource = new List<JobCardTransDto>();
            YarnbindingSource.DataSource = new List<JobCardTransDto>();

            DelClrTrans = new List<JobCardTransDto>();
            DelChemicalTrans = new List<JobCardTransDto>();
            DelYarnTrans = new List<JobCardTransDto>();

            divLookUpEdit.Focus();
        }
        public override void ResetPage()
        {
            base.ResetPage();
            TyperadioGroup.SelectedIndex = 0;
            divLookUpEdit.EditValue = 1;
            this.ActiveControl = voucherLookup.buttonEdit1;
            voucherLookup.SetDefault();
            voucherDateEdit.EditValue = DateTime.Now;

            VoucherNotextEdit.Text = "New";

            DyeingTyperadioGroup.SelectedIndex = 0;
            MachineNolookUpEdit.EditValue = null;
            ChemicallookUpEdit.EditValue = null;

            OrderNobuttonEdit.EditValue = null;
            accLookup1.SetEmpty();
            FinishproductLookup.SetEmpty();
            colorLookup1.SetEmpty();
            colorLookup1.SetGroup();
            OrderQtyspinEdit.Value = 0;
            QtyspinEdit.Value = 0;
            TolPerSpinEdit.Value = 0;

            GreyLookup.SetEmpty();
            LotNoLookUpEdit.EditValue = null;
            BatchNoLookUpEdit.EditValue = null;
            gradeLookup1.SetEmpty();
            NoOfConesspinEdit.Value = 0;
            QtyspinEdit.Value = 0;
            RemarkTextBoxExt.Text = string.Empty;
            TolPerSpinEdit.Value = 0;

            accLookup1.Enabled = true;
            FinishproductLookup.Enabled = true;
            colorLookup1.Enabled = true;
            OrderQtyspinEdit.Enabled = true;

            ClrConsbindingSource.DataSource = new List<JobCardTransDto>();
            ChemicalbindingSource.DataSource = new List<JobCardTransDto>();
            YarnbindingSource.DataSource = new List<JobCardTransDto>();

            DelClrTrans = new List<JobCardTransDto>();
            DelChemicalTrans = new List<JobCardTransDto>();
            DelYarnTrans = new List<JobCardTransDto>();

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
            if (Convert.ToInt32(FinishproductLookup.SelectedValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "ProductId", Operation = Op.Equals, Value = Convert.ToInt32(FinishproductLookup.SelectedValue) });
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

            var _Translist = ClrConsbindingSource.DataSource as List<JobCardTransDto>;
            var _ChemicalList = ChemicalbindingSource.DataSource as List<JobCardTransDto>;
            var _YarnList = YarnbindingSource.DataSource as List<JobCardTransDto>;

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

                        _find.Type = TyperadioGroup.Text;

                        if (divLookUpEdit.EditValue != null)
                            _find.DivId = Convert.ToInt32(divLookUpEdit.EditValue);

                        _find.VoucherId = Convert.ToInt32(voucherLookup.SelectedValue);

                        _find.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));

                        _find.DyeingType = DyeingTyperadioGroup.Text;

                        if (MachineNolookUpEdit.EditValue != null)
                            _find.MachineId = Convert.ToInt32(MachineNolookUpEdit.EditValue);

                        if (ChemicallookUpEdit.EditValue != null)
                            _find.RPUIId = Convert.ToInt32(ChemicallookUpEdit.EditValue);

                        if (OrderNobuttonEdit.EditValue != null)
                            _find.OrderId = Convert.ToInt32(OrderNobuttonEdit.EditValue);

                        if (accLookup1.SelectedValue != null)
                            _find.AccountId = Convert.ToInt32(accLookup1.SelectedValue);

                        if (FinishproductLookup.SelectedValue != null)
                            _find.ProductId = Convert.ToInt32(FinishproductLookup.SelectedValue);

                        if (colorLookup1.SelectedValue != null)
                            _find.ColorId = Convert.ToInt32(colorLookup1.SelectedValue);

                        _find.OrderQty = OrderQtyspinEdit.Value;

                        if (GreyLookup.SelectedValue != null)
                            _find.GrayItemId = Convert.ToInt32(GreyLookup.SelectedValue);

                        if (LotNoLookUpEdit.EditValue != null)
                            _find.LotNo = LotNoLookUpEdit.EditValue.ToString();

                        if (BatchNoLookUpEdit.EditValue != null)
                            _find.BatchId = Convert.ToInt32(BatchNoLookUpEdit.EditValue);

                        if (gradeLookup1.SelectedValue != null)
                            _find.GradeId = Convert.ToInt32(gradeLookup1.SelectedValue);

                        if (NoOfConesspinEdit.Value != null)
                            _find.NoOfCones = Convert.ToInt32(NoOfConesspinEdit.Value);

                        if (QtyspinEdit.Value != null)
                            _find.Qty = Convert.ToDecimal(QtyspinEdit.Value);
                        
                        if (TolPerSpinEdit.Value != null)
                            _find.TolPer = Convert.ToDecimal(TolPerSpinEdit.Value);

                        _find.Remark = RemarkTextBoxExt.Text;
                        //_find.TotPer

                        _find.CompanyId = KontoGlobals.CompanyId;

                        var map = new Mapper(config);
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

                        var transModel = new JobCardTransModel();
                        //Color Jobcard Trans
                         
                        foreach (var item in _Translist)
                        {
                            var transid = item.Id;
                            item.JobCardId = _find.Id;
                            item.TransType = 1;//1 for Color Consumption
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

                        //Chemical Jobcard Trans
                         
                        foreach (var item in _ChemicalList)
                        {
                            var transid = item.Id;
                            item.JobCardId = _find.Id;
                            item.TransType = 2;//2 for Chemical Consumption
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
                        //Yarn Consumption Jobcard Trans

                        foreach (var item in _YarnList)
                        {
                            var transid = item.Id;

                            transModel = new JobCardTransModel();
                            if (item.Id > 0)
                            {
                                transModel = db.jobCardTrans.Find(item.Id);
                            }
                            map = new Mapper(config);
                            map.Map(item, transModel);

                            transModel.JobCardId = _find.Id;
                            transModel.TransType = 3;//3 for Yarn Consumption
                            if (item.Id <= 0)
                            {
                                db.jobCardTrans.Add(transModel);
                                db.SaveChanges();
                            }
                        }
                        //Color
                        foreach (var item in DelClrTrans)
                        {
                            if (item.Id <= 0) continue;
                            var _model = db.jobCardTrans.Find(item.Id);
                            _model.IsDeleted = true;
                        }
                        //Chemical Jobcard Trans
                        foreach (var item in DelChemicalTrans)
                        {
                            if (item.Id <= 0) continue;
                            var _model = db.jobCardTrans.Find(item.Id);
                            _model.IsDeleted = true;
                        }
                        //Yarn Jobcard Trans
                        foreach (var item in DelYarnTrans)
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
            //var trans = ClrConsbindingSource.DataSource as List<JobCardTransDto>;
            //var chemicaltrans = ChemicalbindingSource.DataSource as List<JobCardTransDto>;

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
            else if (Convert.ToInt32(FinishproductLookup.SelectedValue) <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Product", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FinishproductLookup.Focus();
                return false;
            }
            else if (OrderQtyspinEdit.Value <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Order Qty", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                OrderQtyspinEdit.Focus();
                return false;
            }
            else if (Convert.ToInt32(GreyLookup.SelectedValue) <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Grey Product", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                GreyLookup.Focus();
                return false;
            }
            else if (TolPerSpinEdit.Value> 100)
            {
                MessageBoxAdv.Show(this, "Invalid Tol Per", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TolPerSpinEdit.Focus();
                return false;
            }
            //else if (trans.Count <= 0)
            //{
            //    MessageBoxAdv.Show(this, "At least one transaction should be entered!", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    ClrConsumptiongridView.Focus();
            //    return false;
            //}
            //else if (chemicaltrans.Count <= 0)
            //{
            //    MessageBoxAdv.Show(this, "At least one Chemical consumption should be entered!", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    ChemicalgridView.Focus();
            //    return false;
            //}
            return true;
        }
        private void LoadData(JobCardModel pdata)
        {
            KontoContext db = new KontoContext();

            VoucherNotextEdit.Text = pdata.VoucherNo;
            TyperadioGroup.EditValue = pdata.Type;
            divLookUpEdit.EditValue = pdata.DivId;
            voucherLookup.SelectedValue = pdata.VoucherId;
            voucherDateEdit.EditValue = KontoUtils.IToD(pdata.VoucherDate);
            DyeingTyperadioGroup.EditValue = pdata.DyeingType;
            MachineNolookUpEdit.EditValue = pdata.MachineId;

            if (pdata.RPUIId != null)
                ChemicallookUpEdit.EditValue = pdata.RPUIId;

            OrderNobuttonEdit.EditValue = pdata.OrderId;
            if (pdata.OrderId > 0)
            {
                accLookup1.Enabled = false;
                FinishproductLookup.Enabled = false;
                //colorLookup1.Enabled = false;
                //OrderQtyspinEdit.Enabled = false;
            }

            accLookup1.SelectedValue = pdata.AccountId;
            if (pdata.AccountId > 0)
                accLookup1.SetAcc((int)pdata.AccountId);

            FinishproductLookup.SelectedValue = pdata.ProductId;

            if (pdata.ProductId > 0)
                FinishproductLookup.SetGroup((int)pdata.ProductId);

            colorLookup1.SelectedValue = pdata.ColorId;
            colorLookup1.SetGroup();

            if (pdata.OrderQty > 0)
                OrderQtyspinEdit.Value = (decimal)pdata.OrderQty;

            if (pdata.GrayItemId > 0)
            {
                GreyLookup.SelectedValue = pdata.GrayItemId;
                GreyLookup.SetGroup((int)pdata.GrayItemId);
            }

            LotNoLookUpEdit.EditValue = pdata.LotNo;
            BatchNoLookUpEdit.EditValue = pdata.BatchId;
            gradeLookup1.SelectedValue = pdata.GradeId;
            gradeLookup1.SetValue();

            if (pdata.NoOfCones > 0)
                NoOfConesspinEdit.Value = (int)pdata.NoOfCones;

            if (pdata.Qty > 0)
                QtyspinEdit.Value = (decimal)pdata.Qty;
            if (pdata.TolPer > 0)
                TolPerSpinEdit.Value = (decimal)pdata.TolPer;

            RemarkTextBoxExt.Text = pdata.Remark;
            //_find.TotPer

            var translist =
                (from bt in db.jobCardTrans
                 join pd in db.Products on bt.ItemId equals pd.Id into join_p
                 from pd in join_p.DefaultIfEmpty()
                 where bt.JobCardId == pdata.Id &&
               bt.IsActive == true && bt.IsDeleted == false
               && bt.TransType == 1
                 select new JobCardTransDto()
                 {
                     ItemId = (int)bt.ItemId,
                     JobCardId = bt.JobCardId,
                     Id = bt.Id,
                     ItemName = pd.ProductName,
                     LotNo = bt.LotNo,
                     ColorPer = bt.ColorPer,
                     Remark = bt.Remark,
                     ConsumeQty = bt.ConsumeQty,
                     Rate = bt.Rate,
                     Amount = bt.Amount
                 }).ToList();
            var _chemicallist =
                (from bt in db.jobCardTrans
                 join pd in db.Products on bt.ItemId equals pd.Id into join_p
                 from pd in join_p.DefaultIfEmpty()
                 where bt.JobCardId == pdata.Id &&
               bt.IsActive == true && bt.IsDeleted == false
               && bt.TransType == 2
                 select new JobCardTransDto()
                 {
                     ItemId = (int)bt.ItemId,
                     JobCardId = bt.JobCardId,
                     Id = bt.Id,
                     ItemName = pd.ProductName,
                     LotNo = bt.LotNo,
                     ColorPer = bt.ColorPer,
                     RefId = bt.RefId,
                     RefTransId = bt.RefTransId,
                     Remark = bt.Remark,
                     ConsumeQty = bt.ConsumeQty,
                     Rate = bt.Rate,
                     Amount = bt.Amount
                 }).ToList();
            var _Yarnlist =
               (from bt in db.jobCardTrans
                join pd in db.Products on bt.ItemId equals pd.Id into join_p
                from pd in join_p.DefaultIfEmpty()
                where bt.JobCardId == pdata.Id &&
              bt.IsActive == true && bt.IsDeleted == false
              && bt.TransType == 3
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
                    Remark = bt.Remark,
                    ConsumeQty = bt.ConsumeQty
                }).ToList();

            ClrConsbindingSource.DataSource = translist;
            ChemicalbindingSource.DataSource = _chemicallist;
            YarnbindingSource.DataSource = _Yarnlist;

            DelClrTrans = new List<JobCardTransDto>();
            DelChemicalTrans = new List<JobCardTransDto>();
            DelYarnTrans = new List<JobCardTransDto>();

            divLookUpEdit.Focus();
        }
        #endregion
    }
}