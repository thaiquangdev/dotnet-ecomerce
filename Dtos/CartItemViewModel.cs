namespace asp_mvc.Dtos
{
     public class CartItemViewModel
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Thumb { get; set; }
        public int Discount { get; set; }
    }
}