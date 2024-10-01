namespace EnozomTask.Models
{
    namespace EnozomTask.Models
    {
        public class Borrowing
        {
            public int Id { get; set; }
            public int StudentId { get; set; }
            public int CopyId { get; set; }
            public DateTime BorrowDate { get; set; } = DateTime.Now;
            public DateTime? ReturnDate { get; set; }
            public int StatusId { get; set; } // Foreign key to BorrowingStatus

            // Navigation properties
            public virtual Student Student { get; set; }
            public virtual Copy Copy { get; set; }
            public virtual CopyStatus Status { get; set; } // Link to the Status table
        }
    }

}
