using Jobby.Notifier.Services.EmailNotifier.Interfaces;
using Jobby.Notifier.Services.EmailNotifier.Models;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Jobby.Notifier.Services.EmailNotifier
{
    public class EmailNotifier : IEmailNotifier
    {
        public EmailNotifierOptions Options { get; }

        private readonly IEmailSender emailSender;

        public EmailNotifier(IOptions<EmailNotifierOptions> optionsAccessor, IEmailSender emailSender)
        {
            this.emailSender = emailSender;
            Options = optionsAccessor.Value;
        }

        public async Task<NotifyByEmailResult> NotifyAsync(NotifyByEmailDetails notifyDetails)
        {
            var emailDetails = new SendEmailDetails
            {
                ToEmail = notifyDetails.Email,
                FromEmail = Options.FromEmail,
                Subject = Options.Subject,
                Content = $"New notification!<br/>{notifyDetails.Message}",
                IsHtml = true
            };

            var sendResult = await emailSender.SendAsync(emailDetails);

            return new NotifyByEmailResult()
            {
                Errors = sendResult.Errors
            };
        }
    }
}
