using System.ComponentModel.DataAnnotations;

namespace asp_mvc.Models
{
    public class Publisher
    {
        [Key]
        public int PublisherId {get; set;}
        public string PublisherName {get; set;}
        public string PublisherThumb {get; set;}
        public string PublisherImage {get; set;}
        public ICollection<Product> Products {get; set;} = new List<Product>();
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
    }
}