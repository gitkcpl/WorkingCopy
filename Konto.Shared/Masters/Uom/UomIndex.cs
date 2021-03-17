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

namespace Konto.Shared.Masters.Uom
{
    public partial class UomIndex : KontoMetroForm
    {
        private List<UomModel> FilterView = new List<UomModel>();
        public UomIndex()
        {
            InitializeComponent();
            tabControlAdv1.TabPages[2].TabVisible = false;
            tabControlAdv1.TabPages[3].TabVisible = false;
            this.Load += UomIndex_Load;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();
            cbp.Add(new ComboBoxPairs("Qty", "Q"));
            cbp.Add(new ComboBoxPairs("Nos.", "N"));
            rateOnComboBox.DisplayMember = "_Key";
            rateOnComboBox.ValueMember = "_Value";
            rateOnComboBox.DataSource = cbp;

            this.FirstActiveControl = unitNameTextBox;
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
                var _list = tabPageAdv2.Controls[0] as UomListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new UomListView();
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

                Log.Error(ex, "Uom Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private void UomIndex_Load(object sender, EventArgs e)
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

                Log.Error(ex, "Uom Load");
                MessageBox.Show(ex.ToString());
            }

        }
        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(unitNameTextBox.Text) || unitNameTextBox.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid Unit Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                unitNameTextBox.Clear();
                unitNameTextBox.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(codeTextBoxExt.Text) || unitNameTextBox.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid Unit Code", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                codeTextBoxExt.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(gstUnitTextBox.Text) || gstUnitTextBox.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid GST Unit Code", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gstUnitTextBox.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(rateOnComboBox.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Rate Apply", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rateOnComboBox.Focus();
                return false;
            }


            using (var db = new KontoContext())
            {
                var find = db.Uoms.FirstOrDefault(
                   x => x.UnitName == unitNameTextBox.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "Unit Name Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    unitNameTextBox.Focus();
                    return false;
                }
            }

            return true;
        }

        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<UomModel>();
            this.Text = "Unit Master [Add New]";
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;

        }
        public override void ResetPage()
        {
            base.ResetPage();
            unitNameTextBox.Clear();
            codeTextBoxExt.Clear();
            gstUnitTextBox.Clear();
            toggleSwitch1.EditValue = true;
            toggleSwitch1.Enabled = false;

        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var model = db.Uoms.Find(_key);
                LoadData(model);
            }

        }
        private void LoadData(UomModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            unitNameTextBox.Text = model.UnitName;
            codeTextBoxExt.Text = model.UnitCode;
            gstUnitTextBox.Text = model.GSTUnit;
            rateOnComboBox.SelectedValue = model.RateOn;
            toggleSwitch1.EditValue = model.IsActive;
            toggleSwitch1.Enabled = true;
            codeTextBoxExt.Focus();
            this.Text = "Unit Master [View/Modify]";
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
            UomModel _find = new UomModel();

            if (!string.IsNullOrWhiteSpace(unitNameTextBox.Text.Trim()))
                filter.Add(new Filter { PropertyName = "UnitName", Operation = Op.Contains, Value = unitNameTextBox.Text.Trim() });

            if (!string.IsNullOrWhiteSpace(codeTextBoxExt.Text.Trim()))
                filter.Add(new Filter { PropertyName = "UnitCode", Operation = Op.StartsWith, Value = codeTextBoxExt.Text.Trim() });

            if (!string.IsNullOrWhiteSpace(gstUnitTextBox.Text.Trim()))
                filter.Add(new Filter { PropertyName = "GSTUnit", Operation = Op.Contains, Value = gstUnitTextBox.Text.Trim() });

            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });


            using (var db = new KontoContext())
            {
                FilterView = db.Uoms.Where(ExpressionBuilder.GetExpression<UomModel>(filter))
                    .OrderBy(x => x.UnitName).ToList();
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
            UomModel model = new UomModel();
            using (var db = new KontoContext())
            {
                if (this.PrimaryKey != 0)
                    model = db.Uoms.Find(this.PrimaryKey);

                model.UnitName = unitNameTextBox.Text.Trim();
                model.UnitCode = codeTextBoxExt.Text.Trim();
                model.GSTUnit = gstUnitTextBox.Text.Trim();
                model.RateOn = rateOnComboBox.SelectedValue.ToString();
                model.IsActive = Convert.ToBoolean(toggleSwitch1.EditValue);

                if (this.PrimaryKey == 0)
                {
                    // model.RowId = Guid.NewGuid();
                    db.Uoms.Add(model);
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
