using MediatR;
using Shared.DTOs;
using Shared.DTOs.Product;
using Shared.SeedWorks;

namespace Product.Application.Features.V1.Products.Queries.GetProducts;

public class GetProductsQuery : IRequest<ApiResult<PagedDto<ProductDto>>>
{
    public int Page { get; set; } = 1;
}