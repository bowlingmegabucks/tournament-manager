using BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;
using ErrorOr;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BowlingMegabucks.TournamentManager.Api;

internal static class ApiExtensions
{
    /// <summary>
    /// Converts a list of errors to an appropriate HTTP problem details response.
    /// Validation errors include an errors collection, while other errors use the detail field.
    /// </summary>
    /// <param name="errors">The list of errors to convert.</param>
    /// <returns>An IResult representing the problem details response.</returns>
    public static IResult ToProblemDetails(this List<Error> errors)
    {
        if (errors.Count == 0)
        {
            return Results.Problem();
        }

        // Check if any errors are validation errors
        bool hasValidationErrors = errors.Exists(error => error.Type == ErrorType.Validation);

        if (hasValidationErrors)
        {
            return CreateValidationProblemDetails(errors);
        }

        // For non-validation errors, use the first error's information
        Error firstError = errors[0];
        int statusCode = firstError.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Results.Problem(
            detail: firstError.Description,
            statusCode: statusCode,
            title: GetErrorTitle(firstError.Type));
    }

    /// <summary>
    /// Creates a validation problem details response with individual errors mapped to an errors collection.
    /// </summary>
    /// <param name="errors">The list of errors, which may include both validation and non-validation errors.</param>
    /// <returns>A validation problem details response.</returns>
    private static IResult CreateValidationProblemDetails(List<Error> errors)
    {
        var problemDetails = new ValidationProblemDetails
        {
            Title = "One or more validation errors occurred.",
            Detail = "Please check the errors collection for specific validation failures.",
            Status = StatusCodes.Status400BadRequest,
        };

        // Group errors by their code/field for better organization
        var validationErrors = errors
            .Where(error => error.Type == ErrorType.Validation)
            .GroupBy(error => error.Code, StringComparer.Ordinal)
            .ToDictionary(
                group => group.Key,
                group => group.Select(error => error.Description).ToArray(),
                StringComparer.Ordinal);

        foreach (KeyValuePair<string, string[]> kvp in validationErrors)
        {
            problemDetails.Errors.Add(kvp.Key, kvp.Value);
        }

        // If there are non-validation errors mixed in, add them as additional context
        var nonValidationErrors = errors.Where(error => error.Type != ErrorType.Validation).ToList();
        if (nonValidationErrors.Count > 0)
        {
            problemDetails.Extensions.Add("additionalErrors", nonValidationErrors.Select(e => new
            {
                code = e.Code,
                description = e.Description,
                type = e.Type.ToString(),
            }));
        }

        return Results.ValidationProblem(problemDetails.Errors, problemDetails.Detail, problemDetails.Title);
    }

    /// <summary>
    /// Gets an appropriate title based on the error type.
    /// </summary>
    /// <param name="errorType">The type of error.</param>
    /// <returns>A human-readable title for the error type.</returns>
    private static string GetErrorTitle(ErrorType errorType) => errorType switch
    {
        ErrorType.NotFound => "Resource not found",
        ErrorType.Conflict => "Conflict occurred",
        ErrorType.Unauthorized => "Unauthorized access",
        ErrorType.Forbidden => "Access forbidden",
        ErrorType.Validation => "Validation failed",
        _ => "An error occurred",
    };

    internal static OffsetPaginationApiResponse<T> ToApiResponse<T>(
        this OffsetPaginationQueryResponse<T> queryResponse)
        => new()
        {
            TotalItems = queryResponse.TotalItems,
            TotalPages = queryResponse.TotalPages,
            CurrentPage = queryResponse.CurrentPage,
            PageSize = queryResponse.PageSize,
            Items = queryResponse.Items,
        };

    internal static OffsetPaginationApiResponse<TDestination> ConvertValues<TSource, TDestination>(this OffsetPaginationApiResponse<TSource> response, Func<TSource, TDestination> converter)
        => new()
        {
            TotalItems = response.TotalItems,
            TotalPages = response.TotalPages,
            CurrentPage = response.CurrentPage,
            PageSize = response.PageSize,
            Items = response.Items.Select(converter).ToList().AsReadOnly(),
        };
    internal static BadRequest<ValidationProblemDetails> InvalidId(string message, string id)
        => TypedResults.BadRequest(new ValidationProblemDetails
        {
            Title = "Invalid ID format",
            Detail = $"The provided ID could not be parsed: {id}",
            Status = StatusCodes.Status400BadRequest,
            Errors = new Dictionary<string, string[]>(StringComparer.Ordinal)
            {
                { "id", [ message ] },
            },
        });
}
