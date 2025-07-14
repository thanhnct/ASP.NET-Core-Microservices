using MediatR;
using Product.Application.Features.V1.Products.Common;
using Shared.SeedWorks;

namespace Product.Application.Features.V1.Products.Commands.UpdateProduct;

public class UpdateProductCommand : CreateOrUpdateCommand, IRequest<ApiResult<bool>>
{
    public long Id { get; set; }
}