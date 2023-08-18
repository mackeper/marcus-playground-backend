using Application.CQRS;
using SharedKernel.Result;

namespace Application.Todo;
public sealed class AddItemCommandHandler : ICommandHandler<AddItemCommand>
{
    public Task<Result> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Result.Success());
    }
}
