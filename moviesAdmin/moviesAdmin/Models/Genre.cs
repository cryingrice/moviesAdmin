namespace moviesAdmin.Models
{
    public class Genre
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        // Navigation property: One Genre can have many Movies
        public ICollection<Movie> Movies { get; set; } = [];
    }
}