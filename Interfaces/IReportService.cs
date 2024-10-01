using EnozomTask.DTO;

namespace EnozomTask.Interfaces
{
    public interface IReportService
    {
        Task<List<ReportDto>> GenerateReportAsync();

    }
}
