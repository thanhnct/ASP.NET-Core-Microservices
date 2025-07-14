using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Repositories;
using Shared.SeedWorks;

namespace Product.Application.Features.V1.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ApiResult<bool>>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ApiResult<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByCondition(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (product == null)
        {
            return new ApiResult<bool>(false, "Product not found");
        }

        product.No = request.No!;
        product.Name = request.Name!;
        product.Summary = request.Summary;
        product.Description = request.Description;
        product.Price = request.Price;

        await _productRepository.UpdateAsync(product);
        await _productRepository.SaveChangeAsync();

        return new ApiSuccessResult<bool>(true);
    }
}