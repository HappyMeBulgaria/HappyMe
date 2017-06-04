using System.Collections.Generic;
using HappyMe.Services.Common.MailSender.Contracts;

namespace HappyMe.Web.Controllers
{
    using HappyMe.Common.Constants;
    using HappyMe.Data.Models;
    using HappyMe.Web.Controllers.Base;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IMailSender mailSender;

        public HomeController(
            UserManager<User> userManager,
            IMailSender mailSender)
            : base(userManager)
        {
            this.mailSender = mailSender;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult About()
        {
            this.ViewData["Message"] = "Your application ge.";

            return this.View();
        }

        public IActionResult TestEmail()
        {
            this.mailSender.SendMail("kristian.mariyanov@gmail.com", "Test Email", "It works!", new List<string> { "yana.slavcheva@gmail.com" });

            return this.View();
        }

        public IActionResult Error()
        {
            return this.View();
        }
    }
}
