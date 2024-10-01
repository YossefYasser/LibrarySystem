using EnozomTask.Models.EnozomTask.Models;

namespace EnozomTask.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Navigation property to link borrowings
        public List<Borrowing> Borrowings { get; set; }
    }
}
