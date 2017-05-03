namespace HappyMe.Web.Config
{
    using System.Threading.Tasks;

    using HappyMe.Common;

    using Microsoft.AspNet.Identity;

    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await MailSender.Instance.SendMailAsync(message.Destination, message.Subject, message.Body);
        }
    }
}