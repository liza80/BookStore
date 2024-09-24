using Microsoft.EntityFrameworkCore;
using WebApplication15.Models;


namespace WebApplication15.Models
{
    public class BooksStoreContext : DbContext
    {
        public BooksStoreContext(DbContextOptions<BooksStoreContext> options)
       : base(options)
        {
        }

        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Book>(entity=>entity.Property(b=>b.Id).ValueGeneratedNever());
            modelBuilder.Entity<ShoppingCart>(entity => entity.Property(b => b.Id).ValueGeneratedNever());
        }

    }
}
