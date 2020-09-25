using DevExpress.XtraGrid.Views.Grid;
using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Admin;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Masters.Dtos;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Security
{
    public partial class RoleIndex : KontoMetroForm
    {
        private List<RolesModel> FilterView = new List<RolesModel>();
        private List<RolePermissionDto> MasterDtoList = new List<RolePermissionDto>();
        private List<RolePermissionDto> TransactionDtoList = new List<RolePermissionDto>();
        private List<RolePermissionDto> SetupDtoList = new List<RolePermissionDto>();
        private List<RolePermissionDto> ReportDtoList = new List<RolePermissionDto>();
        private List<RolePermissionDto> OtherDtoList = new List<RolePermissionDto>();
        private List<RolePermissionDto> DelList = new List<RolePermissionDto>();
        public RoleIndex()
        {
            InitializeComponent();
            this.Load += SizeIndex_Load;
            okSimpleButton.Click += OkSimpleButton_Click;
            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;
            addPermSimpleButton.Click += AddPermSimpleButton_Click;
            customGridControl1.DataSource = MasterDtoList;
            customGridControl2.DataSource = TransactionDtoList;
            customGridControl3.DataSource = ReportDtoList;
            customGridControl4.DataSource = SetupDtoList;
            customGridControl5.DataSource = OtherDtoList;
        }

        private void AddPermSimpleButton_Click(object sender, EventArgs e)
        {
            var frm = new AddPermission();
            var sindex = tabControlAdv2.SelectedIndex;
            string PType = "MASTER";
            if (sindex == 0)
                PType = "MASTER";
            else if (sindex == 1)
                PType = "TRANSACTION";
            else if (sindex == 3)
                PType = "SETUP";
            else if (sindex == 2)
                PType = "REPORT";
            else if (sindex == 4)
                PType = "OTHER";
            frm.PType = PType;
            frm.RoleId = this.PrimaryKey;
            frm.ShowDialog(this);

            Int32[] selectedRowHandles = frm.SelectedRowHandles;
            if (selectedRowHandles== null || selectedRowHandles.Count() == 0) return;

            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                int selectedRowHandle = selectedRowHandles[i];
                if (selectedRowHandle >= 0)
                {
                    var row = frm.customGridView1.GetRow(selectedRowHandle) as PermissionsModel;
                    var dto = new RolePermissionDto();
                    dto.Id = row.Id;
                    dto.PermissionType = row.PermissionType;
                    dto.Descr = row.PermissionDescription;
                    if (tabControlAdv2.SelectedIndex == 0)
                    {
                        var dt = MasterDtoList.FirstOrDefault(x => x.Id == row.Id);
                        if (dt == null)
                            MasterDtoList.Add(dto);
                    }
                    else if (tabControlAdv2.SelectedIndex == 1)
                    {
                        var dt = TransactionDtoList.FirstOrDefault(x => x.Id == row.Id);
                        if (dt == null)
                            TransactionDtoList.Add(dto);
                    }
                    else if (tabControlAdv2.SelectedIndex == 2)
                    {
                        var dt = ReportDtoList.FirstOrDefault(x => x.Id == row.Id);
                        if (dt == null)
                            ReportDtoList.Add(dto);
                    }
                    else if (tabControlAdv2.SelectedIndex == 3)
                    {
                        var dt = SetupDtoList.FirstOrDefault(x => x.Id == row.Id);
                        if (dt == null)
                            SetupDtoList.Add(dto);
                    }
                    else
                    {
                        var dt = OtherDtoList.FirstOrDefault(x => x.Id == row.Id);
                        if (dt == null)
                            OtherDtoList.Add(dto);
                    }
                }

            }

            SetGridDataSource();

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
                var _list = tabPageAdv2.Controls[0] as RoleListView;
                _list.ActiveControl = _list.KontoGrid;
                return;
            }
            if (tabControlAdv1.SelectedIndex == 1)
            {
                var _ListView = new RoleListView();
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

                Log.Error(ex, "Size Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
        }

        private void SizeIndex_Load(object sender, EventArgs e)
        {
            try
            {
                NewRec();

                this.ActiveControl = nameTextBox;

             
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Size Load");
                MessageBox.Show(ex.ToString());
            }
        }
        private bool ValidateData()
        {

            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                MessageBoxAdv.Show(this, "Invalid Role Name", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nameTextBox.Focus();
                return false;
            }
           
            using (var db = new KontoContext())
            {
                var find = db.Roles.FirstOrDefault(
                   x => x.RoleName == nameTextBox.Text.Trim() && x.Id != this.PrimaryKey && !x.IsDeleted);

                if (find != null)
                {
                    MessageBoxAdv.Show(this, "Role Name Already Exists", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nameTextBox.Focus();
                    return false;
                }
            }

            return true;
        }

        public override void NewRec()
        {
            base.NewRec();
            this.FilterView = new List<RolesModel>();
            this.Text = "User Role [Add New]";
            this.ActiveControl = nameTextBox;
            MasterDtoList = new List<RolePermissionDto>();
            TransactionDtoList = new List<RolePermissionDto>();
            SetupDtoList = new List<RolePermissionDto>();
            ReportDtoList = new List<RolePermissionDto>();
            OtherDtoList = new List<RolePermissionDto>();
            DelList = new List<RolePermissionDto>();
            SetGridDataSource();
        }

        public override void ResetPage()
        {
            base.ResetPage();
            nameTextBox.Clear();
            remarkTextBox.Clear();
            checkEdit1.Checked = false;

        }
        public override void EditPage(int _key)
        {
            base.EditPage(_key);
            this.PrimaryKey = _key;

            using (var db = new KontoContext())
            {
                var model = db.Roles.Find(_key);
                LoadData(model);
            }

        }
        private void LoadData(RolesModel model)
        {
            this.ResetPage();
            this.PrimaryKey = model.Id;
            nameTextBox.Text = model.RoleName;
            checkEdit1.Checked = model.IsSysAdmin;
            remarkTextBox.Text = model.RoleDescription;
            nameTextBox.Focus();

            using(var db = new KontoContext())
            {
                var _lst = (from pr in db.RolePermissions
                           join mst in db.Permissions on pr.PermissionId equals mst.Id
                           where pr.RoleId == model.Id
                           select new RolePermissionDto()
                           {
                               Descr = mst.PermissionDescription,
                               Id = mst.Id,
                               PermissionType = mst.PermissionType,
                               RoleId = pr.RoleId,
                               TransId = pr.Id
                           }
               ).ToList();
                MasterDtoList = _lst.Where(x => x.PermissionType == "Master").ToList();
                TransactionDtoList = _lst.Where(x => x.PermissionType == "Transaction").ToList();
                SetupDtoList = _lst.Where(x => x.PermissionType == "Setup").ToList();
                ReportDtoList = _lst.Where(x => x.PermissionType == "Report").ToList();
                OtherDtoList = _lst.Where(x => x.PermissionType == "Other").ToList();
                DelList = new List<RolePermissionDto>();
            }
            SetGridDataSource();

            this.Text = "Role Master [View/Modify]";

        }
        private void SetGridDataSource()
        {
            customGridControl1.DataSource = MasterDtoList;
            customGridControl1.RefreshDataSource();

            customGridControl2.DataSource = TransactionDtoList;
            customGridControl2.RefreshDataSource();

            customGridControl3.DataSource = ReportDtoList;
            customGridControl3.RefreshDataSource();

            customGridControl4.DataSource = SetupDtoList;
            customGridControl4.RefreshDataSource();

            customGridControl5.DataSource = OtherDtoList;
            customGridControl5.RefreshDataSource();
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
                FilterView = db.Roles.Where(ExpressionBuilder.GetExpression<RolesModel>(filter))
                    .OrderBy(x => x.RoleName).ToList();
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
            RolesModel model = new RolesModel();
            var _dtolist = new List<RolePermissionDto>();
            _dtolist.AddRange(this.MasterDtoList);
            _dtolist.AddRange(this.TransactionDtoList);
            _dtolist.AddRange(this.ReportDtoList);
            _dtolist.AddRange(this.SetupDtoList);
            _dtolist.AddRange(this.OtherDtoList);

            using (var db = new KontoContext())
            {
                using (var _tran = db.Database.BeginTransaction())
                {

                    try
                    {
                        if (this.PrimaryKey != 0)
                            model = db.Roles.Find(this.PrimaryKey);
                         model.RoleName = nameTextBox.Text.Trim();
                        model.RoleDescription = remarkTextBox.Text.Trim();
                        model.IsSysAdmin = checkEdit1.Checked;
                        model.IsActive = true;
                        if (this.PrimaryKey == 0)
                        {
                            db.Roles.Add(model);
                            db.SaveChanges();
                        }
                        RolePermission _role = null;
                        var _permlist = new List<RolePermission>();
                        foreach (var _dto in _dtolist)
                        {
                            if (_dto.TransId == 0)
                            {
                               _role = new RolePermission();
                                _role.PermissionType = _dto.PermissionType;
                                _role.PermissionId = _dto.Id;
                                _role.RoleId = model.Id;
                                _role.RowId = Guid.NewGuid();
                                _permlist.Add(_role);
                            }
                            else
                            {
                                _role = db.RolePermissions.Find(_dto.TransId);
                                _role.PermissionType = _dto.PermissionType;
                                _role.PermissionId = _dto.Id;
                            }
                        }
                        foreach (var item in DelList)
                        {
                            _role = db.RolePermissions.Find(item.TransId);
                            db.RolePermissions.Remove(_role);
                        }
                        db.RolePermissions.AddRange(_permlist);
                        db.SaveChanges();

                        _tran.Commit();
                        IsSaved = true;
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Role Saved");
                        MessageBox.Show(ex.ToString());
                    }
                    
                }
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

        private void customGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
            {
                if (MessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo) !=
                  DialogResult.Yes)
                    return;
                GridView view = sender as GridView;
                var rw = view.GetRow(view.FocusedRowHandle) as RolePermissionDto;
                view.DeleteRow(view.FocusedRowHandle);
                DelList.Add(rw);
            }
        }
    }
}
