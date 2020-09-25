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

namespace Konto.Shared.Masters.Color
{
    public partial class ColorIndex : KontoMetroForm
    {
        private List<ColorModel> FilterView = new List<ColorModel>();
        public ColorIndex()
        {
            InitializeComponent();
            tabControlAdv1.TabPages[2].TabVisible = false;
            tabControlAdv1.TabPages[3].TabVisible = false;
            this.Load += ColorIndex_Load;
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;
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
                var _list = tabPageAdv2.Controls[0] as ColorListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new ColorListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Color Master [View]";

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

                Log.Error(ex, "Color Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private void ColorIndex_Load(object sender, EventArgs e)
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

            if (string.IsNullOrWhiteSpace(colorNameTextBox.Text) || colorNameTextBox.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid Color Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                colorNameTextBox.Focus();
                return false;
            }
           


            using (var db = new KontoContext())
            {
                var find = db.ColorModels.FirstOrDefault(
                   x => x.ColorName == colorNameTextBox.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "Color Name Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    colorNameTextBox.Focus();
                    return false;
                }
            }

            return true;
        }

        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<ColorModel>();
            this.Text = "Color Master [Add New]";
            this.ActiveControl = codeTextBoxExt;

            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
        }
        public override void ResetPage()
        {
            base.ResetPage();
            colorNameTextBox.Clear();
            codeTextBoxExt.Clear();
            remarkTextBox.Clear();
            colorEdit1.EditValue = DBNull.Value;
            toggleSwitch1.EditValue = true;
            toggleSwitch1.Enabled = false;

        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var model = db.ColorModels.Find(_key);
                LoadData(model);
            }

        }
        private void LoadData(ColorModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            colorNameTextBox.Text = model.ColorName;
            codeTextBoxExt.Text = model.ColorCode;
            remarkTextBox.Text = model.Remark;
            colorEdit1.EditValue = model.RGB;
            toggleSwitch1.EditValue = model.IsActive;
            toggleSwitch1.Enabled = true;
            codeTextBoxExt.Focus();
            this.Text = "Color Master [View/Modify]";

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
            ColorModel _find = new ColorModel();

            if (!string.IsNullOrWhiteSpace(colorNameTextBox.Text.Trim()))
                filter.Add(new Filter { PropertyName = "ColorName", Operation = Op.Contains, Value = colorNameTextBox.Text.Trim() });

            if (!string.IsNullOrWhiteSpace(codeTextBoxExt.Text.Trim()))
                filter.Add(new Filter { PropertyName = "ColorCode", Operation = Op.StartsWith, Value = codeTextBoxExt.Text.Trim() });

        

            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });


            using (var db = new KontoContext())
            {
                FilterView = db.ColorModels.Where(ExpressionBuilder.GetExpression<ColorModel>(filter))
                    .OrderBy(x => x.ColorName).ToList();
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
            ColorModel model = new ColorModel();
            using (var db = new KontoContext())
            {
                if (this.PrimaryKey != 0)
                    model = db.ColorModels.Find(this.PrimaryKey);

                model.ColorName = colorNameTextBox.Text.Trim();
                model.ColorCode = codeTextBoxExt.Text.Trim();
                model.Remark = remarkTextBox.Text.Trim();
                if (colorEdit1.EditValue != DBNull.Value)
                    model.RGB = colorEdit1.EditValue.ToString();
                else
                    model.RGB = DBNull.Value.ToString();

                model.IsActive = Convert.ToBoolean(toggleSwitch1.EditValue);

                if (this.PrimaryKey == 0)
                {
                    // model.RowId = Guid.NewGuid();
                    db.ColorModels.Add(model);
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
