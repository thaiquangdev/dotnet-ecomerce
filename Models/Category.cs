using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace asp_mvc.Models
{
    public class Category
    {
        [Key]
        public int CategoryId {get; set;}
        [Required(ErrorMessage = "Category Name is required")]
        [StringLength(100)]
        [DisplayName("Category Name")]
        public string CategoryName {get; set;}
        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();

        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
    }
}