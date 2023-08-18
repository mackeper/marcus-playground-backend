using Application.CQRS;

namespace Application.Todo;
public record AddItemCommand(string Name, string Text) : ICommand;
