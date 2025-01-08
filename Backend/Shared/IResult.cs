namespace Shared;

public interface IResult
{
    bool IsSuccess { get; }
    List<Error> Errors { get; }
}