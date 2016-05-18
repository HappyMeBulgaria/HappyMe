namespace Te4Fest.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Te4Fest.Data;
    using Te4Fest.Data.Models;

    public class DefaultMigrationConfiguration : DbMigrationsConfiguration<Te4FestDbContext>
    {
        public DefaultMigrationConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Te4FestDbContext context)
        {
            if (!context.Modules.Any())
            {
                for (int i = 0; i < 20; i++)
                {
                    var module = new Module
                    {
                        Name = $"Test Name{i}",
                        Description = $"Some description{i}",
                        IsActive = i % 2 == 0
                    };
                    context.Modules.AddOrUpdate(module);
                    context.SaveChanges();
                }
            }
        }
    }
}
