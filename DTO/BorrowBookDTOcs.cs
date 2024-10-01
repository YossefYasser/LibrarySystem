namespace EnozomTask.DTO
{
  
        public class BorrowBookDto
        {
            public int StudentId { get; set; }
            public int  copyId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        }
    

}
