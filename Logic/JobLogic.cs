using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VoyageAPI.Adapter;
using VoyageAPI.Context;
using VoyageAPI.DTOs;
using State = VoyageAPI.Models.State;

namespace VoyageAPI.Logic
{
    public class JobLogic : IJobLogic
    {
        ApplicationDbContext _context;
        public JobLogic(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<JobDTO> GetPendingJobs(int employeeId)
        {
            List<JobDTO> result = JobAdapter.mapJobs(_context.Jobs.AsQueryable()
                .Where(job => (job.Employee.Id == employeeId && job.State == State.Pending))
                .Include(job => job.Product));;
            return result;
        }

        public List<JobDTO> GetInProcessJobs(int employeeId)
        {
            List<JobDTO> result = JobAdapter.mapJobs(_context.Jobs.AsQueryable()
                .Where(job => (job.Employee.Id == employeeId && job.State == State.InProcess))
                .Include(job => job.Product));
            return result;
        }

        public List<JobDTO> GetFinishedJobs(int employeeId)
        {
            List<JobDTO> result = JobAdapter.mapJobs(_context.Jobs.AsQueryable()
                .Where(job => (job.Employee.Id == employeeId && job.State == State.Finished))
                .Include(job => job.Product));
            return result;
        }
    }
}