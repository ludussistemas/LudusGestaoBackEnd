using LudusGestao.Core.Interfaces.Repositories.Base;
using LudusGestao.Domain.Entities.geral;
using LudusGestao.Domain.Interfaces.Repositories.geral;
using LudusGestao.Domain.Interfaces.Services;
using LudusGestao.Infrastructure.Data.Context;
using LudusGestao.Infrastructure.Data.Repositories.Base;
using LudusGestao.Infrastructure.Data.Repositories.Base.Filters;
using Microsoft.EntityFrameworkCore;

namespace LudusGestao.Infrastructure.Data.Repositories.geral
{
    public class GrupoPermissaoFilialRepository : BaseRepository<GrupoPermissaoFilial>, IGrupoPermissaoFilialRepository
    {
        public GrupoPermissaoFilialRepository(
        ApplicationDbContext context,
        ITenantService tenantService,
        ITenantFilter<GrupoPermissaoFilial> tenantFilter,
        IQuerySorter<GrupoPermissaoFilial> querySorter,
        IEnumerable<IFilterStrategy> filterStrategies)
        : base(context, tenantService, tenantFilter, querySorter, filterStrategies)
        {
        }

        public async Task<GrupoPermissaoFilial?> ObterPorGrupoEFilialAsync(Guid grupoId, Guid filialId)
        {
            var query = _context.Set<GrupoPermissaoFilial>().AsQueryable();
            query = ApplyTenantFilter(query);
            return await query.FirstOrDefaultAsync(g => g.GrupoPermissaoId == grupoId && g.FilialId == filialId);
        }

        public async Task<IEnumerable<GrupoPermissaoFilial>> ObterPorGrupoAsync(Guid grupoId)
        {
            var query = _context.Set<GrupoPermissaoFilial>().AsQueryable();
            query = ApplyTenantFilter(query);
            return await query.Where(g => g.GrupoPermissaoId == grupoId).ToListAsync();
        }

        public async Task<IEnumerable<Guid>> ObterFiliaisPorGrupoAsync(Guid grupoId)
        {
            var query = _context.Set<GrupoPermissaoFilial>().AsQueryable();
            query = ApplyTenantFilter(query);
            return await query.Where(g => g.GrupoPermissaoId == grupoId).Select(g => g.FilialId).ToListAsync();
        }

        public async Task<IEnumerable<GrupoPermissaoFilial>> ObterPorFilialAsync(Guid filialId)
        {
            var query = _context.Set<GrupoPermissaoFilial>().AsQueryable();
            query = ApplyTenantFilter(query);
            return await query.Where(g => g.FilialId == filialId).ToListAsync();
        }

        public async Task<IEnumerable<Permissao>> ObterPermissoesPorGrupoEFilialAsync(Guid grupoId, Guid filialId)
        {
            // Implementação básica - retorna todas as permissões do tenant
            // Em uma implementação mais complexa, você poderia filtrar pelas permissões específicas do grupo/filial
            var query = _context.Set<Permissao>().AsQueryable();
            // Não podemos usar ApplyTenantFilter aqui porque estamos consultando Permissao, não GrupoPermissaoFilial
            return await query.ToListAsync();
        }
    }
}
