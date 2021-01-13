using AutoMapper;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Konto.App.Shared;
using Konto.App.Shared.Para;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters.Dtos;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using Konto.Shared.Masters.Design;
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

namespace Konto.Weaves.TakaProduction
{
    public partial class TakaProdIndex : KontoMetroForm
    {
        private List<Prod_EmpDto> DelProdEmp = new List<Prod_EmpDto>();
        private List<ProdWeftItemDTO> DelWeft = new List<ProdWeftItemDTO>();
        private List<ProdModel> FilterView = new List<ProdModel>();
        private List<BeamStatusByMachineDto> BeamProdTrans = new List<BeamStatusByMachineDto>();

        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        public TakaProdIndex()
        {
            InitializeComponent();

            FillLookup();
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            this.Load += TakaProdIndex_Load;

            this.MainLayoutFile = KontoFileLayout.TakaProd_Index;
            this.GridLayoutFile = KontoFileLayout.TakaProd_Trans;

            ProdgridControl.ProcessGridKey += ProdGridControl_ProcessGridKey;
            ProdgridView.InitNewRow += ProdgridView_InitNewRow;
            ProdgridView.CellValueChanged += ProdgridView_CellValueChanged;
            ProdgridView.KeyDown += ProdgridView_KeyDown;
            ProdgridControl.Enter += ProdGridControl_Enter;
            //  gridView1.ValidateRow += GridView1_ValidateRow;
            ProdgridView.MouseUp += ProdgridView_MouseUp;
            ProdgridView.InvalidRowException += ProdgridView_InvalidRowException;
            ProdgridView.ShowingEditor += ProdgridView_ShowingEditor;
            ProdgridView.DoubleClick += ProdgridView_DoubleClick;
            ProdgridView.CustomDrawRowIndicator += ProdgridView_CustomDrawRowIndicator;

            WeftgridControl.ProcessGridKey += WeftgridControl_ProcessGridKey;
            WeftgridView.InitNewRow += WeftgridView_InitNewRow;
            WeftgridView.CellValueChanged += WeftgridView_CellValueChanged;
            WeftgridView.KeyDown += WeftgridView_KeyDown;
            WeftgridControl.Enter += WeftgridControl_Enter;
            //  gridView1.ValidateRow += GridView1_ValidateRow;
            WeftgridView.MouseUp += WeftgridView_MouseUp;
            WeftgridView.InvalidRowException += WeftgridView_InvalidRowException;
            WeftgridView.ShowingEditor += WeftgridView_ShowingEditor;
            WeftgridView.DoubleClick += WeftgridView_DoubleClick;
            WeftgridView.CustomDrawRowIndicator += WeftgridView_CustomDrawRowIndicator;

            divLookUpEdit.EditValueChanged += DivLookUpEdit_EditValueChanged;
            MachineNolookUpEdit.EditValueChanged += MachineNolookUpEdit_EditValueChanged;
            productLookup1.SelectedValueChanged += ProductLookup1_SelectedValueChanged;
            productRepositoryItemButtonEdit.ButtonClick += ProductRepositoryItemButtonEdit_ButtonClick;
            //  designLookup.buttonEdit1.KeyDown += ButtonEdit1_KeyDown;
            designLookup.LostFocus += DesignLookup_LostFocus;
            voucherLookup1.SelectedValueChanged += VoucherLookup_SelectedValueChanged;
        }

