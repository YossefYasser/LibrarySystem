using EnozomTask.Models.EnozomTask.Models;
using EnozomTask.Models;
using EnozomTask.Interfaces;
using EnozomTask.Data;
using Microsoft.EntityFrameworkCore;
using EnozomTask.DTO;

namespace EnozomTask.Repositories
{
    public class BorrowingRepository : IBorrowingRepository
    {
        private readonly ApplicationDBContext _context;

        public BorrowingRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task AddBorrowingAsync(Borrowing borrowing)
        {
            await _context.Borrowings.AddAsync(borrowing);
            await _context.SaveChangesAsync();
        }

        public async Task<Copy> GetAvailableCopyAsync(int copyId)
        {
            return await _context.Copies
                       .Include(c => c.Book)
                       .Include(c => c.CopyStatus)
                       .FirstOrDefaultAsync(c => c.Id == copyId
                                                 && c.CopyStatus.Status != "Borrowed"
                                                 && c.CopyStatus.Status != "Lost");
        }

        public async Task<Borrowing> GetBorrowingByIdAsync(int borrowingId)
        {
            return await _context.Borrowings
            .Include(b => b.Copy)
            .ThenInclude(c => c.Book)
            .FirstOrDefaultAsync(b => b.Id == borrowingId);
        }

      

        public  async Task UpdateBorrowingAndCopy(Borrowing borrowing, int statusId)
        {
            borrowing.Copy.CopyStatusId = statusId; // Set the status based on the provided status
            borrowing.StatusId = statusId;
            await _context.SaveChangesAsync();
        }

       
    }
}
