using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Masters.Dtos;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Shared.Masters.Item
{
    public partial class BarcodeView : KontoForm
    {
        public int PurchaseId { get; set; }

        public BarcodeView()
        {
            InitializeComponent();
            this.Load += BarcodeView_Load;
        }

        private void BarcodeView_Load(object sender, EventArgs e)
        {

            if (this.PurchaseId != 0)
            {
                using (var _context = new KontoContext())
                {
                    _context.Database.CommandTimeout = 0;
                    var _model = (from pd in _context.Products
                                  join bal in _context.StockBals on pd.Id equals bal.ProductId
                                  join pr in _context.Prices on pd.Id equals pr.ProductId

                                  join cat in _context.CategroyModels on pd.CategoryId equals cat.Id into cat_join
                                  from cat in cat_join.DefaultIfEmpty()

                                  join grp in _context.PGroups on pd.GroupId equals grp.Id into grp_join
                                  from grp in grp_join.DefaultIfEmpty()

                                  join sub in _context.PSubGroups on pd.SubGroupId equals sub.Id into sub_join
                                  from sub in sub_join.DefaultIfEmpty()

                                  join pt in _context.ProductTypes on pd.PTypeId equals pt.Id
                                  join tx in _context.TaxMasters on pd.TaxId equals tx.Id
                                  join um in _context.Uoms on pd.UomId equals um.Id

                                  join sz in _context.SizeModels on pd.SizeId equals sz.Id into sz_join
                                  from sz1 in sz_join.DefaultIfEmpty()

                                  join ac in _context.Accs on pd.VendorId equals ac.Id into ac_join
                                  from ac in ac_join.DefaultIfEmpty()

                                  join br in _context.Brands on pd.BrandId equals br.Id into br_join
                                  from br in br_join.DefaultIfEmpty()

                                  join cl in _context.ColorModels on pd.ColorId equals cl.Id into cl_join
                                  from cl in cl_join.DefaultIfEmpty()

                                  join bt in _context.BillTrans on pd.Id equals bt.ProductId

                                  where bal.CompanyId == KontoGlobals.CompanyId && bal.BranchId == KontoGlobals.BranchId && bal.YearId == KontoGlobals.YearId
                                  && bt.BillId == this.PurchaseId

                                  select new PosBarcodeListDto
                                  {
                                      Barcode = pd.BarCode,
                                      Brand = br.BrandName,
                                      Category = cat.CatName,
                                      Color = cl.ColorName,
                                      GroupName = grp.GroupName,
                                      Id = pd.Id,
                                      ItemCode = pd.ProductCode,
                                      Mrp = pr.Mrp,
                                      Pcs = (int)pr.IssueQty,
                                      ProductName = pd.ProductName,
                                      SaleRate = pr.SaleRate,
                                      Size = sz1.SizeName,
                                      SubGroup = sub.SubName,
                                      Vendor = ac.AccName
                                  }).ToList();


                    posBarcodeListDtoBindingSource.DataSource = _model;
                    gridControl1.RefreshDataSource();
                }
            }
        }

        private void okSimpleButton_Click(object sender, EventArgs e)
        {
            var db = new KontoContext();
            var _paraList = new List<ReportParaModel>();

            var rws = gridView1.GetSelectedRows();
            string _cond = "1=1";
            int _ReportId = 0;
            var reportid = db.ReportParas.DefaultIfEmpty().Max(k => k == null ? 0 : k.ReportId);
            _ReportId = reportid + 1;

            foreach (var item in rws)
            {
                var model = gridView1.GetRow(item) as PosBarcodeListDto;
                if (model != null)
                {
                    var _md = db.Prices.FirstOrDefault(x => x.ProductId == model.Id);
                    _md.IssueQty = model.Pcs;
                }
                var ModelReport = new ReportParaModel();
                ModelReport.ReportId = _ReportId;
                ModelReport.ParameterName = "item";
                ModelReport.ParameterValue = model.Id;
                _paraList.Add(ModelReport);
            }

            if(_paraList.Count > 0)
                db.ReportParas.AddRange(_paraList);
            
            
            db.SaveChanges();
            
            
            if (this.PurchaseId > 0)
            {
                _cond = _cond + " AND bt.BillId=" + this.PurchaseId;
            }

            var rpt = new StiReport();
            if(comboBoxEdit1.Text =="Format 1")
            {
                rpt.Load("reg\\pos\\barcode.mrt");
            }

              ((StiSqlDatabase)rpt.Dictionary.Databases["con"]).ConnectionString = KontoGlobals.sqlConnectionString.ConnectionString;
            rpt.Compile();
            rpt["condition"] = _cond;
            rpt["reportid"] = _ReportId;
            
            if (_paraList.Count > 0)
                rpt["item"] = "'Y'";

          
            StiOptions.Viewer.ViewerTitle = "Barcode Print";
            rpt.Render(true);
            rpt.ShowWithRibbonGUI();
        }
    }
}
