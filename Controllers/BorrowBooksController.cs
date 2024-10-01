using EnozomTask.Data;
using EnozomTask.DTO;
using EnozomTask.Interfaces;
using EnozomTask.Models;
using EnozomTask.Models.EnozomTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static EnozomTask.Services.BorrowingService;

namespace EnozomTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowBooksController : ControllerBase
    {
        private readonly IBorrowingService _borrowingService;

        public BorrowBooksController(IBorrowingService borrowingService)
            {
            _borrowingService = borrowingService;
                

        }


        [HttpPost("borrow")]
        public async Task<IActionResult> BorrowBookAsync( BorrowBookDto borrowRequest)
        {
            try {
               await _borrowingService.BorrowBookAsync(borrowRequest);
                

                return Ok("Book borrowed successfully.");
            }
            catch (ArgumentException argEx)
            {
                return BadRequest(argEx.Message); // 400 Bad Request
            }
            catch (BorrowBookServiceException serviceEx)
            {
                return NotFound(serviceEx.Message); // 404 Not Found 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred while borrowing the book."); // 500 Internal Server Error
            }


        }


        [HttpPost("return")]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnBookDto returnBookDto)
        {

            // Validate input
            try {


                await _borrowingService.ReturnBookAsync(returnBookDto);
                return Ok("Book returned successfully.");

            }
            catch (ArgumentException argEx)
            {
                return BadRequest(argEx.Message); // 400 bad request errors
            }
            catch (BookServiceException serviceEx)
            {
                return NotFound(serviceEx.Message); // 404 Not Found 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred."); 
            }

        }

    }
}
