using LivlogNoDI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LivlogNoDI.Data
{
    public class LivlogNoDIContext : DbContext
    {

        public LivlogNoDIContext (DbContextOptions<LivlogNoDIContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Constants.Configuration.CONNECTION_STRING);
            
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Book> Books { get; set; } = default!;
        public DbSet<Customer> Customers { get; set; } = default!;
        public DbSet<CustomerBook> CustomerBooks { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerBook>()
                .HasKey(cb => cb.Id);

            modelBuilder.Entity<CustomerBook>()
                .HasOne(cb => cb.Customer)
                    .WithMany(c => c.CustomerBooks)
                .HasForeignKey(cb => cb.CustomerId);

            modelBuilder.Entity<CustomerBook>()
                .HasOne(cb => cb.Book)
                    .WithMany(b => b.CustomerBooks)
                .HasForeignKey(cb => cb.BookId);
        }
    }
}
