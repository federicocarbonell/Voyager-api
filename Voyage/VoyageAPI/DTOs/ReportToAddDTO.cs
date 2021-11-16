using System;
using System.Collections.Generic;

namespace VoyageAPI.DTOs
{
    public class ReportToAddDTO
    {
        public int ProductId { get; set; }
        public int EmployeeId { get; set; }
        public string ArrivedTime { get; set; }
        public string Summary { get; set; }
        public string Detail { get; set; }
        public string Comment { get; set; }
        public string Image { get; set; }
    }
}
