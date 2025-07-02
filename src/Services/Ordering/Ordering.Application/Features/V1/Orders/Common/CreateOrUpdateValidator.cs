using FluentValidation;

namespace Ordering.Application.Features.V1.Orders.Common;

public class CreateOrUpdateValidator : AbstractValidator<CreateOrUpdateCommand>
{
    public CreateOrUpdateValidator()
    {
        RuleFor(p => p.FirstName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        
        RuleFor(p => p.LastName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        
        RuleFor(p => p.EmailAddress)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .EmailAddress().WithMessage("{PropertyName} is not a valid email address.");
        
        RuleFor(p => p.TotalPrice)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
    }
}