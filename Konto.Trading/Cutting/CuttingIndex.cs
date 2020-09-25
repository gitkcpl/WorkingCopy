using AutoMapper;
using DevExpress.XtraEditors;
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

namespace Konto.Trading.Cutting
{
    public partial class CuttingIndex : KontoMetroForm
    {
        private List<ChallanModel> FilterView = new List<ChallanModel>();
        private List<CuttingTransDto> DelTrans = new List<CuttingTransDto>();
        private List<CuttingTransDto> TakaCutList = new List<CuttingTransDto>();
        private List<CuttingTransDto> DelTakaCutList = new List<CuttingTransDto>();

        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;

        public CuttingIndex()
        {
            InitializeComponent();

            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            gridControl1.ProcessGridKey += GridControl1_ProcessGridKey;
            productRepositoryItemButtonEdit.ButtonClick += ProductRepositoryItemButtonEdit_ButtonClick;

            accLookup1.SelectedValueChanged += AccLookup1_SelectedValueChanged;
            gridView1.InitNewRow += GridView1_InitNewRow;
            gridView1.CellValueChanged += GridView1_CellValueChanged;
            gridView1.KeyDown += GridView1_KeyDown;
            gridControl1.Enter += GridControl1_Enter;
            gridView1.CustomDrawRowIndicator += GridView1_CustomDrawRowIndicator;
            gridView1.ShowingEditor += GridView1_ShowingEditor;
            gridView1.MouseUp += GridView1_MouseUp;

            challanNoRrepositoryItemButtonEdit.ButtonClick += ChallanNoRrepositoryItemButtonEdit_ButtonClick;
            gridView1.DoubleClick += GridView1_DoubleClick;
            this.MainLayoutFile = KontoFileLayout.Cutting_Index;
            this.GridLayoutFile = KontoFileLayout.Cutting_Trans;

        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveDataAsync(true);
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Cutting Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }
        private void AccLookup1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (accLookup1.LookupDto == null) return;
            using (var db = new KontoContext())
            {
                var list = new List<CuttingListDto>();
                db.Database.CommandTimeout = 0;
                var spcol = db.SpCollections.FirstOrDefault(k => k.Id ==
                            (int)SpCollectionEnum.PendingForCutting);
                if (spcol == null)
                {
                    list = db.Database.SqlQuery<CuttingListDto>(
                    "dbo.PendingForCutting @CompanyId={0},@VoucherTypeId={1},@AccountId={2}",
                KontoGlobals.CompanyId, (int)VoucherTypeEnum.MillReceipt, Convert.ToInt32(accLookup1.SelectedValue)).ToList();
                }
                else
                {
                    list = db.Database.SqlQuery<CuttingListDto>(
                     spcol.Name + " @CompanyId={0},@VoucherTypeID={1},@AccountId={2} ",
                    KontoGlobals.CompanyId, (int)VoucherTypeEnum.MillReceipt, Convert.ToInt32(accLookup1.SelectedValue)).ToList();
                }

                if (list.Count > 0)
                {
                    ShowPendingCutting(list);
                }
                else
                {
                    MessageBox.Show("No Pending Lot!");
                }
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
                var _list = tabPageAdv2.Controls[0] as CuttingListView;
                _list.ActiveControl = _list.KontoGrid;
                this.Text = "Cutting [View]";
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new CuttingListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Cutting [View]";
            }
        }
        #region UDF
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
            OrderNoTextEdit.Text = model.ChallanNo;

            if (Convert.ToInt32(model.EmpId) != 0)
            {
                empLookup1.SelectedValue = model.EmpId;
                empLookup1.SetGroup();
            }

            remarkTextEdit.Text = model.Remark;

            createdLabelControl.Text = "Created By: " + model.CreateUser + " [ " + model.CreateDate + " ]";
            modifyLabelControl.Text = "Modified By: " + model.ModifyUser + " [ " + model.ModifyDate ?? string.Empty + " ]";


