using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoyageAPI.DTOs;
using VoyageAPI.Models;

namespace VoyageAPI.Logic
{
    public interface IReportLogic
    {
        ICollection<ReportDTO> GetReport(int productId);
        ReportDTO AddReport(int productId, Report report);
        ReportDTO GetReportDetail(int reportId);
    }
}
