using Jobby.Notifier.Services.EmailNotifier.Models;
using System.Threading.Tasks;

namespace Jobby.Notifier.Services.EmailNotifier.Interfaces
{
    public interface IEmailSender
    {
        Task<SendEmailResult> SendAsync(EmailDetails emailDetails);
    }
}
