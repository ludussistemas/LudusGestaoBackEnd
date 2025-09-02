using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Domain.Entities.geral;

namespace LudusGestao.Domain.Interfaces.Repositories.geral
{
    public interface IGrupoPermissaoFilialRepository : IBaseRepository<GrupoPermissaoFilial>
    {
        Task<GrupoPermissaoFilial?> ObterPorGrupoEFilialAsync(Guid grupoId, Guid filialId);
        Task<IEnumerable<GrupoPermissaoFilial>> ObterPorGrupoAsync(Guid grupoId);
        Task<IEnumerable<Guid>> ObterFiliaisPorGrupoAsync(Guid grupoId);
        Task<IEnumerable<GrupoPermissaoFilial>> ObterPorFilialAsync(Guid filialId);
        Task<IEnumerable<Permissao>> ObterPermissoesPorGrupoEFilialAsync(Guid grupoId, Guid filialId);
    }
}
