﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using VoyageAPI.Controllers;
using VoyageAPI.DTOs;
using VoyageAPI.Logic;
using VoyageAPI.Models;

namespace VoyageTest.Controller_Tests
{
    [TestClass]
    public class ReportControllerTest
    {
        [TestMethod]
        public void TestGetProductReportOk()
        {
            ICollection<string> images = new List<string>();
            images.Add("Imagen1");
            images.Add("Imagen2");
            ReportDTO report1 = new ReportDTO
            {
                Id = 1,
                ProductName = "Heladera Samsung",
                VisitDate = "27/oct/2021",
                EmployeeName = "José Pablo",
                Summary = "Se arreglo la heladera",
                Detail = "Le faltaba gas",
                Comment = "Tener cuidado al abrir que esta llena",
                Images = images
            };


            ReportDTO report2 = new ReportDTO
            {
                Id = 2,
                ProductName = "Cocina Samsung",
                VisitDate = "27/oct/2021",
                EmployeeName = "José Pablo",
                Summary = "Se arreglo la cocina",
                Detail = "Estaba tapado el caño del gas",
                Comment = "Proxima vez llevar otro caño",
                Images = images
            };
            ICollection<ReportDTO> reportsReturn = new List<ReportDTO>();
            reportsReturn.Add(report1);
            reportsReturn.Add(report2);
            Mock<IReportLogic> mockReport = new Mock<IReportLogic>(MockBehavior.Strict);
            Mock<IProductLogic> mockProduct = new Mock<IProductLogic>(MockBehavior.Strict);
            mockReport.Setup(m => m.GetReport(1)).Returns(reportsReturn);
            ProductDTO productToReturn = new ProductDTO
            {
                Id = 1,
                Name = "Panasonic 5000",
                Description = "Heladera panasonic",
                Year = 2021
            };
            mockProduct.Setup(m => m.GetProductInfo(1)).Returns(productToReturn);
            ReportController controller = new ReportController(mockReport.Object, mockProduct.Object);
            var result = controller.GetProductReport(1);
            OkObjectResult okResult = result.Result as OkObjectResult;
            ICollection<ReportDTO> resultReports = okResult.Value as ICollection<ReportDTO>;

            mockReport.VerifyAll();
            mockProduct.VerifyAll();
            foreach (ReportDTO report in resultReports)
            {
                Assert.IsTrue(reportsReturn.Contains(report));
            }
        }

        [TestMethod]
        public void TestGetProductReportNotFound()
        {
            ICollection<ReportDTO> reportsReturn = new List<ReportDTO>();
            Mock<IReportLogic> mockReport = new Mock<IReportLogic>(MockBehavior.Strict);
            Mock<IProductLogic> mockProduct = new Mock<IProductLogic>(MockBehavior.Strict);
            mockReport.Setup(m => m.GetReport(1)).Returns(reportsReturn);
            ProductDTO productToReturn = new ProductDTO
            {
                Id = 1,
                Name = "Panasonic 5000",
                Description = "Heladera panasonic",
                Year = 2021
            };
            mockProduct.Setup(m => m.GetProductInfo(1)).Returns(productToReturn);
            ReportController controller = new ReportController(mockReport.Object, mockProduct.Object);
            var result = controller.GetProductReport(1);
            OkObjectResult okResult = result.Result as OkObjectResult;
            ICollection<ReportDTO> resultReports = okResult.Value as ICollection<ReportDTO>;

            mockReport.VerifyAll();
            mockProduct.VerifyAll();
            Assert.AreEqual(resultReports.Count, reportsReturn.Count);
        }

        [TestMethod]
        public void TestGetProductReportsNotFoundProductId()
        {
            ProductDTO productToReturn = null;
            Mock<IReportLogic> mockReport = new Mock<IReportLogic>(MockBehavior.Strict);
            Mock<IProductLogic> mockProduct = new Mock<IProductLogic>(MockBehavior.Strict);
            mockProduct.Setup(m => m.GetProductInfo(1)).Returns(productToReturn);

            ReportController controller = new ReportController(mockReport.Object, mockProduct.Object);
            var result = controller.GetProductReport(1);
            NotFoundObjectResult notFoundResult = result.Result as NotFoundObjectResult;

            mockReport.VerifyAll();
            mockProduct.VerifyAll();
            Assert.AreEqual("No Product found.", notFoundResult.Value);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [TestMethod]
        public void TestCreateReportOk()
        {
            List<Image> images = new List<Image>();
            images.Add(new Image { Id = 1, Path = "Imagen1"});
            images.Add(new Image { Id = 2, Path = "Imagen2" });

            ICollection<string> imagesDTO = new List<string>();
            imagesDTO.Add("Imagen1");
            imagesDTO.Add("Imagen2");

            Report report = new Report
            {
                Id = 1,
                Product = new Product { Id = 1 },
                VisitDate = DateTime.Now,
                TimeArrival = DateTime.Now,
                TimeResolution = DateTime.Now,
                Summary = "Se arreglo la heladera",
                Detail = "Le faltaba gas",
                Comment = "Tener cuidado al abrir que esta llena",
                Images = images
            };

            ReportDTO reportDTO = new ReportDTO
            {
                Id = 1,
                ProductName = "Heladera Samsung",
                VisitDate = DateTime.Now.ToString(),
                EmployeeName = "José Pablo",
                Summary = "Se arreglo la heladera",
                Detail = "Le faltaba gas",
                Comment = "Tener cuidado al abrir que esta llena",
                Images = imagesDTO
            };

            ProductDTO productToReturn = new ProductDTO
            {
                Id = 1,
                Name = "Panasonic 5000",
                Description = "Heladera panasonic",
                Year = 2021
            };
            Mock<IProductLogic> mockProduct = new Mock<IProductLogic>(MockBehavior.Strict);
            Mock<IReportLogic> mockReport = new Mock<IReportLogic>(MockBehavior.Strict);
            mockReport.Setup(m => m.AddReport(1,report)).Returns(reportDTO);

            ReportController controller = new ReportController(mockReport.Object, mockProduct.Object);
            var result = controller.PostReport(1, report);
            OkObjectResult okResult = result.Result as OkObjectResult;

            mockReport.VerifyAll();

            Assert.AreEqual(reportDTO, okResult.Value);
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}
