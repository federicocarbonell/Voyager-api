using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VoyageAPI.Controllers;
using VoyageAPI.DTOs;
using VoyageAPI.Logic;

namespace VoyageTest
{
    [TestClass]
    public class UpdateStateJobTest
    {
        [TestMethod]
        public void TestCorrectRequestFromChangeStateJob()
        {
            var job = new JobDTO();
            job.State = State.Finished;
            var Ilogic = new Mock<IJobLogic>();
            Ilogic.Setup(j => j.UpdateStateJob(1, null));
            var controller = new Mock<JobController>(Ilogic);
            var result = controller.Object.ModifyStateJob(1, job);
            var expectedResponse = new ActionResult<JobDTO>(job);
            Assert.AreEqual(result, expectedResponse);
        }
    }
}
