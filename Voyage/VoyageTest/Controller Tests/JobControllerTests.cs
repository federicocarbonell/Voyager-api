using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VoyageAPI.Controllers;
using VoyageAPI.DTOs;
using VoyageAPI.Logic;

namespace VoyageTest
{
    [TestClass]
    public class JobControllerTests
    {

        List<JobDTO> jobsToReturn;
        ProductDTO product;

        public void InitializeContext(State state)
        {
            product = new ProductDTO { Id = 1, Description = "Freezer", Name = "Panasonic", Year = 2020 };
            jobsToReturn = new List<JobDTO>()
            {
                new JobDTO
                {
                    Id = 1,
                    Description = "Test",
                    Latitude = "1",
                    Longitude = "1",
                    Product = product,
                    State = state,
                    Time = "15:00",
                },
                new JobDTO
                {
                    Id = 2,
                    Description = "Test 2",
                    Latitude = "2",
                    Longitude = "2",
                    Product = product,
                    State = state,
                    Time = "16:00"
                },
            };
        }


        [TestMethod]
        public void GetPendingJobsTestOk()
        {
            InitializeContext(State.Pending);
            Mock<IJobLogic> mock = new Mock<IJobLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetPendingJobs(1)).Returns(jobsToReturn);
            JobController controller = new JobController(mock.Object);

            var result = controller.GetPendingJobs(1);
            OkObjectResult okResult = result.Result as OkObjectResult;
            List<JobDTO> jobs = okResult.Value as List<JobDTO>;

            mock.VerifyAll();
            Assert.AreEqual(jobs.First().Id, jobsToReturn.First().Id);
            Assert.AreEqual(jobs.First().Description, jobsToReturn.First().Description);
            Assert.AreEqual(jobs.First().Latitude, jobsToReturn.First().Longitude);
            Assert.AreEqual(jobs.First().Product.Id, jobsToReturn.First().Product.Id);
            Assert.AreEqual(jobs.First().State, jobsToReturn.First().State);
            Assert.AreEqual(jobs.First().Time, jobsToReturn.First().Time);
            Assert.AreEqual(jobs.Count(), jobsToReturn.Count());
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public void GetPendingJobsTestEmptyList()
        {
            List<JobDTO> jobsToReturn = null;
            Mock<IJobLogic> mock = new Mock<IJobLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetPendingJobs(1)).Returns(jobsToReturn);
            JobController controller = new JobController(mock.Object);

            var result = controller.GetPendingJobs(1);
            NotFoundObjectResult notFoundResult = result.Result as NotFoundObjectResult;

            mock.VerifyAll();
            Assert.AreEqual("No Jobs pending." , notFoundResult.Value);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [TestMethod]
        public void GetInProcessJobsTestOk()
        {
            InitializeContext(State.InProcess);
            Mock<IJobLogic> mock = new Mock<IJobLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetInProcessJobs(1)).Returns(jobsToReturn);
            JobController controller = new JobController(mock.Object);

            var result = controller.GetInProcessJobs(1);
            OkObjectResult okResult = result.Result as OkObjectResult;
            List<JobDTO> jobs = okResult.Value as List<JobDTO>;

            mock.VerifyAll();
            Assert.AreEqual(jobs.First().Id, jobsToReturn.First().Id);
            Assert.AreEqual(jobs.First().Description, jobsToReturn.First().Description);
            Assert.AreEqual(jobs.First().Latitude, jobsToReturn.First().Longitude);
            Assert.AreEqual(jobs.First().Product.Id, jobsToReturn.First().Product.Id);
            Assert.AreEqual(jobs.First().State, jobsToReturn.First().State);
            Assert.AreEqual(jobs.First().Time, jobsToReturn.First().Time);
            Assert.AreEqual(jobs.Count(), jobsToReturn.Count());
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public void GetInProcessJobsTestEmptyList()
        {
            List<JobDTO> jobsToReturn = null;
            Mock<IJobLogic> mock = new Mock<IJobLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetInProcessJobs(1)).Returns(jobsToReturn);
            JobController controller = new JobController(mock.Object);

            var result = controller.GetInProcessJobs(1);
            NotFoundObjectResult notFoundResult = result.Result as NotFoundObjectResult;

            mock.VerifyAll();
            Assert.AreEqual("No Jobs in process.", notFoundResult.Value);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [TestMethod]
        public void GetFinishedJobsTestOk()
        {
            InitializeContext(State.Finished);
            Mock<IJobLogic> mock = new Mock<IJobLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetFinishedJobs(1)).Returns(jobsToReturn);
            JobController controller = new JobController(mock.Object);

            var result = controller.GetFinishedJobs(1);
            OkObjectResult okResult = result.Result as OkObjectResult;
            List<JobDTO> jobs = okResult.Value as List<JobDTO>;

            mock.VerifyAll();
            Assert.AreEqual(jobs.First().Id, jobsToReturn.First().Id);
            Assert.AreEqual(jobs.First().Description, jobsToReturn.First().Description);
            Assert.AreEqual(jobs.First().Latitude, jobsToReturn.First().Longitude);
            Assert.AreEqual(jobs.First().Product.Id, jobsToReturn.First().Product.Id);
            Assert.AreEqual(jobs.First().State, jobsToReturn.First().State);
            Assert.AreEqual(jobs.First().Time, jobsToReturn.First().Time);
            Assert.AreEqual(jobs.Count(), jobsToReturn.Count());
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public void GetFinishedJobsTestEmptyList()
        {
            List<JobDTO> jobsToReturn = null;
            Mock<IJobLogic> mock = new Mock<IJobLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetFinishedJobs(1)).Returns(jobsToReturn);
            JobController controller = new JobController(mock.Object);

            var result = controller.GetFinishedJobs(1);
            NotFoundObjectResult notFoundResult = result.Result as NotFoundObjectResult;

            mock.VerifyAll();
            Assert.AreEqual("No Jobs finished.", notFoundResult.Value);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [TestMethod]
        public void ModifyStateJobTestOk()
        {
            JobDTO job = new JobDTO()
            {
                Id = 1,
                State = State.Pending,
                Description = "New job"
            };
            Mock<IJobLogic> mock = new Mock<IJobLogic>(MockBehavior.Strict);
            mock.Setup(m => m.UpdateStateJob(1, job));
            JobController controller = new JobController(mock.Object);

            var result = controller.ModifyStateJob(1, job);
            OkResult okResult = result as OkResult;

            mock.VerifyAll();
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}
