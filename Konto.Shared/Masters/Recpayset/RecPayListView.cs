﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Konto.Core.Shared.Frms;
using Konto.Data.Models.Masters.Dtos;
using Konto.App.Shared;
using Konto.Data;
using AutoMapper;
using Konto.Core.Shared.Libs;
using Syncfusion.Windows.Forms;
using Serilog;
using Konto.Data.Models.Masters;

namespace Konto.Shared.Masters.Recpayset
{
    public partial class RecPayListView : ListBaseView
    {

        private List<RpSetLisDto> _modelList = new List<RpSetLisDto>();
        public RecPayListView()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.RpSet_List_Layout;
            this.Load += BranchListView_Load;
        }

        private void BranchListView_Load(object sender, EventArgs e)
        {
            //this.RefreshGrid();
        }

        public override void RefreshGrid()
        {
            base.RefreshGrid();
            

            using (var _db = new KontoContext())
            {
                _modelList = (from p in _db.RPSets
                              join ac in _db.Accs on p.AccountId equals ac.Id into join_acc
                              from ac in join_acc.DefaultIfEmpty()
                              where !p.IsDeleted && p.YearId == KontoGlobals.YearId && p.CompId == KontoGlobals.CompanyId
                              select new RpSetLisDto()
                              {
                                  Id = p.Id, AmtCap = p.AmtCap, CreateDate = p.CreateDate, CreateUser = p.CreateUser,
                                  Drcr = p.Drcr, Field = p.Field, HsnCode = p.HsnCode, IsActive = p.IsActive, Ledger = ac.AccName,
                                  ModifyDate = p.ModifyDate, ModifyUser = p.ModifyUser, PerCap = p.PerCap, PlusMinus = p.PlusMinus,
                                  RecPay = p.RecPay
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
                    var model = db.RPSets.Find(_id);
                    model.IsDeleted = true;
                    db.SaveChanges();
                    customGridView1.DeleteRow(customGridView1.FocusedRowHandle);
                    MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Rpset delete");
                MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
            }
        }
    }
}
