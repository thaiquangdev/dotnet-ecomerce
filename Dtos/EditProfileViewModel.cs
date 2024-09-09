using System.ComponentModel.DataAnnotations;

namespace asp_mvc.Dtos
{
    public class EditProfileViewModel
    {
        [Required]
        public string UserName {get; set;}

        [Required]
        public string PhoneNumber {get; set;}
        
        [Required]
        public string FullName {get; set;}
    }
}