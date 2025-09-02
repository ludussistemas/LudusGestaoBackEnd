using LudusGestao.Domain.Interfaces.Services.geral;

namespace LudusGestao.Application.Services
{
    public class ModuloAcessoService : IModuloAcessoService
    {
        private readonly IPermissaoVerificacaoService _permissaoVerificacaoService;

        public ModuloAcessoService(IPermissaoVerificacaoService permissaoVerificacaoService)
        {
            _permissaoVerificacaoService = permissaoVerificacaoService;
        }

        public async Task<bool> VerificarAcessoModuloAsync(Guid usuarioId, string moduloPai)
        {
            // Verificar se usuário tem acesso ao módulo pai
            var permissaoModulo = $"{moduloPai}.acesso";
            return await _permissaoVerificacaoService.VerificarPermissaoUsuarioAsync(usuarioId, permissaoModulo);
        }

        public async Task<IEnumerable<string>> ObterModulosUsuarioAsync(Guid usuarioId)
        {
            var permissoes = await _permissaoVerificacaoService.ObterPermissoesUsuarioAsync(usuarioId);
            var modulos = new HashSet<string>();

            foreach (var permissao in permissoes)
            {
                if (permissao.EndsWith(".acesso"))
                {
                    var modulo = permissao.Replace(".acesso", "");
                    modulos.Add(modulo);
                }
            }

            return modulos;
        }
    }
}
