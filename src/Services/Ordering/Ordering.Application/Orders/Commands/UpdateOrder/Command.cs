namespace Ordering.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(OrderDTO Order) : ICommand<UpdateOrderResult>;

public record UpdateOrderResult(bool IsSuccess);
