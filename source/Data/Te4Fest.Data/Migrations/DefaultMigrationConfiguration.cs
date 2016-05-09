namespace ComponentCms.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using ComponentCms.Common.Constants;
    using ComponentCms.Data.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class DefaultMigrationConfiguration : DbMigrationsConfiguration<ComponentCmsDbContext>
    {
        private readonly string[] defaultRoles =
        {
            RoleConstants.Administrator,
            RoleConstants.SpinTrusted,
            RoleConstants.RegFishTrusted,
            RoleConstants.HuGtoTrusted,
            RoleConstants.FishRegHuTrusted,
            RoleConstants.SpinSecondTrusted
        };

        public DefaultMigrationConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ComponentCmsDbContext context)
        {
            foreach (var role in this.defaultRoles)
            {
                if (!context.Roles.Any(x => x.Name == role))
                {
                    context.Roles.Add(new IdentityRole(role));
                    context.SaveChanges();
                }
            }

            if (!context.Users.Any())
            {
                var userManger = new UserManager<User>(new UserStore<User>(context));
                var result = userManger.CreateAsync(
                    new User { Email = "info@bggrinders.com", UserName = "info@bggrinders.com", CreatedOn = DateTime.Now },
                    "BgGrindersPassWordQ123456").Result;

                var user = context.Users.FirstOrDefault(x => x.Email == "info@bggrinders.com");
                if (user != null)
                {
                    var userId = user.Id;
                    var role = context.Roles.FirstOrDefault(x => x.Name == RoleConstants.Administrator);

                    role.Users.Add(new IdentityUserRole
                    {
                        UserId = userId,
                        RoleId = role.Id
                    });
                }

                context.SaveChanges();
            }
        }
    }
}
