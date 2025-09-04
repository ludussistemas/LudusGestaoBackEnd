using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Domain.Entities.geral.permissao;
using LudusGestao.Domain.Interfaces.Repositories.geral.permissao;
using LudusGestao.Domain.Interfaces.Services;
using LudusGestao.Domain.Interfaces.Services.infra;
using LudusGestao.Infrastructure.Data.Context;
using LudusGestao.Infrastructure.Data.Repositories.Base;
using LudusGestao.Infrastructure.Data.Repositories.Base.Filters;
using Microsoft.EntityFrameworkCore;

namespace LudusGestao.Infrastructure.Data.Repositories.geral.permissao
{
    public class UsuarioFilialGrupoRepository : BaseRepository<UsuarioFilialGrupo>, IUsuarioFilialGrupoRepository
    {
        public UsuarioFilialGrupoRepository(
            ApplicationDbContext context,
            ITenantService tenantService,
            ITenantFilter<UsuarioFilialGrupo> tenantFilter,
            IQuerySorter<UsuarioFilialGrupo> querySorter,
            IEnumerable<IFilterStrategy> filterStrategies) 
            : base(context, tenantService, tenantFilter, querySorter, filterStrategies)
        {
        }

        public async Task<List<Guid>> ObterGruposDoUsuarioNaFilialAsync(Guid usuarioId, Guid filialId)
        {
            return await _context.UsuariosFiliaisGrupos
                .Where(ufg => ufg.UsuarioId == usuarioId && ufg.FilialId == filialId)
                .Select(ufg => ufg.GrupoId)
                .ToListAsync();
        }
    }
}


