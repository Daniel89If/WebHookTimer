using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebHookTimer.Controllers;
using WebHookTimer.Interfaces;
using WebHookTimer.Services;

namespace WebHookTimer.Tests.Controllers
{
    public class TimerControllerTests
    {
        private readonly TimerRequestManagment _timerManagment;
        private readonly IDbManager _dbManager;
        private readonly TimerController _timerController;

        public TimerControllerTests()
        {
            _dbManager = A.Fake<IDbManager>();
            _timerManagment = new TimerRequestManagment(_dbManager);
            _timerController = new TimerController(_timerManagment);
        }


        [Fact]
        public void GetTimerById_SendInt_ReturnNotFound()
        {
            var id = "";
            NotFoundResult result = (NotFoundResult)_timerController.GetTimerById(id);
            
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }
    }
}
