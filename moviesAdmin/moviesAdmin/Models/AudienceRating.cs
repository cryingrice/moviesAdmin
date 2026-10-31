namespace moviesAdmin.Models
{
    public class AudienceRating
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        // Navigation property: One AudienceRating can have many Movies
        public ICollection<Movie> Movies { get; set; } = [];
    }
}