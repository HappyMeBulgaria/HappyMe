namespace HappyMe.Web.Services
{
    using System.Threading.Tasks;

    using HappyMe.Services.Common;

    public interface IEmailSender : IService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
