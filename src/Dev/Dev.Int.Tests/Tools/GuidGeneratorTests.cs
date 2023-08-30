using Dev;
using Microsoft.AspNetCore.Mvc.Testing;
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
    public async Task GetGuid()
    {
        // Arrange
        var factory = CreateFactory();
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/guid");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.True(Guid.TryParse(await response.Content.ReadAsStringAsync(), out _));
    }
}