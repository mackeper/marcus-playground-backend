using BlogService.Domain;
using BlogService.Infrastructure.Converters;
using Microsoft.EntityFrameworkCore;

namespace BlogService.Infrastructure;

internal sealed class BlogDbContext : DbContext
{
    public DbSet<Entry>? Entries { get; set; }
    public BlogDbContext(DbContextOptions options) : base(options) { }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder
            .Properties<DateTime>()
            .HaveConversion(typeof(UtcValueConverter));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Entry>()
            .Property(e => e.Tags)
            .HasConversion
            (
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
            );
    }
}
