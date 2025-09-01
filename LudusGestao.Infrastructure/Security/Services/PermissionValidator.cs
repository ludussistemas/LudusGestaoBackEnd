using System;
using System.Threading.Tasks;
using LudusGestao.Domain.Interfaces.Services;

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
            return await _permissaoService.UsuarioTemPermissaoAsync(userId, permission);
        }

        public async Task<bool> HasModuleAccessAsync(Guid userId, string module)
        {
            return await _permissaoService.UsuarioTemAcessoModuloAsync(userId, module);
        }
    }
}
