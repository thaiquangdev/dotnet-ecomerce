using System.ComponentModel.DataAnnotations;
using asp_mvc.Models;

namespace asp_mvc.Dtos
{
    public class SubCategoryViewModel 
    {
        [Required]
        [Display(Name = "Sub Category Name")]
        public string SubCategoryName { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
    }
}