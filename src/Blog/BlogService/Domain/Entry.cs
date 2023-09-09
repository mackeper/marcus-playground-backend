namespace BlogService.Domain;

internal sealed class Entry : IEntity
{
    public int Id { get; private set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string[] Tags { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool Published { get; set; }
    public bool IsDeleted { get; set; }

    public Entry(int id, string title, string content, string[] tags, DateTime createdAt, DateTime updatedAt, bool published, bool isDeleted)
    {
        Id = id;
        Title = title;
        Content = content;
        Tags = tags;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Published = published;
        IsDeleted = isDeleted;
    }

    public bool IsValid()
        => Title != null
        && Content != null
        && Tags != null
        && CreatedAt.Kind == DateTimeKind.Utc
        && CreatedAt >= DateTimeOffset.UnixEpoch.UtcDateTime
        && UpdatedAt.Kind == DateTimeKind.Utc
        && UpdatedAt >= DateTimeOffset.UnixEpoch.UtcDateTime;

    public bool IsEqual(Entry other)
        => Id == other.Id
        && Title == other.Title
        && Content == other.Content
        && Tags == other.Tags
        && CreatedAt == other.CreatedAt
        && UpdatedAt == other.UpdatedAt
        && Published == other.Published
        && IsDeleted == other.IsDeleted;
}
