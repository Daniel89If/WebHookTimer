using Microsoft.AspNetCore.Mvc;
using WebHookTimer.Interfaces;
using WebHookTimer.Models;
using WebHookTimer.Services;

namespace WebHookTimer.Controllers
{
    [ApiController]
    [Route("WebHook")]
    public class TimerController : Controller
    {
        private readonly TimerRequestManagment _timerManagment;

        public TimerController(TimerRequestManagment timerManagment)
        {
            _timerManagment = timerManagment;
        }

        [HttpGet("GetTimerById")]
        public IActionResult GetTimerById(string id)
        {
            try
            {
                var res = _timerManagment.GetTimerById(id);
                return res?.Result?.id == null ? NotFound() : Ok(res.Result);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPost("SetTimer")]
        public IActionResult SetNewTimer(PostSetTimerRequest newTimer)
        {
            try
            {
                var id = _timerManagment.SetNewRequest(newTimer);
                return String.IsNullOrEmpty(id) ? NotFound() : Ok(id);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
