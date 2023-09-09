using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlogService.Infrastructure.Converters;

internal sealed class UtcValueConverter : ValueConverter<DateTime, DateTime>
{
    public UtcValueConverter() : base(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc)) { }
}
