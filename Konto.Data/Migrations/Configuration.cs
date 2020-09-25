namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Konto.Data.KontoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Konto.Data.KontoContext";
        }

        protected override void Seed(Konto.Data.KontoContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            // Delete all stored procs, views
            //foreach (var file in Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sql\\Seed"), "*.sql"))
            //{
            //    context.Database.ExecuteSqlCommand(File.ReadAllText(file), new object[0]);
            //}

            //// Add Stored Procedures
            //foreach (var file in Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sql\\StoredProcs"), "*.sql"))
            //{
            //    context.Database.ExecuteSqlCommand(File.ReadAllText(file), new object[0]);
            //}

        }
    }
}
