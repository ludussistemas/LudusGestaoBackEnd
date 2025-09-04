using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Domain.Entities.geral.permissao;

namespace LudusGestao.Domain.Interfaces.Repositories.geral.permissao
{
    public interface IGrupoPermissaoSubmoduloAcaoRepository : IBaseRepository<GrupoPermissaoSubmoduloAcao>
    {
        Task<bool> TemPermissaoSubmoduloAsync(List<Guid> grupoIds, string submoduloNome, string acaoNome);
        Task<IEnumerable<Submodulo>> ObterSubmodulosPermitidosAsync(List<Guid> grupoIds, Guid moduloId);
    }
}


