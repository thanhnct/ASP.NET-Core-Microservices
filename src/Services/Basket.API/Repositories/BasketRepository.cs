using Basket.API.Models;
using Basket.API.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;
        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task<bool> DeleteCart(string userName)
        {
            await _redisCache.RemoveAsync(userName);
            return true;
        }

        public async Task<Cart?> GetCartByUserName(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);
            if (String.IsNullOrEmpty(basket))
            {
                return null;
            }
            return JsonSerializer.Deserialize<Cart>(basket);
        }

        public async Task<Cart?> UpdateCart(Cart basket, DistributedCacheEntryOptions options)
        {
            var basketString = JsonSerializer.Serialize(basket);
            await _redisCache.SetStringAsync(basket.UserName, basketString, options);
            return await GetCartByUserName(basket.UserName);
        }
    }
}
