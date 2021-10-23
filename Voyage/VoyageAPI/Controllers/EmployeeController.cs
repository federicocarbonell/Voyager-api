using Microsoft.AspNetCore.Mvc;
using VoyageAPI.DTOs;
using VoyageAPI.Context;
using System.Linq;
using VoyageAPI.Logic;
using System;

namespace VoyageAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeLogic _employeeLogic;

        public EmployeeController(IEmployeeLogic employeeLogic)
        {
            _employeeLogic = employeeLogic;
        }

        [HttpPost()]
        public ActionResult<EmployeeDTO> Login([FromBody] EmployeeLogin employee)
        {
            try
            {
                var emp = _employeeLogic.EmployeeLogin(employee.Email, employee.Password);
                if (emp == null)
                {
                    return NotFound("Usuario no encontrado.");
                }
                return Ok(emp);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}