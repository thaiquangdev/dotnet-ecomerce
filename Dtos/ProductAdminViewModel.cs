using asp_mvc.Models;

namespace asp_mvc.Dtos
{
    public class ProductAdminViewModel 
    {
        public IEnumerable<Product> Products {get; set;} = new List<Product>();
        public int CurrentPage {get; set;}
        public int TotalPages {get; set;}
    }
}