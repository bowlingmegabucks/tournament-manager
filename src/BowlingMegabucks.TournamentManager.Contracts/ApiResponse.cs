namespace BowlingMegabucks.TournamentManager.Contracts;

/// <summary>
/// Standard API response for successful operations.
/// </summary>
/// <typeparam name="T">Type of the data returned.</typeparam>
/// <example>
/// {
///   "data": { "id": "550e8400-e29b-41d4-a716-446655440000", "name": "Spring Championship 2025" },
///   "message": "Operation completed successfully."
/// }
/// </example>
public class ApiResponse<T>
{

    /// <summary>
    /// The data returned from the API.
    /// </summary>
    /// <example>{ "id": "550e8400-e29b-41d4-a716-446655440000", "name": "Spring Championship 2025" }</example>
    public T Data { get; set; } = default!;


    /// <summary>
    /// Optional message providing additional context.
    /// </summary>
    /// <example>Operation completed successfully.</example>
    public string? Message { get; set; }

}

/// <summary>
/// Factory methods for ApiResponse
/// </summary>
public static class ApiResponse
{
    /// <summary>
    /// Factory method for a successful response
    /// </summary>
    public static ApiResponse<T> Ok<T>(T data, string? message = null)
    {
        return new ApiResponse<T>
        {
            Data = data,
            Message = message,
        };
    }
}
