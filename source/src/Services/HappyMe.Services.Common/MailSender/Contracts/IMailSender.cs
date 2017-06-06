namespace HappyMe.Services.Common.MailSender.Contracts
{
    using System.Collections.Generic;

    public interface IMailSender : IService
    {
        void SendMail(string recipient, string subject, string messageBody, IEnumerable<string> bccRecipients = null);
    }
}
