using LudusGestao.Domain.Interfaces.Services.geral;
using LudusGestao.Domain.Interfaces.Services.geral.permissao;

namespace LudusGestao.Infrastructure.Security.Services
{
    public class PermissionValidator : IPermissionValidator
    {
        private readonly IPermissaoAcessoService _permissaoService;

        public PermissionValidator(IPermissaoAcessoService permissaoService)
        {
            _permissaoService = permissaoService;
        }

        public async Task<bool> HasPermissionAsync(Guid userId, string modulo, string submodulo, string acao, Guid? filialId = null)
        {
            // Se não há filial, retornar false (não é possível verificar permissão sem filial)
            if (!filialId.HasValue)
            {
                return false;
            }

            // Verificar permissão no submodulo com filial
            return await _permissaoService.TemPermissaoSubmodulo(userId, filialId.Value, submodulo, acao);
        }

        public async Task<bool> HasModuleAccessAsync(Guid userId, string modulo, Guid? filialId = null)
        {
            // Se não há filial, retornar false (não é possível verificar acesso sem filial)
            if (!filialId.HasValue)
            {
                return false;
            }

            // Verificar se o usuário tem acesso ao módulo (qualquer ação) com filial
            return await _permissaoService.TemPermissaoModulo(userId, filialId.Value, modulo, "Visualizar");
        }
    }
}
