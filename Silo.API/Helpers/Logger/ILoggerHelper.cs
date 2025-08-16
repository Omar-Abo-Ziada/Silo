﻿
namespace Silo.API.Helpers.Logger;

public interface ILoggerHelper<TCategoryName>
{
    void LogCritical(Exception exception, string message, params object[] args);
    void LogCritical(string message, params object[] args);
    void LogDebug(string message, params object[] args);
    void LogError(Exception exception, string message, params object[] args);
    void LogError(string message, params object[] args);
    void LogInformation(string message, params object[] args);
    void LogTrace(string message, params object[] args);
    void LogWarning(string message, params object[] args);
}
