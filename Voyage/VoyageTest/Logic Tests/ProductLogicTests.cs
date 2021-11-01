using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VoyageAPI.Context;
using VoyageAPI.DTOs;
using VoyageAPI.Logic;
using VoyageAPI.Models;

namespace VoyageTest.LogicTests
{
    [TestClass]
    public class ProductLogicTests
    {
        private ApplicationDbContext context;

        [TestCleanup]
        public void CleanUp()
        {
            this.context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void TestGetProductInfoOk()
        {
            Product product = new Product
            {
                Id = 1,
                Name = "Panasonic 5000",
                Description = "Heladera panasonic",
                Year = 2021
            };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(new Guid().ToString())
                .Options;
            context = new ApplicationDbContext(options);
            context.Products.Add(product);
            context.SaveChanges();

            ProductLogic productLogic = new ProductLogic(context);
            ProductDTO expectedResult = new ProductDTO
            {
                Id = 1,
                Name = "Panasonic 5000",
                Description = "Heladera panasonic",
                Year = 2021
            };

            ProductDTO result = productLogic.GetProductInfo(1);
            Assert.AreEqual(result.Id, expectedResult.Id);
            Assert.AreEqual(result.Name, expectedResult.Name);
            Assert.AreEqual(result.Description, expectedResult.Description);
            Assert.AreEqual(result.Year, expectedResult.Year);
        }

        [TestMethod]
        public void TestGetProductInfoNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(new Guid().ToString())
                .Options;
            context = new ApplicationDbContext(options);
            context.SaveChanges();

            ProductLogic productLogic = new ProductLogic(context);
            ProductDTO expectedResult = null;

            ProductDTO result = productLogic.GetProductInfo(1);
            Assert.AreEqual(result, expectedResult);
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
                VisitDate =  DateTime.Now,
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
            context.SaveChanges();

            ProductLogic productLogic = new ProductLogic(context);

            ICollection<ReportDTO> result = productLogic.GetReport(1);

            ICollection<string> imagesExpected = new List<string>();
            imagesExpected.Add("Imagen1");
            imagesExpected.Add("Imagen2");
            ReportDTO reportExpected1 = new ReportDTO
            {
                Id = 1,
                ProductName = "Heladera Samsung",
                VisitDate = "27/oct/2021",
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
                VisitDate = "27/oct/2021",
                EmployeeName = "José Pablo",
                Summary = "Se arreglo la cocina",
                Detail = "Estaba tapado el caño del gas",
                Comment = "Proxima vez llevar otro caño",
                Images = imagesExpected
            };
            ICollection<ReportDTO> reportsExpected = new List<ReportDTO>();
            reportsExpected.Add(reportExpected1);
            reportsExpected.Add(reportExpected2);
            foreach(ReportDTO reports in result)
            {
                reportsExpected.Contains(reports);
            }
        }

        [TestMethod]
        public void TestGetProductReportsNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(new Guid().ToString())
                .Options;
            context = new ApplicationDbContext(options);
            context.SaveChanges();

            ProductLogic productLogic = new ProductLogic(context);
            ICollection<ReportDTO> expectedResult = new List<ReportDTO>();

            ICollection<ReportDTO> result = productLogic.GetReport(1);
            Assert.AreEqual(result.Count, expectedResult.Count);
        }
    }
}
