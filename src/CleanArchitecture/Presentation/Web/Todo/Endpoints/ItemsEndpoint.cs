using ILogger = Application.Logging.ILogger;

namespace Web.Todo.Endpoints;

public class ItemsEndpoint
{
    private readonly ILogger logger;

    public ItemsEndpoint(ILogger logger)
    {
        this.logger = logger;
    }

    public object Handle()
    {
        return new
        {
            TodoLists = new[] { "Hej" }
        };
    }
}
