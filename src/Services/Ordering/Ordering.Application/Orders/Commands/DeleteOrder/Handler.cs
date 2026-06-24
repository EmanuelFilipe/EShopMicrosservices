namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderHandler(IApplicationDbContext dbContex) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(command.OrderId);

        var order = await dbContex.Orders.FindAsync(orderId, cancellationToken);

        if (order is null) throw new OrderNotFoundException(command.OrderId);

        dbContex.Orders.Remove(order);
        await dbContex.SaveChangesAsync(cancellationToken);

        return new DeleteOrderResult(true);
    }
}
