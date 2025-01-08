namespace Shared
{
    public record Error(string Code, string Description,ErrorType Type)
    {
        public static readonly Error None = new(string.Empty, string.Empty,ErrorType.None);
        public static readonly Error NullValue = new("Error.NullValue", "Null value was provided",ErrorType.Failure);
        public static Error Validation(string code,string description) => new(code, description,ErrorType.Validation);
        public static implicit operator Result(Error error) => Result.Failure(error);
    }
}
