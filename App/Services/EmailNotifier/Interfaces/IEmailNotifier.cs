using Jobby.Notifier.Services.EmailNotifier.Models;
using System.Threading.Tasks;

namespace Jobby.Notifier.Services.EmailNotifier.Interfaces
{
    public interface IEmailNotifier
    {
        Task<NotifyByEmailResult> NotifyAsync(NotifyByEmailDetails notifyDetails);
    }
}
