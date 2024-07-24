namespace Dashboard;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options =>
            options.AddDefaultPolicy(policy =>
                policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

        var urls = builder.Configuration.GetValue<string>("Urls") ?? "http://localhost:5006";
        builder.WebHost.UseUrls(urls);

        var app = builder.Build();

        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        if (app.Environment.IsProduction()) {
            app.UseExceptionHandler("/error");
            app.UseHttpsRedirection();
        }

        AddEndpoints(app);

        app.UseCors();
        app.Run();
    }

    private static void AddEndpoints(WebApplication app) {
        var summaries = new[]
        {
            "Freezing",
            "Bracing",
            "Chilly",
            "Cool",
            "Mild",
            "Warm",
            "Balmy",
            "Hot",
            "Sweltering",
            "Scorching"
        };

        app.MapGet("/weatherforecast", () => {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();
            return forecast;
        })
        .WithName("GetWeatherForecast")
        .WithOpenApi();

        new Todo.Api().RegisterEndpoints(app);
    }

    record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary) {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
