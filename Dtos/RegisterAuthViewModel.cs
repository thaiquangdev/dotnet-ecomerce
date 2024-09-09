using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace asp_mvc.Dtos
{
    public class RegisterAuthViewModel
    {
        [Required]
        [DisplayName("User Name")]
        public string UserName {get; set;}
        [Required]
        [EmailAddress]
        public string Email {get; set;}
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber {get; set;}
        [Required]
        [DataType(DataType.Password)]
        public string Password {get; set;}
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
    }
}