namespace Dashboard.Todo;

public class Api
{
    public void RegisterEndpoints(WebApplication app)
    {
        app.MapGet("/todo", Items);
    }

    private Item[] Items() => new[]
    {
        new Item { Title = "Feed the cats" },
        new Item { Title = "Clean the bathroom" },
        new Item { Title = "Wash the dishes" },
        new Item { Title = "Vacuum the living room" },
        new Item { Title = "Mow the lawn" }
    };

}
