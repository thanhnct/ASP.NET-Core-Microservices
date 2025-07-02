using MediatR;
using Ordering.Application.Common.Models;
using Ordering.Application.Features.V1.Orders.Common;
using Shared.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.V1.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand : CreateOrUpdateCommand, IRequest<ApiResult<OrderDto>>
    {
        public long Id { get; private set; }

        public void SetId(long id)
        {
            Id = id;
        }
    }
}
