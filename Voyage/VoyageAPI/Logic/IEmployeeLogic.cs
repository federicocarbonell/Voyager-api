using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoyageAPI.DTOs;
using VoyageAPI.Models;

namespace VoyageAPI.Logic
{
    public interface IEmployeeLogic
    {
        public EmployeeDTO EmployeeLogin(string email, string password);
        public IEnumerable<Employee> GetAll();
    }
}
