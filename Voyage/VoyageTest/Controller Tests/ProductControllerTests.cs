using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VoyageAPI.Controllers;
using VoyageAPI.DTOs;
using VoyageAPI.Logic;

namespace VoyageTest.ControllerTests
{
    [TestClass]
    public class ProductControllerTests
    {
        [TestMethod]
        public void TestGetProductInfoOk()
        {
            ProductDTO productToReturn = new ProductDTO
            {
                Id = 1,
                Name = "Panasonic 5000",
                Description = "Heladera panasonic",
                Year = 2021
            };
            Mock<IProductLogic> mock = new Mock<IProductLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetProductInfo(1)).Returns(productToReturn);

            ProductController controller = new ProductController(mock.Object);
            var result = controller.GetProductInfo(1);
            OkObjectResult okResult = result.Result as OkObjectResult;
            ProductDTO resultProduct = okResult.Value as ProductDTO;

            mock.VerifyAll();
            Assert.AreEqual(productToReturn.Id, resultProduct.Id);
            Assert.AreEqual(productToReturn.Name, resultProduct.Name);
            Assert.AreEqual(productToReturn.Description, resultProduct.Description);
            Assert.AreEqual(productToReturn.Year, resultProduct.Year);
        }

        [TestMethod]
        public void TestGetProductInfoNotFound()
        {
            ProductDTO productToReturn = null;
            Mock<IProductLogic> mock = new Mock<IProductLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetProductInfo(1)).Returns(productToReturn);

            ProductController controller = new ProductController(mock.Object);
            var result = controller.GetProductInfo(1);
            NotFoundObjectResult notFoundResult = result.Result as NotFoundObjectResult;

            mock.VerifyAll();
            Assert.AreEqual("No Product found.", notFoundResult.Value);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

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
            Mock<IProductLogic> mock = new Mock<IProductLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetReport(1)).Returns(reportsReturn);
            ProductDTO productToReturn = new ProductDTO
            {
                Id = 1,
                Name = "Panasonic 5000",
                Description = "Heladera panasonic",
                Year = 2021
            };
            mock.Setup(m => m.GetProductInfo(1)).Returns(productToReturn);
            ProductController controller = new ProductController(mock.Object);
            var result = controller.GetProductReport(1);
            OkObjectResult okResult = result.Result as OkObjectResult;
            ICollection<ReportDTO> resultReports = okResult.Value as ICollection<ReportDTO>;

            mock.VerifyAll();
            foreach(ReportDTO report in resultReports)
            {
                Assert.IsTrue(reportsReturn.Contains(report));
            }
        }

        [TestMethod]
        public void TestGetProductReportNotFound()
        {
            ICollection<ReportDTO> reportsReturn = new List<ReportDTO>();
            Mock<IProductLogic> mock = new Mock<IProductLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetReport(1)).Returns(reportsReturn);
            ProductDTO productToReturn = new ProductDTO
            {
                Id = 1,
                Name = "Panasonic 5000",
                Description = "Heladera panasonic",
                Year = 2021
            };
            mock.Setup(m => m.GetProductInfo(1)).Returns(productToReturn);
            ProductController controller = new ProductController(mock.Object);
            var result = controller.GetProductReport(1);
            OkObjectResult okResult = result.Result as OkObjectResult;
            ICollection<ReportDTO> resultReports = okResult.Value as ICollection<ReportDTO>;

            mock.VerifyAll();
            Assert.AreEqual(resultReports.Count, reportsReturn.Count);
        }

        [TestMethod]
        public void TestGetProductReportsNotFoundProductId()
        {
            ProductDTO productToReturn = null;
            Mock<IProductLogic> mock = new Mock<IProductLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetProductInfo(1)).Returns(productToReturn);

            ProductController controller = new ProductController(mock.Object);
            var result = controller.GetProductReport(1);
            NotFoundObjectResult notFoundResult = result.Result as NotFoundObjectResult;

            mock.VerifyAll();
            Assert.AreEqual("No Product found.", notFoundResult.Value);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }
    }
}
