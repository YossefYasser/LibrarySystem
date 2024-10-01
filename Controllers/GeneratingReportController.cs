using EnozomTask.Data;
using EnozomTask.DTO;
using EnozomTask.Interfaces;
using EnozomTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnozomTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneratingReportController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IReportService _reportService;

        public GeneratingReportController( IReportService reportService)
        {
            _reportService = reportService;
        
        }

        [HttpGet("report")]
        public async Task<IActionResult> GetBookCopiesReport()
        {
            try
            {
                var report = await _reportService.GenerateReportAsync();
                return Ok(report);
            }
            catch (Exception ex) {return BadRequest(ex.Message); }

            
        }
    }
}
