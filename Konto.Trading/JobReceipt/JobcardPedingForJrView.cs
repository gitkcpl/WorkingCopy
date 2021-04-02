using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Wvs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Trading.JobReceipt
{
    public partial class JobcardPedingForJrView : KontoForm
    {
        public JobcardPedingForJrView()
        {
            InitializeComponent();
            this.Load += JobcardPedingForJrView_Load;
            okSimpleButton.Click += OkSimpleButton_Click;
            cancelSimpleButton.Click += CancelSimpleButton_Click;
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void JobcardPedingForJrView_Load(object sender, EventArgs e)
        {
            var list = new List<PendingJobDto>();
            using (var _db = new KontoContext())
            {
                var spcol = _db.SpCollections.FirstOrDefault(k => k.Id ==
                                     (int)SpCollectionEnum.JobCardList);
                if (spcol == null)
                {
                    list = _db.Database
                              .SqlQuery<PendingJobDto>("dbo.JobCardList @CompanyId={0} ,@VTypeId={1}",
                                  KontoGlobals.CompanyId, VoucherTypeEnum.JobCard).ToList();
                }
                else
                {
                    list = _db.Database
                           .SqlQuery<PendingJobDto>(spcol.Name + " @CompanyId={0} ,@VTypeId={1}",
                               KontoGlobals.CompanyId, VoucherTypeEnum.JobCard).ToList();
                }

                pendingJobDtoBindingSource.DataSource = list;
                gridControl1.RefreshDataSource();
            }
        }
    }
}
