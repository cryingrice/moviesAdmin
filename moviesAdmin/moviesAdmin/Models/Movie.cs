namespace moviesAdmin.Models
{
    //The movie data includes title, synopsis, genre, rating (e.g., PG-13), runtime hours/minutes, and release date.
    public class Movie
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Genre { get; set; }
        public required string Rating { get; set; }
        public string? Synopsis { get; set; }
        public int Runtime { get; set; }
        public DateOnly ReleaseDate { get; set; }
    }
}
