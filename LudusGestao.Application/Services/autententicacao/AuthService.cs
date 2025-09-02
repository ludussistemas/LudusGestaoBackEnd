using AutoMapper;
using LudusGestao.Application.Common.Interfaces;
using LudusGestao.Application.DTOs.Auth;
using LudusGestao.Application.DTOs.Usuario;
using LudusGestao.Core.Models;
using LudusGestao.Domain.Interfaces.Repositories.geral;
using Microsoft.Extensions.Logging;
using DomainAuth = LudusGestao.Domain.Interfaces.Services.autenticacao.IAuthService;

namespace LudusGestao.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly DomainAuth _authService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthService> _logger;
        private readonly AuthOptions _authOptions;

        public AuthService(DomainAuth authService, IUsuarioRepository usuarioRepository, IMapper mapper, ILogger<AuthService> logger, Microsoft.Extensions.Options.IOptions<AuthOptions> authOptions)
        {
            _authService = authService;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _logger = logger;
            _authOptions = authOptions?.Value ?? new AuthOptions();
        }

        public async Task<ApiResponse<TokenDTO>> LoginAsync(LoginDTO dto)
        {
            var usuario = await _usuarioRepository.ObterPorEmailGlobal(dto.Email);
            if (usuario == null)
                return new ApiResponse<TokenDTO>(null) { Success = false, Message = "Usuário ou senha inválidos" };

            var senhaValida = await _authService.ValidarSenhaAsync(usuario, dto.Senha);
            if (!senhaValida)
                return new ApiResponse<TokenDTO>(null) { Success = false, Message = "Usuário ou senha inválidos" };

            var accessToken = await _authService.GerarTokenAsync(usuario);
            var refreshToken = await _authService.GerarRefreshTokenAsync(usuario);

            // Mapear usuário para DTO
            var usuarioDto = _mapper.Map<UsuarioDTO>(usuario);

            var tokenDto = new TokenDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiraEm = DateTime.UtcNow.AddHours(_authOptions.AccessTokenHours),
                Usuario = usuarioDto
            };

            return new ApiResponse<TokenDTO>(tokenDto, "Login realizado com sucesso");
        }

        public async Task<ApiResponse<TokenDTO>> RefreshAsync(RefreshTokenDTO dto)
        {
            _logger.LogInformation("[AuthAppService] Iniciando refresh com token: {TokenPrefix}...", dto.RefreshToken.Substring(0, Math.Min(20, dto.RefreshToken.Length)));

            if (await _authService.RefreshTokenFoiUsadoAsync(dto.RefreshToken))
            {
                _logger.LogWarning("[AuthAppService] Refresh token já foi utilizado");
                return new ApiResponse<TokenDTO>(null) { Success = false, Message = "Refresh token já foi utilizado" };
            }

            var tokenValido = _authService.ValidarRefreshToken(dto.RefreshToken);
            _logger.LogInformation("[AuthAppService] Validação do refresh token: {TokenValido}", tokenValido);

            if (!tokenValido)
            {
                _logger.LogWarning("[AuthAppService] Refresh token inválido ou expirado");
                return new ApiResponse<TokenDTO>(null) { Success = false, Message = "Refresh token inválido ou expirado" };
            }

            var usuario = await _authService.ObterUsuarioDoRefreshTokenAsync(dto.RefreshToken);
            if (usuario == null)
            {
                _logger.LogWarning("[AuthAppService] Usuário não encontrado do refresh token");
                return new ApiResponse<TokenDTO>(null) { Success = false, Message = "Refresh token inválido ou expirado" };
            }

            _logger.LogInformation("[AuthAppService] Usuário encontrado: {Email}, invalidando token antigo", usuario.Email);
            await _authService.InvalidarRefreshTokenAsync(dto.RefreshToken);

            var novoAccessToken = await _authService.GerarTokenAsync(usuario);
            var novoRefreshToken = await _authService.GerarRefreshTokenAsync(usuario);

            // Mapear usuário para DTO
            var usuarioDto = _mapper.Map<UsuarioDTO>(usuario);

            var tokenDto = new TokenDTO
            {
                AccessToken = novoAccessToken,
                RefreshToken = novoRefreshToken,
                ExpiraEm = DateTime.UtcNow.AddHours(_authOptions.AccessTokenHours),
                Usuario = usuarioDto
            };

            _logger.LogInformation("[AuthAppService] Token renovado com sucesso");
            return new ApiResponse<TokenDTO>(tokenDto, "Token renovado com sucesso");
        }
    }
}