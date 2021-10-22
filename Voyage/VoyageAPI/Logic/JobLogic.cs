using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VoyageAPI.Adapter;
using VoyageAPI.Context;
using VoyageAPI.DTOs;
using VoyageAPI.Models;
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

        public void UpdateStateJob(int employeeId, JobDTO job)
        {
            if(employeeId < 0) throw new System.IndexOutOfRangeException("Incorrect Id.");
            IQueryable<Job> jobsResulting = _context.Jobs.AsQueryable()
                .Where(j => j.Id == employeeId);
            if(jobsResulting.Count() == 0) throw new System.IndexOutOfRangeException("There is no job with that id.");
            Job resultJob = jobsResulting.First();
            resultJob.State = (State)job.State;
            _context.SaveChanges();
        }
    }
}