            using (var _context = new KontoContext())
            {
                var _list = (from ct in _context.ChallanTranses
                             join pd in _context.Products on ct.ProductId equals pd.Id into join_pd
                             from pd in join_pd.DefaultIfEmpty()
                             join ch in _context.Challans on ct.RefId equals ch.Id into join_ch
                             from ch in join_ch.DefaultIfEmpty()
                             orderby ct.Id
                             where ct.IsActive == true && ct.IsDeleted == false &&
                             ct.ChallanId == model.Id
                             select new CuttingTransDto()
                             {
                                 Id = ct.Id,
                                 ChallanId = ct.ChallanId,
                                 LotNo = ct.LotNo,
                                 MiscId = ct.MiscId,
                                 Pcs = ct.Pcs,
                                 ProductId = (int)ct.ProductId,
                                 ProductName = pd.ProductName,
                                 Qty = ct.Qty,
                                 IssueQty = ct.IssueQty,
                                 ShQty = ct.IssueQty - ct.Qty,
                                 ShPer = (ct.IssueQty - ct.Qty) > 0 ? decimal.Round((decimal)((ct.IssueQty - ct.Qty) / ct.IssueQty * 100), 2) : 0,
                                 Rate = ct.Rate,
                                 RefId = ct.RefId,
                                 RefVoucherId = ct.RefVoucherId,
                                 Remark = ct.Remark,
                                 RefNo = ct.RefNo,
                                 ChallanNo = ch.VoucherNo
                             }).ToList();

                var _Cutlist = _list.Where(k => k.RefVoucherId == model.VoucherId).ToList();
                this.bindingSource1.DataSource = _list.Where(k => k.RefVoucherId != model.VoucherId).ToList();
                this.TakaCutList = _Cutlist;
            }

            this.Text = "Cutting [View/Modify]";

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
            else if ((string.IsNullOrEmpty(OrderNoTextEdit.Text)))
            {
                MessageBoxAdv.Show(this, "Invalid Order No", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                OrderNoTextEdit.Focus();
                return false;
            }


            else if (dt > KontoGlobals.ToDate || dt < KontoGlobals.FromDate)
            {
                MessageBoxAdv.Show(this, "Voucher date out of financial range", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherDateEdit.Focus();
            }

            else if (gridView1.RowCount == 1)
            {
                MessageBoxAdv.Show(this, "At Least One Product Should be Entered", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gridView1.Focus();
                return false;
            }

            return true;
        }

        private void OpenCuttingDetail(CuttingTransDto dr)
        {
            var frm = new CuttingDetailWindow();

            var takalist = TakaCutList.Where(k => k.RefId == dr.Id).ToList();
            if (takalist.Count <= 0)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CuttingTransDto, CuttingTransDto>().ForMember(x => x.Id, p => p.Ignore());
                });
                CuttingTransDto nr = new CuttingTransDto();

                var map = new Mapper(config);
                map.Map(dr, nr);
                nr.IssueQty = 0;
                //CuttingTransDto nr = new CuttingTransDto();
                //nr = dr;
                frm.TransList = new List<CuttingTransDto>();
                frm.TransList.Add(nr);
            }
            else
            {
                frm.TransList = takalist;
            }
            frm.FinalMtr = dr.IssueQty;
            frm.ShowDialog();

