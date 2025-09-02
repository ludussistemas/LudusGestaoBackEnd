namespace LudusGestao.Core.Interfaces.Controllers
{
    public interface ILoggingService
    {
        Task LogInformationAsync(string message, object? data = null);
        Task LogWarningAsync(string message, object? data = null);
        Task LogErrorAsync(string message, Exception? exception = null, object? data = null);
        Task LogDebugAsync(string message, object? data = null);
    }
}
