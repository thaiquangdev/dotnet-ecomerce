using System.ComponentModel.DataAnnotations;

namespace asp_mvc.Dtos
{
    public class LoginAuthViewModel
    {
        [Required]
        public string Email {get; set;}
        [Required]
        public string Password {get; set;}
    }
}