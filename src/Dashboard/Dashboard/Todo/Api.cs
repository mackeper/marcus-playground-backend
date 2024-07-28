using Dashboard.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Todo;

public class Api {
    private readonly EntryMapper entryMapper;

    public Api(EntryMapper entryMapper) {
        this.entryMapper = entryMapper;
    }

    public void RegisterEndpoints(WebApplication app) {
        var group = app.MapGroup("/todo");
        group.MapGet("", GetEntries);
        group.MapPost("", CreateEntry);
        group.MapPut("/{uuid}/complete", CompleteEntry);
        group.MapPut("/{uuid}/uncomplete", UncompleteEntry);
        group.MapDelete("/{uuid}", DeleteEntry);
    }

    private async Task<IEnumerable<EntryDTOOut>> GetEntries(DashboardDbContext db)
        => (await db.Entries!.ToListAsync()).Select(entryMapper.Map);

    private async Task<IResult> CreateEntry(DashboardDbContext db, EntryDTOIn entryDTOIn) {
        var newEntry = entryMapper.Map(entryDTOIn);

        // TODO - Add validation logic here

        await db.Entries!.AddAsync(newEntry);
        await db.SaveChangesAsync();
        return Results.Created($"/entires/{newEntry.Id}", entryMapper.Map(newEntry));
    }

    private async Task<IResult> CompleteEntry(DashboardDbContext db, Guid uuid) {
        var entry = await db.Entries!.FindAsync(uuid);
        if (entry == null) {
            return Results.NotFound();
        }

        entry.IsCompleted = true;
        entry.CompletedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();
        return Results.Ok();
    }

    private async Task<IResult> UncompleteEntry(DashboardDbContext db, Guid uuid) {
        var entry = await db.Entries!.FindAsync(uuid);
        if (entry == null) {
            return Results.NotFound();
        }

        entry.IsCompleted = false;
        entry.CompletedAt = null;

        await db.SaveChangesAsync();
        return Results.Ok();
    }

    private async Task<IResult> DeleteEntry(DashboardDbContext db, Guid uuid) {
        var entry = await db.Entries!.FindAsync(uuid);
        if (entry == null) {
            return Results.NotFound();
        }

        db.Entries!.Remove(entry);
        await db.SaveChangesAsync();
        return Results.Ok();
    }
}
