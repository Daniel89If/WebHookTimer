using WebHookTimer.Models;

namespace WebHookTimer.Interfaces
{
    public interface IDbManager
    {
        void InitTable();
        Task<TimerInfo> GetTimerInfoById(string id);
        Task<List<TimerInfo>> GetExpiredTimers();
        Task<string> SetNewTimerRecord(TimerInfo newTimerRecord);
    }
}
