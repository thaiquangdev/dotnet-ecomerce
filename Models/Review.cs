using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace asp_mvc.Models
{
    public class Review 
    {
        [Key]
        public int ReviewId {get; set;}

        [ForeignKey("Product")]
        public int ProductId {get; set;}

        [ForeignKey("User")]
        public int UserId {get; set;}

        [Range(1, 5)]
        public int Rating {get; set;}

        public string Comment {get; set;} = String.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public Product Product { get; set; } 
        public User User { get; set; }

    }
}