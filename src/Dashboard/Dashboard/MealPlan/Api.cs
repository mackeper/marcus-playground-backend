using Dashboard.Infrastructure;
using Dashboard.MealPlan.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.MealPlan;

public class Api {
    private readonly MealPlanMapper mealPlanMapper;

    public Api(MealPlanMapper mealPlanMapper) {
        this.mealPlanMapper = mealPlanMapper;
    }

    public void RegisterEndpoints(WebApplication app) {
        var group = app.MapGroup("/todo");
        group.MapGet("", GetMealPlans);
        group.MapPost("", CreateMealPlan);
        group.MapDelete("/{uuid}", DeleteMealPlan);
    }

    private async Task<IEnumerable<MealPlanDTOOut>> GetMealPlans(DashboardDbContext db)
        => (await db.MealPlans!.ToListAsync()).Select(mealPlanMapper.Map);

    private async Task<IResult> CreateMealPlan(DashboardDbContext db, MealPlanDTOIn mealPlanDTOIn) {
        var newEntry = mealPlanMapper.Map(mealPlanDTOIn);

        // TODO - Add validation logic here

        await db.MealPlans!.AddAsync(newEntry);
        await db.SaveChangesAsync();
        return Results.Created($"/entires/{newEntry.Uuid}", mealPlanMapper.Map(newEntry));
    }

    private async Task<IResult> DeleteMealPlan(DashboardDbContext db, Guid uuid) {
        var entry = await db.MealPlans!.FindAsync(uuid);
        if (entry == null) {
            return Results.NotFound();
        }

        db.MealPlans!.Remove(entry);
        await db.SaveChangesAsync();
        return Results.Ok();
    }
}
