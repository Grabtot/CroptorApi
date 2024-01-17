using FluentValidation;

namespace Croptor.Application.Orders.Commands;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(order => order.Amount).GreaterThan(0);
    }
}