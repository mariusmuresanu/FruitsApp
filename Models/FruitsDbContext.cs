using Microsoft.EntityFrameworkCore;


namespace FruitsApp.Models
{
    public class FruitsDbContext : DbContext
    {
        public FruitsDbContext(DbContextOptions<FruitsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Fruit> Fruits { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
