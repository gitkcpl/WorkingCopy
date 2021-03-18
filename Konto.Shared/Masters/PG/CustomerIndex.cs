﻿using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Pos;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Masters.PG
{
    public partial class CustomerIndex : KontoMetroForm
    {
        private List<CustomerModel> FilterView = new List<CustomerModel>();
        public CustomerIndex()
        {
            InitializeComponent();

            this.FirstActiveControl = nameTextBox;

            tabControlAdv1.TabPages[2].TabVisible = false;
            tabControlAdv1.TabPages[3].TabVisible = false;

            this.Load += PgIndex_Load;
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;
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
                var _list = tabPageAdv2.Controls[0] as PgListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new PgListView();
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

                Log.Error(ex, "Customer Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private void PgIndex_Load(object sender, EventArgs e)
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

                Log.Error(ex, "Customer Load");
                MessageBox.Show(ex.ToString());
            }
        }
        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(nameTextBox.Text) || nameTextBox.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid Customer Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nameTextBox.Focus();
                return false;
            }


            if (!string.IsNullOrEmpty(mobileNoTextBox.Text))
            {
                using (var db = new KontoContext())
                {
                    var find = db.Customers.FirstOrDefault(
                       x => x.MobileNo == mobileNoTextBox.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

                    if (find != null)
                    {
                        MessageBoxAdv.Show(this, "Customer Mobile No Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        nameTextBox.Focus();
                        return false;
                    }
                }
            }

            return true;
        }

        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<CustomerModel>();
            this.Text = "Customer [Add New]";
            this.ActiveControl = nameTextBox; ;
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
            modifyLabelControl.Text = string.Empty;
        }
        public override void ResetPage()
        {
            base.ResetPage();
            nameTextBox.Clear();
            mobileNoTextBox.Clear();
            addressTextBoxExt.Clear();
            memberNoTextBox.Clear();
            toggleSwitch1.EditValue = true;
            toggleSwitch1.Enabled = false;

        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var model = db.Customers.Find(_key);
                LoadData(model);
            }

        }
        private void LoadData(CustomerModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            nameTextBox.Text = model.CustomerName;
            mobileNoTextBox.Text = model.MobileNo;
            contactNoTextBox.Text = model.Phone;
            memberNoTextBox.Text = model.MemberNo;
            addressTextBoxExt.Text = model.Address;
            areaLookup1.SelectedValue = model.AreaId;
            areaLookup1.SetArea();
            cityLookup1.SelectedValue = model.CityId;
            cityLookup1.SetCity();
            emailTextBox.Text = model.Email;
            dobDateEdit.EditValue = model.Dob;
            annDateEdit.EditValue = model.AnniDate;
            memberDateEdit.EditValue = model.MemberDate;
            gstnoTextBox.Text = model.GstNo;
            toggleSwitch1.EditValue = model.IsActive;
            toggleSwitch1.Enabled = true;
            nameTextBox.Focus();
            this.Text = "Customer [View/Modify]";
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
            PartyGroupModel _find = new PartyGroupModel();

            if (!string.IsNullOrWhiteSpace(nameTextBox.Text.Trim()))
                filter.Add(new Filter { PropertyName = "CustomerName", Operation = Op.Contains, Value = nameTextBox.Text.Trim() });

            

            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });


            using (var db = new KontoContext())
            {
                FilterView = db.Customers.Where(ExpressionBuilder.GetExpression<CustomerModel>(filter))
                    .OrderBy(x => x.CustomerName).ToList();
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
            CustomerModel model = new CustomerModel();
            using (var db = new KontoContext())
            {
                if (this.PrimaryKey != 0)
                    model = db.Customers.Find(this.PrimaryKey);

                model.CustomerName = nameTextBox.Text.Trim();
                model.MobileNo = mobileNoTextBox.Text.Trim();
                model.MemberNo = memberNoTextBox.Text.Trim();
                model.Address = addressTextBoxExt.Text.Trim();
                model.IsActive = Convert.ToBoolean(toggleSwitch1.EditValue);

                model.Address = addressTextBoxExt.Text.Trim();

                if (Convert.ToInt32(areaLookup1.SelectedValue) != 0)
                    model.AreaId = Convert.ToInt32(areaLookup1.SelectedValue);
                else
                    model.AreaId = null;

                if (Convert.ToInt32(cityLookup1.SelectedValue) != 0)
                    model.CityId = Convert.ToInt32(cityLookup1.SelectedValue);
                else
                    model.CityId = null;

                model.Phone = contactNoTextBox.Text.Trim();
                model.Dob = dobDateEdit.DateTime;
                model.AnniDate = annDateEdit.DateTime;
                model.Email = emailTextBox.Text.Trim();
                model.MemberDate = memberDateEdit.DateTime;
                model.GstNo = gstnoTextBox.Text.Trim();
                

                if (this.PrimaryKey == 0)
                {
                    // model.RowId = Guid.NewGuid();
                    db.Customers.Add(model);
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