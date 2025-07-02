using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        Task<Cart?> GetCartByUserName(string userName);

        Task<Cart?> UpdateCart(Cart basket, DistributedCacheEntryOptions options);

        Task<bool> DeleteCart(string userName);
    }
}
