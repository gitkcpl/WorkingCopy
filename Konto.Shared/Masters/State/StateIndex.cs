using Konto.Core.Shared.Frms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Serilog;
using Konto.Data;
using Syncfusion.Windows.Forms;
using Konto.Data.Models.Masters;
using Konto.Core.Shared;
using Konto.Core.Shared.Libs;
using Konto.App.Shared;

namespace Konto.Shared.Masters.State
{
    public partial class StateIndex : KontoMetroForm
    {
       private List<StateModel> FilterView = new List<StateModel>();
      
        public StateIndex()
        {
            InitializeComponent();
            tabControlAdv1.TabPages[2].TabVisible = false;
            tabControlAdv1.TabPages[3].TabVisible = false;
            this.okSimpleButton.Click += okSimpleButton_Click;

            this.FirstActiveControl = stateNameTextBox;
        }

       

        private void FillCountry()
        {
            using (var db = new KontoContext())
            {
                var cnt = db.Countries.OrderBy(x => x.CountryName).ToList();
                countryIdComboBox.DataSource = cnt;
            }
        }
        private bool ValidateData()
        {
           
            if (string.IsNullOrWhiteSpace(stateNameTextBox.Text) ||  stateNameTextBox.Text.Length <=1)
            {
                MessageBoxAdv.Show(this,"Invalid State Name","Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                stateNameTextBox.Clear();
                stateNameTextBox.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(countryIdComboBox.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Country", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                countryIdComboBox.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(gstCodeTextBox.Text) || gstCodeTextBox.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid State Code", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gstCodeTextBox.Clear();
                gstCodeTextBox.Focus();
                return false;
            }

            using (var db = new KontoContext())
            {
                var find =db.States.FirstOrDefault(
                   x => x.StateName == stateNameTextBox.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "State Name Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    stateNameTextBox.Focus();
                    return false;
                }
            }

            return true;
        }

        private void StateIndex_Load(object sender, EventArgs e)
        {
            try
            {
                NewRec();
                countryIdComboBox.DisplayMember = "CountryName";
                countryIdComboBox.ValueMember = "Id";

                FillCountry();

                this.ActiveControl = stateNameTextBox;

                if(this.PrimaryKey == 0)
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
            stateNameTextBox.Clear();
            gstCodeTextBox.Clear();
            toggleSwitch1.EditValue = true;
            toggleSwitch1.Enabled = false;
          
         
            stateNameTextBox.Focus();
        }

        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using(var db = new KontoContext())
            {
                var model = db.States.Find(_key);
                LoadData(model);
            }

        }

        private void LoadData(StateModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            stateNameTextBox.Text = model.StateName;
            countryIdComboBox.SelectedValue = model.CountryId;
            gstCodeTextBox.Text = model.GstCode;
            toggleSwitch1.EditValue = model.IsActive;
            toggleSwitch1.Enabled = true;
            stateNameTextBox.Focus();
            this.Text = "State Master [View/Modify]";

            createdLabelControl.Text = "Created By: " + model.CreateUser + " [ " + model.CreateDate + " ]";
            modifyLabelControl.Text = "Modified By: " + model.ModifyUser + " [ " + model.ModifyDate ?? string.Empty + " ]";

        }

        public override void FirstRec()
        {
            base.FirstRec();
            this.RecordNo = 0;
            var model = FilterView[RecordNo];
            LoadData(model);
        }
        public override void NextRec()
        {
            base.NextRec();
            this.RecordNo = this.RecordNo + 1;
            
            if (this.RecordNo >= this.TotalRecord)
                this.RecordNo = this.TotalRecord - 1;

            LoadData(FilterView[this.RecordNo]);

        }
        public override void PrevRec()
        {
            base.PrevRec();
            this.RecordNo = this.RecordNo - 1;
            if (this.RecordNo == -1)
                this.RecordNo = 0;

            LoadData(FilterView[this.RecordNo]);
        }
        public override void LastRec()
        {
            base.LastRec();
            this.RecordNo = this.TotalRecord - 1;

            LoadData(FilterView[this.RecordNo]);
        }
        public override  void FindRec()
        {
            List<Filter> filter = new List<Filter>();
            StateModel _find = new StateModel();
            if(!string.IsNullOrWhiteSpace(stateNameTextBox.Text.Trim()))
                filter.Add(new Filter { PropertyName = "StateName", Operation = Op.Contains, Value = stateNameTextBox.Text.Trim() });

            if(countryIdComboBox.SelectedValue!=null)
                filter.Add(new Filter { PropertyName = "CountryId", Operation = Op.Equals, Value = Convert.ToInt32(countryIdComboBox.SelectedValue) });

            if(!string.IsNullOrWhiteSpace(gstCodeTextBox.Text.Trim()))
                filter.Add(new Filter { PropertyName = "GstCode", Operation = Op.Equals, Value = gstCodeTextBox.Text });

            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });

            using (var db = new KontoContext())
            {
                FilterView = db.States.Where(ExpressionBuilder.GetExpression<StateModel>(filter)).ToList();
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
            base.FindRec();

        }
        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<StateModel>();
            this.Text = "State Master [Add New]";
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

                Log.Error(ex, "State Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }

        }

        private void tabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                stateNameTextBox.Focus();
                return;
            }
            if (tabPageAdv2.Controls.Count > 0) {
                var _statelist = tabPageAdv2.Controls[0] as StateListView;
                _statelist.ActiveControl = _statelist.KontoGrid;
                return; 
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _stateListView = new StateListView(this.Tag);
                _stateListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_stateListView);
               
            }
        }

        public override void SaveDataAsync(bool newmode)
        {
           
            bool IsSaved = false;
            if (!ValidateData()) return;
            StateModel model = new StateModel();
            using (var db = new KontoContext())
            {
                if (this.PrimaryKey != 0)
                    model = db.States.Find(this.PrimaryKey);
                model.StateName = stateNameTextBox.Text.Trim();
                model.GstCode = gstCodeTextBox.Text.Trim();
                model.CountryId = Convert.ToInt32(countryIdComboBox.SelectedValue);
                model.IsActive = Convert.ToBoolean(toggleSwitch1.EditValue);

                if (this.PrimaryKey == 0)
                {
                    //model.RowId = Guid.NewGuid();
                    db.States.Add(model);
                }
                db.SaveChanges();
              
                IsSaved = true;

            }
            if (IsSaved)
            {
                
                base.SaveDataAsync(newmode);
                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (!this.OpenForLookup && newmode)
                {
                    this.ResetPage();
                    NewRec();
                }
                else
                {
                    this.Close();
                    this.Dispose();
                }
            }
        }


        private void countrySimpleButton_Click(object sender, EventArgs e)
        {
            var frm = new Country.CountryView();
            frm.OpenForLookup = true;
            frm.ShowDialog(this);
            FillCountry();
            countryIdComboBox.Focus();
        }
    }
}
