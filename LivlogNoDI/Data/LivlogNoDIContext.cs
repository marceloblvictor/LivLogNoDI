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

        public DbSet<Book> Books { get; set; } = default!;
    }
}
