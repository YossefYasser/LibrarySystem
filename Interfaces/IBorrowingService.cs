using EnozomTask.DTO;
using EnozomTask.Models.EnozomTask.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnozomTask.Interfaces
{
    public interface IBorrowingService
    {
        Task BorrowBookAsync(BorrowBookDto borrowRequest);
        Task ReturnBookAsync(ReturnBookDto returnBookDto);
    }

}
