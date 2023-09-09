using Microsoft.AspNetCore.Mvc;
using Web.Todo.Endpoints;
using ILogger = Application.Logging.ILogger;

namespace Web.Todo;

public class Routes
{
    public Routes()
    {

    }

    public static void Initialize(IEndpointRouteBuilder builder, ILogger logger)
    {
        var createItemEndpoint = new CreateItemEndpoint(logger);
        var itemEndpoint = new ItemsEndpoint(logger);

        builder.MapPost("/todo", ([FromHeader] name) => createItemEndpoint.Handle());
        builder.MapGet("/todo", () => itemEndpoint.Handle());
    }
}
