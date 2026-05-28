using Microsoft.EntityFrameworkCore;


namespace Task4U.Infrastructure
{
    public class TskDbContext : DbContext
    {
       public TskDbContext(DbContextOptions<TskDbContext> options) : base(options) {}
        public DbSet<Task4U.Models.Usuario> Usuario { get; set; } = default!;

    }
}
