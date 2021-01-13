using AutoMapper;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Admin;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using Konto.Data.Models.Transaction.TradingDto;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Trading.GreyOrder
{
    public partial class GreyOrderIndex : KontoMetroForm
    {
        private List<OrdDto> FilterView = new List<OrdDto>();
        private string GreyPurchaseAgainstGoDtoFile = KontoFileLayout.Grey_Order_Purchase_Layout;
        private string GreyIssueAgainstGoDtoFile = KontoFileLayout.Grey_Order_Issue_Layout;
        private string GreyRcptAgainstGoDtoFile = KontoFileLayout.Grey_Order_Mill_Receipt_Layout;
        public GreyOrderIndex()
        {
            InitializeComponent();

            tabControlAdv1.TabPages[2].TabVisible = false;
           // tabControlAdv1.TabPages[3].TabVisible = false;

            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;
            okSimpleButton.Click += OkSimpleButton_Click;
           
            purchaseGridControl1.DoubleClick += PurchaseGridControl1_DoubleClick;
            issueGridControl1.DoubleClick += IssueGridControl1_DoubleClick;
            rcptGridControl1.DoubleClick += RcptGridControl1_DoubleClick;
            daysSpinEdit.EditValueChanged += DaysSpinEdit_EditValueChanged;
            requireDateEdit.EditValueChanged += RequireDateEdit_EditValueChanged;
            this.MainLayoutFile = KontoFileLayout.GreyOrder_Index;
            this.GridLayoutFile = KontoFileLayout.GreyOrder_Trans;
            FillLookup();
            voucherLookup1.SelectedValueChanged += VoucherLookup1_SelectedValueChanged;
        }

        private void VoucherLookup1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.PrimaryKey == 0 && Convert.ToInt32(voucherLookup1.SelectedValue) > 0)
            {
                voucherNoTextEdit.Text = "New-" + DbUtils.NextSerialNo(Convert.ToInt32(voucherLookup1.SelectedValue), 1);
            }
        }

        private void RequireDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (daysSpinEdit.Value == 0) return;
            requireDateEdit.DateTime = voucherDateEdit.DateTime.AddDays(Convert.ToDouble(daysSpinEdit.Value));
        }

        private void DaysSpinEdit_EditValueChanged(object sender, EventArgs e)
        {
            requireDateEdit.DateTime = voucherDateEdit.DateTime.AddDays(Convert.ToDouble(daysSpinEdit.Value));
        }

        private void RcptGridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (rcptGridView1.FocusedRowHandle < 0) return;
            var id = Convert.ToInt32(rcptGridView1.GetRowCellValue(rcptGridView1.FocusedRowHandle, "Id"));
            var frm = new MillReceipt.MrvIndex();
            frm.Tag = MenuId.Mill_Receipt;
            frm.ViewOnlyMode = true;
            frm.EditKey = id;

            frm.ShowDialog();
        }

        private void IssueGridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (issueGridView1.FocusedRowHandle < 0) return;
            var id = Convert.ToInt32(issueGridView1.GetRowCellValue(issueGridView1.FocusedRowHandle, "Id"));
            var frm = new MillIssue.MillIssueIndex();
            frm.Tag = MenuId.Mill_Issue;
            frm.ViewOnlyMode = true;
            frm.EditKey = id;

            frm.ShowDialog();
        }

        private void PurchaseGridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (purchaseGridView1.FocusedRowHandle < 0) return;
            var id = Convert.ToInt32(purchaseGridView1.GetRowCellValue(purchaseGridView1.FocusedRowHandle, "Id"));
            var frm = new GP.GPIndex();
            frm.Tag = MenuId.Grey_Purchase;
            frm.ViewOnlyMode = true;
            frm.EditKey = id;

            frm.ShowDialog();
        }

        #region UDF
        private void FillLookup()
        {
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("APPROVED", "APPROVED"),
                new ComboBoxPairs("PENDING", "PENDING"),
                new ComboBoxPairs("CLOSED", "CLOSED"),
                new ComboBoxPairs("CANCELLED", "CANCELLED")
            };

            statusLookUpEdit.Properties.ValueMember = "_Key";
            statusLookUpEdit.Properties.DisplayMember = "_Value";
            statusLookUpEdit.Properties.DataSource = cbp;

        }
        private bool ValidateData()
        {
            var dt = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
            var reqdt = Convert.ToInt32(requireDateEdit.DateTime.ToString("yyyyMMdd"));
            if (Convert.ToInt32(voucherLookup1.SelectedValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Voucher", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherLookup1.Focus();
                return false;
            }
            else if (Convert.ToInt32(pgLookup1.SelectedValue) == 0 && Convert.ToInt32(accLookup1.SelectedValue)==0)
            {
                MessageBoxAdv.Show(this, "Invalid Weaver or Group", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pgLookup1.Focus();
                return false;
            }
            else if (Convert.ToInt32(productLookup1.SelectedValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Grey Quality", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                productLookup1.Focus();
                return false;
            }
            else if (lotSpinEdit.Value == 0)
            {
                MessageBoxAdv.Show(this, "Invalid No of Lot", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lotSpinEdit.Focus();
                return false;
            }
            else if (totalMeterSpinEdit.Value == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Total Meters", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                totalMeterSpinEdit.Focus();
                return false;
            }
            else if (rateSpinEdit.Value == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Grey Rate", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rateSpinEdit.Focus();
                return false;
            }
            else if (dt > KontoGlobals.ToDate || dt < KontoGlobals.FromDate)
            {
                MessageBoxAdv.Show(this, "Order date out of financial range", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                voucherDateEdit.Focus();
                return false;
            }
            else if (reqdt < dt)
            {
                MessageBoxAdv.Show(this, "Require Date Cant Not be Less than order date", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                requireDateEdit.Focus();
                return false;
            }

            if (this.PrimaryKey != 0)
            {
                using (var db = new KontoContext())
                {
                    var vid = Convert.ToInt32(this.voucherLookup1.SelectedValue);
                    var exist = db.ChallanTranses.Any(x => x.MiscId == this.PrimaryKey && x.RefVoucherId == vid && x.IsDeleted == false && x.IsActive == true);
                    if (exist)
                    {
                        MessageBoxAdv.Show("Order Exist In Challan.. Can not Edit Order", "Access Denied !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
            }
            return true;
        }

        private void LoadData(OrdDto model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            voucherLookup1.SelectedValue = model.VoucherId;
            voucherLookup1.SetGroup(model.VoucherId);
            voucherNoTextEdit.Text = model.VoucherNo;
            voucherDateEdit.EditValue = KontoUtils.IToD(model.VoucherDate);
            pgLookup1.SelectedValue = model.PGroupId;
            pgLookup1.SetGroup();
            accLookup1.SelectedValue = model.AccId;
            accLookup1.SetAcc((int)model.AccId);
            refNoTextEdit.Text = model.RefNo;
            requireDateEdit.EditValue = model.RequireDate;
            daysSpinEdit.Value = Convert.ToDecimal(model.Extra1);
            if (model.AgentId != 0)
            {
                agentLookup.SelectedValue = model.AgentId;
                agentLookup.SetAcc(model.AgentId);
            }
            
            remarkTextEdit.Text = model.Remarks;

            createdLabelControl.Text = "Created By: " + model.CreateUser + " [ " + model.CreateDate + " ]";
            modifyLabelControl.Text = "Modified By: " + model.ModifyUser + " [ " + model.ModifyDate ?? string.Empty + " ]";

            using (var _context = new KontoContext())
            {
                var _list = (from ot in _context.OrdTranses
                             join pd in _context.Products on ot.ProductId equals pd.Id into join_pd
                             from pd in join_pd.DefaultIfEmpty()
                             join cl in _context.ColorModels on ot.ColorId equals cl.Id into join_cl
                             from cl in join_cl.DefaultIfEmpty()
                             join grd in _context.Grades on ot.GradeId equals grd.Id into join_grd
                             from grd in join_grd.DefaultIfEmpty()
                             join des in _context.Products on ot.DesignId equals des.Id into join_des
                             from des in join_des.DefaultIfEmpty()
                             orderby ot.Id
                             where ot.IsActive == true && ot.IsDeleted == false &&
                             ot.OrdId == model.Id
                             select new OrdTransDto()
                             {
                                 AvgWt = ot.AvgWt,
                                 Id = ot.Id,
                                 CancelReason = ot.CancelReason,
                                 Cess = ot.Cess,
                                 CessAmt = ot.CessAmt,
                                 Cgst = ot.Cgst,
                                 CgstAmt = ot.CgstAmt,
                                 ColorId = ot.ColorId,
                                 ColorName = cl.ColorName,
                                 CommDescr = ot.CommDescr,
                                 Cut = ot.Cut,
                                 DeptId = ot.DeptId,
                                 DesignId = ot.DesignId,
                                 DesignNo = des.ProductCode,
                                 Disc = ot.Disc,
                                 DiscAmt = ot.DiscAmt,
                                 DivisionId = ot.DivisionId,
                                 GradeId = ot.GradeId,
                                 GradeName = grd.GradeName,
                                 Igst = ot.Igst,
                                 IgstAmt = ot.IgstAmt,
                                 LotPcs = ot.LotPcs,
                                 NetTotal = ot.NetTotal,
                                 NoOfLot = ot.NoOfLot,
                                 OrdId = ot.OrdId,
                                 OrdStatus = ot.OrdStatus,
                                 Priority = ot.Priority,
                                 ProductId = ot.ProductId,
                                 ProductName = pd.ProductName,
                                 Qty = ot.Qty,
                                 Rate = ot.Rate,
                                 RefId = ot.RefId,
                                 RefVoucherId = ot.RefVoucherId,
                                 Remark = ot.Remark,
                                 Sgst = ot.Sgst,
                                 SgstAmt = ot.SgstAmt,
                                 Total = ot.Total,
                                 UomId = ot.UomId,
                                 WarpItemId = ot.WarpItemId

                             }).SingleOrDefault();

                if (_list != null)
                {
                    productLookup1.SelectedValue = _list.ProductId;
                    productLookup1.SetGroup(_list.ProductId);
                    lotSpinEdit.Value = _list.NoOfLot;
                    wtSpinEdit.Value = _list.AvgWt;
                    takaLotSpinEdit.Value = _list.LotPcs;
                    metersSpinEdit.Value = _list.Cut;
                    rateSpinEdit.Value = _list.Rate;
                    discDpinEdit.Value = _list.Disc;
                    statusLookUpEdit.EditValue = _list.OrdStatus;

                }

                //get purchase list against order
                var purchaseList = (from c in _context.Challans
                                    join ct in _context.ChallanTranses on c.Id equals ct.ChallanId into join_c
                                    from ct in join_c.DefaultIfEmpty()
                                    join ac in _context.Accs on c.AccId equals ac.Id
                                    where ct.MiscId == model.Id && ct.RefVoucherId == model.VoucherId
                                    && !c.IsDeleted && c.IsActive == true && !ct.IsDeleted
                                    select new GreyPurchaseAgainstGoDto()
                                    {
                                        ChlnDate = c.VoucherDate,
                                        ChallanNo = c.BillNo,
                                        VoucherNo = c.VoucherNo,
                                        Party = ac.AccName,
                                        LotNo = ct.LotNo,
                                        Pcs = ct.Pcs,
                                        Mtrs = ct.Qty,
                                        Rate = ct.Rate,
                                        Id = c.Id,
                                    }).ToList();

                purchaseGridControl1.DataSource = purchaseList;
                KontoUtils.RestoreLayoutGrid(this.GreyPurchaseAgainstGoDtoFile, purchaseGridView1);

                // grey issue against order
                var issueList = (from c in _context.Challans
                                 join ct in _context.ChallanTranses on c.Id equals ct.ChallanId
                                 join pt in _context.ChallanTranses on new { p1=ct.MiscId,p2= (int) ct.RefId } equals new {p1= pt.ChallanId,p2= pt.Id }
                                 join p in _context.Challans on pt.ChallanId equals p.Id
                                 join ac in _context.Accs on c.AccId equals ac.Id
                                 where pt.MiscId == model.Id && pt.RefVoucherId == model.VoucherId
                                   && !c.IsDeleted && c.IsActive == true && !ct.IsDeleted
                                   && !p.IsDeleted && p.IsActive == true && !pt.IsDeleted
                                 select new GreyPurchaseAgainstGoDto()
                                    {
                                        ChlnDate = c.VoucherDate,
                                        ChallanNo = c.BillNo,
                                        VoucherNo = c.VoucherNo,
                                        Party = ac.AccName,
                                        LotNo = ct.LotNo,
                                        Pcs = ct.Pcs,
                                        Mtrs = ct.Qty,
                                        Rate = ct.Rate,
                                        Id = c.Id,
                                    }).ToList();

                issueGridControl1.DataSource = issueList;
                KontoUtils.RestoreLayoutGrid(this.GreyIssueAgainstGoDtoFile, issueGridView1);

                // grey receipt list

                // grey issue against order
                var rcptList = ( 
                                 from oc in _context.Challans
                                 join  ot in _context.ChallanTranses on oc.Id equals ot.ChallanId
                                 join pt in _context.ChallanTranses on new {p1=ot.Id,p2=ot.ChallanId} equals new {p1= (int)pt.RefId,p2=pt.MiscId}
                                 join rt in _context.ChallanTranses on new {p1=pt.Id,p2=pt.ChallanId} equals new {p1=(int)rt.RefId,p2=rt.MiscId}
                                 join rc in _context.Challans on rt.ChallanId equals rc.Id
                                 join ac in _context.Accs on  rc.AccId equals ac.Id
                                 where ot.MiscId == model.Id && ot.RefVoucherId == model.VoucherId
                                   && !oc.IsDeleted && oc.IsActive == true && !ot.IsDeleted && !pt.IsDeleted
                                   && !rt.IsDeleted && !rc.IsDeleted && !ac.IsDeleted
                                 select new MillReceiptAgainstOrder()
                                 {
                                     ChlnDate = rc.VoucherDate,
                                     ChallanNo = rc.BillNo,
                                     VoucherNo = rc.VoucherNo,
                                     Party = ac.AccName,
                                     LotNo =rt.LotNo,
                                     GreyPcs = (int) rt.IssuePcs,
                                     GreyMtrs = rt.IssueQty,
                                     FinPcs = rt.Pcs,
                                     FinMtrs = rt.Qty,
                                     Rate = rt.Rate,
                                     Id = rc.Id,
                                 }).ToList();

                rcptGridControl1.DataSource = rcptList;
                KontoUtils.RestoreLayoutGrid(this.GreyRcptAgainstGoDtoFile, rcptGridView1);
            }

            this.Text = "Greys Order [View/Modify]";

            if (RBAC.UserRight == null || RBAC.UserRight.IsSysAdmin)
            {
                voucherLookup1.Enabled = true;
                voucherNoTextEdit.Enabled = true;
            }
            else if (this.PrimaryKey != 0)
            {
                voucherLookup1.Enabled = false;
                voucherNoTextEdit.Enabled = false;
            }
            this.ActiveControl = refNoTextEdit;
        }
        #endregion

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.F2 | Keys.Shift))
            {
                var frm = new GridPropertView();
                if (this.purchaseGridView1.IsFocusedView)
                {
                    frm.gridControl1.DataSource = this.purchaseGridControl1.DataSource;
                    frm.gridView1.Assign(this.purchaseGridView1, false);
                    if (frm.ShowDialog() != DialogResult.OK) return true;
                    this.purchaseGridView1.Assign(frm.gridView1, false);
                    KontoUtils.SaveLayoutGrid(this.GreyPurchaseAgainstGoDtoFile, this.purchaseGridView1);
                }
                else if (this.issueGridView1.IsFocusedView)
                {
                    frm.gridControl1.DataSource = this.issueGridView1.DataSource;
                    frm.gridView1.Assign(this.issueGridView1, false);
                    if (frm.ShowDialog() != DialogResult.OK) return true;
                    this.issueGridView1.Assign(frm.gridView1, false);
                    KontoUtils.SaveLayoutGrid(this.GreyIssueAgainstGoDtoFile, this.issueGridView1);
                }
                else if (this.rcptGridView1.IsFocusedView)
                {
                    frm.gridControl1.DataSource = this.rcptGridView1.DataSource;
                    frm.gridView1.Assign(this.rcptGridView1, false);
                    if (frm.ShowDialog() != DialogResult.OK) return true;
                    this.rcptGridView1.Assign(frm.gridView1, false);
                    KontoUtils.SaveLayoutGrid(this.GreyRcptAgainstGoDtoFile, this.rcptGridView1);
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                voucherLookup1.Focus();
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as GreyOrderListView;
                _list.ActiveControl = _list.KontoGrid;
                this.Text = "Grey Order [View]";
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new GreyOrderListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Grey Order [View]";

            }
            if (tabControlAdv1.SelectedIndex == 3)
            {
                if (tabPageAdv4.Controls.Count > 0) return;
                var _frm = Activator.CreateInstance("Konto.Reporting", "Konto.Reporting.Para.OrdPara.OrdParaMainView").Unwrap() as KontoForm;
                _frm.ReportFilterType = "GreyOrder";
                _frm.TopLevel = false;
                _frm.Parent = tabPageAdv4;
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

                Log.Error(ex, "Grey Order Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

       
        #region Parent function
        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<OrdDto>();
            this.Text = "Grey Order [Add New]";
            statusLookUpEdit.EditValue = "APPROVED";
            voucherNoTextEdit.Text = "New";
            voucherDateEdit.EditValue = DateTime.Now;
            requireDateEdit.EditValue = DateTime.Now;
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
            this.ActiveControl = voucherLookup1.buttonEdit1;
            voucherLookup1.SetDefault();
            takaLotSpinEdit.Value = 12;
            voucherLookup1.Enabled = true;
            voucherNoTextEdit.Enabled = false;
            purchaseGridControl1.DataSource = null;
            issueGridControl1.DataSource = null;
            rcptGridControl1.DataSource = null;

        }
        public override void ResetPage()
        {
            base.ResetPage();

            pgLookup1.SetEmpty();
            refNoTextEdit.Text = string.Empty;
            voucherDateEdit.DateTime = DateTime.Now;
            requireDateEdit.DateTime = DateTime.Now;
            voucherNoTextEdit.Text = string.Empty;
            agentLookup.SetEmpty();
            remarkTextEdit.Text = string.Empty;
            productLookup1.SetEmpty();
            wtSpinEdit.Value = 0;
            lotSpinEdit.Value = 0;
            takaLotSpinEdit.Value = 0;
            metersSpinEdit.Value = 0;
            rateSpinEdit.Value = 0;
            discDpinEdit.Value = 0;
            daysSpinEdit.Value = 0;
            
        }

        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrdModel, OrdDto>();
            });

            using (var db = new KontoContext())
            {
                var bill = db.Ords.Find(_key);
                var model = new OrdDto();
                var mapper = new Mapper(config);
                mapper.Map(bill, model);
                LoadData(model);
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

            if (Convert.ToInt32(pgLookup1.SelectedValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "PGroupId", Operation = Op.Equals, Value = Convert.ToInt32(pgLookup1.SelectedValue) });
            }

            filter.Add(new Filter { PropertyName = "CompId", Operation = Op.Equals, Value = KontoGlobals.CompanyId });
            filter.Add(new Filter { PropertyName = "YearId", Operation = Op.Equals, Value = KontoGlobals.YearId });
            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });
            filter.Add(new Filter { PropertyName = "TypeId", Operation = Op.Equals, Value = (int)VoucherTypeEnum.GreyOrder });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrdModel, OrdDto>();
            });

            using (var db = new KontoContext())
            {
                FilterView = db.Ords.Where(ExpressionBuilder.GetExpression<OrdModel>(filter))
                    .OrderBy(x => x.VoucherDate).ThenBy(x => x.Id)
                    .ProjectToList<OrdDto>(config);

                if (FilterView.Count == 0)
                {
                    MessageBoxAdv.Show(this, "No Record Found", "Find !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ResetPage();
                    this.NewRec();
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
            OrdDto model = new OrdDto();
            model.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);
            model.OrderStatusId = 1;
            model.VoucherNo = voucherNoTextEdit.Text.Trim();
            model.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
            model.RequireDate = requireDateEdit.DateTime;
            model.PGroupId = Convert.ToInt32(pgLookup1.SelectedValue);
            model.AccId = Convert.ToInt32(accLookup1.SelectedValue);
            model.RefNo = refNoTextEdit.Text.Trim();
            model.EmpId = 1;
            
            model.Remarks = remarkTextEdit.Text.Trim();
            model.TransportId = 1;
            model.AgentId = Convert.ToInt32(agentLookup.SelectedValue);
            model.PayTermsId = 1;
            model.TypeId = (int)VoucherTypeEnum.GreyOrder;
            model.CompId = KontoGlobals.CompanyId;
            model.YearId = KontoGlobals.YearId;
            model.BranchId = KontoGlobals.BranchId;
            model.Extra1 = daysSpinEdit.Text;

            var _find = new OrdModel();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrdDto, OrdModel>().ForMember(x => x.Id, p => p.Ignore()
                );
                cfg.CreateMap<OrdTransDto, OrdTransModel>().ForMember(x => x.Id, p => p.Ignore());
            });

         

            using (var db = new KontoContext())
            {
                using (var _tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        string createuser = KontoGlobals.UserName;
                        DateTime createdate = DateTime.Now;
                        var tranModel = new OrdTransModel();
                        if(model.AccId==0)
                         model.AccId = 1;
                        if (this.PrimaryKey != 0)
                        {
                            _find = db.Ords.Find(this.PrimaryKey);
                            tranModel = db.OrdTranses.FirstOrDefault(x => x.OrdId == _find.Id);
                            createuser = _find.CreateUser;
                            createdate = Convert.ToDateTime(_find.CreateDate);
                        }

                        var map = new Mapper(config);
                        map.Map(model, _find);

                        _find.IsActive = true;
                        if (this.PrimaryKey == 0)
                        {
                              _find.VoucherNo = DbUtils.NextSerialNo(_find.VoucherId, db);
                              model.VoucherNo = _find.VoucherNo;
                            if (DbUtils.CheckExistVoucherNo(_find.VoucherId, _find.VoucherNo, db, _find.Id))
                            {
                                MessageBox.Show("Duplicate Voucher No Not Allowed");
                                _tran.Rollback();
                                return;
                            }
                            _find.TypeId = (int)VoucherTypeEnum.GreyOrder;
                            db.Ords.Add(_find);
                            db.SaveChanges();
                        }
                        else
                        {
                            _find.CreateDate = createdate;
                            _find.CreateUser = createuser;
                        }
                        tranModel.ProductId = Convert.ToInt32(productLookup1.SelectedValue);
                        tranModel.OrdId = _find.Id;
                        tranModel.NoOfLot = Convert.ToInt32( lotSpinEdit.Value);
                        tranModel.LotPcs = Convert.ToInt32(takaLotSpinEdit.Value);
                        tranModel.Rate = rateSpinEdit.Value;
                        tranModel.AvgWt = wtSpinEdit.Value;
                        tranModel.Cut = metersSpinEdit.Value;
                        tranModel.OrdStatus = statusLookUpEdit.EditValue.ToString();
                        tranModel.Disc = discDpinEdit.Value;
                        tranModel.Qty = totalMeterSpinEdit.Value;
                      
                        tranModel.UomId = 24;
                        if (tranModel.Qty == 0)
                            tranModel.Qty = tranModel.NoOfLot* tranModel.LotPcs;
                        if (tranModel.Id == 0)
                            {

                                db.OrdTranses.Add(tranModel);
                            }

                        //if (this.PrimaryKey == 0)
                        //    DbUtils.UsedSerial(_find.VoucherId, _SerialValue, db);

                        db.SaveChanges();
                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Grey Order Save");
                        MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());

                    }
                }
            }



            if (IsSaved)
            {
                NewRec();
                base.SaveDataAsync(newmode);
                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage + " Order No.: " + model.VoucherNo, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (!this.OpenForLookup && newmode)
                {
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


        #endregion

        private void lotSpinEdit_EditValueChanged(object sender, EventArgs e)
        {
            if(lotSpinEdit.Value >0 && metersSpinEdit.Value > 0)
                totalMeterSpinEdit.Value = lotSpinEdit.Value * metersSpinEdit.Value;
        }

        private void metersSpinEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (lotSpinEdit.Value > 0 && metersSpinEdit.Value > 0)
                totalMeterSpinEdit.Value = lotSpinEdit.Value * metersSpinEdit.Value;
        }
    }
}
