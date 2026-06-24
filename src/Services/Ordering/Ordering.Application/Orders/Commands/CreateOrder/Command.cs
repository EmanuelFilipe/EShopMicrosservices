using BuildingBlocks.CQRS;
using Ordering.Application.DTOs;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDTO orderDTO) : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid id);
