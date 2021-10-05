using Microsoft.AspNetCore.Mvc;
using VoyageAPI.DTOs;
using VoyageAPI.Context;
using System.Linq;

namespace VoyageAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext dataContext)
        {
            _context = dataContext;
        }

        [HttpPost()]
        public ActionResult<EmployeeDTO> Get([FromBody] EmployeeLogin employee)
        {
            var emp = _context.Employees.FirstOrDefault(e => e.Email.Equals(employee.Email) && e.Password.Equals(employee.Password));
            if (emp == null)
            {
                return NotFound("User does not exist.");
            }
            EmployeeDTO employeeDTO = new EmployeeDTO
            {
                Id = emp.Id,
                Name = emp.Name
            };
            return Ok(employeeDTO);
        }
        
    }
}
