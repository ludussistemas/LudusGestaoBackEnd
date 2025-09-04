// Arquivo criado para padronização da estrutura de DTOs 

namespace LudusGestao.Domain.DTOs.Auth;

public class LoginDTO
{
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}
