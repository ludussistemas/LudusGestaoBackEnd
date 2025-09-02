using LudusGestao.Application.DTOs.Auth;
using LudusGestao.Core.Models;

namespace LudusGestao.Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<TokenDTO>> LoginAsync(LoginDTO dto);
        Task<ApiResponse<TokenDTO>> RefreshAsync(RefreshTokenDTO dto);
    }
}