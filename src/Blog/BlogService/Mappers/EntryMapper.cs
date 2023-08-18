using BlogService.Domain;
using BlogService.DTOs;

namespace BlogService.Mappers;

internal sealed class EntryMapper : IMapper<Entry, EntryDTO>
{
    public Entry Map(EntryDTO entryDTO)
        => new(
            entryDTO.Id,
            entryDTO.Title,
            entryDTO.Content,
            entryDTO.Tags,
            DateTimeOffset.FromUnixTimeMilliseconds(entryDTO.CreatedAt).UtcDateTime,
            DateTimeOffset.FromUnixTimeMilliseconds(entryDTO.UpdatedAt).UtcDateTime,
            entryDTO.Published,
            entryDTO.IsDeleted
            );

    public EntryDTO Map(Entry entry)
        => new(
            entry.Id,
            entry.Title,
            entry.Content,
            entry.Tags,
            new DateTimeOffset(entry.CreatedAt).ToUnixTimeMilliseconds(),
            new DateTimeOffset(entry.UpdatedAt).ToUnixTimeMilliseconds(),
            entry.Published,
            entry.IsDeleted
            );
}
