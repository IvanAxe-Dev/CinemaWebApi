using Cinema.Core.Models;
using Cinema.Core.ServiceContracts;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Cinema.Core.Services
{
    public abstract class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;
        private readonly IConfiguration _configuration;
        public EmailService(IOptions<EmailConfiguration> emailConfig, IConfiguration configuration)
        {
            _emailConfig = emailConfig.Value;
            _configuration = configuration;
        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        protected abstract MimeMessage CreateEmailMessage(Message message);
        

        protected void Send(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.UserName, _configuration["email-sender-password"]);

                client.Send(mailMessage);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
