using LudusGestao.Domain.Entities.geral;
using LudusGestao.Domain.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LudusGestao.Domain.Interfaces.Repositories.geral
{
    public interface IGrupoPermissaoFilialRepository : IBaseRepository<GrupoPermissaoFilial>
    {
        Task<GrupoPermissaoFilial?> ObterPorGrupoEFilialAsync(Guid grupoId, Guid filialId);
        Task<IEnumerable<GrupoPermissaoFilial>> ObterPorGrupoAsync(Guid grupoId);
        Task<IEnumerable<Guid>> ObterFiliaisPorGrupoAsync(Guid grupoId);
        Task<IEnumerable<GrupoPermissaoFilial>> ObterPorFilialAsync(Guid filialId);
    }
} 