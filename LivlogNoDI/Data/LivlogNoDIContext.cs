using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LivlogNoDI.Models.Entities;

namespace LivlogNoDI.Data
{
    public class LivlogNoDIContext : DbContext
    {
        public LivlogNoDIContext (DbContextOptions<LivlogNoDIContext> options)
            : base(options)
        {
        }

        public DbSet<LivlogNoDI.Models.Entities.Book> Books { get; set; } = default!;
    }
}
