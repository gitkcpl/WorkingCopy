using GrapeCity.ActiveReports;
using Konto.App.Shared;
using Konto.App.Shared.Para;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Transaction.Dtos;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Reports
{
    public partial class DocPrintParaView : KontoForm
    {
        public VoucherTypeEnum VoucherType { get; set; }
      
        public string TableName { get; set; }
        public int[] SelectedRows { get; set; }
        public string FilterType { get; set; }
        public string FromVoucherNo { get; set; }
        public string ToVoucherNo { get; set; }
        public int FromDate { get; set; }
        public int ToDate { get; set; }
        public int VoucherId { get; set; }
        public int PartyId { get; set; }
        public int ReportId { get; set; }
        public ReportTypeModel ReportFormat { get; set; }
        public string ParaType { get; set; }

      
        public DocPrintParaView()
        {
            InitializeComponent();
            this.Load += DocPrintParaView_Load;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.voucherLookup1.SelectedValueChanged += VoucherLookup1_SelectedValueChanged;
           
        }

        private void VoucherLookup1_SelectedValueChanged(object sender, EventArgs e)
        {
            FillVoucher();
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void CustomSimpleButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(formatLookUpEdit.Text)) return;
            var rep = formatLookUpEdit.Properties.GetDataSourceRowByKeyValue(formatLookUpEdit.EditValue) as ReportTypeModel;
            var frmd = new KontoArDesignerView();
            frmd.endUserDesigner1._reportName = rep.FileName;
            frmd.endUserDesigner1.reportDesigner.LoadReport(new FileInfo(rep.FileName));
            frmd.Text = "Konto Designer - " +  rep.FileName;
            frmd.Show();
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(formatLookUpEdit.Text))
                {
                    MessageBox.Show("Multi Print Not defined for this module..");
                    return;
                }
                this.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);
                this.PartyId = Convert.ToInt32(accLookup1.SelectedValue);
                this.FromVoucherNo = fromTextEdit.Text.Trim();
                this.ToVoucherNo = toTextEdit.Text.Trim();
                this.FromDate = Convert.ToInt32(fromDateEdit.DateTime.ToString("yyyyMMdd"));
                this.ToDate = Convert.ToInt32(ToDateEdit.DateTime.ToString("yyyyMMdd"));
                this.FilterType = filterTypeLookUpEdit.EditValue.ToString();
                this.ReportFormat = formatLookUpEdit.Properties.GetDataSourceRowByKeyValue(formatLookUpEdit.EditValue) as ReportTypeModel;

                this.SelectedRows = gridView1.GetSelectedRows();
                List<ReportParaModel> _paralists = new List<ReportParaModel>();
                var _db = new KontoContext();
                var reportid = _db.ReportParas.DefaultIfEmpty().Max(k => k == null ? 0 : k.ReportId);
                ReportId = reportid != 0 ? reportid + 1 : 1;

                foreach (var item in this.SelectedRows)
                {
                    var row = gridView1.GetRow(item) as VoucherNoDto;
                    var ModelReport = new ReportParaModel();
                    ModelReport.ReportId = ReportId;
                    ModelReport.ParameterName = this.ParaType;
                    ModelReport.ParameterValue = row.Id;
                    ModelReport.CreateDate = DateTime.Now;
                    ModelReport.CreateUser = KontoGlobals.UserName;
                    _paralists.Add(ModelReport);

                }
                var rep = formatLookUpEdit.Properties.GetDataSourceRowByKeyValue(formatLookUpEdit.EditValue) as ReportTypeModel;
                PageReport rpt = new PageReport();
                rpt.Load(new FileInfo(rep.FileName));
                rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;
                
                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);
               
               
               

                    if (this.TableName == "ORD" || this.TableName == "PORD")
                    {
                        var BillId = new List<IdListDto>();
                        if (filterTypeLookUpEdit.EditValue.ToString() == "Party Wise")
                            BillId = OrderPrintParty(_db);
                        else if (filterTypeLookUpEdit.EditValue.ToString() == "Date Wise")
                            BillId = OrderPrintDate(_db);
                        else if (_paralists.Count == 0)
                            BillId = OrderPrintDefault(_db);
                        if (BillId != null)
                        {
                            foreach (var id in BillId)
                            {
                                var ModelReport = new ReportParaModel();
                                ModelReport.ReportId = ReportId;
                                ModelReport.ParameterName = this.ParaType;
                                ModelReport.ParameterValue = id.Id;
                                ModelReport.CreateDate = DateTime.Now;
                                ModelReport.CreateUser = KontoGlobals.UserName;
                                _paralists.Add(ModelReport);
                            }
                            if (_paralists.Count > 0)
                            {
                                _db.ReportParas.AddRange(_paralists);
                                _db.SaveChanges();
                                doc.Parameters["Ord"].CurrentValue = "Y";
                                doc.Parameters["id"].CurrentValue = 0;
                            }
                        }
                        else
                        {
                            doc.Parameters["Ord"].CurrentValue = "N";
                            doc.Parameters["id"].CurrentValue = this.EditKey;
                        }
                    }
                    else if (this.TableName == "PURRET" || this.TableName == "BILL" ||
                        this.TableName == "SALERET" || this.TableName == "DRCR")
                    {
                        var BillId = new List<IdListDto>();
                        if (filterTypeLookUpEdit.EditValue.ToString() == "Party Wise")
                            BillId = PretPrintParty(_db);
                        else if (filterTypeLookUpEdit.EditValue.ToString() == "Date Wise")
                            BillId = PretPrintDate(_db);
                        else if (_paralists.Count == 0)
                             BillId = PretPrintDefault(_db);
                        if (BillId != null)
                        {
                            foreach (var id in BillId)
                            {
                                var ModelReport = new ReportParaModel();
                                ModelReport.ReportId = ReportId;
                                ModelReport.ParameterName = this.ParaType;
                                ModelReport.ParameterValue = id.Id;
                                ModelReport.CreateDate = DateTime.Now;
                                ModelReport.CreateUser = KontoGlobals.UserName;
                                _paralists.Add(ModelReport);
                            }
                            if (_paralists.Count > 0)
                            {
                                _db.ReportParas.AddRange(_paralists);
                                _db.SaveChanges();
                                doc.Parameters["bill"].CurrentValue = "Y";
                                doc.Parameters["id"].CurrentValue = 0;
                            }
                        }
                        else
                        {
                            doc.Parameters["bill"].CurrentValue = "N";
                            doc.Parameters["id"].CurrentValue = this.EditKey;
                        }
                       // rpt.ResourceLocator = new MySubreportLocator();


                    }
                    else if (this.TableName == "MI" || this.TableName == "JIB" || this.TableName == "challan" ||
                        this.TableName == "JSC" || this.TableName=="GRN")
                    {
                        var BillId = new List<IdListDto>();
                        if (filterTypeLookUpEdit.EditValue.ToString() == "Party Wise")
                            BillId = MiPrintParty(_db);
                        else if (filterTypeLookUpEdit.EditValue.ToString() == "Date Wise")
                            BillId = MiPrintDate(_db);
                        else if(_paralists.Count == 0)
                            BillId = MiPrintDefault(_db);
                        if (BillId != null)
                        {
                            foreach (var id in BillId)
                            {
                                var ModelReport = new ReportParaModel();
                                ModelReport.ReportId = ReportId;
                                ModelReport.ParameterName = this.ParaType;
                                ModelReport.ParameterValue = id.Id;
                                ModelReport.CreateDate = DateTime.Now;
                                ModelReport.CreateUser = KontoGlobals.UserName;
                                _paralists.Add(ModelReport);
                            }
                            if (_paralists.Count > 0)
                            {
                                _db.ReportParas.AddRange(_paralists);
                                _db.SaveChanges();
                                doc.Parameters["challan"].CurrentValue = "Y";
                                doc.Parameters["id"].CurrentValue = 0;
                            }
                        }
                        else
                        {
                            doc.Parameters["challan"].CurrentValue = "N";
                            doc.Parameters["id"].CurrentValue = this.EditKey;
                        }
                    }


                
                doc.Parameters["reportid"].CurrentValue = ReportId;

                var frm = new KontoRepViewer(doc);
                frm.Text = "Doc/Bill Print";
                frm.ShowDialog(this);

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Doc Print");
                MessageBox.Show(ex.ToString());
            }
            
        }
        #region "Order Print"
        private List<IdListDto> OrderPrintParty(KontoContext db)
        {
            var BillId = (from o in db.Ords
                           orderby o.Id
                          where o.CompId == KontoGlobals.CompanyId && o.YearId == KontoGlobals.YearId && !o.IsDeleted
                                && o.AccId == this.PartyId
                                && o.VoucherId == this.VoucherId
                                && o.IsActive && !o.IsDeleted && o.VoucherDate>=this.FromDate && o.VoucherDate<=this.ToDate
                          select new IdListDto()
                          {
                              Id = o.Id
                          }
                            ).ToList();

            return BillId;
            
        }
        private List<IdListDto> OrderPrintDate(KontoContext db)
        {
            var BillId = (from o in db.Ords
                          orderby o.Id
                          where o.CompId == KontoGlobals.CompanyId && o.YearId == KontoGlobals.YearId && !o.IsDeleted
                                && o.VoucherId == this.VoucherId
                                && o.IsActive && !o.IsDeleted && o.VoucherDate >= this.FromDate && o.VoucherDate <= this.ToDate
                          select new IdListDto()
                          {
                              Id = o.Id
                          }
                            ).ToList();

            return BillId;

        }
        private List<IdListDto> OrderPrintDefault(KontoContext _db)
        {
            if (fromTextEdit.Text.Trim() != toTextEdit.Text.Trim())
            {
                //using (var sqlcon = new SqlConnection(db.Database.Connection.ConnectionString))
                //{
                //    string sql = string.Empty;
                //    if (this.VoucherType == VoucherTypeEnum.SalesOrder)
                //    {
                //        sql = "select Id from Ord where dbo.ufnGetNumeric(VoucherNo) Between dbo.UfnGetnumeric('" + fromTextEdit.Text.Trim() + "') ";
                //        sql = sql + " and dbo.ufnGetNumeric('" + toTextEdit.Text.Trim() + ")'";
                //        sql = sql + " and voucherdate between " + fromDateEdit.DateTime.ToString("yyyyMMdd") + " AND " + ToDateEdit.DateTime.ToString("yyyyMMdd");
                //        sql = sql + " and voucherid =" + voucherLookup1.SelectedValue;
                //        sql = sql + " AND compid=" + KontoGlobals.CompanyId;
                //    }
                //    using (var cmd = new SqlCommand(sql, sqlcon))
                //    {

                //    }
                //}

                int frmVNo = 0;
                int TVNo = 0;
                var frmVouchers = _db.Ords.FirstOrDefault(k => k.VoucherNo == fromTextEdit.Text.Trim()
                                        && k.VoucherId == this.VoucherId
                                         && k.IsActive && !k.IsDeleted && k.YearId == KontoGlobals.YearId
                                         && k.CompId == KontoGlobals.CompanyId);
                var Tovoucher = _db.Ords.FirstOrDefault(k => k.VoucherNo == toTextEdit.Text.Trim()
                            && k.VoucherId == this.VoucherId
                             && k.IsActive && !k.IsDeleted && k.YearId == KontoGlobals.YearId
                                         && k.CompId == KontoGlobals.CompanyId);

                if (frmVouchers != null)
                    frmVNo = frmVouchers.Id;

                if (Tovoucher != null)
                    TVNo = Tovoucher.Id;
                var ords = _db.Ords.Where(o => o.Id >= frmVNo && o.Id <= TVNo && o.VoucherId == this.VoucherId
                            && !o.IsDeleted && o.IsActive && o.CompId == KontoGlobals.CompanyId
                            && o.YearId == KontoGlobals.YearId)
                            .Select(x => new IdListDto { Id = x.Id }).ToList();
                return ords;
                
            }

            return null;
        }
        #endregion

        #region "Mill Issue Print'
        private List<IdListDto> MiPrintParty(KontoContext db)
        {
            var BillId = (from o in db.Challans
                          orderby o.Id
                          where o.CompId == KontoGlobals.CompanyId && o.YearId == KontoGlobals.YearId && !o.IsDeleted
                                && o.AccId == this.PartyId
                                && o.VoucherId == this.VoucherId
                                && o.IsActive && !o.IsDeleted && o.VoucherDate >= this.FromDate && o.VoucherDate <= this.ToDate
                          select new IdListDto()
                          {
                              Id = o.Id
                          }
                            ).ToList();

            return BillId;

        }
        private List<IdListDto> MiPrintDate(KontoContext db)
        {
            var BillId = (from o in db.Challans
                          orderby o.Id
                          where o.CompId == KontoGlobals.CompanyId && o.YearId == KontoGlobals.YearId && !o.IsDeleted
                                && o.VoucherId == this.VoucherId
                                && o.IsActive && !o.IsDeleted && o.VoucherDate >= this.FromDate && o.VoucherDate <= this.ToDate
                          select new IdListDto()
                          {
                              Id = o.Id
                          }
                            ).ToList();

            return BillId;

        }
        private List<IdListDto> MiPrintDefault(KontoContext _db)
        {
            if (fromTextEdit.Text.Trim() != toTextEdit.Text.Trim())
            {
                
                int frmVNo = 0;
                int TVNo = 0;
                var frmVouchers = _db.Challans.FirstOrDefault(k => k.VoucherNo == fromTextEdit.Text.Trim()
                                        && k.VoucherId == this.VoucherId
                                         && k.IsActive && !k.IsDeleted && k.YearId == KontoGlobals.YearId
                                         && k.CompId == KontoGlobals.CompanyId);
                var Tovoucher = _db.Challans.FirstOrDefault(k => k.VoucherNo == toTextEdit.Text.Trim()
                            && k.VoucherId == this.VoucherId
                             && k.IsActive && !k.IsDeleted && k.YearId == KontoGlobals.YearId
                                         && k.CompId == KontoGlobals.CompanyId);

                if (frmVouchers != null)
                    frmVNo = frmVouchers.Id;

                if (Tovoucher != null)
                    TVNo = Tovoucher.Id;
                var ords = _db.Challans.Where(o => o.Id >= frmVNo && o.Id <= TVNo && o.VoucherId == this.VoucherId
                            && !o.IsDeleted && o.IsActive && o.CompId== KontoGlobals.CompanyId 
                            && o.YearId== KontoGlobals.YearId)
                            .Select(x => new IdListDto { Id = x.Id }).ToList();
                return ords;

            }

            return null;
        }
        #endregion

        #region "Purchase Return Print"
        private List<IdListDto> PretPrintParty(KontoContext db)
        {
            var BillId = (from o in db.Bills
                          orderby o.Id
                          where o.CompId == KontoGlobals.CompanyId && o.YearId == KontoGlobals.YearId && !o.IsDeleted
                                && o.AccId == this.PartyId
                                && o.VoucherId == this.VoucherId
                                && o.IsActive && !o.IsDeleted && o.VoucherDate >= this.FromDate && o.VoucherDate <= this.ToDate
                          select new IdListDto()
                          {
                              Id = o.Id
                          }
                            ).ToList();

            return BillId;

        }
        private List<IdListDto> PretPrintDate(KontoContext db)
        {
            var BillId = (from o in db.Bills
                          orderby o.Id
                          where o.CompId == KontoGlobals.CompanyId && o.YearId == KontoGlobals.YearId && !o.IsDeleted
                                && o.VoucherId == this.VoucherId
                                && o.IsActive && !o.IsDeleted && o.VoucherDate >= this.FromDate && o.VoucherDate <= this.ToDate
                          select new IdListDto()
                          {
                              Id = o.Id
                          }
                            ).ToList();

            return BillId;

        }
        private List<IdListDto> PretPrintDefault(KontoContext _db)
        {
            if (fromTextEdit.Text.Trim() != toTextEdit.Text.Trim())
            {
               

                int frmVNo = 0;
                int TVNo = 0;
                var frmVouchers = _db.Bills.FirstOrDefault(k => k.VoucherNo == fromTextEdit.Text.Trim()
                                        && k.VoucherId == this.VoucherId
                                         && k.IsActive && !k.IsDeleted && k.YearId == KontoGlobals.YearId
                                         && k.CompId == KontoGlobals.CompanyId);
                var Tovoucher = _db.Bills.FirstOrDefault(k => k.VoucherNo == toTextEdit.Text.Trim()
                            && k.VoucherId == this.VoucherId
                             && k.IsActive && !k.IsDeleted && k.YearId == KontoGlobals.YearId
                                         && k.CompId == KontoGlobals.CompanyId);

                if (frmVouchers != null)
                    frmVNo = frmVouchers.Id;

                if (Tovoucher != null)
                    TVNo = Tovoucher.Id;
                var ords = _db.Bills.Where(o => o.Id >= frmVNo && o.Id <= TVNo && o.VoucherId == this.VoucherId
                             && !o.IsDeleted && o.IsActive && o.CompId == KontoGlobals.CompanyId
                            && o.YearId == KontoGlobals.YearId)
                            .Select(x => new IdListDto { Id = x.Id }).ToList();
                return ords;

            }

            return null;
        }
        #endregion
        public DocPrintParaView(VoucherTypeEnum _vtype,string _title,string fromno, string tono,string _tablename, string _paratype)
        {
            InitializeComponent();
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.customSimpleButton.Click += CustomSimpleButton_Click;
            this.Load += DocPrintParaView_Load;
            this.voucherLookup1.SelectedValueChanged += VoucherLookup1_SelectedValueChanged;
            this.VoucherType = _vtype;
            this.Text = _title;
            this.TableName = _tablename;
            fromTextEdit.Text = fromno;
            toTextEdit.Text = tono;
            this.ParaType = _paratype;
            
            
        }

        private void DocPrintParaView_Load(object sender, EventArgs e)
        {
            if (!KontoGlobals.isSysAdm) customSimpleButton.Visible = false;

            fromDateEdit.EditValue= DateTime.ParseExact(KontoGlobals.FromDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture);
            ToDateEdit.EditValue = DateTime.ParseExact(KontoGlobals.ToDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture);

            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Default", "Default"),
                new ComboBoxPairs("Party Wise", "Party Wise"),
                new ComboBoxPairs("Date Wise", "Date Wise")

            };
            filterTypeLookUpEdit.Properties.DataSource = cbp;
            filterTypeLookUpEdit.EditValue = "Default";
            this.voucherLookup1.VTypeId = this.VoucherType;
            this.accLookup1.VoucherType = this.VoucherType;
            this.voucherLookup1.SetDefault();
           // FillVoucher();
        }

        private void FillVoucher()
        {
            using (var db = new KontoContext())
            {
                var model = db.ReportTypes.Where(x => x.ReportTypes.ToUpper() == this.TableName.ToUpper()).ToList();
                formatLookUpEdit.Properties.DataSource = model;
                if (model.Count() > 0)
                {
                    formatLookUpEdit.EditValue = model[0].Id;
                }
                if (this.VoucherType == VoucherTypeEnum.SaleInvoice)
                {
                    var id = model.FirstOrDefault(k => k.FileName == "reg\\doc\\" + BillPara.Default_Invoice_Print).Id;
                    if (Convert.ToInt32(id) > 0)
                    {
                        formatLookUpEdit.EditValue = id;
                    }
                }

                var vid = (int)this.VoucherType;
                var _idid = Convert.ToInt32(voucherLookup1.SelectedValue);
                if (this.VoucherType == VoucherTypeEnum.SalesOrder || this.VoucherType == VoucherTypeEnum.PurchaseOrder)
                {

                    var _vlist = (from o in db.Ords
                                  join voucher in db.Vouchers on o.VoucherId equals voucher.Id into join_voucher
                                  from voucher in join_voucher.DefaultIfEmpty()
                                  orderby o.Id
                                  where o.CompId == KontoGlobals.CompanyId && o.YearId == KontoGlobals.YearId && !o.IsDeleted
                                  && (o.VoucherDate >= KontoGlobals.FromDate && o.VoucherDate <= KontoGlobals.ToDate)
                                  && voucher.VTypeId == vid && o.VoucherId == _idid
                                  orderby o.Id descending
                                  select new VoucherNoDto()
                                  {
                                      VoucherNo = o.VoucherNo,
                                      VoucherId = o.VoucherId,
                                      Id = o.Id
                                  }).ToList();

                    this.gridControl1.DataSource = null;
                    this.gridControl1.DataSource = _vlist;
                }
                else if (this.VoucherType == VoucherTypeEnum.PurchaseReturn || this.VoucherType == VoucherTypeEnum.SaleInvoice ||
                     this.VoucherType == VoucherTypeEnum.SaleReturn || this.VoucherType == VoucherTypeEnum.DebitCreditNote)
                {
                    var _vlist = (from o in db.Bills
                                  join voucher in db.Vouchers on o.VoucherId equals voucher.Id into join_voucher
                                  from voucher in join_voucher.DefaultIfEmpty()
                                  orderby o.Id
                                  where o.CompId == KontoGlobals.CompanyId && o.YearId == KontoGlobals.YearId && !o.IsDeleted
                                  && (o.VoucherDate >= KontoGlobals.FromDate && o.VoucherDate <= KontoGlobals.ToDate)
                                  && voucher.VTypeId == vid && o.VoucherId == _idid
                                  orderby o.Id descending
                                  select new VoucherNoDto()
                                  {
                                      VoucherNo = o.VoucherNo,
                                      VoucherId = o.VoucherId,
                                      Id = o.Id
                                  }).ToList();

                    this.gridControl1.DataSource = null;
                    this.gridControl1.DataSource = _vlist;
                }
                else if (this.VoucherType == VoucherTypeEnum.MillIssue || this.VoucherType == VoucherTypeEnum.JobIssue
                    || this.VoucherType == VoucherTypeEnum.SalesChallan || this.VoucherType == VoucherTypeEnum.OutJobChallan
                    || this.VoucherType == VoucherTypeEnum.Inward || this.VoucherType == VoucherTypeEnum.MillReceipt ||
                    this.VoucherType == VoucherTypeEnum.JobReceipt)
                {
                    var _vlist = (from o in db.Challans
                                  join voucher in db.Vouchers on o.VoucherId equals voucher.Id into join_voucher
                                  from voucher in join_voucher.DefaultIfEmpty()
                                  orderby o.Id
                                  where o.CompId == KontoGlobals.CompanyId && o.YearId == KontoGlobals.YearId && !o.IsDeleted
                                  && (o.VoucherDate >= KontoGlobals.FromDate && o.VoucherDate <= KontoGlobals.ToDate)
                                  && voucher.VTypeId == vid && o.VoucherId == _idid
                                  orderby o.Id descending
                                  select new VoucherNoDto()
                                  {
                                      VoucherNo = o.VoucherNo,
                                      VoucherId = o.VoucherId,
                                      Id = o.Id
                                  }).ToList();

                    this.gridControl1.DataSource = null;
                    this.gridControl1.DataSource = _vlist;
                }
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
    class IdListDto
    {
        public int Id { get; set; }
    }
}
