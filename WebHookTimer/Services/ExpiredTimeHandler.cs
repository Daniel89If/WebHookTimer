using WebHookTimer.Interfaces;
using WebHookTimer.Models;

namespace WebHookTimer.Services
{
    public class ExpiredTimeHandler : IExpiredTime
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ExpiredTimeHandler> _logger;

        public ExpiredTimeHandler(IHttpClientFactory httpClientFactory, ILogger<ExpiredTimeHandler> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public void Handle(List<TimerInfo> timerInfos)
        {
            foreach (TimerInfo timerInfo in timerInfos)
            {
                _ = SendTimerEndResponse(timerInfo);
            }
        }

        private async Task SendTimerEndResponse(TimerInfo timerInfo)
        {
            try
            {
                using var client = _httpClientFactory.CreateClient();
                var response = await client.PostAsync(timerInfo.webhookUrl, null);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning($"Failed to send webhook for {timerInfo.id}. Status Code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending webhook request");
            }
        }
    }
}
