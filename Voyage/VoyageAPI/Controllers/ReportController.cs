using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using VoyageAPI.DTOs;
using VoyageAPI.Logic;
using VoyageAPI.Models;

namespace VoyageAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly IReportLogic _reportLogic;
        private readonly IProductLogic _productLogic;

        public ReportController(IReportLogic reportLogic, IProductLogic productLogic)
        {
            _reportLogic = reportLogic;
            _productLogic = productLogic;
        }

        [HttpGet("{productId}")]
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
                    ICollection<ReportDTO> reports = _reportLogic.GetReport(productId);
                    return Ok(reports);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{productId}")]
        public ActionResult<ReportDTO> GetProductReportDetail([FromRoute] int productId, [FromRoute] int reportId)
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
                    ReportDTO report = _reportLogic.GetReportDetail(productId, reportId);
                    return Ok(report);
                }
            }
            catch (IndexOutOfRangeException ind)
            {
                return BadRequest(ind.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("{productId}")]
        public ActionResult<ReportDTO> PostReport([FromRoute] int productId, [FromBody] Report report)
        {
            try
            {
                ReportDTO reportReturn = _reportLogic.AddReport(productId, report);
                return Ok(reportReturn);
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
