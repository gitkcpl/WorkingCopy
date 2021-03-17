using Konto.App.Shared;
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

namespace Konto.Shared.Masters.PackingType
{
    public partial class PackingTypeIndex : KontoMetroForm
    {
        private List<PackingTypeModel> FilterView = new List<PackingTypeModel>();
        public PackingTypeIndex()
        {
            InitializeComponent();
            this.Load += GradeIndex_Load;
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            this.FirstActiveControl = nameTextBox;
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
                var _list = tabPageAdv2.Controls[0] as PackingListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new PackingListView();
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

                Log.Error(ex, "Packing Type Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private void GradeIndex_Load(object sender, EventArgs e)
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

                Log.Error(ex, "Packing Type Load");
                MessageBox.Show(ex.ToString());
            }
        }
        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Packing Type Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nameTextBox.Clear();
                nameTextBox.Focus();
                return false;
            }
           


            using (var db = new KontoContext())
            {
                var find = db.PackingTypes.FirstOrDefault(
                   x => x.TypeName == nameTextBox.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "Packing Name Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nameTextBox.Focus();
                    return false;
                }
            }

            return true;
        }

        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<PackingTypeModel>();
            this.Text = "Packing Type Master [Add New]";
        }
        public override void ResetPage()
        {
            base.ResetPage();
            nameTextBox.Clear();
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
                var model = db.PackingTypes.Find(_key);
                LoadData(model);
            }
        }
        private void LoadData(PackingTypeModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            nameTextBox.Text = model.TypeName;
           
            remarkTextBox.Text = model.Remark;
            
            toggleSwitch1.EditValue = model.IsActive;
            toggleSwitch1.Enabled = true;
            nameTextBox.Focus();
            this.Text = "Packing Type Master [View/Modify]";
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
            PackingTypeModel _find = new PackingTypeModel();

            if (!string.IsNullOrWhiteSpace(nameTextBox.Text.Trim()))
                filter.Add(new Filter { PropertyName = "TypeName", Operation = Op.Contains, Value = nameTextBox.Text.Trim() });

            filter.Add(new Filter { PropertyName = "IsDeleted", Operation = Op.Equals, Value = false });


            using (var db = new KontoContext())
            {
                FilterView = db.PackingTypes.Where(ExpressionBuilder.GetExpression<PackingTypeModel>(filter))
                    .OrderBy(x => x.TypeName).ToList();
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
            PackingTypeModel model = new PackingTypeModel();
            using (var db = new KontoContext())
            {
                if (this.PrimaryKey != 0)
                    model = db.PackingTypes.Find(this.PrimaryKey);

                model.TypeName = nameTextBox.Text.Trim();
                model.Remark = remarkTextBox.Text.Trim();
                
                model.IsActive = Convert.ToBoolean(toggleSwitch1.EditValue);

                if (this.PrimaryKey == 0)
                {
                    // model.RowId = Guid.NewGuid();
                    db.PackingTypes.Add(model);
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
                    NewRec();
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