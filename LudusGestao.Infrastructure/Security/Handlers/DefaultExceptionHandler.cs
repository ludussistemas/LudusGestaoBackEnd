using LudusGestao.Domain.Interfaces.Services;
using System.Net;

namespace LudusGestao.Infrastructure.Security.Handlers
{
    public class DefaultExceptionHandler : IExceptionHandler
    {
        public bool CanHandle(Exception exception)
        {
            return true; // Handler padrão para qualquer exceção não tratada
        }

        public (HttpStatusCode statusCode, string message) Handle(Exception exception)
        {
            return (HttpStatusCode.InternalServerError, "Ocorreu um erro interno no servidor.");
        }
    }
}
