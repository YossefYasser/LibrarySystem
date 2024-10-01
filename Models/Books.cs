namespace EnozomTask.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property for copies of the book
        public List<Copy> Copies { get; set; }
    }

}
