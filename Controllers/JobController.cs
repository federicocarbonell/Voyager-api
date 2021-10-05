using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoyageAPI.Adapter;
using VoyageAPI.Context;
using VoyageAPI.DTOs;
using State = VoyageAPI.Models.State;

namespace VoyageAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobController(ApplicationDbContext dataContext)
        {
            _context = dataContext;
        }

        [HttpGet("{employeeId}/pending", Name = "GetPendingJobs")]
        public ActionResult<JobDTO> GetPendingJobs([FromRoute] int employeeId)
        {
            List<JobDTO> result = JobAdapter.mapJobs(_context.Jobs.AsQueryable()
                .Where(job => (job.Employee.Id == employeeId && job.State == State.Pending))
                .Include(job => job.Product));;
            if (result == null)
            {
                return NotFound("No Jobs pending.");
            }

            return Ok(result);
        }
        
        [HttpGet("{employeeId}/inProcess", Name = "GetInProcessJobs")]
        public ActionResult<JobDTO> GetInProcessJobs([FromRoute] int employeeId)
        {
            List<JobDTO> result = JobAdapter.mapJobs(_context.Jobs.AsQueryable()
                .Where(job => (job.Employee.Id == employeeId && job.State == State.InProcess))
                .Include(job => job.Product));
            if (result == null)
            {
                return NotFound("No Jobs in process.");
            }

            return Ok(result);
        }
        
        [HttpGet("{employeeId}/finished", Name = "GetFinishedJobs")]
        public ActionResult<JobDTO> GetFinishedJobs([FromRoute] int employeeId)
        {
            List<JobDTO> result = JobAdapter.mapJobs(_context.Jobs.AsQueryable()
                .Where(job => (job.Employee.Id == employeeId && job.State == State.Finished))
                .Include(job => job.Product));
            if (result == null)
            {
                return NotFound("No Jobs finished.");
            }

            return Ok(result);
        }
    }
}