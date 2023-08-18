using BlogService.Domain;

namespace BlogService.Tests.Domain;
public sealed class EntryTests
{
    [Theory]
    [InlineData(1, "Title", "Content", new[] { "Tag1", "Tag2" }, "2023-08-15T00:00:00Z", true, false, true)] // Valid entry
    [InlineData(2, null, "Content", new[] { "Tag1" }, "2023-08-15T12:00:00Z", false, false, false)]      // Missing title
    [InlineData(3, "Title", null, new[] { "Tag1" }, "2023-08-15T12:00:00Z", false, false, false)]      // Missing content
    [InlineData(4, "Title", "Content", null, "2023-08-15T12:00:00Z", false, false, false)]             // Missing tags
    [InlineData(5, "Title", "Content", new[] { "Tag1" }, "2023-08-15T12:00:00Z", false, false, true)]  // Valid entry with missing Published flag
    [InlineData(6, "Title", "Content", new[] { "Tag1" }, "2023-08-15T12:00:00Z", true, false, true)]   // Valid entry
    [InlineData(7, "Title", "Content", new[] { "Tag1" }, "1970-01-01T00:00:00Z", false, false, true)] // DateTime is equal to epoch
    public static void IsValid_ReturnsExpectedResult(
        int id, string title, string content, string[] tags, string dateTimeStr, bool published, bool isDeleted, bool expectedResult)
    {
        // Arrange
        var dateTime = DateTimeOffset.Parse(dateTimeStr).UtcDateTime;
        var entry = new Entry(id, title, content, tags, dateTime, dateTime, published, isDeleted);

        // Act
        var result = entry.IsValid();

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void IsValid_NonUtcDateTime_ReturnsFalse()
    {
        // Arrange
        var dateTime = DateTimeOffset.Parse("2023-08-15T12:00:00+02:00").DateTime;
        var entry = new Entry(1, "Title", "Content", new[] { "Tag1" }, dateTime, dateTime, true, false);

        // Act
        var result = entry.IsValid();

        // Assert
        Assert.False(result);
    }
}
