using LudusGestao.Domain.Interfaces.Services.geral;
using LudusGestao.Domain.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;

namespace LudusGestao.API.Controllers.autenticacao;

[ApiController]
[Route("api/autenticacao")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("entrar")]
    public async Task<IActionResult> Entrar([FromBody] LoginDTO dto)
    {
        var result = await _authService.LoginAsync(dto);
        if (!result.Success)
            return Unauthorized(result);
        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenDTO dto)
    {
        var result = await _authService.RefreshAsync(dto);
        if (!result.Success)
            return Unauthorized(result);
        return Ok(result);
    }
}
