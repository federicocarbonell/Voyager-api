using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VoyageAPI.Context;
using VoyageAPI.Controllers;
using VoyageAPI.DTOs;
using VoyageAPI.Logic;

namespace VoyageTests
{
    [TestClass]
    public class EmployeeControllerTest
    {
        [TestMethod]
        public void TestLoginOk()
        {
            Mock<IEmployeeLogic> emLogic = new Mock<IEmployeeLogic>(MockBehavior.Strict);
            EmployeeDTO ex = new EmployeeDTO { Id = 1, Name = "Rodrigo" };
            emLogic.Setup(e => e.EmployeeLogin("abc", "Rodrigo")).Returns(ex);
            EmployeeController emController = new EmployeeController(emLogic.Object);
            EmployeeLogin el = new EmployeeLogin { Email = "abc", Password = "Rodrigo" };
            var result = emController.Login(el);
            ActionResult<EmployeeDTO> emEx = new ActionResult<EmployeeDTO>(ex);
            var result2 = result.Result as OkObjectResult;
            emLogic.VerifyAll();
            Assert.AreEqual(emEx.Value, result2.Value);
        }

        [TestMethod]
        public void TestLoginBad()
        {
            Mock<IEmployeeLogic> emLogic = new Mock<IEmployeeLogic>(MockBehavior.Strict);
            NotFoundObjectResult expectedResult = new NotFoundObjectResult("Usuario no encontrado.");
            EmployeeDTO nullEmployee = null;
            emLogic.Setup(e => e.EmployeeLogin("abc", "Rodrigo")).Returns(nullEmployee);
            EmployeeController emController = new EmployeeController(emLogic.Object);
            EmployeeLogin el = new EmployeeLogin { Email = "abc", Password = "Rodrigo" };
            var result = emController.Login(el);
            var result2 = result.Result as NotFoundObjectResult;
            emLogic.VerifyAll();
            Assert.AreEqual(expectedResult.StatusCode, result2.StatusCode);
        }
    }
}