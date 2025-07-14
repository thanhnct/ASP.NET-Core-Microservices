using MediatR;
using Shared.DTOs.Product;
using Shared.SeedWorks;

namespace Product.Application.Features.V1.Products.Queries.GetProductById;

public class GetProductByIdQuery : IRequest<ApiResult<ProductDto?>>
{
    public long Id { get; set; }
}