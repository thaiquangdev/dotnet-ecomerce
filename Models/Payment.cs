using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace asp_mvc.Models
{
    public class Payment 
    {
        [Key]
        public int PaymentId {get; set;}
        [ForeignKey("Cart")]
        public int CartId {get; set;}
        [ForeignKey("User")]
        public int UserId {get; set;}
        public double Amount {get; set;}

        public Cart Cart {get; set;}
        public User User {get; set;}

        public string PaymentMethod { get; set; } 
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; }
    }
}