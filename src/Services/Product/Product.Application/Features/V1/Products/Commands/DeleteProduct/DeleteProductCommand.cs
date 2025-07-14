using MediatR;
using Shared.SeedWorks;

namespace Product.Application.Features.V1.Products.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest<ApiResult<bool>>
{
    public long Id { get; set; }
}