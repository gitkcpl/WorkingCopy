using Konto.App.Shared;
using Konto.Core.Shared;
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

namespace Konto.Shared.Masters.Country
{
    public partial class CountryView : KontoMetroForm
    {
        private List<CountryModel> FilterView = new List<CountryModel>();
        public CountryView()
        {
            InitializeComponent();
            tabControlAdv1.TabPages[2].TabVisible = false;
            tabControlAdv1.TabPages[3].TabVisible = false;
        }

        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(countryNameTextBox.Text) || countryNameTextBox.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid Country Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                countryNameTextBox.Clear();
                countryNameTextBox.Focus();
                return false;
            }
          

            using (var db = new KontoContext())
            {
                var find = db.Countries.FirstOrDefault(
                   x => x.CountryName == countryNameTextBox.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "Country Name Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    countryNameTextBox.Focus();
                    return false;
                }
            }

            return true;
        }

        private void CountryView_Shown(object sender, EventArgs e)
        {
           // this.ActiveControl = countryNameTextBox;
        }

        private void CountryView_Load(object sender, EventArgs e)
        {
            this.ActiveControl = countryNameTextBox;

            if (this.PrimaryKey == 0)
            {
                toggleSwitch1.Enabled = false;
            }
        }

        public override void ResetPage()
        {
            base.ResetPage();
            countryNameTextBox.Clear();
            toggleSwitch1.EditValue = true;
            toggleSwitch1.Enabled = false;

            countryNameTextBox.Focus();
        }

        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var model = db.Countries.Find(_key);
                LoadData(model);
            }

        }

        private void LoadData(CountryModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            countryNameTextBox.Text = model.CountryName;
           
            toggleSwitch1.EditValue = model.IsActive;
            toggleSwitch1.Enabled = true;
            countryNameTextBox.Focus();
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
        public override void FindRec()
        {
            List<Filter> filter = new List<Filter>();
            StateModel _find = new StateModel();
            if (!string.IsNullOrWhiteSpace(countryNameTextBox.Text.Trim()))
                filter.Add(new Filter { PropertyName = "CountryName", Operation = Op.Contains, Value = countryNameTextBox.Text.Trim() });

            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });

            using (var db = new KontoContext())
            {
                FilterView = db.Countries.Where(ExpressionBuilder.GetExpression<CountryModel>(filter)).ToList();
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
            this.FilterView = new List<CountryModel>();
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
                countryNameTextBox.Focus();
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _countrylist = tabPageAdv2.Controls[0] as CountryListView;
                _countrylist.ActiveControl = _countrylist.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _countrylist = new CountryListView();
                _countrylist.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_countrylist);

            }
        }


        public override void SaveDataAsync(bool newmode)
        {
            //base.SaveData();
            bool IsSaved = false;
            if (!ValidateData()) return;
            CountryModel model = new CountryModel();
            using (var db = new KontoContext())
            {
                if (this.PrimaryKey != 0)
                    model = db.Countries.Find(this.PrimaryKey);

                model.CountryName = countryNameTextBox.Text.Trim();
              
                model.IsActive = Convert.ToBoolean(toggleSwitch1.EditValue);

                if (this.PrimaryKey == 0)
                {
                   // model.RowId = Guid.NewGuid();
                    db.Countries.Add(model);
                }
                db.SaveChanges();
                this.FilterView = new List<CountryModel>();
                IsSaved = true;

            }
            if (IsSaved)
            {
                base.SaveDataAsync(newmode);
                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (!this.OpenForLookup)
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
