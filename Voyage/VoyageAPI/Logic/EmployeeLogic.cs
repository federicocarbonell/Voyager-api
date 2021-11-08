using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using VoyageAPI.Context;
using VoyageAPI.DTOs;
using VoyageAPI.Models;

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
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Ahora este es nuestro secreto.");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, emp.Name)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            emp.Token = tokenHandler.WriteToken(token);
            _context.Employees.Update(emp);
            _context.SaveChanges();

            EmployeeDTO employeeDTO = new EmployeeDTO
            {
                Id = emp.Id,
                Name = emp.Name,
                Token = emp.Token
            };
            return employeeDTO;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }
    }
}
