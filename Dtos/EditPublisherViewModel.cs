using System.ComponentModel.DataAnnotations;

namespace asp_mvc.Dtos
{
    public class EditPublisherViewModel
{
    public int PublisherId { get; set; }

    [Required]
    public string PublisherName { get; set; }

    public IFormFile PublisherThumb { get; set; }
    public string ExistingPublisherThumb { get; set; }

    public IFormFile PublisherImage { get; set; }
    public string ExistingPublisherImage { get; set; }
}
}