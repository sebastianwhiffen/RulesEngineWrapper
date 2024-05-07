using MediatR;
using RulesEngine.Data;
using RulesEngineWrapper.Domain;

namespace RulesEngineWrapper.Infrastructure;
static class MediatorExtension
{
  public static async Task DispatchDomainEventsAsync(this IMediator mediator, RulesEngineContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        foreach (var domainEntity in domainEntities)
            domainEntity.Entity.ClearDomainEvents();

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}
