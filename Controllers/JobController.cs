using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VoyageAPI.Context;
using VoyageAPI.DTOs;
using VoyageAPI.Logic;

namespace VoyageAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IJobLogic _jobLogic;

        public JobController(IJobLogic jobLogic)
        {
            _jobLogic = jobLogic;
        }

        [HttpGet("{employeeId}/pending", Name = "GetPendingJobs")]
        public ActionResult<JobDTO> GetPendingJobs([FromRoute] int employeeId)
        {
            List<JobDTO> result = _jobLogic.GetPendingJobs(employeeId);
            if (result == null)
            {
                return NotFound("No Jobs pending.");
            }

            return Ok(result);
        }
        
        [HttpGet("{employeeId}/inProcess", Name = "GetInProcessJobs")]
        public ActionResult<JobDTO> GetInProcessJobs([FromRoute] int employeeId)
        {
            List<JobDTO> result = _jobLogic.GetInProcessJobs(employeeId);
            if (result == null)
            {
                return NotFound("No Jobs in process.");
            }

            return Ok(result);
        }
        
        [HttpGet("{employeeId}/finished", Name = "GetFinishedJobs")]
        public ActionResult<JobDTO> GetFinishedJobs([FromRoute] int employeeId)
        {
            List<JobDTO> result = _jobLogic.GetFinishedJobs(employeeId);
            if (result == null)
            {
                return NotFound("No Jobs finished.");
            }

            return Ok(result);
        }
    }
}