using Jobby.Notifier.Services.EmailNotifier.Interfaces;
using Jobby.Notifier.Services.EmailNotifier.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using SendGrid;
using System;
using SendGrid.Helpers.Mail;
using System.IO;
using System.Linq;

namespace Jobby.Notifier.Infra.EmailSenders.SendGridES
{
    public class SendGridEmailSender : IEmailSender
    {
        private readonly DataContractJsonSerializer Serializer =
            new DataContractJsonSerializer(typeof(ICollection<SendGridResponseError>));

        public SendGridOptions Options { get; }

        public SendGridEmailSender(IOptions<SendGridOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public async Task<SendEmailResult> SendAsync(EmailDetails details)
        {
            Response response = await SendAsync(Options.Key, details);
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                return new SendEmailResult();
            }

            try
            {
                Stream bodyResult = await response.Body.ReadAsStreamAsync();
                var sendGridResponse = Serializer.ReadObject(bodyResult) as SendGridResponse;

                var errorResult = new SendEmailResult
                {
                    Errors = sendGridResponse?.Errors
                        .Select(error => error.Message)
                        .ToList()
                };
                if (errorResult.Errors == null || errorResult.Errors.Count() == 0)
                {
                    errorResult.Errors = new List<string>() { "Unknown error from email sending service." };
                }

                return errorResult;
            }
            catch (Exception)
            {
                return new SendEmailResult()
                {
                    Errors = new List<string>() { "Unknown error occurred." }
                };
            }
        }

        private async Task<Response> SendAsync(string apiKey, EmailDetails details)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage
            {
                From = new EmailAddress(details.FromEmail),
                Subject = details.Subject,
                PlainTextContent = details.IsHtml ? null : details.Content,
                HtmlContent = details.IsHtml ? details.Content : null,
            };
            msg.AddTo(new EmailAddress(details.ToEmail));

            return await client.SendEmailAsync(msg);
        }
    }
}
