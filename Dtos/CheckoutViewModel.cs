namespace asp_mvc.Dtos
{
    public class CheckoutViewModel
    {
         public string Phone { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Address { get; set; } 

        public List<CartItemViewModel> CartItems { get; set; }

        // Thông tin thanh toán
        public double Amount { get; set; }
        public int CartId { get; set; }
    }
}