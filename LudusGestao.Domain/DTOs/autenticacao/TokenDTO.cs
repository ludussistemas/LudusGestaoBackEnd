// Arquivo criado para padronização da estrutura de DTOs 
using LudusGestao.Domain.DTOs.Usuario;

namespace LudusGestao.Domain.DTOs.Auth;

public class RefreshTokenDTO
{
    public string RefreshToken { get; set; } = string.Empty;
}

public class TokenDTO
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ExpiraEm { get; set; }
    public UsuarioDTO Usuario { get; set; } = new();
}
