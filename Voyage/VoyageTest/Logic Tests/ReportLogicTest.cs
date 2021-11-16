using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using VoyageAPI.Context;
using VoyageAPI.DTOs;
using VoyageAPI.Logic;
using VoyageAPI.Models;

namespace VoyageTest.Logic_Tests
{
    [TestClass]
    public class ReportLogicTest
    {
        private ApplicationDbContext context;

        [TestCleanup]
        public void CleanUp()
        {
            this.context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void TestGetProductReportOk()
        {
            Product product = new Product
            {
                Id = 1,
                Name = "Cocina Samsung",
                Description = "Heladera panasonic",
                Year = 2021
            };

            Employee employee = new Employee
            {
                Id = 1,
                Name = "José Pablo",
                Email = "josepablogoni@gmail.com",
                Password = "josepablo"
            };

            Report report1 = new Report
            {
                Id = 1,
                Product = product,
                VisitDate = DateTime.Now.ToString(),
                TimeArrival = DateTime.Now.ToString(),
                TimeResolution = DateTime.Now.ToString(),
                Employee = employee,
                Summary = "Se arreglo la heladera",
                Detail = "Le faltaba gas",
                Comment = "Tener cuidado al abrir que esta llena",
                Image = "abc"
            };

            Report report2 = new Report
            {
                Id = 2,
                Product = product,
                VisitDate = DateTime.Now.ToString(),
                TimeArrival = DateTime.Now.ToString(),
                TimeResolution = DateTime.Now.ToString(),
                Employee = employee,
                Summary = "Se arreglo la cocina",
                Detail = "Estaba tapado el caño del gas",
                Comment = "Proxima vez llevar otro caño",
                Image = "abc"
            };


            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(new Guid().ToString())
                .Options;
            context = new ApplicationDbContext(options);
            context.Reports.Add(report1);
            context.Reports.Add(report2);
            context.Employees.Add(employee);
            context.SaveChanges();

            ReportLogic reportLogic = new ReportLogic(context);

            ICollection<ReportDTO> result = reportLogic.GetReport(1);

            ICollection<string> imagesExpected = new List<string>();
            imagesExpected.Add("Imagen1");
            imagesExpected.Add("Imagen2");
            ReportDTO reportExpected1 = new ReportDTO
            {
                Id = 1,
                ProductName = "Cocina Samsung",
                VisitDate = "8/11/2021",
                EmployeeName = "José Pablo",
                Summary = "Se arreglo la heladera",
                Detail = "Le faltaba gas",
                Comment = "Tener cuidado al abrir que esta llena",
                Image = "abc"
            };


            ReportDTO reportExpected2 = new ReportDTO
            {
                Id = 2,
                ProductName = "Cocina Samsung",
                VisitDate = "8/11/2021",
                EmployeeName = "José Pablo",
                Summary = "Se arreglo la cocina",
                Detail = "Estaba tapado el caño del gas",
                Comment = "Proxima vez llevar otro caño",
                Image = "abc"
            };
            ICollection<ReportDTO> reportsExpected = new List<ReportDTO>();
            reportsExpected.Add(reportExpected1);
            reportsExpected.Add(reportExpected2);
            Assert.IsTrue(reportsExpected.Count == result.Count);
        }

        [TestMethod]
        public void TestGetProductReportsNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(new Guid().ToString())
                .Options;
            context = new ApplicationDbContext(options);
            context.SaveChanges();

            ReportLogic reportLogic = new ReportLogic(context);
            ICollection<ReportDTO> expectedResult = new List<ReportDTO>();

            ICollection<ReportDTO> result = reportLogic.GetReport(1);
            Assert.AreEqual(result.Count, expectedResult.Count);
        }

