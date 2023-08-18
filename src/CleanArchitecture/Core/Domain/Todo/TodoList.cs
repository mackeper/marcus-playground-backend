using SharedKernel.DDD;

namespace Domain.Todo;
public class TodoList : AggregateRoot
{
    public string Name { get; }
    private readonly IList<TodoItem> items;

    public TodoList(Guid id, string name, IList<TodoItem> items) : base(id)
    {
        Name = name;
        this.items = items;
    }

    public void AddItem(TodoItem item) => items.Add(item);
    public void RemoveItem(TodoItem item) => items.Remove(item);
    public IReadOnlyList<TodoItem> GetAll() => items.AsReadOnly();
    public TodoItem Get(int index) => items[index];
    public void SetItemDone() { }
}
