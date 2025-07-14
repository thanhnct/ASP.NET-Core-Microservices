using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Repositories;
using Product.Infrastructure.Persistence;

namespace Product.Infrastructure.Repositories;

public class ProductRepository : RepositoryBaseAsync<Domain.Entities.Product, long, ProductContext>, IProductRepository
{
    public ProductRepository(ProductContext dbContext, IUnitOfWork<ProductContext> unitOfWork) : base(dbContext, unitOfWork)
    {
    }

    public async Task<Domain.Entities.Product?> GetProductByNoAsync(string productNo)
    {
        return await FindByCondition(x => x.No.Equals(productNo)).FirstOrDefaultAsync();
    }
}