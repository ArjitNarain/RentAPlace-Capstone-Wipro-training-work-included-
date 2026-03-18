using Microsoft.EntityFrameworkCore;

namespace MyWebAPI3.Models
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {

        }
        public DbSet<Book> books { get; set; }
       
    }
}
