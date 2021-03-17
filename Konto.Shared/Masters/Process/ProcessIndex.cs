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

namespace Konto.Shared.Masters.Process
{
    public partial class ProcessIndex : KontoMetroForm
    {
        private List<ProcessModel> FilterView = new List<ProcessModel>();
        public ProcessIndex()
        {
            InitializeComponent();
            tabControlAdv1.TabPages[2].TabVisible = false;
            tabControlAdv1.TabPages[3].TabVisible = false;
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            taxTypelookUpEdit.Properties.DisplayMember = "DisplayText";
            taxTypelookUpEdit.Properties.ValueMember = "Id";

            this.FirstActiveControl = processNameTextBox;

            using (var db = new KontoContext())
            {
                var model = (from p in db.TaxMasters
                             where !p.IsDeleted && p.IsActive
                             orderby p.TaxName
                             select new BaseLookupDto
                             {
                                 DisplayText = p.TaxName,
                                 Id = p.Id
                             }).ToList();
                taxTypelookUpEdit.Properties.DataSource = model;
            }
        }
        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAdv1.SelectedIndex == 0)
            {
                processNameTextBox.Focus();
                return;
            }
            if (tabPageAdv2.Controls.Count > 0)
            {
                var _list = tabPageAdv2.Controls[0] as ProcessListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new ProcessListView();
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

                Log.Error(ex, "Process Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(processNameTextBox.Text) || processNameTextBox.Text.Length <= 1)
            {
                MessageBoxAdv.Show(this, "Invalid Process Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                processNameTextBox.Clear();
                processNameTextBox.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(hsnTextBoxExt.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Hsn/Sac Code", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                processNameTextBox.Clear();
                processNameTextBox.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(taxTypelookUpEdit.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Gst Type", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                taxTypelookUpEdit.Focus();
                return false;
            }

            using (var db = new KontoContext())
            {
                var find = db.Process.FirstOrDefault(
                   x => x.ProcessName == processNameTextBox.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "Process/Job Name Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    processNameTextBox.Focus();
                    return false;
                }
            }

            return true;
        }

        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<ProcessModel>();
            this.Text = "Process Master [Add New]";
            this.ActiveControl = processNameTextBox;
            createdLabelControl.Text = "Create By: " + KontoGlobals.UserName;
        }

        private void DivIndex_Load(object sender, EventArgs e)
        {
            try
            {
                NewRec();

                this.ActiveControl = processNameTextBox;

                if (this.PrimaryKey == 0)
                {
                    toggleSwitch1.Enabled = false;
                }

            }
            catch (Exception ex)
            {

                Log.Error(ex, "Process Load");
                MessageBox.Show(ex.ToString());
            }
        }

        public override void ResetPage()
        {
            base.ResetPage();
            processNameTextBox.Clear();
            hsnTextBoxExt.Clear();
            prikontoTextBoxExt1.Clear();
            taxTypelookUpEdit.EditValue = DBNull.Value;
            toggleSwitch1.EditValue = true;
            toggleSwitch1.Enabled = false;

        }

        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var model = db.Process.Find(_key);
                LoadData(model);
            }

        }
        private void LoadData(ProcessModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            processNameTextBox.Text = model.ProcessName;
            hsnTextBoxExt.Text = model.HsnCode;
            taxTypelookUpEdit.EditValue = model.TaxId;
            prikontoTextBoxExt1.Text = model.Priority.ToString();
            toggleSwitch1.EditValue = model.IsActive;
            toggleSwitch1.Enabled = true;
            processNameTextBox.Focus();
            this.Text = "Process Master [View/Modify]";
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
            DivisionModel _find = new DivisionModel();
            if (!string.IsNullOrWhiteSpace(processNameTextBox.Text.Trim()))
                filter.Add(new Filter { PropertyName = "ProcessName", Operation = Op.Contains, Value = processNameTextBox.Text.Trim() });


            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });
           

            using (var db = new KontoContext())
            {
                FilterView = db.Process.Where(ExpressionBuilder.GetExpression<ProcessModel>(filter)).ToList();
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
            ProcessModel model = new ProcessModel();
            using (var db = new KontoContext())
            {
                if (this.PrimaryKey != 0)
                    model = db.Process.Find(this.PrimaryKey);
                model.ProcessName = processNameTextBox.Text.Trim();

                model.Priority = Convert.ToInt32(prikontoTextBoxExt1.Text);
                model.TaxId = Convert.ToInt32( taxTypelookUpEdit.EditValue);
                model.HsnCode = hsnTextBoxExt.Text.Trim();
                model.IsActive = Convert.ToBoolean(toggleSwitch1.EditValue);

                if (this.PrimaryKey == 0)
                {
                    // model.RowId = Guid.NewGuid();
                    db.Process.Add(model);
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
                    processNameTextBox.Focus();
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
