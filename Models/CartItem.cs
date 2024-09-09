using System.ComponentModel.DataAnnotations.Schema;

namespace asp_mvc.Models
{
    public class CartItem 
    {
        public int CartItemId { get; set; }
        [ForeignKey("Cart")]
        public int CartId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }  

        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}