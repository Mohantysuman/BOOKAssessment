
using Microsoft.EntityFrameworkCore;
using webApi5.Model;

namespace webApi5
{
    public class BookDemoDbContext : DbContext
    {
        public DbSet<books> BookDetails { get; set; }

        public BookDemoDbContext()
        {

        }

        public BookDemoDbContext(DbContextOptions<BookDemoDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-MIL5M1L9;Initial Catalog=Books;Integrated Security=True;");
        }

    }
}

