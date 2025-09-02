using System.Net;

namespace LudusGestao.Domain.Interfaces.Services
{
    public interface IExceptionHandler
    {
        bool CanHandle(Exception exception);
        (HttpStatusCode statusCode, string message) Handle(Exception exception);
    }
}
