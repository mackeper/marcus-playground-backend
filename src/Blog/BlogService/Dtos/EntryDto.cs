namespace BlogService.DTOs;
public record struct EntryDTO(
    int Id,
    string Title,
    string Content,
    string[] Tags,
    long CreatedAt,
    long UpdatedAt,
    bool Published,
    bool IsDeleted
    ) : IDto;
