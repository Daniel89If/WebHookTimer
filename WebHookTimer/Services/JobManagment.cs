using System.Timers;
using WebHookTimer.Interfaces;

namespace WebHookTimer.Services
{
    public class JobManagment : IJobManagment
    {
        private IDbManager _IdbManager;
        private IExpiredTime _expiredTime;

        public JobManagment(IDbManager idbManager, IExpiredTime expiredTime)
        {
            _IdbManager = idbManager;
            _expiredTime = expiredTime;
            GetRecordsJob();
        }

        public void GetRecordsJob()
        {
            var timer = new System.Timers.Timer(10000);
            timer.Elapsed += JobTimerEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void JobTimerEvent(object? sender, ElapsedEventArgs e)
        {
            var data = _IdbManager.GetExpiredTimers();
            _expiredTime.Handle(data.Result);
        }
    }
}
