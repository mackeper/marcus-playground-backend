using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dashboard.Infrastructure.Converters;

internal sealed class UtcValueConverter : ValueConverter<DateTime, DateTime> {
    public UtcValueConverter() : base(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc)) { }
}
