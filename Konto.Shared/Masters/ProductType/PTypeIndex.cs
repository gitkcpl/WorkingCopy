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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Shared.Masters.ProductType
{
    
    public partial class PTypeIndex : KontoMetroForm
    {
        private List<ProductTypeModel> FilterView = new List<ProductTypeModel>();
        public PTypeIndex()
        {
            InitializeComponent();
            tabControlAdv1.TabPages[2].TabVisible = false;
            tabControlAdv1.TabPages[3].TabVisible = false;
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;
            this.Load += PTypeIndex_Load;
        }

        private void PTypeIndex_Load(object sender, EventArgs e)
        {
            try
            {
                NewRec();

                this.ActiveControl = typeNameTextBox;

                if (this.PrimaryKey == 0)
                {
                    toggleSwitch1.Enabled = false;
                }

            }
            catch (Exception ex)
            {

                Log.Error(ex, "product Type Load");
                MessageBox.Show(ex.ToString());
            }
        }

        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                typeNameTextBox.Focus();
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as PTypeListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new PTypeListView();
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

                Log.Error(ex, "Product Type Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(typeNameTextBox.Text) || typeNameTextBox.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid Type Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                typeNameTextBox.Clear();
                typeNameTextBox.Focus();
                return false;
            }

            using (var db = new KontoContext())
            {
                var find = db.ProductTypes.FirstOrDefault(
                   x => x.TypeName == typeNameTextBox.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "Product Type Name Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   typeNameTextBox.Focus();
                    return false;
                }
            }

            return true;
        }

        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<ProductTypeModel>();
            this.Text = "Product Type [Add New]";
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
        }
    
        public override void ResetPage()
        {
            base.ResetPage();
            typeNameTextBox.Clear();
            remarkTextBoxExt.Clear();
            toggleSwitch1.EditValue = true;
            toggleSwitch1.Enabled = false;

        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var model = db.ProductTypes.Find(_key);
                LoadData(model);
            }

        }
        private void LoadData(ProductTypeModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            typeNameTextBox.Text = model.TypeName;
            remarkTextBoxExt.Text = model.Remark;
            toggleSwitch1.EditValue = model.IsActive;
            toggleSwitch1.Enabled = true;
            typeNameTextBox.Focus();
            this.Text = "Product Type [View/Modify]";
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
            ProductTypeModel _find = new ProductTypeModel();
            if (!string.IsNullOrWhiteSpace(typeNameTextBox.Text.Trim()))
                filter.Add(new Filter { PropertyName = "TypeName", Operation = Op.Contains, Value = typeNameTextBox.Text.Trim() });


            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });
            

            using (var db = new KontoContext())
            {
                FilterView = db.ProductTypes.Where(ExpressionBuilder.GetExpression<ProductTypeModel>(filter))
                    .OrderBy(x=>x.TypeName).ToList();
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
            ProductTypeModel model = new ProductTypeModel();
            using (var db = new KontoContext())
            {
                if (this.PrimaryKey != 0)
                    model = db.ProductTypes.Find(this.PrimaryKey);
                model.TypeName= typeNameTextBox.Text.Trim();
                model.Remark = remarkTextBoxExt.Text.Trim();
                model.IsActive = Convert.ToBoolean(toggleSwitch1.EditValue);

                if (this.PrimaryKey == 0)
                {
                    // model.RowId = Guid.NewGuid();
                    db.ProductTypes.Add(model);
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
                    typeNameTextBox.Focus();
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
