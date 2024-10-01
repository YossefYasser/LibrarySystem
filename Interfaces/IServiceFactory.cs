namespace EnozomTask.Interfaces
{
    public interface IServiceFactory
    {
        IBorrowingService CreateBorrowingService();
        IReportService CreateReportService();
    }
}
