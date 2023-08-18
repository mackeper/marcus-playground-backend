namespace SharedKernel.DDD;
public abstract class ValueObject : IEquatable<ValueObject>
{
    public bool Equals(ValueObject? other) => other is not null && ValuesAreEqual(other);
    public override bool Equals(object? obj) => obj is ValueObject valueObject && ValuesAreEqual(valueObject);
    public override int GetHashCode() => GetAtomicValues().Aggregate(default(int), HashCode.Combine);
    public abstract IEnumerable<object> GetAtomicValues();
    private bool ValuesAreEqual(ValueObject other) => GetAtomicValues().SequenceEqual(other.GetAtomicValues());
}
