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

            Image image = new Image
            {
                Id = 1,
                Path = "Imagen1"
            };

            List<Image> images = new List<Image>();
            images.Add(image);

            Report report1 = new Report
            {
                Id = 1,
                Product = product,
                VisitDate = DateTime.Now,
                TimeArrival = DateTime.Now,
                TimeResolution = DateTime.Now,
                Employee = employee,
                Summary = "Se arreglo la heladera",
                Detail = "Le faltaba gas",
                Comment = "Tener cuidado al abrir que esta llena",
                Images = images
            };

            Report report2 = new Report
            {
                Id = 2,
                Product = product,
                VisitDate = DateTime.Now,
                TimeArrival = DateTime.Now,
                TimeResolution = DateTime.Now,
                Employee = employee,
                Summary = "Se arreglo la cocina",
                Detail = "Estaba tapado el caño del gas",
                Comment = "Proxima vez llevar otro caño",
                Images = images
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
                Images = imagesExpected
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
                Images = imagesExpected
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

            Image image = new Image
            {
                Id = 1,
                Path = "Imagen1"
            };

            List<Image> images = new List<Image>();
            images.Add(image);

            Report report1 = new Report
            {
                Id = 1,
                Product = product,
                VisitDate = DateTime.Now,
                TimeArrival = DateTime.Now,
                TimeResolution = DateTime.Now,
                Employee = employee,
                Summary = "Se arreglo la heladera",
                Detail = "Le faltaba gas",
                Comment = "Tener cuidado al abrir que esta llena",
                Images = images
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

            ICollection<string> imagesExpected = new List<string>();
            imagesExpected.Add("Imagen1");
            ReportDTO reportExpected1 = new ReportDTO
            {
                Id = 1,
                ProductName = "Cocina Samsung",
                VisitDate = "8/11/2021",
                EmployeeName = "José Pablo",
                Summary = "Se arreglo la heladera",
                Detail = "Le faltaba gas",
                Comment = "Tener cuidado al abrir que esta llena",
                Images = imagesExpected
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

            Image image = new Image
            {
                Id = 1,
                Path = "Imagen1"
            };

            List<Image> images = new List<Image>();
            images.Add(image);

            Report report1 = new Report
            {
                Id = 1,
                Product = product,
                VisitDate = DateTime.Now,
                TimeArrival = DateTime.Now,
                TimeResolution = DateTime.Now,
                Employee = employee,
                Summary = "Se arreglo la heladera",
                Detail = "Le faltaba gas",
                Comment = "Tener cuidado al abrir que esta llena",
                Images = images
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

            Image image = new Image
            {
                Id = 1,
                Path = "Imagen1"
            };

            List<Image> images = new List<Image>();
            images.Add(image);

            Report report1 = new Report
            {
                Id = 1,
                Product = product,
                VisitDate = DateTime.Now,
                TimeArrival = DateTime.Now,
                TimeResolution = DateTime.Now,
                Employee = employee,
                Summary = null,
                Detail = "Le faltaba gas",
                Comment = "Tener cuidado al abrir que esta llena",
                Images = images
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
    }
}
