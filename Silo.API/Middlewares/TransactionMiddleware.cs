namespace Silo.API.Middlewares;

public class TransactionMiddleware 
{
    private readonly RequestDelegate _next;
    private readonly ILoggerHelper<TransactionMiddleware> _logger;

    public TransactionMiddleware(RequestDelegate next, ILoggerHelper<TransactionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, GeneralDbContext dbContext)
    {
        // We only want to open transactions for requests that can change data.
        if (!IsTransactionalMethod(context.Request.Method))
        {
            await _next(context);
            return;
        }

        // Start a new transaction.
        // IApplicationDbContext is scoped per request, so this is safe.
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        _logger.LogInformation("====> Begin DB transaction for request {Method} {Path}", context.Request.Method, context.Request.Path);

        try
        {
            // Continue processing the request pipeline.
            await _next(context);
            await transaction.CommitAsync();
            _logger.LogInformation("====> Commit DB transaction for request {Method} {Path}", context.Request.Method, context.Request.Path);

        }
        catch (Exception ex)
        {
            // If any middleware in the pipeline throws an exception,
            // catch it, roll back the transaction, and then re-throw.
            _logger.LogError(ex, "====> Rollback DB transaction for request {Method} {Path} due to an unhandled exception.", context.Request.Method, context.Request.Path);
            await transaction.RollbackAsync();
            throw; // Re-throw the exception to be handled by your global error handler.
        }
    }

    private static bool IsTransactionalMethod(string method)
    {
        return HttpMethods.IsPost(method) ||
               HttpMethods.IsPut(method) ||
               HttpMethods.IsPatch(method) ||
               HttpMethods.IsDelete(method);
    }
}
