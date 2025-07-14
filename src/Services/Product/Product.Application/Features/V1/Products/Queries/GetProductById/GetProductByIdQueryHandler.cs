using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Repositories;
using Shared.DTOs.Product;
using Shared.SeedWorks;

namespace Product.Application.Features.V1.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ApiResult<ProductDto?>>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ApiResult<ProductDto?>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByCondition(x => x.Id == request.Id)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                No = p.No,
                Name = p.Name,
                Summary = p.Summary,
                Description = p.Description,
                Price = p.Price
            })
            .FirstOrDefaultAsync(cancellationToken);

        return new ApiSuccessResult<ProductDto?>(product);
    }
}