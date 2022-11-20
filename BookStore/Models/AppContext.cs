using Microsoft.EntityFrameworkCore;

namespace BookStore.Models
{
    public class AppContext : DbContext
    {
        public DbSet<Customer> Customer { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=book_store;Username=postgres;Password=123");
        }
    }
}
