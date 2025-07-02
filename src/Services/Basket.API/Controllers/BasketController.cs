using Basket.API.Models;
using Basket.API.Repositories.Interfaces;
using EventBus.Messages.IntegrationEvents.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : Controller
    {
        private readonly IBasketRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketController(IBasketRepository repository, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> GetBasketByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest();
            }

            var basket = await _repository.GetCartByUserName(userName);

            if (basket == null)
            {
                return NotFound();
            }

            return Ok(basket);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasket([FromBody] Cart cart)
        {
            if (cart == null)
            {
                return BadRequest();
            }

            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.UtcNow.AddHours(1))
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));

            var cartReturn = await _repository.UpdateCart(cart, options);

            if (cartReturn == null)
            {
                return NotFound();
            }

            return Ok(cartReturn);
        }

        [HttpDelete("{userName}")]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest();
            }

            var result = await _repository.DeleteCart(userName);

            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout checkout)
        {
            if (checkout == null)
            {
                return BadRequest();
            }

            var basket = await _repository.GetCartByUserName(checkout.UserName);

            if (basket == null)
            {
                return NotFound();
            }

            var eventMessage = new BacketCheckoutEvent
            {
                UserName = checkout.UserName,
                FirstName = checkout.FirstName,
                LastName = checkout.LastName
            };
            eventMessage.TotalPrice = basket.TotalPrice;

            await _publishEndpoint.Publish(eventMessage);

            await _repository.DeleteCart(checkout.UserName);

            return Accepted();
        }
    }
}
