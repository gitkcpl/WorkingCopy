using System;
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
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.TradingDto;

namespace Konto.Trading.MillIssue
{
    public partial class MillIssueListView : ListBaseView
    {
        //private List<OpBillListDto> _modelList = new List<OpBillListDto>();
        private string GreyRcptAgainstGoDtoFile = KontoFileLayout.Grey_Order_Mill_Receipt_Layout;
        public MillIssueListView()

        {
            InitializeComponent();
            this.listDateRange1.GetButtonClick += ListDateRange1_GetButtonClick;
            //  this.GridLayoutFileName = KontoFileLayout.Op_Bill_List;
            this.customGridView1.FocusedRowChanged += CustomGridView1_FocusedRowChanged;
            this.rcptGridControl.DoubleClick += RcptGridControl_DoubleClick;
            this.rcptGridControl.ProcessGridKey += RcptGridControl_ProcessGridKey;
            this.ReportPrint = true;
            listAction1.EditDeleteDisabled(false);
        }

        private void RcptGridControl_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.F2 | Keys.Shift))
            {
                var frm = new GridPropertView();
                frm.gridControl1.DataSource = this.rcptGridControl.DataSource;
                frm.gridView1.Assign(this.rcptGridView, false);
                if (frm.ShowDialog() != DialogResult.OK) return;
                this.rcptGridView.Assign(frm.gridView1, false);
                KontoUtils.SaveLayoutGrid(this.GreyRcptAgainstGoDtoFile, this.rcptGridView);
            }
        }

        private void RcptGridControl_DoubleClick(object sender, EventArgs e)
        {
            if (rcptGridView.FocusedRowHandle < 0) return;
            var id = Convert.ToInt32(rcptGridView.GetRowCellValue(rcptGridView.FocusedRowHandle, "Id"));
            var frm = new MillReceipt.MrvIndex();
            frm.Tag = MenuId.Mill_Receipt;
            frm.ViewOnlyMode = true;
            frm.EditKey = id;

            frm.ShowDialog();
        }

        private void CustomGridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (customGridView1.FocusedRowHandle < 0) return;
            var _id = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "Id"));
            var _vid = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "VoucherId"));

            using (var _context = new KontoContext())
            {
                var rcptList = (
                                from oc in _context.Challans
                                join ot in _context.ChallanTranses on oc.Id equals ot.ChallanId
                                join rt in _context.ChallanTranses on new { p1 = ot.Id, p2 = ot.ChallanId } equals new { p1 = (int)rt.RefId, p2 = rt.MiscId }
                                join rc in _context.Challans on rt.ChallanId equals rc.Id
                                join ac in _context.Accs on rc.AccId equals ac.Id
                                where rt.MiscId == _id && rt.RefVoucherId == _vid
                                  && !oc.IsDeleted && oc.IsActive == true && !ot.IsDeleted 
                                  && !rt.IsDeleted && !rc.IsDeleted && !ac.IsDeleted
                                select new MillReceiptAgainstOrder()
                                {
                                    ChlnDate = rc.VoucherDate,
                                    ChallanNo = rc.BillNo,
                                    VoucherNo = rc.VoucherNo,
                                    Party = ac.AccName,
                                    LotNo = rt.LotNo,
                                    GreyPcs = (int)rt.IssuePcs,
                                    GreyMtrs = rt.IssueQty,
                                    FinPcs = rt.Pcs,
                                    FinMtrs = rt.Qty,
                                    Rate = rt.Rate,
                                    Id = rc.Id,
                                }).ToList();

                rcptGridControl.DataSource = rcptList;
                KontoUtils.RestoreLayoutGrid(this.GreyRcptAgainstGoDtoFile, rcptGridView);
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
                        cmd.Parameters.Add("@VTypeId", SqlDbType.Int).Value = (int)VoucherTypeEnum.MillIssue;
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
                
                
                

            }
            catch (Exception ex)
            {
                Log.Error(ex, "MIll Issue List Error");
                MessageBoxAdv.Show(this, "Error While Generating List !!", "Exception ", ex.ToString());
            }
        }

       
        public override void DeleteRec()
        {
            base.DeleteRec();

            if (customGridView1.FocusedRowHandle < 0) return;
            try
            {
                var _id = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "Id"));
                var _vid = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "VoucherId"));
                var _deleted = Convert.ToBoolean(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "IsDeleted"));
                if (_deleted)
                {
                    MessageBoxAdv.Show("Record Already in Deleted State", "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBoxAdv.Show(KontoGlobals.DeleteBeforeMsg, "Delete ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                using (var db = new KontoContext())
                {
                    using(var _tran = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var exist = db.ChallanTranses.Any(x => x.MiscId == _id && x.RefVoucherId == _vid && x.IsDeleted == false && x.IsActive == true);
                            if (exist)
                            {
                                MessageBoxAdv.Show("Mill Receipt Exists.. Can not Delete Mill Issue", "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            var model = db.Challans.Find(_id);

                            var stk = db.StockTranses.Where(k => k.MasterRefId == model.RowId).ToList();

                            if (stk != null)
                                db.StockTranses.RemoveRange(stk);
                            var Trans = db.ChallanTranses.Where(x => x.ChallanId == model.Id).ToList();
                            foreach (var ord in Trans)
                            {
                                var delProdOut = db.ProdOuts.Where(p => p.TransId == ord.Id &&
                                 p.RefId == model.Id).ToList();

                                //Challan Trans
                                ord.IsDeleted = true;

                                foreach (var poitem in delProdOut)
                                {
                                    ProdModel pitem = db.Prods.Find(poitem.ProdId);
                                    if (pitem != null && pitem.RefId != model.Id && pitem.TransId != ord.Id && pitem.VoucherId != model.VoucherId)
                                    {
                                        pitem.ProdStatus = "STOCK";
                                    }
                                    else
                                    {
                                        pitem.IsDeleted = true;
                                    }
                                    poitem.IsDeleted = true;
                                }
                            }

                            model.IsDeleted = true;
                            db.SaveChanges();
                            _tran.Commit();
                            customGridView1.DeleteRow(customGridView1.FocusedRowHandle);
                            MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            _tran.Rollback();
                            Log.Error(ex, "Mill Issue delete");
                            MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
                        }
                    }
                  

                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Mill Issue delete");
                MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
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
                var id = this.KontoView.GetRowCellValue(this.KontoView.FocusedRowHandle, "VoucherNo").ToString();

                var frm = new DocPrintParaView(VoucherTypeEnum.MillIssue, "Mill Challan",id,id,"MI", "ChallanId");
                frm.EditKey = Convert.ToInt32(this.KontoView.GetRowCellValue(this.KontoView.FocusedRowHandle, "Id"));
                frm.ShowDialog();


            }

           
          

        }
    }
}
