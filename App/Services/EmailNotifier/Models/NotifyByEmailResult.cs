using System.Collections.Generic;

namespace Jobby.Notifier.Services.EmailNotifier.Models
{
    public class NotifyByEmailResult
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
