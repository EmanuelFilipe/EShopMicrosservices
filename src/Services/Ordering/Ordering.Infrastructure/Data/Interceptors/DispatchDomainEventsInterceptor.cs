using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ordering.Infrastructure.Data.Interceptors;

public class DispatchDomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await DispatchDomainEvents(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public async Task DispatchDomainEvents(DbContext? context)
    {
        if (context is null) return;

        // Obtém todas as entidades que implementam IAggregate e que possuem eventos de domínio pendentes
        var aggregates = context.ChangeTracker.Entries<IAggregate>()
                                              .Where(a => a.Entity.DomainEvents.Any())
                                              .Select(a => a.Entity);

        // Extrai todos os eventos de domínio dessas entidades e coloca em uma lista
        var domainEvents = aggregates.SelectMany(a => a.DomainEvents)
                                     .ToList();

        // Limpa os eventos de domínio já capturados de cada entidade, evitando duplicação
        aggregates.ToList().ForEach(a => a.ClearDomainEvents());

        // Publica cada evento de domínio capturado usando o mediator (padrão Mediator),
        // permitindo que handlers apropriados processem esses eventos
        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}
