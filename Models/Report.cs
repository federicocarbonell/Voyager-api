using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VoyageAPI.Models
{
    [Table("Report")]
    public class Report
    {
        public int Id { get; set; }
        public string ArriveTime { get; set; }
        public string LeaveTime { get; set; }
        public Employee Employee { get; set; }
        public string Details { get; set; }
        public string Summary { get; set; }
        public string Comments { get; set; }
        public string Image { get; set; }

    }
}
