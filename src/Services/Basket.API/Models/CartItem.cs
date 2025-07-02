namespace Basket.API.Models
{
    public class CartItem
    {
        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string? ItemNo { get; set; }

        public string? ItemName { get; set; }
    }
}
