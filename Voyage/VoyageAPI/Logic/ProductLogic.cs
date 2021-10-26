using System;
using System.Linq;
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
    }
}
