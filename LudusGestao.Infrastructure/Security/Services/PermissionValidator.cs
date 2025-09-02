using LudusGestao.Domain.Interfaces.Services.geral;

namespace LudusGestao.Infrastructure.Security.Services
{
    public class PermissionValidator : IPermissionValidator
    {
        private readonly IPermissaoVerificacaoService _permissaoService;

        public PermissionValidator(IPermissaoVerificacaoService permissaoService)
        {
            _permissaoService = permissaoService;
        }

        public async Task<bool> HasPermissionAsync(Guid userId, string permission)
        {
            return await _permissaoService.VerificarPermissaoUsuarioAsync(userId, permission);
        }

        public async Task<bool> HasModuleAccessAsync(Guid userId, string module)
        {
            // Implementação básica - verificar se o usuário tem alguma permissão do módulo
            var permissoes = await _permissaoService.ObterPermissoesUsuarioAsync(userId);
            return permissoes.Any(p => p.StartsWith(module + "."));
        }
    }
}
