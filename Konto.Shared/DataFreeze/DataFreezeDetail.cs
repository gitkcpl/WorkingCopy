using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Admin;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Shared.DataFreeze
{
    public partial class DataFreezeDetail : KontoForm
    {
        List<DataFreezeListDto> DFree = new List<DataFreezeListDto>();
        public DataFreezeDetail()
        {
            InitializeComponent();
            this.FormClosed += DataFreezeDetail_FormClosed;
        }

        private void DataFreezeDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage != null)
            {
                var tabcontrol = tabpage.Parent as TabControlAdv;
                if (tabcontrol != null)
                    tabcontrol.TabPages.Remove(tabpage);
            }
        }

        private void DataFreezeDetail_Load(object sender, EventArgs e)
        {
            FrmDateEdit.DateTime = DateTime.Now;
            ToDateEdit.DateTime = DateTime.Now;

            ShowDataFreezeAsync();
            // Show popup for Password
            var frm = new DataFreezePass { };

            frm.ShowDialog();// this.Parent.Parent.Parent);
            if (frm.DialogResult == DialogResult.No)
            {
                this.Close();
            }

            gridControl1.DataSource = DFree;
        }
        private   void ShowDataFreezeAsync()
        {
            try
            {

                var sp = new SysParaModel();
                var _db = new KontoContext();
                _db.Database.CommandTimeout = 0;
                  DFree = new List<DataFreezeListDto>(_db.Database.SqlQuery<DataFreezeListDto>(
                        "dbo.DataFreeze @CompanyId={0}", Convert.ToInt32(KontoGlobals.CompanyId)));
                if (DFree.Count == 0)
                {
                   
                   var  VTypeList = new List<VoucherTypeModel>(_db.VoucherTypes.ToList());
                    if (VTypeList.Count > 0)
                    {
                        List<DataFreezeModel> DataFList = new List<DataFreezeModel>();
                        foreach (var item in VTypeList)
                        {
                            var vtl = new DataFreezeModel();
                            vtl.CompanyID = Convert.ToInt32(KontoGlobals.CompanyId);
                            vtl.ModifyUser = KontoGlobals.UserName;
                            vtl.ModifyDate = DateTime.Now;
                            vtl.Freeze = false;
                            vtl.VoucherTypeID = item.Id;
                            vtl.FromDate = KontoGlobals.FromDate;
                            vtl.ToDate = KontoGlobals.ToDate;
                            DataFList.Add(vtl);
                        }
                        _db.DFreeze.AddRange(DataFList);
                        _db.SaveChanges();
                        ShowDataFreezeAsync();
                    }
                }
            }
            catch (Exception Ex)
            {
                string str = Ex.ToString();
            }
        }

        private void CheckAllBtn_Click(object sender, EventArgs e)
        {
            if (CheckAllBtn.Text == "Check All")
            {
                foreach (DataFreezeListDto item in DFree)
                {
                    item.Freeze = true;
                    CheckAllBtn.Text = "UnCheck All";
                }
            }
            else
            {
                foreach (var item in DFree)
                {
                    item.Freeze = false;
                    CheckAllBtn.Text = "Check All";
                    //cd.DFree.Add(item);
                }
            }
            gridControl1.DataSource = DFree;
        }

        private void okSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                KontoContext _db = new KontoContext();
                int fdate = Convert.ToInt32(FrmDateEdit.DateTime.ToString("yyyyMMdd"));
                int tdate = Convert.ToInt32(ToDateEdit.DateTime.ToString("yyyyMMdd"));
                foreach (var item in DFree)
                {  
                    var cds = _db.DFreeze.Where(p => p.Id == item.Id).FirstOrDefault();
                    if (item.Freeze != cds.Freeze)
                    {
                        cds.FromDate = fdate;
                        cds.ToDate = tdate;
                        cds.ModifyDate = DateTime.Now;
                        cds.ModifyUser = KontoGlobals.UserName;
                        cds.Freeze = item.Freeze;
                        cds.Pass = KontoGlobals.Pass;
                        cds.VoucherTypeID = item.VoucherTypeID;
                        _db.Entry(cds).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                _db.SaveChanges();
              //  System.Windows.MessageBox.Show("Record Updated Successfully..!!");
                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception Ex)
            {
                string str = Ex.ToString();
            }
        }

        private void cancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var frm = new DataFreezePassChange { };

            frm.ShowDialog();// this.Parent.Parent.Parent); 
        }
    }
}
