using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using GrapeCity.ActiveReports;
using Konto.App.Shared;
using Konto.App.Shared.Para;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Data.Models.Transaction.Dtos;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Konto.Data.Models.Apparel;
using Konto.Core.Shared.Libs;
using AutoMapper;
using Konto.Data.Models.Masters.Dtos;
using Konto.Data.Models.Apparel.Dtos;
using Konto.Data.Models.Transaction;
using Konto.Shared.Masters.Emp;
using System.IO;

namespace Konto.Apparel.BOM
{
    public partial class BOMIndex : KontoMetroForm
    { 
        private List<BOMList> FilterView = new List<BOMList>();
        private List<BomTransDto> PFormula = new List<BomTransDto>();
        private List<BomModel> bomlist = new List<BomModel>();
        private List<BarcodeModel> barcodelist = new List<BarcodeModel>();
        private List<BomOrderDto> bomOrderDtos = new List<BomOrderDto>();
        decimal? Stock = 0;
        TextEdit headerEdit = new TextEdit();
        GridColumn activeCol = null;
        public BOMIndex()
        {
            InitializeComponent();
            FillLookup();

            this.Load += BOMIndex_Load;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;
            EmployeeButtonEdit2.ButtonClick += EmployeeButtonEdit2_ButtonClick;
            gridControl1.ProcessGridKey += GridControl1_ProcessGridKey;
            gridView1.CellValueChanged += GridView1_CellValueChanged;
            voucherLookup1.SelectedValueChanged += VoucherLookup1_SelectedValueChanged;

            this.MainLayoutFile = KontoFileLayout.BOM_Layout;
          //  this.GridLayoutFile = KontoFileLayout.BOM_Layout;
            okSimpleButton.Click += OkSimpleButton_Click;

            headerEdit.Hide();
            headerEdit.Parent = this.gridControl1;
            headerEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            headerEdit.Leave += new EventHandler(headerEdit_Leave);

            QualityLookup.EditValueChanged += QualityLookup_EditValueChanged;
            targetQtyButtonEdit.ButtonClick += TargetQtyButtonEdit_ButtonClick;
            targetQtyButtonEdit.EditValueChanged += TargetQtyButtonEdit_EditValueChanged;
        }

        private void TargetQtyButtonEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetForumla();
        }

