using BuildingBlocks.Messaging.Event;
using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Orders.EventHandlers.Integration;

public class BasketCheckoutEventHandler(ISender sender, ILogger<BasketCheckoutEventHandler> logger) 
    : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var command = MapToCreateOrderCommand(context.Message);
        await sender.Send(command);
    }

    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
    {
        var orderId = Guid.NewGuid();

        var addressDTO = new AddressDTO(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine,
                                        message.Country, message.State, message.ZipCode);

        var payment = new PaymentDTO(message.CardName, message.CardNumber, message.Expiration, message.CVV, message.PaymentMethod);

        var orderDTO = new OrderDTO(
            Id: orderId,
            CustomerId: message.CustomerId,
            OrderName: message.UserName,
            ShippingAddress: addressDTO,
            BillingAddress: addressDTO,
            Payment: payment,
            Status: Ordering.Domain.Enums.OrderStatus.Pending,
            OrderItems: [
                new OrderItemDTO(orderId, new Guid("8443c2e2-01d0-41af-9988-4c9c6e8a2e39"), 2, 500),
                new OrderItemDTO(orderId, new Guid("10ab9c5b-ebb8-43d1-a5b7-afb94ff882f2"), 1, 400),
            ]
        );

        return new CreateOrderCommand(orderDTO);        
    }
}
