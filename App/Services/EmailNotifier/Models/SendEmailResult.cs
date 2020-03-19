using System;
using System.Collections.Generic;
using System.Text;

namespace Jobby.Notifier.Services.EmailNotifier.Models
{
    public class SendEmailResult
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
