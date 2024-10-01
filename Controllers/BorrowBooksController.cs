using EnozomTask.Data;
using EnozomTask.DTO;
using EnozomTask.Models;
using EnozomTask.Models.EnozomTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnozomTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowBooksController : ControllerBase
    {
         private readonly ApplicationDBContext _context;
        private readonly CopyStatus? borrowedStatus;

        public BorrowBooksController(ApplicationDBContext context)
            {
                _context = context;
            borrowedStatus = _context.Statuses
           .FirstOrDefault(s => s.Status == "Borrowed");

            if (borrowedStatus == null)
            {
                throw new InvalidOperationException("Borrowed status not found in the database.");
            }
        }


        [HttpPost("borrow")]
        public async Task<IActionResult> BorrowBookAsync( BorrowBookDto borrowRequest)
        {
            // Validate request
            if (borrowRequest == null  || borrowRequest.ExpectedReturnDate == null || borrowRequest.StudentId <= 0 || borrowRequest.copyId <= 0 || borrowRequest.BorrowDate == null)
            {
                return BadRequest("Invalid borrow request.");
            }
         
            // Find the book copy that matches the provided book name and is available
            var availableCopy = _context.Copies
                .Include(c => c.Book) // Include Book navigation property
                .Include(c => c.CopyStatus) // Include CopyStatus navigation property
                .FirstOrDefault(c => c.Id == borrowRequest.copyId
                                     && c.CopyStatus.Status != "Borrowed" // Exclude borrowed copies
                                     && c.CopyStatus.Status != "Lost"); // Exclude lost copies

            if (availableCopy == null)
            {
                return NotFound("No available copies found for the requested book.");
            }
           

            // Create the borrowing record
            var borrowing = new Borrowing
            {
                StudentId = borrowRequest.StudentId,
                CopyId = availableCopy.Id,
                BorrowDate = borrowRequest.BorrowDate,
                ReturnDate = borrowRequest.ExpectedReturnDate,
                StatusId = borrowedStatus.Id // Assuming 1 corresponds to "Borrowed" status in the CopyStatus table
            };

            // Update the copy status to "Borrowed"

            // Update the status of the copy to "Borrowed"
            

            availableCopy.CopyStatusId = borrowedStatus.Id;

            _context.Borrowings.Add(borrowing);
            _context.SaveChanges();

            return Ok("Book borrowed successfully.");
        }


        [HttpPost("return")]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnBookDto returnBookDto)
        {
            
            // Validate input
            if (returnBookDto == null || returnBookDto.StudentId <= 0  || returnBookDto.borrwingId <= 0)
            {
                return BadRequest("Invalid input.");
            }

            // Find the borrowing record
            var borrowing = await _context.Borrowings
                .Include(b => b.Copy)
                .ThenInclude(c => c.Book)
                .FirstOrDefaultAsync(b => b.Id == returnBookDto.borrwingId );

            if (borrowing == null )
            {
                return NotFound("Borrowing record not found or book name mismatch.");
            }

            // Update return date and copy status

            if (borrowing.StatusId != borrowedStatus.Id)
            {
                return NotFound("You have already retured this book");
            }
            borrowing.ReturnDate = DateTime.Now;
            
            // Set the copy status based on the provided status
            var status = await _context.Statuses.FirstOrDefaultAsync(s => s.Status.ToLower() == returnBookDto.Status.ToLower());
            if (status == null)
            {
                return BadRequest("Invalid status provided.");
            }

            borrowing.Copy.CopyStatusId = status.Id; // Set the status based on the provided status
            borrowing.StatusId = status.Id;
            await _context.SaveChangesAsync();

            return Ok("Book returned successfully.");
        }

    }
}
