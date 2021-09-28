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

        [HttpGet()]
        public ActionResult<EmployeeDTO> Get([FromQuery] int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id.Equals(id));
            if (employee == null)
            {
                return NotFound("User does not exist.");
            }
            EmployeeDTO employeeDTO = new EmployeeDTO
            {
                Id = employee.Id,
                Name = employee.Name
            };
            return Ok(employeeDTO);
        }
        
    }
}
