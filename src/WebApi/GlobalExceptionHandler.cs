using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Last-resort HTTP exception boundary. It logs technical details while returning standard Problem Details;
/// expected not-found results remain normal return values rather than exceptions.
/// </summary>
public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    /// <summary>
    /// Converts exceptions that escaped an endpoint into RFC-style Problem Details. Validation
    /// errors are safe to expose as 400 responses; unexpected exception details are logged but
    /// deliberately hidden from clients.
    /// </summary>
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An unhandled error occurred while processing the request.");
        // ArgumentOutOfRangeException also derives from ArgumentException, so validation failures map to 400.
        var status = exception is ArgumentException ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError;
        context.Response.StatusCode = status;
        await context.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = status,
            Title = status == 400 ? "Invalid request" : "An unexpected error occurred",
            Detail = status == 400 ? exception.Message : null
        }, cancellationToken);
        return true;
    }
}
