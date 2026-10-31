using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace moviesAdmin.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(179, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 179 characters.")]
        public required string Title { get; set; }

        // Foreign Key for Genre
        [ForeignKey("Genre")]
        public int? GenreId { get; set; }

        // Navigation property: Many Movies belong to One Genre
        public Genre? Genre { get; set; }

        // Foreign Key for AudienceRating
        [ForeignKey("AudienceRating")]
        public int? AudienceRatingId { get; set; }

        // Navigation property: Many Movies belong to One AudienceRating
        public AudienceRating? AudienceRating { get; set; }

        [StringLength(500, ErrorMessage = "Synopsis cannot exceed 500 characters.")]
        public string? Synopsis { get; set; }

        [Range(1, 51420, ErrorMessage = "Runtime must be a number between 1 and 51420.")]
        public int Runtime { get; set; }

        [Display(Name = "Date Released")]
        public DateOnly ReleaseDate { get; set; }

        public String? PosterUrl { get; set; }
    }
}