using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.API.Extensions;

public static class ResultToActionResultMapper
{
    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Value);

        return MapError(result.Errors);
    }

    public static IActionResult ToActionResult(this Result result)
    {
        if (result.IsSuccess)
            return new NoContentResult();

        return MapError(result.Errors);
    }

    private static ObjectResult MapError(List<IError> errors)
    {
        var firstError = errors.FirstOrDefault();
        var type = firstError?.Metadata.GetValueOrDefault("ErrorType")?.ToString();

        return type switch
        {
            "NotFound" => new NotFoundObjectResult(errors),
            "BadRequest" => new BadRequestObjectResult(errors),
            _ => new ObjectResult(errors) { StatusCode = 500 }
        };
    }
}
