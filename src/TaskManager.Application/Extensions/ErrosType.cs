using FluentResults;

namespace TaskManager.Application.Extensions;

public class NotFoundError : Error
{
    public NotFoundError(string message) : base(message)
    {
        Metadata.Add("ErrorType", "NotFound");
    }
}

public class BadRequestError : Error
{
    public BadRequestError(string message) : base(message)
    {
        Metadata.Add("ErrorType", "BadRequest");
    }
}