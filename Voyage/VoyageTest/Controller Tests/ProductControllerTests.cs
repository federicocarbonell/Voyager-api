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
    }
}
