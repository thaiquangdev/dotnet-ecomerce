using asp_mvc.Models;

namespace asp_mvc.Dtos
{
    public class HomeViewModel
    {
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public IEnumerable<Publisher> Publishers { get; set; } = new List<Publisher>();
        public IEnumerable<Product> Products {get; set;} = new List<Product>();
        public IEnumerable<Product> ProductSolds {get; set;} = new List<Product>();
    }
}