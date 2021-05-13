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

namespace Konto.Shared.Masters.Tax
{
    public partial class TaxIndex : KontoMetroForm
    {
        private List<TaxModel> FilterView = new List<TaxModel>();
        public TaxIndex()
        {
            InitializeComponent();
            tabControlAdv1.TabPages[2].TabVisible = false;
            tabControlAdv1.TabPages[3].TabVisible = false;

            this.Load += CategoryIndex_Load;
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();
            cbp.Add(new ComboBoxPairs("GST", "GST"));
            cbp.Add(new ComboBoxPairs("Nil Rated", "Nil Rated"));
            cbp.Add(new ComboBoxPairs("Non GST", "Non GST"));
            cbp.Add(new ComboBoxPairs("Exempted", "Exempted"));
            cbp.Add(new ComboBoxPairs("Other", "Other"));
            taxTypeComboBox.DisplayMember = "_Key";
            taxTypeComboBox.ValueMember = "_Value";
            taxTypeComboBox.DataSource = cbp;

            List<ComboBoxPairs> cesstype = new List<ComboBoxPairs>();
            cesstype.Add(new ComboBoxPairs("None", "None"));
            cesstype.Add(new ComboBoxPairs("Percentage", "Percentage"));
            cesstype.Add(new ComboBoxPairs("% + Per Thousand", "% + Per Thousand"));
            cesstype.Add(new ComboBoxPairs("% or Fix Rate (Higher One)", "% or Fix Rate (Higher One)"));
            cesstype.Add(new ComboBoxPairs("Fix Rate Per Thousand", "Fix Rate Per Thousand"));
            cesstype.Add(new ComboBoxPairs("Fix Rate Per Tonnes", "Fix Rate Per Tonnes"));
            cessTypekontoComboBoxEx.DisplayMember = "_Key";
            cessTypekontoComboBoxEx.ValueMember = "_Value";
            cessTypekontoComboBoxEx.DataSource = cesstype;

            this.FirstActiveControl = taxNameTextBox;
        }

        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                taxNameTextBox.Focus();
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as TaxListView;
                _list.ActiveControl = _list.KontoGrid;
                this.Text = "Tax Master [View]";
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new TaxListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Tax Master [View]";
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

                Log.Error(ex, "Tax Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private void CategoryIndex_Load(object sender, EventArgs e)
        {
            try
            {
                NewRec();

                this.ActiveControl = taxNameTextBox;

                if (this.PrimaryKey == 0)
                {
                    toggleSwitch1.Enabled = false;
                }

            }
            catch (Exception ex)
            {

                Log.Error(ex, "Tax Load");
                MessageBox.Show(ex.ToString());
            }
        }
        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(taxNameTextBox.Text) || taxNameTextBox.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid Tax Slab", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                taxNameTextBox.Focus();
                return false;
            }
            else if(taxTypeComboBox.SelectedIndex < 0)
            {
                MessageBoxAdv.Show(this, "Invalid Tax Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                taxTypeComboBox.Focus();
                return false;
            }
            else if(cessTypekontoComboBoxEx.SelectedIndex < 0)
            {
                MessageBoxAdv.Show(this, "Invalid Tax cess Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cessTypekontoComboBoxEx.Focus();
                return false;
            }
           


            using (var db = new KontoContext())
            {
                var find = db.TaxMasters.FirstOrDefault(
                   x => x.TaxName == taxNameTextBox.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "Tax Slab Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    taxNameTextBox.Focus();
                    return false;
                }
            }

            return true;
        }

        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<TaxModel>();
            this.Text = "Tax Master [Add New]";
            this.ActiveControl = taxNameTextBox;
            taxTypeComboBox.SelectedIndex = 0;
            cessTypekontoComboBoxEx.SelectedIndex = 0;

            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
        }
        public override void ResetPage()
        {
            base.ResetPage();
            taxNameTextBox.Clear();
            sgstDecimaKontoTextBox.DoubleValue = 0;
            cgstDecimaKontoTextBox.DoubleValue = 0;
            igstDecimalTextBox.DoubleValue = 0;
            cessRateDecimaKontoTextBox.DoubleValue = 0;
            cessPerDecimaKontoTextBox.DoubleValue = 0;
           
            toggleSwitch1.EditValue = true;
            toggleSwitch1.Enabled = false;

        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var model = db.TaxMasters.Find(_key);
                LoadData(model);
            }

        }
        private void LoadData(TaxModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            taxNameTextBox.Text = model.TaxName;
            taxTypeComboBox.SelectedValue = model.TaxType;
            sgstDecimaKontoTextBox.DoubleValue = (double) model.Sgst;
            cgstDecimaKontoTextBox.DoubleValue = (double)model.Cgst;
            igstDecimalTextBox.DoubleValue = (double)model.Igst;
            cessTypekontoComboBoxEx.SelectedValue = model.CessType;
            cessRateDecimaKontoTextBox.DoubleValue = (double)model.CessRate;
            cessPerDecimaKontoTextBox.DoubleValue = (double)model.Cess;
           
            toggleSwitch1.EditValue = model.IsActive;
            toggleSwitch1.Enabled = true;
            taxNameTextBox.Focus();
            this.Text = "Tax Master [View/Modify]";

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
            TaxModel _find = new TaxModel();

            if (!string.IsNullOrWhiteSpace(taxNameTextBox.Text.Trim()))
                filter.Add(new Filter { PropertyName = "TaxName", Operation = Op.Contains, Value = taxNameTextBox.Text.Trim() });

         

            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });


            using (var db = new KontoContext())
            {
                FilterView = db.TaxMasters.Where(ExpressionBuilder.GetExpression<TaxModel>(filter))
                    .OrderBy(x => x.TaxName).ToList();
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
            TaxModel model = new TaxModel();
            using (var db = new KontoContext())
            {
                if (this.PrimaryKey != 0)
                    model = db.TaxMasters.Find(this.PrimaryKey);

                model.TaxName = taxNameTextBox.Text.Trim();
                model.TaxType = taxTypeComboBox.SelectedValue.ToString();
                model.Sgst = (decimal)sgstDecimaKontoTextBox.DoubleValue;
                model.Cgst = (decimal)cgstDecimaKontoTextBox.DoubleValue;
                model.Igst = (decimal)igstDecimalTextBox.DoubleValue;
                model.CessType = cessTypekontoComboBoxEx.SelectedValue.ToString();
                model.Cess = (decimal) cessPerDecimaKontoTextBox.DoubleValue;
                model.CessRate = (decimal)cessRateDecimaKontoTextBox.DoubleValue;
                model.IsActive = Convert.ToBoolean(toggleSwitch1.EditValue);

                if (this.PrimaryKey == 0)
                {
                    // model.RowId = Guid.NewGuid();
                    db.TaxMasters.Add(model);
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
                    taxNameTextBox.Focus();
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
