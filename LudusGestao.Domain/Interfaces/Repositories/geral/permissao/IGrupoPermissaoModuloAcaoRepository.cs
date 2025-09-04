using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Domain.Entities.geral.permissao;

namespace LudusGestao.Domain.Interfaces.Repositories.geral.permissao
{
    public interface IGrupoPermissaoModuloAcaoRepository : IBaseRepository<GrupoPermissaoModuloAcao>
    {
        Task<bool> TemPermissaoModuloAsync(List<Guid> grupoIds, string moduloNome, string acaoNome);
        Task<IEnumerable<Modulo>> ObterModulosPermitidosAsync(List<Guid> grupoIds);
    }
}


