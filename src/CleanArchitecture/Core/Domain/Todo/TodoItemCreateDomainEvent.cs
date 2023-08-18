using SharedKernel.Events;

namespace Domain.Todo;
public sealed record TodoItemCreateDomainEvent(Guid id, string name, string text) : IDomainEvent;
