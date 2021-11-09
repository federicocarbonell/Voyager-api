using System;
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
    }
}
