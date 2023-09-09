using SharedKernel.Events;

namespace SharedKernel.DDD;
public abstract class AggregateRoot : Entity, IAggregateRoot
{
    private readonly List<IDomainEvent> domainEvents = new();

    protected AggregateRoot(Guid id) : base(id) { }

    protected void RaiseDomainEvent(IDomainEvent domainEvent) => domainEvents.Add(domainEvent);
}
