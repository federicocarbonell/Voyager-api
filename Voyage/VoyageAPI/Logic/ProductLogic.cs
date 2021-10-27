using System;
using System.Collections.Generic;
using System.Linq;
using VoyageAPI.Adapter;
using VoyageAPI.Context;
using VoyageAPI.DTOs;
using VoyageAPI.Models;

namespace VoyageAPI.Logic
{
    public class ProductLogic : IProductLogic
    {
        ApplicationDbContext _context;
        public ProductLogic(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddReport(int productId, Report report)
        {
            Product product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null) throw new IndexOutOfRangeException("Incorrect product ID.");
            Employee employee = _context.Employees.FirstOrDefault(e => e.Id == report.Employee.Id);
            if (employee == null) throw new IndexOutOfRangeException("Incorrect employee ID.");
            if (report.VisitDate == null) report.VisitDate = DateTime.Now;
            else
            {
                if (report.VisitDate < DateTime.Now) throw new ArgumentException("The visit date cannot be greater than today.");
            }
            if (report.TimeArrival == null) report.TimeArrival = DateTime.Now;
            if (report.TimeResolution == null) report.TimeResolution = DateTime.Now;
            if(report.Summary == null) throw new ArgumentException("The report must contain a summary.");
            if (report.Detail == null) throw new ArgumentException("The report must contain a details.");
            if (report.Comment == null) throw new ArgumentException("The report must contain a comment.");
            if(report.Images != null)
            {
                if(report.Images.Count == 0) throw new ArgumentException("The report must contain at least one image.");
                foreach (Image image in report.Images)
                {
                    if (image.Path == null) report.Images.Remove(image);
                }
            }
            else throw new ArgumentException("The report must contain at least one image.");
            _context.Reports.Add(report);
        }

        public ProductDTO GetProductInfo(int productId)
        {
            Product product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return null;
            }
            ProductDTO productDTO = new ProductDTO
            {
                Id = product.Id,
                Description = product.Description,
                Name = product.Name,
                Year = product.Year
            };
            return productDTO;
        }

        public ICollection<ReportDTO> GetReport(int productId)
        {
            List<ReportDTO> result = ReportAdapter.mapReport(_context.Products.AsQueryable()
                .Where(product => product.Id == productId)
                .FirstOrDefault().Reports);
            return result;
        }
    }
}
