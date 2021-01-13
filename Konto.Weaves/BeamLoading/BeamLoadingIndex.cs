using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Data.Models.Masters.Dtos;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using Konto.Shared.Masters.Emp;
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
using Konto.Core.Shared.Libs;
using AutoMapper;

namespace Konto.Weaves.BeamLoading
{
    public partial class BeamLoadingIndex : KontoMetroForm
    {
        private List<ProdModel> FilterView = new List<ProdModel>();
        private List<Prod_EmpDto> DelTrans = new List<Prod_EmpDto>();
        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        public bool AddEdit = true;
        public bool ISCLS = false;
        public int ProdId = 0;

        public BeamLoadingIndex()
        {
            InitializeComponent();

            FillLookup();
            this.Load += BeamLoadingIndex_Load;

            okSimpleButton.Click += OkSimpleButton_Click;
            SelectionOksimpleButton.Click += SelectionOksimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            this.MainLayoutFile = KontoFileLayout.BeamLoading_Index;
            this.GridLayoutFile = KontoFileLayout.BeamLoading_Trans;

            gridControl1.ProcessGridKey += GridControl1_ProcessGridKey;
            gridView1.InitNewRow += GridView1_InitNewRow;
            gridView1.CellValueChanged += GridView1_CellValueChanged;
            gridView1.KeyDown += GridView1_KeyDown;
            gridControl1.Enter += GridControl1_Enter;
            //  gridView1.ValidateRow += GridView1_ValidateRow;
            gridView1.MouseUp += GridView1_MouseUp;
            gridView1.InvalidRowException += GridView1_InvalidRowException;
            gridView1.ShowingEditor += GridView1_ShowingEditor;
            gridView1.DoubleClick += GridView1_DoubleClick;
            gridView1.CustomDrawRowIndicator += GridView1_CustomDrawRowIndicator;

            EmprepositoryItemButtonEdit.ButtonClick += EmpRepositoryItemButtonEdit_ButtonClick;

            divLookUpEdit.EditValueChanged += DivLookUpEdit_EditValueChanged;
            BeamPositionlookUpEdit.EditValueChanged += BeamPositionlookUpEdit_EditValueChanged;
        }


