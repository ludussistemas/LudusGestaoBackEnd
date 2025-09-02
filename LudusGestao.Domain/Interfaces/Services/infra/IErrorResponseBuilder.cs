namespace LudusGestao.Domain.Interfaces.Services
{
    public interface IErrorResponseBuilder
    {
        Task BuildErrorResponseAsync(object context, int statusCode, string message);
    }
}
