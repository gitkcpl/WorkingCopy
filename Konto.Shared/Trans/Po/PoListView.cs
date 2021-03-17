using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Konto.Core.Shared.Frms;
using Konto.App.Shared;
using Konto.Data;
using Konto.Core.Shared.Libs;
using Syncfusion.Windows.Forms;
using Serilog;
using Konto.Data.Models.Transaction.Dtos;
using System.Data.SqlClient;
using Konto.Shared.Reports;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using System.Linq.Dynamic.Core;

namespace Konto.Shared.Trans.Po
{
   

    public partial class PoListView : ListBaseView
    {
        private List<OpBillListDto> _modelList = new List<OpBillListDto>();

        public PoListView()

        {
            InitializeComponent();
            this.listDateRange1.GetButtonClick += ListDateRange1_GetButtonClick;
            //  this.GridLayoutFileName = KontoFileLayout.Op_Bill_List;
            this.customGridView1.FocusedRowChanged += CustomGridView1_FocusedRowChanged;
            this.ReportPrint = true;
            listAction1.EditDeleteDisabled(false);
            this.customGridView1.PopupMenuShowing += CustomGridView1_PopupMenuShowing;
            this.Load += PoListView_Load;
        }

        private void CustomGridView1_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (customGridView1.FocusedRowHandle < 0) return;
            var _id = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "Id"));
            var _vid = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "VoucherId"));


            using (var db = new KontoContext())
            {
                var lsts = (from c in db.Challans
                           join ct in db.ChallanTranses on c.Id equals ct.ChallanId
                           join ac in db.Accs on c.AccId equals ac.Id
                           join pd in db.Products on ct.ProductId equals pd.Id
                           where !c.IsDeleted && !ct.IsDeleted
                           && ct.MiscId == _id && ct.RefVoucherId == _vid
                           select new GrnAgainstOrderDto
                           {
                               BillNo= c.BillNo,ChallanNo=c.ChallanNo,Id= c.Id,Pcs= ct.Pcs,
                               ProductName= pd.ProductName,Qty= ct.Qty,Rate=ct.Rate,VoucherDate= c.VoucherDate,
                               VoucherNo= c.VoucherNo,VTypeId= c.TypeId

                           }).ToList();

                if(lsts.Count==0)
                {
                    lsts = (from c in db.Bills
                            join ct in db.BillTrans on c.Id equals ct.BillId
                            join ac in db.Accs on c.AccId equals ac.Id
                            join pd in db.Products on ct.ProductId equals pd.Id
                            where !c.IsDeleted && !ct.IsDeleted
                            && ct.RefId == _id && ct.RefVoucherId == _vid
                            select new GrnAgainstOrderDto
                            {
                                BillNo = c.BillNo,
                                ChallanNo = c.RefNo,
                                Id = c.Id,
                                Pcs = ct.Pcs,
                                ProductName = pd.ProductName,
                                Qty = ct.Qty,
                                Rate = ct.Rate,
                                VoucherDate = c.VoucherDate,
                                VoucherNo = c.VoucherNo,
                                VTypeId = c.TypeId

                            }).ToList();
                }

                grnAgainstOrderDtoBindingSource.DataSource = lsts;
                gridControl1.RefreshDataSource();
            }

        }

        private void PoListView_Load(object sender, EventArgs e)
        {
            // this.dockingManager1.DockControl(this.panel1,this, DockingStyle.Left, 200);
            //this.dockingManager1.SetEnableDocking(this.panel1, true);
            //this.dockingManager1.SetDockLabel(this.panel1, "Action");

           // OrderList();
        }

        private void CustomGridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            {
                int rowHandle = e.HitInfo.RowHandle;
                // Delete existing menu items, if any.
                e.Menu.Items.Clear();
                // Add the Rows submenu with the 'Delete Row' command
                e.Menu.Items.Add(CreateSubMenuRows(view, rowHandle));
                // Add the 'Cell Merging' check menu item.
                DXMenuItem item = CreateMenuItemCellMerging(view, rowHandle);
                item.BeginGroup = true;
                e.Menu.Items.Add(item);
            }
        }

        DXMenuItem CreateSubMenuRows(GridView view, int rowHandle)
        {
            DXSubMenuItem subMenu = new DXSubMenuItem("Rows");
            string deleteRowsCommandCaption;
            if (view.IsGroupRow(rowHandle))
                deleteRowsCommandCaption = "&Delete rows in this group";
            else
                deleteRowsCommandCaption = "&Delete this row";
            DXMenuItem menuItemDeleteRow = new DXMenuItem(deleteRowsCommandCaption, new EventHandler(OnDeleteRowClick));
            menuItemDeleteRow.Tag = new RowInfo(view, rowHandle);
            menuItemDeleteRow.Enabled = view.IsDataRow(rowHandle) || view.IsGroupRow(rowHandle);
            subMenu.Items.Add(menuItemDeleteRow);
            return subMenu;
        }

        DXMenuCheckItem CreateMenuItemCellMerging(GridView view, int rowHandle)
        {
            DXMenuCheckItem checkItem = new DXMenuCheckItem("Cell &Merging",
              view.OptionsView.AllowCellMerge, null, new EventHandler(OnCellMergingClick));
            checkItem.Tag = new RowInfo(view, rowHandle);
          //  checkItem.ImageOptions.Image = imageCollection1.Images[1];
            return checkItem;
        }

        void OnDeleteRowClick(object sender, EventArgs e)
        {
            DXMenuItem menuItem = sender as DXMenuItem;
            RowInfo ri = menuItem.Tag as RowInfo;
            if (ri != null)
            {
                string message = menuItem.Caption.Replace("&", "");
                if (XtraMessageBox.Show(message + " ?", "Confirm operation", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
                ri.View.DeleteRow(ri.RowHandle);
            }
        }

        void OnCellMergingClick(object sender, EventArgs e)
        {
            DXMenuCheckItem item = sender as DXMenuCheckItem;
            RowInfo info = item.Tag as RowInfo;
            info.View.OptionsView.AllowCellMerge = item.Checked;
        }

        private void ListDateRange1_GetButtonClick(object sender, EventArgs e)
        {
            this.GridLayoutFileName = listDateRange1.SelectedItem.LayoutFile;
            var DtCriterias = new DataTable();
            try
            {
                var db = new KontoContext();
                
                using (var con = new SqlConnection(db.Database.Connection.ConnectionString))
                {
                    using (var cmd = new SqlCommand(listDateRange1.SelectedItem.SpName, con))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Add("@fromDate", SqlDbType.Int).Value = listDateRange1.FromDate;
                        cmd.Parameters.Add("@ToDate", SqlDbType.Int).Value = listDateRange1.ToDate;
                        cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = KontoGlobals.CompanyId;
                        cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = KontoGlobals.BranchId;
                        cmd.Parameters.Add("@YearId", SqlDbType.Int).Value = KontoGlobals.YearId;
                        cmd.Parameters.Add("@VTypeId", SqlDbType.Int).Value = (int)VoucherTypeEnum.PurchaseOrder;
                        if (listDateRange1.SelectedItem.Extra1 == "Deleted")
                        {
                            cmd.Parameters.Add("@Deleted", SqlDbType.Int).Value = 1;
                        }
                        if (listDateRange1.SelectedItem.GroupCol != null)
                        {
                            string grpCol = listDateRange1.SelectedItem.GroupCol;
                            cmd.Parameters.Add("@GrpBy", SqlDbType.Text).Value = listDateRange1.SelectedItem.GroupCol;
                        }
                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();
                        DtCriterias.Load(cmd.ExecuteReader());
                        con.Close();
                        customGridView1.Columns.Clear();
                        customGridControl1.DataSource = DtCriterias;
                    }
                }
                if (string.IsNullOrEmpty(this.GridLayoutFileName) || this.KontoView == null) return;

                KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, this.KontoView);

                this.ActiveControl = customGridControl1;


                if (DtCriterias.Rows.Count == 0)
                    listAction1.EditDeleteDisabled(false);
                else
                {
                    if (customGridView1.Columns.ColumnByFieldName("Id") != null && customGridView1.Columns.ColumnByFieldName("VoucherId") != null)
                        listAction1.EditDeleteDisabled(true);
                    else
                        listAction1.EditDeleteDisabled(false);
                }

               // OrderList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Purchase Order List Error");
                MessageBoxAdv.Show(this, "Error While Generating List !!", "Exception ", ex.ToString());
            }
        }

        public override void RefreshGrid()
        {
          
        }

        public override void DeleteRec()
        {
            base.DeleteRec();

            if (customGridView1.FocusedRowHandle < 0) return;
            try
            {
                var _id = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "Id"));
                var _vid = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "VoucherId"));
                var _deleted = Convert.ToBoolean(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "IsDeleted"));
                if (_deleted)
                {
                    MessageBoxAdv.Show("Record Already in Deleted State", "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBoxAdv.Show(KontoGlobals.DeleteBeforeMsg, "Delete ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                using (var db = new KontoContext())
                {
                    var exist = db.ChallanTranses.Any(x => x.MiscId == _id && x.RefVoucherId == _vid && x.IsDeleted == false && x.IsActive == true);
                    if (exist)
                    {
                        MessageBoxAdv.Show("Order Exist In Challan.. Can not Delete Order", "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    var model = db.Ords.Find(_id);
                    model.IsDeleted = true;
                    var transes = db.OrdTranses.Where(x => x.OrdId == _id);
                    foreach (var item in transes)
                    {
                        item.IsDeleted = true;
                    }
                    db.SaveChanges();
                    customGridView1.DeleteRow(customGridView1.FocusedRowHandle);
                    MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Po delete");
                MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
            }
        }

        public override void Print()
        {
            base.Print();
            if (this.customGridView1.FocusedRowHandle <= -1) return;
            if (KontoView.Columns.ColumnByFieldName("Id") != null)
            {
                if (KontoView.Columns.ColumnByFieldName("IsDeleted") != null)
                {
                    if (Convert.ToBoolean(this.KontoView.GetRowCellValue(this.KontoView.FocusedRowHandle, "IsDeleted")))
                    {
                        return;
                    }
                }
                var id = this.KontoView.GetRowCellValue(this.KontoView.FocusedRowHandle, "OrderNo").ToString();

                var frm = new DocPrintParaView(VoucherTypeEnum.PurchaseOrder, "Purchase Order Print",id,id,"PORD", "OrdId");
                frm.EditKey = Convert.ToInt32(this.KontoView.GetRowCellValue(this.KontoView.FocusedRowHandle, "Id"));
                frm.ShowDialog();


            }

           
          

        }


        public object OrderList()
        {

            using (var db = new KontoContext()) {
                db.Database.CommandTimeout = 0;

                var lst = (from od in db.Ords
                           join ac in db.Accs on od.AccId equals ac.Id
                           join v in db.Vouchers on od.VoucherId equals v.Id
                           join rcd in (from ch in db.Challans // if order ref from challan
                                        join ct in db.ChallanTranses on ch.Id equals ct.ChallanId
                                        join vc in db.Vouchers on ct.RefVoucherId equals vc.Id
                                        where !ch.IsDeleted && !ct.IsDeleted && ct.MiscId != null
                                        && vc.VTypeId == (int)VoucherTypeEnum.PurchaseOrder
                                        group ct by (int)ct.MiscId into g
                                        select new
                                        {
                                            OrderId = g.Key,
                                            Qty = g.Sum(x => x.Qty)
                                        }
                                        ) on od.Id equals rcd.OrderId into chjoin
                           from ch1 in chjoin.DefaultIfEmpty()
                           join bl in (from ch in db.Bills // if order ref direct in invoice
                                       join ct in db.BillTrans on ch.Id equals ct.BillId
                                       join vc in db.Vouchers on ct.RefVoucherId equals vc.Id
                                       where !ch.IsDeleted && !ct.IsDeleted && ct.RefId != null
                                       && vc.VTypeId == (int)VoucherTypeEnum.PurchaseOrder
                                       group ct by (int)ct.RefId into g
                                       select new
                                       {
                                           OrderId = g.Key,
                                           Qty = g.Sum(x => x.Qty)
                                       }
                                         ) on od.Id equals bl.OrderId into invjoin
                           from inv in invjoin.DefaultIfEmpty()

                           where v.VTypeId == (int)VoucherTypeEnum.PurchaseOrder && !od.IsDeleted & od.IsActive
                           && od.VoucherDate >= listDateRange1.FromDate && od.VoucherDate <= listDateRange1.ToDate
                           && od.CompId == KontoGlobals.CompanyId
                           select new { od, ch1, inv }
                           ).AsQueryable();

                var fldlist = "new (od.Id,DateTime.ParseExact(od.VoucherDate,\"yyyyMMdd\", CultureInfo.CurrentCulture),ac.AccName)";
                var lsts = lst.Select(fldlist);


                return lsts;
            }

            
        }

    }
}
