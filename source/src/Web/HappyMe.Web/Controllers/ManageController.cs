﻿namespace HappyMe.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using HappyMe.Data.Models;
    using HappyMe.Web.Models;
    using HappyMe.Web.Services;
    using HappyMe.Web.ViewModels.Manage;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    [Authorize]
    public class ManageController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly string externalCookieScheme;
        private readonly IEmailSender emailSender;
        private readonly ISmsSender smsSender;
        private readonly ILogger logger;

        public ManageController(
          UserManager<User> userManager,
          SignInManager<User> signInManager,
          IOptions<IdentityCookieOptions> identityCookieOptions,
          IEmailSender emailSender,
          ISmsSender smsSender,
          ILoggerFactory loggerFactory)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.externalCookieScheme = identityCookieOptions.Value.ExternalCookieAuthenticationScheme;
            this.emailSender = emailSender;
            this.smsSender = smsSender;
            this.logger = loggerFactory.CreateLogger<ManageController>();
        }

        [HttpGet]
        public async Task<IActionResult> Index(ManageMessageId? message = null)
        {
            this.ViewData["StatusMessage"] =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : string.Empty;

            var user = await this.GetCurrentUserAsync();
            if (user == null)
            {
                return this.View("Error");
            }

            var model = new IndexViewModel
            {
                HasPassword = await this.userManager.HasPasswordAsync(user),
                PhoneNumber = await this.userManager.GetPhoneNumberAsync(user),
                TwoFactor = await this.userManager.GetTwoFactorEnabledAsync(user),
                Logins = await this.userManager.GetLoginsAsync(user),
                BrowserRemembered = await this.signInManager.IsTwoFactorClientRememberedAsync(user)
            };
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveLogin(RemoveLoginViewModel account)
        {
            ManageMessageId? message = ManageMessageId.Error;
            var user = await this.GetCurrentUserAsync();
            if (user != null)
            {
                var result = await this.userManager.RemoveLoginAsync(user, account.LoginProvider, account.ProviderKey);
                if (result.Succeeded)
                {
                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    message = ManageMessageId.RemoveLoginSuccess;
                }
            }

            return this.RedirectToAction(nameof(this.ManageLogins), new { Message = message });
        }

        public IActionResult AddPhoneNumber()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            // Generate the token and send it
            var user = await this.GetCurrentUserAsync();
            if (user == null)
            {
                return this.View("Error");
            }

            var code = await this.userManager.GenerateChangePhoneNumberTokenAsync(user, model.PhoneNumber);
            await this.smsSender.SendSmsAsync(model.PhoneNumber, "Your security code is: " + code);
            return this.RedirectToAction(nameof(this.VerifyPhoneNumber), new { model.PhoneNumber });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnableTwoFactorAuthentication()
        {
            var user = await this.GetCurrentUserAsync();
            if (user != null)
            {
                await this.userManager.SetTwoFactorEnabledAsync(user, true);
                await this.signInManager.SignInAsync(user, isPersistent: false);
                this.logger.LogInformation(1, "User enabled two-factor authentication.");
            }

            return this.RedirectToAction(nameof(this.Index), "Manage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DisableTwoFactorAuthentication()
        {
            var user = await this.GetCurrentUserAsync();
            if (user != null)
            {
                await this.userManager.SetTwoFactorEnabledAsync(user, false);
                await this.signInManager.SignInAsync(user, isPersistent: false);
                this.logger.LogInformation(2, "User disabled two-factor authentication.");
            }

            return this.RedirectToAction(nameof(this.Index), "Manage");
        }

        [HttpGet]
        public async Task<IActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var user = await this.GetCurrentUserAsync();
            if (user == null)
            {
                return this.View("Error");
            }

            await this.userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);

            // Send an SMS to verify the phone number
            return phoneNumber == null ? this.View("Error") : this.View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.GetCurrentUserAsync();
            if (user != null)
            {
                var result = await this.userManager.ChangePhoneNumberAsync(user, model.PhoneNumber, model.Code);
                if (result.Succeeded)
                {
                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    return this.RedirectToAction(nameof(this.Index), new { Message = ManageMessageId.AddPhoneSuccess });
                }
            }

            this.ModelState.AddModelError(string.Empty, "Failed to verify phone number");
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemovePhoneNumber()
        {
            var user = await this.GetCurrentUserAsync();
            if (user != null)
            {
                var result = await this.userManager.SetPhoneNumberAsync(user, null);
                if (result.Succeeded)
                {
                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    return this.RedirectToAction(nameof(this.Index), new { Message = ManageMessageId.RemovePhoneSuccess });
                }
            }

            return this.RedirectToAction(nameof(this.Index), new { Message = ManageMessageId.Error });
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.GetCurrentUserAsync();
            if (user != null)
            {
                var result = await this.userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    this.logger.LogInformation(3, "User changed their password successfully.");
                    return this.RedirectToAction(nameof(this.Index), new { Message = ManageMessageId.ChangePasswordSuccess });
                }

                this.AddErrors(result);
                return this.View(model);
            }

            return this.RedirectToAction(nameof(this.Index), new { Message = ManageMessageId.Error });
        }

        [HttpGet]
        public IActionResult SetPassword()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.GetCurrentUserAsync();
            if (user != null)
            {
                var result = await this.userManager.AddPasswordAsync(user, model.NewPassword);
                if (result.Succeeded)
                {
                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    return this.RedirectToAction(nameof(this.Index), new { Message = ManageMessageId.SetPasswordSuccess });
                }

                this.AddErrors(result);
                return this.View(model);
            }

            return this.RedirectToAction(nameof(this.Index), new { Message = ManageMessageId.Error });
        }

        [HttpGet]
        public async Task<IActionResult> ManageLogins(ManageMessageId? message = null)
        {
            this.ViewData["StatusMessage"] =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.AddLoginSuccess ? "The external login was added."
                : message == ManageMessageId.Error ? "An error has occurred."
                : string.Empty;
            var user = await this.GetCurrentUserAsync();
            if (user == null)
            {
                return this.View("Error");
            }

            var userLogins = await this.userManager.GetLoginsAsync(user);
            var otherLogins = this.signInManager.GetExternalAuthenticationSchemes().Where(auth => userLogins.All(ul => auth.AuthenticationScheme != ul.LoginProvider)).ToList();
            this.ViewData["ShowRemoveButton"] = user.PasswordHash != null || userLogins.Count > 1;
            return this.View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LinkLogin(string provider)
        {
            // Clear the existing external cookie to ensure a clean login process
            await this.HttpContext.Authentication.SignOutAsync(this.externalCookieScheme);

            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = this.Url.Action(nameof(this.LinkLoginCallback), "Manage");
            var properties = this.signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, this.userManager.GetUserId(this.User));
            return this.Challenge(properties, provider);
        }

        [HttpGet]
        public async Task<ActionResult> LinkLoginCallback()
        {
            var user = await this.GetCurrentUserAsync();
            if (user == null)
            {
                return this.View("Error");
            }

            var info = await this.signInManager.GetExternalLoginInfoAsync(await this.userManager.GetUserIdAsync(user));
            if (info == null)
            {
                return this.RedirectToAction(nameof(this.ManageLogins), new { Message = ManageMessageId.Error });
            }

            var result = await this.userManager.AddLoginAsync(user, info);
            var message = ManageMessageId.Error;
            if (result.Succeeded)
            {
                message = ManageMessageId.AddLoginSuccess;

                // Clear the existing external cookie to ensure a clean login process
                await this.HttpContext.Authentication.SignOutAsync(this.externalCookieScheme);
            }

            return this.RedirectToAction(nameof(this.ManageLogins), new { Message = message });
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private Task<User> GetCurrentUserAsync()
        {
            return this.userManager.GetUserAsync(this.HttpContext.User);
        }
    }
}
