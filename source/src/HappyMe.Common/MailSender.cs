﻿//------------------------------------------------------------------------------
// <auto-generated>
// Well, not really. This is just a trick to get StyleCop off my back.
// </auto-generated>
//------------------------------------------------------------------------------
namespace HappyMe.Common
{
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MailKit.Net.Smtp;

    // TODO: rewrite MailSender for .net standard - http://dotnetthoughts.net/how-to-send-emails-from-aspnet-core/

    /// TODO: Reimplement as a normal class and use IoC to inject where needed. It would be easier to test
    public sealed class MailSender
    {
#if DEBUG
        private const string SendFrom = "info@bggrinders.com";
        private const string SendFromName = "BGGrinders";

        private const string Username = "51374bab85fd339d0";
        private const string Password = "b2505c2c374635";

        private const string ServerAddress = "mailtrap.io";
        private const int ServerPort = 2525;
#else

            private const string SendFrom = "postmaster@happyme.site";
            private const string SendFromName = "HappyMe";

            private const string Username = "postmaster@happyme.site";

            private const string ServerAddress = "mail.happyme.site";
            private const int ServerPort = 25;

            private string Password => "TO ADD BACK";
#endif

        private static readonly object SyncRoot = new object();

        private static MailSender instance;
        private readonly SmtpClient mailClient;

        private MailSender()
        {
            this.mailClient = new SmtpClient
            {
                
                //Credentials = new NetworkCredential(Username, Password),
                //Port = ServerPort,
                //Host = ServerAddress,
                //EnableSsl = false,
            };
        }

        public static MailSender Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new MailSender();
                        }
                    }
                }

                return instance;
            }
        }

        ////public async Task SendMailAsync(string recipient, string subject, string messageBody, IEnumerable<string> bccRecipients = null)
        ////{
        ////    //var message = this.PrepareMessage(recipient, subject, messageBody, bccRecipients);
        ////    //await this.mailClient.SendMailAsync(message);
        ////}

        public void SendMail(string recipient, string subject, string messageBody, IEnumerable<string> bccRecipients = null)
        {
            //var message = this.PrepareMessage(recipient, subject, messageBody, bccRecipients);
            //this.mailClient.Send(message);
        }

        //private MailMessage PrepareMessage(string recipient, string subject, string messageBody, IEnumerable<string> bccRecipients)
        //{
        //    var mailTo = new MailAddress(recipient);
        //    //var mailFrom = new MailAddress(SendFrom, SendFromName);
        //    //var message = new MailMessage(mailFrom, mailTo)
        //    //{
        //    //    Body = messageBody,
        //    //    BodyEncoding = Encoding.UTF8,
        //    //    IsBodyHtml = true,
        //    //    Subject = subject,
        //    //    SubjectEncoding = Encoding.UTF8,
        //    //};

        //    //if (bccRecipients != null)
        //    //{
        //    //    foreach (var bccRecipient in bccRecipients)
        //    //    {
        //    //        message.Bcc.Add(bccRecipient);
        //    //    }
        //    //}

        //    return null;
        //}
    }
}
