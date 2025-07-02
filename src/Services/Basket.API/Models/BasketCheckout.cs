namespace Basket.API.Models
{
    public class BasketCheckout
    {
        public decimal TotalPrice { get; set; }

        public string? UserName { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }
    }
}
