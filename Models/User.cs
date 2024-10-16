using System.ComponentModel.DataAnnotations;

namespace asp_mvc.Models
{
    public class User 
    {
        [Key]
        public int UserId {get; set;}
        public string UserName {get; set;}
        public string? FullName {get; set;}
        public string Email {get; set;}
        public string Password {get; set;}
        public string PhoneNumber {get; set;}
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<Review> Reviews {get; set;} = new List<Review>();
        public ICollection<Cart> Carts {get; set;} = new List<Cart>();
        public ICollection<ShippingDetail> ShippingDetails {get; set;} = new List<ShippingDetail>();
        public ICollection<Payment> Payments {get; set;} = new List<Payment>();
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
    }
}