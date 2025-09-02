using LudusGestao.Core.Interfaces.Controllers;

namespace LudusGestao.Core.Services
{
    public class DefaultLoggingService : ILoggingService
    {
        public virtual Task LogInformationAsync(string message, object? data = null)
        {
            // Implementação padrão - pode ser sobrescrita para usar um logger real
            Console.WriteLine($"[INFO] {message}");
            if (data != null)
            {
                Console.WriteLine($"[INFO] Data: {System.Text.Json.JsonSerializer.Serialize(data)}");
            }
            return Task.CompletedTask;
        }

        public virtual Task LogWarningAsync(string message, object? data = null)
        {
            // Implementação padrão - pode ser sobrescrita para usar um logger real
            Console.WriteLine($"[WARN] {message}");
            if (data != null)
            {
                Console.WriteLine($"[WARN] Data: {System.Text.Json.JsonSerializer.Serialize(data)}");
            }
            return Task.CompletedTask;
        }

        public virtual Task LogErrorAsync(string message, Exception? exception = null, object? data = null)
        {
            // Implementação padrão - pode ser sobrescrita para usar um logger real
            Console.WriteLine($"[ERROR] {message}");
            if (exception != null)
            {
                Console.WriteLine($"[ERROR] Exception: {exception.Message}");
                Console.WriteLine($"[ERROR] StackTrace: {exception.StackTrace}");
            }
            if (data != null)
            {
                Console.WriteLine($"[ERROR] Data: {System.Text.Json.JsonSerializer.Serialize(data)}");
            }
            return Task.CompletedTask;
        }

        public virtual Task LogDebugAsync(string message, object? data = null)
        {
            // Implementação padrão - pode ser sobrescrita para usar um logger real
            Console.WriteLine($"[DEBUG] {message}");
            if (data != null)
            {
                Console.WriteLine($"[DEBUG] Data: {System.Text.Json.JsonSerializer.Serialize(data)}");
            }
            return Task.CompletedTask;
        }
    }
}
