namespace Shared.Logging
{
    /// <summary>
    /// Interface for logging service to abstract logging implementations.
    /// </summary>
    public interface ILoggerService
    {
        void LogInfo(string message);
        void LogError(string message);
    }
}

