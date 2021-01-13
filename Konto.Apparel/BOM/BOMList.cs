using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;

using Syncfusion.Windows.Forms;
using System.Windows.Forms;
using Serilog;
using GrapeCity.ActiveReports;
using System.IO;
using Syncfusion.Windows.Forms.Tools;

namespace Konto.Apparel.BOM
{
    public partial class BOMList : ListBaseView
    {

        public BOMList()
        {
            InitializeComponent();

            this.GridLayoutFileName = KontoFileLayout.BOM_List_Layout;
            listDateRange1.GetButtonClick += ListDateRange1_GetButtonClick;
            listAction1.EditDeleteDisabled(false);
            this.ReportPrint = true;
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
                        cmd.Parameters.Add("@compid", SqlDbType.Int).Value = KontoGlobals.CompanyId;
                        cmd.Parameters.Add("@branchid", SqlDbType.Int).Value = KontoGlobals.BranchId;
                        cmd.Parameters.Add("@yearid", SqlDbType.Int).Value = KontoGlobals.YearId;

                        cmd.Parameters.Add("@VTypeId", SqlDbType.Int).Value = (int)VoucherTypeEnum.BOM;

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
                        customGridView1.ShowLoadingPanel();
                        customGridView1.Columns.Clear();
                        customGridControl1.DataSource = DtCriterias;
                        customGridView1.HideLoadingPanel();
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




            }
            catch (Exception ex)
            {
                Log.Error(ex, "bom List Error");
                MessageBoxAdv.Show(this, "Error While Generating List !!", "Exception ", ex.ToString());

            }
        }

        public override void RefreshGrid()
        {
           
        }
        public override void Print()
        {
            base.Print();
            //if (this.customGridView1.FocusedRowHandle < 0) return;
            //if (KontoView.Columns.ColumnByFieldName("Id") != null)
            //{
            //    if (KontoView.Columns.ColumnByFieldName("IsDeleted") != null)
            //    {
            //        if (Convert.ToBoolean(this.KontoView.GetRowCellValue(this.KontoView.FocusedRowHandle, "IsDeleted")))
            //        {
            //            return;
            //        }
            //    }
            //    var id = this.KontoView.GetRowCellValue(this.KontoView.FocusedRowHandle, "VoucherNo").ToString();

            //    PageReport rpt = new PageReport();

            //    rpt.Load(new FileInfo("reg\\doc\\barcode.rdlx"));

            //    rpt.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

            //    GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(rpt);

            //    doc.Parameters["reportid"].CurrentValue = Convert.ToInt32(this.KontoView.GetRowCellValue(this.KontoView.FocusedRowHandle, "Id")); 
            //    var frm = new KontoRepViewer(doc);
            //    frm.Text = "Print";
            //    var _tab = this.Parent.Parent as TabControlAdv;
            //    if (_tab == null) return;
            //    var pg1 = new TabPageAdv();
            //    pg1.Text = "Barcode Print";
            //    _tab.TabPages.Add(pg1);
            //    _tab.SelectedTab = pg1;
            //    frm.TopLevel = false;
            //    frm.Parent = pg1;
            //    frm.Location = new System.Drawing.Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
            //    frm.Show();// = true;


            
        }
        public override void CancleRecord()
        {
            base.CancleRecord();

            if (customGridView1.SelectedRowsCount <= 0) return;
            var drs = customGridView1.GetSelectedRows();
            if (MessageBoxAdv.Show(KontoGlobals.CancelBeforeMsg, "Delete ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            using (var db = new KontoContext())
            {
                using (var _tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var rowno in drs)
                        {

                            var row = customGridView1.GetDataRow(rowno);

                            var _id = Convert.ToInt32(row["Id"]);
                            
                            var _vid = Convert.ToInt32(row["VoucherId"]);
                            
                            var _deleted = Convert.ToBoolean(row["IsDeleted"]);

                            if (_deleted)
                            {
                                MessageBoxAdv.Show("Record Already in Deleted State", "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }



                            var model = db.Boms.Find(_id);
                            model.IsActive = false;


                            var trans = db.BOMTranses.Where(x => x.BOMId == _id).ToList();
                            foreach (var item in trans)
                            {
                                item.IsActive = false;
                            }
                        }

                        customGridView1.DeleteSelectedRows();
                        db.SaveChanges();
                        _tran.Commit();
                        MessageBoxAdv.Show(KontoGlobals.CancelAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "BOM cancel");
                        MessageBoxAdv.Show(this, "Error While Cancellation !!", "Exception ", ex.ToString());
                    }
                }


            }
        }
        public override void DeleteRec()
        {
            base.DeleteRec();

            if (customGridView1.SelectedRowsCount <= 0) return;
            var drs = customGridView1.GetSelectedRows();

            if (MessageBoxAdv.Show(KontoGlobals.DeleteBeforeMsg, "Delete ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            using (var db = new KontoContext())
            {
                using (var _tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var rowno in drs)
                        {
                            var row = customGridView1.GetDataRow(rowno);

                            var _id = Convert.ToInt32(row["Id"]); 
                            var _vid = Convert.ToInt32(row["VoucherId"]); 
                            var _deleted = Convert.ToBoolean(row["IsDeleted"]); 

                            if (_deleted)
                            {
                                MessageBoxAdv.Show("Record Already in Deleted State", "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }



                            var model = db.Boms.Find(_id);
                            model.IsDeleted = true;


                            var trans = db.BOMTranses.Where(x => x.BOMId == _id).ToList();
                            foreach (var item in trans)
                            {
                                item.IsDeleted = true;
                            }
                        }

                        customGridView1.DeleteSelectedRows();
                        db.SaveChanges();
                        _tran.Commit();
                        MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                        Log.Error(ex, "Sale Invoice delete");
                        MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
                    }
                }
            }

        }
    }
}
