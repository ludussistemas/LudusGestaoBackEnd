using LudusGestao.Domain.DTOs.Auth;
using LudusGestao.Core.Models;

namespace LudusGestao.Domain.Interfaces.Services.geral
{
    public interface IAuthService
    {
        Task<ApiResponse<TokenDTO>> LoginAsync(LoginDTO dto);
        Task<ApiResponse<TokenDTO>> RefreshAsync(RefreshTokenDTO dto);
    }
}
