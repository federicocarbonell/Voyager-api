using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VoyageAPI.Context;
using VoyageAPI.DTOs;
using VoyageAPI.Logic;
using VoyageAPI.Models;

namespace VoyageTest.LogicTests
{
    [TestClass]
    public class JobLogicTests
    {
        private List<Job> jobsToReturn;
        private Product product;
        private ProductDTO productDTO;
        private Employee employee;
        private Job oneJob;
        private Job anotherJob;
        private JobLogic jobLogic;
        private ApplicationDbContext context;

        [TestCleanup]
        public void CleanUp()
        {
            this.context.Database.EnsureDeleted();
        }

        public void InitializeContext(VoyageAPI.Models.State state)
        {
            employee = new Employee { Id = 1, Name = "Fede", Email = "Fede@fede.com", Jobs = jobsToReturn, Password = "fede" };
            product = new Product { Id = 1, Description = "Freezer", Name = "Panasonic", Year = 2020 };
            productDTO = new ProductDTO { Id = 1, Description = "Freezer", Name = "Panasonic", Year = 2020 }; ;
            oneJob = new Job
            {
                Id = 1,
                Description = "Test",
                Latitude = "1",
                Longitude = "1",
                Product = product,
                State = state,
                Time = "15:00",
                Employee = employee
            };
            anotherJob = new Job
            {
                Id = 2,
                Description = "Test 2",
                Latitude = "2",
                Longitude = "2",
                Product = product,
                State = state,
                Time = "16:00",
                Employee = employee
            };
        }

        [TestMethod]
        public void GetPendingJobsTestOk()
        {
            InitializeContext(VoyageAPI.Models.State.Pending);
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(new Guid().ToString())
                .Options;
            context = new ApplicationDbContext(options);
            context.Jobs.Add(oneJob);
            context.Jobs.Add(anotherJob);
            context.SaveChanges();
            jobLogic = new JobLogic(context);
            List<JobDTO> expectedResult = new List<JobDTO>
            {
                new JobDTO
                {
                    Id = 1,
                    Description = "Test",
                    Latitude = "1",
                    Longitude = "1",
                    Product = productDTO,
                    State = VoyageAPI.DTOs.State.Pending,
                    Time = "15:00",
                },
                new JobDTO
                {
                    Id = 2,
                    Description = "Test 2",
                    Latitude = "2",
                    Longitude = "2",
                    Product = productDTO,
                    State = VoyageAPI.DTOs.State.Pending,
                    Time = "16:00",
                },
            };

            List<JobDTO> result = jobLogic.GetPendingJobs(employee.Id);
            Assert.AreEqual(expectedResult.Count(), result.Count());
        }

        [TestMethod]
        public void GetPendingJobsTestEmpty()
        {
            InitializeContext(VoyageAPI.Models.State.Pending);
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(new Guid().ToString())
                .Options;
            context = new ApplicationDbContext(options);
            context.Jobs.Add(oneJob);
            context.Jobs.Add(anotherJob);
            context.SaveChanges();
            jobLogic = new JobLogic(context);
            List<JobDTO> expectedResult = new List<JobDTO>
            {
            };

            List<JobDTO> result = jobLogic.GetPendingJobs(2);
            Assert.AreEqual(expectedResult.Count(), result.Count());
        }

        [TestMethod]
        public void GetInProcessJobsTestOk()
        {
            InitializeContext(VoyageAPI.Models.State.InProcess);
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(new Guid().ToString())
                .Options;
            context = new ApplicationDbContext(options);
            context.Jobs.Add(oneJob);
            context.Jobs.Add(anotherJob);
            context.SaveChanges();
            jobLogic = new JobLogic(context);
            List<JobDTO> expectedResult = new List<JobDTO>
            {
                new JobDTO
                {
                    Id = 1,
                    Description = "Test",
                    Latitude = "1",
                    Longitude = "1",
                    Product = productDTO,
                    State = VoyageAPI.DTOs.State.InProcess,
                    Time = "15:00",
                },
                new JobDTO
                {
                    Id = 2,
                    Description = "Test 2",
                    Latitude = "2",
                    Longitude = "2",
                    Product = productDTO,
                    State = VoyageAPI.DTOs.State.InProcess,
                    Time = "16:00",
                },
            };

            List<JobDTO> result = jobLogic.GetInProcessJobs(employee.Id);
            Assert.AreEqual(expectedResult.Count(), result.Count());
        }

        [TestMethod]
        public void GetInProcessJobsTestEmpty()
        {
            InitializeContext(VoyageAPI.Models.State.InProcess);
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(new Guid().ToString())
                .Options;
            context = new ApplicationDbContext(options);
            context.Jobs.Add(oneJob);
            context.Jobs.Add(anotherJob);
            context.SaveChanges();
            jobLogic = new JobLogic(context);
            List<JobDTO> expectedResult = new List<JobDTO>
            {
            };

            List<JobDTO> result = jobLogic.GetPendingJobs(2);
            Assert.AreEqual(expectedResult.Count(), result.Count());
        }

        [TestMethod]
        public void GetFinishedJobsTestOk()
        {
            InitializeContext(VoyageAPI.Models.State.Finished);
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(new Guid().ToString())
                .Options;
            context = new ApplicationDbContext(options);
            context.Jobs.Add(oneJob);
            context.Jobs.Add(anotherJob);
            context.SaveChanges();
            jobLogic = new JobLogic(context);
            List<JobDTO> expectedResult = new List<JobDTO>
            {
                new JobDTO
                {
                    Id = 1,
                    Description = "Test",
                    Latitude = "1",
                    Longitude = "1",
                    Product = productDTO,
                    State = VoyageAPI.DTOs.State.Finished,
                    Time = "15:00",
                },
                new JobDTO
                {
                    Id = 2,
                    Description = "Test 2",
                    Latitude = "2",
                    Longitude = "2",
                    Product = productDTO,
                    State = VoyageAPI.DTOs.State.Finished,
                    Time = "16:00",
                },
            };

            List<JobDTO> result = jobLogic.GetFinishedJobs(employee.Id);
            Assert.AreEqual(expectedResult.Count(), result.Count());
        }

        [TestMethod]
        public void GetFinishedJobsTestEmpty()
        {
            InitializeContext(VoyageAPI.Models.State.Finished);
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(new Guid().ToString())
                .Options;
            context = new ApplicationDbContext(options);
            context.Jobs.Add(oneJob);
            context.Jobs.Add(anotherJob);
            context.SaveChanges();
            jobLogic = new JobLogic(context);
            List<JobDTO> expectedResult = new List<JobDTO>
            {
            };

            List<JobDTO> result = jobLogic.GetFinishedJobs(2);
            Assert.AreEqual(expectedResult.Count(), result.Count());
        }
    }
}
