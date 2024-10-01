using EnozomTask.Data;
using EnozomTask.DTO;
using EnozomTask.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ReportRepository : IReportRepository
{
    private readonly ApplicationDBContext _context;

    public ReportRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<List<ReportDto>> GetReportDataAsync()
    {
        var reportData = await _context.Copies
                .Include(c => c.Book) // Include related Book
                .Include(c => c.CopyStatus) // Include related CopyStatus
                .GroupJoin(
                    _context.Borrowings,
                    copy => copy.Id,
                    borrowing => borrowing.CopyId,
                    (copy, borrowings) => new
                    {
                        Copy = copy,
                        LastBorrowing = borrowings.OrderByDescending(b => b.BorrowDate).FirstOrDefault()
                    }
                )
                .Select(x => new ReportDto
                {
                    BookName = x.Copy.Book.Name, // Book name from the Copy
                    CopyId = x.Copy.Id, // Copy ID
                    Status = x.Copy.CopyStatus.Status, // Copy status
                    BorrowId = (x.LastBorrowing == null || x.Copy.CopyStatus.Id != 4)
                        ? (int?)null // Set BorrowId to null if no borrowing found or status ID is not 4 ( not borrowed)
                        : x.LastBorrowing.Id
                })
                .ToListAsync();
        return reportData;
    }
}