        [TestMethod]
        public void TestAddProductOK()
        {
            Product product = new Product
            {
                Id = 1,
                Name = "Cocina Samsung",
                Description = "Heladera panasonic",
                Year = 2021
            };

            Employee employee = new Employee
            {
                Id = 1,
                Name = "José Pablo",
                Email = "josepablogoni@gmail.com",
                Password = "josepablo"
            };

            ReportToAddDTO report1 = new ReportToAddDTO
            {
                ProductId = product.Id,
                ArrivedTime = DateTime.Now.ToString(),
                EmployeeId = employee.Id,
                Summary = "Se arreglo la heladera",
                Detail = "Le faltaba gas",
                Comment = "Tener cuidado al abrir que esta llena",
                Image = "abc"
            };


            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(new Guid().ToString())
                .Options;
            context = new ApplicationDbContext(options);
            context.Products.Add(product);
            context.Employees.Add(employee);
            context.SaveChanges();

            ReportLogic reportLogic = new ReportLogic(context);

            ReportDTO result = reportLogic.AddReport(1,report1);
            ReportDTO reportExpected1 = new ReportDTO
            {
                Id = 1,
                ProductName = "Cocina Samsung",
                VisitDate = DateTime.Now.ToString(),
                EmployeeName = "José Pablo",
                Summary = "Se arreglo la heladera",
                Detail = "Le faltaba gas",
                Comment = "Tener cuidado al abrir que esta llena",
                Image = "abc"
            };

            Assert.IsTrue(reportExpected1.Id == result.Id);
            Assert.IsTrue(reportExpected1.ProductName == result.ProductName);
            Assert.IsTrue(reportExpected1.VisitDate == result.VisitDate);
            Assert.IsTrue(reportExpected1.EmployeeName == result.EmployeeName);
            Assert.IsTrue(reportExpected1.Summary == result.Summary);
            Assert.IsTrue(reportExpected1.Detail == result.Detail);
            Assert.IsTrue(reportExpected1.Comment == result.Comment);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException), "Incorrect product ID.")]
        public void TestAddProductFailNotFoundProductID()
        {
            Product product = new Product
            {
                Id = 1,
                Name = "Cocina Samsung",
                Description = "Heladera panasonic",
                Year = 2021
            };

            Employee employee = new Employee
            {
                Id = 1,
                Name = "José Pablo",
                Email = "josepablogoni@gmail.com",
                Password = "josepablo"
            };


            ReportToAddDTO report1 = new ReportToAddDTO
            {
                ProductId = product.Id,
                ArrivedTime = DateTime.Now.ToString(),
                EmployeeId = employee.Id,
                Summary = "Se arreglo la heladera",
                Detail = "Le faltaba gas",
                Comment = "Tener cuidado al abrir que esta llena",
                Image = "abc"
            };


            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(new Guid().ToString())
                .Options;
            context = new ApplicationDbContext(options);
            context.SaveChanges();

            ReportLogic reportLogic = new ReportLogic(context);

            reportLogic.AddReport(1, report1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The report must contain a summary.")]
        public void TestAddProductFailNullSummary()
        {
            Product product = new Product
            {
                Id = 1,
                Name = "Cocina Samsung",
                Description = "Heladera panasonic",
                Year = 2021
            };

            Employee employee = new Employee
            {
                Id = 1,
                Name = "José Pablo",
                Email = "josepablogoni@gmail.com",
                Password = "josepablo"
            };

            ReportToAddDTO report1 = new ReportToAddDTO
            {
                ProductId = product.Id,
                ArrivedTime = DateTime.Now.ToString(),
                EmployeeId = employee.Id,
                Detail = "Le faltaba gas",
                Comment = "Tener cuidado al abrir que esta llena",
                Image = "abc"
            };


            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(new Guid().ToString())
                .Options;
            context = new ApplicationDbContext(options);
            context.Products.Add(product);
            context.Employees.Add(employee);
            context.SaveChanges();

            ReportLogic reportLogic = new ReportLogic(context);

            reportLogic.AddReport(1, report1);
        }

        [TestMethod]
        public void TestGetProductReportDetailOk()
        {
            Product product = new Product
            {
                Id = 1,
                Name = "Cocina Samsung",
                Description = "Heladera panasonic",
                Year = 2021
            };

            Employee employee = new Employee
            {
                Id = 1,
                Name = "José Pablo",
                Email = "josepablogoni@gmail.com",
                Password = "josepablo"
            };

            Report report1 = new Report
            {
                Id = 1,
                Product = product,
                VisitDate = "8/11/2021",
                TimeArrival = DateTime.Now.ToString(),
                TimeResolution = DateTime.Now.ToString(),
                Employee = employee,
                Summary = "Se arreglo la heladera",
                Detail = "Le faltaba gas",
                Comment = "Tener cuidado al abrir que esta llena",
                Image = "abc"
            };


            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(new Guid().ToString())
                .Options;
            context = new ApplicationDbContext(options);
            context.Reports.Add(report1);
            context.Employees.Add(employee);
            context.Products.Add(product);
            context.SaveChanges();

            ReportLogic reportLogic = new ReportLogic(context);

            ReportDTO result = reportLogic.GetReportDetail(1);

            ReportDTO reportExpected1 = new ReportDTO
            {
                Id = 1,
                ProductName = "Cocina Samsung",
                VisitDate = "8/11/2021",
                EmployeeName = "José Pablo",
                Summary = "Se arreglo la heladera",
                Detail = "Le faltaba gas",
                Comment = "Tener cuidado al abrir que esta llena",
                Image = "abc"

            };

            Assert.IsTrue(reportExpected1.Id == result.Id);
            Assert.IsTrue(reportExpected1.ProductName == result.ProductName);
            Assert.IsTrue(reportExpected1.VisitDate == result.VisitDate);
            Assert.IsTrue(reportExpected1.EmployeeName == result.EmployeeName);
            Assert.IsTrue(reportExpected1.Summary == result.Summary);
            Assert.IsTrue(reportExpected1.Detail == result.Detail);
            Assert.IsTrue(reportExpected1.Comment == result.Comment);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException), "Incorrect report ID.")]
        public void TestGetProductReportsDetailNotFound()
        {
            Product product = new Product
            {
                Id = 1,
                Name = "Cocina Samsung",
                Description = "Heladera panasonic",
                Year = 2021
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(new Guid().ToString())
                .Options;
            context = new ApplicationDbContext(options);
            context.Products.Add(product);
            context.SaveChanges();

            ReportLogic reportLogic = new ReportLogic(context);

            reportLogic.GetReportDetail(1);

        }
    }
}
