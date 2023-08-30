namespace Dev.Tools.GuidGenerator;

public class Api
{
    public void RegisterEndpoints(WebApplication app)
    {
        app.MapGet("/guid", () =>
        {
            return Guid.NewGuid().ToString();
        })
        .WithName("GetGuid")
        .WithOpenApi();
    }
}
