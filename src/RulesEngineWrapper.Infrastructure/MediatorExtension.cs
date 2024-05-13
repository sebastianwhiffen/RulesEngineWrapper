using MediatR;
using RulesEngine.Data;
using RulesEngineWrappers.Domain;

namespace RulesEngineWrappers.Infrastructure;
static class MediatorExtension
{
  public static async Task DispatchDomainEventsAsync(this IMediator mediator, RulesEngineWrapperContext ctx)
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
