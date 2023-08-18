using BlogService.Domain;
using BlogService.DTOs;
using BlogService.Infrastructure;
using BlogService.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;
using System.Net;
using System.Net.Http.Json;

namespace BlogService.Int.Tests.Api;

public sealed class EntryTests
{
    private static WebApplicationFactory<Program> CreateFactory()
        => CreateFactory(Array.Empty<Domain.Entry>());
    private static WebApplicationFactory<Program> CreateFactory(Domain.Entry entry)
        => CreateFactory(new Domain.Entry[] { entry });
    private static WebApplicationFactory<Program> CreateFactory(Domain.Entry[] entries)
    {
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<BlogDbContext>));
                services.Remove(dbContextDescriptor!);

                var dbConnectionDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbConnection));
                services.Remove(dbConnectionDescriptor!);

                // Create open SqliteConnection so EF won't automatically close it.
                services.AddSingleton<DbConnection>(container =>
                {
                    var connection = new SqliteConnection("DataSource=:memory:");
                    connection.Open();

                    return connection;
                });

                services.AddDbContext<BlogDbContext>((container, options) =>
                {
                    var connection = container.GetRequiredService<DbConnection>();
                    options.UseSqlite(connection);
                });
            });
        });

        using var scope = factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BlogDbContext>();
        Assert.NotNull(dbContext.Entries);
        dbContext.Entries.AddRange(entries);
        dbContext.SaveChanges();

        return factory;
    }

    private static Entry CreateEntry(
        int id = 0,
        string title = "Title",
        string content = "Content",
        string[]? tags = null,
        DateTime? dateTime = null,
        DateTime? updatedAt = null,
        bool published = false,
        bool isDeleted = false
        )
        => new(
            id,
            title,
            content,
            tags ?? Array.Empty<string>(),
            dateTime ?? DateTime.UtcNow,
            updatedAt ?? DateTime.UtcNow,
            published,
            isDeleted
            );

    private static EntryDTO CreateEntryDTO(
        int id = 0,
        string title = "Title",
        string content = "Content",
        string[]? tags = null,
        DateTime? createdAt = null,
        DateTime? updatedAt = null,
        bool published = false,
        bool isDeleted = false
        )
        => new(
            id,
            title,
            content,
            tags ?? Array.Empty<string>(),
            createdAt is null ? DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() : new DateTimeOffset(createdAt.Value).ToUnixTimeMilliseconds(),
            updatedAt is null ? DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() : new DateTimeOffset(updatedAt.Value).ToUnixTimeMilliseconds(),
            published,
            isDeleted
            );

    private static void AssertEntryDTO(EntryDTO entry1, EntryDTO entry2)
    {
        Assert.Equal(entry1.Id, entry2.Id);
        Assert.Equal(entry1.Title, entry2.Title);
        Assert.Equal(entry1.Content, entry2.Content);
        Assert.Equal(entry1.Tags, entry2.Tags);
        Assert.Equal(entry1.CreatedAt, entry2.CreatedAt);
        Assert.Equal(entry1.UpdatedAt, entry2.UpdatedAt);
        Assert.Equal(entry1.Published, entry2.Published);
        Assert.Equal(entry1.IsDeleted, entry2.IsDeleted);
    }

    [Fact]
    public static async Task GetEntries_ReturnsAllEntries()
    {
        // Arrange
        var entry = CreateEntry();
        Assert.True(entry.IsValid());

        var factory = CreateFactory(new[] { entry });

        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/entries");

        // Assert
        response.EnsureSuccessStatusCode();
        var entries = (await response.Content.ReadFromJsonAsync<EntryDTO[]>())!.Single();

        AssertEntryDTO(new EntryMapper().Map(entry), entries);
    }

    [Fact]
    public static async Task GetEntries_NoEntries_ReturnsEmptyList()
    {
        // Arrange
        var factory = CreateFactory();

        // Act
        var response = await factory.CreateClient().GetAsync("/entries");

        // Assert
        response.EnsureSuccessStatusCode();
        var entries = await response.Content.ReadFromJsonAsync<EntryDTO[]>();
        Assert.NotNull(entries);
        Assert.Empty(entries);
    }

    [Fact]
    public static async Task GetEntryById()
    {
        // Arrange
        var entries = new[]
        {
            CreateEntry(1),
            CreateEntry(2),
            CreateEntry(3),
        };

        var factory = CreateFactory(entries);

        // Act
        var response = await factory.CreateClient().GetAsync("/entries/2");

        // Assert
        response.EnsureSuccessStatusCode();
        var entry = await response.Content.ReadFromJsonAsync<EntryDTO>();
        AssertEntryDTO(new EntryMapper().Map(entries[1]), entry);
    }

    [Fact]
    public static async Task GetEntryById_NonExistentId_ReturnsNotFound()
    {
        // Arrange
        var factory = CreateFactory();

        // Act
        var response = await factory.CreateClient().GetAsync("/entries/999");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public static async Task CreateEntry_ValidInput_ReturnsCreatedEntry()
    {
        // Arrange
        var newEntry = CreateEntryDTO(1);
        var factory = CreateFactory();

        // Act
        var response = await factory.CreateClient().PostAsJsonAsync("/entries", newEntry);

        // Assert
        response.EnsureSuccessStatusCode();
        var createdEntry = await response.Content.ReadFromJsonAsync<EntryDTO>();

        AssertEntryDTO(newEntry, createdEntry);
    }

    [Theory]
    [InlineData(null, "Content", new string[] { "Tag1" }, 1689705600, true)]
    [InlineData("Title", null, new string[] { "Tag1" }, 1689705600, true)]
    [InlineData("Title", "Content", null, 1689705600, true)]
    [InlineData("Title", "Content", new string[] { "Tag1" }, -1, true)]
    public static async Task CreateEntry_InvalidInput_ReturnsBadRequest(string title, string content, string[] tags, long createdAt, bool published)
    {
        // Arrange
        var invalidEntry = CreateEntry(0, title, content, tags, DateTimeOffset.FromUnixTimeMilliseconds(createdAt).DateTime , published: published);
        var factory = CreateFactory();

        // Act
        var response = await factory.CreateClient().PostAsJsonAsync("/entries", invalidEntry);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public static async Task UpdateEntry_ValidInput_ReturnsOk()
    {
        // Arrange
        var entryToUpdate = CreateEntryDTO();
        var factory = CreateFactory(CreateEntry());

        // Act
        var response = await factory.CreateClient().PutAsJsonAsync("/entries/1", entryToUpdate);

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public static async Task UpdateEntry_NonExistentId_ReturnsNotFound()
    {
        // Arrange
        var entryToUpdate = CreateEntryDTO();
        var factory = CreateFactory();

        // Act
        var response = await factory.CreateClient().PutAsJsonAsync("/entries/999", entryToUpdate);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Theory]
    [InlineData(null, "Content", new string[] { "Tag1" }, true)]
    [InlineData("Title", null, new string[] { "Tag1" }, true)]
    public static async Task UpdateEntry_InvalidInput_ReturnsBadRequest(string title, string content, string[] tags, bool published)
    {
        // Arrange
        var entryToUpdate = CreateEntryDTO(1, title, content, tags, published: published);
        var factory = CreateFactory(CreateEntry());

        // Act
        var response = await factory.CreateClient().PutAsJsonAsync("/entries/1", entryToUpdate);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}

