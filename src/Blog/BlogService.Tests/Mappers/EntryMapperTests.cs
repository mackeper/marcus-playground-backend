using BlogService.Domain;
using BlogService.DTOs;
using BlogService.Mappers;

namespace BlogService.Tests.Mappers;
public sealed class EntryMapperTests
{
    [Fact]
    public void MapEntryDTOToEntry()
    {
        // Arrange
        var entryDTO = new EntryDTO(
            0,
            "Title",
            "Content",
            new[] { "Tag1", "Tag2" },
            DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            true,
            true
            );

        var entryMapper = new EntryMapper();

        // Act
        var entry = entryMapper.Map(entryDTO);

        // Assert
        Assert.True(entry.IsValid(), @"Entry should be valid.");
        Assert.Equal(0, entry.Id);
        Assert.Equal(entryDTO.Title, entry.Title);
        Assert.Equal(entryDTO.Content, entry.Content);
        Assert.Equal(entryDTO.Tags, entry.Tags);
        Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(entryDTO.CreatedAt).UtcDateTime, entry.CreatedAt);
        Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(entryDTO.UpdatedAt).UtcDateTime, entry.UpdatedAt);
        Assert.Equal(entryDTO.Published, entry.Published);
    }

    [Fact]
    public void MapEntryToEntryDTO()
    {
        // Arrange
        var entry = new Entry(
            0,
            "Title",
            "Content",
            new[] { "Tag1", "Tag2" },
            DateTime.UtcNow,
            DateTime.UtcNow,
            true,
            true
            );

        Assert.True(entry.IsValid(), @"Entry should be valid.");

        var entryMapper = new EntryMapper();

        // Act
        var entryDTO = entryMapper.Map(entry);

        // Assert
        Assert.Equal(entry.Title, entryDTO.Title);
        Assert.Equal(entry.Content, entryDTO.Content);
        Assert.Equal(entry.Tags, entryDTO.Tags);
        Assert.Equal(new DateTimeOffset(entry.CreatedAt).ToUnixTimeMilliseconds(), entryDTO.CreatedAt);
        Assert.Equal(new DateTimeOffset(entry.UpdatedAt).ToUnixTimeMilliseconds(), entryDTO.UpdatedAt);
        Assert.Equal(entry.Published, entryDTO.Published);
    }
}
