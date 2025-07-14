using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Repositories;
using Shared.SeedWorks;

namespace Product.Application.Features.V1.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ApiResult<bool>>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ApiResult<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByCondition(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (product == null)
        {
            return new ApiResult<bool>(false, "Product not found");
        }

        await _productRepository.DeleteAsync(product);
        await _productRepository.SaveChangeAsync();

        return new ApiSuccessResult<bool>(true);
    }
}