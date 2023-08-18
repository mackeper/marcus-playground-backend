using Application.CQRS;
using Domain.Todo;
using SharedKernel.Result;

namespace Application.Todo;
public sealed class GetItemsQueryHandler : IQueryHandler<GetItemsQuery, TodoItem[]>
{
    private readonly ITodoRepository repository;

    public GetItemsQueryHandler(ITodoRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<TodoItem[]>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
    {
        var response = await repository.Get(request.Id);
        if (response.IsFailure)
            return Result.Failure<TodoItem[]>(response.Error);
        var list = response.Value;
        return Result.Success(list.GetAll().ToArray());
    }
}
