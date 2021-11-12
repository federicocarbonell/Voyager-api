using System.Collections.Generic;
using System.Linq;
using VoyageAPI.DTOs;
using VoyageAPI.Models;

namespace VoyageAPI.Adapter
{
    public class ReportAdapter
    {
        public static List<ReportDTO> mapReport(IQueryable<Report> reports)
        {
            List<ReportDTO> convertedReports = new List<ReportDTO>();
            if (reports == null) return new List<ReportDTO>();
            foreach (Report report in reports)
            {
                ReportDTO dto = new ReportDTO
                {
                    Id = report.Id,
                    ProductName = report.Product.Name,
                    VisitDate = report.VisitDate.Day + "/" + report.VisitDate.Month + "/" + report.VisitDate.Year,
                    EmployeeName = report.Employee.Name,
                    Summary = report.Summary,
                    Detail = report.Detail,
                    Comment = report.Comment,
                    Image = report.Image
                };

                convertedReports.Add(dto);
            }

            return convertedReports;
        }
    }
}
