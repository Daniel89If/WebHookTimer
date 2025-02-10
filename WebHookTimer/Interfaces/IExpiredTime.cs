using WebHookTimer.Models;

namespace WebHookTimer.Interfaces
{
    public interface IExpiredTime
    {
        void Handle(List<TimerInfo> timerInfos);
    }
}
