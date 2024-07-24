using Microsoft.EntityFrameworkCore;
using Dashboard.Infrastructure.Converters;

namespace Dashboard.Infrastructure;

internal sealed class DashboardDbContext : DbContext {
    public DbSet<Entry>? Entries { get; set; }
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
