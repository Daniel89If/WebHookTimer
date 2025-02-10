using System.Collections.Concurrent;
using WebHookTimer.Interfaces;
using WebHookTimer.Models;

namespace WebHookTimer.Services
{
    public class TimerRequestManagment
    {
        private IDbManager _dbManager;
        public TimerRequestManagment(IDbManager dbManager)
        {
            _dbManager = dbManager;
        }
        public string SetNewRequest(PostSetTimerRequest newRequest)
        {
            var newTimer = Mapping(newRequest);
            // Write to db
            var id = _dbManager.SetNewTimerRecord(newTimer);

            return newTimer.id;
        }

        public async Task<TimerStatus?> GetTimerById(string id)
        {
            try
            {
                var timerStatus = new TimerStatus();
                var timer = await _dbManager.GetTimerInfoById(id);
                if (timer != null)
                {
                    timerStatus.id = timer.id;
                    var timeSpan = timer.expiredTime.Subtract(DateTime.Now);
                    timerStatus.timeLeft = (Int64)(timeSpan).TotalSeconds;
                }
                return timerStatus;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }         
        }

        private TimerInfo Mapping(PostSetTimerRequest request)
        {
            var timerInfo = new TimerInfo();
            timerInfo.timeCreated = DateTime.Now;
            timerInfo.hours = request.hours;
            var left = request.hours * 3600 + request.minutes * 60 + request.seconds;
            timerInfo.minutes = request.minutes;
            timerInfo.seconds = request.seconds;
            timerInfo.expiredTime = timerInfo.timeCreated.AddSeconds(left);
            timerInfo.webhookUrl = request.webhookUrl;
            timerInfo.status = "Started";
            timerInfo.id = GenerateUUId();

            return timerInfo;
        }

        private string GenerateUUId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
