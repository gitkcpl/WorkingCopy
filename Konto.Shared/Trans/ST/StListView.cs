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
using Konto.Shared.Trans.Common;
using DevExpress.XtraGrid.Views.Grid;
using Konto.Data.Models.Transaction.Dtos;
using GrapeCity.ActiveReports;
using System.IO;
using Syncfusion.Windows.Forms.Tools;
using System.Drawing;

namespace Konto.Shared.Trans.ST
{
    public partial class StListView : ListBaseView
    {
        //private List<OpBillListDto> _modelList = new List<OpBillListDto>();

        public StListView()

        {
            InitializeComponent();
            this.listDateRange1.GetButtonClick += ListDateRange1_GetButtonClick;
            //  this.GridLayoutFileName = KontoFileLayout.Op_Bill_List;

            this.ReportPrint = true;
            listAction1.EditDeleteDisabled(false);
            ewbButton.Click += EwbButton_Click;
        }

        private void EwbButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (customGridView1.FocusedRowHandle < 0) return;
                var row = customGridView1.GetDataRow(customGridView1.FocusedRowHandle);

                var _id = Convert.ToInt32(row["Id"]);
                var _vid = Convert.ToInt32(row["VoucherId"]);

                using (var _context = new KontoContext())
                {
                    var bm = _context.Challans.Find(_id);
                    // var bt = db.ChallanTranses.Where(x => x.ChallanId == _id).ToList();
                    var _list = (from ct in _context.ChallanTranses
                                 join pd in _context.Products on ct.ProductId equals pd.Id 
                                 join um in _context.Uoms on ct.UomId equals  um.Id
                                 orderby ct.Id
                                 where ct.IsActive == true && ct.IsDeleted == false &&
                                 ct.ChallanId == bm.Id    
                                 select new GrnTransDto()
                                 {
                                     Id = ct.Id,
                                     Cess = ct.Cess,
                                     CessPer = ct.CessPer,
                                     Cgst = ct.Cgst,
                                     CgstPer = ct.CgstPer,
                                     ChallanId = ct.ChallanId,
                                     ColorId = ct.ColorId.HasValue ? (int)ct.ColorId : 1,
                                     ColorName = pd.ProductDesc,
                                     Cops = ct.Cops,
                                     DesignId = ct.DesignId.HasValue ? (int)ct.DesignId : 1,
                                     Disc = ct.Disc,
                                     DiscPer = ct.DiscPer,
                                     Freight = ct.Freight,
                                     FreightRate = ct.FreightRate,
                                     GradeId = ct.GradeId.HasValue ? (int)ct.GradeId : 1,
                                     GradeName = pd.HsnCode,
                                     DesignNo = um.UnitCode,
                                     Gross = ct.Gross,
                                     Igst = ct.Igst,
                                     IgstPer = ct.IgstPer,
                                     LotNo = ct.LotNo,
                                     MiscId = ct.MiscId,
                                     OtherAdd = ct.OtherAdd,
                                     OtherLess = ct.OtherLess,
                                     Pcs = ct.Pcs,
                                     ProductId = (int)ct.ProductId,
                                     ProductName = pd.ProductName,
                                     Qty = ct.Qty,
                                     Rate = ct.Rate,
                                     RefId = ct.RefId,
                                     RefVoucherId = ct.RefVoucherId,
                                     Remark = ct.Remark,
                                     Sgst = ct.Sgst,
                                     SgstPer = ct.SgstPer,
                                     Total = ct.Total,
                                     UomId = (int)ct.UomId,
                                     BarcodeNo = pd.BarCode,
                                     IsReceived = ct.IsReceived,
                                     ReceiveDateTime = ct.ReceiveDateTime ?? DateTime.Now
                                 }).ToList();
                    var frm = new EwbChallanView() { RefId = _id, VoucherId = _vid, BModel = bm, TModel = _list };
                    frm.ShowDialog();
                }



            }
            catch (Exception ex)
            {
                Log.Error(ex, "sale eway bill");
                MessageBox.Show(ex.ToString());
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
                        cmd.Parameters.Add("@VTypeId", SqlDbType.Int).Value = (int)VoucherTypeEnum.Stock_Transfer;
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
                Log.Error(ex, "Stock Transfer List Error");
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
                            var _id = Convert.ToInt32(row["Id"]);
                            var _vid = Convert.ToInt32(row["VoucherId"]);
                            var _deleted = Convert.ToBoolean(row["IsDeleted"]);
                            //var _id = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "Id"));
                            //var _vid = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "VoucherId"));
                            //var _deleted = Convert.ToBoolean(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "IsDeleted"));

                            if (_deleted)
                            {
                                MessageBoxAdv.Show("Record Already in Deleted State", "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }


                            var exist = db.BillTrans.Any(x => x.RefId == _id && x.RefVoucherId == _vid && x.IsDeleted == false && x.IsActive == true);
                            if (exist)
                            {
                                MessageBoxAdv.Show("Challan Exist In Bill.. Can not Delete Order", "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            var model = db.Challans.Find(_id);
                            model.IsDeleted = true;

                            var stk = db.StockTranses.Where(k => k.MasterRefId == model.RowId).ToList();

                            if (stk != null)
                            {
                                db.StockTranses.RemoveRange(stk);
                            }
                            var trans = db.ChallanTranses.Where(x => x.ChallanId == model.Id).ToList();
                            foreach (var item in trans)
                            {
                                var delProdOut = db.ProdOuts.Where(p => p.TransId == item.Id &&
                                     p.RefId == item.ChallanId &&
                                     p.VoucherId == model.VoucherId).ToList();
                                foreach (var poitem in delProdOut)
                                {
                                    var pitem = db.Prods.Find(poitem.ProdId);

                                    if (pitem != null)
                                    {
                                        pitem.ProdStatus = "STOCK";
                                    }
                                    poitem.IsDeleted = true;
                                }
                               item.IsDeleted = true;
                            }

                        }

                        customGridView1.DeleteSelectedRows();
                        db.SaveChanges();
                        _tran.Commit();
                        MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "stock Transfer delete");
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
              //  var id = this.KontoView.GetRowCellValue(this.KontoView.FocusedRowHandle, "VoucherNo").ToString();
                var row = customGridView1.GetDataRow(this.customGridView1.FocusedRowHandle);
                var _id = Convert.ToInt32(row["Id"]);
                var _vid = Convert.ToInt32(row["VoucherId"]);
                //var frm = new DocPrintParaView(VoucherTypeEnum.SalesChallan, "Challan Print",id,id, "challan", "ChallanId");
                //frm.EditKey = Convert.ToInt32(this.KontoView.GetRowCellValue(this.KontoView.FocusedRowHandle, "Id"));
                //frm.ShowDialog();

                PageReport rpt = new PageReport();

                rpt.Load(new FileInfo("reg\\doc\\stock_transfer_challan.rdlx"));

                rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);

                doc.Parameters["id"].CurrentValue = _id;
                doc.Parameters["challan"].CurrentValue = "N";
                doc.Parameters["reportid"].CurrentValue = 0;
                var frm = new KontoRepViewer(doc);
                frm.Text = "Transfer Challan";
                frm.WindowState = FormWindowState.Maximized;
                //var _tab = this.Parent.Parent as TabControlAdv;
                //if (_tab == null) return;
                //var pg1 = new TabPageAdv();
                //pg1.Text = "Transfer Challan Print";
                //_tab.TabPages.Add(pg1);
                //_tab.SelectedTab = pg1;
                //frm.TopLevel = false;
                //frm.Parent = pg1;
                //frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                frm.Show();// = true;
            }

        }
        public override void ImportExcel()
        {
            //base.ImportExcel();
            //var _imp = new BillUploadView();
            //_imp.FillTemplate((int)VoucherTypeEnum.SalesChallan);
            //_imp.ShowDialog();
        }
    }
}
