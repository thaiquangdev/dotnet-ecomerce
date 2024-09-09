using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using asp_mvc.Models;

namespace asp_mvc
{
    public class SubCategory
    {
        [Key]
        public int SubCategoryId {get; set;}
        [Required]
        [DisplayName("Sub Category Name")]
        public string SubCategoryName {get; set;}
        [ForeignKey("Category")]
        public int CategoryId {get; set;}
        public Category Category {get; set;}

        public ICollection<Product> Products {get; set;} = new List<Product>();

        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
    }
}