using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Shared.Masters.State;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Masters.MachineMaster
{
    public partial class MachineIndex : KontoMetroForm
    {
        private List<MachineMasterModel> FilterView = new List<MachineMasterModel>();
        public MachineIndex()
        {
            InitializeComponent();
            okSimpleButton.Click += okSimpleButton_Click;
            tabControlAdv1.TabPages[2].TabVisible = false;
            tabControlAdv1.TabPages[3].TabVisible = false;
            DivIdComboBox.DisplayMember = "DivisionName";
            DivIdComboBox.ValueMember = "Id";
            FillDiv();
        }
        private void FillDiv()
        {
            using (var db = new KontoContext())
            {
                var cnt = db.Divisions.Where(x=>!x.IsDeleted && x.IsActive).OrderBy(x => x.DivisionName).ToList();
                DivIdComboBox.DataSource = cnt;
            }
        }
        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(NameTextBox.Text) || NameTextBox.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid Machine Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                NameTextBox.Clear();
                NameTextBox.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(DivIdComboBox.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Division", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DivIdComboBox.Focus();
                return false;
            }
            
            using (var db = new KontoContext())
            {
                var find = db.MachineMasters.FirstOrDefault(
                   x => x.MachineName == NameTextBox.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "Machine Name Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    NameTextBox.Focus();
                    return false;
                }
            }

            return true;
        }
        private void CityIndex_Load(object sender, EventArgs e)
        {
            try
            {
                NewRec();  
                this.ActiveControl = NameTextBox;
                if (this.PrimaryKey == 0)
                {
                    toggleSwitch1.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "State Load");
                MessageBox.Show(ex.ToString());
            }
        }

        public override void ResetPage()
        {
            base.ResetPage();
            NameTextBox.Clear();
            RemarkTextBoxExt.Clear();
            toggleSwitch1.EditValue = true;
            toggleSwitch1.Enabled = false;
            NameTextBox.Focus();
        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var model = db.MachineMasters.Find(_key);
                LoadData(model);
            }
        }

        private void LoadData(MachineMasterModel model)
        {
            
            this.ResetPage();
            this.PrimaryKey = model.Id;
            NameTextBox.Text = model.MachineName;
            DivIdComboBox.SelectedValue = model.DivId;
            RemarkTextBoxExt.Text = model.Remark;
            toggleSwitch1.EditValue = model.IsActive;
            toggleSwitch1.Enabled = true;
            NameTextBox.Focus();
            this.Text = "Machine Master [View/Modify]";
            createdLabelControl.Text = "Created By: " + model.CreateUser + " [ " + model.CreateDate + " ]";
            modifyLabelControl.Text = "Modified By: " + model.ModifyUser + " [ " + model.ModifyDate ?? string.Empty + " ]";
        }

        public override void FirstRec()
        {
            base.FirstRec();
            LoadData(FilterView[RecordNo]);
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
            MachineMasterModel _find = new MachineMasterModel();
            if (!string.IsNullOrWhiteSpace(NameTextBox.Text.Trim()))
                filter.Add(new Filter { PropertyName = "MachineName", Operation = Op.Contains, Value = NameTextBox.Text.Trim() });

            if ( DivIdComboBox.SelectedIndex >-1)
                filter.Add(new Filter { PropertyName = "DivId", Operation = Op.Equals, Value = Convert.ToInt32(DivIdComboBox.SelectedValue) });

         
            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });

            using (var db = new KontoContext())
            {
                FilterView = db.MachineMasters.Where(ExpressionBuilder.GetExpression<MachineMasterModel>(filter)).ToList();
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

        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<MachineMasterModel>();
            this.Text = "Machine Master [Add New]";
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
        }

        private void okSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveDataAsync(true);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Machine Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private void tabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                NameTextBox.Focus();
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _citylist = tabPageAdv2.Controls[0] as MachineListView;
                _citylist.ActiveControl = _citylist.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _cityListView = new MachineListView();
                _cityListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_cityListView);

            }
        }

        public override void SaveDataAsync(bool newmode)
        {

            bool IsSaved = false;
            if (!ValidateData()) return;
            MachineMasterModel model = new MachineMasterModel();
            using (var db = new KontoContext())
            {
                if (this.PrimaryKey != 0)
                    model = db.MachineMasters.Find(this.PrimaryKey);
                model.MachineName = NameTextBox.Text.Trim();

                model.Remark = RemarkTextBoxExt.Text;
                model.DivId = Convert.ToInt32(DivIdComboBox.SelectedValue);

                model.CompanyID = KontoGlobals.CompanyId;
                model.IsActive = Convert.ToBoolean(toggleSwitch1.EditValue);

                if (this.PrimaryKey == 0)
                {
                    db.MachineMasters.Add(model);
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
                    this.ResetPage();
                else
                {
                    this.Close();
                    this.Dispose();
                }
            }
        }
         
    }
}
