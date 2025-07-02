using MediatR;
using Ordering.Application.Common.Models;
using Shared.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.V1.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHanlder : IRequestHandler<UpdateOrderCommand, ApiResult<OrderDto>>
    {
        public Task<ApiResult<OrderDto>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
