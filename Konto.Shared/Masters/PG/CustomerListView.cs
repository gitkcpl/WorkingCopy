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
using AutoMapper.QueryableExtensions;
using Konto.Data.Models.Pos;
using Konto.Data.Models.Pos.Dtos;

namespace Konto.Shared.Masters.PG
{
    public partial class CustomerListView : ListBaseView
    {
        private List<CustomerListDto> _modelList = new List<CustomerListDto>();
        public CustomerListView()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.Customer_Master_List_Layout;

            this.Load += PgListView_Load;
        }

        private void PgListView_Load(object sender, EventArgs e)
        {
          //  this.RefreshGrid();
        }
        public override void RefreshGrid()
        {
            try
            {
                base.RefreshGrid();
                var configuration = new MapperConfiguration(cfg =>
                    cfg.CreateMap<CustomerModel, CustomerListDto>());

                using (var _db = new KontoContext())
                {
                    _modelList = _db.Customers.Where(x => !x.IsDeleted)
                                .OrderBy(x => x.CustomerName)
                                 .Select(x => new CustomerListDto
                                 {
                                     Address = x.Address,
                                     AnniDate = x.AnniDate,
                                     CustomerName = x.CustomerName,
                                     Dob = x.Dob,
                                     GstNo = x.GstNo,
                                     MemberDate = x.MemberDate,
                                     MemberNo = x.MemberNo,
                                     MobileNo = x.MobileNo,Id= x.Id
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

                Log.Error(ex, "customer refresh Grid");
                MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
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
                    var model = db.Customers.Find(_id);
                    model.IsDeleted = true;
                    db.SaveChanges();
                    customGridView1.DeleteRow(customGridView1.FocusedRowHandle);
                    MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "customer delete");
                MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
            }
        }
        public override void ImportExcel()
        {
            //base.ImportExcel();
            //var _exp = new PgImport();
            //_exp.ShowDialog();
        }
    }
}
