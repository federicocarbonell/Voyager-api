using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using VoyageAPI.Context;
using VoyageAPI.DTOs;
using VoyageAPI.Filter;
using VoyageAPI.Logic;
using VoyageAPI.Models;

namespace VoyageTest.Logic_Tests
{
    [TestClass]
    public class FilterAuthorizationTest
    {
        private ApplicationDbContext context;

        [TestCleanup]
        public void CleanUp()
        {
            this.context.Database.EnsureDeleted();
        }
        
        [TestMethod]
        public void AuthorizationAttributeFilterValidToken()
        {
            IHeaderDictionary headers = new HeaderDictionary();
            headers.Add("Authorization", "TestToken");
            Mock<HttpRequest> mockHttpRequest = new Mock<HttpRequest>();
            mockHttpRequest.Setup(r => r.Headers).Returns(headers);
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(h => h.Request).Returns(mockHttpRequest.Object);
            ActionContext actionContext = new ActionContext(
                mockHttpContext.Object,
                new Mock<Microsoft.AspNetCore.Routing.RouteData>().Object,
                new Mock<ActionDescriptor>().Object);
            AuthorizationFilterContext actionExecutingContext = new AuthorizationFilterContext(
                actionContext,
                new Mock<IList<IFilterMetadata>>().Object);
            Employee employee = new Employee { Id = 1, Name = "Jose Pablo", Email = "josepablogoni@gmail.com", Password = "JosePablo", Token = "TestToken" };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(new Guid().ToString())
                .Options;
            context = new ApplicationDbContext(options);
            context.Employees.Add(employee);
            context.SaveChanges();
            Mock<EmployeeLogic> logic = new Mock<EmployeeLogic>(context);
            AuthorizationFilter filter = new AuthorizationFilter(logic.Object);

            filter.OnAuthorization(actionExecutingContext);

            Assert.IsNull(actionExecutingContext.Result);
        }
        

        [TestMethod]
        public void AuthorizationAttributeFilterInvalidToken()
        {
            IHeaderDictionary headers = new HeaderDictionary();
            headers.Add("Authorization", "dasnj2323nds");
            Mock<HttpRequest> mockHttpRequest = new Mock<HttpRequest>();
            mockHttpRequest.Setup(r => r.Headers).Returns(headers);
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(h => h.Request).Returns(mockHttpRequest.Object);
            ActionContext actionContext = new ActionContext(
                mockHttpContext.Object,
                new Mock<Microsoft.AspNetCore.Routing.RouteData>().Object,
                new Mock<ActionDescriptor>().Object);
            AuthorizationFilterContext actionExecutingContext = new AuthorizationFilterContext(
                actionContext,
                new Mock<IList<IFilterMetadata>>().Object);
            Employee employee = new Employee { Id = 1, Name = "Jose Pablo", Email = "josepablogoni@gmail.com", Password = "JosePablo" };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(new Guid().ToString())
                .Options;
            context = new ApplicationDbContext(options);
            context.Employees.Add(employee);
            context.SaveChanges();
            Mock<EmployeeLogic> logic = new Mock<EmployeeLogic>(context);
            AuthorizationFilter filter = new AuthorizationFilter(logic.Object);

            filter.OnAuthorization(actionExecutingContext);
            var result = actionExecutingContext.Result as ContentResult;

            Assert.AreEqual(403, result.StatusCode);
        }

        [TestMethod]
        public void AuthorizationAttributeFilterNotToken()
        {
            IHeaderDictionary headers = new HeaderDictionary();
            headers.Add("Authorization", "");
            Mock<HttpRequest> mockHttpRequest = new Mock<HttpRequest>();
            mockHttpRequest.Setup(r => r.Headers).Returns(headers);
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(h => h.Request).Returns(mockHttpRequest.Object);
            ActionContext actionContext = new ActionContext(
                mockHttpContext.Object,
                new Mock<Microsoft.AspNetCore.Routing.RouteData>().Object,
                new Mock<ActionDescriptor>().Object);
            AuthorizationFilterContext actionExecutingContext = new AuthorizationFilterContext(
                actionContext,
                new Mock<IList<IFilterMetadata>>().Object);
            Employee employee = new Employee { Id = 1, Name = "Jose Pablo", Email = "josepablogoni@gmail.com", Password = "JosePablo" };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(new Guid().ToString())
                .Options;
            context = new ApplicationDbContext(options);
            context.Employees.Add(employee);
            context.SaveChanges();
            Mock<EmployeeLogic> logic = new Mock<EmployeeLogic>(context);
            AuthorizationFilter filter = new AuthorizationFilter(logic.Object);

            filter.OnAuthorization(actionExecutingContext);
            var result = actionExecutingContext.Result as ContentResult;

            Assert.AreEqual(401, result.StatusCode);
        }
    }
}
