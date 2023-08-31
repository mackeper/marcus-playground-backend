namespace Dev.Tools.GuidGenerator;

public class Api
{
    public void RegisterEndpoints(WebApplication app)
    {
        app.MapGet("/guid", () => Guids(1).First());
        app.MapGet("/guid/{count:int}", Guids);
    }

    private Guid[] Guids(int count)
    {
        if (count < 0)
            return Array.Empty<Guid>();
        return Enumerable.Range(0, count).Select(_ => Guid.NewGuid()).ToArray();
    }

}
