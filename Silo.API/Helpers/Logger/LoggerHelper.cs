namespace Silo.API.Helpers.Logger;

public class LoggerHelper<TCategoryName>(ILogger<TCategoryName> logger) : ILoggerHelper<TCategoryName>
{
    private readonly ILogger<TCategoryName> _logger = logger;

    public void LogInformation(string message, params object[] args) => _logger.LogInformation($"[{DateTime.UtcNow}] {message}", args);
    public void LogWarning(string message, params object[] args) => _logger.LogWarning($"[{DateTime.UtcNow}] {message}", args);
    public void LogError(string message, params object[] args) => _logger.LogError($"[{DateTime.UtcNow}] {message}", args);
    public void LogError(Exception exception, string message, params object[] args) => _logger.LogError(exception, $"[{DateTime.UtcNow}] {message}", args);
    public void LogDebug(string message, params object[] args) => _logger.LogDebug($"[{DateTime.UtcNow}] {message}", args);
    public void LogTrace(string message, params object[] args) => _logger.LogTrace($"[{DateTime.UtcNow}] {message}", args);
    public void LogCritical(string message, params object[] args) => _logger.LogCritical($"[{DateTime.UtcNow}] {message}", args);
    public void LogCritical(Exception exception, string message, params object[] args) => _logger.LogCritical(exception, $"[{DateTime.UtcNow}] {message}", args);
}
