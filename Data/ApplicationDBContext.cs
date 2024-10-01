using EnozomTask.Models;
using EnozomTask.Models.EnozomTask.Models;
using Microsoft.EntityFrameworkCore;

namespace EnozomTask.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options) { }


        public DbSet<Student> Students { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Copy> Copies { get; set; }
        public DbSet<CopyStatus> Statuses { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CopyStatus>().HasData(
                new CopyStatus { Id = 1, Status = "Good" },
                new CopyStatus { Id = 2, Status = "Damaged" },
                new CopyStatus { Id = 3, Status = "Lost" },
                new CopyStatus { Id = 4, Status = "Borrowed" }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student { StudentId = 1, Name = "Ali", Email = "Ali@enozom.com", PhoneNumber = "0122224400" },
                new Student { StudentId = 2, Name = "Mohamed", Email = "mohamed@enozom.com", PhoneNumber = "0111155000" },
                new Student { StudentId = 3, Name = "Ahmed", Email = "Ahmed@enozom.com", PhoneNumber = "0155553311" }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Name = "Clean Code" },
                new Book { Id = 2, Name = "Algorithms" }
            );

            modelBuilder.Entity<Copy>().HasData(
                new Copy { Id = 1, BookId = 1, CopyStatusId = 1 }, // Clean Code - Good
                new Copy { Id = 2, BookId = 1, CopyStatusId = 1 }, // Clean Code - Good
                new Copy { Id = 3, BookId = 2, CopyStatusId = 1 }  // Algorithms - Good
            );
        }
    }
}
