using AutoMapper;
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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Masters.Catalog
{
    public partial class CatalogIndex : KontoMetroForm
    {
        private List<CatalogModel> FilterView = new List<CatalogModel>();
        private BindingList<PImageModel> _trans = new BindingList<PImageModel>();
        private List<PImageModel> _del = new List<PImageModel>();
        public CatalogIndex()
        {
            InitializeComponent();
            
            tabControlAdv1.TabPages[2].TabVisible = false;
            tabControlAdv1.TabPages[3].TabVisible = false;

            this.Load += SizeIndex_Load;
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;
            gridControl1.DataSource = _trans;
        }

        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                codeTextBoxExt.Focus();
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as CatalogListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new CatalogListView();
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

                Log.Error(ex, "Catalogue Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private void SizeIndex_Load(object sender, EventArgs e)
        {
            try
            {
                NewRec();

                this.ActiveControl = codeTextBoxExt;

                if (this.PrimaryKey == 0)
                {
                    toggleSwitch1.Enabled = false;
                }

            }
            catch (Exception ex)
            {

                Log.Error(ex, "Size Load");
                MessageBox.Show(ex.ToString());
            }
        }
        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(catalogNameTextBox.Text) || catalogNameTextBox.Text.Length <=1)
            {
                MessageBoxAdv.Show(this, "Invalid Catalogue Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                catalogNameTextBox.Focus();
                return false;
            }
           


            using (var db = new KontoContext())
            {
                var find = db.SizeModels.FirstOrDefault(
                   x => x.SizeName == catalogNameTextBox.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "Catalogue Name Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    catalogNameTextBox.Focus();
                    return false;
                }
            }

            return true;
        }

        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<CatalogModel>();
            this.Text = "Catalogue Master [Add New]";
            this.ActiveControl = codeTextBoxExt;
            _trans = new BindingList<PImageModel>();
            _del = new List<PImageModel>();
            gridControl1.DataSource = _trans;

            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
        }
        public override void ResetPage()
        {
            base.ResetPage();
            catalogNameTextBox.Clear();
            codeTextBoxExt.Clear();
            remarkTextBox.Clear();
            _trans = new BindingList<PImageModel>();
            _del = new List<PImageModel>();
            gridControl1.DataSource = _trans;
            toggleSwitch1.EditValue = true;
            toggleSwitch1.Enabled = false;

        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var model = db.Catalogs.Find(_key);
                LoadData(model);
            }

        }
        private void LoadData(CatalogModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            catalogNameTextBox.Text = model.CatalogName;
            codeTextBoxExt.Text = model.CatalogNo;
            remarkTextBox.Text = model.Remark;
            
            toggleSwitch1.EditValue = model.IsActive;
            toggleSwitch1.Enabled = true;

            using(var db = new KontoContext())
            {
                _trans = new BindingList<PImageModel>(db.PImagies.Where(x => x.ProductId == model.Id && !x.IsDeleted && x.Category == "C").ToList());
            }
            gridControl1.DataSource = _trans;
            codeTextBoxExt.Focus();
            this.Text = "Catalogue Master [View/Modify]";

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
            PSizeModel _find = new PSizeModel();

            if (!string.IsNullOrWhiteSpace(catalogNameTextBox.Text.Trim()))
                filter.Add(new Filter { PropertyName = "CatalogName", Operation = Op.Contains, Value = catalogNameTextBox.Text.Trim() });

            if (!string.IsNullOrWhiteSpace(codeTextBoxExt.Text.Trim()))
                filter.Add(new Filter { PropertyName = "CatalogNo", Operation = Op.StartsWith, Value = codeTextBoxExt.Text.Trim() });

        

            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });


            using (var db = new KontoContext())
            {
                FilterView = db.Catalogs.Where(ExpressionBuilder.GetExpression<CatalogModel>(filter))
                    .OrderBy(x => x.CatalogName).ToList();
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
            CatalogModel model = new CatalogModel();
            model.CatalogName = catalogNameTextBox.Text.Trim();
            model.CatalogNo = codeTextBoxExt.Text.Trim();
            model.Remark = remarkTextBox.Text.Trim();
            model.IsActive = Convert.ToBoolean(toggleSwitch1.EditValue);
            int id = model.Id;
            using (var db = new KontoContext())
            {
                using (var _tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (this.PrimaryKey == 0)
                        {
                            db.Catalogs.Add(model);
                            db.SaveChanges();
                            id = model.Id;
                        }
                        else
                        {
                           var _model = db.Catalogs.Find(this.PrimaryKey);
                            var config = new MapperConfiguration(cfg =>
                            {
                                cfg.CreateMap<CatalogModel, CatalogModel>().ForMember(x => x.Id, p => p.Ignore()
                                ).ForMember(x => x.RowId, p => p.Ignore());
                            });
                            var mapper = config.CreateMapper();
                            mapper.Map<CatalogModel, CatalogModel>(model, _model);
                            id = _model.Id;
                        }
                        
                        int pimageid = 1;
                        var PImagies = gridControl1.DataSource as BindingList<PImageModel>;
                        foreach (var pi in PImagies)
                        {
                            var findigm = db.PImagies.Where(
                                        x => x.ImagePath == pi.ImagePath && x.Id != pi.Id && !x.IsDeleted).ToList();

                            if (findigm.Count >0)
                            {
                               
                                MessageBox.Show("Entered File Already Exists");
                                this.catalogNameTextBox.Focus();
                                return;
                            }
                            string fpath = pi.ImagePath;

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
                            pi.ImagePath = targetPath;
                            if (pi.Id == 0)
                            {
                                pi.ProductId = id;
                                pi.Category = "C";
                                db.PImagies.Add(pi);
                            }
                            else
                            {
                                var _finpi = db.PImagies.Find(pi.Id);
                                _finpi.ImagePath = pi.ImagePath;
                               
                            }
                            pimageid = pimageid + 1;
                        }

                        // delete entry from image
                        foreach (var pi in _del)
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
                        Log.Error(ex, "catalog save");
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
                    codeTextBoxExt.Focus();
                }
                else
                {
                    this.Close();
                    this.Dispose();
                }
            }
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dlg = new OpenFileDialog();
           dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            //Open the Pop-Up Window to select the file 
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dlg.FileName)) return;
                gridView1.AddNewRow();
                //var row = gridView1.GetRow(gridView1.FocusedRowHandle) as PImageModel;
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle,"ImagePath",dlg.FileName);
               // gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Img", Image.FromFile(dlg.FileName));
                //buttonEdit1.Text = dlg.FileName;
                //pictureEdit1.Image = Image.FromFile(dlg.FileName);
            }
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            view.SetRowCellValue(e.RowHandle, view.Columns["Img"], null);
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
            {
                if (MessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo) !=
                  DialogResult.Yes)
                    return;
                GridView view = sender as GridView;
                var row = view.GetRow(view.FocusedRowHandle) as PImageModel;
                view.DeleteRow(view.FocusedRowHandle);
                _del.Add(row);
            }
        }
    }
}
