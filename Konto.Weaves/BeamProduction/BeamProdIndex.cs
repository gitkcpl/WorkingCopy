using AutoMapper;
using Konto.App.Shared;
using Konto.App.Shared.Para;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Transaction;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Weaves.BeamProduction
{
    public partial class BeamProdIndex : KontoMetroForm
    {
        private List<ProdModel> FilterView = new List<ProdModel>();
        public BeamProdIndex()
        {
            InitializeComponent();

            FillLookup();
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;
            SetParameter();

            this.Load += BeamProdIndex_Load;

            MtrspinEdit.ValueChanged += MtrspinEdit_ValueChanged;
            DenierspinEdit.ValueChanged += DenierspinEdit_ValueChanged;
            NoOfTakaSpinEdit.ValueChanged += NoOfTakaSpinEdit_ValueChanged;
            LengthSpinEdit.ValueChanged += LengthSpinEdit_ValueChanged;
            EndsSpinEdit.ValueChanged += EndsSpinEdit_ValueChanged;

            this.MainLayoutFile = KontoFileLayout.BeamProd_Index;
            this.GridLayoutFile = KontoFileLayout.BeamProd_Trans;
            voucherLookup11.SelectedValueChanged += VoucherLookup11_SelectedValueChanged;

            this.FirstActiveControl = voucherLookup11;
        }

        private void VoucherLookup11_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.PrimaryKey == 0 && Convert.ToInt32(voucherLookup11.SelectedValue) > 0)
            {
                BeamNotextEdit.Text = "New-" + DbUtils.NextSerialNo(Convert.ToInt32(voucherLookup11.SelectedValue), 1);
            }
        }

        private void BeamProdIndex_Load(object sender, EventArgs e)
        {
            MtrspinEdit.Value = 0;
            DenierspinEdit.Value = 0;
            WastagespinEdit.Value = 0;
            NoOfTakaSpinEdit.Value = 0;
            LengthSpinEdit.Value = 0;
            NetWeightSpinEdit.Value = 0;
            EndsSpinEdit.Value = 0;
            WidthSpinEdit.Value = 0;
            PickSpinEdit.Value = 0;
        }

        private void MtrspinEdit_ValueChanged(object sender, EventArgs e)
        {
            LengthSpinEdit.Value = NoOfTakaSpinEdit.Value * MtrspinEdit.Value;
        }
        private void DenierspinEdit_ValueChanged(object sender, EventArgs e)
        {
            NetWeightSpinEdit.Value = decimal.Round(((DenierspinEdit.Value * EndsSpinEdit.Value * LengthSpinEdit.Value) / 9000000) , MidpointRounding.AwayFromZero);
        }
        private void NoOfTakaSpinEdit_ValueChanged(object sender, EventArgs e)
        {
            // if (takaTxt.Value != null && txtMtrsPerTaka.Value != null)
            LengthSpinEdit.Value = NoOfTakaSpinEdit.Value * MtrspinEdit.Value;
        }
        private void EndsSpinEdit_ValueChanged(object sender, EventArgs e)
        {
            // if (txtDenier.Value != null && lengthTxt.Value != null && endsTxt.Value != null)
            NetWeightSpinEdit.Value = decimal.Round( ((DenierspinEdit.Value * EndsSpinEdit.Value * LengthSpinEdit.Value) / 9000000),MidpointRounding.AwayFromZero);

        }
        private void LengthSpinEdit_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (MtrspinEdit.Value != 0)
                    NoOfTakaSpinEdit.Value = LengthSpinEdit.Value / MtrspinEdit.Value;

                // if (txtDenier.Value != null && lengthTxt.Value != null && endsTxt.Value != null)
                NetWeightSpinEdit.Value = decimal.Round( (((DenierspinEdit.Value * EndsSpinEdit.Value * LengthSpinEdit.Value) / 9000000)), MidpointRounding.AwayFromZero);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SetParameter()
        {
            using (var db = new KontoContext())
            {
                var _paralists = db.CompParas.Include("SysPara")
                              .Where(x => x.SysPara.Category == "BeamProd" && x.CompId == KontoGlobals.CompanyId)
                             .ToList();

                foreach (var item in _paralists)
                {
                    var value = item.ParaValue;
                    switch (item.ParaId)
                    {
                        case 206:
                            {
                                ProdPara.AutoYarnConsumption = (value == "Y") ? true : false;
                                break;
                            }
                    }
                }
            }

        }
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

                divLookUpEdit.Properties.DataSource = _divLists;
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
                var _ListView = new BeamProdList();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Beam Production [View]";
            }
            else if (tabControlAdv1.SelectedIndex == 3)
            {
                if (tabPageAdv4.Controls.Count > 0) return;
                var _frm = Activator.CreateInstance("Konto.Reporting", "Konto.Reporting.Para.BeamProdPara.BPParaMainView").Unwrap() as KontoForm;

                _frm.TopLevel = false;
                _frm.Parent = tabPageAdv4;
                _frm.ReportFilterType = "BeamProd";
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
                Log.Error(ex, "Beam Production Invoice Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
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
            this.FilterView = new List<ProdModel>();
            this.Text = "Beam Production [Add New]";

            divLookUpEdit.EditValue = 1;
            this.ActiveControl = voucherLookup11.buttonEdit1;
            
            voucherDateEdit.EditValue = DateTime.Now;

            WarperLookup1.SelectedValue = 1;
            WarperLookup1.SetGroup();
            DrawerLookup.SelectedValue = 1;
            DrawerLookup.SetGroup();
            DrawerDateDateEdit.EditValue = DateTime.Now;
            WDateDateEdit1.EditValue = DateTime.Now;

            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
            voucherLookup11.SetDefault();
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

            WarperLookup1.SelectedValue = 1;
            WarperLookup1.SetGroup();
            DrawerLookup.SelectedValue = 1;
            DrawerLookup.SetGroup();
            DrawerDateDateEdit.EditValue = DateTime.Now;
            WDateDateEdit1.EditValue = DateTime.Now;

          //  BeamNotextEdit.Text = "New";
            MtrspinEdit.Value = 0;
            DenierspinEdit.Value = 0;
            WastagespinEdit.Value = 0;
            NoOfTakaSpinEdit.Value = 0;
            LengthSpinEdit.Value = 0;
            EndsSpinEdit.Value = 0;
            WidthSpinEdit.Value = 0;
            PickSpinEdit.Value = 0;
            NetWeightSpinEdit.Value = 0;
            wRatespinEdit.Value = 0;
            DrawerRatespinEdit.Value = 0;

            IsClosecheckEdit.Checked = false;

            RemarkTextBoxExt.Text = string.Empty;

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

            bool IsSaved = false;
            if (!ValidateData()) return;

            var _find = new ProdModel();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProdModel, ProdModel>();
            });

            using (var db = new KontoContext())
            {
                using (var _tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (this.PrimaryKey != 0)
                        {
                            _find = db.Prods.Find(this.PrimaryKey);
                        }
                        if (divLookUpEdit.EditValue != null)
                            _find.DivId = Convert.ToInt32(divLookUpEdit.EditValue);

                        _find.VoucherId = Convert.ToInt32(voucherLookup11.SelectedValue);

                        _find.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
                        _find.ProductId = Convert.ToInt32(BeamLookup.SelectedValue);
                        _find.CopsProductId = Convert.ToInt32(YarnproductLookup.SelectedValue);
                        _find.BoxProductId = Convert.ToInt32(GreyproductLookup.SelectedValue);

                        _find.CheckEmpId = Convert.ToInt32(WarperLookup1.SelectedValue);
                        _find.PackEmpId = Convert.ToInt32(DrawerLookup.SelectedValue);
                        if (DrawerDateDateEdit.EditValue != null)
                            _find.DrawingDate = Convert.ToDateTime(DrawerDateDateEdit.EditValue);

                        if (CloseDateDateEdit.EditValue != null)
                            _find.CloseDate = Convert.ToDateTime(CloseDateDateEdit.EditValue);

                        if (WDateDateEdit1.EditValue != null)
                            _find.WarpingDate = Convert.ToDateTime(WDateDateEdit1.EditValue);


                        _find.CopsWt = MtrspinEdit.Value;

                        _find.Pallet = Convert.ToInt32(DenierspinEdit.Value);
                        _find.CartnWt = WastagespinEdit.Value;
                        _find.Cops = Convert.ToInt32(NoOfTakaSpinEdit.Value);
                        _find.Ply = Convert.ToInt32(LengthSpinEdit.Value);
                        _find.Tops = Convert.ToInt32(EndsSpinEdit.Value);
                        _find.GrossWt = WidthSpinEdit.Value;
                        _find.TareWt = PickSpinEdit.Value;
                        _find.NetWt = NetWeightSpinEdit.Value;
                        _find.BoxRate = wRatespinEdit.Value;

                        _find.CopsRate = DrawerRatespinEdit.Value;
                        _find.IsClose = IsClosecheckEdit.Checked;
                        _find.Remark = RemarkTextBoxExt.Text;
                        _find.CompId = KontoGlobals.CompanyId;
                        _find.BranchId = KontoGlobals.BranchId;

                        _find.CompId = KontoGlobals.CompanyId;
                        _find.YearId = KontoGlobals.YearId;
                        _find.BranchId = KontoGlobals.BranchId;
                        _find.CProductId = _find.ProductId;
                        _find.IsOk = true;
                        // _find.VoucherNo = BeamNotextEdit.Text;
                        if (this.PrimaryKey==0 && !voucherLookup11.GroupDto.ManualSeries)
                        {
                            var _srno = DbUtils.NextSerialNo((int)_find.VoucherId, db, 0);
                            _find.VoucherNo = _srno;
                            _find.SrNo = 1;
                        }
                        else
                        {
                            _find.VoucherNo = BeamNotextEdit.Text.Trim();
                        }
                        if (_find.Id <= 0)
                        {
                            _find.ProdStatus = "STOCK";

                            db.Prods.Add(_find);
                        }
                        db.SaveChanges();

                        //Stock Trans
                        var st = db.StockTranses.Where(k => k.RefId == _find.RowId).ToList();
                        db.StockTranses.RemoveRange(st);

                        bool IsIssue = false;
                        string TableName = "Beam Production";
                        decimal RcptQty = 0;
                        decimal IssueQty = 0;
                        decimal qty = 0;
                        int pcs = 1;
                        if (_find.NetWt == 0)
                        {
                            RcptQty = _find.Ply != 0 ? (decimal)_find.Ply : 0;
                            IssueQty = 0;
                            qty = _find.Ply != 0 ? (decimal)_find.Ply : 0;
                            pcs = 1;

                        }
                        else
                        {
                            RcptQty = _find.Ply != 0 ? (decimal)_find.Ply : 0;
                            IssueQty = 0;
                            qty = _find.Ply != 0 ? (decimal)_find.Ply : 0;
                            pcs = 1;
                        }
                        StockEffect.StockTransProdEntry(_find, IsIssue, RcptQty, IssueQty, qty, pcs, TableName, db);
                        db.SaveChanges();
                        //if (ProdPara.AutoYarnConsumption)
                        //{
                        //    IsIssue = true;
                        //    TableName = "Beam Production";

                        //    var yarnlist = db.WeftItems.Where(k => k.IsActive && !k.IsDeleted && k.RefId == _find.ProductId).ToList();
                        //    var map = new Mapper(config);
                        //    ProdModel model;
                        //    foreach (var item in yarnlist)
                        //    {
                        //        RcptQty = 0;
                        //        if (item.TypeId == 2)
                        //        {
                        //            if (_find.Ply != 0 && item.Qty != null && item.Mtr != null)
                        //            {
                        //                IssueQty = ((decimal)_find.Ply * (decimal)item.Qty) / (decimal)item.Mtr;
                        //                qty = ((decimal)_find.Ply * (decimal)item.Qty) / (decimal)item.Mtr;
                        //            }
                        //        }
                        //        pcs = 1;

                        //        model = new ProdModel();
                        //        map.Map(_find, model);
                        //        model.ProductId = (int)item.ProductId;
                        //        StockEffect.StockTransProdEntry(model, IsIssue, RcptQty, IssueQty, qty, pcs, TableName, db);
                        //        db.SaveChanges();
                        //    }
                        //}

                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Beam Production" + " Save");
                        MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());

                    }
                }
            }

            if (IsSaved)
            {
                // NewRec();

                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage + " Beam No.: " + _find.VoucherNo, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (!this.OpenForLookup && newmode)
                {
                    if (this.voucherLookup11.GroupDto.PrintAfterSave && MessageBox.Show("Print Bill ?", "Print", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
            var closeDate = 0;

            if (CloseDateDateEdit.DateTime != null)
                closeDate = Convert.ToInt32(CloseDateDateEdit.DateTime.ToString("yyyyMMdd"));

            if (Convert.ToInt32(divLookUpEdit.EditValue) <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Division", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                divLookUpEdit.Focus();
                return false;
            }
            else if (Convert.ToInt32(voucherLookup11.SelectedValue) <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Voucher", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherLookup11.Focus();
                return false;
            }
            else if (dt > KontoGlobals.ToDate || dt < KontoGlobals.FromDate)
            {
                MessageBoxAdv.Show(this, "Date out of financial range", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherDateEdit.Focus();
                return false;
            }
            else if (Convert.ToInt32(BeamLookup.SelectedValue) <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Beam Product", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BeamLookup.Focus();
                return false;
            }
            else if (Convert.ToInt32(YarnproductLookup.SelectedValue) <= 0)
            {
                MessageBoxAdv.Show(this, "Invalid Yarn Product", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                YarnproductLookup.Focus();
                return false;
            }
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
            else if (closeDate > dt)
            {
                MessageBoxAdv.Show(this, "Close Date Can Not be grater than order date", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CloseDateDateEdit.Focus();
                return false;
            }

            return true;
        }

        private void LoadData(ProdModel pdata)
        {
            divLookUpEdit.EditValue = pdata.DivId;

            voucherLookup11.SelectedValue = pdata.VoucherId;
            //  voucherDateEdit.EditValue = pdata.VoucherDate;
            voucherDateEdit.EditValue = KontoUtils.IToD(pdata.VoucherDate);
            BeamLookup.SelectedValue = pdata.ProductId;
            BeamLookup.SetGroup((int)pdata.ProductId);
            YarnproductLookup.SelectedValue = pdata.CopsProductId;
            YarnproductLookup.SetGroup((int)pdata.CopsProductId);
            GreyproductLookup.SelectedValue = pdata.BoxProductId;
            GreyproductLookup.SetGroup((int)pdata.BoxProductId);

            WarperLookup1.SelectedValue = pdata.CheckEmpId;
            DrawerLookup.SelectedValue = pdata.PackEmpId;
            DrawerDateDateEdit.EditValue = pdata.DrawingDate;
            CloseDateDateEdit.EditValue = pdata.CloseDate;
            WDateDateEdit1.EditValue = pdata.WarpingDate;

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
            wRatespinEdit.Value = pdata.BoxRate;
            DrawerRatespinEdit.Value = pdata.CopsRate;

            IsClosecheckEdit.Checked = pdata.IsClose;

            RemarkTextBoxExt.Text = pdata.Remark;

            divLookUpEdit.Focus();
        }

        #endregion
    }
}