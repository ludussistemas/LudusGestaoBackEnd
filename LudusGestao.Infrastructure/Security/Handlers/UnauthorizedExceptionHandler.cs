using LudusGestao.Domain.Interfaces.Services;
using System.Net;

namespace LudusGestao.Infrastructure.Security.Handlers
{
    public class UnauthorizedExceptionHandler : IExceptionHandler
    {
        public bool CanHandle(Exception exception)
        {
            return exception is UnauthorizedAccessException;
        }

        public (HttpStatusCode statusCode, string message) Handle(Exception exception)
        {
            return (HttpStatusCode.Unauthorized, exception.Message);
        }
    }
}
