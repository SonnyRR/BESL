namespace BESL.Messaging
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link https://go.microsoft.com/fwlink/?LinkID=532713
    public class NullMessageSender : IEmailSender, ISmsSender
    {
        private readonly ILoggerFactory loggerFactory;
        private readonly string sendGridApiKey;

        public NullMessageSender(ILoggerFactory logger, IConfiguration configuration)
        {
            this.loggerFactory = logger;

#warning Don't forget to add your SendGrid API key to secrets. https://app.sendgrid.com/guide/integrate/langs/csharp
            // dotnet user-secrets set "sendgrid-api-key" "sendgrid_api_key"
            this.sendGridApiKey = configuration["sendgrid-api-key"];
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            var mailService = new SendGridEmailSender(this.loggerFactory, this.sendGridApiKey, "vk@besl.bg", "VK");
            return mailService.SendEmailAsync(email, subject, message);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.CompletedTask;
        }
    }
}
