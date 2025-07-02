using Product.API.Models;
using Shared.DTOs.Product;
using Shared.DTOs;

namespace Product.API.Services.Interfaces
{
    public interface IProductService
    {
        Task<PagedDto<ProductDto>> GetProducts(int page = 1);

        Task<ProductDto?> GetProductById(long id);

        Task<long> CreateProduct(ProductCreateDto productDto);

        Task UpdateProduct(long id, ProductUpdateDto productDto);

        Task DeleteProduct(long id);
    }
}
