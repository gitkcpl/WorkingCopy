using DevExpress.XtraGrid.Views.Grid;
using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Masters.Design
{
    public partial class DesignIndex : KontoMetroForm
    {
        private List<ProductModel> FilterView = new List<ProductModel>();
        private BindingList<PImageModel> _trans = new BindingList<PImageModel>();
        private List<PImageModel> _delImg = new List<PImageModel>();

        public DesignIndex()
        {
            InitializeComponent();
            tabControlAdv1.TabPages[2].TabVisible = false;
            tabControlAdv1.TabPages[3].TabVisible = false;

            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;
            gridControl1.DataSource = _trans;
            repositoryItemButtonEdit1.ButtonClick += RepositoryItemButtonEdit1_ButtonClick;
            gridView1.InitNewRow += GridView1_InitNewRow;
            gridView1.KeyDown += GridView1_KeyDown;
          

            this.MainLayoutFile = KontoFileLayout.Design_Master;

            FillLookup();
        }

        private void MaxOrdSpinEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Return)
            {
                tabbedControlGroup1.SelectedTabPageIndex = 1;
                groupLookup1.Focus();
                e.Handled = true;
            }
        }

        private void GridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
            {
                if (MessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo) !=
                  DialogResult.Yes)
                    return;
                GridView view = sender as GridView;
                var row = view.GetRow(view.FocusedRowHandle) as PImageModel;
                view.DeleteRow(view.FocusedRowHandle);
                _delImg.Add(row);
            }
        }

        private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            view.SetRowCellValue(e.RowHandle, view.Columns["Img"], null);
        }

        private void RepositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            //Open the Pop-Up Window to select the file 
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dlg.FileName)) return;
                gridView1.AddNewRow();
               
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "ImagePath", dlg.FileName);
            
            }
        }

        private void FillLookup()
        {

            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Yes", "Yes"),
                new ComboBoxPairs("No", "No")
            };

            stockReqComboBoxEx.DisplayMember = "_Value";
            stockReqComboBoxEx.ValueMember = "_Key";

          
            stockReqComboBoxEx.DataSource = cbp;
          

            taxTypelookUpEdit.Properties.DisplayMember = "DisplayText";
            taxTypelookUpEdit.Properties.ValueMember = "Id";

            purUnitlookUpEdit.Properties.DisplayMember = "DisplayText";
            purUnitlookUpEdit.Properties.ValueMember = "Id";

            unitLookUpEdit.Properties.DisplayMember = "DisplayText";
            unitLookUpEdit.Properties.ValueMember = "Id";
            using (var db = new KontoContext())
            {
                var model = (from p in db.TaxMasters
                             where !p.IsDeleted && p.IsActive
                             orderby p.TaxName
                             select new BaseLookupDto
                             {
                                 DisplayText = p.TaxName,
                                 Id = p.Id
                             }).ToList();
                taxTypelookUpEdit.Properties.DataSource = model;

                var uom = (from p in db.Uoms
                           where !p.IsDeleted && p.IsActive
                           orderby p.UnitName
                           select new BaseLookupDto
                           {
                               DisplayText = p.UnitName,
                               Id = p.Id
                           }).ToList();


                var itemlkp = (from p in db.Products
                               where !p.IsDeleted && p.IsActive && p.ItemType=="I"
                               orderby p.ProductName
                               select new BaseLookupDto()
                               {
                                   DisplayText = p.ProductName,
                                   Id = p.Id
                               }).ToList();

                var Designlkp = (from p in db.Products
                               where !p.IsDeleted && p.IsActive && p.ItemType == "D"
                               orderby p.ProductName
                               select new BaseLookupDto()
                               {
                                   DisplayText = p.ProductName,
                                   Id = p.Id
                               }).ToList();

                parentLookUpEdit.Properties.DataSource = Designlkp;
                topLookUpEdit.Properties.DataSource = itemlkp;
                bottomLookUpEdit.Properties.DataSource = itemlkp;
                dupLookUpEdit.Properties.DataSource = itemlkp;
                purUnitlookUpEdit.Properties.DataSource = uom;
                unitLookUpEdit.Properties.DataSource = uom;
            }


        }
        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                noTextBoxExt.Focus();
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as DesignListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new DesignListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);

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

                Log.Error(ex, "Design Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private bool ValidateData()
        {


            tabbedControlGroup1.SelectedTabPageIndex = 0;
            if (string.IsNullOrWhiteSpace(nameTextBoxExt.Text) || nameTextBoxExt.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid Design Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nameTextBoxExt.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(noTextBoxExt.Text) || noTextBoxExt.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid Design No.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                noTextBoxExt.Focus();
                return false;
            }

            else if (Convert.ToInt32(pTypeLookup1.SelectedValue) == 0)
            {
                MessageBoxAdv.Show(this, "Invalid Product Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pTypeLookup1.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(hsnTextBoxExt.Text) || hsnTextBoxExt.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid hsn Code", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                hsnTextBoxExt.Focus();
                return false;
            }

            else if (string.IsNullOrWhiteSpace(descTextBoxExt.Text) || descTextBoxExt.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid Design Description", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                descTextBoxExt.Focus();
                return false;
            }
          
            else if (string.IsNullOrEmpty(taxTypelookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Tax Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                taxTypelookUpEdit.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(purUnitlookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Unit", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                purUnitlookUpEdit.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(unitLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid  Unit", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                unitLookUpEdit.Focus();
                return false;
            }
            else if (stockReqComboBoxEx.SelectedIndex==-1)
            {
                MessageBoxAdv.Show(this, "Invalid Stock Required", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                stockReqComboBoxEx.Focus();
                return false;
            }
       
            using (var db = new KontoContext())
            {
                var find = db.Products.FirstOrDefault(
                   x => x.ProductCode == noTextBoxExt.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted && x.ItemType == "D");

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "Design No. Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    noTextBoxExt.Focus();
                    return false;
                }

                var find1 = db.Products.FirstOrDefault(
                   x => x.ProductName == nameTextBoxExt.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted && x.ItemType=="D");

                if (find1 != null)
                {
                    MessageBoxAdv.Show(this, "Design Name Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    noTextBoxExt.Focus();
                    return false;
                }
            }

            return true;
        }

        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<ProductModel>();
            taxTypelookUpEdit.EditValue = 1;
            purUnitlookUpEdit.EditValue = 1;
            unitLookUpEdit.EditValue = 1;
            stockReqComboBoxEx.SelectedIndex = 0;
          
            pTypeLookup1.SelectedValue = 1;
            pTypeLookup1.SetArea();
            
            checkEdit1.Checked = true;
            this.Text = "Design Master [Add New]";
            dateEdit1.EditValue = DateTime.Now;
            _trans = new BindingList<PImageModel>();
            _delImg = new List<PImageModel>();
            gridControl1.DataSource = _trans;
            tabbedControlGroup1.SelectedTabPageIndex = 0;

            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
        }

        private void BranchIndex_Load(object sender, EventArgs e)
        {
            try
            {
               
                NewRec();

                this.ActiveControl = dateEdit1;

                if (this.PrimaryKey == 0)
                {
                    toggleSwitch1.Enabled = false;
                }

            }
            catch (Exception ex)
            {

                Log.Error(ex, "product Load");
                MessageBox.Show(ex.ToString());
            }
        }

        public override void ResetPage()
        {
            base.ResetPage();
            _trans = new BindingList<PImageModel>();
            _delImg = new List<PImageModel>();
            gridControl1.DataSource = _trans;
            
            noTextBoxExt.Clear();
            nameTextBoxExt.Clear();
            descTextBoxExt.Clear();
            hsnTextBoxExt.Clear();
            costSpintEdit.Value = 0;
            purRatespinEdit.Value = 0;
            purDiscspinEdit.Value = 0;
            saleRatespinEdit.Value = 0;
            mrpSpinEdit.Value = 0;
            saleDiscSpinEdit.Value = 0;
            packingSpinEdit.Value = 0;
            laborSpinEdit.Value = 0;
            topLookUpEdit.EditValue = DBNull.Value;
            bottomLookUpEdit.EditValue = DBNull.Value;
            dupLookUpEdit.EditValue = DBNull.Value;
            parentLookUpEdit.EditValue = DBNull.Value;
            accLookup1.SetEmpty();
            catelogLookup1.SetEmpty();
            groupLookup1.SetEmpty();
            subGroupLookup1.SetEmpty();
            brandLookup1.SetEmpty();
            categoryLookup1.SetEmpty();
            sizeLookup1.SetEmpty();
            colorLookup1.SetEmpty(); 

            toggleSwitch1.EditValue = true;
            toggleSwitch1.Enabled = false;
            
        }

        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var model = db.Products.Find(_key);
                LoadData(model);
            }

        }

        private void LoadData(ProductModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            noTextBoxExt.Text = model.BarCode;
            dateEdit1.EditValue = model.DDate;
            nameTextBoxExt.Text = model.ProductName;
            descTextBoxExt.Text = model.ProductDesc;
            noTextBoxExt.Text = model.ProductCode;
            pTypeLookup1.SelectedValue = model.PTypeId;
            pTypeLookup1.SetArea();
            parentLookUpEdit.EditValue = model.ParentItemId;
            hsnTextBoxExt.Text = model.HsnCode;
            taxTypelookUpEdit.EditValue = model.TaxId;
            purUnitlookUpEdit.EditValue = model.PurUomId;
            unitLookUpEdit.EditValue = model.UomId;
            purDiscspinEdit.Value = model.PurDisc;
            saleDiscSpinEdit.Value = model.SaleDisc;
            packingSpinEdit.Value = model.Price1;
            laborSpinEdit.Value = model.Price2;
            stockReqComboBoxEx.SelectedValue = model.StockReq;
            accLookup1.SelectedValue = model.AccId;
            if (model.AccId != null)
                accLookup1.SetAcc(Convert.ToInt32(model.AccId));
            
            catelogLookup1.SelectedValue = model.CatalogId;
            catelogLookup1.SetValue();

            topLookUpEdit.EditValue = model.FabricTopId;
            bottomLookUpEdit.EditValue = model.FabricBottomId;
            dupLookUpEdit.EditValue = model.FabricDupattaId;
            parentLookUpEdit.EditValue = model.ParentItemId;

            checkEdit1.Checked = model.CheckNegative;
          
            costSpintEdit.Value = model.ActualCost;

            groupLookup1.SelectedValue = model.GroupId;
            groupLookup1.SetGroup();
            subGroupLookup1.SelectedValue = model.SubGroupId;
            subGroupLookup1.SetGroup();
            brandLookup1.SelectedValue = model.BrandId;
            brandLookup1.SetGroup();
            categoryLookup1.SelectedValue = model.CategoryId;
            categoryLookup1.SetGroup();
            colorLookup1.SelectedValue = model.ColorId;
            colorLookup1.SetGroup();
            sizeLookup1.SelectedValue = model.SizeId;
            sizeLookup1.SetGroup();
            toggleSwitch1.EditValue = model.IsActive;
            this.Text = "Design Master [View/Modify]";

            toggleSwitch1.Enabled = true;

            PriceModel pm = null;
            using(var db = new KontoContext())
            {
                pm = db.Prices.FirstOrDefault(x => x.ProductId == model.Id);
                _trans = new BindingList<PImageModel>(db.PImagies.Where(x => x.ProductId == model.Id && !x.IsDeleted && x.Category == "D").ToList());
            }
            gridControl1.DataSource = _trans;

            gridControl1.RefreshDataSource();

            if (pm == null) return;
            purRatespinEdit.Value = pm.DealerPrice;
            saleRatespinEdit.Value = pm.SaleRate;
            
            mrpSpinEdit.Value = pm.Mrp;

            createdLabelControl.Text = "Created By: " + model.CreateUser + " [ " + model.CreateDate + " ]";
            modifyLabelControl.Text = "Modified By: " + model.ModifyUser + " [ " + model.ModifyDate ?? string.Empty + " ]";

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
            ProductModel _find = new ProductModel();
            if (!string.IsNullOrWhiteSpace(nameTextBoxExt.Text.Trim()))
               filter.Add(new Filter { PropertyName = "ProductName", Operation = Op.Contains, Value = nameTextBoxExt.Text.Trim() });

            if (!string.IsNullOrWhiteSpace(noTextBoxExt.Text.Trim()))
                filter.Add(new Filter { PropertyName = "ProductCode", Operation = Op.Contains, Value = noTextBoxExt.Text.Trim() });


            filter.Add(new Filter { PropertyName = "ItemType", Operation = Op.Equals, Value = "D" });

            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });
            

            using (var db = new KontoContext())
            {
                FilterView = db.Products.Where(ExpressionBuilder.GetExpression<ProductModel>(filter)).ToList();
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
            ProductModel model = new ProductModel();

            PriceModel pm = new PriceModel();

            using (var db = new KontoContext())
            {
                using(var _tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (this.PrimaryKey != 0)
                        {
                            model = db.Products.Find(this.PrimaryKey);
                            pm = db.Prices.FirstOrDefault(x => x.ProductId == model.Id);
                        }
                        pm.DealerPrice = purRatespinEdit.Value;
                        pm.SaleRate = saleRatespinEdit.Value;
                        pm.Mrp = mrpSpinEdit.Value;
                       
                        model.ProductCode = noTextBoxExt.Text.Trim();
                        model.PTypeId = Convert.ToInt32(pTypeLookup1.SelectedValue);
                        model.DesignDate = Convert.ToInt32(dateEdit1.DateTime.ToString("yyyyMMdd"));
                        model.ProductName = nameTextBoxExt.Text.Trim();
                        model.ProductDesc = descTextBoxExt.Text.Trim();
                        model.IsActive = toggleSwitch1.IsOn;
                        model.HsnCode = hsnTextBoxExt.Text.Trim();
                        model.TaxId = Convert.ToInt32(taxTypelookUpEdit.EditValue);
                        model.PurUomId = Convert.ToInt32(purUnitlookUpEdit.EditValue);
                        model.UomId = Convert.ToInt32(unitLookUpEdit.EditValue);
                        model.ActualCost = costSpintEdit.Value;
                        model.PurDisc = purDiscspinEdit.Value;
                        model.SaleDisc = saleDiscSpinEdit.Value;
                        model.StockReq = stockReqComboBoxEx.SelectedValue.ToString();
                        model.Price1 = packingSpinEdit.Value;
                        model.Price2 = laborSpinEdit.Value;
                        model.CheckNegative = checkEdit1.Checked;
                        if (!string.IsNullOrEmpty(topLookUpEdit.Text))
                            model.FabricTopId = Convert.ToInt32(topLookUpEdit.EditValue);
                        else
                            model.FabricTopId = null;

                        if (!string.IsNullOrEmpty(bottomLookUpEdit.Text))
                            model.FabricBottomId = Convert.ToInt32(bottomLookUpEdit.EditValue);
                        else
                            model.FabricBottomId = null;

                        if (!string.IsNullOrEmpty(dupLookUpEdit.Text))
                            model.FabricDupattaId = Convert.ToInt32(dupLookUpEdit.EditValue);
                        else
                            model.FabricDupattaId = null;

                        if (!string.IsNullOrEmpty(parentLookUpEdit.Text))
                            model.ParentItemId = Convert.ToInt32(parentLookUpEdit.EditValue);
                        else
                            model.ParentItemId = null;

                        model.StyleId = 1;
                        model.ItemType = "D";

                        if (Convert.ToInt32(accLookup1.SelectedValue) == 0)
                            model.AccId = 1;
                        else
                            model.AccId = Convert.ToInt32(accLookup1.SelectedValue);

                        if (Convert.ToInt32(catelogLookup1.SelectedValue) == 0)
                            model.CatalogId = 1;
                        else
                            model.CatalogId = Convert.ToInt32(catelogLookup1.SelectedValue);

                        if (Convert.ToInt32(groupLookup1.SelectedValue) == 0)
                            model.GroupId = 1;
                        else
                            model.GroupId = Convert.ToInt32(groupLookup1.SelectedValue);
                        if (Convert.ToInt32(subGroupLookup1.SelectedValue) == 0)
                            model.SubGroupId = 1;
                        else
                            model.SubGroupId = Convert.ToInt32(subGroupLookup1.SelectedValue);
                        if (Convert.ToInt32(brandLookup1.SelectedValue) == 0)
                            model.BrandId = 1;
                        else
                            model.BrandId = Convert.ToInt32(brandLookup1.SelectedValue);

                        if (Convert.ToInt32(categoryLookup1.SelectedValue) == 0)
                            model.CategoryId = 1;
                        else
                            model.CategoryId = Convert.ToInt32(categoryLookup1.SelectedValue);

                        if (Convert.ToInt32(sizeLookup1.SelectedValue) == 0)
                            model.SizeId = 1;
                        else
                            model.SizeId = Convert.ToInt32(sizeLookup1.SelectedValue);

                        if (Convert.ToInt32(colorLookup1.SelectedValue) == 0)
                            model.ColorId = 1;
                        else
                            model.ColorId = Convert.ToInt32(colorLookup1.SelectedValue);


                        if (this.PrimaryKey == 0)
                        {
                            db.Products.Add(model);
                            db.SaveChanges();
                            pm.ProductId = model.Id;

                            db.Prices.Add(pm);

                            var complist = db.Companies.Where(p => p.IsActive && !p.IsDeleted).ToList();
                            var yearlist = db.FinYears.Where(x => x.IsActive == true && x.IsDeleted == false).ToList();
                            var storelist = db.Stores.Where(x => x.IsActive && !x.IsDeleted).ToList();
                            var branchlist = db.Branches.Where(x => x.IsActive && !x.IsDeleted).ToList();
                            foreach (var comp in complist)
                            {
                                foreach (var yr in yearlist)
                                {
                                    foreach (var branch in branchlist)
                                    {
                                       // foreach (var store in storelist)
                                        //{
                                            StockBalModel _model = new StockBalModel();

                                            _model.ProductId = model.Id;
                                            _model.ItemCode = model.RowId;
                                            _model.BalQty = 0;
                                            _model.CompanyId = comp.Id;
                                            _model.YearId = yr.Id;
                                            _model.BranchId = branch.Id;
                                        _model.GodownId = KontoGlobals.GodownId;
                                            _model.RowId = Guid.NewGuid();
                                            _model.CreateUser = KontoGlobals.UserName;
                                            _model.CreateDate = DateTime.Now;
                                            _model.OpNos = 0;
                                            _model.OpQty = 0;
                                            db.StockBals.Add(_model);
                                       // }
                                    }
                                }
                            }
                        }
                        int pimageid = 1;
                        // product images
                        foreach (PImageModel atc in _trans)
                        {
                            var fin = db.PImagies.FirstOrDefault(x => x.ImagePath == atc.ImagePath && x.Id != atc.Id && !x.IsDeleted);
                            if (fin != null)
                            {
                                MessageBox.Show("Entered Image File Already Exists");
                                return;
                            }
                            string fpath = atc.ImagePath;

                            int lst1 = fpath.LastIndexOf("\\");
                            string xtn1 = fpath.Substring(lst1 + 1);
                            string path = (System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)) + "\\Pimagies\\Catalog\\";
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            string targetPath = path + pimageid + "_" + model.Id + xtn1;
                            if (!File.Exists(targetPath))
                            {
                                System.IO.File.Copy(fpath, targetPath, true);
                            }
                            atc.ImagePath = targetPath;
                            atc.ProductId = model.Id;
                            atc.Category = "D";

                            if (atc.RowId == Guid.Empty)
                            {
                                db.PImagies.Add(atc);
                            }
                            pimageid = pimageid + 1;
                        }

                        // delete entry from image
                        foreach (var pi in _delImg)
                        {
                            if (pi.Id > 0)
                            {
                                var _finpi = db.PImagies.Find(pi.Id);
                                _finpi.IsDeleted = true;

                            }
                        }

                        db.SaveChanges();

                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "product Save");
                        MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());

                    }
                }
               

            }
            if (IsSaved)
            {
                NewRec();
                base.SaveDataAsync(newmode);
                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (!this.OpenForLookup && newmode)
                {
                    this.ResetPage();
                   noTextBoxExt.Focus();
                }
                else
                {
                    this.Close();
                    this.Dispose();
                }
            }
        }

    }
}
