﻿using Konto.App.Shared;
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

namespace Konto.Shared.Masters.Haste
{
    public partial class HasteIndex : KontoMetroForm
    {
        private List<HasteModel> FilterView = new List<HasteModel>();
        public HasteIndex()
        {
            InitializeComponent();
            this.Load += EmpIndex_Load;
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            this.FirstActiveControl = nameTextBox;
        }

        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                nameTextBox.Focus();
                if (this.PrimaryKey == 0)
                    this.Text = "Misc. Master [Add]";
                else
                    this.Text = "Misc. Master [Modify/View]";
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as HasteListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new HasteListView();
                _ListView.Dock = DockStyle.Fill;
                tabPageAdv2.Controls.Add(_ListView);
                this.Text = "Misc. Master [Modify/View]";
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

                Log.Error(ex, "Misc Master Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private void EmpIndex_Load(object sender, EventArgs e)
        {
            try
            {
                List<ComboBoxPairs> cbp = new List<ComboBoxPairs> {
                new ComboBoxPairs("Master","Master"),
                new ComboBoxPairs("Haste","Haste"),
            };
                typeLookUpEdit.Properties.ValueMember = "_Key";
                typeLookUpEdit.Properties.DisplayMember = "_Value";
                typeLookUpEdit.Properties.DataSource = cbp;

                NewRec();

                this.ActiveControl = nameTextBox;

                if (this.PrimaryKey == 0)
                {
                    toggleSwitch1.Enabled = false;
                }

            }
            catch (Exception ex)
            {

                Log.Error(ex, "Emp Load");
                MessageBox.Show(ex.ToString());
            }
        }
        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(nameTextBox.Text) || nameTextBox.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid Description Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nameTextBox.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(typeLookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                typeLookUpEdit.Focus();
                return false;
            }
           

            using (var db = new KontoContext())
            {
                var find = db.Hastes.FirstOrDefault(
                   x => x.HasteName == nameTextBox.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "type Name Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nameTextBox.Focus();
                    return false;
                }
            }

            return true;
        }

        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<HasteModel>();
            this.Text = "Misc Master [Add New]";
            typeLookUpEdit.EditValue = "Master";
            this.ActiveControl = nameTextBox; ;
        }
        public override void ResetPage()
        {
            base.ResetPage();
            nameTextBox.Clear();
            contactNoTextBox.Clear();
            address1TextBoxExt.Clear();
            address2TextBoxExt.Clear();
            aadharTrextBoxExt.Clear();
            panNoTextBoxExt.Clear();
            emailTextBoxExt.Clear();
            remarkTextBox.Clear();
            toggleSwitch1.EditValue = true;
            toggleSwitch1.Enabled = false;

        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var model = db.Hastes.Find(_key);
                LoadData(model);
            }

        }
        private void LoadData(HasteModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            nameTextBox.Text = model.HasteName;
            contactNoTextBox.Text = model.MobileNo;
            remarkTextBox.Text = model.Remark;
            address1TextBoxExt.Text = model.Address1;
            address2TextBoxExt.Text = model.Address2;
            aadharTrextBoxExt.Text = model.AadharNo;
            panNoTextBoxExt.Text = model.PanNo;
            emailTextBoxExt.Text = model.Email;
            typeLookUpEdit.EditValue = model.MType;
            toggleSwitch1.EditValue = model.IsActive;
            toggleSwitch1.Enabled = true;
            nameTextBox.Focus();
            this.Text = "Misc Master [View/Modify]";
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
           // EmpModel _find = new EmpModel();

            if (!string.IsNullOrWhiteSpace(nameTextBox.Text.Trim()))
                filter.Add(new Filter { PropertyName = "HasteName", Operation = Op.Contains, Value = nameTextBox.Text.Trim() });

            

            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });


            using (var db = new KontoContext())
            {
                FilterView = db.Hastes.Where(ExpressionBuilder.GetExpression<HasteModel>(filter))
                    .OrderBy(x => x.HasteName).ToList();
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
            HasteModel model = new HasteModel();
            using (var db = new KontoContext())
            {
                if (this.PrimaryKey != 0)
                    model = db.Hastes.Find(this.PrimaryKey);

                model.HasteName = nameTextBox.Text.Trim();
                model.MobileNo = contactNoTextBox.Text.Trim();
                model.Remark = remarkTextBox.Text.Trim();
                model.Address1 = address1TextBoxExt.Text.Trim();
                model.IsActive = Convert.ToBoolean(toggleSwitch1.EditValue);
                model.Address2 = address2TextBoxExt.Text.Trim();
                model.AadharNo = aadharTrextBoxExt.Text.Trim();
                model.PanNo = panNoTextBoxExt.Text.Trim();
                model.Email = emailTextBoxExt.Text.Trim();
                model.CompId = KontoGlobals.CompanyId;
                model.MType = typeLookUpEdit.EditValue.ToString();
                if (this.PrimaryKey == 0)
                {
                    // model.RowId = Guid.NewGuid();
                    db.Hastes.Add(model);
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
                    this.NewRec();
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
