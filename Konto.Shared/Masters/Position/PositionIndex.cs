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

namespace Konto.Shared.Masters.Position
{
    public partial class PositionIndex : KontoMetroForm
    {
        private List<PositionModel> FilterView = new List<PositionModel>();
        public PositionIndex()
        {
            InitializeComponent();
            tabControlAdv1.TabPages[2].TabVisible = false;
            tabControlAdv1.TabPages[3].TabVisible = false;
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            this.FirstActiveControl = posNameTextBox;
        }
        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                posNameTextBox.Focus();
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as PositionListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new PositionListView();
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

                Log.Error(ex, "Division Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(posNameTextBox.Text) || posNameTextBox.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid Division Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                posNameTextBox.Clear();
                posNameTextBox.Focus();
                return false;
            }

            using (var db = new KontoContext())
            {
                var find = db.Divisions.FirstOrDefault(
                   x => x.DivisionName == posNameTextBox.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "Division Name Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    posNameTextBox.Focus();
                    return false;
                }
            }

            return true;
        }

        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<PositionModel>();
            this.Text = "Position Master [Add New]";
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
        }

        private void DivIndex_Load(object sender, EventArgs e)
        {
            try
            {
                NewRec();

                this.ActiveControl = posNameTextBox;

                if (this.PrimaryKey == 0)
                {
                    toggleSwitch1.Enabled = false;
                }

            }
            catch (Exception ex)
            {

                Log.Error(ex, "Position Load");
                MessageBox.Show(ex.ToString());
            }
        }

        public override void ResetPage()
        {
            base.ResetPage();
            posNameTextBox.Clear();
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
                var model = db.Positions.Find(_key);
                LoadData(model);
            }

        }
        private void LoadData(PositionModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            posNameTextBox.Text = model.PositionName;
            remarkTextBoxExt.Text = model.Remark;
            toggleSwitch1.EditValue = model.IsActive;
            toggleSwitch1.Enabled = true;
            posNameTextBox.Focus();
            this.Text = "Division Master [View/Modify]";
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
            PositionModel _find = new PositionModel();
            if (!string.IsNullOrWhiteSpace(posNameTextBox.Text.Trim()))
                filter.Add(new Filter { PropertyName = "PositionName", Operation = Op.Contains, Value = posNameTextBox.Text.Trim() });


            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });
            //filter.Add(new Filter { PropertyName = "BranchId", Operation = Op.Equals, Value = KontoGlobals.BranchId });

            using (var db = new KontoContext())
            {
                FilterView = db.Positions.Where(ExpressionBuilder.GetExpression<PositionModel>(filter)).ToList();
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
            PositionModel model = new PositionModel();
            using (var db = new KontoContext())
            {
                if (this.PrimaryKey != 0)
                    model = db.Positions.Find(this.PrimaryKey);
                model.PositionName = posNameTextBox.Text.Trim();
                
                //model.BranchId = KontoGlobals.BranchId;

                model.Remark = remarkTextBoxExt.Text.Trim();
                model.IsActive = Convert.ToBoolean(toggleSwitch1.EditValue);

                if (this.PrimaryKey == 0)
                {
                    // model.RowId = Guid.NewGuid();
                    db.Positions.Add(model);
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
                    posNameTextBox.Focus();
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
