using BuildingBlocks.CQRS;
using Ordering.Application.DTOs;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(OrderDTO OrderDTO) : ICommand<UpdateOrderResult>;

public record UpdateOrderResult(bool IsSuccess);
