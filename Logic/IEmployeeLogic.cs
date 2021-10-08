using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoyageAPI.DTOs;

namespace VoyageAPI.Logic
{
    public interface IEmployeeLogic
    {
        public EmployeeDTO EmployeeLogin(string email, string password);
    }
}
