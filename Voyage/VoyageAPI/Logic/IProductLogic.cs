using System;
using VoyageAPI.DTOs;

namespace VoyageAPI.Logic
{
    public interface IProductLogic
    {
        public ProductDTO GetProductInfo(int productId);
    }
}
