using MediatR;
using Ordering.Application.Features.V1.Orders.Common;
using Shared.SeedWorks;

namespace Ordering.Application.Features.V1.Orders;

public class CreateOrderCommand : CreateOrUpdateCommand, IRequest<ApiResult<long>>
{
    public string? UserName { get; set; }
}