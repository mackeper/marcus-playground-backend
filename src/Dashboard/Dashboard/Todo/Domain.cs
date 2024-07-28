namespace Dashboard.Todo;

public class Entry {
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Content { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? CompletedAt { get; set; }
    public bool IsCompleted { get; set; }

    public Entry(Guid id, string title, string content, DateTime createdAt, DateTime? completedAt, bool isCompleted) {
        Id = id;
        Title = title;
        Content = content;
        CreatedAt = createdAt;
        CompletedAt = completedAt;
        IsCompleted = isCompleted;
    }
}

public record EntryDTOOut(Guid Id, string Title, string Content, long CreatedAt, long? CompletedAt, bool IsCompleted);
public record EntryDTOIn(string Title, string Content);

public class EntryMapper {
    public Entry Map(EntryDTOOut entryDTO)
        => new(
            entryDTO.Id,
            entryDTO.Title,
            entryDTO.Content,
            DateTimeOffset.FromUnixTimeMilliseconds(entryDTO.CreatedAt).UtcDateTime,
            entryDTO.CompletedAt.HasValue
                ? DateTimeOffset.FromUnixTimeMilliseconds(entryDTO.CompletedAt.Value).UtcDateTime
                : null,
            entryDTO.IsCompleted);

    public EntryDTOOut Map(Entry entry)
        => new(
            entry.Id,
            entry.Title,
            entry.Content,
            new DateTimeOffset(entry.CreatedAt).ToUnixTimeMilliseconds(),
            entry.CompletedAt.HasValue
                ? new DateTimeOffset(entry.CompletedAt.Value).ToUnixTimeMilliseconds()
                : null,
            entry.IsCompleted);

    public Entry Map(EntryDTOIn entryDTO)
        => new(
            Guid.NewGuid(),
            entryDTO.Title,
            entryDTO.Content,
            DateTime.UtcNow,
            null,
            false);
}
