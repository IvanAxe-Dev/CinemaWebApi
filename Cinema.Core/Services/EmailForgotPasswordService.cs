using Cinema.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using Cinema.Core.ServiceContracts;

namespace Cinema.Core.Services
{
    public class EmailForgotPasswordService : EmailService, IEmailForgotPasswordService
    {
        private readonly EmailConfiguration _emailConfig;
        private readonly IConfiguration _configuration;
        public EmailForgotPasswordService(IOptions<EmailConfiguration> emailConfig, IConfiguration configuration) : base(emailConfig, configuration)
        {
            _emailConfig = emailConfig.Value;
            _configuration = configuration;
        }

        protected override MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"Link to reset your password <a href={message.Content}>Link</a>";

            emailMessage.From.Add(new MailboxAddress("Cinema", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = bodyBuilder.ToMessageBody();

            return emailMessage;
        }
    }
}
