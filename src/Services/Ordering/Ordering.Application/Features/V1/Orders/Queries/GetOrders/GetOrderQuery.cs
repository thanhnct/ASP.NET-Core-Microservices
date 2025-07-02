using MediatR;
using Ordering.Application.Common.Models;
using Shared.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.V1.Orders
{
    public class GetOrderQuery : IRequest<ApiResult<List<OrderDto>>>
    {
        public string UserName { get; private set; }

        public GetOrderQuery(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }
    }
}
