using FluentValidation;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.OrderDTO.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.OrderDTO.OrderName).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.OrderDTO.CustomerId).NotEmpty().WithMessage("CustomerId should not be empty");
    }
}