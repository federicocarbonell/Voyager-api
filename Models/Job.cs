using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VoyageAPI.Models
{
    [Table("Job")]
    public class Job
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public Employee Employee { get; set; }
        public int State { get; set; }
        public string Time { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
