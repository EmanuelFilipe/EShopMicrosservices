using MassTransit;
using Microsoft.FeatureManagement;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler(IPublishEndpoint publishEndpoint, 
                                      IFeatureManager featureManager,
                                      ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        // verificando se o recurso esta ativado pelo FeatureManagement antes de publica-lo
        if (await featureManager.IsEnabledAsync("OrderFullfillment"))
        {
            var orderCreatedIntegrationEvent = domainEvent.Order.ToOrderDTO();
            await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
        }
    }
}
