using FluentResults;
using FluentValidation.Results;

namespace TaskManager.Application.Extensions;

public static class ValidationResultExtensions
{
    public static Result ToResult(this ValidationResult validationResult)
    {
        return validationResult.IsValid
            ? Result.Ok()
            : Result.Fail(validationResult.Errors.Select(e => new BadRequestError(e.ErrorMessage)));
    }
}
