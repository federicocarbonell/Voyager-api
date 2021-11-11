﻿using System;
using System.Collections.Generic;

namespace VoyageAPI.DTOs
{
    public class ReportDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string VisitDate { get; set; }
        public string TimeArrival { get; set; }
        public string TimeResolution { get; set; }
        public string EmployeeName { get; set; }
        public string Summary { get; set; }
        public string Detail { get; set; }
        public string Comment { get; set; }
        public ICollection<string> Images { get; set; }
    }
}
