using System;
using System.Collections.Generic;
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
        private Employee employee;

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
            jobsToReturn = new List<Job>()
            {
                new Job
                {
                    Id = 1,
                    Description = "Test",
                    Latitude = "1",
                    Longitude = "1",
                    Product = product,
                    State = state,
                    Time = "15:00",
                    Employee = employee
                },
                new Job
                {
                    Id = 2,
                    Description = "Test 2",
                    Latitude = "2",
                    Longitude = "2",
                    Product = product,
                    State = state,
                    Time = "16:00",
                    Employee = employee
                },
            };
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException), "Incorrect Id.")]
        public void ModifyStateJobFailId()
        {
            Job job = new Job()
            {
                Id = 1,
                State = VoyageAPI.Models.State.Pending,
                Description = "New job"
            };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                .UseInMemoryDatabase(new Guid().ToString())
                                .Options;
            context = new ApplicationDbContext(options);
            context.Jobs.Add(job);
            context.SaveChanges();
            IJobLogic jobLogic = new JobLogic(context);

            JobDTO jobExpected = new JobDTO()
            {
                Id = 1,
                State = VoyageAPI.DTOs.State.Pending,
                Description = "New job"
            };
            jobLogic.UpdateStateJob(-1, jobExpected);
        }

        [TestMethod]
        public void ModifyStateJobCorrect()
        {
            Job job = new Job()
            {
                Id = 1,
                State = VoyageAPI.Models.State.Pending,
                Description = "New job"
            };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                .UseInMemoryDatabase(new Guid().ToString())
                                .Options;
            context = new ApplicationDbContext(options);
            context.Jobs.Add(job);
            context.SaveChanges();
            IJobLogic jobLogic = new JobLogic(context);

            JobDTO jobExpected = new JobDTO()
            {
                Id = 1,
                State = VoyageAPI.DTOs.State.Finished,
                Description = "New job"
            };
            jobLogic.UpdateStateJob(1, jobExpected);
            Assert.AreEqual(context.Jobs.Find(1).State, VoyageAPI.Models.State.Finished);
        }
    }
}
