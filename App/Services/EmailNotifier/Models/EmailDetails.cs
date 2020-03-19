namespace Jobby.Notifier.Services.EmailNotifier.Models
{
    public class EmailDetails
    {
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public bool IsHtml { get; set; }
    }
}
