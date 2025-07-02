using MediatR;
using Shared.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.V1.Orders
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ApiResult<long>>
    {
        public Task<ApiResult<long>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
