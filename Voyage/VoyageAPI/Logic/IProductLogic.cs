using System;
using System.Collections.Generic;
using VoyageAPI.DTOs;
using VoyageAPI.Models;

namespace VoyageAPI.Logic
{
    public interface IProductLogic
    {
        public ProductDTO GetProductInfo(int productId);
    }
}
