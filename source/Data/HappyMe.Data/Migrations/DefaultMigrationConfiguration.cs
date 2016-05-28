namespace HappyMe.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    using HappyMe.Data;
    using HappyMe.Data.Models;

    public class DefaultMigrationConfiguration : DbMigrationsConfiguration<HappyMeDbContext>
    {
        private const int ImagesToSeed = 20;
        private const int ModulesToSeed = 20;

        public DefaultMigrationConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(HappyMeDbContext context)
        {
            this.SeedImages(context);
            this.SeedModules(context);
        }

        private void SeedImages(HappyMeDbContext context)
        {
            if (!context.Images.Any())
            {
                for (int i = 0; i < ImagesToSeed; i++)
                {
                    var image = new Image
                    {
                        Path = $"Fake Path {i}",
                    };

                    context.Images.AddOrUpdate(image);
                }

                context.SaveChanges();
            }
        }

        private void SeedModules(HappyMeDbContext context)
        {
            if (!context.Modules.Any())
            {
                for (int i = 0; i < ModulesToSeed; i++)
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
