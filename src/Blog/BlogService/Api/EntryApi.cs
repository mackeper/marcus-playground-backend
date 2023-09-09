using BlogService.Domain;
using BlogService.DTOs;
using BlogService.Infrastructure;
using BlogService.Mappers;
using Microsoft.EntityFrameworkCore;

namespace BlogService.Api;

internal sealed class EntryApi
{
    private readonly IMapper<Entry, EntryDTO> entryMapper;

    public EntryApi(IMapper<Entry, EntryDTO> entryMapper)
    {
        this.entryMapper = entryMapper;
    }

    public void AddEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/entries");
        group.MapGet("", GetEntries);
        group.MapGet("{id}", GetEntryById);
        group.MapPost("", CreateEntry);
        group.MapPut("{id}", UpdateEntry);
    }

    private async Task<IEnumerable<EntryDTO>> GetEntries(BlogDbContext db)
        => (await db.Entries!.ToListAsync()).Where(entry => !entry.IsDeleted).Select(entryMapper.Map);

    private async Task<IResult> GetEntryById(BlogDbContext db, int id)
        => await db.Entries!.FindAsync(id) is Domain.Entry entry
            ? Results.Ok(entryMapper.Map(entry))
            : Results.NotFound();

    private async Task<IResult> CreateEntry(BlogDbContext db, EntryDTO entryDTO)
    {
        var entry = entryMapper.Map(entryDTO);

        if (!entry.IsValid())
            return Results.BadRequest();

        await db.Entries!.AddAsync(entry);
        await db.SaveChangesAsync();
        return Results.Created($"/entires/{entry.Id}", entryMapper.Map(entry));
    }

    private async Task<IResult> UpdateEntry(BlogDbContext db, int id, EntryDTO entryDTO)
    {
        var existingEntry = await db.Entries!.FindAsync(id);
        if (existingEntry == null)
            return Results.NotFound();

        var updatedEntry = entryMapper.Map(entryDTO);
        existingEntry.Title = updatedEntry.Title;
        existingEntry.Content = updatedEntry.Content;
        existingEntry.Tags = updatedEntry.Tags;
        existingEntry.UpdatedAt = updatedEntry.UpdatedAt;
        existingEntry.Published = updatedEntry.Published;
        existingEntry.IsDeleted = updatedEntry.IsDeleted;

        if (!existingEntry.IsValid())
            return Results.BadRequest();

        await db.SaveChangesAsync();
        return Results.Ok();
    }
}
