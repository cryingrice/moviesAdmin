using System.ComponentModel.DataAnnotations;

namespace moviesAdmin.Models
{
    public class CriticReview
    {

        public int Id { get; set; }

        public required string Description { get; set; }

        [Range(1, 5, ErrorMessage = "Ratings must be a number between 1 and 5.")]
        public int Ratings { get; set; }

        public bool IsPublished { get; set; }

        [Display(Name = "Created By")]
        public required string CreatedBy { get; set; }

        [DataType(DataType.Date)]
        public DateOnly CreatedDate { get; set; }
    }
}
