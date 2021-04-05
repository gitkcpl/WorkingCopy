using Konto.App.Shared;
using Konto.Data.Models.Admin;
using Konto.Data.Models.Apparel;
using Konto.Data.Models.Gstn;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Pos;
using Konto.Data.Models.Transaction;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Konto.Data
{
    public interface IKontoContext : IDisposable
    {

    }
    public class KontoContext : DbContext, IKontoContext
    {
        static KontoContext()
        {

            Database.SetInitializer<KontoContext>(null);
        }

        public KontoContext()
          : base(KontoGlobals.sqlConnectionString.ConnectionString)
        {
            this.Database.CommandTimeout = 0;
        }
        public KontoContext(string connectionString)
            : base(connectionString)
        {
            this.Database.CommandTimeout = 0;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<KontoContext, Migrations.Configuration>());

            //Database.SetInitializer<KontoContext>(null);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
            modelBuilder.Entity<BillTransModel>().Property(p => p.Rate).HasPrecision(18, 4);
            modelBuilder.Entity<BillTransModel>().Property(p => p.Qty).HasPrecision(18, 3);
            modelBuilder.Entity<ChallanTransModel>().Property(p => p.Rate).HasPrecision(18, 4);
            modelBuilder.Entity<ChallanTransModel>().Property(p => p.Qty).HasPrecision(18, 3);
            modelBuilder.Entity<BillModel>().Property(p => p.TcsPer).HasPrecision(18, 3);
            modelBuilder.Entity<AccOtherModel>().Property(p => p.TcsPer).HasPrecision(18, 3);

            modelBuilder.Entity<ItemBatch>().HasIndex(p => new { p.ProductId, p.BatchNo }).IsUnique();

            modelBuilder.Entity<ItemSerial>().HasIndex(p => new { p.ProductId, p.SerialNo }).IsUnique();

            // modelBuilder.Entity<AccModel>().Property(p => p.GSTDate).HasColumnType("datetime2");
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            TrackChanges();
            return await base.SaveChangesAsync(cancellationToken);
        }


        public override int SaveChanges()
        {
            TrackChanges();
            try
            {
                return base.SaveChanges();
            }
            catch (Exception ex)
            {

                var exp = ex as DbEntityValidationException;
                if (exp != null) {
                    var errorMessages = exp.EntityValidationErrors
                   .SelectMany(x => x.ValidationErrors)
                   .Select(x => x.ErrorMessage);

                    // Join the list to a single string.
                    var fullErrorMessage = string.Join("; ", errorMessages);

                    // Combine the original exception message with the new one.
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                    // Throw a new DbEntityValidationException with the improved exception message.
                    throw new DbEntityValidationException(exceptionMessage, exp.EntityValidationErrors);
                }
                else
                {
                    throw new Exception("Erron at Save", ex);
                }

            }

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

        public Func<DateTime> TimestampProvider { get; set; } = ()
           => DateTime.Now;

        object GetPrimaryKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry =
                ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            var abd = objectStateEntry.EntitySet.Name;
            return objectStateEntry.EntityKey.EntityKeyValues[0].Value;

        }

        private string GetTableName(DbEntityEntry entry)
        {
            var objectStateEntry =
                ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            return objectStateEntry.EntitySet.Name;
        }

        private void TrackChanges()
        {



            var modifiedEntities = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified).ToList();

            var now = DateTime.UtcNow;

            foreach (var change in modifiedEntities)
            {

                if (change.Entity is AuditedEntity)
                {
                    var primaryKey = GetPrimaryKeyValue(change);
                    var entityName = GetTableName(change);

                    foreach (var prop in change.OriginalValues.PropertyNames)
                    {
                        if (prop == "ModifyUser" || prop == "ModifyDate") continue;
                        string originalValue = string.Empty;
                        string currentValue = string.Empty;
                        if (change.OriginalValues[prop] != null)
                            originalValue = change.OriginalValues[prop].ToString();

                        if (change.CurrentValues[prop] != null)
                            currentValue = change.CurrentValues[prop].ToString();
                        if (originalValue != currentValue)
                        {
                            AuditLog log = new AuditLog()
                            {
                                EntityName = entityName,
                                PrimaryKeyValue = (int)primaryKey,
                                PropertyName = prop,
                                OldValue = originalValue,
                                NewValue = currentValue,
                                DateChanged = now,
                                UserName = KontoGlobals.UserName,
                                MenuId = KontoGlobals.MenuId

                            };

                            log.EntryMode = Convert.ToBoolean(change.Property("IsDeleted").CurrentValue)
                                ? "Delete"
                                : "Edit";

                            AuditLogs.Add(log);

                        }
                    }
                }
            }

            foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                if (entry.Entity is AuditedEntity)
                {
                    var auditable = entry.Entity as AuditedEntity;
                    if (entry.State == EntityState.Added)
                    {
                        auditable.RowId = Guid.NewGuid();
                        //auditable.CreatedBy = UserProvider;//
                        auditable.CreateDate = TimestampProvider();
                        auditable.CreateUser = KontoGlobals.UserName;
                        //auditable.UpdatedOn = TimestampProvider();
                    }
                    else
                    {
                        //auditable.UpdatedBy = UserProvider;
                        auditable.ModifyUser = KontoGlobals.UserName;
                        auditable.ModifyDate = TimestampProvider();
                    }
                    auditable.IpAddress = UserIpAddress;
                }
            }
        }

        #region "DbSet"

        #region "Admin"
        public DbSet<ErpModule> ErpModules { get; set; }
        public DbSet<UserMasterModel> UserMasters { get; set; }
        public DbSet<PermissionsModel> Permissions { get; set; }
        public DbSet<RolesModel> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Menu_PackageModel> Menu_Packages { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<SysParaModel> SysParas { get; set; }
        public DbSet<CompParaModel> CompParas { get; set; }
        public DbSet<ListPageModel> ListPages { get; set; }
        public DbSet<SPCollectionModel> SpCollections { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Gstr2ADump> Gstr2ADumps { get; set; }
        public DbSet<Gstr2ATransDump> Gstr2ATransDumps { get; set; }

        #endregion

        #region "Masters"
        public DbSet<CountryModel> Countries { get; set; }
        public DbSet<StateModel> States { get; set; }
        public DbSet<CityModel> Cities { get; set; }
        public DbSet<AreaModel> Areas { get; set; }
        public DbSet<BranchModel> Branches { get; set; }
        public DbSet<DivisionModel> Divisions { get; set; }
        public DbSet<CompModel> Companies { get; set; }
        public DbSet<AcGroupModel> AcGroupModels { get; set; }
        public DbSet<VoucherTypeModel> VoucherTypes { get; set; }
        public DbSet<VoucherPartyModel> VoucherParties { get; set; }
        public DbSet<VoucherBookModel> VoucherBooks { get; set; }
        public DbSet<VoucherItemModel> VoucherItems { get; set; }
        public DbSet<VoucherModel> Vouchers { get; set; }
        public DbSet<VchSetupModel> VchSetups { get; set; }
        public DbSet<RouteModel> Routes { get; set; }
        public DbSet<EmpModel> Emps { get; set; }
        public DbSet<ReportTypeModel> ReportTypes { get; set; }
        public DbSet<PartyGroupModel> PartyGroups { get; set; }
        public DbSet<DeducteeModel> Deductees { get; set; }
        public DbSet<NopModule> Nops { get; set; }
        public DbSet<NobModel> Nobs { get; set; }
        public DbSet<SpParaModel> SpParas { get; set; }
        public DbSet<ColorSetModel> Colorset { get; set; }
        public DbSet<SerialNumbersShelf> SerialNumbersShelfs { get; set; }
        public DbSet<CustomRepModel> Customreps { get; set; }
        public DbSet<ReportParaModel> ReportParas { get; set; }
        public DbSet<AccModel> Accs { get; set; }
        public DbSet<AccAddressModel> AccAddresses { get; set; }
        public DbSet<AccBankModel> AccBanks { get; set; }
        public DbSet<AccOtherModel> AccOthers { get; set; }
        public DbSet<AccBalModel> AccBals { get; set; }
        public DbSet<StyleModel> StyleModels { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<PriceModel> Prices { get; set; }
        public DbSet<StockBalModel> StockBals { get; set; }
        public DbSet<StoreModel> Stores { get; set; }
        public DbSet<FinYearModel> FinYears { get; set; }
        public DbSet<RPSetModel> RPSets { get; set; }
        public DbSet<GradeModel> Grades { get; set; }
        public DbSet<TermsModel> Terms { get; set; }
        public DbSet<WarpItemModel> WarpItems { get; set; }
        public DbSet<TransType> transTypes { get; set; }
        public DbSet<HasteModel> Hastes { get; set; }
        public DbSet<ProcessModel> Process { get; set; }
        public DbSet<TemplateModel> Templates { get; set; }
        public DbSet<EmailSmsLogModel> EmailSmsLogs { get; set; }

        public DbSet<LastSerialModel> LastSerials { get; set; }

        public DbSet<TemplateTrans> Templatetrans { get; set; }
        public DbSet<TempFieldModel> TempFields { get; set; }

        public DbSet<CostHeadModel> CostHeads {get;set;}

        #endregion
        #region product
        public DbSet<UomModel> Uoms { get; set; }
        public DbSet<ProductTypeModel> ProductTypes { get; set; }
        public DbSet<BrandModel> Brands { get; set; }
        public DbSet<PGroupModel> PGroups { get; set; }
        public DbSet<PSubGroupModel> PSubGroups { get; set; }
        public DbSet<PSizeModel> SizeModels { get; set; }
        public DbSet<ColorModel> ColorModels { get; set; }
        public DbSet<PCategroyModel> CategroyModels { get; set; }
        public DbSet<TaxModel> TaxMasters { get; set; }
        public DbSet<CatalogModel> Catalogs { get; set; }
        public DbSet<PImageModel> PImagies { get; set; }
        public DbSet<PFormulaModel> PFormulas { get; set; }
        public DbSet<WeftItemModel> WeftItems { get; set; }
        public DbSet<RefBankModel> RefBanks { get; set; }

        #endregion

        #region Transaction
        public DbSet<BillModel> Bills { get; set; }
        public DbSet<BillTransModel> BillTrans { get; set; }
        public DbSet<BillRefModel> BillRefs { get; set; }
        public DbSet<BillDelvModel> BillDelvs { get; set; }
        public DbSet<BtoBModel> BtoBs { get; set; }
        public DbSet<LedgerTransModel> Ledgers { get; set; }
        public DbSet<DataFreezeModel> DFreeze { get; set; }
        public DbSet<ChallanModel> Challans { get; set; }
        public DbSet<ChlDelvModel> ChallanDelvs { get; set; }
        public DbSet<ChallanTransModel> ChallanTranses { get; set; }
        public DbSet<StockTransModel> StockTranses { get; set; }
        public DbSet<OrdModel> Ords { get; set; }
        public DbSet<OrdTransModel> OrdTranses { get; set; }
        public DbSet<OrdDelvModel> OrdDelvs { get; set; }
        public DbSet<ProdModel> Prods { get; set; }
        public DbSet<ProdOutModel> ProdOuts { get; set; }
        public DbSet<AttachmentModel> Attachments { get; set; }
        public DbSet<JobReceiptModel> JobReceipts { get; set; }
        public DbSet<DbVersion> DbVersions { get; set; }
        public DbSet<TakaBeamModel> TakaBeams { get; set; }
        public DbSet<BatchModel> batches { get; set; }
        public DbSet<BatchTransModel> batchTranses { get; set; }
        #endregion

        #region Weavings
        public DbSet<JobCardModel> jobCards { get; set; }
        public DbSet<JobCardTransModel> jobCardTrans { get; set; }
        public DbSet<Prod_WeftModel> prod_Wefts { get; set; }
        public DbSet<MachineMasterModel> MachineMasters { get; set; }
        public DbSet<LoadingTranModel> loadingTranModels { get; set; }
        public DbSet<Prod_EmpModel> Prod_Emps { get; set; }
        public DbSet<PositionModel> Positions { get; set; }
        public DbSet<PackingTypeModel> PackingTypes { get; set; }
        public DbSet<EmpRate> EmpRates { get; set; }
        #endregion

        #region Apparel
        public DbSet<BomModel> Boms { get; set; }
        public DbSet<BOMTransModel> BOMTranses { get; set; }

        public DbSet<BarcodeModel> Barcodes { get; set; }
        public DbSet<BarcodeTrans> BarcodeTrans { get; set; }
        public DbSet<BarcodeStock> BarcodeStocks { get; set; }
        #endregion

        #region POS

        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<BillPay> BillPays { get; set; }
        public DbSet<ItemBatch> ItemBatches { get; set; }
        public DbSet<ItemSerial> ItemSerials { get; set; }
        #endregion

        public DbSet<Ewb> Ewbs { get; set; }
        public DbSet<ApiBal> ApiBals { get; set; }
        public DbSet<EInv> EInvs { get; set; }

        #endregion
    }

}
