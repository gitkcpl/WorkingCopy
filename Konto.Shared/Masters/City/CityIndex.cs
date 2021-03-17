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

namespace Konto.Shared.Masters.City
{
    public partial class CityIndex : KontoMetroForm
    {
        private List<CityModel> FilterView = new List<CityModel>();
        public CityIndex()
        {
            InitializeComponent();
            okSimpleButton.Click += okSimpleButton_Click;
            tabControlAdv1.TabPages[2].TabVisible = false;
            tabControlAdv1.TabPages[3].TabVisible = false;
            stateIdComboBox.DisplayMember = "StateName";
            stateIdComboBox.ValueMember = "Id";
            FillState();

            this.FirstActiveControl = cityNameTextBox;
        }
        private void FillState()
        {
            using (var db = new KontoContext())
            {
                var cnt = db.States.Where(x=>!x.IsDeleted && x.IsActive).OrderBy(x => x.StateName).ToList();
                stateIdComboBox.DataSource = cnt;
            }
        }
        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(cityNameTextBox.Text) || cityNameTextBox.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid City Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cityNameTextBox.Clear();
                cityNameTextBox.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(stateIdComboBox.Text))
            {
                MessageBoxAdv.Show(this, "Invalid State", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                stateIdComboBox.Focus();
                return false;
            }
            
            using (var db = new KontoContext())
            {
                var find = db.Cities.FirstOrDefault(
                   x => x.CityName == cityNameTextBox.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "City Name Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cityNameTextBox.Focus();
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
               

              //  FillState();

                this.ActiveControl = cityNameTextBox;

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
            cityNameTextBox.Clear();
            toggleSwitch1.EditValue = true;
            toggleSwitch1.Enabled = false;
            cityNameTextBox.Focus();
        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var model = db.Cities.Find(_key);
                LoadData(model);
            }

        }

        private void LoadData(CityModel model)
        {
            
            this.ResetPage();
            this.PrimaryKey = model.Id;
            cityNameTextBox.Text = model.CityName;
            stateIdComboBox.SelectedValue = model.StateId;
            toggleSwitch1.EditValue = model.IsActive;
            toggleSwitch1.Enabled = true;
            cityNameTextBox.Focus();
            this.Text = "City Master [View/Modify]";
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
            CityModel _find = new CityModel();
            if (!string.IsNullOrWhiteSpace(cityNameTextBox.Text.Trim()))
                filter.Add(new Filter { PropertyName = "CityName", Operation = Op.Contains, Value = cityNameTextBox.Text.Trim() });

            if ( stateIdComboBox.SelectedIndex >-1)
                filter.Add(new Filter { PropertyName = "StateId", Operation = Op.Equals, Value = Convert.ToInt32(stateIdComboBox.SelectedValue) });

         
            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });

            using (var db = new KontoContext())
            {
                FilterView = db.Cities.Where(ExpressionBuilder.GetExpression<CityModel>(filter)).ToList();
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
            this.FilterView = new List<CityModel>();
            this.Text = "City Master [Add New]";
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

                Log.Error(ex, "city Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private void tabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                cityNameTextBox.Focus();
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _citylist = tabPageAdv2.Controls[0] as CityListView;
                _citylist.ActiveControl = _citylist.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _cityListView = new CityListView();
                _cityListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_cityListView);

            }
        }

        public override void SaveDataAsync(bool newmode)
        {

            bool IsSaved = false;
            if (!ValidateData()) return;
            CityModel model = new CityModel();
            using (var db = new KontoContext())
            {
                if (this.PrimaryKey != 0)
                    model = db.Cities.Find(this.PrimaryKey);
                model.CityName = cityNameTextBox.Text.Trim();
              
                model.StateId = Convert.ToInt32(stateIdComboBox.SelectedValue);
                model.IsActive = Convert.ToBoolean(toggleSwitch1.EditValue);

                if (this.PrimaryKey == 0)
                {
                   // model.RowId = Guid.NewGuid();
                    db.Cities.Add(model);
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

        private void countrySimpleButton_Click(object sender, EventArgs e)
        {
            var frm = new StateIndex();
            frm.OpenForLookup = true;
            frm.ShowDialog(this);
            FillState();
            stateIdComboBox.Focus();
        }
    }
}