        #region Event
        private void BeamLoadingIndex_Load(object sender, EventArgs e)
        {
            MtrspinEdit.Enabled = false;
            DenierspinEdit.Enabled = false;
            WastagespinEdit.Enabled = false;
            NoOfTakaSpinEdit.Enabled = false;
            LengthSpinEdit.Enabled = false;
            NetWeightSpinEdit.Enabled = false;
            EndsSpinEdit.Enabled = false;
            WidthSpinEdit.Enabled = false;
            PickSpinEdit.Enabled = false;
            BeamNotextEdit.Enabled = false;
            voucherLookup11.Enabled = false;
            voucherDateEdit.Enabled = false;
            BeamLookup.Enabled = false;
            YarnproductLookup.Enabled = false;
        }
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
        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                divLookUpEdit.Focus();
                return;
            }
            else if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new BeamLoadingList();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Beam Loading [View]";
            }
        }
        private void BeamPositionlookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (BeamPositionlookUpEdit.EditValue == null) return;
            if (MachineNolookUpEdit.EditValue == null) return;

            int Position =Convert.ToInt32(BeamPositionlookUpEdit.EditValue);

            //if (Position.ToUpper() != "OTHER")
            //{
                int MacId = Convert.ToInt32(MachineNolookUpEdit.EditValue);
                KontoContext _db = new KontoContext();
                var FreeMac = _db.Prods.Where(k => k.MacId == MacId
                            && k.IsActive && !k.IsDeleted && !k.IsClose &&
                           (k.SubGradeId == Position)
                            && k.CompId == (int)KontoGlobals.CompanyId
                            && k.ProdStatus == "LOADED" && k.Id != this.PrimaryKey).ToList();

                if (FreeMac.Count > 0)
                {
                    MessageBox.Show(BeamPositionlookUpEdit.Text + " Position Not Free..Please change Position.");
                    BeamPositionlookUpEdit.EditValue = null;
                }
            //}
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveDataAsync(true);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Beam Production Invoice Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }
        private void SelectionOksimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(divLookUpEdit.EditValue) <= 0)
                {
                    MessageBoxAdv.Show(this, "Please Select any Division", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    divLookUpEdit.Focus();
                    return;
                }

                var stf = new PendingBeamLoadingView();
                KontoContext _db = new KontoContext();

                var _list = new List<BeamProdDto>();

                var spcol = _db.SpCollections.FirstOrDefault(k => k.Id ==
                            (int)SpCollectionEnum.PendingBeamLoading);
                if (spcol == null)
                {
                    _list = _db.Database.SqlQuery<BeamProdDto>(
                        "dbo.PendingBeamLoading @CompanyId={0},@BLVoucherTypeID={1},@INVoucherTypeID={2},@divId={3}",
                 KontoGlobals.CompanyId, (int)VoucherTypeEnum.BeamProd, (int)VoucherTypeEnum.Inward, divLookUpEdit.EditValue).ToList();
                }
                else
                {
                    _list = _db.Database.SqlQuery<BeamProdDto>(
                    spcol.Name + " @CompanyId={0},@BLVoucherTypeID={1},@INVoucherTypeID={2},@divId={3}",
             KontoGlobals.CompanyId, (int)VoucherTypeEnum.BeamProd, (int)VoucherTypeEnum.Inward, divLookUpEdit.EditValue).ToList();
                }

                if (_list.Count == 0) return;

                stf.ItemList = _list;

                var showDialog = stf.ShowDialog();
                if (stf.SelectedRow != null)
                {
                    var Model = _db.Prods.FirstOrDefault(k => k.Id == stf.SelectedRow.Id);
                    LoadingDateEdit.DateTime = DateTime.Now;
                    voucherLookup11.SelectedValue = Model.VoucherId;
                    voucherDateEdit.EditValue = KontoUtils.IToD(Model.VoucherDate);
                    BeamLookup.SelectedValue = Model.ProductId;
                    BeamLookup.SetGroup((int)Model.ProductId);
                    if (Model.CopsProductId != null)
                    {
                        YarnproductLookup.SelectedValue = Model.CopsProductId;
                        YarnproductLookup.SetGroup((int)Model.CopsProductId);
                    }
                    if (Model.BoxProductId != null)
                    {
                        GreyproductLookup.SelectedValue = Model.BoxProductId;
                        GreyproductLookup.SetGroup((int)Model.BoxProductId);
                    }

                    BeamNotextEdit.Text = Model.VoucherNo;
                    MtrspinEdit.Value = Model.CopsWt;
                    DenierspinEdit.Value = Model.Pallet;
                    WastagespinEdit.Value = Model.CartnWt;
                    NoOfTakaSpinEdit.Value = Model.Cops;
                    LengthSpinEdit.Value = Model.Ply;
                    EndsSpinEdit.Value = Model.Tops;
                    WidthSpinEdit.Value = Model.GrossWt;
                    PickSpinEdit.Value = Model.TareWt;
                    NetWeightSpinEdit.Value = Model.NetWt;
                    ProdId = Model.Id;
                    MachineNolookUpEdit.Focus();
                }
                else
                {
                    MessageBox.Show("Please Select Beam!!!!");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Beam Production Invoice Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        #endregion

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
                var _PositionLists = (from p in db.Positions
                                      where p.IsActive && !p.IsDeleted
                                      select new BaseLookupDto()
                                      {
                                          DisplayText = p.PositionName,
                                          Id = p.Id
                                      }).ToList();
                divLookUpEdit.Properties.DataSource = _divLists;
                BeamPositionlookUpEdit.Properties.DataSource = _PositionLists;

                //var _macList = (from p in db.machineMasters
                //                where p.IsActive && !p.IsDeleted
                //                select new BaseLookupDto()
                //                {
                //                    DisplayText = p.MachineName,
                //                    Id = p.Id
                //                }).ToList();

                //MachineNolookUpEdit.Properties.DataSource = _macList;
                //    List<ComboBoxPairs> cbp = new List<ComboBoxPairs>
                //{
                //    new ComboBoxPairs("Lower", "L"),
                //    new ComboBoxPairs("Middle", "M"),
                //    new ComboBoxPairs("Upper", "U"),
                //    new ComboBoxPairs("Other", "O")
                //};

            }
        }
        private Prod_EmpDto PreOpenLookup()
        {
            gridView1.GetRow(gridView1.FocusedRowHandle);
            if (gridView1.GetRow(gridView1.FocusedRowHandle) == null)
            {
                gridView1.AddNewRow();
            }
            var dr = (Prod_EmpDto)gridView1.GetRow(gridView1.FocusedRowHandle);
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
                var model = frm.SelectedItem as EmpLookupDto;

                gridView1.FocusedColumn = gridView1.GetNearestCanFocusedColumn(gridView1.FocusedColumn);
            }
        }


        #endregion
        #region GridView
        private void GridView1_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var itm = gridView1.GetFocusedRow() as BillTransDto;
            if (itm == null) return;
            if (!"Pcs,Qty,ProductName,ColorName,GradeName,DesignName,LotNo,UomId".Contains(gridView1.FocusedColumn.FieldName)) return;
            if (Convert.ToInt32(itm.RefId) > 0)
                e.Cancel = true;
        }
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
            if (!(gridView1.GetRow(e.RowHandle) is BillTransDto er)) return;

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
                var row = view.GetRow(view.FocusedRowHandle) as Prod_EmpDto;
                view.DeleteRow(view.FocusedRowHandle);
                DelTrans.Add(row);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                GridView view = sender as GridView;
                if (gridView1.FocusedColumn.FieldName == "EmpName")
                {
                    view.SetRowCellValue(view.FocusedRowHandle, colEmpName, string.Empty);
                    view.SetRowCellValue(view.FocusedRowHandle, colEmpId, 0);
                }
            }
        }
        private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var rw = gridView1.GetRow(e.RowHandle) as Prod_EmpDto;
            rw.Id = -1 * gridView1.RowCount;
        }
        private void GridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                var dr = PreOpenLookup();
                if (dr == null) return;
                if (gridView1.FocusedColumn.FieldName == "EmpName")
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
                Log.Error(ex, "Beam Loading GridControl KeyDown");
                MessageBoxAdv.Show(this, "Error Lookup Setup !!", "Exception ", ex.ToString());

            }
        }
        private void EmpRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dr = PreOpenLookup();
            if (dr != null)
                OpenEmpLookup((int)dr.EmpId, dr);
        }
        private void GridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
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
            this.Text = "Beam Production [Add New]";

            divLookUpEdit.EditValue = null;
            this.ActiveControl = voucherLookup11.buttonEdit1;
            voucherLookup11.SetDefault();
            voucherDateEdit.EditValue = DateTime.Now;
            BeamLookup.SetEmpty();
            YarnproductLookup.SetEmpty();
            GreyproductLookup.SetEmpty();

            MachineNolookUpEdit.EditValue = null;
            BeamPositionlookUpEdit.EditValue = null;

            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;

            DelTrans = new List<Prod_EmpDto>();
            this.bindingSource1.DataSource = new List<Prod_EmpDto>();
            divLookUpEdit.Focus();
        }
        public override void ResetPage()
        {
            base.ResetPage();
            divLookUpEdit.EditValue = 1;

            voucherLookup11.SetDefault();
            voucherDateEdit.EditValue = DateTime.Now;
            BeamLookup.SetEmpty();
            YarnproductLookup.SetEmpty();
            GreyproductLookup.SetEmpty();

            BeamNotextEdit.Text = string.Empty;
            MtrspinEdit.Value = 0;
            DenierspinEdit.Value = 0;
            WastagespinEdit.Value = 0;
            NoOfTakaSpinEdit.Value = 0;
            LengthSpinEdit.Value = 0;
            EndsSpinEdit.Value = 0;
            WidthSpinEdit.Value = 0;
            PickSpinEdit.Value = 0;
            NetWeightSpinEdit.Value = 0;

            DelTrans = new List<Prod_EmpDto>();
            this.bindingSource1.DataSource = new List<Prod_EmpDto>();

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


            if (Convert.ToInt32(voucherLookup11.SelectedValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "VoucherId", Operation = Op.Equals, Value = Convert.ToInt32(voucherLookup11.SelectedValue) });
            }

            if (Convert.ToInt32(divLookUpEdit.EditValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "DivId", Operation = Op.Equals, Value = Convert.ToInt32(divLookUpEdit.EditValue) });
            }
            if (Convert.ToInt32(BeamLookup.SelectedValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "ProductId", Operation = Op.Equals, Value = Convert.ToInt32(BeamLookup.SelectedValue) });
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
            if (ProdId == 0) return;

            bool IsSaved = false;
            if (!ValidateData()) return;

            var _find = new ProdModel();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Prod_EmpDto, Prod_EmpModel>().ForMember(x => x.Id, p => p.Ignore());
            });
            var map = new Mapper(config);
            var ProdEmpList = bindingSource1.DataSource as List<Prod_EmpDto>;
            using (var db = new KontoContext())
            {
                using (var _tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        _find = db.Prods.Find(ProdId);
                        if (_find == null) return;
                        //if (_find.IsClose)
                        //{
                        //    MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", "Beam is already close...");
                        //    return;
                        //}
                        if (divLookUpEdit.EditValue != null)
                            _find.DivId = Convert.ToInt32(divLookUpEdit.EditValue);

                        _find.VoucherId = Convert.ToInt32(voucherLookup11.SelectedValue);

                        _find.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
                        _find.ProductId = Convert.ToInt32(BeamLookup.SelectedValue);
                        _find.CopsProductId = Convert.ToInt32(YarnproductLookup.SelectedValue);
                        _find.BoxProductId = Convert.ToInt32(GreyproductLookup.SelectedValue);

                        _find.ProdStatus = "LOADED";
                        _find.MacId = (int)MachineNolookUpEdit.EditValue;

                        if(!string.IsNullOrEmpty(BeamPositionlookUpEdit.EditValue.ToString()))
                        _find.SubGradeId =Convert.ToInt32( BeamPositionlookUpEdit.EditValue);
                        _find.LoadingDate = LoadingDateEdit.DateTime;

                        _find.ModifyDate = DateTime.Now;
                        _find.ModifyUser = KontoGlobals.UserName;
                         
                        _find.VoucherNo = BeamNotextEdit.Text;
                        _find.CopsWt = MtrspinEdit.Value;

                        _find.Pallet = Convert.ToInt32(DenierspinEdit.Value);
                        _find.CartnWt = WastagespinEdit.Value;
                        _find.Cops = Convert.ToInt32(NoOfTakaSpinEdit.Value);
                        _find.Ply = Convert.ToInt32(LengthSpinEdit.Value);
                        _find.Tops = Convert.ToInt32(EndsSpinEdit.Value);
                        _find.GrossWt = WidthSpinEdit.Value;
                        _find.TareWt = PickSpinEdit.Value;
                        _find.NetWt = NetWeightSpinEdit.Value;

                        var loadtrans = new LoadingTranModel();
                        if (AddEdit)
                        {
                            loadtrans.ProdId = _find.Id;
                            loadtrans.BeamPotion = BeamPositionlookUpEdit.Text;
                            loadtrans.DivId = _find.DivId;
                            loadtrans.IsDeleted = false;
                            loadtrans.LoadingDate = _find.LoadingDate;
                            loadtrans.MacId = _find.MacId;
                            loadtrans.ProductId = _find.ProductId;
                            loadtrans.CreateDate = DateTime.Now;
                            loadtrans.CreateUser = KontoGlobals.UserName;
                            if (!_find.IsClose)
                                loadtrans.ProdStatus = "LOADED";
                            else
                                loadtrans.ProdStatus = "CLOSE";

                            db.loadingTranModels.Add(loadtrans);
                            db.SaveChanges();
                        }
                        else if ((ISCLS != _find.IsClose))
                        {
                            loadtrans = db.loadingTranModels.FirstOrDefault(k => k.ProdId == _find.Id
                           && (k.ProdStatus.ToUpper() == "CLOSE" || k.ProdStatus.ToUpper() == "LOADED"));

                            if (!_find.IsClose)
                                loadtrans.ProdStatus = "LOADED";
                            else
                                loadtrans.ProdStatus = "CLOSE";

                        }
                        else
                        {
                            loadtrans = db.loadingTranModels.FirstOrDefault(k => k.ProdId == _find.Id
                                   && k.ProdStatus.ToUpper() == "LOADED");
                        }

                        var tranModel = new Prod_EmpModel();
                        foreach (var item in ProdEmpList)
                        {
                            var transid = item.Id;

                            if (item.Id > 0)
                            {
                                tranModel = db.Prod_Emps.Find(item.Id);
                            }
                            map = new Mapper(config);
                            map.Map(item, tranModel);

                            if (item.Id <= 0)
                            {
                                tranModel.ProdId = _find.Id;
                                tranModel.LoadingTransId = loadtrans.Id;
                                tranModel.VoucherId = (int)VoucherTypeEnum.BeamProd;
                                tranModel.CreateDate = DateTime.Now;
                                tranModel.CreateUser = KontoGlobals.UserName;

                                db.Prod_Emps.Add(tranModel);
                                db.SaveChanges();
                            }
                        }
                        foreach (var item in DelTrans)
                        {
                            if (item.Id > 0)
                            {
                                tranModel = db.Prod_Emps.Find(item.Id);
                                if (tranModel != null)
                                    tranModel.IsDeleted = true;
                            }
                        }

                        db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Beam Loading" + " Save");
                        MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());

                    }
                }
            }

            if (IsSaved)
            {
                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage + " Beam No.: " + _find.VoucherNo, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (!this.OpenForLookup && newmode)
                {
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
            var LoadDate = 0;

            var trans = bindingSource1.DataSource as List<Prod_EmpDto>;

            if (LoadingDateEdit.DateTime != null)
                LoadDate = Convert.ToInt32(LoadingDateEdit.DateTime.ToString("yyyyMMdd"));

            if (Convert.ToInt32(divLookUpEdit.EditValue) <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Division", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                divLookUpEdit.Focus();
                return false;
            }
            if (Convert.ToInt32(MachineNolookUpEdit.EditValue) <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Machine", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MachineNolookUpEdit.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(BeamPositionlookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Beam Position", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BeamPositionlookUpEdit.Focus();
                return false;
            }
            else if (Convert.ToInt32(voucherLookup11.SelectedValue) <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Voucher", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherLookup11.Focus();
                return false;
            }
            else if (LoadDate > KontoGlobals.ToDate || LoadDate < KontoGlobals.FromDate)
            {
                MessageBoxAdv.Show(this, "Date out of financial range", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadingDateEdit.Focus();
                return false;
            }
            //else if (Convert.ToInt32(GreyproductLookup.SelectedValue) <= 0)
            //{
            //    MessageBoxAdv.Show(this, "Invalid Grey Product", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    GreyproductLookup.Focus();
            //    return false;
            //}
            else if (string.IsNullOrWhiteSpace(BeamNotextEdit.Text) || BeamNotextEdit.Text.Length <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Beam No", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BeamNotextEdit.Focus();
                return false;
            }
            else if (NetWeightSpinEdit.Value <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Net Weight", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                NetWeightSpinEdit.Focus();
                return false;
            }
            else if (LoadDate < dt)
            {
                MessageBoxAdv.Show(this, "Load Date Can Not be grater than Voucherdate", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadingDateEdit.Focus();
                return false;
            }
            //else if (gridView1.RowCount == 1)
            //{
            //    MessageBoxAdv.Show(this, "At Least One Employee Should be Entered", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    gridView1.Focus();
            //    return false;
            //}
            //else if (trans.Any(x => x.EmpId == 0))
            //{
            //    MessageBoxAdv.Show(this, "Empty Employee Can bot be accepted", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    gridView1.Focus();
            //    return false;
            //}

            return true;
        }

        private void LoadData(ProdModel pdata)
        {
            ProdId = pdata.Id;
            divLookUpEdit.EditValue = pdata.DivId;
            MachineNolookUpEdit.EditValue = pdata.MacId;
            BeamPositionlookUpEdit.EditValue = pdata.SubGradeId;
            LoadingDateEdit.EditValue = pdata.LoadingDate;

            voucherLookup11.SelectedValue = pdata.VoucherId;
            voucherDateEdit.EditValue = KontoUtils.IToD(pdata.VoucherDate);
            BeamLookup.SelectedValue = pdata.ProductId;
            BeamLookup.SetGroup((int)pdata.ProductId);
            YarnproductLookup.SelectedValue = pdata.CopsProductId;
            YarnproductLookup.SetGroup((int)pdata.CopsProductId);
            GreyproductLookup.SelectedValue = pdata.BoxProductId;
            GreyproductLookup.SetGroup((int)pdata.BoxProductId);

            BeamNotextEdit.Text = pdata.VoucherNo;
            MtrspinEdit.Value = pdata.CopsWt;
            DenierspinEdit.Value = pdata.Pallet;
            WastagespinEdit.Value = pdata.CartnWt;
            NoOfTakaSpinEdit.Value = pdata.Cops;
            LengthSpinEdit.Value = pdata.Ply;
            EndsSpinEdit.Value = pdata.Tops;
            WidthSpinEdit.Value = pdata.GrossWt;
            PickSpinEdit.Value = pdata.TareWt;
            NetWeightSpinEdit.Value = pdata.NetWt;

            ISCLS = pdata.IsClose;
            AddEdit = false;
            using (KontoContext db = new KontoContext())
            {

                var list = (from o in db.Prod_Emps
                            join em in db.Emps on o.EmpId equals em.Id into join_em
                            from em in join_em.DefaultIfEmpty()
                            join v in db.Vouchers on o.VoucherId equals v.Id into join_v
                            from v in join_v.DefaultIfEmpty()
                            where !o.IsDeleted && o.ProdId == pdata.Id
                            && v.VTypeId == (int)VoucherTypeEnum.BeamProd
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

                this.bindingSource1.DataSource = list;
            }

            divLookUpEdit.Focus();
        }

        #endregion
    }
}
