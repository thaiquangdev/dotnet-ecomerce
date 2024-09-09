using System.ComponentModel.DataAnnotations;

namespace asp_mvc.Dtos
{
    public class EditProductViewModel
    {
        
        [Required]
        public string Title {get; set;}
        [DataType(DataType.Text)]
        [Required]
        public string Description {get; set;}
        
        [Required]
        public double Discount {get; set;}
        [Required]
        public double PriceNew {get; set;}
        [Required]
        [Range(1, 1000)]
        public int Stock {get; set;}
        [Required]
        public string Author {get; set;}
        [Display(Name = "Sub Category")]
        public int SubCategoryId {get; set;}
        public IEnumerable<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
        public DateTime UpdatedAt {get; set;}
    }
}