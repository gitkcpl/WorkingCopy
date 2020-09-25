using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Shared.DataFreeze;
using Serilog;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Masters.Merge
{
    public partial class MergeIndex : KontoForm
    {
        public MergeIndex()
        {
            InitializeComponent();
            this.FormClosed += MergeIndex_FormClosed;
        }

        private void MergeIndex_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage != null)
            {
                var tabcontrol = tabpage.Parent as TabControlAdv;
                if (tabcontrol != null)
                    tabcontrol.TabPages.Remove(tabpage);
            }
        }
        private void MergeIndex_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Please take backup before merging");
            var frm = new DataFreezePass { };
            frm.ShowDialog();// this.Parent.Parent.Parent);
            if (frm.DialogResult == DialogResult.Cancel)
            {
                this.Close();               
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            KontoContext db = new KontoContext();
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (Convert.ToInt32(orgaccLookup.SelectedValue) != 0 && Convert.ToInt32(wrongaccLookup.SelectedValue) != 0)
                    {
                        int Id1 = Convert.ToInt32(orgaccLookup.SelectedValue);
                        int Id2 = Convert.ToInt32(wrongaccLookup.SelectedValue);
                        int compId = KontoGlobals.CompanyId;
                        int yearId = KontoGlobals.YearId;

                        var accBal = db.AccBals.Where(k => k.AccId == Id2).ToList();

                        foreach (var item in accBal)
                        {
                            var accBal1 = db.AccBals.FirstOrDefault(k => k.AccId == Id1 && k.CompId == item.CompId && k.YearId == item.YearId);
                            accBal1.Bal += item.Bal;
                            accBal1.OpBal += item.OpBal;
                            accBal1.OpCredit += item.OpCredit;
                            accBal1.OpDebit += item.OpDebit;
                            accBal1.ModifyUser = KontoGlobals.UserName; 

                            db.SaveChanges();
                        }

                        string sql_1 = " DELETE FROM AccBal WHERE AccId = " + Id2; //"dbo.AccBal";
                        sql_1 += " UPDATE AccBank SET AccId = " + Id1 + " WHERE AccId = " + Id2; //"dbo.AccBank";
                        sql_1 += " UPDATE AccAddress SET AccId = " + Id1 + " WHERE AccId = " + Id2; 
                        sql_1 += " UPDATE AccOther SET AccId = " + Id1 + " WHERE AccId = " + Id2;  
                        sql_1 += " UPDATE AccOther SET OpStockAccId = " + Id1 + " WHERE OpStockAccId = " + Id2;  
                        sql_1 += " UPDATE AccOther SET IntAccId = " + Id1 + " WHERE IntAccId = " + Id2;  
                        sql_1 += " UPDATE AccOther SET IntTdsAccId = " + Id1 + " WHERE IntTdsAccId = " + Id2;  
                        sql_1 += " UPDATE AccOther SET TcsAccId = " + Id1 + " WHERE TcsAccId = " + Id2; 
                        sql_1 += " UPDATE AccOther SET TdsAccId = " + Id1 + " WHERE TdsAccId = " + Id2; 
                        sql_1 += " UPDATE AccOther SET DepAccId = " + Id1 + " WHERE DepAccId = " + Id2;  
                       
                        sql_1 += " UPDATE Ord SET AccId = " + Id1 + " WHERE AccId = " + Id2; //"dbo.BillMain";
                        sql_1 += " UPDATE OrdDelv SET AccId = " + Id1 + " WHERE AccId = " + Id2; //"dbo.BillMain";

                        sql_1 += " UPDATE Billmain SET BookAcId = " + Id1 + " WHERE BookAcId = " + Id2; //"dbo.BillMain";
                        sql_1 += " UPDATE Billmain SET AccId = " + Id1 + " WHERE AccId = " + Id2; //"dbo.BillMain";
                        sql_1 += " UPDATE Billmain SET TransId = " + Id1 + " WHERE TransId = " + Id2; //"dbo.BillMain";
                        sql_1 += " UPDATE Billmain SET DelvAccId = " + Id1 + " WHERE DelvAccId = " + Id2; //"dbo.BillMain";
                        sql_1 += " UPDATE Billmain SET AgentId = " + Id1 + " WHERE AgentId = " + Id2; //"dbo.BillMain";
                        sql_1 += " UPDATE Billmain SET HasteId = " + Id1 + " WHERE HasteId = " + Id2; //"dbo.BillMain";
                        sql_1 += " UPDATE BillTrans SET ToAccId = " + Id1 + " WHERE ToAccId = " + Id2; //"dbo.BillTrans";
                        sql_1 += " UPDATE BillRef SET AgentId = " + Id1 + " WHERE AgentId= " + Id2; //"dbo.BillTrans";
                        sql_1 += " UPDATE BillRef SET AccountId = " + Id1 + " WHERE AccountId = " + Id2; //"dbo.BillRef"
                        sql_1 += " UPDATE BillDelv SET AccId = " + Id1 + " WHERE AccId = " + Id2;  

                        sql_1 += " UPDATE Challan SET AccId = " + Id1 + " WHERE AccId = " + Id2; //"dbo.Challan" 
                        sql_1 += " UPDATE Challan SET AgentId = " + Id1 + " WHERE AgentId= " + Id2; //"dbo.Challan" 
                        sql_1 += " UPDATE Challan SET BookAcId = " + Id1 + " WHERE BookAcId = " + Id2; //"dbo.Challan"
                        sql_1 += " UPDATE Challan SET TransId = " + Id1 + " WHERE TransId = " + Id2; //"dbo.Challan"
                        sql_1 += " UPDATE Challan SET DelvAccId = " + Id1 + " WHERE DelvAccId = " + Id2; //"dbo.Challan"

                        sql_1 += " UPDATE ChallanDelv SET AccId = " + Id1 + " WHERE AccId = " + Id2;  

                         sql_1 += " UPDATE LedgerTrans SET AccountId = " + Id1 + " WHERE AccountId = " + Id2; 
                        sql_1 += " UPDATE WeftItem SET AccId = " + Id1 + " WHERE AccId = " + Id2; //"dbo.WeftItem"

                        sql_1 += " UPDATE Template SET AccId = " + Id1 + " WHERE AccId = " + Id2;  
                         
                        using (var con = new SqlConnection(KontoGlobals.Conn))
                        {
                            con.Open();
                            var cmd = new SqlCommand(sql_1, con);
                            cmd.ExecuteNonQuery();
                             
                            con.Close();
                        }

                        //Delete wrong Account
                        AccModel acId = db.Accs.Find(Id2);
                        acId.ModifyDate = DateTime.Now;
                        acId.ModifyUser = KontoGlobals.UserName;
                        acId.IsDeleted = true;
                        db.SaveChanges();
 
                    }
                    if (Convert.ToInt32(OrgproductLookup.SelectedValue) != 0 && Convert.ToInt32(wrngproductLookup.SelectedValue) != 0)
                    {
                        //Delete wrong Product
                        int ItemId1 = Convert.ToInt32(OrgproductLookup.SelectedValue);
                        int ItemId2 = Convert.ToInt32(wrngproductLookup.SelectedValue);

                        var prBal = db.StockBals.Where(k => k.ProductId == ItemId2).ToList();
                        foreach (var item in prBal)
                        {
                            var prBal1 = db.StockBals.FirstOrDefault(k => k.ProductId == ItemId1 && k.CompanyId == item.CompanyId && k.YearId == item.YearId && k.BranchId == item.BranchId);
                            prBal1.BalQty += item.BalQty;
                            prBal1.BalNos += item.BalNos;
                            prBal1.OpQty += item.OpQty;
                            prBal1.OpNos += item.OpNos;
                            prBal1.IssueNo += item.IssueNo;
                            prBal1.IssueQty += item.IssueQty;
                            prBal1.RcptNos += item.RcptNos;
                            prBal1.RcptQty += item.RcptQty;
                            prBal1.ModifyUser = KontoGlobals.UserName;
                            //await prodbalRep.UpdateAsyn(prBal1, prBal1.Id);
                            db.SaveChanges();
                        }
                        // string sql_6 = " UPDATE ProductBal SET ProductId = " + ItemId1 + " WHERE ProductId = " + ItemId2; //"dbo.ProductBal";
                        // string sql_9 = " UPDATE JobCard SET ProductId = " + ItemId1 + " WHERE ProductId = " + ItemId2; //"dbo.JobCard";
                        //string sql_10 = " UPDATE JobCardTrans SET ItemId = " + ItemId1 + " WHERE ItemId = " + ItemId2; //"dbo.JobCardTrans";
                        //string sql_11 = " UPDATE LoadingTrans SET ProductId = " + ItemId1 + " WHERE ProductId = " + ItemId2; //"dbo.LoadingTrans";
                        //string sql_12 = " UPDATE BOMTrans SET ProductId = " + ItemId1 + " WHERE ProductId = " + ItemId2; //"dbo.BOMTrans";
                        //string sql_15 = " UPDATE RCPUITrans SET ProductId = " + ItemId1 + " WHERE ProductId = " + ItemId2; //"dbo.RCPUITrans";

                        string sql_1 = " UPDATE BillTrans SET ProductId = " + ItemId1 + " WHERE ProductId = " + ItemId2; //"dbo.BillTrans";
                        sql_1 += " UPDATE ChallanTrans SET ProductId = " + ItemId1 + " WHERE ProductId = " + ItemId2; //"dbo.ChallanTrans";
                        sql_1 += " UPDATE OrdTrans SET ProductId = " + ItemId1 + " WHERE ProductId = " + ItemId2; //"dbo.OrdTrans";
                        sql_1 += " UPDATE StockTrans SET ItemId = " + ItemId1 + " WHERE ItemId = " + ItemId2; //"dbo.StockTrans";
                        sql_1 += " UPDATE WeftItem SET ProductId = " + ItemId1 + " WHERE ProductId = " + ItemId2; //"dbo.WeftItem";
                                                                                                                  
                        sql_1 += " UPDATE Prod SET ProductId = " + ItemId1 + " WHERE ProductId = " + ItemId2; //"dbo.Prod";
                        sql_1 +=  " UPDATE ProdOut SET ProductId = " + ItemId1 + " WHERE ProductId = " + ItemId2; //"dbo.ProdOut";
                                                                                                                  
                        sql_1 += " UPDATE Batch SET ItemId = " + ItemId1 + " WHERE ItemId = " + ItemId2; //"dbo.Batch";
                        sql_1 += " UPDATE BatchTrans SET ItemId = " + ItemId1 + " WHERE ItemId = " + ItemId2; //"dbo.BatcTrans";
                                                                                                            
                        sql_1 += " UPDATE JobReceipt SET ProductId = " + ItemId1 + " WHERE ProductId = " + ItemId2; //"dbo.JobReceipt";
                        sql_1 += " UPDATE BillRef SET ItemId = " + ItemId1 + " WHERE ItemId = " + ItemId2; //"dbo.BillRef";
                        sql_1 += " UPDATE ColorSet SET ItemId = " + ItemId1 + " WHERE ItemId = " + ItemId2; //"dbo.BillRef";

                        sql_1 += " UPDATE ProductPrice SET ProductId = " + ItemId1 + " WHERE ProductId = " + ItemId2; //"dbo.BillRef";
                        sql_1 += " UPDATE PImage SET ProductId = " + ItemId1 + " WHERE ProductId = " + ItemId2; //"dbo.BillRef";
                         
                        using (var con = new SqlConnection(KontoGlobals.Conn))
                        {
                            con.Open();
                            var cmd = new SqlCommand(sql_1, con);
                            cmd.ExecuteNonQuery();
                             
                            con.Close();
                        }

                        ProductModel itemId = db.Products.Find(ItemId2);
                        itemId.ModifyUser = KontoGlobals.UserName;
                        itemId.ModifyDate = DateTime.Now;
                        itemId.IsDeleted = true; 
                        db.SaveChanges();
                         
                    }
                     
                    transaction.Commit();
                    MessageBoxAdv.Show(KontoGlobals.SaveMessage, "Confirmation !!", MessageBoxButtons.OK);
                    orgaccLookup.SetEmpty();
                    wrongaccLookup.SetEmpty();
                    OrgproductLookup.SetEmpty();
                    wrngproductLookup.SetEmpty();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Log.Error(ex, "Account / Product Merge");
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void cancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
