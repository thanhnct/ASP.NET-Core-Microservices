using MediatR;
using Product.Domain.Repositories;
using Shared.SeedWorks;

namespace Product.Application.Features.V1.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ApiResult<long>>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ApiResult<long>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Domain.Entities.Product
        {
            Id = 0,
            No = request.No!,
            Name = request.Name!,
            Summary = request.Summary,
            Description = request.Description,
            Price = request.Price
        };

        var result = await _productRepository.CreateAsync(product);

        return new ApiSuccessResult<long>(result);
    }
}