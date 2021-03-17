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

namespace Konto.Shared.Masters.SubGroup
{
    public partial class SubGroupIndex : KontoMetroForm
    {
        private List<PSubGroupModel> FilterView = new List<PSubGroupModel>();
        public SubGroupIndex()
        {
            InitializeComponent();
            tabControlAdv1.TabPages[2].TabVisible = false;
            tabControlAdv1.TabPages[3].TabVisible = false;

            this.Load += SubGroupIndex_Load;
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            this.FirstActiveControl = groupNameTextBox;
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
                var _list = tabPageAdv2.Controls[0] as SubGroupListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new SubGroupListView();
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

                Log.Error(ex, "SubGroup Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private void SubGroupIndex_Load(object sender, EventArgs e)
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

                Log.Error(ex, "SubGroup Load");
                MessageBox.Show(ex.ToString());
            }
        }
        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(groupNameTextBox.Text) || groupNameTextBox.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid Sub Group Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                groupNameTextBox.Focus();
                return false;
            }
            else if(Convert.ToInt32(groupLookup1.SelectedValue)==0)
            {
                MessageBoxAdv.Show(this, "Invalid Group Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                groupLookup1.Focus();
                return false;
            }


            using (var db = new KontoContext())
            {
                var find = db.PSubGroups.FirstOrDefault(
                   x => x.SubName == groupNameTextBox.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "Sub Group Name Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    groupNameTextBox.Focus();
                    return false;
                }
            }

            return true;
        }

        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<PSubGroupModel>();
            this.Text = "Sub Group Master [Add New]";
            this.ActiveControl = codeTextBoxExt;

            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
        }
        public override void ResetPage()
        {
            base.ResetPage();
            groupNameTextBox.Clear();
            codeTextBoxExt.Clear();
            remarkTextBox.Clear();
            groupLookup1.SetEmpty();
            toggleSwitch1.EditValue = true;
            toggleSwitch1.Enabled = false;

        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var model = db.PSubGroups.Find(_key);
                LoadData(model);
            }

        }
        private void LoadData(PSubGroupModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            groupNameTextBox.Text = model.SubName;
            codeTextBoxExt.Text = model.SubCode;
            remarkTextBox.Text = model.Extra1;
            groupLookup1.SelectedValue = model.PGroupId;
            groupLookup1.SetGroup();
            toggleSwitch1.EditValue = model.IsActive;
            toggleSwitch1.Enabled = true;
            codeTextBoxExt.Focus();
            this.Text = "Sub Group Master [View/Modify]";

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
            PGroupModel _find = new PGroupModel();

            if (!string.IsNullOrWhiteSpace(groupNameTextBox.Text.Trim()))
                filter.Add(new Filter { PropertyName = "SubName", Operation = Op.Contains, Value = groupNameTextBox.Text.Trim() });

            if (!string.IsNullOrWhiteSpace(codeTextBoxExt.Text.Trim()))
                filter.Add(new Filter { PropertyName = "SubCode", Operation = Op.StartsWith, Value = codeTextBoxExt.Text.Trim() });

        

            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });


            using (var db = new KontoContext())
            {
                FilterView = db.PSubGroups.Where(ExpressionBuilder.GetExpression<PSubGroupModel>(filter))
                    .OrderBy(x => x.SubName).ToList();
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
            PSubGroupModel model = new PSubGroupModel();
            using (var db = new KontoContext())
            {
                if (this.PrimaryKey != 0)
                    model = db.PSubGroups.Find(this.PrimaryKey);

                model.SubName = groupNameTextBox.Text.Trim();
                model.SubCode = codeTextBoxExt.Text.Trim();
                model.Extra1 = remarkTextBox.Text.Trim();
                model.PGroupId = Convert.ToInt32(groupLookup1.SelectedValue);
                model.IsActive = Convert.ToBoolean(toggleSwitch1.EditValue);

                if (this.PrimaryKey == 0)
                {
                    // model.RowId = Guid.NewGuid();
                    db.PSubGroups.Add(model);
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
