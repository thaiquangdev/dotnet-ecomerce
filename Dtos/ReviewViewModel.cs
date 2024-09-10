// ReviewViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace asp_mvc.Dtos
{
    public class ReviewViewModel
    {
        public int ProductId { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters.")]
        public string Comment { get; set; }
    }
}