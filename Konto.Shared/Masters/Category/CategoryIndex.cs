using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Masters.Category
{
    public partial class CategoryIndex : KontoMetroForm
    {
        private List<PCategroyModel> FilterView = new List<PCategroyModel>();
        public CategoryIndex()
        {
            InitializeComponent();
            tabControlAdv1.TabPages[2].TabVisible = false;
            tabControlAdv1.TabPages[3].TabVisible = false;

            this.Load += CategoryIndex_Load;
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            this.FirstActiveControl = catNameTextBox;
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
                var _list = tabPageAdv2.Controls[0] as CategoryListView;
                _list.ActiveControl = _list.KontoGrid;
                this.Text = "Category Master [View]";
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new CategoryListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Category Master [View]";
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

                Log.Error(ex, "Category Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private void CategoryIndex_Load(object sender, EventArgs e)
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

                Log.Error(ex, "Category Load");
                MessageBox.Show(ex.ToString());
            }
        }
        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(catNameTextBox.Text) || catNameTextBox.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid Category Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                catNameTextBox.Focus();
                return false;
            }
           


            using (var db = new KontoContext())
            {
                var find = db.CategroyModels.FirstOrDefault(
                   x => x.CatName == catNameTextBox.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "Category Name Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    catNameTextBox.Focus();
                    return false;
                }
            }

            return true;
        }

        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<PCategroyModel>();
            this.Text = "Category Master [Add New]";
            this.ActiveControl = codeTextBoxExt;

            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
        }
        public override void ResetPage()
        {
            base.ResetPage();
            catNameTextBox.Clear();
            codeTextBoxExt.Clear();
            remarkTextBox.Clear();
            toggleSwitch1.EditValue = true;
            toggleSwitch1.Enabled = false;

        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var model = db.CategroyModels.Find(_key);
                LoadData(model);
            }

        }
        private void LoadData(PCategroyModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            catNameTextBox.Text = model.CatName;
            codeTextBoxExt.Text = model.CatCode;
            remarkTextBox.Text = model.Remark;
            
            toggleSwitch1.EditValue = model.IsActive;
            toggleSwitch1.Enabled = true;
            codeTextBoxExt.Focus();
            this.Text = "Size Master [View/Modify]";

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
            PCategroyModel _find = new PCategroyModel();

            if (!string.IsNullOrWhiteSpace(catNameTextBox.Text.Trim()))
                filter.Add(new Filter { PropertyName = "CatName", Operation = Op.Contains, Value = catNameTextBox.Text.Trim() });

            if (!string.IsNullOrWhiteSpace(codeTextBoxExt.Text.Trim()))
                filter.Add(new Filter { PropertyName = "CatCode", Operation = Op.StartsWith, Value = codeTextBoxExt.Text.Trim() });

        

            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });


            using (var db = new KontoContext())
            {
                FilterView = db.CategroyModels.Where(ExpressionBuilder.GetExpression<PCategroyModel>(filter))
                    .OrderBy(x => x.CatName).ToList();
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
            PCategroyModel model = new PCategroyModel();
            using (var db = new KontoContext())
            {
                if (this.PrimaryKey != 0)
                    model = db.CategroyModels.Find(this.PrimaryKey);

                model.CatName = catNameTextBox.Text.Trim();
                model.CatCode = codeTextBoxExt.Text.Trim();
                model.Remark = remarkTextBox.Text.Trim();
                
                model.IsActive = Convert.ToBoolean(toggleSwitch1.EditValue);

                if (this.PrimaryKey == 0)
                {
                    // model.RowId = Guid.NewGuid();
                    db.CategroyModels.Add(model);
                }
                db.SaveChanges();

                IsSaved = true;

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
    }
}
