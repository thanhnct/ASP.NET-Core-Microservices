using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;
using Shared.SeedWorks;

namespace Ordering.Application.Features.V1.Orders
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, ApiResult<List<OrderDto>>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<GetOrderQueryHandler> _logger;

        public GetOrderQueryHandler(IOrderRepository orderRepository, ILogger<GetOrderQueryHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _logger = logger;
        }

        public async Task<ApiResult<List<OrderDto>>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"BEGIN: Method GetOrderQuery User: {request.UserName}");

            var orders = await _orderRepository.GetOrderByUserName(request.UserName);

            _logger.LogInformation($"END: Method GetOrderQuery User: {request.UserName}");

            var orderResult = orders.Select(o => new OrderDto
            {
                Id = o.Id,
                UserName = o.UserName,
                TotalPrice = o.TotalPrice,
                FirstName = o.FirstName,
                LastName = o.LastName,
                EmailAddress = o.EmailAddress,
                ShippingAddress = o.ShippingAddress,
                InvoiceAddress = o.InvoiceAddress,
                Status = o.Status
            }).ToList();

            return new ApiSuccessResult<List<OrderDto>>(orderResult);
        }
    }
}
