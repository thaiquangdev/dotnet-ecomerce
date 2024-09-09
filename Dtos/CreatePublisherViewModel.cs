using System.ComponentModel.DataAnnotations;

namespace asp_mvc.Dtos
{
    public class CreatePublisherViewModel
    {
        [Key]
        public int PublisherId {get; set;}
        public string PublisherName {get; set;}
        public IFormFile PublisherThumb {get; set;}
        public IFormFile PublisherImage {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
    }
}