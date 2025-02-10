namespace WebHookTimer.Models
{
    public class PostSetTimerRequest
    {
        public Int64 hours { get; set; }
        public Int64 minutes { get; set; }
        public Int64 seconds { get; set; }
        public string webhookUrl { get; set; }
    }
}
