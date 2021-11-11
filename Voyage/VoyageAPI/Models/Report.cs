using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoyageAPI.Models
{
    [Table("Report")]
    public class Report
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public string VisitDate { get; set; }
        public string TimeArrival { get; set; }
        public string TimeResolution { get; set; }
        public Employee Employee { get; set; }
        public string Summary { get; set; }
        public string Detail { get; set; }
        public string Comment { get; set; }
        public List<Image> Images { get; set; }

    }
}
