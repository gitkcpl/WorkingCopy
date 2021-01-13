using AutoMapper;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using GrapeCity.ActiveReports;
using Konto.App.Shared;
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
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Yarn.YarnProduction
{
    public partial class YarnProdIndex : KontoMetroForm
    {

        private List<BeamProdDto> DelProdTrans = new List<BeamProdDto>();
        private List<ProdModel> FilterView = new List<ProdModel>();
        private List<BeamProdDto> ProdTrans = new List<BeamProdDto>();

        private bool NewAfterSave = false;
        TextEdit headerEdit = new TextEdit();

        public YarnProdIndex()
        {
            InitializeComponent();

            FillLookup();
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            this.Load += TakaProdIndex_Load;

            this.MainLayoutFile = KontoFileLayout.Yarn_Packing_Index;
            this.GridLayoutFile = KontoFileLayout.Yarn_Packing_Trans ;

            //transgridControl.ProcessGridKey += ProdGridControl_ProcessGridKey;
            //transgridView.InitNewRow += transgridView_InitNewRow;
            //transgridView.CellValueChanged += transgridView_CellValueChanged;
            //transgridView.KeyDown += transgridView_KeyDown;
            //transgridControl.Enter += ProdGridControl_Enter;
            ////  gridView1.ValidateRow += GridView1_ValidateRow;
            //transgridView.MouseUp += transgridView_MouseUp;
            //transgridView.InvalidRowException += transgridView_InvalidRowException;
            //transgridView.DoubleClick += transgridView_DoubleClick;

            transgridView.ShowingEditor += transgridView_ShowingEditor;
            transgridView.CustomDrawRowIndicator += transgridView_CustomDrawRowIndicator;

            JobNoLookUpEdit.EditValueChanged += JobNoLookUpEdit_EditValueChanged;
            //BatchNoLookUpEdit.EditValueChanged += BatchNoLookUpEdit_EditValueChanged;

            CopsSpinEdit.ValueChanged += CopsSpinEdit_ValueChanged;
            CopsWeightSpinEdit.ValueChanged += CopsWeightSpinEdit_ValueChanged;
            BoxWeightSpinEdit.ValueChanged += BoxWeightSpinEdit_ValueChanged;
            TareWeightSpinEdit.ValueChanged += TareWeightSpinEdit_ValueChanged;
            GrossweightSpinEdit.ValueChanged += GrossweightSpinEdit_ValueChanged;
            voucherLookup.SelectedValueChanged += VoucherLookup_SelectedValueChanged;
        }

        private void VoucherLookup_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.PrimaryKey == 0 && Convert.ToInt32(voucherLookup.SelectedValue) > 0)
            {
                BoxNotextEdit.Text = "New-" + DbUtils.NextSerialNo(Convert.ToInt32(voucherLookup.SelectedValue), 1);
            }
        }

        private void GrossweightSpinEdit_ValueChanged(object sender, EventArgs e)
        {
            NetWeightSpinEdit.Value = GrossweightSpinEdit.Value - TareWeightSpinEdit.Value;
        }

        private void TareWeightSpinEdit_ValueChanged(object sender, EventArgs e)
        {
            NetWeightSpinEdit.Value = GrossweightSpinEdit.Value - TareWeightSpinEdit.Value;
        }

        private void BoxWeightSpinEdit_ValueChanged(object sender, EventArgs e)
        {
            decimal tareWt = 0;
            
                tareWt = (CopsSpinEdit.Value * CopsWeightSpinEdit.Value) + BoxWeightSpinEdit.Value;

            TareWeightSpinEdit.Value = (decimal)tareWt;
            NetWeightSpinEdit.Value = GrossweightSpinEdit.Value - tareWt;
        }

        private void CopsWeightSpinEdit_ValueChanged(object sender, EventArgs e)
        {
            decimal tareWt = 0;
           
                tareWt = (CopsSpinEdit.Value * CopsWeightSpinEdit.Value) + BoxWeightSpinEdit.Value;

            TareWeightSpinEdit.Value = tareWt;
            NetWeightSpinEdit.Value = GrossweightSpinEdit.Value - tareWt;
        }

        private void CopsSpinEdit_ValueChanged(object sender, EventArgs e)
        {
            
            decimal tareWt = 0;
          
                tareWt = (CopsSpinEdit.Value * CopsWeightSpinEdit.Value) + BoxWeightSpinEdit.Value;

            TareWeightSpinEdit.Value = tareWt;
            NetWeightSpinEdit.Value = GrossweightSpinEdit.Value - tareWt;
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
                var _jobList = (from p in db.jobCards

                                where p.IsActive && !p.IsDeleted
                                select new BaseLookupDto()
                                {
                                    DisplayText = p.VoucherNo,
                                    Id = p.Id
                                }).ToList();
                //var _batchList = (from p in db.jobCards
                //                  where p.IsActive && !p.IsDeleted
                //                  select new BaseLookupDto()
                //                  {
                //                      DisplayText = p.VoucherNo,
                //                      Id = p.Id
                //                  }).ToList();
                var _packList = (from p in db.PackingTypes
                                 where p.IsActive && !p.IsDeleted
                                 select new BaseLookupDto()
                                 {
                                     DisplayText = p.TypeName,
                                     Id = p.Id
                                 }).ToList();
                List<ComboBoxPairs> cbp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Fresh Packing", "Fresh Packing"),
                new ComboBoxPairs("Return Goods", "Return Goods"),
                new ComboBoxPairs("Repacking", "Repacking"),
            };

                List<ComboBoxPairs> cbp1 = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("S", "S"),
                new ComboBoxPairs("Z", "Z"),
                new ComboBoxPairs("0", "0")
            };
                MachineNolookUpEdit.Properties.DataSource = _macList;
                divLookUpEdit.Properties.DataSource = _divLists;
                JobNoLookUpEdit.Properties.DataSource = _jobList;//From Batch.. from job card pending
                // BatchNoLookUpEdit.Properties.DataSource = _batchList;
                MachineNolookUpEdit.Properties.DataSource = _macList;
                PackTypeLookUpEdit.Properties.DataSource = _packList;
                PackingTypeLookUpEdit.Properties.DataSource = cbp;
                TwistLookUpEdit.Properties.DataSource = cbp1;
            }
        }
        private Prod_EmpDto PreOpenLookup()
        {
            transgridView.GetRow(transgridView.FocusedRowHandle);
            if (transgridView.GetRow(transgridView.FocusedRowHandle) == null)
            {
                transgridView.AddNewRow();
            }
            var dr = (Prod_EmpDto)transgridView.GetRow(transgridView.FocusedRowHandle);
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

                transgridView.FocusedColumn = transgridView.GetNearestCanFocusedColumn(transgridView.FocusedColumn);
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

                transgridView.FocusedColumn = transgridView.GetNearestCanFocusedColumn(transgridView.FocusedColumn);
            }
        }
        #endregion
        #region transgridView
        private void transgridView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var itm = transgridView.GetFocusedRow() as Prod_EmpDto;
            if (itm == null) return;
            if (!"EmpName,Extra1".Contains(transgridView.FocusedColumn.FieldName)) return;

        }
        private void transgridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.Appearance.ForeColor = Color.FromArgb(227, 22, 91);
            if (e.RowHandle < 0)
                e.Info.DisplayText = "";
            else
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        #region Comment

        //private void transgridView_MouseUp(object sender, MouseEventArgs e)
        //{
        //    DevExpress.Utils.DXMouseEventArgs args = (e as DevExpress.Utils.DXMouseEventArgs);
        //    GridView view = sender as GridView;
        //    GridHitInfo hi = view.CalcHitInfo(args.Location);
        //    if (hi.InColumn)
        //    {
        //        GridViewInfo ViewInfo = view.GetViewInfo() as GridViewInfo;

        //        if ((e.Button & MouseButtons.Left) != 0)
        //        {
        //            if (ViewInfo.ColumnsInfo[hi.Column].CaptionRect.Contains(new Point(e.X, e.Y)))
        //            {
        //                ViewInfo.SelectionInfo.ClearPressedInfo();
        //                args.Handled = true;
        //            }
        //        }
        //    }
        //}
        //private void transgridView_DoubleClick(object sender, EventArgs e)
        //{
        //    DevExpress.Utils.DXMouseEventArgs args = (e as DevExpress.Utils.DXMouseEventArgs);
        //    GridView view = sender as GridView;
        //    GridHitInfo hi = view.CalcHitInfo(args.Location);
        //    if (hi.InColumn)
        //    {
        //        GridViewInfo vi = view.GetViewInfo() as GridViewInfo;
        //        Rectangle bounds = vi.ColumnsInfo[hi.Column].Bounds;
        //        bounds.Width -= 10;
        //        bounds.Height -= 3;
        //        bounds.Y += 3;
        //        headerEdit.SetBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);
        //        headerEdit.EditValue = hi.Column.Caption;
        //        headerEdit.Show();
        //        headerEdit.Focus();
        //        activeCol = hi.Column;
        //    }
        //}
        // private void ProdGridControl_Enter(object sender, EventArgs e)
        //{
        //    transgridView.FocusedColumn = transgridView.VisibleColumns[0];
        //}
        //private void transgridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //{
        //    if (e.Column == null) return;
        //    if (!(transgridView.GetRow(e.RowHandle) is Prod_EmpDto er)) return;
        //    if (e.Column.FieldName == "NightMtrs")
        //    {
        //        er.TotalMtrs = er.NightMtrs + er.DayMtrs;
        //        if (er.TotalMtrs > 0)
        //        {
        //            er.Amount = er.Rate * er.TotalMtrs;
        //        }
        //    }
        //    if (e.Column.FieldName == "DayMtrs")
        //    {
        //        er.TotalMtrs = er.NightMtrs + er.DayMtrs;
        //        if (er.TotalMtrs > 0)
        //        {
        //            er.Amount = er.Rate * er.TotalMtrs;
        //        }
        //    }
        //}
        //private void transgridView_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode != Keys.Delete) return;

        //    if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
        //    {
        //        if (MessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo) !=
        //          DialogResult.Yes)
        //            return;
        //        GridView view = sender as GridView;
        //        var row = view.GetRow(view.FocusedRowHandle) as BeamProdDto;
        //        view.DeleteRow(view.FocusedRowHandle);
        //        DelProdTrans.Add(row);
        //    }
        //    else if (e.KeyCode == Keys.Delete)
        //    {
        //        GridView view = sender as GridView;
        //        //if (transgridView.FocusedColumn.FieldName == "EmpName")
        //        //{
        //        //    view.SetRowCellValue(view.FocusedRowHandle, colEmpName, string.Empty);
        //        //    view.SetRowCellValue(view.FocusedRowHandle, colEmpId, 0);
        //        //}
        //    }
        //}
        //private void transgridView_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        //{
        //    var rw = transgridView.GetRow(e.RowHandle) as Prod_EmpDto;
        //    rw.Id = -1 * transgridView.RowCount;
        //}
        //private void ProdGridControl_ProcessGridKey(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        var dr = PreOpenLookup();
        //        if (dr == null) return;
        //        if (transgridView.FocusedColumn.FieldName == "EmpName")
        //        {
        //            if (e.KeyCode == Keys.Enter)
        //            {
        //                if (dr.EmpId == 0 || dr.EmpId == null)
        //                {
        //                    OpenEmpLookup(0, dr);
        //                    // e.Handled = true;
        //                }
        //            }
        //            else if (e.KeyCode == Keys.F1)
        //            {
        //                OpenEmpLookup((int)dr.EmpId, dr);
        //                e.Handled = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex, "Taka Prod GridControl KeyDown");
        //        MessageBoxAdv.Show(this, "Error Lookup Setup !!", "Exception ", ex.ToString());

        //    }
        //}
        //private void EmpRepositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    var dr = PreOpenLookup();
        //    if (dr != null)
        //        OpenEmpLookup((int)dr.EmpId, dr);
        //}
        //private void transgridView_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        //{
        //    e.ExceptionMode = ExceptionMode.NoAction;
        //}
        #endregion
        #endregion

        #region Event
        private void TakaProdIndex_Load(object sender, EventArgs e)
        {
            PlySpinEdit.Value = 0;
            PackingCopsSpinEdit.Value = 0;
            PackingQtySpinEdit.Value = 0;
            CopsWeightSpinEdit.Value = 0;
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
                var _ListView = new YarnProdListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Yarn Production [View]";
            }
            else if (tabControlAdv1.SelectedIndex == 3)
            {
                if (tabPageAdv4.Controls.Count > 0) return;

                this.Text = "Yarn Production [ReportView]";
                var _frm = Activator.CreateInstance("Konto.Reporting", "Konto.Reporting.Para.YarnPara.YarnParaMainView").Unwrap() as KontoForm;

                _frm.TopLevel = false;
                _frm.Parent = tabPageAdv4;
                //  _frm.ReportFilterType = "TP";
                _frm.Location = new Point(tabPageAdv4.Location.X + tabPageAdv4.Width / 2 - _frm.Width / 2, tabPageAdv4.Location.Y + tabPageAdv4.Height / 2 - _frm.Height / 2);
                _frm.Show();// = true;

            }
        }
        //private void BatchNoLookUpEdit_EditValueChanged(object sender, EventArgs e)
        //{
        //    if (BatchNoLookUpEdit.EditValue != null)
        //        using (KontoContext _db = new KontoContext())
        //        {
        //            int? Bid = Convert.ToInt32(BatchNoLookUpEdit.EditValue);
        //            KontoContext db = new KontoContext();

        //            var batchDetail = db.batches.FirstOrDefault(k => k.Id == Bid);
        //            if (batchDetail != null)
        //            {
        //                ProductLookup.SelectedValue = batchDetail.ItemId;
        //                colorLookup.SelectedValue = batchDetail.ShadeId;
        //            }

        //            ProdTrans = ((from p in _db.Prods
        //                          join b in _db.batches on p.BatchId equals b.Id into join_b
        //                          from b in join_b.DefaultIfEmpty()
        //                          join g in _db.Grades on p.GradeId equals g.Id into join_g
        //                          from g in join_g.DefaultIfEmpty()
        //                          join m in _db.machineMasters on p.MacId equals m.Id into join_m
        //                          from m in join_m.DefaultIfEmpty()
        //                          join bo in _db.Products on p.BoxProductId equals bo.Id into join_bo
        //                          from bo in join_bo.DefaultIfEmpty()
        //                          join sg in _db.Grades on p.SubGradeId equals sg.Id into join_sg
        //                          from sg in join_sg.DefaultIfEmpty()
        //                          orderby p.Id
        //                          where !p.IsDeleted && p.VTypeId == (int)VoucherTypeEnum.YarnProd
        //                          && p.BatchId == Bid
        //                          select new BeamProdDto()
        //                          {
        //                              VoucherNo = p.VoucherNo,
        //                              BatchName = b.VoucherNo,
        //                              GradeName = g.GradeName,
        //                              MachineName = m.MachineName,
        //                              Cops = p.Cops,
        //                              CopsWt = p.CopsWt,
        //                              BoxWt = p.BoxWt,
        //                              GrossWt = p.GrossWt,
        //                              TareWt = p.TareWt,
        //                              NetWt = p.NetWt,
        //                              BoxItem = bo.ProductName,
        //                              SubGradeName = sg.GradeName,
        //                          }).ToList());
        //        }
        //}
        private void JobNoLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (JobNoLookUpEdit.EditValue == null) return;
            using (KontoContext _db = new KontoContext())
            {
                int? jId = Convert.ToInt32(JobNoLookUpEdit.EditValue);
                decimal NoOfCones = 0;
                decimal Qty = 0;

                //// From Jobcard
                //var batchDetail = _db.jobCards.FirstOrDefault(k => k.Id == jId && !k.IsDeleted);
                //if (batchDetail != null)
                //{
                //    ProductLookup.SelectedValue = batchDetail.ProductId;
                //    ProductLookup.SetGroup((int)batchDetail.ProductId);
                //    colorLookup.SelectedValue = batchDetail.ColorId;
                //    colorLookup.SetGroup();
                //    if (batchDetail.NoOfCones != null)
                //        NoOfCones = (decimal)batchDetail.NoOfCones;

                //    PackingCopsSpinEdit.Value = NoOfCones;

                //    if (batchDetail.Qty != null)
                //        Qty = (decimal)batchDetail.Qty;
                //    PackingQtySpinEdit.Value = Qty;
                //}

                //from Batch
                var batchDetail = _db.jobCards.FirstOrDefault(k => k.Id == jId);
                if (batchDetail != null)
                {
                    ProductLookup.SelectedValue = batchDetail.ProductId;
                    ProductLookup.SetGroup((int)batchDetail.ProductId);
                    colorLookup.SelectedValue = batchDetail.ColorId;
                    colorLookup.SetGroup();
                }
                ProdTrans = (from p in _db.Prods
                             join b in _db.jobCards on p.BatchId equals b.Id into join_b
                             from b in join_b.DefaultIfEmpty()
                             join g in _db.Grades on p.GradeId equals g.Id into join_g
                             from g in join_g.DefaultIfEmpty()
                             join m in _db.MachineMasters on p.MacId equals m.Id into join_m
                             from m in join_m.DefaultIfEmpty()
                             join bo in _db.Products on p.BoxProductId equals bo.Id into join_bo
                             from bo in join_bo.DefaultIfEmpty()
                             join sg in _db.Grades on p.SubGradeId equals sg.Id into join_sg
                             from sg in join_sg.DefaultIfEmpty()
                             join v in _db.Vouchers on p.VoucherId equals v.Id into join_v
                             from v in join_v.DefaultIfEmpty()
                             orderby p.Id
                             where !p.IsDeleted && v.VTypeId == (int)VoucherTypeEnum.YarnProd
                             //&& p.RefSCId == jId //Use with Jobcard
                             && p.BatchId == jId //Batch
                             select new BeamProdDto()
                             {
                                 VoucherNo = p.VoucherNo,
                                 Cops = p.Cops,
                                 CopsWt = p.CopsWt,
                                 BoxWt = p.BoxWt,
                                 GrossWt = p.GrossWt,
                                 TareWt = p.TareWt,
                                 NetWt = p.NetWt,
                                 BoxItem = bo.ProductName,
                                 SubGradeName = sg.GradeName,
                                 BatchName = b.VoucherNo,
                                 GradeName = g.GradeName,
                                 MachineName = m.MachineName,
                             }).ToList();

                transbindingSource.DataSource = ProdTrans;
                if (ProdTrans.Sum(k => k.Cops) != null)
                    BalanceCopsSpinEdit.Value = NoOfCones - (decimal)ProdTrans.Sum(k => k.Cops);

                BalQtySpinEdit.Value = Qty - ProdTrans.Sum(k => k.NetWt);
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
                Log.Error(ex, "Yarn Production Invoice Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
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

                rpt.Load(new FileInfo("reg\\Doc\\PackingSlip.rdlx"));

                rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);

                doc.Parameters["FrmBill"].CurrentValue = BoxNotextEdit.Text;
                doc.Parameters["ToBill"].CurrentValue = BoxNotextEdit.Text;
                doc.Parameters["VoucherID"].CurrentValue = Convert.ToInt32(voucherLookup.SelectedValue);
                var frm = new KontoRepViewer(doc);
                frm.Text = "Packing Slip Preview";
                var _tab = this.Parent.Parent as TabControlAdv;
                if (_tab == null) return;
                var pg1 = new TabPageAdv();
                pg1.Text = "Packing Slip Print";
                _tab.TabPages.Add(pg1);
                _tab.SelectedTab = pg1;
                frm.TopLevel = false;
                frm.Parent = pg1;
                frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                frm.Show();// = true;

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Packing Slip Print");
                MessageBoxAdv.Show(this, "Error While Print !!", "Exception ", ex.ToString());

            }
        }
        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<ProdModel>();
            this.Text = "Yarn Production [Add New]";

            BoxNotextEdit.Text = "New";

            if (!NewAfterSave)
            {
                PackingTypeLookUpEdit.EditValue = "Fresh Packing";
                divLookUpEdit.EditValue = 1;
                this.ActiveControl = voucherLookup.buttonEdit1;
                
                voucherDateEdit.EditValue = DateTime.Now;

                divLookUpEdit.EditValue = 1;

                ProductLookup.SetEmpty();
                colorLookup.SetEmpty();

                MachineNolookUpEdit.EditValue = 0;

                JobNoLookUpEdit.EditValue = 0;
                //BatchNoLookUpEdit.EditValue = 0;

                gradeLookup1.SetEmpty();
                BoxItemLookup.SetEmpty();
                CopsItemLookup.SetEmpty();
                PlyItemLookup.SetEmpty();
                PalletItemLookup.SetEmpty();
                PackerLookup.SetEmpty();
                CheckerempLookup2.SetEmpty();

                TwistLookUpEdit.SelectedText = string.Empty;

                transbindingSource.DataSource = new List<BeamProdDto>();

                createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
                modifyLabelControl.Text = string.Empty;
                PlySpinEdit.Value = 0;
                CopsSpinEdit.Value = 0;
                CopsWeightSpinEdit.Value = 0;

                BoxWeightSpinEdit.Value = 0;
                TareWeightSpinEdit.Value = 0;
                NetWeightSpinEdit.Value = 0;
                RemarkTextBoxExt.Text = string.Empty;
                
                voucherLookup.SetDefault();
            }
            else
            {
                BoxNotextEdit.Text = "New-" + DbUtils.NextSerialNo(Convert.ToInt32(voucherLookup.SelectedValue), 1);
            }

            //Cops Detail
            GrossweightSpinEdit.Value = 0;


            DelProdTrans = new List<BeamProdDto>();

            PackingTypeLookUpEdit.Focus();
        }
        public override void ResetPage()
        {
            base.ResetPage();

            NewAfterSave = false;
            voucherLookup.SetDefault();
            voucherDateEdit.EditValue = DateTime.Now;

            divLookUpEdit.EditValue = 1;

            ProductLookup.SetEmpty();
            colorLookup.SetEmpty();

            MachineNolookUpEdit.EditValue = 0;

            gradeLookup1.SetEmpty();
            BoxItemLookup.SetEmpty();
            CopsItemLookup.SetEmpty();
            PlyItemLookup.SetEmpty();
            PalletItemLookup.SetEmpty();
            PackerLookup.SetEmpty();
            CheckerempLookup2.SetEmpty();

            JobNoLookUpEdit.EditValue = 0;
            //BatchNoLookUpEdit.EditValue = 0;

            transbindingSource.DataSource = new List<BeamProdDto>();

            PackingTypeLookUpEdit.SelectedText = string.Empty;
            TwistLookUpEdit.SelectedText = string.Empty;
            PlySpinEdit.Value = 0;
            CopsSpinEdit.Value = 0;
            CopsWeightSpinEdit.Value = 0;
            GrossweightSpinEdit.Value = 0;
            BoxWeightSpinEdit.Value = 0;
            TareWeightSpinEdit.Value = 0;
            NetWeightSpinEdit.Value = 0;
            RemarkTextBoxExt.Text = string.Empty;

            DelProdTrans = new List<BeamProdDto>();

            PackingTypeLookUpEdit.Focus();
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
                cfg.CreateMap<ProdModel, BeamProdDto>();
            });
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
                        _find.ProdStatus = "STOCK";
                        if (PackingTypeLookUpEdit.EditValue != null)
                            _find.Extra1 = PackingTypeLookUpEdit.EditValue.ToString();

                        if (divLookUpEdit.EditValue != null)
                            _find.DivId = Convert.ToInt32(divLookUpEdit.EditValue);

                        _find.VoucherId = Convert.ToInt32(voucherLookup.SelectedValue);

                        _find.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));

                        //if (JobNoLookUpEdit.EditValue != null)
                        //    _find.RefSCId = Convert.ToInt32(JobNoLookUpEdit.EditValue);

                        if (JobNoLookUpEdit.EditValue != null)
                            _find.BatchId = Convert.ToInt32(JobNoLookUpEdit.EditValue);

                        _find.ProductId = Convert.ToInt32(ProductLookup.SelectedValue);
                        if (colorLookup.SelectedValue != null)
                            _find.ColorId = Convert.ToInt32(colorLookup.SelectedValue);
                        if (MachineNolookUpEdit.EditValue != null)
                            _find.MacId = Convert.ToInt32(MachineNolookUpEdit.EditValue);

                        if (gradeLookup1.SelectedValue != null)
                            _find.GradeId = Convert.ToInt32(gradeLookup1.SelectedValue);
                        if (BoxItemLookup.SelectedValue != null)
                            _find.BoxProductId = Convert.ToInt32(BoxItemLookup.SelectedValue);

                        if (CopsItemLookup.SelectedValue != null)
                            _find.CopsProductId = Convert.ToInt32(CopsItemLookup.SelectedValue);

                        if (PlyItemLookup.SelectedValue != null)
                            _find.PlyProductId = Convert.ToInt32(PlyItemLookup.SelectedValue);

                        if (PalletItemLookup.SelectedValue != null)
                            _find.PalletProductId = Convert.ToInt32(PalletItemLookup.SelectedValue);
                        if (PackerLookup.SelectedValue != null)
                            _find.PackEmpId = Convert.ToInt32(PackerLookup.SelectedValue);

                        if (CheckerempLookup2.SelectedValue != null)
                            _find.CheckEmpId = Convert.ToInt32(CheckerempLookup2.SelectedValue);

                        if (TwistLookUpEdit.EditValue != null)
                            _find.TwistType = TwistLookUpEdit.EditValue.ToString();

                        if (PackTypeLookUpEdit.EditValue != null)
                            _find.PackId = Convert.ToInt32(PackTypeLookUpEdit.EditValue);

                        
                            _find.Ply = (int)PlySpinEdit.Value;

                        _find.Cops = (int)CopsSpinEdit.Value;
                        _find.CopsWt = CopsWeightSpinEdit.Value;
                        _find.GrossWt = GrossweightSpinEdit.Value;
                        _find.BoxWt = BoxWeightSpinEdit.Value;
                        _find.TareWt = TareWeightSpinEdit.Value;
                        _find.NetWt = NetWeightSpinEdit.Value;

                        _find.Remark = RemarkTextBoxExt.Text;

                        _find.CompId = KontoGlobals.CompanyId;
                        _find.YearId = KontoGlobals.YearId;
                        _find.BranchId = KontoGlobals.BranchId;
                        _find.CProductId = _find.ProductId;
                        var subGrade = db.Grades.FirstOrDefault(k => k.RefGradeId == _find.GradeId
                                       && k.StartWt <= _find.NetWt && k.EndWt >= _find.NetWt && !k.IsDeleted);

                        if (subGrade != null)
                            _find.SubGradeId = subGrade.Id;

                        if (this.PrimaryKey == 0)
                        {
                            var _srno = DbUtils.NextSerialNo((int)_find.VoucherId, db, 0);
                            _find.VoucherNo = _srno;  //DbUtils.NextSerialNo(_find.VoucherId, db);
                            _find.SrNo = 1;
                            db.Prods.Add(_find);
                            db.SaveChanges();
                        }
                        else
                        {
                            _find.CreateDate = createdate;
                            _find.CreateUser = createuser;
                        }

                        //sotock effect
                        var stk = db.StockTranses.Where(k => k.MasterRefId == _find.RowId).ToList();
                        if (stk != null)
                            db.StockTranses.RemoveRange(stk);

                        int ProductId = 0;
                        for (int i = 0; i <= 4; i++)
                        {
                            if (i == 0 && _find.ProductId == null)
                            {
                                continue;
                            }
                            else if (i == 1 && (_find.BoxProductId == null || _find.BoxProductId == 0))
                            {
                                continue;
                            }
                            else if (i == 2 && ((_find.Cops <= 0) || (_find.CopsProductId == null || _find.CopsProductId == 0)))
                            {
                                continue;
                            }
                            else if (i == 3 && ((_find.Ply <= 0) || (_find.PlyProductId == null || _find.PlyProductId == 0)))
                            {
                                continue;
                            }
                            else if (i == 4 && (_find.PalletProductId == null || _find.PalletProductId == 0))
                            {
                                continue;
                            }
                            bool IsIssue = false;
                            string TableName = "Yarn Production";
                            decimal RcptQty = (decimal)_find.NetWt;
                            decimal IssueQty = 0;
                            decimal qty = (decimal)_find.NetWt;
                            int Nos = 1;

                            if (i == 0)
                            {
                                IsIssue = false;
                                RcptQty = (decimal)_find.NetWt;
                                IssueQty = 0;
                                qty = (decimal)_find.NetWt;
                                Nos = 1;
                                ProductId = (int)_find.ProductId;

                            }
                            else if (i == 1)
                            {
                                IsIssue = true;
                                RcptQty = 0;
                                IssueQty = 1;
                                qty = 1;
                                Nos = 1;
                                ProductId = (int)_find.BoxProductId;

                            }
                            else if (i == 2)
                            {
                                IsIssue = true;
                                RcptQty = 0;
                                IssueQty = 1 * (_find.Cops != 0 ? (int)_find.Cops : 0);
                                qty = 1 * (_find.Cops != 0 ? (int)_find.Cops : 0);
                                Nos = 1 * (_find.Cops != 0 ? (int)_find.Cops : 0);
                                ProductId = (int)_find.CopsProductId;

                            }
                            else if (i == 3)
                            {
                                IsIssue = true;
                                RcptQty = 0;
                                IssueQty = 1 * (_find.Ply != 0 ? (int)_find.Ply : 0);
                                qty = 1 * (_find.Ply != 0 ? (int)_find.Ply : 0);
                                Nos = 1 * (_find.Ply != 0 ? (int)_find.Ply : 0);
                                ProductId = (int)_find.PlyProductId;

                            }
                            else if (i == 4)
                            {
                                IsIssue = true;
                                RcptQty = 0;
                                IssueQty = 1;
                                qty = 1;
                                Nos = 1;
                                ProductId = (int)_find.PalletProductId;
                            }

                            StockEffect.StockTransProdEntry(_find, IsIssue, RcptQty, IssueQty, qty, Nos, TableName, db, ProductId);
                            db.SaveChanges();
                        }
                        db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Yarn Production Save");
                        MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
                    }
                }
            }

            if (IsSaved)
            {
                // NewRec();

                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage + " Box No.: " + _find.VoucherNo, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (!this.OpenForLookup && newmode)
                {
                    int ID = this.PrimaryKey;
                    if (this.voucherLookup.GroupDto.PrintAfterSave && MessageBox.Show("Print Production ?", "Print", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        this.PrimaryKey = _find.Id;
                        Print();
                        this.PrimaryKey = 0;
                    }
                    ProdTrans = new List<BeamProdDto>();
                    ProdTrans = transbindingSource.DataSource as List<BeamProdDto>;
                    BeamProdDto model = new BeamProdDto();
                    var map = new Mapper(config);
                    map.Map(_find, model);
                    if (ID > 0)
                    {
                        BeamProdDto _data = ProdTrans.Find(k => k.VoucherNo == _find.VoucherNo);
                        ProdTrans.Remove(_data);
                    }
                    model.GradeName = gradeLookup1.SelectedText;
                    model.MachineName = MachineNolookUpEdit.SelectedText;
                    model.BoxItem = BoxItemLookup.SelectedText;

                    ProdTrans.Add(model);

                    NewAfterSave = true;

                    base.SaveDataAsync(newmode);
                    // this.ResetPage();
                    this.NewRec();
                    transbindingSource.DataSource = new List<BeamProdDto>();
                    transbindingSource.DataSource = ProdTrans;

                    CopsSpinEdit.Focus();
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
            var Prodstrans = transbindingSource.DataSource as List<BeamProdDto>;

            if (Convert.ToInt32(divLookUpEdit.EditValue) <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Division", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                divLookUpEdit.Focus();
                return false;
            }
            else if (Convert.ToInt32(JobNoLookUpEdit.EditValue) <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Jobcard / Batch No", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBoxAdv.Show(this, "Invalid Beam Product", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ProductLookup.Focus();
                return false;
            }
            else if (Convert.ToInt32(colorLookup.SelectedValue) <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Color", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                colorLookup.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(BoxNotextEdit.Text) || BoxNotextEdit.Text.Length <= 0 || string.IsNullOrEmpty(BoxNotextEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Box No", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BoxNotextEdit.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(CopsWeightSpinEdit.Text) || CopsWeightSpinEdit.Text.Length <= 0 || string.IsNullOrEmpty(CopsWeightSpinEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Cops Weight", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CopsWeightSpinEdit.Focus();
                return false;
            }
            else if (GrossweightSpinEdit.Value <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Gross Weight", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                GrossweightSpinEdit.Focus();
                return false;
            }
            else if (TareWeightSpinEdit.Value <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Tare Weight", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TareWeightSpinEdit.Focus();
                return false;
            }
            else if (NetWeightSpinEdit.Value <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Net Weight", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                NetWeightSpinEdit.Focus();
                return false;
            }
            else if (!string.IsNullOrEmpty(BoxNotextEdit.Text) && this.PrimaryKey > 0)
            {
                var db = new KontoContext();
                int vid = (int)voucherLookup.SelectedValue;
                var takaExists = db.Prods.FirstOrDefault(x => x.VoucherNo == BoxNotextEdit.Text
                          && x.CompId == KontoGlobals.CompanyId && x.YearId == KontoGlobals.YearId && x.BranchId == KontoGlobals.BranchId
                          && x.VoucherId == vid && x.IsDeleted == false && x.IsActive && x.Id != this.PrimaryKey);

                if (takaExists != null)
                {
                    MessageBoxAdv.Show(this, "Box No already exists!!", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    BoxNotextEdit.Focus();
                    return false;
                }
            }
            //else if (ProdsEmp.Count > 0)
            //{
            //    var prodMtr = ProdsEmp.Sum(k => k.TotalMtrs);
            //    var nMtr = PlySpinEdit.Value;
            //    if (prodMtr != nMtr)
            //    {
            //        MessageBoxAdv.Show(this, "Employee meter should be matched with taka meter!", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        PackingCopsSpinEdit.Focus();
            //        return false;
            //    }
            //}

            return true;
        }
        private void LoadData(ProdModel pdata)
        {
            KontoContext _db = new KontoContext();
            BoxNotextEdit.Text = pdata.VoucherNo;
            PackingTypeLookUpEdit.EditValue = pdata.Extra1;
            divLookUpEdit.EditValue = pdata.DivId;
            voucherLookup.SelectedValue = pdata.VoucherId;
            voucherDateEdit.EditValue = KontoUtils.IToD(pdata.VoucherDate);

            ProductLookup.SelectedValue = pdata.ProductId;
            ProductLookup.SetGroup((int)pdata.ProductId);

            if (pdata.ColorId != null)
            {
                colorLookup.SelectedValue = pdata.ColorId;
                colorLookup.SetGroup();
            }
            MachineNolookUpEdit.EditValue = pdata.MacId;

            if (pdata.GradeId != null)
            {
                gradeLookup1.SelectedValue = pdata.GradeId;
                gradeLookup1.SetValue();
            }
            if (pdata.BoxProductId != null)
            {
                BoxItemLookup.SelectedValue = pdata.BoxProductId;
                BoxItemLookup.SetGroup((int)pdata.BoxProductId);
            }
            if (pdata.CopsProductId != null)
            {
                CopsItemLookup.SelectedValue = pdata.CopsProductId;
                CopsItemLookup.SetGroup((int)pdata.CopsProductId);
            }
            if (pdata.PlyProductId != null)
            {
                PlyItemLookup.SelectedValue = pdata.PlyProductId;
                PlyItemLookup.SetGroup((int)pdata.PlyProductId);
            }
            if (pdata.PalletProductId != null)
            {
                PalletItemLookup.SelectedValue = pdata.PalletProductId;
                PalletItemLookup.SetGroup((int)pdata.PalletProductId);
            }

            if (pdata.PackEmpId != null)
            {
                PackerLookup.SelectedValue = pdata.PackEmpId;
                PackerLookup.SetGroup();
            }
            if (pdata.CheckEmpId != null)
            {
                CheckerempLookup2.SelectedValue = pdata.CheckEmpId;
                CheckerempLookup2.SetGroup();
            }
            TwistLookUpEdit.EditValue = pdata.TwistType;

            if (pdata.PackId != null)
                PackTypeLookUpEdit.EditValue = pdata.PackId;

           
                PlySpinEdit.Value = pdata.Ply;

            CopsSpinEdit.Value = pdata.Cops;
            CopsWeightSpinEdit.Value = pdata.CopsWt;
            GrossweightSpinEdit.Value = pdata.GrossWt;
            BoxWeightSpinEdit.Value = pdata.BoxWt;
            TareWeightSpinEdit.Value = pdata.TareWt;
            NetWeightSpinEdit.Value = pdata.NetWt;
            RemarkTextBoxExt.Text = pdata.Remark;

            //if (pdata.RefSCId != null)
            //    JobNoLookUpEdit.EditValue = pdata.RefSCId;
            if (pdata.BatchId != null)
                JobNoLookUpEdit.EditValue = pdata.BatchId;

            PackingTypeLookUpEdit.Focus();
            DelProdTrans = new List<BeamProdDto>();
        }
        #endregion
    }
}