        private void TargetQtyButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.PrimaryKey != 0) return;
            try
            {
                if (string.IsNullOrEmpty(ordertypeLookUpEdit1.Text)) return;
                var ordfrm = new BomPendingOrderView();
                if (ordfrm.ShowDialog() != DialogResult.OK) return;

                Int32[] selectedRowHandles = ordfrm.SelectedRows;
                if (selectedRowHandles == null || selectedRowHandles.Count() == 0) return;
                int id = 0;
                var lst = new List<BomOrderDto>();
                foreach (var item in selectedRowHandles)
                {
                    var ord = ordfrm.gridView1.GetRow(item) as PendingOrderDto;
                    var bomt = new BomOrderDto();
                    bomt.OrderTransId = Convert.ToInt32(ord.TransId);
                    bomt.Id = 0;
                    bomt.OrderDate = ord.VoucherDate;
                    bomt.OrderNo = ord.VoucherNo;
                    bomt.RefNo = ord.RefNo;
                    bomt.ProductId = Convert.ToInt32(ord.ProductId);
                    bomt.ProductName = ord.Product;
                    bomt.AccName = ord.Party;
                    
                    bomt.Qty = Convert.ToDecimal(ord.TotalQty);
                    lst.Add(bomt);

                }
                var dup = lst.GroupBy(x => x.ProductId)
                                .Select(g => g.Key);


                if (dup.Count() > 1)
                {
                    MessageBox.Show("Product should be same in all Selected order");
                    return;
                }
                bomOrderDtos = new List<BomOrderDto>();
                bomOrderDtos.AddRange(lst);

                if (lst.Count > 0)
                {
                    var sum = lst.Where(x => x.Balance > 0).Sum(x => x.Balance);
                    targetQtyButtonEdit.Text = sum.ToString();
                    QualityLookup.Properties.SelectedValue = lst[0].ProductId;
                    QualityLookup.Properties.DisplayText = lst[0].ProductName;
                    QualityLookup.EditValue = lst[0].ProductName;
                    QualityLookup.Enabled = false;
                    
                }

                portalOrderDtoBindingSource.DataSource = bomOrderDtos;
                gridControl2.DataSource = portalOrderDtoBindingSource;
                //gridControl2.RefreshDataSource();
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Bom Order Selection");
                MessageBox.Show(ex.ToString());
            }
       
            
           
        }

        private void QualityLookup_EditValueChanged(object sender, EventArgs e)
        {
            GetForumla();
        }

        private void GetForumla()
        {
            try
            {
                if (this.PrimaryKey != 0) return;
                if (string.IsNullOrEmpty(ordertypeLookUpEdit1.Text)) return;

                if (string.IsNullOrEmpty(targetQtyButtonEdit.Text)) return;
                if (Convert.ToInt32(QualityLookup.Properties.SelectedValue) == 0) return;

                var _qty = Convert.ToDecimal(targetQtyButtonEdit.Text);
                var pid = Convert.ToInt32(QualityLookup.Properties.SelectedValue);
                using (var db = new KontoContext())
                {
                    PFormula = (from p in db.PFormulas
                                join pd in db.Products on p.RefProductId equals pd.Id
                                join cl in db.ColorModels on p.ColorId equals cl.Id into j1
                                from j2 in j1.DefaultIfEmpty()
                                where p.ProductId == pid
                                select new BomTransDto
                                {
                                    ProductName = pd.ProductName,
                                    ProductId =(int) p.RefProductId,
                                    BaseQty = ordertypeLookUpEdit1.EditValue.ToString() == "Bulk" ? p.Cut : p.Qty,
                                    Rate = p.Rate,
                                    Amount = p.Total,
                                    UomId =(int) p.UomId,
                                    RequireQty = ordertypeLookUpEdit1.EditValue.ToString() == "Bulk" ? _qty * p.Cut : _qty * p.Qty,
                                    Remark1 = p.Remark,
                                    ColorId = p.ColorId,
                                    ColorName = j2.ColorName
                                }).ToList();

                    foreach (var item in PFormula)
                    {
                        item.Stock = StockEffect.GetStock(item.ProductId);
                        item.ShortQty =  item.Stock- item.RequireQty;
                    }

                    gridControl1.DataSource = PFormula;
                    gridControl1.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "bom Quality Lookup chane");
                MessageBox.Show(ex.ToString());
            }

        }

        private void GridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == null) return;
            if (!(gridView1.GetRow(e.RowHandle) is PFormulaDto er)) return;
            GridCalculation(er, e.Column.FieldName);
        }

        private void GridCalculation(PFormulaDto er, string fieldName)
        {
            //if (fieldName == "Qty" && er.Qty > 0)
            //{
            //    er.ReqQty = er.Qty * Convert.ToDecimal(targetqtyTextBoxExt.Text);
            //    er.Total = er.ReqQty * er.Rate;
            //}
            //if (fieldName == "Remark" && er.Remark == "ExtQty")
            //{
            //    er.ReqQty = er.cut * Convert.ToDecimal(targetqtyTextBoxExt.Text);
            //    er.Total = er.ReqQty * er.Rate;
            //}
            //else
            //{
            //    er.ReqQty = er.Qty * Convert.ToDecimal(targetqtyTextBoxExt.Text);
            //    er.Total = er.ReqQty * er.Rate;
            //}
            gridView1.UpdateCurrentRow();
        }

        private void VoucherLookup1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.PrimaryKey == 0 && Convert.ToInt32(voucherLookup1.SelectedValue) > 0)
            {
                voucherNoTextEdit.Text = DbUtils.NextSerialNo(Convert.ToInt32(voucherLookup1.SelectedValue), 1);
            }
        }
    

        private void GridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (gridView1.FocusedColumn.FieldName == "Remark")
            {

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
                Log.Error(ex, "BOM Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }


        private void SaveDataAsync(bool newmode)
        {
            int ID;
            var bm = new BomModel();
            List<BOMTransModel> btmlist = new List<BOMTransModel>();
            bool IsSaved = false;
            if (!ValidateData()) return;
            BomModel bom = new BomModel();
            using (var db = new KontoContext())
            {

                using (var _tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (this.PrimaryKey != 0)
                             bm = db.Boms.Find(this.PrimaryKey);
                        

                            bm.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
                          
                            bm.IsActive = true;
                            bm.DivisionId = divLookUpEdit.EditValue != null ? Convert.ToInt32(divLookUpEdit.EditValue) : 0;
                            bm.ProductId = QualityLookup.Properties.SelectedValue != null ? Convert.ToInt32(QualityLookup.Properties.SelectedValue) : 0;
                            bm.VoucherId = voucherLookup1.SelectedValue != null ? Convert.ToInt32(voucherLookup1.SelectedValue) : 0;
                            bm.VoucherNo = voucherNoTextEdit.Text != null ? Convert.ToString(voucherNoTextEdit.Text) : "";
                            bm.Remark = remarkTextEdit.Text != string.Empty ? remarkTextEdit.Text : "";
                            bm.IsDeleted = false;
                            bm.IsActive = true;
                            bm.Description = Convert.ToString(ordertypeLookUpEdit1.EditValue);
                        bm.TargetQty = Convert.ToDecimal(targetQtyButtonEdit.EditValue);

                            if (bm.Id == 0) {
                            bm.CompId = KontoGlobals.CompanyId;
                            bm.YearId = KontoGlobals.YearId;
                            bm.BranchId = KontoGlobals.BranchId;

                                db.Boms.Add(bm);
                                db.SaveChanges();

                            }

                        foreach (var item in PFormula)
                        {
                            var btm = new BOMTransModel();

                            if (item.Id > 0)
                                btm = db.BOMTranses.Find(item.Id);

                            btm.BOMId = bm.Id;
                            btm.UomId = item.UomId;
                            btm.ProductId = item.ProductId;
                            btm.ColorId = item.ColorId;
                            btm.BaseQty = item.BaseQty;
                            btm.RequireQty = item.RequireQty;
                            btm.Stock = item.Stock;
                            btm.Remark1 = item.Remark1;
                            btm.Rate = item.Rate;
                            btm.ShortQty = item.ShortQty;
                            btm.Amount = item.Amount;
                            btm.TransType = 1;
                            btm.IsDeleted = false;
                            btm.IsActive = true;

                            if (btm.Id == 0)
                                db.BOMTranses.Add(btm);
                        }

                        foreach (var item in bomOrderDtos)
                        {
                            var btm = new BOMTransModel();

                            if (item.Id > 0)
                                btm = db.BOMTranses.Find(item.Id);

                            btm.ProductId = item.ProductId;
                            btm.UomId = item.EmpId;
                            btm.OrderTransId = item.OrderTransId;
                            btm.AccId = item.AccId;
                            btm.BaseQty = item.Qty;
                            btm.Stock = item.StockQty;
                            btm.ShortQty = item.Balance;
                            btm.Remark1 = item.Remark1;
                            btm.Remark2 = item.Remark2;
                            btm.TransType = 2;
                            btm.IsDeleted = false;
                            btm.IsActive = true;

                            if (btm.Id == 0)
                            {
                                btm.BOMId = bm.Id;
                                db.BOMTranses.Add(btm);
                            }
                        }
                        db.SaveChanges();
                        _tran.Commit();
                        MessageBoxAdv.Show("Record Save Successfully..");
                        this.ResetPage();
                        this.NewRec();

                        //if (item.Balance != 0)
                        //{
                        //    var barcode = new BarcodeModel();


                        //    //  var reportlastid = db.barcodes.OrderByDescending(k => k.ReportId).FirstOrDefault();
                        //    //   var lastbarcodeno = db.barcodes.OrderByDescending(k => k.Id).FirstOrDefault();
                        //    int barcodeno = 1000;
                        //    barcode.ProductId = item.ProductId;
                        //    barcode.AccId = item.AccId;
                        //    barcode.EmpId = item.EmpId;
                        //    barcode.ComboPieces = item.Balance;
                        //    int OrdId = db.OrdTranses.FirstOrDefault(p => p.Id == item.OrdtransID).OrdId;
                        //    barcode.OrderId = OrdId;
                        //    barcode.YearId = KontoGlobals.YearId;
                        //    barcode.CompId = KontoGlobals.CompanyId;
                        //    if (lastbarcodeno != null)
                        //    {
                        //        barcodeno = Convert.ToInt16(lastbarcodeno.BarcodeNo) + 1;
                        //    }
                        //    else
                        //    {
                        //        barcodeno = 1000;
                        //    }
                        //    barcode.ReportId = ID;
                        //    barcode.BarcodeNo = barcodeno.ToString();
                        //    barcode.Stock = "NA";
                        //    barcode.IsActive = true;
                        //    barcode.IsDeleted = false;
                        //    barcode.CreateUser = KontoGlobals.UserName;
                        //    barcode.CreateDate = DateTime.Now;
                        //    barcodelist.Add(barcode);
                        //    //   db.barcodes.Add(barcode);
                        //    db.SaveChanges();
                        //}



                        //PageReport rpt = new PageReport();

                        //rpt.Load(new FileInfo("reg\\doc\\barcode.rdlx"));

                        //rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

                        //GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);

                        //doc.Parameters["reportid"].CurrentValue = ID;
                        //var frm = new KontoRepViewer(doc);
                        //frm.Text = "Print";
                        //var _tab = this.Parent.Parent as TabControlAdv;
                        //if (_tab == null) return;
                        //var pg1 = new TabPageAdv();
                        //pg1.Text = "Barcode Print";
                        //_tab.TabPages.Add(pg1);
                        //_tab.SelectedTab = pg1;
                        //frm.TopLevel = false;
                        //frm.Parent = pg1;
                        //frm.Location = new System.Drawing.Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                        //frm.Show();// = true;

                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "BOM Save");
                        MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
                    }

                    if (this.OpenForLookup)
                    {
                        this.Close();
                        this.Dispose();
                    }
                }
            }
        }

        private void EmployeeButtonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
           var dr = PreOpenLookup();
            if (dr != null)
                OpenEmpLookup(dr.EmpId, dr);
        }

        private BomOrderDto PreOpenLookup()
        {
            var pfordto = new BomOrderDto();
            gridView2.GetRow(gridView2.FocusedRowHandle);
            if (gridView2.GetRow(gridView2.FocusedRowHandle) == null)
            {
                gridView2.AddNewRow();
            }
            var dr = (BomOrderDto)gridView2.GetRow(gridView2.FocusedRowHandle);
            return dr;
        }

        private void OpenEmpLookup(int? Empid, BomOrderDto dr)
        {
            var frm = new EmpLkpWindow();
            frm.SelectedValue = Convert.ToInt32(Empid);
            frm.Tag = MenuId.Emp;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                gridView1.BeginDataUpdate();
                dr.EmpId = frm.SelectedValue;
                dr.EmployeeName = frm.SelectedTex;

                var model = frm.SelectedItem as BomOrderDto;
                gridView1.FocusedColumn = gridView1.GetNearestCanFocusedColumn(gridView2.FocusedColumn);
            }
        }
        
        private void LoadData(BomModel model, KontoContext _context = null)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            divLookUpEdit.EditValue = model.DivisionId;

            voucherLookup1.SelectedValue = model.VoucherId;

            Remark.Text = model.Remark;
            targetQtyButtonEdit.Text = Convert.ToString(model.TargetQty);
            ordertypeLookUpEdit1.EditValue = model.Description;
            voucherDateEdit.EditValue = KontoUtils.IToD(model.VoucherDate);
            voucherNoTextEdit.Text = model.VoucherNo;
            QualityLookup.Properties.SelectedValue = model.ProductId;
            //QualityLookup.Properties.DisplayText =

            using (var _db = new KontoContext())
            {
                var prd = _db.Products.Find(model.ProductId);
                if(prd!= null)
                {
                    QualityLookup.EditValue = prd.ProductName;
                    QualityLookup.Properties.DisplayText = prd.ProductName;
                }

                PFormula = (from b in _db.Boms
                            join bt in _db.BOMTranses on b.Id equals bt.BOMId
                            join q in _db.Products on b.ProductId equals q.Id
                            join cate in _db.CategroyModels on bt.UomId equals cate.Id into j1
                            from s1 in j1.DefaultIfEmpty()
                            join col in _db.ColorModels on bt.ColorId equals col.Id into j2
                            from s2 in j2.DefaultIfEmpty()
                            where b.Id == model.Id && bt.TransType == 1
                            orderby b.ProductId
                            select new BomTransDto
                            {
                                Amount=bt.Amount,
                                BaseQty = bt.BaseQty,
                                BOMId = bt.BOMId,Id = bt.Id,OrderTransId = bt.OrderTransId,
                                RefQty = bt.RefQty,RequireQty = bt.RequireQty,ShortQty= bt.ShortQty,
                                Stock = bt.Stock,UomId= bt.UomId,
                                ProductId = bt.ProductId,
                                
                                ProductName = q.ProductName,
                                ColorId = bt.ColorId,
                                ColorName = s2.ColorName,
                                
                                Rate = bt.Rate
                            }).ToList();

                gridControl1.DataSource = PFormula;


                bomOrderDtos = (from b in _db.Boms
                              join bt in _db.BOMTranses on b.Id equals bt.BOMId
                              join q in _db.Products on bt.ProductId equals q.Id
                             
                              join emp in _db.Emps on bt.UomId equals emp.Id into e1
                              from e2 in e1.DefaultIfEmpty()

                              join ot in _db.OrdTranses on bt.OrderTransId equals ot.Id into o1
                              from o2 in o1.DefaultIfEmpty()
                              join o in _db.Ords on o2.OrdId equals o.Id
                                join a in _db.Accs on o.AccId equals a.Id into a1
                                from a2 in a1.DefaultIfEmpty()

                                where b.Id == model.Id 
                              orderby b.ProductId
                              select new BomOrderDto
                              {
                                  Id = bt.Id,
                                  ProductId = bt.ProductId,
                                  ProductName = q.ProductName,
                                  OrderDate = o.VoucherDate,
                                  OrderNo = o.RefNo,
                                  EmpId = bt.UomId,
                                  EmployeeName = e2.EmpName,
                                  Qty = bt.BaseQty,
                                  AccId = bt.AccId,
                                  AccName = a2.AccName,
                                  RefNo = o.RefNo,
                                  Balance = bt.RequireQty,
                                  StockQty = bt.Stock,
                                  Remark1 = bt.Remark1,
                                  Remark2 = bt.Remark2,
                                  BomId = b.Id,OrderTransId= bt.OrderTransId
                                  
                              }).ToList();

                gridControl2.DataSource = bomOrderDtos;
            }
        }
        #region Grid 
        #region Event
        private void BOMIndex_Load(object sender, EventArgs e)
        {
            try
            {
                //if (StoreIssuePara.Issue_By_Barcode)
                //{
                //    this.gridView1.OptionsBehavior.ReadOnly = true;
                //}
                //else
                //{
                //    this.gridView1.OptionsBehavior.ReadOnly = false;
                //}
            }
            catch (Exception ex)
            {
                Log.Error(ex, "BOM Load");
                MessageBox.Show(ex.ToString());
            }
        }
        void headerEdit_Leave(object sender, EventArgs e)
        {
            activeCol.Caption = headerEdit.Text;
            headerEdit.Hide();
        }
        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (tabControlAdv1.SelectedIndex == 0)
                {
                    ordertypeLookUpEdit1.Focus();
                    return;
                }
                if (tabPageAdv2.Controls.Count > 0)
                {
                    var _list = tabPageAdv2.Controls[0] as BOMList;
                    _list.ActiveControl = _list.KontoGrid;
                    return;
                }
                if (tabControlAdv1.SelectedIndex == 1)
                {
                    var _ListView = new BOMList();
                    _ListView.Dock = DockStyle.Fill;
                    tabPageAdv2.Controls.Add(_ListView);
                    this.Text = "Bom List [View]";
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private void FillLookup()
        {
            ordertypeLookUpEdit1.Properties.DisplayMember = "_Key";
            ordertypeLookUpEdit1.Properties.ValueMember = "_Value";
            List<ComboBoxPairs> fabriclist = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("NA", "NA"),
                new ComboBoxPairs("Not Processed", "Not Processed"),
                new ComboBoxPairs("Issue To Cutting", "Issue To Cutting"),
                new ComboBoxPairs("Cut & Kept", "Cut & Kept"),
                new ComboBoxPairs("No Fabrics", "No Fabrics"),
                new ComboBoxPairs("NO Needs Cut", "NO Needs Cut"),
                new ComboBoxPairs("Other", "Other")
            };
            List<ComboBoxPairs> cuttinglist = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("NA", "NA"),
                new ComboBoxPairs("Done", "Done"),
                new ComboBoxPairs("In Process", "In Process"),
                new ComboBoxPairs("No Fabric", "No Fabric"),
                new ComboBoxPairs("Other", "Other")
            };
            List<ComboBoxPairs> cmb = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Bulk", "Bulk"),
                new ComboBoxPairs("Retail", "Retail")
            };
            List<ComboBoxPairs> issuetypelist = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("ReqQty", "ReqQty"),
                new ComboBoxPairs("RefQty", "RefQty")
            };

            IssuetypeItemLookUpEdit.DataSource = issuetypelist;
            Remark1ItemLookUpEdit.DataSource = cuttinglist;
            Remark2ItemLookUpEdit.DataSource = fabriclist;
            ordertypeLookUpEdit1.Properties.DataSource = cmb;
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
        public override void ResetPage()
        {
            this.UpdateMessage(string.Empty);

            if (this.Modify_Permission)
                okSimpleButton.Enabled = true;

            QualityLookup.EditValue = string.Empty;
            targetQtyButtonEdit.EditValue = 0;
            divLookUpEdit.EditValue = 1;
            voucherDateEdit.DateTime = DateTime.Now;
            voucherNoTextEdit.Text = string.Empty;
            PFormula = new List<BomTransDto>();
            bomOrderDtos = new List<BomOrderDto>();
            gridControl1.DataSource = PFormula;
            gridControl2.DataSource = bomOrderDtos;
            gridControl1.Refresh();
            gridControl2.Refresh();
        }
        public override void NewRec()
        {
            this.Text = "BOM [Add New]";

            voucherDateEdit.EditValue = DateTime.Now;

            if (!BillPara.Ask_For_Voucher_Selection)
                voucherLookup1.SetDefault();
            else
                voucherLookup1.SetEmpty();

            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
            this.SITbindingSource.DataSource = new List<GrnTransDto>();
              targetQtyButtonEdit.Text = "0";
            divLookUpEdit.EditValue = 1;
            QualityLookup.EditValue = string.Empty;
            PFormula = new List<BomTransDto>();
            bomOrderDtos = new List<BomOrderDto>();
            gridControl1.DataSource = PFormula;
            gridControl2.DataSource = bomOrderDtos;
            gridControl1.Refresh();
            gridControl2.Refresh();
        }
        public override void FirstRec()
        {
            base.FirstRec();
            var model = FilterView[RecordNo];
            //LoadData(model);
        }
        public override void NextRec()
        {
            base.NextRec();
            //LoadData(FilterView[this.RecordNo]);
        }
        public override void PrevRec()
        {
            base.PrevRec();
            //LoadData(FilterView[this.RecordNo]);
        }
        public override void LastRec()
        {
            base.LastRec();
            //LoadData(FilterView[this.RecordNo]);
        }

        public override void FindRec()
        {
            List<Filter> filter = new List<Filter>();

            if (Convert.ToInt32(voucherLookup1.SelectedValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "VoucherId", Operation = Op.Equals, Value = Convert.ToInt32(voucherLookup1.SelectedValue) });
            }
            if (!string.IsNullOrEmpty(voucherNoTextEdit.Text.Trim()))
            {
                filter.Add(new Filter { PropertyName = "VoucherNo", Operation = Op.Equals, Value = voucherNoTextEdit.Text.Trim() });
            }
            if (Convert.ToInt32(QualityLookup.EditValue) > 0)
            {
                filter.Add(new Filter { PropertyName = "ProductId", Operation = Op.Equals, Value = Convert.ToInt32(QualityLookup.EditValue) });
            }

            filter.Add(new Filter { PropertyName = "CompId", Operation = Op.Equals, Value = KontoGlobals.CompanyId });
            filter.Add(new Filter { PropertyName = "YearId", Operation = Op.Equals, Value = KontoGlobals.YearId });
            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BomModel, BOMList>();
            });

            using (var db = new KontoContext())
            {

            }

        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;
            using (var db = new KontoContext())
            {
               var bom = db.Boms.Find(_key);
                LoadData(bom);
            }
        }
        private bool ValidateData()
        {
            //var trans = SITbindingSource.DataSource as List<PortalOrderDto>;

            //var dt = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));
            //if (dt > KontoGlobals.ToDate || dt < KontoGlobals.FromDate)
            //{
            //    MessageBoxAdv.Show(this, "Challan date out of financial range", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    voucherDateEdit.Focus();
            //    return false;
            //}
            //else if (gridView1.RowCount == 1)
            //{
            //    MessageBoxAdv.Show(this, "At Least One Product Should be Entered", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    gridView1.Focus();
            //    return false;
            //}
            return true;
        }

        #endregion
    }
}

#endregion