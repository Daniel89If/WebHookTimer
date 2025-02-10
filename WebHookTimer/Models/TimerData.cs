namespace WebHookTimer.Models
{
    public class TimerData
    {
        List<TimerInfo> timers;
    }

    public class TimerInfo
    {
        public string id { get; set; }
        public DateTime timeCreated { get; set; }
        public Int64 hours { get; set; }
        public Int64 minutes { get; set; }
        public Int64 seconds { get; set; }
        public DateTime expiredTime { get; set; }
        public string webhookUrl { get; set; }
        public string status { get; set; }
    }
}
