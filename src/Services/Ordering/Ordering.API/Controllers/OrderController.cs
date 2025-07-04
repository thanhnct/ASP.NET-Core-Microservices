using Contracts.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Common.Models;
using Ordering.Application.Features.V1.Orders;
using Shared.Services.Email;
using System.ComponentModel.DataAnnotations;

namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IEmailService<MailRequest> _emailService;
        public OrderController(IMediator mediator, IEmailService<MailRequest> emailService)
        {
            _mediator = mediator;
            _emailService = emailService;
        }

        [HttpGet("GetOrderByUserName/{userName}")]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrderByUserName([FromRoute] string userName)
        {
            var query = new GetOrderQuery(userName);
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var order = await _mediator.Send(command);
            return Ok(order);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteOrder([FromRoute] long id)
        {
            var command = new DeleteOrderCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("test-email")]
        public async Task<IActionResult> TestEmail()
        {
            var request = new MailRequest
            {
                FromEmail = "hello@gmail.com",
                ToEmail = "congthanh29897@gmail.com",
                Subject = "Test Email",
                Body = "<h1>Test Email</h1>"
            };

            await _emailService.SendEmailAsync(request);

            return Ok();
        }
    }

}
