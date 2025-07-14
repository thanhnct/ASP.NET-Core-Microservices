using MediatR;
using Product.Domain.Repositories;
using Shared.DTOs;
using Shared.DTOs.Product;
using Shared.SeedWorks;
using X.PagedList;
using X.PagedList.EF;

namespace Product.Application.Features.V1.Products.Queries.GetProducts;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ApiResult<PagedDto<ProductDto>>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ApiResult<PagedDto<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = _productRepository.FindAll();

        var pagedProducts = await products.ToPagedListAsync(request.Page, 10);

        var result = new PagedDto<ProductDto>
        {
            Items = pagedProducts.Select(p => new ProductDto
            {
                Id = p.Id,
                No = p.No,
                Name = p.Name,
                Summary = p.Summary,
                Description = p.Description,
                Price = p.Price
            }),
            Page = pagedProducts.PageNumber,
            PageSize = pagedProducts.PageSize,
            TotalCount = pagedProducts.TotalItemCount
        };

        return new ApiSuccessResult<PagedDto<ProductDto>>(result);
    }
}