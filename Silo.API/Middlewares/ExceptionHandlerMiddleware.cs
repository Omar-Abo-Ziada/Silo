namespace Silo.API.Middlewares;

public class ExceptionHandlerMiddleware : IMiddleware
{
    private readonly ILoggerHelper<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(ILoggerHelper<ExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = GetStatusCode(exception);

        // Safe message for clients
        var errorMessage = "An unexpected error occurred. Please try again later.";

        var response = ApiResponse<object>.Failure(
            errors: [new Error(Code: statusCode, Description: errorMessage, Type: ErrorType.General)],
            statusCode: statusCode
        );

        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsJsonAsync(response);
    }

    private static HttpStatusCode GetStatusCode(Exception exception) =>
     exception switch
     {
         UnauthorizedAccessException => HttpStatusCode.Unauthorized,
         NotImplementedException => HttpStatusCode.NotImplemented,
         KeyNotFoundException => HttpStatusCode.NotFound,
         _ => HttpStatusCode.InternalServerError
     };
}
