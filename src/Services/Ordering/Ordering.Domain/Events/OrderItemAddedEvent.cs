namespace Ordering.Domain.Events;

public record OrderItemAddedEvent(Order order, OrderItem orderItem) : IDomainEvent;
