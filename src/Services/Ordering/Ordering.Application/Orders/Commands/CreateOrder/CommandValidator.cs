using FluentValidation;

namespace Ordering.Application.Orders.Commands.CreateOrder;

// Não se esqueça que para ativar esse pattern é preciso registra-lo no DI do layer APPLICATION
public class CommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CommandValidator()
    {
        RuleFor(x => x.orderDTO.OrderName).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.orderDTO.CustomerId).NotEmpty().WithMessage("CustomerId is required");
        RuleFor(x => x.orderDTO.OrderItems).NotEmpty().WithMessage("OrderItems should not be empty");
    }
}