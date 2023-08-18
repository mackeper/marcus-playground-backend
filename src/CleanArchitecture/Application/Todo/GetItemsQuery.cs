using Application.CQRS;
using Domain.Todo;

namespace Application.Todo;
public record GetItemsQuery(Guid Id) : IQuery<TodoItem[]>;
