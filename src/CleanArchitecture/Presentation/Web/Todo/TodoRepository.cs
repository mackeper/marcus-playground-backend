using Application.Todo;
using Domain.Todo;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Result;

namespace Web.Todo;

public class TodoRepository : ITodoRepository
{
    private readonly TodoDbContext context;

    public TodoRepository(TodoDbContext context)
    {
        this.context = context;
    }

    public async Task<Result<TodoList>> Create(TodoList entity)
    {
        context.TodoLists.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<Result> Delete(Guid id)
    {
        var list = await context.TodoLists.FirstOrDefaultAsync(list => list.Id == id);
        if (list is null)
            return Result.Failure(new Error("404", $"Todo list with id {id} was not found."));

        return Result.Success();
    }

    public async Task<Result<TodoList>> Get(Guid id)
    {
        var list = await context.TodoLists.FirstOrDefaultAsync(list => list.Id == id);
        if (list is null)
            return Result.Failure<TodoList>(new Error("404", $"Todo list with id {id} was not found."));

        return Result.Success(list);
    }

    public async Task<Result<TodoList[]>> GetAll()
    {
        return await context.TodoLists.ToArrayAsync();
    }

    public async Task<Result> Update(TodoList entity)
    {
        var list = await context.TodoLists.FirstOrDefaultAsync(list => list.Id == entity.Id);
        if (list is null)
            return Result.Failure(new Error("404", $"Todo list with id {entity.Id} was not found."));

        list = entity; // TODO This does not work.
        await context.SaveChangesAsync();

        return Result.Success();
    }
}
