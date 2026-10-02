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
    }
}