using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoyageAPI.Context;
using VoyageAPI.DTOs;

namespace VoyageAPI.Logic
{
    public class EmployeeLogic : IEmployeeLogic
    {
        ApplicationDbContext _context;
        public EmployeeLogic(ApplicationDbContext context)
        {
            _context = context;
        }
        public EmployeeDTO EmployeeLogin(string email, string password)
        {
            var emp = _context.Employees.FirstOrDefault(e => e.Email.Equals(email) && e.Password.Equals(password));
            if (emp == null)
            {
                return null;
            }
            EmployeeDTO employeeDTO = new EmployeeDTO
            {
                Id = emp.Id,
                Name = emp.Name
            };
            return employeeDTO;
        }
    }
}
