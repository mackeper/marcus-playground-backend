using Domain.Todo;
using Microsoft.EntityFrameworkCore;

namespace Web.Todo;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions options) : base(options) { }

    public DbSet<TodoList> TodoLists { get; set; }
}
