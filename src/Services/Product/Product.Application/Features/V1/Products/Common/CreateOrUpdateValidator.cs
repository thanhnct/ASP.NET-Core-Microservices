using FluentValidation;

namespace Product.Application.Features.V1.Products.Common;

public class CreateOrUpdateValidator : AbstractValidator<CreateOrUpdateCommand>
{
    public CreateOrUpdateValidator()
    {
        RuleFor(x => x.No)
            .NotEmpty().WithMessage("Product No is required")
            .MaximumLength(150).WithMessage("Product No must not exceed 150 characters");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product Name is required")
            .MaximumLength(250).WithMessage("Product Name must not exceed 250 characters");

        RuleFor(x => x.Summary)
            .MaximumLength(450).WithMessage("Summary must not exceed 450 characters");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}