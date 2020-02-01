namespace BESL.Infrastructure.Messaging
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    using static BESL.Common.GlobalConstants;

    public class BeslMessageSender : IEmailSender, ISmsSender
    {
        private readonly ILoggerFactory loggerFactory;
        private readonly string sendGridApiKey;

        public BeslMessageSender(ILoggerFactory logger, IConfiguration configuration)
        {
            this.loggerFactory = logger;
            this.sendGridApiKey = configuration["sendgrid-api-key"];
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            var mailService = new SendGridEmailSender(this.loggerFactory, this.sendGridApiKey, LEAGUE_EMAIL, LEAGUE_EMAIL_SENDER_NAME);
            return mailService.SendEmailAsync(email, subject, message);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.CompletedTask;
        }
    }
}
