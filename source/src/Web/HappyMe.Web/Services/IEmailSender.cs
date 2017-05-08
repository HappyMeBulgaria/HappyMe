using System.Threading.Tasks;

namespace HappyMe.Web.Services
{
    using HappyMe.Services.Common;

    public interface IEmailSender : IService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
