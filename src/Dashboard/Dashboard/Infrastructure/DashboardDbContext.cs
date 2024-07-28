using Microsoft.EntityFrameworkCore;
using Dashboard.Infrastructure.Converters;
using Dashboard.Todo;
using Dashboard.MealPlan.Domain;

namespace Dashboard.Infrastructure;

internal sealed class DashboardDbContext : DbContext {
    public DbSet<Entry>? Entries { get; set; }
    public DbSet<Meal>? Meals { get; set; }
    public DbSet<Dashboard.MealPlan.Domain.MealPlan>? MealPlans { get; set; }
    public DashboardDbContext(DbContextOptions options) : base(options) { }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder
            .Properties<DateTime>()
            .HaveConversion(typeof(UtcValueConverter));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
    }
}
