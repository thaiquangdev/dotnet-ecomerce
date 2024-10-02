using System.ComponentModel.DataAnnotations.Schema;

namespace asp_mvc.Models
{
    public class Cart 
    {
        public int CartId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public double TotalPrice { get; set; }
        public string Status {get; set;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public User User { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<ShippingDetail> ShippingDetails { get; set; } = new List<ShippingDetail>();
        public Payment Payment {get; set;}
    }
}