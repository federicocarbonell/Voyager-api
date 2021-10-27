using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VoyageAPI.DTOs;
using VoyageAPI.Logic;

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

        [HttpGet("{productId}/reports", Name = "GetProductInfo")]
        public ActionResult<ReportDTO> GetProductReport([FromRoute] int productId)
        {
            try
            {
                ProductDTO product = _productLogic.GetProductInfo(productId);
                if (product == null)
                {
                    return NotFound("No Product found.");
                }
                else
                {
                    ICollection<ReportDTO> reports = _productLogic.GetReport(productId);
                    return Ok(reports);
                }
            }catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }

            
        }
    }
}
