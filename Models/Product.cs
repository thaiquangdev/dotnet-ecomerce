using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace asp_mvc.Models
{
    public class Product
    {
        [Key]
        public int ProductId {get; set;}
        [StringLength(255)]
        public string Title {get; set;}
        [DataType(DataType.Text)]
        public string Description {get; set;}
        [DataType(DataType.Text)]
        public string ImageUrl {get; set;}
        [DataType(DataType.Text)]
        public string Thumb {get; set;}
        public double PriceNew {get; set;}
        public double Discount {get; set;}
        public int Stock {get; set;}
        public int Sold {get; set;}
        public string Author {get; set;}
        [ForeignKey("SubCategory")]
        public int SubCategoryId {get; set;}
        public SubCategory SubCategory {get; set;}
        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }

        public ICollection<Review> Reviews {get; set;} = new List<Review>();
        public ICollection<CartItem> CartItems {get; set;} = new List<CartItem>();

        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
    }
}