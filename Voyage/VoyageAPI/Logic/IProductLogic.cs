using System;
using System.Collections.Generic;
using VoyageAPI.DTOs;

namespace VoyageAPI.Logic
{
    public interface IProductLogic
    {
        public ProductDTO GetProductInfo(int productId);
        ICollection<ReportDTO> GetReport(int productId);
    }
}
