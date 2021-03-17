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

namespace Konto.Shared.Masters.LedgerGroup
{
    public partial class AcGroupIndex : KontoMetroForm
    {
        private List<AcGroupModel> FilterView = new List<AcGroupModel>();
        public AcGroupIndex()
        {
            InitializeComponent();

            this.FirstActiveControl = codeTextBoxExt;

            tabControlAdv1.TabPages[2].TabVisible = false;
            tabControlAdv1.TabPages[3].TabVisible = false;

            this.Load += AcGroupIndex_Load;
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;
            

            List<ComboBoxPairs> motbp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("None", "N"),
                new ComboBoxPairs("Receivable", "R"),
                new ComboBoxPairs("Payable", "P"),

            };
            recPayComboBoxEx.DisplayMember = "_Key";
            recPayComboBoxEx.ValueMember = "_Value";
            recPayComboBoxEx.DataSource = motbp;

            List<ComboBoxPairs> _naturelist = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("ASSETS", "ASSETS"),
                new ComboBoxPairs("LIABILITIES", "LIABILITIES"),
                new ComboBoxPairs("INCOME", "INCOME"),
                new ComboBoxPairs("EXPENSE", "EXPENSE"),
                new ComboBoxPairs("TRADING INCOME", "TRADING INCOME"),
                new ComboBoxPairs("TRADING EXPENSE", "TRADING EXPENSE"),

            };
            natureComboBox.DisplayMember = "_Key";
            natureComboBox.ValueMember = "_Value";
            natureComboBox.DataSource = _naturelist;
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
                var _list = tabPageAdv2.Controls[0] as AcGroupListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new AcGroupListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Ledger Group [View]";

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

                Log.Error(ex, "Ledger Group Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private void AcGroupIndex_Load(object sender, EventArgs e)
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

            if (string.IsNullOrWhiteSpace(nameTextBox.Text) || nameTextBox.Text.Length < 2)
            {
                MessageBoxAdv.Show(this, "Invalid Ledger Group Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nameTextBox.Focus();
                return false;
            }
            else  if(natureComboBox.SelectedIndex < 0)
            {
                MessageBoxAdv.Show(this, "Invalid Group Nature", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                natureComboBox.Focus();
                return false;
            }
            else if (recPayComboBoxEx.SelectedIndex < 0)
            {
                MessageBoxAdv.Show(this, "Invalid Rec/pay", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                recPayComboBoxEx.Focus();
                return false;
            }
            

            using (var db = new KontoContext())
            {
                var find = db.AcGroupModels.FirstOrDefault(
                   x => x.GroupName == nameTextBox.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "Ledger Group Name Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nameTextBox.Focus();
                    return false;
                }
            }

            return true;
        }

        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<AcGroupModel>();
            this.Text = "Ledger Group [Add New]";
            this.ActiveControl = codeTextBoxExt;
            natureComboBox.SelectedIndex = 0;
            recPayComboBoxEx.SelectedIndex = 0;
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
        }
        public override void ResetPage()
        {
            base.ResetPage();
            nameTextBox.Clear();
            codeTextBoxExt.Clear();
            oppNameTextBox.Clear();
            toggleSwitch1.EditValue = true;
            toggleSwitch1.Enabled = false;

        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var model = db.AcGroupModels.Find(_key);
                LoadData(model);
            }

        }
        private void LoadData(AcGroupModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            nameTextBox.Text = model.GroupName;
            codeTextBoxExt.Text = model.GroupCode;
            oppNameTextBox.Text = model.OppSideName;
            natureComboBox.SelectedValue = model.Nature;
            if(model.Extra1!=null)
            recPayComboBoxEx.SelectedValue = model.Extra1;
            toggleSwitch1.EditValue = model.IsActive;
            toggleSwitch1.Enabled = true;
            codeTextBoxExt.Focus();
            this.Text = "Ledger Group [View/Modify]";

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

            if (!string.IsNullOrWhiteSpace(nameTextBox.Text.Trim()))
                filter.Add(new Filter { PropertyName = "GroupName", Operation = Op.Contains, Value = nameTextBox.Text.Trim() });

            

            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });


            using (var db = new KontoContext())
            {
                FilterView = db.AcGroupModels.Where(ExpressionBuilder.GetExpression<AcGroupModel>(filter))
                    .OrderBy(x => x.GroupName).ToList();
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
            AcGroupModel model = new AcGroupModel();
            using (var db = new KontoContext())
            {
                if (this.PrimaryKey != 0)
                    model = db.AcGroupModels.Find(this.PrimaryKey);

                model.GroupName = nameTextBox.Text.Trim();
                model.GroupCode = codeTextBoxExt.Text.Trim();
                model.OppSideName = oppNameTextBox.Text.Trim();
                model.Nature = natureComboBox.SelectedValue.ToString();
                model.Extra1 = recPayComboBoxEx.SelectedValue.ToString();
                model.IsActive = Convert.ToBoolean(toggleSwitch1.EditValue);

                if (this.PrimaryKey == 0)
                {
                    // model.RowId = Guid.NewGuid();
                    db.AcGroupModels.Add(model);
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
