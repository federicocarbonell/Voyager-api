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

        [HttpPost("{productId}")]
        public ActionResult PostReport([FromRoute] int productId, [FromBody] Report report)
        {
            try
            {
                _productLogic.AddReport(productId, report);
                return Ok();
            }
            catch (IndexOutOfRangeException ind)
            {
                return BadRequest(ind.Message);
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
