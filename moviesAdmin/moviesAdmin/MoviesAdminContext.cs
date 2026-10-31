using Microsoft.EntityFrameworkCore;
using moviesAdmin.Models;

namespace MoviesAdmin
{
    public class MoviesAdminContext : DbContext
    {
        public MoviesAdminContext(DbContextOptions<MoviesAdminContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; } = default!;
        public DbSet<Genre> Genres { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial genre data
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Title = "Action" },
                new Genre { Id = 2, Title = "Comedy" },
                new Genre { Id = 3, Title = "Drama" },
                new Genre { Id = 4, Title = "Horror" },
                new Genre { Id = 5, Title = "Romance" },
                new Genre { Id = 6, Title = "Sci-Fi" },
                new Genre { Id = 7, Title = "Thriller" },
                new Genre { Id = 8, Title = "Animation" }
            );
        }
    }
}