using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using asp_mvc.Models;

namespace asp_mvc.Dtos
{
    public class CreateProductViewModel
    {
        
        [Required]
        public string Title {get; set;}
        [DataType(DataType.Text)]
        [Required]
        public string Description {get; set;}
        [DataType(DataType.Text)]
        [Required]
        [DisplayName("Image")]
        public IFormFile ImageUrl {get; set;}
        [DataType(DataType.Text)]
        [Required]
        public IFormFile Thumb {get; set;}
        [Required]
        public double Discount {get; set;}
        [Required]
        public double PriceNew {get; set;}
        [Required]
        [Range(1, 1000)]
        public int Stock {get; set;}
        public int Sold {get; set;}
        [Required]
        public string Author {get; set;}
        [Display(Name = "Sub Category")]
        public int SubCategoryId {get; set;}
        public IEnumerable<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
        [Display(Name = "Publisher")]
        public int PublisherId {get; set;}
        public IEnumerable<Publisher> Publishers { get; set; } = new List<Publisher>();
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
    }
}