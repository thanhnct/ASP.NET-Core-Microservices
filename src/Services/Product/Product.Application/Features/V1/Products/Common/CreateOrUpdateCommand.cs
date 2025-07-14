namespace Product.Application.Features.V1.Products.Common;

public class CreateOrUpdateCommand
{
    public string? No { get; set; }
    public string? Name { get; set; }
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
}