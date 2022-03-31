using Microsoft.EntityFrameworkCore;

namespace DAL.Model
{
    public class PerpustakaanDBContext : DbContext
    {
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<Book> Books { get; set; }

        public PerpustakaanDBContext(DbContextOptions<PerpustakaanDBContext> options) : base(options) { }
    }
}
