using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VoyageAPI.DTOs;
using VoyageAPI.Logic;
using VoyageAPI.Models;

namespace VoyageAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductLogic _productLogic;

        public ProductController(IProductLogic productLogic)
        {
            _productLogic = productLogic;
        }

        [HttpGet("{productId}", Name = "GetProductInfo")]
        public ActionResult<ProductDTO> GetProductInfo([FromRoute] int productId)
        {

            ProductDTO product = _productLogic.GetProductInfo(productId);
            if (product == null)
            {
                return NotFound("No Product found.");
            }

            return Ok(product);
        }
    }
}
