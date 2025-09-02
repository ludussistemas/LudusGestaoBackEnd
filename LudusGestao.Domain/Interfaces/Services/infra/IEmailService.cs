namespace LudusGestao.Domain.Interfaces.Services.infra
{
    public interface IEmailService
    {
        Task EnviarEmailAsync(string destinatario, string assunto, string mensagem);
    }
}
