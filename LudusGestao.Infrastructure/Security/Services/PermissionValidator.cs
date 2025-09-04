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

        public async Task<bool> HasPermissionAsync(Guid userId, Guid filialId, string permission)
        {
            // Extrair módulo e ação da permissão (ex: "Configuracoes.Empresa.Visualizar")
            var parts = permission.Split('.');
            if (parts.Length != 3)
            {
                return false;
            }

            var moduloNome = parts[0];
            var submoduloNome = parts[1];
            var acaoNome = parts[2];

            // Verificar permissão no submodulo
            return await _permissaoService.TemPermissaoSubmodulo(userId, filialId, submoduloNome, acaoNome);
        }

        public async Task<bool> HasModuleAccessAsync(Guid userId, Guid filialId, string module)
        {
            // Verificar se o usuário tem acesso ao módulo (qualquer ação)
            return await _permissaoService.TemPermissaoModulo(userId, filialId, module, "Visualizar");
        }
    }
}
