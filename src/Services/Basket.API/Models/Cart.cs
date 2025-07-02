namespace Basket.API.Models
{
    public class Cart
    {
        public required string UserName { get; set; }

        public List<CartItem> Items { get; set; } = new();

        public Cart()
        {
        }

        public Cart(string userName)
        {
            UserName = userName;
        }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (var item in Items)
                {
                    totalPrice += item.Price * item.Quantity;
                }
                return totalPrice;
            }
        }
    }
}
