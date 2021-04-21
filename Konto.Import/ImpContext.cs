using Keysoft.Erp.Data.Models;


using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace Keysoft.Erp.Data
{
    public partial class ImpContext : DbContext
    {
        static ImpContext()
        {

            Database.SetInitializer<ImpContext>(null);
        }
        public ImpContext(SqlConnection con) : base(con, contextOwnsConnection: false)
        {
            this.Database.CommandTimeout = 0;
        }
        
        public ImpContext()
          : base("Name=ImpContext")
        {
            this.Database.CommandTimeout = 0;
        }
      
        public string UserIpAddress
        {

            get
            {
                IPHostEntry IPHost = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
                foreach (var ipAddress in IPHost.AddressList)
                {
                    return ipAddress.ToString();
                }

                return "NA";
            }
        }
        object GetPrimaryKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry =
                ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            var abd = objectStateEntry.EntitySet.Name;
            if (objectStateEntry.EntityKey.EntityKeyValues == null)
                return 0;
            return objectStateEntry.EntityKey.EntityKeyValues[0].Value;

        }

        private string GetTableName(DbEntityEntry entry)
        {
            var objectStateEntry =
                ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            return objectStateEntry.EntitySet.Name;
        }
        public Func<DateTime> TimestampProvider { get; set; } = ()
            => DateTime.UtcNow;

       

       

        public DbSet<ClosingEntry> ClosingEntries { get; set; }

        public DbSet<AcGroup> AcGroup { get; set; }

        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseTrans> PurchaseTranses { get; set; }
        public DbSet<PurchaseAddOn> PurchaseAdonOn { get; set; }
        public DbSet<IRN> IRNs { get; set; }
        public DbSet<Ledger> Ledgers { get; set; }
        public DbSet<account> Accounts { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<VoucherType> VoucherTypes { get; set; }
        public DbSet<SalesPurchaseFooter> SalesPurchaseFooterTranses { get; set; }

        public DbSet<PurchaseBill> PurchaseBills { get; set; }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleTrans> SaleTranses { get; set; }
        public DbSet<SaleAddon> SaleAddons { get; set; }

        public DbSet<SaleRet> SaleRets { get; set; }
        public DbSet<SaleRetTrans> SaleRetTranses { get; set; }
        public DbSet<SaleRetAddon> SaleRetAddons { get; set; }
        public DbSet<SalesRetBill> SaleRetBills { get; set; }

        public DbSet<PurchaseRet> PurchaseRets { get; set; }
        public DbSet<PurchaseRetTrans> PurchaseRetTranses { get; set; }
        public DbSet<PurchaseRetAdOn> PurchaseRetAdons { get; set; }
        public DbSet<PurchaseRetBill> PurchaseRetBills { get; set; }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankTrans> BankTranses { get; set; }
        public DbSet<BankBill> BankBills { get; set; }
        public DbSet<BankBillAdjust> BankBillAdjusts { get; set; }

        public DbSet<Cash> Cashes { get; set; }
        public DbSet<CashTrans> CashTranses { get; set; }
        public DbSet<CashBill> CashBills { get; set; }
        public DbSet<CashBillAdjust> CashBillAdjusts { get; set; }

        public DbSet<Jv> Jvs { get; set; }
        public DbSet<JvTrans> JvTranses { get; set; }
        public DbSet<JvBill> JvBills { get; set; }

        public DbSet<CrDrNote> CrDrNotes { get; set; }
        public DbSet<CrDrNoteTrans> CrdrNoteTranses { get; set; }
        public DbSet<CrDrNoteAddon> CrdrNoteAddOns { get; set; }
        public DbSet<CrDrBill> CrDrBills { get; set; }

        public DbSet<BtoB> B2Bs { get; set; }
        public DbSet<BtoBTrans> B2bTranses {get;set;}

        public DbSet<OpSales> OpSales { get; set; }
        public DbSet<OpPurchase> OpPurchases { get; set; }

        public DbSet<KeyYear> FinYears { get; set; }
        public DbSet<OpAccount> OpAccounts { get; set; }

        public DbSet<StockData> StockDatas { get; set; }

      

        public DbSet<Po> Pos { get; set; }
        public DbSet<PoTrans> PoTranses { get; set; }

        public DbSet<So> Sos { get; set; }
        public DbSet<SoTrans> SoTranses { get; set; }
        public DbSet<SoDelv> SoDelvs { get; set; }

        public DbSet<Pc> Pcs { get; set; }
        public DbSet<PcTrans> PcTranses { get; set; }
        public DbSet<PcTransD> PcTransDs { get; set; }

        public DbSet<Sc> Scs { get; set; }
        public DbSet<ScTrans> ScTranses { get; set; }
        public DbSet<ScTransD> ScTransDs { get; set; }

        public DbSet<Issue> Issues { get; set; }
        public DbSet<IssueTrans> IssueTranses { get; set; }

        public DbSet<Uom> Uoms { get; set; }
        public DbSet<Color> Colors { get; set; }

        public DbSet<LmsProd> LmsProds { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Screen> Screens { get; set; }
        public DbSet<Grade> Grades { get; set; }

        public DbSet<ItemCat> ItemCats { get; set; }

        public DbSet<Part_Payment> PartPayments { get; set; }

        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Transport> Transports { get; set; }

        public DbSet<SmsMaster> SmsMasters { get; set; }

        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<TaxPayersEWBSessionDB> TaxPayers { get; set; }

        public DbSet<MachineIssue> MachineIssues { get; set; }
        public DbSet<MachineIssueTrans> MachineIssueTranss { get; set; }
        public DbSet<MachineIssueTransd> MachineIssueTransds { get; set; }

        #region YarnDyeing
       
        public DbSet<TexProd> TexProds { get; set; }

        public DbSet<Batch> Batches { get; set; }
        public DbSet<JobCard> JobCards { get; set; }
        public DbSet<VatClass> VatClasses { get; set; }

        #endregion


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            


            modelBuilder.Entity<ClosingEntry>().ToTable("closingEntry");
            modelBuilder.Entity<AcGroup>().ToTable("acgroup");
            modelBuilder.Entity<StockData>().ToTable("StockData");

            modelBuilder.Entity<Purchase>().ToTable("Purchase");
            modelBuilder.Entity<PurchaseAddOn>().ToTable("purchase_addon");
            modelBuilder.Entity<PurchaseTrans>().ToTable("purchase_trans");
            modelBuilder.Entity<PurchaseBill>().ToTable("purchasebill");

            modelBuilder.Entity<PurchaseRet>().ToTable("Purchaseret");
            modelBuilder.Entity<PurchaseRetAdOn>().ToTable("purchaseRet_addon");
            modelBuilder.Entity<PurchaseRetTrans>().ToTable("purchaseRet_trans");
            modelBuilder.Entity<PurchaseRetBill>().ToTable("Purret_bill");

            modelBuilder.Entity<Sale>().ToTable("sales");
            modelBuilder.Entity<SaleAddon>().ToTable("sales_addon");
            modelBuilder.Entity<SaleTrans>().ToTable("sales_trans");

            modelBuilder.Entity<SaleRet>().ToTable("salesret");
            modelBuilder.Entity<SaleRetAddon>().ToTable("salesret_addon");
            modelBuilder.Entity<SaleRetTrans>().ToTable("salesret_trans");
            modelBuilder.Entity<SalesRetBill>().ToTable("saleret_bill");

            modelBuilder.Entity<account>().ToTable("account");

            modelBuilder.Entity<account>()
            .Property(e => e.account_Id)
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<Voucher>().ToTable("voucher");
            modelBuilder.Entity<VoucherType>().ToTable("voucher_type");
            modelBuilder.Entity<Ledger>().ToTable("ledger");
            modelBuilder.Entity<SalesPurchaseFooter>().ToTable("key_Sales_Purchase_Footer");

            modelBuilder.Entity<Bank>().ToTable("bank");
            modelBuilder.Entity<BankTrans>().ToTable("bank_trans");
            modelBuilder.Entity<BankBill>().ToTable("bank_bill");
            modelBuilder.Entity<BankBillAdjust>().ToTable("bank_bill_adjust");

            modelBuilder.Entity<Cash>().ToTable("cash");
            modelBuilder.Entity<CashTrans>().ToTable("cash_trans");
            modelBuilder.Entity<CashBill>().ToTable("cash_bill");
            modelBuilder.Entity<CashBillAdjust>().ToTable("cash_bill_adjust");

            modelBuilder.Entity<Jv>().ToTable("jv");
            modelBuilder.Entity<JvTrans>().ToTable("jv_trans");
            modelBuilder.Entity<JvBill>().ToTable("Jv_bill");
           

            modelBuilder.Entity<CrDrNote>().ToTable("crdrnote");
            modelBuilder.Entity<CrDrNoteAddon>().ToTable("crdrnote_addon");
            modelBuilder.Entity<CrDrNoteTrans>().ToTable("crdrnote_trans");
            modelBuilder.Entity<CrDrBill>().ToTable("crdr_bill");

            modelBuilder.Entity<BtoB>().ToTable("btob");
            modelBuilder.Entity<BtoBTrans>().ToTable("btob_trans");

            modelBuilder.Entity<OpPurchase>().ToTable("op_purchase");
            modelBuilder.Entity<OpSales>().ToTable("op_sales");

            modelBuilder.Entity<KeyYear>().ToTable("keyyear");
            modelBuilder.Entity<OpAccount>().ToTable("opaccount");

            modelBuilder.Entity<Po>().ToTable("po");
            modelBuilder.Entity<PoTrans>().ToTable("po_trans");

            modelBuilder.Entity<Pc>().ToTable("pc");
            modelBuilder.Entity<PcTrans>().ToTable("pc_trans");
            modelBuilder.Entity<PcTransD>().ToTable("pc_trans_d");

            modelBuilder.Entity<So>().ToTable("so");
            modelBuilder.Entity<SoTrans>().ToTable("so_trans");

            modelBuilder.Entity<Sc>().ToTable("sc");
            modelBuilder.Entity<ScTrans>().ToTable("sc_trans");
            modelBuilder.Entity<ScTransD>().ToTable("sc_trans_d");

            modelBuilder.Entity<Issue>().ToTable("IssueMaster");
            modelBuilder.Entity<IssueTrans>().ToTable("IssueTran");

            modelBuilder.Entity<MachineIssue>().ToTable("MacIssue");
            modelBuilder.Entity<MachineIssueTrans>().ToTable("MacIssItem_Trans");
            modelBuilder.Entity<MachineIssueTransd>().ToTable("MacIss_Trans");

            modelBuilder.Entity<Uom>().ToTable("unit");
            modelBuilder.Entity<Color>().ToTable("itemcol");

            modelBuilder.Entity<LmsProd>().ToTable("lms_prod");

            modelBuilder.Entity<Item>().ToTable("Item");

            modelBuilder.Entity<Screen>().ToTable("Screen");
            modelBuilder.Entity<Grade>().ToTable("grade");
            modelBuilder.Entity<ItemCat>().ToTable("itemcat");

            modelBuilder.Entity<Part_Payment>().ToTable("part_payment");

            modelBuilder.Entity<City>().ToTable("city");
            modelBuilder.Entity<State>().ToTable("state");
            modelBuilder.Entity<Area>().ToTable("area");
            modelBuilder.Entity<Transport>().ToTable("trans");

            
            modelBuilder.Entity<TexProd>().ToTable("texprod");
            modelBuilder.Entity<Batch>().ToTable("Batch");
            modelBuilder.Entity<JobCard>().ToTable("yd.job_card");

            modelBuilder.Entity<SmsMaster>().ToTable("SMSMaster");

            modelBuilder.Entity<AuditLog>().ToTable("AuditLog");

            modelBuilder.Entity<IRN>().ToTable("einvoice");

            modelBuilder.Entity<CrDrNoteTrans>().Property(x => x.Qty).HasPrecision(18, 3);
            modelBuilder.Entity<Item>().Property(x => x.BeamWeight).HasPrecision(18, 4);

            modelBuilder.Entity<TexProd>().Property(x => x.CartonWeight).HasPrecision(11, 4);
            modelBuilder.Entity<TexProd>().Property(x => x.GrossWeight).HasPrecision(11, 4);
            modelBuilder.Entity<TexProd>().Property(x => x.TareWeight).HasPrecision(11, 4);
            modelBuilder.Entity<TexProd>().Property(x => x.CopsWeight).HasPrecision(11, 4);
            modelBuilder.Entity<TexProd>().Property(x => x.NetWeight).HasPrecision(11, 4);

            modelBuilder.Entity<StockData>().Property(x => x.InQty).HasPrecision(11, 4);
            modelBuilder.Entity<StockData>().Property(x => x.OutQty).HasPrecision(11, 4);

            modelBuilder.Entity<Sc>().Property(x => x.Rate).HasPrecision(18, 3);
            modelBuilder.Entity<Sc>().Property(x => x.TotalQty).HasPrecision(18, 4);
            modelBuilder.Entity<ScTrans>().Property(x => x.Qty).HasPrecision(18, 4);
            modelBuilder.Entity<ScTransD>().Property(x => x.NetWeight).HasPrecision(11, 4);
            modelBuilder.Entity<ScTransD>().Property(x => x.GrossWeight).HasPrecision(11, 4);
            modelBuilder.Entity<ScTransD>().Property(x => x.TareWeight).HasPrecision(11, 4);

            modelBuilder.Entity<SaleTrans>().Property(x => x.Qty).HasPrecision(18, 4);
            modelBuilder.Entity<Sale>().Property(x => x.TotalQty).HasPrecision(18, 4);
            modelBuilder.Entity<SaleAddon>().Property(x => x.DefaultPer).HasPrecision(18, 3);
            modelBuilder.Entity<PurchaseAddOn>().Property(x => x.DefaultPer).HasPrecision(18, 3);
            modelBuilder.Entity<SaleRetAddon>().Property(x => x.DefaultPer).HasPrecision(18, 3);
            modelBuilder.Entity<PurchaseRetAdOn>().Property(x => x.DefaultPer).HasPrecision(18, 3);

            modelBuilder.Entity<SaleRet>().Property(x => x.TotalQty).HasPrecision(18, 4);
            modelBuilder.Entity<SaleTrans>().Property(x => x.Rate).HasPrecision(18, 3);
            modelBuilder.Entity<SaleRetTrans>().Property(x => x.Rate).HasPrecision(18, 3);

            modelBuilder.Entity<PurchaseTrans>().Property(x => x.Qty).HasPrecision(18, 4);
            modelBuilder.Entity<Purchase>().Property(x => x.TotalQty).HasPrecision(18, 4);
            modelBuilder.Entity<PurchaseRet>().Property(x => x.TotalQty).HasPrecision(18, 4);
            modelBuilder.Entity<PurchaseTrans>().Property(x => x.Rate).HasPrecision(18, 3);
            modelBuilder.Entity<PurchaseRetTrans>().Property(x => x.Rate).HasPrecision(18, 3);
            modelBuilder.Entity<PurchaseRetTrans>().Property(x => x.Qty).HasPrecision(18, 4);

            modelBuilder.Entity<MachineIssue>().Property(x => x.Qty).HasPrecision(24, 3);
            modelBuilder.Entity<MachineIssue>().Property(x => x.Rate).HasPrecision(18, 3);
            modelBuilder.Entity<MachineIssue>().Property(x => x.TotalQty).HasPrecision(15, 3);
            modelBuilder.Entity<MachineIssueTrans>().Property(x => x.Qty).HasPrecision(18, 3);
            modelBuilder.Entity<MachineIssueTrans>().Property(x => x.Rate).HasPrecision(18, 3);
            modelBuilder.Entity<MachineIssueTrans>().Property(x => x.Amount).HasPrecision(18, 3);
            modelBuilder.Entity<MachineIssueTransd>().Property(x => x.Weight).HasPrecision(24, 4);
            modelBuilder.Entity<MachineIssueTransd>().Property(x => x.Rate).HasPrecision(18, 3);

            modelBuilder.Entity<MachineIssue>().Property(x => x.Qty).HasPrecision(24, 3);
            modelBuilder.Entity<MachineIssue>().Property(x => x.Rate).HasPrecision(18, 3);
            modelBuilder.Entity<MachineIssue>().Property(x => x.TotalQty).HasPrecision(15, 3);
            modelBuilder.Entity<MachineIssueTrans>().Property(x => x.Qty).HasPrecision(18, 3);
            modelBuilder.Entity<MachineIssueTrans>().Property(x => x.Rate).HasPrecision(18, 3);
            modelBuilder.Entity<MachineIssueTrans>().Property(x => x.Amount).HasPrecision(18, 3);
            modelBuilder.Entity<MachineIssueTransd>().Property(x => x.Weight).HasPrecision(24, 4);
            modelBuilder.Entity<MachineIssueTransd>().Property(x => x.Rate).HasPrecision(18, 3);

            base.OnModelCreating(modelBuilder);
        }
    }

    public partial class ImpContext : DbContext
    {
        public ImpContext(String connString)
            : base(connString)
        {
        }
    }
}