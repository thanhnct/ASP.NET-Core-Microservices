using FluentValidation;
using Product.Application.Features.V1.Products.Common;

namespace Product.Application.Features.V1.Products.Commands.CreateProduct;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        Include(new CreateOrUpdateValidator());
    }
}