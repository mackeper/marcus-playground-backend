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
}
