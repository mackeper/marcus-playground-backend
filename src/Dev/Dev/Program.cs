namespace Dev;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options =>
            options.AddDefaultPolicy(policy =>
                policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

        var urls = builder.Configuration.GetValue<string>("Urls") ?? "http://localhost:5003";
        builder.WebHost.UseUrls(urls);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        AddEndpoints(app);

        app.Run();
    }

    public static void AddEndpoints(WebApplication app)
    {
        new Tools.GuidGenerator.Api().RegisterEndpoints(app);
    }
}   