        private void VoucherLookup_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.PrimaryKey == 0 && Convert.ToInt32(voucherLookup1.SelectedValue) > 0)
            {
                TakaNotextEdit.Text = "New-" + DbUtils.NextSerialNo(Convert.ToInt32(voucherLookup1.SelectedValue), 1);
            }
        }

        private void ProductRepositoryItemButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var dr = PreWeftOpenLookup();
            OpenItemLookup( Convert.ToInt32( dr.ProductId),dr);
        }

        private void ProductLookup1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(productLookup1.SelectedValue) == 0) return;

            var itemid = Convert.ToInt32(productLookup1.SelectedValue);

            var db = new KontoContext();

             List<ProdWeftItemDTO> ProdWeftList = new List<ProdWeftItemDTO>(
                 (from p in db.WeftItems
                  join pd in db.Products on p.ProductId equals pd.Id
                  where p.RefId == itemid
                              && !p.IsDeleted
                  select new ProdWeftItemDTO()
                  {
                      ProductId = p.ProductId,
                      ProductName = pd.ProductName,
                      Denier = p.Denier,
                      PI = p.PI,
                      RS = p.RS,
                      Qty = p.Qty,
                      WeftId = p.Id
                  }
                 ).ToList());

                stdWtspinEdit.Value = (decimal)ProdWeftList.Sum(k => k.Qty);
                WeftbindingSource.DataSource = ProdWeftList;
            
        }

        private void DesignLookup_LostFocus(object sender, EventArgs e)
        {
            TakaNotextEdit.Focus();
        }

        //private void ButtonEdit1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (designLookup.SelectedValue == null && e.KeyCode != Keys.Enter) return;
        //    if (designLookup.SelectedValue != null && e.KeyCode != Keys.F1) return;

        //    var frm = new DesignLkpWindow();
        //    frm.Tag = MenuId.Design_Master;
        //    if (designLookup.SelectedValue != null)
        //        frm.SelectedValue = (int)designLookup.SelectedValue;

        //    frm.ShowDialog();
        //    if (frm.DialogResult == DialogResult.OK)
        //    {
        //        designLookup.SelectedValue = frm.SelectedValue;
        //    }
        //}

        //private void DesignLookup_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (designLookup.SelectedValue == null && e.KeyCode != Keys.Enter) return;
        //    if (designLookup.SelectedValue != null && e.KeyCode != Keys.F1) return;

        //    var frm = new DesignLkpWindow();
        //    frm.Tag = MenuId.Design_Master;
        //    if(designLookup.SelectedValue!=null)
        //    frm.SelectedValue = (int)designLookup.SelectedValue; 

        //    frm.ShowDialog();
        //    if (frm.DialogResult == DialogResult.OK)
        //    {
        //        designLookup.SelectedValue = frm.SelectedValue;
        //    }
        //}

        private void DivLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (divLookUpEdit.EditValue == null) return;

            using (var db = new KontoContext())
            {
                int divid = Convert.ToInt32(divLookUpEdit.EditValue);
                var _macList = (from p in db.MachineMasters
                                where p.IsActive && !p.IsDeleted &&
                                p.DivId == divid
                                select new BaseLookupDto()
                                {
                                    DisplayText = p.MachineName,
                                    Id = p.Id
                                }).ToList();

                MachineNolookUpEdit.Properties.DataSource = _macList;
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
                var _macList = (from p in db.MachineMasters
                                where p.IsActive && !p.IsDeleted
                                select new BaseLookupDto()
                                {
                                    DisplayText = p.MachineName,
                                    Id = p.Id
                                }).ToList();

                MachineNolookUpEdit.Properties.DataSource = _macList;
                divLookUpEdit.Properties.DataSource = _divLists;
            }
        }
        private Prod_EmpDto PreOpenLookup()
        {
            ProdgridView.GetRow(ProdgridView.FocusedRowHandle);
            if (ProdgridView.GetRow(ProdgridView.FocusedRowHandle) == null)
            {
                ProdgridView.AddNewRow();
            }
            var dr = (Prod_EmpDto)ProdgridView.GetRow(ProdgridView.FocusedRowHandle);
            return dr;
        }
        private ProdWeftItemDTO PreWeftOpenLookup()
        {
            WeftgridView.GetRow(WeftgridView.FocusedRowHandle);
            if (WeftgridView.GetRow(WeftgridView.FocusedRowHandle) == null)
            {
                WeftgridView.AddNewRow();
            }
            var dr = (ProdWeftItemDTO)WeftgridView.GetRow(WeftgridView.FocusedRowHandle);
            return dr;
        }
        private void OpenEmpLookup(int _selvalue, Prod_EmpDto er)
        {
            var frm = new EmpLkpWindow();
            frm.Tag = MenuId.Emp;
            frm.SelectedValue = _selvalue;
            //frm.PTypeId = ProductTypeEnum.;

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                er.EmpId = frm.SelectedValue;
                er.EmpName = frm.SelectedTex;
                var pid = Convert.ToInt32(productLookup1.SelectedValue);
                using(var db = new KontoContext()) {
                    var rt = db.EmpRates.FirstOrDefault(x => x.EmpId == er.EmpId && x.ProductId == pid);
                    if (rt != null)
                    {
                        er.Rate = rt.Rate;
                        er.Amount = er.TotalMtrs * er.Rate;
                    }
                }
                ProdgridView.FocusedColumn = ProdgridView.GetVisibleColumn(colEmpName.VisibleIndex + 1);
               // ProdgridView.FocusedColumn = ProdgridView.GetNearestCanFocusedColumn(ProdgridView.FocusedColumn);
            }
        }
        private void OpenItemLookup(int _selvalue, ProdWeftItemDTO er)
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
                WeftgridView.FocusedColumn = WeftgridView.GetVisibleColumn(colEmpName.VisibleIndex + 1);
             }
        }
        public TakaBeamModel GetByMasterIdAsync(int prodid, int beamid, KontoContext db)
        {

            var _list = (from bt in db.TakaBeams
                         join t in db.Prods on bt.ProdId equals t.Id into join_t
                         from pd in join_t.DefaultIfEmpty()
                         orderby bt.Id
                         where bt.IsActive == true && bt.IsDeleted == false &&
                         bt.ProdId == prodid && bt.BeamId == beamid
                         select new
                         {
                             bt.ProdId,
                             bt.CreateDate,
                             bt.CreateUser,
                             bt.Id,
                             bt.IpAddress,
                             bt.IsActive,
                             bt.IsDeleted,
                             bt.BeamId,
                             bt.Per,
                             bt.Qty,
                             bt.ModifyDate,
                             bt.ModifyUser,
                             bt.RowId
                         }).ToList()
               .Select(c => new TakaBeamModel
               {
                   BeamId = c.BeamId,
                   CreateDate = c.CreateDate,
                   CreateUser = c.CreateUser,
                   Id = c.Id,
                   IpAddress = c.IpAddress,
                   IsActive = c.IsActive,
                   IsDeleted = c.IsDeleted,
                   ProdId = c.ProdId,
                   Qty = c.Qty,
                   Per = c.Per,
                   ModifyDate = c.ModifyDate,
                   ModifyUser = c.ModifyUser,
                   RowId = c.RowId
               }
               ).ToList();
            TakaBeamModel lst = _list.FirstOrDefault();
            return lst; //await _context.Set<OrdTransModel>().Where(x => x.OrdId == id).ToListAsync();

        }
        #endregion
        #region ProdGridView
        private void ProdgridView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var itm = ProdgridView.GetFocusedRow() as Prod_EmpDto;
            if (itm == null) return;
            if (!"EmpName,Extra1".Contains(ProdgridView.FocusedColumn.FieldName)) return;

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
            //ProdgridView.FocusedColumn = ProdgridView.VisibleColumns[0];
            ProdgridView.FocusedColumn = ProdgridView.Columns["ProdDate"];
        }
        private void ProdgridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == null) return;
            if (!(ProdgridView.GetRow(e.RowHandle) is Prod_EmpDto er)) return;
            if (e.Column.FieldName == "NightMtrs")
            {
                er.TotalMtrs = er.NightMtrs + er.DayMtrs;
                
            }
            if (e.Column.FieldName == "DayMtrs")
            {
                er.TotalMtrs = er.NightMtrs + er.DayMtrs;
                
            }
            er.Amount = er.Rate * er.TotalMtrs;

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
                var row = view.GetRow(view.FocusedRowHandle) as Prod_EmpDto;
                view.DeleteRow(view.FocusedRowHandle);
                DelProdEmp.Add(row);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                GridView view = sender as GridView;
                if (ProdgridView.FocusedColumn.FieldName == "EmpName")
                {
                    view.SetRowCellValue(view.FocusedRowHandle, colEmpName, string.Empty);
                    view.SetRowCellValue(view.FocusedRowHandle, colEmpId, 0);
                }
            }
        }
        private void ProdgridView_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var rw = ProdgridView.GetRow(e.RowHandle) as Prod_EmpDto;
            rw.Id = -1 * ProdgridView.RowCount;
            rw.ProdDate = DateTime.Now.Date;
        }
        private void ProdGridControl_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                var dr = PreOpenLookup();
                if (dr == null) return;
                if (ProdgridView.FocusedColumn.FieldName == "EmpName")
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        if (dr.EmpId == 0 || dr.EmpId == null)
                        {
                            OpenEmpLookup(0, dr);
                            // e.Handled = true;
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        OpenEmpLookup((int)dr.EmpId, dr);
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
        private void EmpRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
                OpenEmpLookup((int)dr.EmpId, dr);
        }
        private void ProdgridView_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        #endregion

        #region WeftgridView
        private void WeftgridView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var itm = WeftgridView.GetFocusedRow() as ProdWeftItemDTO;
            if (itm == null) return;
            if (!"Qty,ProductName".Contains(WeftgridView.FocusedColumn.FieldName)) return;
        }
        private void WeftgridView_MouseUp(object sender, MouseEventArgs e)
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
        private void WeftgridView_DoubleClick(object sender, EventArgs e)
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
        private void WeftgridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.Appearance.ForeColor = Color.FromArgb(227, 22, 91);
            if (e.RowHandle < 0)
                e.Info.DisplayText = "";
            else
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        private void WeftgridControl_Enter(object sender, EventArgs e)
        {
            WeftgridView.FocusedColumn = WeftgridView.VisibleColumns[0];
        }
        private void WeftgridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == null) return;
            if (!(WeftgridView.GetRow(e.RowHandle) is ProdWeftItemDTO er)) return;

        }
        private void WeftgridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete) return;

            if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
            {
                if (MessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo) !=
                  DialogResult.Yes)
                    return;
                GridView view = sender as GridView;
                var row = view.GetRow(view.FocusedRowHandle) as ProdWeftItemDTO;
                view.DeleteRow(view.FocusedRowHandle);
                DelWeft.Add(row);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                GridView view = sender as GridView;
                if (WeftgridView.FocusedColumn.FieldName == "ProductName")
                {
                    view.SetRowCellValue(view.FocusedRowHandle, colProductName, string.Empty);
                    view.SetRowCellValue(view.FocusedRowHandle, colProductId, 0);
                }
            }
        }
        private void WeftgridView_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var rw = WeftgridView.GetRow(e.RowHandle) as ProdWeftItemDTO;
            rw.Id = -1 * WeftgridView.RowCount;
        }
        private void WeftgridControl_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                
                if (WeftgridView.FocusedColumn.FieldName == "ProductName")
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        var dr = PreWeftOpenLookup();
                        if (dr == null) return;
                        if (dr.ProductId == 0 || dr.ProductId == null)
                        {
                            OpenItemLookup(0, dr);
                            // e.Handled = true;
                        }
                    }
                    else if (e.KeyCode == Keys.F1)
                    {
                        var dr = PreWeftOpenLookup();
                        if (dr == null) return;
                        OpenItemLookup((int)dr.ProductId, dr);
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
        private void WeftgridView_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        #endregion
        #region Event

        private void TakaProdIndex_Load(object sender, EventArgs e)
        {
            MeterSpinEdit.Value = 0;
            WeightSpinEdit.Value = 0;
            TotalPcsSpinEdit.Value = 0;
            MendorSpinEdit.Value = 0;
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
                var _ListView = new TakaProdList();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Taka Production [View]";
            }
            else if (tabControlAdv1.SelectedIndex == 3)
            {
                if (tabPageAdv4.Controls.Count > 0) return;

                this.Text = "Taka Production [ReportView]";
                var _frm = Activator.CreateInstance("Konto.Reporting", "Konto.Reporting.Para.TakaPara.TakaParaMainView").Unwrap() as KontoForm;

                _frm.TopLevel = false;
                _frm.Parent = tabPageAdv4;
                //  _frm.ReportFilterType = "TP";
                _frm.Location = new Point(tabPageAdv4.Location.X + tabPageAdv4.Width / 2 - _frm.Width / 2, tabPageAdv4.Location.Y + tabPageAdv4.Height / 2 - _frm.Height / 2);
                _frm.Show();// = true;
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
                Log.Error(ex, "Taka Production Invoice Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private void MachineNolookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (MachineNolookUpEdit.EditValue == null) return;
                int? Mid = Convert.ToInt32(MachineNolookUpEdit.EditValue);
                KontoContext db = new KontoContext();
                var prod = new List<BeamStatusByMachineDto>();

                //var spcol = db.SpCollections.FirstOrDefault(k => k.Id ==
                //            (int)SpCollectionEnum.MachineWiseTakaProdList);
                //if (spcol == null)
                //{
                prod = db.Database.SqlQuery<BeamStatusByMachineDto>(
                     "dbo.beam_status_by_machine @CompanyId={0},@vtypeid={1},@Status={2},@MachineId={3}",
                     KontoGlobals.CompanyId, (int)VoucherTypeEnum.BeamProd, "LOADED", Mid).ToList();
                //}
                //else
                //{
                //    prod = db.Database.SqlQuery<BeamProdDto>(
                //     spcol.Name + " @CompanyId={0},@VoucherID={1},@Status={2},@MachineId={3}",
                //   KontoGlobals.CompanyId, (int)VoucherTypeEnum.BeamProd, "LOADED", Mid).ToList();
                //}

                BeamProdTrans = prod;

                var beamProd = BeamProdTrans.FirstOrDefault();
                if (beamProd != null && beamProd.Id != 0)
                {
                    if (beamProd.GreyProductId != null) {
                        productLookup1.SelectedValue = beamProd.GreyProductId;
                        productLookup1.SetGroup((int)beamProd.GreyProductId);
                            }
                    

                    List<BeamProdDto> TakaTrans = new List<BeamProdDto>(
                      (from p in db.Prods
                       join pd in db.Products on p.ProductId equals pd.Id
                       join tb in db.TakaBeams on p.Id equals tb.ProdId into tb_join
                       from tb in tb_join.DefaultIfEmpty()
                       where tb.BeamId == beamProd.Id
                       && !p.IsDeleted && !tb.IsDeleted
                       select new BeamProdDto()
                       {
                           VoucherDate = p.VoucherDate,
                           GrossWt = p.GrossWt,
                           ProductId = p.ProductId,
                           VoucherNo = p.VoucherNo,
                           NetWt = p.NetWt, ProductName= pd.ProductName
                       }
                        ).ToList());

                    PreviousTakabindingSource.DataSource = TakaTrans;
                }
                BeambindingSource.DataSource = BeamProdTrans;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                Log.Error(ex, "Taka Prod Machine_lookup");
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
            this.Text = "Taka Production [Add New]";

            divLookUpEdit.EditValue = 1;
            this.ActiveControl = voucherLookup1.buttonEdit1;
            voucherLookup1.SetDefault();
            voucherDateEdit.EditValue = DateTime.Now;

           // TakaNotextEdit.Text = "New";

            productLookup1.SetEmpty();
            productLookup1.SetGroup(0);
            designLookup.SetEmpty();

           

            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;

            WeftbindingSource.DataSource = new List<ProdWeftItemDTO>();
            ProdbindingSource.DataSource = new List<Prod_EmpDto>();
            BeambindingSource.DataSource = new List<BeamProdDto>();
            PreviousTakabindingSource.DataSource = new List<BeamProdDto>();

            DelWeft = new List<ProdWeftItemDTO>();
            DelProdEmp = new List<Prod_EmpDto>();

            divLookUpEdit.Focus();

            MachineNolookUpEdit_EditValueChanged(MachineNolookUpEdit, null);
        }
        public override void ResetPage()
        {
            base.ResetPage();
            divLookUpEdit.EditValue = 1;

            voucherLookup1.SetDefault();
            voucherDateEdit.EditValue = DateTime.Now;
            productLookup1.SetEmpty();

            //MendorEmpLookup.SelectedValue = 1;
            //MendorEmpLookup.SetGroup();

            TakaNotextEdit.Text = "New";
            MeterSpinEdit.Value = 0;
            TotalPcsSpinEdit.Value = 1;
            MendorSpinEdit.Value = 0;
            WeightSpinEdit.Value = 0;
            stdWtspinEdit.Value = 0;
            WtShouldspinEdit.Value = 0;
            CheckerRateSpinEdit.Value = 0;
            FolderRateSpinEdit.Value = 0;

            StartDateEdit.DateTime = DateTime.Now;
            FoldDateEdit.DateTime = DateTime.Now;
            designLookup.SetEmpty();
            colorLookup.SetEmpty();
            gradeLookup1.SetEmpty();
            MendorEmpLookup.SetEmpty();
            CheckerempLookup2.SetEmpty();
            FolderempLookup.SetEmpty();

            RemarkTextBoxExt.Text = string.Empty;

            WeftbindingSource.DataSource = new List<ProdWeftItemDTO>();
            ProdbindingSource.DataSource = new List<Prod_EmpDto>();
            BeambindingSource.DataSource = new List<BeamProdDto>();
            PreviousTakabindingSource.DataSource = new List<BeamProdDto>();
            DelWeft = new List<ProdWeftItemDTO>();
            DelProdEmp = new List<Prod_EmpDto>();

            divLookUpEdit.Focus();
        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var pdata = db.Prods.Find(_key);
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


            if (Convert.ToInt32(voucherLookup1.SelectedValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "VoucherId", Operation = Op.Equals, Value = Convert.ToInt32(voucherLookup1.SelectedValue) });
            }

            if (Convert.ToInt32(divLookUpEdit.EditValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "DivId", Operation = Op.Equals, Value = Convert.ToInt32(divLookUpEdit.EditValue) });
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
                FilterView = db.Prods.Where(ExpressionBuilder.GetExpression<ProdModel>(filter))
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

            ProdModel _find = new ProdModel();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Prod_EmpDto, Prod_EmpModel>().ForMember(x => x.Id, p => p.Ignore());
                cfg.CreateMap<ProdWeftItemDTO, Prod_WeftModel>().ForMember(x => x.Id, p => p.Ignore());
            });

            var _Emplist = ProdbindingSource.DataSource as List<Prod_EmpDto>;
            var _Weftlist = WeftbindingSource.DataSource as List<ProdWeftItemDTO>;
            var _Beamlist = BeambindingSource.DataSource as List<BeamStatusByMachineDto>;

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

                        if (divLookUpEdit.EditValue != null)
                            _find.DivId = Convert.ToInt32(divLookUpEdit.EditValue);

                        _find.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);

                        _find.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
                        _find.MacId = Convert.ToInt32(MachineNolookUpEdit.EditValue);
                        _find.ProductId = Convert.ToInt32(productLookup1.SelectedValue);

                        if (designLookup.SelectedValue != null)
                            _find.PlyProductId = Convert.ToInt32(designLookup.SelectedValue);

                        //_find.StartDate =Convert.ToInt32(StartDateEdit.DateTime.ToString("yyyyMMdd"));
                        _find.LoadingDate = FoldDateEdit.DateTime;


                        if (colorLookup.SelectedValue != null)
                            _find.ColorId = Convert.ToInt32(colorLookup.SelectedValue);

                        if (gradeLookup1.SelectedValue != null)
                            _find.GradeId = Convert.ToInt32(gradeLookup1.SelectedValue);

                        _find.NetWt = MeterSpinEdit.Value;
                        _find.GrossWt = WeightSpinEdit.Value;
                        _find.Tops = (int)TotalPcsSpinEdit.Value;

                        if (FolderempLookup.SelectedValue != null)
                            _find.PackEmpId = Convert.ToInt32(FolderempLookup.SelectedValue);

                        _find.CopsRate = FolderRateSpinEdit.Value;

                        if (MendorEmpLookup.SelectedValue != null)
                            _find.JobId = Convert.ToInt32(MendorEmpLookup.SelectedValue);
                        _find.BoxRate = MendorSpinEdit.Value;

                        if (CheckerempLookup2.SelectedValue != null)
                            _find.CheckEmpId = Convert.ToInt32(CheckerempLookup2.SelectedValue);
                        _find.FinQty = CheckerRateSpinEdit.Value;
                        _find.Remark = RemarkTextBoxExt.Text;

                        _find.CompId = KontoGlobals.CompanyId;
                        _find.YearId = KontoGlobals.YearId;
                        _find.BranchId = KontoGlobals.BranchId;
                        _find.CProductId = _find.ProductId;
                        
                      

                        // _find.VoucherNo = BeamNotextEdit.Text;
                        if (string.IsNullOrEmpty(_find.VoucherNo))
                        {
                            var _srno = DbUtils.NextSerialNo((int)_find.VoucherId, db, 0);
                            _find.VoucherNo = _srno;
                            _find.SrNo = 1;
                        }
                        var map = new Mapper(config);
                        if (this.PrimaryKey == 0)
                        {
                            _find.ProdStatus = "STOCK";
                            //var _srno = DbUtils.NextSerialNo(_find.VoucherId, db, 0);
                            //_find.VoucherNo = _srno;  //DbUtils.NextSerialNo(_find.VoucherId, db);
                            //model.VoucherNo = _find.VoucherNo;
                            //_find.TypeId = (int)VoucherTypeEnum.Inward;
                            db.Prods.Add(_find);
                            db.SaveChanges();
                        }
                        else
                        {
                            _find.CreateDate = createdate;
                            _find.CreateUser = createuser;
                        }

                        //Prod Emps
                        foreach (var item in _Emplist)
                        {
                            var transid = item.Id;
                            item.ProdId = _find.Id;
                            var empModel = new Prod_EmpModel();
                            if (item.Id > 0)
                            {
                                empModel = db.Prod_Emps.Find(item.Id);
                            }
                            map = new Mapper(config);
                            map.Map(item, empModel);

                            if (item.Id <= 0)
                            {
                                empModel.VoucherId = _find.VoucherId;

                                db.Prod_Emps.Add(empModel);
                                db.SaveChanges();

                            }
                            //Trans.Add(tranModel);

                        }



                        //Weft Item
                        foreach (var item in _Weftlist)
                        {
                            var transid = item.Id;
                            item.ProdId = _find.Id;
                            var weftModel = new Prod_WeftModel();
                            if (item.Id > 0)
                            {
                                weftModel = db.prod_Wefts.Find(item.Id);
                            }
                            map = new Mapper(config);
                            map.Map(item, weftModel);

                            if (item.Id <= 0)
                            {
                                db.prod_Wefts.Add(weftModel);
                                db.SaveChanges();
                            }
                            //Trans.Add(tranModel); 
                        }
                        //Add item in Taka Beam
                        TakaBeamModel tb;
                        decimal? totalnet = _Beamlist.Sum(k => k.Mtrs);
                        foreach (var item in _Beamlist)
                        {
                            var transid = item.Id;
                            tb = db.TakaBeams.FirstOrDefault(x => x.BeamId == item.Id && x.ProdId == _find.Id);
                                
                             //   GetByMasterIdAsync(_find.Id, item.Id, db);
                            if (tb == null)
                            {
                                tb = new TakaBeamModel();
                                tb.Per = (item.Mtrs * 100) / totalnet;
                                tb.Qty = (_find.GrossWt * tb.Per) / 100;

                                tb.Mtr = (_find.NetWt * tb.Per) / 100;
                                tb.ProdId = _find.Id;
                                tb.BeamId = item.Id;
                                tb.CreateDate = DateTime.Now;
                                tb.CreateUser = KontoGlobals.UserName;
                                tb.IsActive =true;
                                db.TakaBeams.Add(tb);
                                db.SaveChanges();
                            }
                            else
                            {
                                tb.Per = (item.Mtrs * 100) / totalnet;
                                tb.Qty = (_find.GrossWt * tb.Per) / 100;
                                tb.Mtr = (_find.NetWt * tb.Per) / 100;
                              //  tb.ModifyDate = DateTime.Now;
                               // tb.ModifyUser = KontoGlobals.UserName;
                            }
                            //Trans.Add(tranModel); 
                        }

                        //delete item from Prod Emp
                        foreach (var item in DelProdEmp)
                        {
                            if (item.Id <= 0) continue;
                            var _model = db.Prod_Emps.Find(item.Id);
                            if (_model != null)
                                _model.IsDeleted = true;
                        }
                        // delete from weft Item
                        foreach (var p in DelWeft)
                        {
                            if (p.Id <= 0) continue;
                            var prd = db.prod_Wefts.Find(p.Id);
                            if (prd != null)
                                prd.IsDeleted = true;
                        }
                        //sotock effect
                        var stk = db.StockTranses.Where(k => k.MasterRefId == _find.RowId).ToList();
                        if (stk != null)
                            db.StockTranses.RemoveRange(stk);

                        bool IsIssue = false;
                        string TableName = "Taka Production";
                        decimal RcptQty = _find.NetWt;
                        decimal IssueQty = 0;
                        decimal qty = _find.NetWt;
                        int pcs = 1;

                        StockEffect.StockTransProdEntry(_find, IsIssue,
                                   RcptQty, IssueQty, qty, pcs, TableName, db);

                        //if (ProdPara.AutoYarnConsumption)
                        //{
                        //    IsIssue = true;
                        //    ProdModel model;
                        //    var yarnlist = db.WeftItems.Where(k => k.IsActive && !k.IsDeleted && k.RefId == _find.ProductId).ToList();
                        //    if (yarnlist.Count > 0)
                        //    {
                        //        foreach (var item in yarnlist)
                        //        {
                        //            RcptQty = 0;
                        //            if (item.TypeId == 1)
                        //            {
                        //                if (_find.NetWt != 0 && item.Qty != null)
                        //                {
                        //                    IssueQty = ((decimal)_find.NetWt * (decimal)item.Qty) / (decimal)100;
                        //                    qty = ((decimal)_find.NetWt * (decimal)item.Qty) / (decimal)100;
                        //                }
                        //            }

                        //            pcs = 1;

                        //            model = new ProdModel();
                        //            map.Map(_find, model);
                        //            model.ProductId = (int)item.ProductId;
                        //            StockEffect.StockTransProdEntry(model, IsIssue, RcptQty, IssueQty, qty, pcs, TableName, db);
                        //            db.SaveChanges();
                        //        }
                        //    }
                        //    else
                        //    {
                        //        yarnlist = db.WeftItems.Where(k => k.IsActive && !k.IsDeleted && k.ItemId == _find.ProductId && k.MColorId == _find.ColorId).ToList();
                        //        foreach (var item in yarnlist)
                        //        {
                        //            RcptQty = 0;
                        //            if (item.IType == "T")
                        //            {
                        //                if (_find.NetWt != 0 && item.Qty != null && item.Totcard == 0 && item.Card == 0)
                        //                {
                        //                    IssueQty = ((decimal)_find.NetWt * (decimal)item.Qty) / (decimal)100;
                        //                    qty = ((decimal)_find.NetWt * (decimal)item.Qty) / (decimal)100;
                        //                }
                        //                else
                        //                {
                        //                    decimal lmtr = (item.Card / item.TotPick) / (decimal)39.37;
                        //                    IssueQty = ((decimal)_find.NetWt * (decimal)item.Qty) / lmtr;
                        //                    qty = ((decimal)_find.NetWt * (decimal)item.Qty);
                        //                }
                        //            }

                        //            pcs = 1;

                        //            model = new ProdModel();
                        //            map.Map(_find, model);
                        //            model.ProductId = (int)item.ProductId;
                        //            StockEffect.StockTransProdEntry(model, IsIssue, RcptQty, IssueQty, qty, pcs, TableName, db);
                        //            db.SaveChanges();
                        //        }
                        //    }
                        //}

                        db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Grn Save");
                        MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());

                    }
                }
            }

            if (IsSaved)
            {
                // NewRec();

                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage + " Taka No.: " + _find.VoucherNo, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (!this.OpenForLookup && newmode)
                {
                    if (this.voucherLookup1.GroupDto.PrintAfterSave && MessageBox.Show("Print Bill ?", "Print", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
            var ProdsEmp = ProdbindingSource.DataSource as List<Prod_EmpDto>;
            // var Weft = WeftbindingSource.DataSource as List<ProdWeftItemDTO>;

            if (Convert.ToInt32(divLookUpEdit.EditValue) <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Division", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                divLookUpEdit.Focus();
                return false;
            }
            else if (Convert.ToInt32(voucherLookup1.SelectedValue) <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Voucher", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherLookup1.Focus();
                return false;
            }
            else if (dt > KontoGlobals.ToDate || dt < KontoGlobals.FromDate)
            {
                MessageBoxAdv.Show(this, "Date out of financial range", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherDateEdit.Focus();
                return false;
            }
            else if (Convert.ToInt32(productLookup1.SelectedValue) <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Beam Product", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                productLookup1.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(TakaNotextEdit.Text) || TakaNotextEdit.Text.Length <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Taka No", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TakaNotextEdit.Focus();
                return false;
            }
            else if (MeterSpinEdit.Value <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Net Weight", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MeterSpinEdit.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(TakaNotextEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Taka No", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TakaNotextEdit.Focus();
                return false;
            }
            
            //else if (ProdsEmp.Count <= 0)
            //{
            //    MessageBoxAdv.Show(this, "Production Detail is required. !", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    ProdgridView.Focus();
            //    return false;
            //}
            else if (ProdsEmp.Count > 0)
            {
                var prodMtr = ProdsEmp.Sum(k => k.TotalMtrs);
                var nMtr = MeterSpinEdit.Value;
                if (prodMtr != nMtr)
                {
                    MessageBoxAdv.Show(this, "Employee meter should be matched with taka meter!", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    MeterSpinEdit.Focus();
                    return false;
                }
            }
            else if (!string.IsNullOrEmpty(TakaNotextEdit.Text))
            {
                var db = new KontoContext();
                int vid = (int)voucherLookup1.SelectedValue;
                var takaExists = db.Prods.FirstOrDefault(x => x.VoucherNo == TakaNotextEdit.Text
                          && x.CompId == KontoGlobals.CompanyId && x.YearId == KontoGlobals.YearId && x.Id != this.PrimaryKey
                          && x.VoucherId == vid && x.IsDeleted == false && x.IsActive);

                if (takaExists != null)
                {
                    MessageBoxAdv.Show(this, "Taka No already exists!!", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TakaNotextEdit.Focus();
                    return false;
                }
            }
            return true;
        }

        private void LoadData(ProdModel pdata)
        {
            KontoContext db = new KontoContext();

            this.PrimaryKey = pdata.Id;

            TakaNotextEdit.Text = pdata.VoucherNo;
            divLookUpEdit.EditValue = pdata.DivId;
            voucherLookup1.SelectedValue = pdata.VoucherId;
            voucherDateEdit.EditValue = KontoUtils.IToD(pdata.VoucherDate);
            productLookup1.SelectedValue = pdata.ProductId;
            productLookup1.SetGroup((int)pdata.ProductId);

            designLookup.SelectedValue = pdata.PlyProductId;
            if (pdata.PlyProductId > 0)
                designLookup.SetGroup((int)pdata.PlyProductId);
            MachineNolookUpEdit.EditValue = pdata.MacId;

            FoldDateEdit.DateTime = (DateTime)pdata.LoadingDate;

            if (pdata.ColorId != null)
            {
                colorLookup.SelectedValue = pdata.ColorId;
                colorLookup.SetGroup();
            }
            if (pdata.GradeId != null)
            {
                gradeLookup1.SelectedValue = pdata.GradeId;
                gradeLookup1.SetValue();
            }
            MeterSpinEdit.Value = pdata.NetWt;
            WeightSpinEdit.Value = pdata.GrossWt;
            TotalPcsSpinEdit.Value = pdata.Tops;

            FolderempLookup.SelectedValue = pdata.PackEmpId;
            FolderempLookup.SetGroup();
            pdata.CopsRate = FolderRateSpinEdit.Value;
            MendorEmpLookup.SelectedValue = pdata.JobId;
            MendorEmpLookup.SetGroup();
            pdata.BoxRate = MendorSpinEdit.Value;

            CheckerempLookup2.SelectedValue = pdata.CheckEmpId;
            CheckerempLookup2.SetGroup();
            CheckerRateSpinEdit.Value = pdata.FinQty;
            RemarkTextBoxExt.Text = pdata.Remark;

            var weftlist =
                (from p in db.prod_Wefts
                 join pd in db.Products on p.ProductId equals pd.Id into join_p
                 from pd in join_p.DefaultIfEmpty()
                 where p.ProdId == pdata.Id &&
               p.IsActive == true && p.IsDeleted == false
                 select new ProdWeftItemDTO()
                 {
                     ProductId = p.ProductId,
                     Denier = p.Denier,
                     PI = p.PI,
                     Qty = (p.Qty),
                     Id = p.Id,
                     Extra1 = p.Extra1,
                     Extra2 = p.Extra2,
                     IsActive = p.IsActive,
                     IsDeleted = p.IsDeleted,
                     ProdId = p.ProdId,
                     ProductName = pd.ProductName,
                     RowId = p.RowId
                 }
           ).ToList();

            var emplist = (from o in db.Prod_Emps
                           join em in db.Emps on o.EmpId equals em.Id into join_em
                           from em in join_em.DefaultIfEmpty()
                           join v in db.Vouchers on o.VoucherId equals v.Id into join_v
                           from v in join_v.DefaultIfEmpty()
                           where !o.IsDeleted && o.ProdId == pdata.Id
                           && v.VTypeId == (int)VoucherTypeEnum.TakaProd
                           select new
                           {
                               Id = o.Id,
                               VoucherId = o.VoucherId,
                               Amount = o.Amount,
                               CreateDate = o.CreateDate,
                               CreateUser = o.CreateUser,
                               DayMtrs = o.DayMtrs,
                               EmpId = o.EmpId,
                               EmpName = em.EmpName,
                               Extra1 = o.Extra1,
                               Extra2 = o.Extra2,
                               IpAddress = o.IpAddress,
                               IsActive = o.IsActive,
                               IsDeleted = o.IsDeleted,
                               LoadingTransId = o.LoadingTransId,
                               ModifyDate = o.ModifyDate,
                               ModifyUser = o.ModifyUser,
                               NightMtrs = o.NightMtrs,
                               ProdDate = o.ProdDate,
                               ProdId = o.ProdId,
                               Rate = o.Rate,
                               RowId = o.RowId,
                               TotalMtrs = o.TotalMtrs
                           }).ToList()
                            .Select(c => new Prod_EmpDto
                            {
                                Id = c.Id,
                                VoucherId = c.VoucherId,
                                Amount = c.Amount,
                                DayMtrs = c.DayMtrs,
                                EmpId = c.EmpId,
                                EmpName = c.EmpName,
                                Extra1 = c.Extra1,
                                Extra2 = c.Extra2,
                                IsDeleted = c.IsDeleted,
                                LoadingTransId = c.LoadingTransId,
                                NightMtrs = c.NightMtrs,
                                ProdDate = c.ProdDate,
                                ProdId = c.ProdId,
                                Rate = c.Rate,
                                TotalMtrs = c.TotalMtrs
                            }).ToList();

            BeamProdTrans = db.Database.SqlQuery<BeamStatusByMachineDto>(
                     "dbo.beam_status_by_machine @CompanyId={0},@vtypeid={1},@Status={2},@MachineId={3}",
                     KontoGlobals.CompanyId, (int)VoucherTypeEnum.BeamProd, "LOADED", pdata.MacId).ToList();

            WeftbindingSource.DataSource = weftlist;
            ProdbindingSource.DataSource = emplist;
            BeambindingSource.DataSource = BeamProdTrans;

            DelWeft = new List<ProdWeftItemDTO>();
            DelProdEmp = new List<Prod_EmpDto>();

            divLookUpEdit.Focus();

        }
        #endregion
    }
}