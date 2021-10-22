using Microsoft.AspNetCore.Mvc;
using VoyageAPI.DTOs;
using VoyageAPI.Context;
using System.Linq;
using VoyageAPI.Logic;

namespace VoyageAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmployeeLogic _employeeLogic;

        public EmployeeController(ApplicationDbContext dataContext, IEmployeeLogic employeeLogic)
        {
            _context = dataContext;
            _employeeLogic = employeeLogic;
        }

        [HttpPost()]
        public ActionResult<EmployeeDTO> Get([FromBody] EmployeeLogin employee)
        {
            var emp = _employeeLogic.EmployeeLogin(employee.Email, employee.Password);
            if (emp == null)
            {
                return NotFound();
            }
            return Ok(emp);
        }
        
    }
}
