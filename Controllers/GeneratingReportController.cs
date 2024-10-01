using EnozomTask.Data;
using EnozomTask.DTO;
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
       

        public GeneratingReportController(ApplicationDBContext context)
        {
            _context = context;
        
        }

        [HttpGet("report")]
        public async Task<IActionResult> GetBookCopiesReport()
        {
            var report = await _context.Copies
                .Include(c => c.Book)
                .Include(c => c.CopyStatus)
                .Select(c => new BookCopyReportDto
                {
                    BookName = c.Book.Name,
                    CopyId = c.Id,
                    Status = c.CopyStatus.Status
                })
                .ToListAsync();

            return Ok(report);
        }
    }
}
