using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Konto.Core.Shared.Frms;
using Konto.App.Shared;
using Konto.Data;
using Konto.Core.Shared.Libs;
using Syncfusion.Windows.Forms;
using Serilog;
using System.Data.SqlClient;
using Konto.Shared.Reports;
using DevExpress.XtraGrid.Views.Grid;
using GrapeCity.ActiveReports;
using System.IO;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;

namespace Konto.Shared.Account.Receipt
{
    public partial class ReceiptListView : ListBaseView
    {
        //private List<OpBillListDto> _modelList = new List<OpBillListDto>();

        public ReceiptListView()

        {
            InitializeComponent();
            this.listDateRange1.GetButtonClick += ListDateRange1_GetButtonClick;
            chqRetSimpleButton.Click += ChqRetSimpleButton_Click;
            //  this.GridLayoutFileName = KontoFileLayout.Op_Bill_List;
            this.customGridView1.FocusedRowChanged += CustomGridView1_FocusedRowChanged;
            this.ReportPrint = true;
            listAction1.EditDeleteDisabled(false);
        }

        private void ChqRetSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (customGridView1.SelectedRowsCount <= 0) return;
                if (MessageBoxAdv.Show(this,"Are you sure?, all adjustment will be removed.. ?","Cheque Return", MessageBoxButtons.YesNo) == DialogResult.No) return;
                using (var db = new KontoContext())
                {
                    var rw = customGridView1.GetFocusedDataRow();
                    var _transid = Convert.ToInt32(rw["TransId"]);
                    var _id = Convert.ToInt32(rw["Id"]);

                    var rcpt = db.Bills.Find(_id);

                    if (rcpt == null) return;
                    // check for existing return entry

                    if (db.Bills.Any(x =>
                        x.RefId == rcpt.Id && x.RefVoucherId == rcpt.RefVoucherId &&
                        !x.IsDeleted && x.IsActive))
                    {
                        MessageBox.Show(@"Cheque Return Entry Already Exists");
                        return;
                    }
                    
                    
                    var rctrans = db.BillTrans.Find(_transid);
                    if(rctrans== null) return;

                    bool result = LedgerEff.DataFreezeStatus(rcpt.VoucherDate, rcpt.TypeId, db);
                    if (result == false)
                    {
                        MessageBox.Show(KontoGlobals.DeleteFreezeWarning);
                        return;
                    }
                    var billDel = db.BtoBs.Where(k => k.RefId == rcpt.Id && k.RefVoucherId == rcpt.VoucherId && k.IsActive && k.IsDeleted == false).ToList();
                    using (var _tran = db.Database.BeginTransaction())
                    {
                        try
                        {
                            if (billDel.Count > 0)
                            {
                                db.BtoBs.RemoveRange(billDel);
                            }

                            var model = new BillModel();
                            model.VoucherId = db.Vouchers
                                .FirstOrDefault(x => x.VTypeId == (int) VoucherTypeEnum.PaymentVoucher).Id; 
                            
                            model.VoucherDate = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));


                            model.AccId = rcpt.AccId;

                            model.VoucherNo = DbUtils.NextSerialNo(model.VoucherId, db);


                            model.RefNo = rcpt.RefNo;


                            model.EmpId = rcpt.EmpId;
                            model.StoreId = rcpt.StoreId;

                            model.Remarks = rcpt.Remarks;

                            model.TypeId = (int)VoucherTypeEnum.PaymentVoucher;
                            model.CompId = KontoGlobals.CompanyId;
                            model.YearId = KontoGlobals.YearId;
                            model.BranchId = KontoGlobals.BranchId;
                            model.TotalAmount = rctrans.NetTotal;
                            model.GrossAmount = 0;
                            model.TotalQty = 0;
                            model.TotalPcs = 0;
                            model.IsActive = true;
                            model.RefId = rcpt.Id;
                            model.RefVoucherId = rcpt.RefVoucherId;
                            db.Bills.Add(model);
                            db.SaveChanges();

                            var tm = new BillTransModel();
                            tm.BillId = model.Id;
                            tm.NetTotal = rctrans.NetTotal;
                            tm.Total = rctrans.Total;
                            tm.ToAccId = rctrans.ToAccId;
                            tm.RpType = "Bill";
                            tm.ChequeNo = rctrans.ChequeNo;
                            tm.ChequeDate = rctrans.ChequeDate;
                            tm.Remark = "Cheque Return Voucher No: " + rcpt.VoucherNo;
                            tm.IsActive = true;
                            db.BillTrans.Add(tm);
                            db.SaveChanges();

                            var br = db.BillRefs.FirstOrDefault(x =>
                                x.BillId == rctrans.BillId && x.BillTransId == rctrans.Id);

                            var bs = new BtoBModel();
                            bs.Amount = tm.NetTotal;
                            
                            bs.BillId = br.BillId;
                            bs.BillTransId = br.BillTransId;
                            bs.BillVoucherId = br.BillVoucherId;
                            bs.RefVoucherId = model.VoucherId;
                            bs.CompanyId = KontoGlobals.CompanyId;
                            bs.RefTransId = tm.Id;
                            bs.RefId = tm.BillId;
                            bs.RefCode = br.RowId;
                            bs.IsActive = true;
                            bs.TransType = "Payment";
                            bs.BillNo = rcpt.VoucherNo;
                            db.BtoBs.Add(bs);

                            var bss = new List<PendBillListDto>();

                            var pbs =    new PendBillListDto
                                {
                                    Amount = bs.Amount, BillId = bs.BillId, BillTransId = bs.BillTransId,
                                    BillVoucherId = bs.BillVoucherId, RefTransId = bs.RefTransId,
                                    RefCode = bs.RefCode
                                };
                            bss.Add(pbs);

                            var Trans = new List<BillTransModel>
                            {
                                tm
                            };

                            LedgerEff.BillRefEntrypayrec("Debit", model, Trans, new List<BankTransDto>(), db);

                            LedgerEff.LedgerTransEntryRecPay("Debit", model, db, Trans, bss);

                            rctrans.Remark = "ChqRet: " + rctrans.Remark;
                            db.SaveChanges();
                            _tran.Commit();
                            MessageBox.Show("Cheque Return Saved Successfully");

                        }
                        catch (Exception ex)
                        {
                            _tran.Rollback();       
                            Log.Error(ex,"cheque return");
                            MessageBox.Show(ex.ToString());
                        }
                    }
                   

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "cheq_return,receipt");
                MessageBox.Show(ex.ToString());
                
            }
        }

        private void CustomGridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (customGridView1.FocusedRowHandle < 0) return;
                var _id = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "Id"));
                var _vid = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "VoucherId"));
                var _acid = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "ToAccId"));
                var _transid = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "TransId"));
                using (var db = new KontoContext())
                {
                    var proc = "dbo.PendingBill";
                    var spcol = db.SpCollections.FirstOrDefault(k => k.Id ==
                        (int)SpCollectionEnum.PendingBill);
                    if (spcol != null) proc = spcol.Name;

                    var BillList = db.Database.SqlQuery<PendBillListDto>(
                            spcol.Name + " @CompanyId={0},@AccountId={1},@VoucherTypeId={2},@BillType={3}" +
                            ",@RefId={4},@RefTransId={5},@RefVoucherId={6}",
                             KontoGlobals.CompanyId, _acid,(int)VoucherTypeEnum.ReceiptVoucher, "DEBIT", _id, _transid, _vid).ToList();

                    gridControl1.DataSource = BillList.Where(x=>x.Amount > 0).ToList();
                }   

            }
            catch (Exception ex)
            {

                Log.Error(ex, "Receipt List");
                
            }
        }

        private void ListDateRange1_GetButtonClick(object sender, EventArgs e)
        {
            this.GridLayoutFileName = listDateRange1.SelectedItem.LayoutFile;
            var DtCriterias = new DataTable();
            try
            {
                var db = new KontoContext();
                using (var con = new SqlConnection(db.Database.Connection.ConnectionString))
                {
                    
                    using (var cmd = new SqlCommand(listDateRange1.SelectedItem.SpName, con))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Add("@fromDate", SqlDbType.Int).Value = listDateRange1.FromDate;
                        cmd.Parameters.Add("@ToDate", SqlDbType.Int).Value = listDateRange1.ToDate;
                        cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = KontoGlobals.CompanyId;
                        cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = KontoGlobals.BranchId;
                        cmd.Parameters.Add("@YearId", SqlDbType.Int).Value = KontoGlobals.YearId;
                        cmd.Parameters.Add("@VTypeId", SqlDbType.Int).Value = (int)VoucherTypeEnum.ReceiptVoucher;
                        if (listDateRange1.SelectedItem.Extra1 == "Deleted")
                        {
                            cmd.Parameters.Add("@Deleted", SqlDbType.Int).Value = 1;
                        }
                        if (listDateRange1.SelectedItem.GroupCol != null)
                        {
                            string grpCol = listDateRange1.SelectedItem.GroupCol;
                            cmd.Parameters.Add("@GrpBy", SqlDbType.Text).Value = listDateRange1.SelectedItem.GroupCol;
                        }
                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();
                       
                        DtCriterias.Load(cmd.ExecuteReader());
                        con.Close();
                        customGridView1.ShowLoadingPanel();
                        customGridView1.Columns.Clear();
                        customGridControl1.DataSource = DtCriterias;
                        customGridView1.HideLoadingPanel();
                    }
                }
                if (string.IsNullOrEmpty(this.GridLayoutFileName) || this.KontoView == null) return;

                KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, this.KontoView);

                this.ActiveControl = customGridControl1;


                if (DtCriterias.Rows.Count == 0)
                    listAction1.EditDeleteDisabled(false);
                else
                {
                    if (customGridView1.Columns.ColumnByFieldName("Id") != null && customGridView1.Columns.ColumnByFieldName("VoucherId") != null)
                        listAction1.EditDeleteDisabled(true);
                    else
                        listAction1.EditDeleteDisabled(false);
                }


                customGridView1.OptionsSelection.MultiSelect = true;
                customGridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Gp List Error");
                MessageBoxAdv.Show(this, "Error While Generating List !!", "Exception ", ex.ToString());
            }
        }

        public override void RefreshGrid()
        {
          
        }

        public override void DeleteRec()
        {
            base.DeleteRec();

            if (customGridView1.SelectedRowsCount <= 0) return;
            var drs = customGridView1.GetSelectedRows();
            if (MessageBoxAdv.Show(KontoGlobals.DeleteBeforeMsg, "Delete ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            using (var db = new KontoContext())
            {
                using (var _tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var rowno in drs)
                        {
                            var row = customGridView1.GetDataRow(rowno);

                            var _id = Convert.ToInt32(row["Id"]);   //Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "Id"));
                                                                    //var _vid = //Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "VoucherId"));
                                                                    //var _deleted = Convert.ToBoolean(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "IsDeleted"));
                            var _vid = Convert.ToInt32(row["VoucherId"]);
                            var _deleted = Convert.ToBoolean(row["IsDeleted"]);
                            if (_deleted)
                            {
                                MessageBoxAdv.Show("Record Already in Deleted State", "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _tran.Rollback();
                                return;
                            }

                            var model = db.Bills.Find(_id);

                            bool result = LedgerEff.DataFreezeStatus(model.VoucherDate, model.TypeId, db);
                            if (result == false)
                            {
                                MessageBox.Show(KontoGlobals.DeleteFreezeWarning);
                                _tran.Rollback();
                                return;
                            }

                            //Delete drcr debit note

                            var drCr = db.BillTrans.Where(k => (k.RefId == model.Id && k.RefVoucherId == model.VoucherId)).ToList();

                            foreach (var item in drCr)
                            {
                                var drCrM = db.Bills.FirstOrDefault(k => k.Id == item.BillId);
                                drCrM.IsDeleted = true;
                                
                               
                                var st = db.Ledgers.Where(k => k.RefId == drCrM.RowId && k.IsActive && k.IsDeleted == false).ToList();
                                db.Ledgers.RemoveRange(st);

                                item.IsDeleted = true;
                            }

                            
                            var trans = db.BillTrans.Where(x => x.BillId == model.Id).ToList();
                            foreach (var item in trans)
                            {
                                item.IsDeleted = true;
                            }


                            model.IsDeleted = true;
                            LedgerEff.DeleLedgEffect(model, db);
                        }
                        customGridView1.DeleteSelectedRows();
                        db.SaveChanges();
                        _tran.Commit();
                        MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Receipt delete");
                        MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
                    }
                }

            }
        }
           
        

        public override void Print()
        {
            base.Print();
            if (this.customGridView1.FocusedRowHandle < 0) return;
            if (KontoView.Columns.ColumnByFieldName("Id") != null)
            {
                if (KontoView.Columns.ColumnByFieldName("IsDeleted") != null)
                {
                    if (Convert.ToBoolean(this.KontoView.GetRowCellValue(this.KontoView.FocusedRowHandle, "IsDeleted")))
                    {
                        return;
                    }
                }
                var _id = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "Id"));
                var _Vno = this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "VoucherNo").ToString();
                var _Vdate = this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "VoucherDate").ToString();
                var vid = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "VoucherId"));

                try
                {
                    var frm = new BankPrintView();
                    frm.frmVouchers = _Vno;
                    frm.toVouchers = _Vno;
                    frm.IsPayment = false;
                    if (frm.ShowDialog() != DialogResult.OK) return;

                    KontoContext db = new KontoContext();
                    int ReportId = 0;
                    if (!string.IsNullOrEmpty(frm.frmVouchers.ToString().Trim()) && !string.IsNullOrEmpty(frm.toVouchers.ToString().Trim()))
                    {
                        if (frm.frmVouchers.ToString().Trim() != frm.toVouchers.ToString().Trim())
                        {
                            var frmVouchers = db.Bills.FirstOrDefault(k => k.VoucherNo == frm.frmVouchers
                                           && k.VoucherId == vid
                                            && k.IsActive && !k.IsDeleted && k.YearId == KontoGlobals.YearId
                                            && k.CompId == KontoGlobals.CompanyId);
                            var Tovoucher = db.Bills.FirstOrDefault(k => k.VoucherNo == frm.toVouchers
                                        && k.VoucherId == vid
                                         && k.IsActive && !k.IsDeleted && k.YearId == KontoGlobals.YearId
                                                     && k.CompId == KontoGlobals.CompanyId);
                            if (frmVouchers != null && frmVouchers != null)
                            {
                                var billIds = db.Bills.Where(k => k.Id >= frmVouchers.Id && k.Id <= Tovoucher.Id
                                && k.IsActive && !k.IsDeleted && k.VoucherId == vid & k.CompId == KontoGlobals.CompanyId
                                && k.YearId == KontoGlobals.YearId).ToList();

                                if (billIds != null)
                                {
                                    var reportid = db.ReportParas.DefaultIfEmpty().Max(k => k == null ? 0 : k.ReportId);
                                    ReportId = reportid != 0 ? reportid + 1 : 1;
                                    foreach (var id in billIds)
                                    {
                                        var ModelReport = new ReportParaModel();
                                        ModelReport.ReportId = ReportId;
                                        ModelReport.ParameterName = "BillId";
                                        ModelReport.ParameterValue = id.Id;
                                        ModelReport.CreateDate = DateTime.Now;
                                        ModelReport.CreateUser = KontoGlobals.UserName;

                                        db.ReportParas.Add(ModelReport);
                                        db.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                    if (frm.chkFarwrd)
                    {
                        PageReport rpt = new PageReport();

                        rpt.Load(new FileInfo("reg\\doc\\ChequeForwardingPrint.rdlx"));

                        rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

                        GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);

                        doc.Parameters["reportid"].CurrentValue = 0;// ReportId;
                        doc.Parameters["VTypeId"].CurrentValue = (int)VoucherTypeEnum.ReceiptVoucher;
                        int frmdate = Convert.ToInt32(Convert.ToDateTime(_Vdate).ToString("yyyyMMdd"));
                        doc.Parameters["fromdate"].CurrentValue = frmdate;
                        //if (ReportId <= 0)
                        //{
                        doc.Parameters["bill"].CurrentValue = "N";
                        doc.Parameters["id"].CurrentValue = _id;
                        //}
                        //else
                        //{
                        //    doc.Parameters["bill"].CurrentValue = "Y";
                        //    doc.Parameters["id"].CurrentValue = 0;
                        //}

                        var frm1 = new KontoRepViewer(doc);
                        frm1.Text = "Receipt Voucher Print";
                        frm1.Show();// = true;
                    }
                    if (frm.chkRec)
                    {
                        PageReport rpt = new PageReport();

                        rpt.Load(new FileInfo("reg\\doc\\ReceiptVoucherPrint.rdlx"));

                        rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

                        GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);

                        doc.Parameters["reportid"].CurrentValue = ReportId;
                        doc.Parameters["VTypeId"].CurrentValue = (int)VoucherTypeEnum.ReceiptVoucher;

                        if (ReportId <= 0)
                        {
                            doc.Parameters["bill"].CurrentValue = "N";
                            doc.Parameters["id"].CurrentValue = _id;
                        }
                        else
                        {
                            doc.Parameters["bill"].CurrentValue = "Y";
                            doc.Parameters["id"].CurrentValue = 0;
                        }
                        var frm1 = new KontoRepViewer(doc);
                        frm1.Text = "Receipt Voucher Print";
                        frm1.Show();// = true;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Receipt print");
                    MessageBoxAdv.Show(this, "Error While Print !!", "Exception ", ex.ToString());

                }
            }


        }
        public override void ImportExcel()
        {
            base.ImportExcel();
            var _exp = new RecImport();
            _exp.ShowDialog();
        }

        private void ReceiptListView_Load(object sender, EventArgs e)
        {

        }
    }
}
