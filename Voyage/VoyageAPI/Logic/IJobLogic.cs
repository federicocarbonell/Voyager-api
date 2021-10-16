using System.Collections.Generic;
using VoyageAPI.DTOs;

namespace VoyageAPI.Logic
{
    public interface IJobLogic
    {
        public List<JobDTO> GetPendingJobs(int employeeId);
        public List<JobDTO> GetInProcessJobs(int employeeId);
        public List<JobDTO> GetFinishedJobs(int employeeId);
        public void UpdateStateJob(int employeeId, JobDTO job);
    }
}