using MoreDotNet.Extensions.Collections;

namespace HappyMe.Services.Common.MailSender
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using HappyMe.Services.Common.MailSender.Contracts;

    using MailKit.Net.Smtp;
    using MimeKit;

    public sealed class MailSender : IMailSender, IDisposable
    {

        private const string SendFrom = "postmaster@happyme.site";
        private const string SendFromName = "HappyMe";

        private const string Username = "postmaster@happyme.site";

        private const string ServerAddress = "mail.happyme.site";
        private const int ServerPort = 8889;

        private readonly SmtpClient mailClient;

        public MailSender(string password)
        {
            this.mailClient = new SmtpClient();
            this.mailClient.Connect(ServerAddress, ServerPort, false);
            this.mailClient.AuthenticationMechanisms.Remove("XOAUTH2");
            this.mailClient.Authenticate(Username, password);
        }

        public void SendMail(string recipient, string subject, string messageBody, IEnumerable<string> bccRecipients = null)
        {
            var message = new MimeMessage();
            message.Subject = subject;
            message.From.Add(new MailboxAddress(SendFromName, SendFrom));
            message.To.Add(new MailboxAddress(recipient));
            if (bccRecipients != null)
            {
                message.Bcc.AddRange(bccRecipients.Select(r => new MailboxAddress(r)));
            }

            message.Body = new TextPart("html")
            {
                Text = messageBody
            };

            this.mailClient.Send(message);
        }

        public void Dispose()
        {
            this.mailClient.Disconnect(true);
        }
    }
}
