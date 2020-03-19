using System.Collections.Generic;

namespace Jobby.Notifier.Infra.EmailSenders.SendGridES
{
    class SendGridResponse
    {
        public List<SendGridResponseError> Errors { get; set; }
    }
}
