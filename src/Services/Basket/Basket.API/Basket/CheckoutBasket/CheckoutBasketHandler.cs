using BuildingBlocks.Messaging.Event;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckoutDTO BasketCheckoutDTO) : ICommand<CheckoutBasketResult>;
public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketCommandHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        // get existing basket with total price
        var basket = await repository.GetBasket(command.BasketCheckoutDTO.UserName, cancellationToken);

        if (basket is null) return new CheckoutBasketResult(false);
        
        // set totalprice on basketcheckout event message
        var eventMessage = command.BasketCheckoutDTO.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;

        // send basketcheckout event to rabbitmq using masstransit
        await publishEndpoint.Publish(eventMessage, cancellationToken);

        // delete current basket
        await repository.DeleteBasket(command.BasketCheckoutDTO.UserName, cancellationToken);

        return new CheckoutBasketResult(true);
    }
}
