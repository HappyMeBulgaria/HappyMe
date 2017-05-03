namespace HappyMe.Web
{
    using System;

    using HappyMe.Common.Constants;
    using HappyMe.Data;
    using HappyMe.Data.Models;
    using HappyMe.Web.Config;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;

    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<User>(context.Get<HappyMeDbContext>()));

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<User>(manager)
            {
                AllowOnlyAlphanumericUserNames = false, 
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = UserValidationConstants.PasswordMinLength
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(GlobalConstants.DefaultAccountLockoutTimeInMinutes);
            manager.MaxFailedAccessAttemptsBeforeLockout = GlobalConstants.MaxFailedAccessAttemptsBeforeLockout;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider(
                "Phone Code", 
                new PhoneNumberTokenProvider<User> { MessageFormat = "Your security code is {0}" });
            manager.RegisterTwoFactorProvider(
                "Email Code", 
                new EmailTokenProvider<User> { Subject = "Security Code", BodyFormat = "Your security code is {0}" });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<User>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }
    }
}