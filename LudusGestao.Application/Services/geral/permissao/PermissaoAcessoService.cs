using LudusGestao.Domain.DTOs.geral.permissao;
using LudusGestao.Domain.Interfaces.Repositories.geral.permissao;
using LudusGestao.Domain.Interfaces.Services.geral.permissao;

namespace LudusGestao.Application.Services.geral.permissao
{
    public class PermissaoAcessoService : IPermissaoAcessoService
    {
        private readonly IUsuarioFilialGrupoRepository _usuarioFilialGrupoRepository;
        private readonly IGrupoPermissaoModuloAcaoRepository _grupoPermissaoModuloAcaoRepository;
        private readonly IGrupoPermissaoSubmoduloAcaoRepository _grupoPermissaoSubmoduloAcaoRepository;
        private readonly IModuloRepository _moduloRepository;
        private readonly ISubmoduloRepository _submoduloRepository;

        public PermissaoAcessoService(
            IUsuarioFilialGrupoRepository usuarioFilialGrupoRepository,
            IGrupoPermissaoModuloAcaoRepository grupoPermissaoModuloAcaoRepository,
            IGrupoPermissaoSubmoduloAcaoRepository grupoPermissaoSubmoduloAcaoRepository,
            IModuloRepository moduloRepository,
            ISubmoduloRepository submoduloRepository)
        {
            _usuarioFilialGrupoRepository = usuarioFilialGrupoRepository;
            _grupoPermissaoModuloAcaoRepository = grupoPermissaoModuloAcaoRepository;
            _grupoPermissaoSubmoduloAcaoRepository = grupoPermissaoSubmoduloAcaoRepository;
            _moduloRepository = moduloRepository;
            _submoduloRepository = submoduloRepository;
        }

        public async Task<bool> TemPermissaoModulo(Guid usuarioId, Guid filialId, string moduloNome, string acaoNome)
        {
            var gruposIds = await _usuarioFilialGrupoRepository.ObterGruposDoUsuarioNaFilialAsync(usuarioId, filialId);
            return await _grupoPermissaoModuloAcaoRepository.TemPermissaoModuloAsync(gruposIds, moduloNome, acaoNome);
        }

        public async Task<bool> TemPermissaoSubmodulo(Guid usuarioId, Guid filialId, string submoduloNome, string acaoNome)
        {
            var gruposIds = await _usuarioFilialGrupoRepository.ObterGruposDoUsuarioNaFilialAsync(usuarioId, filialId);
            return await _grupoPermissaoSubmoduloAcaoRepository.TemPermissaoSubmoduloAsync(gruposIds, submoduloNome, acaoNome);
        }

        public async Task<object> MontarMenu(Guid usuarioId, Guid filialId)
        {
            var gruposIds = await _usuarioFilialGrupoRepository.ObterGruposDoUsuarioNaFilialAsync(usuarioId, filialId);
            
            var modulosPermitidos = await _grupoPermissaoModuloAcaoRepository.ObterModulosPermitidosAsync(gruposIds);
            
            var menu = new MenuDto
            {
                Modulos = new List<ModuloMenuDto>()
            };

            foreach (var modulo in modulosPermitidos)
            {
                var submodulosPermitidos = await _grupoPermissaoSubmoduloAcaoRepository.ObterSubmodulosPermitidosAsync(gruposIds, modulo.Id);
                
                var moduloMenu = new ModuloMenuDto
                {
                    Id = modulo.Id,
                    Nome = modulo.Nome,
                    Submodulos = submodulosPermitidos.Select(s => new SubmoduloMenuDto
                    {
                        Id = s.Id,
                        Nome = s.Nome
                    }).ToList()
                };

                menu.Modulos.Add(moduloMenu);
            }

            return menu;
        }

        public async Task<bool> UsuarioTemAcessoAFilial(Guid usuarioId, Guid filialId)
        {
            var gruposIds = await _usuarioFilialGrupoRepository.ObterGruposDoUsuarioNaFilialAsync(usuarioId, filialId);
            return gruposIds != null && gruposIds.Any();
        }
    }
}
