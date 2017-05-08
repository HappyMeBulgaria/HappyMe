using System.Threading.Tasks;

namespace HappyMe.Web.Services
{
    public interface IEmailSender : IService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
