using MediatR;
using Product.Application.Features.V1.Products.Common;
using Shared.SeedWorks;

namespace Product.Application.Features.V1.Products.Commands.CreateProduct;

public class CreateProductCommand : CreateOrUpdateCommand, IRequest<ApiResult<long>>
{
}