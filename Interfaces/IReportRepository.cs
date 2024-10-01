using EnozomTask.DTO;

namespace EnozomTask.Interfaces
{
    public interface IReportRepository
    {
        Task<List<ReportDto>> GetReportDataAsync();

    }
}
