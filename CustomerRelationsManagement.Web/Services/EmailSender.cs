using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;

namespace CustomerRelationsManagement.Web.Services
{
    public class EmailSender : IEmailSender
    {
        private string smtpServer;
        private int smptPort;
        private string fromEmailAddress;

        public EmailSender(string smtpServer, int smptPort, string fromEmailAddress)
        {
            this.smtpServer = smtpServer;
            this.smptPort = smptPort;
            this.fromEmailAddress = fromEmailAddress;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MailMessage
            {
                From = new MailAddress(fromEmailAddress),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            message.To.Add(new MailAddress(email));

            using var client = new SmtpClient(smtpServer, smptPort);
            client.Send(message);

            return Task.CompletedTask;
        }
    }
}
