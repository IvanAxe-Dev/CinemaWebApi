using Cinema.Core.Models;
using Cinema.Core.ServiceContracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Services
{
    public class EmailConfirmationService : EmailService, IEmailConfirmationService
    {
        private readonly EmailConfiguration _emailConfig;
        private readonly IConfiguration _configuration;
        public EmailConfirmationService(IOptions<EmailConfiguration> emailConfig, IConfiguration configuration) : base(emailConfig, configuration)
        {
            _emailConfig = emailConfig.Value;
            _configuration = configuration;
        }

        protected override MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"Link to confirm your email <a href={message.Content}>Link</a>";

            emailMessage.From.Add(new MailboxAddress("Cinema", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = bodyBuilder.ToMessageBody();

            return emailMessage;
        }
    }
}
