using Microsoft.EntityFrameworkCore;

namespace RTL.TVMaze.Scraper.Application.Dependencies.SqlDataStorage
{
    public class TVMazeContext : DbContext
    {
        public TVMazeContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Show> Shows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Show>()
                .Property(b => b.Model)
                .IsRequired();
        }
    }
}
