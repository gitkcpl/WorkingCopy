using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Transaction.Dtos;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Weaves.JobCard
{
    public partial class JobCardList : ListBaseView
    {
        private List<JobCardDto> _modelList = new List<JobCardDto>();
        public JobCardList()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.JobCard_List;
        }
        public override void RefreshGrid()
        {
            base.RefreshGrid();
            //var configuration = new MapperConfiguration(cfg =>
            //    cfg.CreateMap<DivisionModel, DivListDto>());

            using (var _db = new KontoContext())
            {
                var spcol = _db.SpCollections.FirstOrDefault(k => k.Id == (int)SpCollectionEnum.JobCardList);
                if (spcol == null)
                {
                    _modelList = new List<JobCardDto>(_db.Database.SqlQuery<JobCardDto>(
                        "dbo.JobCardList @VoucherTypeID={0}, @CompanyId={1}",
                        (int)VoucherTypeEnum.JobCard, KontoGlobals.CompanyId).ToList());
                }
                else
                {
                    _modelList = new List<JobCardDto>(_db.Database.SqlQuery<JobCardDto>(spcol.Name + " @VoucherTypeID={0}, @CompanyId={1}",
                            (int)VoucherTypeEnum.JobCard, KontoGlobals.CompanyId).ToList());
                }
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
                    var Model  = db.jobCards.Find(_id);
                    var Trans = db.jobCardTrans.Where(k => k.JobCardId==_id && k.IsDeleted == false).ToList();
                    foreach (var item in Trans)
                    {
                        item.IsDeleted = true;
                    }

                    Model.IsDeleted = true;

                    db.SaveChanges();
                    customGridView1.DeleteRow(customGridView1.FocusedRowHandle);
                    MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "JobCard delete");
                MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
            }
        }
    }
}