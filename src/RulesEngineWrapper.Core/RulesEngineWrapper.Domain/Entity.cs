
namespace RulesEngineWrapper.Domain
{
    public abstract class Entity
    {
        public virtual Guid Id { get; set; }

        // private List<INotification> _domainEvents;
        // public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        // public void AddDomainEvent(INotification eventItem)
        // {
        //     _domainEvents = _domainEvents ?? new List<INotification>();
        //     _domainEvents.Add(eventItem);
        // }

        // public void RemoveDomainEvent(INotification eventItem)
        // {
        //     _domainEvents?.Remove(eventItem);
        // }

        // public void ClearDomainEvents()
        // {
        //     _domainEvents?.Clear();
        // }

    }
}