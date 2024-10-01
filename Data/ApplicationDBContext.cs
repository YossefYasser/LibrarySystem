using Microsoft.EntityFrameworkCore;

namespace EnozomTask.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options) { }
    }
}
