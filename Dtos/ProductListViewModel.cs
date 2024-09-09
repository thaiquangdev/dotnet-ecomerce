using asp_mvc.Models;

namespace asp_mvc.Dtos
{
    public class ProductListViewModel 
    {
        public IEnumerable<Product> Products {get; set;} = new List<Product>();
        public IEnumerable<SubCategory> SubCategories {get; set;} = new List<SubCategory>();
    }
}