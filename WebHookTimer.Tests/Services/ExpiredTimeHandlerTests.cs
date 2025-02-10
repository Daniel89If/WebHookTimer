using FakeItEasy;
using Microsoft.Extensions.Logging;
using WebHookTimer.Interfaces;
using WebHookTimer.Models;
using WebHookTimer.Services;

namespace WebHookTimer.Tests.Services
{
    public class ExpiredTimeHandlerTests
    {
        private readonly IExpiredTime _expiredTime;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<ExpiredTimeHandler> _logger;

        public ExpiredTimeHandlerTests()
        {
            _clientFactory = A.Fake<IHttpClientFactory>();
            _logger = A.Fake<ILogger<ExpiredTimeHandler>>();
            _expiredTime = new ExpiredTimeHandler(_clientFactory, _logger);
        }

        [Fact]
        public void Handle_EmptyList_DoNotActivSendTimerEndResponse()
        {
            List<TimerInfo> timerInfos = new List<TimerInfo>();
            _expiredTime.Handle(timerInfos);
        }
    }
}
