using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Xunit;

namespace Dev.Int.Tests.Tools;

public sealed class GuidGeneratorTests
{
    private static WebApplicationFactory<Program> CreateFactory()
    {
        var factory = new WebApplicationFactory<Program>();
        return factory;
    }
    [Fact]
    public async Task GetGuid_Always_ReturnOneGuid()
    {
        // Arrange
        var factory = CreateFactory();
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/guid");

        // Assert
        response.EnsureSuccessStatusCode();
        var guid = await response.Content.ReadFromJsonAsync<string>();
        Assert.NotNull(guid);
        Assert.True(Guid.TryParse(guid, out _));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(1337)]
    public async Task GetGuid_NonNegativeCount_ReturnCountGuid(int count)
    {
        // Arrange
        var factory = CreateFactory();
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/guid/{count}");

        // Assert
        response.EnsureSuccessStatusCode();
        var guids = await response.Content.ReadFromJsonAsync<string[]>();
        Assert.NotNull(guids);
        Assert.Equal(count, guids.Length);
        Assert.Equal(count, guids.Distinct().Count());
        foreach (var guid in guids)
        {
            Assert.True(Guid.TryParse(guid, out _), guid);
        }
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-1337)]
    public async Task GetGuid_NegativeCount_ReturnEmptyArray(int count)
    {
        // Arrange
        var factory = CreateFactory();
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/guid/{count}");

        // Assert
        response.EnsureSuccessStatusCode();
        var guids = await response.Content.ReadFromJsonAsync<string[]>();
        Assert.NotNull(guids);
        Assert.Empty(guids);
    }
}