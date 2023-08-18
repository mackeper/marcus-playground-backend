using SharedKernel.DDD;
using SharedKernel.Result;
using System.Runtime.CompilerServices;

namespace Domain.Todo;
public class TodoName : ValueObject
{
    public const int MaxLength = 50;
    public String Value { get; }

    public TodoName(string value)
    {
        Value = value;
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
