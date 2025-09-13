namespace Silo.API.Middlewares;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var _logger = context.RequestServices.GetRequiredService<ILoggerHelper<CustomExceptionHandlerMiddleware>>();

        try
        {
            await _next(context);
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
            errors: [new Error(Code: "EXCEPTIONHANDLER", Description: errorMessage, Type: ErrorType.General)],
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
