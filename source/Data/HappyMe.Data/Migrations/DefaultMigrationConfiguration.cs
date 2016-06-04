namespace HappyMe.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using HappyMe.Common.Constants;
    using HappyMe.Data;
    using HappyMe.Data.Models;

    using Microsoft.AspNet.Identity.EntityFramework;

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
            this.SeedRoles(context);
        }

        private void SeedRoles(HappyMeDbContext context)
        {
            if (!context.Roles.Any())
            {
                var roleNames = new[]
                {
                    RoleConstants.Administrator,
                    RoleConstants.Parent,
                    RoleConstants.Child
                };

                foreach (var roleName in roleNames)
                {
                    context.Roles.Add(new IdentityRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = roleName
                    });
                }

                context.SaveChanges();
            }
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
                        IsActive = i % 2 == 0,
                        IsPublic = true
                    };

                    context.Modules.AddOrUpdate(module);
                    context.SaveChanges();
                }
            }
        }
    }
}
