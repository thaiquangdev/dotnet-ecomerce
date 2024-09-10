using asp_mvc.Models;

namespace asp_mvc.Dtos 
{
    public class ProductDetailViewModel 
    {
        public Product Product { get; set; }
        public List<Review> Reviews { get; set; }
    }
}