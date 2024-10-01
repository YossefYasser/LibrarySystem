using EnozomTask.Data;
using EnozomTask.DTO;
using EnozomTask.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EnozomTask.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService( IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
            
        }
        public async Task<List<ReportDto>> GenerateReportAsync()
        {
            var report = await _reportRepository.GetReportDataAsync();

            return report;
        }


    }
}
