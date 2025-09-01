using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Threading.Tasks;
using LudusGestao.Domain.Interfaces.Services;

namespace LudusGestao.Infrastructure.Security.Services
{
    public class ErrorResponseBuilder : IErrorResponseBuilder
    {
        public async Task BuildErrorResponseAsync(object context, int statusCode, string message)
        {
            var httpContext = (Microsoft.AspNetCore.Http.HttpContext)context;
            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/json";
            
            var errorResponse = new
            {
                success = false,
                message = message,
                statusCode = statusCode
            };
            
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
}
