using SharedKernel.DDD;

namespace Domain.Todo;

public class TodoItem : Entity
{
    public string Name { get; }
    public string Text { get; }
    public bool IsDone { get; private set; }

    public TodoItem(Guid id, string name, string text, bool isDone = false) : base(id)
    {
        Name = name;
        Text = text;
        IsDone = isDone;
    }

    public void SetDone(bool isDone) => IsDone = isDone;

}
