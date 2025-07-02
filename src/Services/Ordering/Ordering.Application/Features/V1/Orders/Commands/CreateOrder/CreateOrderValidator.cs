using FluentValidation;

namespace Ordering.Application.Features.V1.Orders;

public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        
    }
}