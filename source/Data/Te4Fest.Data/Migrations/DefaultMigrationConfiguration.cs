namespace Te4Fest.Data.Migrations
{
    using System.Data.Entity.Migrations;

    using Te4Fest.Data;

    public class DefaultMigrationConfiguration : DbMigrationsConfiguration<Te4FestDbContext>
    {
        public DefaultMigrationConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Te4FestDbContext context)
        {
        }
    }
}
