using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VoyageAPI.Adapter;
using VoyageAPI.Context;
using VoyageAPI.DTOs;
using VoyageAPI.Models;

namespace VoyageAPI.Logic
{
    public class ReportLogic : IReportLogic
    {
        ApplicationDbContext _context;
        public ReportLogic(ApplicationDbContext context)
        {
            _context = context;
        }

        public ReportDTO AddReport(int productId, ReportToAddDTO reportDTO)
        {
            Report report = new Report();

            report.Product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (report.Product == null) throw new IndexOutOfRangeException("Incorrect product ID.");
            report.Employee = _context.Employees.FirstOrDefault(e => e.Id == reportDTO.EmployeeId);

            if (report.Employee == null) throw new IndexOutOfRangeException("Incorrect employee ID.");
            if (reportDTO.ArrivedTime == null) report.TimeArrival = DateTime.Now.ToString();

            report.TimeResolution = DateTime.Now.ToString();
            report.VisitDate = DateTime.Now.ToString();

            if (reportDTO.Summary == null) throw new ArgumentException("The report must contain a summary.");
            if (reportDTO.Detail == null) throw new ArgumentException("The report must contain a details.");
            if (reportDTO.Comment == null) throw new ArgumentException("The report must contain a comment.");

            report.TimeArrival = reportDTO.ArrivedTime;
            report.Comment = reportDTO.Comment;
            report.Detail = reportDTO.Detail;
            report.Image = reportDTO.Image;
            report.Summary = reportDTO.Summary;

            _context.Add(report);

            if (report.Product.Reports == null) report.Product.Reports = new List<Report>();
            report.Product.Reports.Add(report);
            _context.SaveChanges();

            return new ReportDTO
            {
                Id = report.Id,
                ProductName = report.Product.Name,
                VisitDate = report.VisitDate, 
                EmployeeName = report.Employee.Name,
                Summary = report.Summary,
                Detail = report.Detail,
                Comment = report.Comment,
                Image = report.Image
            };
        }

        public ICollection<ReportDTO> GetReport(int productId)
        {
            List<ReportDTO> result = ReportAdapter.mapReport(_context.Reports.AsQueryable()
                .Where(report => report.Product.Id == productId)
                .Include(report => report.Product)
                .Include(report => report.Employee));
            if (result.Count == 0) return new List<ReportDTO>();
            return result;
        }

        public ReportDTO GetReportDetail(int reportId)
        {
            Report report = _context.Reports
                .Include(report => report.Product)
                .Include(report => report.Employee)
                .FirstOrDefault(r => r.Id == reportId);
            if (report == null) throw new IndexOutOfRangeException("Incorrect report ID.");
            List<string> convertedImagePath = new List<string>();
            return new ReportDTO
            {
                Id = report.Id,
                ProductName = report.Product.Name,
                VisitDate = report.VisitDate,
                EmployeeName = report.Employee.Name,
                Summary = report.Summary,
                Detail = report.Detail,
                Comment = report.Comment,
                Image = report.Image            
            };
        }
    }
}
