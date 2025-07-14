using Contracts.Common.Interfaces;

namespace Product.Domain.Repositories;

public interface IProductRepository : IRepositoryBaseAsync<Entities.Product, long>
{
    Task<Entities.Product?> GetProductByNoAsync(string productNo);
}