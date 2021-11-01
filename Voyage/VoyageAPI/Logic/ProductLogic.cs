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
    public class ProductLogic : IProductLogic
    {
        ApplicationDbContext _context;
        public ProductLogic(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddReport(int productId, Report report)
        {
            report.Product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (report.Product == null) throw new IndexOutOfRangeException("Incorrect product ID.");
            report.Employee = _context.Employees.FirstOrDefault(e => e.Id == report.Employee.Id);
            if (report.Employee == null) throw new IndexOutOfRangeException("Incorrect employee ID.");
            if (report.VisitDate == null) report.VisitDate = DateTime.Now;
            else
            {
                if (report.VisitDate > DateTime.Now) throw new ArgumentException("The visit date cannot be greater than today.");
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

            _context.Add(report);
            if (report.Product.Reports == null) report.Product.Reports = new List<Report>();
            report.Product.Reports.Add(report);
            _context.SaveChanges();
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
            List<ReportDTO> result = ReportAdapter.mapReport(_context.Reports.AsQueryable()
                .Where(report => report.Product.Id == productId)
                .Include(report => report.Product)
                .Include(report => report.Images)
                .Include(report => report.Employee));
            if (result.Count == 0) return new List<ReportDTO>();
            return result;
        }
    }
}
