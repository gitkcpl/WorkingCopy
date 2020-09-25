using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Admin;
using Konto.Data.Models.Masters;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Security
{
    public partial class UserIndex : KontoMetroForm
    {
        private List<UserMasterModel> FilterView = new List<UserMasterModel>();
        public UserIndex()
        {
            InitializeComponent();
            this.Load += RoleIndex_Load;
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;
            FillRole();
        }
        private void FillRole()
        {
            using (var db = new KontoContext())
            {
                var models = db.Roles.OrderBy(x => x.RoleName).ToList();
                roleLookUpEdit.Properties.DataSource = models;
            }
        }
        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                nameTextBox.Focus();
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as UserListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new UserListView();
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

                Log.Error(ex, "Role Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private void RoleIndex_Load(object sender, EventArgs e)
        {
            try
            {
                NewRec();

                this.ActiveControl = nameTextBox;

                if (this.PrimaryKey == 0)
                {
                    toggleSwitch1.Enabled = false;
                }

            }
            catch (Exception ex)
            {

                Log.Error(ex, "Role Load");
                MessageBox.Show(ex.ToString());
            }
        }
        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                MessageBoxAdv.Show(this, "Invalid User Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nameTextBox.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(passwordTextBoxExt.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Password", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                passwordTextBoxExt.Focus();
                return false;
            }
            else if (confirmTextBoxExt.Text.Trim()!= passwordTextBoxExt.Text.Trim())
            {
                MessageBoxAdv.Show(this, "Password and confirm password does not match", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                passwordTextBoxExt.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(roleLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Password and confirm password does not match", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                roleLookUpEdit.Focus();
                return false;
            }
           


            using (var db = new KontoContext())
            {
                var find = db.SizeModels.FirstOrDefault(
                   x => x.SizeName == nameTextBox.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "User Name Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nameTextBox.Focus();
                    return false;
                }
            }

            return true;
        }

        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<UserMasterModel>();
            this.Text = "User Master [Add New]";
            this.ActiveControl = nameTextBox;
        }
        public override void ResetPage()
        {
            base.ResetPage();
            nameTextBox.Clear();
            passwordTextBoxExt.Clear();
            confirmTextBoxExt.Clear();
            roleLookUpEdit.EditValue = DBNull.Value;
            toggleSwitch1.EditValue = true;
            toggleSwitch1.Enabled = false;

        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var model = db.UserMasters.Find(_key);
                LoadData(model);
            }

        }
        private void LoadData(UserMasterModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            nameTextBox.Text = model.UserName;
            passwordTextBoxExt.Text = KontoUtils.Decrypt(model.UserPass, "sblw-3hn8-sqoy19"); 
            roleLookUpEdit.EditValue = model.RoleId;
            
            toggleSwitch1.EditValue = model.IsActive;
            toggleSwitch1.Enabled = true;
            nameTextBox.Focus();
            this.Text = "User Master [View/Modify]";
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
            PSizeModel _find = new PSizeModel();

            if (!string.IsNullOrWhiteSpace(nameTextBox.Text.Trim()))
                filter.Add(new Filter { PropertyName = "RoleName", Operation = Op.Contains, Value = nameTextBox.Text.Trim() });

          
            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });


            using (var db = new KontoContext())
            {
                FilterView = db.UserMasters.Where(ExpressionBuilder.GetExpression<UserMasterModel>(filter))
                    .OrderBy(x => x.UserName).ToList();
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
            UserMasterModel model = new UserMasterModel();
            using (var db = new KontoContext())
            {
                if (this.PrimaryKey != 0)
                    model = db.UserMasters.Find(this.PrimaryKey);

                model.UserName = nameTextBox.Text.Trim();
                model.UserPass = KontoUtils.Encrypt(passwordTextBoxExt.Text.Trim(), "sblw-3hn8-sqoy19"); 
                model.RoleId = Convert.ToInt32(roleLookUpEdit.EditValue);
                model.IsActive = Convert.ToBoolean(toggleSwitch1.EditValue);

                if (this.PrimaryKey == 0)
                {
                    // model.RowId = Guid.NewGuid();
                    db.UserMasters.Add(model);
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
                    nameTextBox.Focus();
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
