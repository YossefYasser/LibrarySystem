namespace EnozomTask.DTO
{
    public class ReportDto

    {
        public int? BorrowId { get; set; }   // Add this property

        public string BookName { get; set; }
        public int CopyId { get; set; }
        public string Status { get; set; }
    }

}
