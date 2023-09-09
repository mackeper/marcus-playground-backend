namespace SharedKernel.Result;
public class Error : IEquatable<Error>
{
    public static readonly Error None = new(string.Empty, string.Empty);

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public Error(int code, string message)
    {
        Code = code.ToString();
        Message = message;
    }

    public string Code { get; }
    public string Message { get; }

    public static implicit operator string(Error error) => error.Code;

    public static bool operator ==(Error? first, Error? second)
    {
        if (first is null && second is null) return true;
        if (first is null || second is null) return false;
        return first.Equals(second);
    }
    public static bool operator !=(Error? first, Error? second) => !(first == second);

    public bool Equals(Error? other) => other is not null && Code == other.Code && Message == other.Message;
    public override bool Equals(Object? obj) => obj is not null && obj is Error other && Code == other.Code && Message == other.Message;

    public override int GetHashCode() =>
        Code.GetHashCode() * 41 ^
        Message.GetHashCode() * 41;


    public static Error NotFound(string message) => new(404, message);
}
