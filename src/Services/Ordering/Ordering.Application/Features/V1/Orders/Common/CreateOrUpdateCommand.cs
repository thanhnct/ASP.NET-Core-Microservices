namespace Ordering.Application.Features.V1.Orders.Common;

public class CreateOrUpdateCommand
{
    public decimal TotalPrice { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? EmailAddress { get; set; }

    public string? ShippingAddress { get; set; }

    public string? InvoiceAddress { get; set; }
}