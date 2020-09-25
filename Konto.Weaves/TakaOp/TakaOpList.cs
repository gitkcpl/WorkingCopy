using Aspose.Cells;
using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Weaves.TakaOp
{
    public partial class TakaOpList : ListBaseView
    {
        private List<BeamProdDto> _modelList = new List<BeamProdDto>();
        public TakaOpList()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.TakaOp_List;
            OpUploadSimpleButton.Click += OpUploadSimpleButton_Click;
        }

        private void OpUploadSimpleButton_Click(object sender, EventArgs e)
        {
            List<ProdModel> ExcelProdList = new List<ProdModel>();

            KontoContext _db = new KontoContext();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Open Taka Op Excel File";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.ShowDialog();
            string filePath = openFileDialog1.FileName;

            if (string.IsNullOrEmpty(filePath)) return;

            Workbook workbook = new Workbook(filePath);
            Worksheet worksheet = workbook.Worksheets[0];
            var exp = new ExportTableOptions();
            exp.ExportAsString = true;
            exp.ExportColumnName = true;

            var dt = worksheet.Cells.ExportDataTable(0, 0, worksheet.Cells.Rows.Count,
                     worksheet.Cells.MaxDataColumn + 1, exp);

            var pid = _db.Prods.FirstOrDefault();
            int ProdId = 0;
            if (pid != null)
            {
                ProdId = _db.Prods.Max(k => k.Id);
            }
            else
                ProdId = 0;

            int vouchrId = _db.Vouchers.FirstOrDefault(k => k.VTypeId == (int)VoucherTypeEnum.OpTaka).Id;

            var maxGroid = _db.Prods.Where(k => k.VoucherId == vouchrId).Max(k => k.SubGradeId);
            int subGrdId = maxGroid != null ? (int)maxGroid + 1 : 1;

            using (var _tran = _db.Database.BeginTransaction())
            {
                try
                {
                    ProdModel product;
                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        product = new ProdModel();
                        product.VoucherDate = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
                        product.VoucherNo = dr["TakaNo"].ToString();

                        if (product.VoucherNo != "" && product.VoucherNo != null)
                        {
                            string ProductName = dr["QualityName"].ToString();
                            if (ProductName != null)
                            {
                                var pname = _db.Products.FirstOrDefault(k => k.ProductName == ProductName && k.ItemType == "I");
                                product.ProductId = pname.Id;

                                var DrawerName = dr["DesignNo"].ToString();
                                var batch = _db.Products.FirstOrDefault(k => k.ProductCode == DrawerName && k.ItemType == "D");
                                if (batch != null)
                                    product.PlyProductId = batch.Id;

                                var Division = dr["Division"].ToString();
                                var div = _db.Products.FirstOrDefault(k => k.ProductCode == Division);
                                if (div != null)
                                    product.DivId = div.Id;

                                var Color = dr["Color"].ToString();
                                var Colr = _db.ColorModels.FirstOrDefault(k => k.ColorName == Color);
                                if (Colr != null)
                                    product.ColorId = Colr.Id;

                                var machine = dr["MachineNo"].ToString();
                                var mac = _db.MachineMasters.FirstOrDefault(k => k.MachineName == machine);
                                if (mac != null)
                                    product.MacId = mac.Id;

                                if (dr["TakaMtr"] != null)
                                    product.NetWt = Convert.ToDecimal(dr["TakaMtr"].ToString());

                                if (dr["TakaWeight"] != null)
                                    product.TareWt = Convert.ToDecimal(dr["TakaWeight"].ToString());

                                product.Remark = dr["BeamNo"].ToString();
                                ProdId = ProdId + 1;

                                product.Id = ProdId;
                                product.VoucherId = vouchrId;
                                product.SubGradeId = subGrdId;
                                product.CompId = KontoGlobals.CompanyId;
                                product.YearId = KontoGlobals.YearId;
                                product.BranchId = KontoGlobals.BranchId;
                                product.VTypeId = (int)VoucherTypeEnum.OpTaka;
                                ExcelProdList.Add(product);

                            }
                            else
                            {
                                ExcelProdList = new List<ProdModel>();

                                MessageBox.Show("Product Name '" + ProductName + "' not found!!!!");
                                return;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (ExcelProdList.Count > 0)
                    {
                        _db.Prods.AddRange(ExcelProdList);
                        _db.SaveChanges();
                    }
                    var StockList = new List<StockTransModel>();

                    StockTransModel strans;
                    // subGrdId = (subGrdId + 1);

                    var oplist = _db.Prods.Where(k => k.VoucherId == vouchrId && k.SubGradeId == subGrdId).ToList();
                    var st = _db.StockTranses.FirstOrDefault();
                    int sid = 0;
                    if (st != null)
                        sid = _db.StockTranses.Max(k => k.Id);

                    foreach (var item in oplist)
                    {
                        strans = new StockTransModel();
                        sid = sid + 1;
                        strans.Id = sid;
                        strans.KeyFldValue = item.Id;
                        strans.ItemId = (int)item.ProductId;
                        strans.VoucherId = (int)item.VoucherId;
                        strans.CompanyId = (short)KontoGlobals.CompanyId;
                        strans.BranchId = KontoGlobals.BranchId;
                        strans.YearId = KontoGlobals.YearId;
                        strans.CreateUser = KontoGlobals.UserName;
                        strans.CreateDate = DateTime.Now;
                        strans.RcptNos = 1;
                        strans.RcptQty = (decimal)item.NetWt;


                        strans.RefId = item.RowId;
                        strans.MasterRefId = item.RowId;
                        strans.VoucherDate = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
                        strans.TransDate = DateTime.Now; ;
                        strans.BillNo = item.VoucherNo;
                        strans.VoucherNo = item.VoucherNo;

                        strans.TableName = "TakaOp";
                        strans.KeyFldValue = item.Id;

                        strans.RcptNos = 1;
                        strans.RcptQty = item.NetWt;
                        strans.Qty = item.NetWt;
                        strans.Pcs = 1;

                        strans.TransDateTime = DateTime.Now;
                        strans.CreateDate = DateTime.Now;
                        strans.CreateUser = KontoGlobals.UserName;

                        _db.StockTranses.Add(strans);
                        _db.SaveChanges();

                    }
                
                    _tran.Commit();
                    MessageBox.Show("upload & saved successfully");
                }
                catch (Exception ex)
                {
                    ExcelProdList = new List<ProdModel>();
                    _tran.Rollback();
                    Log.Error(ex, "Taka OpListViewModel Upload product List.");
                }
            }

        }

        public override void RefreshGrid()
        {
            base.RefreshGrid();

            using (var _db = new KontoContext())
            {
                int vtypeId = (int)VoucherTypeEnum.OpTaka;
                _modelList = (from pd in _db.Prods
                              join v in _db.Vouchers on pd.VoucherId equals v.Id into vou_join
                              from vou in vou_join.DefaultIfEmpty()
                              join p in _db.Products on pd.ProductId equals p.Id into pro_join
                              from pro in pro_join.DefaultIfEmpty()
                              join c in _db.ColorModels on pd.ColorId equals c.Id into Color_join
                              from col in Color_join.DefaultIfEmpty()
                              join d in _db.Products on pd.PlyProductId equals d.Id into Design_join
                              from d in Design_join.DefaultIfEmpty()
                              join div in _db.Divisions on pd.DivId equals div.Id into Div_join
                              from div in Div_join.DefaultIfEmpty()
                              join m in _db.MachineMasters on pd.MacId equals m.Id into mac_join
                              from m1 in mac_join.DefaultIfEmpty()
                              where vou.VTypeId == vtypeId
                              && pd.IsActive
                              && !pd.IsDeleted
                              && (pd.VoucherDate >= KontoGlobals.FromDate && pd.VoucherDate <= KontoGlobals.ToDate)
                              select new BeamProdDto()
                              {
                                  Id = pd.Id,
                                  BoxProductId = pd.BoxProductId,
                                  BoxRate = pd.BoxRate,
                                  ColorId = pd.ColorId,
                                  CompId = pd.CompId,
                                  Cops = pd.Cops,
                                  CopsRate = pd.CopsRate,
                                  CopsWt = pd.CopsWt,
                                  CurrQty = pd.CurrQty,
                                  ProductName = pro.ProductName,
                                  DivId = pd.DivId,
                                  DesignNo = d.ProductName,
                                  FinQty = pd.FinQty,
                                  GradeId = pd.GradeId,
                                  GrossWt = pd.GrossWt,
                                  IsClose = pd.IsClose,
                                  IssueRefId = pd.IssueRefId,
                                  IssueRefVoucherId = pd.IssueRefVoucherId,
                                  LoadingDate = pd.LoadingDate,
                                  MachineName = m1.MachineName,
                                  MacId = pd.MacId,
                                  NetWt = pd.NetWt,
                                  PackId = pd.PackId,
                                  PlyProductId = pd.PlyProductId,
                                  Pallet = pd.Pallet,
                                  Ply = pd.Ply,
                                  ProdStatus = pd.ProdStatus,
                                  ProductId = pd.ProductId,
                                  RefId = pd.RefId,
                                  Remark = pd.Remark,
                                  SrNo = pd.SrNo,
                                  SubGradeId = pd.SubGradeId,
                                  TareWt = pd.TareWt,
                                  Tops = pd.Tops,
                                  TransId = pd.TransId,
                                  TwistType = pd.TwistType,
                                  VoucherDate = pd.VoucherDate,
                                  VoucherId = pd.VoucherId,
                                  VoucherNo = pd.VoucherNo,
                                  ColorName = col.ColorName,
                                  YearId = pd.YearId,
                                  IsActive = pd.IsActive,
                                  IsDeleted = pd.IsDeleted
                              }
                ).ToList();
            }

            customGridControl1.DataSource = _modelList;
            if (string.IsNullOrEmpty(this.GridLayoutFileName) || this.KontoView == null) return;

            KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, this.KontoView);

            this.ActiveControl = customGridControl1;
            if (_modelList.Count == 0)
                listAction1.EditDeleteDisabled(false);
            else
                listAction1.EditDeleteDisabled(true);
        }

        public override void DeleteRec()
        {
            base.DeleteRec();

            if (customGridView1.FocusedRowHandle < 0) return;
            try
            {
                var _id = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "Id"));
                var _Voucherid = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "VoucherId"));

                if (MessageBoxAdv.Show(KontoGlobals.DeleteBeforeMsg, "Delete ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                using (var db = new KontoContext())
                {
                    var Model = db.Prods.FirstOrDefault(k => k.VoucherId == _Voucherid && k.Id == _id);
                    Model.IsDeleted = true;

                    //sotock effect
                    var stk = db.StockTranses.FirstOrDefault(k => k.MasterRefId == Model.RowId);
                    if (stk != null)
                        db.StockTranses.Remove(stk);

                    db.SaveChanges();
                    customGridView1.DeleteRow(customGridView1.FocusedRowHandle);
                    MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Taka Opening delete");
                MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
            }
        }
    }
}