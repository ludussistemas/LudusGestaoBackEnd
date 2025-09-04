// Arquivo criado para padronização da estrutura de DTOs 

namespace LudusGestao.Domain.DTOs.Auth;

public class RegisterDTO
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}
