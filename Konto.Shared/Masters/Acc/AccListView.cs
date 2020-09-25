using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Konto.Core.Shared.Frms;
using Konto.Data.Models.Masters.Dtos;
using Konto.App.Shared;
using AutoMapper;
using Konto.Data.Models.Masters;
using Konto.Data;
using Konto.Core.Shared.Libs;
using Syncfusion.Windows.Forms;
using Serilog;
using Syncfusion.Windows.Forms.Tools;
using System.Drawing;
using Konto.Core.Shared;

namespace Konto.Shared.Masters.Acc
{
    public partial class AccListView : ListBaseView
    {
        private List<AccListDto> _modelList = new List<AccListDto>();
        public AccListView()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.Account_Master_List_Layout;

            this.Load += AccListView_Load;
            opBalsimpleButton.Click += OpBalsimpleButton_Click;
            addressSimpleButton.Click += AddressSimpleButton_Click;
            bankSimpleButton.Click += BankSimpleButton_Click;
            splitButton1.DropDowItemClicked += SplitButton1_DropDowItemClicked;
        }

        private void SplitButton1_DropDowItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (customGridView1.FocusedRowHandle < 0) return;
            var frm = this.ParentForm as KontoForm;
            if(frm!=null && (frm.Create_Permission || frm.Modify_Permission))
            {
                var acid = Convert.ToInt32(customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle,
                       customGridView1.Columns["Id"]));
                if (e.ClickedItem.Text == "Depreciation")
                {
                    var frmd = new DeprView();
                    frmd.AccId = acid;
                    frmd.ShowDialog(this);
                }
                else if (e.ClickedItem.Text == "TDS")
                {
                    var frmd = new TdsView();
                    frmd.AccId = acid;
                    frmd.ShowDialog(this);
                }
                else if (e.ClickedItem.Text == "TCS")
                {
                    var frmd = new TcsView();
                    frmd.AccId = acid;
                    frmd.ShowDialog(this);
                }
                else if (e.ClickedItem.Text == "OpStock")
                {
                    var frmd = new OpStockView();
                    frmd.AccId = acid;
                    frmd.ShowDialog(this);
                }
                else if(e.ClickedItem.Text =="Interest")
                {
                    var frmd = new InterestView();
                    frmd.AccId = acid;
                    frmd.ShowDialog(this);
                }
                else
                {
                    var frm1 = new AccImport();
                    frm1.ShowDialog();
                }
            }
        }

        private void DeprToolstripitem_Click(object sender, EventArgs e)
        {

        }

        private void BankSimpleButton_Click(object sender, EventArgs e)
        {
            if (customGridView1.FocusedRowHandle < 0) return;
            var frm = new AccBankView();
            frm.AccId = Convert.ToInt32(customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle,
                customGridView1.Columns["Id"]));
            frm.ShowDialog(this);
        }

        private void AddressSimpleButton_Click(object sender, EventArgs e)
        {
            if (customGridView1.FocusedRowHandle < 0) return;
            var frm = new AddressView();
            frm.AccId = Convert.ToInt32(customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle,
                customGridView1.Columns["Id"]));
            frm.ShowDialog(this);
        }

        private void OpBalsimpleButton_Click(object sender, EventArgs e)
        {
            
            var _tab = this.Parent.Parent.Parent.Parent.Parent as TabControlAdv;
            if (_tab == null) return;
            var frm = new OpBalBulkEditView();
            var pg1 = new TabPageAdv();
            pg1.Text = "Account Op Balance";
            _tab.TabPages.Add(pg1);
            _tab.SelectedTab = pg1;
            frm.TopLevel = false;
            frm.Parent = pg1;
            frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
            frm.Show();// = true;
        }

        private void AccListView_Load(object sender, EventArgs e)
        {
            //this.RefreshGrid();
        }
        public override void RefreshGrid()
        {
            try
            {
                base.RefreshGrid();
                var configuration = new MapperConfiguration(cfg =>
                    cfg.CreateMap<EmpModel, EmpListDto>());

                using (var _context = new KontoContext())
                {
                    _context.Database.CommandTimeout = 0;
                    _modelList = (from ac in _context.Accs
                                   join bal in _context.AccBals on ac.Id equals bal.AccId
                                   join adr in _context.AccAddresses on bal.AddressId equals adr.Id into join_adr
                                   from adr in join_adr.DefaultIfEmpty()
                                   join grp in _context.AcGroupModels on bal.GroupId equals grp.Id
                                   join ct in _context.Cities on bal.CityId equals ct.Id into join_ct
                                   from ct in join_ct.DefaultIfEmpty()
                                   join st in _context.States on ct.StateId equals st.Id into join_st
                                   from st in join_st.DefaultIfEmpty()
                                   join ag in _context.Accs on ac.AgentId equals ag.Id into join_ag
                                   from ag in join_ag.DefaultIfEmpty()
                                   join tr in _context.Accs on ac.TransportId equals tr.Id into join_tr
                                   from tr in join_tr.DefaultIfEmpty()
                                   join em in _context.Emps on ac.EmpId equals em.Id into join_em
                                   from em in join_em.DefaultIfEmpty()
                                   join pg in _context.PartyGroups on ac.PGroupId equals pg.Id into join_pg
                                   from pg in join_pg.DefaultIfEmpty()
                                   orderby ac.AccName
                                   where ac.IsActive && !ac.IsDeleted && bal.CompId == KontoGlobals.CompanyId && bal.YearId == KontoGlobals.YearId
                                   select new AccListDto()
                                   {
                                       AccName = ac.AccName,
                                       Address1 = bal.Address1,
                                       Address2 = bal.Address2,
                                       CityName = ct.CityName,
                                       AddressId = ac.AddressId,
                                       AgentId = ac.AgentId,
                                       Bal = bal.Bal,
                                       CrDays = ac.CrDays,
                                       CrLimit = ac.CrLimit,
                                       EmpId = ac.EmpId,
                                       MobileNo = adr.MobileNo,
                                       GroupId = bal.GroupId,
                                       Id = ac.Id,
                                       TransportId = ac.TransportId,
                                       Email = adr.Email,
                                       PGroupId = ac.PGroupId,
                                       StateId = st.Id,
                                       TcsReq = ac.TcsReq,
                                       TdsReq = ac.TdsReq,
                                       VatTds = ac.VatTds,
                                       GroupName = grp.GroupName,
                                       Agent = ag.AccName,
                                       Transport = tr.AccName,
                                       BToB = ac.BToB,
                                       PartyGroup = pg.GroupName,
                                       SalesMan = em.EmpName,
                                       GSTIN = ac.GstIn,
                                       PanNo = ac.PanNo,
                                       AadharNo = ac.AadharNo,
                                       OpBal = bal.OpBal,
                                       AcGroup = grp,
                                       CreateDate = ac.CreateDate,
                                       CreateUser = ac.CreateUser
                                   }).ToList();

                }

                customGridControl1.DataSource = _modelList;
                if (string.IsNullOrEmpty(this.GridLayoutFileName) || this.KontoView == null) return;

                KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, this.KontoView);

                this.ActiveControl = customGridControl1;
                if (_modelList.Count == 0)
                    listAction1.EditDeleteDisabled(false);
                else
                    listAction1.EditDeleteDisabled(true);
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Acc refresh Grid");
                MessageBoxAdv.Show(this, "Error While List !!", "Exception ", ex.ToString());
            }
           
        }

        public override void DeleteRec()
        {
            base.DeleteRec();

            if (customGridView1.FocusedRowHandle < 0) return;
            try
            {
                var _id = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "Id"));
                if (MessageBoxAdv.Show(KontoGlobals.DeleteBeforeMsg, "Delete ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                using (var db = new KontoContext())
                {
                    var model = db.Accs.Find(_id);
                    model.IsDeleted = true;
                    db.SaveChanges();
                    customGridView1.DeleteRow(customGridView1.FocusedRowHandle);
                    MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Emp delete");
                MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
            }
        }
    }
}
