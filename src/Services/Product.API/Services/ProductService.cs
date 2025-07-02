using Contracts.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Product.API.Data;
using Product.API.Models;
using Product.API.Services.Interfaces;
using Shared.DTOs.Product;
using Shared.DTOs;
using X.PagedList.EF;


namespace Product.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryBaseAsync<Catalog, long, ProductContext> _productRepository;

        public ProductService(IRepositoryBaseAsync<Catalog, long, ProductContext> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<PagedDto<ProductDto>> GetProducts(int page = 1)
        {
            var products = await _productRepository.FindAll().ToPagedListAsync(page, 10);

            return new PagedDto<ProductDto>
            {
                Items = products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    No = p.No,
                    Summary = p.Summary,
                    Price = p.Price,
                    Description = p.Description
                }),
                Page = products.PageNumber,
                PageSize = products.PageSize,
                TotalCount = products.TotalItemCount
            };
        }

        public async Task<ProductDto?> GetProductById(long id)
        {
            var product = await _productRepository.FindByCondition(p => p.Id == id).Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                No = p.No,
                Summary = p.Summary,
                Price = p.Price,
                Description = p.Description
            }).FirstOrDefaultAsync();

            return product;
        }

        public async Task<long> CreateProduct(ProductCreateDto productDto)
        {
            var newProduct = new Catalog
            {
                Id = 0,
                Name = productDto.Name,
                No = productDto.No,
                Summary = productDto.Summary,
                Price = productDto.Price,
                Description = productDto.Description
            };

            var id = await _productRepository.CreateAsync(newProduct);
            return id;
        }

        public async Task UpdateProduct(long id, ProductUpdateDto productDto)
        {
            var product = await _productRepository.FindByCondition(p => p.Id == id).FirstOrDefaultAsync();
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            product.Name = productDto.Name;
            product.No = productDto.No;
            product.Summary = productDto.Summary;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteProduct(long id)
        {
            var product = await _productRepository.FindByCondition(p => p.Id == id).FirstOrDefaultAsync();
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            await _productRepository.DeleteAsync(product);
        }
    }
}
