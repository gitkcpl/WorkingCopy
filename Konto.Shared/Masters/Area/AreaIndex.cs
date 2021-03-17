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

namespace Konto.Shared.Masters.Area
{
    public partial class AreaIndex : KontoMetroForm
    {
        private List<AreaModel> FilterView = new List<AreaModel>();

        public AreaIndex()
        {
            InitializeComponent();
            this.okSimpleButton.Click += okSimpleButton_Click;
            tabControlAdv1.TabPages[2].TabVisible = false;
            tabControlAdv1.TabPages[3].TabVisible = false;

            this.FirstActiveControl = areaNameTextBox;
        }
        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(areaNameTextBox.Text) || areaNameTextBox.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid Area Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                areaNameTextBox.Clear();
                areaNameTextBox.Focus();
                return false;
            }
            if (cityLookup1.SelectedValue==null || string.IsNullOrEmpty(cityLookup1.SelectedText))
            {
                MessageBoxAdv.Show(this, "Invalid City", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cityLookup1.Focus();
                return false;
            }

            using (var db = new KontoContext())
            {
                var find = db.Areas.FirstOrDefault(
                   x => x.AreaName == areaNameTextBox.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "Area Name Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    areaNameTextBox.Focus();
                    return false;
                }
            }

            return true;
        }
        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<AreaModel>();
            this.Text = "Area Master [Add New]";
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
        }
        private void AreaIndex_Load(object sender, EventArgs e)
        {
            try
            {
                NewRec();

                this.ActiveControl = areaNameTextBox;

                if (this.PrimaryKey == 0)
                {
                    toggleSwitch1.Enabled = false;
                }

            }
            catch (Exception ex)
            {

                Log.Error(ex, "area Load");
                MessageBox.Show(ex.ToString());
            }
        }

        public override void ResetPage()
        {
            base.ResetPage();
            areaNameTextBox.Clear();
            toggleSwitch1.EditValue = true;
            toggleSwitch1.Enabled = false;
            cityLookup1.SetEmpty();
        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var model = db.Areas.Find(_key);
                LoadData(model);
            }

        }
        private void LoadData(AreaModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            areaNameTextBox.Text = model.AreaName;
            cityLookup1.SelectedValue = model.CityId;
            cityLookup1.SetCity();
            toggleSwitch1.EditValue = model.IsActive;
            toggleSwitch1.Enabled = true;

            areaNameTextBox.Focus();
            this.Text = "Area Master [View/Modify]";

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
            AreaModel _find = new AreaModel();
            if (!string.IsNullOrWhiteSpace(areaNameTextBox.Text.Trim()))
                filter.Add(new Filter { PropertyName = "AreaName", Operation = Op.Contains, Value = areaNameTextBox.Text.Trim() });

            if (Convert.ToInt32(cityLookup1.SelectedValue) != 0)
                filter.Add(new Filter { PropertyName = "CityId", Operation = Op.Equals, Value = cityLookup1.SelectedValue });


            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });

            using (var db = new KontoContext())
            {
                FilterView = db.Areas.Where(ExpressionBuilder.GetExpression<AreaModel>(filter)).ToList();
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
                areaNameTextBox.Focus();
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as AreaListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new AreaListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);

            }
        }

        public override void SaveDataAsync(bool newmode)
        {

            bool IsSaved = false;
            if (!ValidateData()) return;
           AreaModel model = new AreaModel();
            using (var db = new KontoContext())
            {
                if (this.PrimaryKey != 0)
                    model = db.Areas.Find(this.PrimaryKey);
                model.AreaName = areaNameTextBox.Text.Trim();

                model.CityId = Convert.ToInt32(cityLookup1.SelectedValue);
                model.IsActive = Convert.ToBoolean(toggleSwitch1.EditValue);

                if (this.PrimaryKey == 0)
                {
                    // model.RowId = Guid.NewGuid();
                    db.Areas.Add(model);
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
                    areaNameTextBox.Focus();
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
