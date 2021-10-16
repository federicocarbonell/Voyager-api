using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VoyageAPI.Context;
using VoyageAPI.Models;

namespace VoyageTest.LogicTests
{
    [TestClass]
    public class JobLogicTests
    {
        private List<Job> jobsToReturn;
        private Product product;
        private Employee employee;

        public void InitializeContext(State state)
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
        public void GetPendingJobsTestOk()
        {
            InitializeContext(State.Pending);
            Mock<ApplicationDbContext> mock = new Mock<ApplicationDbContext>(MockBehavior.Strict);
            mock.Setup(m => m.Jobs.AsQueryable().Where(job => (job.Employee.Id == 1 && job.State == State.Pending))
                .Include(job => job.Product)).Returns();
        }
    }
}
