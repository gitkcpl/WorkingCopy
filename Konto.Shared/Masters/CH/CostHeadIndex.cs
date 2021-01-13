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

namespace Konto.Shared.Masters.CH
{
    public partial class CostHeadIndex : KontoMetroForm
    {
        private List<CostHeadModel> FilterView = new List<CostHeadModel>();
        public CostHeadIndex()
        {
            InitializeComponent();
            tabControlAdv1.TabPages[2].TabVisible = false;
            tabControlAdv1.TabPages[3].TabVisible = false;
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;
        }
        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                divNameTextBox.Focus();
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as CostHeadListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new CostHeadListView();
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

                Log.Error(ex, "Cost Haad Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(divNameTextBox.Text) || divNameTextBox.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid Cost Center Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                divNameTextBox.Clear();
                divNameTextBox.Focus();
                return false;
            }

            using (var db = new KontoContext())
            {
                var find = db.CostHeads.FirstOrDefault(
                   x => x.HeadName == divNameTextBox.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "Cost Center Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    divNameTextBox.Focus();
                    return false;
                }
            }

            return true;
        }

        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<CostHeadModel>();
            this.Text = "Cost Center[Add New]";
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
        }

        private void DivIndex_Load(object sender, EventArgs e)
        {
            try
            {
                NewRec();

                this.ActiveControl = divNameTextBox;

                if (this.PrimaryKey == 0)
                {
                    toggleSwitch1.Enabled = false;
                }

            }
            catch (Exception ex)
            {

                Log.Error(ex, "costhead Load");
                MessageBox.Show(ex.ToString());
            }
        }

        public override void ResetPage()
        {
            base.ResetPage();
            divNameTextBox.Clear();
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
                var model = db.CostHeads.Find(_key);
                LoadData(model);
            }

        }
        private void LoadData(CostHeadModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            divNameTextBox.Text = model.HeadName;
            remarkTextBoxExt.Text = model.Remark;
            toggleSwitch1.EditValue = model.IsActive;
            toggleSwitch1.Enabled = true;
            divNameTextBox.Focus();
            this.Text = "Cost Center [View/Modify]";
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
            CostHeadModel _find = new CostHeadModel();
            if (!string.IsNullOrWhiteSpace(divNameTextBox.Text.Trim()))
                filter.Add(new Filter { PropertyName = "HeadName", Operation = Op.Contains, Value = divNameTextBox.Text.Trim() });


            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });
            filter.Add(new Filter { PropertyName = "BranchId", Operation = Op.Equals, Value = KontoGlobals.BranchId });

            using (var db = new KontoContext())
            {
                FilterView = db.CostHeads.Where(ExpressionBuilder.GetExpression<CostHeadModel>(filter)).ToList();
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
            CostHeadModel model = new CostHeadModel();
            using (var db = new KontoContext())
            {
                if (this.PrimaryKey != 0)
                    model = db.CostHeads.Find(this.PrimaryKey);
                model.HeadName = divNameTextBox.Text.Trim();
                
                model.BranchId = KontoGlobals.BranchId;

                model.Remark = remarkTextBoxExt.Text.Trim();
                model.IsActive = Convert.ToBoolean(toggleSwitch1.EditValue);

                if (this.PrimaryKey == 0)
                {
                    // model.RowId = Guid.NewGuid();
                    db.CostHeads.Add(model);
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
                    divNameTextBox.Focus();
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
