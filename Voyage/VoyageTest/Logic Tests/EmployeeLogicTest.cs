using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using VoyageAPI.Context;
using VoyageAPI.DTOs;
using VoyageAPI.Logic;
using VoyageAPI.Models;

namespace VoyageTest
{
    [TestClass]
    public class EmployeeLogicTest
    {
        EmployeeLogic employeeLogic;

        [TestMethod]
        public void TestLogin()
        {

            Employee e1 = new Employee
            {
                Id = 10,
                Email = "rodrigo@gmail.com",
                Password = "abc",
                Name = "Rodrigo"
            };
            Employee e2 = new Employee
            {
                Id = 20,
                Email = "fede@gmail.com",
                Password = "abc",
                Name = "Fede"
            };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(new Guid().ToString())
                .Options;
            var context = new ApplicationDbContext(options);
            context.Employees.Add(e1);
            context.Employees.Add(e2);
            context.SaveChanges();
            employeeLogic = new EmployeeLogic(context);
            EmployeeDTO expected = new EmployeeDTO
            {
                Id = 10,
                Name = "Rodrigo"
            };
            EmployeeDTO result = employeeLogic.EmployeeLogin("rodrigo@gmail.com", "abc");
            Assert.AreEqual(expected.Id, result.Id);
        }
    }
}