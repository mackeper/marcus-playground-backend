using Dashboard.Infrastructure;
using Dashboard.MealPlan.Domain;
using Dashboard.Todo;
using Microsoft.EntityFrameworkCore;

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

        var connectionString = builder.Configuration.GetConnectionString("Dashboard") ?? "Data Source=Dashboard.db";
        builder.Services.AddDbContext<DashboardDbContext>(options => options.UseSqlite(connectionString));

        var app = builder.Build();

        using (var scope = app.Services.CreateScope()) {
            var dbContext = scope.ServiceProvider.GetRequiredService<DashboardDbContext>();
            dbContext.Database.Migrate();
        }

        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
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
        new Todo.Api(new EntryMapper()).RegisterEndpoints(app);
        new MealPlan.Api(new MealPlanMapper(new MealMapper())).RegisterEndpoints(app);
    }
}
