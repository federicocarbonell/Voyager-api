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
            List<string> convertedImagePath = new List<string>();
            if (reports == null) return new List<ReportDTO>();
            foreach (Report report in reports)
            {
                foreach(Image image in report.Images)
                {
                    convertedImagePath.Add(image.Path);
                }

                ReportDTO dto = new ReportDTO
                {
                    Id = report.Id,
                    ProductName = report.Product.Name,
                    VisitDate = report.VisitDate,
                    TimeArrival = report.TimeArrival,
                    TimeResolution = report.TimeResolution,
                    EmployeeName = report.Employee.Name,
                    Summary = report.Summary,
                    Detail = report.Detail,
                    Comment = report.Comment,
                    Images = convertedImagePath
                };

                convertedReports.Add(dto);
            }

            return convertedReports;
        }
    }
}
