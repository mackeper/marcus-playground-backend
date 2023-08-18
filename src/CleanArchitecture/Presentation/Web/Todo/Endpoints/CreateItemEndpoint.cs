using Application.Logging;
using ILogger = Application.Logging.ILogger;

namespace Web.Todo.Endpoints;

public class CreateItemEndpoint
{
    private readonly ILogger logger;

    public CreateItemEndpoint(ILogger logger)
    {
        this.logger = logger;
    }

    public Task<object> Handle()
    {
        return Task.FromResult((object)new
        {
            Name = "Hej",
        });
    }
}
