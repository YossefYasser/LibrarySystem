namespace EnozomTask.Models
{

 public class Copy
{
    public int Id { get; set; }
    public int BookId { get; set; } // Foreign key to the Book
    public int CopyStatusId { get; set; } // Foreign key to CopyStatus

    // Navigation properties
    public virtual Book Book { get; set; }
    public virtual CopyStatus CopyStatus { get; set; }
}


}
