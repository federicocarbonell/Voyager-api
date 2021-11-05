using System.Collections.Generic;
using System.Linq;
using VoyageAPI.DTOs;
using VoyageAPI.Models;
using State = VoyageAPI.DTOs.State;

namespace VoyageAPI.Adapter
{
    public class JobAdapter
    {
        public static List<JobDTO> mapJobs(IQueryable<Job> jobs)
        {
            List<JobDTO> convertedJobs = new List<JobDTO>();
            foreach (Job job in jobs)
            {
                ProductDTO product = new ProductDTO
                {
                    Id = job.Product.Id,
                    Name = job.Product.Name,
                    Description = job.Product.Description,
                    Year = job.Product.Year
                };

                JobDTO dto = new JobDTO
                {
                    Id = job.Id,
                    Product = product,
                    State = (State) job.State,
                    Description = job.Description,
                    Time = job.Time,
                    Direction = job.Direction
                };
                convertedJobs.Add(dto);
            }

            return convertedJobs;
        }
    }
}