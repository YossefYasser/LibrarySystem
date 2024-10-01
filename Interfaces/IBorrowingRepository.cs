using EnozomTask.Models;
using EnozomTask.Models.EnozomTask.Models;

namespace EnozomTask.Interfaces
{
    public interface IBorrowingRepository
    {
        Task<Copy> GetAvailableCopyAsync( int copyId);
        Task AddBorrowingAsync(Borrowing borrowing);

        Task<Borrowing> GetBorrowingByIdAsync(int borrowingId); 
        Task UpdateBorrowingAndCopy(Borrowing borrowing,int statusId);
    }
}