            //remove existing entry
            foreach (var po in takalist)
            {
                this.TakaCutList.Remove(po);
            }
            foreach (var ch in frm.TransList)
            {
                ch.RefId = dr.Id;
                this.TakaCutList.Add(ch);
            }
            foreach (var pro in frm.DelTakaCutList)
            {
                this.TakaCutList.Remove(pro);
                this.DelTakaCutList.Add(pro);
            }
            dr.Qty = frm.TransList.Sum(k => k.Qty);
            dr.Pcs = frm.TransList.Sum(k => k.Pcs);
            GridCalculation(dr);
        }
        private void ShowPendingCutting(List<CuttingListDto> list)
        {
            List<CuttingTransDto> Trans = new List<CuttingTransDto>();

            var frm = new PendingCuttingWindow();
            frm.TransList = list;

            frm.ShowDialog();
            using (var db = new KontoContext())
            {
                var list1 = new List<CuttingListDto>();
                db.Database.CommandTimeout = 0;
                var spcol = db.SpCollections.FirstOrDefault(k => k.Id ==
                            (int)SpCollectionEnum.PendingForCuttingDet);
                if (spcol == null)
                {
                    list1 = db.Database.SqlQuery<CuttingListDto>(
                    "dbo.PendingForCuttingDet @CompanyId={0},@ToDate={1},@FromDate={2},@ChallanId={3}",
                KontoGlobals.CompanyId, (int)KontoGlobals.FromDate, KontoGlobals.ToDate, frm.SelectedRow.TransId).ToList();
                }
                else
                {
                    list1 = db.Database.SqlQuery<CuttingListDto>(
                     spcol.Name + " @CompanyId={0},@FromDate={1},@ToDate={2},@ChallanId={3}",
                   KontoGlobals.CompanyId, (int)KontoGlobals.FromDate, KontoGlobals.ToDate, frm.SelectedRow.TransId).ToList();
                }

                if (list1.Count > 0)
                {
                    int id = 0;
                    foreach (var ch in list1)
                    {
                        CuttingTransDto ct = new CuttingTransDto();
                        id = id - 1;
                        ct.Id = id;
                        ct.Pcs = 1;
                        ct.IssueQty = ch.FinMeter != null ? (decimal)ch.FinMeter : 0;
                        ct.ChallanNo = ch.ChallanNo;
                        ct.RefNo = ch.RefNo;
                        ct.LotNo = ch.LotNo;
                        ct.ProductName = ch.Product;
                        ct.ProductId = (int)ch.ProductId;

                        ct.DesignNo = ch.DesignNo;
                        ct.DesignId = (int)ch.DesignId;

                        ct.ColorName = ch.ColorName;
                        ct.ColorId = (int)ch.ColorId;

                        ct.Qty = ch.FinMeter != null ? (decimal)ch.FinMeter : 0;
                        ct.ShQty = ct.IssueQty - ct.Qty > 0 ? ct.IssueQty - ct.Qty : 0;
                        ct.ShPer = (ct.IssueQty - ct.Qty) > 0 ? decimal.Round((decimal)((ct.IssueQty - ct.Qty) / ct.IssueQty * 100), 2) : 0;

                        ct.RefId = ch.Id;
                        ct.RefVoucherId = ch.RefVoucherId;
                        ct.MiscId = ch.TransId;
                        ct.BatchId = ch.ProdOutId;
                        ct.TakaVNo = ch.TakaVNo;

                        Trans.Add(ct);
                    }
                    bindingSource1.DataSource = Trans;
                }
                else
                {
                    MessageBox.Show("No Pending Cutting!");
                }
            }

        }

        private CuttingTransDto PreOpenLookup()
        {
            if (Convert.ToInt32(accLookup1.SelectedValue) == 0) return null;
            gridView1.GetRow(gridView1.FocusedRowHandle);
            if (gridView1.GetRow(gridView1.FocusedRowHandle) == null)
            {
                gridView1.AddNewRow();
            }
            var dr = (CuttingTransDto)gridView1.GetRow(gridView1.FocusedRowHandle);
            return dr;
        }
        private void OpenItemLookup(int _selvalue, CuttingTransDto er)
        {
            var frm = new ProductLkpWindow();

            frm.SelectedValue = _selvalue;
            frm.PTypeId = ProductTypeEnum.GREY;
            frm.VoucherType = VoucherTypeEnum.GreyOrder;

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                er.ProductId = frm.SelectedValue;
                er.ProductName = frm.SelectedTex;
                var model = frm.SelectedItem as ProductLookupDto;
                er.UnitId = model.PurUomId;
                er.Rate = model.DealerPrice;

                gridView1.FocusedColumn = gridView1.VisibleColumns[gridView1.FocusedColumn.VisibleIndex + 1];
            }
            GridCalculation(er);

        }
        private void OpenColorLookup(int _selvalue, CuttingTransDto er)
        {
            var frm = new ColorLkpWindow();
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
        private void OpenDesignLookup(int _selvalue, CuttingTransDto er)
        {
            var frm = new DesignLkpWindow();
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
        #endregion

        public void GridCalculation(CuttingTransDto er)
        {
            er.ShQty = er.IssueQty - er.Qty;
            var shper = er.ShQty / er.IssueQty * 100;
            er.ShPer = decimal.Round((decimal)shper, 2);
        }
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
            if (!(gridView1.GetFocusedRow() is CuttingTransDto itm)) return;

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
            if (!(gridView1.GetRow(e.RowHandle) is CuttingTransDto er)) return;
            GridCalculation(er);
        }
        private void GridView1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                GridView view = sender as GridView;
                var row = view.GetRow(view.FocusedRowHandle) as CuttingTransDto;
                OpenCuttingDetail(row);
            }
            if (e.KeyCode != Keys.Delete) return;

            if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
            {
                if (MessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo) !=
                  DialogResult.Yes)
                    return;
                GridView view = sender as GridView;
                var row = view.GetRow(view.FocusedRowHandle) as CuttingTransDto;
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
            var rw = gridView1.GetRow(e.RowHandle) as CuttingTransDto;
            rw.Id = -1 * gridView1.RowCount;
        }

        private void GridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(accLookup1.SelectedValue) == 0) return;
                var dr = PreOpenLookup();
                if (dr == null) return;
                if (gridView1.FocusedColumn.FieldName == "ChallanNo")
                {
                    OpenCuttingDetail(dr);
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
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Cutting GridControl KeyDown");
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
        private void ChallanNoRrepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
                OpenCuttingDetail(dr);
        }

    
        #endregion
      
        #region Parent Function

        public override void Print()
        {

        }
        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<ChallanModel>();
            this.Text = "Cutting [Add New]";

            voucherNoTextEdit.Text = "New";
            voucherDateEdit.EditValue = DateTime.Now;
            empLookup1.SelectedValue = 1;
            empLookup1.SetGroup();
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
            this.ActiveControl = voucherLookup1.buttonEdit1;
            voucherLookup1.SetDefault();

            DelTrans = new List<CuttingTransDto>();
            this.bindingSource1.DataSource = new List<CuttingTransDto>();

        }
        public override void ResetPage()
        {
            base.ResetPage();

            accLookup1.SetEmpty();
            empLookup1.SetEmpty();
            OrderNoTextEdit.Text = string.Empty;
            voucherDateEdit.DateTime = DateTime.Now;
            remarkTextEdit.Text = string.Empty;

            DelTrans = new List<CuttingTransDto>();
            TakaCutList = new List<CuttingTransDto>();
            DelTakaCutList = new List<CuttingTransDto>();
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
            if (!string.IsNullOrEmpty(OrderNoTextEdit.Text.Trim()))
            {
                filter.Add(new Filter { PropertyName = "ChallanNo", Operation = Op.Equals, Value = OrderNoTextEdit.Text.Trim() });
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
                cfg.CreateMap<CuttingTransDto, ChallanTransModel>().ForMember(x => x.Id, p => p.Ignore());
            });

            var _translist = bindingSource1.DataSource as List<CuttingTransDto>;
            List<ChallanTransModel> Trans = new List<ChallanTransModel>();
            List<ChallanTransModel> TakaTrans = new List<ChallanTransModel>();
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
                        _find.TotalAmount = 0;
                        UpdateChallan(db, _find);

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

                            if (tranModel.Id <= 0)
                            {
                                db.ChallanTranses.Add(tranModel);
                                db.SaveChanges();

                            }
                            item.Id = tranModel.Id;
                            Trans.Add(tranModel);


                            var takalist = TakaCutList.Where(k => (k.RefId == item.Id || k.RefId == transid)).ToList();
                            foreach (var tk in takalist)
                            {
                                var trmodel = new ChallanTransModel();
                                if (tk.Id > 0)
                                {
                                    trmodel = db.ChallanTranses.Find(tk.Id);
                                }
                                map = new Mapper(config);
                                map.Map(tk, trmodel);

                                if (trmodel.Id == 0)
                                {
                                    trmodel.RefId = item.Id;
                                    trmodel.RefVoucherId = _find.VoucherId;
                                    trmodel.ChallanId = _find.Id;
                                     
                                    db.ChallanTranses.Add(trmodel);
                                    db.SaveChanges();
                                }
                                TakaTrans.Add(trmodel);
                            }
                        }
                        //delete item from ord trans
                        foreach (var item in DelTrans)
                        {
                            if (item.Id == 0) continue;

                            var takalist = db.ChallanTranses.Where(k => k.RefId == item.Id && k.ChallanId == _find.Id).ToList();
                            foreach (var taka in takalist)
                            {
                                taka.IsDeleted = true;
                            }
                            var _model = db.ChallanTranses.Find(item.Id);
                            _model.IsDeleted = true;
                        }

                        //delete item from cutting detail list
                        foreach (var item in DelTakaCutList)
                        {
                            var trmodel = new ChallanTransModel();
                            if (item.Id > 0)
                            {
                                trmodel = db.ChallanTranses.Find(item.Id);
                                map = new Mapper(config);
                                map.Map(item, trmodel);

                                trmodel.IsDeleted = true;
                            }
                        }
                        //sotock effect
                        var stk = db.StockTranses.Where(k => k.MasterRefId == _find.RowId).ToList();
                        if (stk != null)
                            db.StockTranses.RemoveRange(stk);

                        foreach (var item in Trans)
                        {
                            string TableName = "Cutting";
                            var stockReq = db.Products.FirstOrDefault(k => k.Id == item.ProductId).StockReq;
                            if (stockReq == "No") continue;
                            //   var prList = ProdList.Where(x => x.TransId == item.Id).ToList();
                            StockEffect.StockTransChlnEntry(_find, item, true, TableName, KontoGlobals.UserName, db);
                        }

                        foreach (var item in TakaTrans)
                        {
                            string TableName = "Cutting";
                            var stockReq = db.Products.FirstOrDefault(k => k.Id == item.ProductId).StockReq;
                            if (stockReq == "No") continue;
                            //   var prList = ProdList.Where(x => x.TransId == item.Id).ToList();
                            StockEffect.StockTransChlnEntry(_find, item, false, TableName, KontoGlobals.UserName, db);
                        }

                        db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Cutting Save");
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

        private void UpdateChallan(KontoContext db, ChallanModel model)
        {

            model.DivId = 1;

            model.ChallanType = (int)ChallanTypeEnum.INWARD_FROM_JOB;
            model.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);
            model.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));

            model.AccId = Convert.ToInt32(accLookup1.SelectedValue);

            model.VoucherNo = voucherNoTextEdit.Text.Trim();


            model.ChallanNo = OrderNoTextEdit.Text.Trim();

            model.EmpId = Convert.ToInt32(empLookup1.SelectedValue);

            model.Remark = remarkTextEdit.Text.Trim();

            model.TypeId = (int)VoucherTypeEnum.Cutting;
            model.CompId = KontoGlobals.CompanyId;
            model.YearId = KontoGlobals.YearId;
            model.BranchId = KontoGlobals.BranchId;

            var _translist = bindingSource1.DataSource as List<CuttingTransDto>;
            model.TotalAmount = 0;

            model.TotalQty = _translist.Sum(x => x.Qty);
            model.TotalPcs = _translist.Sum(x => x.Pcs);
            model.IsActive = true;

            if (model.Id == 0)
            {
                model.VoucherNo = DbUtils.NextSerialNo(model.VoucherId, db);
                db.Challans.Add(model);
                db.SaveChanges();
            }
        }

        #endregion
    }
}