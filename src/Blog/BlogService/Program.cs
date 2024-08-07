﻿using BlogService.Api;
using BlogService.Infrastructure;
using BlogService.Mappers;
using Microsoft.EntityFrameworkCore;

namespace BlogService;

public sealed class Program {
    private static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options =>
            options.AddDefaultPolicy(policy =>
                policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));


        var urls = builder.Configuration.GetValue<string>("Urls") ?? "http://localhost:5000";
        builder.WebHost.UseUrls(urls);

        var connectionString = builder.Configuration.GetConnectionString("Blog") ?? "Data Source=Blog.db";
        builder.Services.AddDbContext<BlogDbContext>(options => options.UseSqlite(connectionString));

        var app = builder.Build();

        using (var scope = app.Services.CreateScope()) {
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogDbContext>();
            dbContext.Database.Migrate();
        }

        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseHttpsRedirection();

        AddEndpoints(app);

        app.UseCors();
        app.Run();

    }

    private static void AddEndpoints(WebApplication app) {
        new EntryApi(new EntryMapper()).AddEndpoints(app);
    }
}
