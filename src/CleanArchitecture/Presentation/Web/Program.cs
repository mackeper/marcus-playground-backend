using Application.Todo;
using Autofac.Extensions.DependencyInjection;
using Serilog;
using Web.Adapters.Logging;
using Web.Todo.Endpoints;

namespace Web;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            Args = args,
            ApplicationName = typeof(Program).Assembly.FullName,
        });
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

        var logOutputTemplate = @"{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}";
        var loggerAdapter = new SerilogAdapter(
            new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: logOutputTemplate)
            .WriteTo.File(@"log.txt", outputTemplate: logOutputTemplate)
            .CreateLogger()
        );
        builder.Services.AddSingleton<Application.Logging.ILogger, SerilogAdapter>(_ => loggerAdapter);

        RegisterCommandsAndHandlers(builder.Services);

        var app = builder.Build();

        var logger = (Application.Logging.ILogger)app.Services.GetService(typeof(Application.Logging.ILogger))!;

        app.MapGet("Todo", () => new CreateItemEndpoint(logger).Handle());
        app.MapPost("Todo", () => new CreateItemEndpoint(logger).Handle());

        app.MapGet("/", () => "Hello, World!");

        logger.Information("Hello, World");

        app.Run();
    }

    private static void RegisterCommandsAndHandlers(IServiceCollection builder)
    {
        builder.AddScoped<AddItemCommandHandler>();
    }
}
