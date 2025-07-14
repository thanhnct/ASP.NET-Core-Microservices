using FluentValidation;
using Product.Application.Features.V1.Products.Common;

namespace Product.Application.Features.V1.Products.Commands.UpdateProduct;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        Include(new CreateOrUpdateValidator());
        
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Product Id must be greater than 0");
    }
}