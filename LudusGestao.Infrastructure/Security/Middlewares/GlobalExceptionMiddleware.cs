using LudusGestao.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text.Json;

namespace LudusGestao.Infrastructure.Security.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var serviceProvider = context.RequestServices;
            var exceptionHandlers = serviceProvider.GetServices<IExceptionHandler>();

            var (statusCode, message) = GetExceptionResponse(exception, exceptionHandlers);

            context.Response.StatusCode = (int)statusCode;

            var errorResponse = new
            {
                success = false,
                message = message,
                statusCode = (int)statusCode,
                timestamp = DateTime.UtcNow
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }

        private static (HttpStatusCode statusCode, string message) GetExceptionResponse(
            Exception exception,
            IEnumerable<IExceptionHandler> handlers)
        {
            foreach (var handler in handlers)
            {
                if (handler.CanHandle(exception))
                {
                    return handler.Handle(exception);
                }
            }

            // Fallback para exceção não tratada
            return (HttpStatusCode.InternalServerError, "Ocorreu um erro interno no servidor.");
        }
    }
}
