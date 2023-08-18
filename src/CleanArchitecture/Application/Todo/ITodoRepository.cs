using Domain.Todo;
using Infrastructure.Persistance;

namespace Application.Todo;
public interface ITodoRepository : IRepository<TodoList>
{
}
